using System;

namespace Engine
{
    /// <summary>
    /// Base abstract class for districts.
    /// </summary>
    public abstract class District
    {
        /// <summary>
        /// Reference to Kingdom.resources.
        /// </summary>
        protected Resources Resources;

        /// <summary>
        /// District upgrade level.
        /// </summary>
        public int UpgradeLevel { get; protected set; } = 1;

        /// <summary>
        /// Maximum possible district upgrade level.
        /// </summary>
        public int MaxUpgradeLevel { get; protected set; } = 4;

        protected District(Resources resources)
        {
            Resources = resources;
        }

        /// <summary>
        /// Action to perform on Game Tick.
        /// </summary>
        public virtual void OnTick() { }

        /// <summary>
        /// Try to upgrade the district.
        /// This will only work if Kingdom Resources are greater than the UpgradeCost.
        /// Otherwise throws an exception.
        /// </summary>
        public virtual void UpgradeTry()
        {
            if (UpgradeLevel == MaxUpgradeLevel)
            {
                throw new DistrictAtMaxLevelException();
            }

            Resources.PayCostTry(UpgradeCost());

            UpgradeLevel++;
        }

        /// <summary>
        /// Cost to upgrade district.
        /// </summary>
        /// <returns></returns>
        public abstract Resources UpgradeCost();

        /// <summary>
        /// Is District upgradeable?
        /// </summary>
        /// <returns></returns>
        public bool CanUpgrade()
        {
            return UpgradeLevel < MaxUpgradeLevel;
        }

        /// <summary>
        /// District workers (Villagers assigned to district).
        /// </summary>
        public int Workers { get; protected set; } = 0;

        /// <summary>
        /// Try to assign a worker.
        /// This will only work if can assign workers and there are free ones.
        /// Otherwise throws an exception.
        /// </summary>
        public virtual void AssignWorker()
        {
            if (! CanAssignWorkers())
            {
                throw new CannotAssignWorkersException();
            }
            if (Resources.FreePopulation == 0)
            {
                throw new NoFreePopulationException();
            }

            Workers++;
            Resources.FreePopulation--;
        }

        /// <summary>
        /// Try to free a worker.
        /// This will only work if can assign workers and has any workers.
        /// Otherwise throws an exception.
        /// </summary>
        public virtual void FreeWorker()
        {
            if (!CanAssignWorkers())
            {
                throw new CannotAssignWorkersException();
            }
            if (Workers == 0)
            {
                throw new NoAssignedWorkersException();
            }

            Workers--;
            Resources.FreePopulation++;
        }

        /// <summary>
        /// Can District Assign workers?
        /// </summary>
        /// <returns></returns>
        public virtual bool CanAssignWorkers()
        {
            return false;
        }

        /// <summary>
        /// Try to purchase item.
        /// This will only work if Kingdom Resources are greater than the PurchaseCost.
        /// Otherwise throws an exception.
        /// </summary>
        public virtual void PurchaseTry() { throw new NotImplementedException(); }

        /// <summary>
        /// Cost to purchase item.
        /// </summary>
        /// <returns></returns>
        public virtual Resources PurchaseCost() { throw new NotImplementedException(); }

        /// <summary>
        /// Name of item purchase.
        /// </summary>
        /// <returns></returns>
        public virtual string PurchaseName() { throw new NotImplementedException(); }

        /// <summary>
        /// Is Item purchaseable?
        /// </summary>
        /// <returns></returns>
        public virtual bool CanPurchase() { throw new NotImplementedException(); }

        /// <summary>
        /// Discription of what upgrading the district provides.
        /// </summary>
        /// <returns></returns>
        public abstract string Description();
    }
}
