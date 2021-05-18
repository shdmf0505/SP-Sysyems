#region using

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
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 자주/최종검사 이상발생
    /// 업  무  설  명  : 자주/최종검사 이상발생을 등록한다
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-11-04 
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SelfFinishAOIInspAbnormalOccurrence : SmartConditionManualBaseForm
    {
        #region Local Variables
        // TODO : 화면에서 사용할 내부 변수 추가
        private string abnormalType = "SelfInspectionTake";
        DXMenuItem _myContextMenu1;
        private string _strPlantId ="";

        #endregion
        
        #region 생성자
        public SelfFinishAOIInspAbnormalOccurrence()
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
            grdAbnormal.GridButtonItem = GridButtonItem.Export;
            grdAbnormal.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdAbnormal.View.SetSortOrder("NCRISSUEDATE", DevExpress.Data.ColumnSortOrder.Descending);
            grdAbnormal.View.SetIsReadOnly();

            var ncrInfo = grdAbnormal.View.AddGroupColumn("");

            ncrInfo.AddTextBoxColumn("NCRISSUEDATE", 150);

            ncrInfo.AddTextBoxColumn("STATENAME", 100);

            ncrInfo.AddTextBoxColumn("ABNOCRNO", 200)
                .SetIsHidden();

            ncrInfo.AddTextBoxColumn("ABNOCRTYPE", 200)
                .SetIsHidden();

            var processSegmentInfo = grdAbnormal.View.AddGroupColumn("PROCESSSEGMENTINFO");

            processSegmentInfo.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSNAME", 150);

            processSegmentInfo.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 150);

            processSegmentInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);

            processSegmentInfo.AddTextBoxColumn("AREANAME", 150);

            processSegmentInfo.AddTextBoxColumn("INSPECTIONDEFNAME", 150);

            processSegmentInfo.AddTextBoxColumn("INSPECTIONUSER", 100);

            var productdefInfo = grdAbnormal.View.AddGroupColumn("PRODUCTINFO");

            productdefInfo.AddTextBoxColumn("PRODUCTDEFID", 150);

            productdefInfo.AddTextBoxColumn("PRODUCTDEFNAME", 150);

            productdefInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 150);

            productdefInfo.AddTextBoxColumn("PARENTLOTID", 200);

            productdefInfo.AddTextBoxColumn("LOTID", 200);

            productdefInfo.AddTextBoxColumn("DEGREE", 80);

            productdefInfo.AddTextBoxColumn("PANELQTY", 100);

            productdefInfo.AddTextBoxColumn("PCSQTY", 100);

            var defectInfo = grdAbnormal.View.AddGroupColumn("DEFECTINFO");

            defectInfo.AddTextBoxColumn("DECISIONDEGREE", 100);

            defectInfo.AddTextBoxColumn("DEFECTCODENAME", 150);

            defectInfo.AddTextBoxColumn("QCSEGMENTNAME", 150);

            defectInfo.AddTextBoxColumn("DEFECTQTY", 100);

            defectInfo.AddTextBoxColumn("DEFECTRATE", 100);

            var reasonInfo = grdAbnormal.View.AddGroupColumn("REASONINFO");

            reasonInfo.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 150)
                .SetLabel("REASONPRODUCTDEFNAME");

            reasonInfo.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 100);

            reasonInfo.AddTextBoxColumn("REASONCONSUMABLELOTID", 200);

            reasonInfo.AddTextBoxColumn("REASONSEGMENTNAME", 150);

            reasonInfo.AddTextBoxColumn("REASONAREANAME", 150);

            var carInfo = grdAbnormal.View.AddGroupColumn("");

            carInfo.AddTextBoxColumn("CHARGERNAME", 100)
                .SetLabel("OWNER");

            //car 관련 날짜
            carInfo.AddTextBoxColumn("CARREQUESTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CARREQUESTDATE")
                .SetTextAlignment(TextAlignment.Center);

            carInfo.AddTextBoxColumn("CAREXPECTEDRECEIPTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CAREXPECTEDRECEIPTDATE")
                .SetTextAlignment(TextAlignment.Center);

            carInfo.AddTextBoxColumn("RECEIPTDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARRECEIPTDATE")
                 .SetTextAlignment(TextAlignment.Center);

            carInfo.AddTextBoxColumn("APPROVALDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARAPPROVALDATE")
                 .SetTextAlignment(TextAlignment.Center);

            carInfo.AddTextBoxColumn("CLOSEDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARCLOSEDATE")
                 .SetTextAlignment(TextAlignment.Center);

            carInfo.AddTextBoxColumn("ISCLOSE", 80);

            grdAbnormal.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdAbnormal.View.DoubleClick += View_DoubleClick;
            grdAbnormal.InitContextMenuEvent += grdAbnormal_InitContextMenuEvent;

            //2020-01-13 affectLot isLocking 에 따른 추가 
            grdAbnormal.View.RowCellStyle += View_RowCellStyle;
        }
        private void grdAbnormal_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            //Affect LOT 산정(출) 화면전환 메뉴ID : PG-QC-0556, 프로그램ID : Micube.SmartMES.QualityAnalysis.AffectLot
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-QC-0556"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0556" });
        }
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdAbnormal.View.GetFocusedDataRow();
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

        /// <summary>
        /// 더블클릭이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = grdAbnormal.View.GetFocusedDataRow();
            if (row == null) return;

            DialogManager.ShowWaitArea(pnlContent);

            //버튼의 enable
            bool isEnable = btnPopupFlag.Enabled;

            if (!Format.GetString(row["ISMODIFY"]).Equals("Y"))
            {
                isEnable = false;
            }
      

            abnormalType = row["ABNOCRTYPE"].ToString();
            
            SelfFinishAOIInspAbnormalOccurrencePopup popup = new SelfFinishAOIInspAbnormalOccurrencePopup(row, abnormalType);
            popup.CurrentDataRow = row;
            popup.SetStandardDataToUserControl();
            popup.FormBorderStyle = FormBorderStyle.Sizable;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.isEnable = isEnable;
            popup._strPlantId = _strPlantId;
            popup.Owner = this;
            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);

            if(DialogResult==DialogResult.OK)
            Popup_FormClosed();
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
            DataTable changed = grdAbnormal.GetChangedRows();

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
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            _strPlantId = values["P_PLANTID"].ToString();

            DataTable dt = await SqlExecuter.QueryAsync("SelectSelfFinishAOIAbnormalOccurrence", "10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdAbnormal.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 3.1, true, Conditions);

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            this.Conditions.AddComboBox("OPERATIONCLASS", new SqlQuery("GetProcessSegmentClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                  .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                  .SetEmptyItem()
                  .SetLabel("LARGEPROCESSSEGMENT")
                  .SetPosition(3.2);

            this.Conditions.AddComboBox("P_MIDDLEPROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                  .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                  .SetEmptyItem()
                  .SetRelationIds("OPERATIONCLASS")
                  .SetLabel("MIDDLEPROCESSSEGMENT")
                  .SetPosition(3.3);

            InitializeConditionProcessSegmentId_Popup();

            InitializeConditionAreaId_Popup();

            AddConditionDefectCodePopup();

            InitializeConditionReasonAreaId_Popup();
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
        /// <summary>
        /// 표준공정
        /// </summary>
        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentByTclass", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetLabel("PROCESSSEGMENT")
                .SetRelationIds("OPERATIONCLASS", "P_MIDDLEPROCESSSEGMENTCLASSID")
                .SetPosition(3.4);

            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");

            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 200);
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 작업장
        /// </summary>
        private void InitializeConditionAreaId_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("AreaType", "Area");
            //param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("AREA", UserInfo.Current.Area);
            param.Add("P_USERID", UserInfo.Current.Id);

            var areaIdPopup = this.Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAuthorityUserUseArea", "10001", param), "AREANAME", "AREAID")
                .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREANAME")
                .SetLabel("AREA")
                .SetRelationIds("P_PLANTID")
                .SetPosition(3.52);

            areaIdPopup.Conditions.AddTextBox("AREAIDNAME");

            areaIdPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaIdPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        //불량코드 팝업
        private void AddConditionDefectCodePopup()
        {
            // SelectPopup 항목 추가
            var conditionDefectCode = this.Conditions.AddSelectPopup("P_DEFECTCODE", new SqlQuery("GetDefectCodeList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}",  $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODENAME", "DEFECTCODE")
                .SetPopupLayout("DEFECTCODE", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("DEFECTCODENAME")
                .SetPosition(4.1);

            conditionDefectCode.SetPopupResultCount(1);

            conditionDefectCode.Conditions.AddTextBox("DEFECTCODENAME");

            conditionDefectCode.GridColumns.AddTextBoxColumn("DEFECTCODE", 150);
            conditionDefectCode.GridColumns.AddTextBoxColumn("DEFECTNAME", 200);
            conditionDefectCode.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150);
            conditionDefectCode.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200);
        }

        private void InitializeConditionReasonAreaId_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("AreaType", "Area");
            //param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("AREA", UserInfo.Current.Area);

            var areaIdPopup = this.Conditions.AddSelectPopup("p_reasonAreaId", new SqlQuery("GetAreaList", "10003", param), "AREANAME", "AREAID")
                .SetPopupLayout(Language.Get("SELECTREASONAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREANAME")
                .SetLabel("REASONAREANAME")
                .SetRelationIds("P_PLANTID")
                .SetPosition(4.2);

            areaIdPopup.Conditions.AddTextBox("AREA")
                .SetLabel("TXTAREA");

            areaIdPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaIdPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
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
            grdAbnormal.View.CheckValidation();

            DataTable changed = grdAbnormal.GetChangedRows();

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
