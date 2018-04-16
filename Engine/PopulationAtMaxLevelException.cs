using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <inheritdoc />
    /// <summary>
    /// Exception thrown when Population is at maximum and cannot be increased further.
    /// </summary>
    [Serializable]
    public class PopulationAtMaxLevelException : Exception
    {
        public PopulationAtMaxLevelException()
        {
        }

        public PopulationAtMaxLevelException(string message) : base(message)
        {
        }

        public PopulationAtMaxLevelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PopulationAtMaxLevelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}