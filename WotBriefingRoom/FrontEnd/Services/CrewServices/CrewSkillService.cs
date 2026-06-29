using System.Globalization;
using BonusModels.BonusModels;

namespace FrontEnd.Services.CrewServices
{
    public class CrewSkillService
    {
        private const string BiasSkillId = "102";

        private List<CrewSkills> _crewSkills = new List<CrewSkills>();

        public void SetCrewSkills(List<CrewSkills>? crewSkills)
        {
            _crewSkills = crewSkills ?? new List<CrewSkills>();
        }

        public bool HasSkill(HashSet<string>? activeMatrixButtons, string skillId)
        {
            return activeMatrixButtons != null && activeMatrixButtons.Contains(skillId);
        }

        public CrewSkills? GetSkillById(string? skillId)
        {
            if (string.IsNullOrWhiteSpace(skillId))
                return null;

            if (!int.TryParse(skillId.Trim(), out var id))
                return null;

            return _crewSkills.FirstOrDefault(x => x.Id == id);
        }

        public double GetCrewLevelIncrease(string skillId)
        {
            var skill = GetSkillById(skillId);
            if (skill?.Args == null || skill.Args.Count == 0)
                return 0.0;

            var arg = skill.Args.FirstOrDefault(a =>
                a != null &&
                !string.IsNullOrWhiteSpace(a.Name) &&
                a.Name.Equals("crewLevelIncrease", StringComparison.OrdinalIgnoreCase));

            if (arg == null)
                return 0.0;

            if (arg.ValueNum.HasValue && arg.ValueNum.Value != 0)
                return arg.ValueNum.Value;

            if (!string.IsNullOrWhiteSpace(arg.ValueRaw) &&
                double.TryParse(arg.ValueRaw, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed))
            {
                return parsed;
            }

            return 0.0;
        }

        public double GetBiasEfficiency(HashSet<string>? activeMatrixButtons)
        {
            if (!HasSkill(activeMatrixButtons, BiasSkillId))
                return 1.0;

            var increase = GetCrewLevelIncrease(BiasSkillId);
            return 1.0 + increase;
        }

        public double GetCrewFactor(double efficiency)
        {
            return 0.57 + 0.43 * efficiency;
        }

        public double GetBiasFactor(HashSet<string>? activeMatrixButtons)
        {
            var efficiency = GetBiasEfficiency(activeMatrixButtons);
            return GetCrewFactor(efficiency);
        }

        public double? ApplyReloadTime(double? currentValue, HashSet<string>? activeMatrixButtons)
        {
            if (currentValue == null)
                return null;

            return currentValue.Value / GetBiasFactor(activeMatrixButtons);
        }

        public double? ApplyAimingTime(double? currentValue, HashSet<string>? activeMatrixButtons)
        {
            if (currentValue == null)
                return null;

            return currentValue.Value / GetBiasFactor(activeMatrixButtons);
        }

        public double? ApplyDispersion(double? currentValue, HashSet<string>? activeMatrixButtons)
        {
            if (currentValue == null)
                return null;

            return currentValue.Value / GetBiasFactor(activeMatrixButtons);
        }

        public double? ApplyRotationSpeed(double? currentValue, HashSet<string>? activeMatrixButtons)
        {
            if (currentValue == null)
                return null;

            return currentValue.Value * GetBiasFactor(activeMatrixButtons);
        }

        public double? ApplyViewRange(double? currentValue, HashSet<string>? activeMatrixButtons)
        {
            if (currentValue == null)
                return null;

            return currentValue.Value * GetBiasFactor(activeMatrixButtons);
        }

        public string FormatValue(double? value, string format = "0.00", string unit = "")
        {
            if (value == null)
                return "-";

            return value.Value.ToString(format, CultureInfo.InvariantCulture) + unit;
        }
    }
}