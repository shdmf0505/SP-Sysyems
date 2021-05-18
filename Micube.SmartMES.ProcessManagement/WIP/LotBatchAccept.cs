#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 일괄 인수 등록
    /// 업  무  설  명  : 인수 대기 중인 Lot을 일괄 인수 등록 한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-09-04
    /// 수  정  이  력  :
    ///     2021.05.07 전우성 : 화면 개편 및 코드 정리
    ///
    /// </summary>
    public partial class LotBatchAccept : SmartConditionManualBaseForm
    {
        #region 생성자

        public LotBatchAccept()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeControler();
            InitializeLanguageKey();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            smartLayoutControl1.SetLanguageKey(layoutControlItem2, "WORKMAN");
            smartLayoutControl1.SetLanguageKey(layoutControlItem3, "COMMENT");
            grdMain.LanguageKey = "LIST";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 메인 그리드

            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            //작업장 ID
            grdMain.View.AddTextBoxColumn("AREAID").SetIsHidden();
            //작업장 명
            grdMain.View.AddTextBoxColumn("AREANAME").SetIsHidden();
            // LOT ID
            grdMain.View.AddTextBoxColumn("LOTID", 180);
            //공정수순
            grdMain.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            //공정
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            //공정ID
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            //품목코드
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            //품목명
            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            //UOM(단위)
            grdMain.View.AddTextBoxColumn("UNIT", 65).SetTextAlignment(TextAlignment.Center);
            //수량
            grdMain.View.AddSpinEditColumn("QTY", 65).SetTextAlignment(TextAlignment.Right);
            //Pnl 수량
            grdMain.View.AddSpinEditColumn("PANELQTY", 90).SetTextAlignment(TextAlignment.Right);
            //수주번호
            grdMain.View.AddTextBoxColumn("PRODUCTIONORDERID").SetTextAlignment(TextAlignment.Center).SetIsHidden();
            // 전 공정 인계 시간
            grdMain.View.AddTextBoxColumn("SEGMENTINCOMETIME", 150).SetTextAlignment(TextAlignment.Center).SetLabel("PREVPROCESSSEGMENTSENDLOTTIME");
            // 이전작업장
            grdMain.View.AddTextBoxColumn("PREVAREA", 160);
            // 공정체공
            grdMain.View.AddSpinEditColumn("SEGMENTSTAYINGTIMEH", 100).SetTextAlignment(TextAlignment.Right);

            grdMain.View.PopulateColumns();

            grdMain.View.SetIsReadOnly();
            grdMain.ShowButtonBar = true;

            grdMain.View.FixColumn(new string[] { "LOTID" });

            #endregion 메인 그리드
        }

        /// <summary>
        /// UI Controler 초기화
        /// </summary>
        private void InitializeControler()
        {
            ConditionItemSelectPopup options = new ConditionItemSelectPopup();
            options.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            options.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
            options.Id = "USER";
            options.SearchQuery = new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}");
            options.IsMultiGrid = false;
            options.DisplayFieldName = "USERNAME";
            options.ValueFieldName = "USERID";
            options.LanguageKey = "USER";
            options.Conditions.AddTextBox("USERIDNAME");
            options.GridColumns.AddTextBoxColumn("USERID", 150);
            options.GridColumns.AddTextBoxColumn("USERNAME", 200);
            txtWorker.SelectPopupCondition = options;

            layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;

            txtWorker.SetValue(UserInfo.Current.Id);
            txtWorker.Text = UserInfo.Current.Name;
        }

        #endregion 컨텐츠 영역 초기화

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            if (ShowMessage(MessageBoxButtons.YesNo, DialogResult.No, "InfoSave").Equals(DialogResult.No))
            {
                return;
            }

            base.OnToolbarSaveClick();

            string lotIds = string.Empty;

            foreach (DataRow dr in grdMain.View.GetCheckedRows().Rows)
            {
                lotIds += dr["LOTID"] + ",";
            }

            MessageWorker messageWorker = new MessageWorker("SaveReceiveLot");
            messageWorker.SetBody(new MessageBody()
            {
                { "EnterpriseId", UserInfo.Current.Enterprise },
                { "PlantId", UserInfo.Current.Plant },
                { "Worker", txtWorker.GetValue() },
                { "LotId", lotIds },
                { "DefectQty", 0 },    // 고정값
                { "Comment", txtComment.EditValue },
                { "IsBatch", "Y" }    // 일괄 인수 여부 (고정값)
            });

            messageWorker.Execute();
            ShowMessage("SuccedSave");
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdMain.View.ClearDatas();

            if (await SqlExecuter.QueryAsync("SelectBatchAccept", "10001", values) is DataTable dt)
            {
                if (dt.Rows.Count.Equals(0))
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                }

                grdMain.DataSource = dt;
            }
        }

        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 0, false, Conditions, true, true);

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.5, false, Conditions);
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartComboBox>("P_Hold").EditValue = "N";
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
            grdMain.View.CheckValidation();

            if (grdMain.View.GetCheckedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion 유효성 검사
    }
}