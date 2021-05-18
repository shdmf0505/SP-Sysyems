#region using

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 >원자재 가공품 수입검사 결과등록
    /// 업  무  설  명  : 원자재 가공품 수입검사결과를 등록한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-08-21
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SubassemblyImportInspectionResult : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        #endregion

        #region 생성자

        public SubassemblyImportInspectionResult()
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
            #region 상단그리드 초기화
            // TODO : 그리드 초기화 로직 추가
            grdInspectionResult.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore |GridButtonItem.Export;
            grdInspectionResult.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdInspectionResult.View.SetIsReadOnly();

            grdInspectionResult.View.AddTextBoxColumn("ACCEPTDATE", 150)
                .SetLabel("INSPECTIONRECEIPTDATE");

            grdInspectionResult.View.AddTextBoxColumn("PRODUCTDEFID", 150);

            grdInspectionResult.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);

            grdInspectionResult.View.AddTextBoxColumn("PRODUCTDEFTYPE", 150)
                .SetLabel("CONSUMABLETYPE");

            grdInspectionResult.View.AddTextBoxColumn("VENDORNAME", 150);

            grdInspectionResult.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);

            grdInspectionResult.View.AddTextBoxColumn("LOTID", 200);

            grdInspectionResult.View.AddTextBoxColumn("CREATEDQTY", 150)
                .SetLabel("QTY")
                .SetTextAlignment(TextAlignment.Right);

            grdInspectionResult.View.AddTextBoxColumn("UNIT", 150);

            grdInspectionResult.View.AddTextBoxColumn("INSPECTIONRESULT", 150);

            grdInspectionResult.View.PopulateColumns();
            #endregion

            #region 하단그리드 초기화

            #region 외관검사 탭
            grdInspectionItem.GridButtonItem = GridButtonItem.None;
            grdInspectionItem.View.OptionsView.AllowCellMerge = true;
            grdInspectionItem.View.SetIsReadOnly();

            grdInspectionItem.View.SetSortOrder("SORT");

            //grdInspectionItem.View.AddTextBoxColumn("TYPE", 200)
            //    .SetIsReadOnly()
            //    .SetLabel("INSPTYPE");

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMCLASSID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetIsReadOnly();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMNAME", 250)
                .SetIsReadOnly();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMVERSION", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMTYPE", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddSpinEditColumn("INSPECTIONQTY", 150)
                .SetTextAlignment(TextAlignment.Right);

            grdInspectionItem.View.AddSpinEditColumn("SPECOUTQTY", 150)
                .SetTextAlignment(TextAlignment.Right);

            grdInspectionItem.View.AddTextBoxColumn("DEFECTRATE", 150)
                .SetIsReadOnly()
               // .SetDisplayFormat("{###.#:P0}", MaskTypes.Numeric);
               .SetDisplayFormat("###.#", MaskTypes.Numeric)
               .SetTextAlignment(TextAlignment.Right);


            grdInspectionItem.View.AddComboBoxColumn("INSPECTIONRESULT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdInspectionItem.View.AddTextBoxColumn("SORT", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdInspectionItem.View.PopulateColumns();
            #endregion

            #region 측정검사 탭
            grdInspectionItemSpec.GridButtonItem = GridButtonItem.None;
            grdInspectionItemSpec.View.OptionsView.AllowCellMerge = true;
            grdInspectionItemSpec.View.SetAutoFillColumn("INSPECTIONSTANDARD");
            grdInspectionItemSpec.View.SetIsReadOnly();

            grdInspectionItemSpec.View.SetSortOrder("SORT");

            //grdInspectionItemSpec.View.AddTextBoxColumn("TYPE", 200)
            //    .SetIsReadOnly()
            //    .SetLabel("INSPTYPE");

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMCLASSID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMNAME", 250)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMVERSION", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMTYPE", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("MEASUREVALUE", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,0.######", MaskTypes.Numeric);

            grdInspectionItemSpec.View.AddComboBoxColumn("INSPECTIONRESULT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPECTIONSTANDARD", 200)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("SORT", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.PopulateColumns();
            #endregion

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
            //등록 버튼 클릭 이벤트
            //btnRegister.Click += BtnRegister_Click;
            //grdInspectionResult 더블클릭 이벤트
            grdInspectionResult.View.DoubleClick += View_DoubleClick;

            //grdInspectionResult FocusedRow change 이벤트
            grdInspectionResult.View.FocusedRowChanged += (s, e) =>
            {
                SearchDefectList(grdInspectionResult.View.GetDataRow(e.FocusedRowHandle), grdInspectionItem, grdInspectionItemSpec, "OK_NG", "SubassemblyInspection");
            };

            //특정 컬럼만 Merge하는 이벤트 
            grdInspectionItem.View.CellMerge += View_CellMerge;
            grdInspectionItemSpec.View.CellMerge += View_CellMerge;

            grdInspectionResult.View.RowCellStyle += View_RowCellStyle;
            grdInspectionItem.View.RowCellStyle += View_RowCellStyle;
            grdInspectionItemSpec.View.RowCellStyle += View_RowCellStyle;
        }

        /// <summary>
        /// NG 시 Row 빨간색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView grid = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)grid.GetDataRow(e.RowHandle);

            if (Format.GetString(row["INSPECTIONRESULT"]).Equals("NG"))
            {
                if (!grid.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.Red;
            }

        }

        /// <summary>
        /// row 더블클릭하여 팝업여는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            SmartBandedGridView view = sender as SmartBandedGridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            DataRow row = view.GetDataRow(info.RowHandle);
            if (row == null) return;

            DialogManager.ShowWaitArea(pnlContent);

            SubassemblyImportInspectionResultPopup popup = new SubassemblyImportInspectionResultPopup("updateData");
            popup.CurrentDataRow = row;
            popup._type = "updateData";
            popup.SetPopupToDisplay();
            popup.FormBorderStyle = FormBorderStyle.Sizable;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Owner = this;
            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);

            if (popup.DialogResult == DialogResult.OK)
            {
                SearchMainGrd();
                SearchDefectList(grdInspectionResult.View.GetFocusedDataRow(), grdInspectionItem, grdInspectionItemSpec, "OK_NG", "SubassemblyInspection");
            }
        }

        /// <summary>
        /// 등록 버튼이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            Btn_RegistClick();
        }

        /// <summary>
        /// CellMerge이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null) return;

            if (e.Column.FieldName == "INSPECTIONMETHODNAME" || e.Column.FieldName == "INSPECTIONSTANDARD")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);
                e.Merge = (str1 == str2);
            }
            else
            {
                e.Merge = false;
            }

            e.Handled = true;
        }
        #endregion

        #region 툴바
        /*
        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdInspectionResult.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
        }
        */

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Regist"))
            {
                Btn_RegistClick();
            }

            if (btn.Name.ToString().Equals("ReportsPrint"))
            {
                Btn_PrintClick();
            }
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
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("RESOURCETYPE", "SubassemblyInspection");

            DataTable dt = await SqlExecuter.QueryAsync("SelectSubassemblyInspection", "10001" ,values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdInspectionResult.DataSource = dt;

            SearchDefectList(grdInspectionResult.View.GetFocusedDataRow(), grdInspectionItem, grdInspectionItemSpec, "OK_NG", "SubassemblyInspection");
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            InitializeConditionPopup_Vendor();
            InitializeConditionProcessSegmentId_Popup();
            InitializeConditionPopup_ProductDefId();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region 조회조건 팝업
        private void InitializeConditionPopup_Vendor()
        {
            // 팝업 컬럼 설정
            var vendorPopup = Conditions.AddSelectPopup("P_VENDORID", new SqlQuery("GetVendorList", "10001",$"ENTERPRISEID={UserInfo.Current.Enterprise}","VENDORTYPE=Supplier"), "VENDORNAME", "VENDORID")
                                        .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, false)
                                        .SetPopupLayoutForm(400, 600)
                                        .SetPopupResultCount(1)
                                        .SetPosition(3.1)
                                        .SetLabel("VENDOR")
                                        .SetRelationIds("P_PLANTID")
                                        .SetPopupAutoFillColumns("VENDORNAME");

            // 팝업 조회조건
            vendorPopup.Conditions.AddTextBox("VENDORID")
                       .SetLabel("VENDOR");

            // 팝업 그리드
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 150);

            vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
        }

        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentList", "10002", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(3.2);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
        }
        private void InitializeConditionPopup_ProductDefId()
        {
            var productPopup = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                                         .SetPopupLayout("PRODUCTDEF", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 600)
                                         .SetPopupResultCount(1)
                                         .SetPosition(4.1)
                                         .SetLabel("PRODUCTDEF");

            productPopup.Conditions.AddTextBox("PRODUCTDEF");

            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 200);
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
            grdInspectionResult.View.CheckValidation();

            DataTable changed = grdInspectionResult.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 하단 그리드 불량정보를 조회하는 함수
        /// </summary>
        /// <param name="row"></param>
        /// <param name="grid"></param>
        /// <param name="gridSpec"></param>
        /// <param name="inspItemType"></param>
        private void SearchDefectList(DataRow row, SmartBandedGrid grid, SmartBandedGrid gridSpec, string inspItemType, string resourceType)
        {
            if (row == null)
            {
                grid.DataSource = null;
                gridSpec.DataSource = null;
                return;
            }

            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"LANGUAGETYPE", Framework.UserInfo.Current.LanguageType},
                {"P_RESOURCEID", row["LOTID"]},
                {"RESOURCETYPE", resourceType},
                {"P_PROCESSRELNO", row["LOTID"]},
                {"TXNGROUPHISTKEY", row["TXNGROUPHISTKEY"]},
                {"P_INSPITEMTYPE", inspItemType},
                {"P_RELRESOURCEID", row["PRODUCTDEFID"] },
                {"P_RELRESOURCEVERSION", row["PRODUCTDEFVERSION"] },
                {"P_RELRESOURCETYPE", "Consumable" },
                {"P_INSPECTIONDEFID", "SubassemblyInspection"},
                {"P_INSPECTIONCLASSID", "SubassemblyInspection"}
            };

            //원자재 가공품
            //ItemClassTable - Defect
            //DataTable defectItemClassTable = SqlExecuter.Query("SelectItemClassToInspection", "10002", values);
            //ItemTable - Defect
            DataTable defectItemTable = SqlExecuter.Query("SelectSubassemblyInspectionExterior", "10001", values);

            values.Remove("P_INSPITEMTYPE");
            values.Add("P_INSPITEMTYPE", "SPC");

            //ItemClassTable - Measure
            //DataTable measureItemClassTable = SqlExecuter.Query("SelectItemClassToInspection", "10001", values);
            //ItemTable - Measure
            DataTable measureItemTable = SqlExecuter.Query("SelectSubassemblyInspectionMeasure", "10001", values);

            //grdDefectInspection
            //grdInspectionItem.DataSource = SetDataTableOrder(defectItemClassTable, defectItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
            grdInspectionItem.DataSource = defectItemTable;

            //grdMeasureInspection       
            //grdInspectionItemSpec.DataSource = SetDataTableOrder(measureItemClassTable, measureItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
            grdInspectionItemSpec.DataSource = measureItemTable;
        }

        /// <summary>
        /// 대분류에 해당하는 소분류 나열 순서를 정해주는 함수
        /// ex) 대분류 10,20,30..
        ///     소분류 10-1,10-2...
        /// </summary>
        private DataTable SetDataTableOrder(DataTable table1, DataTable table2)
        {
            int inspItemclassNo = 10;

            DataTable bindingTable = table2.Clone();
            DataRow addRow;

            foreach (DataRow row1 in table1.Rows)
            {
                int inspItemNo = 1;
                row1["SORT"] = inspItemclassNo;

                foreach (DataRow row2 in table2.Rows)
                {
                    if (row1["INSPITEMID"].Equals(row2["INSPITEMCLASSID"]))
                    {
                        row2["SORT"] = inspItemclassNo + "_" + inspItemNo;

                        addRow = bindingTable.NewRow();
                        addRow.ItemArray = row2.ItemArray.Clone() as object[];
                        bindingTable.Rows.Add(addRow);
                        inspItemNo++;
                    }
                }

                addRow = bindingTable.NewRow();
                addRow.ItemArray = row1.ItemArray.Clone() as object[];
                bindingTable.Rows.Add(addRow);
                inspItemclassNo += 10;
            }

            return bindingTable;
        }

        private async void SearchMainGrd()
        {
             await OnSearchAsync();          
        }

        /// <summary>
        /// 등록버튼 클릭 이벤트 버튼
        /// </summary>
        private void Btn_RegistClick()
        {
            DialogManager.ShowWaitArea(pnlContent);

            SubassemblyImportInspectionResultPopup popup = new SubassemblyImportInspectionResultPopup("insertData");
            popup._type = "insertData";
            popup.SetPopupToInsert();
            popup.FormBorderStyle = FormBorderStyle.Sizable;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Owner = this;
            popup.ShowDialog();
            DialogManager.CloseWaitArea(pnlContent);

            if (popup.DialogResult == DialogResult.OK)
            {
                SearchMainGrd();
                SearchDefectList(grdInspectionResult.View.GetFocusedDataRow(), grdInspectionItem, grdInspectionItemSpec, "OK_NG", "SubassemblyInspection");
            }
        }

        /// <summary>
        /// 성적서 출력 이벤트 함수
        /// </summary>
        private void Btn_PrintClick()
        {
            DataTable checkedRows = grdInspectionResult.View.GetCheckedRows();
            DataSet dsReport = new DataSet();

            DataTable headerRows = new DataTable();
            headerRows.Columns.Add(new DataColumn("LBLPRODUCTDEFID", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLPRODUCTDEFNAME", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLLOTID", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLPROCESSSEGMENTNAME", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLSAMPLEATTACHMENT", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLTOP", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLMIDDLE", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLBELOW", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLINSPECTIONUSER", typeof(string)));

            DataRow hearRow = headerRows.NewRow();
            hearRow["LBLPRODUCTDEFID"] = Language.Get("PRODUCTDEFID"); ;
            hearRow["LBLPRODUCTDEFNAME"] = Language.Get("PRODUCTDEFNAME");
            hearRow["LBLLOTID"] = Language.Get("LOTID");
            hearRow["LBLPROCESSSEGMENTNAME"] = Language.Get("PROCESSSEGMENTNAME");
            hearRow["LBLSAMPLEATTACHMENT"] = Language.Get("SAMPLEATTACHMENT");
            hearRow["LBLTOP"] = Language.Get("TOP");
            hearRow["LBLMIDDLE"] = Language.Get("MIDDLE");
            hearRow["LBLBELOW"] = Language.Get("BELOW");
            hearRow["LBLINSPECTIONUSER"] = Language.Get("INSPECTIONUSER");
            headerRows.Rows.Add(hearRow);

            dsReport.Tables.Add(headerRows);
            dsReport.Tables.Add(checkedRows);

            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.QualityAnalysis.Report.SubassemblyImportInspection.repx");

            XtraReport report = XtraReport.FromStream(stream);
            report.DataSource = dsReport.Tables[1];
            //report.DataMember = dsReport.Tables[0].TableName;

            Band header = report.Bands["ReportHeader"];
            SetReportControlDataBinding(header.Controls, dsReport.Tables[0]);

            Band band = report.Bands["Detail"];
            SetReportControlDataBinding(band.Controls, dsReport.Tables[1]);

            DataTable dt = grdInspectionResult.View.GetCheckedRows();

            if (dt.Rows.Count == 0)
            {
                throw MessageException.Create("NotSelectedPintInfo");
            }
            else
            {
                //report.Print();
                //report.PrintingSystem.EndPrint += PrintingSystem_EndPrint1; ;
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreview();
            }
        }

        /// <summary>
        /// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
        /// </summary>
        private void SetReportControlDataBinding(XRControlCollection controls, DataTable dataSource)
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()))
                        control.DataBindings.Add("Text", dataSource, control.Tag.ToString());

                    SetReportControlDataBinding(control.Controls, dataSource);
                }
            }
        }
        #endregion
    }
}
