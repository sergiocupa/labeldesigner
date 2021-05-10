

using Label;
using LabelDesigner.UI.Fields;
using System;
using System.Drawing;

namespace LabelDesigner.Artifacts
{
    public class RenderedTextElement : Artifact
    {
        
        public RenderedTextElement(LabelElement element, PrintLabel label, Action<Artifact> editar, Action<Artifact> selected)
        {
            var e = element;
            if (e == null)
            {
                e = new LabelElement(LabelElementType.RENDERED_TEXT, label.Home);
                e.UID = Guid.NewGuid().ToString();
                e.Name = LabelElementIDControl.GetElementImageName();
                e.SetEditedFont(new Font(FontFamily.GenericSansSerif, 30, FontStyle.Regular));
                e.Value = "1234";
                label.Elements.Add(e);
                label.Save();
            }

            var ui = new RenderedTextField();
            Init(ui, e, editar, selected);
        }

    }
}
