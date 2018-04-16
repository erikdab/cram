using System;

namespace Engine
{
    /// <summary>
    /// Kingdom resources.
    /// </summary>
    public class Resources
    {
        public Resources()
        {
            Wood = 0;
            Food = 0;
            Gold = 0;
            Stone = 0;
            Population = 3;
            FreePopulation = Population;
            MaximumPopulation = 5;
        }
        
        /// <summary>
        /// Wood resource quantity.
        /// </summary>
        private int _wood;

        /// <summary>
        /// Wood resource quantity Property.
        /// </summary>
        public int Wood
        {
            get => _wood;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(value.ToString(), "Wood must be greater or equal to zero.");
                }

                _wood = value;
            }
        }

        /// <summary>
        /// Food resource quantity.
        /// </summary>
        private int _food;

        /// <summary>
        /// Food resource quantity Property.
        /// </summary>
        public int Food
        {
            get => _food;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(value.ToString(), "Food must be greater or equal to zero.");
                }

                _food = value;
            }
        }

        /// <summary>
        /// Gold resource quantity.
        /// </summary>
        private int _gold;

        /// <summary>
        /// Gold resource quantity Property.
        /// </summary>
        public int Gold
        {
            get => _gold;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(value.ToString(), "Gold must be greater or equal to zero.");
                }

                _gold = value;
            }
        }

        /// <summary>
        /// Stone resource quantity.
        /// </summary>
        private int _stone;

        /// <summary>
        /// Stone resource quantity Property.
        /// </summary>
        public int Stone
        {
            get => _stone;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(value.ToString(), "Stone must be greater or equal to zero.");
                }

                _stone = value;
            }
        }

        /// <summary>
        /// Population resource quantity.
        /// </summary>
        private int _population;

        /// <summary>
        /// Population resource quantity Property.
        /// </summary>
        public int Population
        {
            get => _population;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(value.ToString(), "Population must be greater or equal to zero.");
                }

                _population = value;
            }
        }

        /// <summary>
        /// FreePopulation resource quantity.
        /// </summary>
        private int _freePopulation;

        /// <summary>
        /// Population resource quantity Property.
        /// </summary>
        public int FreePopulation
        {
            get => _freePopulation;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(value.ToString(), "FreePopulation must be greater or equal to zero.");
                }

                _freePopulation = value;
            }
        }

        /// <summary>
        /// MaximumPopulation resource quantity.
        /// </summary>
        private int _maximumPopulation;

        /// <summary>
        /// MaximumPopulation resource quantity Property.
        /// </summary>
        public int MaximumPopulation
        {
            get => _maximumPopulation;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(value.ToString(), "MaximumPopulation must be greater or equal to zero.");
                }

                _maximumPopulation = value;
            }
        }

        /// <summary>
        /// Find a resource by name and return it.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int Find(string name)
        {
            switch (name)
            {
                case "Wood":
                    return Wood;
                case "Food":
                    return Food;
                case "Stone":
                    return Stone;
                case "Gold":
                    return Gold;
            }
            throw new ArgumentException($"Resources do not contain resource with name: {name}");
        }

        /// <summary>
        /// Resource Names.
        /// </summary>
        /// <returns></returns>
        public string[] ResourceNames()
        {
            return new[] {"Food", "Wood", "Stone", "Gold"};
        }

        /// <summary>
        /// Check if there are enough resources for a purchase.
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public bool SufficientResources(Resources cost)
        {
            return Wood >= cost.Wood &&
                   Food >= cost.Food &&
                   Gold >= cost.Gold &&
                   Stone >= cost.Stone;
        }

        /// <summary>
        /// Try to pay the cost for something.
        /// </summary>
        /// <param name="cost"></param>
        public void PayCostTry(Resources cost)
        {
            if (! SufficientResources(cost))
            {
                throw new PayInsufficientResourcesException(cost);
            }

            Wood -= cost.Wood;
            Food -= cost.Food;
            Gold -= cost.Gold;
            Stone -= cost.Stone;
        }

        public override string ToString()
        {
            return $"Wood: {Wood}, Food: {Food}, Gold: {Gold}, Stone: {Stone}, Population: {Population}.";
        }
    }
}
