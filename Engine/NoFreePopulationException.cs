using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <inheritdoc />
    /// <summary>
    /// Exception thrown when trying to assign workers to Production District when there is no free population.
    /// </summary>
    [Serializable]
    public class NoFreePopulationException : Exception
    {
        public NoFreePopulationException()
        {
        }

        public NoFreePopulationException(string message) : base(message)
        {
        }

        public NoFreePopulationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoFreePopulationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}