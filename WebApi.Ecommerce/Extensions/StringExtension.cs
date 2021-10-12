using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebApi.Ecommerce.Extensions
{
    public static class StringExtension
    {
        public static string OnlyNumbers(this String input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var onlyNumbers = new Regex(@"[^\d]");
            return onlyNumbers.Replace(input, "");
        }

        public static bool IsValidDocument(this String input)
        {
            return (IsCpf(input) || IsCnpj(input));
        }

        /// <summary>
        /// Regra - https://jsfiddle.net/83zopn51/1/
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidPhone(this String input)
        {
            var result = true;

            if (string.IsNullOrEmpty(input))
                return false;

            var number = input.OnlyNumbers();

            //verifica se tem a qtde de numero correto
            if (!(number.Length >= 10 && number.Length <= 11))
                return false;

            //Se tiver 11 caracteres, verificar se começa com 9 o celular
            if (number.Length == 11 && IsNonoDigitValid(Convert.ToInt16(number.Substring(2, 1)))) return false;

            // Verifica se prefixo é maior que 7777
            if (number.Length == 11 && IsPrefixCellPhoneValid(Convert.ToInt32(number.Substring(3, 4)))) return false;

            // DDDs validos
            if (IsDddValid(Convert.ToInt16(number.Substring(0, 2)))) return false;

            // Validar se o numero é telefone mesmo ou do tipo rádio (se o numero começa entre 2 e 5, ou 7)
            if (number.Length == 10 && IsPrefixPhoneValid(Convert.ToInt16(number.Substring(2, 1)))) return false;

            return result;
        }

        private static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        private static bool IsDddValid(int ddd)
        {
            bool result = true;

            int[] listDddValid = new[] {
                    11,12,13,14,15,16,17,18,19,
                    21,22,24,27,28,
                    31,32,33,34,35,37,38,
                    41,42,43,44,45,46,47,48,49,
                    51,53,54,55,
                    61,62,63,64,65,66,67,68,69,
                    71,73,74,75,77,79,
                    81,82,83,84,85,86,87,88,89,
                    91,92,93,94,95,96,97,98,99
                };

            if (listDddValid.Contains(ddd))
            {
                result = false;
            }

            return result;
        }

        private static bool IsNonoDigitValid(int nonoDigit)
        {
            return (nonoDigit != 9);
        }

        private static bool IsPrefixCellPhoneValid(int prefix)
        {
            return (prefix < 7777);
        }

        private static bool IsPrefixPhoneValid(int prefix)
        {
            bool result = true;

            int[] listPrefixValid = new[] { 2, 3, 4, 5, 7 };

            if (listPrefixValid.Contains(prefix))
            {
                result = false;
            }

            return result;
        }
    }
}
