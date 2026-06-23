using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HealthModuleData
    {
        [BsonElement("maxHealth")]
        public string MaxHealth { get; set; } = string.Empty;

        [BsonElement("maxRegenHealth")]
        public string MaxRegenHealth { get; set; } = string.Empty;

        [BsonElement("repairCost")]
        public string RepairCost { get; set; } = string.Empty;
    }
}
