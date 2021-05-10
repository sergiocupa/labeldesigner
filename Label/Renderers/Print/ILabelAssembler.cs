

using System.Text;


namespace Label.Renderers.Print
{
    public interface ILabelAssembler
    {


        StringBuilder AssembleElement(LabelElement element, PrintableField field);


    }
}
