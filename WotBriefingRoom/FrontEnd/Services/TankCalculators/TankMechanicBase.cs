using DTO;
using DTO.TanksModels;
using FrontEnd.Services.CrewServices;
using System.Globalization;

namespace FrontEnd.Services.TankCalculators
{
    public abstract class TankMechanicBase : ITankMechanic
    {
        protected readonly CrewSkillService CrewSkillService;
        protected const double ModulStatConverter = 0.959;

        protected TankMechanicBase(CrewSkillService crewSkillService)
        {
            CrewSkillService = crewSkillService;
        }

        public abstract string TankKey { get; }

        public virtual bool CanHandle(FrontEndData tank)
        {
            return tank.Level != 11;
        }

        public virtual string DisplayDpm(TankMechanicContext context)
        {
            var gun = GetSelectedGun(context);
            var ammo = GetSelectedAmmo(context);

            if (ammo?.Damage == null || ammo.Damage.Count < 2)
                return "-";

            var reloadTime = ParseDouble(gun?.ReloadTime);
            var modifiedReload = CrewSkillService.ApplyReloadTime(
                reloadTime,
                context.ActiveMatrixButtons
            );

            if (modifiedReload == null || modifiedReload <= 0)
                return "-";

            var fireRate = 60d / modifiedReload.Value / ModulStatConverter;
            var dpm = ammo.Damage[1] * fireRate;

            return dpm.ToString("0", CultureInfo.InvariantCulture);
        }

        public virtual string DisplayFireRate(TankMechanicContext context)
        {
            var gun = GetSelectedGun(context);
            var reloadTime = ParseDouble(gun?.ReloadTime);

            var modifiedReload = CrewSkillService.ApplyReloadTime(
                reloadTime,
                context.ActiveMatrixButtons
            );

            if (modifiedReload == null || modifiedReload <= 0)
                return "-";

            var shotsPerMinute = 60d / modifiedReload.Value / ModulStatConverter;

            return shotsPerMinute.ToString("0.00", CultureInfo.InvariantCulture);
        }

        public virtual string DisplayGunReloadTime(TankMechanicContext context)
        {
            var gun = GetSelectedGun(context);
            var rawValue = ParseDouble(gun?.ReloadTime);

            if (rawValue == null)
                return "-";

            var baseValue = rawValue.Value * ModulStatConverter;

            var modified = CrewSkillService.ApplyReloadTime(
                baseValue,
                context.ActiveMatrixButtons
            );

            return CrewSkillService.FormatValue(modified, "0.00", " s");
        }

        public virtual string DisplayGunAimingTime(TankMechanicContext context)
        {
            var gun = GetSelectedGun(context);
            var rawValue = ParseDouble(gun?.AimingTime);

            if (rawValue == null)
                return "-";

            var baseValue = rawValue.Value * ModulStatConverter;

            var modified = CrewSkillService.ApplyAimingTime(
                baseValue,
                context.ActiveMatrixButtons
            );

            return CrewSkillService.FormatValue(modified, "0.00", " s");
        }

        public virtual string DisplayGunDispersionRadius(TankMechanicContext context)
        {
            var gun = GetSelectedGun(context);
            var rawValue = ParseDouble(gun?.ShotDispersionRadius);

            if (rawValue == null)
                return "-";

            var baseValue = rawValue.Value * ModulStatConverter;

            var modified = CrewSkillService.ApplyDispersion(
                baseValue,
                context.ActiveMatrixButtons
            );

            return CrewSkillService.FormatValue(modified, "0.00");
        }

        public virtual string DisplayTurretVision(TankMechanicContext context)
        {
            var turret = GetSelectedTurret(context);
            var baseValue = ParseDouble(turret?.CircularVisionRadius);

            var modified = CrewSkillService.ApplyViewRange(
                baseValue,
                context.ActiveMatrixButtons
            );

            return CrewSkillService.FormatValue(modified, "0.00", " m");
        }

        public virtual string DisplayRotationSpeed(TankMechanicContext context)
        {
            var chassis = GetSelectedChassis(context);
            var rawValue = ParseDouble(chassis?.RotationSpeed);

            if (rawValue == null)
                return "-";

            var baseValue = rawValue.Value / ModulStatConverter;

            var modified = CrewSkillService.ApplyRotationSpeed(
                baseValue,
                context.ActiveMatrixButtons
            );

            return CrewSkillService.FormatValue(modified, "0.00", " deg/s");
        }

        protected GunModuleData? GetSelectedGun(TankMechanicContext context)
        {
            var tank = context.Tank;

            if (tank?.Guns == null || tank.Guns.Count == 0)
                return null;

            if (!string.IsNullOrWhiteSpace(context.SelectedGunKey) &&
                tank.Guns.TryGetValue(context.SelectedGunKey, out var selectedGun))
            {
                return selectedGun;
            }

            return tank.Guns.Values.FirstOrDefault();
        }

        protected AmmoData? GetSelectedAmmo(TankMechanicContext context)
        {
            var tank = context.Tank;

            if (tank?.Ammo == null || tank.Ammo.Count == 0)
                return null;

            var selected = tank.Ammo.FirstOrDefault(a => a.Type == context.SelectedAmmoType);

            return selected ?? tank.Ammo.FirstOrDefault();
        }

        protected ChassisData? GetSelectedChassis(TankMechanicContext context)
        {
            var tank = context.Tank;

            if (tank?.Chassis == null || tank.Chassis.Count == 0)
                return null;

            if (!string.IsNullOrWhiteSpace(context.SelectedChassisKey) &&
                tank.Chassis.TryGetValue(context.SelectedChassisKey, out var selectedChassis))
            {
                return selectedChassis;
            }

            return tank.Chassis.Values.FirstOrDefault();
        }

        protected TurretDataFrontEnd? GetSelectedTurret(TankMechanicContext context)
        {
            var tank = context.Tank;

            if (tank?.Turrets0 == null || tank.Turrets0.Count == 0)
                return null;

            if (!string.IsNullOrWhiteSpace(context.SelectedTurretKey) &&
                tank.Turrets0.TryGetValue(context.SelectedTurretKey, out var selectedTurret))
            {
                return selectedTurret;
            }

            return tank.Turrets0.Values.FirstOrDefault();
        }

        protected static double? ParseDouble(object? value)
        {
            if (value == null)
                return null;

            if (value is string s)
            {
                if (string.IsNullOrWhiteSpace(s))
                    return null;

                if (double.TryParse(
                    s,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var result))
                {
                    return result;
                }

                return null;
            }

            try
            {
                return Convert.ToDouble(value, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }
    }
}
