#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 품질 비용분석 > 비정상비용 집계현황
    /// 업  무  설  명  : 비정상비용에 대한 집계현황을 보여준다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-09-04
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class AbnormalCostTotalStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public AbnormalCostTotalStatus()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            InitializeGrid();
            InitializationSummaryRow();
        }

        /// <summary>        
        /// 비정상비용 집계현황 그리드
        /// </summary>
        private void InitializeGrid()
        {
            grdAbnormalCostTotalStatus.GridButtonItem = GridButtonItem.Export;
            grdAbnormalCostTotalStatus.View.SetIsReadOnly();

            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("SENDTIME", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("TAKEOVERDATE"); // 인계일시
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("PLANTID", 100)
                .SetLabel("Site"); // 발생 Site
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("LOTTYPE", 100)
                .SetTextAlignment(TextAlignment.Center); // 양산구분
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("PRODUCTDEFID", 150); // 품목코드
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("PRODUCTDEFNAME", 250); // 품목명
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Lot No"); // Lot No
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("PROCESSSEGMENTTYPE", 100)
                .SetTextAlignment(TextAlignment.Center); // 공정유형
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("VENDORNAME", 150)
                .SetLabel("VENDOR"); // 업체명
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("AREANAME", 150)
                .SetLabel("AREA"); // 작업장명
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetLabel("PROCESSSEGMENT"); // 공정명
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("SENDPCSQTY", 100)
                .SetLabel("WORKQTY"); // 작업수량
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("PCSPRICE", 100); // PCS단가
            grdAbnormalCostTotalStatus.View.AddTextBoxColumn("OCCURPRICE", 100); // 발생비용

            grdAbnormalCostTotalStatus.View.PopulateColumns();

            grdAbnormalCostTotalStatus.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        // 그리드 합계 초기화
        private void InitializationSummaryRow()
        {
            grdAbnormalCostTotalStatus.View.Columns["PROCESSSEGMENTNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdAbnormalCostTotalStatus.View.Columns["PROCESSSEGMENTNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdAbnormalCostTotalStatus.View.Columns["SENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdAbnormalCostTotalStatus.View.Columns["SENDPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdAbnormalCostTotalStatus.View.Columns["PCSPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdAbnormalCostTotalStatus.View.Columns["PCSPRICE"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdAbnormalCostTotalStatus.View.Columns["OCCURPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdAbnormalCostTotalStatus.View.Columns["OCCURPRICE"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdAbnormalCostTotalStatus.View.OptionsView.ShowFooter = true;
            grdAbnormalCostTotalStatus.ShowStatusBar = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {

        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //DataTable changed = grdWorkDefectPriceStatus.GetChangedRows();

            //ExecuteRule("SaveInspectionGrade", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("p_languageType", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("GetAbnormalCostTotalStatus", "10001", values);

            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                this.ShowMessage("NoSelectData");
                grdAbnormalCostTotalStatus.DataSource = null;
                return;
            }

            grdAbnormalCostTotalStatus.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeConditionPopup_Product();
            InitializeConditionPopup_Vendor();
            InitializeConditionPopup_Area();
            InitializeConditionPopup_processSegment();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 800)
               .SetLabel("PRODUCT")
               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(3.1);

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetValidationKeyColumn();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100)
                .SetIsHidden();
        }

        /// <summary>
        /// 업체 조회조건
        /// </summary>
        private void InitializeConditionPopup_Vendor()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("p_vendorId", new SqlQuery("GetVendorListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "VENDORNAME", "VENDORID")
               .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("VENDOR")
               .SetPopupResultCount(1)
               .SetPosition(5.1);

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("VENDORIDNAME");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("VENDORID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
        }

        /// <summary>
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Area()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("AREA")
               .SetPopupResultCount(1)
               .SetPosition(5.2);

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("AREAIDNAME");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        /// <summary>
        /// 공정 조회조건
        /// </summary>
        private void InitializeConditionPopup_processSegment()
        {
            // 팝업 컬럼설정
            var segmentPopup = Conditions.AddSelectPopup("p_processsegmentId", new SqlQuery("GetSmallProcesssegmentListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
               .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("PROCESSSEGMENT")
               .SetPopupResultCount(1)
               .SetPosition(5.3);

            // 팝업 조회조건
            segmentPopup.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");

            // 팝업 그리드
            segmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            segmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            //grdWorkDefectPriceStatus.View.CheckValidation();

            //DataTable changed = grdWorkDefectPriceStatus.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        #endregion
    }
}
