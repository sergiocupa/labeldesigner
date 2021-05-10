

using Label;
using LabelDesigner.UI.Fields;
using System;


namespace LabelDesigner.Artifacts
{
    public class ImageElement : Artifact
    {
        public ImageElement(LabelElement element, PrintLabel label, Action<Artifact> editar, Action<Artifact> selected)
        {
            var e = element;
            if (e == null)
            {
                e = new LabelElement(LabelElementType.PRELOADED_IMAGE, label.Home);
                e.UID  = Guid.NewGuid().ToString();
                e.Name = LabelElementIDControl.GetElementImageName();

                label.Elements.Add(e);
                label.Save();
            }

            var ui = new ImageField();
            Init(ui, e, editar, selected);
        }
    }
}
