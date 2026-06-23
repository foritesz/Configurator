using MongoDB.Bson.Serialization.Attributes;

namespace DTO.TanksModels
{
    public class GunModuleData
    {
        [BsonElement("pitchLimits")]
        public PitchLimitsData PitchLimits { get; set; } = new();

        [BsonElement("armor")]
        public Dictionary<string, object> Armor { get; set; } = new();

        [BsonElement("reloadTime")]
        public string ReloadTime { get; set; } = string.Empty;

        [BsonElement("aimingTime")]
        public string AimingTime { get; set; } = string.Empty;

        [BsonElement("invisibilityFactorAtShot")]
        public string InvisibilityFactorAtShot { get; set; } = string.Empty;

        [BsonElement("turretYawLimits")]
        public string TurretYawLimits { get; set; } = string.Empty;

        [BsonElement("shotDispersionRadius")]
        public string ShotDispersionRadius { get; set; } = string.Empty;

        [BsonElement("shotDispersionFactors")]
        public GunShotDispersionFactorsData ShotDispersionFactors { get; set; } = new();
    }

    public class PitchLimitsData
    {
        [BsonElement("minPitch")]
        public string MinPitch { get; set; } = string.Empty;

        [BsonElement("maxPitch")]
        public string MaxPitch { get; set; } = string.Empty;
    }

    public class GunShotDispersionFactorsData
    {
        [BsonElement("afterShot")]
        public string AfterShot { get; set; } = string.Empty;

        [BsonElement("turretRotation")]
        public string TurretRotation { get; set; } = string.Empty;

        [BsonElement("whileGunDamaged")]
        public string WhileGunDamaged { get; set; } = string.Empty;
    }
}
