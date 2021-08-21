using System;

namespace ProductStore.API.DBFirst.Utils.Errors
{
    [Serializable]
    public class AppErrors : Exception
    {
        public AppErrors()
        { }

        public AppErrors(string message)
            : base(message)
        { }

        public AppErrors(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}