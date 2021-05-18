using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micube.SmartMES.QualityAnalysis.Helper
{
    /// <summary>
    /// 프 로 그 램 명  : 검사 액션관련 함수 정의
    /// 업  무  설  명  : 검사 액션관련 함수 정의
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-11-29
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    class InspectionHelper
    {

        /// <summary>
        /// 판정기준 : NCRDecisionDegree 시
        /// OK/NG 판정이나 Spec에 의한 판정
        /// </summary>
        /// <param name="inspectionClassId"></param>
        /// <param name="decisionDegree"></param>
        /// <param name="qcGrade"></param>
        /// <param name="sequence"></param>
        public static string GetQcGradeAndSequenceNCRAndSpecType(string inspectionClassId, string decisionDegree, out string sequence)
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"P_INSPECTIONCLASSID",inspectionClassId},
                {"P_NCRDECISIONDEGREE",decisionDegree},
                {"ENTERPRISEID", Framework.UserInfo.Current.Enterprise},
                {"PLANTID", Framework.UserInfo.Current.Plant}
            };

            DataTable ncrStandardDt = SqlExecuter.Query("SelectNCRCheckBasis", "10001", param);

            if (ncrStandardDt.Rows.Count > 0)
            {
                DataRow standardRow = ncrStandardDt.Rows[0];

                string qcGrade = "";
                qcGrade = standardRow["QCGRADE"].ToString();
                sequence = standardRow["PRIORITY"].ToString();

                return qcGrade;
            }
            else
            {
                sequence = null;
                return null;
            }
        }

        /// <summary>
        /// 판정기준 : NCRDecisionDegree / DecisionDegree 시
        /// 수량이나 불량율로 판정
        /// 판정 기준이 없을 때 메세지 아이디를 반환한다
        /// </summary>
        /// <param name="inspectionClassId"></param>
        /// <param name="decisionDegree"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static string SetQcGradeAndResultNCRQtyRateType(DataRow inputRow, string inspectionClassId, string decisionDegree, bool isDefectQty, out string messageId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"P_INSPECTIONCLASSID",inspectionClassId},
                {"P_NCRDECISIONDEGREE",decisionDegree},
                {"ENTERPRISEID", Framework.UserInfo.Current.Enterprise},
                {"PLANTID", Framework.UserInfo.Current.Plant}
            };

            DataTable ncrStandardDt = SqlExecuter.Query("SelectNCRCheckBasis", "10001", param);

            string result = "";
            string qcGrade = "";
            string sequence = "";
            bool isMatching = false;// NG기준에 맞는지 여부 true => result "NG"
                                    //                     false => result "OK"


            if (ncrStandardDt.Rows.Count == 0 )
            {//비교 할 Row가 없을 때
                messageId = "NoStandardData";
                return result;
  
            } else
            {//비교 할 Row가 하나 이상 일때

                foreach (DataRow standardRow in ncrStandardDt.Rows)
                {
                    if (standardRow["QTYORRATE"] != null && standardRow["QCGRADE"] != null)
                    {//수량/불량율 기준이 있는것만

                        qcGrade = standardRow["QCGRADE"].ToString();
                        sequence = standardRow["PRIORITY"].ToString();

                        if (standardRow["QTYORRATE"].ToString().Equals("QTY"))
                        {//수량으로 판정하는 경우

                            isMatching = GetMatchingResultQTY(inputRow, standardRow, isDefectQty); 

                        } else if (standardRow["QTYORRATE"].ToString().Equals("RATE"))
                        {//불량 율로 판정하는 경우

                            isMatching = GetMatchingResultRate(inputRow, standardRow);

                        }

                        if (isMatching == true)
                        {
                            result = "NG";
                            inputRow["QCGRADE"] = qcGrade;
                            inputRow["PRIORITY"] = sequence;
                            break;
                        }
                        else
                        {
                            result = "OK";
                        }

                    }
                }

                messageId = null;
                return result;
            } 

        }


        /// <summary>
        /// QTY 판정일 경우
        /// Row별로 입력값이 기준에 해당되는지 판정하는 함수
        /// true => 판정범위안에 속함 result => "NG"
        /// flase => 판정범위에 속하지 않음 result => "OK"
        /// </summary>
        /// <param name="inputRow"></param>
        /// <param name="standardRow"></param>
        /// <param name="IsDefectQty"></param>
        /// <returns></returns>
        public static bool GetMatchingResultQTY(DataRow inputRow, DataRow standardRow, bool IsDefectQty)
        {
            bool isMatching = false;

            string condition = standardRow["NGCONDITION"].ToString();

            int ngQuantity = standardRow["NGQUANTITY"].ToSafeInt32();
            int specOutQty = 0;

            if (IsDefectQty == true)
            {//sf_insipectionDefect 테이블에 insert될때 컬럼 명
                specOutQty = inputRow["DEFECTQTY"].ToSafeInt32();
            }
            else
            {//sf_inspectionItemResutl 테이블에 insert될때 컬럼 명
                specOutQty = inputRow["SPECOUTQTY"].ToSafeInt32();
            }

            switch (condition)
            {
                case "EQ"://동일
                    if (ngQuantity == specOutQty)
                    {
                        isMatching = true;
                    }
                    else
                    {
                        isMatching = false;
                    }
                    break;

                case "GE"://이상
                    if (ngQuantity <= specOutQty)
                    {
                        isMatching = true;

                    } else
                    {
                        isMatching = false;
                    }
                    break;

                case "GT"://초과
                    if (ngQuantity < specOutQty)
                    {
                        isMatching = true;

                    }
                    else
                    {
                        isMatching = false;
                    }
                    break;

                case "LE"://이하
                    if (ngQuantity >= specOutQty)
                    {
                        isMatching = true;

                    }
                    else
                    {
                        isMatching = false;
                    }
                    break;

                case "LT"://미만
                    if (ngQuantity > specOutQty)
                    {
                        isMatching = true;

                    }
                    else
                    {
                        isMatching = false;
                    }
                    break;
            }

            return isMatching;
        }

        /// <summary>
        /// RATE 판정일 경우
        /// Row별로 입력값이 기준에 해당되는지 판정하는 함수
        /// true => 판정범위안에 속함 result => "NG"
        /// flase => 판정범위에 속하지 않음 result => "OK" 
        /// </summary>
        /// <param name="inputRow"></param>
        /// <param name="standardRow"></param>
        /// <returns></returns>
        private static bool GetMatchingResultRate(DataRow inputRow, DataRow standardRow)
        {
            bool isMatching = false;

            string condition = standardRow["NGCONDITION"].ToString();

            decimal fromNGRate = standardRow["FROMNGRATE"].ToSafeDecimal();
            decimal toNGRate = standardRow["TONGRATE"].ToSafeDecimal();

            decimal defectRate = inputRow["DEFECTRATE"].ToString().Replace("%", "").ToSafeDecimal();

            switch (condition)
            {
                case "BT"://사이 (초과, 미만)
                    if (fromNGRate <= defectRate && defectRate < toNGRate)
                    {
                        isMatching = true;
                    }
                    else
                    {
                        isMatching = false;
                    }
                    break;

                case "EQ"://동일  ******from / to 정해지면 수정 가능성 있음
                    if (toNGRate == defectRate)
                    {
                        isMatching = true;
                    }
                    else
                    {
                        isMatching = false;
                    }
                    break;

                case "GE"://이상  ******from / to 정해지면 수정 가능성 있음
                    if (toNGRate <= defectRate)
                    {
                        isMatching = true;

                    }
                    else
                    {
                        isMatching = false;
                    }
                    break;

                case "GT"://초과  ******from / to 정해지면 수정 가능성 있음
                    if (toNGRate < defectRate)
                    {
                        isMatching = true;

                    }
                    else
                    {
                        isMatching = false;
                    }
                    break;

                case "LE"://이하  ******from / to 정해지면 수정 가능성 있음
                    if (toNGRate >= defectRate)
                    {
                        isMatching = true;

                    }
                    else
                    {
                        isMatching = false;
                    }
                    break;

                case "LT"://미만  ******from / to 정해지면 수정 가능성 있음
                    if (toNGRate > defectRate)
                    {
                        isMatching = true;

                    }
                    else
                    {
                        isMatching = false;
                    }
                    break;
            }

            return isMatching;
        }

        /// <summary>
        /// 판정기준 : AQLDecisionDegree 시
        /// </summary>
        /// <param name="inspectionClassId"></param>
        /// <param name="decisionDegree"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static string SetQcGradeAndResultAQLType(DataRow inputRow, string inspectionClassId, string decisionDegree,string lotQty, bool isDefect, out string messageId)
        {
            string result = "";

            string aqlInspectionLevel = inputRow["AQLINSPECTIONLEVEL"].ToString();
            string aqlDefectLevel = inputRow["AQLDEFECTLEVEL"].ToString();

            if (string.IsNullOrWhiteSpace(aqlInspectionLevel) ||string.IsNullOrWhiteSpace(aqlDefectLevel) || string.IsNullOrWhiteSpace(lotQty))
            {
                messageId = "NoStandardData";//판정 기준이 없습니다.
                return "NG";
            }

            //AQL 판정기준 가져오는 쿼리
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_SPCLEVEL", aqlInspectionLevel);
            param.Add("P_DEFLEVEL", aqlDefectLevel);
            param.Add("P_LOTQTY", lotQty.ToSafeInt32());

            DataTable aqlStandard = SqlExecuter.Query("SelectAQLCheckBasis", "10001", param);

            if (aqlStandard.Rows.Count > 0)
            {//spec값
                DataRow AQLStandardRow = aqlStandard.Rows[0];

                //스펙기준 값
                string standardDefectQty = AQLStandardRow["DEFECTRATE"].ToString();
                decimal inputedSpecOutQty = 0;

                if (isDefect == true)
                {
                    inputedSpecOutQty = inputRow["DEFECTQTY"].ToSafeDecimal();
                }
                else
                {
                    inputedSpecOutQty = inputRow["SPECOUTQTY"].ToSafeDecimal();
                }

                if (!string.IsNullOrWhiteSpace(standardDefectQty))
                {
                    decimal defectQty = standardDefectQty.ToSafeDecimal();

                    //불량수량이 기준을 벗어났을 경우
                    //2020-03-27 강유라 초과 -> 이상으로 기준변경
                    if (inputedSpecOutQty >= defectQty)
                    {
                        result = "NG";
                    }
                    else
                    {
                        result = "OK";
                    }
                }
            }

            //AQL의 액션 등급을 구하는 쿼리
            Dictionary<string, object> gradeParam = new Dictionary<string, object>()
            {
                {"P_INSPECTIONCLASSID",inspectionClassId},
                {"P_DECISIONDEGREE",decisionDegree},
                {"ENTERPRISEID", Framework.UserInfo.Current.Enterprise},
                {"PLANTID", Framework.UserInfo.Current.Plant}
            };

            DataTable actionGradeDt = SqlExecuter.Query("SelectAQLActionGrade", "10001", gradeParam);

            if (actionGradeDt.Rows.Count > 0)
            {
                DataRow actionGradeRow = actionGradeDt.Rows[0];

                inputRow["QCGRADE"] = actionGradeRow["QCGRADE"].ToString();
                inputRow["PRIORITY"] = actionGradeRow["PRIORITY"].ToString();

            }
            else
            {
                messageId = "NoActionStandardData";//판정 등급이 없습니다.
            }

            messageId = null;
            return result;
        }

        /// <summary>
        /// 판정기준 : AQLDecisionDegree 시
        /// </summary>
        /// <param name="inspectionClassId"></param>
        /// <param name="decisionDegree"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static string SetQcGradeAndResultAQLType(DataRow inputRow,string aqlinspectionLevel, string aqldefectLevel, string inspectionClassId, string decisionDegree, string lotQty, bool isDefect, out string messageId)
        {
            string result = "";

            if (string.IsNullOrWhiteSpace(aqlinspectionLevel) || string.IsNullOrWhiteSpace(aqldefectLevel) || string.IsNullOrWhiteSpace(lotQty))
            {
                messageId = "NoStandardData";//판정 기준이 없습니다.
                return "NG";
            }

            //AQL 판정기준 가져오는 쿼리
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_SPCLEVEL", aqlinspectionLevel);
            param.Add("P_DEFLEVEL", aqldefectLevel);
            param.Add("P_LOTQTY", lotQty.ToSafeInt32());

            DataTable aqlStandard = SqlExecuter.Query("SelectAQLCheckBasis", "10001", param);

            if (aqlStandard.Rows.Count > 0)
            {//spec값
                DataRow AQLStandardRow = aqlStandard.Rows[0];

                //스펙기준 값
                string standardDefectQty = AQLStandardRow["DEFECTRATE"].ToString();
                decimal inputedSpecOutQty = 0;

                if (isDefect == true)
                {
                    inputedSpecOutQty = inputRow["DEFECTQTY"].ToSafeDecimal();
                }
                else
                {
                    inputedSpecOutQty = inputRow["SPECOUTQTY"].ToSafeDecimal();
                }

                if (!string.IsNullOrWhiteSpace(standardDefectQty))
                {
                    decimal defectQty = standardDefectQty.ToSafeDecimal();

                    //불량수량이 기준을 벗어났을 경우
                    //2020-03-27 강유라 초과 -> 이상으로 기준변경
                    if (inputedSpecOutQty >= defectQty)
                    {
                        result = "NG";
                    }
                    else
                    {
                        result = "OK";
                    }
                }
            }

            //AQL의 액션 등급을 구하는 쿼리
            Dictionary<string, object> gradeParam = new Dictionary<string, object>()
            {
                {"P_INSPECTIONCLASSID",inspectionClassId},
                {"P_DECISIONDEGREE",decisionDegree},
                {"ENTERPRISEID", Framework.UserInfo.Current.Enterprise},
                {"PLANTID", Framework.UserInfo.Current.Plant}
            };

            DataTable actionGradeDt = SqlExecuter.Query("SelectAQLActionGrade", "10001", gradeParam);

            if (actionGradeDt.Rows.Count > 0)
            {
                DataRow actionGradeRow = actionGradeDt.Rows[0];

                inputRow["QCGRADE"] = actionGradeRow["QCGRADE"].ToString();
                inputRow["PRIORITY"] = actionGradeRow["PRIORITY"].ToString();

            }
            else
            {
                messageId = "NoActionStandardData";//판정 등급이 없습니다.
            }

            messageId = null;
            return result;
        }

        /// <summary>
        /// 판정기준 : NCRDecisionDegree 시
        /// 외관 검사, 스펙 검사의 결과 중 가장 높은 등급의 조치등급을 반환하는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetPriorityQCGrade(SmartBandedGrid exGrd, SmartBandedGrid specGrd, string exDecisionType)
        {
            DataTable exDt = exGrd.DataSource as DataTable;
            DataTable specDt = specGrd.DataSource as DataTable;

            CheckHasPriority(exDt, specDt);

            string priorityQCGrade = "";
            /*
            var exGrade = exDt.AsEnumerable()
                .OrderBy(r => r["NCRDECISIONDEGREE"]).ThenBy(r => r["SEQUENCE"])
                .Select(r => new { degree = r["NCRDECISIONDEGREE"], sequence = r["SEQUENCE"], qcGrade = r["QCGRADE"] })
                .FirstOrDefault();

            var specGrade = exDt.AsEnumerable()
               .OrderBy(r => r["NCRDECISIONDEGREE"]).ThenBy(r => r["SEQUENCE"])
               .Select(r => new { degree = r["NCRDECISIONDEGREE"], sequence = r["SEQUENCE"], qcGrade = r["QCGRADE"] })
               .FirstOrDefault();
*/
            DataRow exRow = null;
            DataRow specRow = null;

            string exDegree = "";
            string exQCGrade = "";
            int exSequence = 99;
            string specDegree = "";
            string specQCGrade = "";
            int specSequence = 99;

            List<DataRow> exGrade = null;

            if (exDecisionType.Equals("AQL"))
            {
                exGrade = exDt.AsEnumerable()
                    .OrderBy(r => r["AQLDECISIONDEGREE"].ToString()).ThenBy(r => r["PRIORITY"].ToString())
                    .Where(r => !string.IsNullOrWhiteSpace(r["QCGRADE"].ToString()) && !string.IsNullOrWhiteSpace(r["PRIORITY"].ToString())
                    && r["INSPECTIONRESULT"].ToString().Equals("NG"))
                    .ToList();

                exDecisionType = "AQLDECISIONDEGREE";
            }
            else if (exDecisionType.Equals("NCR"))
            {
                exGrade = exDt.AsEnumerable()
                   .OrderBy(r => r["NCRDECISIONDEGREE"].ToString()).ThenBy(r => r["PRIORITY"].ToString())
                   .Where(r => !string.IsNullOrWhiteSpace(r["QCGRADE"].ToString()) && !string.IsNullOrWhiteSpace(r["PRIORITY"].ToString())
                   && r["INSPECTIONRESULT"].ToString().Equals("NG"))
                   .ToList();

                exDecisionType = "NCRDECISIONDEGREE";
            }

            var specGrade = specDt.AsEnumerable()
               .OrderBy(r => r["NCRDECISIONDEGREE"]).ThenBy(r => r["PRIORITY"])
               .Where(r => !string.IsNullOrWhiteSpace(r["QCGRADE"].ToString()) && !string.IsNullOrWhiteSpace(r["PRIORITY"].ToString())
               && r["INSPECTIONRESULT"].ToString().Equals("NG"))
               .ToList();

            if (exGrade.Count > 0)
            {
                exRow = exGrade.CopyToDataTable().Rows[0];
                exDegree = exRow[exDecisionType].ToString();
                exQCGrade = exRow["QCGRADE"].ToString();
                exSequence = exRow["PRIORITY"].ToSafeInt32();
            }

            if (specGrade.Count > 0)
            {
                specRow = specGrade.CopyToDataTable().Rows[0];
                specDegree = specRow["NCRDECISIONDEGREE"].ToString();
                specQCGrade = specRow["QCGRADE"].ToString();
                specSequence = specRow["PRIORITY"].ToSafeInt32();
            }


            if (exRow != null && specRow != null)
            {
                int exDegreeAscii = Convert.ToInt32(exDegree.ToCharArray()[0]);
                int specDegreeAscii = Convert.ToInt32(specDegree.ToCharArray()[0]);

                if (exDegreeAscii < specDegreeAscii)
                {//외관의 판정기준이 높은 경우 (ex: 외관 : A 스펙 : B)
                    priorityQCGrade = exQCGrade;
                }
                else if (exDegreeAscii > specDegreeAscii)
                {//스펙의 판정기준이 높은 경우 (ex: 외관 : B 스펙 : A)
                    priorityQCGrade = specQCGrade;
                }
                else if (exDegreeAscii == specDegreeAscii)
                {//판정기준이 같은 경우 (ex: 외관 : A 스펙 : A)

                    if (exSequence < specSequence)
                    {//외관의 판정기준 순서가 높은 경우 (ex: 외관 : 1 스펙 : 4)
                        priorityQCGrade = exQCGrade;
                    }
                    else
                    {// 스펙의 판정기준 순서가 높거나 같은경우
                        priorityQCGrade = specQCGrade;
                    }
                }
            }
            else if (exRow != null && specRow == null)
            {
                priorityQCGrade = exQCGrade;
            }
            else if (exRow == null && specRow != null)
            {
                priorityQCGrade = specQCGrade;
            }

            return priorityQCGrade;

        }


        /// <summary>
        /// 판정기준 : NCRDecisionDegree 시
        /// 외관 검사, 스펙 검사의 결과 중 가장 높은 등급의 조치등급을 반환하는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetPriorityQCGradeOSP(SmartBandedGrid exGrd, DataTable specDt, string exDecisionType)
        {
            DataTable exDt = exGrd.DataSource as DataTable;

            CheckHasPriority(exDt, specDt);

            string priorityQCGrade = "";
            /*
            var exGrade = exDt.AsEnumerable()
                .OrderBy(r => r["NCRDECISIONDEGREE"]).ThenBy(r => r["SEQUENCE"])
                .Select(r => new { degree = r["NCRDECISIONDEGREE"], sequence = r["SEQUENCE"], qcGrade = r["QCGRADE"] })
                .FirstOrDefault();

            var specGrade = exDt.AsEnumerable()
               .OrderBy(r => r["NCRDECISIONDEGREE"]).ThenBy(r => r["SEQUENCE"])
               .Select(r => new { degree = r["NCRDECISIONDEGREE"], sequence = r["SEQUENCE"], qcGrade = r["QCGRADE"] })
               .FirstOrDefault();
*/
            DataRow exRow = null;
            DataRow specRow = null;

            string exDegree = "";
            string exQCGrade = "";
            int exSequence = 99;
            string specDegree = "";
            string specQCGrade = "";
            int specSequence = 99;

            List<DataRow> exGrade = null;

            if (exDecisionType.Equals("AQL"))
            {
                exGrade = exDt.AsEnumerable()
                    .OrderBy(r => r["AQLDECISIONDEGREE"].ToString()).ThenBy(r => r["PRIORITY"].ToString())
                    .Where(r => !string.IsNullOrWhiteSpace(r["QCGRADE"].ToString()) && !string.IsNullOrWhiteSpace(r["PRIORITY"].ToString())
                    && r["INSPECTIONRESULT"].ToString().Equals("NG"))
                    .ToList();

                exDecisionType = "AQLDECISIONDEGREE";
            }
            else if (exDecisionType.Equals("NCR"))
            {
                 exGrade = exDt.AsEnumerable()
                    .OrderBy(r => r["DECISIONDEGREE"].ToString()).ThenBy(r => r["PRIORITY"].ToString())
                    .Where(r => !string.IsNullOrWhiteSpace(r["QCGRADE"].ToString()) && !string.IsNullOrWhiteSpace(r["PRIORITY"].ToString())
                    && r["INSPECTIONRESULT"].ToString().Equals("NG"))
                    .ToList();

                exDecisionType = "DECISIONDEGREE";
            }
            

            var specGrade = specDt.AsEnumerable()
               .OrderBy(r => r["NCRDECISIONDEGREE"].ToString()).ThenBy(r => r["PRIORITY"].ToString())
               .Where(r => !string.IsNullOrWhiteSpace(r["QCGRADE"].ToString()) && !string.IsNullOrWhiteSpace(r["PRIORITY"].ToString())
               && r["INSPECTIONRESULT"].ToString().Equals("NG"))
               .ToList();
            
            if (exGrade.Count > 0)
            {
                exRow = exGrade.CopyToDataTable().Rows[0];
                exDegree = exRow[exDecisionType].ToString();
                exQCGrade = exRow["QCGRADE"].ToString();
                exSequence = exRow["PRIORITY"].ToSafeInt32();
            }
            
            if (specGrade.Count > 0)
            {
                specRow = specGrade.CopyToDataTable().Rows[0];
                specDegree = specRow["NCRDECISIONDEGREE"].ToString();
                specQCGrade = specRow["QCGRADE"].ToString();
                specSequence = specRow["PRIORITY"].ToSafeInt32();
            }


            if (exRow != null && specRow != null)
            {
                int exDegreeAscii = Convert.ToInt32(exDegree.ToCharArray()[0]);
                int specDegreeAscii = Convert.ToInt32(specDegree.ToCharArray()[0]);

                if (exDegreeAscii < specDegreeAscii)
                {//외관의 판정기준이 높은 경우 (ex: 외관 : A 스펙 : B)
                    priorityQCGrade = exQCGrade;
                }
                else if (exDegreeAscii > specDegreeAscii)
                {//스펙의 판정기준이 높은 경우 (ex: 외관 : B 스펙 : A)
                    priorityQCGrade = specQCGrade;
                }
                else if (exDegreeAscii == specDegreeAscii)
                {//판정기준이 같은 경우 (ex: 외관 : A 스펙 : A)

                    if (exSequence < specSequence)
                    {//외관의 판정기준 순서가 높은 경우 (ex: 외관 : 1 스펙 : 4)
                        priorityQCGrade = exQCGrade;
                    }
                    else
                    {// 스펙의 판정기준 순서가 높거나 같은경우
                        priorityQCGrade = specQCGrade;
                    }
                }
            }
            else if (exRow != null && specRow == null)
            {
                priorityQCGrade = exQCGrade;
            }
            else if (exRow == null && specRow != null)
            {
                priorityQCGrade = specQCGrade;
            }

            return priorityQCGrade;

        }

        /// <summary>
        /// 검사항목의 PROCESSRELNO을 세팅하는 함수
        /// </summary>
        /// <param name="processRelNo"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable SetProcessRelNo(string processRelNo, DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                row["PROCESSRELNO"] = processRelNo;
            }

            return table;
        }

        /// <summary>
        /// 외관, 스펙검사 데이터에 우선순위 데이터 존재여부
        /// </summary>
        /// <param name="exDt"></param>
        /// <param name="specDt"></param>
        private static void CheckHasPriority(DataTable exDt, DataTable specDt)
        {
            var exCount = exDt.AsEnumerable()
                .Where(r => !string.IsNullOrWhiteSpace(Format.GetString(r["QCGRADE"])) && string.IsNullOrWhiteSpace(Format.GetString(r["PRIORITY"])))
                .ToList().Count;

            var specCount = specDt.AsEnumerable()
                .Where(r => !string.IsNullOrWhiteSpace(Format.GetString(r["QCGRADE"])) && string.IsNullOrWhiteSpace(Format.GetString(r["PRIORITY"])))
                .ToList().Count;

            if (exCount > 0 || specCount > 0)
            {
                throw MessageException.Create("NoPriorityStandard");
            }
        }
    }
}
