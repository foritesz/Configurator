using DTO;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text.Json;
using DTO.TanksModels;
using BackEnd.Services.MongoDb;

namespace BackEnd.ApiClients
{
    public class TanksId : ITankService
    {
        private readonly WorldOfTanksApi _api;
        private readonly MongoDbContext _mongoDbContext; 
        private readonly ILogger<TanksId> _logger;

        public TanksId(WorldOfTanksApi api, MongoDbContext mongoDbContext, ILogger<TanksId> logger)
        {
            _api = api;
            _mongoDbContext = mongoDbContext;
        }

        public async Task<List<int>> GetTankIdsAsync()
        {
            string url = $"{WorldOfTanksApi.BaseUrl}encyclopedia/vehicles/?application_id={_api.ApplicationId}";

            var response = await _api.HttpClient.GetStringAsync(url);

            using JsonDocument doc = JsonDocument.Parse(response);
            var root = doc.RootElement;

            var tankIds = new List<int>();

            if (root.GetProperty("status").GetString() == "ok")
            {
                var data = root.GetProperty("data");

                foreach (var tankProperty in data.EnumerateObject())
                {
                    if (int.TryParse(tankProperty.Name, out int tankId))
                    {
                        tankIds.Add(tankId);
                    }
                }
            }
            return tankIds;
        }

        public async Task<Dictionary<int, JsonElement>> GetTanksDataAsync(List<int> tankIds)
        {
            if (tankIds == null || tankIds.Count == 0)
                return new Dictionary<int, JsonElement>();

            const int maxBatchSize = 100;

            var tanksData = new Dictionary<int, JsonElement>();

            for (int i = 0; i < tankIds.Count; i += maxBatchSize)
            {
                var batchIds = tankIds.Skip(i).Take(maxBatchSize);
                string idsParam = string.Join(",", batchIds);

                string url = $"{WorldOfTanksApi.BaseUrl}encyclopedia/vehicles/?application_id={_api.ApplicationId}&tank_id={idsParam}";
                var response = await _api.HttpClient.GetStringAsync(url);

                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                if (root.GetProperty("status").GetString() == "ok")
                {
                    var data = root.GetProperty("data");
                    foreach (var tankProperty in data.EnumerateObject())
                    {
                        if (int.TryParse(tankProperty.Name, out int tankId))
                        {
                            tanksData[tankId] = tankProperty.Value.Clone();
                        }
                    }
                }

            }

            return tanksData;
        }

        public async Task SaveTanksToMongoAsync(Dictionary<int, JsonElement> tanksJsonData)
        {
            static bool GetBool(JsonElement e, string name)
                => e.TryGetProperty(name, out var p) &&
                   (p.ValueKind == JsonValueKind.True || p.ValueKind == JsonValueKind.False) &&
                   p.GetBoolean();

            static string SanitizeFolderName(string name)
            {

                var noWs = new string(name.Where(c => !char.IsWhiteSpace(c)).ToArray());


                var invalid = Path.GetInvalidFileNameChars().Concat(new[] { '.' }).ToHashSet();//the folder is become a bug and cannot be deleted without this!
                var cleaned = new string(noWs.Where(c => !invalid.Contains(c)).ToArray());


                return string.IsNullOrWhiteSpace(cleaned) ? "UnknownTank" : cleaned;
            }



            var envRoot = Environment.GetEnvironmentVariable("REPOSITORIES_ROOT");


            var defaultLocalRoot = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Repositories", "TankData")
            );

            string repositoriesRoot = Path.GetFullPath(
               string.IsNullOrWhiteSpace(envRoot) ? defaultLocalRoot : envRoot
            );

            Directory.CreateDirectory(repositoriesRoot);

            var tanks = new List<TankData>();

            foreach (var (tankId, json) in tanksJsonData)
            {
                string name = json.TryGetProperty("name", out var nameProp)
                    ? nameProp.GetString() ?? "N/A"
                    : "N/A";

                string contourImage = "N/A";
                if (json.TryGetProperty("images", out var imagesEl) &&
                    imagesEl.ValueKind == JsonValueKind.Object &&
                    imagesEl.TryGetProperty("contour_icon", out var contourIconEl) &&
                    contourIconEl.ValueKind == JsonValueKind.String)
                {
                    contourImage = contourIconEl.GetString() ?? "N/A";
                }

                byte tier = json.TryGetProperty("tier", out var tierProp)
                    ? (byte)tierProp.GetInt32()
                    : (byte)0;

                string type= json.TryGetProperty("type", out var typeProp)
                ? typeProp.GetString() ?? "N/A"
                : "N/A";

                bool isPremium = GetBool(json, "is_premium");


                string safeName = SanitizeFolderName(name);
                string tankFolder = Path.Combine(repositoriesRoot, "TankData", safeName);

                string collisionPath = Path.Combine(tankFolder, "Collision");
                string modelPath = Path.Combine(tankFolder, "Model");
                string dataPath = Path.Combine(tankFolder, "Data");

                Directory.CreateDirectory(collisionPath);
                Directory.CreateDirectory(modelPath);
                Directory.CreateDirectory(dataPath);

                var tank = new TankData
                {
                    TankId = tankId,
                    Name = name,
                    Level = tier,
                    Type=type,
                    IsPremium = isPremium,
                    Regular = !isPremium,
                    ContourImage = contourImage,
                    CollisionPath = collisionPath,
                    ModelPath = modelPath,
                    DataPath = dataPath

                };

                tanks.Add(tank);

            }
       


            var collection = _mongoDbContext.Tanks;

            foreach (var tank in tanks)
            {
                var filter = Builders<TankData>.Filter.Eq(t => t.TankId, tank.TankId);
                await collection.ReplaceOneAsync(filter, tank, new ReplaceOptions { IsUpsert = true });
            }
        }


        public async Task<TankData?> GetTankProfileAsync(int tankId)
        {
            string url = $"{WorldOfTanksApi.BaseUrl}encyclopedia/vehicleprofile/?application_id={_api.ApplicationId}&tank_id={tankId}";

            try
            {
                var response = await _api.HttpClient.GetStringAsync(url);
                var json = JObject.Parse(response);

                if (json["status"]?.ToString() == "ok")
                {
                    var tank = json["data"]?[tankId.ToString()];
                    if (tank == null) return null;

                    var engine = tank["engine"];
                    var gun = tank["gun"];
                    var turret = tank["turret"];
                    var armor = tank["armor"]?["hull"];

                    var ammoList = new List<AmmoData>();

                    var ammoArray = tank["ammo"];

                    if (ammoArray != null && ammoArray is JArray ammoJArray)
                    {
                        foreach (var a in ammoJArray)
                        {
                            ammoList.Add(new AmmoData
                            {
                                Type = a["type"]?.ToString(),
                                Stun = a["stun"]?.Type == JTokenType.Null ? null : a["stun"]?.Value<int>(),

                                Penetration = a["penetration"]?.ToObject<List<int>>(),
                                Damage = a["damage"]?.ToObject<List<int>>()
                            });
                        }
                    }

                    return new TankData
                    {
                        TankId = tankId,
                        Name = turret?["name"]?.ToString() ?? "N/A",
                        EngineName = engine?["name"]?.ToString() ?? "N/A",
                        EnginePower = engine?["power"]?.Value<int>() ?? 0,
                        GunName = gun?["name"]?.ToString() ?? "N/A",
                        GunCaliber = gun?["caliber"]?.Value<int>() ?? 0,
                        FireRate = MathF.Round(gun?["fire_rate"]?.Value<float>() ?? 0f, 3),
                        HP = tank["hull_hp"]?.Value<int>() ?? 0,
                        HullHP = tank["hp"]?.Value<int>() ?? 0,
                        EngineWeight = engine?["weight"]?.Value<int>() ?? 0,
                        GunWeight = gun?["weight"]?.Value<int>() ?? 0,
                        Ammo = ammoList
                    };
                }
            }
            catch (Exception ex)
            {
                _logger?.LogInformation($"GetTankProfileAsync hiba: {ex.Message}");
            }

            return null;
        }




        public async Task SaveTankProfilesToMongoAsync(List<int> tankIds)
        {
            var collection = _mongoDbContext.Tanks;

            foreach (var id in tankIds)
            {
                var tankProfile = await GetTankProfileAsync(id);
                if (tankProfile == null) continue;

                // Csak a profil-mezőket frissítjük, nem írjuk felül az IsPremium/Regular mezőket
                var update = Builders<TankData>.Update
                    .Set(t => t.HP, tankProfile.HP)
                    .Set(t => t.HullHP, tankProfile.HullHP)
                    .Set(t => t.EngineName, tankProfile.EngineName)
                    .Set(t => t.EnginePower, tankProfile.EnginePower)
                    .Set(t => t.EngineWeight, tankProfile.EngineWeight)
                    .Set(t => t.GunName, tankProfile.GunName)
                    .Set(t => t.FireRate, tankProfile.FireRate)
                    .Set(t => t.GunCaliber, tankProfile.GunCaliber)
                    .Set(t => t.GunWeight, tankProfile.GunWeight)
                    .Set(t=>  t.Ammo, tankProfile.Ammo)
                    .SetOnInsert(t => t.TankId, id); // ha nincs meg, beszúráskor kitöltjük az ID-t

                await collection.UpdateOneAsync(
                    Builders<TankData>.Filter.Eq(t => t.TankId, id),
                    update,
                    new UpdateOptions { IsUpsert = true }
                );
            }
        }
    }
}