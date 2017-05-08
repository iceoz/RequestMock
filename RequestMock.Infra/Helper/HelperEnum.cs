using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace RequestMock.Infra.Helper
{
    public static class HelperEnum
    {
        public static async Task<IDictionary<int, string>> ObterDescricaoEnconde()
        {
            var dcEnum = await ObterDescricoesT<Encode>();
            return await ObterDicionarioPorInteiro(dcEnum);
        }

        public static async Task<IDictionary<int, string>> ObterDescricaoContentType()
        {
            var dcEnum = await ObterDescricoesT<ContentType>();
            return await ObterDicionarioPorInteiro(dcEnum);
        }

        public static async Task<IDictionary<int, string>> ObterDescricaoStatusCode()
        {
            var dcEnum = await ObterNomesT<HttpStatusCode>();
            dcEnum[HttpStatusCode.OK] = "OK";

            return await ObterDicionarioPorInteiro(dcEnum);
        }

        public static async Task<IDictionary<int, string>> ObterDicionarioPorInteiro<T>(IDictionary<T, string> dicionario) where T : IConvertible
        {
            return await Task.Run<IDictionary<int, string>>(() => { return dicionario.ToDictionary(x => x.Key.ToInt32(CultureInfo.CurrentCulture), x => x.Value); });
        }

        private static async Task<IDictionary<T, string>> ObterDescricoesT<T>() where T : struct, IConvertible
        {
            return await Task.Run<IDictionary<T, string>>(() =>
            {
                Dictionary<T, string> retorno = new Dictionary<T, string>();

                foreach (var name in Enum.GetNames(typeof(T)))
                {
                    if (Enum.TryParse(name, out T atual))
                    {
                        var memberInfo = atual.GetType().GetTypeInfo().GetMember(atual.ToString());
                        var attribute = memberInfo[0].GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();
                        retorno.Add(atual, atual.ObterDescricao());
                    }
                }

                return retorno;
            });
        }

        public static string ObterDescricao<T>(this T enumerador) where T : struct, IConvertible
        {
            var memberInfo = enumerador.GetType().GetTypeInfo().GetMember(enumerador.ToString());
            var attribute = memberInfo[0].GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();
            return attribute.Description;
        }

        private static async Task<IDictionary<T, string>> ObterNomesT<T>() where T : struct, IConvertible
        {
            return await Task.Run<IDictionary<T, string>>(() =>
            {
                Dictionary<T, string> retorno = new Dictionary<T, string>();

                foreach (var name in Enum.GetNames(typeof(T)))
                {
                    if (Enum.TryParse(name, out T atual))
                    {
                        if (!retorno.ContainsKey(atual))
                            retorno.Add(atual, DisplayCamelCaseString(name));
                    }
                }

                return retorno;
            });
        }

        public static string DisplayCamelCaseString(string camelCase)
        {
            List<char> chars = new List<char>();
            chars.Add(camelCase[0]);
            foreach (char c in camelCase.Skip(1))
            {
                if (char.IsUpper(c))
                {
                    chars.Add(' ');
                    chars.Add(char.ToLower(c));
                }
                else
                    chars.Add(c);
            }

            return new string(chars.ToArray());
        }
    }
}
