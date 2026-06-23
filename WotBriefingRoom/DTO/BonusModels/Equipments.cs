using MongoDB.Bson.Serialization.Attributes;

namespace BonusModels.BonusModels
{
    public class Equipments
    {
        [BsonId] public string _Id { get { return Id.ToString() + "|" + Source; } }
        [BsonIgnore] public int Id { get; set; }
        public string Source { get; set; }
        public string Tags { get; set; }
        public int Price { get; set; }
        public List<NameValue> ScriptFactors { get; set; }
        public List<NameValue> ScriptSimple { get; set; }
        public List<NameValue> KpiMul { get; set; }
        public List<NameValue> KpiAdd { get; set; }
    }
}
