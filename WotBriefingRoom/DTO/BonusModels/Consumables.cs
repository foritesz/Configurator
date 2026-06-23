using MongoDB.Bson.Serialization.Attributes;


namespace BonusModels.BonusModels
{
    public class Consumables
    {
        [BsonId] public int Id { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Improved { get; set; }
        public string Tags { get; set; }
        public List<NameValue> ScriptFactors { get; set; }
        public List<NameValue> ScriptSimple { get; set; }
        public List<NameValue> KpiMul { get; set; }
    }
}
