

using Label.Renderers.Base;
using System;
using System.Text;


namespace Label.Renderers.Qrcode
{
    internal class QRCodeData
    {
         public static int NUM_MASK_PATTERNS = 8;


         public QRCodeData()
        {
            MaskPattern = -1;
        }


        public Mode Mode { get; set; }


        public ErrorCorrectionLevel ECLevel { get; set; }


        public Version Version { get; set; }


        public int MaskPattern { get; set; }


        public ByteMatrix Matrix { get; set; }


        public override String ToString()
        {
            var result = new StringBuilder(200);
            result.Append("<<\n");
            result.Append(" mode: ");
            result.Append(Mode);
            result.Append("\n ecLevel: ");
            result.Append(ECLevel);
            result.Append("\n version: ");
            if (Version == null)
                result.Append("null");
            else
                result.Append(Version);
            result.Append("\n maskPattern: ");
            result.Append(MaskPattern);
            if (Matrix == null)
            {
                result.Append("\n matrix: null\n");
            }
            else
            {
                result.Append("\n matrix:\n");
                result.Append(Matrix.ToString());
            }
            result.Append(">>\n");
            return result.ToString();
        }


        public static bool isValidMaskPattern(int maskPattern)
        {
            return maskPattern >= 0 && maskPattern < NUM_MASK_PATTERNS;
        }
    }
}
