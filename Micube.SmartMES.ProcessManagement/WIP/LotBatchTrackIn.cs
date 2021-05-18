#region using
using DevExpress.Utils;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 일괄 작업시작
    /// 업  무  설  명  : 같은 작업장 같은 품목의 LOT들을 함께 작업시작 시킨다.
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-12-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotBatchTrackIn : SmartConditionManualBaseForm
    {
        #region 생성자
        public LotBatchTrackIn()
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
            InitializeInspectionUser();
            InitializeGrid();

            txtWorker.SetValue(UserInfo.Current.Id);
            txtWorker.Text = UserInfo.Current.Name;
        }

        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 0.1, false, Conditions, true,true);                 // 작업장
            InitializeCondition_ProductPopup();                                                             // 품목
            CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 2.1, false, Conditions);   // 공정
            CommonFunction.AddConditionBriefLotPopup("P_LOTID", 2.2, false, Conditions);                    // LOT No.
            Conditions.GetCondition("P_PROCESSSEGMENTID").IsRequired = true;
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("PRODUCTDEFID")
                .SetPosition(0.2)
                .SetValidationIsRequired()
                .SetPopupResultCount(1)
                .SetPopupApplySelection((selectRow, gridRow) =>
                {

                    List<string> productDefnameList = new List<string>();
                    List<string> productRevisionList = new List<string>();

                    selectRow.AsEnumerable().ForEach(r =>
                    {
                        productDefnameList.Add(Format.GetString(r["PRODUCTDEFNAME"]));
                        productRevisionList.Add(Format.GetString(r["PRODUCTDEFVERSION"]));
                    });

                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Join(",", productDefnameList);
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Join(",", productRevisionList);
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

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").ReadOnly = true;
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").ButtonClick += LotBatchTrackIn_ButtonClick;
        }
        #endregion

        private void InitializeInspectionUser()
        {
            ConditionItemSelectPopup options = new ConditionItemSelectPopup();
            options.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            options.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
            options.ResultCount = 0;
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
        }

        /// <summary>        
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            #region 메인 그리드
            grdLotList.View.SetIsReadOnly();
            grdLotList.ShowButtonBar = false;
            grdLotList.GridButtonItem = GridButtonItem.None;
            //체크 박스 설정
            grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //작업장 ID
            grdLotList.View.AddTextBoxColumn("AREAID", 70)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            //작업장 명
            grdLotList.View.AddTextBoxColumn("AREANAME", 120);
            // LOT ID
            grdLotList.View.AddTextBoxColumn("LOTID", 220)
                .SetTextAlignment(TextAlignment.Center);
            //공정수순
            grdLotList.View.AddTextBoxColumn("USERSEQUENCE", 70)
                .SetTextAlignment(TextAlignment.Center);
            // 공정
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 140);
            //공정ID
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTID")
                .SetIsHidden();
            //품목코드
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 140);
            //품목명
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 270);
            //UOM(단위)
            grdLotList.View.AddTextBoxColumn("UNIT", 65)
                .SetTextAlignment(TextAlignment.Center);
            //수량
            grdLotList.View.AddTextBoxColumn("QTY", 80)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //Pnl 수량
            grdLotList.View.AddTextBoxColumn("PANELQTY", 80)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //수주번호
            grdLotList.View.AddTextBoxColumn("PRODUCTIONORDERID", 95)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            grdLotList.View.AddTextBoxColumn("SEGMENTINCOMETIME", 150)
                .SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            grdLotList.View.PopulateColumns();
            #endregion

            #region 설비 리스트 그리드
            grdEquipmentList.GridButtonItem = GridButtonItem.None;
            grdEquipmentList.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdEquipmentList.View.SetIsReadOnly();
            //체크 박스 설정
            grdEquipmentList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //설비코드
            grdEquipmentList.View.AddTextBoxColumn("EQUIPMENTID", 110);
            //설비명
            grdEquipmentList.View.AddTextBoxColumn("EQUIPMENTNAME", 230);
            //현재 일수설비 및 작업중 설비의 LOT 수
            grdEquipmentList.View.AddTextBoxColumn("LOTCNT", 70)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //설비 상태
            grdEquipmentList.View.AddTextBoxColumn("STATE", 120)
                .SetTextAlignment(TextAlignment.Center);
            grdEquipmentList.View.PopulateColumns();
            #endregion

            #region 특기사항

            grdComment.GridButtonItem = GridButtonItem.None;
            grdComment.ShowButtonBar = false;
            grdComment.ShowStatusBar = false;

            grdComment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdComment.View.SetIsReadOnly();

            // 공정수순ID
            grdComment.View.AddTextBoxColumn("PROCESSPATHID", 150)
                .SetValidationKeyColumn()
                .SetIsHidden();
            // 공정수순
            grdComment.View.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Center);
            // 공정ID
            grdComment.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();
            // 공정명
            grdComment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 특기사항
            grdComment.View.AddTextBoxColumn("DESCRIPTION", 500)
                .SetLabel("REMARKS");
            // 현재공정여부
            grdComment.View.AddTextBoxColumn("ISCURRENTPROCESS", 70)
                .SetIsHidden();

            grdComment.View.PopulateColumns();


            grdComment.View.OptionsView.ShowIndicator = false;
            grdComment.View.OptionsView.AllowCellMerge = true;

            grdComment.View.Columns["PROCESSPATHID"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["USERSEQUENCE"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["PROCESSSEGMENTID"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["PROCESSSEGMENTNAME"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["DESCRIPTION"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["ISCURRENTPROCESS"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            #endregion

            #region 공정 SPEC
            grdProcessSpec.GridButtonItem = GridButtonItem.None;
            grdProcessSpec.ShowButtonBar = false;
            grdProcessSpec.ShowStatusBar = false;

            grdProcessSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProcessSpec.View.SetIsReadOnly();

            // 공정수순ID
            grdProcessSpec.View.AddTextBoxColumn("PROCESSPATHID", 150)
                .SetValidationKeyColumn()
                .SetIsHidden();
            // 공정수순
            grdProcessSpec.View.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Center);
            // 공정ID
            grdProcessSpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();
            // 공정명
            grdProcessSpec.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 항목
            grdProcessSpec.View.AddTextBoxColumn("SPECCLASSNAME", 120)
                .SetLabel("SPECITEM");
            // 하한값
            grdProcessSpec.View.AddSpinEditColumn("LSL", 90)
                .SetDisplayFormat("#,##0.00");
            // 중간값
            grdProcessSpec.View.AddSpinEditColumn("SL", 90)
                .SetDisplayFormat("#,##0.00");
            // 상한값
            grdProcessSpec.View.AddSpinEditColumn("USL", 90)
                .SetDisplayFormat("#,##0.00");
            // 현재공정여부
            grdProcessSpec.View.AddTextBoxColumn("ISCURRENTPROCESS", 70)
                .SetIsHidden();

            grdProcessSpec.View.PopulateColumns();


            grdProcessSpec.View.OptionsView.ShowIndicator = false;
            grdProcessSpec.View.OptionsView.AllowCellMerge = true;

            grdProcessSpec.View.Columns["SPECCLASSNAME"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["LSL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["SL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["USL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["ISCURRENTPROCESS"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            #endregion


        }

        #region Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdLotList.View.FocusedRowChanged += grdLotListView_FocusedRowChanged;
            grdEquipmentList.View.CheckStateChanged += grdEquipmentListView_CheckStateChanged;

            this.Shown += LotBatchTrackIn_Shown;
 
        }



     
        
        private void grdLotListView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchCommentAndSpec(grdLotList.View.GetFocusedDataRow());
        }

        private void SearchCommentAndSpec(DataRow lotRow)
        {
            if (lotRow == null)
            {
                return;
            }

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
          
            string resourceid = lotRow["RESOURCEID"].ToString();
            values.Add("RESOURCEID", resourceid);
            string lotId = lotRow["LOTID"].ToString();
            string processSegmentId = lotRow["PROCESSSEGMENTID"].ToString();


            Dictionary<string, object> commentParam = new Dictionary<string, object>();


            DataTable dtEquipmentList = SqlExecuter.Query("SelectEquipmentListForBatchTrackIn", "10001", values);
            grdEquipmentList.DataSource = dtEquipmentList;


       
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            commentParam.Add("LOTID", lotId);
            commentParam.Add("PROCESSSEGMENTID", processSegmentId);

            grdComment.DataSource = SqlExecuter.Query("SelectCommentByProcess", "10001", commentParam);

            Dictionary<string, object> processSpecParam = new Dictionary<string, object>();
            processSpecParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            processSpecParam.Add("PLANTID", UserInfo.Current.Plant);
            processSpecParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            processSpecParam.Add("LOTID", lotId);
            processSpecParam.Add("PROCESSSEGMENTID", processSegmentId);
            processSpecParam.Add("CONTROLTYPE", "XBARR");
            processSpecParam.Add("SPECCLASSID", "OperationSpec");

            grdProcessSpec.DataSource = SqlExecuter.Query("SelectProcessSpecByProcess", "10001", processSpecParam);
        }

        private void grdEquipmentListView_CheckStateChanged(object sender, EventArgs e)
        {
            // 한 행만 체크 가능
            grdEquipmentList.View.CheckStateChanged -= grdEquipmentListView_CheckStateChanged;
            grdEquipmentList.View.CheckedAll(false);

            if (grdEquipmentList.View.GetFocusedRow() != null)
            {
                grdEquipmentList.View.CheckRow(grdEquipmentList.View.FocusedRowHandle, true);
            }

            grdEquipmentList.View.CheckStateChanged += grdEquipmentListView_CheckStateChanged;
        }

        private void LotBatchTrackIn_Shown(object sender, EventArgs e)
        {
            smartSpliterContainer1.SplitterPosition = smartSpliterContainer1.Height / 2;
        }

        private void LotBatchTrackIn_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Empty;
                Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
            }
        }
        #endregion

        #region 툴바
        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            if (ShowMessage(MessageBoxButtons.YesNo, DialogResult.No, "InfoSave") == DialogResult.No) return;

            base.OnToolbarSaveClick();

            DataTable lotList = grdLotList.View.GetCheckedRows();

            if (isSpecChanged(lotList))
            {
                // 사양변경품입니다. LOT CARD 공정 특이사항 및 사양변경 통보서 확인하시고 진행바랍니다
                ShowMessage("SPECCHANGE");
            }

            string processSegmentId = lotList.Rows[0]["PROCESSSEGMENTID"].ToString();
            string equipmentId = grdEquipmentList.View.GetCheckedRows().Rows[0]["EQUIPMENTID"].ToString();

            Dictionary<string, object> messageParam = new Dictionary<string, object>();
            messageParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            messageParam.Add("PLANTID", UserInfo.Current.Plant);
            messageParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType); 
            messageParam.Add("P_PRODUCTDEFID", lotList.Rows[0]["PRODUCTDEFID"].ToString());
            messageParam.Add("P_PRODUCTDEFVERSION", lotList.Rows[0]["PRODUCTDEFVERSION"].ToString());
            messageParam.Add("PROCESSSEGMENTID", processSegmentId);

            DataTable messageInfo = SqlExecuter.Query("GetRoutingOperationList", "10001", messageParam);

            ShowMessage("WorkStartComment", Format.GetString(messageInfo.Rows[0]["DESCRIPTION"]));

            List<string> lotIds = new List<string>();
            foreach (DataRow lot in lotList.Rows)
            {
                lotIds.Add(lot["LOTID"].ToString());
            }

            MessageWorker messageWorker = new MessageWorker("SaveLotBatchTrackIn");
            messageWorker.SetBody(new MessageBody()
            {
                { "LOTIDS", string.Join(",", lotIds) }
                , { "EQUIPMENTID", equipmentId }
                , { "PROCESSSEGMENTID", processSegmentId }
                , { "WORKERS", txtWorker.GetValue() }
                , { "COMMENT", txtComment.EditValue }
            });
            messageWorker.Execute();

            ShowMessage("SuccedSave");
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
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdEquipmentList.DataSource = null;
            grdComment.DataSource = null;
            grdProcessSpec.DataSource = null;

            DataTable dtLotList = await SqlExecuter.QueryAsync("SelectBatchTrackIn", "10001", values);
            grdLotList.DataSource = dtLotList;
            if (dtLotList.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }
            else
            {
                string resourceid = Format.GetFullTrimString(grdLotList.View.GetRowCellValue(0,"RESOURCEID"));
                values.Add("RESOURCEID", resourceid);
                DataTable dtEquipmentList = await SqlExecuter.QueryAsync("SelectEquipmentListForBatchTrackIn", "10001", values);
                grdEquipmentList.DataSource = dtEquipmentList;

                SearchCommentAndSpec(dtLotList.Rows[0]);
            }
            DialogResult result = WipFunction.ShowWindowTimeArrivedPopup(values["P_AREAID"].ToString());    // Window Time 이 도래한 Lot 목록 표시
            if(result == DialogResult.OK)
            {
                OpenWindowTimeLot();
            }
        }
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            if (grdLotList.View.GetCheckedRows().Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            if (grdEquipmentList.View.GetCheckedRows().Rows.Count == 0)
            {
                // 설비는 필수로 선택해야 합니다. {0}
                throw MessageException.Create("EquipmentIsRequired", string.Empty);
            }
        }

        private void OpenWindowTimeLot()
        {
            try
            {
                DialogManager.ShowWaitDialog();
                string menuId = "PG-SG-0480";   // Window Time Lot 조회
                var param = new Dictionary<string, object>();
                OpenMenu(menuId, param); //다른창 호출..
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }

        // 수주사양결재나 R/C에서 사양변경여부를 체크하면
        // 지정된 중공정 또는 표준공정에서 사양변경 여부 확인 팝업을 표시
        private bool isSpecChanged(DataTable lots) 
        {
            List<string> lotIds = new List<string>();
            foreach(DataRow lot in lots.Rows)
            {
                lotIds.Add("'" + lot["LOTID"].ToString() + "'");
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTIDLIST", string.Join(",", lotIds));
            DataTable specChanged = SqlExecuter.Query("GetSpecChangedLots", "10001", param);
            return specChanged.Rows.Count > 0;
        }
        #endregion
    }
}
