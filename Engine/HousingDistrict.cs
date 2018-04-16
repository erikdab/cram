namespace Engine
{
    /// <inheritdoc />
    /// <summary>
    /// Housing (villager housing & producing) district.
    /// </summary>
    public class HousingDistrict : District
    {
        public HousingDistrict(Resources resources) : base(resources) { }

        public override void UpgradeTry()
        {
            base.UpgradeTry();

            Resources.MaximumPopulation += 5;
        }

        public override Resources UpgradeCost()
        {
            return new Resources
            {
                Wood = 70 * UpgradeLevel
            };
        }

        public override void PurchaseTry()
        {
            if (Resources.Population == Resources.MaximumPopulation)
            {
                throw new PopulationAtMaxLevelException();
            }

            Resources.PayCostTry(PurchaseCost());

            Resources.Population++;
            Resources.FreePopulation++;
        }

        public override Resources PurchaseCost()
        {
            return new Resources
            {
                Food = 30 + 20 * UpgradeLevel
            };
        }

        public override string PurchaseName()
        {
            return "Buy Villager";
        }

        public override bool CanPurchase()
        {
            return Resources.Population < Resources.MaximumPopulation;
        }

        public override string Description()
        {
            return "Upgrade Housing to increase maximum population size.";
        }

        public override string ToString()
        {
            return "Housing";
            
        }
    }
}
