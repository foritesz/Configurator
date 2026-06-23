using DTO;
using FrontEnd.Services.CrewServices;

namespace FrontEnd.Services.TankCalculators
{
    public abstract class Tier11MechanicBase : TankMechanicBase
    {
        protected Tier11MechanicBase(CrewSkillService crewSkillService)
            : base(crewSkillService)
        {
        }

        public override bool CanHandle(FrontEndData tank)
        {
            return tank.Level == 11 && tank.Name == TankKey;
        }

        public abstract string DisplayUniqueMechanic(TankMechanicContext context);
    }
}
