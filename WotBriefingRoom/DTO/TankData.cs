using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DTO.TanksModels
{
    [BsonIgnoreExtraElements]
    public class TankData
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int TankId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("tier")]
        public byte Level { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("is_premium")]
        public bool IsPremium { get; set; }

        [BsonElement("regular")]
        public bool Regular { get; set; }

        [BsonElement("contour_icon")]
        public string ContourImage { get; set; }

        public string CollisionPath { get; set; } = string.Empty;
        public string ModelPath { get; set; } = string.Empty;
        public string DataPath { get; set; } = string.Empty;


        public int HP { get; set; }
        public int HullHP { get; set; }
        public string EngineName { get; set; }
        public int EnginePower { get; set; }
        public int EngineWeight { get; set; }
        public string GunName { get; set; }
        public float FireRate { get; set; }
        public int GunCaliber { get; set; }
        public int GunWeight { get; set; }

        [BsonElement("ammo")]
        public List<AmmoData> Ammo { get; set; } = new();

        [BsonElement("chassis")]
        public Dictionary<string, ChassisData> Chassis { get; set; } = new();

        [BsonElement("crew")]
        public CrewData Crew { get; set; } = new();

        [BsonElement("engines")]
        public Dictionary<string, EngineModuleData> Engines { get; set; } = new();

        [BsonElement("fuelTanks")]
        public Dictionary<string, object> FuelTanks { get; set; } = new();

        [BsonElement("guns")]
        public Dictionary<string, GunModuleData> Guns { get; set; } = new();

        [BsonElement("hull")]
        public HullData Hull { get; set; } = new();

        [BsonElement("invisibility")]
        public InvisibilityData Invisibility { get; set; } = new();

        [BsonElement("radios")]
        public Dictionary<string, object> Radios { get; set; } = new();

        [BsonElement("speedLimits")]
        public SpeedLimitsData SpeedLimits { get; set; } = new();

        [BsonElement("turrets0")]
        public Dictionary<string, TurretData> Turrets0 { get; set; } = new();
    }
}