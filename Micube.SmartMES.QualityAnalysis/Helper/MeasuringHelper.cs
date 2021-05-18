using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micube.SmartMES.QualityAnalysis.Helper
{
    /// <summary>
    /// 프 로 그 램 명  : 계측기 관리
    /// 업  무  설  명  : 계측기 관련 공통 함수 정의
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-11-29
    /// 수  정  이  력  : 
    /// 2019-11-29 - 이승우 최초 작성.
    /// 
    /// </summary>
    public static class MeasuringHelper
    {
        public static MeasuringSeverPath mServerPath = MeasuringSeverPath.Create();

    }

    /// <summary>
    /// 계측기 파일 및 Image Server Directory
    /// </summary>
    public class MeasuringSeverPath
    {
        public string filesReport;
        public string image;

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static MeasuringSeverPath Create()
        {
            MeasuringSeverPath c = new MeasuringSeverPath();
            c.filesReport = "Measuring/Report";
            c.image = "Measuring/Image";
            return c;
        }
    }


}
