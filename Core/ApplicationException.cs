using System;

namespace Core
{
    public class GenshinException : Exception
    {
        public GenshinException(string? message) : base(message)
        {
        }
    }
}