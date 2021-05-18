using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// SPC 
/// </summary>
namespace Micube.SmartMES.SPC
{
    /// <summary>
    /// Spc 화면 표시 설정 값 정의.
    /// </summary>
    public static class SpcDefVal
    {
        public static SpcDisplayOption disOpt = SpcDisplayOption.Create();
    }


    /// <summary>
    /// 화면 표시 설정 Option
    /// </summary>
    public class SpcDisplayOption
    {
        public SpcDisplayDecimalpoint decPoint;
        //초기화 
        public static SpcDisplayOption Create()
        {
            SpcDisplayOption c = new SpcDisplayOption();
            c.decPoint = SpcDisplayDecimalpoint.Create();
            return c;
        }
    }
    /// <summary>
    /// 소수점 자리수 설정.
    /// </summary>
    public class SpcDisplayDecimalpoint
    {
        public int Cpk;
        public int Sigma;
        public int Spec;
        public int Control;
        public int Uot;
        public int RControl;

        /// <summary>
        /// 초기화 - Spec 소수점 기본값.
        /// </summary>
        /// <returns></returns>
        public static SpcDisplayDecimalpoint Create()
        {
            SpcDisplayDecimalpoint c = new SpcDisplayDecimalpoint();
            c.Cpk = 2;
            c.Sigma = 6;
            c.Spec = 3;
            c.Control = 3;
            c.RControl = 3;
            c.Uot=3;
            return c;
        }
    }

    /// <summary>
    /// Chart 이미지 종류
    /// </summary>
    public enum ChartImageFormat
    {
        Png = 1,
        Gif = 2,
        Jpeg = 3,
        Bmp = 4

    }

}
