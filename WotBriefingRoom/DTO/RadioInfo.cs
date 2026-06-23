using MongoDB.Bson.Serialization.Attributes;

namespace DTO.TanksModels
{
    public class RadioInfo
    {
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        [BsonElement("signal_range")]
        public string SignalRange { get; set; } = string.Empty;
        [BsonElement("weight")]
        public string Weight { get; set; } = string.Empty;
    }
}
