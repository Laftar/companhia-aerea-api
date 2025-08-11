namespace Desafio001.Api
{
    public class ApiResponse
    {
        public bool Error { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public object? Detalhes { get; set; }

        public static ApiResponse Sucesso(object? detalhes)
        {
            return new ApiResponse { Error = false, Detalhes = detalhes };
        }

        public static ApiResponse Falha(object? detalhes )
        {
            return new ApiResponse { Error = true, Detalhes = detalhes };
        }
    }
}


