namespace Desafio001.Dominio.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }

        public CustomException(string message, int statusCode = 202) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
