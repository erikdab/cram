namespace Engine
{
    /// <inheritdoc />
    /// <summary>
    /// Farm (food producing) district.
    /// </summary>
    public class FarmDistrict : District
    {
        public FarmDistrict(Resources resources) : base(resources) { }

        public override void OnTick()
        {
            Resources.Food += Workers * UpgradeLevel * 2;
        }

        public override Resources UpgradeCost()
        {
            return new Resources
            {
                Food = 130 * UpgradeLevel
            };
        }

        public override bool CanAssignWorkers()
        {
            return true;
        }

        public override string Description()
        {
            return "Upgrade the Farms to increase food production.";
        }

        public override string ToString()
        {
            return "Farm";
        }
    }
}
