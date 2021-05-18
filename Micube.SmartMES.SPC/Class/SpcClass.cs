#region using

using DevExpress.XtraCharts;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.SPCLibrary;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;

#endregion

namespace Micube.SmartMES.SPC
{
    /// <summary>
    /// 프 로 그 램 명  : Micube.SmartMES.SPC > Class > SpcClass
    /// 업  무  설  명  : SPC 통계용 Form 공통 사용 함수 정의
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-08-01
    /// 수  정  이  력  : 
    /// 
    /// 2019-08-01  최초작성
    /// 
    /// </summary>
    public class SpcClass
    {

        public static string SubGroupSymbolDb = "@#";
        public static string SubGroupSymbolView = "/";
        /// <summary>SPC ComboBox DataTable
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateTableSpcComboBox(string tableName)
        {
            DataTable dataTable = new DataTable
            {
                //XBAR
                TableName = tableName
            };

            dataTable.Columns.Add("Display", typeof(string));
            dataTable.Columns.Add("Value", typeof(string));

            return dataTable;
        }

        /// <summary>
        /// Left Chart Type Grid의 Display, Value Field에 값을 설정한다.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="chartType"></param>
        public static void SetcboLeftChartType(DataTable dt, ControlChartType chartType)
        {
            DataRow dr = dt.NewRow();
            string rtnValue = "";
            dr["Display"] = ConversionChartTypeByString(chartType, out rtnValue);
            dr["Value"] = rtnValue;
            dt.Rows.Add(dr);
        }

        /// <summary>
        /// Control Chart Type Enum을 String으로 변환한다.
        /// </summary>
        /// <param name="chartType"></param>
        /// <returns></returns>
        public static string ConversionChartTypeByString(ControlChartType chartType, out string rtnValue)
        {
            string value = string.Empty;
            rtnValue = "";
            switch (chartType)
            {
                case ControlChartType.XBar_R:
                    value = "XBAR-R";
                    rtnValue = "XBARR";
                    break;
                case ControlChartType.XBar_S:
                    value = "XBAR-S";
                    rtnValue = "XBARS";
                    break;
                case ControlChartType.Merger:
                    value = "XBAR-P";
                    rtnValue = "XBARP";
                    break;
                case ControlChartType.I_MR:
                    value = "I-MR";
                    rtnValue = "I";
                    break;
                case ControlChartType.np:
                    value = "NP";
                    rtnValue = "NP";
                    break;
                case ControlChartType.p:
                    value = "P";
                    rtnValue = "P";
                    break;
                case ControlChartType.c:
                    value = "C";
                    rtnValue = "C";
                    break;
                case ControlChartType.u:
                    value = "U";
                    rtnValue = "U";
                    break;
            }

            return value;
        }

        /// <summary>
        /// Spc 사용 사전 정보 조회
        /// 다국어 직접 조회
        /// </summary>
        public static void SpcDictionaryDataSetting()
        {
            ConditionCollection Conditions = new ConditionCollection();
            //var row = grdDictionaryClass.View.GetDataRow(grdDictionaryClass.View.FocusedRowHandle);
            //grdDictionary.DataSource = SqlExecuter.Procedure("usp_com_selectDictionary", cond);
            try
            {
                //그리드 캡션
                if (SpcDictionary.dtGridCaption == null || SpcDictionary.dtGridCaption.Rows.Count <= 0)
                {
                    SpcDictionary.Language = UserInfo.Current.LanguageType;
                    var cond = Conditions.GetValues();
                    cond.Add("P_VALIDSTATE", "Valid");
                    cond.Add("P_DICTIONARYCLASSID", "GRID");
                    cond.Add("P_CONDITIONITEM", "*");
                    cond.Add("P_CONDITIONVALUE", "SPC");
                    SpcDictionary.dtGridCaption = SqlExecuter.Query("SelectDictionary", "10001", cond);
                    //Console.Write(SpcDictionary.dtGridCaption.Rows.Count);
                }

                //컨트롤 라벨
                if (SpcDictionary.dtControlCaption == null || SpcDictionary.dtControlCaption.Rows.Count <= 0)
                {
                    SpcDictionary.Language = UserInfo.Current.LanguageType;
                    var cond = Conditions.GetValues();
                    cond.Add("P_VALIDSTATE", "Valid");
                    cond.Add("P_DICTIONARYCLASSID", "CONTROLLABEL");
                    cond.Add("P_CONDITIONITEM", "*");
                    cond.Add("P_CONDITIONVALUE", "SPC");
                    SpcDictionary.dtControlCaption = SqlExecuter.Query("SelectDictionary", "10001", cond);
                    //Console.Write(SpcDictionary.dtGridCaption.Rows.Count);
                }

                //Group 캡션
                if (SpcDictionary.dtGroupCaption == null || SpcDictionary.dtGroupCaption.Rows.Count <= 0)
                {
                    SpcDictionary.Language = UserInfo.Current.LanguageType;
                    var cond = Conditions.GetValues();
                    cond.Add("P_VALIDSTATE", "Valid");
                    cond.Add("P_DICTIONARYCLASSID", "GROUPCAPTION");
                    cond.Add("P_CONDITIONITEM", "*");
                    cond.Add("P_CONDITIONVALUE", "SPC");
                    SpcDictionary.dtGroupCaption = SqlExecuter.Query("SelectDictionary", "10001", cond);
                    //Console.Write(SpcDictionary.dtGridCaption.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //string testData = SpcDictionary.read(SpcDicClassId.GRID, "SPCPCOUNT");

        }

        #region Sigma 관리
        /// <summary>
        /// XBar Sigmar 분석 자료 반환.
        /// </summary>
        /// <param name="subgroup"></param>
        /// <param name="centerData"></param>
        /// <returns></returns>
        public SpcSigmaResult GetXBarSigmaData(SpcSigmaResult result, ControlSpec controlSpec, ReturnXBarResult rtnXBar, string subgroup)
        {
            //result 객체 Check
            if (result == null)
            {
                result = SpcSigmaResult.Create();
            }
            //rtnXBar 객체 Check
            if (rtnXBar == null)
            {
                return result;
            }

            ////rtnXBar.XBarSigmar 값 Check
            //if (rtnXBar.XBarSigma == SpcLimit.MIN || rtnXBar.XBarSigma == SpcLimit.MAX)
            //{
            //    //Console.WriteLine(rtnXBar.XBAR);
            //    return result;
            //}
            //rtnXBar.XBAR 값 Check
            if (rtnXBar.XBAR == SpcLimit.MIN || rtnXBar.XBAR == SpcLimit.MAX)
            {
                //Console.WriteLine(rtnXBar.XBAR);
                return result;
            }

            result.XBAR = rtnXBar.XBAR;
            result.XRSP = rtnXBar.XRSP;

            //Sigmar 
            if (rtnXBar.XBAR.ToSafeDoubleStaMin() != SpcLimit.MIN)
            {
                //rtnXBar.XBarSigma = rtnXBar.XRSP.ToSafeDoubleStaMin() / d2;
                rtnXBar.uslSigma3 = rtnXBar.UCL.ToSafeDoubleStaMin();
                rtnXBar.uslSigma1 = (rtnXBar.uslSigma3 - rtnXBar.XBAR.ToSafeDoubleStaMin()) / 3;
                rtnXBar.uslSigma2 = rtnXBar.uslSigma1 * 2;

                rtnXBar.lslSigma3 = rtnXBar.LCL.ToSafeDoubleStaMin();
                rtnXBar.lslSigma1 = (rtnXBar.XBAR.ToSafeDoubleStaMin() - rtnXBar.lslSigma3) / 3;
                rtnXBar.lslSigma2 = rtnXBar.lslSigma1 * 2;
            }

            //Sigma
            int nDigit = result.nSigmaRangeDigit;
            result.nSigma = rtnXBar.XBarSigma;
            result.nSigmaRound = Math.Round((result.nSigma), nDigit);
            result.nSigma1 = rtnXBar.uslSigma1;
            result.nSigma2 = rtnXBar.uslSigma2;
            result.nSigma3 = rtnXBar.uslSigma3;

            //result.nSigmaWithin = sigmaWith;
            //result.nSigmaTotal = sigmaTotal;

            //Subgroup Check
            if (result.subGroup != null || result.subGroup == "")
            {
                result.subGroup = subgroup;
            }


            if (result.nSigma1 != SpcLimit.MAX && result.nSigma1 != SpcLimit.MIN && result.nSigma1.ToString() != double.NaN.ToString())
            {
                
                result.nSigma1Max = rtnXBar.XBAR + result.nSigma1;
                result.nSigma1MaxRound = Math.Round((result.nSigma1Max), nDigit);
                result.nSigma2Max = rtnXBar.XBAR + result.nSigma2;
                result.nSigma2MaxRound = Math.Round((result.nSigma2Max), nDigit);
                result.nSigma3Max = result.nSigma3;
                result.nSigma3MaxRound = Math.Round((result.nSigma3Max), nDigit);

                result.nSigma1Min = rtnXBar.XBAR - rtnXBar.lslSigma1;
                result.nSigma1MinRound = Math.Round((result.nSigma1Min), nDigit);
                result.nSigma2Min = rtnXBar.XBAR - rtnXBar.lslSigma2;
                result.nSigma2MinRound = Math.Round((result.nSigma2Min), nDigit);
                result.nSigma3Min = rtnXBar.lslSigma3;
                result.nSigma3MinRound = Math.Round((result.nSigma3Min), nDigit);

                result.nSigma1Round = Math.Round(result.nSigma1, nDigit);
                result.nSigma2Round = Math.Round(result.nSigma2, nDigit);
                result.nSigma3Round = Math.Round(result.nSigma3, nDigit);
            }

            return result;

        }

        /// <summary>
        /// 공정능력 분석 자료 반환.
        /// </summary>
        /// <param name="subgroup"></param>
        /// <param name="centerData"></param>
        /// <returns></returns>
        public SpcSigmaResult GetSigmaData(ControlSpec controlSpec, RtnPPDataTable rtnCpkData, string subgroup, double centerData)
        {
            //sia확인 : 3 sigma 계산 부분.
            SpcSigmaResult result = SpcSigmaResult.Create();

            result = GetCpkResultData(rtnCpkData, subgroup);//공정능력 결과 한행 반환.

            double sigmaWith = SpcLimit.MIN;
            double sigmaTotal = SpcLimit.MIN;

            switch (controlSpec.spcOption.sigmaType)
            {
                case SigmaType.Yes:
                    switch (controlSpec.spcOption.chartType)
                    {
                        case ControlChartType.XBar_R:
                            sigmaWith = result.cpkResult.nSVALUE_RTDC4;//R - RTDC4
                            sigmaTotal = result.cpkResult.nPVALUE_STDC4;
                            break;
                        case ControlChartType.XBar_S:
                            sigmaWith = result.cpkResult.nSVALUE_STDC4;//S - STDC4
                            sigmaTotal = result.cpkResult.nPVALUE_STDC4;
                            break;
                        case ControlChartType.I_MR:
                            sigmaWith = result.cpkResult.nSVALUE_STDC4;//S - STDC4
                            sigmaTotal = result.cpkResult.nPVALUE_STDC4;
                            break;
                        case ControlChartType.Merger:
                            sigmaWith = result.cpkResult.nSVALUE_PTDC4;//합동 - PTDC4
                            sigmaTotal = result.cpkResult.nPVALUE_STDC4;
                            break;
                        case ControlChartType.np:
                            break;
                        case ControlChartType.p:
                            break;
                        case ControlChartType.c:
                            break;
                        case ControlChartType.u:
                            break;
                        default:
                            break;
                    }
                    break;
                case SigmaType.No:
                    switch (controlSpec.spcOption.chartType)
                    {
                        case ControlChartType.XBar_R:
                            //sigmaWith = result.cpkResult.nSVALUE_RTD;
                            sigmaWith = result.cpkResult.nSVALUE_RTDC4;//R - MiniTab에서는 무조건 추정치를 사용함 주의.
                            sigmaTotal = result.cpkResult.nPVALUE_STD;
                            break;
                        case ControlChartType.XBar_S:
                            sigmaWith = result.cpkResult.nSVALUE_STD;
                            sigmaTotal = result.cpkResult.nPVALUE_STD;
                            break;
                        case ControlChartType.I_MR:
                            //sigmaWith = result.cpkResult.nSVALUE_STD;
                            sigmaWith = result.cpkResult.nSVALUE_STDC4;//S - STDC4
                            sigmaTotal = result.cpkResult.nPVALUE_STD;
                            break;
                        case ControlChartType.Merger:
                            sigmaWith = result.cpkResult.nSVALUE_PTD;
                            sigmaTotal = result.cpkResult.nPVALUE_STD;
                            break;
                        case ControlChartType.np:
                            break;
                        case ControlChartType.p:
                            break;
                        case ControlChartType.c:
                            break;
                        case ControlChartType.u:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            //* 주의 * 공정능력 분석 Sigma 입력 - histogram graph 사용.
            result.nSigma = sigmaWith;
            result.nSigmaWithin = sigmaWith;
            result.nSigmaTotal = sigmaTotal;
            result.subGroup = result.cpkResult.SUBGROUP;

            return result;
        }

        /// <summary>
        /// 공정능력 분석 자료 반환.
        /// </summary>
        /// <param name="subgroup"></param>
        /// <param name="centerData"></param>
        /// <returns></returns>
        public SpcSigmaResult GetSigmaData_B20191217(ControlSpec controlSpec, RtnPPDataTable rtnCpkData, string subgroup, double centerData)
        {
            //sia확인 : 3 sigma 계산 부분.
            SpcSigmaResult result = SpcSigmaResult.Create();

            result = GetCpkResultData(rtnCpkData, subgroup);//공정능력 결과 한행 반환.
            double sigmaWith = SpcLimit.MIN;
            double sigmaTotal = SpcLimit.MIN;

            switch (controlSpec.spcOption.sigmaType)
            {
                case SigmaType.Yes:
                    switch (controlSpec.spcOption.chartType)
                    {
                        case ControlChartType.XBar_R:
                            sigmaWith = result.cpkResult.nSVALUE_RTDC4;//R - RTDC4
                            sigmaTotal = result.cpkResult.nPVALUE_STDC4;
                            break;
                        case ControlChartType.XBar_S:
                            sigmaWith = result.cpkResult.nSVALUE_STDC4;//S - STDC4
                            sigmaTotal = result.cpkResult.nPVALUE_STDC4;
                            break;
                        case ControlChartType.I_MR:
                            break;
                        case ControlChartType.Merger:
                            sigmaWith = result.cpkResult.nSVALUE_PTDC4;//합동 - PTDC4
                            sigmaTotal = result.cpkResult.nPVALUE_STDC4;
                            break;
                        case ControlChartType.np:
                            break;
                        case ControlChartType.p:
                            break;
                        case ControlChartType.c:
                            break;
                        case ControlChartType.u:
                            break;
                        default:
                            break;
                    }
                    break;
                case SigmaType.No:
                    switch (controlSpec.spcOption.chartType)
                    {
                        case ControlChartType.XBar_R:
                            sigmaWith = result.cpkResult.nSVALUE_RTD;
                            sigmaTotal = result.cpkResult.nPVALUE_STD;
                            break;
                        case ControlChartType.XBar_S:
                            sigmaWith = result.cpkResult.nSVALUE_STD;
                            sigmaTotal = result.cpkResult.nPVALUE_STD;
                            break;
                        case ControlChartType.I_MR:
                            break;
                        case ControlChartType.Merger:
                            sigmaWith = result.cpkResult.nSVALUE_PTD;
                            sigmaTotal = result.cpkResult.nPVALUE_STD;
                            break;
                        case ControlChartType.np:
                            break;
                        case ControlChartType.p:
                            break;
                        case ControlChartType.c:
                            break;
                        case ControlChartType.u:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            //Sigma
            result.nSigma = sigmaWith;
            result.nSigmaWithin = sigmaWith;
            result.nSigmaTotal = sigmaTotal;
            result.subGroup = subgroup;

            if (result.nSigma != SpcLimit.MAX && result.nSigma != SpcLimit.MIN)
            {
                result.nSigma = result.cpkResult.nSVALUE_PTDC4;

                result.nSigma1 = Math.Round((result.nSigma / 3), 6);
                result.nSigma2 = Math.Round((result.nSigma1 * 2), 6);
                result.nSigma3 = Math.Round((result.nSigma), 6);

                result.nSigma1Max = centerData + result.nSigma1;
                result.nSigma2Max = centerData + result.nSigma2;
                result.nSigma3Max = centerData + result.nSigma3;

                result.nSigma1Min = centerData - result.nSigma1;
                result.nSigma2Min = centerData - result.nSigma2;
                result.nSigma3Min = centerData - result.nSigma3;
            }

            return result;
        }


        /// <summary>
        /// 공정능력 결과 1행 반환.
        /// </summary>
        /// <param name="subgroup"></param>
        /// <returns></returns>
        public SpcSigmaResult GetCpkResultData(RtnPPDataTable rtnCpkData, string subgroup)
        {
            SpcSigmaResult result = SpcSigmaResult.Create();
            //Sigma 추출
            //sia작업 : Null 일때 작업.

            try
            {
                var sigmaData = rtnCpkData.Where(x => x.SUBGROUP == subgroup);

                foreach (DataRow item in sigmaData)
                {
                    //sia확인 : 공정능력 결과 DB 전이.
                    result.cpkResult.nSEQNO = SpcFunction.IsDbNckInt64(item, "SEQNO");
                    result.cpkResult.nGROUPID = SpcFunction.IsDbNckInt64(item, "GROUPID");
                    result.cpkResult.SUBGROUP = SpcFunction.IsDbNck(item, "SUBGROUP");
                    result.cpkResult.nEXTRAID = SpcFunction.IsDbNckInt64(item, "EXTRAID");
                    result.cpkResult.EXTRACONDITIONS = SpcFunction.IsDbNck(item, "EXTRACONDITIONS");
                    result.cpkResult.nLSL = SpcFunction.IsDbNckDoubleMin(item, "LSL");
                    result.cpkResult.nCSL = SpcFunction.IsDbNckDoubleMax(item, "CSL");
                    result.cpkResult.nUSL = SpcFunction.IsDbNckDoubleMax(item, "USL");
                    result.cpkResult.SPECMODE = SpcFunction.IsDbNck(item, "SPECMODE");
                    result.cpkResult.nSAMPLINGCOUNT = SpcFunction.IsDbNckInt64(item, "SAMPLINGCOUNT");
                    result.cpkResult.nSUBGROUPCOUNT = SpcFunction.IsDbNckInt64(item, "SUBGROUPCOUNT");
                    result.cpkResult.nPCOUNT = SpcFunction.IsDbNckInt64(item, "PCOUNT");
                    result.cpkResult.nKCOUNT = SpcFunction.IsDbNckInt64(item, "KCOUNT");
                    result.cpkResult.ISSAME = Convert.ToBoolean(item["ISSAME"]);
                    result.cpkResult.nNVALUE_TOL = SpcFunction.IsDbNckDoubleMax(item, "NVALUE_TOL");
                    result.cpkResult.nNVALUE_AVG = SpcFunction.IsDbNckDoubleMax(item, "NVALUE_AVG");
                    result.cpkResult.nSVALUE_AVG = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_AVG");
                    result.cpkResult.nSVALUE_RTD = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_RTD");
                    result.cpkResult.nSVALUE_RTDC4 = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_RTDC4");
                    result.cpkResult.nSVALUE_STD = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_STD");
                    result.cpkResult.nSVALUE_STDC4 = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_STDC4");
                    result.cpkResult.nSVALUE_PTD = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_PTD");
                    result.cpkResult.nSVALUE_PTDC4 = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_PTDC4");
                    result.cpkResult.nCP = SpcFunction.IsDbNckDoubleMax(item, "CP");
                    result.cpkResult.nCPL = SpcFunction.IsDbNckDoubleMax(item, "CPL");
                    result.cpkResult.nCPU = SpcFunction.IsDbNckDoubleMax(item, "CPU");
                    result.cpkResult.nCPK = SpcFunction.IsDbNckDoubleMax(item, "CPK");
                    result.cpkResult.nCPM = SpcFunction.IsDbNckDoubleMax(item, "CPM");
                    result.cpkResult.nJUDGMENTCPK = SpcFunction.IsDbNck(item, "JUDGMENTCPK");
                    result.cpkResult.nPCISUBGROUP = SpcFunction.IsDbNck(item, "PCISUBGROUP");
                    result.cpkResult.nPCI_d2 = SpcFunction.IsDbNckDoubleMax(item, "PCI_D2");
                    result.cpkResult.nPCI_c4S = SpcFunction.IsDbNckDoubleMax(item, "PCI_C4S");
                    result.cpkResult.nPCI_c4C = SpcFunction.IsDbNckDoubleMax(item, "PCI_C4C");
                    result.cpkResult.nPVALUE_AVG = SpcFunction.IsDbNckDoubleMax(item, "PVALUE_AVG");
                    result.cpkResult.nPVALUE_STD = SpcFunction.IsDbNckDoubleMax(item, "PVALUE_STD");
                    result.cpkResult.nPVALUE_STDC4 = SpcFunction.IsDbNckDoubleMax(item, "PVALUE_STDC4");
                    result.cpkResult.nPP = SpcFunction.IsDbNckDoubleMax(item, "PP");
                    result.cpkResult.nPPL = SpcFunction.IsDbNckDoubleMax(item, "PPL");
                    result.cpkResult.nPPU = SpcFunction.IsDbNckDoubleMax(item, "PPU");
                    result.cpkResult.nPPK = SpcFunction.IsDbNckDoubleMax(item, "PPK");
                    result.cpkResult.JUDGMENTPPK = SpcFunction.IsDbNck(item, "JUDGMENTPPK");
                    result.cpkResult.nPPI_c4 = SpcFunction.IsDbNckDoubleMax(item, "PPI_C4");

                    result.cpkResult.nTargetUSL = SpcFunction.IsDbNckDoubleMax(item, "TAUSL");
                    result.cpkResult.nTargetCSL = SpcFunction.IsDbNckDoubleMax(item, "TACSL");
                    result.cpkResult.nTargetLSL = SpcFunction.IsDbNckDoubleMax(item, "TALSL");

                    result.cpkResult.ppmWithinLSL = SpcFunction.IsDbNckDoubleMin(item, "PPMWITHINLSL");
                    result.cpkResult.ppmWithinUSL = SpcFunction.IsDbNckDoubleMin(item, "PPMWITHINUSL");
                    result.cpkResult.ppmWithinTOT = SpcFunction.IsDbNckDoubleMin(item, "PPMWITHINTOT");
                    result.cpkResult.ppmOverallLSL = SpcFunction.IsDbNckDoubleMin(item, "PPMOVERALLLSL");
                    result.cpkResult.ppmOverallUSL = SpcFunction.IsDbNckDoubleMin(item, "PPMOVERALLUSL");
                    result.cpkResult.ppmOverallTOT = SpcFunction.IsDbNckDoubleMin(item, "PPMOVERALLTOT");
                    result.cpkResult.ppmObserveLSLN = SpcFunction.IsDbNckDoubleMin(item, "PPMOBSERVELSLN");
                    result.cpkResult.ppmObserveUSLN = SpcFunction.IsDbNckDoubleMin(item, "PPMOBSERVEUSLN");
                    result.cpkResult.ppmObserveLSL = SpcFunction.IsDbNckDoubleMin(item, "PPMOBSERVELSL");
                    result.cpkResult.ppmObserveUSL = SpcFunction.IsDbNckDoubleMin(item, "PPMOBSERVEUSL");
                    result.cpkResult.ppmObserveTOT = SpcFunction.IsDbNckDoubleMin(item, "PPMOBSERVETOT");

                    result.cpkResult.nSTATUS = SpcFunction.IsDbNckInt64(item, "STATUS");
                    result.cpkResult.STATUSMESSAGE = SpcFunction.IsDbNck(item, "STATUSMESSAGE");
                    result.cpkResult.nERRORNO = SpcFunction.IsDbNckInt64(item, "ERRORNO");
                    result.cpkResult.ERRORMESSAGE = SpcFunction.IsDbNck(item, "ERRORMESSAGE");

                    //다국어 처리도 이 함수에서 처리함. -> 공정능력 상세 Popup에서 표시됨.
                    //Cpk 결과값 한행(1Raw)을 Data Table 형식으로 전이.
                    SpcSigmaResult.CpkResultDataTableWrite(ref result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        #endregion Sigma 관리

        /// <summary>
        /// SPC 조회 조건중 일자 List 반환 함수.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<SpcConditionDateDays> SpcConditionList(string startDate, string endDate)
        {
            List<SpcConditionDateDays> rtnlstDay = new List<SpcConditionDateDays>();

            try
            {

                //DateTime dateStart = Convert.ToDateTime(param["P_PERIOD_PERIODFR"]); //2020-02-01 08:30:00
                //DateTime dateEnd = Convert.ToDateTime(param["P_PERIOD_PERIODTO"]);//2020-03-01 08:30:00

                DateTime dateStart;     //Convert.ToDateTime("2020-02-01 08:30:00");
                DateTime dateEnd;       //Convert.ToDateTime("2020-02-10 08:30:00");
                DateTime dateStartAdd;
                DateTime dateStartEnd;

                dateStart = Convert.ToDateTime(startDate);
                dateEnd = Convert.ToDateTime(endDate);


                TimeSpan ts = dateEnd - dateStart;

                int diffDay = ts.Days;  //날짜의 차이 구하기
                if (diffDay <= 0)
                {
                    diffDay = 1;
                }

                for (int i = 0; i < diffDay; i++)
                {
                    dateStartAdd = dateStart.AddDays(i);
                    dateStartEnd = dateStart.AddDays(i + 1);
                    //Console.WriteLine(dateStartAdd.ToString("yyyy-MM-dd HH:mm:ss"));
                    SpcConditionDateDays tempDays = new SpcConditionDateDays();
                    if (i != 0)
                    {
                        tempDays.DateStart = dateStartAdd.ToString("yyyy-MM-dd 00:00:00");
                    }
                    else
                    {
                        tempDays.DateStart = dateStartAdd.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (i < diffDay - 1)
                    {
                        tempDays.DateEnd = dateStartEnd.ToString("yyyy-MM-dd 00:00:00");
                    }
                    else
                    {
                        tempDays.DateEnd = dateEnd.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    rtnlstDay.Add(tempDays);

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rtnlstDay;
        }

        /// <summary>
        /// Spc 조회 조건 시간별 List 함수.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<SpcConditionDateDays> SpcConditionListHour(string startDate, string endDate)
        {
            List<SpcConditionDateDays> rtnlstHour = new List<SpcConditionDateDays>();

            try
            {

                //DateTime dateStart = Convert.ToDateTime(param["P_PERIOD_PERIODFR"]); //2020-02-01 08:30:00
                //DateTime dateEnd = Convert.ToDateTime(param["P_PERIOD_PERIODTO"]);//2020-03-01 08:30:00

                DateTime HourStart;     //Convert.ToDateTime("2020-02-01 08:30:00");
                DateTime HourEnd;       //Convert.ToDateTime("2020-02-10 08:30:00");
                DateTime HourStartAdd;
                DateTime HourStartEnd;

                DateTime dateEndTemp;
                DateTime dateEndFirst;
                int addHours = 0;
                HourStart = Convert.ToDateTime(startDate);
                HourEnd = Convert.ToDateTime(endDate);
                string endFirst = HourEnd.ToString("yyyy-MM-dd 00:00:00");
                dateEndFirst = Convert.ToDateTime(endFirst);

                TimeSpan ts = HourEnd - HourStart;
                TimeSpan tsEnd = HourEnd - dateEndFirst;

                int diffHour = 0;
                if (tsEnd.Hours > 0)
                {
                    addHours = tsEnd.Hours;
                    ts = dateEndFirst - HourStart;
                    if (ts.Hours == 0)
                    {
                        diffHour = 24;
                    }
                    diffHour = diffHour + addHours + 1;  //시간의 차이 구하기
                }
                else
                {
                    diffHour = ts.Hours + 1;  //시간의 차이 구하기
                }

                
                if (diffHour <= 1)
                {
                    if (ts.Hours == 00 && ts.Days == 1)
                    {
                        diffHour = 24;
                    }
                    else
                    {
                        diffHour = 1;
                    }
                }

                for (int i = 0; i < diffHour; i++)
                {
                    HourStartAdd = HourStart.AddHours(i);
                    HourStartEnd = HourStart.AddHours(i + 1);
                    //Console.WriteLine(dateStartAdd.ToString("yyyy-MM-dd HH:mm:ss"));
                    SpcConditionDateDays tempHours = new SpcConditionDateDays();
                    if (i != 0)
                    {
                        tempHours.DateStart = HourStartAdd.ToString("yyyy-MM-dd HH:00:00");
                    }
                    else
                    {
                        tempHours.DateStart = HourStartAdd.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (i < diffHour - 1)
                    {
                        dateEndTemp = HourStart.AddHours(i + 1);
                        tempHours.DateEnd = dateEndTemp.ToString("yyyy-MM-dd HH:00:00");
                    }
                    else
                    {
                        tempHours.DateEnd = string.Format("{0}", HourEnd.ToString("yyyy-MM-dd HH:mm:ss"));
                    }

                    rtnlstHour.Add(tempHours);

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rtnlstHour;
        }

        /// <summary>
        /// Spec Data - DataTable - 업무별 조회한 Raw자료중 Subgroup별 Spec 저장.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable SpcCreateMainRawSpecTable(string tableName = "dtTempMainRawSpecData")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("SUBGROUP", typeof(string));
            dt.Columns.Add("CTYPE", typeof(string));
            dt.Columns.Add("SPECTYPE", typeof(string));
            dt.Columns.Add("USL", typeof(double));
            dt.Columns.Add("SL", typeof(double));
            dt.Columns.Add("LSL", typeof(double));
            dt.Columns.Add("LCL", typeof(double));
            dt.Columns.Add("CL", typeof(double));
            dt.Columns.Add("UOL", typeof(double));
            dt.Columns.Add("LOL", typeof(double));
            return dt;
        }

        #region Chart Tool Strip Menu Item 처리

        /// <summary>
        /// Chart image Save
        /// </summary>
        /// <param name="chart">Chart 객체</param>
        /// <param name="formatDefault">기본 파일 확장자</param>
        /// <param name="fileNameDefault">기본 파일 명</param>
        public static void ChartImageSave(ChartControl chart, String fileNameDefault = "Chart", string frmTitle = "Save an Chart image", ChartImageFormat formatDefault = ChartImageFormat.Gif)
        {
            ImageFormat formatType = ImageFormat.Gif;
            SaveFileDialog saveimage = new SaveFileDialog();
            //saveimage.FileName = "Chart" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx"; //초기 파일명을 지정할 때 사용한다.
            saveimage.FileName = string.Format("{0}_{1}", fileNameDefault, DateTime.Now.ToString("yyyyMMdd_HHmmss")); //초기 파일명을 지정할 때 사용한다.
            //saveimage.Filter = "Excel|*.xlsx";
            //saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveimage.Filter = "Gif Image|*.gif|Png Image|*.png|JPeg Image|*.jpg|Bitmap Image|*.bmp";
            saveimage.Title = frmTitle; // "Save an Chart image";
            saveimage.ShowDialog();
            switch (saveimage.FilterIndex)
            {
                case 1:
                    formatType = ImageFormat.Gif;
                    break;
                case 2:
                    formatType = ImageFormat.Jpeg;
                    break;
                case 3:
                    formatType = ImageFormat.Png;
                    break;
                case 4:
                    formatType = ImageFormat.Bmp;
                    break;

            }

            if (saveimage.FileName != "")
            {
                //SaveChartImageToFile(ChrPoltA, ImageFormat.Png, "D://image1.png");
                SaveChartImageToFile(chart, formatType, saveimage.FileName);
                //Image image = ChartImage(ChrPoltA, ImageFormat.Png);
                //image.Save(saveimage.FileName);
            }
        }

        /// <summary>
        /// Chart 이미지 복사
        /// </summary>
        /// <param name="chart"></param>
        public static void ChartImageCopy(ChartControl chart)
        {
            try
            {
                Image image = ChartImage(chart, ImageFormat.Gif);
                Clipboard.SetImage(image);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Chart Image Memory Copy
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Image ChartImage(ChartControl chart, ImageFormat format)
        {
            Image image = null;
            try
            {
                using (MemoryStream s = new MemoryStream())
                {
                    chart.ExportToImage(s, format);
                    image = Image.FromStream(s);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return image;
        }
        /// <summary>
        /// Chart Image Export
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="format"></param>
        /// <param name="fileName"></param>
        public static void SaveChartImageToFile(ChartControl chart, ImageFormat format, String fileName)
        {
            try
            {
                chart.ExportToImage(fileName, format);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Directory 생성.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DirectoryCreate(string path)
        {
            bool result = false;
            string sDirPath;

            try
            {
                sDirPath = Application.StartupPath + "\\images";
                DirectoryInfo di = new DirectoryInfo(sDirPath);
                if (di.Exists == false)
                {
                    di.Create();

                }
                result = true;

                if (di.Exists == false)
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Image Clipboard 복사
        /// </summary>
        /// <param name="replacementImage"></param>
        /// <returns></returns>
        public static Image SwapClipboardImage(Image replacementImage)
        {
            Image returnImage = null;

            try
            {
                if (Clipboard.ContainsImage())
                {
                    returnImage = Clipboard.GetImage();
                    Clipboard.SetImage(replacementImage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return returnImage;
        }

        #endregion Chart Tool Strip Menu Item 처리
    }

    /// <summary>
    /// SPC 통계 분석용 입력 Parameter
    /// </summary>
    public class SpcParameter
    {
        #region Global Variable

        /// <summary>
        /// Spc 입력 측정값 저장.
        /// </summary>
        public DataTable SpcData;

        /// <summary>
        /// Spc 입력 통계분석 측정값 저장. 10/2
        /// </summary>
        public DataTable SpcDataAnalysisTable;

        #endregion 

        #region 생성자

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static SpcParameter Create()
        {
            SpcParameter c = new SpcParameter();
            c.SpcData = c.CreateTableSpcData();
            c.SpcDataAnalysisTable = c.CreateTableSpcDataAnalysisMode();
            return c;
        }

        #endregion

        #region Private Function

        /// <summary>SPC 자료 등록 테이블 객체 생성.
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTableSpcData()
        {
            DataTable dataTable = new DataTable();

            //XBAR
            dataTable.Columns.Add("Label", typeof(string));
            dataTable.Columns.Add("LabelInt", typeof(Int64));
            dataTable.Columns.Add("nValue", typeof(double));
            dataTable.Columns.Add("nSigmaWithin", typeof(double));
            dataTable.Columns.Add("nSigmaTotal", typeof(double));
            dataTable.Columns.Add("nOverRuleUSL", typeof(double));
            dataTable.Columns.Add("nOverRuleLSL", typeof(double));
            dataTable.Columns.Add("nOverRuleUCL", typeof(double));
            dataTable.Columns.Add("nOverRuleLCL", typeof(double));
            dataTable.Columns.Add("nOverRuleUOL", typeof(double));
            dataTable.Columns.Add("nOverRuleLOL", typeof(double));
            //dataTable.Columns.Add("UOL", typeof(DateTime));
            dataTable.Columns.Add("UOL", typeof(double));
            dataTable.Columns.Add("LOL", typeof(double));
            dataTable.Columns.Add("USL", typeof(double));
            dataTable.Columns.Add("CSL", typeof(double));
            dataTable.Columns.Add("LSL", typeof(double));
            dataTable.Columns.Add("UCL", typeof(double));
            dataTable.Columns.Add("CCL", typeof(double));
            dataTable.Columns.Add("LCL", typeof(double));
            dataTable.Columns.Add("AUCL", typeof(double));//Analysis
            dataTable.Columns.Add("ACCL", typeof(double));
            dataTable.Columns.Add("ALCL", typeof(double));

            //R
            dataTable.Columns.Add("nRValue", typeof(double));
            dataTable.Columns.Add("nROverRuleUCL", typeof(double));
            dataTable.Columns.Add("nROverRuleLCL", typeof(double));
            dataTable.Columns.Add("RAUCL", typeof(double));//Analysis
            dataTable.Columns.Add("RACCL", typeof(double));
            dataTable.Columns.Add("RALCL", typeof(double));

            return dataTable;
        }

        /// <summary>SPC 자료 등록 테이블 객체 생성. (분석용)
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTableSpcDataAnalysisMode()
        {
            DataTable dataTable = new DataTable();

            //XBAR
            dataTable.Columns.Add("SUBGROUP", typeof(string));
            dataTable.Columns.Add("SUBGROUPNAME", typeof(string));
            dataTable.Columns.Add("SAMPLING", typeof(string));
            dataTable.Columns.Add("SAMPLINGNAME", typeof(string));
            dataTable.Columns.Add("Label", typeof(string));
            dataTable.Columns.Add("LabelInt", typeof(Int64));
            dataTable.Columns.Add("nValue", typeof(double));

            //Candle Stick Type
            dataTable.Columns.Add("n01LowMin", typeof(double));
            dataTable.Columns.Add("n02HightMax", typeof(double));
            dataTable.Columns.Add("n03CloseQ1", typeof(double));
            dataTable.Columns.Add("n04Median", typeof(double));
            dataTable.Columns.Add("n05Mean", typeof(double));
            dataTable.Columns.Add("n06OpenQ3", typeof(double));

            dataTable.Columns.Add("n11pCBS", typeof(double));
            dataTable.Columns.Add("n12CBS", typeof(double));
            dataTable.Columns.Add("n21Qp1", typeof(int));
            dataTable.Columns.Add("n22Qp2", typeof(int));
            dataTable.Columns.Add("n23Qp3", typeof(int));
            dataTable.Columns.Add("n24Qp4", typeof(int));
            dataTable.Columns.Add("n25Q1", typeof(double));
            dataTable.Columns.Add("n26Q2", typeof(double));
            dataTable.Columns.Add("n27Q3", typeof(double));
            dataTable.Columns.Add("n28Q4", typeof(double));
            dataTable.Columns.Add("n31UiQR", typeof(double));
            dataTable.Columns.Add("n32LiQR", typeof(double));
            dataTable.Columns.Add("n33SamplingCount", typeof(int));

            dataTable.Columns.Add("n41MIN", typeof(double));
            dataTable.Columns.Add("n42MAX", typeof(double));



            dataTable.Columns.Add("nSigmaWithin", typeof(double));
            dataTable.Columns.Add("nSigmaTotal", typeof(double));
            dataTable.Columns.Add("nOverRuleUSL", typeof(double));
            dataTable.Columns.Add("nOverRuleLSL", typeof(double));
            dataTable.Columns.Add("nOverRuleUCL", typeof(double));
            dataTable.Columns.Add("nOverRuleLCL", typeof(double));
            dataTable.Columns.Add("nOverRuleUOL", typeof(double));
            dataTable.Columns.Add("nOverRuleLOL", typeof(double));
            //dataTable.Columns.Add("UOL", typeof(DateTime));
            dataTable.Columns.Add("UOL", typeof(double));
            dataTable.Columns.Add("LOL", typeof(double));
            dataTable.Columns.Add("USL", typeof(double));
            dataTable.Columns.Add("CSL", typeof(double));
            dataTable.Columns.Add("LSL", typeof(double));
            dataTable.Columns.Add("UCL", typeof(double));
            dataTable.Columns.Add("CCL", typeof(double));
            dataTable.Columns.Add("LCL", typeof(double));
            dataTable.Columns.Add("AUCL", typeof(double));//Analysis
            dataTable.Columns.Add("ACCL", typeof(double));
            dataTable.Columns.Add("ALCL", typeof(double));

            //R
            dataTable.Columns.Add("nRValue", typeof(double));
            dataTable.Columns.Add("nROverRuleUCL", typeof(double));
            dataTable.Columns.Add("nROverRuleLCL", typeof(double));
            dataTable.Columns.Add("RAUCL", typeof(double));//Analysis
            dataTable.Columns.Add("RACCL", typeof(double));
            dataTable.Columns.Add("RALCL", typeof(double));

            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <param name="label"></param>
        /// <param name="labelName"></param>
        /// <param name="value"></param>
        private void SetSpcParameterToLabel(SpcParameter para, string label, object labelName, float value)
        {
            DataRow rowSpcData = para.SpcData.NewRow();
            rowSpcData[label] = labelName;
            rowSpcData["nValue"] = value;
            para.SpcData.Rows.Add(rowSpcData);
        }

        #endregion

        #region Public Function

        /// <summary>
        /// 관리도 SPEC Check 
        /// </summary>
        /// <param name="returnSpec"></param>
        /// <returns></returns>
        public DataTable SpecCheckXBar(ControlSpec controlSpec, ref SpcParameter xbarData)
        {
            foreach (DataRow row in xbarData.SpcData.Rows)
            {
                double nvalue;
                double nRvalue;

                //Xbar
                //nvalue = Convert.IsDBNull(row["nValue"]) ? 0.0f : Convert.ToDouble(row["nValue"]);
                nvalue = Convert.IsDBNull(row["nValue"]) ? 0.0f : (double)row["nValue"];
                nRvalue = Convert.IsDBNull(row["nRValue"]) ? 0.0f : (double)row["nRValue"];
                //row["AUCL"] = nvalue + 15f;
                //row["ALCL"] = nvalue - 15f;

                ////R
                //nRvalue = nvalue - 3.5;
                //row["nRValue"] = nRvalue;
                //row["RAUCL"] = nRvalue + 11.3f;
                //row["RALCL"] = nRvalue - 11.2f;



                //Control
                if (controlSpec.nXbar.ucl != SpcLimit.MIN && controlSpec.nXbar.ucl != SpcLimit.MAX)
                {
                    if (nvalue > controlSpec.nXbar.ucl)
                    {
                        row["nOverRuleUCL"] = nvalue;
                    }
                }
                if (controlSpec.nXbar.lcl != SpcLimit.MIN && controlSpec.nXbar.lcl != SpcLimit.MAX)
                { 
                    if (nvalue < controlSpec.nXbar.lcl)
                    {
                        row["nOverRuleLCL"] = nvalue;
                    }
                }

                //Spec
                if (controlSpec.nXbar.usl != SpcLimit.MIN && controlSpec.nXbar.usl != SpcLimit.MAX)
                {
                    if (nvalue > controlSpec.nXbar.usl)
                    {
                        row["nOverRuleUSL"] = nvalue;
                    }
                }

                if (controlSpec.nXbar.lsl != SpcLimit.MIN && controlSpec.nXbar.lsl != SpcLimit.MAX)
                {
                    if (nvalue < controlSpec.nXbar.lsl)
                    {
                        row["nOverRuleLSL"] = nvalue;
                    }
                }

                //Outlier
                if (controlSpec.nXbar.uol != SpcLimit.MIN && controlSpec.nXbar.uol != SpcLimit.MAX)
                {
                    if (nvalue > controlSpec.nXbar.uol)
                    {
                        row["nOverRuleUOL"] = nvalue;
                    }
                }
                if (controlSpec.nXbar.lol != SpcLimit.MIN && controlSpec.nXbar.lol != SpcLimit.MAX)
                {
                    if (nvalue < controlSpec.nXbar.lol)
                    {
                        row["nOverRuleLOL"] = nvalue;
                    }
                }
                //R Control
                if (controlSpec.nR.ucl != SpcLimit.MIN && controlSpec.nR.ucl != SpcLimit.MAX)
                {
                    if (nRvalue > controlSpec.nR.ucl)
                    {
                        row["nROverRuleUCL"] = nRvalue;
                    }
                }
                if (controlSpec.nR.lcl != SpcLimit.MIN && controlSpec.nR.lcl != SpcLimit.MAX)
                {
                    if (nRvalue < controlSpec.nR.lcl)
                    {
                        row["nROverRuleLCL"] = nRvalue;
                    }
                }
            }

            return xbarData.SpcData;
            //Console.WriteLine(xbarData.SpcData.Rows.Count);
        }

        /// <summary>
        /// Control Chart 초기화 Data 생성 함수.
        /// </summary>
        /// <param name="returnSpecXbar"></param>
        /// <returns></returns>
        public DataTable ClearDataSpcXBar(out ControlSpec returnSpec)
        {
            ControlSpec cntrolSpec = ControlSpec.Create();
            returnSpec = cntrolSpec;

            //rowSpcData["nOverRule"] = 86.3f;
            SpcParameter xbarData = SpcParameter.Create();
            DataRow rowSpcData;
            //일괄 Update 같은 값을 전부 적용함.
            //sia참조 : Linq-Update문
            xbarData.SpcData.AsEnumerable().ToList<DataRow>().ForEach(r =>
            {
                r["UOL"] = cntrolSpec.nXbar.uol; r["LOL"] = cntrolSpec.nXbar.lol;
                r["USL"] = cntrolSpec.nXbar.usl; r["CSL"] = cntrolSpec.nXbar.csl; r["LSL"] = cntrolSpec.nXbar.lsl;
                r["UCL"] = cntrolSpec.nXbar.ucl; r["CCL"] = cntrolSpec.nXbar.ccl; r["LCL"] = cntrolSpec.nXbar.lcl;
                //r["AUCL"] = nUCL + 15; r["ACCL"] = nCCL; r["ALCL"] = nLCL - 15;
            });

            foreach (DataRow row in xbarData.SpcData.Rows)
            {
                double nvalue;
                double nRvalue;

                //Xbar
                //nvalue = Convert.IsDBNull(row["nValue"]) ? 0.0f : Convert.ToDouble(row["nValue"]);
                nvalue = Convert.IsDBNull(row["nValue"]) ? 0.0f : (double)row["nValue"];
                row["AUCL"] = nvalue + 15f;
                row["ALCL"] = nvalue - 15f;

                //R
                nRvalue = nvalue - 3.5;
                row["nRValue"] = nRvalue;
                row["RAUCL"] = nRvalue + 11.3f;
                row["RALCL"] = nRvalue - 11.2f;

                //Spec
                if (nvalue > cntrolSpec.nXbar.usl)
                {
                    row["nOverRuleUSL"] = nvalue;
                }

                if (nvalue < cntrolSpec.nXbar.lsl)
                {
                    row["nOverRuleLSL"] = nvalue;
                }

                //Control
                if (nvalue > cntrolSpec.nXbar.ucl)
                {
                    row["nOverRuleUCL"] = nvalue;
                }

                if (nvalue < cntrolSpec.nXbar.lcl)
                {
                    row["nOverRuleLCL"] = nvalue;
                }

                //Outlier
                if (nvalue > cntrolSpec.nXbar.uol)
                {
                    row["nOverRuleUOL"] = nvalue;
                }

                if (nvalue < cntrolSpec.nXbar.lol)
                {
                    row["nOverRuleLOL"] = nvalue;
                }

                //R Control
                if (nRvalue > cntrolSpec.nR.ucl)
                {
                    row["nROverRuleUCL"] = nRvalue;
                }

                if (nRvalue < cntrolSpec.nR.lcl)
                {
                    row["nROverRuleLCL"] = nRvalue;
                }
            }

            return xbarData.SpcData;
            //Console.WriteLine(xbarData.SpcData.Rows.Count);
        }

        /// <summary>
        /// Control Chart Test Data 생성 함수.
        /// </summary>
        /// <param name="returnSpecXbar"></param>
        /// <returns></returns>
        public DataTable TestDataSpcXBar(out ControlSpec returnSpec)
        {
            ControlSpec cntrolSpec = ControlSpec.Create();
            //cntrolSpec.nXbar.value= double.NaN;
            cntrolSpec.nXbar.uol = 110.5f;
            cntrolSpec.nXbar.lol = -10.5f;
            cntrolSpec.nXbar.usl = 83.78f;
            cntrolSpec.nXbar.csl = 41.55f;//Xbar
            cntrolSpec.nXbar.lsl = 13.324;
            cntrolSpec.nXbar.ucl = 56.78f;
            cntrolSpec.nXbar.ccl = 23.67f; //xR
            cntrolSpec.nXbar.lcl = 27.3243f;

            //cntrolSpec.nR.value = double.NaN;
            //cntrolSpec.nR.uol = double.NaN;
            //cntrolSpec.nR.lol = double.NaN;
            //cntrolSpec.nR.usl = double.NaN;
            //cntrolSpec.nR.csl = double.NaN;
            //cntrolSpec.nR.lsl = double.NaN;
            cntrolSpec.nR.ucl = 56.78f;
            //cntrolSpec.nR.ccl = double.NaN;
            cntrolSpec.nR.lcl = 27.3243f;

            returnSpec = cntrolSpec;

            //rowSpcData["nOverRule"] = 86.3f;
            SpcParameter xbarData = SpcParameter.Create();

            SetSpcParameterToLabel(xbarData, "Label", "Lot-001", 10.85f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-002", 65.73f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-003", 65.73f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-004", 4.93f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-005", 28.5f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-006", 53.16f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-007", 63.55f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-008", 86.3f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-009", 28.62f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-010", 80.76f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-011", 13.48f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-012", 10.85f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-013", 97.23f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-014", 88.5f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-015", 93.09f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-016", 53.83f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-017", 41.32f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-018", 59.73f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-019", 115.2f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-020", -12.5f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-021", 46.18f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-022", 20.55f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-023", 13.33f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-024", 37.84f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-025", 63.06f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-026", 62.85f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-027", 7.63f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-028", 74.61f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-029", 76.79f);
            SetSpcParameterToLabel(xbarData, "Label", "Lot-030", 75.57f);

            //일괄 Update 같은 값을 전부 적용함.
            xbarData.SpcData.AsEnumerable().ToList<DataRow>().ForEach(r =>
            {
                r["UOL"] = cntrolSpec.nXbar.uol; r["LOL"] = cntrolSpec.nXbar.lol;
                r["USL"] = cntrolSpec.nXbar.usl; r["CSL"] = cntrolSpec.nXbar.csl; r["LSL"] = cntrolSpec.nXbar.lsl;
                r["UCL"] = cntrolSpec.nXbar.ucl; r["CCL"] = cntrolSpec.nXbar.ccl; r["LCL"] = cntrolSpec.nXbar.lcl;
                //r["AUCL"] = nUCL + 15; r["ACCL"] = nCCL; r["ALCL"] = nLCL - 15;
            });

            foreach (DataRow row in xbarData.SpcData.Rows)
            {
                double nvalue;
                double nRvalue;

                //Xbar
                //nvalue = Convert.IsDBNull(row["nValue"]) ? 0.0f : Convert.ToDouble(row["nValue"]);
                nvalue = Convert.IsDBNull(row["nValue"]) ? 0.0f : (double)row["nValue"];
                row["AUCL"] = nvalue + 15f;
                row["ALCL"] = nvalue - 15f;

                //R
                nRvalue = nvalue - 3.5;
                row["nRValue"] = nRvalue;
                row["RAUCL"] = nRvalue + 11.3f;
                row["RALCL"] = nRvalue - 11.2f;

                //Spec
                if (nvalue > cntrolSpec.nXbar.usl)
                {
                    row["nOverRuleUSL"] = nvalue;
                }

                if (nvalue < cntrolSpec.nXbar.lsl)
                {
                    row["nOverRuleLSL"] = nvalue;
                }

                //Control
                if (nvalue > cntrolSpec.nXbar.ucl)
                {
                    row["nOverRuleUCL"] = nvalue;
                }

                if (nvalue < cntrolSpec.nXbar.lcl)
                {
                    row["nOverRuleLCL"] = nvalue;
                }

                //Outlier
                if (nvalue > cntrolSpec.nXbar.uol)
                {
                    row["nOverRuleUOL"] = nvalue;
                }

                if (nvalue < cntrolSpec.nXbar.lol)
                {
                    row["nOverRuleLOL"] = nvalue;
                }

                //R Control
                if (nRvalue > cntrolSpec.nR.ucl)
                {
                    row["nROverRuleUCL"] = nRvalue;
                }

                if (nRvalue < cntrolSpec.nR.lcl)
                {
                    row["nROverRuleLCL"] = nRvalue;
                }
            }

            return xbarData.SpcData;
            //Console.WriteLine(xbarData.SpcData.Rows.Count);
        }

        /// <summary>
        /// 공정능력 Chart Test Data 생성 함수.
        /// </summary>
        /// <param name="returnSpec"></param>
        /// <returns></returns>
        public DataTable TestDataSpcCpk(out ControlSpec returnSpec)
        {
            ControlSpec cntrolSpec = ControlSpec.Create();
            //cntrolSpec.nXbar.value= double.NaN;
            cntrolSpec.nXbar.usl = 83.78f;
            cntrolSpec.nXbar.csl = 41.55f;//Xbar
            cntrolSpec.nXbar.lsl = 13.324;

            returnSpec = cntrolSpec;

            //rowSpcData["nOverRule"] = 86.3f;
            SpcParameter xbarData = SpcParameter.Create();
            SetSpcParameterToLabel(xbarData, "LabelInt", 10, 10.85f);
            SetSpcParameterToLabel(xbarData, "LabelInt", 20, 65.73f);
            SetSpcParameterToLabel(xbarData, "LabelInt", 30, 33.41f);
            SetSpcParameterToLabel(xbarData, "LabelInt", 40, 4.93f);
            SetSpcParameterToLabel(xbarData, "LabelInt", 50, 28.5f);
            SetSpcParameterToLabel(xbarData, "LabelInt", 60, 53.16f);
            SetSpcParameterToLabel(xbarData, "LabelInt", 70, 63.55f);
            SetSpcParameterToLabel(xbarData, "LabelInt", 80, 86.3f);
            SetSpcParameterToLabel(xbarData, "LabelInt", 90, 28.62f);
            SetSpcParameterToLabel(xbarData, "LabelInt", 100, 80.76f);

            //일괄 Update 같은 값을 전부 적용함.
            xbarData.SpcData.AsEnumerable().ToList<DataRow>().ForEach(r =>
            {
                r["UOL"] = cntrolSpec.nXbar.uol; r["LOL"] = cntrolSpec.nXbar.lol;
                r["USL"] = cntrolSpec.nXbar.usl; r["CSL"] = cntrolSpec.nXbar.csl; r["LSL"] = cntrolSpec.nXbar.lsl;
                r["UCL"] = cntrolSpec.nXbar.ucl; r["CCL"] = cntrolSpec.nXbar.ccl; r["LCL"] = cntrolSpec.nXbar.lcl;
                //r["AUCL"] = nUCL + 15; r["ACCL"] = nCCL; r["ALCL"] = nLCL - 15;
            });

            foreach (DataRow row in xbarData.SpcData.Rows)
            {
                double nvalue;
                double nRvalue;

                //Xbar
                //nvalue = Convert.IsDBNull(row["nValue"]) ? 0.0f : Convert.ToDouble(row["nValue"]);
                nvalue = Convert.IsDBNull(row["nValue"]) ? 0.0f : (double)row["nValue"];
                row["AUCL"] = nvalue + 15f;
                row["ALCL"] = nvalue - 15f;

                //R
                nRvalue = nvalue - 3.5;
                row["nRValue"] = nRvalue;
                row["RAUCL"] = nRvalue + 11.3f;
                row["RALCL"] = nRvalue - 11.2f;

                //Spec
                if (nvalue > cntrolSpec.nXbar.usl)
                {
                    row["nOverRuleUSL"] = nvalue;
                }

                if (nvalue < cntrolSpec.nXbar.lsl)
                {
                    row["nOverRuleLSL"] = nvalue;
                }

                //Control
                if (nvalue > cntrolSpec.nXbar.ucl)
                {
                    row["nOverRuleUCL"] = nvalue;
                }

                if (nvalue < cntrolSpec.nXbar.lcl)
                {
                    row["nOverRuleLCL"] = nvalue;
                }

                //Outlier
                if (nvalue > cntrolSpec.nXbar.uol)
                {
                    row["nOverRuleUOL"] = nvalue;
                }

                if (nvalue < cntrolSpec.nXbar.lol)
                {
                    row["nOverRuleLOL"] = nvalue;
                }

                //R Control
                if (nRvalue > cntrolSpec.nR.ucl)
                {
                    row["nROverRuleUCL"] = nRvalue;
                }

                if (nRvalue < cntrolSpec.nR.lcl)
                {
                    row["nROverRuleLCL"] = nRvalue;
                }
            }

            return xbarData.SpcData;
            //Console.WriteLine(xbarData.SpcData.Rows.Count);
        }

        /// <summary>
        /// 공정능력 Chart Test Data 생성 함수.
        /// </summary>
        /// <param name="returnSpec"></param>
        /// <returns></returns>
        public DataTable CpkClearData(out ControlSpec returnSpec)
        {
            ControlSpec cntrolSpec = ControlSpec.Create();
            //cntrolSpec.nXbar.value= double.NaN;
            cntrolSpec.nXbar.usl = 0f;
            cntrolSpec.nXbar.csl = 0f;//Xbar
            cntrolSpec.nXbar.lsl = 0f;

            returnSpec = cntrolSpec;

            //rowSpcData["nOverRule"] = 86.3f;
            SpcParameter xbarData = SpcParameter.Create();
            SetSpcParameterToLabel(xbarData, "LabelInt", 0, 0f);

            //일괄 Update 같은 값을 전부 적용함.
            xbarData.SpcData.AsEnumerable().ToList<DataRow>().ForEach(r =>
            {
                r["UOL"] = cntrolSpec.nXbar.uol; r["LOL"] = cntrolSpec.nXbar.lol;
                r["USL"] = cntrolSpec.nXbar.usl; r["CSL"] = cntrolSpec.nXbar.csl; r["LSL"] = cntrolSpec.nXbar.lsl;
                r["UCL"] = cntrolSpec.nXbar.ucl; r["CCL"] = cntrolSpec.nXbar.ccl; r["LCL"] = cntrolSpec.nXbar.lcl;
                //r["AUCL"] = nUCL + 15; r["ACCL"] = nCCL; r["ALCL"] = nLCL - 15;
            });

            return xbarData.SpcData;
            //Console.WriteLine(xbarData.SpcData.Rows.Count);
        }

        #endregion
    }

    /// <summary>
    /// SPC 통계 분석용 고정 Check용 변수.
    /// </summary>
    public static class SpcFlag
    {
        /// <summary>
        /// java heap 오류 방지. 2/5
        /// </summary>
        public const long nSpcQueryMaxCount = 20001;

        /// <summary>
        /// 마지막 자료 구분.
        /// </summary>
        public const string DataEnd = "@#DataEnd";
        /// <summary>
        /// Test Mode 구분 0-운영, 1-Test
        /// </summary>
        public static int isTestMode;
    }

    /// <summary>
    /// Trend 가공 Class
    /// </summary>
    public class TrendClass
    {
        public string Month;
        public string CreatedTime;
        public string EnterpriseId;
        public string PlantId;
        public string ProductDefId;
        public double PcsQty;
        public double DefectQty;
        public double Rate;
        public static List<TrendClass> TrendClassList;

        public static List<TrendClass> Create(DataTable dt)
        {
            TrendClassList = new List<TrendClass>();

            foreach (DataRow dr in dt.Rows)
            {
                TrendClassList.Add(new TrendClass()
                {
                    Month = Convert.ToDateTime(dr["SUBGROUP"]).ToString("yyyy/M").Replace("-", "/"),
                    CreatedTime = Convert.ToDateTime(dr["SAMPLING"]).ToString("M/d").Replace("-", "/"),
                    EnterpriseId = dr["ENTERPRISEID"].ToString(),
                    PlantId = dr["PLANTID"].ToString(),
                    ProductDefId = dr["PRODUCTDEFID"].ToString(),
                    PcsQty = dr["NSUBVALUE"].ToSafeInt32(),
                    DefectQty = dr["NVALUE"].ToSafeInt32()
                });
            }

            TrendClassList = (from main in TrendClassList
                              group main by new { main.CreatedTime, main.Month, main.ProductDefId } into gr
                              select new TrendClass
                              {
                                  CreatedTime = gr.Key.CreatedTime,
                                  Month = gr.Key.Month,
                                  ProductDefId = gr.Key.ProductDefId,
                                  PcsQty = gr.Sum(x => x.PcsQty),
                                  DefectQty = gr.Sum(x => x.DefectQty)
                              }).ToList();

            return TrendClassList;
        }
    }

    /// <summary>
    /// SPC 이벤트 Option 
    /// </summary>
    public class SpcEventsOption
    {
        /// <summary>
        /// 그룹명 ID
        /// </summary>
        public string groupId;
        /// <summary>
        /// 그룹명
        /// </summary>
        public string groupName;
        /// <summary>
        /// 서브 Id
        /// </summary>
        public string subId;
        /// <summary>
        /// 서브 그룹명
        /// </summary>
        public string subName;
        /// <summary>
        /// Chart 번호
        /// </summary>
        public int nChartNo;
        /// <summary>
        /// Chart 명
        /// </summary>
        public string ChartName;
        /// <summary>
        /// P, NP, C, U ChartType 변경 유부 구분
        /// </summary>
        public bool isPcChartType;
    }

    /// <summary>
    /// Spc Chart ShowWaitArea Parameter
    /// </summary>
    public class SpcShowWaitAreaOption
    {
        /// <summary>
        /// Waite 구분: true-실행, false-정지
        /// </summary>
        public bool CheckValue;
        public bool ChartTypeChange;
    }

    /// <summary>
    /// SPC 이벤트 Option 
    /// </summary>
    public class SpcEventsChartMessage
    {
        /// <summary>
        /// 메인 메세지 입력
        /// </summary>
        public string mainMessage;
    }

    #region Rules
    /// <summary>
    /// SPC Rules Check
    /// </summary>
    public class RulesCheck
    {
        /// <summary>
        /// 룰 Check 입력 자료
        /// </summary>
        public List<RulesPara> lstRulesPara;
        /// <summary>
        /// 룰 Check 결과 자료.
        /// </summary>
        public List<RuleCheckResult> lstRuleCheckResult;
        //public List<RulesPara> rulesResult;
        /// <summary>
        /// 검증 X 축 Point 전체 건수.
        /// </summary>
        public int nPointMaxCount;

        public Color Rule01Color;
        public Color Rule02Color;
        public Color Rule03Color;
        public Color Rule04Color;
        public Color Rule05Color;
        public Color Rule06Color;
        public Color Rule07Color;
        public Color Rule08Color;
        public Color Rule09Color;
        public Color Rule10Color;

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static RulesCheck Create()
        {
            RulesCheck cr = new RulesCheck();
            cr.lstRulesPara = new List<RulesPara>();
            cr.lstRuleCheckResult = new List<RuleCheckResult>();
            cr.nPointMaxCount = 0;
            cr.Rule01Color = Color.DarkCyan;
            cr.Rule02Color = Color.DarkViolet;
            cr.Rule03Color = Color.DarkSalmon;
            cr.Rule04Color = Color.DarkSeaGreen;
            cr.Rule05Color = Color.DarkBlue;
            cr.Rule06Color = Color.DarkOrange;
            cr.Rule07Color = Color.DarkKhaki;
            cr.Rule08Color = Color.DarkGreen;
            cr.Rule09Color = Color.DarkGoldenrod;
            cr.Rule10Color = Color.DarkSlateGray;
            return cr;
        }


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public RulesCheck CopyDeep()
        {
            RulesCheck cr = new RulesCheck();
            
            foreach (RulesPara item in this.lstRulesPara)
            {
                cr.lstRulesPara.Add(item.CopyDeep());
            }

            foreach (RuleCheckResult item in this.lstRuleCheckResult)
            {
                cr.lstRuleCheckResult.Add(item.CopyDeep());
            }
            
            cr.nPointMaxCount = this.nPointMaxCount;
            cr.Rule01Color = this.Rule01Color;
            cr.Rule02Color = this.Rule02Color;
            cr.Rule03Color = this.Rule03Color;
            cr.Rule04Color = this.Rule04Color;
            cr.Rule05Color = this.Rule05Color;
            cr.Rule06Color = this.Rule06Color;
            cr.Rule07Color = this.Rule07Color;
            cr.Rule08Color = this.Rule08Color;
            cr.Rule09Color = this.Rule09Color;
            cr.Rule10Color = this.Rule10Color;
            return cr;
        }
    }

    /// <summary>
    /// Rule Check 구분값.
    /// </summary>
    public class RulesPara
    {
        /// <summary>
        /// 룰번호
        /// </summary>
        public int ruleNo;
        /// <summary>
        /// 룰상태
        /// </summary>
        public string status;
        /// <summary>
        /// 설명
        /// </summary>
        public string messageDiscription;
        /// <summary>
        /// 해설
        /// </summary>
        public string messageComment;

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static RulesPara Create()
        {
            RulesPara cr = new RulesPara();
            cr.ruleNo = 0;
            cr.status = "";
            cr.messageDiscription = "";
            cr.messageComment = "";
            return cr;
        }


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public RulesPara CopyDeep()
        {
            RulesPara cr = new RulesPara();
            cr.ruleNo = this.ruleNo;
            cr.status = this.status;
            cr.messageDiscription = this.messageDiscription;
            cr.messageComment = this.messageComment;
            return cr;
        }
    }

    /// <summary>
    /// Rule Check 결과 반환 값 저장.
    /// </summary>
    public class RuleCheckResult
    {
        /// <summary>
        /// 룰 번호.
        /// </summary>
        public int RuleNo;
        /// <summary>
        /// 룰 Over Check : true - Over 자료 있음.
        /// </summary>
        public bool isRuleOver;
        ///// <summary>
        ///// 관리도 분석 DataTable
        ///// </summary>
        //// RtnControlDataTable staData;
        ///
        /// <summary>
        /// Points 정의 
        /// </summary>
        public List<RuleCheckSerialsPoint> listPoint;

        /// <summary>
        /// 시작 ID
        /// </summary>
        public int nStartPoint;
        /// <summary>
        /// 종료 ID
        /// </summary>
        public int nEndPoint;

        /// <summary>
        /// 시작 ID Sub 개수 1번
        /// </summary>
        public int nStartPoint01;
        /// <summary>
        /// 종료 ID Sub 개수 1번
        /// </summary>
        public int nEndPoint01;

        /// <summary>
        /// Rule Checkt 개수
        /// </summary>
        public int nRuleCheckCount;
        /// <summary>
        /// Rule Checkt Sub 개수 1번
        /// </summary>
        public int nRuleCheckCount01;
        /// <summary>
        /// Chart Point Color
        /// </summary>
        public Color RuleColor;
        /// <summary>
        /// 상태 0-정상, 0값 이상 -> 룰 분석중 오류 발생.
        /// </summary>
        public int status;
        /// <summary>
        /// 메세지
        /// </summary>
        public string message;
        /// <summary>
        /// 설명
        /// </summary>
        public string discription;
        /// <summary>
        /// 해설
        /// </summary>
        public string comment;

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static RuleCheckResult Create()
        {
            RuleCheckResult cr = new RuleCheckResult();
            //cr.staData = new RtnControlDataTable();
            cr.RuleNo = 0;
            cr.isRuleOver = false;
            cr.listPoint = new List<RuleCheckSerialsPoint>();
            cr.nStartPoint = 0;
            cr.nEndPoint = 0;
            cr.nStartPoint01 = 0;
            cr.nEndPoint01 = 0;

            cr.nRuleCheckCount = 0;
            cr.nRuleCheckCount01 = 0;

            cr.RuleColor = Color.DarkViolet;

            cr.status = -1;
            cr.message = "";
            return cr;
        }

        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public RuleCheckResult CopyDeep()
        {
            RuleCheckResult cr = new RuleCheckResult();
            //cr.listPoint = new List<RuleCheckSerialsPoint>();
            if(this.listPoint !=null)
            {
                foreach (RuleCheckSerialsPoint item in this.listPoint)
                {
                    cr.listPoint.Add(item.CopyDeep());
                }
            }
            cr.RuleNo = this.RuleNo;
            cr.isRuleOver = this.isRuleOver;
            cr.nStartPoint = this.nStartPoint;
            cr.nEndPoint = this.nEndPoint;
            cr.nStartPoint01 = this.nStartPoint01;
            cr.nEndPoint01 = this.nEndPoint01;

            cr.nRuleCheckCount = this.nRuleCheckCount;
            cr.nRuleCheckCount01 = this.nRuleCheckCount01;

            cr.RuleColor = this.RuleColor;

            cr.status = this.status;
            cr.message = this.message;
            return cr;
        }
    }
    /// <summary>
    /// 룰 Check 자료의 사작, 종료 index 및 자료 저장.
    /// </summary>
    public class RuleCheckSerialsPoint
    {
        /// <summary>
        /// 시작 ID
        /// </summary>
        public int nStartPoint;
        /// <summary>
        /// 종료 ID
        /// </summary>
        public int nEndPoint;

        //public List<RuleCheckSerialsPoint> listPoint;
        public List<RuleCheckSerialsPointData> listPointData;
        public static RuleCheckSerialsPoint Create()
        {
            RuleCheckSerialsPoint cr = new RuleCheckSerialsPoint();
            cr.nStartPoint = 0;
            cr.nEndPoint = 0;
            cr.listPointData = new List<RuleCheckSerialsPointData>();
            return cr;
        }

        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public RuleCheckSerialsPoint CopyDeep()
        {
            RuleCheckSerialsPoint cr = new RuleCheckSerialsPoint();
            cr.nStartPoint = this.nStartPoint;
            cr.nEndPoint = this.nEndPoint;
            foreach (RuleCheckSerialsPointData item in this.listPointData)
            {
                cr.listPointData.Add(item.CopyDeep());
            }
            return cr;
        }

    }

    /// <summary>
    /// 룰 Check Point Data 저장.
    /// </summary>
    public class RuleCheckSerialsPointData
    {
        public int nindex;
        public double nCheckValue;
        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static RuleCheckSerialsPointData Create()
        {
            RuleCheckSerialsPointData cr = new RuleCheckSerialsPointData();
            cr.nindex = 0;
            cr.nCheckValue = 0;
            return cr;
        }

        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public RuleCheckSerialsPointData CopyDeep()
        {
            RuleCheckSerialsPointData cr = new RuleCheckSerialsPointData();
            cr.nindex = this.nindex;
            cr.nCheckValue = this.nCheckValue;
            return cr;
        }
    }
    #endregion Rules

    #region Chart Message
    /// <summary>
    /// SPC Chart 공통 Message
    /// </summary>
    public static class ChartMessage
    {
        public static bool isUse;
        public static MessageSpcRulesCheck rules;
        /// <summary>
        /// SPC Chart 공통 Message 설정.
        /// </summary>
        public static void MessageSetting()
        {
            try
            {
                if (!isUse)
                {
                    //초기화.
                    SpcLibMessage.common = new MessageSpcCommon();
                    rules = new MessageSpcRulesCheck();

                    //Test Mode
                    if (SpcFlag.isTestMode == 0)
                    {
                        MessageSettingRealData();
                    }
                    else
                    {
                        MessageSettingTestData();
                    }

                    isUse = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// 다국어 사용 Data 입력.
        /// </summary>
        public static void MessageSettingRealData()
        {
            //Message 읽기.
            Language.LanguageMessageItem item;

            #region Spc Library 공통 
            //공통
            item = Language.GetMessage("SPCCOMMON1001"); SpcLibMessage.common.com1001 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMON1002"); SpcLibMessage.common.com1002 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMON1003"); SpcLibMessage.common.com1003 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMON1004"); SpcLibMessage.common.com1004 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMON1005"); SpcLibMessage.common.com1005 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMON1006"); SpcLibMessage.common.com1006 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMON1007"); SpcLibMessage.common.com1007 = item.Message ?? item.ItemId;//Save an Chart image

            //공통-Tab
            item = Language.GetMessage("SPCCOMMON1011"); SpcLibMessage.common.com1011 = item.Message ?? item.ItemId;//관리도
            item = Language.GetMessage("SPCCOMMON1012"); SpcLibMessage.common.com1012 = item.Message ?? item.ItemId;//공정능력
            item = Language.GetMessage("SPCCOMMON1013"); SpcLibMessage.common.com1013 = item.Message ?? item.ItemId;//분석
            item = Language.GetMessage("SPCCOMMON1014"); SpcLibMessage.common.com1014 = item.Message ?? item.ItemId;//Raw Data
            item = Language.GetMessage("SPCCOMMON1015"); SpcLibMessage.common.com1015 = item.Message ?? item.ItemId;//Over Rules
            
            //공통-Popup Menu
            item = Language.GetMessage("SPCCOMMON2101"); SpcLibMessage.common.com2101 = item.Message ?? item.ItemId;//Image 복사 - Popup Menu 
            item = Language.GetMessage("SPCCOMMON2102"); SpcLibMessage.common.com2102 = item.Message ?? item.ItemId;//Image 저장 - Popup Menu 

            //공통-통계 분석 
            item = Language.GetMessage("SPCCOMMONCPK1001"); SpcLibMessage.common.comCpk1001 = item.Message ?? item.ItemId;

            //공통-통계 분석 
            item = Language.GetMessage("SPCCOMMONCPK1011"); SpcLibMessage.common.comCpk1011 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1012"); SpcLibMessage.common.comCpk1012 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1013"); SpcLibMessage.common.comCpk1013 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1014"); SpcLibMessage.common.comCpk1014 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1015"); SpcLibMessage.common.comCpk1015 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1016"); SpcLibMessage.common.comCpk1016 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1017"); SpcLibMessage.common.comCpk1017 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1018"); SpcLibMessage.common.comCpk1018 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1019"); SpcLibMessage.common.comCpk1019 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1020"); SpcLibMessage.common.comCpk1020 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1021"); SpcLibMessage.common.comCpk1021 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1022"); SpcLibMessage.common.comCpk1022 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1023"); SpcLibMessage.common.comCpk1023 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1024"); SpcLibMessage.common.comCpk1024 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1025"); SpcLibMessage.common.comCpk1025 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1026"); SpcLibMessage.common.comCpk1026 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1027"); SpcLibMessage.common.comCpk1027 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1028"); SpcLibMessage.common.comCpk1028 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1029"); SpcLibMessage.common.comCpk1029 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1030"); SpcLibMessage.common.comCpk1030 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1031"); SpcLibMessage.common.comCpk1031 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1032"); SpcLibMessage.common.comCpk1032 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1033"); SpcLibMessage.common.comCpk1033 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1034"); SpcLibMessage.common.comCpk1034 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1035"); SpcLibMessage.common.comCpk1035 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1036"); SpcLibMessage.common.comCpk1036 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1037"); SpcLibMessage.common.comCpk1037 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1038"); SpcLibMessage.common.comCpk1038 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1039"); SpcLibMessage.common.comCpk1039 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1040"); SpcLibMessage.common.comCpk1040 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1041"); SpcLibMessage.common.comCpk1041 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1042"); SpcLibMessage.common.comCpk1042 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1043"); SpcLibMessage.common.comCpk1043 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1044"); SpcLibMessage.common.comCpk1044 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1045"); SpcLibMessage.common.comCpk1045 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1046"); SpcLibMessage.common.comCpk1046 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1047"); SpcLibMessage.common.comCpk1047 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONCPK1048"); SpcLibMessage.common.comCpk1048 = item.Message ?? item.ItemId;
            #endregion Spc Library 공통 

            #region Rules Check
            item = Language.GetMessage("SPCRULECHECKMESSAGE1000"); rules.chk1000 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCRULECHECKMESSAGE1001"); rules.chk1001 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCRULECHECKMESSAGE1002"); rules.chk1002 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCRULECHECKMESSAGE1003"); rules.chk1003 = item.Message ?? item.ItemId;

            item = Language.GetMessage("SPCRULECHECKMESSAGE1011"); rules.chk1011 = item.Message ?? item.ItemId;


            item = Language.GetMessage("SPCCOMMONRULE1001"); SpcLibMessage.common.rule1001 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONRULE1002"); SpcLibMessage.common.rule1002 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONRULE1003"); SpcLibMessage.common.rule1003 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONRULE1004"); SpcLibMessage.common.rule1004 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONRULE1005"); SpcLibMessage.common.rule1005 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONRULE1006"); SpcLibMessage.common.rule1006 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONRULE1007"); SpcLibMessage.common.rule1007 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONRULE1008"); SpcLibMessage.common.rule1008 = item.Message ?? item.ItemId;
            item = Language.GetMessage("SPCCOMMONRULE1009"); SpcLibMessage.common.rule1009 = item.Message ?? item.ItemId;
            #endregion Rules Check
        }

        /// <summary>
        /// Test Data
        /// </summary>
        public static void MessageSettingTestData()
        {
            string defaultMessage = "Test-";

            #region Spc Library 공통 
            SpcLibMessage.common.com1001 = string.Format("{0}분석 자료가 없습니다.", defaultMessage);
            SpcLibMessage.common.com1002 = string.Format("{0}분석 입력자료 읽기.", defaultMessage);
            SpcLibMessage.common.com1003 = string.Format("{0}공정능력 SPEC 확인.", defaultMessage);
            SpcLibMessage.common.com1004 = string.Format("{0}XX", defaultMessage);
            SpcLibMessage.common.com1005 = string.Format("{0}XX", defaultMessage);
            SpcLibMessage.common.com1006 = string.Format("{0}추정치", defaultMessage);
            SpcLibMessage.common.com1007 = string.Format("{0}Save an Chart image", defaultMessage);

            SpcLibMessage.common.com1011 = string.Format("{0}관리도", defaultMessage);
            SpcLibMessage.common.com1012 = string.Format("{0}공정능력", defaultMessage);
            SpcLibMessage.common.com1013 = string.Format("{0}분석", defaultMessage);
            SpcLibMessage.common.com1014 = string.Format("{0}Raw Data", defaultMessage);
            SpcLibMessage.common.com1015 = string.Format("{0}Over Rules", defaultMessage);

            SpcLibMessage.common.com1006 = string.Format("{0}Image 복사", defaultMessage);
            SpcLibMessage.common.com1006 = string.Format("{0}Image 저장", defaultMessage);


            SpcLibMessage.common.comCpk1001 = string.Format("{0}공정능력 분석 함수 실행. [SpcLibPpkCbMuti]", defaultMessage);

            SpcLibMessage.common.comCpk1011 = string.Format("{0}공정능력 분석용 USL, LSL 값이 없습니다.", defaultMessage);
            SpcLibMessage.common.comCpk1012 = string.Format("{0}한개 이상 SPEC이 있어야 합니다.", defaultMessage);
            SpcLibMessage.common.comCpk1013 = string.Format("{0}LSL SPEC이 없습니다.", defaultMessage);
            SpcLibMessage.common.comCpk1014 = string.Format("{0}USL SPEC이 없습니다.", defaultMessage);
            SpcLibMessage.common.comCpk1015 = string.Format("{0}LSL, USL SPEC이 모두 없습니다.", defaultMessage);
            SpcLibMessage.common.comCpk1016 = string.Format("{0}그러므로 공정능력을 계산 할 수 없습니다.", defaultMessage);
            SpcLibMessage.common.comCpk1017 = string.Format("{0}입력 자료가 없습니다.", defaultMessage);
            SpcLibMessage.common.comCpk1018 = string.Format("{0}변동부족: Sampling Count가 1개 입니다.", defaultMessage);
            SpcLibMessage.common.comCpk1019 = string.Format("{0}값의 변동이 없을시: SubGroup의 값이 전부 같으면 계산하지 않음(변동이 없음)", defaultMessage);
            SpcLibMessage.common.comCpk1020 = string.Format("{0}중심치 이탈", defaultMessage);
            SpcLibMessage.common.comCpk1021 = string.Format("{0}중심치 이탈(%)", defaultMessage);
            SpcLibMessage.common.comCpk1022 = string.Format("{0}공정능력 SPEC Control이 정의되지 않았습니다. [controlSpec]", defaultMessage);
            SpcLibMessage.common.comCpk1023 = string.Format("{0}공정능력 Sigma 결과 값이 정의되지 않았습니다. [controlSpec.sigmaResult]", defaultMessage);
            SpcLibMessage.common.comCpk1024 = string.Format("{0}공정능력 내부 Sigma 값이 정의되지 않았습니다. [controlSpec.sigmaResult.nSigmaWithin]", defaultMessage);
            SpcLibMessage.common.comCpk1025 = string.Format("{0}공정능력 전체 Sigma 값이 정의되지 않았습니다. [controlSpec.sigmaResult.nSigmaTotal]", defaultMessage);
            SpcLibMessage.common.comCpk1026 = string.Format("{0}Histogram 간격 폭 계산 할 수 없습니다.", defaultMessage);
            SpcLibMessage.common.comCpk1027 = string.Format("{0}Histogram조회한 자료가 없습니다. 우측의 조회버튼을 Click 하시길 바랍니다.", defaultMessage);
            SpcLibMessage.common.comCpk1028 = string.Format("{0}측정값이 1개 임으로 표준편차를 계산 할 수 없습니다.", defaultMessage);
            SpcLibMessage.common.comCpk1029 = string.Format("{0}상태 Message:", defaultMessage);
            SpcLibMessage.common.comCpk1030 = string.Format("{0}분석 Message:", defaultMessage);
            SpcLibMessage.common.comCpk1031 = string.Format("{0}해석용", defaultMessage);
            SpcLibMessage.common.comCpk1032 = string.Format("{0}Over Rule", defaultMessage);
            SpcLibMessage.common.comCpk1033 = string.Format("{0}값", defaultMessage);
            SpcLibMessage.common.comCpk1034 = string.Format("{0}asixs 라벨:", defaultMessage);
            SpcLibMessage.common.comCpk1035 = string.Format("{0}추정치 사용", defaultMessage);
            SpcLibMessage.common.comCpk1036 = string.Format("{0}추정치 미사용", defaultMessage);
            SpcLibMessage.common.comCpk1037 = string.Format("{0}추정치 미선택", defaultMessage);
            SpcLibMessage.common.comCpk1038 = string.Format("{0}같지 않음", defaultMessage);
            SpcLibMessage.common.comCpk1039 = string.Format("{0}Subgroup별 Sampling 수가 같지 않음.", defaultMessage);//Tooltip
            SpcLibMessage.common.comCpk1040 = string.Format("{0}관리용", defaultMessage);
            SpcLibMessage.common.comCpk1041 = string.Format("{0}직접입력", defaultMessage);
            SpcLibMessage.common.comCpk1042 = string.Format("{0}SBar 합동", defaultMessage);
            SpcLibMessage.common.comCpk1043 = string.Format("{0}재분석", defaultMessage);
            SpcLibMessage.common.comCpk1044 = string.Format("{0}같음", defaultMessage);

            SpcLibMessage.common.comCpk1045 = string.Format("{0}Nelson rules", defaultMessage);
            SpcLibMessage.common.comCpk1046 = string.Format("{0}Western Electric rules", defaultMessage);
            SpcLibMessage.common.comCpk1047 = string.Format("{0}Rules Setting - Nelson rules", defaultMessage);
            SpcLibMessage.common.comCpk1048 = string.Format("{0}Rules Setting - Western Electric rules", defaultMessage);

            //[Sample 코드]
            //SpcLibMessage.common.comCpk10XX = string.Format("{0}XXX", defaultMessage);




            #endregion Spc Library 공통


            #region Rules Check
            rules.chk1000 = string.Format("{0}정상 검증.", defaultMessage);
            rules.chk1001 = string.Format("{0}입력 자료가 없습니다.", defaultMessage);
            rules.chk1002 = string.Format("{0}UCL 3 Sigmar가 없습니다.", defaultMessage);
            rules.chk1003 = string.Format("{0}LCL 3 Sigmar가 없습니다.", defaultMessage);

            rules.chk1011 = string.Format("{0}Rule Over 발생.", defaultMessage);


            // SpcLibMessage.common.rule1001 = string.Format("{0}XX", defaultMessage);

            SpcLibMessage.common.rule1001 = string.Format("{0}입력 자료가 없습니다.", defaultMessage);
            SpcLibMessage.common.rule1002 = string.Format("{0}UCL 3 Sigma가 없습니다.", defaultMessage);
            SpcLibMessage.common.rule1003 = string.Format("{0}LCL 3 Sigma가 없습니다.", defaultMessage);
            SpcLibMessage.common.rule1004 = string.Format("{0}Sigma가 없습니다.", defaultMessage);
            SpcLibMessage.common.rule1005 = string.Format("{0}UCL 2 Sigma가 없습니다.", defaultMessage);
            SpcLibMessage.common.rule1006 = string.Format("{0}LCL 2 Sigma가 없습니다.", defaultMessage);
            SpcLibMessage.common.rule1007 = string.Format("{0}UCL 1 Sigma가 없습니다.", defaultMessage);
            SpcLibMessage.common.rule1008 = string.Format("{0}LCL 1 Sigma가 없습니다.", defaultMessage);
            SpcLibMessage.common.rule1009 = string.Format("{0}평균 값이 없습니다.", defaultMessage);

            #endregion Rules Check
        }
    }



    /// <summary>
    /// SPC Rules Check Message
    /// </summary>
    public class MessageSpcRulesCheck
    {
        public string chk0999;
        /// <summary>
        /// 정상검증
        /// </summary>
        public string chk1000;
        /// <summary>
        /// 입력 자료가 없습니다.
        /// </summary>
        public string chk1001;
        /// <summary>
        /// UCL 3 Sigma가 없습니다.
        /// </summary>
        public string chk1002;
        /// <summary>
        /// LCL 3 Sigma가 없습니다.
        /// </summary>
        public string chk1003;
        /// <summary>
        /// Rule Over 발생.
        /// </summary>
        public string chk1011;
    }
    #endregion Chart Message

    /// <summary>
    /// SPC 조회조건 일자 List
    /// </summary>
    public class SpcConditionDateDays
    {
        public string DateStart;
        public string DateEnd;
    }


}
