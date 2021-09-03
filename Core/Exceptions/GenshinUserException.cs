using System;
using System.Runtime.Serialization;

namespace Core.Exceptions
{
    /// <summary>
    /// Represents a 400-level exception.
    /// </summary>
    [Serializable]
    public class GenshinUserException : Exception
    {
        public GenshinUserException(string message) : base(message) { }

        protected GenshinUserException(
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