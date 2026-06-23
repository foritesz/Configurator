using DTO.TanksModels;
using Microsoft.Extensions.Logging;
using Serilog;
using MongoDB.Bson;
using MongoDB.Driver;
using DTO.TanksModels;
using BackEnd.Services.TankMatch;
using BackEnd.Services.Xml;
using BackEnd.Services.MongoDb;

namespace BackEnd.Services.TankDataImport
{
    public class TankDataImporter
    {
        private readonly XmlParser _xmlParser;
        private readonly TankMatcher _tankMatcher;
        private readonly IMongoCollection<TankData> _tankCollection;
        private readonly ILogger<TankDataImporter> _logger;

        public TankDataImporter(XmlParser xmlParser, TankMatcher tankMatcher, MongoDbContext dbContext, ILogger<TankDataImporter> logger)
        {
            _xmlParser = xmlParser;
            _tankMatcher = tankMatcher;
            _tankCollection = dbContext.Tanks;
            _logger = logger;
        }

        public async Task ImportXmlData(string folderPath)
        {
            var matches = _tankMatcher.MatchFilesToTanks(folderPath);

            // Tanknév alapján csoportosítás (egy tankhoz több fájl is tartozhat, pl. siege)
            var groupedMatches = matches
                .GroupBy(m => m.Value)
                .Where(g => g.Key != "No match")
                .ToDictionary(g => g.Key, g => g.Select(x => x.Key).ToList());

            foreach (var kvp in groupedMatches)
            {
                string tankName = kvp.Key;
                var fileList = kvp.Value;

                foreach (var fileName in fileList)
                {
                    string filePath = Path.Combine(folderPath, $"{fileName}.xml");

                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine($"Hiányzó fájl: {filePath}");
                        continue;
                    }

                    _logger.LogInformation("Feldolgozás: {FileName} → {TankName}", fileName, tankName);

                    var parsedData = _xmlParser.ParseXmlFile(filePath);
                    bool isSiege = fileName.ToLowerInvariant().Contains("siege");

                    var update = Builders<TankData>.Update;
                    var updates = new List<UpdateDefinition<TankData>>();

                    foreach (var kv in parsedData)
                    {
                        // Siege esetén a "SiegeStats.{kulcs}" alá kerül
                        string targetKey = isSiege ? $"SiegeStats.{kv.Key}" : kv.Key;
                        updates.Add(update.Set(targetKey, kv.Value.ToBsonDocument()));
                    }

                    var combinedUpdate = update.Combine(updates);

                    await _tankCollection.UpdateOneAsync(
                        Builders<TankData>.Filter.Eq(t => t.Name, tankName),
                        combinedUpdate,
                        new UpdateOptions { IsUpsert = true }
                    );

                    _logger.LogInformation("{Mode} mentve: {TankName}", isSiege ? "Siege mód" : "Normál mód", tankName);
                }
            }
        }
    }
}
