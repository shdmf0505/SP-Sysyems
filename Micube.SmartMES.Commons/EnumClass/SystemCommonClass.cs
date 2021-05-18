#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

#endregion

/// <summary>
/// 시스템에서 공통으로 사용되는 클래스
/// </summary>
namespace Micube.SmartMES.Commons
{
    public static class SystemCommonClass
    {
        public static string GetEnumToString(Enum type)
        {
            return type.ToString();
        }

        public static string ImageTempPath =  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Micube\\SmartMES\\Setting", "temp.bmp");
    }
    
    /// <summary>
    /// ProcessFourStepInfo User Control에서 사용되는 작업 구분
    /// </summary>
    public enum ProcessType
    {
        // 인수 등록
        LotAccept = 0,
        // 작업 시작
        StartWork = 1,
        // 작업 완료
        WorkCompletion = 2,
        // 인계 등록
        TransitRegist = 3
    }

    public enum LotCardType
    {
        // 양산(샘플)
        Normal = 0,
        // 분할
        Split = 1,
        //병합
        Merge = 2,
        //R/C 변경
        RCChange = 3,
        //재작업
        Rework = 4,
        //재검(반품)
        Return = 5
    }
}
