using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <inheritdoc />
    /// <summary>
    /// Exception thrown when trying to free workers from Production District when no workers are assigned.
    /// </summary>
    [Serializable]
    public class NoAssignedWorkersException : Exception
    {
        public NoAssignedWorkersException()
        {
        }

        public NoAssignedWorkersException(string message) : base(message)
        {
        }

        public NoAssignedWorkersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoAssignedWorkersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}