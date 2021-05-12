

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;


namespace Utils
{
    public class Enumerator
    {
        public static T Get<T>(string content, T _default)
        {
            var valores = Enum.GetNames(typeof(T));

            if (!string.IsNullOrEmpty(content) && valores.Contains(content))
            {
                return (T)Enum.Parse(typeof(T), content);
            }
            return _default;
        }


        public static List<DadosDoEnumerador> GetElements(Type type)
        {
            List<DadosDoEnumerador> itens = new List<DadosDoEnumerador>();
            var valores = Enum.GetNames(type);

            int ix = 0;
            while (ix < valores.Length)
            {
                var vv = Enum.Parse(type, valores[ix]);

                DadosDoEnumerador item = new DadosDoEnumerador()
                {
                    Valor = (int)vv,
                    Nome = valores[ix],
                    Descricao = Get(vv),
                    Tipo = type
                };
                itens.Add(item);
                ix++;
            }
            return itens;
        }



        public static string Get(object Dado)
        {
            FieldInfo fi = Dado.GetType().GetField(Dado.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
                                                        else return Dado.ToString();
        }
    }

    public class DadosDoEnumerador
    {
        public int Valor { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Type Tipo { get; set; }
    }
}
