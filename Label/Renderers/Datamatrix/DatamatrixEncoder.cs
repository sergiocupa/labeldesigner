

using Label.Renderers.Base;
using System;
using System.Drawing;


namespace Label.Renderers.Datamatrix
{
    public class DatamatrixEncoder : IEncoder
    {



        public EncoderResult Render(string contents)
        {
            if (string.IsNullOrEmpty(contents))
            {
                throw new ArgumentException("Found empty contents", contents);
            }

            if (Width < 0 || Height < 0)
            {
                throw new ArgumentException("Requested dimensions can't be negative: " + Width + 'x' + Height);
            }

            // Try to get force shape & min / max size
            var shape = ESymbolShapeHint.FORCE_SQUARE;
            var defaultEncodation = Encodation.ASCII;
            Dimension minSize = null;
            Dimension maxSize = null;

            //1. step: Data encodation
            String encoded = HighLevelEncoder.encodeHighLevel(contents, shape, minSize, maxSize, defaultEncodation);

            SymbolInfo symbolInfo = SymbolInfo.lookup(encoded.Length, shape, minSize, maxSize, true);

            //2. step: ECC generation
            String codewords = ErrorCorrection.encodeECC200(encoded, symbolInfo);

            //3. step: Module placement in Matrix
            var placement = new DefaultPlacement(codewords, symbolInfo.getSymbolDataWidth(), symbolInfo.getSymbolDataHeight());
            placement.place();

            var rects = encodeLowLevel(placement, symbolInfo, ElementHeight, Width, Height, Zoom);
            return rects;
        }


        private static EncoderResult encodeLowLevel(DefaultPlacement placement, SymbolInfo symbolInfo, int elementHeigth, int width, int height, double zoom)
        {
            int symbolWidth = symbolInfo.getSymbolDataWidth();
            int symbolHeight = symbolInfo.getSymbolDataHeight();

            var matrix = new ByteMatrix(symbolInfo.getSymbolWidth(), symbolInfo.getSymbolHeight());

            int matrixY = 0;

            for (int y = 0; y < symbolHeight; y++)
            {
                // Fill the top edge with alternate 0 / 1
                int matrixX;
                if ((y % symbolInfo.matrixHeight) == 0)
                {
                    matrixX = 0;
                    for (int x = 0; x < symbolInfo.getSymbolWidth(); x++)
                    {
                        matrix.set(matrixX, matrixY, (x % 2) == 0);
                        matrixX++;
                    }
                    matrixY++;
                }
                matrixX = 0;
                for (int x = 0; x < symbolWidth; x++)
                {
                    // Fill the right edge with full 1
                    if ((x % symbolInfo.matrixWidth) == 0)
                    {
                        matrix.set(matrixX, matrixY, true);
                        matrixX++;
                    }
                    matrix.set(matrixX, matrixY, placement.getBit(x, y));
                    matrixX++;
                    // Fill the right edge with alternate 0 / 1
                    if ((x % symbolInfo.matrixWidth) == symbolInfo.matrixWidth - 1)
                    {
                        matrix.set(matrixX, matrixY, (y % 2) == 0);
                        matrixX++;
                    }
                }
                matrixY++;
                // Fill the bottom edge with full 1
                if ((y % symbolInfo.matrixHeight) == symbolInfo.matrixHeight - 1)
                {
                    matrixX = 0;
                    for (int x = 0; x < symbolInfo.getSymbolWidth(); x++)
                    {
                        matrix.set(matrixX, matrixY, true);
                        matrixX++;
                    }
                    matrixY++;
                }
            }

            if(elementHeigth >= 1)
            {
                var W = matrix.Width * elementHeigth;
                var H = matrix.Height * elementHeigth;
                return convertByteMatrixToBitMatrix(matrix, W, H, zoom);
            }
            else
            {
                return convertByteMatrixToBitMatrix(matrix, width, height, zoom);
            }
        }


        private static EncoderResult convertByteMatrixToBitMatrix(ByteMatrix matrix, int reqWidth, int reqHeight, double zoom)
        {
            EncoderResult result = new EncoderResult();
            var matrixWidth = matrix.Width;
            var matrixHeight = matrix.Height;
            var outputWidth = Math.Max(reqWidth, matrixWidth);
            var outputHeight = Math.Max(reqHeight, matrixHeight);

            int multiple = Math.Min(outputWidth / matrixWidth, outputHeight / matrixHeight);

            int leftPadding = (outputWidth - (matrixWidth * multiple)) / 2;
            int topPadding = (outputHeight - (matrixHeight * multiple)) / 2;

            BitMatrix output;

            // remove padding if requested width and height are too small
            if (reqHeight < matrixHeight || reqWidth < matrixWidth)
            {
                leftPadding = 0;
                topPadding = 0;
                output = new BitMatrix(matrixWidth, matrixHeight);
            }
            else
            {
                output = new BitMatrix(reqWidth, reqHeight);
            }

            output.clear();
            for (int inputY = 0, outputY = topPadding; inputY < matrixHeight; inputY++, outputY += multiple)
            {
                // Write the contents of this row of the bytematrix
                for (int inputX = 0, outputX = leftPadding; inputX < matrixWidth; inputX++, outputX += multiple)
                {
                    if (matrix[inputX, inputY] == 1)
                    {
                        var x_z = outputX  * zoom;
                        var y_z = outputY  * zoom;
                        var m_z = multiple * zoom;

                        var r = new Rectangle((int)x_z, (int)y_z, (int)m_z, (int)m_z);
                        result.Rectangles.Add(r);
                        result.Width = (int)(x_z + m_z);
                        result.Height = (int)(y_z + m_z);
                    }
                }
            }

            return result;
        }


        public int ElementHeight { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public double Zoom { get; set; }


        public DatamatrixEncoder(int elementHeight, int width, int height, double zoom)
        {
            Width = width;
            Height = height;
            ElementHeight = elementHeight;
            Zoom = zoom;
        }
    }
}
