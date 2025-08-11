namespace Desafio001.Servico.Utils
{
    public class StringUtils
    {
        public static string SomenteNumeros(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            return System.Text.RegularExpressions.Regex.Replace(input, @"\D", "");
        }

    }
}
