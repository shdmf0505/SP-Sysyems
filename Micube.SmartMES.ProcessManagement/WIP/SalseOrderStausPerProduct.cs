#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;

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

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 투입관리 > 모델별 수주 진척 현황
    /// 업  무  설  명  : 모델별 수주 진척 현황을 조회한다.
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-10-26
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SalseOrderStausPerProduct : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public SalseOrderStausPerProduct()
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

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 수주 리스트 그리드를 초기화 한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdSalesOrderStatus.GridButtonItem = GridButtonItem.None;
            grdSalesOrderStatus.ShowButtonBar = false;
            grdSalesOrderStatus.ShowStatusBar = false;
            grdSalesOrderStatus.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdSalesOrderStatus.View.SetIsReadOnly();
            grdSalesOrderStatus.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center);
            grdSalesOrderStatus.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            grdSalesOrderStatus.View.AddTextBoxColumn("PRODUCTIONORDERID", 100).SetTextAlignment(TextAlignment.Center);
            grdSalesOrderStatus.View.AddTextBoxColumn("LINENO", 80).SetTextAlignment(TextAlignment.Center);
            grdSalesOrderStatus.View.AddTextBoxColumn("RETURNWIP", 100).SetDisplayFormat("#,##0");
            grdSalesOrderStatus.View.AddTextBoxColumn("PRODUCTIONNOTPACKED", 100).SetDisplayFormat("#,##0");
            grdSalesOrderStatus.View.AddTextBoxColumn("NOTINPUTPCSQTY", 100).SetDisplayFormat("#,##0");
            grdSalesOrderStatus.View.AddTextBoxColumn("BEFORESHIP", 100).SetDisplayFormat("#,##0");
            grdSalesOrderStatus.View.AddTextBoxColumn("AFTERSHIP", 100).SetDisplayFormat("#,##0");
            grdSalesOrderStatus.View.AddTextBoxColumn("HOLD", 100).SetDisplayFormat("#,##0");
            grdSalesOrderStatus.View.AddTextBoxColumn("DEFECT", 100).SetDisplayFormat("#,##0");
            grdSalesOrderStatus.View.AddTextBoxColumn("PCSPNL", 100).SetDisplayFormat("#,##0");
            grdSalesOrderStatus.View.AddTextBoxColumn("PCSMM", 100).SetDisplayFormat("#,##0");
            grdSalesOrderStatus.View.AddTextBoxColumn("YIELD2MONTH", 100).SetDisplayFormat("P2");
            grdSalesOrderStatus.View.AddTextBoxColumn("YIELD1MONTH", 100).SetDisplayFormat("P2");
            grdSalesOrderStatus.View.AddTextBoxColumn("YIELD2WEEK", 100).SetDisplayFormat("P2");
            grdSalesOrderStatus.View.PopulateColumns();
            grdSalesOrderStatus.View.OptionsView.ShowIndicator = false;
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
        }

        #endregion

        #region 툴바

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            DataTable dtProductionOrder = await QueryAsync("SelectSalseOrderStausPerProduct", "10001", values);

            grdSalesOrderStatus.DataSource = dtProductionOrder;

            // 검색 결과에 데이터가 없는 경우
            if (dtProductionOrder.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            InitializeCondition_ProductPopup();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(0.5)
                .SetPopupResultCount(0)
                .SetPopupApplySelection((selectRow, gridRow) => {
                    string productDefName = string.Join(",", selectRow.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFNAME")).ToArray());
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
                });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetDefault("Product");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion
    }
}
