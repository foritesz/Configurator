using MongoDB.Bson.Serialization.Attributes;

namespace DTO.TanksModels
{
    public class EngineInfo
    {
        public string Name { get; set; }
        public int Power { get; set; }
    }

    public class EngineModuleData
    {
        [BsonElement("unlocks")]
        public UnlocksData Unlocks { get; set; } = new();
    }

    public class UnlocksData
    {
        [BsonElement("engine")]
        public CostData? Engine { get; set; }

        [BsonElement("vehicle")]
        public CostData? Vehicle { get; set; }
    }

    public class CostData
    {
        [BsonElement("cost")]
        public string Cost { get; set; } = string.Empty;
    }
}
