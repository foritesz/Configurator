using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class InvisibilityData
    {
        [BsonElement("moving")]
        public string Moving { get; set; } = string.Empty;

        [BsonElement("still")]
        public string Still { get; set; } = string.Empty;

        [BsonElement("camouflageBonus")]
        public string ComouFlageBonus { get; set; } = string.Empty;

        [BsonElement("firePenalty")]
        public string FirePenalty { get; set; } = string.Empty;
    }
}
