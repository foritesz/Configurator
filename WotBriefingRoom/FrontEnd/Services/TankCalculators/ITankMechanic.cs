using DTO;

namespace FrontEnd.Services.TankCalculators
{
    public interface ITankMechanic
    {
        string TankKey { get; }

        bool CanHandle(FrontEndData tank);

        string DisplayDpm(TankMechanicContext context);
        string DisplayFireRate(TankMechanicContext context);
        string DisplayGunReloadTime(TankMechanicContext context);
        string DisplayGunAimingTime(TankMechanicContext context);
        string DisplayGunDispersionRadius(TankMechanicContext context);
        string DisplayTurretVision(TankMechanicContext context);
        string DisplayRotationSpeed(TankMechanicContext context);
    }
}
