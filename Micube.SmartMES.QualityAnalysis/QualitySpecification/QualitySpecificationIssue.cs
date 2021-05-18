#region using

using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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
    /// 프 로 그 램 명  : 품질관리 > 품질규격 > 품질규격 이상발생
    /// 업  무  설  명  : 품질 규격 측정값중 NG가 발생한 항목들에 대해 이상발생처리를 한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-10-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class QualitySpecificationIssue : SmartConditionManualBaseForm
    {
        #region Local Variables
        DXMenuItem _myContextMenu1;
        #endregion

        #region 생성자

        public QualitySpecificationIssue()
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

            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>        
        /// 품질규격 이상발생 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdSpecIssue.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdSpecIssue.View.SetSortOrder("MEASUREDATETIME", DevExpress.Data.ColumnSortOrder.Descending);
            grdSpecIssue.View.SetIsReadOnly();
            
            grdSpecIssue.View.AddTextBoxColumn("STATENAME", 180)
                .SetTextAlignment(TextAlignment.Center); // 진행현황명
            grdSpecIssue.View.AddTextBoxColumn("MEASUREDATETIME", 180)
                .SetTextAlignment(TextAlignment.Center); // 측정일시
            grdSpecIssue.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 180); // 공정명
            grdSpecIssue.View.AddTextBoxColumn("AREANAME", 150); // 작업장명
            grdSpecIssue.View.AddTextBoxColumn("EQUIPMENTNAME", 220); // 설비명
            grdSpecIssue.View.AddTextBoxColumn("MEASUSERNAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("MEASURER"); // 측정자명
            grdSpecIssue.View.AddTextBoxColumn("CUSTOMERNAME", 150); // 고객사
            grdSpecIssue.View.AddTextBoxColumn("PRODUCTDEFID", 150); // 품목 ID
            grdSpecIssue.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 품목 Rev
            grdSpecIssue.View.AddTextBoxColumn("PRODUCTDEFNAME", 220); // 품목명
            grdSpecIssue.View.AddTextBoxColumn("LOTTYPE", 80)
                .SetTextAlignment(TextAlignment.Center); // 양산구분
            grdSpecIssue.View.AddTextBoxColumn("LOTID", 220)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Lot No"); // Lot No
            grdSpecIssue.View.AddTextBoxColumn("INSPITEMNAME", 120); // 검사항목명 (측정항목명)
            grdSpecIssue.View.AddTextBoxColumn("SPECSCOPE", 150); // 규격범위
            grdSpecIssue.View.AddTextBoxColumn("ISCLOSE", 80)
                .SetTextAlignment(TextAlignment.Center); // 마감여부
            grdSpecIssue.View.AddTextBoxColumn("AVERAGEVALUE", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetTextAlignment(TextAlignment.Right); // 평균
            grdSpecIssue.View.AddTextBoxColumn("MAXVALUE", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetTextAlignment(TextAlignment.Right); // 최대값
            grdSpecIssue.View.AddTextBoxColumn("MINVALUE", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetTextAlignment(TextAlignment.Right); // 최소값
            grdSpecIssue.View.AddTextBoxColumn("DEVIATION", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetTextAlignment(TextAlignment.Right); // 편차

            for (int i = 0; i < 60; i++)
            {
                grdSpecIssue.View.AddTextBoxColumn(string.Concat("MEASUREVALUE", "_", i + 1), 80)
                            .SetLabel(string.Concat(Language.Get("MEASURVALUE"), " ", i + 1))
                            .SetDisplayFormat("0.000", MaskTypes.Numeric)
                            .SetTextAlignment(TextAlignment.Right);
            } // 측정값 1 ~ 60

            grdSpecIssue.View.AddTextBoxColumn("STATE", 100)
                .SetIsHidden(); // 진행현황코드
            grdSpecIssue.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsHidden(); // 공정 ID
            grdSpecIssue.View.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden(); // 작업장 ID
            grdSpecIssue.View.AddTextBoxColumn("EQUIPMENTID", 100)
                .SetIsHidden(); // 설비 ID
            grdSpecIssue.View.AddTextBoxColumn("MEASURER", 100)
                .SetIsHidden(); // 측정자 ID
            grdSpecIssue.View.AddTextBoxColumn("CUSTOMERID", 100)
                .SetIsHidden(); // 고객사 ID
            grdSpecIssue.View.AddTextBoxColumn("DAITEMID", 100)
                .SetIsHidden(); // 검사항목 ID(측정항목)
            grdSpecIssue.View.AddTextBoxColumn("ABNOCRNO", 100)
                .SetIsHidden(); // 이상발생번호
            grdSpecIssue.View.AddTextBoxColumn("ABNOCRTYPE", 100)
                .SetIsHidden(); // 이상발생타입
            grdSpecIssue.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden(); // ResourceType
            grdSpecIssue.View.AddTextBoxColumn("ARRAYPOINTID", 100)
                .SetIsHidden(); // NG 측정포인트값
            grdSpecIssue.View.AddTextBoxColumn("ISMODIFY")
                .SetIsHidden(); // 작업장 통제 권한에 따른 수정가능여부
            grdSpecIssue.View.AddTextBoxColumn("WORKSTARTTIME")
                .SetIsHidden(); // 작업시작시간
            grdSpecIssue.View.AddTextBoxColumn("WORKENDTIME")
                .SetIsHidden(); // 작업종료시간

            grdSpecIssue.View.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 200)
                .SetLabel("REASONPRODUCTDEFNAME"); // 원인품목 명
            grdSpecIssue.View.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 100)
                .SetTextAlignment(TextAlignment.Center); // 원인품목 Version
            grdSpecIssue.View.AddTextBoxColumn("REASONCONSUMABLELOTID", 200)
                .SetTextAlignment(TextAlignment.Center); // 원인자재 Lot 
            grdSpecIssue.View.AddTextBoxColumn("REASONSEGMENTNAME", 150); // 원인공정 명
            grdSpecIssue.View.AddTextBoxColumn("REASONAREANAME", 150); // 원인작업장 명

            //2020-01-15 강유라 car관련 요청, 접수예정일, 접수, 승인,유효성평가 날짜 조회 추가
            //car 관련 날짜
            grdSpecIssue.View.AddTextBoxColumn("CARREQUESTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CARREQUESTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdSpecIssue.View.AddTextBoxColumn("CAREXPECTEDRECEIPTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CAREXPECTEDRECEIPTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdSpecIssue.View.AddTextBoxColumn("RECEIPTDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARRECEIPTDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdSpecIssue.View.AddTextBoxColumn("APPROVALDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARAPPROVALDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdSpecIssue.View.AddTextBoxColumn("CLOSEDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARCLOSEDATE")
                 .SetTextAlignment(TextAlignment.Center);
            grdSpecIssue.View.PopulateColumns();

            grdSpecIssue.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdSpecIssue.View.DoubleClick += View_DoubleClick;
            grdSpecIssue.InitContextMenuEvent += grdSpecIssue_InitContextMenuEvent;
            grdSpecIssue.View.RowCellStyle += View_RowCellStyle;

            //2020-01-13 affectLot isLocking 에 따른 추가 
            //grdSpecIssue.View.RowCellStyle += View_RowCellStyle;
        }

        /// <summary>
        /// NG가 발생한 측정포인트에 대해서 글씨색을 빨간색으로 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (IsNgPoint(e.Column.FieldName, e.RowHandle))
            {
                e.Appearance.ForeColor = Color.Red;
            }

            GridView grid = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)grid.GetDataRow(e.RowHandle);

            if (Format.GetString(row["AFFECTISLOCKING"]).Equals("Y"))
            {
                if (!grid.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.BackColor = Color.Yellow;
            }
        }

        /// <summary>
        /// NG 발생 포인트 컬럼 추출 함수
        /// </summary>
        /// <param name="col"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        private bool IsNgPoint(string col, int handle)
        {
            bool flag = false;

            try
            {
                string ngPoint = Format.GetString(grdSpecIssue.View.GetRowCellValue(handle, "ARRAYPOINTID"));
                string[] ngSplitPoint = ngPoint.Split(',');

                foreach (string str in ngSplitPoint)
                {
                    if (("MEASUREVALUE_" + str).Equals(col))
                    {
                        flag = true;
                        break;
                    }
                }

                return flag;
            }
            catch
            {
                return flag;
            }
        }

        /// <summary>
        /// Row 더블클릭시 이상발생팝업 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = grdSpecIssue.View.GetFocusedDataRow();

            pnlContent.ShowWaitArea();

            QualitySpecificationIssuePopup popup = new QualitySpecificationIssuePopup(row);
            popup.Owner = this;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ParentControl = this;
            popup.SearchAutoAffectLot();
            popup.btnSave.Enabled = btnFlag.Enabled;

            popup.ShowDialog();

            pnlContent.CloseWaitArea();
        }

        private void grdSpecIssue_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            //Affect LOT 산정(출) 화면전환 메뉴ID : PG-QC-0556, 프로그램ID : Micube.SmartMES.QualityAnalysis.AffectLot
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-QC-0556"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0556" });
        }

        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdSpecIssue.View.GetFocusedDataRow();
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
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdList.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
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
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_USERID", UserInfo.Current.Id);

            DataTable dt = await SqlExecuter.QueryAsync("GetQualitySpecificationIssue", "10001", values);

            if (dt.Rows.Count < 1) 
            {
                this.ShowMessage("NoSelectData");
                grdSpecIssue.DataSource = null;
                return;
            }

            grdSpecIssue.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
            //    .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
            //    .SetDefault(UserInfo.Current.Plant)
            //    .SetLabel("PLANT")
            //    .SetPosition(1.1);

            InitializeConditionPopup_Processsegment();
            InitializeConditionPopup_Area();
            InitializeConditionPopup_Equipment();
            InitializeConditionPopup_Chemical();
            InitializeConditionPopup_Customer();
            InitializeConditionPopup_Product();
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
        /// 표준공정 조회조건
        /// </summary>
        private void InitializeConditionPopup_Processsegment()
        {
            var processsegmentPopup = Conditions.AddSelectPopup("p_processsegmentId", new SqlQuery("GetSmallProcesssegmentListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTIDVERSION")
               .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 600)
               .SetLabel("PROCESSSEGMENT")
               .SetPopupResultCount(1)
               .SetPosition(1.2);

            processsegmentPopup.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");

            processsegmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            processsegmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            processsegmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetTextAlignment(TextAlignment.Center);
            processsegmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTIDVERSION", 100)
                .SetIsHidden();
        }

        /// <summary>
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Area()
        {
            var areaPopup = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAuthorityUserUseArea", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_USERID={UserInfo.Current.Id}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("AREA")
               .SetPopupResultCount(1)
               .SetPosition(1.3)
               .SetRelationIds("p_plantId");

            areaPopup.Conditions.AddTextBox("AREAIDNAME")
                .SetLabel("AREAIDNAME");

            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        /// <summary>
        /// 설비 조회조건
        /// </summary>
        private void InitializeConditionPopup_Equipment()
        {
            var equipmentPopup = Conditions.AddSelectPopup("p_equipmentId", new SqlQuery("GetEquipmentListBySpecIssue", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "EQUIPMENTNAME", "EQUIPMENTID")
               .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("EQUIPMENT")
               .SetPopupResultCount(1)
               .SetPosition(1.4)
               .SetRelationIds("p_plantId", "p_areaId");
            
            equipmentPopup.Conditions.AddTextBox("EQUIPMENTIDNAME")
                .SetLabel("EQUIPMENTIDNAME");

            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);
        }

        /// <summary>
        /// 검사항목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Chemical()
        {
            var inspitemPopup = Conditions.AddSelectPopup("p_inspitemId", new SqlQuery("GetInspitemListBySpecIssue", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMNAME", "INSPITEMID")
               .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("INSPITEM")
               .SetPopupResultCount(1)
               .SetPosition(1.5);

            inspitemPopup.Conditions.AddTextBox("INSPITEMIDNAME");

            inspitemPopup.GridColumns.AddTextBoxColumn("INSPITEMID", 150)
                .SetValidationKeyColumn();
            inspitemPopup.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
        }

        /// <summary>
        /// 고객사 조회조건
        /// </summary>
        private void InitializeConditionPopup_Customer()
        {
            var customerPopup = Conditions.AddSelectPopup("p_customerId", new SqlQuery("GetCustomerListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
               .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("CUSTOMER")
               .SetPopupResultCount(1)
               .SetPosition(1.6);

            customerPopup.Conditions.AddTextBox("CUSTOMERIDNAME");

            customerPopup.GridColumns.AddTextBoxColumn("CUSTOMERID", 150)
                .SetValidationKeyColumn();
            customerPopup.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
        }

        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 600)
               .SetLabel("PRODUCT")
               .SetPopupResultCount(1)
               .SetPosition(1.7);

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetValidationKeyColumn();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100)
                .SetIsHidden();
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
            //grdList.View.CheckValidation();

            //DataTable changed = grdList.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        #endregion

        #region Global Function

        /// <summary>
        /// Popup 닫혔을때 재검색하기 위한 함수
        /// </summary>
        public void Search()
        {
            this.OnSearchAsync();
        }

        #endregion
    }
}
