#region using

#endregion

using System;
using System.Data;

namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// 프 로 그 램 명  : SPC Sigma 관련 항목 정의
    /// 업  무  설  명  : SPC Sigma 관련 항목 정의
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-17
    /// 수  정  이  력  : 
    /// 
    /// 2019-07-17  최초작성
    /// 
    /// </summary>
    public class SpcSigma
    {

    }

    /// <summary>
    /// Sigma 결과값 저장 
    /// </summary>
    public class SpcSigmaResult
    {
        public SpcCpkResult cpkResult = SpcCpkResult.Create();
        public DataTable dtCpkResult;
        public string subGroup;
        public string subGroupName;

        /// <summary>
        /// 평균
        /// </summary>
        public double XBAR;
        /// <summary>
        /// R, S, 합동. 저장.
        /// </summary>
        public double XRSP;

        /// <summary>
        /// 반올림 자리수 기본값 6.
        /// </summary>
        public int nSigmaRangeDigit;

        public double nSigmaWithin;
        public double nSigmaTotal;
        public double nSigmaWithinRound;
        public double nSigmaTotalRound;

        public double nSigma;
        public double nSigmaRound;

        public double nSigma1;
        public double nSigma2;
        public double nSigma3;
        public double nSigma1Round;
        public double nSigma2Round;
        public double nSigma3Round;

        public double nSigma1Max;
        public double nSigma2Max;
        public double nSigma3Max;
        public double nSigma1Min;
        public double nSigma2Min;
        public double nSigma3Min;

        
        public double nSigma1MaxRound;
        public double nSigma2MaxRound;
        public double nSigma3MaxRound;
        public double nSigma1MinRound;
        public double nSigma2MinRound;
        public double nSigma3MinRound;


        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static SpcSigmaResult Create()
        {
            return new SpcSigmaResult
            {
                dtCpkResult = CreateDataTableCpkResult(),

                nSigmaRangeDigit = 6,

                subGroup = "",
                subGroupName = "",

                XBAR = SpcLimit.MIN,
                XRSP = SpcLimit.MIN,

                nSigmaWithin = SpcLimit.MIN,
                nSigmaTotal = SpcLimit.MIN,
                nSigmaWithinRound = SpcLimit.MIN,
                nSigmaTotalRound = SpcLimit.MIN,

                nSigma = SpcLimit.MIN,
                nSigmaRound = SpcLimit.MIN,

                nSigma1 = SpcLimit.MIN,
                nSigma2 = SpcLimit.MIN,
                nSigma3 = SpcLimit.MIN,
                nSigma1Round = SpcLimit.MIN,
                nSigma2Round = SpcLimit.MIN,
                nSigma3Round = SpcLimit.MIN,

                nSigma1Max = SpcLimit.MIN,
                nSigma2Max = SpcLimit.MIN,
                nSigma3Max = SpcLimit.MIN,
                nSigma1Min = SpcLimit.MAX,
                nSigma2Min = SpcLimit.MAX,
                nSigma3Min = SpcLimit.MAX,
                nSigma1MaxRound = SpcLimit.MIN,
                nSigma2MaxRound = SpcLimit.MIN,
                nSigma3MaxRound = SpcLimit.MIN,
                nSigma1MinRound = SpcLimit.MAX,
                nSigma2MinRound = SpcLimit.MAX,
                nSigma3MinRound = SpcLimit.MAX

            };
        }


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public SpcSigmaResult CopyDeep()
        {
            SpcSigmaResult cs = SpcSigmaResult.Create();
            cs.cpkResult = cpkResult.CopyDeep();

            if (dtCpkResult != null)
            {
                foreach (DataRow rowData in dtCpkResult.Rows)
                {
                    cs.dtCpkResult.ImportRow(rowData);
                }
            }

            cs.subGroup = this.subGroup;
            cs.subGroupName = this.subGroupName;

            cs.XBAR = this.XBAR;
            cs.XRSP = this.XRSP;

            cs.nSigmaRangeDigit = nSigmaRangeDigit;

            cs.nSigmaWithin = this.nSigmaWithin;
            cs.nSigmaTotal = this.nSigmaTotal;
            cs.nSigmaWithinRound = this.nSigmaWithinRound;
            cs.nSigmaTotalRound = this.nSigmaTotalRound;

            cs.nSigma = this.nSigma;
            cs.nSigmaRound = this.nSigmaRound;
            
            cs.nSigma1 = this.nSigma1;
            cs.nSigma2 = this.nSigma2;
            cs.nSigma3 = this.nSigma3;
            cs.nSigma1Round = this.nSigma1Round;
            cs.nSigma2Round = this.nSigma2Round;
            cs.nSigma3Round = this.nSigma3Round;

            cs.nSigma1Max = this.nSigma1Max;
            cs.nSigma2Max = this.nSigma2Max;
            cs.nSigma3Max = this.nSigma3Max;
            cs.nSigma1Min = this.nSigma1Min;
            cs.nSigma2Min = this.nSigma2Min;
            cs.nSigma3Min = this.nSigma3Min;
            cs.nSigma1MaxRound = this.nSigma1MaxRound;
            cs.nSigma2MaxRound = this.nSigma2MaxRound;
            cs.nSigma3MaxRound = this.nSigma3MaxRound;
            cs.nSigma1MinRound = this.nSigma1MinRound;
            cs.nSigma2MinRound = this.nSigma2MinRound;
            cs.nSigma3MinRound = this.nSigma3MinRound;

            return cs;
        }

        /// <summary>
        /// Cpk 결과값 한행(1Raw) DataTable Set 으로 필드 구성.
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateDataTableCpkResult()
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "dtCpkResultSingle";
            dataTable.Columns.Add("SEQ", typeof(string));
            dataTable.Columns.Add("FIELDNAME", typeof(string));
            dataTable.Columns.Add("FIELDDATA", typeof(string));
            dataTable.Columns.Add("FIELDDETAILDATA", typeof(string));
            dataTable.Columns.Add("FIELDID", typeof(string));
            return dataTable;
        }
        /// <summary>
        /// Cpk 결과값 한행(1Raw)을 Data Table 형식으로 전이.
        /// </summary>
        /// <param name="result"></param>
        public static void CpkResultDataTableWrite(ref SpcSigmaResult result)
        {
            //상세 Popup에서 표시 할 Grid Cpk 결과값 구성
            //DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSEQNO", result.cpkResult.nSEQNO);
            //DataTableNewWrite(ref result.dtCpkResult, "01", "SPCGROUPID", result.cpkResult.nGROUPID);
            //DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSUBGROUP", result.cpkResult.SUBGROUP);
            //DataTableNewWrite(ref result.dtCpkResult, "01", "SPCEXTRAID", result.cpkResult.nEXTRAID);
            //DataTableNewWrite(ref result.dtCpkResult, "01", "SPCEXTRACONDITIONS", result.cpkResult.EXTRACONDITIONS);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCLSL",result.cpkResult.nLSL, "dbl", 3);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCCSL", result.cpkResult.nCSL, "dbl", 3);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCUSL", result.cpkResult.nUSL, "dbl", 3);
            //DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSPECMODE", result.cpkResult.SPECMODE, "str");
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSAMPLINGCOUNT", result.cpkResult.nSAMPLINGCOUNT);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSUBGROUPCOUNT", result.cpkResult.nSUBGROUPCOUNT);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPCOUNT", result.cpkResult.nPCOUNT);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCKCOUNT", result.cpkResult.nKCOUNT);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCISSAME", result.cpkResult.ISSAME);
            if (result.cpkResult.ISSAME)
            {
                DataTableNewWrite(ref result.dtCpkResult, "01", "SPCISSAMENAME", SpcLibMessage.common.comCpk1038);//같지않음.
            }
            else
            {
                DataTableNewWrite(ref result.dtCpkResult, "01", "SPCISSAMENAME", SpcLibMessage.common.comCpk1044);//같음.
            }
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCNVALUE_TOL", result.cpkResult.nNVALUE_TOL, "dbl", 3);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCNVALUE_AVG", result.cpkResult.nNVALUE_AVG, "dbl", 3);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSVALUE_AVG", result.cpkResult.nSVALUE_AVG, "dbl", 3);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSVALUE_RTD", result.cpkResult.nSVALUE_RTD, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSVALUE_RTDC4", result.cpkResult.nSVALUE_RTDC4, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSVALUE_STD", result.cpkResult.nSVALUE_STD, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSVALUE_STDC4", result.cpkResult.nSVALUE_STDC4, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSVALUE_PTD", result.cpkResult.nSVALUE_PTD, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSVALUE_PTDC4", result.cpkResult.nSVALUE_PTDC4, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCCP", result.cpkResult.nCP, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCCPL", result.cpkResult.nCPL, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCCPU", result.cpkResult.nCPU, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCCPK", result.cpkResult.nCPK, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCCPM", result.cpkResult.nCPM, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCJUDGMENTCPK", result.cpkResult.nJUDGMENTCPK);
            //DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPCISUBGROUP", result.cpkResult.nPCISUBGROUP);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPCI_D2", result.cpkResult.nPCI_d2, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPCI_C4S", result.cpkResult.nPCI_c4S, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPCI_C4C", result.cpkResult.nPCI_c4C, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPVALUE_AVG", result.cpkResult.nPVALUE_AVG, "dbl", 3);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPVALUE_STD", result.cpkResult.nPVALUE_STD, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPVALUE_STDC4", result.cpkResult.nPVALUE_STDC4, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPP", result.cpkResult.nPP, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPL", result.cpkResult.nPPL, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPU", result.cpkResult.nPPU, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPK", result.cpkResult.nPPK, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCJUDGMENTPPK", result.cpkResult.JUDGMENTPPK, "str");
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPI_C4", result.cpkResult.nPPI_c4, "dbl", 6);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCTAUSL", result.cpkResult.nTargetUSL, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCTACSL", result.cpkResult.nTargetCSL, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCTALSL", result.cpkResult.nTargetLSL, "dbl", 2);
            //*PPM Label
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMOBSERVEGROUP", "");
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMLSL", result.cpkResult.ppmObserveLSL, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMUSL", result.cpkResult.ppmObserveUSL, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMTOTAL", result.cpkResult.ppmObserveTOT, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMWITHINGROUP", "");
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMLSL", result.cpkResult.ppmWithinLSL, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMUSL", result.cpkResult.ppmWithinUSL, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMTOTAL", result.cpkResult.ppmWithinTOT, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMOVERALLGROUP", "");
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMLSL", result.cpkResult.ppmOverallLSL, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMUSL", result.cpkResult.ppmOverallUSL, "dbl", 2);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCPPMTOTAL", result.cpkResult.ppmOverallTOT, "dbl", 2);


            //Detail cpk raw 값 표시 처리
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSTATUS", result.cpkResult.nSTATUS, "long");
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCSTATUSMESSAGE", result.cpkResult.STATUSMESSAGE);
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCERRORNO", result.cpkResult.nERRORNO, "long");
            DataTableNewWrite(ref result.dtCpkResult, "01", "SPCERRORMESSAGE", result.cpkResult.ERRORMESSAGE);

        }

        /// <summary>
        /// Data Table Row 자료 입력
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="seq"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldData"></param>
        public static void DataTableNewWrite(ref DataTable dataTable, string seq, string fieldName, object fieldData, string type = "str", int digit = 3)
        {
            double dblData;
            double intData;
            object subData;

            try
            {
                try
                {
                    switch (type)
                    {
                        case "dbl":
                            dblData = (double)fieldData;
                            subData = Math.Round(dblData, digit);
                            break;
                        case "int":
                            intData = (int)fieldData;
                            subData = intData;
                            break;
                        case "long":
                            intData = (long)fieldData;
                            subData = intData;
                            break;
                        case "str":
                        default:
                            subData = fieldData;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    subData = fieldData;
                    Console.WriteLine(ex.Message);
                }        

                if(subData.ToSafeString() == SpcLimit.MAXTXT || subData.ToSafeString() == SpcLimit.MINTXT)
                {
                    subData = "";
                    fieldData = "";
                }

                string fieldNameValue = SpcDictionary.read(SpcDicClassId.GRID, fieldName);
                DataRow dataRow = dataTable.NewRow();
                dataRow["SEQ"] = seq;
                dataRow["FIELDNAME"] = fieldNameValue;
                dataRow["FIELDDATA"] = subData.ToSafeString();
                dataRow["FIELDDETAILDATA"] = fieldData.ToSafeString();
                dataRow["FIELDID"] = fieldName;

                dataTable.Rows.Add(dataRow);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

    /// <summary>
    /// 공정능력 결과값 저장 
    /// </summary>
    public class SpcCpkResult
    {
        public long nSEQNO;
        public long nGROUPID;
        public string SUBGROUP;
        public long nEXTRAID;
        public string EXTRACONDITIONS;
        public double nLSL;
        public double nCSL;
        public double nUSL;
        public string SPECMODE;
        public long nSAMPLINGCOUNT;
        public long nSUBGROUPCOUNT;
        public long nPCOUNT;
        public long nKCOUNT;
        public double nNVALUE_TOL;
        public double nNVALUE_AVG;
        public double nSVALUE_AVG;
        /// <summary>
        /// Sampling 개수가 같지 않으면 true
        /// </summary>
        public bool ISSAME;
        /// <summary>
        /// R 표준편차 추정치 미사용.
        /// </summary>
        public double nSVALUE_RTD;
        /// <summary>
        /// R 표준편차 추정치 사용.
        /// </summary>
        public double nSVALUE_RTDC4;
        /// <summary>
        /// S 표준편차 추정치 미사용.
        /// </summary>
        public double nSVALUE_STD;
        /// <summary>
        /// S 표준편차 추정치 사용.
        /// </summary>
        public double nSVALUE_STDC4;
        /// <summary>
        /// 합동 표준편차 추정치 미사용. 
        /// </summary>
        public double nSVALUE_PTD;
        /// <summary>
        /// 합동 표준편차 추정치 사용.
        /// </summary>
        public double nSVALUE_PTDC4;
        public double nCP;
        public double nCPL;
        public double nCPU;
        public double nCPK;
        public double nCPM;
        public string nJUDGMENTCPK;
        public string nPCISUBGROUP;
        public double nPCI_d2;
        public double nPCI_c4S;
        public double nPCI_c4C;
        public double nPVALUE_AVG;
        /// <summary>
        /// Ppk 표준편차 추정치 미사용.
        /// </summary>
        public double nPVALUE_STD;
        /// <summary>
        /// Ppk 표준편차 추정치 사용.
        /// </summary>
        public double nPVALUE_STDC4;
        public double nPP;
        public double nPPL;
        public double nPPU;
        public double nPPK;
        public double nPPM;
        public string JUDGMENTPPK;
        public double nPPI_c4;
        /// <summary>
        /// 중심치 이탈 하한.
        /// </summary>
        public double nTargetLSL;
        /// <summary>
        /// 목표값.
        /// </summary>
        public double nTargetCSL;
        /// <summary>
        /// 중심치 이탈 상한.
        /// </summary>
        public double nTargetUSL;

        //PPM
        /// <summary>
        /// 기대성능(군내) PPM 규격 하한)
        /// </summary>
        public double ppmWithinLSL;
        /// <summary>
        /// 기대성능(군내) PPM 규격 상한)
        /// </summary>
        public double ppmWithinUSL;
        /// <summary>
        /// 기대성능(군내) PPM 규격 총계)
        /// </summary>
        public double ppmWithinTOT;
        /// <summary>
        /// 기대성능(전체) PPM 규격 하한)
        /// </summary>
        public double ppmOverallLSL;
        /// <summary>
        /// 기대성능(전체) PPM 규격 상한)
        /// </summary>
        public double ppmOverallUSL;
        /// <summary>
        /// 기대성능(전체) PPM 규격 총계)
        /// </summary>
        public double ppmOverallTOT;
        /// <summary>
        /// 관측성 PPM 규격 하한 값)
        /// </summary>
        public double ppmObserveLSLN;
        /// <summary>
        /// 관측성 PPM 규격 상한 값)
        /// </summary>
        public double ppmObserveUSLN;
        /// 관측성 PPM 규격 하한)
        public double ppmObserveLSL;
        /// 관측성 PPM 규격 상한)
        public double ppmObserveUSL;
        /// 관측성 PPM 규격 총계)
        public double ppmObserveTOT;

        /// <summary>
        /// 상태
        /// </summary>
        public long nSTATUS;
        public string STATUSMESSAGE;
        public long nERRORNO;
        public string ERRORMESSAGE;

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static SpcCpkResult Create()
        {
            return new SpcCpkResult
            {
                nSEQNO = 0,
                nGROUPID = 0,
                SUBGROUP = "",
                nEXTRAID = 0,
                EXTRACONDITIONS = "",
                nLSL = SpcLimit.MAX,
                nCSL = SpcLimit.MAX,
                nUSL = SpcLimit.MAX,
                SPECMODE = "",
                nSAMPLINGCOUNT = 0,
                nSUBGROUPCOUNT = 0,
                nPCOUNT = 0,
                nKCOUNT = 0,
                ISSAME = false,
                nNVALUE_TOL = SpcLimit.MAX,
                nNVALUE_AVG = SpcLimit.MAX,
                nSVALUE_AVG = SpcLimit.MAX,
                nSVALUE_RTD = SpcLimit.MAX,
                nSVALUE_RTDC4 = SpcLimit.MAX,
                nSVALUE_STD = SpcLimit.MAX,
                nSVALUE_STDC4 = SpcLimit.MAX,
                nSVALUE_PTD = SpcLimit.MAX,
                nSVALUE_PTDC4 = SpcLimit.MAX,
                nCP = SpcLimit.MAX,
                nCPL = SpcLimit.MAX,
                nCPU = SpcLimit.MAX,
                nCPK = SpcLimit.MAX,
                nCPM = SpcLimit.MAX,
                nJUDGMENTCPK = "",
                nPCISUBGROUP = "",
                nPCI_d2 = SpcLimit.MAX,
                nPCI_c4S = SpcLimit.MAX,
                nPCI_c4C = SpcLimit.MAX,
                nPVALUE_AVG = SpcLimit.MAX,
                nPVALUE_STD = SpcLimit.MAX,
                nPVALUE_STDC4 = SpcLimit.MAX,
                nPP = SpcLimit.MAX,
                nPPL = SpcLimit.MAX,
                nPPU = SpcLimit.MAX,
                nPPK = SpcLimit.MAX,
                nPPM = SpcLimit.MAX,
                JUDGMENTPPK = "",
                nPPI_c4 = SpcLimit.MAX,

                nTargetLSL = SpcLimit.MIN,
                nTargetCSL = SpcLimit.MIN,
                nTargetUSL = SpcLimit.MIN,

                ppmWithinLSL = SpcLimit.MIN,
                ppmWithinUSL = SpcLimit.MIN,
                ppmWithinTOT = SpcLimit.MIN,
                ppmOverallLSL = SpcLimit.MIN,
                ppmOverallUSL = SpcLimit.MIN,
                ppmOverallTOT = SpcLimit.MIN,
                ppmObserveLSLN = SpcLimit.MIN,
                ppmObserveUSLN = SpcLimit.MIN,
                ppmObserveLSL = SpcLimit.MIN,
                ppmObserveUSL = SpcLimit.MIN,
                ppmObserveTOT = SpcLimit.MIN,

                nSTATUS = 0,
                STATUSMESSAGE = "",
                nERRORNO = 0,
                ERRORMESSAGE = ""
            };
        }


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public SpcCpkResult CopyDeep()
        {
            SpcCpkResult cs = SpcCpkResult.Create();
            cs.nSEQNO = this.nSEQNO;
            cs.nGROUPID = this.nGROUPID;
            cs.SUBGROUP = this.SUBGROUP;
            cs.nEXTRAID = this.nEXTRAID;
            cs.EXTRACONDITIONS = this.EXTRACONDITIONS;
            cs.nLSL = this.nLSL;
            cs.nCSL = this.nCSL;
            cs.nUSL = this.nUSL;
            cs.SPECMODE = this.SPECMODE;
            cs.nSAMPLINGCOUNT = this.nSAMPLINGCOUNT;
            cs.nSUBGROUPCOUNT = this.nSUBGROUPCOUNT;
            cs.nPCOUNT = this.nPCOUNT;
            cs.nKCOUNT = this.nKCOUNT;
            cs.ISSAME = this.ISSAME;
            cs.nNVALUE_TOL = this.nNVALUE_TOL;
            cs.nNVALUE_AVG = this.nNVALUE_AVG;
            cs.nSVALUE_AVG = this.nSVALUE_AVG;
            cs.nSVALUE_RTD = this.nSVALUE_RTD;
            cs.nSVALUE_RTDC4 = this.nSVALUE_RTDC4;
            cs.nSVALUE_STD = this.nSVALUE_STD;
            cs.nSVALUE_STDC4 = this.nSVALUE_STDC4;
            cs.nSVALUE_PTD = this.nSVALUE_PTD;
            cs.nSVALUE_PTDC4 = this.nSVALUE_PTDC4;
            cs.nCP = this.nCP;
            cs.nCPL = this.nCPL;
            cs.nCPU = this.nCPU;
            cs.nCPK = this.nCPK;
            cs.nCPM = this.nCPM;
            cs.nJUDGMENTCPK = this.nJUDGMENTCPK;
            cs.nPCISUBGROUP = this.nPCISUBGROUP;
            cs.nPCI_d2 = this.nPCI_d2;
            cs.nPCI_c4S = this.nPCI_c4S;
            cs.nPCI_c4C = this.nPCI_c4C;
            cs.nPVALUE_AVG = this.nPVALUE_AVG;
            cs.nPVALUE_STD = this.nPVALUE_STD;
            cs.nPVALUE_STDC4 = this.nPVALUE_STDC4;
            cs.nPP = this.nPP;
            cs.nPPL = this.nPPL;
            cs.nPPU = this.nPPU;
            cs.nPPK = this.nPPK;
            cs.nPPM = this.nPPM;
            cs.JUDGMENTPPK = this.JUDGMENTPPK;
            cs.nPPI_c4 = this.nPPI_c4;

            cs.nTargetLSL = this.nTargetLSL;
            cs.nTargetCSL = this.nTargetCSL;
            cs.nTargetUSL = this.nTargetUSL;

            cs.ppmWithinLSL = this.ppmWithinLSL;
            cs.ppmWithinUSL = this.ppmWithinUSL;
            cs.ppmWithinTOT = this.ppmWithinTOT;
            cs.ppmOverallLSL = this.ppmOverallLSL;
            cs.ppmOverallUSL = this.ppmOverallUSL;
            cs.ppmOverallTOT = this.ppmOverallTOT;
            cs.ppmObserveLSLN = this.ppmObserveLSLN;
            cs.ppmObserveUSLN = this.ppmObserveUSLN;
            cs.ppmObserveLSL = this.ppmObserveLSL;
            cs.ppmObserveUSL = this.ppmObserveUSL;
            cs.ppmObserveTOT = this.ppmObserveTOT;

            cs.nSTATUS = this.nSTATUS;
            cs.STATUSMESSAGE = this.STATUSMESSAGE;
            cs.nERRORNO = this.nERRORNO;
            cs.ERRORMESSAGE = this.ERRORMESSAGE;

            return cs;
        }


    }
}
