using MongoDB.Bson.Serialization.Attributes;

namespace BonusModels.BonusModels
{
    public class FiledModefication
    {
        [BsonId] public string Id { get; set; }
        public string LocName { get; set; }
        public string TreeName { get; set; }
        public byte UnlockLevel { get; set; }
        public byte Level { get; set; }
        public byte MinVehicleLevel { get; set; }
        public List<NameValue> Modifiers { get; set; }
        public List<NameValue> Kpi { get; set; }
    }
}
