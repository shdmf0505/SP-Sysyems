#region using

using Micube.SmartMES.Commons.SPCLibrary;

using System.Linq;

#endregion

namespace System
{
    /// <summary>
    /// SPC 문자열 변환 사용자 정의 static class 정의
    /// </summary>
    public static class StringHelper
    {
        #region 문자형

        /// <summary>
        /// param을 string으로 변환한다. Null이면 공백을 리턴한다
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToSafeString(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString().Trim();
            }
        }

        /// <summary>
        /// param을 string으로 변환한다. Null이면 공백을 리턴한다
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToSafeDBString(this object obj)
        {
            if (Convert.IsDBNull(obj) != false)
            {
                return obj.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// double 값을 반올림 후 문자로 반환.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToSafeRoundString(this double obj, int d = 2)
        {
            double nValue;
            string sValue = "";
            if (Convert.IsDBNull(obj) != false)
            {
            }
            else
            {
                if (obj != SpcLimit.MAX && obj != SpcLimit.MIN)
                {
                    nValue = Math.Round(obj, d);
                    sValue = nValue.ToString();
                }
            }
            return sValue;
        }

        #endregion

        #region 숫자형

        /// <summary>
        /// Null이거나 공백인 경우 0을 리턴. 아니라면 int로 변환
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToSafeInt16(this object obj)
        {
            if (string.IsNullOrEmpty(obj.ToSafeString()))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt16(obj);
            }
        }

        /// <summary>
        /// Null이거나 공백인 경우 Null을 리턴. 아니라면 int로 변환
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? ToNullOrInt16(this object obj)
        {
            if (string.IsNullOrEmpty(obj.ToSafeString()))
            {
                return null;
            }
            else
            {
                return Convert.ToInt16(obj);
            }
        }

        /// <summary>
        /// Null이거나 공백인 경우 0을 리턴. 아니라면 Int32로 변환
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int32 ToSafeInt32(this object obj)
        {
            if (string.IsNullOrEmpty(obj.ToSafeString()))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// Null이거나 공백인 경우 Null을 리턴. 아니라면 double로 변환
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double? ToNullOrInt32(this object obj)
        {
            if (string.IsNullOrEmpty(obj.ToSafeString()))
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// Null이거나 공백인 경우 0을 리턴. 아니라면 long로 변환
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToSafeInt64(this object obj)
        {
            if (Convert.IsDBNull(obj) != false)
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(obj.ToSafeString()))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        /// <summary>
        /// Null이거나 공백인 경우 0을 리턴. 아니라면 long로 변환
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long? ToSafeInt64Db(this object obj)
        {
            if (Convert.IsDBNull(obj) != false)
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(obj.ToSafeString()))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        /// <summary>
        /// Null이거나 공백인 경우 Null을 리턴. 아니라면 long로 변환
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long? ToNullOrInt64(this object obj)
        {
            if (string.IsNullOrEmpty(obj.ToSafeString()))
            {
                return null;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// 통계용 Double 변환. Null이면 SpcLimit Max 리턴
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToSafeDoubleStaMax(this object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj.ToSafeString()))
                {
                    return SpcLimit.MAX;
                }
                else
                {
                    return Convert.ToDouble(obj);
                }
            }
            catch (Exception)
            {
                return SpcLimit.MAX;
            }
        }

        /// <summary>
        /// 통계용 Double 변환. Null이면 SpcLimit Min 리턴
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToSafeDoubleStaMin(this object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj.ToSafeString()))
                {
                    return SpcLimit.MIN;
                }
                else
                {
                    return Convert.ToDouble(obj);
                }
            }
            catch (Exception)
            {
                return SpcLimit.MIN;
            }
        }

        /// <summary>
        /// 오브젝트가 Null인 경우 0 리턴
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToSafeDoubleZero(this object obj)
        {
            try
            {
                if (Convert.IsDBNull(obj) != false)
                {
                    return 0;
                }
                else
                {
                    if (string.IsNullOrEmpty(obj.ToSafeString()))
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToDouble(obj);
                    }
                }


            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// 오브젝트가 Null인 경우 Nan 리턴
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToSafeDoubleNaN(this object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj.ToSafeString()))
                {
                    return double.NaN;
                }
                else
                {
                    return Convert.ToDouble(obj);
                }
            }
            catch (Exception)
            {
                return double.NaN;
            }

        }

        /// <summary>
        /// 오브젝트가 Null이나 공백이면 False / 아니면 True
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDouble(this object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj.ToSafeString()))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Null이거나 공백인 경우 Null을 리턴. 아니라면 double로 변환
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double? ToNullOrDouble(this object obj)
        {
            if (string.IsNullOrEmpty(obj.ToSafeString()))
            {
                return null;
            }

            if (obj.ToString().Length == 1)
            {
                switch (obj.ToString())
                {
                    case ".":
                    case ",":
                    case "/":
                    case "*":
                    case "-":
                    case "+":
                        return null;
                }
            }

            return Convert.ToDouble(obj);
        }

        /// <summary>
        /// Null이거나 공백인 경우 0을 리턴. 아니라면 decimal로 변환
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToSafeDecimal(this object obj)
        {
            if (string.IsNullOrEmpty(obj.ToSafeString()))
            {
                return 0;
            }

            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// Null이거나 공백인 경우 null을 리턴. 아니라면 decimal로 변환
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal? ToNullOrDecimal(this object obj)
        {
            if (string.IsNullOrEmpty(obj.ToSafeString()))
            {
                return null;
            }

            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// value가 숫자이면 True
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(object value) => double.TryParse(value.ToSafeString(), out double number);
        
        /// <summary>
        /// value가 숫자가 아니라면 0, 맞다면 value를 리턴한다
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double GetNumber(object value)
        {
            bool isNum = double.TryParse(value.ToSafeString(), out double number);

            if (!isNum)
            {
                number = 0;
            }

            return number;
        }

        /// <summary>
        /// 입력값은 문자열로 변환한다
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetNumberString(object value)
        {
            bool isNum = double.TryParse(value.ToSafeString(), out double number);

            if (!isNum)
            {
                return value.ToSafeString();
            }

            return number.ToSafeString();
        }

        /// <summary>
        /// 지수 설정
        /// </summary>
        /// <param name="value">지수 값</param>
        /// <param name="decimalpoint">소수점 수</param>
        /// <param name="formatMethod">포멧 Type(Default : '#')</param>
        /// <returns></returns>
        public static decimal GetExponentialFormat(object value, int decimalpoint, char formatMethod = '#')
        {
            string format = string.Concat(Enumerable.Repeat(formatMethod, decimalpoint));

            if (!string.IsNullOrEmpty(format))
            {
                format = "." + format;
            }

            string result = string.Format("{0:0" + format + "E+00}", value);

            return decimal.Parse(result, System.Globalization.NumberStyles.Float);
        }

        /// <summary>
        /// 문자열 포멧을 설정한다
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimalpoint"></param>
        /// <param name="formatMethod"></param>
        /// <returns></returns>
        public static string GetExponentialString(object value, int decimalpoint, char formatMethod = '#')
        {
            string format = string.Concat(Enumerable.Repeat(formatMethod, decimalpoint));

            if (!string.IsNullOrEmpty(format))
            {
                format = "." + format;
            }

            return string.Format("{0:0" + format + "E+00}", value);
        }

        /// <summary>
        /// value에 정수가 아닌 문자열은 삭제된다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetExtractionNumber(string value) => Text.RegularExpressions.Regex.Replace(value, @"[^0-9]", "");

        #endregion
    }
}
