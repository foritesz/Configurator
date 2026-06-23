using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DTO
{
    public class TurretDataFrontEnd
    {
        public Dictionary<string, JsonElement> Armor { get; set; } = new();
        public HealthModuleData TurretRotatorHealth { get; set; } = new();
        public HealthModuleData SurveyingDeviceHealth { get; set; } = new();
        public string CircularVisionRadius { get; set; } = string.Empty;
    }
}
