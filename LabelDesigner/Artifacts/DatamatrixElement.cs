

using Label;
using LabelDesigner.UI.Fields;
using System;


namespace LabelDesigner.Artifacts
{
    public class DatamatrixElement : Artifact
    {

        public DatamatrixElement(LabelElement element, PrintLabel label, Action<Artifact> editar, Action<Artifact> selected)
        {
            var e = element;
            if (e == null)
            {
                e = new LabelElement(LabelElementType.DATAMATRIX, label.Home);
                e.UID = Guid.NewGuid().ToString();
                e.Material.Thickness = 4;
                e.Value = "1234";
                label.Elements.Add(e);
                label.Save();
            }

            var ui = new Datamatrix();
            Init(ui, e, editar, selected);
        }

    }
}
