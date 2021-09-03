using System;
using System.Runtime.Serialization;

namespace Core.Exceptions
{
    /// <summary>
    /// Represents a 500-level exception.
    /// </summary>
    [Serializable]
    public class GenshinApplicationException : Exception
    {
        public GenshinApplicationException(string? message) : base(message) { }

        protected GenshinApplicationException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }

        public override void GetObjectData(
            SerializationInfo info,
            StreamingContext context
        )
        {
            base.GetObjectData(info, context);
        }
    }
}