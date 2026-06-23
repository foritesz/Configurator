using MongoDB.Bson.Serialization.Attributes;

namespace BonusModels.BonusModels
{
    public class CrewSkills
    {
        [BsonId] public int Id { get; set; }
        public List<NameValue> Args { get; set; }
    }
}
