

using Label;
using LabelDesigner.UI.Fields;
using System;


namespace LabelDesigner.Artifacts
{
    public class BoxElement : Artifact
    {

        public BoxElement(LabelElement element, PrintLabel label, Action<Artifact> editar, Action<Artifact> selected)
        {
            var e = element;
            if (e == null)
            {
                e = new LabelElement(LabelElementType.GRAPHIC_BOX, label.Home);
                e.UID = Guid.NewGuid().ToString();
                e.Size.Width  = 100;
                e.Size.Height = 100;
                e.Material.Thickness = 4;
                label.Elements.Add(e);
                label.Save();
            }

            var ui = new Box();
            Init(ui, e, editar, selected);
        }

    }
}
