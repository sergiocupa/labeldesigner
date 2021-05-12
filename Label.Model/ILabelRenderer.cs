

using System.Drawing;


namespace Label.Model
{

    public interface ILabelRenderer
    {

        Font GetPrinterFont(string font_name, double size);

    }


    public interface ILabelAssembler
    {

        string AssembleLabel(PrintLabel label);
        string AssembleLabel(PrintLabel label, object data);

    }


    public enum LabelProtocol
    {
        UNKNOWN = 0,
        ZPL2    = 1,
        EPL     = 2
    }


  

}
