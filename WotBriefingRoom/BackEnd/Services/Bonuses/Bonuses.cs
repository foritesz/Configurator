//using BackEnd.Models.BonusModels;
using MongoDB.Driver;
using System.Globalization;
using System.Xml.Linq;
using BonusModels.BonusModels;
using BackEnd.Services.MongoDb;

namespace BackEnd.Services.Bonuses
{
    public class Bonuses:IBonuses
    {
        private readonly MongoDbContext _db;
        private readonly string _basePath;
        private readonly ILogger<Bonuses> _logger;

        public Bonuses(MongoDbContext db, string basePath, ILogger<Bonuses> logger)
        {
            _db = db;
            _basePath = basePath;
            _logger = logger;

        }

        // Fő belépési pont – mindent lefuttat
        public async Task ImportAll() //async mert asnyc metodust használ
        {
            await SafeRun(() =>
            {
                var items = ParseVehicleEquipments(Path.Combine(_basePath, "vehicle_equipments.xml"));
                UpsertMany(_db.VehicleConsumables, items, x => x.Id);
            }, "vehicle_equipments.xml");

            await SafeRun(() =>
            {
                var items = ParsePerks(Path.Combine(_basePath, "perks.xml"));
                UpsertMany(_db.Skills, items, x => x.Id);
            }, "perks.xml");

            await SafeRun(() =>
            {
                var all = new List<Equipments>();
                AddDevicesIfExists(all, "modernized_devices.xml", "modernized_devices");
                AddDevicesIfExists(all, "deluxe_devices.xml", "deluxe_devices");
                AddDevicesIfExists(all, "tiers_devices.xml", "tiers_devices");
                AddDevicesIfExists(all, "trophy_devices.xml", "trophy_devices");

                if (all.Count > 0)
                    UpsertMany(_db.VehicleEquipments, all, x => x._Id);
            }, "devices_all (modernized/deluxe/tiers/trophy)");

            await SafeRun(() =>
            {
                var merged = MergeModificationsWithTrees(
                    Path.Combine(_basePath, "modifications.xml"),
                    Path.Combine(_basePath, "trees.xml"));

                // LocName üres elemek kiszűrése mentés előtt
                var filtered = merged.Where(m => !string.IsNullOrWhiteSpace(m.LocName)).ToList();
                UpsertMany(_db.ModificationsTrees, filtered, x => x.Id);
            }, "modifications_trees");

        }

        // ================== PARSERS ==================

        private List<Consumables> ParseVehicleEquipments(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(path);
            var doc = XDocument.Load(path);
            var list = new List<Consumables>();

            foreach (var item in doc.Root.Elements())
            {
                var e = new Consumables();
                e.Id = ParseInt(Get(item, "id"));
                e.Description = Get(item, "description");
                e.Price = ParseInt(Get(item, "price"));
                e.Improved = Get(item, "improved");
                e.Tags = Get(item, "tags");

                var script = item.Element("script");
                e.ScriptFactors = new List<NameValue>();
                e.ScriptSimple = new List<NameValue>();
                if (script != null)
                {
                    foreach (var f in script.Descendants("factor"))
                        e.ScriptFactors.Add(MakeNV(Get(f, "attribute"), Get(f, "valueByLevel")));

                    string[] simple = {
                    "cooldownSeconds","reuseCount","crewLevelIncrease","bonusValue",
                    "stunResistanceEffect","stunResistanceDuration","enginePowerFactor",
                    "turretRotationSpeedFactor","autoactivate","deploySeconds","consumeSeconds",
                    "rechargeSeconds","maxSpeedFactor","engineHpLossPerSecond"
                };
                    for (int i = 0; i < simple.Length; i++)
                    {
                        var n = script.Element(simple[i]);
                        if (n != null) e.ScriptSimple.Add(MakeNV(simple[i], n.Value.Trim()));
                    }
                }

                var kpi = item.Element("kpi");
                e.KpiMul = new List<NameValue>();
                if (kpi != null)
                {
                    foreach (var m in kpi.Elements("mul"))
                        e.KpiMul.Add(MakeNV(Get(m, "name"), Get(m, "value")));
                }

                list.Add(e);
            }
            return list;
        }

        private List<CrewSkills> ParsePerks(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(path);
            var doc = XDocument.Load(path);
            var list = new List<CrewSkills>();

            foreach (var perk in doc.Root.Elements("perk"))
            {
                var p = new CrewSkills();
                p.Id = ParseInt(Get(perk, "id"));
                p.Args = perk.Descendants("arg")
                    .Select(a => MakeNV(Get(a, "argId"), Get(a, "value")))
                    .ToList();
                list.Add(p);
            }
            return list;
        }

        private void AddDevicesIfExists(List<Equipments> acc, string fileName, string sourceTag)
        {
            var path = Path.Combine(_basePath, fileName);
            if (!File.Exists(path)) return;

            var doc = XDocument.Load(path);
            foreach (var item in doc.Root.Elements())
            {
                var d = new Equipments();
                d.Id = ParseInt(Get(item, "id"));
                d.Source = sourceTag;
                d.Tags = Get(item, "tags");

                var priceNode = item.Element("price");
                var priceText = priceNode == null ? "" : string.Concat(priceNode.Nodes().OfType<XText>().Select(t => t.Value)).Trim();
                d.Price = ParseInt(priceText);

                d.ScriptFactors = new List<NameValue>();
                d.ScriptSimple = new List<NameValue>();
                var script = item.Element("script");
                if (script != null)
                {
                    foreach (var f in script.Descendants("factor"))
                        d.ScriptFactors.Add(MakeNV(Get(f, "attribute"), Get(f, "valueByLevel")));

                    string[] simple = {
                    "weight","trackRotateSpeedFactor","wheelRotateSpeedFactor",
                    "wheelCenterRotationFwdSpeed","trackMoveSpeedFactor","wheelMoveSpeedFactor"
                };
                    for (int i = 0; i < simple.Length; i++)
                    {
                        var n = script.Element(simple[i]);
                        if (n != null) d.ScriptSimple.Add(MakeNV(simple[i], n.Value.Trim()));
                    }
                }

                d.KpiMul = new List<NameValue>();
                d.KpiAdd = new List<NameValue>();
                var kpi = item.Element("kpi");
                if (kpi != null)
                {
                    foreach (var m in kpi.Elements("mul"))
                        d.KpiMul.Add(MakeNV(Get(m, "name"), Get(m, "value")));
                    foreach (var a in kpi.Elements("add"))
                        d.KpiAdd.Add(MakeNV(Get(a, "name"), Get(a, "value")));
                }

                acc.Add(d);
            }
        }

        private List<FiledModefication> MergeModificationsWithTrees(string modificationsPath, string treesPath)
        {
            if (!File.Exists(modificationsPath)) throw new FileNotFoundException(modificationsPath);
            if (!File.Exists(treesPath)) throw new FileNotFoundException(treesPath);

            var modDoc = XDocument.Load(modificationsPath);
            var mods = modDoc.Root.Elements()
                .Select(e => new FiledModefication
                {
                    Id = Get(e, "id"),
                    LocName = Get(e, "locName"),
                    Modifiers = (e.Element("modifiers") ?? new XElement("modifiers"))
                        .Descendants("mul")
                        .Select(m => MakeNV(Get(m, "name"), Get(m, "value")))
                        .ToList(),
                    Kpi = (e.Element("kpi") ?? new XElement("kpi"))
                        .Descendants("mul")
                        .Select(k => MakeNV(Get(k, "name"), Get(k, "value")))
                        .ToList()
                })
                .Where(m => !string.IsNullOrEmpty(m.Id))
                .ToDictionary(x => x.Id, x => x);

            var treeDoc = XDocument.Load(treesPath);

            // stepId -> (roleName, level, minVehicleLevel)
            var byStepId = new Dictionary<string, Tuple<string, byte, byte>>();
            // unlockedId -> (roleName, unlockLevel, minVehicleLevel)
            var byUnlocks = new Dictionary<string, Tuple<string, byte, byte>>();

            foreach (var role in treeDoc.Root.Elements())
            {
                string roleName = role.Name.LocalName;
                var steps = (role.Element("steps") ?? new XElement("steps")).Elements("step");
                foreach (var step in steps)
                {
                    string stepId = Get(step, "id");
                    byte level = ParseByte(Get(step, "level"));
                    byte minVehicleLevel = ParseByte(
                        ((((step.Element("vehicleFilter") ?? new XElement("vehicleFilter"))
                            .Element("include") ?? new XElement("include"))
                            .Element("vehicle") ?? new XElement("vehicle"))
                            .Element("minLevel") ?? new XElement("minLevel")).Value.Trim()
                    );

                    if (!string.IsNullOrEmpty(stepId) && !byStepId.ContainsKey(stepId))
                        byStepId[stepId] = Tuple.Create(roleName, level, minVehicleLevel);

                    var unlocks = (step.Element("unlocks") ?? new XElement("unlocks")).Value;
                    if (!string.IsNullOrWhiteSpace(unlocks))
                    {
                        var ids = unlocks.Split(new[] { ' ', '\t', '\r', '\n', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < ids.Length; i++)
                        {
                            var unlockedId = ids[i].Trim();
                            if (!byUnlocks.ContainsKey(unlockedId))
                                byUnlocks[unlockedId] = Tuple.Create(roleName, level, minVehicleLevel);
                        }
                    }
                }
            }

            foreach (var kv in mods)
            {
                var m = kv.Value;

                Tuple<string, byte, byte> hit;
                if (byStepId.TryGetValue(m.Id, out hit))
                {
                    m.TreeName = hit.Item1;
                    m.Level = hit.Item2;
                    m.MinVehicleLevel = hit.Item3;
                }

                Tuple<string, byte, byte> hitU;
                if (byUnlocks.TryGetValue(m.Id, out hitU))
                {
                    if (string.IsNullOrEmpty(m.TreeName)) m.TreeName = hitU.Item1;
                    m.UnlockLevel = hitU.Item2;
                    if (m.MinVehicleLevel == 0) m.MinVehicleLevel = hitU.Item3;
                }
            }

            return mods.Values.ToList();
        }

        // ================== HELPERS ==================

        private static void UpsertMany<T, TKey>(IMongoCollection<T> coll, IEnumerable<T> docs, Func<T, TKey> idSelector)
        {
            var models = new List<WriteModel<T>>();
            foreach (var d in docs)
            {
                var key = idSelector(d);
                var filter = Builders<T>.Filter.Eq("_id", key);
                models.Add(new ReplaceOneModel<T>(filter, d) { IsUpsert = true });
            }
            if (models.Count > 0)
                coll.BulkWrite(models, new BulkWriteOptions { IsOrdered = false });
        }

        private static string Get(XElement parent, string name)
        {
            var n = parent.Element(name);
            return n == null ? "" : (n.Value ?? "").Trim();
        }

        private static int ParseInt(string s)
        {
            int v;
            return int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out v) ? v : 0;
        }

        private static byte ParseByte(string s)
        {
            byte v;
            return byte.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out v) ? v : (byte)0;
        }

        private static double? ParseDoubleNullable(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            s = s.Replace(',', '.');
            double v;
            return double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out v) ? v : null;
        }

        private static NameValue MakeNV(string name, string raw)
        {
            var nv = new NameValue();
            nv.Name = name;
            nv.ValueRaw = raw;
            nv.ValueNum = ParseDoubleNullable(raw);
            return nv;
        }

        private async Task SafeRun(Action action, string label) // async needed to the logger
        {
            try { action(); }
            catch (FileNotFoundException) { _logger.LogWarning("[FIGYELEM] Hiányzó fájl(ok): {Label}", label); }
            catch (Exception ex) { _logger.LogError(ex, "[HIBA] {Label}", label); }
        }
    }
}

