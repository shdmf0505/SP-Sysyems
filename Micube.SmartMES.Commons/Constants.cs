using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micube.SmartMES.Commons
{
    public static class Constants
    {
        /// <summary>
        /// 인수 대기
        /// </summary>
        public const string WaitForReceive = "WaitForReceive";
        /// <summary>
        /// 인수
        /// </summary>
        public const string Wait = "Wait";
        /// <summary>
        /// 작업
        /// </summary>
        public const string Run = "Run";
        /// <summary>
        /// 인계 대기
        /// </summary>
        public const string WaitForSend = "WaitForSend";
        /// <summary>
        /// 일반 Lot Card 위치
        /// </summary>
        public const string NormaLotCardPath = "Micube.SmartMES.Commons.Report.LotCardProduction.repx";
        /// <summary>
        /// 일반 Lot Card 위치(영풍)
        /// </summary>
        public const string NormaLotCardPath_YP = "Micube.SmartMES.Commons.Report.LotCardProduction_YP.repx";
        /// <summary>
        /// 일반 Lot Card 위치(영풍) Ver2
        /// </summary>
        public const string LotCardPath_YP = "Micube.SmartMES.Commons.Report.LotCardProduction_YP_ver2.repx";
        /// <summary>
        /// 재작업
        /// </summary>
        public const string ReworkLotCardPath = "Micube.SmartMES.Commons.Report.LotCardProduction_Rework.repx";
        /// <summary>
        /// 영풍
        /// </summary>
        public const string EnterPrise_YoungPoong = "YOUNGPOONG";
        /// <summary>
        /// 인터플렉스
        /// </summary>
        public const string EnterPrise_InterFlex = "INTERFLEX";
        /// <summary>
        /// 엑셀 connect string
        /// </summary>
        public const string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        public const string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

        /// <summary>
        /// 법인
        /// </summary>
        public const string IFC = "IFC";
        public const string IFV = "IFV";
        public const string CCT = "CCT";
        public const string YPE = "YPE";
        public const string YPEV = "YPEV";

        public const string SDCBoxAddList = "ROHS,HF,BOXSEQ,EXPIREDDATE,LOTNO";
        public const string SDCUtypeCaseAddList = "ROHS,HF,EXPIREDDATE,ULOT,LOTNO,BOXSEQ";
    }
}
