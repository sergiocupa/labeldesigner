

using Label;
using LabelDesigner.UI.Fields;
using System;


namespace LabelDesigner.Artifacts
{
    public class TextElement : Artifact
    {

        public TextElement(LabelElement element, PrintLabel label, Action<Artifact> editar, Action<Artifact> selected)
        {
            var e = element;
            if (e == null)
            {
                e = new LabelElement(LabelElementType.PRINTER_FONT, label.Home);
                e.UID = Guid.NewGuid().ToString();
                e.Font = new TextFont() { FamilyName = "0", Size = 30 };
                e.Value = "1234...";
                label.Elements.Add(e);
                label.Save();
            }

            var ui = new TextField();
            Init(ui, e, editar, selected);
        }

    }
}
