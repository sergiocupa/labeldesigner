

using Label;
using LabelDesigner.UI.Fields;
using System;


namespace LabelDesigner.Artifacts
{

    public class Barcode128Element : Artifact
    {

        public Barcode128Element(LabelElement element, PrintLabel label, Action<Artifact> editar, Action<Artifact> selected)
        {
            var e = element;
            if(e == null)
            {
                e = new LabelElement(LabelElementType.CODIGO_BARRA_128, label.Home);
                e.UID = Guid.NewGuid().ToString();
                e.Material.Thickness = 3;
                e.Size.Height = 60;
                e.Value = "1234";
                label.Elements.Add(e);
                label.Save();
            }

            var ui = new Barcode128();
            Init(ui, e, editar, selected);
        }
    }
}
