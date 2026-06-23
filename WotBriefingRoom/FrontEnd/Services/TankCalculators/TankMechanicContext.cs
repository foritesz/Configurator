using DTO;

namespace FrontEnd.Services.TankCalculators
{
    public class TankMechanicContext
    {
        public FrontEndData? Tank { get; set; }

        public string SelectedGunKey { get; set; } = "Összes";
        public string SelectedTurretKey { get; set; } = "Összes";
        public string SelectedChassisKey { get; set; } = "Összes";
        public string SelectedAmmoType { get; set; } = string.Empty;

        public HashSet<string> ActiveMatrixButtons { get; set; } = new();
    }
}
