using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HullData
    {
        [BsonElement("armor")]
        public Dictionary<string, object> Armor { get; set; } = new();

        [BsonElement("primaryArmor")]
        public string PrimaryArmor { get; set; } = string.Empty;

        [BsonElement("weight")]
        public string Weight { get; set; } = string.Empty;

        [BsonElement("ammoBayHealth")]
        public HealthModuleData AmmoBayHealth { get; set; } = new();
    }
}
