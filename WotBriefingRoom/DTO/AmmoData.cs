using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AmmoData
    {
        [BsonElement("penetration")]
        public List<int>?Penetration { get; set; }

        [BsonElement("damage")]
        public List<int>? Damage { get; set; }

        [BsonElement("type")]
        public string? Type { get; set; }

        [BsonElement("stun")]
        public int? Stun { get; set; } // lehet null
    }
}
