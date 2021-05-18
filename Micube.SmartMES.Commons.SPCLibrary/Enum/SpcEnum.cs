#region using

using System.Drawing;

#endregion

/// <summary>
/// SPC 통계 분석용 열거형. 
/// </summary>
namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// 프 로 그 램 명  : SPC Library
    /// 업  무  설  명  : SPC 통계에서 사용되는 열거형 변수 선언.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-08
    /// 수  정  이  력  : 
    /// 2019-07-11  SpcNumericalInitial 추가, SpcLimit Class형으로 변경
    /// 2019-07-08  최초작성
    /// 
    /// </summary>


    /// <summary>
    /// SPC Chart Type
    /// </summary>
    public enum SpcChartType : int
    {
        xbarr = 11,
        r = 12,
        xbars = 21,
        s = 22,
        m = 31,

        np = 41,
        p = 51,
        c = 61,
        u = 71,
    }

    /// <summary>
    /// 관리도 Chart Type
    /// </summary>
    public enum ControlChartType
    {
        XBar_R = 1,
        XBar_S = 2,
        I_MR = 3,
        Merger = 4,
        np = 5,
        p = 6,
        c = 7,
        u = 8,
    }
    /// <summary>
    /// SPC 한계값 설정.
    /// </summary>
    public static class SpcLimit
    {
        public const double MAX = 999999999f;
        public const double MIN = -999999999f;
        public const string SMAX = "999999999";
        public const string SMIN = "-999999999";
        public const string MAXTXT = "1000000000";
        public const string MINTXT = "-1000000000";
        public const string NONEDATA = "NaN";
        /// <summary>
        /// Chart 그룹 레이아웃 타이틀 앞 여백
        /// </summary>
        public const string ChartGroupLayoutTitleSpace = "    ";
    };

    /// <summary>
    /// SPC 통계 수치 초기화 방법 구분.
    /// </summary>
    public enum SpcNumericalInitial
    {
        Sta = 0,
        NaN = 1,
        Zero = 2
    }

    /// <summary>
    /// 추정치 사용여부
    /// </summary>
    public enum SigmaType
    {
        Yes = 0,
        No = 1
    }
    /// <summary>
    /// 관리선 구분
    /// </summary>
    public enum LimitType
    {
        /// <summary>
        /// 해석용
        /// </summary>
        Interpretation = 0,
        /// <summary>
        /// 관리용
        /// </summary>
        Management = 1,
        /// <summary>
        /// 직접입력
        /// </summary>
        Direct = 2
    }

    /// <summary>
    /// 해석용 관리선 표시유무
    /// </summary>
    public class IsInterpretation
    {
        public bool XBAR;
        public bool XRAR;

        public bool UCL;
        public bool CCL;
        public bool LCL;

        public bool RUCL;
        public bool RCCL;
        public bool RLCL;
        public static IsInterpretation Create()
        {
            IsInterpretation c = new IsInterpretation();
            c.XBAR = false;
            c.XRAR = false;
            c.UCL = false;
            c.CCL = false;
            c.LCL = false;

            c.RUCL = false;
            c.RCCL = false;
            c.RLCL = false;
            return c;
        }

    }

    /// <summary>
    /// 공정능력 Option
    /// </summary>
    public class SpcCpkDefinition
    {
        public const int DECIAL_PLACE_DEFAULT_VALUE = 2;
        public const int DECIMAL_PLACE_SIGMA_DEFAULT_VALUE = 6;
        public int decimalPlace;
        public int SigmadecimalPlace;

        public static SpcCpkDefinition Create()
        {
            return new SpcCpkDefinition
            {
                decimalPlace = DECIAL_PLACE_DEFAULT_VALUE,
                SigmadecimalPlace = DECIMAL_PLACE_SIGMA_DEFAULT_VALUE
            };
        }
    }

    #region Spc Chart Option 기본 속성

    /// <summary>
    /// Spc Chart Option 기본 속성
    /// </summary>
    public static class SpcViewOption
    {
        public static SpcChartOptionAttribute att;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int AttDefaultSetting()
        {
            if (att != null)
            {
                return 3;
            }

            att = SpcChartOptionAttribute.Create();
            //att.sigma1.constantLine.ForeColor = Color.FromArgb(250, 192, 143);
            att.sigma1.constantLine.ForeColor = Color.FromArgb(255, 128, 0);

            att.sigma1.checkBox.ForeColor = att.sigma1.constantLine.ForeColor;
            att.sigma1.textBox.ForeColor = Color.Black;

            //att.sigma2.constantLine.ForeColor = Color.FromArgb(195, 214, 155);
            att.sigma2.constantLine.ForeColor = Color.FromArgb(0, 192, 0);
            att.sigma2.checkBox.ForeColor = att.sigma2.constantLine.ForeColor;
            att.sigma2.textBox.ForeColor = Color.Black;

            //att.sigma3.constantLine.ForeColor = Color.FromArgb(141, 179, 226);
            att.sigma3.constantLine.ForeColor = Color.FromArgb(102, 98, 251);
            att.sigma3.checkBox.ForeColor = att.sigma3.constantLine.ForeColor;
            att.sigma3.textBox.ForeColor = Color.Black;
            //att.sigma1.ForeColor
            return 0;
        }
    }

    /// <summary>
    /// Spc Chart Option 속성 정의
    /// </summary>
    public class SpcChartOptionAttribute
    {
        public SpcChartControlOption xD;
        public SpcChartControlOption xBar;
        public SpcChartControlOption xR;
        public SpcChartControlOption xS;
        public SpcChartControlOption xP;
        public SpcChartControlOption usl;
        public SpcChartControlOption csl;
        public SpcChartControlOption lsl;
        public SpcChartControlOption ucl;
        public SpcChartControlOption ccl;
        public SpcChartControlOption lcl;

        public SpcChartControlOption uol;
        public SpcChartControlOption lol;

        public SpcChartControlOption sigma1;
        public SpcChartControlOption sigma2;
        public SpcChartControlOption sigma3;

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static SpcChartOptionAttribute Create()
        {
            return new SpcChartOptionAttribute
            {
                xD = SpcChartControlOption.Create(),
                xBar = SpcChartControlOption.Create(),
                xR = SpcChartControlOption.Create(),
                xS = SpcChartControlOption.Create(),
                xP = SpcChartControlOption.Create(),
                usl = SpcChartControlOption.Create(),
                csl = SpcChartControlOption.Create(),
                lsl = SpcChartControlOption.Create(),
                ucl = SpcChartControlOption.Create(),
                ccl = SpcChartControlOption.Create(),
                lcl = SpcChartControlOption.Create(),
                uol = SpcChartControlOption.Create(),
                lol = SpcChartControlOption.Create(),
                sigma1 = SpcChartControlOption.Create(),
                sigma2 = SpcChartControlOption.Create(),
                sigma3 = SpcChartControlOption.Create()
            };
        }
    }

    /// <summary>
    /// SPC Option 속성 정의.
    /// </summary>
    public class SpcChartControlOption
    {
        public SpcControlAttribute checkBox;
        public SpcControlAttribute textBox;
        public SpcControlAttribute label;
        public SpcControlAttribute constantLine;
        public SpcControlAttribute seriesLine;
        public static SpcChartControlOption Create()
        {
            return new SpcChartControlOption
            {
                checkBox = new SpcControlAttribute(),
                textBox = new SpcControlAttribute(),
                label = new SpcControlAttribute(),
                constantLine = new SpcControlAttribute(),
                seriesLine = new SpcControlAttribute()
            };
        }
    }

    /// <summary>
    /// SPC Option 속성 정의.
    /// </summary>
    public class SpcControlAttribute
    {
        public string TextDefault;
        public string CaptionDefault;
        public Color ForeColor;
    }

    #endregion

    /// <summary>
    /// SPC 내에서 사용하는 기호 정의.
    /// </summary>
    public static class SpcSym
    {
        /// <summary>
        /// subgroup 항목별 구분 기호
        /// </summary>
        public const string subgroupSep11 = "@#";
        public const string subgroupSep12 = "/ ";
    }

    /// <summary>
    /// Histogram 분석 집계 구분
    /// </summary>
    public enum HistogramTotalize
    {
        None = 0,
        Bar = 1,
        Within = 2,
        Total = 3
    }

    /// <summary>
    /// 직접입력 WholeRange 설정.
    /// </summary>
    public class ChartWholeRangeDirectValue
    {
        public bool isWholeRange;
        public double? WholeRangeP1Max;
        public double? WholeRangeP1Min;
        public double? WholeRangeP2Max;
        public double? WholeRangeP2Min;
        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static ChartWholeRangeDirectValue Create()
        {
            ChartWholeRangeDirectValue c = new ChartWholeRangeDirectValue();
            c.isWholeRange = false;
            c.WholeRangeP1Max = null;
            c.WholeRangeP1Min = null;
            c.WholeRangeP2Max = null;
            c.WholeRangeP2Min = null;
            return c;
        }

    }

    /// <summary>
    /// 공정능력 판정 Type
    /// </summary>
    public enum JudgmentType
    {
             Terrific = 1
            ,Excellent = 2
            ,Good = 3
            ,Fair = 4
            ,Poor = 5
            ,Terrible = 6
    }
    /// <summary>
    /// 분석용 Chart 구분 - 분석 Chart Type
    /// </summary>
    public enum AnalysisChartType
    {
          None = 0              //미선택
        , AnalysisPolt = 1      //비교분석 Polt
        , AnalysisLine = 2      //비교분석 Line
        , BoxPlot =3            //Box Plot
        , ThreePointDiagram =4  //산점도
        , TimeSeries = 5        //시계열도
    }

    /// <summary>
    /// 불량율 값 구분
    /// </summary>
    public enum DefectValueType
    {
          none = 0              //미선택
        , value = 1             //불량수량
        , rate = 2              //불량율
        , valueAndRate = 3      //부량수량(불량율)
    }

    /// <summary>
    /// Spec 구분
    /// </summary>
    public enum SpcSpecType
    {
          LSL = 0               //하한
        , USL=1                 //상한
        , Target=2              //목표값
    }

    /// <summary>
    /// PPM 성능별 구분
    /// </summary>
    public enum SpcPpmMode
    {
          Within = 0               //군내
        , Total = 1                //상한
        , Observe = 2              //관측
    }



}
