#region using

using System;
using System.Data;
using System.Globalization;

#endregion

namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// 프 로 그 램 명  : Commons.SPCLibrary > Class > SpcFunction
    /// 업  무  설  명  : SPC Library 공통 함수
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-05-31
    /// 수  정  이  력  : 
    ///
    /// 2019-05-31  최초작성
    /// 
    /// </summary>
    public class SpcFunction
    {
        /// <summary>
        /// Test Mode 구분: 0-운영, 1=Test
        /// </summary>
        public static int isPublicTestMode = 0;
        #region [DB 필드 자료의 Null Check 함수]

        /// <summary>
        /// DB 필드 자료의 Null Check
        /// </summary>
        /// <param name="dataRow">Data Set Row</param>
        /// <param name="fieldName">Column 필드 명</param>
        /// <returns></returns>
        public static long IsDbNckInt64(DataRow dataRow, string fieldName)
        {
            long result = 0;
            bool nullCheck;

            try
            {
                nullCheck = Convert.IsDBNull(dataRow[fieldName]);

                if (!nullCheck)
                {
                    result = Convert.ToInt64(dataRow[fieldName]);
                }
            }
            catch (Exception)
            {
                return result;
            }
            finally
            {

            }

            return result;
        }

        /// <summary>
        /// DB 필드 자료의 Null Check 
        /// </summary>
        /// <param name="dataRow">DataRow 객체</param>
        /// <param name="fieldName">필드명</param>
        /// <returns>double</returns>
        public static double IsDbNckDoubleMax(DataRow dataRow, string fieldName)
        {
            double result = SpcLimit.MAX;
            bool nullCheck;

            try
            {
                nullCheck = Convert.IsDBNull(dataRow[fieldName]);

                if (!nullCheck)
                {
                    result = Convert.ToDouble(dataRow[fieldName]);
                }
            }
            catch (Exception)
            {
                return result;
            }
            finally
            {

            }

            return result;
        }

        /// <summary>
        /// DB 필드 자료의 Null Check 
        /// </summary>
        /// <param name="dataRow">DataRow 객체</param>
        /// <param name="fieldName">필드명</param>
        /// <returns>double - Min</returns>
        public static double IsDbNckDoubleMin(DataRow dataRow, string fieldName)
        {
            double result = SpcLimit.MIN;
            bool nullCheck;

            try
            {
                nullCheck = Convert.IsDBNull(dataRow[fieldName]);

                if (!nullCheck)
                {
                    result = Convert.ToDouble(dataRow[fieldName]);
                }
            }
            catch (Exception)
            {
                return result;
            }
            finally
            {

            }

            return result;
        }
        /// <summary>
        /// DB 필드 자료의 Null Check 후 자료가 있을 때만 입력. (값: double? value)
        /// </summary>
        /// <param name="dataRow">DataRow 객체</param>
        /// <param name="fieldName">필드명</param>
        /// <param name="value">double? 형 자료 입력</param>
        /// <returns>성공-0, 실패-1</returns>
        public static int IsDbNckDoubleWrite(DataRow dataRow, string fieldName, double? nValue, bool isNullCancel = false)
        {
            try
            {
                if (nValue != null)
                {
                    if (isNullCancel)
                    {
                        if (nValue != SpcLimit.MAX && nValue != SpcLimit.MIN)
                        {
                            dataRow[fieldName] = nValue;
                        }
                    }
                    else
                    {
                        dataRow[fieldName] = nValue;
                    }
                }
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {

            }

            return 0;
        }
        /// <summary>
        /// DB 필드 자료의 Null Check 후 자료가 있을 때만 입력. (값: double? value)
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="fieldName"></param>
        /// <param name="srcDataRow"></param>
        /// <param name="srcfieldName"></param>
        /// <returns></returns>
        public static int IsDbNckDoubleWrite(DataRow dataRow, string fieldName, DataRow srcDataRow, string srcfieldName = "")
        {
            double? nValue = null;
            try
            {
                if (srcfieldName != "")
                {
                    nValue = srcDataRow[srcfieldName].ToNullOrDouble();
                }
                else
                {
                    nValue = srcDataRow[fieldName].ToNullOrDouble();
                }

                if (nValue != null && nValue != SpcLimit.MAX && nValue != SpcLimit.MIN)
                {
                    dataRow[fieldName] = nValue;
                }

            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {

            }

            return 0;
        }
        /// <summary>
        /// DB 필드 자료의 Null Check 후 자료가 있을 때만 입력. (값: int32? value)
        /// </summary>
        /// <param name="dataRow">DataRow 객체</param>
        /// <param name="fieldName">필드명</param>
        /// <param name="value">int? 형 자료 입력</param>
        /// <returns>성공-0, 실패-1</returns>
        public static int IsDbNckInt32Write(DataRow dataRow, string fieldName, int? value)
        {
            try
            {
                if (value != null)
                {
                    dataRow[fieldName] = value.ToNullOrInt32();
                }
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {

            }

            return 0;
        }

        /// <summary>
        /// DB 필드 자료의 Null Check 후 자료가 있을 때만 입력. (값: int64? value)
        /// </summary>
        /// <param name="dataRow">DataRow 객체</param>
        /// <param name="fieldName">필드명</param>
        /// <param name="value">int? 형 자료 입력</param>
        /// <returns>성공-0, 실패-1</returns>
        public static int IsDbNckInt64Write(DataRow dataRow, string fieldName, int? value)
        {
            try
            {
                if (value != null)
                {
                    dataRow[fieldName] = value.ToNullOrInt64();
                }
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {

            }

            return 0;
        }

        public static int IsDbNckInt64Write(DataRow dataRow, string fieldName, DataRow srcDataRow, string srcfieldName ="")
        {
            try
            {
                if (srcfieldName != "")
                {
                    dataRow[fieldName] = srcDataRow[srcfieldName].ToNullOrInt64();
                }
                else
                {
                    dataRow[fieldName] = srcDataRow[fieldName].ToNullOrInt64();
                }
                
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {

            }

            return 0;
        }

        /// <summary>
        /// DB 필드 자료의 Null Check 후 자료가 있을 때만 입력. (값: double? value)
        /// </summary>
        /// <param name="dataRow">DataRow 객체</param>
        /// <param name="fieldName">필드명</param>
        /// <param name="value">string? 형 자료 입력</param>
        /// <returns>성공-0, 실패-1</returns>
        public static int IsDbNckStringWrite(DataRow dataRow, string fieldName, string value)
        {
            try
            {
                if (value != null)
                {
                    dataRow[fieldName] = value.ToSafeString();
                }
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {

            }

            return 0;
        }
        /// <summary>
        /// DB 필드 자료의 Null Check 후 자료가 있을 때만 입력. (값: double? value)
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="fieldName"></param>
        /// <param name="srcDataRow"></param>
        /// <param name="srcfieldName"></param>
        /// <returns></returns>
        public static int IsDbNckStringWrite(DataRow dataRow, string fieldName, DataRow srcDataRow)
        {
            try
            {
                dataRow[fieldName] = srcDataRow[fieldName].ToSafeString();
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {

            }

            return 0;
        }

        /// <summary>
        /// DB 필드 자료의 Null Check 후 자료가 있을 때만 입력. (값: double? value)
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="fieldName"></param>
        /// <param name="srcDataRow"></param>
        /// <param name="srcfieldName"></param>
        /// <param name="ReplaceOld"></param>
        /// <param name="ReplaceNew"></param>
        /// <returns></returns>
        public static int IsDbNckStringWrite(DataRow dataRow, string fieldName, DataRow srcDataRow, string srcfieldName = "", string ReplaceOld = "", string ReplaceNew = "")
        {
            try
            {
                if (srcfieldName != "")
                {
                    if (ReplaceOld != "")
                    {
                        dataRow[fieldName] = srcDataRow[srcfieldName].ToSafeString().Replace(ReplaceOld, ReplaceNew);
                    }
                    else
                    {
                        dataRow[fieldName] = srcDataRow[srcfieldName].ToSafeString();
                    }
                }
                else
                {
                    if (ReplaceOld != "")
                    {
                        dataRow[fieldName] = srcDataRow[fieldName].ToSafeString().Replace(ReplaceOld, ReplaceNew);
                    }
                    else
                    {
                        dataRow[fieldName] = srcDataRow[fieldName].ToSafeString();
                    }
                    
                }
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {

            }

            return 0;
        }
       
        public static int IsDbNckDoubleWriteDBNull(DataRow dataRow, string fieldName, double? value)
        {
            try
            {
                if (value != null)
                {
                    dataRow[fieldName] = value.ToNullOrDouble();
                }
                else
                {
                    dataRow[fieldName] = DBNull.Value;
                }
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {

            }

            return 0;
        }
        /// <summary>
        /// DB 필드 자료의 Null Check
        /// </summary>
        /// <param name="dataRow">Data Set Row</param>
        /// <param name="fieldName">Column 필드 명</param>
        /// <returns></returns>
        public static string IsDbNck(DataRow dataRow, string fieldName)
        {
            string result = string.Empty;
            bool nullCheck;

            try
            {
                nullCheck = Convert.IsDBNull(dataRow[fieldName]);

                if (!nullCheck)
                {
                    result = dataRow[fieldName].ToString();
                }
            }
            catch (Exception)
            {
                return result;
            }
            finally
            {

            }

            return result;
        }

        /// <summary>
        /// Object를 String으로 변환
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IsDbNck(object str)
        {
            string result = string.Empty;

            try
            {
                if (str != null)
                {
                    result = Convert.ToString(str);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }

            return result;
        }

        /// <summary>
        /// DB 필드 자료의 Null Check
        /// </summary>
        /// <param name="dataRow">Data Set Row</param>
        /// <param name="col">Column index</param>
        /// <returns></returns>
        public static string IsDbNck(DataRow dataRow, int col)
        {
            string strRec = string.Empty;

            try
            {
                strRec = Convert.IsDBNull(dataRow[col].ToString()) == true ? string.Empty : dataRow[col].ToString();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }

            return strRec;
        }

        /// <summary>
        /// DB Int64 자료 Null Check
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long IsDbNckInt64(object obj)
        {
            try
            {
                return Convert.IsDBNull(obj) != false ? 0 : Convert.ToInt64(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// DB 필드 자료의 NullCheck 및 고정 소수점 자리 표시
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="fieldName"></param>
        /// <param name="decimalPlace"></param>
        /// <returns></returns>
        public static string IsDbNck(DataRow dataRow, string fieldName, int decimalPlace)
        {
            string result = string.Empty;
            string checkData = string.Empty;
            decimal decmalData = 0;

            try
            {
                checkData = IsDbNck(dataRow, fieldName);
                if (checkData != string.Empty)
                {
                    decmalData = Convert.ToDecimal(checkData);
                    //decmalData = Math.Round(decmalData, decimalPlace);
                    //CultureInfo daDK = CultureInfo.CreateSpecificCulture("da-DK");

                    switch (decimalPlace)
                    {
                        case 1:
                            result = string.Format(CultureInfo.InvariantCulture, "{0:00.0}", decmalData);
                            break;
                        case 2:
                            result = string.Format(CultureInfo.InvariantCulture, "{0:00.00}", decmalData);
                            break;
                        case 3:
                            result = string.Format(CultureInfo.InvariantCulture, "{0:00.000}", decmalData);
                            break;
                        case 4:
                            result = string.Format(CultureInfo.InvariantCulture, "{0:00.0000}", decmalData);
                            break;
                        case 5:
                            result = string.Format(CultureInfo.InvariantCulture, "{0:00.0000}", decmalData);
                            break;
                        case 6:
                            result = string.Format(CultureInfo.InvariantCulture, "{0:00.0000}", decmalData);
                            break;
                        default:
                            result = string.Format(CultureInfo.InvariantCulture, "{0:00.000}", decmalData);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        #endregion

        /// <summary>
        /// Chart의 Data의 WholeRange를 반환. 
        /// </summary>
        /// <param name="spec"></param>
        public static ChartWholeValue chartWholeRange(SpcSpec spec, SPCOption spcOption, IsSpecCancel isCancel)
        {
            ChartWholeValue c = new ChartWholeValue();
            #region MAX
            //Value
            if (isCancel.Bar != true && spec.BarMax != SpcLimit.MAX && spec.BarMax != SpcLimit.MIN && spec.BarMax > c.editMax)
            {
                c.editMax = spec.BarMax;
            }
            if (isCancel.Bar != true && spec.BarMax != SpcLimit.MAX && spec.BarMin != SpcLimit.MIN && spec.BarMin > c.editMax)
            {
                c.editMax = spec.BarMin;
            }
            if (isCancel.R != true && spec.RMax != SpcLimit.MAX && spec.RMax != SpcLimit.MIN && spec.RMax > c.editMax)
            {
                c.editMax = spec.RMax;
            }
            if (isCancel.R != true && spec.RMin != SpcLimit.MAX && spec.RMax != SpcLimit.MIN && spec.RMin > c.editMax)
            {
                c.editMax = spec.RMin;
            }

            //XBar
            if (isCancel.xBar != true && spec.XDBar != SpcLimit.MAX && spec.XDBar != SpcLimit.MIN && spec.XDBar > c.editMax)
            {
                c.editMax = spec.XDBar;
            }
            if (isCancel.xR != true && spec.XDRar != SpcLimit.MAX && spec.XDRar != SpcLimit.MIN && spec.XDRar > c.editMax)
            {
                c.editMax = spec.XDRar;
            }

            //Spec
            if (isCancel.usl != true && spec.usl != SpcLimit.MAX && spec.usl != SpcLimit.MIN && spec.usl > c.editMax)
            {
                c.editMax = spec.usl;
            }

            if (isCancel.csl != true && spec.csl != SpcLimit.MAX && spec.csl != SpcLimit.MIN && spec.csl > c.editMax)
            {
                c.editMax = spec.csl;
            }

            if (isCancel.lsl != true && spec.lsl != SpcLimit.MAX && spec.lsl != SpcLimit.MIN && spec.lsl > c.editMax)
            {
                c.editMax = spec.lsl;
            }

            //Control
            if (isCancel.ucl != true && spec.ucl != SpcLimit.MAX && spec.ucl != SpcLimit.MIN && spec.ucl > c.editMax)
            {
                c.editMax = spec.ucl;
            }

            if (isCancel.ccl != true && spec.ccl != SpcLimit.MAX && spec.ccl != SpcLimit.MIN && spec.ccl > c.editMax)
            {
                c.editMax = spec.ccl;
            }

            if (isCancel.lcl != true && spec.lcl != SpcLimit.MAX && spec.lcl != SpcLimit.MIN && spec.lcl > c.editMax)
            {
                c.editMax = spec.lcl;
            }

            //Out-lier
            if (isCancel.uol != true && spec.uol != SpcLimit.MAX && spec.uol != SpcLimit.MIN && spec.uol > c.editMax)
            {
                c.editMax = spec.uol;
            }

            if (isCancel.uol != true && spec.lol != SpcLimit.MAX && spec.lol != SpcLimit.MIN && spec.lol > c.editMax)
            {
                c.editMax = spec.lol;
            }
            #endregion

            #region Min
            //Value
            if (isCancel.Bar != true && spec.BarMax != SpcLimit.MAX && spec.BarMax != SpcLimit.MIN && spec.BarMax < c.editMax)
            {
                c.editMin = spec.BarMax;
            }
            if (isCancel.Bar != true && spec.BarMin != SpcLimit.MAX && spec.BarMin != SpcLimit.MIN && spec.BarMin < c.editMax)
            {
                c.editMin = spec.BarMin;
            }
            if (isCancel.R != true && spec.RMax != SpcLimit.MAX && spec.RMax != SpcLimit.MIN && spec.RMax < c.editMax)
            {
                c.editMin = spec.RMax;
            }
            if (isCancel.R != true && spec.RMin != SpcLimit.MAX && spec.RMin != SpcLimit.MIN && spec.RMin < c.editMax)
            {
                c.editMin = spec.RMin;
            }

            //XBar
            if (isCancel.xBar != true && spec.XDBar != SpcLimit.MAX && spec.XDBar != SpcLimit.MIN && spec.XDBar < c.editMin)
            {
                c.editMin = spec.XDBar;
            }
            if (isCancel.xR != true && spec.XDRar != SpcLimit.MAX && spec.XDRar != SpcLimit.MIN && spec.XDRar < c.editMin)
            {
                c.editMin = spec.XDRar;
            }

            //Spec
            if (isCancel.usl != true && spec.usl != SpcLimit.MAX && spec.usl != SpcLimit.MIN && spec.usl < c.editMin)
            {
                c.editMin = spec.usl;
            }

            if (isCancel.csl != true && spec.csl != SpcLimit.MAX && spec.csl != SpcLimit.MIN && spec.csl < c.editMin)
            {
                c.editMin = spec.csl;
            }

            if (isCancel.lsl != true && spec.lsl != SpcLimit.MAX && spec.lsl != SpcLimit.MIN && spec.lsl < c.editMin)
            {
                c.editMin = spec.lsl;
            }

            //Control
            if (isCancel.ucl != true && spec.ucl != SpcLimit.MAX && spec.ucl != SpcLimit.MIN && spec.ucl < c.editMin)
            {
                c.editMin = spec.ucl;
            }

            if (isCancel.ccl != true && spec.ccl != SpcLimit.MAX && spec.ccl != SpcLimit.MIN && spec.ccl < c.editMin)
            {
                c.editMin = spec.ccl;
            }

            if (isCancel.lcl != true && spec.lcl != SpcLimit.MAX && spec.lcl != SpcLimit.MIN && spec.lcl < c.editMin)
            {
                c.editMin = spec.lcl;
            }

            //Out-lier
            if (spec.uol != SpcLimit.MAX && spec.uol != SpcLimit.MIN && spec.uol < c.editMin)
            {
                c.editMin = spec.uol;
            }

            if (spec.lol != SpcLimit.MAX && spec.lol != SpcLimit.MIN && spec.lol < c.editMin)
            {
                c.editMin = spec.lol;
            }
            #endregion
            double maginMax = 0;
            double maginMin = 0;
            maginMax = c.editMax * 0.2;
            maginMin = c.editMin * 0.2;
            //c.max = Convert.ToInt64(c.editMax);
            //c.min = Convert.ToInt64(c.editMin);
            c.max = c.editMax + maginMax;
            c.min = c.editMin - maginMin;

            return c;
        }
        /// <summary>
        /// Chart의 Data의 WholeRange를 반환. 
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="spcOption"></param>
        /// <param name="isCancel"></param>
        /// <returns></returns>
        public static ChartWholeValue chartWholeRangeCpk(SpcSpec spec, SPCOption spcOption, IsSpecCancel isCancel)
        {
            ChartWholeValue c = ChartWholeValue.Create();
            #region MAX
            //Bar
            if (isCancel.cpkBar != true && spcOption.CpkTotalize.Bar.ndXMax != SpcLimit.MAX && spcOption.CpkTotalize.Bar.ndXMax != SpcLimit.MIN && spcOption.CpkTotalize.Bar.ndXMax > c.editMax)
            {
                c.editMax = spcOption.CpkTotalize.Bar.ndXMax;
            }
            //Within
            if (isCancel.cpkWithin != true && spcOption.CpkTotalize.WithIn.ndXMax != SpcLimit.MAX && spcOption.CpkTotalize.WithIn.ndXMax != SpcLimit.MIN && spcOption.CpkTotalize.WithIn.ndXMax > c.editMax)
            {
                c.editMax = spcOption.CpkTotalize.WithIn.ndXMax;
            }
            //Total
            if (isCancel.cpkTotal != true && spcOption.CpkTotalize.Total.ndXMax != SpcLimit.MAX && spcOption.CpkTotalize.Total.ndXMax != SpcLimit.MIN && spcOption.CpkTotalize.Total.ndXMax > c.editMax)
            {
                c.editMax = spcOption.CpkTotalize.Total.ndXMax;
            }


            //Spec
            if (isCancel.usl != true && spec.usl != SpcLimit.MAX && spec.usl != SpcLimit.MIN && spec.usl > c.editMax)
            {
                c.editMax = spec.usl;
            }

            if (isCancel.csl != true && spec.csl != SpcLimit.MAX && spec.csl != SpcLimit.MIN && spec.csl > c.editMax)
            {
                c.editMax = spec.csl;
            }

            if (isCancel.lsl != true && spec.lsl != SpcLimit.MAX && spec.lsl != SpcLimit.MIN && spec.lsl > c.editMax)
            {
                c.editMax = spec.lsl;
            }

            //Target USL (중심치 이탈 상한)
            if (isCancel.taUsl != true && spec.taUsl != SpcLimit.MAX && spec.taUsl != SpcLimit.MIN && spec.taUsl > c.editMax)
            {
                c.editMax = spec.taUsl;
            }
            if (isCancel.taCsl != true && spec.taCsl != SpcLimit.MAX && spec.taCsl != SpcLimit.MIN && spec.taCsl > c.editMax)
            {
                c.editMax = spec.taCsl;
            }
            //if (isCancel.taLsl != true && spec.taLsl != SpcLimit.MAX && spec.taLsl != SpcLimit.MIN && spec.taLsl > c.editMax)
            //{
            //    c.editMax = spec.taLsl;
            //}


            #endregion

            #region Min
            //Value
            //Bar
            if (isCancel.cpkBar != true && spcOption.CpkTotalize.Bar.ndXMin != SpcLimit.MAX && spcOption.CpkTotalize.Bar.ndXMin != SpcLimit.MIN && spcOption.CpkTotalize.Bar.ndXMin < c.editMin)
            {
                c.editMin = spcOption.CpkTotalize.Bar.ndXMin;
            }
            //Within
            if (isCancel.cpkWithin != true && spcOption.CpkTotalize.WithIn.ndXMin != SpcLimit.MAX && spcOption.CpkTotalize.WithIn.ndXMin != SpcLimit.MIN && spcOption.CpkTotalize.WithIn.ndXMin < c.editMin)
            {
                c.editMin = spcOption.CpkTotalize.WithIn.ndXMin;
            }
            //Total
            if (isCancel.cpkTotal != true && spcOption.CpkTotalize.Total.ndXMin != SpcLimit.MAX && spcOption.CpkTotalize.Total.ndXMin != SpcLimit.MIN && spcOption.CpkTotalize.Total.ndXMin < c.editMin)
            {
                c.editMin = spcOption.CpkTotalize.Total.ndXMin;
            }


            //Spec
            if (isCancel.usl != true && spec.usl != SpcLimit.MAX && spec.usl != SpcLimit.MIN && spec.usl < c.editMin)
            {
                c.editMin = spec.usl;
            }

            if (isCancel.csl != true && spec.csl != SpcLimit.MAX && spec.csl != SpcLimit.MIN && spec.csl < c.editMin)
            {
                c.editMin = spec.csl;
            }

            if (isCancel.lsl != true && spec.lsl != SpcLimit.MAX && spec.lsl != SpcLimit.MIN && spec.lsl < c.editMin)
            {
                c.editMin = spec.lsl;
            }

            //Target USL (중심치 이탈 상한)
            if (isCancel.taUsl != true && spec.taUsl != SpcLimit.MAX && spec.taUsl != SpcLimit.MIN && spec.taUsl < c.editMin)
            {
                c.editMin = spec.taUsl;
            }
            if (isCancel.taCsl != true && spec.taCsl != SpcLimit.MAX && spec.taCsl != SpcLimit.MIN && spec.taCsl < c.editMin)
            {
                c.editMin = spec.taCsl;
            }

            //if (isCancel.taLsl != true && spec.taLsl != SpcLimit.MAX && spec.taLsl != SpcLimit.MIN && spec.taLsl < c.editMin)
            //{
            //    c.editMin = spec.taLsl;
            //}


            #endregion
            double maginMax = 0;
            double maginMin = 0;
            maginMax = c.editMax * 0.000002;
            maginMin = c.editMin * 0.000002;

            //c.max = Convert.ToInt64(c.editMax);
            //c.min = Convert.ToInt64(c.editMin);
            c.max = c.editMax + maginMax;
            if (c.editMin > 0)
            {
                c.min = c.editMin - maginMin;
            }
            else
            {
                c.min = c.editMin + maginMin;
            }

            return c;
        }

        /// <summary>
        /// Chart Serials Caption 설정.
        /// </summary>
        /// <param name="serialCaption"></param>
        public ControlChartSerialCaption ControlChartToolsNameSetting(ControlChartSerialCaption serialCaption)
        {
            if (serialCaption == null)
            {
                serialCaption = ControlChartSerialCaption.Create();
            }

            serialCaption.AUCL.toolTipName = "해석용 UCL:";
            serialCaption.AUCL.toolTipValueType = "{V:F3}";
            serialCaption.AUCL.toolTipValueFormat = string.Format("{0} {1}", serialCaption.NVALUE.toolTipName, serialCaption.NVALUE.toolTipValueType);

            serialCaption.AUCL.toolTipName = "해석용 LCL:";
            serialCaption.AUCL.toolTipValueType = "{V:F3}";
            serialCaption.AUCL.toolTipValueFormat = string.Format("{0} {1}", serialCaption.NVALUE.toolTipName, serialCaption.NVALUE.toolTipValueType);

            serialCaption.NVALUE.toolTipName = "Value:";
            serialCaption.NVALUE.toolTipValueType = "{V:F3}";
            serialCaption.NVALUE.toolTipValueFormat = string.Format("{0} {1}", serialCaption.NVALUE.toolTipName, serialCaption.NVALUE.toolTipValueType);

            serialCaption.AUCL.toolTipName = "해석용 UCL:";
            serialCaption.AUCL.toolTipValueType = "{V:F3}";
            serialCaption.AUCL.toolTipValueFormat = string.Format("{0} {1}", serialCaption.NVALUE.toolTipName, serialCaption.NVALUE.toolTipValueType);

            serialCaption.RAUCL.toolTipName = "해석용 LCL:";
            serialCaption.RAUCL.toolTipValueType = "{V:F3}";
            serialCaption.RAUCL.toolTipValueFormat = string.Format("{0} {1}", serialCaption.NVALUE.toolTipName, serialCaption.NVALUE.toolTipValueType);

            serialCaption.RVALUE.toolTipName = "Value:";
            serialCaption.RVALUE.toolTipValueType = "{V:F3}";
            serialCaption.RVALUE.toolTipValueFormat = string.Format("{0} {1}", serialCaption.NVALUE.toolTipName, serialCaption.NVALUE.toolTipValueType);

            return serialCaption;
        }

        /// <summary>
        /// 일자만 반환. 10/10
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static string DateToDay(string dateValue)
        {
            string resultDdata = "01";
            try
            {
                DateTime dtTemp = Convert.ToDateTime(dateValue);
                resultDdata = dtTemp.ToString("dd");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return resultDdata;
        }
        /// <summary>
        /// 한달 일자 반환.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static string[] DayMonthList(string dateValue)
        {
            string[] resultDdata = new string[32];
            DateTime dtValue;
            DateTime dtTemp;
            string dateData = "";
            string firstMonth = "";
            string firstMonthCheck = "";
            try
            {
                //dateValue = "2019-02-03";
                dtTemp = Convert.ToDateTime(dateValue);
                dateData = dtTemp.ToString("yyyy-MM");
                dateData = string.Format("{0}-01", dateData);
                firstMonth = dtTemp.ToString("MM");
                dtValue = Convert.ToDateTime(dateData);
                for (int i = 1; i < 32; i++)
                {
                    firstMonthCheck = dtValue.ToString("MM");
                    if (firstMonth != firstMonthCheck)
                    {
                        resultDdata[i] = "";
                    }
                    else
                    {
                        resultDdata[i] = dtValue.ToString("MM-dd");
                    }

                    dtValue = dtValue.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return resultDdata;
        }
    } //end class SpcFunction 

    #region Spc Library Message
    public static class SpcLibMessage
    {
        public static bool isUse;
        public static MessageSpcCommon common;
    }

    /// <summary>
    /// SPC 공통 Message
    /// </summary>
    public class MessageSpcCommon
    {
        #region Common
        /// <summary>
        /// Temp
        /// </summary>
        public string comXXXX;
        /// <summary>
        /// Temp
        /// </summary>
        public string comCpkXXXX;
        /// <summary>
        /// 분석 자료가 없습니다.
        /// </summary>
        public string com1001;
        /// <summary>
        /// 분석 입력자료 읽기.
        /// </summary>
        public string com1002;
        /// <summary>
        /// 공정능력 SPEC 확인.
        /// </summary>
        public string com1003;

        public string com1004;

        public string com1005;
        /// <summary>
        /// 추정치
        /// </summary>
        public string com1006;
        /// <summary>
        /// Save an Chart image
        /// </summary>
        public string com1007;

        //공통-Tab
        /// <summary>
        /// 관리도
        /// </summary>
        public string com1011;
        /// <summary>
        /// 공정능력
        /// </summary>
        public string com1012;
        /// <summary>
        /// 분석
        /// </summary>
        public string com1013;
        /// <summary>
        /// Raw Data
        /// </summary>
        public string com1014;
        /// <summary>
        /// Over Rules
        /// </summary>
        public string com1015;

        //*공통-Popup Menu
        /// <summary>
        /// Image 복사
        /// </summary>
        public string com2101;
        /// <summary>
        /// Image 저장
        /// </summary>
        public string com2102;

        #endregion Common

        #region Rules
        /// <summary>
        /// 입력 자료가 없습니다.
        /// </summary>
        public string rule1001;
        /// <summary>
        /// UCL 3 Sigma가 없습니다.
        /// </summary>
        public string rule1002;
        /// <summary>
        /// LCL 3 Sigma가 없습니다.
        /// </summary>
        public string rule1003;
        /// <summary>
        /// Sigma가 없습니다.
        /// </summary>
        public string rule1004;
        /// <summary>
        /// UCL 2 Sigma가 없습니다.
        /// </summary>
        public string rule1005;
        /// <summary>
        /// LCL 2 Sigma가 없습니다.
        /// </summary>
        public string rule1006;
        /// <summary>
        /// UCL 1 Sigma가 없습니다.
        /// </summary>
        public string rule1007;
        /// <summary>
        /// LCL 1 Sigma가 없습니다.
        /// </summary>
        public string rule1008;
        /// <summary>
        /// 평균 값이 없습니다.
        /// </summary>  
        public string rule1009;
        #endregion Rules


        #region 공정능력 및 관리도
        /// <summary>
        /// 공정능력 분석 함수 실행. [SpcLibPpkCbMuti]
        /// </summary>
        public string comCpk1001;

        /// <summary>
        /// 공정능력 분석용 USL, LSL 값이 없습니다.
        /// </summary>
        public string comCpk1011;
        /// <summary>
        /// 한개 이상 SPEC이 있어야 합니다.
        /// </summary>
        public string comCpk1012;
        /// <summary>
        /// LSL SPEC이 없습니다.
        /// </summary>
        public string comCpk1013;
        /// <summary>
        /// USL SPEC이 없습니다.
        /// </summary>
        public string comCpk1014;
        /// <summary>
        /// LSL, USL SPEC이 모두 없습니다.
        /// </summary>
        public string comCpk1015;
        /// <summary>
        /// 그러므로 공정능력을 계산 할 수 없습니다.
        /// </summary>
        public string comCpk1016;
        /// <summary>
        /// 입력 자료가 없습니다.
        /// </summary>
        public string comCpk1017;
        /// <summary>
        /// 변동부족: Sampling Count가 1개 입니다.
        /// </summary>
        public string comCpk1018;
        /// <summary>
        /// 값의 변동이 없을시: SubGroup의 값이 전부 같으면 계산하지 않음(변동이 없음)
        /// </summary>
        public string comCpk1019;

        //*
        /// <summary>
        /// 중심치 이탈
        /// </summary>
        public string comCpk1020;
        /// <summary>
        /// 중심치 이탈(%)
        /// </summary>
        public string comCpk1021;
        /// <summary>
        /// 공정능력 SPEC Control이 정의되지 않았습니다. [controlSpec]
        /// </summary>
        public string comCpk1022;
        /// <summary>
        /// 공정능력 Sigma 결과 값이 정의되지 않았습니다. [controlSpec.sigmaResult]
        /// </summary>
        public string comCpk1023;
        /// <summary>
        /// 공정능력 내부 Sigma 값이 정의되지 않았습니다. [controlSpec.sigmaResult.nSigmaWithin]
        /// </summary>
        public string comCpk1024;
        /// <summary>
        /// 공정능력 전체 Sigma 값이 정의되지 않았습니다. [controlSpec.sigmaResult.nSigmaTotal]
        /// </summary>
        public string comCpk1025;
        /// <summary>
        /// Histogram 간격 폭 계산 할 수 없습니다.
        /// </summary>
        public string comCpk1026;
        /// <summary>
        /// 조회한 자료가 없습니다. 우측의 조회버튼을 Click 하시길 바랍니다.
        /// </summary>
        public string comCpk1027;
        /// <summary>
        /// 측정값이 1개 임으로 표준편차를 계산 할 수 없습니다.
        /// </summary>
        public string comCpk1028;
        /// <summary>
        /// 상태 Message:
        /// </summary>
        public string comCpk1029;
        /// <summary>
        /// 분석 Message:
        /// </summary>
        public string comCpk1030;
        /// <summary>
        /// 해석용
        /// </summary>
        public string comCpk1031;
        /// <summary>
        /// Over Rule
        /// </summary>
        public string comCpk1032;
        /// <summary>
        /// 값
        /// </summary>
        public string comCpk1033;
        /// <summary>
        /// asixs 라벨:
        /// </summary>
        public string comCpk1034;
        /// <summary>
        /// 추정치 사용
        /// </summary>
        public string comCpk1035;
        /// <summary>
        /// 추정치 미사용
        /// </summary>
        public string comCpk1036;
        /// <summary>
        /// 추정치 미선택
        /// </summary>
        public string comCpk1037;
        /// <summary>
        /// 같지 않음
        /// </summary>
        public string comCpk1038;
        /// <summary>
        /// Subgroup별 Sampling 수가 같지 않음.
        /// </summary>
        public string comCpk1039;
        /// <summary>
        /// 관리용
        /// </summary>
        public string comCpk1040;
        /// <summary>
        /// 직접입력
        /// </summary>
        public string comCpk1041;
        /// <summary>
        /// SBar 합동
        /// </summary>
        public string comCpk1042;
        /// <summary>
        /// 재분석
        /// </summary>
        public string comCpk1043;
        /// <summary>
        /// 같음
        /// </summary>
        public string comCpk1044;
        /// <summary>
        /// Nelson rules
        /// </summary>
        public string comCpk1045;
        /// <summary>
        /// Western Electric rules
        /// </summary>
        public string comCpk1046;
        /// <summary>
        /// Rules Setting - Nelson rules
        /// </summary>
        public string comCpk1047;
        /// <summary>
        /// Rules Setting - Western Electric rules
        /// </summary>
        public string comCpk1048;





        #endregion 공정능력 및 관리도
    }
    #endregion Spc Library Message

}//end namespace
