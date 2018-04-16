using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <inheritdoc />
    /// <summary>
    /// Exception thrown when trying to purchase something from a District when there are insufficient resources to pay for it.
    /// </summary>
    [Serializable]
    public class PayInsufficientResourcesException : Exception
    {
        public Resources CostResources { get; private set; }

        public PayInsufficientResourcesException(Resources costResources)
        {
            CostResources = costResources;
        }

        public PayInsufficientResourcesException(string message, Resources costResources) : base(message)
        {
            CostResources = costResources;
        }

        public PayInsufficientResourcesException(string message, Exception innerException, Resources costResources) : base(message, innerException)
        {
            CostResources = costResources;
        }

        protected PayInsufficientResourcesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}