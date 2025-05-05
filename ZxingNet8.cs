using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using ZXing.Windows.Compatibility;
using ZXing;
using ZXing.Common;


namespace ZxingBarcode
{
    public class ZxingNet8
    {
        public string ReadBarcode(string imageName)
        {
            try
            {
                BarcodeReader reader;
                List<BarcodeFormat> formatList = new List<BarcodeFormat>();

                formatList.Add(BarcodeFormat.AZTEC);
                formatList.Add(BarcodeFormat.CODABAR);
                formatList.Add(BarcodeFormat.CODE_39);
                formatList.Add(BarcodeFormat.CODE_93);
                formatList.Add(BarcodeFormat.CODE_128);
                formatList.Add(BarcodeFormat.DATA_MATRIX);
                formatList.Add(BarcodeFormat.EAN_8);
                formatList.Add(BarcodeFormat.EAN_13);
                formatList.Add(BarcodeFormat.ITF);
                formatList.Add(BarcodeFormat.MAXICODE);
                formatList.Add(BarcodeFormat.PDF_417);
                formatList.Add(BarcodeFormat.QR_CODE);
                formatList.Add(BarcodeFormat.RSS_14);
                formatList.Add(BarcodeFormat.RSS_EXPANDED);
                formatList.Add(BarcodeFormat.UPC_A);
                formatList.Add(BarcodeFormat.UPC_E);
                formatList.Add(BarcodeFormat.UPC_EAN_EXTENSION);
                formatList.Add(BarcodeFormat.MSI);
                formatList.Add(BarcodeFormat.PLESSEY);
                formatList.Add(BarcodeFormat.IMB);
                formatList.Add(BarcodeFormat.PHARMA_CODE);

                reader = new BarcodeReader { AutoRotate = true };
                reader.Options.PossibleFormats = formatList;
                reader.Options.TryHarder = true;
                reader.Options.TryInverted = true;

                Bitmap Image = (Bitmap)Bitmap.FromFile(imageName);
                Result result = reader.Decode(Image);
                Image.Dispose();

                if (result == null)
                {
                    return "";
                }
                return result.Text.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string BarcodeGenerate(string source, string outputFile, int inFormat, int height, int width, bool pureBarcode, int margin)
        {
            try
            {


                BarcodeWriter writer = new BarcodeWriter();
                switch (inFormat)
                {
                    case 1:
                        writer.Format = BarcodeFormat.AZTEC;
                        break;
                    case 2:
                        writer.Format = BarcodeFormat.CODABAR;
                        break;
                    case 3:
                        writer.Format = BarcodeFormat.CODE_39;
                        break;
                    case 4:
                        writer.Format = BarcodeFormat.CODE_93;
                        break;
                    case 5:
                        writer.Format = BarcodeFormat.CODE_128;
                        break;
                    case 6:
                        writer.Format = BarcodeFormat.DATA_MATRIX;
                        break;
                    case 7:
                        writer.Format = BarcodeFormat.EAN_8;
                        break;
                    case 8:
                        writer.Format = BarcodeFormat.EAN_13;
                        break;
                    case 9:
                        writer.Format = BarcodeFormat.ITF;
                        break;
                    case 10:
                        writer.Format = BarcodeFormat.MAXICODE;
                        break;
                    case 11:
                        writer.Format = BarcodeFormat.PDF_417;
                        break;
                    case 12:
                        writer.Format = BarcodeFormat.QR_CODE;
                        break;
                    case 13:
                        writer.Format = BarcodeFormat.RSS_14;
                        break;
                    case 14:
                        writer.Format = BarcodeFormat.RSS_EXPANDED;
                        break;
                    case 15:
                        writer.Format = BarcodeFormat.UPC_A;
                        break;
                    case 16:
                        writer.Format = BarcodeFormat.UPC_E;
                        break;
                    case 17:
                        writer.Format = BarcodeFormat.UPC_EAN_EXTENSION;
                        break;
                    case 18:
                        writer.Format = BarcodeFormat.MSI;
                        break;
                    case 19:
                        writer.Format = BarcodeFormat.PLESSEY;
                        break;
                    case 20:
                        writer.Format = BarcodeFormat.IMB;
                        break;
                    case 21:
                        writer.Format = BarcodeFormat.PHARMA_CODE;
                        break;
                    case 22:
                        writer.Format = BarcodeFormat.All_1D;
                        break;
                }
                {

                    writer.Options = new EncodingOptions
                    {
                        Height = height,
                        Width = width,
                        PureBarcode = pureBarcode,
                        Margin = margin,
                    };
                };

                var bitmap = writer.Write(source);
                bitmap.Save(outputFile);
                return outputFile;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


    }
}
