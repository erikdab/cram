using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <inheritdoc />
    /// <summary>
    /// Exception thrown when district is already at max level and cannot be upgraded further.
    /// </summary>
    [Serializable]
    public class DistrictAtMaxLevelException : Exception
    {
        public DistrictAtMaxLevelException()
        {
        }

        public DistrictAtMaxLevelException(string message) : base(message)
        {
        }

        public DistrictAtMaxLevelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DistrictAtMaxLevelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}