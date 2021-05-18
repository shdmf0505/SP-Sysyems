#region using

using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;
using static Micube.SmartMES.Commons.SPCLibrary.SpcDataTable;

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Micube.SmartMES.Commons.SPCLibrary;

#endregion

namespace Micube.SmartMES.Commons.SPCLibrary
{

    /// <summary>
    /// 프 로 그 램 명  : SPC 통계 Library
    /// 업  무  설  명  : SPC 관리도 및 공정능력 분석.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-12-23
    /// 수  정  이  력  : 
    /// 2019-12-23  합동 Sigma 수정.
    /// 2019-07-17  통계분석 다중처리 추가
    /// 2019-07-15  SPCOutData 추가
    /// 2019-05-30  최초작성
    /// </summary>



    /// <summary>
    /// 통계분석용 입력 자료 Class
    /// </summary>
    public class AnalysisExecutionParameter
    {
        public bool isAgainAnalysis;
        public bool isAgainAnalysisXBar;
        public bool isAgainAnalysisCpk;
        public bool isAgainAnalysisButtonXBar;
        public bool isAgainAnalysisButtonCpk;
        public bool isAgainAnalysisOverRules;

        public SigmaType AgainAnalysisSigmaType;
        /// <summary>
        /// DB의 측정값입력 필수 필드명.
        /// </summary>
        public AnalysisBasicTableFiledName dtInputRawMainFieldName;
        /// <summary>
        /// DB의 측정값입력
        /// </summary>
        public DataTable dtInputRawData;
        /// <summary>
        /// DB의 Subgroup별 SPEC 값 들을 입력
        /// </summary>
        public DataTable dtInputSpecData;
        /// <summary>
        /// 관리도 및 공정능력 분석용 통계 Chart 처리 Opton 입력
        /// </summary>
        public SPCOption spcOption;
        public static AnalysisExecutionParameter Create()
        {
            AnalysisExecutionParameter c = new AnalysisExecutionParameter();
            c.spcOption = SPCOption.Create();
            c.dtInputRawMainFieldName = AnalysisBasicTableFiledName.Create();
            return c;
        }
    }




    /// <summary>
    /// 분석용 원본 자료의 필드명 설정.
    /// </summary>
    public class AnalysisBasicTableFiledName
    {
        /// <summary>
        /// 서브그룹 
        /// </summary>
        public string subgroupNameFiled;
        /// <summary>
        /// 서브그룹명
        /// </summary>
        public string subgroupNameFiledName;
        /// <summary>
        /// 서브그룹명 01 - 예비 1
        /// </summary>
        public string subgroupNameFiledName01;

        /// <summary>
        /// 시료
        /// </summary>
        public string samplingNameFiled;
        /// <summary>
        /// 시료명
        /// </summary>
        public string samplingNameFiledName;

        /// <summary>
        /// 시료 예비 01 - IMR Mode의 Raw Data 표시용으로 주로 사용.
        /// </summary>
        public string samplingNameFiled01;
        /// <summary>
        /// 시료명 예비 01 - IMR Mode의 Raw Data 표시용으로 주로 사용.
        /// </summary>
        public string samplingNameFiledName01;

        /// <summary>
        /// 시료 예비 02
        /// </summary>
        public string samplingNameFiled02;
        /// <summary>
        /// 시료명 예비 02
        /// </summary>
        public string samplingNameFiledName02;
        /// <summary>
        /// 측정값 및 불량수량
        /// </summary>
        public string nValue;
        /// <summary>
        /// 검사수량 및 투입수량 (P, NP, C, U 에서 주로 사용)
        /// </summary>
        public string nSubValue;

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static AnalysisBasicTableFiledName Create()
        {
            AnalysisBasicTableFiledName c = new AnalysisBasicTableFiledName();
            c.Clear(ref c);
            return c;
        }

        /// <summary>
        /// 초기화
        /// </summary>
        /// <param name="data"></param>
        public void Clear(ref AnalysisBasicTableFiledName data)
        {
            data.subgroupNameFiled = "SUBGROUP";
            data.subgroupNameFiledName = "SUBGROUPNAME";
            data.subgroupNameFiledName01 = "";

            data.samplingNameFiled = "SAMPLING";
            data.samplingNameFiledName = "SAMPLINGNAME";

            data.samplingNameFiled01 = "";
            data.samplingNameFiledName01 = "";
            data.samplingNameFiled02 = "";
            data.samplingNameFiledName02 = "";

            data.nValue = "NVALUE";
            data.nSubValue = "NSUBVALUE";
        }

        /// <summary>
        /// 초기화 후 기본 값 입력.
        /// </summary>
        /// <param name="data"></param>
        public void ClearDefault(ref AnalysisBasicTableFiledName data)
        {
            data.subgroupNameFiled = "SUBGROUP";
            data.subgroupNameFiledName = "SUBGROUPNAME";
            data.subgroupNameFiledName01 = "";

            data.samplingNameFiled = "SAMPLING";
            data.samplingNameFiledName = "SAMPLINGNAME";

            data.samplingNameFiled01 = "SAMPLING";
            data.samplingNameFiledName01 = "SAMPLINGNAME";
            data.samplingNameFiled02 = "SAMPLING";
            data.samplingNameFiledName02 = "SAMPLINGNAME";

            data.nValue = "NVALUE";
            data.nSubValue = "NSUBVALUE";
        }
    }

    /// <summary>
    /// SPC 통계 - 관리도 알고리즘 분석 Option
    /// </summary>
    public class SPCOption
    {
        /// <summary>
        /// DB에 저장된 기본 Chart 구분
        /// </summary>
        public string specDefaultChartType;
        /// <summary>
        /// 관리도 Type
        /// </summary>
        public ControlChartType chartType;
        /// <summary>
        /// Chart Panel의 타이틀 정의.
        /// </summary>
        public ControlChartName chartName;
        /// <summary>
        /// 추정치 사용여부
        /// </summary>
        public SigmaType sigmaType;
        /// <summary>
        /// 관리선 구분
        /// </summary>
        public LimitType limitType;
        /// <summary>
        /// 관리선 구분 - 직접 입력 Chart Index 입력.
        /// </summary>
        public bool[] LimitTypeUseIndex;
        /// <summary>
        /// Histogram 분석 집계값 저장.
        /// </summary>
        public HistogramAnalysisTotalize CpkTotalize;
        /// <summary>
        /// Sampling Count가 같지 않을때 true;
        /// </summary>
        public bool isSame;
        /// <summary>
        /// Sampling Count가 같지 않을때의 Sigma 값;
        /// </summary>
        public double SameSigma;
        /// <summary>
        /// 사용자 직접입력 Spec 입력시 사용될 CPK용 값.
        /// </summary>
        public SpcSpec DirectModeCpkSpec;

        /// <summary>
        /// 초기화 ----------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public static SPCOption Create()
        {
            SPCOption c = new SPCOption();
            c.chartType = ControlChartType.XBar_R;
            c.sigmaType = SigmaType.Yes;
            c.limitType = LimitType.Management;
            c.LimitTypeUseIndex = new bool[5];
            for (int i = 0; i < c.LimitTypeUseIndex.Length; i++)
            {
                c.LimitTypeUseIndex[i] = false;
            }
            c.chartName = new ControlChartName();
            c.chartName.SerialCaption = ControlChartSerialCaption.Create();
            c.CpkTotalize = HistogramAnalysisTotalize.Create();
            c.chartName.xBarChartType = "";
            c.chartName.xCpkChartType = "";
            c.chartName.mainTitle = "";
            c.chartName.subTitle = "";
            c.chartName.mainCSL = "";
            c.chartName.subCCL = "";
            c.isSame = false;
            c.SameSigma = SpcLimit.MIN;

            c.DirectModeCpkSpec = SpcSpec.Create();
            return c;
        }

        /// <summary>
        /// spcOption.chartType 값 설정.
        /// </summary>
        /// <param name="chartType"></param>
        /// <param name="spcOption"></param>
        public void ChartTypeSetting(string chartType, ref SPCOption spcOption)
        {
            switch (chartType)
            {
                case "XBARR":
                case "R":
                    spcOption.chartType = ControlChartType.XBar_R;
                    break;
                case "XBARS":
                case "S":
                    spcOption.chartType = ControlChartType.XBar_S;
                    break;
                case "IMR":
                case "MR":
                case "I":
                    spcOption.chartType = ControlChartType.I_MR;
                    break;
                case "XBARP":
                case "PL":
                    spcOption.chartType = ControlChartType.Merger;
                    break;
                case "P":
                case "NP":
                case "C":
                case "U":
                    spcOption.chartType = ControlChartType.p;
                    break;
            }
        }

        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public SPCOption CopyDeep()
        {
            SPCOption cs = SPCOption.Create();
            cs.specDefaultChartType = this.specDefaultChartType;
            cs.chartType = this.chartType;
            cs.sigmaType = this.sigmaType;
            cs.limitType = this.limitType;
            cs.chartName = this.chartName.CopyDeep();
            if (this.LimitTypeUseIndex != null)
            {
                for (int i = 0; i < this.LimitTypeUseIndex.Length; i++)
                {
                    cs.LimitTypeUseIndex[i] = this.LimitTypeUseIndex[i];
                }
            }
            cs.CpkTotalize = CpkTotalize.CopyDeep();
            cs.isSame = this.isSame;
            cs.SameSigma = this.SameSigma;
            cs.DirectModeCpkSpec = this.DirectModeCpkSpec.CopyDeep();
            return cs;
        }


    }

    /// <summary>
    /// Chart Panel의 타이틀 정의.
    /// </summary>
    public class ControlChartName
    {
        public ControlChartSerialCaption SerialCaption;
        public string keySubGroup;
        public string keySubGroupName;
        public string keySampling;
        public string keySamplingName;
        public string xBarChartType;
        public string xCpkChartType;
        public string mainTitle;
        public string subTitle;
        public string mainCSL;
        public string subCCL;


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public ControlChartName CopyDeep()
        {
            ControlChartName cs = new ControlChartName();
            
            cs.SerialCaption = this.SerialCaption.CopyDeep();
            cs.keySubGroup = this.keySubGroup;
            cs.keySubGroupName = this.keySubGroupName;
            cs.keySampling = this.keySampling;
            cs.keySamplingName = this.keySamplingName;
            cs.xBarChartType = this.xBarChartType;
            cs.xCpkChartType = this.xCpkChartType;
            cs.mainTitle = this.mainTitle;
            cs.subTitle = this.subTitle;
            cs.mainCSL = this.mainCSL;
            cs.subCCL = this.subCCL;

            return cs;
        }
    }

    /// <summary>
    /// Chart Serial 속성
    /// </summary>
    public class ControlChartSerialCaption
    {
        public ControlChartSerialAttribute AUCL;
        public ControlChartSerialAttribute ALCL;
        public ControlChartSerialAttribute NVALUE;

        public ControlChartSerialAttribute RAUCL;
        public ControlChartSerialAttribute RALCL;
        public ControlChartSerialAttribute RVALUE;

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static ControlChartSerialCaption Create()
        {
            ControlChartSerialCaption c = new ControlChartSerialCaption();
            c.AUCL = new ControlChartSerialAttribute();
            c.ALCL = new ControlChartSerialAttribute();
            c.NVALUE = new ControlChartSerialAttribute();
            c.RAUCL = new ControlChartSerialAttribute();
            c.RALCL = new ControlChartSerialAttribute();
            c.RVALUE = new ControlChartSerialAttribute();
            return c;
        }

        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public ControlChartSerialCaption CopyDeep()
        {
            ControlChartSerialCaption cs = ControlChartSerialCaption.Create();
            cs.AUCL = this.AUCL.CopyDeep();
            cs.ALCL = this.ALCL.CopyDeep();
            cs.NVALUE = this.NVALUE.CopyDeep();

            cs.RAUCL = this.RAUCL.CopyDeep();
            cs.RALCL = this.RALCL.CopyDeep();
            cs.RVALUE = this.RVALUE.CopyDeep();
            return cs;
        }

    }

    /// <summary>
    /// Chart Serial 속성
    /// </summary>
    public class ControlChartSerialAttribute
    {
        public string toolTipName;
        public string toolTipValueType;
        public string toolTipValueFormat;


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public ControlChartSerialAttribute CopyDeep()
        {
            ControlChartSerialAttribute cs = new ControlChartSerialAttribute();
            cs.toolTipName = this.toolTipName;
            cs.toolTipValueType = this.toolTipValueType;
            cs.toolTipValueFormat = this.toolTipValueFormat;
            return cs;
        }
    }

    /// <summary>
    /// SPC 통계 - 관리도 알고리즘 입력자료 구조 Class
    /// </summary>
    public class SPCPara
    {
        /// <summary>
        /// 생성 Chart User Control 고유 ID.
        /// </summary>
        public int ChartID;
        /// <summary>
        /// 입력 자료 DataSet
        /// </summary>
        public ParPIDataTable InputData;
        /// <summary>
        /// 입력 자료 DataSet Sum형.
        /// </summary>
        public ParPIDataTable InputDataSum;
        /// <summary>
        /// 입력자료 DataSet 분석용.
        /// </summary>
        public ParPIDataTable AnalysisInputData;
        /// <summary>
        /// 입력 자료 Subgroup Spec DataSet
        /// </summary>
        public ParPISpecDataTable InputSpec;
        /// <summary>
        /// DB 조회 Raw Data 테이블
        /// </summary>
        public DataTable tableRawData;
        /// <summary>
        /// SPC Subgroup별 Spec 테이블.
        /// </summary>
        public DataTable tableSubgroupSpec;
        /// <summary>
        /// 공정능력 분석용 임시 저장 SPEC
        /// </summary>
        public SpcSpec cpkTempSpec;

        /// <summary>
        /// 서브그룹 ID
        /// </summary>
        public string subGrouopID;
        
        public double? UOL;
        public double? LOL;

        public double? LSL;
        public double? CSL;
        public double? USL;

        public double? LCL;
        public double? CCL;
        public double? UCL;

        public double? RUCL;
        public double? RCCL;
        public double? RLCL;
        public double? RUOL;
        public double? RLOL;

        public double returnValueXBar;
        public double returnValueXBarSigmar;

        /// <summary>
        /// 관리도 분석 통계값.
        /// </summary>
        public ReturnXBarResult rtnXBar;

        /// <summary>
        /// Spec Option.
        /// </summary>
        public SPCOption spcOption;
        /// <summary>
        /// XBar 통계분석 상태 Message
        /// </summary>
        public SPCOutData xBarOutData;
        /// <summary>
        /// 공정능력 통계분석 상태 Message
        /// </summary>
        public SPCOutData cpkOutData;
        /// <summary>
        /// 사용 SPEC 구분 : 0-SPEC, 1-Control Limit 사용(입력된 LSL, USL이 Type 구분함)
        /// </summary>
        public int SpecMode = 0;

        /// <summary>
        /// 프로시져 내부의 처리 값을 표시 할 지 여부 : 표시-Y / 미표시-N
        /// </summary>
        public string LogMode = "N";

        /// <summary>
        /// 불편화 상수를 사용하여 공정능력을 계산 할 지 여부: 사용-Y / 미사용-N
        /// </summary>
        public SigmaType isUnbiasing;

        /// <summary>
        /// Chart Type Main
        /// </summary>
        private string _ChartTypeMain = "";
        /// <summary>
        /// Chart Type Sub
        /// </summary>
        private string _ChartTypeSub = "";
        /// <summary>
        /// Chart Panes 표시 개수
        /// </summary>
        private int _ChartPanesCount = 0;
        


        /// <summary>
        /// Chart Type 입력
        /// </summary>
        /// <param name="value"></param>
        public void ChartTypeMain(string value)
        {
            _ChartTypeMain = value;
            switch (_ChartTypeMain)
            {
                case "XBARR":
                    _ChartTypeSub = "R";
                    _ChartPanesCount = 2;
                    break;
                case "XBARS":
                    _ChartTypeSub = "S";
                    _ChartPanesCount = 2;
                    break;
                case "XBARP":
                    _ChartTypeSub = "P";
                    _ChartPanesCount = 2;
                    break;
                case "IMR":
                case "I":
                case "MR":
                    _ChartTypeSub = "MR";
                    _ChartPanesCount = 2;
                    break;
                case "P":
                case "NP":
                case "C":
                case "U":
                    _ChartTypeSub = "";
                    _ChartPanesCount = 1;
                    break;
                default:
                    _ChartTypeSub = "";
                    _ChartPanesCount = 1;
                    break;
            }
        }
        /// <summary>
        /// Chart Main Type 읽기
        /// </summary>
        /// <returns></returns>
        public string ChartTypeMain()
        {
            return _ChartTypeMain;
        }

        /// <summary>
        /// Chart PanesCount 읽기
        /// </summary>
        /// <returns></returns>
        public int ChartPanesCount()
        {
            return _ChartPanesCount;
        }

        /// <summary>
        /// Chart Sub Type 읽기
        /// </summary>
        /// <returns></returns>
        public string ChartTypeSub()
        {
            return _ChartTypeSub;
        }
        /// <summary>
        /// tableSubgroupSpec Table 초기화
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable tableSubgroupSpecCreate(string tableName)
        {
            tableName = string.Format("spcSubgroupSpec_{0}", tableName);
            DataTable c = SpcDataTable.CreateTableSubgroupSpec(tableName);
            return c;
        }


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public SPCPara CopyDeep()
        {
            SPCPara cs = new SPCPara();
            cs.ChartID = this.ChartID;
            cs.InputData = new ParPIDataTable();
            cs.InputDataSum = new ParPIDataTable();

            if (this.InputData != null)
            {
                foreach (DataRow rowData in this.InputData.Rows)
                {
                    cs.InputData.ImportRow(rowData);
                }
            }

            if (this.InputDataSum != null)
            {
                foreach (DataRow rowData in this.InputDataSum.Rows)
                {
                    cs.InputDataSum.ImportRow(rowData);
                }
            }

            cs.AnalysisInputData = new ParPIDataTable();
            if (this.AnalysisInputData != null)
            {
                foreach (DataRow rowData in this.AnalysisInputData.Rows)
                {
                    cs.AnalysisInputData.ImportRow(rowData);
                }
            }

            cs.InputSpec = new ParPISpecDataTable();
            if (this.InputSpec != null)
            {
                foreach (DataRow rowData in this.InputSpec.Rows)
                {
                    cs.InputSpec.ImportRow(rowData);
                }
            }
            cs.tableRawData = new DataTable();
            if (this.tableRawData != null)
            {
                foreach (DataRow rowData in this.tableRawData.Rows)
                {
                    cs.tableRawData.ImportRow(rowData);
                }
            }
            cs.tableSubgroupSpec = new DataTable();
            if (this.tableSubgroupSpec != null)
            {
                foreach (DataRow rowData in this.tableSubgroupSpec.Rows)
                {
                    cs.tableSubgroupSpec.ImportRow(rowData);
                }
            }

            if (cpkTempSpec != null)
            {
                cs.cpkTempSpec = this.cpkTempSpec.CopyDeep();
            }

            cs.subGrouopID = this.subGrouopID;
            cs.UOL = this.UOL;
            cs.LOL = this.LOL;
            cs.LSL = this.LSL;
            cs.CSL = this.CSL;
            cs.USL = this.USL;
            cs.LCL = this.LCL;
            cs.CCL = this.CCL;
            cs.UCL = this.UCL;
            cs.RUCL = this.RUCL;
            cs.RCCL = this.RCCL;
            cs.RLCL = this.RLCL;
            cs.RUOL = this.RUOL;
            cs.RLOL = this.RLOL;

            if (this.rtnXBar != null)
            {
                cs.rtnXBar = this.rtnXBar.CopyDeep();
            }

            if (this.spcOption != null)
            {
                cs.spcOption = this.spcOption.CopyDeep();
            }

            if (this.xBarOutData != null)
            {
                cs.xBarOutData = this.xBarOutData.CopyDeep();
            }
            if (this.cpkOutData != null)
            {
                cs.cpkOutData = this.cpkOutData.CopyDeep();
            }
            cs.SpecMode = this.SpecMode;
            cs.LogMode = this.LogMode;
            cs.isUnbiasing = this.isUnbiasing;
            cs._ChartTypeMain = this._ChartTypeMain;
            cs._ChartTypeSub = this._ChartTypeSub;
            cs._ChartPanesCount = this._ChartPanesCount;
            return cs;
        }
    }
    /// <summary>
    /// 관리도 XBAR 분석 통계값 저장.
    /// </summary>
    public class ReturnXBarResult
    {

        /// <summary>
        /// 관리도 Sigmar
        /// </summary>
        public double XBarSigma;
        
        public double uslSigma3;
        public double uslSigma2;
        public double uslSigma1;
        public double lslSigma3;
        public double lslSigma2;
        public double lslSigma1;
        /// <summary>
        /// 평균
        /// </summary>
        public double XBAR;
        /// <summary>
        /// R, S, 합동. 저장.
        /// </summary>
        public double XRSP;

        public double UCL;
        public double LCL;
        public double RUCL;
        public double RLCL;

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static ReturnXBarResult Create()
        {
            ReturnXBarResult cr = new ReturnXBarResult();
            cr.XBarSigma = SpcLimit.MIN;
            cr.uslSigma3 = SpcLimit.MIN;
            cr.uslSigma2 = SpcLimit.MIN;
            cr.uslSigma1 = SpcLimit.MIN;
            cr.lslSigma3 = SpcLimit.MIN;
            cr.lslSigma2 = SpcLimit.MIN;
            cr.lslSigma1 = SpcLimit.MIN;

            cr.XBAR = SpcLimit.MIN;
            cr.XRSP = SpcLimit.MIN;
            cr.UCL = SpcLimit.MIN;
            cr.LCL = SpcLimit.MIN;
            cr.RUCL = SpcLimit.MIN;
            cr.RLCL = SpcLimit.MIN;
            return cr;
        }
        /// <summary>
        /// 클래스 복사.
        /// </summary>
        /// <returns></returns>
        public ReturnXBarResult CopyDeep()
        {
            ReturnXBarResult cr = ReturnXBarResult.Create();
            cr.XBarSigma =  this.XBarSigma;
            cr.uslSigma3 = this.uslSigma3;
            cr.uslSigma2 = this.uslSigma2;
            cr.uslSigma1 = this.uslSigma1;
            cr.lslSigma3 = this.lslSigma3;
            cr.lslSigma2 = this.lslSigma2;
            cr.lslSigma1 = this.lslSigma1;

            cr.XBAR = this.XBAR;
            cr.XRSP = this.XRSP;
            cr.UCL =  this.UCL ;
            cr.LCL =  this.LCL ;
            cr.RUCL = this.RUCL;
            cr.RLCL = this.RLCL;

            return cr;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class RetrunPpkDatas
    {
        public long GROUPID;
        public string SUBGROUP;
        public long SAMPLEID;
        public string SAMPLING;
        public double PCI_VARIANCE;
        public double PCI_VARIANCE_STDEV;
        public double PPI_VARIANCE;
        public long NN;
        public long SNN;
    }

    /// <summary>
    /// 
    /// </summary>
    public class RetrunPpkDatasTotal
    {
        public long GROUPID;
        public long GROUPCOUNT;
        public string SUBGROUP;
        public long SAMPLEID;
        public string SAMPLING;
        public double PCI_TOTVARIANCE;
        public double PCI_TOTVARIANCE_STDEV;
        public double PPI_TOTVARIANCE;
        public long MaxNN;
        public long SumSNN;
    }

    /// <summary>
    /// SPC 통계 분석값 반환 Class
    /// </summary>
    public class SPCOutData
    {
        /// <summary>
        /// 메세지 표시 여부 
        /// </summary>
        public int RESULTFLAG;

        /// <summary>
        /// 실행 상태 번호
        /// </summary>
        public int STATUS;

        /// <summary>
        /// 실행 상태 Message
        /// </summary>
        public string STATUSMESSAGE;
        /// <summary>
        /// 오류 값
        /// </summary>
        public int HRESULT;
        /// <summary>
        /// 오류 번호
        /// </summary>
        public int ERRORNO;

        /// <summary>
        /// 오류 Message
        /// </summary>
        public string ERRORMESSAGE;

        /// <summary>
        /// Subgroup 통계 반환
        /// </summary>
        public SpcDataTable spcDataTable;

        /// <summary>
        /// Subgroup별 
        /// </summary>
        public List<SpcStatusMessage> subgroupStatus;
        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static SPCOutData Create()
        {
            SPCOutData c = new SPCOutData();
            c.spcDataTable = SpcDataTable.Create();
            c.subgroupStatus = new List<SpcStatusMessage>();
            return c;
        }


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public SPCOutData CopyDeep()
        {
            SPCOutData cs = SPCOutData.Create();
            cs.RESULTFLAG = this.RESULTFLAG;
            cs.STATUS = this.STATUS;
            cs.STATUSMESSAGE = this.STATUSMESSAGE;
            cs.HRESULT = this.HRESULT;
            cs.ERRORNO = this.ERRORNO;
            cs.ERRORMESSAGE = this.ERRORMESSAGE;
            cs.spcDataTable = spcDataTable.CopyDeep();
            foreach (SpcStatusMessage item in this.subgroupStatus)
            {
                cs.subgroupStatus.Add(item);
            }
            return cs;
        }

        /// <summary>
        /// Message Clear 8/22
        /// </summary>
        public void MessageClear()
        {
            RESULTFLAG = 0;
            STATUS = 0;
            STATUSMESSAGE = "";
            HRESULT = 0;
            ERRORNO = 0;
            ERRORMESSAGE = "";
        }

        /// <summary>
        /// 통계분석 상태 및 오류 Message 반환.
        /// </summary>
        /// <param name="subgroupID">서브 그룹 ID</param>
        /// <returns></returns>
        public int GetSubGroupStatusMessage(string subgroupID, ref string resultMessage)
        {
            int nResult = 0;
            string statusMessage = "";
            string errorMessage = "";
            resultMessage = "";

            try
            {
                var statData = from item in subgroupStatus
                                    where (item.SUBGROUPID == subgroupID)
                                    select item;
                foreach (var item in statData)
                {
                    nResult = item.RESULTFLAG;
                    if (nResult != 0)
                    { 
                        if (item.STATUSMESSAGE.ToSafeString() != "")
                        {
                            statusMessage = SpcLibMessage.common.comCpk1029;//상태 Message:
                            statusMessage = string.Format("{0} {1}", statusMessage, item.STATUSMESSAGE.ToSafeString());
                        }

                        if (item.ERRORMESSAGE.ToSafeString() != "")
                        {
                            nResult = 1;
                            errorMessage = SpcLibMessage.common.comCpk1030;//분석 Message:
                            errorMessage = string.Format("{0} {1}", errorMessage, item.ERRORMESSAGE.ToSafeString());
                        }

                        if (statusMessage != "")
                        {
                            resultMessage = statusMessage;
                        }

                        if (errorMessage != "")
                        {
                            resultMessage = string.Format("{0}{2}{2}{1}", resultMessage, errorMessage, Environment.NewLine);
                        }
                    }
                }
            }
            catch (Exception)
            {
                resultMessage = "";
            }

            return nResult;
        }
    }

    /// <summary>
    /// 상태 값
    /// </summary>
    public class SpcStatusMessage
    {
        /// <summary>
        /// Subgroup
        /// </summary>
        public string SUBGROUPID;
        /// <summary>
        /// 메세지 표시 여부 
        /// </summary>
        public int RESULTFLAG;
        /// <summary>
        /// 실행 상태 번호
        /// </summary>
        public int STATUS;

        /// <summary>
        /// 실행 상태 Message
        /// </summary>
        public string STATUSMESSAGE;
        /// <summary>
        /// 오류 값
        /// </summary>
        public int HRESULT;
        /// <summary>
        /// 오류 번호
        /// </summary>
        public int ERRORNO;

        /// <summary>
        /// 오류 Message
        /// </summary>
        public string ERRORMESSAGE;


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public SpcStatusMessage CopyDeep()
        {
            SpcStatusMessage cs = new SpcStatusMessage();
            cs.SUBGROUPID = this.SUBGROUPID;
            cs.RESULTFLAG = this.RESULTFLAG;
            cs.STATUS = this.STATUS;
            cs.STATUSMESSAGE = this.STATUSMESSAGE;
            cs.HRESULT = this.HRESULT;
            cs.ERRORNO = this.ERRORNO;
            cs.ERRORMESSAGE = this.ERRORMESSAGE;
            return cs;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    public class SPCRawSum
    {
        public long GROUPID;
        public long MAXCOUNT;
        public long KCOUNT;
        public double? NVALUE_TOL;
        public double? NVALUE_AVG;
        public double? NVALUE_VAC;
    }

    /// <summary>
    /// 서브그룹 합계
    /// </summary>
    public class SubGroupSum
    {
        public long? GROUPID;
        public long? SAMPLEID = 0;
        public long? NN = 0;
        public long? TOTNN = 0;
        public long? COUNT = 0;
        public long? TOTCOUNT = 0;
        public string SUBGROUP = string.Empty;
        public string SAMPLING = string.Empty;
        public string SUBGROUPNAME = string.Empty;
        public string SAMPLINGNAME = string.Empty;
        public double? MAX = null;
        public double? MIN = null;
        public double? R = null;
        public double? RBAR = null;
        public double? SBAR = null;
        public double? PBAR = null;
        public double? MR = null;
        public double? SUM = null;
        public double? SUMSUB = null;
        public double? AVG = 0;
        public double? STDE = null;
        public double? STDSUM = null;
        public double? SUMSTDE = null;
        public double? SUMSTDEBAR = null;
        public double? STDEP = null;
        public double? SUMSTDEP = null;
        public double? SUMSTDEPBAR = null;
        public double? TOTSUM = null;
        public double? TOTAVG = null;
        public double? TOTDEVSQU = null;
    }

    /// <summary>
    /// 
    /// </summary>
    public class SamplingGroupSum
    {
        public long? GROUPID;
        public long? SAMPLEID = 0;
        public long? NN = 0;
        public long? SNN = 0;
        public long? TOTNN = 0;
        public long? COUNT = 0;
        public long? TOTCOUNT = 0;
        public long? GROUPCOUNT = 0;
        public string SUBGROUP = string.Empty;
        public string SAMPLING = string.Empty;
        public string SUBGROUPNAME = string.Empty;
        public string SAMPLINGNAME = string.Empty;
        public double? MAX = null;
        public double? MIN = null;
        public double? R = null;
        public double? BAR = null;
        public double? RUCL = null;
        public double? RCL = null;
        public double? RLCL = null;
        public double? UCL = null;
        public double? CL = null;
        public double? LCL = null;
        public double? XBAR = null;
        public double? RBAR = null;
        public double? SBAR = null;
        public double? PBAR = null;
        public double? MR = null;
        public double? SUM = null;
        public double? SUMSUB = null;
        public double? AVG = 0;
        public double? STDE = null;
        public double? STDEsg = null;
        public double? SUMSTDE = null;
        public double? SUMSTDEBAR = null;
        public double? STDEP = null;
        public double? SUMSTDEP = null;
        public double? SUMSTDEPBAR = null;
        public double? TOTSUM = null;
        public double? TOTAVG = null;
        public double? SAMESIGMA = null;
    }

    /// <summary>
    /// SPC 통계 알고리즘 Class
    /// </summary>
    public class SPCLibs
    {
        /// <summary>
        /// PPM 백분위 수.
        /// </summary>
        public  const int NPPMPARTSPER = 1000000;
        #region 공정능력 IMR

        /// <summary>
        /// 공정능력 분석 - IMR
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnPPDataTable SpcLibPPIMR(SPCPara para, SPCOption spcOption, ref SPCOutData outData)
        {
            RtnPPDataTable rtnDataSet;
            outData.MessageClear();
            rtnDataSet = new RtnPPDataTable();
            DataTable rawData = para.InputData;
            int i = 0;
            int j = 0;
            string LSLNULL = "Y";
            string USLNULL = "Y";

            double d2 = 1.128;
            double? c4;

            long? GROUPID;
            long? MAXCOUNT = 0;
            long? MINCOUNT = 0;
            long? KCOUNT = 0;
            long? SAMPLE_ID = 0;
            double? NVALUE_TOL = null;
            double? NVALUE_AVG = null;
            double? NVALUE_VAC = null;
            double? SVALUE_AVG = null;
            double? SVALUE_STD = null;
            double? PVALUE_AVG = null;
            double? PVALUE_STD = null;
            double? PVALUE_STDD = null;
            double? PVALUE_STDC4 = null;
            double? PPKVALUE_STD = null;

            double? CP;
            double? CPL;
            double? CPU;
            double? CPK;
            double? PM;
            double? PP;
            double? PPL;
            double? PPU;
            double? PPK;
            double? PPM;

            TempPCIDataTable PCIVARIANCE = new TempPCIDataTable();
            TempPPIDataTable PPIVARIANCE = new TempPPIDataTable();

            try
            {
                if (para.LSL != null)
                {
                    LSLNULL = "N";
                }
                else
                {
                    outData.STATUS = 10110;
                    LSLNULL = "Y";
                    outData.ERRORNO = 1;
                    outData.STATUSMESSAGE = "LSL SPEC이 없습니다.";
                }

                if (para.USL != null)
                {
                    USLNULL = "N";
                }
                else
                {
                    outData.STATUS = 10120;
                    USLNULL = "Y";
                    outData.ERRORNO = 2;
                    outData.STATUSMESSAGE = "USL SPEC이 없습니다.";
                }

                if (LSLNULL == "Y" && USLNULL == "Y")
                {
                    outData.STATUS = 10130;
                    USLNULL = "Y";
                    outData.ERRORNO = 3;
                    outData.ERRORMESSAGE = "LSL, USL SPEC이 모두 없습니다. 그러므로 공정능력을 계산 할 수 없습니다.";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                //throw;
            }

            //전체 Count 및 총합, 평균 처리
            try
            {
                var query = from order in rawData.AsEnumerable()
                            group order by order.Field<long>("GROUPID") into g
                            select new
                            {
                                GROUPID = g.Key,
                                min = g.Min(f => f.Field<double?>("NVALUE")),
                                max = g.Max(f => f.Field<double?>("NVALUE")),
                                avg = g.Average(f => f.Field<double?>("NVALUE")),
                                sum = g.Sum(f => f.Field<double?>("NVALUE")),
                                count = g.Count()
                            };

                foreach (var order in query)
                {
                    Console.WriteLine("ContactID = {0} \t TotalDue sum = {1}", order.min, order.max);
                    GROUPID = order.GROUPID;
                    MINCOUNT = Convert.ToInt64(order.min);
                    MAXCOUNT = Convert.ToInt64(order.max);
                    KCOUNT = order.count;
                    NVALUE_TOL = Convert.ToDouble(order.sum);
                    NVALUE_AVG = order.avg;
                    NVALUE_VAC = MAXCOUNT - NVALUE_AVG; // Variance Check

                    Console.WriteLine("Summary : GROUPID = {0},  MINCOUNT = {1}, MAXCOUNT = {2},  KCOUNT = {3}, " +
                                      "NVALUE_TOL = {4},  NVALUE_AVG = {5}, NVALUE_VAC = {6}",
                                      GROUPID, MINCOUNT, MAXCOUNT, KCOUNT, NVALUE_TOL, NVALUE_AVG, NVALUE_VAC);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            //*Validation check Start. -----------------------------------------------
            //자료가 있는지 Check.
            if (MAXCOUNT <= 0)
            {
                outData.ERRORNO = 50100;
                outData.ERRORMESSAGE = "입력 자료가 없습니다.";
                goto NEXT_NED;
            }

            //Return 기본값 입력.
            DataRow rtnRow = rtnDataSet.NewRow();
            rtnRow["GROUPID"] = para.InputData.Rows[0]["GROUPID"];
            rtnRow["LSL"] = para.LSL;
            rtnRow["USL"] = para.USL;
            rtnRow["SPECMODE"] = para.SpecMode;
            rtnRow["KCOUNT"] = KCOUNT;
            rtnRow["NVALUE_TOL"] = NVALUE_TOL;
            rtnRow["NVALUE_AVG"] = NVALUE_AVG;
            rtnRow["PCISUBGROUP"] = NVALUE_VAC;
            rtnDataSet.Rows.Add(rtnRow);

            //입력 자료 개수 Check.
            if (MAXCOUNT == 1)
            {
                outData.ERRORNO = 50200;
                outData.ERRORMESSAGE = "전체 자료 수가  1일 때 : Sampling Count 변동이 없습니다.";
                goto NEXT_NED;
            }

            //값의 변동이 없을시 Check.
            if (NVALUE_VAC == 1)
            {
                outData.ERRORNO = 50200;
                outData.ERRORMESSAGE = "값의 변동이 없을시 : SubGroup의 값이 전부 같으면 계산하지 않음(변동이 없음으로";
                goto NEXT_NED;
            }

            //rawData.Rows.Count
            //*Variance 처리.
            long vGROUPID = 0;
            long vSUBGROUP = 0;
            long vSAMPLEID = 0;
            string vSAMPLING = "";
            double vNVALUE = 0;
            double vFVALUE = 0;
            double vSVALUE = 0;
            TempPCIVARIANCE_DataTable tempPCIVARIANCE = new TempPCIVARIANCE_DataTable();

            try
            {
                for (i = 1; i < rawData.Rows.Count; i++)
                {
                    DataRow row = rawData.Rows[i];
                    DataRow rowFront = rawData.Rows[i - 1];
                    //Console.WriteLine(row["SAMPLEID"].ToString());
                    vGROUPID = Convert.ToInt64(row["GROUPID"]);
                    vSAMPLEID = Convert.ToInt64(row["SAMPLEID"]);
                    vSAMPLING = row["SAMPLING"].ToString();
                    vNVALUE = Convert.ToDouble(row["NVALUE"]);
                    vFVALUE = Convert.ToDouble(rowFront["NVALUE"]);
                    vSVALUE = Math.Abs(vFVALUE - vNVALUE);

                    DataRow rowPciVariance = tempPCIVARIANCE.NewRow();
                    rowPciVariance["TEMPID"] = 1; //i;
                    rowPciVariance["SAMPLEID"] = vSAMPLEID;
                    rowPciVariance["FVALUE"] = vNVALUE;
                    rowPciVariance["NVALUE"] = vFVALUE;
                    rowPciVariance["SVALUE"] = vSVALUE;
                    tempPCIVARIANCE.Rows.Add(rowPciVariance);
                }

                Console.WriteLine(tempPCIVARIANCE.Rows.Count);
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            //추정치 결과값 반환.
            try
            {
                var query = from order in tempPCIVARIANCE.AsEnumerable()
                            group order by order.Field<long>("TEMPID") into g
                            select new
                            {
                                SAMPLEID = g.Key,
                                SVALUEAVG = g.Average(f => f.Field<double?>("SVALUE")),
                                SVALUESTD = g.Average(f => f.Field<double?>("SVALUE")),
                            };

                foreach (var order in query)
                {
                    SAMPLE_ID = order.SAMPLEID;
                    SVALUE_AVG = Convert.ToDouble(order.SVALUEAVG);
                    SVALUE_STD = Convert.ToDouble(order.SVALUESTD) / d2;
                    Console.WriteLine("Summary : GROUPID = {0},  SVALUE_AVG = {1}, SVALUE_STD = {2}",
                                      SAMPLE_ID, SVALUE_AVG, SVALUE_STD);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }


            //KCOUNT
            //NVALUE_TOL
            //NVALUE_AVG
            //SVALUE_AVG
            //SVALUE_STD
            CP = (para.USL - para.LSL) / (6 * SVALUE_STD);
            CPL = (NVALUE_AVG - para.LSL) / (3 * SVALUE_STD);
            CPU = (para.USL - NVALUE_AVG) / (3 * SVALUE_STD);
            CPK = null;

            if (USLNULL == "Y" && LSLNULL == "Y")
            {
                CPK = null;
            }
            else if (USLNULL == "N" && LSLNULL == "N")
            {
                CPK = (CPU > CPL ? CPL : CPU);
            }
            else if (USLNULL == "Y" && LSLNULL == "N")
            {
                CPK = CPL;
            }
            else if (USLNULL == "N" && LSLNULL == "Y")
            {
                CPK = CPU;
            }

            //*I-MR 계산값 전이 Start.----------------------------------------------------
            rtnDataSet.Rows[0]["SAMPLINGCOUNT"] = 1;
            rtnDataSet.Rows[0]["SUBGROUPCOUNT"] = KCOUNT;
            rtnDataSet.Rows[0]["PCOUNT"] = KCOUNT - 1;
            rtnDataSet.Rows[0]["KCOUNT"] = KCOUNT;
            rtnDataSet.Rows[0]["NVALUE_TOL"] = NVALUE_TOL;
            rtnDataSet.Rows[0]["NVALUE_AVG"] = NVALUE_AVG;
            rtnDataSet.Rows[0]["SVALUE_AVG"] = SVALUE_AVG;
            rtnDataSet.Rows[0]["SVALUE_STDC4"] = SVALUE_STD;
            rtnDataSet.Rows[0]["CP"] = CP;
            rtnDataSet.Rows[0]["CPL"] = CPL;
            rtnDataSet.Rows[0]["CPU"] = CPU;
            rtnDataSet.Rows[0]["CPK"] = CPK;
            rtnDataSet.Rows[0]["PCI_d2"] = d2;
            //rtnDataSet.Rows[0]["JUDGMENTCPK"] = , JUDGMENTCPK = case when @CPK > 1.33 then  'A' when @CPK <= 1.33 and @CPK > 1.00 then 'B' when @CPK <= 1.00 and @CPK >= 0.67 then  'C'   when @CPK< 0.67 then  'D'  else null   end;
            rtnDataSet.Rows[0]["STATUS"] = outData.STATUS;
            rtnDataSet.Rows[0]["STATUSMESSAGE"] = outData.STATUSMESSAGE;
            //*I-MR 계산값 전이 End.----------------------------------------------------
            Console.WriteLine(rtnDataSet.Rows.Count);
        //double cdf = 0;
        //cdf = CumDensity(2.079923538);
        //Console.WriteLine(cdf);

        //* PCI 공정능력 분석 ---------------------------------------------------------------------------------------------------
        //*종료
        NEXT_NED:

            return rtnDataSet;
        }

        /// <summary>공정능력 분석 - IMR & 종합
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnPPDataTable SpcLibPpkIMR(SPCPara para, SPCOption spcOption, ParPIDataTable singleRawData, ref SPCOutData outData)
        {
            RtnPPDataTable rtnDataSet;
            outData.MessageClear();

            rtnDataSet = new RtnPPDataTable();
            DataTable rawData;
            ParPIDataTable dbPara = null;
            if (singleRawData != null)
            {
                rawData = singleRawData;
                dbPara = singleRawData;
            }
            else
            {
                rawData = para.InputData;
                dbPara = para.InputData;
            }

            int i = 0;
            int j = 0;
            string LSLNULL = "Y";
            string USLNULL = "Y";

            double d2 = 1.128;

            double? c4;

            long? GROUPID;
            long? MAXCOUNT = 0;
            long? MINCOUNT = 0;
            long? KCOUNT = 0;
            long? SAMPLE_ID = 0;
            double? NVALUE_TOL = null;
            double? NVALUE_AVG = 0;
            double? NVALUE_VAC = null;
            double? SVALUE_AVG = null;
            double? SVALUE_STD_WITH = null;
            double? SVALUE_STD = null;
            double? SVALUE_STD_D2 = null;
            double? PVALUE_AVG = null;
            double? PVALUE_STD = null;
            double? PVALUE_STDD = null;
            double? PVALUE_STDC4 = null;

            double? CP;
            double? CPL;
            double? CPU;
            double? CPK;
            double? CPM;
            double? PP;
            double? PPL;
            double? PPU;
            double? PPK;
            double? PPM;

            string SUBGROUP = "";
            string SUBGROUPNAME = "";

            TempPCIDataTable PCIVARIANCE = new TempPCIDataTable();
            TempPPIDataTable PPIVARIANCE = new TempPPIDataTable();
            outData.ERRORMESSAGE = "";

            try
            {
                if (para.cpkTempSpec.lsl != SpcLimit.MAX && para.cpkTempSpec.lsl != SpcLimit.MIN)
                {
                    LSLNULL = "N";
                }
                else
                {
                    LSLNULL = "Y";
                    outData.STATUS = 10110;
                    outData.STATUSMESSAGE = SpcLibMessage.common.comCpk1013;//LSL SPEC이 없습니다.
                }

                if (para.cpkTempSpec.usl != SpcLimit.MAX && para.cpkTempSpec.usl != SpcLimit.MIN)
                {
                    USLNULL = "N";
                }
                else
                {
                    USLNULL = "Y";
                    outData.STATUS = 10120;
                    outData.STATUSMESSAGE = SpcLibMessage.common.comCpk1014; //USL SPEC이 없습니다.
                }

                if (LSLNULL == "Y" && USLNULL == "Y")
                {
                    USLNULL = "Y";
                    outData.STATUS = 10130;
                    outData.STATUSMESSAGE = string.Format("{0}{1}{2}", SpcLibMessage.common.comCpk1015, Environment.NewLine, SpcLibMessage.common.comCpk1016); // LSL, USL SPEC이 모두 없습니다. 그러므로 공정능력을 계산 할 수 없습니다.
                    outData.RESULTFLAG = 2;
                    goto NEXT_NED;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                //throw;
            }

            //전체 Count 및 총합, 평균 처리
            try
            {
                var query = from order in rawData.AsEnumerable()
                            group order by order.Field<long>("GROUPID") into g
                            select new
                            {
                                GROUPID = g.Key,
                                nSUBGROUP = g.Max(f => f.Field<string>("SUBGROUP")),
                                nSUBGROUPNAME = g.Max(f => f.Field<string>("SUBGROUPNAME")),
                                min = g.Min(f => f.Field<double?>("NVALUE")),
                                max = g.Max(f => f.Field<double?>("NVALUE")),
                                avg = g.Average(f => f.Field<double?>("NVALUE")),
                                sum = g.Sum(f => f.Field<double?>("NVALUE")),
                                count = g.Count()
                            };

                foreach (var order in query)
                {
                    Console.WriteLine("ContactID = {0} \t TotalDue sum = {1}", order.min, order.max);
                    GROUPID = order.GROUPID;
                    SUBGROUP = order.nSUBGROUP;
                    SUBGROUPNAME = order.nSUBGROUPNAME;
                    MINCOUNT = Convert.ToInt64(order.min);
                    MAXCOUNT = Convert.ToInt64(order.max);
                    KCOUNT = order.count;
                    NVALUE_TOL = Convert.ToDouble(order.sum);
                    NVALUE_AVG = order.avg;
                    NVALUE_VAC = MAXCOUNT - NVALUE_AVG; // Variance Check

                    Console.WriteLine("Summary : GROUPID = {0},  MINCOUNT = {1}, MAXCOUNT = {2},  KCOUNT = {3}, " +
                                      "NVALUE_TOL = {4},  NVALUE_AVG = {5}, NVALUE_VAC = {6}"
                                      , GROUPID, MINCOUNT, MAXCOUNT, KCOUNT, NVALUE_TOL, NVALUE_AVG, NVALUE_VAC);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                outData.RESULTFLAG = 2;
            }

            //*Validation check Start. -----------------------------------------------
            //자료가 있는지 Check.
            if (KCOUNT <= 0)
            {
                outData.ERRORNO = 50100;
                outData.ERRORMESSAGE = SpcLibMessage.common.comCpk1017;//입력 자료가 없습니다.
                goto NEXT_NED;
            }

            //Return 기본값 입력.
            DataRow rtnRow = rtnDataSet.NewRow();
            rtnRow["GROUPID"] = dbPara.Rows[0]["GROUPID"];
            rtnRow["SUBGROUP"] = dbPara.Rows[0]["SUBGROUP"];
            rtnRow["SUBGROUPNAME"] = dbPara.Rows[0]["SUBGROUPNAME"];
            SpcFunction.IsDbNckDoubleWrite(rtnRow, "LSL", para.cpkTempSpec.lsl);
            SpcFunction.IsDbNckDoubleWrite(rtnRow, "USL", para.cpkTempSpec.usl);
            rtnRow["SPECMODE"] = para.SpecMode;
            rtnRow["KCOUNT"] = KCOUNT;
            rtnRow["NVALUE_TOL"] = NVALUE_TOL;
            rtnRow["NVALUE_AVG"] = NVALUE_AVG;
            rtnRow["PCISUBGROUP"] = NVALUE_VAC;
            rtnDataSet.Rows.Add(rtnRow);

            //입력 자료 개수 Check.
            if (KCOUNT == 1)
            {
                outData.ERRORNO = 50200;
                ///전체 자료 수가  1일 때 : Sampling Count 변동이 없습니다.
                outData.ERRORMESSAGE = SpcLibMessage.common.comCpk1018;//변동부족: Sampling Count가 1개 입니다.
                outData.RESULTFLAG = 2;
                goto NEXT_NED;
            }

            //값의 변동이 없을시 Check.
            if (NVALUE_VAC == 1)
            {
                outData.ERRORNO = 50200;
                outData.ERRORMESSAGE = SpcLibMessage.common.comCpk1019;//값의 변동이 없을시: SubGroup의 값이 전부 같으면 계산하지 않음(변동이 없음)
                outData.RESULTFLAG = 2;
                goto NEXT_NED;
            }

            //공정능력분석 - I-MR 처리.,
            //*Variance 처리.
            long vGROUPID = 0;
            long vSUBGROUP = 0;
            long vSAMPLEID = 0;
            string vSAMPLING = "";
            double vNVALUE = 0;
            double vFVALUE = 0;
            double vSVALUE = 0;
            TempPCIVARIANCE_DataTable tempPCIVARIANCE = new TempPCIVARIANCE_DataTable();

            try
            {
                for (i = 1; i < rawData.Rows.Count; i++)
                {
                    DataRow row = rawData.Rows[i];
                    DataRow rowFront = rawData.Rows[i - 1];
                    //Console.WriteLine(row["SAMPLEID"].ToString());
                    vGROUPID = Convert.ToInt64(row["GROUPID"]);
                    vSAMPLEID = Convert.ToInt64(row["SAMPLEID"]);
                    vSAMPLING = row["SAMPLING"].ToString();
                    vNVALUE = Convert.ToDouble(row["NVALUE"]);
                    vFVALUE = Convert.ToDouble(rowFront["NVALUE"]);
                    vSVALUE = Math.Abs(vFVALUE - vNVALUE);

                    DataRow rowPciVariance = tempPCIVARIANCE.NewRow();
                    rowPciVariance["TEMPID"] = 1; //i;
                    rowPciVariance["SAMPLEID"] = vSAMPLEID;
                    rowPciVariance["FVALUE"] = vNVALUE;
                    rowPciVariance["NVALUE"] = vFVALUE;
                    rowPciVariance["SVALUE"] = vSVALUE;
                    tempPCIVARIANCE.Rows.Add(rowPciVariance);
                }

                Console.WriteLine(tempPCIVARIANCE.Rows.Count);
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                outData.RESULTFLAG = 2;
                goto NEXT_NED;
            }

            //추정치 결과값 반환.
            try
            {
                var query = from order in tempPCIVARIANCE.AsEnumerable()
                            group order by order.Field<long>("TEMPID") into g
                            select new
                            {
                                SAMPLEID = g.Key,
                                SVALUEAVG = g.Average(f => f.Field<double?>("SVALUE")),
                                SVALUESTD = g.Average(f => f.Field<double?>("SVALUE")),
                            };

                foreach (var order in query)
                {
                    SAMPLE_ID = order.SAMPLEID;
                    SVALUE_AVG = Convert.ToDouble(order.SVALUEAVG);
                    SVALUE_STD = Convert.ToDouble(order.SVALUESTD);
                    SVALUE_STD_D2 = Convert.ToDouble(order.SVALUESTD) / d2;
                    Console.WriteLine("Summary : GROUPID = {0},  SVALUE_AVG = {1}, SVALUE_STD = {2}",
                                      SAMPLE_ID, SVALUE_AVG, SVALUE_STD);
                }
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 2;
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            JudgmentType judgmentCPK = JudgmentType.Terrible;
            JudgmentType judgmentPPK = JudgmentType.Terrible;

            //*I-MR은 추정치 무조건 사용.
            if (spcOption.sigmaType != SigmaType.Yes)
            {
                SVALUE_STD_WITH = SVALUE_STD_D2;
                SVALUE_STD = SVALUE_STD_D2;
            }
            else
            {
                SVALUE_STD_WITH = SVALUE_STD_D2;
            }

            CP = (para.cpkTempSpec.usl - para.cpkTempSpec.lsl) / (6 * SVALUE_STD_WITH);
            CPL = (NVALUE_AVG - para.cpkTempSpec.lsl) / (3 * SVALUE_STD_WITH);
            CPU = (para.cpkTempSpec.usl - NVALUE_AVG) / (3 * SVALUE_STD_WITH);
            CPK = null;

            if (USLNULL == "Y" && LSLNULL == "Y")
            {
                CPK = null;
            }
            else if (USLNULL == "N" && LSLNULL == "N")
            {
                CPK = (CPU > CPL ? CPL : CPU);
            }
            else if (USLNULL == "Y" && LSLNULL == "N")
            {
                CPK = CPL;
            }
            else if (USLNULL == "N" && LSLNULL == "Y")
            {
                CPK = CPU;
            }

            //PPK 판정
            if (CPK != null && CPK != SpcLimit.MIN && CPK != SpcLimit.MAX)
            {
                judgmentCPK = SpcJudgment(CPK.ToSafeDoubleStaMin());
            }



            //*I-MR 계산값 전이 Start.----------------------------------------------------
            DataRow rowRtn = rtnDataSet.Rows[0];
            rtnDataSet.Rows[0]["SAMPLINGCOUNT"] = 1;
            rtnDataSet.Rows[0]["SUBGROUPCOUNT"] = KCOUNT;
            rtnDataSet.Rows[0]["PCOUNT"] = KCOUNT - 1;
            rtnDataSet.Rows[0]["KCOUNT"] = KCOUNT;
            rtnDataSet.Rows[0]["NVALUE_TOL"] = NVALUE_TOL;
            rtnDataSet.Rows[0]["NVALUE_AVG"] = NVALUE_AVG;
            rtnDataSet.Rows[0]["SVALUE_AVG"] = SVALUE_AVG;
            rtnDataSet.Rows[0]["SVALUE_STD"] = SVALUE_STD;
            rtnDataSet.Rows[0]["SVALUE_STDC4"] = SVALUE_STD_D2;
            SpcFunction.IsDbNckDoubleWrite(rowRtn, "CP", CP);
            SpcFunction.IsDbNckDoubleWrite(rowRtn, "CPL", CPL);
            SpcFunction.IsDbNckDoubleWrite(rowRtn, "CPU", CPU);
            SpcFunction.IsDbNckDoubleWrite(rowRtn, "CPK", CPK);

            rtnDataSet.Rows[0]["ISSAME"] = false;
            //rtnDataSet.Rows[0]["CP"] = CP;
            //rtnDataSet.Rows[0]["CPL"] = CPL;
            //rtnDataSet.Rows[0]["CPU"] = CPU;
            //rtnDataSet.Rows[0]["CPK"] = CPK;
            rtnDataSet.Rows[0]["PCI_d2"] = d2;
            rtnDataSet.Rows[0]["JUDGMENTCPK"] = judgmentCPK; //, JUDGMENTCPK = case when @CPK > 1.33 then  'A' when @CPK <= 1.33 and @CPK > 1.00 then 'B' when @CPK <= 1.00 and @CPK >= 0.67 then  'C'   when @CPK< 0.67 then  'D'  else null   end;
            rtnDataSet.Rows[0]["STATUS"] = outData.STATUS;
            rtnDataSet.Rows[0]["STATUSMESSAGE"] = outData.STATUSMESSAGE;
            //*I-MR 계산값 전이 End.----------------------------------------------------
            //Console.WriteLine(rtnDataSet.Rows.Count);

            //* PCI 공정능력 분석 Start---------------------------------------------------------------------------------------------------
            outData.STATUS = 8500;
            c4 = SPCSta.GetCoe("c4", KCOUNT.ToSafeInt64());
            TempPPIDataTable tempTbPPIData = new TempPPIDataTable();
            try
            {
                for (i = 0; i < rawData.Rows.Count; i++)
                {
                    DataRow row = rawData.Rows[i];
                    vGROUPID = Convert.ToInt64(row["GROUPID"]);
                    vSAMPLEID = Convert.ToInt64(row["SAMPLEID"]);
                    vSAMPLING = row["SAMPLING"].ToString();
                    vNVALUE = Convert.ToDouble(row["NVALUE"]);
                    vFVALUE = Convert.ToDouble(NVALUE_AVG) - vNVALUE;
                    vSVALUE = vFVALUE * vFVALUE;

                    Console.WriteLine(string.Format("S[{0}] => {1}", vSAMPLEID, vSVALUE));

                    DataRow rowPpiVariance = tempTbPPIData.NewRow();
                    rowPpiVariance["TEMPID"] = 1; //i;
                    rowPpiVariance["GROUPID"] = vGROUPID;
                    rowPpiVariance["SAMPLEID"] = vSAMPLEID;
                    rowPpiVariance["FVALUE"] = vNVALUE;
                    rowPpiVariance["NVALUE"] = vFVALUE;
                    rowPpiVariance["SVALUE"] = vSVALUE;
                    tempTbPPIData.Rows.Add(rowPpiVariance);
                }

                Console.WriteLine(tempTbPPIData.Rows.Count);
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                outData.RESULTFLAG = 2;
                goto NEXT_NED;
            }

            //추정치 결과값 반환.
            try
            {
                var query = from order in tempTbPPIData.AsEnumerable()
                            group order by order.Field<long>("GROUPID") into g
                            select new
                            {
                                SAMPLEID = g.Key,
                                PVALUEAVG = g.Sum(f => f.Field<double?>("SVALUE")),
                                PVALUESTDD = g.Sum(f => f.Field<double?>("SVALUE")),
                                PVALUESTDC4 = g.Sum(f => f.Field<double?>("SVALUE")),
                            };

                foreach (var order in query)
                {
                    SAMPLE_ID = order.SAMPLEID;
                    PVALUE_AVG = Convert.ToDouble(order.PVALUEAVG) / (KCOUNT - 1);
                    PVALUE_STDD = Math.Sqrt(Convert.ToDouble(PVALUE_AVG));
                    PVALUE_STDC4 = PVALUE_STDD / c4;
                    //Console.WriteLine("Summary : SAMPLE_ID = {0},  PVALUE_AVG = {1}, PVALUE_STDD = {2}, PVALUE_STDC4 = {3}"
                    //    , SAMPLE_ID, PVALUE_AVG, PVALUE_STDD, PVALUE_STDC4);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                outData.RESULTFLAG = 2;
                goto NEXT_NED;
            }

            double? PPKVALUE_STD = null;
            switch (spcOption.sigmaType)
            {
                case SigmaType.No:
                    PPKVALUE_STD = PVALUE_STDD;//PPK 추정치 미사용 // calPCI_WITHIN_SBAR;
                    break;
                case SigmaType.Yes:
                    PPKVALUE_STD = PVALUE_STDC4;//PPK 추정치 사용
                    break;
                default:
                    PPKVALUE_STD = PVALUE_STDC4;//PPK 추정치 사용
                    break;
            }

            PP = (para.cpkTempSpec.usl - para.cpkTempSpec.lsl) / (6 * PPKVALUE_STD);
            PPL = (NVALUE_AVG - para.cpkTempSpec.lsl) / (3 * PPKVALUE_STD);
            PPU = (para.cpkTempSpec.usl - NVALUE_AVG) / (3 * PPKVALUE_STD);

            
            PPK = null;
            if (USLNULL == "Y" && LSLNULL == "Y")
            {
                PPK = null;
            }
            else if (USLNULL == "N" && LSLNULL == "N")
            {
                PPK = (PPU > PPL ? PPL : PPU);
            }
            else if (USLNULL == "Y" && LSLNULL == "N")
            {
                PPK = PPL;
            }
            else if (USLNULL == "N" && LSLNULL == "Y")
            {
                PPK = PPU;
            }

            //PPK 판정
            if (PPK != null && PPK != SpcLimit.MIN && PPK != SpcLimit.MAX)
            {
                judgmentPPK = SpcJudgment(PPK.ToSafeDoubleStaMin());
            }

            //*PPK 계산값 전이 Start.----------------------------------------------------
            rtnDataSet.Rows[0]["PVALUE_AVG"] = NVALUE_AVG;
            rtnDataSet.Rows[0]["PVALUE_STD"] = PVALUE_STDD;
            rtnDataSet.Rows[0]["PVALUE_STDC4"] = PVALUE_STDC4;
            SpcFunction.IsDbNckDoubleWrite(rowRtn, "PP", PP);
            SpcFunction.IsDbNckDoubleWrite(rowRtn, "PPL", PPL);
            SpcFunction.IsDbNckDoubleWrite(rowRtn, "PPU", PPU);
            SpcFunction.IsDbNckDoubleWrite(rowRtn, "PPK", PPK);
            //rtnDataSet.Rows[0]["PP"] = PP;
            //rtnDataSet.Rows[0]["PPL"] = PPL;
            //rtnDataSet.Rows[0]["PPU"] = PPU;
            //rtnDataSet.Rows[0]["PPK"] = PPK;
            rtnDataSet.Rows[0]["PPI_c4"] = c4;
            rtnDataSet.Rows[0]["JUDGMENTPPK"] = judgmentPPK; 


            #region PPM 계산 처리.
            double ppmWithinLSL = 0, ppmWithinUSL = 0, ppmWithinTOT = 0;
            double ppmWithinLSLRound = 0, ppmWithinUSLRound = 0, ppmWithinTOTRound = 0;
            double ppmOverallLSL = 0, ppmOverallUSL = 0, ppmOverallTOT = 0;
            double ppmOverallLSLRound = 0, ppmOverallUSLRound = 0, ppmOverallTOTRound = 0;
            double ppmObserveLSLN = 0, ppmObserveUSLN = 0;
            double ppmObserveLSL = 0, ppmObserveUSL = 0, ppmObserveTOT = 0;
            double ppmObserveLSLRound = 0, ppmObserveUSLRound = 0, ppmObserveTOTRound = 0;
            
            //PPM < LSL 규격 하한
            if (LSLNULL == "N")
            {
                ppmWithinLSL = SPCLibs.SpcPpm(SpcPpmMode.Within, SpcSpecType.LSL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.lsl, SVALUE_STD_WITH.ToSafeDoubleStaMin(), out ppmWithinLSLRound);
                ppmOverallLSL = SPCLibs.SpcPpm(SpcPpmMode.Total, SpcSpecType.LSL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.lsl, PPKVALUE_STD.ToSafeDoubleStaMin(), out ppmOverallLSLRound);
                ppmObserveLSL = SPCLibs.SpcPpmObserve(SpcPpmMode.Observe, SpcSpecType.LSL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.lsl, KCOUNT.ToSafeDoubleStaMin(), singleRawData, out ppmObserveLSLN, out ppmObserveLSLRound);

                ppmWithinTOT = ppmWithinLSL;
                ppmOverallTOT = ppmOverallLSL;
                ppmObserveTOT = ppmObserveLSL;

                ppmWithinTOTRound = ppmWithinLSLRound;
                ppmOverallTOTRound = ppmOverallLSLRound;
                ppmObserveTOTRound = ppmObserveLSLRound;

            }
            else
            {
                ppmWithinLSL = SpcLimit.MIN;
                ppmOverallLSL = SpcLimit.MIN;
                ppmObserveLSL = SpcLimit.MIN;
            }
            
            //PPM > USL 규격 상한
            if (USLNULL == "N")
            {
                ppmWithinUSL = SPCLibs.SpcPpm(SpcPpmMode.Within, SpcSpecType.USL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.usl, SVALUE_STD_WITH.ToSafeDoubleStaMin(), out ppmWithinUSLRound);
                ppmOverallUSL = SPCLibs.SpcPpm(SpcPpmMode.Total, SpcSpecType.USL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.usl, PPKVALUE_STD.ToSafeDoubleStaMin(), out ppmOverallUSLRound);
                ppmObserveUSL = SPCLibs.SpcPpmObserve(SpcPpmMode.Observe, SpcSpecType.USL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.usl, KCOUNT.ToSafeDoubleStaMin(), singleRawData, out ppmObserveUSLN, out ppmObserveUSLRound);

                ppmWithinTOT = ppmWithinUSL;
                ppmOverallTOT = ppmOverallUSL;
                ppmObserveTOT = ppmObserveUSL;

                ppmWithinTOTRound = ppmWithinUSLRound;
                ppmOverallTOTRound = ppmOverallUSLRound;
                ppmObserveTOTRound = ppmObserveUSLRound;
            }
            else
            {
                ppmWithinUSL = SpcLimit.MIN;
                ppmOverallUSL = SpcLimit.MIN;
                ppmObserveUSL = SpcLimit.MIN;
            }

            //PPM > TOTAL (LSL & USL)
            if (USLNULL == "N" && LSLNULL == "N")
            {
                ppmWithinTOT = ppmWithinLSL + ppmWithinUSL;
                ppmOverallTOT = ppmOverallLSL + ppmOverallUSL;
                ppmObserveTOT = ppmObserveLSL + ppmObserveUSL;

                ppmWithinTOTRound = ppmWithinLSLRound + ppmWithinUSLRound;
                ppmOverallTOTRound = ppmOverallLSLRound + ppmOverallUSLRound;
                ppmObserveTOTRound = ppmObserveLSLRound + ppmObserveUSLRound;
            }

            //*PPK 계산값 전이 Start.----------------------------------------------------
            rtnDataSet.Rows[0]["PPMWITHINLSL"] = ppmWithinLSL;
            rtnDataSet.Rows[0]["PPMWITHINUSL"] = ppmWithinUSL;
            rtnDataSet.Rows[0]["PPMWITHINTOT"] = ppmWithinTOT;
            rtnDataSet.Rows[0]["PPMOVERALLLSL"] = ppmOverallLSL;
            rtnDataSet.Rows[0]["PPMOVERALLUSL"] = ppmOverallUSL;
            rtnDataSet.Rows[0]["PPMOVERALLTOT"] = ppmOverallTOT;
            rtnDataSet.Rows[0]["PPMOBSERVELSLN"] = ppmObserveLSLN;
            rtnDataSet.Rows[0]["PPMOBSERVEUSLN"] = ppmObserveUSLN;
            rtnDataSet.Rows[0]["PPMOBSERVELSL"] = ppmObserveLSL;
            rtnDataSet.Rows[0]["PPMOBSERVEUSL"] = ppmObserveUSL;
            rtnDataSet.Rows[0]["PPMOBSERVETOT"] = ppmObserveTOT;
            //*PPK 계산값 전이 End.----------------------------------------------------
            //Console.WriteLine(rtnDataSet.Rows.Count);
            Console.WriteLine(string.Format("ppmWithinTOT => {0} ", ppmWithinTOT));
            Console.WriteLine(string.Format("ppmOverallTOT => {0} ", ppmOverallTOT));

            #endregion PPM 계산 처리.
                       

            rtnDataSet.Rows[0]["STATUS"] = outData.STATUS;
            rtnDataSet.Rows[0]["STATUSMESSAGE"] = outData.STATUSMESSAGE;

            //*I-MR 계산값 전이 End.----------------------------------------------------
            //Console.WriteLine(rtnDataSet.Rows.Count);


        //*종료
        NEXT_NED:

            return rtnDataSet;
        }

        #endregion

        #region 공정능력 합동 & 종합

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputSpec"></param>
        /// <param name="subGroupId"></param>
        /// <param name="chartType"></param>
        /// <returns></returns>
        public static SpcSpec SpcLibDBControlSpecSearch(ParPISpecDataTable InputSpec, string subGroupId, ControlSpec controlSpec)
        {
            SpcSpec cpkTempSpec = SpcSpec.Create();
            string chartType = controlSpec.spcOption.chartName.xBarChartType.ToSafeString();
            string defaultChartType = controlSpec.spcOption.specDefaultChartType;
            string subChartCheckData = "";

            bool isSpecDefault = false;
            try
            {
                var rowSpec = InputSpec.AsEnumerable().AsParallel();
                var query = rowSpec.AsParallel()
                    .Where(w => w.Field<string>("SUBGROUP") == subGroupId)
                    .Where(w => w.Field<string>("CHARTTYPE") == chartType)
                    .OrderBy(or => or.Field<string>("CHARTTYPE"));
                foreach (DataRow row in query)
                {
                    isSpecDefault = true;
                    cpkTempSpec.usl = SpcFunction.IsDbNckDoubleMin(row, "USL");
                    cpkTempSpec.csl = SpcFunction.IsDbNckDoubleMin(row, "CSL");
                    cpkTempSpec.lsl = SpcFunction.IsDbNckDoubleMin(row, "LSL");
                    cpkTempSpec.ucl = SpcFunction.IsDbNckDoubleMin(row, "UCL");
                    cpkTempSpec.ccl = SpcFunction.IsDbNckDoubleMin(row, "CCL");
                    cpkTempSpec.lcl = SpcFunction.IsDbNckDoubleMin(row, "LCL");
                    cpkTempSpec.uol = SpcFunction.IsDbNckDoubleMin(row, "UOL");
                    cpkTempSpec.lol = SpcFunction.IsDbNckDoubleMin(row, "LOL");
                }

                //선택 Chart
                subChartCheckData = chartType;

                //기본 SPEC 으로 대체
                if (defaultChartType != "" && isSpecDefault != true)
                {
                    query = rowSpec.AsParallel()
                        .Where(w => w.Field<string>("SUBGROUP") == subGroupId)
                        .Where(w => w.Field<string>("CHARTTYPE") == defaultChartType)
                        .OrderBy(or => or.Field<string>("CHARTTYPE"));
                    foreach (DataRow row in query)
                    {
                        cpkTempSpec.usl = SpcFunction.IsDbNckDoubleMin(row, "USL");
                        cpkTempSpec.csl = SpcFunction.IsDbNckDoubleMin(row, "CSL");
                        cpkTempSpec.lsl = SpcFunction.IsDbNckDoubleMin(row, "LSL");
                        cpkTempSpec.ucl = SpcFunction.IsDbNckDoubleMin(row, "UCL");
                        cpkTempSpec.ccl = SpcFunction.IsDbNckDoubleMin(row, "CCL");
                        cpkTempSpec.lcl = SpcFunction.IsDbNckDoubleMin(row, "LCL");
                        cpkTempSpec.uol = SpcFunction.IsDbNckDoubleMin(row, "UOL");
                        cpkTempSpec.lol = SpcFunction.IsDbNckDoubleMin(row, "LOL");
                        //기본 Chart 자료로 대체
                        subChartCheckData = defaultChartType;
                    }
                }

                string subChartType = "";
                switch (subChartCheckData)
                {
                    case "XBARR":
                        subChartType = "R";
                        break;
                    case "XBARS":
                        subChartType = "S";
                        break;
                    case "I":
                        subChartType = "MR";
                        break;
                    case "XBARP":
                        subChartType = "PL";
                        break;
                    case "P":
                    case "NP":
                    case "C":
                    case "U":
                    default:
                        subChartType = "";
                        break;
                }

                if (subChartType != "")
                {
                    var querySub = rowSpec.AsParallel()
                        .Where(w => w.Field<string>("SUBGROUP") == subGroupId)
                        .Where(w => w.Field<string>("CHARTTYPE") == subChartType)
                        .OrderBy(or => or.Field<string>("CHARTTYPE"));
                    foreach (DataRow row in querySub)
                    {
                        cpkTempSpec.rUcl = SpcFunction.IsDbNckDoubleMin(row, "UCL");
                        cpkTempSpec.rUcl = SpcFunction.IsDbNckDoubleMin(row, "LCL");
                    }
                }

            }
            catch (Exception ex)
            {
                //outData.ERRORNO = ex.HResult;
                //outData.ERRORMESSAGE = ex.Message.ToString();
            }


            return cpkTempSpec;
        }

        /// <summary>
        /// 공정능력 분석 다중 Data 처리.
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnPPDataTable SpcLibPpkCbMuti(SPCPara para, SPCOption spcOption, ref SPCOutData outData)
        {
            int nSeq = 0;
            int nLimitDirectMaxCount = 5;
            int nLimitDirectIndex = 0;

            SPCPara mutiPara = para;
            RtnPPDataTable rtnDataSet = new RtnPPDataTable();
            RtnPPDataTable rtnSingleDataSet = new RtnPPDataTable();
            lstPCISUBGROUPDataTable tempPciSubGroupList = new lstPCISUBGROUPDataTable();
            para.InputDataSum = null;
            para.InputDataSum = new ParPIDataTable();

            outData.MessageClear();
            outData.STATUS = 1101;
            outData.STATUSMESSAGE = SpcLibMessage.common.comCpk1001;//시작-공정능력 분석 함수 실행. [SpcLibPpkCbMuti]
            //전체 Count 및 총합, 평균 처리
            try
            {
                var sumSubGroup = from g in para.InputData //where b.NVALUE !=1
                                  group g by g.SUBGROUP into g
                                  select new
                                  {
                                      vGROUPID = g.Max(s => s.GROUPID),
                                      vSAMPLEID = g.Max(s => s.SAMPLEID),
                                      vSUBGROUP = g.Key,
                                      vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),
                                      vSAMPLING = g.Max(s => s.SAMPLING),
                                      vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),
                                      //vUSL = g.Max(s => s.USL),
                                      //vCSL = g.Max(s => s.CSL),
                                      //vLSL = g.Min(s => s.LSL),
                                      vMAX = g.Max(s => s.NVALUE),
                                      vMIN = g.Min(s => s.NVALUE),
                                      vSUM = g.Sum(s => s.NVALUE),
                                      vAVG = g.Average(s => s.NVALUE),
                                      vNN = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var item in sumSubGroup)
                {
                    DataRow dataRow = tempPciSubGroupList.NewRow();
                    //Console.WriteLine("ContactID = {0} \t TotalDue sum = {1}", order.min, order.max);
                    dataRow["TEMPID"] = nSeq++;
                    dataRow["GROUPID"] = item.vGROUPID;
                    dataRow["SAMPLEID"] = item.vSAMPLEID;
                    dataRow["SUBGROUP"] = item.vSUBGROUP;
                    dataRow["SUBGROUPNAME"] = item.vSUBGROUPNAME;
                    dataRow["SAMPLING"] = item.vSAMPLING;
                    dataRow["SAMPLINGNAME"] = item.vSAMPLINGNAME;
                    //dataRow["USL"] = item.vUSL.ToSafeDoubleStaMin(); 
                    //dataRow["CSL"] = item.vCSL.ToSafeDoubleStaMin();
                    //dataRow["LSL"] = item.vLSL.ToSafeDoubleStaMin();
                    dataRow["MAX"] = item.vMAX.ToSafeDoubleStaMin();
                    dataRow["MIN"] = item.vMIN.ToSafeDoubleStaMin();
                    dataRow["SUM"] = item.vSUM.ToSafeDoubleStaMin();
                    dataRow["AVG"] = item.vAVG.ToSafeDoubleStaMin();
                    dataRow["NN"] = item.vNN;
                    dataRow["COUNT"] = item.vCOUNT;
                    tempPciSubGroupList.Rows.Add(dataRow);
                }
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            outData.STATUS = 1111;
            outData.STATUSMESSAGE = SpcLibMessage.common.com1002;//분석 입력자료 읽기."Raw Data Read."
            ParPIDataTable inputPciRawData = new ParPIDataTable();
            int nMaxRow = tempPciSubGroupList.Rows.Count;
            //int nSampleID 
            for (int i = 0; i < nMaxRow; i++)
            {
                inputPciRawData.Rows.Clear();
                DataRow dataRow = tempPciSubGroupList.Rows[i];
                string subGroupId = dataRow["SUBGROUP"].ToSafeString();

                outData.STATUS = 1112;
                outData.STATUSMESSAGE = string.Format("Index: {0}, SubgroupID: {0}", subGroupId);

                var pciRawData = para.InputData.AsEnumerable().Where(x => x.SUBGROUP == subGroupId);

                foreach (var item in pciRawData)
                {
                    DataRow dataRowItem = inputPciRawData.NewRow();
                    dataRowItem["GROUPID"] = item.GROUPID;
                    dataRowItem["SAMPLEID"] = item.SAMPLEID;
                    dataRowItem["SUBGROUP"] = item.SUBGROUP;
                    dataRowItem["SUBGROUPNAME"] = item.SUBGROUPNAME;
                    dataRowItem["SAMPLING"] = item.SAMPLING;
                    dataRowItem["SAMPLINGNAME"] = item.SAMPLINGNAME;
                    dataRowItem["NVALUE"] = item.NVALUE;
                    //dataRowItem["USL"] = item.USL;
                    //dataRowItem["CSL"] = item.CSL;
                    //dataRowItem["LSL"] = item.LSL;
                    inputPciRawData.Rows.Add(dataRowItem);
                }

                outData.STATUS = 1121;
                outData.STATUSMESSAGE = SpcLibMessage.common.com1003;// "Cpk Spec Check";

                //DataTable 일경우 AsEnumerable 변환 후 처리함.

                //spcOption.chartName.xCpkChartType = "sia";//sia테스트 : Spec오류 발생 부분.
                para.cpkTempSpec = SpcSpec.Create();
                para.cpkTempSpec.usl = SpcLimit.MIN;
                para.cpkTempSpec.csl = SpcLimit.MIN;
                para.cpkTempSpec.lsl = SpcLimit.MIN;
                bool isSpecDefaultCheck = false;

                if (para != null && para.InputSpec != null)
                {
                    try
                    {
                        var rowSpec = para.InputSpec.AsEnumerable();
                        var query = rowSpec.AsParallel()
                            .Where(w => w.Field<string>("SUBGROUP") == subGroupId)
                            .Where(w => w.Field<string>("CHARTTYPE") == spcOption.chartName.xCpkChartType)
                            .OrderBy(or => or.Field<string>("CHARTTYPE"));
                        foreach (DataRow row in query)
                        {
                            isSpecDefaultCheck = true;
                            para.cpkTempSpec.usl = SpcFunction.IsDbNckDoubleMin(row, "USL");
                            para.cpkTempSpec.csl = SpcFunction.IsDbNckDoubleMin(row, "CSL");//목표값
                            para.cpkTempSpec.lsl = SpcFunction.IsDbNckDoubleMin(row, "LSL");
                            Console.WriteLine(para.USL);
                        }

                        //선택 Spec이 없을시 기본 Spec 값을 사용함.
                        if (isSpecDefaultCheck != true)
                        {
                            Console.WriteLine("specDefaultChartType: {0}", spcOption.specDefaultChartType);
                            var querySub = rowSpec.AsParallel()
                                .Where(w => w.Field<string>("SUBGROUP") == subGroupId)
                                .Where(w => w.Field<string>("CHARTTYPE") == spcOption.specDefaultChartType)
                                .OrderBy(or => or.Field<string>("CHARTTYPE"));
                            foreach (DataRow row in querySub)
                            {
                                para.cpkTempSpec.usl = SpcFunction.IsDbNckDoubleMin(row, "USL");
                                para.cpkTempSpec.csl = SpcFunction.IsDbNckDoubleMin(row, "CSL");//목표값
                                para.cpkTempSpec.lsl = SpcFunction.IsDbNckDoubleMin(row, "LSL");
                            }
                        }

                        #region 참고
                        ////Spec이 없을시 다른 Type으로 재 조회.
                        //if (para.cpkTempSpec.usl == SpcLimit.MIN && para.cpkTempSpec.lsl == SpcLimit.MIN)
                        //{
                        //    switch (spcOption.chartType)
                        //    {
                        //        case ControlChartType.XBar_R:
                        //            break;
                        //        case ControlChartType.XBar_S:
                        //        case ControlChartType.Merger:
                        //            break;
                        //        case ControlChartType.I_MR:
                        //            if (spcOption.specDefaultChartType != "MR")
                        //            {
                        //                //선택 Spec이 없을시 기본 Spec 값을 사용함.
                        //                if (isSpecDefaultCheck != true)
                        //                {
                        //                    var querySub = rowSpec.AsParallel()
                        //                        .Where(w => w.Field<string>("SUBGROUP") == subGroupId)
                        //                        .Where(w => w.Field<string>("CHARTTYPE") == "MR")
                        //                        .OrderBy(or => or.Field<string>("CHARTTYPE"));
                        //                    foreach (DataRow row in querySub)
                        //                    {
                        //                        para.cpkTempSpec.usl = SpcFunction.IsDbNckDoubleMin(row, "USL");
                        //                        para.cpkTempSpec.csl = SpcFunction.IsDbNckDoubleMin(row, "CSL");//목표값
                        //                        para.cpkTempSpec.lsl = SpcFunction.IsDbNckDoubleMin(row, "LSL");
                        //                    }
                        //                }
                        //            }
                        //            break;
                        //        case ControlChartType.np:
                        //        case ControlChartType.p:
                        //        case ControlChartType.c:
                        //        case ControlChartType.u:
                        //        default:
                        //            break;
                        //    }
                        //}
                        #endregion 참고

                    }
                    catch (Exception ex)
                    {
                        outData.RESULTFLAG = 1;
                        outData.ERRORNO = ex.HResult;
                        outData.ERRORMESSAGE = ex.Message.ToString();
                    }
                }
                else
                {
                    para.cpkTempSpec.usl = SpcLimit.MIN;
                    para.cpkTempSpec.csl = SpcLimit.MIN;
                    para.cpkTempSpec.lsl = SpcLimit.MIN;
                }


                //직접 입력 SPEC Check 9/11
                switch (para.spcOption.limitType)
                {
                    case LimitType.Direct:
                        //if (nLimitDirectIndex >= nLimitDirectMaxCount)
                        //{
                        //    nLimitDirectIndex = 1;
                        //}

                        //if (para.spcOption.LimitTypeUseIndex[nLimitDirectIndex])
                        //{
                        //    para.cpkTempSpec.usl = para.USL.ToSafeDoubleStaMax();
                        //    para.cpkTempSpec.csl = para.CSL.ToSafeDoubleStaMax();
                        //    para.cpkTempSpec.lsl = para.LSL.ToSafeDoubleStaMax();
                        //}
                        //nLimitDirectIndex++;
                        para.cpkTempSpec.usl = para.USL.ToSafeDoubleStaMax();
                        para.cpkTempSpec.csl = para.CSL.ToSafeDoubleStaMax();
                        para.cpkTempSpec.lsl = para.LSL.ToSafeDoubleStaMax();
                        spcOption.DirectModeCpkSpec.usl = para.cpkTempSpec.usl;
                        spcOption.DirectModeCpkSpec.csl = para.cpkTempSpec.csl;
                        spcOption.DirectModeCpkSpec.lsl = para.cpkTempSpec.lsl;

                        break;
                    case LimitType.Interpretation:
                    case LimitType.Management:
                    default:
                        break;
                }

                Console.WriteLine(para.spcOption.limitType.ToString());

                bool isSpecCheck = false;
                //Spec Check - Spec은 한개 라도 있어야 함.
                if (para.cpkTempSpec != null)
                {
                    if (para.cpkTempSpec.usl != SpcLimit.MIN && para.cpkTempSpec.usl != SpcLimit.MAX)
                    {
                        isSpecCheck = true;
                    }

                    if (para.cpkTempSpec.lsl != SpcLimit.MIN && para.cpkTempSpec.lsl != SpcLimit.MAX)
                    {
                        isSpecCheck = true;
                    }


                }

                try
                {
                    //sia확인 : 공정능력 분석 실행 [SPCLIbSta].
                    if (isSpecCheck != false)
                    {
                        rtnSingleDataSet.Rows.Clear();
                        switch (spcOption.chartType)
                        {
                            case ControlChartType.XBar_R:
                            case ControlChartType.XBar_S:
                            case ControlChartType.Merger:
                                rtnSingleDataSet = SpcLibPpkCbMain(para, spcOption, inputPciRawData, ref outData);
                                break;
                            case ControlChartType.I_MR:
                            case ControlChartType.np:
                            case ControlChartType.p:
                            case ControlChartType.c:
                            case ControlChartType.u:
                            default:
                                ParPIDataTable singleSumData;
                                bool imrsumcheck = SpcLibPpkIMRDataCheck(inputPciRawData, para.InputDataSum, out singleSumData);
                                if (!imrsumcheck)
                                {
                                    rtnSingleDataSet = SpcLibPpkIMR(para, spcOption, inputPciRawData, ref outData);
                                }
                                else
                                {
                                    rtnSingleDataSet = SpcLibPpkIMR(para, spcOption, singleSumData, ref outData);
                                    rtnSingleDataSet[0]["DATASUMMODE"] = 1;
                                }
                                break;
                        }

                        if (rtnSingleDataSet != null && rtnSingleDataSet.Rows.Count > 0)
                        {
                            foreach (DataRow r in rtnSingleDataSet.Rows)
                            {
                                rtnDataSet.ImportRow(r);
                            }
                        }
                    }
                    else
                    {
                        //MessageBox.Show("공정능력 분석용 USL, LSL 값이 업습니다.");
                        outData.RESULTFLAG = 2;
                        outData.STATUS = 1201;
                        outData.STATUSMESSAGE = "";
                        outData.STATUSMESSAGE += string.Format("{0}{1}{1}", SpcLibMessage.common.comCpk1011, Environment.NewLine);//공정능력 분석용 USL, LSL 값이 없습니다.
                        outData.STATUSMESSAGE += string.Format("{0}", SpcLibMessage.common.comCpk1012);//한개 이상 SPEC이 있어야 합니다.
                    }

                    Console.WriteLine(rtnDataSet.Rows.Count);
                }
                catch (Exception ex)
                {
                    outData.RESULTFLAG = 1;
                    outData.ERRORNO = ex.HResult;
                    outData.ERRORMESSAGE = ex.Message.ToString();
                }

                SpcStatusMessage subStatus = new SpcStatusMessage();
                subStatus.RESULTFLAG = outData.RESULTFLAG;
                subStatus.SUBGROUPID = subGroupId;
                subStatus.STATUS = outData.STATUS;
                subStatus.STATUSMESSAGE = outData.STATUSMESSAGE;
                subStatus.ERRORNO = outData.ERRORNO;
                subStatus.ERRORMESSAGE = outData.ERRORMESSAGE;
                outData.subgroupStatus.Add(subStatus);

            }

            return rtnDataSet;
        }

        public static bool SpcLibPpkIMRDataCheck(ParPIDataTable singleRawData, ParPIDataTable InputPISumData, out ParPIDataTable singleSumData)
        {
            bool isResult = false;
            long idSeq = 0;
            //ParPIDataTable inputPciRawData = new ParPIDataTable();

            SubGroupSum sg = new SubGroupSum();
            singleSumData = new ParPIDataTable();

            try
            {
                //var sums = empList
                //.GroupBy(x => new { x.Age, x.Sex })
                //.Select(group => new { Peo = group.Key, Count = group.Count() });

                var sumSubGroup = from b in singleRawData //where b.NVALUE !=1
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP,
                                      b.SAMPLEID,
                                      b.SAMPLING
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSAMPLEID = g.Key.SAMPLEID,
                                      vSAMPLING = g.Key.SAMPLING,
                                      vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),
                                      //vSUBGROUPNAME01 = g.Max(s => s.SUBGROUPNAME01.ToSafeDBString()),
                                      vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),
                                      //vSAMPLING01 = g.Max(s => s.SAMPLING01.ToSafeDBString()),
                                      //vSAMPLINGNAME01 = g.Max(s => s.SAMPLINGNAME01.ToSafeDBString()),
                                      //vSAMPLING02 = g.Max(s => s.SAMPLING02.ToSafeDBString()),
                                      //vSAMPLINGNAME02 = g.Max(s => s.SAMPLINGNAME02.ToSafeDBString()),
                                      vNVALUE = g.Average(s => s.NVALUE),
                                      //vNSUBVALUE = g.Average(s => s.NSUBVALUE),
                                      //vUSL = g.Max(s => s.USL),
                                      //vCSL = g.Average(s => s.CSL),
                                      //vLSL = g.Average(s => s.LSL),
                                      //vAVG = g.Average(s => s.AVG),
                                      //vDEV = g.Average(s => s.DEV),
                                      //vDEVSQU = g.Average(s => s.DEVSQU),
                                      vNN = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };


                foreach (var f in sumSubGroup)
                {
                    idSeq++;
                    DataRow sgRow = singleSumData.NewRow();
                    sgRow["GROUPID"] = f.vGROUPID;
                    sgRow["SAMPLEID"] = f.vSAMPLEID;
                    sgRow["SUBGROUP"] = f.vSUBGROUP;
                    sgRow["SAMPLING"] = f.vSAMPLING;
                    sgRow["SUBGROUPNAME"] = f.vSUBGROUPNAME;
                    //sgRow["SUBGROUPNAME01"] = f.vSUBGROUPNAME01;
                    sgRow["SAMPLINGNAME"] = f.vSAMPLINGNAME;
                    //sgRow["SAMPLING01"] = f.vSAMPLING01;
                    //sgRow["SAMPLINGNAME01"] = f.vSAMPLINGNAME01;
                    //sgRow["SAMPLING02"] = f.vSAMPLING02;
                    //sgRow["SAMPLINGNAME02"] = f.vSAMPLINGNAME02;
                    sgRow["NVALUE"] = f.vNVALUE;
                    //sgRow["NSUBVALUE"] = f.vNSUBVALUE;
                    //sgRow["USL"] = f.vUSL;
                    //sgRow["CSL"] = f.vCSL;
                    //sgRow["LSL"] = f.vLSL;
                    //sgRow["AVG"] = f.vAVG;
                    //sgRow["DEV"] = f.vDEV;
                    //sgRow["DEVSQU"] = f.vDEVSQU;

                    if (f.vNN > 1)
                    {
                        isResult = true;//Summary I-MR 처리
                    }

                    singleSumData.Rows.Add(sgRow);

                    Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                        "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                        , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }

                if (isResult)
                {
                    foreach (DataRow item in singleSumData)
                    {
                        InputPISumData.ImportRow(item);
                    }
                }
                else
                {
                    foreach (DataRow item in singleRawData)
                    {
                        InputPISumData.ImportRow(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return isResult;
        }

        /// <summary>
        /// 공정능력 분석 - 합동 & 종합
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnPPDataTable SpcLibPpkCb(SPCPara para, SPCOption spcOption, ref SPCOutData outData)
        {
            ParPIDataTable singleRawData = null;

            return SpcLibPpkCbMain(para, spcOption, singleRawData, ref outData);
        }

        /// <summary>
        /// 공정능력 분석 - 합동 & 종합
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnPPDataTable SpcLibPpkCbMain(SPCPara para, SPCOption spcOption, ParPIDataTable singleRawData, ref SPCOutData outData)
        {
            RtnPPDataTable rtnDataSet;

            rtnDataSet = new RtnPPDataTable();
            DataTable rawData = null;
            ParPIDataTable dbPara = null;

            outData.MessageClear();

            if (singleRawData != null)
            {
                rawData = singleRawData;
                dbPara = singleRawData;
            }
            else
            {
                rawData = para.InputData;
                dbPara = para.InputData;
            }

            int i = 0;
            int j = 0;
            string LSLNULL = "Y";
            string USLNULL = "Y";

            //double d2 = 1.128;

            //double? c4 = null;
            string subgroupName = "";
            long? GROUPID;
            long? MAXCOUNT = 0;
            long? MINCOUNT = 0;
            long? KCOUNT = 0;
            long? SAMPLE_ID = 0;
            long? SUBGROUPCOUNT = 0;
            long? SAMPLECOUNT = 0;
            double? NVALUE_TOL = null;
            double? NVALUE_AVG = 0;
            double? NVALUE_VAC = null;
            double? SVALUE_AVG = null;
            double? SVALUE_STD = null;
            double? PVALUE_AVG = null;
            double? PPKVALUE_STD = null;
            double? PVALUE_STD = null;
            double? PVALUE_STDD = null;
            double? PVALUE_STDC4 = null;
            double? CP;
            double? CPL;
            double? CPU;
            double? CPK;
            double? CPM;
            double? PP;
            double? PPL;
            double? PPU;
            double? PPK;
            double? PPM;

            TempPCIDataTable PCIVARIANCE = new TempPCIDataTable();
            TempPPIDataTable PPIVARIANCE = new TempPPIDataTable();

            try
            {
                if (para.cpkTempSpec.lsl != SpcLimit.MAX && para.cpkTempSpec.lsl != SpcLimit.MIN)
                {
                    LSLNULL = "N";
                }
                else
                {
                    LSLNULL = "Y";
                    outData.STATUS = 10110;
                    outData.STATUSMESSAGE = SpcLibMessage.common.comCpk1013; //LSL SPEC이 없습니다.
                }

                if (para.cpkTempSpec.usl != SpcLimit.MAX && para.cpkTempSpec.usl != SpcLimit.MIN)
                {
                    USLNULL = "N";
                }
                else
                {
                    USLNULL = "Y";
                    outData.STATUS = 10120;
                    outData.STATUSMESSAGE = SpcLibMessage.common.comCpk1014;//USL SPEC이 없습니다.
                }

                if (LSLNULL == "Y" && USLNULL == "Y")
                {
                    USLNULL = "Y";
                    outData.STATUS = 10130;
                    outData.STATUSMESSAGE = string.Format("{0}{1}{2}", SpcLibMessage.common.comCpk1015, Environment.NewLine, SpcLibMessage.common.comCpk1016); // "LSL, USL SPEC이 모두 없습니다. 그러므로 공정능력을 계산 할 수 없습니다.";
                    outData.RESULTFLAG = 2;
                    goto NEXT_NED;
                }
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.HRESULT = ex.HResult;
                outData.ERRORNO = 10110;
                outData.ERRORMESSAGE = ex.Message;
                goto NEXT_NED;
            }


            //전체 Count 및 총합, 평균 처리
            try
            {
                var query = from order in rawData.AsEnumerable().AsParallel()
                            group order by order.Field<long>("GROUPID") into g
                            select new
                            {
                                GROUPID = g.Key,
                                subgroupMax = g.Max(f => f.Field<string>("SUBGROUP")),
                                subgroupName = g.Max(f => f.Field<string>("SUBGROUPNAME")),
                                min = g.Min(f => f.Field<double?>("NVALUE")),
                                max = g.Max(f => f.Field<double?>("NVALUE")),
                                avg = g.Average(f => f.Field<double?>("NVALUE")),
                                sum = g.Sum(f => f.Field<double?>("NVALUE")),
                                count = g.Count()
                            };

                foreach (var order in query)
                {
                    Console.WriteLine("ContactID = {0} \t TotalDue sum = {1}", order.min, order.max);
                    GROUPID = order.GROUPID;
                    subgroupName = order.subgroupMax;
                    MINCOUNT = Convert.ToInt64(order.min);
                    MAXCOUNT = Convert.ToInt64(order.max);
                    KCOUNT = order.count;
                    SUBGROUPCOUNT = order.count;
                    NVALUE_TOL = Convert.ToDouble(order.sum);
                    NVALUE_AVG = order.avg;
                    NVALUE_VAC = MAXCOUNT - NVALUE_AVG; // Variance Check
                    
                    Console.WriteLine("Summary : GROUPID = {0},  MINCOUNT = {1}, MAXCOUNT = {2},  KCOUNT = {3}, " +
                        "NVALUE_TOL = {4},  NVALUE_AVG = {5}, NVALUE_VAC = {6}"
                        , GROUPID, MINCOUNT, MAXCOUNT, KCOUNT, NVALUE_TOL, NVALUE_AVG, NVALUE_VAC);
                }
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.HRESULT = ex.HResult;
                outData.ERRORNO = 10120;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            //*Validation check Start. -----------------------------------------------
            //자료가 있는지 Check.
            if (SUBGROUPCOUNT <= 0)
            {
                outData.RESULTFLAG = 2;
                outData.STATUS = 10210;
                outData.STATUSMESSAGE = SpcLibMessage.common.comCpk1017;//입력 자료가 없습니다."
                goto NEXT_NED;
            }



            //Return 기본값 입력.
            DataRow rtnRow = rtnDataSet.NewRow();
            rtnRow["GROUPID"] = rawData.Rows[0]["GROUPID"];
            rtnRow["SUBGROUP"] = subgroupName;
            rtnRow["LSL"] = para.cpkTempSpec.lsl.ToSafeDoubleStaMin();
            rtnRow["CSL"] = para.cpkTempSpec.csl.ToSafeDoubleStaMin();
            rtnRow["USL"] = para.cpkTempSpec.usl.ToSafeDoubleStaMin();
            rtnRow["SPECMODE"] = para.SpecMode;
            rtnRow["KCOUNT"] = KCOUNT;
            rtnRow["NVALUE_TOL"] = NVALUE_TOL;
            rtnRow["NVALUE_AVG"] = NVALUE_AVG;
            rtnRow["PCISUBGROUP"] = NVALUE_VAC;
            rtnDataSet.Rows.Add(rtnRow);

            //Sampling Count Check
            try
            {
                var query = from order in rawData.AsEnumerable().AsParallel()
                            group order by order.Field<string>("SAMPLING") into g
                            select new
                            {
                                samplingiD = g.Key,
                                count = g.Count()
                            };

                foreach (var order in query)
                {
                    Console.WriteLine("SAMPLING ID = {0} \t Count = {1}", order.samplingiD, order.count);
                    SAMPLECOUNT = order.count;
                }
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.HRESULT = ex.HResult;
                outData.ERRORNO = 10121;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            //입력 자료 개수 Check.
            if (SAMPLECOUNT <= 1)
            {
                outData.STATUS = 10220;
                outData.STATUSMESSAGE = SpcLibMessage.common.comCpk1018;//변동부족 : Sampling Count가 1개 입니다.
                outData.RESULTFLAG = 2;
                goto NEXT_NED;
            }

            //값의 변동이 없을시 Check.
            if (NVALUE_VAC == 0)
            {
                outData.STATUS = 10230;
                outData.STATUSMESSAGE = SpcLibMessage.common.comCpk1019;//값의 변동이 없을시 : SubGroup의 값이 전부 같으면 계산하지 않음(변동이 없음)
                outData.RESULTFLAG = 2;
                goto NEXT_NED; 
            }

            //*평균처리 변수 선언.
            //전체 Count 및 총합, 평균 처리
            long idSeq = 0;
            bool isSamplingNotSame = false;
            long? nNNSameCheck = 0;
            SubGroupSum sg = new SubGroupSum();
            TempPPIDataAvgDataTable tbPPIDataAvg = new TempPPIDataAvgDataTable();

            try
            {

                var sumSubGroup = from b in dbPara //where b.NVALUE !=1
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP,
                                      b.SAMPLEID,
                                      b.SAMPLING
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSAMPLEID = g.Key.SAMPLEID,
                                      vSAMPLING = g.Key.SAMPLING,
                                      vMAX = g.Max(s => s.NVALUE),
                                      vMIN = g.Min(s => s.NVALUE),
                                      vSUM = g.Sum(s => s.NVALUE),
                                      vAVG = g.Average(s => s.NVALUE),
                                      vNN = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in sumSubGroup)
                {
                    sg.GROUPID = f.vGROUPID;
                    sg.SUBGROUP = f.vSUBGROUP;
                    sg.SAMPLEID = f.vSAMPLEID;
                    sg.SAMPLING = f.vSAMPLING;
                    sg.MAX = f.vMAX;
                    sg.MIN = f.vMIN;
                    sg.R = sg.MAX - sg.MIN;
                    sg.SUM = f.vSUM;
                    sg.AVG = f.vAVG;
                    sg.NN = f.vNN;
                    sg.COUNT = f.vCOUNT;

                    if (nNNSameCheck == 0)//1/7추가
                    {
                        nNNSameCheck = sg.NN;
                    }
                    else
                    {
                        if (sg.NN != nNNSameCheck && isSamplingNotSame == false)
                        {
                            isSamplingNotSame = true;
                        }
                    }

                    idSeq++;
                    DataRow sgRow = tbPPIDataAvg.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = sg.GROUPID;
                    sgRow["SUBGROUP"] = sg.SUBGROUP;
                    sgRow["SAMPLING"] = sg.SAMPLING;
                    sgRow["ISSAME"] = isSamplingNotSame;
                    sgRow["MAX"] = sg.MAX;
                    sgRow["MIN"] = sg.MIN;
                    sgRow["R"] = sg.R;
                    sgRow["SUMVALUE"] = sg.SUM;
                    //sgRow["TOTSUMVALUE"] = "";
                    sgRow["AVGVALUE"] = sg.AVG;
                    //sgRow["TOTAVGVALUE"] = "";
                    //sgRow["STDEVALUE"] = sg.STDE;
                    sgRow["NN"] = sg.NN;
                    sgRow["SNN"] = sg.NN - 1;
                    //sgRow["TOTNN"] = "";
                    //sgRow["GROUPNN"] = "";

                    tbPPIDataAvg.Rows.Add(sgRow);

                    //Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.HRESULT = ex.HResult;
                outData.ERRORNO = 10310;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }


            //Subgroup 전체 평균 처리.
            double dvMax = 0.0;
            double dvMin = 0.0;
            double dvR = 0.0;
            double dvRbar = 0.0;

            TempPPIDataAvgTotDataTable tbPPIDataAvgTot = new TempPPIDataAvgTotDataTable();
            long? totvgNN = 0;
            long? totvgKK = 0;
            long? totvgGG = 0;
            long? totvgCC = 0;

            try
            {
                var sumTotGroup = from b in dbPara //where b.NVALUE !=1
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vTOTSUM = g.Sum(s => s.NVALUE),
                                      vTOTAVG = g.Average(s => s.NVALUE),
                                      vTOTNN = g.Sum(s => 1),
                                      vTOTCOUNT = g.Count()
                                  };

                foreach (var f in sumTotGroup)
                {
                    sg.TOTSUM = f.vTOTSUM;
                    sg.TOTSUM = f.vTOTSUM;
                    sg.TOTAVG = f.vTOTAVG;
                    sg.TOTNN = f.vTOTNN;
                    sg.TOTCOUNT = f.vTOTCOUNT;

                    idSeq++;

                    totvgKK = sg.TOTCOUNT;

                    DataRow sgRow = tbPPIDataAvgTot.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = sg.GROUPID;
                    sgRow["SUBGROUP"] = sg.SUBGROUP;
                    sgRow["TOTSUMVALUE"] = sg.TOTSUM;
                    sgRow["TOTAVGVALUE"] = sg.TOTAVG;
                    sgRow["TOTNN"] = sg.TOTNN;
                    //sgRow["TOTSNN"] = sg.TOTAVG;
                    sgRow["TOTCOUNT"] = sg.TOTCOUNT;
                    tbPPIDataAvgTot.Rows.Add(sgRow);

                    for (int d = 0; d < tbPPIDataAvg.Rows.Count; d++)
                    {
                        DataRow sgTotRow = tbPPIDataAvg.Rows[d];

                        sgTotRow["TOTSUMVALUE"] = sg.TOTSUM;
                        sgTotRow["TOTAVGVALUE"] = sg.TOTAVG;
                        sgTotRow["TOTNN"] = sg.TOTNN;
                        //gTotRow["TOTCOUNT"] = sg.TOTCOUNT;
                        //sgTotRow["GROUPNN"] = "";
                    }

                    //Console.WriteLine("Summary : sgSUBGROUP = {0}, " +
                    //    "sgTOTSUM = {1},  sgTOTAVG = {2}, sgTOTNN = {3}, sgTOTCOUNT = {4}"
                    //    , sg.SUBGROUP, sg.TOTSUM, sg.TOTAVG, sg.TOTNN, sg.TOTCOUNT);
                }
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.HRESULT = ex.HResult;
                outData.ERRORNO = 10410;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            try
            {
                var sumPPIDataGroup = from b in tbPPIDataAvg  //where b.NVALUE !=1
                                      group b by new
                                      {
                                          b.GROUPID,
                                          b.SUBGROUP
                                      }
                                      into g
                                      select new
                                      {
                                          vGROUPID = g.Key.GROUPID,
                                          vSUBGROUP = g.Key.SUBGROUP,
                                          vISSAME = g.Max(s => s.ISSAME),
                                          vRSUM = g.Sum(s => s.R),
                                          vRAVG = g.Average(s => s.R),
                                          vGROUPCOUNT = g.Count()
                                      };

                foreach (var f in sumPPIDataGroup)
                {
                    //dvMax,dvMin
                    dvR = f.vRSUM;
                    dvRbar = f.vRAVG;
                    isSamplingNotSame = f.vISSAME;
                }
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.HRESULT = ex.HResult;
                outData.ERRORNO = 10510;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            double A2 = 0.0;
            double d2 = 0.0;
            double d3 = 0.0;
            double D3 = 0.0;
            double D4 = 0.0;
            double c4 = 0.0;
            double fx = 0.0;
            double fxSum = 0.0;
            double fr = 0.0;
            double frd2 = 0.0;
            double frd2Sum = 0.0;
            double sameRSigma = 0.0;
            long nNN = 0;

            #region Sampling 개수가 같지 않을 경우 처리.
            try
            {
                if (isSamplingNotSame)
                {
                    //ctrDataSta
                    for (i = 0; i < tbPPIDataAvg.Rows.Count; i++)
                    {
                        DataRow sgRow = tbPPIDataAvg.Rows[i];
                        sg.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                        sg.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                        sg.SAMPLING = sgRow["SUBGROUP"].ToSafeString();
                        //sg.SUBGROUPNAME = sp.SUBGROUPNAME;
                        //sg.SAMPLINGNAME = sp.SAMPLINGNAME;
                        sg.MAX = sgRow["MAX"].ToSafeDoubleZero();
                        sg.MIN = sgRow["MIN"].ToSafeDoubleZero();
                        sg.R = sgRow["R"].ToSafeDoubleZero();
                        //sg.BAR = sp.BAR;
                        nNN = sgRow["NN"].ToSafeInt64();
                        sg.NN = nNN;
                        //sg.SNN = sp.SNN.ToSafeInt64();
                        
                        d2 = SPCSta.GetCoed2(nNN);
                        d3 = SPCSta.GetCoed3(nNN);

                        //R 표준편차
                        fx = (d2 * d2) / (d3 * d3);
                        fxSum += fx;
                        fr = fx * sg.R.ToSafeDoubleZero();
                        frd2 = fr / d2;
                        frd2Sum += frd2;
                    }

                    sameRSigma = frd2Sum / fxSum;//R
                    //sgRow["SAMESIGMA"] = sameRSigma;
                    Console.WriteLine("sameRSigma: {0}", sameRSigma);
                    Console.WriteLine("ctrDataTot: {0}", tbPPIDataAvgTot.Rows.Count);
                }
                else
                {
                    sameRSigma = SpcLimit.MIN;
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }
            #endregion Sampling 개수가 같지 않을 경우 처리.

            //편차 저장 테이블
            string dvSUBGROUP, dvSAMPLING;
            double dvAVGVALUE = 0.0;
            double dvNVALUE;
            double dvDEVIATION;
            double dvSQUAREDEVIATION = 0.0;

            double dvTOTAVGVALUE = 0.0;
            double dvTOTDEVIATION;
            double dvTOTSQUAREDEVIATION = 0.0;
            long dvGROUPID;
            long dvSAMPLEID;
            long dvNN = 0;

            TempDeviationDataTable tempDeviationDataTable = new TempDeviationDataTable();
            //tbPPIDataAvg
            try
            {
                for (i = 0; i < dbPara.Rows.Count; i++)
                {
                    DataRow row = rawData.Rows[i];

                    dvGROUPID = Convert.ToInt64(row["GROUPID"]);
                    dvSUBGROUP = row["SUBGROUP"].ToString();
                    dvSAMPLEID = Convert.ToInt64(row["SAMPLEID"]);
                    dvSAMPLING = row["SAMPLING"].ToString();
                    //dvAVGVALUE = Convert.ToDouble(row["AVGVALUE"]);
                    dvNVALUE = Convert.ToDouble(row["NVALUE"]);

                    //dvFVALUE = Convert.ToDouble(rowFront["NVALUE"]);
                    //dvSVALUE = Math.Abs(vFVALUE - vNVALUE);00000

                    var rowDataAvg = from item in tbPPIDataAvg
                                     where (item.GROUPID == dvGROUPID
                                     && item.SUBGROUP == dvSUBGROUP
                                     && item.SAMPLING == dvSAMPLING)
                                     select item;

                    foreach (var f in rowDataAvg)
                    {
                        //= Convert.ToDouble(row["NVALUE"]);
                        dvAVGVALUE = f.AVGVALUE;
                        dvTOTAVGVALUE = f.TOTAVGVALUE;
                        dvNN = f.NN;
                    }

                    //if(dvSAMPLEID == smSampleId)
                    dvDEVIATION = dvNVALUE - dvAVGVALUE;
                    dvSQUAREDEVIATION = dvDEVIATION * dvDEVIATION;
                    dvTOTDEVIATION = dvNVALUE - dvTOTAVGVALUE;
                    dvTOTSQUAREDEVIATION = dvTOTDEVIATION * dvTOTDEVIATION;

                    DataRow rowDeviation = tempDeviationDataTable.NewRow();
                    rowDeviation["TEMPID"] = i;
                    rowDeviation["GROUPID"] = dvGROUPID;
                    rowDeviation["SUBGROUP"] = dvSUBGROUP;
                    rowDeviation["SAMPLEID"] = dvSAMPLEID;
                    rowDeviation["SAMPLING"] = dvSAMPLING;
                    rowDeviation["AVGVALUE"] = dvAVGVALUE;
                    rowDeviation["NVALUE"] = dvNVALUE;
                    rowDeviation["DEVIATION"] = dvDEVIATION;
                    rowDeviation["SQUAREDEVIATION"] = dvSQUAREDEVIATION;
                    rowDeviation["TOTAVGVALUE"] = dvTOTAVGVALUE;
                    rowDeviation["TOTDEVIATION"] = dvTOTDEVIATION;
                    rowDeviation["TOTSQUAREDEVIATION"] = dvTOTSQUAREDEVIATION;
                    rowDeviation["NN"] = dvNN;
                    tempDeviationDataTable.Rows.Add(rowDeviation);
                }

                Console.WriteLine(tempDeviationDataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.HRESULT = ex.HResult;
                outData.ERRORNO = 10610;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            //Console.WriteLine("--- PPK 편차");
            RetrunPpkDatas rtnPpk = new RetrunPpkDatas();
            TempPPIVARIANCEDataTable tempDtPPIVARIANCE = new TempPPIVARIANCEDataTable();
            double ppkvStdev = 0.0;

            try
            {
                var TempPpkData = tempDeviationDataTable.AsEnumerable()
                                                        .GroupBy(g => new
                                                        {
                                                            g.GROUPID,
                                                            g.SUBGROUP,
                                                            g.SAMPLING
                                                        })
                                                        .Select(s => new RetrunPpkDatas
                                                        {
                                                            GROUPID = s.Key.GROUPID,
                                                            SUBGROUP = s.Key.SUBGROUP,
                                                            SAMPLING = s.Key.SAMPLING,
                                                            PCI_VARIANCE = s.Sum(b => b.SQUAREDEVIATION),
                                                            PCI_VARIANCE_STDEV = s.Sum(b => b.SQUAREDEVIATION / (b.NN - 1)),
                                                            PPI_VARIANCE = s.Sum(b => b.TOTSQUAREDEVIATION),
                                                            NN = s.Max(b => b.NN),
                                                            SNN = s.Sum(b => 1)
                                                            //Fee = ac.Sum(acs => acs.Fee)
                                                        });

                foreach (var f in TempPpkData)
                {
                    totvgNN = f.NN;
                    totvgGG += 1;
                    totvgCC = totvgCC + (f.SNN - 1);

                    DataRow dr = tempDtPPIVARIANCE.NewRow();
                    ppkvStdev = Math.Sqrt(f.PCI_VARIANCE_STDEV);
                    dr["GROUPID"] = f.GROUPID;
                    dr["SUBGROUP"] = f.SUBGROUP;
                    dr["SAMPLING"] = f.SAMPLING;
                    dr["PCI_VARIANCE"] = f.PCI_VARIANCE;
                    dr["PCI_VARIANCE_STDEV"] = ppkvStdev;
                    dr["PPI_VARIANCE"] = f.PPI_VARIANCE;
                    dr["NN"] = f.NN;
                    dr["SNN"] = f.SNN - 1;

                    tempDtPPIVARIANCE.Rows.Add(dr);
                    Console.WriteLine(f.PCI_VARIANCE);
                    Console.WriteLine(f.PPI_VARIANCE);
                    //= Convert.ToDouble(row["NVALUE"]);
                }

            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.HRESULT = ex.HResult;
                outData.ERRORNO = 10710;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            outData.STATUSMESSAGE = string.Format("" +
                "totvgNN: {0}, " +
                "totvgKK: {1}, " +
                "totvgGG: {2}, " +
                "totvgCC: {3}"
                , totvgNN.ToString()
                , totvgKK.ToString()
                , totvgGG.ToString()
                , totvgCC.ToString()
                );
            Console.WriteLine(outData.STATUSMESSAGE);
            Console.WriteLine(tempDtPPIVARIANCE.Rows.Count);

            double hi = 0.0;
            double hiSum = 0.0;
            double hiSi = 0.0;
            double hiSic4 = 0.0;
            double hiSic4Sum = 0.0;
            double sameSSigma = 0.0;

            #region Sampling 개수가 같지 않을 경우 처리.
            try
            {
                if (isSamplingNotSame)
                {
                    //ctrDataSta
                    for (i = 0; i < tempDtPPIVARIANCE.Rows.Count; i++)
                    {
                        DataRow sgRow = tempDtPPIVARIANCE.Rows[i];
                        sg.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                        sg.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                        sg.SAMPLING = sgRow["SAMPLING"].ToSafeString();
                        //sg.SUBGROUPNAME = sp.SUBGROUPNAME;
                        //sg.SAMPLINGNAME = sp.SAMPLINGNAME;
                        //sg.MAX = sgRow["MAX"].ToSafeDoubleZero();
                        //sg.MIN = sgRow["MIN"].ToSafeDoubleZero();
                        sg.R = sgRow["PCI_VARIANCE_STDEV"].ToSafeDoubleZero();
                        //sg.BAR = sp.BAR;
                        nNN = sgRow["NN"].ToSafeInt64();
                        sg.NN = nNN;

                        //S 표준편차
                        c4 = SPCSta.GetCoe("c4", nNN);
                        hi = (c4 * c4) / (1 - (c4 * c4));
                        hiSum += hi;
                        hiSi = hi * sg.R.ToSafeDoubleZero();
                        hiSic4 = hiSi / c4;
                        hiSic4Sum += hiSic4;

                    }

                    sameSSigma = hiSic4Sum / hiSum;//S
                    //sgRow["SAMESIGMA"] = sameRSigma;
                    Console.WriteLine("sameSSigma: {0}", sameSSigma);
                    Console.WriteLine("ctrDataTot: {0}", tempDtPPIVARIANCE.Rows.Count);
                }
                else
                {
                    sameRSigma = SpcLimit.MIN;
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }
            #endregion Sampling 개수가 같지 않을 경우 처리.

            double d2NN = SPCSta.GetCoed2(totvgNN.ToSafeInt64());
            double d3KK = SPCSta.GetCoed3(totvgKK.ToSafeInt64());
            double c4NN = SPCSta.GetCoe("c4", totvgNN.ToSafeInt64());
            double c4KK = SPCSta.GetCoe("c4", totvgKK.ToSafeInt64());
            //double c4CC = SPCSta.getCoe("c4", totvgCC.ToSafeInt64());
            long? cCC = totvgKK - (totvgGG - 1);
            double c4CC = SPCSta.GetCoe("c4", cCC.ToSafeInt64());

            //*결과값 계산.
            long calGROUPID;
            long calGROUPCOUNT = 0;
            string calSUBGROUP;
            string calSAMPLING;
            double calPCI_TOTVARIANCE;
            double calPCI_TOTVARIANCE_STDEV;
            double calPPI_TOTVARIANCE;
            double calPCI_WITHIN_RBAR = 0.0;
            double calPCI_ADJUST_RBAR = 0.0;
            double calPCI_WITHIN_SBAR = 0.0;
            double calPCI_ADJUST_SBAR = 0.0;
            double calPCI_WITHIN_PSD = 0.0;
            double calPCI_ADJUST_PSD = 0.0;
            double calPCI_ADJUST_PSD_C4 = 0.0;

            double calPPI_STAVARIANCE = 0.0;
            double calPPI_OVERALL = 0.0;
            double calPPI_ADJUSTC4 = 0.0;
            long calNN = 0;
            long calSNN = 0;

            try
            {
                var TempTotPpkData = tempDtPPIVARIANCE.AsEnumerable()
                                                      .GroupBy(g => new
                                                      {
                                                          g.GROUPID,
                                                          g.SUBGROUP
                                                      })
                                                      .Select(s => new RetrunPpkDatasTotal
                                                      {
                                                          GROUPID = s.Key.GROUPID,
                                                          SUBGROUP = s.Key.SUBGROUP,
                                                          SAMPLING = s.Max(b => b.SAMPLING),
                                                          PCI_TOTVARIANCE = s.Sum(b => b.PCI_VARIANCE),
                                                          PCI_TOTVARIANCE_STDEV = s.Sum(b => b.PCI_VARIANCE_STDEV),
                                                          PPI_TOTVARIANCE = s.Sum(b => b.PPI_VARIANCE),
                                                          MaxNN = s.Max(b => b.NN),
                                                          SumSNN = s.Sum(b => b.SNN)
                                                          //Fee = ac.Sum(acs => acs.Fee)
                                                      });

                foreach (var f in TempTotPpkData)
                {
                    calGROUPID = f.GROUPID;
                    calSUBGROUP = f.SUBGROUP;
                    calSAMPLING = f.SAMPLING;
                    calPCI_TOTVARIANCE = f.PCI_TOTVARIANCE;
                    calPCI_TOTVARIANCE_STDEV = f.PCI_TOTVARIANCE_STDEV;
                    calPPI_TOTVARIANCE = f.PPI_TOTVARIANCE;
                    calNN = f.MaxNN;
                    calSNN = f.SumSNN;
                    calGROUPCOUNT = totvgGG.ToSafeInt64();
                    //, a.TOTAVGVALUE as PCI_TOTAVGSAMPLE
                    //, a.TOTSUMVALUE as PCI_TOTSUMSAMPLE
                    //, v.NN as NN //SAMPLECOUNT
                    //, a.TOTNN as KK //TOTSAMPLECOUNT
                    //, v.GROUPCOUNT as GG //GROUPCOUNT
                    //, v.SNN as CC //SUM SAMPLECOUNT
                    //, v.PCI_TOTVARIANCE as PCI_TOTVARIANCE

                    //*R (CPK)
                    if (isSamplingNotSame)
                    {
                        //Samling 개수가 같지 않음.
                        calPCI_WITHIN_RBAR = sameRSigma;
                        calPCI_ADJUST_RBAR = sameRSigma;
                    }
                    else
                    {
                        //Samling 개수가 같음.
                        calPCI_WITHIN_RBAR = dvR / totvgGG.ToSafeInt64();
                        calPCI_ADJUST_RBAR = calPCI_WITHIN_RBAR / d2NN;
                    }

                    //*S (Cpk)
                    if (isSamplingNotSame)
                    {
                        //Samling 개수가 같지 않음.
                        calPCI_WITHIN_SBAR = (calPCI_TOTVARIANCE_STDEV / calGROUPCOUNT);
                        calPCI_ADJUST_SBAR = sameSSigma;
                    }
                    else
                    {
                        //Samling 개수가 같음.
                        calPCI_WITHIN_SBAR = (calPCI_TOTVARIANCE_STDEV / calGROUPCOUNT);
                        calPCI_ADJUST_SBAR = (calPCI_TOTVARIANCE_STDEV / calGROUPCOUNT) / c4NN;
                    }


                    //*합동(Cpk)
                    calPCI_WITHIN_PSD = (calPCI_TOTVARIANCE / calSNN);
                    calPCI_ADJUST_PSD = Math.Sqrt(calPCI_TOTVARIANCE / calSNN);
                    calPCI_ADJUST_PSD_C4 = Math.Sqrt(calPCI_TOTVARIANCE / calSNN) / c4CC;

                    //*종합(PPK)
                    Console.WriteLine(c4NN);
                    //calPPI_TOTVARIANCE = calPPI_TOTVARIANCE;
                    calPPI_STAVARIANCE = calPPI_TOTVARIANCE / (totvgKK.ToSafeInt64() - 1);
                    calPPI_OVERALL = Math.Sqrt(calPPI_TOTVARIANCE / (totvgKK.ToSafeInt64() - 1));
                    calPPI_ADJUSTC4 = Math.Sqrt(calPPI_TOTVARIANCE / (totvgKK.ToSafeInt64() - 1)) / c4KK;

                    //*공정능력 계산값 전이 Start.----------------------------------------------------
                    rtnDataSet.Rows[0]["SAMPLINGCOUNT"] = totvgNN;
                    //rtnDataSet.Rows[0]["SUBGROUPCOUNT"] = KCOUNT;
                    //rtnDataSet.Rows[0]["PCOUNT"] = totvgCC;
                    rtnDataSet.Rows[0]["KCOUNT"] = KCOUNT;
                    rtnDataSet.Rows[0]["NVALUE_TOL"] = NVALUE_TOL;
                    rtnDataSet.Rows[0]["NVALUE_AVG"] = NVALUE_AVG;
                    rtnDataSet.Rows[0]["SVALUE_AVG"] = SVALUE_AVG.ToSafeDoubleZero();

                    rtnDataSet.Rows[0]["ISSAME"] = isSamplingNotSame;

                    rtnDataSet.Rows[0]["SVALUE_RTD"] = calPCI_WITHIN_RBAR;//R
                    rtnDataSet.Rows[0]["SVALUE_RTDC4"] = calPCI_ADJUST_RBAR;//R

                    rtnDataSet.Rows[0]["SVALUE_STD"] = calPCI_WITHIN_SBAR;//S
                    rtnDataSet.Rows[0]["SVALUE_STDC4"] = calPCI_ADJUST_SBAR;//S

                    rtnDataSet.Rows[0]["SVALUE_PTD"] = calPCI_ADJUST_PSD; //합동
                    rtnDataSet.Rows[0]["SVALUE_PTDC4"] = calPCI_ADJUST_PSD_C4;//합동

                    //rtnDataSet.Rows[0]["CP"] = CP;
                    //rtnDataSet.Rows[0]["CPL"] = CPL;
                    //rtnDataSet.Rows[0]["CPU"] = CPU;
                    //rtnDataSet.Rows[0]["CPK"] = CPK;
                    rtnDataSet.Rows[0]["PCI_d2"] = d2NN;

                    //rtnDataSet.Rows[0]["xx"] = calPPI_TOTVARIANCE;
                    //rtnDataSet.Rows[0]["xx"] = calPPI_STAVARIANCE;
                    rtnDataSet.Rows[0]["PVALUE_STD"] = calPPI_OVERALL;
                    rtnDataSet.Rows[0]["PVALUE_STDC4"] = calPPI_ADJUSTC4;
                    rtnDataSet.Rows[0]["PPI_c4"] = c4KK;

                    //rtnDataSet.Rows[0]["JUDGMENTCPK"] = , JUDGMENTCPK = case when @CPK > 1.33 then  'A' when @CPK <= 1.33 and @CPK > 1.00 then 'B' when @CPK <= 1.00 and @CPK >= 0.67 then  'C'   when @CPK< 0.67 then  'D'  else null   end;
                    rtnDataSet.Rows[0]["STATUS"] = outData.STATUS;
                    rtnDataSet.Rows[0]["STATUSMESSAGE"] = outData.STATUSMESSAGE;
                    //*공정능력 계산값 전이 End.----------------------------------------------------

                    //DataRow dr = tempDtPPIVARIANCE.NewRow();
                    //ppkvStdev = Math.Sqrt(f.PCI_VARIANCE_STDEV);
                    //dr["GROUPID"] = f.GROUPID;
                    //dr["SUBGROUP"] = f.SUBGROUP;
                    //dr["SAMPLING"] = f.SAMPLING;
                    //dr["PCI_VARIANCE"] = f.PCI_VARIANCE;
                    //dr["PCI_VARIANCE_STDEV"] = ppkvStdev;
                    //dr["PPI_VARIANCE"] = f.PPI_VARIANCE;
                    //dr["NN"] = f.NN;
                    //dr["SNN"] = f.SNN - 1;

                    //tempDtPPIVARIANCE.Rows.Add(dr);
                    //Console.WriteLine(f.PCI_VARIANCE);

                    //= Convert.ToDouble(row["NVALUE"]);
                }
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.HRESULT = ex.HResult;
                outData.ERRORNO = 10810;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            //rawData.Rows.Count
            //*Variance 처리.
            long vGROUPID = 0;
            long vSUBGROUP = 0;
            long vSAMPLEID = 0;
            string vSAMPLING = "";
            double vNVALUE = 0;
            double vFVALUE = 0;
            double vSVALUE = 0;
            JudgmentType judgmentCPK = JudgmentType.Terrible;
            JudgmentType judgmentPPK = JudgmentType.Terrible;

            TempPCIVARIANCE_DataTable tempPCIVARIANCE = new TempPCIVARIANCE_DataTable();

            try
            {
                for (i = 1; i < rawData.Rows.Count; i++)
                {
                    DataRow row = rawData.Rows[i];
                    DataRow rowFront = rawData.Rows[i - 1];

                    vGROUPID = Convert.ToInt64(row["GROUPID"]);
                    vSAMPLEID = Convert.ToInt64(row["SAMPLEID"]);
                    vSAMPLING = row["SAMPLING"].ToString();
                    vNVALUE = Convert.ToDouble(row["NVALUE"]);
                    vFVALUE = Convert.ToDouble(rowFront["NVALUE"]);
                    vSVALUE = Math.Abs(vFVALUE - vNVALUE);

                    DataRow rowPciVariance = tempPCIVARIANCE.NewRow();
                    rowPciVariance["TEMPID"] = 1; //i;
                    rowPciVariance["SAMPLEID"] = vSAMPLEID;
                    rowPciVariance["FVALUE"] = vNVALUE;
                    rowPciVariance["NVALUE"] = vFVALUE;
                    rowPciVariance["SVALUE"] = vSVALUE;
                    tempPCIVARIANCE.Rows.Add(rowPciVariance);
                }

                Console.WriteLine(tempPCIVARIANCE.Rows.Count);
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.HRESULT = ex.HResult;
                outData.ERRORNO = 10910;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            //sia확인 : 공정능력분석 - R, S, 합동 처리.,
            //*CPK sigma check
            switch (spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                    switch (spcOption.sigmaType)
                    {
                        case SigmaType.No:
                            //SVALUE_STD = calPCI_WITHIN_RBAR;//R
                            SVALUE_STD = calPCI_ADJUST_RBAR;//MiniTab에서는 XBar-R일 경우는 무조건 추정치로 군내로 계산함.
                            break;
                        case SigmaType.Yes:
                            SVALUE_STD = calPCI_ADJUST_RBAR;
                            break;
                        default:
                            SVALUE_STD = calPCI_ADJUST_RBAR;
                            break;
                    }
                    break;
                case ControlChartType.XBar_S:
                    switch (spcOption.sigmaType)
                    {
                        case SigmaType.No:
                            SVALUE_STD = calPCI_WITHIN_SBAR;
                            break;
                        case SigmaType.Yes:
                            SVALUE_STD = calPCI_ADJUST_SBAR;
                            break;
                        default:
                            SVALUE_STD = calPCI_ADJUST_SBAR;
                            break;
                    }
                    break;
                case ControlChartType.Merger:
                    switch (spcOption.sigmaType)
                    {
                        case SigmaType.No:
                            SVALUE_STD = calPCI_ADJUST_PSD;
                            break;
                        case SigmaType.Yes:
                            SVALUE_STD = calPCI_ADJUST_PSD_C4;
                            break;
                        default:
                            SVALUE_STD = calPCI_ADJUST_PSD_C4;
                            break;
                    }
                    break;
                case ControlChartType.I_MR:
                case ControlChartType.p:
                case ControlChartType.np:
                case ControlChartType.u:
                case ControlChartType.c:
                    break;
                default:
                    break;
            }

            //CP 계산
            if (USLNULL == "N" && LSLNULL == "N")
            {
                CP = (para.cpkTempSpec.usl - para.cpkTempSpec.lsl) / (6 * SVALUE_STD);
            }
            else
            {
                CP = SpcLimit.MIN;
            }
            
            //CPL
            if (LSLNULL == "N")
            {
                CPL = (NVALUE_AVG - para.cpkTempSpec.lsl) / (3 * SVALUE_STD);
            }
            else
            {
                CPL = SpcLimit.MIN;
            }

            //CPU
            if (USLNULL == "N")
            {
                CPU = (para.cpkTempSpec.usl - NVALUE_AVG) / (3 * SVALUE_STD);
            }
            else
            {
                CPU = SpcLimit.MIN;
            }

            CPK = null;

            if (USLNULL == "Y" && LSLNULL == "Y")
            {
                CPK = null;
            }
            else if (USLNULL == "N" && LSLNULL == "N")
            {
                CPK = (CPU > CPL ? CPL : CPU);
            }
            else if (USLNULL == "Y" && LSLNULL == "N")
            {
                CPK = CPL;
            }
            else if (USLNULL == "N" && LSLNULL == "Y")
            {
                CPK = CPU;
            }

            //CP 판정
            if (CPK != null && CPK != SpcLimit.MIN && CPK != SpcLimit.MAX)
            {
                judgmentCPK = SpcJudgment(CPK.ToSafeDoubleStaMin());
            }

            //*공정능력 계산값 전이 Start.----------------------------------------------------
            rtnDataSet.Rows[0]["SAMPLINGCOUNT"] = totvgNN;
            rtnDataSet.Rows[0]["SUBGROUPCOUNT"] = totvgGG;
            rtnDataSet.Rows[0]["PCOUNT"] = totvgCC;
            rtnDataSet.Rows[0]["KCOUNT"] = KCOUNT;
            rtnDataSet.Rows[0]["NVALUE_TOL"] = NVALUE_TOL;
            rtnDataSet.Rows[0]["NVALUE_AVG"] = NVALUE_AVG;
            rtnDataSet.Rows[0]["SVALUE_AVG"] = SVALUE_AVG.ToSafeDoubleZero();
            //rtnDataSet.Rows[0]["SVALUE_STDC4"] = SVALUE_STD;
            rtnDataSet.Rows[0]["CP"] = CP.ToSafeDoubleStaMin();
            rtnDataSet.Rows[0]["CPL"] = CPL.ToSafeDoubleStaMin();
            rtnDataSet.Rows[0]["CPU"] = CPU.ToSafeDoubleStaMin();
            rtnDataSet.Rows[0]["CPK"] = CPK.ToSafeDoubleStaMin();
            rtnDataSet.Rows[0]["PCI_d2"] = d2NN;
            rtnDataSet.Rows[0]["JUDGMENTCPK"] = judgmentCPK.ToString();
            rtnDataSet.Rows[0]["STATUS"] = outData.STATUS;
            rtnDataSet.Rows[0]["STATUSMESSAGE"] = outData.STATUSMESSAGE;

            //중심치 이탈 처리.
            double nAvg = SpcLimit.MIN;
            double nUSL = SpcLimit.MIN;
            double nLSL = SpcLimit.MIN;

            //중심치 이탈
            double nTargetPercent = SpcLimit.MIN;
            double nTargetUSL = SpcLimit.MIN;
            double nTargetCSL = SpcLimit.MIN;
            double nTargetLSL = SpcLimit.MIN;

            nAvg = NVALUE_AVG.ToSafeDoubleStaMax();
            nUSL = para.cpkTempSpec.usl.ToSafeDoubleStaMax();
            nLSL = para.cpkTempSpec.lsl.ToSafeDoubleStaMax();
            nTargetCSL = para.cpkTempSpec.csl.ToSafeDoubleStaMax();
            if (nTargetCSL != SpcLimit.MAX && nTargetCSL != SpcLimit.MIN)
            {
                
                if (USLNULL == "N" && LSLNULL == "N")
                {
                    nTargetPercent = (nAvg - nTargetCSL) / ((nUSL - nLSL) / 2) * 100;
                    nTargetUSL = nAvg * (nTargetPercent / 100);
                    nTargetUSL = nAvg + nTargetUSL;
                }
            }

            rtnDataSet.Rows[0]["TAUSL"] = nTargetUSL;
            rtnDataSet.Rows[0]["TACSL"] = nTargetCSL;
            rtnDataSet.Rows[0]["TALSL"] = nTargetPercent;

            //*I-MR 계산값 전이 End.----------------------------------------------------
            //Console.WriteLine(rtnDataSet.Rows.Count);

            //* PCI 공정능력 분석 Start---------------------------------------------------------------------------------------------------
            outData.STATUS = 11110;
            TempPPIDataTable tempTbPPIData = new TempPPIDataTable();
            try
            {
                for (i = 0; i < rawData.Rows.Count; i++)
                {
                    DataRow row = rawData.Rows[i];
                    vGROUPID = Convert.ToInt64(row["GROUPID"]);
                    vSAMPLEID = Convert.ToInt64(row["SAMPLEID"]);
                    vSAMPLING = row["SAMPLING"].ToString();
                    vNVALUE = Convert.ToDouble(row["NVALUE"]);
                    vFVALUE = Convert.ToDouble(NVALUE_AVG) - vNVALUE;
                    vSVALUE = vFVALUE * vFVALUE;

                    Console.WriteLine(string.Format("S[{0}] => {1}", vSAMPLEID, vSVALUE));

                    DataRow rowPpiVariance = tempTbPPIData.NewRow();
                    rowPpiVariance["TEMPID"] = 1; //i;
                    rowPpiVariance["GROUPID"] = vGROUPID;
                    rowPpiVariance["SAMPLEID"] = vSAMPLEID;
                    rowPpiVariance["FVALUE"] = vNVALUE;
                    rowPpiVariance["NVALUE"] = vFVALUE;
                    rowPpiVariance["SVALUE"] = vSVALUE;
                    tempTbPPIData.Rows.Add(rowPpiVariance);
                }

                Console.WriteLine(tempTbPPIData.Rows.Count);
            }
            catch (Exception ex)
            {
                outData.RESULTFLAG = 1;
                outData.HRESULT = ex.HResult;
                outData.ERRORNO = 10910;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            //추정치 결과값 반환.
            try
            {
                var query = from order in tempTbPPIData.AsEnumerable()
                            group order by order.Field<long>("GROUPID") into g
                            select new
                            {
                                SAMPLEID = g.Key,
                                PVALUEAVG = g.Sum(f => f.Field<double?>("SVALUE")),
                                PVALUESTDD = g.Sum(f => f.Field<double?>("SVALUE")),
                                PVALUESTDC4 = g.Sum(f => f.Field<double?>("SVALUE")),
                            };

                foreach (var order in query)
                {
                    SAMPLE_ID = order.SAMPLEID;
                    PVALUE_AVG = Convert.ToDouble(order.PVALUEAVG) / (KCOUNT - 1);
                    PVALUE_STDD = Math.Sqrt(Convert.ToDouble(PVALUE_AVG));
                    PVALUE_STDC4 = PVALUE_STDD / c4KK;
                    //Console.WriteLine("Summary : SAMPLE_ID = {0},  PVALUE_AVG = {1}, PVALUE_STDD = {2}, PVALUE_STDC4 = {3}"
                    //    , SAMPLE_ID, PVALUE_AVG, PVALUE_STDD, PVALUE_STDC4);
                }
            }
            catch (Exception ex)
            {
                //outData.ERRORNO = ex.HResult;
                outData.STATUS = 211110;
                outData.ERRORNO = 11110;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            switch (spcOption.sigmaType)
            {
                case SigmaType.No:
                    PPKVALUE_STD = PVALUE_STDD;//PPK 추정치 미사용 // calPCI_WITHIN_SBAR;
                    break;
                case SigmaType.Yes:
                    PPKVALUE_STD = PVALUE_STDC4;//PPK 추정치 사용
                    break;
                default:
                    PPKVALUE_STD = PVALUE_STDC4;//PPK 추정치 사용
                    break;
            }

            PP = SpcLimit.MIN;
            PPL = SpcLimit.MIN;
            PPU = SpcLimit.MIN;

            //PP
            if (USLNULL == "N" && LSLNULL == "N")
            {
                PP = (para.cpkTempSpec.usl - para.cpkTempSpec.lsl) / (6 * PPKVALUE_STD);
            }
            //PPL
            if (LSLNULL == "N")
            {
                PPL = (NVALUE_AVG - para.cpkTempSpec.lsl) / (3 * PPKVALUE_STD);
            }
            //PPU
            if (USLNULL == "N")
            {
                PPU = (para.cpkTempSpec.usl - NVALUE_AVG) / (3 * PPKVALUE_STD);
            }
            
            PPK = null;
            if (USLNULL == "Y" && LSLNULL == "Y")
            {
                PPK = null;
            }
            else if (USLNULL == "N" && LSLNULL == "N")
            {
                PPK = (PPU > PPL ? PPL : PPU);
            }
            else if (USLNULL == "Y" && LSLNULL == "N")
            {
                PPK = PPL;
            }
            else if (USLNULL == "N" && LSLNULL == "Y")
            {
                PPK = PPU;
            }

            //PPK 판정
            if (PPK != null && PPK != SpcLimit.MIN && PPK != SpcLimit.MAX)
            {
                judgmentPPK = SpcJudgment(PPK.ToSafeDoubleStaMin());
            }

            //*PPK 계산값 전이 Start.----------------------------------------------------
            rtnDataSet.Rows[0]["PVALUE_AVG"] = NVALUE_AVG;
            rtnDataSet.Rows[0]["PVALUE_STD"] = PVALUE_STDD;
            rtnDataSet.Rows[0]["PVALUE_STDC4"] = PVALUE_STDC4;
            rtnDataSet.Rows[0]["PP"] = PP;
            rtnDataSet.Rows[0]["PPL"] = PPL;
            rtnDataSet.Rows[0]["PPU"] = PPU;
            rtnDataSet.Rows[0]["PPK"] = PPK;
            rtnDataSet.Rows[0]["PPI_c4"] = c4KK;
            rtnDataSet.Rows[0]["JUDGMENTPPK"] = judgmentPPK.ToString();

            //rtnDataSet.Rows[0]["STATUS"] = outData.STATUS;
            //rtnDataSet.Rows[0]["STATUSMESSAGE"] = outData.STATUSMESSAGE;
            //*PPK 계산값 전이 End.----------------------------------------------------
            //Console.WriteLine(rtnDataSet.Rows.Count);


            #region PPM 계산 처리.
            double ppmWithinLSL = 0, ppmWithinUSL = 0, ppmWithinTOT = 0;
            double ppmWithinLSLRound = 0, ppmWithinUSLRound = 0, ppmWithinTOTRound = 0;
            double ppmOverallLSL = 0, ppmOverallUSL = 0, ppmOverallTOT = 0;
            double ppmOverallLSLRound = 0, ppmOverallUSLRound = 0, ppmOverallTOTRound = 0;
            double ppmObserveLSLN = 0, ppmObserveUSLN = 0;
            double ppmObserveLSL = 0, ppmObserveUSL = 0, ppmObserveTOT = 0;
            double ppmObserveLSLRound = 0, ppmObserveUSLRound = 0, ppmObserveTOTRound = 0;

            //PPM < LSL 규격 하한
            if (LSLNULL == "N")
            {
                ppmWithinLSL = SPCLibs.SpcPpm(SpcPpmMode.Within, SpcSpecType.LSL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.lsl, SVALUE_STD.ToSafeDoubleStaMin(), out ppmWithinLSLRound);
                ppmOverallLSL = SPCLibs.SpcPpm(SpcPpmMode.Total, SpcSpecType.LSL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.lsl, PPKVALUE_STD.ToSafeDoubleStaMin(), out ppmOverallLSLRound);
                ppmObserveLSL = SPCLibs.SpcPpmObserve(SpcPpmMode.Observe, SpcSpecType.LSL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.lsl, KCOUNT.ToSafeDoubleStaMin(), singleRawData, out ppmObserveLSLN, out ppmObserveLSLRound);
                
                ppmWithinTOT = ppmWithinLSL;
                ppmOverallTOT = ppmOverallLSL;
                ppmObserveTOT = ppmObserveLSL;

                ppmWithinTOTRound = ppmWithinLSLRound;
                ppmOverallTOTRound = ppmOverallLSLRound;
                ppmObserveTOTRound = ppmObserveLSLRound;

            }
            else
            {
                ppmWithinLSL = SpcLimit.MIN;
                ppmOverallLSL = SpcLimit.MIN;
                ppmObserveLSL = SpcLimit.MIN;
            }

            //PPM > USL 규격 상한
            if (USLNULL == "N")
            {
                ppmWithinUSL = SPCLibs.SpcPpm(SpcPpmMode.Within, SpcSpecType.USL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.usl, SVALUE_STD.ToSafeDoubleStaMin(), out ppmWithinUSLRound);
                ppmOverallUSL = SPCLibs.SpcPpm(SpcPpmMode.Total, SpcSpecType.USL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.usl, PPKVALUE_STD.ToSafeDoubleStaMin(), out ppmOverallUSLRound);
                ppmObserveUSL = SPCLibs.SpcPpmObserve(SpcPpmMode.Observe, SpcSpecType.USL, NVALUE_AVG.ToSafeDoubleStaMin(), para.cpkTempSpec.usl, KCOUNT.ToSafeDoubleStaMin(), singleRawData, out ppmObserveUSLN, out ppmObserveUSLRound);

                ppmWithinTOT = ppmWithinUSL;
                ppmOverallTOT = ppmOverallUSL;
                ppmObserveTOT = ppmObserveUSL;

                ppmWithinTOTRound = ppmWithinUSLRound;
                ppmOverallTOTRound = ppmOverallUSLRound;
                ppmObserveTOTRound = ppmObserveUSLRound;
            }
            else
            {
                ppmWithinUSL = SpcLimit.MIN;
                ppmOverallUSL = SpcLimit.MIN;
                ppmObserveUSL = SpcLimit.MIN;
            }

            //PPM > TOTAL (LSL & USL)
            if (USLNULL == "N" && LSLNULL == "N")
            {
                ppmWithinTOT = ppmWithinLSL + ppmWithinUSL;
                ppmOverallTOT = ppmOverallLSL + ppmOverallUSL;
                ppmObserveTOT = ppmObserveLSL + ppmObserveUSL;

                ppmWithinTOTRound = ppmWithinLSLRound + ppmWithinUSLRound;
                ppmOverallTOTRound = ppmOverallLSLRound + ppmOverallUSLRound;
                ppmObserveTOTRound = ppmObserveLSLRound + ppmObserveUSLRound;
            }

            //*PPK 계산값 전이 Start.----------------------------------------------------
            rtnDataSet.Rows[0]["PPMWITHINLSL"] = ppmWithinLSL;
            rtnDataSet.Rows[0]["PPMWITHINUSL"] = ppmWithinUSL;
            rtnDataSet.Rows[0]["PPMWITHINTOT"] = ppmWithinTOT;
            rtnDataSet.Rows[0]["PPMOVERALLLSL"] = ppmOverallLSL;
            rtnDataSet.Rows[0]["PPMOVERALLUSL"] = ppmOverallUSL;
            rtnDataSet.Rows[0]["PPMOVERALLTOT"] = ppmOverallTOT;
            rtnDataSet.Rows[0]["PPMOBSERVELSLN"] = ppmObserveLSLN;
            rtnDataSet.Rows[0]["PPMOBSERVEUSLN"] = ppmObserveUSLN;
            rtnDataSet.Rows[0]["PPMOBSERVELSL"] = ppmObserveLSL;
            rtnDataSet.Rows[0]["PPMOBSERVEUSL"] = ppmObserveUSL;
            rtnDataSet.Rows[0]["PPMOBSERVETOT"] = ppmObserveTOT;
            //*PPK 계산값 전이 End.----------------------------------------------------
            //Console.WriteLine(rtnDataSet.Rows.Count);
            Console.WriteLine(string.Format("ppmWithinTOT => {0} ", ppmWithinTOT));
            Console.WriteLine(string.Format("ppmOverallTOT => {0} ", ppmOverallTOT));

            #endregion PPM 계산 처리.


            //*Message 및 상태 입력 Start----------------------------------------------------
            rtnDataSet.Rows[0]["STATUS"] = outData.STATUS;
            rtnDataSet.Rows[0]["STATUSMESSAGE"] = outData.STATUSMESSAGE;
            //*Message 및 상태 입력 End.----------------------------------------------------


        //*종료
        NEXT_NED:

            return rtnDataSet;
        }

        /// <summary>
        /// 공정능력 판정 처리.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static JudgmentType SpcJudgment(double data)
        {

            JudgmentType result = JudgmentType.Terrible;
            if (2.00f <= data)
            {
                result = JudgmentType.Terrific;
            }
            else if (1.67f <= data && data < 2.00f)
            {
                result = JudgmentType.Excellent;
            }
            else if (1.33f <= data && data < 1.67f)
            {
                result = JudgmentType.Good;
            }
            else if (1.00f <= data && data < 1.33f)
            {
                result = JudgmentType.Fair;
            }
            else if (0.67f <= data && data < 1.00f)
            {
                result = JudgmentType.Poor;
            }
            else if (data < 0.67f)
            {
                result = JudgmentType.Terrible;
            }
            else
            {
                result = JudgmentType.Terrible;
            }
                
            return result;

        }

        #endregion

        #region 관리도
        
        /// <summary>관리도 - XBar-R
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnControlDataTable SpcLibXBarR(SPCPara para, SPCOption spcOption, ref SPCOutData outData)
        {
            RtnControlDataTable rtnDataSet = new RtnControlDataTable();
            DataTable rawData = para.InputData;
            ParPIDataTable dbPara = para.InputData;
            SpcStatusMessage subStatus = new SpcStatusMessage();

            int i = 0;
            long idSeq = 0;
            bool isSamplingNotSame = false;
            long? nNNSameCheck = 0;
            string tempSubGroup = "";
            SubGroupSum sg = new SubGroupSum();
            TempControlDataSTADataTable ctrDataSta = new TempControlDataSTADataTable();
            try
            {
                var sumSubGroup = from b in dbPara
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP,
                                      b.SAMPLEID,
                                      b.SAMPLING
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSAMPLEID = g.Key.SAMPLEID,
                                      vSAMPLING = g.Key.SAMPLING,
                                      vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),//추가 8/7
                                      vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),//추가 8/7
                                      vMAX = g.Max(s => s.NVALUE),
                                      vMIN = g.Min(s => s.NVALUE),
                                      vSUM = g.Sum(s => s.NVALUE),
                                      vAVG = g.Average(s => s.NVALUE),
                                      vNN = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in sumSubGroup)
                {
                    //Console.WriteLine("ContactID = {0} \t TotalDue sum = {1}", order.min, order.max);
                    sg.GROUPID = f.vGROUPID;
                    sg.SUBGROUP = f.vSUBGROUP;
                    sg.SAMPLEID = f.vSAMPLEID;
                    sg.SAMPLING = f.vSAMPLING;
                    sg.SUBGROUPNAME = f.vSUBGROUPNAME;
                    sg.SAMPLINGNAME = f.vSAMPLINGNAME;
                    sg.MAX = f.vMAX;
                    sg.MIN = f.vMIN;
                    sg.R = sg.MAX - sg.MIN;
                    sg.SUM = f.vSUM;
                    sg.AVG = f.vAVG;
                    sg.NN = f.vNN;
                    sg.COUNT = f.vCOUNT;

                    if (sg.SUBGROUP != tempSubGroup)
                    {
                        tempSubGroup = sg.SUBGROUP;
                        nNNSameCheck = 0;
                        isSamplingNotSame = false;
                    }

                    if (nNNSameCheck == 0)//1/7추가
                    {
                        nNNSameCheck = sg.NN;
                    }
                    else
                    {
                        if (sg.NN != nNNSameCheck && isSamplingNotSame == false)
                        {
                            isSamplingNotSame = true;
                        }
                    }

                    idSeq++;
                    DataRow sgRow = ctrDataSta.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = sg.GROUPID;
                    sgRow["SUBGROUP"] = sg.SUBGROUP;
                    sgRow["SAMPLING"] = sg.SAMPLING;
                    sgRow["SUBGROUPNAME"] = sg.SUBGROUPNAME;
                    sgRow["SAMPLINGNAME"] = sg.SAMPLINGNAME;
                    sgRow["ISSAME"] = isSamplingNotSame;
                    sgRow["MAX"] = sg.MAX;
                    sgRow["MIN"] = sg.MIN;
                    sgRow["R"] = sg.R;
                    //sgRow["RUCL"] = "";
                    //sgRow["RLCL"] = "";
                    sgRow["BAR"] = sg.AVG;
                    //sgRow["UCL"] = "";
                    //sgRow["LCL"] = "";
                    sgRow["SUMVALUE"] = sg.SUM;
                    //sgRow["TOTSUM"] = "";
                    //sgRow["TOTAVGVALUE"] = "";
                    sg.STDE = StdDevPpCd(dbPara, sg, out outData.ERRORNO, out outData.ERRORMESSAGE);
                    
                    SpcFunction.IsDbNckDoubleWrite(sgRow, "STDEVALUE", sg.STDE);

                    sgRow["NN"] = sg.NN;
                    sgRow["SNN"] = sg.NN - 1;
                    //sgRow["TOTNN"] = "";
                    //sgRow["GROUPNN"] = "";

                    ctrDataSta.Rows.Add(sgRow);

                    Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                        "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                        , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            Console.WriteLine(ctrDataSta.Rows.Count);

            //*RBar 계산.
            TempControlDataTotDataTable ctrDataTot = new TempControlDataTotDataTable();
            try
            {
                var totSubGroup = from b in ctrDataSta
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vXBAR = g.Average(s => s.BAR),
                                      vSUMVALUE = g.Sum(s => s.SUMVALUE),//1/7추가
                                      vSUMNN = g.Sum(s => s.NN),//1/7추가
                                      vISSAME = g.Max(s => s.ISSAME),//1/7추가
                                      vRBAR = g.Average(s => s.R),
                                      vSUBGROUPCOUNT = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in totSubGroup)
                {
                    idSeq++;
                    DataRow sgRow = ctrDataTot.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = f.vGROUPID;
                    sgRow["SUBGROUP"] = f.vSUBGROUP;

                    isSamplingNotSame = f.vISSAME;
                    if (isSamplingNotSame)
                    {
                        sgRow["XBAR"] = f.vSUMVALUE / f.vSUMNN;//1/7추가
                    }
                    else
                    {
                        sgRow["XBAR"] = f.vXBAR;
                    }

                    sgRow["ISSAME"] = isSamplingNotSame;
                    sgRow["RBAR"] = f.vRBAR;
                    sgRow["GROUPNN"] = f.vCOUNT;
                    ctrDataTot.Rows.Add(sgRow);

                    //Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            Console.WriteLine(ctrDataTot.Rows.Count);

            double A2 = 0.0;
            double d2 = 0.0;
            double d3 = 0.0;
            double D3 = 0.0;
            double D4 = 0.0;
            double c4 = 0.0;
            double fx = 0.0;
            double fxSum = 0.0;
            double fr = 0.0;
            double frd2 = 0.0;
            double frd2Sum = 0.0;
            double sameRSigma = 0.0;
            long nNN = 0;
            para.rtnXBar = ReturnXBarResult.Create();
            SamplingGroupSum grpData = new SamplingGroupSum();

            int nPageId = 1;
            int nPageIdAdd = 0;
            int nPageMax = 4;
            idSeq = 1;
            string subGroupTemp = string.Empty;
            SpcPageField pageDataTemp = SpcPageField.Create();

            outData.spcDataTable.tableSubGroupSta.Rows.Clear();
            outData.spcDataTable.tableNavigator.Rows.Clear();


            #region Sampling 개수가 같지 않을 경우 처리.
            try
            {
                //ctrDataSta
                for (i = 0; i < ctrDataTot.Rows.Count; i++)
                {
                    DataRow sgRow = ctrDataTot.Rows[i];
                    grpData.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                    grpData.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                    //grpData.SAMPLING = sgRow["SAMPLING"].ToSafeString();

                    isSamplingNotSame = Convert.ToBoolean(sgRow["ISSAME"]);
                    if (isSamplingNotSame)
                    {

                        var ctrSubGroup = ctrDataSta.Where(x => x.GROUPID == grpData.GROUPID && x.SUBGROUP == grpData.SUBGROUP);
                        foreach (var sp in ctrSubGroup)
                        {
                            grpData.GROUPID = sp.GROUPID;
                            grpData.SUBGROUP = sp.SUBGROUP;
                            grpData.SAMPLING = sp.SAMPLING;

                            grpData.SUBGROUPNAME = sp.SUBGROUPNAME;
                            grpData.SAMPLINGNAME = sp.SAMPLINGNAME;
                            grpData.MAX = sp.MAX;
                            grpData.MIN = sp.MIN;
                            grpData.R = sp.R;
                            grpData.BAR = sp.BAR;
                            grpData.NN = sp.NN;
                            grpData.SNN = sp.SNN.ToSafeInt64();

                            d2 = SPCSta.GetCoed2(grpData.NN.ToSafeInt64());
                            d3 = SPCSta.GetCoed3(grpData.NN.ToSafeInt64());

                            fx = (d2 * d2) / (d3 * d3);
                            fxSum += fx;
                            fr = fx * grpData.R.ToSafeDoubleZero();
                            frd2 = fr / d2;
                            frd2Sum += frd2;
                        }

                        sameRSigma = frd2Sum / fxSum;
                        sgRow["SAMESIGMA"] = sameRSigma;
                        Console.WriteLine("sameRSigma: {0}", sameRSigma);
                        Console.WriteLine("sameRSigma: {0}", sameRSigma);
                    }
                    else
                    {
                    }
                }
                Console.WriteLine("ctrDataTot: {0}", ctrDataTot.Rows.Count);
                Console.WriteLine("ctrDataTot: {0}", ctrDataTot.Rows.Count);
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }
            #endregion Sampling 개수가 같지 않을 경우 처리.

            try
            {
                //ctrDataSta
                for (i = 0; i < ctrDataSta.Rows.Count; i++)
                {
                    DataRow sgRow = ctrDataSta.Rows[i];
                    grpData.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                    grpData.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                    grpData.SAMPLING = sgRow["SAMPLING"].ToSafeString();
                    grpData.SUBGROUPNAME = sgRow["SUBGROUPNAME"].ToSafeString();
                    grpData.SAMPLINGNAME = sgRow["SAMPLINGNAME"].ToSafeString();
                    grpData.MAX = sgRow["MAX"].ToNullOrDouble();
                    grpData.MIN = sgRow["MIN"].ToNullOrDouble();
                    grpData.R = sgRow["R"].ToNullOrDouble();
                    grpData.BAR = sgRow["BAR"].ToNullOrDouble();

                    //grpData.SUMVALUE = sgRow["SUMVALUE"].ToSafeDouble();
                    //grpData.STDEVALUE = sgRow["STDEVALUE"].ToSafeDouble();
                    grpData.NN = sgRow["NN"].ToSafeInt64();
                    grpData.SNN = sgRow["SNN"].ToSafeInt64();
                    
                    var ctrTot = ctrDataTot.Where(x => x.GROUPID == grpData.GROUPID && x.SUBGROUP == grpData.SUBGROUP);
                    
                    foreach (var f in ctrTot)
                    {
                        grpData.XBAR = f.XBAR;
                        grpData.RBAR = f.RBAR;
                        grpData.GROUPCOUNT = f.GROUPNN;
                        grpData.SAMESIGMA = 0.0;

                        ctrDataSta.Rows[i]["XBAR"] = grpData.XBAR;
                        ctrDataSta.Rows[i]["RBAR"] = grpData.RBAR;
                        ctrDataSta.Rows[i]["GROUPNN"] = grpData.GROUPCOUNT;
                        nNN = grpData.NN.ToSafeInt64();
                        A2 = SPCSta.GetCoeA2(nNN);
                        d2 = SPCSta.GetCoed2(nNN);
                        D3 = SPCSta.GetCoeD3(nNN);
                        d3 = SPCSta.GetCoed3(nNN);
                        D4 = SPCSta.getCoeD4(nNN);
                        c4 = SPCSta.GetCoe("c4", grpData.SNN.ToSafeInt64());

                        //sia확인 : XBAR-R Lib...
                        double nSameSigma = 0.0;
                        isSamplingNotSame = f.ISSAME;
                        if (isSamplingNotSame == false)
                        {
                            //XBar
                            grpData.UCL = grpData.XBAR + (A2 * grpData.RBAR);
                            grpData.CL = grpData.XBAR;
                            grpData.LCL = grpData.XBAR - (A2 * grpData.RBAR);

                            //R
                            grpData.STDEP = grpData.RBAR / d2;
                            //grpData.RUCL = D4 * grpData.RBAR;
                            //grpData.RCL = grpData.RBAR;
                            //grpData.RLCL = D3 * grpData.RBAR;
                            grpData.RUCL = (d2 * grpData.STDEP) + (3 * grpData.STDEP * d3);
                            grpData.RCL = grpData.STDEP;
                            grpData.RLCL = (d2 * grpData.STDEP) - (3 * grpData.STDEP * d3);
                            if (grpData.RLCL < 0)
                            {
                                grpData.RLCL = 0.0;
                            }
                        }
                        else
                        {
                            grpData.SAMESIGMA = f.SAMESIGMA;
                            double nks = 0.0;
                            double nsqNN = 0.0;
                            double nksSqtNN = 0.0;
                            
                            double nd2Sigma = 0.0;
                            double nd3Sigma = 0.0;
                            //double nXBar = 0.0;

                            //nXBar = grpData.XBAR.ToSafeDoubleZero();
                            nSameSigma = grpData.SAMESIGMA.ToSafeDoubleZero();
                            nks = 3 * nSameSigma;
                            nsqNN = Math.Sqrt(nNN);
                            nksSqtNN = nks / nsqNN;
                            //nksSqtNN = Math.Round(nks / nsqNN, 9);
                            nd2Sigma = d2 * nSameSigma;
                            nd3Sigma = d3 * nks;
                            //XBar
                            grpData.UCL = grpData.XBAR + nksSqtNN;
                            grpData.CL = grpData.XBAR;
                            grpData.LCL = grpData.XBAR - nksSqtNN;

                            //R
                            grpData.STDEP = grpData.RBAR / d2;
                            grpData.RUCL = nd2Sigma + nd3Sigma;
                            grpData.RCL = d2 * nSameSigma;
                            grpData.RLCL = nd2Sigma - nd3Sigma;
                            if (grpData.RLCL < 0)
                            {
                                grpData.RLCL = 0.0;
                            }
                            
                        }

                        DataRow drRtn = rtnDataSet.NewRow();

                        drRtn["TEMPID"] = i;
                        drRtn["GROUPID"] = grpData.GROUPID;
                        drRtn["SUBGROUP"] = grpData.SUBGROUP;
                        drRtn["SAMPLING"] = grpData.SAMPLING;
                        drRtn["SUBGROUPNAME"] = grpData.SUBGROUPNAME;
                        drRtn["SAMPLINGNAME"] = grpData.SAMPLINGNAME;
                        drRtn["SAMESIGMA"] = nSameSigma;
                        drRtn["ISSAME"] = isSamplingNotSame;

                        drRtn["MAX"] = grpData.MAX;
                        drRtn["MIN"] = grpData.MIN;
                        drRtn["R"] = grpData.R;

                        if (isSamplingNotSame == false)
                        {
                            drRtn["RBAR"] = grpData.RBAR;
                        }
                        else
                        {
                            drRtn["RBAR"] = grpData.RCL;
                        }
                        
                        drRtn["RUCL"] = grpData.RUCL;
                        drRtn["RLCL"] = grpData.RLCL;
                        drRtn["RCL"] = grpData.RCL;
                        drRtn["BAR"] = grpData.BAR;
                        drRtn["UCL"] = grpData.UCL;
                        drRtn["LCL"] = grpData.LCL;
                        drRtn["CL"] = grpData.CL;
                        drRtn["XBAR"] = grpData.XBAR;
                        
                        //drRtn["SUMVALUE"] = grpData.SUMVALUE;
                        SpcFunction.IsDbNckDoubleWrite(drRtn, "TOTSUM", grpData.TOTSUM);
                        //drRtn["TOTAVGVALUE"] = grpData.TOTAVGVALUE;
                        //drRtn["STDEVALUE"] = grpData.STDEVALUE;
                        drRtn["STDEVALUE"] = grpData.STDEP;

                        drRtn["NN"] = grpData.NN;
                        drRtn["SNN"] = grpData.SNN;
                        drRtn["TOTNN"] = grpData.TOTNN;
                        drRtn["GROUPNN"] = grpData.GROUPCOUNT;

                        rtnDataSet.Rows.Add(drRtn);

                        //sia확인 : 관리도 XBar-R Page 처리

                        #region Page 처리

                        DataRow spcReturnRow = outData.spcDataTable.tableSubGroupSta.NewRow();
                        spcReturnRow["TEMPID"] = i;
                        spcReturnRow["PAGEID"] = nPageId;
                        spcReturnRow["GROUPID"] = grpData.GROUPID.ToSafeInt64();
                        spcReturnRow["SUBGROUP"] = grpData.SUBGROUP;
                        spcReturnRow["XBAR"] = grpData.XBAR.ToSafeDoubleStaMax();
                        spcReturnRow["RBAR"] = grpData.RBAR.ToSafeDoubleStaMax();
                        spcReturnRow["GROUPNN"] = grpData.SNN;
                        outData.spcDataTable.tableSubGroupSta.Rows.Add(spcReturnRow);

                        if (nPageIdAdd > nPageMax)
                        {
                            DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                            spcNavigatorRow["TEMPID"] = nPageId;
                            spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                            spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                            spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                            spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                            spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                            spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                            spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                            spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                            spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                            spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                            spcNavigatorRow["GROUPNN"] = grpData.NN;
                            outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                            nPageId++;
                            nPageIdAdd = 1;

                            pageDataTemp.Start.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.Start.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.Start.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.Start.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.Start.nRBAR = pageDataTemp.Temp.nRBAR;

                            pageDataTemp.End.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.End.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.End.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.End.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.End.nRBAR = pageDataTemp.Temp.nRBAR;
                        }

                        if (nPageIdAdd == 0)
                        {
                            pageDataTemp.Start.nSEQID = i;
                            pageDataTemp.Start.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.Start.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.Start.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.Start.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        //SubGroup 구분
                        if (subGroupTemp != grpData.SUBGROUP)
                        {
                            subGroupTemp = grpData.SUBGROUP;
                            nPageIdAdd++;
                            if (nPageIdAdd > nPageMax)
                            {
                                pageDataTemp.Temp.nSEQID = i;
                                pageDataTemp.Temp.nGROUPID = grpData.GROUPID.ToSafeInt64();
                                pageDataTemp.Temp.SUBGROUP = grpData.SUBGROUP;
                                pageDataTemp.Temp.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                                pageDataTemp.Temp.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            }
                        }

                        if (nPageIdAdd <= nPageMax)
                        {
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        #endregion
                    }

                    //Console.WriteLine("XBar-R Summary : GROUPID = {0},  SUBGROUP = {1}, SAMPLEID = {2},  SAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , grpData.GROUPID, grpData.SUBGROUP, grpData.SAMPLEID, grpData.SAMPLING
                    //    , grpData.XBAR, grpData.RBAR, grpData.NN, grpData.GROUPCOUNT);
                }

                //마지막 Page Check
                if (nPageIdAdd <= nPageMax)
                {
                    DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                    spcNavigatorRow["TEMPID"] = nPageId;
                    spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                    spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                    spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                    spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                    spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;
                    spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                    spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                    spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                    spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                    spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                    spcNavigatorRow["GROUPNN"] = nPageId;
                    outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                }

                //1 Page Chart 4개 일때 Check 
                if (outData.spcDataTable.tableNavigator != null && outData.spcDataTable.tableNavigator.Rows.Count <= 0)
                {
                    if (nPageIdAdd > nPageMax)
                    {
                        DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                        spcNavigatorRow["TEMPID"] = nPageId;
                        spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                        spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                        spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                        spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                        spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;
                        spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                        spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                        spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                        spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                        spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                        spcNavigatorRow["GROUPNN"] = nPageId;
                        outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                    }
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }


        //*종료
        NEXT_NED:

            return rtnDataSet;
        }


        /// <summary>관리도 - XBar-S
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnControlDataTable SpcLibXBarS(SPCPara para, SPCOption spcOption, ref SPCOutData outData)
        {
            RtnControlDataTable rtnDataSet = new RtnControlDataTable();
            DataTable rawData = para.InputData;
            ParPIDataTable dbPara = para.InputData;
            int i = 0;
            long idSeq = 0;
            bool isSamplingNotSame = false;
            long? nNNSameCheck = 0;
            string tempSubGroup = "";

            SubGroupSum sg = new SubGroupSum();
            TempControlDataSTADataTable ctrDataSta = new TempControlDataSTADataTable();
            try
            {
                var sumSubGroup = from b in dbPara
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP,
                                      b.SAMPLEID,
                                      b.SAMPLING
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSAMPLEID = g.Key.SAMPLEID,
                                      vSAMPLING = g.Key.SAMPLING,
                                      vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),//8/8
                                      vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),//8/8
                                      vMAX = g.Max(s => s.NVALUE),
                                      vMIN = g.Min(s => s.NVALUE),
                                      vSUM = g.Sum(s => s.NVALUE),
                                      vAVG = g.Average(s => s.NVALUE),
                                      vNN = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in sumSubGroup)
                {
                    sg.GROUPID = f.vGROUPID;
                    sg.SUBGROUP = f.vSUBGROUP;
                    sg.SAMPLEID = f.vSAMPLEID;
                    sg.SAMPLING = f.vSAMPLING;
                    sg.SUBGROUPNAME = f.vSUBGROUPNAME;
                    sg.SAMPLINGNAME = f.vSAMPLINGNAME;
                    sg.MAX = f.vMAX;
                    sg.MIN = f.vMIN;
                    sg.R = sg.MAX - sg.MIN;
                    sg.SUM = f.vSUM;
                    sg.AVG = f.vAVG;
                    sg.NN = f.vNN;
                    sg.COUNT = f.vCOUNT;

                    if (sg.SUBGROUP !=tempSubGroup)
                    {
                        tempSubGroup = sg.SUBGROUP;
                        nNNSameCheck = 0;
                        isSamplingNotSame = false;
                    }

                    if (nNNSameCheck == 0)//1/7추가
                    {
                        nNNSameCheck = sg.NN;
                    }
                    else
                    {
                        if (sg.NN != nNNSameCheck && isSamplingNotSame == false)
                        {
                            isSamplingNotSame = true;
                        }
                    }

                    spcOption.isSame = isSamplingNotSame;

                    idSeq++;
                    DataRow sgRow = ctrDataSta.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = sg.GROUPID;
                    sgRow["SUBGROUP"] = sg.SUBGROUP;
                    sgRow["SAMPLING"] = sg.SAMPLING;
                    sgRow["SUBGROUPNAME"] = sg.SUBGROUPNAME;
                    sgRow["SAMPLINGNAME"] = sg.SAMPLINGNAME;

                    sgRow["ISSAME"] = isSamplingNotSame;

                    sgRow["MAX"] = sg.MAX;
                    sgRow["MIN"] = sg.MIN;
                    //sgRow["R"] = sg.R;
                    //sgRow["RUCL"] = "";
                    //sgRow["RLCL"] = "";
                    sgRow["BAR"] = sg.AVG;
                    //sgRow["UCL"] = "";
                    //sgRow["LCL"] = "";
                    sgRow["SUMVALUE"] = sg.SUM;
                    //sgRow["TOTSUM"] = "";
                    //sgRow["TOTAVGVALUE"] = "";
                    //sg.STDE = StdDevPpCd(dbPara, sg);
                    sg.STDE = StdDevP(dbPara, sg);
                    sgRow["STDEVALUE"] = sg.STDE;
                    sgRow["R"] = sg.STDE;
                    sgRow["NN"] = sg.NN;
                    sgRow["SNN"] = sg.NN - 1;
                    //sgRow["TOTNN"] = "";
                    //sgRow["GROUPNN"] = "";

                    ctrDataSta.Rows.Add(sgRow);

                    Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                        "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                        , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            Console.WriteLine(ctrDataSta.Rows.Count);

            //*SBar 계산.
            TempControlDataTotDataTable ctrDataTot = new TempControlDataTotDataTable();
            try
            {
                var totSubGroup = from b in ctrDataSta //where b.NVALUE !=1
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vXBAR = g.Average(s => s.BAR),
                                      vSUMVALUE = g.Sum(s => s.SUMVALUE),//1/7추가
                                      vSUMNN = g.Sum(s => s.NN),//1/7추가
                                      vISSAME = g.Max(s => s.ISSAME),//1/7추가
                                      vRBAR = g.Average(s => s.R),
                                      vSTDEVP = g.Sum(s => s.STDEVALUE),
                                      vSUBGROUPCOUNT = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in totSubGroup)
                {
                    idSeq++;
                    DataRow sgRow = ctrDataTot.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = f.vGROUPID;
                    sgRow["SUBGROUP"] = f.vSUBGROUP;
                    //sgRow["XBAR"] = f.vXBAR;
                    isSamplingNotSame = f.vISSAME;

                    if (isSamplingNotSame)
                    {
                        sgRow["XBAR"] = f.vSUMVALUE / f.vSUMNN;//1/7추가
                    }
                    else
                    {
                        sgRow["XBAR"] = f.vXBAR;
                    }
                    sgRow["ISSAME"] = isSamplingNotSame;
                    sgRow["RBAR"] = f.vRBAR;
                    sgRow["RBAR"] = f.vRBAR;
                    sgRow["SUMSTDEVP"] = f.vSTDEVP;
                    sgRow["SUMSTDEVPBAR"] = f.vSTDEVP / f.vCOUNT;
                    sgRow["GROUPNN"] = f.vCOUNT;
                    ctrDataTot.Rows.Add(sgRow);

                    //Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            Console.WriteLine(ctrDataTot.Rows.Count);

            double A2 = 0.0;
            double A3 = 0.0;
            double d2 = 0.0;
            double D3 = 0.0;
            double d3 = 0.0;
            double D4 = 0.0;
            double c4 = 0.0;
            double c5 = 0.0;

            double hi = 0.0;
            double hiSum = 0.0;
            double hiSi = 0.0;
            double hiSic4 = 0.0;
            double hiSic4Sum = 0.0;
            double sameSSigma = 0.0;
            long nNN = 0;

            para.rtnXBar = ReturnXBarResult.Create();
            SamplingGroupSum grpData = new SamplingGroupSum();

            int nPageId = 1;
            int nPageIdAdd = 0;
            int nPageMax = 4;
            idSeq = 1;
            string subGroupTemp = string.Empty;
            SpcPageField pageDataTemp = SpcPageField.Create();

            outData.spcDataTable.tableSubGroupSta.Rows.Clear();
            outData.spcDataTable.tableNavigator.Rows.Clear();

            #region Sampling 개수가 같지 않을 경우 처리.
            try
            {
                //ctrDataSta
                for (i = 0; i < ctrDataTot.Rows.Count; i++)
                {
                    DataRow sgRow = ctrDataTot.Rows[i];
                    grpData.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                    grpData.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                    //grpData.SAMPLING = sgRow["SAMPLING"].ToSafeString();
                    isSamplingNotSame = Convert.ToBoolean(sgRow["ISSAME"]);
                    if (isSamplingNotSame)
                    {
                        var ctrSubGroup = ctrDataSta.Where(x => x.GROUPID == grpData.GROUPID && x.SUBGROUP == grpData.SUBGROUP);
                        foreach (var sp in ctrSubGroup)
                        {
                            grpData.GROUPID = sp.GROUPID;
                            grpData.SUBGROUP = sp.SUBGROUP;
                            grpData.SAMPLING = sp.SAMPLING;

                            grpData.SUBGROUPNAME = sp.SUBGROUPNAME;
                            grpData.SAMPLINGNAME = sp.SAMPLINGNAME;
                            grpData.MAX = sp.MAX;
                            grpData.MIN = sp.MIN;
                            grpData.R = sp.R;
                            grpData.BAR = sp.BAR;
                            grpData.NN = sp.NN;
                            grpData.SNN = sp.SNN.ToSafeInt64();

                            nNN = grpData.NN.ToSafeInt64();

                            switch (spcOption.sigmaType)
                            {
                                case SigmaType.No:
                                    hiSum += 1;
                                    hiSic4Sum += grpData.R.ToSafeDoubleZero();
                                    break;
                                case SigmaType.Yes:
                                default:
                                    c4 = SPCSta.GetCoe("c4", nNN);
                                    hi = (c4 * c4) / (1 - (c4 * c4));
                                    hiSum += hi;
                                    hiSi = hi * grpData.R.ToSafeDoubleZero();
                                    hiSic4 = hiSi / c4;
                                    hiSic4Sum += hiSic4;
                                    break;
                            }
                        }

                        sameSSigma = hiSic4Sum / hiSum;
                        sgRow["SAMESIGMA"] = sameSSigma;

                        //switch (spcOption.sigmaType)
                        //{
                        //    case SigmaType.No:
                        //        sameSSigma = hiSic4Sum / hiSum;
                        //        break;
                        //    case SigmaType.Yes:
                        //    default:
                        //        sameSSigma = hiSic4Sum / hiSum;
                        //        break;
                        //}


                        Console.WriteLine("sameRSigma: {0}", sameSSigma);
                        Console.WriteLine("sameRSigma: {0}", sameSSigma);
                    }
                    else
                    {
                    }
                }


                Console.WriteLine("ctrDataTot: {0}", ctrDataTot.Rows.Count);
                Console.WriteLine("ctrDataTot: {0}", ctrDataTot.Rows.Count);
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }
            #endregion Sampling 개수가 같지 않을 경우 처리.

            try
            {
                //ctrDataSta
                for (i = 0; i < ctrDataSta.Rows.Count; i++)
                {
                    DataRow sgRow = ctrDataSta.Rows[i];
                    grpData.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                    grpData.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                    grpData.SAMPLING = sgRow["SAMPLING"].ToSafeString();
                    grpData.SUBGROUPNAME = sgRow["SUBGROUPNAME"].ToSafeString();
                    grpData.SAMPLINGNAME = sgRow["SAMPLINGNAME"].ToSafeString();
                    grpData.MAX = sgRow["MAX"].ToNullOrDouble();
                    grpData.MIN = sgRow["MIN"].ToNullOrDouble();
                    //grpData.R = sgRow["R"].ToNullOrDouble();
                    grpData.STDEP = sgRow["STDEVALUE"].ToNullOrDouble();
                    grpData.R = grpData.STDEP;
                    grpData.BAR = sgRow["BAR"].ToNullOrDouble();

                    //grpData.SUMVALUE = sgRow["SUMVALUE"].ToSafeDouble();
                    //grpData.STDEVALUE = sgRow["STDEVALUE"].ToSafeDouble();
                    grpData.NN = sgRow["NN"].ToSafeInt64();
                    grpData.SNN = sgRow["SNN"].ToSafeInt64();

                    var ctrTot = ctrDataTot.Where(x => x.GROUPID == grpData.GROUPID && x.SUBGROUP == grpData.SUBGROUP);

                    foreach (var f in ctrTot)
                    {
                        grpData.XBAR = f.XBAR;
                        grpData.RBAR = f.SUMSTDEVPBAR;
                        grpData.GROUPCOUNT = f.GROUPNN;
                        ctrDataSta.Rows[i]["XBAR"] = grpData.XBAR;
                        ctrDataSta.Rows[i]["RBAR"] = grpData.RBAR;
                        ctrDataSta.Rows[i]["GROUPNN"] = grpData.GROUPCOUNT;
                        long eNN = grpData.NN.ToSafeInt64();
                        A2 = SPCSta.GetCoeA2(eNN);
                        //A3 = SPCSta.GetCoeA3(eNN);
                        d2 = SPCSta.GetCoed2(eNN);
                        D3 = SPCSta.GetCoeD3(eNN);
                        D4 = SPCSta.getCoeD4(eNN);
                        c4 = SPCSta.GetCoe("c4", eNN);
                        c5 = SPCSta.GetCoec5(eNN, c4);
                        A3 = 3 / (c4 * Math.Sqrt(eNN));

                        //sia확인 : XBAR-S Lib...
                        double nSameSigma = 0.0;
                        isSamplingNotSame = f.ISSAME;
                        if (isSamplingNotSame == false)
                        {
                            switch (spcOption.sigmaType)
                            {
                                case SigmaType.No:
                                    //XBar
                                    grpData.UCL = grpData.XBAR + ((3 * grpData.RBAR) / Math.Sqrt(eNN));
                                    grpData.CL = grpData.XBAR;
                                    grpData.LCL = grpData.XBAR - ((3 * grpData.RBAR) / Math.Sqrt(eNN));

                                    //S 
                                    grpData.RUCL = grpData.RBAR + (3 * (grpData.RBAR / c4) * Math.Sqrt(1 - (c4 * c4)));
                                    grpData.RCL = grpData.RBAR;
                                    grpData.RLCL = grpData.RBAR - (3 * (grpData.RBAR / c4) * Math.Sqrt(1 - (c4 * c4)));
                                    break;
                                case SigmaType.Yes:
                                default:
                                    //XBar
                                    grpData.UCL = grpData.XBAR + (A3 * grpData.RBAR);
                                    grpData.CL = grpData.XBAR;
                                    grpData.LCL = grpData.XBAR - (A3 * grpData.RBAR);

                                    //S
                                    grpData.RUCL = grpData.RBAR + (3 * (grpData.RBAR / c4) * Math.Sqrt(1 - (c4 * c4)));
                                    grpData.RCL = grpData.RBAR;
                                    grpData.RLCL = grpData.RBAR - (3 * (grpData.RBAR / c4) * Math.Sqrt(1 - (c4 * c4)));
                                    break;
                            }
                        }
                        else
                        {
                            grpData.SAMESIGMA = f.SAMESIGMA;
                            double nks = 0.0;
                            double nsqNN = 0.0;
                            double nksSqtNN = 0.0;
                            
                            double nc4Sigma = 0.0;
                            double nkSigma = 0.0;
                            double nkc5Sigma = 0.0;

                            double nc5c4 = 0.0;
                            double nkc5c4Sigma = 0.0;

                            nSameSigma = grpData.SAMESIGMA.ToSafeDoubleZero();
                            nks = 3 * nSameSigma;
                            nsqNN = Math.Sqrt(eNN);
                            nksSqtNN = nks / nsqNN;
                            //nksSqtNN = Math.Round(nks / nsqNN, 9);
                            nc4Sigma = c4 * nSameSigma;
                            nkSigma = 3 * nSameSigma;
                            nkc5Sigma = c5 * nkSigma;

                            //XBar
                            grpData.UCL = grpData.XBAR + nksSqtNN;
                            grpData.CL = grpData.XBAR;
                            grpData.LCL = grpData.XBAR - nksSqtNN;

                            //S
                            switch (spcOption.sigmaType)
                            {
                                case SigmaType.No:
                                    nc5c4 = c5 / c4;
                                    nkc5c4Sigma = 3 * nc5c4 * nSameSigma;

                                    grpData.RUCL = nSameSigma + nkc5c4Sigma;
                                    grpData.RCL = grpData.R;
                                    grpData.RLCL = nSameSigma - nkc5c4Sigma;
                                    break;
                                case SigmaType.Yes:
                                default:
                                    nc4Sigma = c4 * nSameSigma;
                                    nkSigma = 3 * nSameSigma;
                                    nkc5Sigma = c5 * nkSigma;

                                    grpData.RUCL = nc4Sigma + nkc5Sigma;
                                    grpData.RCL = c4 * nSameSigma;
                                    grpData.RLCL = nc4Sigma - nkc5Sigma;
                                    break;
                            }

                            if (grpData.RLCL < 0)
                            {
                                grpData.RLCL = 0.0;
                            }
                            
                        }
                        


                        DataRow drRtn = rtnDataSet.NewRow();

                        drRtn["TEMPID"] = i;
                        drRtn["GROUPID"] = grpData.GROUPID;
                        drRtn["SUBGROUP"] = grpData.SUBGROUP;
                        drRtn["SAMPLING"] = grpData.SAMPLING;
                        drRtn["SUBGROUPNAME"] = grpData.SUBGROUPNAME;
                        drRtn["SAMPLINGNAME"] = grpData.SAMPLINGNAME;
                        drRtn["SAMESIGMA"] = nSameSigma;
                        drRtn["ISSAME"] = isSamplingNotSame;
                        drRtn["MAX"] = grpData.MAX;
                        drRtn["MIN"] = grpData.MIN;
                        drRtn["R"] = grpData.R;
                        drRtn["RUCL"] = grpData.RUCL;
                        drRtn["RLCL"] = grpData.RLCL;
                        drRtn["RCL"] = grpData.RCL;
                        drRtn["BAR"] = grpData.BAR;
                        drRtn["UCL"] = grpData.UCL;
                        drRtn["LCL"] = grpData.LCL;
                        drRtn["CL"] = grpData.CL;
                        drRtn["XBAR"] = grpData.XBAR;
                        if (isSamplingNotSame == false)
                        {
                            drRtn["RBAR"] = grpData.RBAR;
                        }
                        else
                        {
                            switch (spcOption.sigmaType)
                            {
                                case SigmaType.No:
                                    drRtn["RBAR"] = nSameSigma;
                                    break;
                                case SigmaType.Yes:
                                default:
                                    drRtn["RBAR"] = grpData.RCL;
                                    break;
                            }
                        }
                        //drRtn["SUMVALUE"] = grpData.SUMVALUE;
                        SpcFunction.IsDbNckDoubleWrite(drRtn, "TOTSUM", grpData.TOTSUM);
                        //drRtn["TOTAVGVALUE"] = grpData.TOTAVGVALUE;
                        //drRtn["STDEVALUE"] = grpData.STDEVALUE;
                        drRtn["NN"] = grpData.NN;
                        drRtn["SNN"] = grpData.SNN;
                        drRtn["TOTNN"] = grpData.TOTNN;
                        drRtn["GROUPNN"] = grpData.GROUPCOUNT;

                        rtnDataSet.Rows.Add(drRtn);

                        //sia확인 : 관리도 XBar-S Page 처리

                        #region Page 처리

                        DataRow spcReturnRow = outData.spcDataTable.tableSubGroupSta.NewRow();
                        spcReturnRow["TEMPID"] = i;
                        spcReturnRow["PAGEID"] = nPageId;
                        spcReturnRow["GROUPID"] = grpData.GROUPID.ToSafeInt64();
                        spcReturnRow["SUBGROUP"] = grpData.SUBGROUP;
                        spcReturnRow["XBAR"] = grpData.XBAR.ToSafeDoubleStaMax();
                        spcReturnRow["RBAR"] = grpData.RBAR.ToSafeDoubleStaMax();
                        spcReturnRow["GROUPNN"] = grpData.SNN;
                        outData.spcDataTable.tableSubGroupSta.Rows.Add(spcReturnRow);

                        if (nPageIdAdd > nPageMax)
                        {
                            DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                            spcNavigatorRow["TEMPID"] = nPageId;
                            spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                            spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                            spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                            spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                            spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                            spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                            spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                            spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                            spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                            spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                            spcNavigatorRow["GROUPNN"] = grpData.NN;
                            outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                            nPageId++;
                            nPageIdAdd = 1;

                            pageDataTemp.Start.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.Start.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.Start.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.Start.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.Start.nRBAR = pageDataTemp.Temp.nRBAR;

                            pageDataTemp.End.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.End.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.End.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.End.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.End.nRBAR = pageDataTemp.Temp.nRBAR;
                        }

                        if (nPageIdAdd == 0)
                        {
                            pageDataTemp.Start.nSEQID = i;
                            pageDataTemp.Start.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.Start.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.Start.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.Start.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        //SubGroup 구분
                        if (subGroupTemp != grpData.SUBGROUP)
                        {
                            subGroupTemp = grpData.SUBGROUP;
                            nPageIdAdd++;
                            if (nPageIdAdd > nPageMax)
                            {
                                pageDataTemp.Temp.nSEQID = i;
                                pageDataTemp.Temp.nGROUPID = grpData.GROUPID.ToSafeInt64();
                                pageDataTemp.Temp.SUBGROUP = grpData.SUBGROUP;
                                pageDataTemp.Temp.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                                pageDataTemp.Temp.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            }
                        }

                        if (nPageIdAdd <= nPageMax)
                        {
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        #endregion
                    }

                    //Console.WriteLine("XBar-R Summary : GROUPID = {0},  SUBGROUP = {1}, SAMPLEID = {2},  SAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , grpData.GROUPID, grpData.SUBGROUP, grpData.SAMPLEID, grpData.SAMPLING
                    //    , grpData.XBAR, grpData.RBAR, grpData.NN, grpData.GROUPCOUNT);
                }

                //마지막 Page Check
                if (nPageIdAdd <= nPageMax)
                {
                    DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                    spcNavigatorRow["TEMPID"] = nPageId;
                    spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                    spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                    spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                    spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                    spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                    spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                    spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                    spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                    spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                    spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                    spcNavigatorRow["GROUPNN"] = nPageId;
                    outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                }

                //1 Page Chart 4개 일때 Check 
                if (outData.spcDataTable.tableNavigator != null && outData.spcDataTable.tableNavigator.Rows.Count <= 0)
                {
                    if (nPageIdAdd > nPageMax)
                    {
                        DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                        spcNavigatorRow["TEMPID"] = nPageId;
                        spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                        spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                        spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                        spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                        spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;
                        spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                        spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                        spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                        spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                        spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                        spcNavigatorRow["GROUPNN"] = nPageId;
                        outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                    }
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }


        //*종료
        NEXT_NED:

            return rtnDataSet;
        }

        /// <summary>관리도 - XBar-P
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnControlDataTable SpcLibXBarP(SPCPara para, SPCOption spcOption, ref SPCOutData outData)
        {
            RtnControlDataTable rtnDataSet = new RtnControlDataTable();
            DataTable rawData = para.InputData;
            ParPIDataTable dbPara = para.InputData;
            int i = 0;
            long idSeq = 0;
            SubGroupSum sg = new SubGroupSum();
            TempControlDataSTADataTable ctrDataSta = new TempControlDataSTADataTable();
            try
            {
                var sumSubGroup = from b in dbPara
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP,
                                      b.SAMPLEID,
                                      b.SAMPLING
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSAMPLEID = g.Key.SAMPLEID,
                                      vSAMPLING = g.Key.SAMPLING,
                                      vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),//8/8
                                      vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),//8/8
                                      vMAX = g.Max(s => s.NVALUE),
                                      vMIN = g.Min(s => s.NVALUE),
                                      vSUM = g.Sum(s => s.NVALUE),
                                      vAVG = g.Average(s => s.NVALUE),
                                      vNN = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in sumSubGroup)
                {
                    sg.GROUPID = f.vGROUPID;
                    sg.SUBGROUP = f.vSUBGROUP;
                    sg.SAMPLEID = f.vSAMPLEID;
                    sg.SAMPLING = f.vSAMPLING;
                    sg.SUBGROUPNAME = f.vSUBGROUPNAME;
                    sg.SAMPLINGNAME = f.vSAMPLINGNAME;
                    sg.MAX = f.vMAX;
                    sg.MIN = f.vMIN;
                    sg.R = sg.MAX - sg.MIN;
                    sg.SUM = f.vSUM;
                    sg.AVG = f.vAVG;
                    sg.NN = f.vNN;
                    sg.COUNT = f.vCOUNT;

                    idSeq++;
                    DataRow sgRow = ctrDataSta.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = sg.GROUPID;
                    sgRow["SUBGROUP"] = sg.SUBGROUP;
                    sgRow["SAMPLING"] = sg.SAMPLING;
                    sgRow["SUBGROUPNAME"] = sg.SUBGROUPNAME;
                    sgRow["SAMPLINGNAME"] = sg.SAMPLINGNAME;
                    sgRow["MAX"] = sg.MAX;
                    sgRow["MIN"] = sg.MIN;
                    //sgRow["R"] = sg.R;
                    //sgRow["RUCL"] = "";
                    //sgRow["RLCL"] = "";
                    sgRow["BAR"] = sg.AVG;
                    //sgRow["UCL"] = "";
                    //sgRow["LCL"] = "";
                    sgRow["SUMVALUE"] = sg.SUM;
                    //sgRow["TOTSUM"] = "";
                    //sgRow["TOTAVGVALUE"] = "";
                    //sg.STDE = StdDevPpCd(dbPara, sg);
                    //sg.STDE = StdDevP(dbPara, sg);
                    sg.STDE = StdDevPV(dbPara, sg, out sg.STDSUM);
                    sgRow["STDEVALUE"] = sg.STDE;
                    if (sg.STDSUM != null)
                    {
                        sgRow["VA"] = sg.STDSUM;
                    }
                    sgRow["R"] = sg.STDE;
                    sgRow["NN"] = sg.NN;
                    sgRow["SNN"] = sg.NN - 1;
                    //sgRow["TOTNN"] = "";
                    //sgRow["GROUPNN"] = "";

                    ctrDataSta.Rows.Add(sgRow);

                    //Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }
            Console.WriteLine(ctrDataSta.Rows.Count);

            string paSubgroup = "";
            string paSubgroupValue = "";
            string paSampling = "";
            string paSamplingValue = "";
            string paCheck = "";
            string paCheckValue = "";
            double paAvg = 0f;
            double paValue = 0f;
            double paDev = 0f;
            double paDevSqu = 0f;
            for (i = 0; i < dbPara.Rows.Count; i++)
            {
                DataRow paRow = dbPara.Rows[i];
                paSubgroupValue = paRow["SUBGROUP"].ToSafeString();
                paSamplingValue = paRow["SAMPLING"].ToSafeString();
                paValue = paRow["NVALUE"].ToSafeDoubleZero();
                paCheckValue = string.Format("{0}{1}", paSubgroupValue, paSamplingValue);

                if (paCheckValue != paCheck)
                {
                    paCheck = paCheckValue;
                    paSubgroup = paSubgroupValue;
                    paSampling = paSamplingValue;
                    var staData = from item in ctrDataSta
                                  where (item.SUBGROUP == paSubgroup && item.SAMPLING == paSampling)
                                  select item;
                    foreach (var item in staData)
                    {
                        paAvg = item.BAR.ToSafeDoubleZero();
                    }
                }

                paDev = paValue - paAvg;
                paDevSqu = paDev * paDev;
                paRow["AVG"] = paAvg;
                paRow["DEV"] = paDev;
                paRow["DEVSQU"] = paDevSqu;

            }
            Console.WriteLine(dbPara.Rows.Count);

            paCheck = "";
            var devSubGroup = from b in dbPara
                              group b by new
                              {
                                  b.GROUPID,
                                  b.SUBGROUP,
                                  b.SAMPLING,
                              }
                  into g
                              select new
                              {
                                  vGROUPID = g.Key.GROUPID,
                                  vSUBGROUP = g.Key.SUBGROUP,
                                  vSAMPLING = g.Key.SAMPLING,//8/11
                                  vTOTDEVSQU = g.Sum(s => s.DEVSQU),
                                  //vAVG = g.Average(s => s.NVALUE),
                                  //vNN = g.Sum(s => 1),
                                  vCOUNT = g.Count()
                              };
            foreach (var f in devSubGroup)
            {
                sg.GROUPID = f.vGROUPID;
                sg.SUBGROUP = f.vSUBGROUP;
                sg.SAMPLING = f.vSAMPLING;
                sg.TOTDEVSQU = f.vTOTDEVSQU;
                paCheck = string.Format("{0}{1}", sg.SUBGROUP, sg.SAMPLING);
                for (i = 0; i < ctrDataSta.Rows.Count; i++)
                {
                    DataRow staRow = ctrDataSta.Rows[i];
                    paSubgroupValue = staRow["SUBGROUP"].ToSafeString();
                    paSamplingValue = staRow["SAMPLING"].ToSafeString();
                    //paValue = paRow["NVALUE"].ToSafeDoubleZero();
                    paCheckValue = string.Format("{0}{1}", paSubgroupValue, paSamplingValue);
                    if (paCheckValue == paCheck)
                    {
                        staRow["SUMDEVSQU"] = sg.TOTDEVSQU;
                        break;
                    }
                }
            }

            Console.WriteLine(ctrDataSta.Rows.Count);

            //*SBar 계산.
            TempControlDataTotDataTable ctrDataTot = new TempControlDataTotDataTable();
            try
            {
                var totSubGroup = from b in ctrDataSta
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vXBAR = g.Average(s => s.BAR),
                                      vRBAR = g.Average(s => s.R),
                                      vSTDEVP = g.Sum(s => s.STDEVALUE),
                                      vSUMVA = g.Sum(s => s.VA),
                                      vSUBGROUPCOUNT = g.Sum(s => 1),
                                      vSUMTOTDEVSQU = g.Sum(s => s.STDEVALUE),
                                      vTOTSNN = g.Sum(s => s.SNN.ToSafeInt32()),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in totSubGroup)
                {
                    idSeq++;
                    DataRow sgRow = ctrDataTot.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = f.vGROUPID;
                    sgRow["SUBGROUP"] = f.vSUBGROUP;
                    sgRow["XBAR"] = f.vXBAR;
                    sgRow["RBAR"] = f.vRBAR;
                    sgRow["SUMVA"] = f.vSUMVA;
                    sgRow["SUMSTDEVP"] = f.vSTDEVP;
                    sgRow["SUMSTDEVPBAR"] = f.vSTDEVP / f.vCOUNT;
                    sgRow["SUMTOTDEVSQU"] = f.vSUMTOTDEVSQU;
                    sgRow["GROUPNN"] = f.vCOUNT;
                    sgRow["SNN"] = f.vTOTSNN;
                    ctrDataTot.Rows.Add(sgRow);

                    //Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            Console.WriteLine(ctrDataTot.Rows.Count);

            double A2 = 0.0;
            double A3 = 0.0;
            double d2 = 0.0;
            double D3 = 0.0;
            double D4 = 0.0;
            double c4 = 0.0;
            double c4sg = 0.0;
            double c4maxNN = 0.0;
            double c5 = 0.0;

            para.rtnXBar = ReturnXBarResult.Create();
            SamplingGroupSum grpData = new SamplingGroupSum();

            int nPageId = 1;
            int nPageIdAdd = 0;
            int nPageMax = 4;
            idSeq = 1;
            string subGroupTemp = string.Empty;
            SpcPageField pageDataTemp = SpcPageField.Create();

            outData.spcDataTable.tableSubGroupSta.Rows.Clear();
            outData.spcDataTable.tableNavigator.Rows.Clear();
            double tempXR = 0f;
            double tempXRsg = 0f;
            long maxNN = 0;
            string grpSubgroup = "";
            string grpSubgroupCheck = "";
            try
            {
                for (i = 0; i < ctrDataSta.Rows.Count; i++)
                {
                    DataRow sgRow = ctrDataSta.Rows[i];
                    grpData.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                    grpData.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                    grpData.SAMPLING = sgRow["SAMPLING"].ToSafeString();
                    grpData.SUBGROUPNAME = sgRow["SUBGROUPNAME"].ToSafeString();
                    grpData.SAMPLINGNAME = sgRow["SAMPLINGNAME"].ToSafeString();
                    grpData.MAX = sgRow["MAX"].ToNullOrDouble();
                    grpData.MIN = sgRow["MIN"].ToNullOrDouble();
                    //grpData.R = sgRow["R"].ToNullOrDouble();
                    grpData.STDEP = sgRow["STDEVALUE"].ToNullOrDouble();
                    grpData.R = grpData.STDEP;
                    grpData.BAR = sgRow["BAR"].ToNullOrDouble();

                    //grpData.SUMVALUE = sgRow["SUMVALUE"].ToSafeDouble();
                    //grpData.STDEVALUE = sgRow["STDEVALUE"].ToSafeDouble();
                    grpData.NN = sgRow["NN"].ToSafeInt64();

                    //grpSubgroup = string.Format("{0}{1}", grpData.GROUPID.ToSafeString(), grpData.SUBGROUP);
                    grpSubgroup = grpData.SUBGROUP;
                    //Max Check
                    if (grpSubgroup != grpSubgroupCheck)
                    {
                        grpSubgroupCheck = grpSubgroup;
                        var dataMaxNN = ctrDataSta.Where(x => x.GROUPID == grpData.GROUPID && x.SUBGROUP == grpData.SUBGROUP)
                            .OrderBy(or => or.SUBGROUP);
                        foreach (var f in dataMaxNN)
                        {
                            maxNN = f.NN;
                        }

                        c4maxNN = SPCSta.GetCoe("c4", maxNN);
                    }

                    //grpData.SNN = sgRow["SNN"].ToSafeInt64();

                    var ctrTot = ctrDataTot.Where(x => x.GROUPID == grpData.GROUPID && x.SUBGROUP == grpData.SUBGROUP);

                    foreach (var f in ctrTot)
                    {
                        long nTSNN = f.SNN.ToSafeInt64();
                        long nPSNN = nTSNN + 1;

                        long eNN = grpData.NN.ToSafeInt64();

                        
                        A2 = SPCSta.GetCoeA2(eNN);
                        //A3 = SPCSta.GetCoeA3(eNN);
                        d2 = SPCSta.GetCoed2(eNN);
                        D3 = SPCSta.GetCoeD3(eNN);
                        D4 = SPCSta.getCoeD4(eNN);
                        c4 = SPCSta.GetCoe("c4", eNN);
                        c4sg = SPCSta.GetCoe("c4", nPSNN);
                        A3 = 3 / (c4 * Math.Sqrt(eNN));
                        c5 = SPCSta.GetCoec5(eNN, c4);

                        grpData.XBAR = f.XBAR;

                        //*SBARP
                        tempXR = f.SUMVA.ToSafeDoubleZero() / f.GROUPNN;
                        tempXR = Math.Sqrt(tempXR);
                        grpData.STDE = Math.Round(tempXR, 5);

                        tempXRsg = tempXR / c4sg;
                        tempXRsg = Math.Round(tempXRsg, 5);

                        grpData.STDEsg = tempXRsg;
                        grpData.GROUPCOUNT = f.GROUPNN;
                        grpData.SNN = nTSNN;

                        switch (spcOption.sigmaType)
                        {
                            case SigmaType.No:
                                grpData.RBAR = tempXR;
                                //XBar
                                grpData.UCL = grpData.XBAR + ((3 * grpData.RBAR) / Math.Sqrt(eNN));
                                grpData.CL = grpData.XBAR;
                                grpData.LCL = grpData.XBAR - ((3 * grpData.RBAR) / Math.Sqrt(eNN));

                                //S 
                                grpData.RUCL = grpData.RBAR + (3 * (c5 / c4)) * grpData.RBAR;
                                grpData.RCL = grpData.RBAR;
                                grpData.RLCL = grpData.RBAR - (3 * (c5 / c4)) * grpData.RBAR;
                                break;
                            case SigmaType.Yes:
                            default:
                                grpData.RBAR = c4maxNN * tempXRsg;
                                //XBar
                                grpData.UCL = grpData.XBAR + (A3 * grpData.RBAR);
                                grpData.CL = grpData.XBAR;
                                grpData.LCL = grpData.XBAR - (A3 * grpData.RBAR);

                                //S
                                grpData.RUCL = grpData.RBAR + (3 * (grpData.RBAR / c4) * Math.Sqrt(1 - (c4 * c4)));
                                grpData.RCL = grpData.RBAR;
                                grpData.RLCL = grpData.RBAR - (3 * (grpData.RBAR / c4) * Math.Sqrt(1 - (c4 * c4)));
                                break;
                        }

                        ctrDataSta.Rows[i]["XBAR"] = grpData.XBAR;
                        ctrDataSta.Rows[i]["RBAR"] = grpData.RBAR;
                        ctrDataSta.Rows[i]["GROUPNN"] = grpData.GROUPCOUNT;


                        DataRow drRtn = rtnDataSet.NewRow();

                        drRtn["TEMPID"] = i;
                        drRtn["GROUPID"] = grpData.GROUPID;
                        drRtn["SUBGROUP"] = grpData.SUBGROUP;
                        drRtn["SAMPLING"] = grpData.SAMPLING;
                        drRtn["SUBGROUPNAME"] = grpData.SUBGROUPNAME;
                        drRtn["SAMPLINGNAME"] = grpData.SAMPLINGNAME;
                        drRtn["SAMESIGMA"] = 0.0;//1/7추가
                        drRtn["ISSAME"] = false; //item.ISSAME;//1/7추가
                        drRtn["MAX"] = grpData.MAX;
                        drRtn["MIN"] = grpData.MIN;
                        drRtn["R"] = grpData.R;
                        drRtn["RUCL"] = grpData.RUCL;
                        drRtn["RLCL"] = grpData.RLCL;
                        drRtn["RCL"] = grpData.RCL;
                        drRtn["BAR"] = grpData.BAR;
                        drRtn["UCL"] = grpData.UCL;
                        drRtn["LCL"] = grpData.LCL;
                        drRtn["CL"] = grpData.CL;
                        drRtn["XBAR"] = grpData.XBAR;
                        switch (spcOption.sigmaType)
                        {
                            case SigmaType.No:
                                drRtn["RBAR"] = grpData.RBAR;
                                break;
                            case SigmaType.Yes:
                            default:
                                //drRtn["RBAR"] = grpData.RBAR / c4;
                                drRtn["RBAR"] = grpData.RBAR;
                                break;
                        }
                        //drRtn["SUMVALUE"] = grpData.SUMVALUE;
                        SpcFunction.IsDbNckDoubleWrite(drRtn, "TOTSUM", grpData.TOTSUM);
                        //drRtn["TOTAVGVALUE"] = grpData.TOTAVGVALUE;
                        //drRtn["STDEVALUE"] = grpData.STDEVALUE;
                        drRtn["NN"] = grpData.NN;
                        drRtn["SNN"] = grpData.SNN;
                        drRtn["TOTNN"] = grpData.TOTNN;
                        drRtn["GROUPNN"] = grpData.GROUPCOUNT;

                        rtnDataSet.Rows.Add(drRtn);

                        //sia확인 : 관리도 XBar-P Page 처리

                        #region Page 처리

                        DataRow spcReturnRow = outData.spcDataTable.tableSubGroupSta.NewRow();
                        spcReturnRow["TEMPID"] = i;
                        spcReturnRow["PAGEID"] = nPageId;
                        spcReturnRow["GROUPID"] = grpData.GROUPID.ToSafeInt64();
                        spcReturnRow["SUBGROUP"] = grpData.SUBGROUP;
                        spcReturnRow["XBAR"] = grpData.XBAR.ToSafeDoubleStaMax();
                        spcReturnRow["RBAR"] = grpData.RBAR.ToSafeDoubleStaMax();
                        spcReturnRow["GROUPNN"] = grpData.SNN;
                        outData.spcDataTable.tableSubGroupSta.Rows.Add(spcReturnRow);

                        if (nPageIdAdd > nPageMax)
                        {
                            DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                            spcNavigatorRow["TEMPID"] = nPageId;
                            spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                            spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                            spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                            spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                            spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                            spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                            spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                            spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                            spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                            spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                            spcNavigatorRow["GROUPNN"] = grpData.NN;
                            outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                            nPageId++;
                            nPageIdAdd = 1;

                            pageDataTemp.Start.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.Start.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.Start.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.Start.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.Start.nRBAR = pageDataTemp.Temp.nRBAR;

                            pageDataTemp.End.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.End.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.End.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.End.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.End.nRBAR = pageDataTemp.Temp.nRBAR;
                        }

                        if (nPageIdAdd == 0)
                        {
                            pageDataTemp.Start.nSEQID = i;
                            pageDataTemp.Start.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.Start.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.Start.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.Start.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        //SubGroup 구분
                        if (subGroupTemp != grpData.SUBGROUP)
                        {
                            subGroupTemp = grpData.SUBGROUP;
                            nPageIdAdd++;
                            if (nPageIdAdd > nPageMax)
                            {
                                pageDataTemp.Temp.nSEQID = i;
                                pageDataTemp.Temp.nGROUPID = grpData.GROUPID.ToSafeInt64();
                                pageDataTemp.Temp.SUBGROUP = grpData.SUBGROUP;
                                pageDataTemp.Temp.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                                pageDataTemp.Temp.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            }
                        }

                        if (nPageIdAdd <= nPageMax)
                        {
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        #endregion
                    }

                    //Console.WriteLine("XBar-R Summary : GROUPID = {0},  SUBGROUP = {1}, SAMPLEID = {2},  SAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , grpData.GROUPID, grpData.SUBGROUP, grpData.SAMPLEID, grpData.SAMPLING
                    //    , grpData.XBAR, grpData.RBAR, grpData.NN, grpData.GROUPCOUNT);
                }

                //마지막 Page Check
                if (nPageIdAdd <= nPageMax)
                {
                    DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                    spcNavigatorRow["TEMPID"] = nPageId;
                    spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                    spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                    spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                    spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                    spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                    spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                    spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                    spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                    spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                    spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                    spcNavigatorRow["GROUPNN"] = nPageId;
                    outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                }

                //1 Page Chart 4개 일때 Check 
                if (outData.spcDataTable.tableNavigator != null && outData.spcDataTable.tableNavigator.Rows.Count <= 0)
                {
                    if (nPageIdAdd > nPageMax)
                    {
                        DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                        spcNavigatorRow["TEMPID"] = nPageId;
                        spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                        spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                        spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                        spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                        spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;
                        spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                        spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                        spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                        spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                        spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                        spcNavigatorRow["GROUPNN"] = nPageId;
                        outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                    }
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }


        //*종료
        NEXT_NED:

            return rtnDataSet;
        }

        /// <summary>관리도 - I-MR
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnControlDataTable SpcLibIMR(SPCPara para, SPCOption spcOption, ref SPCOutData outData)
        {
            RtnControlDataTable rtnDataSet = new RtnControlDataTable();
            DataTable rawData = para.InputData;
            ParPIDataTable dbPara = para.InputData;
            int i = 0;
            long idSeq = 0;
            SubGroupSum sg = new SubGroupSum();
            TempControlDataSTADataTable ctrDataSta = new TempControlDataSTADataTable();
            try
            {
                var sumSubGroup = from b in dbPara
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP,
                                      b.SAMPLEID,
                                      b.SAMPLING
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSAMPLEID = g.Key.SAMPLEID,
                                      vSAMPLING = g.Key.SAMPLING,
                                      vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),//8/8
                                      vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),//8/8
                                      vMAX = g.Max(s => s.NVALUE),
                                      vMIN = g.Min(s => s.NVALUE),
                                      vSUM = g.Sum(s => s.NVALUE),
                                      vAVG = g.Average(s => s.NVALUE),
                                      vNN = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };


                foreach (var f in sumSubGroup)
                {
                    sg.GROUPID = f.vGROUPID;
                    sg.SUBGROUP = f.vSUBGROUP;
                    sg.SAMPLEID = f.vSAMPLEID;
                    sg.SAMPLING = f.vSAMPLING;
                    sg.SUBGROUPNAME = f.vSUBGROUPNAME;
                    sg.SAMPLINGNAME = f.vSAMPLINGNAME;
                    sg.MAX = f.vMAX;
                    sg.MIN = f.vMIN;
                    sg.R = sg.MAX - sg.MIN;
                    sg.SUM = f.vSUM;
                    sg.AVG = f.vAVG;
                    sg.NN = f.vNN;
                    sg.COUNT = f.vCOUNT;

                    idSeq++;
                    DataRow sgRow = ctrDataSta.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = sg.GROUPID;
                    sgRow["SUBGROUP"] = sg.SUBGROUP;
                    sgRow["SAMPLING"] = sg.SAMPLING;
                    sgRow["SUBGROUPNAME"] = sg.SUBGROUPNAME;
                    sgRow["SAMPLINGNAME"] = sg.SAMPLINGNAME;
                    sgRow["MAX"] = sg.MAX;
                    sgRow["MIN"] = sg.MIN;
                    sgRow["R"] = 0f;// DBNull.Value;
                    //sgRow["RUCL"] = "";
                    //sgRow["RLCL"] = "";
                    sgRow["BAR"] = sg.AVG; // sg.SUM;
                    //sgRow["UCL"] = "";
                    //sgRow["LCL"] = "";
                    sgRow["SUMVALUE"] = sg.AVG; //sg.SUM;
                    sgRow["SUMSUBVALUE"] = 0f;
                    //sgRow["TOTSUM"] = "";
                    //sgRow["TOTAVGVALUE"] = "";
                    //sg.STDE = StdDevPpCd(dbPara, sg);
                    sg.STDE = 0f;// StdDevP(dbPara, sg);
                    sgRow["STDEVALUE"] = sg.STDE;
                    sgRow["NN"] = sg.NN;
                    sgRow["SNN"] = sg.NN - 1;
                    //sgRow["TOTNN"] = "";
                    //sgRow["GROUPNN"] = "";

                    ctrDataSta.Rows.Add(sgRow);

                    //Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            //*I-MR 계산
            int nMR = 0;
            int nRowMax = ctrDataSta.Rows.Count;
            double nFirstValue = 0f;
            string beforeSubgroup = "";
            string nowSubgroup = "";
            double nNextValue = 0f;
            nMR = -1;
            for (i = 0; i < nRowMax; i++)
            {
                DataRow rowGroup = ctrDataSta.Rows[i];
                nowSubgroup = SpcFunction.IsDbNck(rowGroup, "SUBGROUP");
                if (beforeSubgroup == "")
                {
                    beforeSubgroup = nowSubgroup;
                }

                if (nowSubgroup != beforeSubgroup)
                {
                    nMR = 0;
                    beforeSubgroup = nowSubgroup;
                }
                else
                {
                    nMR++;
                    if (nMR > 0)
                    {
                        DataRow rowFirst = ctrDataSta.Rows[i - 1];
                        DataRow rowNext = ctrDataSta.Rows[i];
                        nFirstValue = SpcFunction.IsDbNckDoubleMin(rowFirst, "BAR");
                        nNextValue = SpcFunction.IsDbNckDoubleMin(rowNext, "BAR");
                        rowNext["R"] = Math.Abs(nFirstValue - nNextValue);
                        rowNext["SUMSUBVALUE"] = rowNext["R"];
                    }
                }


            }

            Console.WriteLine(ctrDataSta.Rows.Count);

            //*MR 계산.
            double d2Mr;
            double xMr;
            long nW = 2;
            d2Mr = SPCSta.GetCoed2(nW);
            TempControlDataTotDataTable ctrDataTot = new TempControlDataTotDataTable();
            try
            {
                idSeq = 0;
                var totSubGroup = from b in ctrDataSta
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP,
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vXBAR = g.Average(s => s.BAR),
                                      vRSUM = g.Sum(s => s.R),
                                      vSTDEVP = g.Sum(s => s.STDEVALUE),
                                      vSUBGROUPCOUNT = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in totSubGroup)
                {
                    idSeq++;
                    DataRow sgRow = ctrDataTot.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = f.vGROUPID;
                    sgRow["SUBGROUP"] = f.vSUBGROUP;
                    sgRow["XBAR"] = f.vXBAR;
                    xMr = f.vRSUM / (f.vCOUNT - 1);
                    sgRow["RBAR"] = xMr;
                    sgRow["NN"] = f.vSUBGROUPCOUNT;
                    sgRow["SUMSTDEVP"] = xMr / d2Mr;
                    sgRow["SUMSTDEVPBAR"] = xMr / d2Mr;
                    sgRow["GROUPNN"] = f.vCOUNT;
                    ctrDataTot.Rows.Add(sgRow);

                    //Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            //Console.WriteLine(ctrDataTot.Rows.Count);

            double A2 = 0.0;
            double A3 = 0.0;
            double d2 = 0.0;
            double d3 = 0.0;
            double D4 = 0.0;
            double c4 = 0.0;

            para.rtnXBar = ReturnXBarResult.Create();
            SamplingGroupSum grpData = new SamplingGroupSum();

            int nPageId = 1;
            int nPageIdAdd = 0;
            int nPageMax = 4;
            idSeq = 1;
            string subGroupTemp = string.Empty;
            SpcPageField pageDataTemp = SpcPageField.Create();

            outData.spcDataTable.tableSubGroupSta.Rows.Clear();
            outData.spcDataTable.tableNavigator.Rows.Clear();

            try
            {
                //ctrDataSta
                for (i = 0; i < ctrDataSta.Rows.Count; i++)
                {
                    DataRow sgRow = ctrDataSta.Rows[i];
                    grpData.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                    grpData.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                    grpData.SAMPLING = sgRow["SAMPLING"].ToSafeString();
                    grpData.SUBGROUPNAME = sgRow["SUBGROUPNAME"].ToSafeString();
                    grpData.SAMPLINGNAME = sgRow["SAMPLINGNAME"].ToSafeString();
                    grpData.MAX = sgRow["MAX"].ToNullOrDouble();
                    grpData.MIN = sgRow["MIN"].ToNullOrDouble();
                    grpData.R = sgRow["R"].ToNullOrDouble();
                    //grpData.STDEP = sgRow["STDEVALUE"].ToNullOrDouble();
                    //grpData.R = grpData.STDEP;
                    grpData.BAR = sgRow["BAR"].ToNullOrDouble();

                    grpData.SUM = sgRow["SUMVALUE"].ToNullOrDouble();
                    
                    grpData.NN = sgRow["NN"].ToSafeInt64();
                    grpData.SNN = sgRow["SNN"].ToSafeInt64();

                    var ctrTot = ctrDataTot.Where(x => x.GROUPID == grpData.GROUPID && x.SUBGROUP == grpData.SUBGROUP);

                    foreach (var f in ctrTot)
                    {
                        grpData.XBAR = f.XBAR;
                        grpData.RBAR = f.RBAR;
                        grpData.GROUPCOUNT = f.GROUPNN;
                        grpData.SUMSTDEP = f.SUMSTDEVP;
                        grpData.NN = f.NN;

                        ctrDataSta.Rows[i]["XBAR"] = grpData.XBAR;
                        ctrDataSta.Rows[i]["RBAR"] = grpData.RBAR;
                        ctrDataSta.Rows[i]["GROUPNN"] = grpData.GROUPCOUNT;

                        A2 = SPCSta.GetCoeA2(grpData.NN.ToSafeInt64());
                        A3 = SPCSta.GetCoeA3(grpData.NN.ToSafeInt64());
                        d2 = SPCSta.GetCoed2(grpData.NN.ToSafeInt64());
                        d3 = SPCSta.GetCoed3(grpData.NN.ToSafeInt64() - 1);
                        D4 = SPCSta.getCoeD4(grpData.NN.ToSafeInt64());
                        c4 = SPCSta.GetCoe("c4", grpData.NN.ToSafeInt64());

                        //IMR Control Limit 계산 12/26
                        //I
                        grpData.UCL = grpData.XBAR + (3 * grpData.RBAR / d2Mr);
                        grpData.CL = grpData.XBAR;
                        grpData.LCL = grpData.XBAR - (3 * grpData.RBAR / d2Mr);

                        //MR
                        grpData.RUCL = grpData.RBAR + (3 * d3 * (grpData.RBAR / d2Mr));
                        grpData.RCL = grpData.RBAR;
                        grpData.RLCL = grpData.RBAR - (3 * d3 * (grpData.RBAR / d2Mr));

                        DataRow drRtn = rtnDataSet.NewRow();

                        drRtn["TEMPID"] = i;
                        drRtn["GROUPID"] = grpData.GROUPID;
                        drRtn["SUBGROUP"] = grpData.SUBGROUP;
                        drRtn["SAMPLING"] = grpData.SAMPLING;
                        drRtn["SUBGROUPNAME"] = grpData.SUBGROUPNAME;
                        drRtn["SAMPLINGNAME"] = grpData.SAMPLINGNAME;
                        drRtn["SAMESIGMA"] = 0.0;//1/7추가
                        drRtn["ISSAME"] = false; //item.ISSAME;//1/7추가
                        drRtn["MAX"] = grpData.MAX;
                        drRtn["MIN"] = grpData.MIN;
                        if (grpData.R.ToNullOrDouble() != null )
                        {
                            try
                            {
                                drRtn["R"] = grpData.R;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine(grpData.R);
                            }

                        }
                        else
                        {
                            //drRtn["R"] = DBNull.Value;
                            drRtn["R"] = grpData.R;
                        }

                        drRtn["RUCL"] = grpData.RUCL;
                        drRtn["RLCL"] = grpData.RLCL;
                        drRtn["RCL"] = grpData.RCL;
                        drRtn["BAR"] = grpData.BAR;//I
                        drRtn["UCL"] = grpData.UCL;
                        drRtn["LCL"] = grpData.LCL;
                        drRtn["CL"] = grpData.CL;
                        drRtn["XBAR"] = grpData.XBAR;
                        drRtn["RBAR"] = grpData.RBAR;
                        //drRtn["SUMVALUE"] = grpData.SUMVALUE;
                        //drRtn["TOTSUM"] = grpData.TOTSUM.ToNullOrDouble();
                        SpcFunction.IsDbNckDoubleWrite(drRtn, "TOTSUM", grpData.TOTSUM);
                        //drRtn["TOTAVGVALUE"] = grpData.TOTAVGVALUE;
                        drRtn["STDEVALUE"] = grpData.SUMSTDEP;
                        drRtn["NN"] = grpData.NN;
                        drRtn["SNN"] = grpData.SNN;
                        drRtn["TOTNN"] = grpData.TOTNN;
                        drRtn["GROUPNN"] = grpData.GROUPCOUNT;

                        rtnDataSet.Rows.Add(drRtn);

                        //sia확인 : 관리도 I-MR Page 처리

                        #region Page 처리

                        DataRow spcReturnRow = outData.spcDataTable.tableSubGroupSta.NewRow();
                        spcReturnRow["TEMPID"] = i;
                        spcReturnRow["PAGEID"] = nPageId;
                        spcReturnRow["GROUPID"] = grpData.GROUPID.ToSafeInt64();
                        spcReturnRow["SUBGROUP"] = grpData.SUBGROUP;
                        spcReturnRow["XBAR"] = grpData.XBAR.ToSafeDoubleStaMax();
                        spcReturnRow["RBAR"] = grpData.RBAR.ToSafeDoubleStaMax();
                        spcReturnRow["GROUPNN"] = grpData.SNN;
                        outData.spcDataTable.tableSubGroupSta.Rows.Add(spcReturnRow);

                        if (nPageIdAdd > nPageMax)
                        {
                            DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                            spcNavigatorRow["TEMPID"] = nPageId;
                            spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                            spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                            spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                            spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                            spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                            spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                            spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                            spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                            spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                            spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                            spcNavigatorRow["GROUPNN"] = grpData.NN;
                            outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                            nPageId++;
                            nPageIdAdd = 1;

                            pageDataTemp.Start.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.Start.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.Start.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.Start.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.Start.nRBAR = pageDataTemp.Temp.nRBAR;

                            pageDataTemp.End.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.End.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.End.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.End.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.End.nRBAR = pageDataTemp.Temp.nRBAR;
                        }

                        if (nPageIdAdd == 0)
                        {
                            pageDataTemp.Start.nSEQID = i;
                            pageDataTemp.Start.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.Start.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.Start.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.Start.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        //SubGroup 구분
                        if (subGroupTemp != grpData.SUBGROUP)
                        {
                            subGroupTemp = grpData.SUBGROUP;
                            nPageIdAdd++;
                            if (nPageIdAdd > nPageMax)
                            {
                                pageDataTemp.Temp.nSEQID = i;
                                pageDataTemp.Temp.nGROUPID = grpData.GROUPID.ToSafeInt64();
                                pageDataTemp.Temp.SUBGROUP = grpData.SUBGROUP;
                                pageDataTemp.Temp.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                                pageDataTemp.Temp.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            }
                        }

                        if (nPageIdAdd <= nPageMax)
                        {
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        #endregion
                    }

                    //Console.WriteLine("XBar-R Summary : GROUPID = {0},  SUBGROUP = {1}, SAMPLEID = {2},  SAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , grpData.GROUPID, grpData.SUBGROUP, grpData.SAMPLEID, grpData.SAMPLING
                    //    , grpData.XBAR, grpData.RBAR, grpData.NN, grpData.GROUPCOUNT);
                }

                //마지막 Page Check
                if (nPageIdAdd <= nPageMax)
                {
                    DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                    spcNavigatorRow["TEMPID"] = nPageId;
                    spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                    spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                    spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                    spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                    spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                    spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                    spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                    spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                    spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                    spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                    spcNavigatorRow["GROUPNN"] = nPageId;
                    outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                }

                //1 Page Chart 4개 일때 Check 
                if (outData.spcDataTable.tableNavigator != null && outData.spcDataTable.tableNavigator.Rows.Count <= 0)
                {
                    if (nPageIdAdd > nPageMax)
                    {
                        DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                        spcNavigatorRow["TEMPID"] = nPageId;
                        spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                        spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                        spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                        spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                        spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;
                        spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                        spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                        spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                        spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                        spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                        spcNavigatorRow["GROUPNN"] = nPageId;
                        outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                    }
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }


        //*종료
        NEXT_NED:

            return rtnDataSet;
        }

        /// <summary>관리도 - P
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnControlDataTable SpcLibP(SPCPara para, SPCOption spcOption, ref SPCOutData outData)
        {
            RtnControlDataTable rtnDataSet = new RtnControlDataTable();
            DataTable rawData = para.InputData;
            ParPIDataTable dbPara = para.InputData;
            int i = 0;
            long idSeq = 0;
            SubGroupSum sg = new SubGroupSum();
            TempControlDataSTADataTable ctrDataSta = new TempControlDataSTADataTable();
            try
            {
                var sumSubGroup = from b in dbPara
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP,
                                      b.SAMPLEID,
                                      b.SAMPLING
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSAMPLEID = g.Key.SAMPLEID,
                                      vSAMPLING = g.Key.SAMPLING,
                                      vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),//8/8
                                      vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),//8/8
                                      vMAX = g.Max(s => s.NVALUE),
                                      vMIN = g.Min(s => s.NVALUE),
                                      vSUM = g.Sum(s => s.NVALUE),
                                      vSUMSUB = g.Sum(s => s.NSUBVALUE),
                                      vAVG = g.Average(s => s.NVALUE),
                                      vNN = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in sumSubGroup)
                {
                    sg.GROUPID = f.vGROUPID;
                    sg.SUBGROUP = f.vSUBGROUP;
                    sg.SAMPLEID = f.vSAMPLEID;
                    sg.SAMPLING = f.vSAMPLING;
                    sg.SUBGROUPNAME = f.vSUBGROUPNAME;
                    sg.SAMPLINGNAME = f.vSAMPLINGNAME;
                    sg.MAX = f.vMAX;
                    sg.MIN = f.vMIN;
                    sg.R = sg.MAX - sg.MIN;
                    sg.SUM = f.vSUM;
                    sg.SUMSUB = f.vSUMSUB;
                    sg.AVG = f.vSUM / f.vSUMSUB;//f.vAVG;
                    sg.NN = f.vNN;
                    sg.COUNT = f.vCOUNT;

                    idSeq++;
                    DataRow sgRow = ctrDataSta.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = sg.GROUPID;
                    sgRow["SUBGROUP"] = sg.SUBGROUP;
                    sgRow["SAMPLING"] = sg.SAMPLING;
                    sgRow["MAX"] = sg.MAX;
                    sgRow["MIN"] = sg.MIN;
                    //sgRow["R"] = sg.R;
                    //sgRow["RUCL"] = "";
                    //sgRow["RLCL"] = "";
                    sgRow["BAR"] = sg.AVG;//P
                    //sgRow["UCL"] = "";
                    //sgRow["LCL"] = "";
                    sgRow["SUMVALUE"] = sg.SUM;
                    sgRow["SUMSUBVALUE"] = sg.SUMSUB;
                    //sgRow["TOTSUM"] = "";
                    //sgRow["TOTAVGVALUE"] = "";
                    //sg.STDE = StdDevPpCd(dbPara, sg);
                    sg.STDE = 0f;// StdDevP(dbPara, sg);
                    sgRow["STDEVALUE"] = sg.STDE;
                    sgRow["R"] = sg.STDE;
                    sgRow["NN"] = sg.NN;
                    sgRow["SNN"] = sg.NN - 1;
                    //sgRow["TOTNN"] = "";
                    //sgRow["GROUPNN"] = "";

                    ctrDataSta.Rows.Add(sgRow);

                    //Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            Console.WriteLine(ctrDataSta.Rows.Count);

            //*SBar 계산.
            TempControlDataTotDataTable ctrDataTot = new TempControlDataTotDataTable();
            try
            {
                var totSubGroup = from b in ctrDataSta //where b.NVALUE !=1
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSUMVALUE = g.Sum(s => s.SUMVALUE),
                                      vSUMSUBVALUE = g.Sum(s => s.SUMSUBVALUE),
                                      vXBAR = g.Average(s => s.BAR),
                                      vRBAR = g.Average(s => s.R),
                                      vSTDEVP = g.Sum(s => s.STDEVALUE),
                                      vSUBGROUPCOUNT = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in totSubGroup)
                {
                    idSeq++;
                    DataRow sgRow = ctrDataTot.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = f.vGROUPID;
                    sgRow["SUBGROUP"] = f.vSUBGROUP;
                    sgRow["XBAR"] = f.vSUMVALUE / f.vSUMSUBVALUE;
                    sgRow["RBAR"] = f.vRBAR;
                    sgRow["SUMSTDEVP"] = 0f;// f.vSTDEVP;
                    sgRow["SUMSTDEVPBAR"] = 0f;// f.vSTDEVP / f.vCOUNT;
                    sgRow["GROUPNN"] = f.vCOUNT;
                    ctrDataTot.Rows.Add(sgRow);

                    //Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            Console.WriteLine(ctrDataTot.Rows.Count);



            para.rtnXBar = ReturnXBarResult.Create();
            SamplingGroupSum grpData = new SamplingGroupSum();

            int nPageId = 1;
            int nPageIdAdd = 0;
            int nPageMax = 4;
            idSeq = 1;
            string subGroupTemp = string.Empty;
            SpcPageField pageDataTemp = SpcPageField.Create();

            outData.spcDataTable.tableSubGroupSta.Rows.Clear();
            outData.spcDataTable.tableNavigator.Rows.Clear();

            try
            {
                //ctrDataSta
                for (i = 0; i < ctrDataSta.Rows.Count; i++)
                {
                    DataRow sgRow = ctrDataSta.Rows[i];
                    grpData.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                    grpData.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                    grpData.SAMPLING = sgRow["SAMPLING"].ToSafeString();
                    grpData.SUBGROUPNAME = sgRow["SUBGROUPNAME"].ToSafeString();
                    grpData.SAMPLINGNAME = sgRow["SAMPLINGNAME"].ToSafeString();
                    grpData.MAX = sgRow["MAX"].ToNullOrDouble();
                    grpData.MIN = sgRow["MIN"].ToNullOrDouble();
                    //grpData.R = sgRow["R"].ToNullOrDouble();
                    grpData.SUM = sgRow["SUMVALUE"].ToNullOrDouble();
                    grpData.SUMSUB = sgRow["SUMSUBVALUE"].ToNullOrDouble();
                    grpData.STDEP = sgRow["STDEVALUE"].ToNullOrDouble();
                    grpData.R = grpData.STDEP;
                    grpData.BAR = sgRow["BAR"].ToNullOrDouble();

                    //grpData.SUMVALUE = sgRow["SUMVALUE"].ToSafeDouble();
                    //grpData.STDEVALUE = sgRow["STDEVALUE"].ToSafeDouble();
                    grpData.NN = sgRow["NN"].ToSafeInt64();
                    grpData.SNN = sgRow["SNN"].ToSafeInt64();

                    var ctrTot = ctrDataTot.Where(x => x.GROUPID == grpData.GROUPID && x.SUBGROUP == grpData.SUBGROUP);

                    foreach (var f in ctrTot)
                    {
                        grpData.XBAR = f.XBAR;
                        grpData.RBAR = f.SUMSTDEVPBAR;
                        grpData.GROUPCOUNT = f.GROUPNN;
                        ctrDataSta.Rows[i]["XBAR"] = grpData.XBAR;
                        ctrDataSta.Rows[i]["RBAR"] = grpData.RBAR;
                        ctrDataSta.Rows[i]["GROUPNN"] = grpData.GROUPCOUNT;

                        //XBar
                        double XPBAR = grpData.XBAR.ToSafeDoubleStaMax();
                        double sumSub = grpData.SUMSUB.ToSafeDoubleStaMax();
                        grpData.UCL = XPBAR + (3 * Math.Sqrt((XPBAR * (1 - XPBAR)) / sumSub));
                        grpData.CL = XPBAR;
                        grpData.LCL = XPBAR - (3 * Math.Sqrt((XPBAR * (1 - XPBAR)) / sumSub));

                        //S
                        grpData.RUCL = 0f;
                        grpData.RCL = 0f;
                        grpData.RLCL = 0f;

                        DataRow drRtn = rtnDataSet.NewRow();

                        drRtn["TEMPID"] = i;
                        drRtn["GROUPID"] = grpData.GROUPID;
                        drRtn["SUBGROUP"] = grpData.SUBGROUP;
                        drRtn["SAMPLING"] = grpData.SAMPLING;
                        drRtn["SUBGROUPNAME"] = grpData.SUBGROUPNAME;
                        drRtn["SAMPLINGNAME"] = grpData.SAMPLINGNAME;
                        drRtn["SAMESIGMA"] = 0.0;//1/7추가
                        drRtn["ISSAME"] = false; //item.ISSAME;//1/7추가
                        drRtn["MAX"] = grpData.MAX;
                        drRtn["MIN"] = grpData.MIN;
                        drRtn["R"] = grpData.R;
                        drRtn["RUCL"] = grpData.RUCL;
                        drRtn["RLCL"] = grpData.RLCL;
                        drRtn["RCL"] = grpData.RCL;
                        drRtn["BAR"] = grpData.BAR;
                        drRtn["UCL"] = grpData.UCL;
                        drRtn["LCL"] = grpData.LCL;
                        drRtn["CL"] = grpData.CL;
                        drRtn["XBAR"] = grpData.XBAR;
                        drRtn["RBAR"] = grpData.RBAR;
                        //drRtn["SUMVALUE"] = grpData.SUMVALUE;
                        //drRtn["TOTSUM"] = grpData.TOTSUM;
                        SpcFunction.IsDbNckDoubleWrite(drRtn, "TOTSUM", grpData.TOTSUM);
                        //drRtn["TOTAVGVALUE"] = grpData.TOTAVGVALUE;
                        //drRtn["STDEVALUE"] = grpData.STDEVALUE;
                        drRtn["NN"] = grpData.NN;
                        drRtn["SNN"] = grpData.SNN;
                        drRtn["TOTNN"] = grpData.TOTNN;
                        drRtn["GROUPNN"] = grpData.GROUPCOUNT;

                        rtnDataSet.Rows.Add(drRtn);

                        //sia확인 : 관리도 P Page 처리

                        #region Page 처리

                        DataRow spcReturnRow = outData.spcDataTable.tableSubGroupSta.NewRow();
                        spcReturnRow["TEMPID"] = i;
                        spcReturnRow["PAGEID"] = nPageId;
                        spcReturnRow["GROUPID"] = grpData.GROUPID.ToSafeInt64();
                        spcReturnRow["SUBGROUP"] = grpData.SUBGROUP;
                        spcReturnRow["XBAR"] = grpData.XBAR.ToSafeDoubleStaMax();
                        spcReturnRow["RBAR"] = grpData.RBAR.ToSafeDoubleStaMax();
                        spcReturnRow["GROUPNN"] = grpData.SNN;
                        outData.spcDataTable.tableSubGroupSta.Rows.Add(spcReturnRow);

                        if (nPageIdAdd > nPageMax)
                        {
                            DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                            spcNavigatorRow["TEMPID"] = nPageId;
                            spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                            spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                            spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                            spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                            spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                            spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                            spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                            spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                            spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                            spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                            spcNavigatorRow["GROUPNN"] = grpData.NN;
                            outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                            nPageId++;
                            nPageIdAdd = 1;

                            pageDataTemp.Start.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.Start.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.Start.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.Start.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.Start.nRBAR = pageDataTemp.Temp.nRBAR;

                            pageDataTemp.End.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.End.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.End.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.End.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.End.nRBAR = pageDataTemp.Temp.nRBAR;
                        }

                        if (nPageIdAdd == 0)
                        {
                            pageDataTemp.Start.nSEQID = i;
                            pageDataTemp.Start.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.Start.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.Start.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.Start.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        //SubGroup 구분
                        if (subGroupTemp != grpData.SUBGROUP)
                        {
                            subGroupTemp = grpData.SUBGROUP;
                            nPageIdAdd++;
                            if (nPageIdAdd > nPageMax)
                            {
                                pageDataTemp.Temp.nSEQID = i;
                                pageDataTemp.Temp.nGROUPID = grpData.GROUPID.ToSafeInt64();
                                pageDataTemp.Temp.SUBGROUP = grpData.SUBGROUP;
                                pageDataTemp.Temp.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                                pageDataTemp.Temp.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            }
                        }

                        if (nPageIdAdd <= nPageMax)
                        {
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        #endregion
                    }

                    Console.WriteLine("XBar-R Summary : GROUPID = {0},  SUBGROUP = {1}, SAMPLEID = {2},  SAMPLING = {3}, " +
                        "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                        , grpData.GROUPID, grpData.SUBGROUP, grpData.SAMPLEID, grpData.SAMPLING
                        , grpData.XBAR, grpData.RBAR, grpData.NN, grpData.GROUPCOUNT);
                }

                //마지막 Page Check
                if (nPageIdAdd <= nPageMax)
                {
                    DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                    spcNavigatorRow["TEMPID"] = nPageId;
                    spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                    spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                    spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                    spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                    spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                    spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                    spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                    spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                    spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                    spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                    spcNavigatorRow["GROUPNN"] = nPageId;
                    outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                }

                //1 Page Chart 4개 일때 Check 
                if (outData.spcDataTable.tableNavigator != null && outData.spcDataTable.tableNavigator.Rows.Count <= 0)
                {
                    if (nPageIdAdd > nPageMax)
                    {
                        DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                        spcNavigatorRow["TEMPID"] = nPageId;
                        spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                        spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                        spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                        spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                        spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;
                        spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                        spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                        spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                        spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                        spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                        spcNavigatorRow["GROUPNN"] = nPageId;
                        outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }


        //*종료
        NEXT_NED:

            return rtnDataSet;
        }

        /// <summary>관리도 - NP
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnControlDataTable SpcLibNP(SPCPara para, SPCOption spcOption, ref SPCOutData outData)
        {
            RtnControlDataTable rtnDataSet = new RtnControlDataTable();
            DataTable rawData = para.InputData;
            ParPIDataTable dbPara = para.InputData;
            int i = 0;
            long idSeq = 0;
            SubGroupSum sg = new SubGroupSum();
            TempControlDataSTADataTable ctrDataSta = new TempControlDataSTADataTable();
            try
            {
                var sumSubGroup = from b in dbPara
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP,
                                      b.SAMPLEID,
                                      b.SAMPLING
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSAMPLEID = g.Key.SAMPLEID,
                                      vSAMPLING = g.Key.SAMPLING,
                                      vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),//8/8
                                      vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),//8/8
                                      vMAX = g.Max(s => s.NVALUE),
                                      vMIN = g.Min(s => s.NVALUE),
                                      vSUM = g.Sum(s => s.NVALUE),
                                      vSUMSUB = g.Sum(s => s.NSUBVALUE),
                                      vAVG = g.Average(s => s.NVALUE),
                                      vNN = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in sumSubGroup)
                {
                    sg.GROUPID = f.vGROUPID;
                    sg.SUBGROUP = f.vSUBGROUP;
                    sg.SAMPLEID = f.vSAMPLEID;
                    sg.SAMPLING = f.vSAMPLING;
                    sg.SUBGROUPNAME = f.vSUBGROUPNAME;
                    sg.SAMPLINGNAME = f.vSAMPLINGNAME;
                    sg.MAX = f.vMAX;
                    sg.MIN = f.vMIN;
                    //sg.R = sg.MAX - sg.MIN;
                    sg.SUM = f.vSUM;
                    sg.SUMSUB = f.vSUMSUB;
                    sg.R = f.vSUM * f.vSUMSUB;
                    sg.AVG = (f.vSUM * f.vSUMSUB) / f.vSUMSUB;
                    sg.NN = f.vNN;
                    sg.COUNT = f.vCOUNT;

                    idSeq++;
                    DataRow sgRow = ctrDataSta.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = sg.GROUPID;
                    sgRow["SUBGROUP"] = sg.SUBGROUP;
                    sgRow["SAMPLING"] = sg.SAMPLING;
                    sgRow["MAX"] = sg.MAX;
                    sgRow["MIN"] = sg.MIN;
                    sgRow["R"] = sg.R;
                    //sgRow["RUCL"] = "";
                    //sgRow["RLCL"] = "";
                    sgRow["BAR"] = sg.SUM;//NP
                    //sgRow["UCL"] = "";
                    //sgRow["LCL"] = "";
                    sgRow["SUMVALUE"] = sg.SUM;
                    sgRow["SUMSUBVALUE"] = sg.SUMSUB;
                    //sgRow["TOTSUM"] = "";
                    sgRow["TOTAVGVALUE"] = sg.AVG;
                    //sg.STDE = StdDevPpCd(dbPara, sg);
                    sg.STDE = 0f;// StdDevP(dbPara, sg);
                    sgRow["STDEVALUE"] = sg.STDE;
                    sgRow["NN"] = sg.NN;
                    sgRow["SNN"] = sg.NN - 1;
                    //sgRow["TOTNN"] = "";
                    //sgRow["GROUPNN"] = "";

                    ctrDataSta.Rows.Add(sgRow);

                    //Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            Console.WriteLine(ctrDataSta.Rows.Count);

            //*SBar 계산.
            TempControlDataTotDataTable ctrDataTot = new TempControlDataTotDataTable();
            try
            {
                var totSubGroup = from b in ctrDataSta
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSUMVALUE = g.Sum(s => s.SUMVALUE),
                                      vSUMSUBVALUE = g.Sum(s => s.SUMSUBVALUE),
                                      vSUMSUBAVG = g.Average(s => s.SUMSUBVALUE),
                                      vSUMRBAR = g.Sum(s => s.R),
                                      vXBAR = g.Average(s => s.BAR),
                                      vRBAR = g.Average(s => s.TOTAVGVALUE),
                                      vSTDEVP = g.Sum(s => s.STDEVALUE),
                                      vSUBGROUPCOUNT = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in totSubGroup)
                {
                    idSeq++;
                    DataRow sgRow = ctrDataTot.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = f.vGROUPID;
                    sgRow["SUBGROUP"] = f.vSUBGROUP;
                    sgRow["XBAR"] = f.vRBAR;
                    sgRow["SUMSTDEVP"] = f.vSUMVALUE / f.vSUMSUBVALUE; //PBAR
                    sgRow["SUMSTDEVPBAR"] = f.vSUMSUBVALUE / f.vSUMRBAR;//NPBAR
                    sgRow["RBAR"] = 0f;
                    sgRow["GROUPNN"] = f.vCOUNT;
                    ctrDataTot.Rows.Add(sgRow);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            para.rtnXBar = ReturnXBarResult.Create();
            SamplingGroupSum grpData = new SamplingGroupSum();

            int nPageId = 1;
            int nPageIdAdd = 0;
            int nPageMax = 4;
            idSeq = 1;
            string subGroupTemp = string.Empty;
            SpcPageField pageDataTemp = SpcPageField.Create();

            outData.spcDataTable.tableSubGroupSta.Rows.Clear();
            outData.spcDataTable.tableNavigator.Rows.Clear();

            try
            {
                //ctrDataSta
                for (i = 0; i < ctrDataSta.Rows.Count; i++)
                {
                    DataRow sgRow = ctrDataSta.Rows[i];
                    grpData.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                    grpData.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                    grpData.SAMPLING = sgRow["SAMPLING"].ToSafeString();
                    grpData.SUBGROUPNAME = sgRow["SUBGROUPNAME"].ToSafeString();
                    grpData.SAMPLINGNAME = sgRow["SAMPLINGNAME"].ToSafeString();
                    grpData.MAX = sgRow["MAX"].ToNullOrDouble();
                    grpData.MIN = sgRow["MIN"].ToNullOrDouble();
                    //grpData.R = sgRow["R"].ToNullOrDouble();
                    grpData.SUM = sgRow["SUMVALUE"].ToNullOrDouble();
                    grpData.SUMSUB = sgRow["SUMSUBVALUE"].ToNullOrDouble();
                    grpData.STDEP = sgRow["STDEVALUE"].ToNullOrDouble();
                    grpData.R = grpData.STDEP;
                    grpData.BAR = sgRow["BAR"].ToNullOrDouble();

                    //grpData.SUMVALUE = sgRow["SUMVALUE"].ToSafeDouble();
                    //grpData.STDEVALUE = sgRow["STDEVALUE"].ToSafeDouble();
                    grpData.NN = sgRow["NN"].ToSafeInt64();
                    grpData.SNN = sgRow["SNN"].ToSafeInt64();

                    var ctrTot = ctrDataTot.Where(x => x.GROUPID == grpData.GROUPID && x.SUBGROUP == grpData.SUBGROUP);

                    foreach (var f in ctrTot)
                    {
                        grpData.XBAR = f.XBAR;//NPBAR
                        grpData.SUMSTDEP = f.SUMSTDEVP;//PBAR
                        grpData.SUMSTDEPBAR = f.SUMSTDEVPBAR;
                        grpData.GROUPCOUNT = f.GROUPNN;
                        ctrDataSta.Rows[i]["XBAR"] = grpData.XBAR;
                        //ctrDataSta.Rows[i]["RBAR"] = grpData.RBAR;
                        ctrDataSta.Rows[i]["GROUPNN"] = grpData.GROUPCOUNT;

                        //XBar
                        double XPBAR = grpData.SUMSTDEP.ToSafeDoubleStaMax();
                        double XNPBAR = grpData.XBAR.ToSafeDoubleStaMax();
                        double sumValue = grpData.SUM.ToSafeDoubleStaMax();
                        double sumSub = grpData.SUMSUB.ToSafeDoubleStaMax();
                        grpData.UCL = XNPBAR + (3 * Math.Sqrt(XNPBAR * (1 - XPBAR)));
                        grpData.CL = XNPBAR;
                        grpData.LCL = XNPBAR - (3 * Math.Sqrt(XNPBAR * (1 - XPBAR)));

                        if (grpData.LCL <= 0)
                        {
                            grpData.LCL = 0f;
                        }

                        //S
                        grpData.RUCL = 0f;
                        grpData.RCL = 0f;
                        grpData.RLCL = 0f;

                        DataRow drRtn = rtnDataSet.NewRow();

                        drRtn["TEMPID"] = i;
                        drRtn["GROUPID"] = grpData.GROUPID;
                        drRtn["SUBGROUP"] = grpData.SUBGROUP;
                        drRtn["SAMPLING"] = grpData.SAMPLING;
                        drRtn["SUBGROUPNAME"] = grpData.SUBGROUPNAME;
                        drRtn["SAMPLINGNAME"] = grpData.SAMPLINGNAME;
                        drRtn["SAMESIGMA"] = 0.0;//1/7추가
                        drRtn["ISSAME"] = false; //item.ISSAME;//1/7추가
                        drRtn["MAX"] = grpData.MAX;
                        drRtn["MIN"] = grpData.MIN;
                        drRtn["R"] = grpData.R;
                        drRtn["RUCL"] = grpData.RUCL;
                        drRtn["RLCL"] = grpData.RLCL;
                        drRtn["RCL"] = grpData.RCL;
                        drRtn["BAR"] = grpData.BAR;
                        drRtn["UCL"] = grpData.UCL;
                        drRtn["LCL"] = grpData.LCL;
                        drRtn["CL"] = grpData.CL;
                        drRtn["XBAR"] = grpData.XBAR;
                        //drRtn["RBAR"] = grpData.RBAR;
                        //drRtn["SUMVALUE"] = grpData.SUMVALUE;
                        //drRtn["TOTSUM"] = grpData.TOTSUM;
                        SpcFunction.IsDbNckDoubleWrite(drRtn, "TOTSUM", grpData.TOTSUM);
                        //drRtn["TOTAVGVALUE"] = grpData.TOTAVGVALUE;
                        //drRtn["STDEVALUE"] = grpData.STDEVALUE;
                        drRtn["NN"] = grpData.NN;
                        drRtn["SNN"] = grpData.SNN;
                        drRtn["TOTNN"] = grpData.TOTNN;
                        drRtn["GROUPNN"] = grpData.GROUPCOUNT;

                        rtnDataSet.Rows.Add(drRtn);

                        //sia확인 : 관리도 NP Page 처리

                        #region Page 처리

                        DataRow spcReturnRow = outData.spcDataTable.tableSubGroupSta.NewRow();
                        spcReturnRow["TEMPID"] = i;
                        spcReturnRow["PAGEID"] = nPageId;
                        spcReturnRow["GROUPID"] = grpData.GROUPID.ToSafeInt64();
                        spcReturnRow["SUBGROUP"] = grpData.SUBGROUP;
                        spcReturnRow["XBAR"] = grpData.XBAR.ToSafeDoubleStaMax();
                        spcReturnRow["RBAR"] = grpData.RBAR.ToSafeDoubleStaMax();
                        spcReturnRow["GROUPNN"] = grpData.SNN;
                        outData.spcDataTable.tableSubGroupSta.Rows.Add(spcReturnRow);

                        if (nPageIdAdd > nPageMax)
                        {
                            DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                            spcNavigatorRow["TEMPID"] = nPageId;
                            spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                            spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                            spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                            spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                            spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                            spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                            spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                            spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                            spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                            spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                            spcNavigatorRow["GROUPNN"] = grpData.NN;
                            outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                            nPageId++;
                            nPageIdAdd = 1;

                            pageDataTemp.Start.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.Start.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.Start.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.Start.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.Start.nRBAR = pageDataTemp.Temp.nRBAR;

                            pageDataTemp.End.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.End.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.End.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.End.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.End.nRBAR = pageDataTemp.Temp.nRBAR;
                        }

                        if (nPageIdAdd == 0)
                        {
                            pageDataTemp.Start.nSEQID = i;
                            pageDataTemp.Start.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.Start.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.Start.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.Start.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        //SubGroup 구분
                        if (subGroupTemp != grpData.SUBGROUP)
                        {
                            subGroupTemp = grpData.SUBGROUP;
                            nPageIdAdd++;
                            if (nPageIdAdd > nPageMax)
                            {
                                pageDataTemp.Temp.nSEQID = i;
                                pageDataTemp.Temp.nGROUPID = grpData.GROUPID.ToSafeInt64();
                                pageDataTemp.Temp.SUBGROUP = grpData.SUBGROUP;
                                pageDataTemp.Temp.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                                pageDataTemp.Temp.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            }
                        }

                        if (nPageIdAdd <= nPageMax)
                        {
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        #endregion
                    }

                    //Console.WriteLine("XBar-R Summary : GROUPID = {0},  SUBGROUP = {1}, SAMPLEID = {2},  SAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , grpData.GROUPID, grpData.SUBGROUP, grpData.SAMPLEID, grpData.SAMPLING
                    //    , grpData.XBAR, grpData.RBAR, grpData.NN, grpData.GROUPCOUNT);
                }

                //마지막 Page Check
                if (nPageIdAdd <= nPageMax)
                {
                    DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                    spcNavigatorRow["TEMPID"] = nPageId;
                    spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                    spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                    spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                    spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                    spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                    spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                    spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                    spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                    spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                    spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                    spcNavigatorRow["GROUPNN"] = nPageId;
                    outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                }

                //1 Page Chart 4개 일때 Check 
                if (outData.spcDataTable.tableNavigator != null && outData.spcDataTable.tableNavigator.Rows.Count <= 0)
                {
                    if (nPageIdAdd > nPageMax)
                    {
                        DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                        spcNavigatorRow["TEMPID"] = nPageId;
                        spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                        spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                        spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                        spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                        spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;
                        spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                        spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                        spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                        spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                        spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                        spcNavigatorRow["GROUPNN"] = nPageId;
                        outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                    }
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }


        //*종료
        NEXT_NED:

            return rtnDataSet;
        }

        /// <summary>관리도 - C
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnControlDataTable SpcLibC(SPCPara para, SPCOption spcOption, ref SPCOutData outData)
        {
            RtnControlDataTable rtnDataSet = new RtnControlDataTable();
            DataTable rawData = para.InputData;
            ParPIDataTable dbPara = para.InputData;
            int i = 0;
            long idSeq = 0;
            SubGroupSum sg = new SubGroupSum();
            TempControlDataSTADataTable ctrDataSta = new TempControlDataSTADataTable();
            try
            {
                var sumSubGroup = from b in dbPara
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP,
                                      b.SAMPLEID,
                                      b.SAMPLING
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSAMPLEID = g.Key.SAMPLEID,
                                      vSAMPLING = g.Key.SAMPLING,
                                      vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),//8/8
                                      vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),//8/8
                                      vMAX = g.Max(s => s.NVALUE),
                                      vMIN = g.Min(s => s.NVALUE),
                                      vSUM = g.Sum(s => s.NVALUE),
                                      vSUMSUB = g.Sum(s => s.NSUBVALUE),
                                      vAVG = g.Average(s => s.NVALUE),
                                      vNN = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in sumSubGroup)
                {
                    sg.GROUPID = f.vGROUPID;
                    sg.SUBGROUP = f.vSUBGROUP;
                    sg.SAMPLEID = f.vSAMPLEID;
                    sg.SAMPLING = f.vSAMPLING;
                    sg.SUBGROUPNAME = f.vSUBGROUPNAME;
                    sg.SAMPLINGNAME = f.vSAMPLINGNAME;
                    sg.MAX = f.vMAX;
                    sg.MIN = f.vMIN;
                    sg.R = sg.MAX - sg.MIN;
                    sg.SUM = f.vSUM;
                    sg.SUMSUB = f.vSUMSUB;
                    sg.AVG = f.vSUM / f.vSUMSUB;//f.vAVG;
                    sg.NN = f.vNN;
                    sg.COUNT = f.vCOUNT;

                    idSeq++;
                    DataRow sgRow = ctrDataSta.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = sg.GROUPID;
                    sgRow["SUBGROUP"] = sg.SUBGROUP;
                    sgRow["SAMPLING"] = sg.SAMPLING;
                    sgRow["MAX"] = sg.MAX;
                    sgRow["MIN"] = sg.MIN;
                    //sgRow["R"] = sg.R;
                    //sgRow["RUCL"] = "";
                    //sgRow["RLCL"] = "";
                    sgRow["BAR"] = sg.SUM;//C
                    //sgRow["UCL"] = "";
                    //sgRow["LCL"] = "";
                    sgRow["SUMVALUE"] = sg.SUM;
                    sgRow["SUMSUBVALUE"] = sg.SUMSUB;
                    //sgRow["TOTSUM"] = "";
                    //sgRow["TOTAVGVALUE"] = "";
                    //sg.STDE = StdDevPpCd(dbPara, sg);
                    sg.STDE = 0f;// StdDevP(dbPara, sg);
                    sgRow["STDEVALUE"] = sg.STDE;
                    sgRow["R"] = sg.STDE;
                    sgRow["NN"] = sg.NN;
                    sgRow["SNN"] = sg.NN - 1;
                    //sgRow["TOTNN"] = "";
                    //sgRow["GROUPNN"] = "";

                    ctrDataSta.Rows.Add(sgRow);

                    //Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            Console.WriteLine(ctrDataSta.Rows.Count);

            //*SBar 계산.
            TempControlDataTotDataTable ctrDataTot = new TempControlDataTotDataTable();
            try
            {
                var totSubGroup = from b in ctrDataSta
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSUMVALUE = g.Sum(s => s.SUMVALUE),
                                      vSUMSUBVALUE = g.Sum(s => s.SUMSUBVALUE),
                                      vXBAR = g.Average(s => s.BAR),
                                      vRBAR = g.Average(s => s.R),
                                      vSTDEVP = g.Sum(s => s.STDEVALUE),
                                      vSUBGROUPCOUNT = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in totSubGroup)
                {
                    idSeq++;
                    DataRow sgRow = ctrDataTot.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = f.vGROUPID;
                    sgRow["SUBGROUP"] = f.vSUBGROUP;
                    sgRow["XBAR"] = f.vSUMVALUE / f.vCOUNT;
                    sgRow["RBAR"] = f.vRBAR;
                    sgRow["SUMSTDEVP"] = 0f;// f.vSTDEVP;
                    sgRow["SUMSTDEVPBAR"] = 0f;// f.vSTDEVP / f.vCOUNT;
                    sgRow["GROUPNN"] = f.vCOUNT;
                    ctrDataTot.Rows.Add(sgRow);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            para.rtnXBar = ReturnXBarResult.Create();
            SamplingGroupSum grpData = new SamplingGroupSum();

            int nPageId = 1;
            int nPageIdAdd = 0;
            int nPageMax = 4;
            idSeq = 1;
            string subGroupTemp = string.Empty;
            SpcPageField pageDataTemp = SpcPageField.Create();

            outData.spcDataTable.tableSubGroupSta.Rows.Clear();
            outData.spcDataTable.tableNavigator.Rows.Clear();

            try
            {
                //ctrDataSta
                for (i = 0; i < ctrDataSta.Rows.Count; i++)
                {
                    DataRow sgRow = ctrDataSta.Rows[i];
                    grpData.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                    grpData.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                    grpData.SAMPLING = sgRow["SAMPLING"].ToSafeString();
                    grpData.SUBGROUPNAME = sgRow["SUBGROUPNAME"].ToSafeString();
                    grpData.SAMPLINGNAME = sgRow["SAMPLINGNAME"].ToSafeString();
                    grpData.MAX = sgRow["MAX"].ToNullOrDouble();
                    grpData.MIN = sgRow["MIN"].ToNullOrDouble();
                    //grpData.R = sgRow["R"].ToNullOrDouble();
                    grpData.SUM = sgRow["SUMVALUE"].ToNullOrDouble();
                    grpData.SUMSUB = sgRow["SUMSUBVALUE"].ToNullOrDouble();
                    grpData.STDEP = sgRow["STDEVALUE"].ToNullOrDouble();
                    grpData.R = grpData.STDEP;
                    grpData.BAR = sgRow["BAR"].ToNullOrDouble();

                    //grpData.SUMVALUE = sgRow["SUMVALUE"].ToSafeDouble();
                    //grpData.STDEVALUE = sgRow["STDEVALUE"].ToSafeDouble();
                    grpData.NN = sgRow["NN"].ToSafeInt64();
                    grpData.SNN = sgRow["SNN"].ToSafeInt64();

                    var ctrTot = ctrDataTot.Where(x => x.GROUPID == grpData.GROUPID && x.SUBGROUP == grpData.SUBGROUP);

                    foreach (var f in ctrTot)
                    {
                        grpData.XBAR = f.XBAR;
                        grpData.RBAR = f.SUMSTDEVPBAR;
                        grpData.GROUPCOUNT = f.GROUPNN;
                        ctrDataSta.Rows[i]["XBAR"] = grpData.XBAR;
                        ctrDataSta.Rows[i]["RBAR"] = grpData.RBAR;
                        ctrDataSta.Rows[i]["GROUPNN"] = grpData.GROUPCOUNT;

                        //XBar
                        double XCBAR = grpData.XBAR.ToSafeDoubleStaMax();
                        double sumValue = grpData.SUM.ToSafeDoubleStaMax();
                        double sumSub = grpData.SUMSUB.ToSafeDoubleStaMax();
                        grpData.UCL = XCBAR + (3 * Math.Sqrt(XCBAR));
                        grpData.CL = XCBAR;
                        grpData.LCL = XCBAR - (3 * Math.Sqrt(XCBAR));

                        if (grpData.LCL <= 0)
                        {
                            grpData.LCL = 0f;
                        }

                        //S
                        grpData.RUCL = 0f;
                        grpData.RCL = 0f;
                        grpData.RLCL = 0f;

                        DataRow drRtn = rtnDataSet.NewRow();

                        drRtn["TEMPID"] = i;
                        drRtn["GROUPID"] = grpData.GROUPID;
                        drRtn["SUBGROUP"] = grpData.SUBGROUP;
                        drRtn["SAMPLING"] = grpData.SAMPLING;
                        drRtn["SUBGROUPNAME"] = grpData.SUBGROUPNAME;
                        drRtn["SAMPLINGNAME"] = grpData.SAMPLINGNAME;
                        drRtn["SAMESIGMA"] = 0.0;//1/7추가
                        drRtn["ISSAME"] = false; //item.ISSAME;//1/7추가
                        drRtn["MAX"] = grpData.MAX;
                        drRtn["MIN"] = grpData.MIN;
                        drRtn["R"] = grpData.R;
                        drRtn["RUCL"] = grpData.RUCL;
                        drRtn["RLCL"] = grpData.RLCL;
                        drRtn["RCL"] = grpData.RCL;
                        drRtn["BAR"] = grpData.BAR;
                        drRtn["UCL"] = grpData.UCL;
                        drRtn["LCL"] = grpData.LCL;
                        drRtn["CL"] = grpData.CL;
                        drRtn["XBAR"] = grpData.XBAR;
                        drRtn["RBAR"] = grpData.RBAR;
                        //drRtn["SUMVALUE"] = grpData.SUMVALUE;
                        //drRtn["TOTSUM"] = grpData.TOTSUM;
                        SpcFunction.IsDbNckDoubleWrite(drRtn, "TOTSUM", grpData.TOTSUM);
                        //drRtn["TOTAVGVALUE"] = grpData.TOTAVGVALUE;
                        //drRtn["STDEVALUE"] = grpData.STDEVALUE;
                        drRtn["NN"] = grpData.NN;
                        drRtn["SNN"] = grpData.SNN;
                        drRtn["TOTNN"] = grpData.TOTNN;
                        drRtn["GROUPNN"] = grpData.GROUPCOUNT;

                        rtnDataSet.Rows.Add(drRtn);

                        //sia확인 : 관리도 C Page 처리

                        #region Page 처리

                        DataRow spcReturnRow = outData.spcDataTable.tableSubGroupSta.NewRow();
                        spcReturnRow["TEMPID"] = i;
                        spcReturnRow["PAGEID"] = nPageId;
                        spcReturnRow["GROUPID"] = grpData.GROUPID.ToSafeInt64();
                        spcReturnRow["SUBGROUP"] = grpData.SUBGROUP;
                        spcReturnRow["XBAR"] = grpData.XBAR.ToSafeDoubleStaMax();
                        spcReturnRow["RBAR"] = grpData.RBAR.ToSafeDoubleStaMax();
                        spcReturnRow["GROUPNN"] = grpData.SNN;
                        outData.spcDataTable.tableSubGroupSta.Rows.Add(spcReturnRow);

                        if (nPageIdAdd > nPageMax)
                        {
                            DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                            spcNavigatorRow["TEMPID"] = nPageId;
                            spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                            spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                            spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                            spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                            spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                            spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                            spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                            spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                            spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                            spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                            spcNavigatorRow["GROUPNN"] = grpData.NN;
                            outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                            nPageId++;
                            nPageIdAdd = 1;

                            pageDataTemp.Start.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.Start.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.Start.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.Start.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.Start.nRBAR = pageDataTemp.Temp.nRBAR;

                            pageDataTemp.End.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.End.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.End.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.End.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.End.nRBAR = pageDataTemp.Temp.nRBAR;
                        }

                        if (nPageIdAdd == 0)
                        {
                            pageDataTemp.Start.nSEQID = i;
                            pageDataTemp.Start.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.Start.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.Start.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.Start.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        //SubGroup 구분
                        if (subGroupTemp != grpData.SUBGROUP)
                        {
                            subGroupTemp = grpData.SUBGROUP;
                            nPageIdAdd++;
                            if (nPageIdAdd > nPageMax)
                            {
                                pageDataTemp.Temp.nSEQID = i;
                                pageDataTemp.Temp.nGROUPID = grpData.GROUPID.ToSafeInt64();
                                pageDataTemp.Temp.SUBGROUP = grpData.SUBGROUP;
                                pageDataTemp.Temp.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                                pageDataTemp.Temp.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            }
                        }

                        if (nPageIdAdd <= nPageMax)
                        {
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        #endregion
                    }

                    //Console.WriteLine("XBar-R Summary : GROUPID = {0},  SUBGROUP = {1}, SAMPLEID = {2},  SAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , grpData.GROUPID, grpData.SUBGROUP, grpData.SAMPLEID, grpData.SAMPLING
                    //    , grpData.XBAR, grpData.RBAR, grpData.NN, grpData.GROUPCOUNT);
                }

                //마지막 Page Check
                if (nPageIdAdd <= nPageMax)
                {
                    DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                    spcNavigatorRow["TEMPID"] = nPageId;
                    spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                    spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                    spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                    spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                    spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                    spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                    spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                    spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                    spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                    spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                    spcNavigatorRow["GROUPNN"] = nPageId;
                    outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                }

                //1 Page Chart 4개 일때 Check 
                if (outData.spcDataTable.tableNavigator != null && outData.spcDataTable.tableNavigator.Rows.Count <= 0)
                {
                    if (nPageIdAdd > nPageMax)
                    {
                        DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                        spcNavigatorRow["TEMPID"] = nPageId;
                        spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                        spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                        spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                        spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                        spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;
                        spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                        spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                        spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                        spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                        spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                        spcNavigatorRow["GROUPNN"] = nPageId;
                        outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                    }
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }


        //*종료
        NEXT_NED:

            return rtnDataSet;
        }

        /// <summary>관리도 - U
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <param name="spcOption"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public static RtnControlDataTable SpcLibU(SPCPara para, SPCOption spcOption, ref SPCOutData outData)
        {
            RtnControlDataTable rtnDataSet = new RtnControlDataTable();
            DataTable rawData = para.InputData;
            ParPIDataTable dbPara = para.InputData;
            int i = 0;
            long idSeq = 0;
            SubGroupSum sg = new SubGroupSum();
            TempControlDataSTADataTable ctrDataSta = new TempControlDataSTADataTable();
            try
            {
                var sumSubGroup = from b in dbPara
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP,
                                      b.SAMPLEID,
                                      b.SAMPLING
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSAMPLEID = g.Key.SAMPLEID,
                                      vSAMPLING = g.Key.SAMPLING,
                                      vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),//8/8
                                      vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),//8/8
                                      vMAX = g.Max(s => s.NVALUE),
                                      vMIN = g.Min(s => s.NVALUE),
                                      vSUM = g.Sum(s => s.NVALUE),
                                      vSUMSUB = g.Sum(s => s.NSUBVALUE),
                                      vAVG = g.Average(s => s.NVALUE),
                                      vNN = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in sumSubGroup)
                {
                    sg.GROUPID = f.vGROUPID;
                    sg.SUBGROUP = f.vSUBGROUP;
                    sg.SAMPLEID = f.vSAMPLEID;
                    sg.SAMPLING = f.vSAMPLING;
                    sg.SUBGROUPNAME = f.vSUBGROUPNAME;
                    sg.SAMPLINGNAME = f.vSAMPLINGNAME;
                    sg.MAX = f.vMAX;
                    sg.MIN = f.vMIN;
                    sg.R = sg.MAX - sg.MIN;
                    sg.SUM = f.vSUM;
                    sg.SUMSUB = f.vSUMSUB;
                    sg.AVG = f.vSUM / f.vSUMSUB;//f.vAVG;
                    sg.NN = f.vNN;
                    sg.COUNT = f.vCOUNT;

                    idSeq++;
                    DataRow sgRow = ctrDataSta.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = sg.GROUPID;
                    sgRow["SUBGROUP"] = sg.SUBGROUP;
                    sgRow["SAMPLING"] = sg.SAMPLING;
                    sgRow["MAX"] = sg.MAX;
                    sgRow["MIN"] = sg.MIN;
                    //sgRow["R"] = sg.R;
                    //sgRow["RUCL"] = "";
                    //sgRow["RLCL"] = "";
                    sgRow["BAR"] = sg.AVG;//P
                    //sgRow["UCL"] = "";
                    //sgRow["LCL"] = "";
                    sgRow["SUMVALUE"] = sg.SUM;
                    sgRow["SUMSUBVALUE"] = sg.SUMSUB;
                    //sgRow["TOTSUM"] = "";
                    //sgRow["TOTAVGVALUE"] = "";
                    //sg.STDE = StdDevPpCd(dbPara, sg);
                    sg.STDE = 0f;// StdDevP(dbPara, sg);
                    sgRow["STDEVALUE"] = sg.STDE;
                    sgRow["R"] = sg.STDE;
                    sgRow["NN"] = sg.NN;
                    sgRow["SNN"] = sg.NN - 1;
                    //sgRow["TOTNN"] = "";
                    //sgRow["GROUPNN"] = "";

                    ctrDataSta.Rows.Add(sgRow);

                    Console.WriteLine("Summary : sgGROUPID = {0},  sgSUBGROUP = {1}, sgSAMPLEID = {2},  sgSAMPLING = {3}, " +
                        "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                        , sg.GROUPID, sg.SUBGROUP, sg.SAMPLEID, sg.SAMPLING, sg.SUM, sg.AVG, sg.NN, sg.COUNT);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }

            Console.WriteLine(ctrDataSta.Rows.Count);

            //*SBar 계산.
            TempControlDataTotDataTable ctrDataTot = new TempControlDataTotDataTable();
            try
            {
                var totSubGroup = from b in ctrDataSta
                                  group b by new
                                  {
                                      b.GROUPID,
                                      b.SUBGROUP
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Key.GROUPID,
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSUMVALUE = g.Sum(s => s.SUMVALUE),
                                      vSUMSUBVALUE = g.Sum(s => s.SUMSUBVALUE),
                                      vXBAR = g.Average(s => s.BAR),
                                      vRBAR = g.Average(s => s.R),
                                      vSTDEVP = g.Sum(s => s.STDEVALUE),
                                      vSUBGROUPCOUNT = g.Sum(s => 1),
                                      vCOUNT = g.Count()
                                  };

                foreach (var f in totSubGroup)
                {
                    idSeq++;
                    DataRow sgRow = ctrDataTot.NewRow();
                    sgRow["TEMPID"] = idSeq;
                    sgRow["GROUPID"] = f.vGROUPID;
                    sgRow["SUBGROUP"] = f.vSUBGROUP;
                    sgRow["XBAR"] = f.vSUMVALUE / f.vSUMSUBVALUE;
                    sgRow["RBAR"] = f.vRBAR;
                    sgRow["SUMSTDEVP"] = 0f;// f.vSTDEVP;
                    sgRow["SUMSTDEVPBAR"] = 0f;// f.vSTDEVP / f.vCOUNT;
                    sgRow["GROUPNN"] = f.vCOUNT;
                    ctrDataTot.Rows.Add(sgRow);
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
                goto NEXT_NED;
            }

            para.rtnXBar = ReturnXBarResult.Create();
            SamplingGroupSum grpData = new SamplingGroupSum();

            int nPageId = 1;
            int nPageIdAdd = 0;
            int nPageMax = 4;
            idSeq = 1;
            string subGroupTemp = string.Empty;
            SpcPageField pageDataTemp = SpcPageField.Create();

            outData.spcDataTable.tableSubGroupSta.Rows.Clear();
            outData.spcDataTable.tableNavigator.Rows.Clear();

            try
            {
                //ctrDataSta
                for (i = 0; i < ctrDataSta.Rows.Count; i++)
                {
                    DataRow sgRow = ctrDataSta.Rows[i];
                    grpData.GROUPID = sgRow["GROUPID"].ToSafeInt64();
                    grpData.SUBGROUP = sgRow["SUBGROUP"].ToSafeString();
                    grpData.SAMPLING = sgRow["SAMPLING"].ToSafeString();
                    grpData.SUBGROUPNAME = sgRow["SUBGROUPNAME"].ToSafeString();
                    grpData.SAMPLINGNAME = sgRow["SAMPLINGNAME"].ToSafeString();
                    grpData.MAX = sgRow["MAX"].ToNullOrDouble();
                    grpData.MIN = sgRow["MIN"].ToNullOrDouble();
                    //grpData.R = sgRow["R"].ToNullOrDouble();
                    grpData.SUM = sgRow["SUMVALUE"].ToNullOrDouble();
                    grpData.SUMSUB = sgRow["SUMSUBVALUE"].ToNullOrDouble();
                    grpData.STDEP = sgRow["STDEVALUE"].ToNullOrDouble();
                    grpData.R = grpData.STDEP;
                    grpData.BAR = sgRow["BAR"].ToNullOrDouble();

                    //grpData.SUMVALUE = sgRow["SUMVALUE"].ToSafeDouble();
                    //grpData.STDEVALUE = sgRow["STDEVALUE"].ToSafeDouble();
                    grpData.NN = sgRow["NN"].ToSafeInt64();
                    grpData.SNN = sgRow["SNN"].ToSafeInt64();

                    var ctrTot = ctrDataTot.Where(x => x.GROUPID == grpData.GROUPID && x.SUBGROUP == grpData.SUBGROUP);

                    foreach (var f in ctrTot)
                    {
                        grpData.XBAR = f.XBAR;
                        grpData.RBAR = f.SUMSTDEVPBAR;
                        grpData.GROUPCOUNT = f.GROUPNN;
                        ctrDataSta.Rows[i]["XBAR"] = grpData.XBAR;
                        ctrDataSta.Rows[i]["RBAR"] = grpData.RBAR;
                        ctrDataSta.Rows[i]["GROUPNN"] = grpData.GROUPCOUNT;

                        //XBar
                        double XUBAR = grpData.XBAR.ToSafeDoubleStaMax();
                        double sumValue = grpData.SUM.ToSafeDoubleStaMax();
                        double sumSub = grpData.SUMSUB.ToSafeDoubleStaMax();
                        grpData.UCL = XUBAR + (3 * Math.Sqrt(XUBAR / sumSub));
                        grpData.CL = XUBAR;
                        grpData.LCL = XUBAR - (3 * Math.Sqrt(XUBAR / sumSub));

                        if (grpData.LCL <= 0)
                        {
                            grpData.LCL = 0f;
                        }

                        //S
                        grpData.RUCL = 0f;
                        grpData.RCL = 0f;
                        grpData.RLCL = 0f;

                        DataRow drRtn = rtnDataSet.NewRow();

                        drRtn["TEMPID"] = i;
                        drRtn["GROUPID"] = grpData.GROUPID;
                        drRtn["SUBGROUP"] = grpData.SUBGROUP;
                        drRtn["SAMPLING"] = grpData.SAMPLING;
                        drRtn["SUBGROUPNAME"] = grpData.SUBGROUPNAME;
                        drRtn["SAMPLINGNAME"] = grpData.SAMPLINGNAME;
                        drRtn["SAMESIGMA"] = 0.0;//1/7추가
                        drRtn["ISSAME"] = false; //item.ISSAME;//1/7추가
                        drRtn["MAX"] = grpData.MAX;
                        drRtn["MIN"] = grpData.MIN;
                        drRtn["R"] = grpData.R;
                        drRtn["RUCL"] = grpData.RUCL;
                        drRtn["RLCL"] = grpData.RLCL;
                        drRtn["RCL"] = grpData.RCL;
                        drRtn["BAR"] = grpData.BAR;
                        drRtn["UCL"] = grpData.UCL;
                        drRtn["LCL"] = grpData.LCL;
                        drRtn["CL"] = grpData.CL;
                        drRtn["XBAR"] = grpData.XBAR;
                        drRtn["RBAR"] = grpData.RBAR;
                        //drRtn["SUMVALUE"] = grpData.SUMVALUE;
                        //drRtn["TOTSUM"] = grpData.TOTSUM;
                        SpcFunction.IsDbNckDoubleWrite(drRtn, "TOTSUM", grpData.TOTSUM);
                        //drRtn["TOTAVGVALUE"] = grpData.TOTAVGVALUE;
                        //drRtn["STDEVALUE"] = grpData.STDEVALUE;
                        drRtn["NN"] = grpData.NN;
                        drRtn["SNN"] = grpData.SNN;
                        drRtn["TOTNN"] = grpData.TOTNN;
                        drRtn["GROUPNN"] = grpData.GROUPCOUNT;

                        rtnDataSet.Rows.Add(drRtn);

                        //sia확인 : 관리도 U Page 처리

                        #region Page 처리

                        DataRow spcReturnRow = outData.spcDataTable.tableSubGroupSta.NewRow();
                        spcReturnRow["TEMPID"] = i;
                        spcReturnRow["PAGEID"] = nPageId;
                        spcReturnRow["GROUPID"] = grpData.GROUPID.ToSafeInt64();
                        spcReturnRow["SUBGROUP"] = grpData.SUBGROUP;
                        spcReturnRow["XBAR"] = grpData.XBAR.ToSafeDoubleStaMax();
                        spcReturnRow["RBAR"] = grpData.RBAR.ToSafeDoubleStaMax();
                        spcReturnRow["GROUPNN"] = grpData.SNN;
                        outData.spcDataTable.tableSubGroupSta.Rows.Add(spcReturnRow);

                        if (nPageIdAdd > nPageMax)
                        {
                            DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                            spcNavigatorRow["TEMPID"] = nPageId;
                            spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                            spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                            spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                            spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                            spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                            spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                            spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                            spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                            spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                            spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                            spcNavigatorRow["GROUPNN"] = grpData.NN;
                            outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                            nPageId++;
                            nPageIdAdd = 1;

                            pageDataTemp.Start.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.Start.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.Start.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.Start.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.Start.nRBAR = pageDataTemp.Temp.nRBAR;

                            pageDataTemp.End.nSEQID = pageDataTemp.Temp.nSEQID;
                            pageDataTemp.End.nGROUPID = pageDataTemp.Temp.nGROUPID;
                            pageDataTemp.End.SUBGROUP = pageDataTemp.Temp.SUBGROUP;
                            pageDataTemp.End.nXBAR = pageDataTemp.Temp.nXBAR;
                            pageDataTemp.End.nRBAR = pageDataTemp.Temp.nRBAR;
                        }

                        if (nPageIdAdd == 0)
                        {
                            pageDataTemp.Start.nSEQID = i;
                            pageDataTemp.Start.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.Start.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.Start.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.Start.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        //SubGroup 구분
                        if (subGroupTemp != grpData.SUBGROUP)
                        {
                            subGroupTemp = grpData.SUBGROUP;
                            nPageIdAdd++;
                            if (nPageIdAdd > nPageMax)
                            {
                                pageDataTemp.Temp.nSEQID = i;
                                pageDataTemp.Temp.nGROUPID = grpData.GROUPID.ToSafeInt64();
                                pageDataTemp.Temp.SUBGROUP = grpData.SUBGROUP;
                                pageDataTemp.Temp.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                                pageDataTemp.Temp.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                            }
                        }

                        if (nPageIdAdd <= nPageMax)
                        {
                            pageDataTemp.End.nSEQID = i;
                            pageDataTemp.End.nGROUPID = grpData.GROUPID.ToSafeInt64();
                            pageDataTemp.End.SUBGROUP = grpData.SUBGROUP;
                            pageDataTemp.End.nXBAR = grpData.XBAR.ToSafeDoubleStaMax();
                            pageDataTemp.End.nRBAR = grpData.RBAR.ToSafeDoubleStaMax();
                        }

                        #endregion
                    }

                    //Console.WriteLine("XBar-R Summary : GROUPID = {0},  SUBGROUP = {1}, SAMPLEID = {2},  SAMPLING = {3}, " +
                    //    "sgSSUM = {4},  sgSAVG = {5}, sgSNN = {6}, sgSCOUNT = {7}"
                    //    , grpData.GROUPID, grpData.SUBGROUP, grpData.SAMPLEID, grpData.SAMPLING
                    //    , grpData.XBAR, grpData.RBAR, grpData.NN, grpData.GROUPCOUNT);
                }

                //마지막 Page Check
                if (nPageIdAdd <= nPageMax)
                {
                    DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                    spcNavigatorRow["TEMPID"] = nPageId;
                    spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                    spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                    spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                    spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                    spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;

                    spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                    spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                    spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                    spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                    spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                    spcNavigatorRow["GROUPNN"] = nPageId;
                    outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                }

                //1 Page Chart 4개 일때 Check 
                if (outData.spcDataTable.tableNavigator != null && outData.spcDataTable.tableNavigator.Rows.Count <= 0)
                {
                    if (nPageIdAdd > nPageMax)
                    {
                        DataRow spcNavigatorRow = outData.spcDataTable.tableNavigator.NewRow();
                        spcNavigatorRow["TEMPID"] = nPageId;
                        spcNavigatorRow["SEQIDSTART"] = pageDataTemp.Start.nSEQID;
                        spcNavigatorRow["GROUPIDSTART"] = pageDataTemp.Start.nGROUPID;
                        spcNavigatorRow["SUBGROUPSTART"] = pageDataTemp.Start.SUBGROUP;
                        spcNavigatorRow["XBARSTART"] = pageDataTemp.Start.nXBAR;
                        spcNavigatorRow["RBARSTART"] = pageDataTemp.Start.nRBAR;
                        spcNavigatorRow["SEQIDEND"] = pageDataTemp.End.nSEQID;
                        spcNavigatorRow["GROUPIDEND"] = pageDataTemp.End.nGROUPID;
                        spcNavigatorRow["SUBGROUPEND"] = pageDataTemp.End.SUBGROUP;
                        spcNavigatorRow["XBAREND"] = pageDataTemp.End.nXBAR;
                        spcNavigatorRow["RBAREND"] = pageDataTemp.End.nRBAR;
                        spcNavigatorRow["GROUPNN"] = nPageId;
                        outData.spcDataTable.tableNavigator.Rows.Add(spcNavigatorRow);
                    }
                }
            }
            catch (Exception ex)
            {
                outData.ERRORNO = ex.HResult;
                outData.ERRORMESSAGE = ex.Message.ToString();
            }


        //*종료
        NEXT_NED:

            return rtnDataSet;
        }
        #endregion

        #region 공정능력 PPM
        /// <summary>
        /// 공정능력 PPM 기대성능별 계산 함수.
        /// </summary>
        /// <param name="option"></param>
        /// <param name="specType"></param>
        /// <param name="xbar"></param>
        /// <param name="spec"></param>
        /// <param name="sgmar"></param>
        /// <param name="roundValue"></param>
        /// <returns></returns>
        public static double SpcPpm(SpcPpmMode option, SpcSpecType specType, double xbar, double spec, double sgmar, out double roundValue)
        {
            double resultValue = SpcLimit.MIN;
            roundValue = SpcLimit.MIN; ;
            double nValue01 = 0f;
            double nValue02;
            double nValue03;
            try
            {
                if (xbar == SpcLimit.MIN)
                {
                    return resultValue;
                }

                if (sgmar == SpcLimit.MIN)
                {
                    return resultValue;
                }

                if (spec == SpcLimit.MIN)
                {
                    return resultValue;
                }


                switch (specType)
                {
                    case SpcSpecType.LSL:
                        nValue01 = (xbar - spec) / sgmar;
                        break;
                    case SpcSpecType.USL:
                        nValue01 = (spec - xbar) / sgmar;
                        break;
                    case SpcSpecType.Target:
                        break;
                    default:
                        break;
                }

                nValue02 = CumDensity(nValue01);
                nValue03 = (1 - nValue02);
                resultValue = NPPMPARTSPER * nValue03;
                roundValue = Math.Round(resultValue, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return resultValue;
        }

        public static double SpcPpmObserve(SpcPpmMode option, SpcSpecType specType, double xbar, double spec, double nNCount, ParPIDataTable rawData, out double nObserveSpecCount, out double roundValue)
        {
            double resultValue = SpcLimit.MIN;
            nObserveSpecCount = SpcLimit.MIN;
            roundValue = SpcLimit.MIN;
            double nValue01 = 0f;

            try
            {
                if (xbar == SpcLimit.MIN)
                {
                    return resultValue;
                }

                if (spec == SpcLimit.MIN)
                {
                    return resultValue;
                }
                
                if (rawData != null && rawData.Rows.Count > 0)
                {
                    //Next...
                }
                else
                {
                    return resultValue;
                }

                double nObserveCount = 0;
                switch (specType)
                {
                    case SpcSpecType.LSL:
                        var dataLsl = rawData.AsEnumerable();
                        var lstRowLsl = dataLsl.AsParallel().Where(w => w.Field<double>("NVALUE") < spec).OrderBy(or => or.Field<string>("SAMPLING"));
                        foreach (DataRow row in lstRowLsl)
                        {
                            nObserveCount += 1;
                        }
                        break;
                    case SpcSpecType.USL:
                        var dataUsl = rawData.AsEnumerable();
                        var lstRowUsl = dataUsl.AsParallel().Where(w => w.Field<double>("NVALUE") > spec).OrderBy(or => or.Field<string>("SAMPLING"));
                        foreach (DataRow row in lstRowUsl)
                        {
                            nObserveCount += 1;
                        }
                        break;
                    case SpcSpecType.Target:
                        break;
                    default:
                        break;
                }

                nObserveSpecCount = nObserveCount;
                nValue01 = nObserveCount / nNCount;
                resultValue = NPPMPARTSPER * nValue01;
                roundValue = Math.Round(resultValue, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return resultValue;
        }


        #endregion 공정능력 PPM

        #region 통계 함수

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbPara"></param>
        /// <param name="sg"></param>
        /// <returns></returns>
        public static double? StdDevPpCd(ParPIDataTable dbPara, SubGroupSum sg, out int errorNo, out string errorMessage)
        {
            double? rtnResult = null;
            errorNo = 0;
            errorMessage = "";
            var ssd = from item in dbPara
                      where (item.GROUPID == sg.GROUPID
                      && item.SUBGROUP == sg.SUBGROUP
                      && item.SAMPLEID == sg.SAMPLEID
                      && item.SAMPLING == sg.SAMPLING)
                      select item.NVALUE;

            rtnResult = SPCSta.StdDev(ssd, out errorNo, out errorMessage);

            return rtnResult;
        }

        /// <summary>
        /// 표준편차 계산.
        /// </summary>
        /// <param name="dbPara"></param>
        /// <param name="sg"></param>
        /// <returns></returns>
        public static double? StdDevP(ParPIDataTable dbPara, SubGroupSum sg)
        {
            double? rtnResult = null;

            var ssd = from item in dbPara
                      where (item.GROUPID == sg.GROUPID
                      && item.SUBGROUP == sg.SUBGROUP
                      && item.SAMPLEID == sg.SAMPLEID
                      && item.SAMPLING == sg.SAMPLING)
                      select item.NVALUE;

            rtnResult = SPCSta.StdDevP(ssd);

            return rtnResult;
        }

        /// <summary>
        /// 표준편차 및 분산 계산.
        /// </summary>
        /// <param name="dbPara"></param>
        /// <param name="sg"></param>
        /// <returns></returns>
        public static double? StdDevPV(ParPIDataTable dbPara, SubGroupSum sg, out double? stdSum)
        {
            double? rtnResult = null;
            stdSum = null;

            var ssd = from item in dbPara
                      where (item.GROUPID == sg.GROUPID
                      && item.SUBGROUP == sg.SUBGROUP
                      && item.SAMPLEID == sg.SAMPLEID
                      && item.SAMPLING == sg.SAMPLING)
                      select item.NVALUE;

            rtnResult = SPCSta.StdDevPV(ssd, out stdSum);

            return rtnResult;
        }

        /// <summary>
        /// 누적 분포 함수.
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public static double CumDensity(double z)
        {
            double p = 0.3275911;
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;

            double a4 = -1.453152027;
            double a5 = 1.061405429;
            
            int sign;
            if (z < 0.0)
            {
                sign = -1;
            }
            else
            {
                sign = 1;
            }

            double x = Math.Abs(z) / Math.Sqrt(2.0);
            double t = 1.0 / (1.0 + (p * x));
            double erf = 1.0 - (((((((((a5 * t) + a4) * t) + a3) * t) + a2) * t) + a1) * t * Math.Exp(-x * x));
            return 0.5 * (1.0 + (sign * erf));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Phi(double x)
        {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
            {
                sign = -1;
            }

            x = Math.Abs(x) / Math.Sqrt(2.0);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + (p * x));
            double y = 1.0 - (((((((((a5 * t) + a4) * t) + a3) * t) + a2) * t) + a1) * t * Math.Exp(-x * x));

            return 0.5 * (1.0 + (sign * y));
        }

        /// <summary>
        /// 
        /// </summary>
        static void TestPhi()
        {
            // Select a few input values
            double[] x = { -3, -1, 0.0, 0.5, 2.1 };

            // Output computed by Mathematica
            // y = Phi[x]
            double[] y =
            {
                0.00134989803163,
                0.158655253931,
                0.5,
                0.691462461274,
                0.982135579437
            };

            double maxError = 0.0;
            for (int i = 0; i < x.Length; ++i)
            {
                double error = Math.Abs(y[i] - Phi(x[i]));
                if (error > maxError)
                {
                    maxError = error;
                }
            }

            Console.WriteLine("Maximum error: {0}", maxError);
        }

        #endregion
    }

}
