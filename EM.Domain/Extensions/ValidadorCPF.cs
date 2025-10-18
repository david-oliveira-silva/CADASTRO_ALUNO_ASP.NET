
namespace EM.Domain.Extensions
{
    public static class ValidadorCPF
    {

        public static bool IsCPF(this string cpf)
        {
            int[] v1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] v2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string ArmCPF;
            string digito;

            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            ArmCPF = cpf.Substring(0, 9);

            soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(ArmCPF[i].ToString()) * v1[i];
            }
            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }


            digito = resto.ToString();
            ArmCPF = ArmCPF + digito;

            //------------------------
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(ArmCPF[i].ToString()) * v2[i];
            }
            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);

        }
    }
}
