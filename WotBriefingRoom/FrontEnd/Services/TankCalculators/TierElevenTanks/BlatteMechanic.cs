using FrontEnd.Services.CrewServices;
using System.Globalization;

namespace FrontEnd.Services.TankCalculators.TierElevenTanks
{
    public class BlatteMechanic : Tier11MechanicBase
    {
        public BlatteMechanic(CrewSkillService crewSkillService)
            : base(crewSkillService)
        {
        }

        public override string TankKey => "Blatte";

        public override string DisplayUniqueMechanic(TankMechanicContext context)
        {
            return "Blatte egyedi mechanika";
        }

        public override string DisplayDpm(TankMechanicContext context)
        {
            var baseValue = base.DisplayDpm(context);

            if (baseValue == "-")
                return "-";

            var value = double.Parse(baseValue, CultureInfo.InvariantCulture);

            return (value * 1.08).ToString("0", CultureInfo.InvariantCulture);
        }
    }
}
