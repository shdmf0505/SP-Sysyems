#region using

#endregion

namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// SPC Rules over 자료 Check값 저장.
    /// </summary>
    public class SpcRulesOver
    {
        public bool isResult;
        public bool isSpec;
        public bool isControlLimit;
        public bool isOutlier;
        public bool isUOL;
        public bool isLOL;
        public bool isUSL;
        public bool isCSL;
        public bool isLSL;
        public bool isUCL;
        public bool isCCL;
        public bool isLCL;
        public ReturnMessage message;
        public SpcRules rulesParameter;

        /// <summary>
        /// 객체 초기화 
        /// </summary>
        /// <returns></returns>
        public static SpcRulesOver Create()
        {
            return new SpcRulesOver
            {
                  isResult = false
                , isSpec = false
                , isControlLimit = false
                , isOutlier = false
                , isUOL = false
                , isLOL = false
                , isUSL = false
                , isCSL = false
                , isLSL = false
                , isUCL = false
                , isCCL = false
                , isLCL = false
                , message = new ReturnMessage()
                , rulesParameter = new SpcRules()
            };            
        }
    }

    /// <summary>
    /// Message 저장
    /// </summary>
    public class ReturnMessage
    {
        public string code;
        public string value;
        public string line1;
        public string line2;
        public string line3;
    }
}
