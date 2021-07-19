using System;
using System.Runtime.Serialization;

namespace Core
{
    [Serializable]
    public class GenshinException : Exception
    {
        public GenshinException(string? message) : base(message) { }

        protected GenshinException(
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