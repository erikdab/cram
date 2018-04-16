using System;

namespace Engine
{
    /// <inheritdoc />
    /// <summary>
    /// Lumberjack (wood producing) district.
    /// </summary>
    public class LumberjackDistrict : District
    {
        public LumberjackDistrict(Resources resources) : base(resources) { }

        public override void OnTick()
        {
            Resources.Wood += (int)Math.Floor(Workers * UpgradeLevel * 1.5);
        }

        public override Resources UpgradeCost()
        {
            return new Resources
            {
                Wood = (int)Math.Floor(130 * Math.Sqrt(UpgradeLevel))
            };
        }

        public override bool CanAssignWorkers()
        {
            return true;
        }

        public override string Description()
        {
            return "Upgrade the Lumberjack to increase wood production.";
        }

        public override string ToString()
        {
            return "Lumberjack";
        }
    }
}
