using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TurretData
    {
        [BsonElement("armor")]
        public Dictionary<string, object> Armor { get; set; } = new();

        [BsonElement("turretRotatorHealth")]
        public HealthModuleData TurretRotatorHealth { get; set; } = new();

        [BsonElement("surveyingDeviceHealth")]
        public HealthModuleData SurveyingDeviceHealth { get; set; } = new();

        [BsonElement("circularVisionRadius")]
        public string CircularVisionRadius { get; set; } = string.Empty;
    }
}
