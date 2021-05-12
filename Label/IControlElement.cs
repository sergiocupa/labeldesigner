

using Label.Model;
using System;


namespace Label
{
    public interface IControlElement
    {

        void ResetBack();
        void SelectedBack();

        public Action Draw { get; set; }
        public Action StartEdit { get; set; }
        public Action Selected { get; set; }
        public LabelElement Element { get; set; }
        public Action<LabelPosition> RePosition { get; set; }
        public ResizedDelegate Resized { get; set; }
        public Artifact Artifact { get; set; }
    }

    public delegate void ResizedDelegate(int width, int height);
}
