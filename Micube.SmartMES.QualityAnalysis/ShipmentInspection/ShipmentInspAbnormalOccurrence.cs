#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
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
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 출하검사 이살발생
    /// 업  무  설  명  : 출하 검사 경과를 등록한다
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-10-30 
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ShipmentInspAbnormalOccurrence : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        DXMenuItem _myContextMenu1;

        #endregion

        #region 생성자

        public ShipmentInspAbnormalOccurrence()
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
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가

            #region 유출공정 그리드 초기화
            grdOutFlow.GridButtonItem = GridButtonItem.Export;
            grdOutFlow.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdOutFlow.View.SetSortOrder("NCRISSUEDATE", DevExpress.Data.ColumnSortOrder.Descending);
            grdOutFlow.View.SetIsReadOnly();

            grdOutFlow.View.AddTextBoxColumn("STATENAME", 150);

            grdOutFlow.View.AddTextBoxColumn("NCRISSUEDATE", 200)
                .SetLabel("OSPPRINTTIME");

            grdOutFlow.View.AddTextBoxColumn("INSPECTIONDATE", 200);

            grdOutFlow.View.AddTextBoxColumn("PRODUCTDEFID", 150);

            grdOutFlow.View.AddTextBoxColumn("PRODUCTDEFVERSION", 150);

            grdOutFlow.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);

            grdOutFlow.View.AddTextBoxColumn("PARENTLOTID", 150);

            grdOutFlow.View.AddTextBoxColumn("LOTID", 150);

            grdOutFlow.View.AddTextBoxColumn("DEGREE", 150);

            grdOutFlow.View.AddTextBoxColumn("NGCOUNT", 150);

            grdOutFlow.View.AddTextBoxColumn("DEFECTCODENAME", 150);

            grdOutFlow.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            grdOutFlow.View.AddTextBoxColumn("INSPECTIONQTY", 150);

            grdOutFlow.View.AddTextBoxColumn("DEFECTQTY", 150);

            grdOutFlow.View.AddTextBoxColumn("DEFECTRATE", 150);

            grdOutFlow.View.AddTextBoxColumn("INSPECTORNAME", 150);

            grdOutFlow.View.AddTextBoxColumn("ISCLOSE", 150);

            //원인           
            grdOutFlow.View.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 150)
                .SetLabel("REASONPRODUCTDEFNAME");

            grdOutFlow.View.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 150);

            grdOutFlow.View.AddTextBoxColumn("REASONCONSUMABLELOTID", 150)
                .SetLabel("CAUSEMATERIALLOT");

            grdOutFlow.View.AddTextBoxColumn("REASONSEGMENTNAME", 150);

            grdOutFlow.View.AddTextBoxColumn("REASONAREANAME", 150);

            //car 관련 날짜
            grdOutFlow.View.AddTextBoxColumn("CARREQUESTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CARREQUESTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdOutFlow.View.AddTextBoxColumn("CAREXPECTEDRECEIPTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CAREXPECTEDRECEIPTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdOutFlow.View.AddTextBoxColumn("RECEIPTDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARRECEIPTDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdOutFlow.View.AddTextBoxColumn("APPROVALDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARAPPROVALDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdOutFlow.View.AddTextBoxColumn("CLOSEDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARCLOSEDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdOutFlow.View.PopulateColumns();
            #endregion

            #region 원인공정 그리드 초기화
            grdCause.GridButtonItem = GridButtonItem.Export;
            grdCause.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdCause.View.SetSortOrder("NCRISSUEDATE", DevExpress.Data.ColumnSortOrder.Descending);
            grdCause.View.SetIsReadOnly();

            grdCause.View.AddTextBoxColumn("STATENAME", 150);

            grdCause.View.AddTextBoxColumn("NCRISSUEDATE", 200)
                .SetLabel("OSPPRINTTIME");

            grdCause.View.AddTextBoxColumn("INSPECTIONDATE", 200);

            grdCause.View.AddTextBoxColumn("NCRISSUEDATE", 200);

            grdCause.View.AddTextBoxColumn("PRODUCTDEFID", 150);

            grdCause.View.AddTextBoxColumn("PRODUCTDEFVERSION", 150);

            grdCause.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);

            grdCause.View.AddTextBoxColumn("PARENTLOTID", 150);

            grdCause.View.AddTextBoxColumn("LOTID", 150);

            grdCause.View.AddTextBoxColumn("DEGREE", 150);

            grdCause.View.AddTextBoxColumn("NGCOUNT", 150);

            grdCause.View.AddTextBoxColumn("DEFECTCODENAME", 150);

            grdCause.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            grdCause.View.AddTextBoxColumn("INSPECTIONQTY", 150);

            grdCause.View.AddTextBoxColumn("DEFECTQTY", 150);

            grdCause.View.AddTextBoxColumn("DEFECTRATE", 150);

            grdCause.View.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 150)
                .SetLabel("REASONPRODUCTDEFNAME");

            grdCause.View.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 150);

            grdCause.View.AddTextBoxColumn("REASONCONSUMABLELOTID", 150);

            grdCause.View.AddTextBoxColumn("REASONSEGMENTNAME", 150);

            grdCause.View.AddTextBoxColumn("REASONAREANAME", 150);

            grdCause.View.AddTextBoxColumn("INSPECTORNAME", 150);

            grdCause.View.AddTextBoxColumn("ISCLOSE", 150);

            //car 관련 날짜
            grdCause.View.AddTextBoxColumn("CARREQUESTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CARREQUESTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdCause.View.AddTextBoxColumn("CAREXPECTEDRECEIPTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CAREXPECTEDRECEIPTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdCause.View.AddTextBoxColumn("RECEIPTDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARRECEIPTDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdCause.View.AddTextBoxColumn("APPROVALDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARAPPROVALDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdCause.View.AddTextBoxColumn("CLOSEDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARCLOSEDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdCause.View.PopulateColumns();
            #endregion
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //그리드 더블클릭 이벤트
            grdOutFlow.View.DoubleClick += View_DoubleClick;
            grdCause.View.DoubleClick += View_DoubleClick;
            grdOutFlow.InitContextMenuEvent += grdOutFlow_InitContextMenuEvent;
            grdCause.InitContextMenuEvent += grdCause_InitContextMenuEvent;

            //2020-01-13 affectLot isLocking 에 따른 추가 
            grdOutFlow.View.RowCellStyle += View_RowCellStyle;
            grdCause.View.RowCellStyle += View_RowCellStyle;
        }
        private void grdOutFlow_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            //Affect LOT 산정(출) 화면전환 메뉴ID : PG-QC-0556, 프로그램ID : Micube.SmartMES.QualityAnalysis.AffectLot
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-QC-0556"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0556" });
        }
        private void grdCause_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            //Affect LOT 산정(출) 화면전환 메뉴ID : PG-QC-0556, 프로그램ID : Micube.SmartMES.QualityAnalysis.AffectLot
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-QC-0556"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0556" });
        }
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdOutFlow.View.GetFocusedDataRow();
                if (currentRow == null) return;

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var param = currentRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

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
        private void View_DoubleClick(object sender, EventArgs e)
        {
            SmartBandedGridView view = (SmartBandedGridView)sender;

            DataRow row = view.GetFocusedDataRow();

            if (row == null) return;

            DialogManager.ShowWaitArea(pnlContent);

            ShipmentInspAbnormalOccurrencePopup popup = new ShipmentInspAbnormalOccurrencePopup(row);
            popup.CurrentDataRow = row;
            popup.isEnable = btnPopupFlag.Enabled;
            popup.SetData();
            popup.FormBorderStyle = FormBorderStyle.Sizable;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Owner = this;
            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);
            Popup_FormClosed();
        }

        /// <summary>
        /// affectLot isLocking Y 시 Row 빨간색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView grid = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)grid.GetDataRow(e.RowHandle);

            if (Format.GetString(row["AFFECTISLOCKING"]).Equals("Y"))
            {
                if (!grid.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.Red;
            }

        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdOutFlow.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
        }

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
            //values.Add("PLANTID",UserInfo.Current.Plant);
            values.Add("ENTERPRISEID",UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ABNOCRTYPE", "SpillShipmentInspection");

            DataTable dtOutFlow = await SqlExecuter.QueryAsync("SelectShipmentAbnormalOccurrence", "10001", values);

            values.Remove("ABNOCRTYPE");
            values.Add("ABNOCRTYPE", "ReasonShipmentInspection");

            DataTable dtCause = await SqlExecuter.QueryAsync("SelectShipmentAbnormalOccurrence", "10001", values);

            if (dtOutFlow.Rows.Count < 1 && dtCause.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdOutFlow.DataSource = dtOutFlow;
            grdCause.DataSource = dtCause;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 3.1, true, Conditions);
            //불량코드
            AddConditionDefectCodePopup();
            //작업장
            AddConditionProcessSegmentPopup();
            //검사자
            AddConditionPopupInspectorPopup();


        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region 조회조건 팝업 초기화

        //불량코드 팝업
        private void AddConditionDefectCodePopup()
        {
            // SelectPopup 항목 추가
            var conditionDefectCode = this.Conditions.AddSelectPopup("P_DEFECTCODE", new SqlQuery("GetDefectCodeList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}",  $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTNAME", "DEFECTCODE")
                .SetPopupLayout("DEFECTCODE", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("DEFECTCODENAME")
                .SetPosition(3.2);

            conditionDefectCode.SetPopupResultCount(1);

            conditionDefectCode.Conditions.AddTextBox("DEFECTCODENAME");

            conditionDefectCode.GridColumns.AddTextBoxColumn("DEFECTCODE", 150);
            conditionDefectCode.GridColumns.AddTextBoxColumn("DEFECTNAME", 200);
            conditionDefectCode.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150);
            conditionDefectCode.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200);
        }

        //원인공정 팝업
        private void AddConditionProcessSegmentPopup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_REASONSEGMENTID", new SqlQuery("GetProcessSegmentList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(3.3);

                processSegmentIdPopup.SetPopupResultCount(1);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT")
                .SetEmptyItem();
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }

        //작업자 팝업
        private void AddConditionPopupInspectorPopup()
        {
            // SelectPopup 항목 추가
            var conditionInspector = this.Conditions.AddSelectPopup("P_INSPECTORUSER", new SqlQuery("GetInspectorByInspectionType", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", "INSPECTIONTYPE=ShipmentInspection", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "USERNAME", "INSPECTORID")
                .SetPopupLayout("INSPECTOR", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("INSPECTOR")
                .SetRelationIds("P_PLANTID")
                .SetPosition(3.4);

            conditionInspector.SetPopupResultCount(1);

            conditionInspector.Conditions.AddTextBox("USERIDNAME");

            conditionInspector.GridColumns.AddTextBoxColumn("INSPECTORID", 150).SetIsHidden();
            conditionInspector.GridColumns.AddTextBoxColumn("USERID", 150);
            conditionInspector.GridColumns.AddTextBoxColumn("USERNAME", 100);
            conditionInspector.GridColumns.AddTextBoxColumn("GRADE", 80);


        }
        #endregion

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdOutFlow.View.CheckValidation();

            DataTable changed = grdOutFlow.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        //popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        private async void Popup_FormClosed()
        {
            await OnSearchAsync();
        }

        #endregion
    }
}
