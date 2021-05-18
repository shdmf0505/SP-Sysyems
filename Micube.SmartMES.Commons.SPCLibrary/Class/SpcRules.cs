#region using

#endregion

namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// SPC Chart Type별 Spec 항목 정의
    /// </summary>
    public class SpcRules
    {
        public double nValue = double.NaN;

        /// <summary>
        /// 기본 Chart Type
        /// </summary>
        public SpcChartType defaultChartType;

        /// <summary>
        /// XBar R Chart Spec
        /// </summary>
        public SpcSpec xbarr = SpcSpec.Create();
        /// <summary>
        /// XBar S Chart Spec
        /// </summary>
        public SpcSpec xbars = SpcSpec.Create();

        /// <summary>
        /// R Chart Spec
        /// </summary>
        public SpcSpec r = SpcSpec.Create();

        /// <summary>
        /// S Chart Spec
        /// </summary>
        public SpcSpec s = SpcSpec.Create();

        /// <summary>
        /// 합동 Chart Spec
        /// </summary>
        public SpcSpec po = SpcSpec.Create();

        /// <summary>
        /// NP Chart Spec
        /// </summary>
        public SpcSpec np = SpcSpec.Create();

        /// <summary>
        /// P Chart Spec
        /// </summary>
        public SpcSpec p = SpcSpec.Create();

        /// <summary>
        /// C Chart Spec
        /// </summary>
        public SpcSpec c = SpcSpec.Create();

        /// <summary>
        /// U Chart Spec
        /// </summary>
        public SpcSpec u = SpcSpec.Create();
    }
}
