


using Label.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;


namespace Label
{

    public class ImageStore
    {

        public static List<Imageb> Icons { get; set; }

        static ImageStore()
        {
            var ic = ElementLoader.Images;
            var n = new List<Imageb>(ic);

            var i = new Imageb() { Image = new Bitmap(16,16) };
            n.Insert(0, i);
            Icons = n;
        }
    }

    public class ElementLoader
    {
        private static List<Imageb> LoadImages()
        {
            List<Imageb> saida = new List<Imageb>();
            Assembly ass = Assembly.GetExecutingAssembly();

            var assembliesg = ass.GetManifestResourceNames();
            var simages = assembliesg.Where(g => g.EndsWith(".png")).ToList();
            List<Imageb> i = new List<Imageb>();
            foreach (var im in simages)
            {
                var st = ass.GetManifestResourceStream(im);
                if (st != null)
                {
                    var bit = new Bitmap(st);
                    var s = new Imageb() { Name = im, Image = bit};
                    i.Add(s);
                }
            }

            var Txt          = i.Where(w => w.Name.EndsWith("text.png")).FirstOrDefault();
            var TxtZ         = i.Where(w => w.Name.EndsWith("text_render.png")).FirstOrDefault();
            var Graphic      = i.Where(w => w.Name.EndsWith("graphics.png")).FirstOrDefault();
            var Picture      = i.Where(w => w.Name.EndsWith("picture.png")).FirstOrDefault();
            var Barcode39    = i.Where(w => w.Name.EndsWith("bar_code_39.png")).FirstOrDefault();
            var Barcode128   = i.Where(w => w.Name.EndsWith("bar_code_128.png")).FirstOrDefault();
            var QrCode16     = i.Where(w => w.Name.EndsWith("qr_code_16.png")).FirstOrDefault();
            var Datamatrix16 = i.Where(w => w.Name.EndsWith("datamatrix_16.png")).FirstOrDefault();

            Txt.ElementType          = LabelElementType.PRINTER_FONT;
            TxtZ.ElementType         = LabelElementType.RENDERED_TEXT;
            Graphic.ElementType      = LabelElementType.GRAPHIC_BOX;
            Picture.ElementType      = LabelElementType.PRELOADED_IMAGE;
            Barcode39.ElementType    = LabelElementType.CODIGO_BARRA_39;
            Barcode128.ElementType   = LabelElementType.CODIGO_BARRA_128;
            QrCode16.ElementType     = LabelElementType.QR_CODE;
            Datamatrix16.ElementType = LabelElementType.DATAMATRIX;

            Txt.Index          = 1;
            TxtZ.Index         = 2;
            Graphic.Index      = 3;
            Picture.Index      = 4;
            Barcode39.Index    = 5;
            Barcode128.Index   = 6;
            QrCode16.Index     = 7;
            Datamatrix16.Index = 8;

            saida.Add(Txt);
            saida.Add(TxtZ);
            saida.Add(Graphic);
            saida.Add(Picture);
            saida.Add(Barcode39);
            saida.Add(Barcode128);
            saida.Add(QrCode16);
            saida.Add(Datamatrix16);

            return saida;
        }

        static ElementLoader()
        {
            Images = LoadImages();
        }

        internal static List<Imageb> Images { get; private set; }
        public static Imageb GetImageByElementType(LabelElementType type)
        {
            var im = Images.Where(w => w.ElementType == type).FirstOrDefault();
            return im;
        }
    }

    public class Imageb 
    {

        internal Imageb(Imageb at)
        {
            if(at != null)
            {
                Name        = string.Copy(at.Name);
                Image       = at.Image;
                ElementType = at.ElementType;
            }
        }
        internal Imageb() { }

        public int Index { get; set; }
        public string Name { get; internal set; }
        public Bitmap Image { get; internal set; }
        public LabelElementType ElementType { get; internal set; }
    }
}
