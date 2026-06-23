using DTO;

namespace FrontEnd.Services.TankCalculators
{
    public class TankMechanicRegistry
    {
        private readonly IEnumerable<ITankMechanic> _mechanics;

        public TankMechanicRegistry(IEnumerable<ITankMechanic> mechanics)
        {
            _mechanics = mechanics;
        }

        public ITankMechanic GetMechanic(FrontEndData tank)
        {
            return _mechanics.FirstOrDefault(x => x.CanHandle(tank))
                ?? throw new InvalidOperationException($"Nincs mechanic ehhez a tankhoz: {tank.Name}");
        }
    }
}
