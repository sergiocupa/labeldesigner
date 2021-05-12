

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Label.Model
{


    public class LabelElement
    {

        public string UID { get; set; }
        public bool Selected { get; set; }
        public LabelSize Size { get; set; }
        public LabelPosition Position { get; set; }
        public LabelMaterial Material { get; set; }
        public LabelPosition Home { get; set; }
        public LabelRotateQuadrant RotateQuadrant { get; set; }
        public LabelElementType ElementType { get; set; }
        public LabelContentType ContentType { get; set; }
        public object Value { get; set; }
        public string Name { get; set; }
        public TextFont Font { get; set; }
        public string UidGroup { get; set; }
        public string Format { get; set; }
        public bool UseFNC1 { get; set; }
        public string PrintScript { get; set; }
        public CompressedImage Image { get; set; }


        [JsonIgnore]
        public List<Rectangle> Rectangles { get; set; }


        public Font GetEditedFont()
        {
            if (Font == null)
            {
                return new Font("Arial", 12, FontStyle.Regular);
            }
            return new Font(Font.FamilyName, Font.Size, Font.Style);
        }

        public void SetEditedFont(Font ft)
        {
            if (ft != null)
            {
                Font = new TextFont() { FamilyName = ft.FontFamily.Name, Size = (int)ft.Size, Style = ft.Style };
            }
            else
            {
                Font = new TextFont() { FamilyName = "Arial", Size = 12, Style = FontStyle.Regular };
            }
        }


        protected LabelElement() { }
        public LabelElement(LabelElementType type, LabelPosition home)
        {
            ElementType = type;

            Position = new LabelPosition();
            Size = new LabelSize();
            Home = home;
            Material = new LabelMaterial();

            Rectangles = new List<Rectangle>();
        }


    }


    public class CompressedImage
    {
        public string CompressedData { get; set; }
    }



    public enum LabelElementType : int
    {
        [GenericAttribute(Description = "-- Indefinido --", PartForMenu = "-- Indefinido --")]
        UNKNOWN = 0,

        [GenericAttribute(Description = "Fonte da Impressora", PartForMenu = "Text - ")]
        PRINTER_FONT = 1,

        [GenericAttribute(Description = "Texto Renderizado", PartForMenu = "Text - ")]
        RENDERED_TEXT = 2,

        [GenericAttribute(Description = "Imagem Carregada", PartForMenu = "Image - ")]
        PRELOADED_IMAGE = 3,

        [GenericAttribute(Description = "Código Barra 39", PartForMenu = "Code 39 - ")]
        CODIGO_BARRA_39 = 4,

        [GenericAttribute(Description = "Código Barra 128", PartForMenu = "Code 128 - ")]
        CODIGO_BARRA_128 = 5,

        [GenericAttribute(Description = "QR Code", PartForMenu = "Qr Code - ")]
        QR_CODE = 6,

        [GenericAttribute(Description = "DataMatrix", PartForMenu = "Datamatrix - ")]
        DATAMATRIX = 7,

        [GenericAttribute(Description = "Graphic Box", PartForMenu = "Box - ")]
        GRAPHIC_BOX = 8
    }

    public enum LabelContentType : int
    {
        [Description("-- Indefinido --")]
        UNKNOWN = 0, 
        [Description("Texto Fixo")]
        PRESET_TEXT = 1,
        [Description("Fonte de Dados")]
        DATASOURCE = 2,
        [Description("Dados Vinculados")]
        BINDED_VALUE = 3
    }



    public class TextFont
    {
        public string FamilyName { get; set; }
        public int Size { get; set; }
        public FontStyle Style { get; set; }
    }

    public class LabelSize
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Magnification { get; set; }
    }

    public class LabelPosition
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double ViewZoom { get; set; }
        public bool BottomLeftJustification { get; set; }
    }

    public class LabelMaterial
    {
        public double Thickness { get; set; }
        public bool FillReverse { get; set; }
    }

    public enum LabelRotateQuadrant : int
    {
        [Description("Normal")]
        NORMAL = 0,
        [Description("Girar 90°")]
        RETATE_90 = 90,
        [Description("Girar 180°")]
        RETATE_180 = 180,
        [Description("Girar 270°")]
        RETATE_270 = 270,
    }


    public class GenericAttribute : Attribute
    {
        public string Description { get; set; }
        public string PartForMenu { get; set; }
    }

    public class LabelParam
    {


        public static List<LabelParam> GetGenericAttributesByEnum(Type type)
        {
            var tpAttribute = typeof(GenericAttribute);
            List<LabelParam> result = new List<LabelParam>();
            var items = Enum.GetValues(type);

            foreach (var item in items)
            {
                var ga = (GenericAttribute)GetAttributeByEnum(item, type, tpAttribute);

                LabelParam lp = new LabelParam();
                lp.ID = (int)item;
                lp.Data = item;
                lp.Description = ga.Description;
                lp.PartForMenu = ga.PartForMenu;
                result.Add(lp);
            }

            return result;
        }

        private static object GetAttributeByEnum(object data, Type tpObject, Type tpAttribute)
        {
            FieldInfo fi = tpObject.GetField(data.ToString());
            var o = fi.GetCustomAttributes(tpAttribute, false);
            return o[0];
        }


        public static List<LabelParam> ObterPorEnum(Type type)
        {
            List<LabelParam> result = new List<LabelParam>();
            var items = Enum.GetValues(type);

            foreach (var item in items)
            {
                LabelParam lp = new LabelParam();
                lp.ID = (int)item;
                lp.Data = item;
                lp.Description = Utils.Enumerator.Get(item);
                result.Add(lp);
            }

            return result;
        }

        public int ID { get; set; }
        public string Description { get; set; }
        public string PartForMenu { get; set; }
        public object Data { get; set; }
    }


    public enum ZplRenderMode
    {
        UNKNOWN = 0,
        GF_Z64 = 1,
        GF_HEX = 2,
        DG_HEX = 3
    }

}
