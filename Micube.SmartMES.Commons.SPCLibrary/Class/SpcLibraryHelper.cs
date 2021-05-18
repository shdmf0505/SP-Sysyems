#region using

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// SPC Library 함수 정의
    /// </summary>
    public class SpcLibraryHelper
    {
        /// <summary>
        /// Spc Chart Type별 Rules Check
        /// </summary>
        /// <param name="spcRules"></param>
        /// <param name="messageOption
        ///  1-Spec, 2-Control, 3-Spec & Control, 4-All
        /// "></param>
        /// <returns></returns>
        public SpcRulesOver SpcSpecRuleCheck(SpcRules spcRules, int messageOption = 1)
        {
            SpcRulesOver returnRulesOver = new SpcRulesOver();

            SpcSpec spec = new SpcSpec();

            switch (spcRules.defaultChartType)
            {
                case SpcChartType.xbarr:
                    spec = spcRules.xbarr;
                    break;
                case SpcChartType.r:
                    spec = spcRules.r;
                    break;
                case SpcChartType.xbars:
                    spec = spcRules.xbars;
                    break;
                case SpcChartType.s:
                    spec = spcRules.s;
                    break;
                case SpcChartType.np:
                    spec = spcRules.np;
                    break;
                case SpcChartType.p:
                    spec = spcRules.p;
                    break;
                case SpcChartType.c:
                    spec = spcRules.c;
                    break;
                case SpcChartType.u:
                    spec = spcRules.u;
                    break;
                default:
                    spec = spcRules.xbarr;
                    break;
            }

            spec.value = spcRules.nValue;
            returnRulesOver = SpecCheck(spec, messageOption);

            return returnRulesOver;
        }

        /// <summary>
        /// Spc Spec Rules Check
        /// </summary>
        /// <param name="nSpec"></param>
        /// <param name="messageOption
        ///  1-Spec, 2-Control, 3-Spec & Control, 4-All
        /// "></param>
        /// <returns></returns>
        public SpcRulesOver SpecCheck(SpcSpec nSpec, int messageOption = 1)
        {
            SpcRulesOver returnValue = SpcRulesOver.Create();

            //Outlier
            if (nSpec.value > nSpec.uol)
            {
                returnValue.isUOL = true;
                returnValue.isOutlier = true;
                returnValue.isResult = true;
            }

            if (nSpec.value < nSpec.lol)
            {
                returnValue.isLOL = true;
                returnValue.isOutlier = true;
                returnValue.isResult = true;
            }

            //Spec
            if (nSpec.value > nSpec.usl)
            {
                returnValue.isUSL = true;
                returnValue.isSpec = true;
                returnValue.isResult = true;
            }

            if (nSpec.value < nSpec.lsl)
            {
                returnValue.isLSL = true;
                returnValue.isSpec = true;
                returnValue.isResult = true;
            }

            //ControlLimit
            if (nSpec.value > nSpec.ucl)
            {
                returnValue.isUCL = true;
                returnValue.isControlLimit = true;
                returnValue.isResult = true;
            }

            if (nSpec.value < nSpec.lcl)
            {
                returnValue.isLCL = true;
                returnValue.isControlLimit = true;
                returnValue.isResult = true;
            }

            //Message 처리
            switch (messageOption)
            {
                case 1://Spec Message
                    //상한 값 Message 처리
                    if (returnValue.isUSL == true)
                    {
                        returnValue.message.code = "03";
                        returnValue.message.line1 = "USL 초과";
                        returnValue.message.line2 = string.Format("값: {0}, USL: {1}", nSpec.value, nSpec.usl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }

                    //하한 값 Message 처리
                    if (returnValue.isLSL == true)
                    {
                        returnValue.message.code = "13";
                        returnValue.message.line1 = "LSL 미만";
                        returnValue.message.line2 = string.Format("값: {0}, LSL: {1}", nSpec.value, nSpec.lsl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }

                    if (returnValue.isResult == true)
                    {
                        returnValue.message.value = string.Format("{0}{3}{3}{1}{3}{3}{3}{2}"
                            , returnValue.message.line1, returnValue.message.line2, returnValue.message.line3, Environment.NewLine);

                    }
                    break;
                case 2://Control Message
                    //상한 값 Message 처리
                    if (returnValue.isUCL == true)
                    {
                        returnValue.message.code = "03";
                        returnValue.message.line1 = "UCL 초과";
                        returnValue.message.line2 = string.Format("값: {0}, UCL: {1}", nSpec.value, nSpec.ucl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }

                    //하한 값 Message 처리
                    if (returnValue.isLCL == true)
                    {
                        returnValue.message.code = "14";
                        returnValue.message.line1 = "LCL 미만";
                        returnValue.message.line2 = string.Format("값: {0}, LCL: {1}", nSpec.value, nSpec.lcl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }

                    if (returnValue.isResult == true)
                    {
                        returnValue.message.value = string.Format("{0}{3}{3}{1}{3}{3}{3}{2}"
                            , returnValue.message.line1, returnValue.message.line2, returnValue.message.line3, Environment.NewLine);

                    }
                    break;
                case 3://Spec & Control
                    //상한 값 Message 처리
                    if (returnValue.isUSL == true)
                    {
                        returnValue.message.code = "03";
                        returnValue.message.line1 = "USL 초과";
                        returnValue.message.line2 = string.Format("값: {0}, USL: {1}", nSpec.value, nSpec.usl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }
                    else if (returnValue.isUCL == true)
                    {
                        returnValue.message.code = "03";
                        returnValue.message.line1 = "UCL 초과";
                        returnValue.message.line2 = string.Format("값: {0}, UCL: {1}", nSpec.value, nSpec.ucl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }

                    //하한 값 Message 처리
                    if (returnValue.isLSL == true)
                    {
                        returnValue.message.code = "13";
                        returnValue.message.line1 = "LSL 미만";
                        returnValue.message.line2 = string.Format("값: {0}, LSL: {1}", nSpec.value, nSpec.lsl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }
                    else if (returnValue.isLCL == true)
                    {
                        returnValue.message.code = "14";
                        returnValue.message.line1 = "LCL 미만";
                        returnValue.message.line2 = string.Format("값: {0}, LCL: {1}", nSpec.value, nSpec.lcl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }

                    if (returnValue.isResult == true)
                    {
                        returnValue.message.value = string.Format("{0}{3}{3}{1}{3}{3}{3}{2}"
                            , returnValue.message.line1, returnValue.message.line2, returnValue.message.line3, Environment.NewLine);

                    }
                    break;
                case 4:
                default:
                    //상한 값 Message 처리
                    if (returnValue.isUSL == true && returnValue.isUCL == true && returnValue.isUOL == true)
                    {
                        returnValue.message.code = "01";
                        returnValue.message.line1 = "USL, UCL, UOL 초과";
                        returnValue.message.line2 = string.Format("값: {0}, USL: {1}, UCL: {2}, UOL: {3}", nSpec.value, nSpec.usl, nSpec.ucl, nSpec.uol);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }
                    else if (returnValue.isUSL == true && returnValue.isUCL == true)
                    {
                        returnValue.message.code = "02";
                        returnValue.message.line1 = "USL, UCL 초과";
                        returnValue.message.line2 = string.Format("값: {0}, USL: {1}, UCL: {2}", nSpec.value, nSpec.usl, nSpec.ucl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }
                    else if (returnValue.isUSL == true)
                    {
                        returnValue.message.code = "03";
                        returnValue.message.line1 = "USL 초과";
                        returnValue.message.line2 = string.Format("값: {0}, USL: {1}", nSpec.value, nSpec.usl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }
                    else if (returnValue.isUCL == true)
                    {
                        returnValue.message.code = "03";
                        returnValue.message.line1 = "UCL 초과";
                        returnValue.message.line2 = string.Format("값: {0}, UCL: {1}", nSpec.value, nSpec.ucl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }

                    //하한 값 Message 처리
                    if (returnValue.isLSL == true && returnValue.isLCL == true && returnValue.isLOL == true)
                    {
                        returnValue.message.code = "11";
                        returnValue.message.line1 = "LSL, LCL, LOL 미만";
                        returnValue.message.line2 = string.Format("값: {0}, LSL: {1}, LCL: {2}, LOL: {3}", nSpec.value, nSpec.lsl, nSpec.lcl, nSpec.lol);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }
                    else if (returnValue.isLSL == true && returnValue.isLCL == true)
                    {
                        returnValue.message.code = "12";
                        returnValue.message.line1 = "LSL, LCL 미만";
                        returnValue.message.line2 = string.Format("값: {0}, LSL: {1}, LCL: {2}", nSpec.value, nSpec.lsl, nSpec.lcl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }
                    else if (returnValue.isLSL == true)
                    {
                        returnValue.message.code = "13";
                        returnValue.message.line1 = "LSL 미만";
                        returnValue.message.line2 = string.Format("값: {0}, LSL: {1}", nSpec.value, nSpec.lsl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }
                    else if (returnValue.isLCL == true)
                    {
                        returnValue.message.code = "14";
                        returnValue.message.line1 = "LCL 미만";
                        returnValue.message.line2 = string.Format("값: {0}, LCL: {1}", nSpec.value, nSpec.lcl);
                        returnValue.message.line3 = string.Format("위와 같이 Rule over가 발생 했습니다.");
                    }

                    if (returnValue.isResult == true)
                    {
                        returnValue.message.value = string.Format("{0}{3}{3}{1}{3}{3}{3}{2}"
                            , returnValue.message.line1, returnValue.message.line2, returnValue.message.line3, Environment.NewLine);

                    }
                    break;
            }

            return returnValue;
        }

        #region 불량률 통계 구성. 10/10
        /// <summary>
        /// 불량 건수, 률 처리 상세 함수.
        /// </summary>
        /// <param name="spcPara"></param>
        /// <param name="dtDefactTotal"></param>
        /// <param name="statusMessage"></param>
        /// <returns></returns>
        public DataTable DefectRateList(SPCPara spcPara, out DataTable dtDefactTotal, out DataTable dtDefactDetail, out string[] gridCaption, out string statusMessage, string subgroupValue = "SUBGROUPNAME")
        {
            DataTable dtDefactDetailView;
            int idSeq = 0;
            string checkMonth = "";
            gridCaption = new string[32];
            statusMessage = "";
            dtDefactTotal = CreateDataTableDefectTotal();
            dtDefactDetail = CreateDataTableDefectRate();
            dtDefactDetailView = CreateDataTableDefectRateView();

            if (spcPara == null || spcPara.InputData == null || spcPara.InputData.Count <= 0)
            {
                statusMessage = SpcLibMessage.common.comCpk1017;//입력 자료가 없습니다.
                return dtDefactDetailView;
            }

            var dbPara = spcPara.InputData.AsEnumerable();
            //var dbPara = spcPara

            //불량 전체 건수 
            try
            {
                var totDefect = from b in dbPara.AsParallel() //where b.NVALUE !=1
                                group b by b.Field<string>(subgroupValue) into g
                                //group b by new {b.SUBGROUP}into g
                                select new
                                {
                                    vSUBGROUP = g.Key,
                                    vTOTSUM = g.Sum(s => s.Field<double>("NVALUE")),
                                    vTOTSUMSUB = g.Sum(s => s.Field<double>("NSUBVALUE")),
                                    vTOTCOUNT = g.Count()
                                };
                foreach (var f in totDefect)
                {
                    idSeq++;
                    //DataRow[] rows = tbPPIDataAvg.Select("SUBGROUP = '" + f.vSUBGROUP + "' ");
                    DataRow drData = dtDefactTotal.NewRow();
                    drData["SEQID"] = idSeq;
                    drData["DEFECTCODE"] = f.vSUBGROUP;
                    drData["DEFECTTOTAL"] = f.vTOTSUM;
                    drData["INPUTTOTAL"] = f.vTOTSUMSUB;
                    dtDefactTotal.Rows.Add(drData);
                }
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
                Console.WriteLine(ex.Message);
            }

            //불량 율 계산
            int i;
            int j;
            int nDayStartPoint = 9;//9에서 일자 시작.
            int nDayEndPoint = 40;//9에서 일자 시작.
            string rateFieldDay = "";
            string defectData = "";
            string defectDateTime = "";
            string defectValueType = "";
            double nDefectTotValue;
            double nInputTotValue;
            double nTotValue;
            string viewTotValue = "";

            double nDefectValue;
            double nDefectInspectionValue;
            double nDefectRate;
            double nDefectRateRound;
            int nValueTypeMax = 4;
            try
            {
                for (i = 0; i < dtDefactTotal.Rows.Count; i++)
                {
                    DataRow drTot = dtDefactTotal.Rows[i];
                    defectData = drTot["DEFECTCODE"].ToSafeString();
                    nInputTotValue = drTot["INPUTTOTAL"].ToSafeDoubleStaMin();
                    nDefectTotValue = drTot["DEFECTTOTAL"].ToSafeDoubleStaMin();

                    idSeq++;
                    for (j = 0; j < nValueTypeMax; j++)
                    {
                        switch (j)
                        {
                            case 0://Value
                                defectValueType = "value";
                                nTotValue = nDefectTotValue;
                                viewTotValue = string.Format("{0:n0}", nTotValue);
                                break;
                            case 1://Rate
                                defectValueType = "rate";
                                nTotValue = Math.Round((nDefectTotValue / nInputTotValue) * 100, 3);
                                //nTotValue = 100;
                                viewTotValue = string.Format("{0}%", nTotValue);
                                break;
                            case 2://Value & Rate
                                defectValueType = "valueAndRate";
                                nTotValue = Math.Round((nDefectTotValue / nInputTotValue) * 100, 3);
                                //nTotValue = 100;
                                viewTotValue = string.Format("{0:n0}({1}%)", nDefectTotValue, nTotValue);
                                break;
                            case 3://투입수량
                                defectValueType = "input";
                                nTotValue = nInputTotValue;
                                viewTotValue = string.Format("{0:n0}", nTotValue);
                                break;
                            default:
                                defectValueType = "none";
                                nTotValue = nDefectTotValue;
                                viewTotValue = string.Format("{0:n0}", nTotValue);
                                break;
                        }

                        DataRow drDataDbl = dtDefactDetail.NewRow();
                        DataRow drData = dtDefactDetailView.NewRow();

                        if (subgroupValue == "SUBGROUPNAME")
                        {
                           var defectRate = from b in dbPara.AsParallel()
                                         where b.Field<string>(subgroupValue) == defectData
                                         group b by new
                                         {
                                             b.SUBGROUPNAME,
                                             b.SAMPLINGNAME01
                                         }
                                             into g
                                         select new
                                         {
                                             vSUBGROUP = g.Key.SUBGROUPNAME,
                                             vSAMPLING = g.Key.SAMPLINGNAME01,
                                             vNVALUE = g.Sum(s => s.NVALUE),
                                             vNSUBVALUE = g.Sum(s => s.NSUBVALUE),
                                             vCOUNT = g.Count()
                                         };
                            foreach (var f in defectRate)
                            {

                                //DataRow[] rows = tbPPIDataAvg.Select("SUBGROUP = '" + f.vSUBGROUP + "' ");
                                //계산용
                                drDataDbl["SEQID"] = idSeq;
                                drDataDbl["GROUPID"] = "DEFECT";
                                drDataDbl["DEFECTCODE"] = f.vSUBGROUP;
                                drDataDbl["DEFECTNAME"] = f.vSUBGROUP;
                                drDataDbl["VALUETYPE"] = defectValueType;
                                drDataDbl["DEFECTTOTAL"] = nDefectTotValue;
                                drDataDbl["INPUTTOTAL"] = nInputTotValue;
                                drDataDbl["TOTVALUE"] = nTotValue;
                                drDataDbl["TOTVALUEVIEW"] = viewTotValue;

                                //표시용.
                                drData["SEQID"] = idSeq;
                                drData["GROUPID"] = "DEFECT";
                                drData["DEFECTCODE"] = f.vSUBGROUP;
                                drData["DEFECTNAME"] = f.vSUBGROUP;
                                drData["VALUETYPE"] = defectValueType;
                                drData["DEFECTTOTAL"] = nDefectTotValue;
                                drData["INPUTTOTAL"] = nInputTotValue;
                                drData["TOTVALUE"] = nTotValue;
                                drData["TOTVALUEVIEW"] = viewTotValue;


                                defectDateTime = f.vSAMPLING.ToSafeString();
                                if (checkMonth == "")
                                {
                                    checkMonth = defectDateTime;
                                }
                                rateFieldDay = SpcFunction.DateToDay(defectDateTime);
                                rateFieldDay = string.Format("D{0}", rateFieldDay);
                                nDefectValue = f.vNVALUE.ToSafeDoubleStaMin();
                                nDefectInspectionValue = f.vNSUBVALUE.ToSafeDoubleStaMin();
                                //nDefectRate = (nDefectValue / nDefectTotValue) * 100;
                                nDefectRate = (nDefectValue / nInputTotValue) * 100;
                                nDefectRateRound = Math.Round(nDefectRate, 3);
                                switch (j)
                                {
                                    case 0://Value
                                        drDataDbl[rateFieldDay] = nDefectValue;
                                        drData[rateFieldDay] = string.Format("{0:n0}", nDefectValue); 
                                        break;
                                    case 1://Rate
                                        drDataDbl[rateFieldDay] = nDefectRateRound;
                                        drData[rateFieldDay] = string.Format("{0}%", nDefectRateRound.ToSafeString());
                                        break;
                                    case 2://Value & Rate
                                        drData[rateFieldDay] = string.Format("{0:n0}({1}%)", nDefectValue, nDefectRateRound.ToSafeString());
                                        break;
                                    case 3://투입수량
                                        drDataDbl["DEFECTTOTAL"] = nInputTotValue;
                                        drDataDbl[rateFieldDay] = nDefectInspectionValue;
                                        drData[rateFieldDay] = nDefectInspectionValue.ToSafeString();
                                        break;
                                    default:
                                        drData[rateFieldDay] = string.Format("Index Error : {0}", j);
                                        break;
                                }


                            }
                        }
                        else
                        {
                            var defectRate = from b in dbPara.AsParallel()
                                         where b.Field<string>(subgroupValue) == defectData
                                         group b by new
                                         {
                                             b.SUBGROUPNAME01,
                                             b.SAMPLINGNAME01
                                         }
                                             into g
                                         select new
                                         {
                                             vSUBGROUP = g.Key.SUBGROUPNAME01,
                                             vSAMPLING = g.Key.SAMPLINGNAME01,
                                             vNVALUE = g.Sum(s => s.NVALUE),
                                             vNSUBVALUE = g.Sum(s => s.NSUBVALUE),
                                             vCOUNT = g.Count()
                                         };
                            foreach (var f in defectRate)
                            {

                                //DataRow[] rows = tbPPIDataAvg.Select("SUBGROUP = '" + f.vSUBGROUP + "' ");
                                //계산용
                                drDataDbl["SEQID"] = idSeq;
                                drDataDbl["GROUPID"] = "DEFECT";
                                drDataDbl["DEFECTCODE"] = f.vSUBGROUP;
                                drDataDbl["DEFECTNAME"] = f.vSUBGROUP;
                                drDataDbl["VALUETYPE"] = defectValueType;
                                drDataDbl["DEFECTTOTAL"] = nDefectTotValue;
                                drDataDbl["INPUTTOTAL"] = nInputTotValue;
                                drDataDbl["TOTVALUE"] = nTotValue;
                                drDataDbl["TOTVALUEVIEW"] = viewTotValue;

                                //표시용
                                drData["SEQID"] = idSeq;
                                drData["GROUPID"] = "DEFECT";
                                drData["DEFECTCODE"] = f.vSUBGROUP;
                                drData["DEFECTNAME"] = f.vSUBGROUP;
                                drData["VALUETYPE"] = defectValueType;
                                drData["DEFECTTOTAL"] = nDefectTotValue;
                                drData["INPUTTOTAL"] = nInputTotValue;
                                drData["TOTVALUE"] = nTotValue;
                                drData["TOTVALUEVIEW"] = viewTotValue;

                                defectDateTime = f.vSAMPLING.ToSafeString();
                                if (checkMonth == "")
                                {
                                    checkMonth = defectDateTime;
                                }
                                rateFieldDay = SpcFunction.DateToDay(defectDateTime);
                                rateFieldDay = string.Format("D{0}", rateFieldDay);
                                nDefectValue = f.vNVALUE.ToSafeDoubleStaMin();
                                nDefectInspectionValue = f.vNSUBVALUE.ToSafeDoubleStaMin();
                                //nDefectRate = (nDefectValue / nDefectTotValue) * 100;
                                nDefectRate = (nDefectValue / nInputTotValue) * 100;
                                nDefectRateRound = Math.Round(nDefectRate, 3);
                                switch (j)
                                {
                                    case 0://Value
                                        drDataDbl[rateFieldDay] = nDefectValue;
                                        drData[rateFieldDay] = string.Format("{0:n0}", nDefectValue);
                                        break;
                                    case 1://Rate
                                        drDataDbl[rateFieldDay] = nDefectRateRound;
                                        drData[rateFieldDay] = string.Format("{0}%", nDefectRateRound.ToSafeString());
                                        break;
                                    case 2://Value & Rate
                                        drData[rateFieldDay] = string.Format("{0:n0}({1}%)", nDefectValue, nDefectRateRound.ToSafeString());
                                        break;
                                    case 3://투입수량
                                        drDataDbl["DEFECTTOTAL"] = nInputTotValue;
                                        drDataDbl[rateFieldDay] = nDefectInspectionValue;
                                        drData[rateFieldDay] = nDefectInspectionValue.ToSafeString();
                                        break;
                                    default:
                                        drData[rateFieldDay] = string.Format("Index Error : {0}", j);
                                        break;
                                }


                            }
                        }

                        //크기 조정.
                        for (int k = nDayStartPoint; k < nDayEndPoint; k++)
                        {
                            drDataDbl[k] = drDataDbl[k].ToSafeDoubleZero();
                        }
                        dtDefactDetail.Rows.Add(drDataDbl);
                        dtDefactDetailView.Rows.Add(drData);
                    }
                }

                if (checkMonth != "")
                {
                    gridCaption = SpcFunction.DayMonthList(checkMonth);
                }
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine(dtDefactDetailView.Rows.Count);
            return dtDefactDetailView;

        }


        /// <summary>
        /// 불량 List 화면 표시용 DataTable 구성.
        /// </summary>
        /// <param name="dtDefactDetail"></param>
        /// <param name="valueType"></param>
        /// <param name="statusMessage"></param>
        /// <returns></returns>
        public DataTable DefectRateView(DataTable dtDefactDetail, string valueType, out string statusMessage)
        {
            int idSeq = 0;
            DataTable dtDefactView;
            statusMessage = "";
            dtDefactView = CreateDataTableDefectRateView();
            try
            {
                idSeq = 0;

                var dtDefactViewTemp = dtDefactDetail.AsEnumerable();
                var query = dtDefactViewTemp.AsParallel()
                    .Where(w => w.Field<string>("VALUETYPE") == valueType)
                    .OrderByDescending(or => or.Field<double>("DEFECTTOTAL"));
                foreach (DataRow row in query)
                {
                    idSeq++;
                    row["SEQID"] = idSeq;
                    dtDefactView.ImportRow(row);
                }

                ////grdTrend.View.ClearColumns();
                //grdTrend.DataSource = null;
                //this.grdTrend.DataSource = dtDefactView;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return dtDefactView;
        }

        /// <summary>
        /// 불량률 총합계 계산.
        /// </summary>
        /// <param name="spcPara"></param>
        /// <param name="dtDefactTotal"></param>
        /// <param name="dtDefactDetail"></param>
        /// <param name="gridCaption"></param>
        /// <param name="statusMessage"></param>
        /// <param name="subgroupValue"></param>
        /// <returns></returns>
        public DataTable DefectRateListSum(SPCPara spcPara, out DataTable dtDefactTotal, out DataTable dtDefactDetail, out string[] gridCaption, out string statusMessage, string subgroupValue = "SUBGROUPNAME")
        {
            DataTable dtDefactDetailView;
            int idSeq = 0;
            string checkMonth = "";
            gridCaption = new string[32];
            statusMessage = "";
            dtDefactTotal = CreateDataTableDefectTotal();
            dtDefactDetail = CreateDataTableDefectRate();
            dtDefactDetailView = CreateDataTableDefectRateView();

            if (spcPara == null || spcPara.InputData == null || spcPara.InputData.Count <= 0)
            {
                statusMessage = SpcLibMessage.common.comCpk1017;//입력 자료가 없습니다.
                return dtDefactDetailView;
            }

            var dbPara = spcPara.InputData.AsEnumerable();
            //var dbPara = spcPara

            //불량 전체 건수 
            try
            {
                var totDefect = from b in dbPara.AsParallel() //where b.NVALUE !=1
                                group b by b.Field<string>(subgroupValue) into g
                                //group b by new {b.SUBGROUP}into g
                                select new
                                {
                                    vSUBGROUP = g.Key,
                                    vTOTSUM = g.Sum(s => s.Field<double>("NVALUE")),
                                    vTOTSUMSUB = g.Sum(s => s.Field<double>("NSUBVALUE")),
                                    vTOTCOUNT = g.Count()
                                };
                foreach (var f in totDefect)
                {
                    idSeq++;
                    //DataRow[] rows = tbPPIDataAvg.Select("SUBGROUP = '" + f.vSUBGROUP + "' ");
                    DataRow drData = dtDefactTotal.NewRow();
                    drData["SEQID"] = idSeq;
                    drData["DEFECTCODE"] = f.vSUBGROUP;
                    drData["DEFECTTOTAL"] = f.vTOTSUM;
                    drData["INPUTTOTAL"] = f.vTOTSUMSUB;
                    dtDefactTotal.Rows.Add(drData);
                }
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
                Console.WriteLine(ex.Message);
            }

            //불량 율 계산
            int i;
            int j;
            int nDayStartPoint = 9;//9에서 일자 시작.
            int nDayEndPoint = 40;//9에서 일자 시작.
            string rateFieldDay = "";
            string defectData = "";
            string defectDateTime = "";
            string defectValueType = "";
            double nDefectTotValue;
            double nInputTotValue;

            double nDefectValue;
            double nDefectInspectionValue;
            double nDefectRate;
            double nDefectRateRound;
            int nValueTypeMax = 4;
            try
            {
                for (i = 0; i < dtDefactTotal.Rows.Count; i++)
                {
                    DataRow drTot = dtDefactTotal.Rows[i];
                    defectData = drTot["DEFECTCODE"].ToSafeString();
                    nDefectTotValue = drTot["DEFECTTOTAL"].ToSafeDoubleStaMin();
                    nInputTotValue = drTot["INPUTTOTAL"].ToSafeDoubleStaMin();
                    idSeq++;
                    for (j = 0; j < nValueTypeMax; j++)
                    {
                        switch (j)
                        {
                            case 0://Value
                                defectValueType = "value";
                                break;
                            case 1://Rate
                                defectValueType = "rate";
                                break;
                            case 2://Value & Rate
                                defectValueType = "valueAndRate";
                                break;
                            case 3://투입수량
                                defectValueType = "input";
                                break;
                            default:
                                defectValueType = "none";
                                break;
                        }

                        DataRow drDataDbl = dtDefactDetail.NewRow();
                        DataRow drData = dtDefactDetailView.NewRow();
{
                            var defectRate = from b in dbPara.AsParallel()
                                             where b.Field<string>(subgroupValue) == defectData
                                             group b by new
                                             {
                                                 b.SUBGROUPNAME,
                                                 b.SAMPLINGNAME01
                                             }
                                              into g
                                             select new
                                             {
                                                 vSUBGROUP = g.Key.SUBGROUPNAME,
                                                 vSAMPLING = g.Key.SAMPLINGNAME01,
                                                 vNVALUE = g.Sum(s => s.NVALUE),
                                                 vNSUBVALUE = g.Sum(s => s.NSUBVALUE),
                                                 vCOUNT = g.Count()
                                             };
                            foreach (var f in defectRate)
                            {

                                //DataRow[] rows = tbPPIDataAvg.Select("SUBGROUP = '" + f.vSUBGROUP + "' ");
                                //계산용
                                drDataDbl["SEQID"] = idSeq;
                                drDataDbl["GROUPID"] = "DEFECT";
                                drDataDbl["DEFECTCODE"] = f.vSUBGROUP;
                                drDataDbl["DEFECTNAME"] = f.vSUBGROUP;
                                drDataDbl["VALUETYPE"] = defectValueType;
                                drDataDbl["DEFECTTOTAL"] = nDefectTotValue;
                                drDataDbl["INPUTTOTAL"] = nInputTotValue;

                                //표시용.
                                drData["SEQID"] = idSeq;
                                drData["GROUPID"] = "DEFECT";
                                drData["DEFECTCODE"] = f.vSUBGROUP;
                                drData["DEFECTNAME"] = f.vSUBGROUP;
                                drData["VALUETYPE"] = defectValueType;
                                drData["DEFECTTOTAL"] = nDefectTotValue;
                                drData["INPUTTOTAL"] = nInputTotValue;

                                defectDateTime = f.vSAMPLING.ToSafeString();
                                if (checkMonth == "")
                                {
                                    checkMonth = defectDateTime;
                                }
                                rateFieldDay = SpcFunction.DateToDay(defectDateTime);
                                rateFieldDay = string.Format("D{0}", rateFieldDay);
                                nDefectValue = f.vNVALUE.ToSafeDoubleStaMin();
                                nDefectInspectionValue = f.vNSUBVALUE.ToSafeDoubleStaMin();
                                nDefectRate = (nDefectValue / nDefectTotValue) * 100;
                                nDefectRateRound = Math.Round(nDefectRate, 3);
                                switch (j)
                                {
                                    case 0://Value
                                        drDataDbl[rateFieldDay] = nDefectValue;
                                        drData[rateFieldDay] = nDefectValue.ToSafeString();
                                        break;
                                    case 1://Rate
                                        drDataDbl[rateFieldDay] = nDefectRateRound;
                                        drData[rateFieldDay] = nDefectRateRound.ToSafeString();
                                        break;
                                    case 2://Value & Rate
                                        drData[rateFieldDay] = string.Format("{0}({1})", nDefectValue.ToSafeString(), nDefectRateRound.ToSafeString());
                                        break;
                                    case 3://투입수량
                                        drDataDbl["DEFECTTOTAL"] = nInputTotValue;
                                        drDataDbl[rateFieldDay] = nDefectInspectionValue;
                                        drData[rateFieldDay] = nDefectInspectionValue.ToSafeString();
                                        break;
                                    default:
                                        drData[rateFieldDay] = string.Format("Index Error : {0}", j);
                                        break;
                                }
                            }
                        }

                        //크기 조정.
                        for (int k = nDayStartPoint; k < nDayEndPoint; k++)
                        {
                            drDataDbl[k] = drDataDbl[k].ToSafeDoubleZero();
                        }
                        dtDefactDetail.Rows.Add(drDataDbl);
                        dtDefactDetailView.Rows.Add(drData);
                    }
                }

                if (checkMonth != "")
                {
                    gridCaption = SpcFunction.DayMonthList(checkMonth);
                }


                for (i = 0; i < dtDefactDetail.Rows.Count; i++)
                {
                    DataRow drDetail = dtDefactDetail.Rows[i];
                    string dd = drDetail["DEFECTCODE"].ToSafeDBString();
                }

            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine(dtDefactDetailView.Rows.Count);
            return dtDefactDetailView;

        }
        /// <summary>
        /// 전체 합계
        /// </summary>
        /// <param name="dtDefactDetail"></param>
        /// <param name="valueType"></param>
        /// <param name="statusMessage"></param>
        /// <returns></returns>
        public DataTable DefectRateViewSum(DataTable dtDefactDetail, out string statusMessage)
        {
            int idSeq = 0;
            DataTable dtDefactSum;
            statusMessage = "";
            dtDefactSum = CreateDataTableDefectRate();
            string valueType = "";
            string[] valueTypes = new string[3];
            valueTypes[0] = "input";
            valueTypes[1] = "value";
            valueTypes[2] = "rate";
            string[] valueTypesName = new string[3];
            valueTypesName[0] = "투입수";
            valueTypesName[1] = "불량수";
            valueTypesName[2] = "불량률";

            try
            {
                for (int i = 0; i < valueTypes.Length; i++)
                {
                    valueType = valueTypes[i];

                    idSeq = 0;

                    var dtDefactViewTemp = dtDefactDetail.AsEnumerable();
                    var query = dtDefactViewTemp.AsParallel()
                        .Where(w => w.Field<string>("VALUETYPE") == valueType)
                        .GroupBy(g => g.Field<string>("GROUPID"))
                        .Select( s => new
                        {
                            vGROUPID = s.Key,
                            vVALUETYPE = s.Max(f => f.Field<string>("VALUETYPE")),
                            vDEFECTTOTAL = s.Sum(f => f.Field<double>("DEFECTTOTAL")),
                            v01 = s.Sum(f => f.Field<double>("D01")),
                            v02 = s.Sum(f => f.Field<double>("D02")),
                            v03 = s.Sum(f => f.Field<double>("D03")),
                            v04 = s.Sum(f => f.Field<double>("D04")),
                            v05 = s.Sum(f => f.Field<double>("D05")),
                            v06 = s.Sum(f => f.Field<double>("D06")),
                            v07 = s.Sum(f => f.Field<double>("D07")),
                            v08 = s.Sum(f => f.Field<double>("D08")),
                            v09 = s.Sum(f => f.Field<double>("D09")),
                            v10 = s.Sum(f => f.Field<double>("D10")),
                            v11 = s.Sum(f => f.Field<double>("D11")),
                            v12 = s.Sum(f => f.Field<double>("D12")),
                            v13 = s.Sum(f => f.Field<double>("D13")),
                            v14 = s.Sum(f => f.Field<double>("D14")),
                            v15 = s.Sum(f => f.Field<double>("D15")),
                            v16 = s.Sum(f => f.Field<double>("D16")),
                            v17 = s.Sum(f => f.Field<double>("D17")),
                            v18 = s.Sum(f => f.Field<double>("D18")),
                            v19 = s.Sum(f => f.Field<double>("D19")),
                            v20 = s.Sum(f => f.Field<double>("D20")),
                            v21 = s.Sum(f => f.Field<double>("D21")),
                            v22 = s.Sum(f => f.Field<double>("D22")),
                            v23 = s.Sum(f => f.Field<double>("D23")),
                            v24 = s.Sum(f => f.Field<double>("D24")),
                            v25 = s.Sum(f => f.Field<double>("D25")),
                            v26 = s.Sum(f => f.Field<double>("D26")),
                            v27 = s.Sum(f => f.Field<double>("D27")),
                            v28 = s.Sum(f => f.Field<double>("D28")),
                            v29 = s.Sum(f => f.Field<double>("D29")),
                            v30 = s.Sum(f => f.Field<double>("D30")),
                            v31 = s.Sum(f => f.Field<double>("D31")),
                        });
                    foreach (var da in query)
                    {
                        idSeq++;
                        DataRow drData = dtDefactSum.NewRow();
                        drData["SEQID"] = idSeq;
                        drData["GROUPID"] = da.vGROUPID;
                        drData["VALUETYPE"] = valueTypesName[i]; // da.vVALUETYPE;
                        drData["DEFECTTOTAL"] = da.vDEFECTTOTAL;
                        drData["D01"] = da.v01;
                        drData["D02"] = da.v02;
                        drData["D03"] = da.v03;
                        drData["D04"] = da.v04;
                        drData["D05"] = da.v05;
                        drData["D06"] = da.v06;
                        drData["D07"] = da.v07;
                        drData["D08"] = da.v08;
                        drData["D09"] = da.v09;
                        drData["D10"] = da.v10;
                        drData["D11"] = da.v11;
                        drData["D12"] = da.v12;
                        drData["D13"] = da.v13;
                        drData["D14"] = da.v14;
                        drData["D15"] = da.v15;
                        drData["D16"] = da.v16;
                        drData["D17"] = da.v17;
                        drData["D18"] = da.v18;
                        drData["D19"] = da.v19;
                        drData["D20"] = da.v20;
                        drData["D21"] = da.v21;
                        drData["D22"] = da.v22;
                        drData["D23"] = da.v23;
                        drData["D24"] = da.v24;
                        drData["D25"] = da.v25;
                        drData["D26"] = da.v26;
                        drData["D27"] = da.v27;
                        drData["D28"] = da.v28;
                        drData["D29"] = da.v29;
                        drData["D30"] = da.v30;
                        drData["D31"] = da.v31;

                        dtDefactSum.Rows.Add(drData);
                        Console.WriteLine(da.vGROUPID);
                    }
                }
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
                Console.WriteLine(ex.Message);
            }

            string codeName = "";
            double sumInputData;
            double sumDefectData;
            double sumRateData;
            int nDayStartPoint = 9;//9에서 일자 시작.
            int nDayEndPoint = 40;//9에서 일자 시작.

            if (dtDefactSum != null && dtDefactSum.Rows.Count > 2)
            {
                sumInputData = dtDefactSum.Rows[0]["DEFECTTOTAL"].ToSafeDoubleZero();
                sumDefectData = dtDefactSum.Rows[1]["DEFECTTOTAL"].ToSafeDoubleZero();
                if (sumDefectData > 0)
                {
                    sumRateData = Math.Round((sumDefectData / sumInputData) * 100, 3);
                }
                else
                {
                    sumRateData = 0;
                }
                dtDefactSum.Rows[2]["DEFECTTOTAL"] = sumRateData;
            }

            //Day
            for (int i = nDayStartPoint; i < nDayEndPoint; i++)
            {
                sumInputData = dtDefactSum.Rows[0][i].ToSafeDoubleZero();
                sumDefectData = dtDefactSum.Rows[1][i].ToSafeDoubleZero();
                if (sumDefectData > 0)
                {
                    sumRateData = Math.Round((sumDefectData / sumInputData) * 100, 3);
                }
                else
                {
                    sumRateData = 0;
                }
                dtDefactSum.Rows[2][i] = sumRateData;
            }

            return dtDefactSum;
        }

        /// <summary>
        /// 불량 합계 테이블 생성.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTableDefectTotal(string tableName = "dtDefacetTotal")
        {
            int nMaxDay = 32;
            string fieldDay = "";
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed defectcode
            dt.Columns.Add("SEQID", typeof(long));
            dt.Columns.Add("DEFECTCODE", typeof(string));
            dt.Columns.Add("DEFECTTOTAL", typeof(double));
            dt.Columns.Add("INPUTTOTAL", typeof(double));
            dt.Columns.Add("TOTVALUE", typeof(double));
            dt.Columns.Add("TOTVALUEVIEW", typeof(string));
            return dt;
        }
        /// <summary>
        /// 불량률 List 테이블 생성.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTableDefectRate(string tableName = "dtDefacetRate")
        {
            int nMaxDay = 32;
            string fieldDay = "";
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed defectcode
            dt.Columns.Add("SEQID", typeof(long));
            dt.Columns.Add("GROUPID", typeof(string));
            dt.Columns.Add("DEFECTCODE", typeof(string));
            dt.Columns.Add("DEFECTNAME", typeof(string));
            dt.Columns.Add("VALUETYPE", typeof(string));
            dt.Columns.Add("DEFECTTOTAL", typeof(double));
            dt.Columns.Add("INPUTTOTAL", typeof(double));
            dt.Columns.Add("TOTVALUE", typeof(double));
            dt.Columns.Add("TOTVALUEVIEW", typeof(string));
            for (int i = 1; i < nMaxDay; i++)
            {
                fieldDay = string.Format("D{0}", i.ToString().PadLeft(2, '0'));
                //dt.Columns.Add(fieldDay, typeof(double));
                dt.Columns.Add(fieldDay, typeof(double));
            }
            return dt;
        }
        /// <summary>
        /// 불량률 List View 테이블 생성.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTableDefectRateView(string tableName = "dtDefacetRateView")
        {
            int nMaxDay = 32;
            string fieldDay = "";
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed defectcode
            dt.Columns.Add("SEQID", typeof(long));
            dt.Columns.Add("GROUPID", typeof(string));
            dt.Columns.Add("DEFECTCODE", typeof(string));
            dt.Columns.Add("DEFECTNAME", typeof(string));
            dt.Columns.Add("VALUETYPE", typeof(string));
            dt.Columns.Add("DEFECTTOTAL", typeof(double));
            dt.Columns.Add("INPUTTOTAL", typeof(double));
            dt.Columns.Add("TOTVALUE", typeof(double));
            dt.Columns.Add("TOTVALUEVIEW", typeof(string));
            for (int i = 1; i < nMaxDay; i++)
            {
                fieldDay = string.Format("D{0}", i.ToString().PadLeft(2, '0'));
                //dt.Columns.Add(fieldDay, typeof(double));
                dt.Columns.Add(fieldDay, typeof(string));
            }
            return dt;
        }
        #endregion //불량률 통계 구성.
    }

    public static class SpcDictionary
    {
        public static string Language;
        public static DataTable dtGridCaption;
        public static DataTable dtControlCaption;
        public static DataTable dtGroupCaption;

        /// <summary>
        /// 다국어 자료 확인
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CaptionDtCheck(SpcDicClassId grp)
        {
            bool result = false;

            try
            {
                switch (grp)
                {
                    case SpcDicClassId.COMMON:
                        break;
                    case SpcDicClassId.GRID:
                        if (dtGridCaption != null && dtGridCaption.Rows.Count > 0)
                        {
                            result = true;
                        }
                        break;
                    case SpcDicClassId.BUTTON:
                        break;
                    case SpcDicClassId.GROUPCAPTION:
                        if (dtGroupCaption != null && dtGroupCaption.Rows.Count > 0)
                        {
                            result = true;
                        }
                        break;
                    case SpcDicClassId.CONTROLLABEL:
                        if (dtControlCaption != null && dtControlCaption.Rows.Count > 0)
                        {
                            result = true;
                        }
                        break;
                    case SpcDicClassId.CONDITION:
                        break;
                    case SpcDicClassId.CONDITIONLABEL:
                        break;
                    case SpcDicClassId.Framework:
                        break;
                    case SpcDicClassId.MAINFORM:
                        break;
                    case SpcDicClassId.MASTERDICTIONARYCLASS:
                        break;
                    case SpcDicClassId.MENU:
                        break;
                    case SpcDicClassId.POPUP:
                        break;
                    case SpcDicClassId.TOOLBAR:
                        break;
                    default:
                        break;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 그리드 라벨
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string read(SpcDicClassId grp, string id)
        {
            string data = "";
            string classID = grp.ToString();

            try
            {
                if (dtGridCaption != null && dtGridCaption.Rows.Count > 0)
                {
                    var dtList = dtGridCaption.AsEnumerable().AsParallel();
                    var query = from r in dtList
                                where r.Field<string>("DICTIONARYCLASSID") == classID
                                where r.Field<string>("DICTIONARYID") == id
                                select new
                                {
                                    classid = r.Field<string>("DICTIONARYID")
                                        ,
                                    id = r.Field<string>("DICTIONARYID")
                                        ,
                                    kor = r.Field<string>("DICTIONARYNAME$$KO-KR")
                                        ,
                                    usa = r.Field<string>("DICTIONARYNAME$$EN-US")
                                        ,
                                    chi = r.Field<string>("DICTIONARYNAME$$ZH-CN")
                                        ,
                                    vin = r.Field<string>("DICTIONARYNAME$$VI-VN")
                                };
                    foreach (var rw in query)
                    {
                        switch (Language)
                        {
                            case "en-US":
                                data = rw.usa.ToString();
                                break;
                            case "zh-CN":
                                data = rw.chi.ToString();
                                break;
                            case "vi-VN":
                                data = rw.vin.ToString();
                                break;
                            case "ko-KR":
                            default:
                                data = rw.kor.ToString();
                                break;
                        }

                        //Console.WriteLine(data1);
                    }
                }
                else
                {
                    data = "";
                }


                if (data != null && data != "")
                {
                }
                else
                {
                    data = id;
                }

            }
            catch (Exception ex)
            {
                data = id;
                Console.WriteLine(ex.Message);
            }
            
            return data;
        }
        /// <summary>
        /// Control 라벨
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string readControl(SpcDicClassId grp, string id)
        {
            string data = "";
            string classID = grp.ToString();

            try
            {
                if (dtControlCaption != null && dtControlCaption.Rows.Count > 0)
                {
                    var dtList = dtControlCaption.AsEnumerable().AsParallel();
                    var query = from r in dtList
                                where r.Field<string>("DICTIONARYCLASSID") == classID
                                where r.Field<string>("DICTIONARYID") == id
                                select new
                                {
                                    classid = r.Field<string>("DICTIONARYID")
                                        ,
                                    id = r.Field<string>("DICTIONARYID")
                                        ,
                                    kor = r.Field<string>("DICTIONARYNAME$$KO-KR")
                                        ,
                                    usa = r.Field<string>("DICTIONARYNAME$$EN-US")
                                        ,
                                    chi = r.Field<string>("DICTIONARYNAME$$ZH-CN")
                                        ,
                                    vin = r.Field<string>("DICTIONARYNAME$$VI-VN")
                                };
                    foreach (var rw in query)
                    {
                        switch (Language)
                        {
                            case "en-US":
                                data = rw.usa.ToString();
                                break;
                            case "zh-CN":
                                data = rw.chi.ToString();
                                break;
                            case "vi-VN":
                                data = rw.vin.ToString();
                                break;
                            case "ko-KR":
                            default:
                                data = rw.kor.ToString();
                                break;
                        }

                        //Console.WriteLine(data1);
                    }
                }
                else
                {
                    data = "";
                }


                if (data != null && data != "")
                {
                }
                else
                {
                    data = id;
                }

            }
            catch (Exception ex)
            {
                data = id;
                Console.WriteLine(ex.Message);
            }

            return data;
        }
        /// <summary>
        /// Group 라벨
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string readGroup(SpcDicClassId grp, string id)
        {
            string data = "";
            string classID = grp.ToString();

            try
            {
                if (dtGroupCaption != null && dtGroupCaption.Rows.Count > 0)
                {
                    var dtList = dtGroupCaption.AsEnumerable().AsParallel();
                    var query = from r in dtList
                                where r.Field<string>("DICTIONARYCLASSID") == classID
                                where r.Field<string>("DICTIONARYID") == id
                                select new
                                {
                                    classid = r.Field<string>("DICTIONARYID")
                                        ,
                                    id = r.Field<string>("DICTIONARYID")
                                        ,
                                    kor = r.Field<string>("DICTIONARYNAME$$KO-KR")
                                        ,
                                    usa = r.Field<string>("DICTIONARYNAME$$EN-US")
                                        ,
                                    chi = r.Field<string>("DICTIONARYNAME$$ZH-CN")
                                        ,
                                    vin = r.Field<string>("DICTIONARYNAME$$VI-VN")
                                };
                    foreach (var rw in query)
                    {
                        switch (Language)
                        {
                            case "en-US":
                                data = rw.usa.ToString();
                                break;
                            case "zh-CN":
                                data = rw.chi.ToString();
                                break;
                            case "vi-VN":
                                data = rw.vin.ToString();
                                break;
                            case "ko-KR":
                            default:
                                data = rw.kor.ToString();
                                break;
                        }

                        //Console.WriteLine(data1);
                    }
                }
                else
                {
                    data = "";
                }


                if (data != null && data != "")
                {
                }
                else
                {
                    data = id;
                }

            }
            catch (Exception ex)
            {
                data = id;
                Console.WriteLine(ex.Message);
            }

            return data;
        }
    }

    /// <summary>
    /// 사전 정보 테이블 dictionaryclassid (SF_DICTIONARY)
    /// </summary>
    public enum SpcDicClassId : int
    {
          COMMON = 1
        , GRID = 2
        , BUTTON = 3
        , GROUPCAPTION = 4
        , CONTROLLABEL = 5
        , CONDITION = 6
        , CONDITIONLABEL = 7
        , Framework =8
        , MAINFORM = 9
        , MASTERDICTIONARYCLASS = 10
        , MENU = 11
        , POPUP = 12
        , TOOLBAR = 13
    }
}
