namespace Desafio001.Servico.Utils
{
    public static class DocumentoUtils
    {
        public static bool CpfValido(string? cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            var apenasNumeros = new string(cpf.Where(char.IsDigit).ToArray());

            if (apenasNumeros.Length != 11)
                return false;

            if (apenasNumeros.Distinct().Count() == 1)
                return false;

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (apenasNumeros[i] - '0') * (10 - i);

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;
            if ((apenasNumeros[9] - '0') != digito1)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (apenasNumeros[i] - '0') * (11 - i);

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;
            if ((apenasNumeros[10] - '0') != digito2)
                return false;

            return true;
        }
    }
}
