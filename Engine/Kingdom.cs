using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// Player's kingdom.
    /// </summary>
    public class Kingdom
    {
        /// <summary>
        /// Kingdom's current resources.
        /// </summary>
        public Resources Resources = new Resources();

        /// <summary>
        /// All Kingdom's districts.
        /// </summary>
        public readonly List<District> Districts;

        /// <summary>
        /// The Kingdom's Farm district.
        /// </summary>
        public FarmDistrict FarmDistrict;

        /// <summary>
        /// The Kingdom's Housing district.
        /// </summary>
        public HousingDistrict HousingDistrict;

        /// <summary>
        /// The Kingdom's Mines district.
        /// </summary>
        public MinesDistrict MinesDistrict;

        /// <summary>
        /// The Kingdom's Lumberjack district.
        /// </summary>
        public LumberjackDistrict LumberjackDistrict;

        /// <summary>
        /// The Kingdom's Castle district.
        /// </summary>
        public CastleDistrict CastleDistrict;

        public Kingdom()
        {
            FarmDistrict = new FarmDistrict(Resources);
            HousingDistrict = new HousingDistrict(Resources);
            MinesDistrict = new MinesDistrict(Resources);
            LumberjackDistrict = new LumberjackDistrict(Resources);
            CastleDistrict = new CastleDistrict(Resources);

            Districts = new List<District>
            {
                FarmDistrict,
                HousingDistrict,
                MinesDistrict,
                LumberjackDistrict,
                CastleDistrict
            };
        }
        
        /// <summary>
        /// Kingdom actions to perform on Game Tick.
        /// </summary>
        public void OnTick()
        {
            foreach (var district in Districts)
            {
                district.OnTick();
            }
        }
    }
}
