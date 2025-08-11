namespace Atendimento.Dominio.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public int StatusCode { get; }
        public object? Conteudo { get; }
        public string? LogInterno { get; }

        protected ExceptionBase(string mensagem, object? conteudo, string? logInterno, int statusCode)
            : base(mensagem)
        {
            StatusCode = statusCode;
            Conteudo = conteudo;
            LogInterno = logInterno;
        }
    }
}
