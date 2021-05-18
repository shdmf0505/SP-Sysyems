using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System.Data;
using System.Threading.Tasks;

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 생산실적 > 원가기준 투입/출고 조회
    /// 업  무  설  명  : 모델별 재공 조회
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2020-01-04
    /// 수  정  이  력  : 
    /// </summary>
    public partial class InOutLotRecord : SmartConditionManualBaseForm
    {
        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public InOutLotRecord()
        {
            InitializeComponent();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeEvent();
            InitializeGrid();
            InitializeSummaryRow();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }
		#endregion

		#region ▶ ComboBox 초기화 |
		/// <summary>
		/// 콤보박스를 초기화 하는 함수
		/// </summary>
		private void InitializeComboBox()
        {
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 투입 그리드
            grdInput.GridButtonItem = GridButtonItem.Export;
            grdInput.View.SetIsReadOnly();
            grdInput.SetIsUseContextMenu(false);
            grdInput.View.AddTextBoxColumn("INPUTTIME", 168).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss"); // 인계시간
            grdInput.View.AddTextBoxColumn("ROOTLOTID", 228).SetTextAlignment(TextAlignment.Center); // root lot id
            grdInput.View.AddTextBoxColumn("LOTID", 228).SetTextAlignment(TextAlignment.Center); // lot id
            grdInput.View.AddTextBoxColumn("PRODUCTIONTYPE", 96).SetTextAlignment(TextAlignment.Center); // 생산구분
            grdInput.View.AddTextBoxColumn("PRODUCTTYPE", 96).SetTextAlignment(TextAlignment.Center); // 제품Type
            grdInput.View.AddTextBoxColumn("LAYER", 84).SetTextAlignment(TextAlignment.Center); // 층수
            grdInput.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center); // 품목코드
            grdInput.View.AddTextBoxColumn("PRODUCTDEFVERSION", 84).SetTextAlignment(TextAlignment.Center); // 품목버전
            grdInput.View.AddTextBoxColumn("PRODUCTDEFNAME", 252); // 품목명
            grdInput.View.AddSpinEditColumn("INPUTPCSQTY", 96).SetLabel("PCSQTY"); // 출고수량(pcs)
            grdInput.View.AddSpinEditColumn("INPUTM2QTY", 96).SetLabel("OSPMM").SetDisplayFormat("#,##0.0000"); // 출고수량(mm)
            grdInput.View.AddTextBoxColumn("PRODUCTIONREQUESTNO", 132).SetTextAlignment(TextAlignment.Center); // 생산의뢰번호
            grdInput.View.AddTextBoxColumn("ISSMT", 96).SetTextAlignment(TextAlignment.Center); // SMT 여부
            grdInput.View.AddTextBoxColumn("ITEMACCOUNT", 84).SetTextAlignment(TextAlignment.Center); // 품목계정
            grdInput.View.AddTextBoxColumn("CONTRACTOR", 144); // 수주처
            grdInput.View.AddTextBoxColumn("SHIPTO", 144); // 납품처
            grdInput.View.AddSpinEditColumn("INPUTPRICE", 140).SetDisplayFormat("#,##0"); // 투입금액
            grdInput.View.AddSpinEditColumn("BAREPRICE", 140).SetDisplayFormat("#,##0");  // BARE금액
            grdInput.View.AddSpinEditColumn("SMTPRICE", 140).SetDisplayFormat("#,##0");   // SMT금액
            grdInput.View.PopulateColumns();
            #endregion

            #region 출고 그리드
            grdShipping.GridButtonItem = GridButtonItem.Export;
            grdShipping.View.SetIsReadOnly();
            grdShipping.SetIsUseContextMenu(false);
            grdShipping.View.AddTextBoxColumn("SENDTIME", 168).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss"); // 인계시간
            grdShipping.View.AddTextBoxColumn("ROOTLOTID", 228).SetTextAlignment(TextAlignment.Center); // root lot id
            grdShipping.View.AddTextBoxColumn("LOTID", 228).SetTextAlignment(TextAlignment.Center); // lot id
            grdShipping.View.AddTextBoxColumn("PRODUCTIONTYPE", 96).SetTextAlignment(TextAlignment.Center); // 생산구분
            grdShipping.View.AddTextBoxColumn("PRODUCTTYPE", 96).SetTextAlignment(TextAlignment.Center); // 제품Type
            grdShipping.View.AddTextBoxColumn("LAYER", 84).SetTextAlignment(TextAlignment.Center); // 층수
            grdShipping.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center); // 품목코드
            grdShipping.View.AddTextBoxColumn("PRODUCTDEFVERSION", 84).SetTextAlignment(TextAlignment.Center); // 품목버전
            grdShipping.View.AddTextBoxColumn("PRODUCTDEFNAME", 252); // 품목명
            grdShipping.View.AddSpinEditColumn("SENDPCSQTY", 96).SetLabel("PCSQTY"); // 출고수량(pcs)
            grdShipping.View.AddSpinEditColumn("SENDM2QTY", 96).SetLabel("OSPMM").SetDisplayFormat("#,##0.0000"); // 출고수량(mm)
            grdShipping.View.AddTextBoxColumn("PRODUCTIONREQUESTNO", 132).SetTextAlignment(TextAlignment.Center); // 생산의뢰번호
            grdShipping.View.AddTextBoxColumn("ISSMT", 96).SetTextAlignment(TextAlignment.Center); // SMT 여부
            grdShipping.View.AddTextBoxColumn("ITEMACCOUNT", 84).SetTextAlignment(TextAlignment.Center); // 품목계정
            grdShipping.View.AddTextBoxColumn("CONTRACTOR", 144); // 수주처
            grdShipping.View.AddTextBoxColumn("SHIPTO", 144); // 납품처
            grdShipping.View.AddSpinEditColumn("RESULTPRICE", 140).SetDisplayFormat("#,##0"); // 투입금액
            grdShipping.View.AddSpinEditColumn("BAREPRICE", 140).SetDisplayFormat("#,##0");  // BARE금액
            grdShipping.View.AddSpinEditColumn("SMTPRICE", 140).SetDisplayFormat("#,##0");   // SMT금액
            grdShipping.View.PopulateColumns();
            #endregion
        }

        private void InitializeSummaryRow()
        {
            grdInput.View.Columns["INPUTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInput.View.Columns["INPUTPCSQTY"].SummaryItem.DisplayFormat = "{0:#,###}";
            grdInput.View.Columns["INPUTM2QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInput.View.Columns["INPUTM2QTY"].SummaryItem.DisplayFormat = "{0:#,##0.00}";
            grdInput.View.OptionsView.ShowFooter = true;
            grdInput.ShowStatusBar = false;

            grdShipping.View.Columns["SENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdShipping.View.Columns["SENDPCSQTY"].SummaryItem.DisplayFormat = "{0:#,###}";
            grdShipping.View.Columns["SENDM2QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdShipping.View.Columns["SENDM2QTY"].SummaryItem.DisplayFormat = "{0:#,##0.00}";
            grdShipping.View.OptionsView.ShowFooter = true;
            grdShipping.ShowStatusBar = false;
        }
        #endregion

        #endregion

        #region ◆ Event |
        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
        }
        #endregion

        #region ◆ 툴바 |
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
        }
        #endregion

        #region ◆ 검색 |
        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            if (tabControl.SelectedTabPage.Name == "tbInput")
            {
                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dtInput = await SqlExecuter.QueryAsync("SelectInputLotList", "10001", values);
                if (dtInput.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                grdInput.DataSource = dtInput;
            }
            else if (tabControl.SelectedTabPage.Name == "tbShipping")
            {
                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dtShipping = await SqlExecuter.QueryAsync("SelectShipLotList", "10001", values);
                if (dtShipping.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                grdShipping.DataSource = dtShipping;
            }
        }
        #endregion

        #region ◆ 유효성 검사 |
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }
        #endregion

        #region ◆ Private Function |
        #endregion
    }
}
