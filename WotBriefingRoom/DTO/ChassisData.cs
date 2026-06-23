using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChassisData
    {
        [BsonElement("armor")]
        public ChassisArmorData Armor { get; set; } = new();

        [BsonElement("weight")]
        public string Weight { get; set; } = string.Empty;

        [BsonElement("maxLoad")]
        public string MaxLoad { get; set; } = string.Empty;

        [BsonElement("terrainResistance")]
        public string TerrainResistance { get; set; } = string.Empty;

        [BsonElement("rotationSpeed")]
        public string RotationSpeed { get; set; } = string.Empty;

        [BsonElement("shotDispersionFactors")]
        public ChassisShotDispersionFactorsData ShotDispersionFactors { get; set; } = new();
    }

    public class ChassisArmorData
    {
        [BsonElement("leftTrack")]
        public string LeftTrack { get; set; } = string.Empty;

        [BsonElement("rightTrack")]
        public string RightTrack { get; set; } = string.Empty;
    }

    public class ChassisShotDispersionFactorsData
    {
        [BsonElement("vehicleMovement")]
        public string VehicleMovement { get; set; } = string.Empty;

        [BsonElement("vehicleRotation")]
        public string VehicleRotation { get; set; } = string.Empty;
    }
}
