using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Micube.SmartMES.QualityAnalysis.KeyboardClipboard
{
    /// <summary>
    /// 공통으로 쓰이는 Util 모음
    /// </summary>
    public static class CommonUtils
    {
        /// <summary>
        /// Convert BitmapSource To Bitmap Format
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Bitmap ConvertBitmapSourceToBitmap(BitmapSource source)
        {
            try
            {
                Bitmap bitmap;
                using (var outStream = new MemoryStream())
                {
                    BitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(source));
                    encoder.Save(outStream);
                    bitmap = new Bitmap(outStream);
                }

                return bitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Recived 받은 Message delimiter로 Split
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>

        public static string[] SplitString(string text, string delimiter)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    return null;
                else
                {
                    int checkIndex = delimiter.IndexOfAny(checkCharacter); // checkCharacter 에 delimiter(┌, ┐)가 없으면 -1로 return(IndexOfAny)
                    if (checkIndex != -1)
                        delimiter = ChangeString(delimiter, delimiter.Substring(checkIndex, 1));

                    string[] regexSpit = Regex.Split(text, delimiter);
                    if (regexSpit.Length > 0)
                        return regexSpit;
                    else
                        return new string[] { text };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// text 를 changedString으로 변환
        /// </summary>
        /// <param name="text"></param>
        /// <param name="changedString"></param>
        /// <returns></returns>
        public static string ChangeString(string text, string changedString)
        {
            return text.Replace(changedString, @"\" + changedString);
        }

        private static char[] checkCharacter = new char[] { '(', ')', '^', '.' };
    }
}
