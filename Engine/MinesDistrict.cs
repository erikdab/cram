using System;

namespace Engine
{
    /// <inheritdoc />
    /// <summary>
    /// Goldmine (gold producing) district.
    /// </summary>
    public class MinesDistrict : District
    {
        public MinesDistrict(Resources resources) : base(resources) { }

        public override void OnTick()
        {
            Resources.Gold += (int)Math.Floor(Workers * UpgradeLevel * 1.3);
            Resources.Stone += (int)Math.Floor(Workers * UpgradeLevel * 1.3);
        }

        public override Resources UpgradeCost()
        {
            return new Resources
            {
                Wood = 130 * UpgradeLevel,
                Stone = 30 * UpgradeLevel,
                Gold = 30 * UpgradeLevel
            };
        }

        public override bool CanAssignWorkers()
        {
            return true;
        }

        public override string Description()
        {
            return "Upgrade the Mines to increase gold and stone production.";
        }

        public override string ToString()
        {
            return "Mines";
        }
    }
}
