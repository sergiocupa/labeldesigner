

using System;
using System.Collections.Generic;
using System.Reflection;


namespace Label.Model
{




    public class PrintableFieldAtt : System.Attribute
    {
        public string Name { get; set; }
        public string Format { get; set; }
        public bool IsVariable { get; set; }
        public bool IsKey { get; set; }
    }



    public class PrintableFieldAttUtil
    {

        public static List<PrintableField> ObterCamposImprimiveis<T>()
        {
            var tp = typeof(T);
            return ObterCamposImprimiveis(tp);
        }

        public static List<PrintableField> ObterCamposImprimiveis(Type tipo)
        {
            List<PrintableField> saida = new List<PrintableField>();
            PropertyInfo[] props = tipo.GetProperties();

            foreach (var item in props)
            {
                PrintableFieldAtt att = (PrintableFieldAtt)item.GetCustomAttribute(typeof(PrintableFieldAtt));

                if ((att != null) && (!string.IsNullOrEmpty(att.Name)))
                {
                    saida.Add(new PrintableField()
                    {
                        Name = att.Name,
                        TypeName = item.PropertyType.Name
                    });
                }
            }
            return saida;
        }



        public static string GetCustomAttributeName(Type type, string typePropertyName)
        {
            string saida = "";
            var prop = type.GetProperty(typePropertyName);
            var att = (PrintableFieldAtt)prop.GetCustomAttribute(typeof(PrintableFieldAtt));

            if (att != null)
            {
                saida = att.Name;
            }
            return saida;
        }
    }



    public class PrintableField
    {
        public string Name { get; set; }
        public object Data { get; set; }
        public string TypeName { get; set; }
        public IFormatter Formatter { get; set; }

    }



    public class Formatter
    {
        public static IFormatter GetFormatter(Type _type)
        {
            var underlyingType = Nullable.GetUnderlyingType(_type);
            var nullable = (underlyingType != null);
            var type = underlyingType ?? _type;

            if (type.Name == "String")
            {
                return new StringFormatter();
            }
            else if (type.Name == "Int32")
            {
                return new Int32Formatter();
            }
            else if (type.Name == "Double")
            {
                return new DoubleFormatter();
            }
            else if (type.Name == "DateTime")
            {
                return new DateTimeFormatter();
            }
            else if (type.Name == "Int64")
            {
                return new Int64Formatter();
            }
            else if (type.Name == "Decimal")
            {
                return new DecimalFormatter();
            }
            else if (type.Name == "Int16")
            {
                return new Int16Formatter();
            }
            else if (type.Name == "Float")
            {
                return new FloatFormatter();
            }
            else if (type.Name == "Byte")
            {
                return new ByteFormatter();
            }
            else
            {
                return new StringFormatter();
            }
        }
    }



    internal class Int32Formatter : IFormatter
    {
        public string Format(object instance, string format)
        {
            return instance != null ? (!string.IsNullOrEmpty(format) ? ((int)instance).ToString(format) : "") : "";
        }
    }
    internal class Int16Formatter : IFormatter
    {
        public string Format(object instance, string format)
        {
            return instance != null ? (!string.IsNullOrEmpty(format) ? ((short)instance).ToString(format) : "") : "";
        }
    }
    internal class Int64Formatter : IFormatter
    {
        public string Format(object instance, string format)
        {
            return instance != null ? (!string.IsNullOrEmpty(format) ? ((long)instance).ToString(format) : "") : "";
        }
    }
    internal class DoubleFormatter : IFormatter
    {
        public string Format(object instance, string format)
        {
            return instance != null ? (!string.IsNullOrEmpty(format) ? ((double)instance).ToString(format) : "") : "";
        }
    }
    internal class DecimalFormatter : IFormatter
    {
        public string Format(object instance, string format)
        {
            return instance != null ? (!string.IsNullOrEmpty(format) ? ((decimal)instance).ToString(format) : "") : "";
        }
    }
    internal class FloatFormatter : IFormatter
    {
        public string Format(object instance, string format)
        {
            return instance != null ? (!string.IsNullOrEmpty(format) ? ((float)instance).ToString(format) : "") : "";
        }
    }
    internal class ByteFormatter : IFormatter
    {
        public string Format(object instance, string format)
        {
            return instance != null ? (!string.IsNullOrEmpty(format) ? ((byte)instance).ToString(format) : "") : "";
        }
    }


    internal class StringFormatter : IFormatter
    {
        public string Format(object instance, string format)
        {
            return instance.ToString();
        }
    }


    internal class DateTimeFormatter : IFormatter
    {
        public string Format(object instance, string format)
        {
            var result = instance != null ? ((DateTime)instance).ToString(format) : "";
            return result;
        }
    }




    public interface IFormatter
    {
        string Format(object instance, string format);
    }
}
