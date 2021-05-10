

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Label
{

    public class Artifact
    {

        public void Init(IControlElement ui, LabelElement element, Action<Artifact> editar, Action<Artifact> selected)
        {
            Element    = element;
            Editar     = editar;
            Selected   = selected;
            ui.Element = Element;
            TreeIcon   = ElementLoader.GetImageByElementType(Element.ElementType);

            Refresh = () => { ui.Draw(); };

            var dn = ElementTypes.Where(w => w.ID == (int)ui.Element.ElementType).FirstOrDefault();

            if (ui.Element.Value == null) ui.Element.Value = "";

            var str = ui.Element.Value.ToString();
            if (str.Length > 30) str = str.Substring(0,30) + "...";

            ui.RePosition = (LabelPosition p) =>
            {
                Element.Position.X = (p.X - (Element.Home.X * Element.Home.ViewZoom)) / Element.Home.ViewZoom;
                Element.Position.Y = (p.Y - (Element.Home.Y * Element.Home.ViewZoom)) / Element.Home.ViewZoom;
            };

            ui.Resized = (int width, int height) =>
            {
                Element.Size.Width  = width;
                Element.Size.Height = height;
                ui.Draw();
            };

            ui.StartEdit = () => 
            { 
                if (Editar != null)   Editar(this); 
            };
            ui.Selected  = () => 
            { 
                if (Selected != null) Selected(this); 
            };
            ui.Artifact  = this;
            UI           = ui;

            UI.Draw();
        }



        public Image Icon { get { return TreeIcon.Image; } set { } }
        public Imageb TreeIcon { get; protected set; }
        public LabelElement Element { get; protected set; }
        public Action<Artifact> Editar { get; protected set; }
        public Action<Artifact> Selected { get; protected set; }
        public Action Refresh { get; protected set; }
        public IControlElement UI { get; protected set; }
        public object Row { get; set; }

        public string CaptionGrid
        {
            get
            {
                var e = Element;
                string oo = e != null ? (!string.IsNullOrEmpty(e.Name) ? e.Name : (e.Value != null ? e.Value.ToString() : "")) : "";
                return oo;
            }
            set { }
        }

        public static List<LabelParam> ElementTypes { get; private set; }

        static Artifact()
        {
            ElementTypes = LabelParam.GetGenericAttributesByEnum(typeof(LabelElementType));
        }
    }
}
