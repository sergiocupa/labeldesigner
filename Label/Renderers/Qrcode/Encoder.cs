﻿

using Label.Renderers.Base;
using System;
using System.Collections.Generic;
using System.Text;


namespace Label.Renderers.Qrcode
{
    internal class Encoder
    {

        // The original table is defined in the table 5 of JISX0510:2004 (p.19).
        private static readonly int[] ALPHANUMERIC_TABLE = {
         -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  // 0x00-0x0f
         -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  // 0x10-0x1f
         36, -1, -1, -1, 37, 38, -1, -1, -1, -1, 39, 40, -1, 41, 42, 43,  // 0x20-0x2f
         0,   1,  2,  3,  4,  5,  6,  7,  8,  9, 44, -1, -1, -1, -1, -1,  // 0x30-0x3f
         -1, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24,  // 0x40-0x4f
         25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, -1, -1, -1, -1, -1,  // 0x50-0x5f
       };

        internal static String DEFAULT_BYTE_MODE_ENCODING = "ISO-8859-1";

        // The mask penalty calculation is complicated.  See Table 21 of JISX0510:2004 (p.45) for details.
        // Basically it applies four rules and summate all penalties.
        private static int calculateMaskPenalty(ByteMatrix matrix)
        {
            return MaskUtil.applyMaskPenaltyRule1(matrix)
                    + MaskUtil.applyMaskPenaltyRule2(matrix)
                    + MaskUtil.applyMaskPenaltyRule3(matrix)
                    + MaskUtil.applyMaskPenaltyRule4(matrix);
        }



        public static QRCodeData encode(String content, ErrorCorrectionLevel ecLevel)
        {
            return encode(content, ecLevel, null);
        }



        public static QRCodeData encode(String content, ErrorCorrectionLevel ecLevel, IDictionary<EEncodeHintType, object> hints)
        {
            // Determine what character encoding has been specified by the caller, if any
            bool hasEncodingHint = hints != null && hints.ContainsKey(EEncodeHintType.CHARACTER_SET);

            var encoding = hints == null || !hints.ContainsKey(EEncodeHintType.CHARACTER_SET) ? null : (String)hints[EEncodeHintType.CHARACTER_SET];
            if (encoding == null)
            {
                encoding = DEFAULT_BYTE_MODE_ENCODING;
            }
            var generateECI = hasEncodingHint || !DEFAULT_BYTE_MODE_ENCODING.Equals(encoding);

            // Pick an encoding mode appropriate for the content. Note that this will not attempt to use
            // multiple modes / segments even if that were more efficient. Twould be nice.
            var mode = chooseMode(content, encoding);

            // This will store the header information, like mode and
            // length, as well as "header" segments like an ECI segment.
            var headerBits = new BitArray();

            // Append ECI segment if applicable
            if (mode == Mode.BYTE && generateECI)
            {
                var eci = CharacterSetECI.getCharacterSetECIByName(encoding);
                if (eci != null)
                {
                    var eciIsExplicitDisabled = (hints != null && hints.ContainsKey(EEncodeHintType.DISABLE_ECI) && hints[EEncodeHintType.DISABLE_ECI] != null && Convert.ToBoolean(hints[EEncodeHintType.DISABLE_ECI].ToString()));
                    if (!eciIsExplicitDisabled)
                    {
                        appendECI(eci, headerBits);
                    }
                }
            }

            // Append the FNC1 mode header for GS1 formatted data if applicable
            var hasGS1FormatHint = hints != null && hints.ContainsKey(EEncodeHintType.GS1_FORMAT);
            if (hasGS1FormatHint && hints[EEncodeHintType.GS1_FORMAT] != null && Convert.ToBoolean(hints[EEncodeHintType.GS1_FORMAT].ToString()))
            {
                // GS1 formatted codes are prefixed with a FNC1 in first position mode header
                appendModeInfo(Mode.FNC1_FIRST_POSITION, headerBits);
            }

            // (With ECI in place,) Write the mode marker
            appendModeInfo(mode, headerBits);

            // Collect data within the main segment, separately, to count its size if needed. Don't add it to
            // main payload yet.
            var dataBits = new BitArray();
            appendBytes(content, mode, dataBits, encoding);

            Version version;
            if (hints != null && hints.ContainsKey(EEncodeHintType.QR_VERSION))
            {
                int versionNumber = Int32.Parse(hints[EEncodeHintType.QR_VERSION].ToString());
                version = Version.getVersionForNumber(versionNumber);
                int bitsNeeded = calculateBitsNeeded(mode, headerBits, dataBits, version);
                if (!willFit(bitsNeeded, version, ecLevel))
                {
                    throw new Exception("Data too big for requested version");
                }
            }
            else
            {
                version = recommendVersion(ecLevel, mode, headerBits, dataBits);
            }

            var headerAndDataBits = new BitArray();
            headerAndDataBits.appendBitArray(headerBits);
            // Find "length" of main segment and write it
            var numLetters = mode == Mode.BYTE ? dataBits.SizeInBytes : content.Length;
            appendLengthInfo(numLetters, version, mode, headerAndDataBits);
            // Put data together into the overall payload
            headerAndDataBits.appendBitArray(dataBits);

            var ecBlocks = version.getECBlocksForLevel(ecLevel);
            var numDataBytes = version.TotalCodewords - ecBlocks.TotalECCodewords;

            // Terminate the bits properly.
            terminateBits(numDataBytes, headerAndDataBits);

            // Interleave data bits with error correction code.
            var finalBits = interleaveWithECBytes(headerAndDataBits,
                                                       version.TotalCodewords,
                                                       numDataBytes,
                                                       ecBlocks.NumBlocks);

            var qrCode = new QRCodeData
            {
                ECLevel = ecLevel,
                Mode = mode,
                Version = version
            };

            //  Choose the mask pattern and set to "qrCode".
            var dimension = version.DimensionForVersion;
            var matrix = new ByteMatrix(dimension, dimension);

            // Enable manual selection of the pattern to be used via hint
            var maskPattern = -1;
            if (hints != null && hints.ContainsKey(EEncodeHintType.QR_MASK_PATTERN))
            {
                var hintMaskPattern = Int32.Parse(hints[EEncodeHintType.QR_MASK_PATTERN].ToString());
                maskPattern = QRCodeData.isValidMaskPattern(hintMaskPattern) ? hintMaskPattern : -1;
            }

            if (maskPattern == -1)
            {
                maskPattern = chooseMaskPattern(finalBits, ecLevel, version, matrix);
            }
            qrCode.MaskPattern = maskPattern;

            // Build the matrix and set it to "qrCode".
            MatrixUtil.buildMatrix(finalBits, ecLevel, version, maskPattern, matrix);
            qrCode.Matrix = matrix;

            return qrCode;
        }



        private static Version recommendVersion(ErrorCorrectionLevel ecLevel, Mode mode, BitArray headerBits, BitArray dataBits)
        {
            // Hard part: need to know version to know how many bits length takes. But need to know how many
            // bits it takes to know version. First we take a guess at version by assuming version will be
            // the minimum, 1:
            var provisionalBitsNeeded = calculateBitsNeeded(mode, headerBits, dataBits, Version.getVersionForNumber(1));
            var provisionalVersion = chooseVersion(provisionalBitsNeeded, ecLevel);

            // Use that guess to calculate the right version. I am still not sure this works in 100% of cases.
            var bitsNeeded = calculateBitsNeeded(mode, headerBits, dataBits, provisionalVersion);
            return chooseVersion(bitsNeeded, ecLevel);
        }

        private static int calculateBitsNeeded(Mode mode, BitArray headerBits, BitArray dataBits, Version version)
        {
            return headerBits.Size + mode.getCharacterCountBits(version) + dataBits.Size;
        }



        internal static int getAlphanumericCode(int code)
        {
            if (code < ALPHANUMERIC_TABLE.Length)
            {
                return ALPHANUMERIC_TABLE[code];
            }
            return -1;
        }



        public static Mode chooseMode(String content)
        {
            return chooseMode(content, null);
        }



        private static Mode chooseMode(String content, String encoding)
        {
            if ("Shift_JIS".Equals(encoding) && isOnlyDoubleByteKanji(content))
            {
                // Choose Kanji mode if all input are double-byte characters
                return Mode.KANJI;
            }
            bool hasNumeric = false;
            bool hasAlphanumeric = false;
            for (int i = 0; i < content.Length; ++i)
            {
                char c = content[i];
                if (c >= '0' && c <= '9')
                {
                    hasNumeric = true;
                }
                else if (getAlphanumericCode(c) != -1)
                {
                    hasAlphanumeric = true;
                }
                else
                {
                    return Mode.BYTE;
                }
            }
            if (hasAlphanumeric)
            {

                return Mode.ALPHANUMERIC;
            }
            if (hasNumeric)
            {

                return Mode.NUMERIC;
            }
            return Mode.BYTE;
        }

        private static bool isOnlyDoubleByteKanji(String content)
        {
            byte[] bytes;
            try
            {
                bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(content);
            }
            catch (Exception)
            {
                return false;
            }
            int length = bytes.Length;
            if (length % 2 != 0)
            {
                return false;
            }
            for (int i = 0; i < length; i += 2)
            {


                int byte1 = bytes[i] & 0xFF;
                if ((byte1 < 0x81 || byte1 > 0x9F) && (byte1 < 0xE0 || byte1 > 0xEB))
                {

                    return false;
                }
            }
            return true;
        }

        private static int chooseMaskPattern(BitArray bits,
                                             ErrorCorrectionLevel ecLevel,
                                             Version version,
                                             ByteMatrix matrix)
        {
            int minPenalty = Int32.MaxValue;  // Lower penalty is better.
            int bestMaskPattern = -1;
            // We try all mask patterns to choose the best one.
            for (int maskPattern = 0; maskPattern < QRCodeData.NUM_MASK_PATTERNS; maskPattern++)
            {

                MatrixUtil.buildMatrix(bits, ecLevel, version, maskPattern, matrix);
                int penalty = calculateMaskPenalty(matrix);
                if (penalty < minPenalty)
                {

                    minPenalty = penalty;
                    bestMaskPattern = maskPattern;
                }
            }
            return bestMaskPattern;
        }

        private static Version chooseVersion(int numInputBits, ErrorCorrectionLevel ecLevel)
        {
            for (int versionNum = 1; versionNum <= 40; versionNum++)
            {
                var version = Version.getVersionForNumber(versionNum);
                if (willFit(numInputBits, version, ecLevel))
                {
                    return version;
                }
            }
            throw new Exception("Data too big");
        }


        private static bool willFit(int numInputBits, Version version, ErrorCorrectionLevel ecLevel)
        {
            // In the following comments, we use numbers of Version 7-H.
            // numBytes = 196
            var numBytes = version.TotalCodewords;
            // getNumECBytes = 130
            var ecBlocks = version.getECBlocksForLevel(ecLevel);
            var numEcBytes = ecBlocks.TotalECCodewords;
            // getNumDataBytes = 196 - 130 = 66
            var numDataBytes = numBytes - numEcBytes;
            var totalInputBytes = (numInputBits + 7) / 8;
            return numDataBytes >= totalInputBytes;
        }



        internal static void terminateBits(int numDataBytes, BitArray bits)
        {
            int capacity = numDataBytes << 3;
            if (bits.Size > capacity)
            {
                throw new Exception("data bits cannot fit in the QR Code" + bits.Size + " > " + capacity);
            }
            for (int i = 0; i < 4 && bits.Size < capacity; ++i)
            {
                bits.appendBit(false);
            }
            // Append termination bits. See 8.4.8 of JISX0510:2004 (p.24) for details.
            // If the last byte isn't 8-bit aligned, we'll add padding bits.
            int numBitsInLastByte = bits.Size & 0x07;
            if (numBitsInLastByte > 0)
            {
                for (int i = numBitsInLastByte; i < 8; i++)
                {
                    bits.appendBit(false);
                }
            }
            // If we have more space, we'll fill the space with padding patterns defined in 8.4.9 (p.24).
            int numPaddingBytes = numDataBytes - bits.SizeInBytes;
            for (int i = 0; i < numPaddingBytes; ++i)
            {
                bits.appendBits((i & 0x01) == 0 ? 0xEC : 0x11, 8);
            }
            if (bits.Size != capacity)
            {
                throw new Exception("Bits size does not equal capacity");
            }
        }



        internal static void getNumDataBytesAndNumECBytesForBlockID(int numTotalBytes,
                                                           int numDataBytes,
                                                           int numRSBlocks,
                                                           int blockID,
                                                           int[] numDataBytesInBlock,
                                                           int[] numECBytesInBlock)
        {
            if (blockID >= numRSBlocks)
            {
                throw new Exception("Block ID too large");
            }
            // numRsBlocksInGroup2 = 196 % 5 = 1
            int numRsBlocksInGroup2 = numTotalBytes % numRSBlocks;
            // numRsBlocksInGroup1 = 5 - 1 = 4
            int numRsBlocksInGroup1 = numRSBlocks - numRsBlocksInGroup2;
            // numTotalBytesInGroup1 = 196 / 5 = 39
            int numTotalBytesInGroup1 = numTotalBytes / numRSBlocks;
            // numTotalBytesInGroup2 = 39 + 1 = 40
            int numTotalBytesInGroup2 = numTotalBytesInGroup1 + 1;
            // numDataBytesInGroup1 = 66 / 5 = 13
            int numDataBytesInGroup1 = numDataBytes / numRSBlocks;
            // numDataBytesInGroup2 = 13 + 1 = 14
            int numDataBytesInGroup2 = numDataBytesInGroup1 + 1;
            // numEcBytesInGroup1 = 39 - 13 = 26
            int numEcBytesInGroup1 = numTotalBytesInGroup1 - numDataBytesInGroup1;
            // numEcBytesInGroup2 = 40 - 14 = 26
            int numEcBytesInGroup2 = numTotalBytesInGroup2 - numDataBytesInGroup2;
            // Sanity checks.
            // 26 = 26
            if (numEcBytesInGroup1 != numEcBytesInGroup2)
            {

                throw new Exception("EC bytes mismatch");
            }
            // 5 = 4 + 1.
            if (numRSBlocks != numRsBlocksInGroup1 + numRsBlocksInGroup2)
            {

                throw new Exception("RS blocks mismatch");
            }
            // 196 = (13 + 26) * 4 + (14 + 26) * 1
            if (numTotalBytes !=
                ((numDataBytesInGroup1 + numEcBytesInGroup1) *
                    numRsBlocksInGroup1) +
                    ((numDataBytesInGroup2 + numEcBytesInGroup2) *
                        numRsBlocksInGroup2))
            {
                throw new Exception("Total bytes mismatch");
            }

            if (blockID < numRsBlocksInGroup1)
            {

                numDataBytesInBlock[0] = numDataBytesInGroup1;
                numECBytesInBlock[0]   = numEcBytesInGroup1;
            }
            else
            {


                numDataBytesInBlock[0] = numDataBytesInGroup2;
                numECBytesInBlock[0]   = numEcBytesInGroup2;
            }
        }



        internal static BitArray interleaveWithECBytes(BitArray bits,
                                               int numTotalBytes,
                                               int numDataBytes,
                                               int numRSBlocks)
        {
            // "bits" must have "getNumDataBytes" bytes of data.
            if (bits.SizeInBytes != numDataBytes)
            {

                throw new Exception("Number of bits and data bytes does not match");
            }

            // Step 1.  Divide data bytes into blocks and generate error correction bytes for them. We'll
            // store the divided data bytes blocks and error correction bytes blocks into "blocks".
            int dataBytesOffset = 0;
            int maxNumDataBytes = 0;
            int maxNumEcBytes = 0;

            // Since, we know the number of reedsolmon blocks, we can initialize the vector with the number.
            var blocks = new List<BlockPair>(numRSBlocks);

            for (int i = 0; i < numRSBlocks; ++i)
            {

                int[] numDataBytesInBlock = new int[1];
                int[] numEcBytesInBlock = new int[1];
                getNumDataBytesAndNumECBytesForBlockID(
                    numTotalBytes, numDataBytes, numRSBlocks, i,
                    numDataBytesInBlock, numEcBytesInBlock);

                int size = numDataBytesInBlock[0];
                byte[] dataBytes = new byte[size];
                bits.toBytes(8 * dataBytesOffset, dataBytes, 0, size);
                byte[] ecBytes = generateECBytes(dataBytes, numEcBytesInBlock[0]);
                blocks.Add(new BlockPair(dataBytes, ecBytes));

                maxNumDataBytes = Math.Max(maxNumDataBytes, size);
                maxNumEcBytes = Math.Max(maxNumEcBytes, ecBytes.Length);
                dataBytesOffset += numDataBytesInBlock[0];
            }
            if (numDataBytes != dataBytesOffset)
            {

                throw new Exception("Data bytes does not match offset");
            }

            BitArray result = new BitArray();

            // First, place data blocks.
            for (int i = 0; i < maxNumDataBytes; ++i)
            {
                foreach (BlockPair block in blocks)
                {
                    byte[] dataBytes = block.DataBytes;
                    if (i < dataBytes.Length)
                    {
                        result.appendBits(dataBytes[i], 8);
                    }
                }
            }
            // Then, place error correction blocks.
            for (int i = 0; i < maxNumEcBytes; ++i)
            {
                foreach (BlockPair block in blocks)
                {
                    byte[] ecBytes = block.ErrorCorrectionBytes;
                    if (i < ecBytes.Length)
                    {
                        result.appendBits(ecBytes[i], 8);
                    }
                }
            }
            if (numTotalBytes != result.SizeInBytes)
            {  // Should be same.
                throw new Exception("Interleaving error: " + numTotalBytes + " and " + result.SizeInBytes + " differ.");
            }

            return result;
        }

        internal static byte[] generateECBytes(byte[] dataBytes, int numEcBytesInBlock)
        {
            int numDataBytes = dataBytes.Length;
            int[] toEncode = new int[numDataBytes + numEcBytesInBlock];
            for (int i = 0; i < numDataBytes; i++)
            {
                toEncode[i] = dataBytes[i] & 0xFF;

            }
            new ReedSolomonEncoder(GenericGF.QR_CODE_FIELD_256).encode(toEncode, numEcBytesInBlock);

            byte[] ecBytes = new byte[numEcBytesInBlock];
            for (int i = 0; i < numEcBytesInBlock; i++)
            {
                ecBytes[i] = (byte)toEncode[numDataBytes + i];

            }
            return ecBytes;
        }



        internal static void appendModeInfo(Mode mode, BitArray bits)
        {
            bits.appendBits(mode.Bits, 4);
        }




        internal static void appendLengthInfo(int numLetters, Version version, Mode mode, BitArray bits)
        {
            int numBits = mode.getCharacterCountBits(version);
            if (numLetters >= (1 << numBits))
            {
                throw new Exception(numLetters + " is bigger than " + ((1 << numBits) - 1));
            }
            bits.appendBits(numLetters, numBits);
        }



        internal static void appendBytes(String content,
                                Mode mode,
                                BitArray bits,
                                String encoding)
        {
            if (mode.Equals(Mode.NUMERIC))
                appendNumericBytes(content, bits);
            else
                if (mode.Equals(Mode.ALPHANUMERIC))
                    appendAlphanumericBytes(content, bits);
                else
                    if (mode.Equals(Mode.BYTE))
                        append8BitBytes(content, bits, encoding);
                    else
                        if (mode.Equals(Mode.KANJI))
                            appendKanjiBytes(content, bits);
                        else
                            throw new Exception("Invalid mode: " + mode);
        }

        internal static void appendNumericBytes(String content, BitArray bits)
        {
            int length = content.Length;

            int i = 0;
            while (i < length)
            {
                int num1 = content[i] - '0';
                if (i + 2 < length)
                {
                    // Encode three numeric letters in ten bits.
                    int num2 = content[i + 1] - '0';
                    int num3 = content[i + 2] - '0';
                    bits.appendBits(num1 * 100 + num2 * 10 + num3, 10);
                    i += 3;
                }
                else if (i + 1 < length)
                {
                    // Encode two numeric letters in seven bits.
                    int num2 = content[i + 1] - '0';
                    bits.appendBits(num1 * 10 + num2, 7);
                    i += 2;
                }
                else
                {
                    // Encode one numeric letter in four bits.
                    bits.appendBits(num1, 4);
                    i++;
                }
            }
        }

        internal static void appendAlphanumericBytes(String content, BitArray bits)
        {
            int length = content.Length;

            int i = 0;
            while (i < length)
            {
                int code1 = getAlphanumericCode(content[i]);
                if (code1 == -1)
                {
                    throw new Exception();
                }
                if (i + 1 < length)
                {
                    int code2 = getAlphanumericCode(content[i + 1]);
                    if (code2 == -1)
                    {
                        throw new Exception();
                    }
                    // Encode two alphanumeric letters in 11 bits.
                    bits.appendBits(code1 * 45 + code2, 11);
                    i += 2;
                }
                else
                {
                    // Encode one alphanumeric letter in six bits.
                    bits.appendBits(code1, 6);
                    i++;
                }
            }
        }

        internal static void append8BitBytes(String content, BitArray bits, String encoding)
        {
            byte[] bytes;
            try
            {
                bytes = Encoding.GetEncoding(encoding).GetBytes(content);
            }
            catch (Exception uee)
            {
                throw new Exception(uee.Message, uee);
            }
            foreach (byte b in bytes)
            {
                bits.appendBits(b, 8);
            }
        }

        internal static void appendKanjiBytes(String content, BitArray bits)
        {
            byte[] bytes;
            try
            {
                bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(content);
            }
            catch (Exception uee)
            {
                throw new Exception(uee.Message, uee);
            }
            if (bytes.Length % 2 != 0)
            {
                throw new Exception("Kanji byte size not even");
            }
            int maxI = bytes.Length - 1; // bytes.length must be even
            for (int i = 0; i < maxI; i += 2)
            {
                int byte1 = bytes[i] & 0xFF;
                int byte2 = bytes[i + 1] & 0xFF;
                int code = (byte1 << 8) | byte2;
                int subtracted = -1;
                if (code >= 0x8140 && code <= 0x9ffc)
                {

                    subtracted = code - 0x8140;
                }
                else if (code >= 0xe040 && code <= 0xebbf)
                {
                    subtracted = code - 0xc140;
                }
                if (subtracted == -1)
                {

                    throw new Exception("Invalid byte sequence");
                }
                int encoded = ((subtracted >> 8) * 0xc0) + (subtracted & 0xff);
                bits.appendBits(encoded, 13);
            }
        }

        private static void appendECI(CharacterSetECI eci, BitArray bits)
        {
            bits.appendBits(Mode.ECI.Bits, 4);

            // This is correct for values up to 127, which is all we need now.
            bits.appendBits(eci.Value, 8);
        }
    }
}
