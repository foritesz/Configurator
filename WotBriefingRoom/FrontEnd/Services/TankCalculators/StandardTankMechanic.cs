using DTO;
using FrontEnd.Services.CrewServices;

namespace FrontEnd.Services.TankCalculators
{
    public class StandardTankMechanic : TankMechanicBase
    {
        public StandardTankMechanic(CrewSkillService crewSkillService)
            : base(crewSkillService)
        {
        }

        public override string TankKey => "STANDARD";

        public override bool CanHandle(FrontEndData tank)
        {
            return tank.Level != 11;
        }
    }
}
