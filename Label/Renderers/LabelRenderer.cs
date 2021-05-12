

using Label.Model;
using Label.Zpl;


namespace Label.Renderers
{

    public class LabelRenderer
    {

        public static ILabelRenderer GetInstance(LabelProtocol protocol)
        {
            ILabelRenderer proto = null;

            switch (protocol)
            {
                case LabelProtocol.ZPL2:
                    proto = new ZplRenderer();
                    break;
                case LabelProtocol.EPL:

                    break;
            }
            return proto;
        }

    }


}
