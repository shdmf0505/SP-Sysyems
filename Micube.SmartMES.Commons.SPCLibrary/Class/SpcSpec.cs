#region using

#endregion

using System;
/// <summary>
/// SPC 통계 분석용 - Spec 구조. 
/// </summary>
namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// 프 로 그 램 명  : SPC Library - Spec 구조 Class
    /// 업  무  설  명  : SPC 통계에서 사용되는 열거형 변수 선언.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-17
    /// 수  정  이  력  : 
    /// 2019-07-17  ControlSpec 클래스 수정 - Sigma 변수 추가.
    /// 2019-07-11  SpcNumericalInitial 추가, SpcLimit Class형으로 변경.
    /// 2019-07-08  최초작성.
    /// 
    /// </summary>

    /// <summary>
    /// SPEC 항목 정의
    /// </summary>
    public class SpcSpec
    {
        public double value;
        public double uol;
        public double lol;
        public double usl;
        public double csl;
        public double lsl;
        public double ucl;
        public double ccl;
        public double lcl;
        /// <summary>
        /// R, S, PL 관리선 UCL
        /// </summary>
        public double rUcl;
        /// <summary>
        /// R, S, PL 관리선 LCL
        /// </summary>
        public double rLcl;


        /// <summary>
        /// 중심치 이탈 상한
        /// </summary>
        public double taUsl;
        /// <summary>
        /// 목표값
        /// </summary>
        public double taCsl;
        /// <summary>
        /// 중심치 이탈 하한
        /// </summary>
        public double taLsl;

        /// <summary>
        /// Default XBar 
        /// </summary>
        public double XDBar;
        /// <summary>
        /// Default XRar
        /// </summary>
        public double XDRar;
        public double XBar;
        public double XRar;
        public double XSar;
        public double XMr;
        public double BarMax;
        public double BarMin;
        public double RMax;
        public double RMin;


        /// <summary>
        /// 통계형 초기화
        /// </summary>
        /// <returns></returns>
        public static SpcSpec Create()
        {
            return new SpcSpec
            {
                value = double.NaN,
                uol = SpcLimit.MAX,
                lol = SpcLimit.MIN,

                usl = SpcLimit.MAX,
                csl = double.NaN,
                lsl = SpcLimit.MIN,

                ucl = SpcLimit.MAX,
                ccl = double.NaN,
                lcl = SpcLimit.MIN,

                rUcl = SpcLimit.MIN,
                rLcl = SpcLimit.MIN,

                taUsl = SpcLimit.MAX,
                taCsl = double.NaN,
                taLsl = SpcLimit.MIN,

                XDBar = SpcLimit.MAX,
                XDRar = SpcLimit.MAX,
                XBar = SpcLimit.MAX,
                XRar = SpcLimit.MAX,
                XSar = SpcLimit.MAX,
                XMr = SpcLimit.MAX,

                BarMax = SpcLimit.MAX,
                BarMin = SpcLimit.MIN,
                RMax = SpcLimit.MAX,
                RMin = SpcLimit.MIN
            };
        }


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public SpcSpec CopyDeep()
        {
            SpcSpec cs = SpcSpec.Create();
            cs.value = this.value;
            cs.uol = this.uol;
            cs.lol = this.lol;
            cs.usl = this.usl;
            cs.csl = this.csl;
            cs.lsl = this.lsl;
            cs.ucl = this.ucl;
            cs.ccl = this.ccl;
            cs.lcl = this.lcl;
            cs.rUcl = this.rUcl;
            cs.rLcl = this.rLcl;
            cs.taUsl = this.taUsl;
            cs.taCsl = this.taCsl;
            cs.taLsl = this.taLsl;
            cs.XDBar = this.XDBar;
            cs.XDRar = this.XDRar;
            cs.XBar = this.XBar;
            cs.XRar = this.XRar;
            cs.XSar = this.XSar;
            cs.XMr = this.XMr;
            cs.BarMax = this.BarMax;
            cs.BarMin = this.BarMin;
            cs.RMax = this.RMax;
            cs.RMin = this.RMin;

            return cs;
        }

        /// <summary>
        /// NaN 값으로 초기화
        /// </summary>
        /// <returns></returns>
        public static SpcSpec CreateNaN()
        {
            return new SpcSpec
            {
                value = double.NaN,
                uol = double.NaN,
                lol = double.NaN,
                usl = double.NaN,
                csl = double.NaN,
                lsl = double.NaN,
                ucl = double.NaN,
                ccl = double.NaN,
                lcl = double.NaN
            };
        }

        /// <summary>
        /// 0.0 값으로 초기화
        /// </summary>
        /// <returns></returns>
        public static SpcSpec CreateZero()
        {
            return new SpcSpec
            {
                value = 0.0f,
                uol = 0.0f,
                lol = 0.0f,
                usl = 0.0f,
                csl = 0.0f,
                lsl = 0.0f,
                ucl = 0.0f,
                ccl = 0.0f,
                lcl = 0.0f
            };
        }
    }

    /// <summary>
    /// 관리도 Spec 정의
    /// </summary>
    public class ControlSpec
    {
        /// <summary>
        /// 화면 표시 Chart index
        /// </summary>
        public int chartIndex;
        /// <summary>
        /// SPC 통계 - 관리도 알고리즘 분석 Option
        /// </summary>
        public SPCOption spcOption;
        /// <summary>
        /// Spec 항목 기본값
        /// </summary>
        public SpcSpec nDefault;
        /// <summary>
        /// Spec 항목 Xbar
        /// </summary>
        public SpcSpec nXbar;
        /// <summary>
        /// Spec 항목 R
        /// </summary>
        public SpcSpec nR;
        /// <summary>
        /// Spec 항목 S
        /// </summary>
        public SpcSpec nS;
        /// <summary>
        /// Spec 항목 P
        /// </summary>
        public SpcSpec nP;
        /// <summary>
        /// Sigma 결과
        /// </summary>
        public SpcSigmaResult sigmaResult;

        /// <summary>
        /// 통계형 초기화
        /// </summary>
        /// <returns></returns>
        public static ControlSpec Create()
        {
            return new ControlSpec
            {
                spcOption = SPCOption.Create(),
                nDefault = SpcSpec.Create(),
                nXbar = SpcSpec.Create(),
                nR = SpcSpec.Create(),
                nS = SpcSpec.Create(),
                nP = SpcSpec.Create(),
                sigmaResult = SpcSigmaResult.Create()
            };
        }

        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public ControlSpec CopyDeep()
        {
            ControlSpec cs = ControlSpec.Create();
            cs.chartIndex = this.chartIndex;
            cs.spcOption = this.spcOption.CopyDeep();
            cs.nDefault = this.nDefault.CopyDeep();

            cs.nXbar = this.nXbar.CopyDeep();
            cs.nR = this.nR.CopyDeep();
            cs.nS = this.nS.CopyDeep();
            cs.nP = this.nP.CopyDeep();

            cs.sigmaResult = this.sigmaResult.CopyDeep();

            return cs;
        }


    }

    /// <summary>
    /// Chart의 Whole 값 
    /// </summary>
    public class ChartWholeValue
    {
        public double editMax;
        public double editMin;
        public double max;
        public double min;
        public static ChartWholeValue Create()
        {
            ChartWholeValue c = new ChartWholeValue();
            c.editMax = SpcLimit.MIN + 0.1;
            c.editMin = SpcLimit.MAX + 0.1;
            c.max = Convert.ToInt64(c.editMax);
            c.min = Convert.ToInt64(c.editMin);
            return c;
        }
    }

    /// <summary>
    /// Spec Cancel flag
    /// </summary>
    public class IsSpecCancel
    {
        public bool value;
        public bool xBar;
        public bool xR;
        public bool Bar;
        public bool R;
        public bool uol;
        public bool lol;
        public bool usl;
        public bool csl;
        public bool lsl;
        public bool ucl;
        public bool ccl;
        public bool lcl;

        public bool taUsl;
        public bool taCsl;
        public bool taLsl;

        public bool cpkBar;
        public bool cpkWithin;
        public bool cpkTotal;
        public static IsSpecCancel Create()
        {
            IsSpecCancel c = new IsSpecCancel();
            Clear(ref c);
            return c;
        }

        /// <summary>
        /// Clear
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static void Clear(ref IsSpecCancel c)
        {
            c.value = false;
            c.xBar = false;
            c.xR = false;
            c.Bar = false;
            c.R = false;
            c.uol = false;
            c.lol = false;
            c.usl = false;
            c.csl = false;
            c.lsl = false;
            c.ucl = false;
            c.ccl = false;
            c.lcl = false;

            c.taUsl = false;
            c.taCsl = false;
            c.taLsl = false;


            c.cpkBar = false;
            c.cpkWithin = false;
            c.cpkTotal = false;
        }
    }

}
