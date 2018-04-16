namespace Engine
{
    public class CastleDistrict : District
    {
        public CastleDistrict(Resources resources) : base(resources) { }

        public override void OnTick() { }

        public override Resources UpgradeCost()
        {
            return new Resources
            {
                Food = 400 * UpgradeLevel * 2,
                Wood = 300 * UpgradeLevel * 2,
                Stone = 250 * UpgradeLevel * 2,
                Gold = 250 * UpgradeLevel * 2
            };
        }

        public override string Description()
        {
            return "Complete the Castle to protect the village and complete the game!";
        }

        public override string ToString()
        {
            return "Castle";
        }
    }
}
