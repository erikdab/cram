using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <inheritdoc />
    /// <summary>
    /// Exception thrown when District is not a Production District (cannot assign workers).
    /// </summary>
    [Serializable]
    public class CannotAssignWorkersException : Exception
    {
        public CannotAssignWorkersException()
        {
        }

        public CannotAssignWorkersException(string message) : base(message)
        {
        }

        public CannotAssignWorkersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotAssignWorkersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}