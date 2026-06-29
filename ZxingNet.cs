using System.Collections.Generic;
using System.Drawing;
using ZXing.Windows.Compatibility;
using ZXing;
using ZXing.Common;

namespace ZxingBarcode
{
    /// <summary>
    /// Versión "clásica" del lector/generador de códigos de barras con ZXing.Net.
    /// <para>
    /// Fijaos en un detalle importante: este ejemplo usa el binding
    /// <c>ZXing.Net.Bindings.Windows.Compatibility</c>, que trabaja con
    /// <see cref="System.Drawing.Bitmap"/> (GDI+, solo Windows). Es la opción más directa si
    /// vuestra aplicación ya vive en Windows. El proyecto hermano <c>RSRBarcode</c> enseña la
    /// alternativa multiplataforma con el binding de SkiaSharp (sin System.Drawing).
    /// </para>
    /// <para>
    /// Desde PowerBuilder se instancia esta clase con el .NET DLL Importer y se llaman sus
    /// métodos públicos directamente. Todos devuelven <c>string</c>, así que se consumen sin
    /// complicaciones desde PB.
    /// </para>
    /// </summary>
    public class ZxingNet
    {
        /// <summary>
        /// Lee un código de barras desde un fichero de imagen y devuelve su contenido.
        /// </summary>
        /// <param name="imageName">Ruta completa del fichero de imagen (PNG, JPG, BMP...).</param>
        /// <returns>
        /// El texto decodificado; cadena vacía si no se reconoce ningún código; o el mensaje de
        /// error si algo falla. Devolvemos siempre un <c>string</c> para que PowerBuilder nunca
        /// reciba una excepción .NET que tenga que capturar.
        /// </returns>
        public string ReadBarcode(string imageName)
        {
            try
            {
                // Le indicamos a ZXing TODOS los formatos que queremos intentar reconocer.
                // Truco: cuantos menos formatos pongáis, más rápida y fiable es la lectura;
                // aquí los metemos todos a modo de demo. Usamos la expresión de colección [...]
                // de C# 12 en lugar de encadenar veintitantas llamadas a .Add(): mismo resultado,
                // mucho más legible.
                List<BarcodeFormat> formatList =
                [
                    BarcodeFormat.AZTEC,
                    BarcodeFormat.CODABAR,
                    BarcodeFormat.CODE_39,
                    BarcodeFormat.CODE_93,
                    BarcodeFormat.CODE_128,
                    BarcodeFormat.DATA_MATRIX,
                    BarcodeFormat.EAN_8,
                    BarcodeFormat.EAN_13,
                    BarcodeFormat.ITF,
                    BarcodeFormat.MAXICODE,
                    BarcodeFormat.PDF_417,
                    BarcodeFormat.QR_CODE,
                    BarcodeFormat.RSS_14,
                    BarcodeFormat.RSS_EXPANDED,
                    BarcodeFormat.UPC_A,
                    BarcodeFormat.UPC_E,
                    BarcodeFormat.UPC_EAN_EXTENSION,
                    BarcodeFormat.MSI,
                    BarcodeFormat.PLESSEY,
                    BarcodeFormat.IMB,
                    BarcodeFormat.PHARMA_CODE,
                ];

                // new() con tipo deducido (target-typed): el compilador ya sabe que es un
                // BarcodeReader por la declaración de la izquierda.
                BarcodeReader reader = new() { AutoRotate = true }; // AutoRotate: reintenta girando la imagen
                reader.Options.PossibleFormats = formatList;
                reader.Options.TryHarder = true;    // dedica más CPU a "esforzarse" en leer
                reader.Options.TryInverted = true;  // prueba también con los colores invertidos

                // El Bitmap es un recurso nativo (GDI+): con 'using' se libera siempre al salir
                // del método, incluso si Decode() lanza. Sustituye al Dispose() manual de antes.
                using Bitmap image = (Bitmap)Bitmap.FromFile(imageName);
                Result result = reader.Decode(image);

                // Decode() devuelve null si no encontró ningún código en la imagen.
                if (result == null)
                {
                    return "";
                }
                return result.Text;
            }
            catch (Exception ex)
            {
                // Patrón del ejemplo: devolvemos el mensaje en vez de propagar la excepción, para
                // que PowerBuilder lo muestre tal cual sin tener que lidiar con errores .NET.
                return ex.Message;
            }
        }

        /// <summary>
        /// Genera un código de barras a partir de un texto y lo guarda como imagen en disco.
        /// </summary>
        /// <param name="source">Texto/valor a codificar dentro del código.</param>
        /// <param name="outputFile">Ruta del fichero de salida (la extensión decide el formato, p. ej. .png).</param>
        /// <param name="inFormat">
        /// Tipo de código a generar como número del 1 al 22 (1=AZTEC, 3=CODE_39, 12=QR_CODE...).
        /// Usamos un <c>int</c> en vez del enum porque desde PowerBuilder es lo cómodo de pasar.
        /// </param>
        /// <param name="height">Alto del código en píxeles.</param>
        /// <param name="width">Ancho del código en píxeles.</param>
        /// <param name="pureBarcode">Si es <c>true</c>, dibuja solo las barras sin el texto debajo.</param>
        /// <param name="margin">Margen blanco (quiet zone) alrededor del código.</param>
        /// <returns>La ruta del fichero generado; o el mensaje de error si algo falla.</returns>
        public string BarcodeGenerate(string source, string outputFile, int inFormat, int height, int width, bool pureBarcode, int margin)
        {
            try
            {
                BarcodeWriter writer = new();

                // Traducimos el número que llega de PowerBuilder al enum BarcodeFormat de ZXing.
                // Mantenemos el switch clásico a propósito: es transparente y fácil de ampliar.
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

                // Opciones de dibujo del código (tamaño, margen y si lleva o no el texto).
                writer.Options = new EncodingOptions
                {
                    Height = height,
                    Width = width,
                    PureBarcode = pureBarcode,
                    Margin = margin,
                };

                // Write() devuelve un Bitmap (recurso nativo): 'using' garantiza que se libera
                // tras guardarlo. Save() infiere el formato del fichero por su extensión.
                using var bitmap = writer.Write(source);
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
