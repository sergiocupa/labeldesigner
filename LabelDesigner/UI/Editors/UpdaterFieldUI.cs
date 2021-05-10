

using Label;
using LabelDesigner.UI.Templates;

namespace LabelDesigner.UI.Editors
{
    internal class UpdaterFieldUI
    {

        internal static void BasicElementData(LabelElement Field, LabelText X, LabelText Y, LabelText Width, LabelText Height, LabelText Thickness)
        {
            X.Content         = X.Content.Replace("\r\n", "");
            Y.Content         = Y.Content.Replace("\r\n", "");
            Width.Content     = Width.Content.Replace("\r\n", "");
            Height.Content    = Height.Content.Replace("\r\n", "");

            var vX = double.Parse(X.Content);
            var vY = double.Parse(Y.Content);
            var vW = double.Parse(Width.Content);
            var vH = double.Parse(Height.Content);
            
            Field.Position.X         = vX;
            Field.Position.Y         = vY;
            Field.Size.Width         = vW;
            Field.Size.Height        = vH;
           
            if(Thickness != null)
            {
                Thickness.Content = Thickness.Content.Replace("\r\n", "");
                var vT = double.Parse(Thickness.Content);
                Field.Material.Thickness = vT;
            }
        }

    }
}
