using System;
using System.Runtime.Serialization;

namespace TiendaVirtual.AccesoDatos
{
    [Serializable]
    public class AccesoDatosException : Exception
    {
        public AccesoDatosException()
        {
        }

        public AccesoDatosException(string message) : base(message)
        {
        }

        public AccesoDatosException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccesoDatosException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}