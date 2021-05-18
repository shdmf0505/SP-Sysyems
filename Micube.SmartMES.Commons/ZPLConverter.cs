#region using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

#endregion

namespace Micube.SmartMES.Commons
{
    public class ZPLImageConverter
    {
        private int blackLimit = 380;
        private int total;
        private int widthBytes;
        private bool compressHex = false;

        private static readonly Dictionary<int, String> MapCode = new Dictionary<int, String>()
        {
            {1, "G"},
            {2, "H"},
            {3, "I"},
            {4, "J"},
            {5, "K"},
            {6, "L"},
            {7, "M"},
            {8, "N"},
            {9, "O" },
            {10, "P"},
            {11, "Q"},
            {12, "R"},
            {13, "S"},
            {14, "T"},
            {15, "U"},
            {16, "V"},
            {17, "W"},
            {18, "X"},
            {19, "Y"},
            {20, "g"},
            {40, "h"},
            {60, "i"},
            {80, "j" },
            {100, "k"},
            {120, "l"},
            {140, "m"},
            {160, "n"},
            {180, "o"},
            {200, "p"},
            {220, "q"},
            {240, "r"},
            {260, "s"},
            {280, "t"},
            {300, "u"},
            {320, "v"},
            {340, "w"},
            {360, "x"},
            {380, "y"},
            {400, "z" }
        };

        /// <summary>
        /// Converts a Bitmap into a ZPL ^GF[A] command input.  Can also specify ZPL header and footer to allow easy printing of label.
        /// </summary>
        /// <param name="image">Bitmap containing image source for ^GF[A] command input string.</param>
        /// <param name="addHeaderFooter">if true surrounds the command input string with the ZPL headers and footers required to generate valid ZPL.</param>
        /// <returns>^GF[A] command input string</returns>
        public String ConvertFromImage(Bitmap image, Boolean addHeaderFooter)
        {
            String hexAscii = CreateBody(image);
            if (compressHex)
            {
                hexAscii = EncodeHexAscii(hexAscii);
            }

            String zplCode = "^GFA," + total + "," + total + "," + widthBytes + ", " + hexAscii;

            if (addHeaderFooter)
            {
                String header = "^XA " + "^FO0,0^GFA," + total + "," + total + "," + widthBytes + ", ";
                String footer = "^FS" + "^XZ";
                zplCode = header + zplCode + footer;
            }
            return zplCode;
        }

        /// <summary>
        /// Driver for generating command input string.
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        private String CreateBody(Bitmap bitmapImage)
        {
            StringBuilder sb = new StringBuilder();
            int height = bitmapImage.Height ;
            int width = bitmapImage.Width ;
            int rgb, red, green, blue, index = 0;
            var auxBinaryChar = new char[] { '0', '0', '0', '0', '0', '0', '0', '0' };
            widthBytes = width / 8;
            if (width % 8 > 0)
            {
                widthBytes = (((int)(width / 8)) + 1);
            }
            else
            {
                widthBytes = width / 8;
            }
            this.total = widthBytes * height;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    rgb = bitmapImage.GetPixel(w, h).ToArgb();
                    red = (rgb >> 16) & 0x000000FF;
                    green = (rgb >> 8) & 0x000000FF;
                    blue = (rgb) & 0x000000FF;
                    char auxChar = '1';
                    int totalColor = red + green + blue;
                    if (totalColor > blackLimit)
                    {
                        auxChar = '0';
                    }
                    auxBinaryChar[index] = auxChar;
                    index++;
                    if (index == 8 || w == (width - 1))
                    {
                        sb.Append(FourByteBinary(new String(auxBinaryChar)));
                        auxBinaryChar = new char[] { '0', '0', '0', '0', '0', '0', '0', '0' };
                        index = 0;
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts binary into integer representation of two hex digits 
        /// </summary>
        /// <param name="binaryStr"></param>
        /// <returns></returns>
        private String FourByteBinary(String binaryStr)
        {
            int value = Convert.ToInt32(binaryStr, 2);
            if (value > 15)
            {
                return Convert.ToString(value, 16).ToUpper();
            }
            else
            {
                return "0" + Convert.ToString(value, 16).ToUpper();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private String EncodeHexAscii(String code)
        {
            int maxlinea = widthBytes * 2;
            StringBuilder sbCode = new StringBuilder();
            StringBuilder sbLinea = new StringBuilder();
            String previousLine = null;
            int counter = 1;
            char aux = code.ElementAt(0);
            bool firstChar = false;
            for (int i = 1; i < code.Length; i++)
            {
                if (firstChar)
                {
                    aux = code.ElementAt(i);
                    firstChar = false;
                    continue;
                }
                if (code.ElementAt(i) == '\n')
                {
                    if (counter >= maxlinea && aux == '0')
                    {
                        sbLinea.Append(",");
                    }
                    else if (counter >= maxlinea && aux == 'F')
                    {
                        sbLinea.Append("!");
                    }
                    else if (counter > 20)
                    {
                        int multi20 = (counter / 20) * 20;
                        int resto20 = (counter % 20);
                        sbLinea.Append(MapCode[multi20]);
                        if (resto20 != 0)
                        {
                            sbLinea.Append(MapCode[resto20]).Append(aux);
                        }
                        else
                        {
                            sbLinea.Append(aux);
                        }
                    }
                    else
                    {
                        sbLinea.Append(MapCode[counter]).Append(aux);
                    }
                    counter = 1;
                    firstChar = true;
                    if (sbLinea.ToString().Equals(previousLine))
                    {
                        sbCode.Append(":");
                    }
                    else
                    {
                        sbCode.Append(sbLinea.ToString());
                    }
                    previousLine = sbLinea.ToString();
                    sbLinea.Length = 0;
                    continue;
                }
                if (aux == code.ElementAt(i))
                {
                    counter++;
                }
                else
                {
                    if (counter > 20)
                    {
                        int multi20 = (counter / 20) * 20;
                        int resto20 = (counter % 20);
                        sbLinea.Append(MapCode[multi20]);
                        if (resto20 != 0)
                        {
                            sbLinea.Append(MapCode[resto20]).Append(aux);
                        }
                        else
                        {
                            sbLinea.Append(aux);
                        }
                    }
                    else
                    {
                        sbLinea.Append(MapCode[counter]).Append(aux);
                    }
                    counter = 1;
                    aux = code.ElementAt(i);
                }
            }
            return sbCode.ToString();
        }

        public void SetCompressHex(bool compressHex)
        {
            this.compressHex = compressHex;
        }

        /// <summary>
        /// Sets black pixel threshold for comparison of zpl pixels which determining whether to render or ignore pixels.  
        /// </summary>
        /// <param name="percentage">threshold percentage for comparison of pixels</param>
        /// <remarks>100+ percentage values will generate entirely black label.</remarks>
        public void SetBlacknessLimitPercentage(int percentage)
        {
            blackLimit = (percentage * 768 / 100);
        }
    }

    public class ZPLUtil
    {
        public static String getZplCode(Bitmap bitmap, Boolean addHeaderFooter)
        {
            ZPLImageConverter zp = new ZPLImageConverter();
            zp.SetCompressHex(true);
            zp.SetBlacknessLimitPercentage(50);
            Bitmap grayBitmap = toGrayScale(bitmap);
            return zp.ConvertFromImage(grayBitmap, addHeaderFooter);
        }

        public static Bitmap toGrayScale(Bitmap bmpOriginal)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(bmpOriginal.Width, bmpOriginal.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
                     new float[] {.3f, .3f, .3f, 0, 0},
                     new float[] {.59f, .59f, .59f, 0, 0},
                     new float[] {.11f, .11f, .11f, 0, 0},
                     new float[] {0, 0, 0, 1, 0},
                     new float[] {0, 0, 0, 0, 1}
               });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(bmpOriginal, new Rectangle(0, 0, bmpOriginal.Width * 2, bmpOriginal.Height * 2),
               0, 0, bmpOriginal.Width * 2, bmpOriginal.Height * 2, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        /// <summary>
        /// 텍스트를 이미지 다운로드 커맨드로 전환한다. 이미지 다운로드 후 ^XG 명령어를 이용해 출력 가능.
        /// ~DGR:SAMPLE.GRF,00080,010,
        /// FFFFFFFFFFFFFFFFFFFF
        /// 8000FFFF0000FFFF0001
        /// 8000FFFF0000FFFF0001
        /// 8000FFFF0000FFFF0001
        /// FFFF0000FFFF0000FFFF
        /// FFFF0000FFFF0000FFFF
        /// FFFF0000FFFF0000FFFF
        /// FFFFFFFFFFFFFFFFFFFF
        /// ^XA
        /// ^FO20,20^XGR:SAMPLE.GRF,1,1^FS
        /// ^XZ
        /// 
        /// ~DGR ... 부터 ^XA 이전까지가 이 함수가 반환하는 값
        /// </summary>
        /// <param name="text">이미지코드로 변환할 텍스트</param>
        /// <param name="imageFileName">바코드 프린터에 다운로드할 이미지 파일명(확장자 제외)</param>
        /// <param name="font">폰트</param>
        /// <returns></returns>
        public static string CreateZPLImageCodeFromText(string text, string imageFileName, Font font)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            using (Bitmap image = CreateImageFromText(text, font))
            {
                string imageCode = ConvertImageToZPLCode(image);
                var totalNumberOfBytes = ((image.Size.Width / 8 + ((image.Size.Width % 8 == 0) ? 0 : 1)) * image.Size.Height).ToString();
                var numberOfBytesPerRow = (image.Size.Width / 8 + ((image.Size.Width % 8 == 0) ? 0 : 1)).ToString();
                return string.Format("~DGR:{3}.GRF,{0},{1},{2}", totalNumberOfBytes, numberOfBytesPerRow, imageCode, imageFileName);
            }
        }

        /// <summary>
        /// 텍스트를 이미지 다운로드 커맨드로 전환한다. 이미지 다운로드 후 ^XG 명령어를 이용해 출력 가능.
        /// ~DGR:SAMPLE.GRF,00080,010,
        /// FFFFFFFFFFFFFFFFFFFF
        /// 8000FFFF0000FFFF0001
        /// 8000FFFF0000FFFF0001
        /// 8000FFFF0000FFFF0001
        /// FFFF0000FFFF0000FFFF
        /// FFFF0000FFFF0000FFFF
        /// FFFF0000FFFF0000FFFF
        /// FFFFFFFFFFFFFFFFFFFF
        /// ^XA
        /// ^FO20,20^XGR:SAMPLE.GRF,1,1^FS
        /// ^XZ
        /// 
        /// ~DGR ... 부터 ^XA 이전까지가 이 함수가 반환하는 값
        /// </summary>
        /// <param name="image">이미지코드로 변환할 이미지</param>
        /// <param name="imageFileName">바코드 프린터에 다운로드할 이미지 파일명(확장자 제외)</param>
        /// <returns></returns>
        public static string CreateZPLImageCodeFromImage(Bitmap image, string imageFileName)
        {
            if (image == null)
            {
                return null;
            }

            string imageCode = ConvertImageToZPLCode(image);
            var totalNumberOfBytes = ((image.Size.Width / 8 + ((image.Size.Width % 8 == 0) ? 0 : 1)) * image.Size.Height).ToString();
            var numberOfBytesPerRow = (image.Size.Width / 8 + ((image.Size.Width % 8 == 0) ? 0 : 1)).ToString();
            return string.Format("~DGR:{3}.GRF,{0},{1},{2}", totalNumberOfBytes, numberOfBytesPerRow, imageCode, imageFileName);
        }

        /// <summary>
        /// 텍스트로 부터 이미지 생성
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        private static Bitmap CreateImageFromText(string text, Font font)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            using (Image fakeImage = new Bitmap(1, 1))  // graphics 객체 생성용 이미지
            using (Graphics g1 = Graphics.FromImage(fakeImage))
            {
                Size textSize = Size.Round(g1.MeasureString(text, font));
                var textImage = new Bitmap(textSize.Width, textSize.Height);

                using (var g2 = Graphics.FromImage(textImage))
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, textImage.Width, textImage.Height), Color.Black, Color.Black, 1.2f, true))
                {
                    g2.Clear(System.Drawing.Color.White);
                    g2.DrawString(text, font, brush, 1, 1);
                    return textImage;
                }
            }
        }

        /// <summary>
        /// 이미지를 ZPL 코드로 변환
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static string ConvertImageToZPLCode(Bitmap image)
        {
            const int BITS_IN_BYTE = 8;
            var result = new StringBuilder();

            for (int y = 0; y < image.Size.Height; y++)
            {
                for (int x = 0; x < image.Size.Width; x += BITS_IN_BYTE)
                {
                    int b = 0; // color of 8 pixels
                    for (int z = 0; z < BITS_IN_BYTE; z++)
                    {
                        b = b << 1;
                        float color;
                        if (x + z >= image.Size.Width)
                        {
                            color = 1; // white
                        }
                        else
                        {
                            color = image.GetPixel(x + z, y).GetBrightness();
                        }
                        if (color < 0.5) // black
                        {
                            b += 1;
                        }
                    }
                    result.Append(b.ToString("X2"));
                }
                result.Append("\n");
            }
            return result.ToString();
        }
    }
}
