#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
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
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 출하검사 성적서 매핑
    /// 업  무  설  명  : 출하검사 성적서 출력에 앞서 출력 될 정보를 시스템과 출력양식 간 매핑 한다.
    /// 생    성    자  : JAR
    /// 생    성    일  : 2020-01-04
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ShipmentInspMappingPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        /// <summary>
        /// SpreadSheet Context Menu
        /// </summary>
        private ContextMenu _popupMenu = new ContextMenu();
        /// <summary>
        /// SpreadSheet Menu Item Array
        /// </summary>
        private string[,] _menuItems;
        /// <summary>
        /// Shipment Document File Download Path
        /// </summary>
        private string _tempFilePath = System.Environment.CurrentDirectory + "\\ShipmentTempFiles\\";
        /// <summary>
        /// 양식 헤더 수정 여부
        /// </summary>
        private bool _isEdit = false;
        /// <summary>
        /// 신규
        /// </summary>
        private int _MaxRevisionNo = 1;
        /// <summary>
        /// 최종 개정번호 버전이 계속 변하므로 LangeuageKey를 미리 가져온다
        /// </summary>
        private string _MaxRevisionNoLanguageKey = Framework.Language.GetDictionary("MAXREVISIONNO").Name;
        private string _ContextMenuText_MappingInfo = Framework.Language.GetDictionary("MAPPINGINFOCONTEXTMENU").Name;
        private string _ContextMenuText_MappingItem = Framework.Language.GetDictionary("MAPPINGCONTEXTMENU").Name;
        private string _ContextMenuText_ImageSize = Framework.Language.GetDictionary("MAPPINGIMAGECONTEXTMENU").Name;

        private DataTable _dtShipmentInspectionType = SqlExecuter.Query("GetCodeFromParentCodeClass", "10001", new Dictionary<string, object> { { "PARENTCODECLASSID", "ShipmentInspectionType" },{ "LANGUAGETYPE", UserInfo.Current.LanguageType } });
        #endregion

        #region 생성자

        public ShipmentInspMappingPopup()
        {
            InitializeComponent();
            InitializeContent();
            InitializeEvent();

            this.DialogResult = DialogResult.None;
        }

        #endregion

        #region 컨텐츠 영역 초기화
        private void InitializeContent()
        {
            sprTemplate.ContextMenu = _popupMenu;
            //SpreadSheet 기본 팝업메뉴 Disable
            sprTemplate.Options.Behavior.ShowPopupMenu = DevExpress.XtraSpreadsheet.DocumentCapability.Disabled;
        }
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMappingList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdMappingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMappingList.ShowButtonBar = true;

            grdMappingList.View.AddTextBoxColumn("MappingSeq")
                .SetIsHidden();
            //grdMappingList.View.AddTextBoxColumn("ParentInspectionClassID")
            //    .SetIsHidden();
            //grdMappingList.View.AddTextBoxColumn("ParentInspectionClassName", 150)
                //.SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("InspectionClassID", 50)
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("InspectionClassName", 150)
                .SetIsReadOnly();
            grdMappingList.View.AddTextBoxColumn("InspectionMethodID", 50)
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("InspectionMethodName", 150)
                .SetIsReadOnly()
                .SetLabel("INSPECTIONMETHODPROCESS");
            grdMappingList.View.AddTextBoxColumn("InspItemID", 50)
                .SetIsHidden();
            InitializeAllInspectionPopup();
            
            //조건부 필수 항목
            grdMappingList.View.AddComboBoxColumn("ShipmentInspectionType", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"CODECLASSID="), "CODENAME", "CODEID");

            grdMappingList.View.AddTextBoxColumn("InspectionPoint", 20)
                .SetIsHidden();

            grdMappingList.View.AddTextBoxColumn("SampleSize", 150)
                .SetDefault(1)
                .SetValidationIsRequired();
            grdMappingList.View.AddCheckBoxColumn("isInspectionTime", 150)
                .SetDefault("False");
            grdMappingList.View.AddCheckBoxColumn("IsVerticalBind", 150)
                .SetDefault("False");
            grdMappingList.View.AddCheckBoxColumn("IsImage", 150)
                .SetDefault("False");
            //GDATA 관련 옵션 추가
            grdMappingList.View.AddCheckBoxColumn("useGdata", 150)
                .SetDefault("False")
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("TargetValue", 80)
                .SetDefault(1)
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("MeasuringCorrection", 50)
                .SetDefault(1)
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("RoundingOff", 50)
                .SetDefault(1)
                .SetIsHidden();

            grdMappingList.View.AddTextBoxColumn("ColumnSpan", 150)
                .SetDefault(1)
                .SetValidationIsRequired();
            grdMappingList.View.AddTextBoxColumn("RowSpan", 150)
                .SetDefault(1)
                .SetValidationIsRequired();

            grdMappingList.View.AddTextBoxColumn("ImageColumnRange", 150)
                .SetDefault(1);
            grdMappingList.View.AddTextBoxColumn("ImageRowRange", 150)
                .SetDefault(1);
            grdMappingList.View.AddCheckBoxColumn("IsKeepRatio", 150)
                .SetDefault("False");

            grdMappingList.View.AddTextBoxColumn("SheetIndex", 150)
                .SetValidationIsRequired();
            grdMappingList.View.AddTextBoxColumn("ColumnStart", 150)
                .SetDefault("A")
                .SetValidationIsRequired();
            grdMappingList.View.AddTextBoxColumn("ColumnEnd", 150)
                .SetDefault("A")
                .SetValidationIsRequired();
            grdMappingList.View.AddTextBoxColumn("RowStart", 150)
                .SetDefault(1)
                .SetValidationIsRequired();
            grdMappingList.View.AddTextBoxColumn("RowEnd", 150)
                .SetDefault(1)
                .SetValidationIsRequired();

            grdMappingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMappingList.View.PopulateColumns();
        }
        /// <summary>
        /// 검사항목 조회 팝업 추가
        /// </summary>
        private void InitializeAllInspectionPopup()
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    {"PRODUCTDEFID", CurrentDataRow["PRODUCTDEFID"].ToString() },
                    {"PRODUCTDEFVERSION", CurrentDataRow["PRODUCTDEFVERSION"].ToString()},
                    {"LANGUAGETYPE", UserInfo.Current.LanguageType }
                };
                string InspectionClassID = "";
                var AllInspectionPopup = grdMappingList.View.AddSelectPopupColumn("InspItemName", 150, new SqlQuery("GetAllInspectionItem", "10001", param))//, "PARENTINSPECTIONCLASSID, INSPITEMNAME")
                    .SetPopupLayout("INSPITEMNAME", PopupButtonStyles.Ok_Cancel, true, false)
                    .SetPopupResultCount(0)
                    .SetPopupLayoutForm(1200, 600, FormBorderStyle.FixedToolWindow, DevExpress.XtraLayout.Utils.LayoutType.Horizontal)
                    .SetValidationIsRequired()
                    .SetLabel("INSPITEMPOINT")
                    .SetPopupApplySelection((selectedRows, dataGridRow) =>
                    {
                        int irow = 0;

                        bool bolAddRow = false;
                        bool bolFirstBind = true;
                        DataRow classRow = grdMappingList.View.GetFocusedDataRow();
                        InspectionClassID = grdMappingList.View.GetFocusedDataRow()["INSPECTIONCLASSID"].ToSafeString();
                        foreach (DataRow row in selectedRows)
                        {
                            //여러 항목을 선택한 경우 선택한만큼 행을 추가 한다.
                            //다음 행이 있는지 체크
                            if (irow <= grdMappingList.View.RowCount - 1)
                            {
                                //첫번째 행인 경우
                                if (irow.Equals(0))
                                {
                                    classRow = grdMappingList.View.GetFocusedDataRow();
                                    //행추가 안함
                                    bolAddRow = false;
                                }
                                else 
                                    bolFirstBind = false;

                                //그리드 loof
                                for (int i = irow; i < grdMappingList.View.RowCount; i++)
                                {
                                    if (bolFirstBind) //첫번째 행인 경우
                                    {
                                        irow = grdMappingList.View.GetFocusedDataSourceRowIndex();
                                        break;
                                    }
                                    if (grdMappingList.View.GetDataRow(i)["INSPITEMID"].ToSafeString().Equals(string.Empty))
                                    {
                                        //현재 Loof중인 행이 비어있는경우 선택된 항목으로 채운다
                                        irow = i;
                                        grdMappingList.View.SelectRow(i);
                                        classRow = grdMappingList.View.GetFocusedDataRow();
                                        
                                        //행추가 안하도록
                                        bolAddRow = false;
                                        break;
                                    }
                                    else
                                    {
                                        //빈 행이 없으므로 행추가한다.
                                        bolAddRow = true;
                                    }
                                }
                            }
                            else
                            {
                                if(irow != grdMappingList.View.RowCount - 1)
                                    bolAddRow = true;
                            }

                            if(bolAddRow)
                            {
                                //행추가
                                grdMappingList.View.AddNewRow();
                                irow = grdMappingList.View.RowCount - 1;
                                grdMappingList.View.SelectRow(irow);
                                classRow = grdMappingList.View.GetFocusedDataRow();
                            }
                            
                            //classRow["PARENTINSPECTIONCLASSID"] = row["PARENTINSPECTIONCLASSID"];
                            //classRow["PARENTINSPECTIONCLASSNAME"] = row["PARENTINSPECTIONCLASSNAME"];
                            classRow["INSPECTIONCLASSID"] = row["INSPECTIONCLASSID"];
                            classRow["INSPECTIONCLASSNAME"] = row["INSPECTIONCLASSNAME"];
                            classRow["INSPECTIONMETHODID"] = row["INSPECTIONMETHODID"];
                            classRow["INSPECTIONMETHODNAME"] = row["INSPECTIONMETHODNAME"];
                            classRow["INSPITEMID"] = row["INSPITEMID"];
                            classRow["INSPITEMNAME"] = row["INSPITEMNAME"];
                            classRow["INSPITEMVERSION"] = row["INSPITEMVERSION"];
                            classRow["IsKeepRatio"] = "False";
                            classRow["IsImage"] = "False";
                            classRow["IsInspectionTime"] = "False";
                            classRow["IsVerticalBind"] = "False";
                            classRow["useGdata"] = "False";
                            classRow["ImageColumnRange"] = 1;
                            classRow["ImageRowRange"] = 1;
                            classRow["ColumnSpan"] = 1;
                            classRow["RowSpan"] = 1;
                            classRow["useGData"] = "False";
                            grdMappingList.View.RaiseValidateRow(irow);
                            grdMappingList.View.GetDataRow(irow).AcceptChanges();

                            //나중에 저장 되도록 속성 변경, 추가된 행이 아닌 빈 행을 채운경우 Modified
                            if(classRow["MAPPINGSEQ"].ToSafeInt32().Equals(0))
                                grdMappingList.View.GetDataRow(irow).SetAdded();
                            else
                                grdMappingList.View.GetDataRow(irow).SetModified();


                            //Target RowIndex 설정
                            irow = grdMappingList.View.GetFocusedDataSourceRowIndex() + 1;
                            grdMappingList.View.FocusedRowHandle = irow;
                        }
                    });

                AllInspectionPopup.GridColumns.AddTextBoxColumn("InspectionClassID", 50)
                    .SetIsHidden();
                AllInspectionPopup.GridColumns.AddTextBoxColumn("InspectionClassName", 150);
                AllInspectionPopup.GridColumns.AddTextBoxColumn("InspectionMethodID", 50)
                    .SetIsHidden();
                AllInspectionPopup.GridColumns.AddTextBoxColumn("InspectionMethodName", 150)
                .SetLabel("INSPECTIONMETHODPROCESS"); 
                AllInspectionPopup.GridColumns.AddTextBoxColumn("InspItem", 50)
                    .SetIsHidden();
                AllInspectionPopup.GridColumns.AddTextBoxColumn("InspitemVersion", 50)
                    .SetIsHidden();
                AllInspectionPopup.GridColumns.AddTextBoxColumn("InspItemName", 150)
                    .SetLabel("INSPITEMPOINT");
                AllInspectionPopup.GridColumns.AddTextBoxColumn("EquipmentInspectionItem", 150);
                AllInspectionPopup.GridColumns.AddTextBoxColumn("InspItemType", 150);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ShipmentInspMappingPopup_Load;
            this.Shown += ShipmentInspMappingPopup_Shown;
            this.FormClosing += ShipmentInspMappingPopup_FormClosing;
            // TODO : 화면에서 사용할 이벤트 추가
            btnFile.Click += BtnFile_Click;
            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;
            btnRevNo.Click += BtnRevNo_Click;

            _popupMenu.Popup += _popupMenu_Popup;

            grdMappingList.View.DoubleClick += View_DoubleClick;
            grdMappingList.View.CustomRowCellEdit += View_CustomRowCellEdit;

            //헤더 컨트롤 전체 이벤트 추가 (Tag -> 컬럼명)
            SmartSplitTableLayoutPanel[] smartSplitTableLayoutPanels = { LayoutPanel1, LayoutPanel2 };

            foreach (Control control in smartSplitTableLayoutPanels)
            {
                foreach (Control c in control.Controls)
                {
                    if (c.GetType().Equals(typeof(SmartTextBox)))
                    {
                        if (!c.Tag.ToSafeString().Equals(string.Empty))
                        {
                            //((SmartTextBox)c).ReadOnly = true;
                            c.TextChanged += C_TextChanged;
                            c.Click += C_Click;
                        }
                    }
                }
            }

            grdMappingList.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["SHIPMENTINSPECTIONTYPE"] = "";
                grdMappingList.View.PostEditor();
            };

            btnCopy.Click += BtnCopy_Click;
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtFileName.Text.Equals(string.Empty))
                {
                    throw Framework.MessageException.Create("NoExcelRevision");
                }
                string FileName = txtFileName.Text;
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    {"LANGUAGETYPE", UserInfo.Current.LanguageType },
                    {"P_EXISTDOCUMENT", "0" }
                };
                ConditionItemSelectPopup popup = CreateSelectPopup("PRODUCTDEFID", new SqlQuery("SelectShipmentInspMappingList", "10001", param));
                popup.ShowSearchButton = true;
                popup.IsMultiGrid = false;
                popup.SetPopupLayout("PRODUCTDEFNAME", PopupButtonStyles.Ok_Cancel, true, true);
                popup.SetPopupResultCount(1);
                popup.SetPopupLayoutForm(1200, 600, FormBorderStyle.FixedToolWindow, DevExpress.XtraLayout.Utils.LayoutType.Horizontal);

                popup.Conditions.AddTextBox("PRODUCTDEFNAME");

                DataTable popupDataSource = CurrentDataRow.Table.Clone();

                popup.GridColumns.AddTextBoxColumn("ProductClassID", 120)
                    .SetIsHidden();
                popup.GridColumns.AddTextBoxColumn("ProductDefID", 150);
                popup.GridColumns.AddTextBoxColumn("ProductDefName", 200);
                popup.GridColumns.AddTextBoxColumn("ProductDefVersion", 120);
                popup.GridColumns.AddTextBoxColumn("ProductDefTypeID", 150)
                    .SetIsHidden();
                popup.GridColumns.AddTextBoxColumn("ProductDefType", 150);
                popup.GridColumns.AddTextBoxColumn("CustomerID",120)
                    .SetIsHidden();
                popup.GridColumns.AddTextBoxColumn("CustomerName", 150);
                popup.GridColumns.AddTextBoxColumn("RevisionNo", 100);
                popup.GridColumns.AddTextBoxColumn("StartSheetIndex",50)
                    .SetIsHidden();
                popup.GridColumns.AddTextBoxColumn("FileName", 200);

                IEnumerable<DataRow> selectedDatas = ShowPopup(popup, popupDataSource.AsEnumerable());
                if (selectedDatas == null)
                    return;
                if(selectedDatas.Count() > 0)
                {
                    foreach(DataRow r in selectedDatas)
                    {
                        SetBindingMappingInfo(r["PRODUCTDEFID"].ToString(),
                            r["PRODUCTDEFVERSION"].ToString(),
                            r["PRODUCTCLASSID"].ToString(),
                            spnSheetIndex.Value.ToSafeInt32(),
                            r["REVISIONNO"].ToSafeInt32(),
                            false, true, true);
                        if (FileName != txtFileName.Text)
                            txtFileName.Text = FileName;
                        break;
                    }
                }

                txtCustomerName.Text = CurrentDataRow["CUSTOMERNAME"].ToString();
                txtProductDefID.Text = CurrentDataRow["PRODUCTDEFID"].ToString();
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.Message);
            }
        }
        #endregion

        #region Event
        #region Grid Event

        /// <summary>
        /// 각 행 별로 검사종류에 따른 출하검사 유형 콤보 바인딩 (규격측정인 경우 공정수입검사와 규격측정, 신뢰성검증의 경우 정기/정기외) - 매핑정보를 이용해 측정값 불러올때 테이블이 다름
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Column.FieldName == "SHIPMENTINSPECTIONTYPE")
            {
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit combo = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();

                DataTable dt = _dtShipmentInspectionType.Clone();
                string InspectionClassID = view.GetRowCellValue(e.RowHandle, "INSPECTIONCLASSID").ToString();

                try
                {
                    dt = _dtShipmentInspectionType.AsEnumerable().Where(w => w.Field<string>("CODECLASSID") == InspectionClassID).CopyToDataTable();
                }
                catch
                {
                    dt = _dtShipmentInspectionType.Clone();//view.SetRowCellValue(i, "INSPECTIONCLASSID", "");
                }
                combo.DataSource = dt;
                combo.NullText = "";
                combo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODEID"));
                combo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODENAME", e.Column.Caption));

                combo.ValueMember = "CODEID";
                combo.DisplayMember = "CODENAME";
                combo.Columns["CODEID"].Visible = false;

                e.RepositoryItem = combo;
            }
        }
        /// <summary>
        /// 매핑 항목 더블클릭 시 스프레드시트에 표기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DataRow focusedRow = grdMappingList.View.GetFocusedDataRow();

            if (focusedRow == null)
                return;
            if (focusedRow["SHEETINDEX"].ToSafeString().Equals(string.Empty))
                return;
            //인덱스는 0부터 시작이므로 -1
            int SelectedExcelSheetIndex = focusedRow["SHEETINDEX"].ToSafeInt32() - 1;
            sprTemplate.Document.History.Undo();

            int check;
            if (int.TryParse(focusedRow["COLUMNSTART"].ToSafeString(), out check) || int.TryParse(focusedRow["COLUMNEND"].ToSafeString(), out check))
                return;

            //RANGE를 A1:D1 형태로 만듬
            string ExcelRange = GetExcelColumnName(focusedRow["COLUMNSTART"].ToSafeString() + focusedRow["ROWSTART"].ToSafeString() + ":" + focusedRow["COLUMNEND"].ToSafeString() + focusedRow["ROWEND"].ToSafeString());
            try
            {
                sprTemplate.Document.Worksheets[SelectedExcelSheetIndex].Selection = sprTemplate.Document.Worksheets[SelectedExcelSheetIndex].Range[ExcelRange];
                sprTemplate.Document.Worksheets[SelectedExcelSheetIndex].Selection.FillColor = Color.Yellow;
            }
            catch (System.ArgumentOutOfRangeException Ax)
            {
                throw Framework.MessageException.Create("엑셀 시트가 존재하지 않습니다.");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (sprTemplate.Document.Worksheets.ActiveWorksheet.Index != SelectedExcelSheetIndex)
            {
                //SpnSheetIndex_EditValueChanging(spnSheetIndex, new DevExpress.XtraEditors.Controls.ChangingEventArgs(sprTemplate.ActiveWorksheet.Index + 1, SelectedExcelSheetIndex + 1));

                spnSheetIndex.Value = SelectedExcelSheetIndex + 1;
            }
        }
        #endregion

        #region TextEditor Event
        /// <summary>
        /// 상단 헤더매핑정보 변경 시 _isEdit = true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C_TextChanged(object sender, EventArgs e)
        {
            //텍스트박스 값 변경됬음을 표시(저장 용)
            _isEdit = true;
        }

        /// <summary>
        /// 헤더 주소값 클릭 시 스프레드 시트에 표기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C_Click(object sender, EventArgs e)
        {
            try
            {
                this.sprTemplate.Document.History.Undo();

                var txtBox = sender as DevExpress.XtraEditors.TextEdit;
                //주소 값 텍스트박스가 아닌 경우 return
                if (txtBox == txtCustomerName || txtBox == txtProductDefID || txtBox == txtProductDefVersion || txtBox == txtModel || txtBox == txtPartNo || txtBox == txtPartName)
                    return;
                //텍스트박스에 값이 없는경우 return
                if (txtBox.Text.Equals(string.Empty) || txtBox.Tag.ToSafeString().Equals(string.Empty))
                    return;

                sprTemplate.Document.Worksheets[spnSheetIndex.Value.ToSafeInt32() - 1].SelectedCell = sprTemplate.Document.Worksheets[spnSheetIndex.Value.ToSafeInt32() - 1].Range[txtBox.Text];
                sprTemplate.Document.Worksheets[spnSheetIndex.Value.ToSafeInt32() - 1].SelectedCell.FillColor = Color.Yellow;
                sprTemplate.Document.Worksheets.ActiveWorksheet = sprTemplate.Document.Worksheets[spnSheetIndex.Value.ToSafeInt32() - 1];
            }
            catch (InvalidOperationException refEx)
            {
                //다국어 변경할것
                throw MessageException.Create(refEx.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Spread Sheet Event
        /// <summary>
        /// 시트 변경 시 상단 시트번호 변경 (이벤트호출)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SprTemplate_ActiveSheetChanging(object sender, DevExpress.Spreadsheet.ActiveSheetChangingEventArgs e)
        {
            spnSheetIndex.EditValueChanging -= SpnSheetIndex_EditValueChanging;
            try
            {
                string FileName = txtFileName.Text;
                int ActiveSheetIndex = sprTemplate.Document.Worksheets[e.NewActiveSheetName].Index;
                if (_isEdit)
                {
                    // 변경된 내용을 저장하겠습니까
                    if (this.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave").Equals(DialogResult.Yes))
                    //if (MessageBox.Show("InfoPopupSave", "InfoPopupSave", MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
                    {
                        //변경이 완료되기 전 OldValue로 저장해야함
                        if (!SaveData(sprTemplate.Document.Worksheets[e.OldActiveSheetName].Index, false))
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                spnSheetIndex.Value = ActiveSheetIndex + 1;

                //정보 Refresh
                SetBindingMappingInfo(CurrentDataRow["PRODUCTDEFID"].ToString(), 
                                      CurrentDataRow["PRODUCTDEFVERSION"].ToString(),
                                      CurrentDataRow["PRODUCTCLASSID"].ToString(),
                                      ActiveSheetIndex, spnRevisionNo.Value.ToInt32(), false, false, false);
                if (FileName != txtFileName.Text)
                    txtFileName.Text = FileName;
            }
            catch (System.ArgumentOutOfRangeException Rex)
            {
                throw Framework.MessageException.Create(Rex.Message);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                spnSheetIndex.EditValueChanging += SpnSheetIndex_EditValueChanging;
            }
        }
        #endregion

        #region Button Event
        private void BtnRevNo_Click(object sender, EventArgs e)
        {
            spnSheetIndex.EditValueChanging -= SpnSheetIndex_EditValueChanging;
            sprTemplate.ActiveSheetChanging -= SprTemplate_ActiveSheetChanging;
            try
            {
                int OldRevisionValue = spnRevisionNo.OldEditValue.ToSafeInt32();
                bool isWithMappingItem = Convert.ToBoolean(chkWithMappingItem.EditValue);

                SetBindingMappingInfo(CurrentDataRow["PRODUCTDEFID"].ToString(),
                                      CurrentDataRow["PRODUCTDEFVERSION"].ToString(),
                                      CurrentDataRow["PRODUCTCLASSID"].ToString(),
                                      -1, spnRevisionNo.Value.ToInt32(), false, isWithMappingItem, true);
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.Message);
            }
            finally
            {
                spnSheetIndex.EditValueChanging += SpnSheetIndex_EditValueChanging;
                sprTemplate.ActiveSheetChanging += SprTemplate_ActiveSheetChanging;
            }
        }
        /// <summary>
        /// 닫기 클릭 시 - 변경된 내용이 있을 시 저장 후 닫음
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                bool isAllSave = grdMappingList.GetChangedRows().Rows.Count > 0 ? true : false;
                if (_isEdit || isAllSave)
                {
                    DialogResult dr = this.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave");
                    //DialogResult dr = MessageBox.Show("InfoPopupSave", "InfoPopupSave", MessageBoxButtons.YesNo);// 
                    // 변경된 내용을 저장하겠습니까
                    if (dr.Equals(DialogResult.Yes))
                    {
                        SaveData(spnSheetIndex.Value.ToSafeInt32() - 1, isAllSave);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
                else
                    this.DialogResult = DialogResult.OK;
                try
                {
                    if (sprTemplate.CreateNewDocument())
                    {
                        if (System.IO.Directory.Exists(_tempFilePath))
                        {
                            System.IO.Directory.Delete(_tempFilePath, true);
                        }
                        GC.Collect();
                    }
                }
                catch (Exception ex)
                {
                    throw Framework.MessageException.Create(ex.Message);
                }
                finally
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            _isEdit = true;
            SaveData(spnSheetIndex.Value.ToSafeInt32() - 1, true);
        }

        /// <summary>
        /// 양식파일 불러오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dr = new OpenFileDialog();
                dr.Filter = "ExcelFile ( *.xlsx, *.xls, *.xlsm) | *.xlsx;*.xls;*.xlsm";
                dr.ShowDialog();

                if (dr.FileName != "")
                {
                    sprTemplate.LoadDocument(dr.FileName);
                    txtFileName.Text = dr.FileName;
                    _isEdit = true;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ContextMenu Event 
        /// <summary>
        /// 스프레드 시트 컨텍스트 메뉴 팝업 생성 (좌측 그리드에 검사항목을 메뉴에 나타내도록 한다 / 시트번호가 같거나 시트번호가 아직 부여되지 않은 항목)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _popupMenu_Popup(object sender, EventArgs e)
        {
            _popupMenu.MenuItems[_popupMenu.MenuItems.Count - 2].MenuItems.Clear();
            string SheetIndex = (sprTemplate.ActiveWorksheet.Index + 1).ToString();
            int InspItemNo = 1;
            for (int i = 0; i < grdMappingList.View.RowCount; i++)
            {
                if (grdMappingList.View.IsNewItemRow(i))
                    continue;
                if (grdMappingList.View.GetRowCellValue(i, "INSPITEMID").ToSafeString().Equals(string.Empty))
                    continue;
                //SheetIndex가 현재 ActiveSheetIndex와 일치하거나 아직 SheetIndex가 지정 되지 않은 항목만
                if (grdMappingList.View.GetRowCellValue(i, "SHEETINDEX").ToSafeString().Equals(SheetIndex) || grdMappingList.View.GetRowCellValue(i, "SHEETINDEX").ToSafeString().Equals(string.Empty))
                {
                    MenuItem item = new MenuItem((i+1).ToString() + ". " + 
                                grdMappingList.View.GetRowCellValue(i, "INSPECTIONCLASSNAME").ToSafeString() + "-" +
                                grdMappingList.View.GetRowCellValue(i, "INSPECTIONMETHODNAME").ToSafeString() + "-" +
                                grdMappingList.View.GetRowCellValue(i, "INSPITEMNAME").ToSafeString());
                    /*----------------------------------------------------------------------------------
                     * ex ) 신뢰성검증-신뢰성평가-기계특성-항목1
                     *---------------------------------------------------------------------------------*/
                    //Tag는 코드 (','문자로 Split 하여 사용함)
                    item.Tag = grdMappingList.View.GetRowCellValue(i, "INSPECTIONCLASSID").ToSafeString() + "," +
                                grdMappingList.View.GetRowCellValue(i, "INSPECTIONMETHODID").ToSafeString() + "," +
                                grdMappingList.View.GetRowCellValue(i, "INSPITEMID").ToSafeString() + "," + SheetIndex + "," + i.ToString();
                    /*----------------------------------------------------------------------------------
                     * ex )Split(',') :: ReliabilityInspection,ReliabilityEvaluation,MechanicalCharacter,ReliabilityEvaluation,InspItemID,0,0
                     * Tag[0] = InspectionClassID
                     * Tag[1] = InspectionMethodID
                     * Tag[2] = InspItemID
                     * Tag[3] = SheetIndex
                     * Tag[4] = RowIndex
                     *---------------------------------------------------------------------------------*/
                    _popupMenu.MenuItems[_popupMenu.MenuItems.Count - 2].MenuItems.Add(item);
                    item.Click += ContextMenuItem_Click;

                    InspItemNo++;
                }
            }
        }

        /// <summary>
        /// 스프레드시트 컨텍스트 메뉴 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //스프레드 시트 변경사항 취소(선택 / 배경색 변경)
                this.sprTemplate.Document.History.Undo();
                MenuItem item = sender as MenuItem;
                int SelectedRowIndex = -1;
                //선택된 메뉴가 검사항목 중 하나 인 경우
                if (item.Parent.GetType().Equals(item.GetType()) && ((System.Windows.Forms.MenuItem)item.Parent).Index.Equals((_menuItems.Length / 2) - 2) && item.Parent.Tag.ToString().Equals("InspectionItem"))
                {
                    //Range
                    IList<DevExpress.Spreadsheet.Range> l = sprTemplate.GetSelectedRanges();

                    //실제값은 [검사항목 명, 검사항목 ID] 이므로 Split.
                    string[] Items = item.Tag.ToString().Split(',');
                    int rowIndex = -1;

                    //선택한 메뉴와 동일한 검사항목을 매핑리스트에서 찾는다.
                    for (int i = 0; i < grdMappingList.View.DataRowCount; i++)
                    {
                        //grdMappingList.View.GetDataRow(i).AcceptChanges();
                        DataRow r = grdMappingList.View.GetDataRow(i) as DataRow;

                        if (grdMappingList.View.IsNewItemRow(i))
                            continue;

                        /*
                         * Items[0] = InspectionClassID
                         * Items[1] = InspectionMethodID
                         * Items[2] = InspItemID
                         * Items[3] = SheetIndex
                         * Items[4] = RowIndex
                         */
                        if (r["INSPECTIONCLASSID"].ToString() == Items[0] && r["INSPECTIONMETHODID"].ToString() == Items[1] &&
                            r["INSPITEMID"].ToString() == Items[2] && i == Items[4].ToSafeInt32())
                        {
                            //현재 활성화된 WorkSheet Index
                            grdMappingList.View.SetRowCellValue(i, "SHEETINDEX", (this.sprTemplate.Document.Worksheets.ActiveWorksheet.Index + 1).ToString());

                            //일치하는 RowHandle
                            rowIndex = i;
                            //매핑이 되어있지 않은 행이 있으면 Loop 종료 후 해당 행에 매핑한다.
                            if (grdMappingList.View.GetFocusedRowCellValue("ROWSTART").ToSafeString().Equals(string.Empty))
                                break;
                        }
                    }
                    //매핑 할 행이 없으면 Return
                    if (rowIndex < 0)
                        return;

                    //Mapping
                    grdMappingList.View.SetRowCellValue(rowIndex, "COLUMNSTART", GetExcelColumnName(l[0].First<DevExpress.Spreadsheet.Cell>().ColumnIndex.ToString()));
                    grdMappingList.View.SetRowCellValue(rowIndex, "COLUMNEND", GetExcelColumnName(l[0].Last<DevExpress.Spreadsheet.Cell>().ColumnIndex.ToString()));
                    grdMappingList.View.SetRowCellValue(rowIndex, "ROWSTART", (l[0].First<DevExpress.Spreadsheet.Cell>().RowIndex + 1).ToString());
                    grdMappingList.View.SetRowCellValue(rowIndex, "ROWEND", (l[0].Last<DevExpress.Spreadsheet.Cell>().RowIndex + 1).ToString());

                    //RowState 변경
                    SelectedRowIndex = rowIndex;
                    grdMappingList.View.RaiseValidateRow(SelectedRowIndex);
                    grdMappingList.View.GetDataRow(SelectedRowIndex).AcceptChanges();
                    if (grdMappingList.View.GetDataRow(SelectedRowIndex).RowState == DataRowState.Unchanged)
                    {
                        if (grdMappingList.View.GetDataRow(SelectedRowIndex)["MAPPINGSEQ"].ToSafeInt32() == 0)
                            grdMappingList.View.GetDataRow(SelectedRowIndex).SetAdded();
                        else
                            grdMappingList.View.GetDataRow(SelectedRowIndex).SetModified();
                    }
                    grdMappingList.View.PostEditor();
                    return;
                }
                //이미지 사이즈 지정
                else if (item.Index == (_menuItems.Length / 2) - 1 && item.Tag.ToString().Equals("ImageSize"))
                {
                    //Range
                    IList<DevExpress.Spreadsheet.Range> l = sprTemplate.GetSelectedRanges();
                    /*
                    IList<DevExpress.Spreadsheet.Range> MergedCells = l[0].GetMergedRanges();
                    //활성화된 행이 없으면 Return
                    //머지된셀은 하나로 보게끔 수정할것
                    int mergedLeftIndex = l[0].LeftColumnIndex;
                    int mergedRightIndex = l[0].RightColumnIndex;
                    int mergedTopIndex = l[0].TopRowIndex;
                    int mergedBottonIndex = l[0].BottomRowIndex;
                    int AllMergedColumnCount = 0;
                    int AllMergedRowCount = 0;
                    int MergedColumnCount = 0;
                    int MergedRowCount = 0;

                    for (int k = 0; k < MergedCells.Count; k++)
                    {
                        if (mergedLeftIndex == MergedCells[k].LeftColumnIndex && mergedTopIndex == MergedCells[k].TopRowIndex)
                        {
                            AllMergedColumnCount += MergedCells[k].ColumnCount;
                            AllMergedRowCount += MergedCells[k].RowCount;
                            MergedRowCount++;
                            MergedColumnCount++;
                            continue;
                        }

                        if (mergedRightIndex == MergedCells[k].RightColumnIndex && mergedTopIndex == MergedCells[k].TopRowIndex)
                        {
                            AllMergedColumnCount += MergedCells[k].ColumnCount;
                            AllMergedRowCount += MergedCells[k].RowCount;
                            MergedRowCount++;
                            MergedColumnCount++;
                            continue;
                        }
                    }
                    

                    int ColCount = 1;
                    int RowCount = 1;
                    for (int row = l[0].First<DevExpress.Spreadsheet.Cell>().RowIndex; row < l[0].Last<DevExpress.Spreadsheet.Cell>().RowIndex; row++)
                    {
                        if (sprTemplate.Document.Worksheets.ActiveWorksheet.Range[GetExcelColumnName(l[0].First<DevExpress.Spreadsheet.Cell>().ColumnIndex.ToString()) + row.ToString() + ":" + GetExcelColumnName(l[0].First<DevExpress.Spreadsheet.Cell>().ColumnIndex.ToString()) + row + 1.ToString()].IsMerged)
                            continue;
                        else
                            RowCount++;
                    }
                    for (int col = l[0].First<DevExpress.Spreadsheet.Cell>().ColumnIndex; col < l[0].Last<DevExpress.Spreadsheet.Cell>().ColumnIndex; col++)
                    {

                        if (sprTemplate.Document.Worksheets.ActiveWorksheet.Range[GetExcelColumnName(col.ToString()) + l[0].First<DevExpress.Spreadsheet.Cell>().RowIndex.ToString() + ":" + GetExcelColumnName((col).ToString()) + l[0].First<DevExpress.Spreadsheet.Cell>().RowIndex.ToString()].GetMergedRanges().Count > 0)
                            col++;
                        else
                            ColCount++;
                    }
                    */

                    SelectedRowIndex = grdMappingList.View.GetFocusedDataSourceRowIndex();
                    if (SelectedRowIndex < 0)
                        return;

                    //Mapping
                    //grdMappingList.View.SetRowCellValue(SelectedRowIndex, "IMAGECOLUMNRANGE", (l[0].ColumnCount).ToString());
                    //grdMappingList.View.SetRowCellValue(SelectedRowIndex, "IMAGEROWRANGE", (l[0].RowCount).ToString());
                    ((DataRow)grdMappingList.View.GetDataRow(SelectedRowIndex))["IMAGECOLUMNRANGE"] = l[0].ColumnCount.ToString();
                    ((DataRow)grdMappingList.View.GetDataRow(SelectedRowIndex))["IMAGEROWRANGE"] = l[0].RowCount.ToString();
                    grdMappingList.View.PostEditor();
                    //RowState 변경
                    if (grdMappingList.View.GetDataRow(SelectedRowIndex).RowState == DataRowState.Unchanged)
                    {
                        if (grdMappingList.View.GetDataRow(SelectedRowIndex)["MAPPINGSEQ"].ToSafeInt32() == 0)
                            grdMappingList.View.GetDataRow(SelectedRowIndex).SetAdded();
                        else
                            grdMappingList.View.GetDataRow(SelectedRowIndex).SetModified();
                    }
                    grdMappingList.View.PostEditor();
                }
                else
                {
                    //고정 항목 매핑 (Model, PartNo, 검사일 등)
                    //컨트롤 중 Tag에 값이 있는 경우 일치하는 컨트롤에 값을 부여한다
                    //Tag = 컬럼명

                    //텍스트박스의 Parent 컨트롤 배열에서 해당 TextBox 찾음
                    SmartSplitTableLayoutPanel[] smartSplitTableLayoutPanels = { LayoutPanel1, LayoutPanel2 };
                    SmartTextBox textbox = null;

                    string ReferenceA1 = "";
                    foreach (Control control in smartSplitTableLayoutPanels)
                    {
                        foreach (Control c in control.Controls)
                        {
                            //TextBox인것만
                            if (c.GetType().Equals(typeof(SmartTextBox)))
                            {
                                if (c.Name.Equals(item.Tag.ToString()))
                                {
                                    c.Text = string.Empty;
                                    //Cell Merge된 경우 대표 Address
                                    if (sprTemplate.ActiveCell.IsMerged)
                                    {
                                        IList<DevExpress.Spreadsheet.Range> l = sprTemplate.ActiveCell.GetMergedRanges();
                                        ReferenceA1 = l[0].First<DevExpress.Spreadsheet.Cell>().GetReferenceA1();
                                    }
                                    else
                                        ReferenceA1 = sprTemplate.ActiveCell.GetReferenceA1();

                                    //변경여부
                                    _isEdit = true;
                                    textbox = (SmartTextBox)c;
                                }
                            }
                        }
                    }
                    //일치하는 Control에 주소값 매핑
                    textbox.Text = ReferenceA1;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        #region Etc
        /// <summary>
        /// 스프레드시트 워크시트 변경 시 변경된 내용 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpnSheetIndex_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (sprTemplate.Document.Worksheets.Count < e.NewValue.ToSafeInt32())
            {
                e.Cancel = true;
                return;
            }
            //이벤트 중복 방지
            sprTemplate.ActiveSheetChanging -= SprTemplate_ActiveSheetChanging;
            try
            {
                int ActiveSheetIndex = e.NewValue.ToSafeInt32() - 1;
                if (_isEdit)
                {
                    // 변경된 내용을 저장하겠습니까
                    if (this.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave").Equals(DialogResult.Yes))
                    //if(MessageBox.Show("InfoPopupSave", "InfoPopupSave", MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
                    {
                        //변경이 완료되기 전 OldValue로 저장해야함
                        if (!SaveData(e.OldValue.ToSafeInt32() - 1, false))
                        {
                            //sprTemplate.Document.Worksheets.ActiveWorksheet = sprTemplate.Document.Worksheets[e.OldValue.ToSafeInt32() - 1];
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                //정보 Refresh
                SetBindingMappingInfo(CurrentDataRow["PRODUCTDEFID"].ToString(),
                                      CurrentDataRow["PRODUCTDEFVERSION"].ToString(),
                                      CurrentDataRow["PRODUCTCLASSID"].ToString(),
                                      ActiveSheetIndex, spnRevisionNo.Value.ToInt32() , false, false, false);
                sprTemplate.Document.Worksheets.ActiveWorksheet = sprTemplate.Document.Worksheets[ActiveSheetIndex];
            }
            catch(System.ArgumentOutOfRangeException Rex)
            {
                throw Framework.MessageException.Create(Rex.Message);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                sprTemplate.ActiveSheetChanging += SprTemplate_ActiveSheetChanging;
            }
        }

        /// <summary>
        /// 양식 Revision 변경 시 체크 (현재버전이 가장 최신인 경우 Cancel)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpnRevisionNo_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                //현재 Rev이 최상위 리비전인이 Check
                //변경한 버전이 최상위 버전일때 , 상위일때, 낮은 Rev Check 일때
                //최상위이면
                int TargetRevisionNo = Convert.ToInt32(e.NewValue);
                int CurrentRevisionNo = Convert.ToInt32(e.OldValue);
                if (_MaxRevisionNo + 2 <= TargetRevisionNo)
                {
                    btnRevNo.Enabled = false;
                    e.Cancel = true;
                }
                else if(_MaxRevisionNo + 1<= TargetRevisionNo)
                {
                    btnRevNo.Enabled = false;
                }
                else if (_MaxRevisionNo > TargetRevisionNo)
                {
                    //Message Y/N
                    btnRevNo.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ShipmentInspMappingPopup_Shown(object sender, EventArgs e)
        {
            SetPopupMenu();
        }
        private void ShipmentInspMappingPopup_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeGrid();
                spnRevisionNo.Value = CurrentDataRow["RevisionNo"].ToSafeInt32();
                if (spnRevisionNo.Value.ToSafeInt32() == 0)
                    spnRevisionNo.Value = 1;
                sprTemplate.Options.Behavior.Worksheet.Insert = DevExpress.XtraSpreadsheet.DocumentCapability.Disabled;
                sprTemplate.Options.Behavior.Worksheet.Insert = DevExpress.XtraSpreadsheet.DocumentCapability.Hidden;

                spnSheetIndex.Value = CurrentDataRow["StartSheetIndex"].ToSafeInt32() + 1;
                SetBindingMappingInfo(CurrentDataRow["PRODUCTDEFID"].ToString(),
                                      CurrentDataRow["PRODUCTDEFVERSION"].ToString(),
                                      CurrentDataRow["PRODUCTCLASSID"].ToString(),
                                      CurrentDataRow["StartSheetIndex"].ToSafeInt32(),
                                      spnRevisionNo.Value.ToInt32(), true, true);

                if (sprTemplate.Document.Worksheets.Count < CurrentDataRow["StartSheetIndex"].ToSafeInt32())
                        return;
                sprTemplate.Document.Worksheets.ActiveWorksheet = sprTemplate.Document.Worksheets[CurrentDataRow["StartSheetIndex"].ToSafeInt32()];
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
            finally
            {
                //Binding 후 이벤트 등록 
                sprTemplate.ActiveSheetChanging += SprTemplate_ActiveSheetChanging;
                spnSheetIndex.EditValueChanging += SpnSheetIndex_EditValueChanging;
                spnRevisionNo.EditValueChanging += SpnRevisionNo_EditValueChanging;
            }


        }

        /// <summary>
        /// 팝업창 닫을 때 양식파일 삭제 함
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShipmentInspMappingPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.DialogResult != DialogResult.None)
                    return;
                bool isAllSave = grdMappingList.GetChangedRows().Rows.Count > 0 ? true : false;
                if (_isEdit || isAllSave)
                {
                    //DialogResult dr = MessageBox.Show("InfoPopupSave", "InfoPopupSave", MessageBoxButtons.YesNo);
                    DialogResult dr = this.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave");
                    // 변경된 내용을 저장하겠습니까
                    if (dr.Equals(DialogResult.Yes))
                    {
                        SaveData(spnSheetIndex.Value.ToSafeInt32() - 1, isAllSave);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
                else
                    this.DialogResult = DialogResult.OK;
                try
                {
                    if (sprTemplate.CreateNewDocument())
                    {
                        if (System.IO.Directory.Exists(_tempFilePath))
                        {
                            System.IO.Directory.Delete(_tempFilePath, true);
                        }
                        GC.Collect();
                    }
                }
                catch (Exception ex)
                {
                    throw Framework.MessageException.Create(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Private Function
        #region Context Menu
        /// <summary>
        /// 스프레드 시트 컨텍스트 메뉴 설정
        /// </summary>
        private void SetPopupMenu()
        {
            _menuItems = this.SetMenuList();
            //_popupMenu = new ContextMenu();
            _popupMenu.MenuItems.Clear();
            MenuItem Item = new MenuItem();

            for (int i = 0; i < _menuItems.Length / 2; i++)
            {
                Item = new MenuItem();
                Item.Text = _menuItems[i, 0];
                Item.Tag = _menuItems[i, 1];
                Item.Click += ContextMenuItem_Click;

                _popupMenu.MenuItems.Add(Item);
            }
        }

        /// <summary>
        /// 스프레드시트 컨텍스트 메뉴 아이템 설정(양식 고정값 추가시 여기에 설정)
        /// </summary>
        /// <returns>메뉴아이템 명칭 배열</returns>
        private string[,] SetMenuList()
        {
            /*메뉴추가*/
            //다국어 (Text : string.Format("{0} 항목 좌표로 지정", 라벨텍스트), Key : 텍스트박스명)
            //컨트롤 Tag에 컬럼값 지정
            return new string[,]
                    {
                        { string.Format(_ContextMenuText_MappingInfo, lblModel.Text), txtModelAddr.Name},
                        { string.Format(_ContextMenuText_MappingInfo, lblPartNo.Text), txtPartNoAddr.Name},
                        { string.Format(_ContextMenuText_MappingInfo, lblPartName.Text), txtPartNameAddr.Name},
                        { string.Format(_ContextMenuText_MappingInfo, lblProductDefVersion.Text), txtProductDefVersionAddr.Name},
                        { string.Format(_ContextMenuText_MappingInfo, lblLotNo.Text), txtLotNoAddr.Name},
                        { string.Format(_ContextMenuText_MappingInfo, lblLotSize.Text), txtLotSizeAddr.Name},
                        { string.Format(_ContextMenuText_MappingInfo, lblInspectionDate.Text), txtInspectionDateAddr.Name},
                        { string.Format(_ContextMenuText_MappingInfo, lblDueDate.Text), txtDueDateAddr.Name },
                        { string.Format(_ContextMenuText_MappingInfo, lblInspectionResult.Text), txtInspectionResultAddr.Name },
                        { string.Format(_ContextMenuText_MappingInfo, lblInspector.Text), txtInspectorAddr.Name},
                        { string.Format(_ContextMenuText_MappingInfo, lblWeek.Text), txtWeekAddr.Name },
                        { string.Format(_ContextMenuText_MappingInfo, lblDegree.Text), txtDegreeAddr.Name},
                        { string.Format(_ContextMenuText_MappingInfo, lblPackingDate.Text), txtPackingDateAddr.Name },
                        //현위치에 추가하세요. 마지막두개는 고정
                        { _ContextMenuText_MappingItem, "InspectionItem"}, //검사항목 좌표로 지정
                        { _ContextMenuText_ImageSize, "ImageSize" } //이미지 사이즈 지정
                    };
        }
        #endregion

        #region Data Binding
        /// <summary>
        /// 매핑데이터 바인딩
        /// </summary>
        /// <param name="SheetIndex">시트 Index (헤더정보 바인딩용)</param>
        /// <param name="isDocumentDownload">양식파일 다운로드 여부(처음 열때 다운로드 / 저장 후 Refresh할땐 다운로드 안함)</param>
        /// <param name="isRefreshGrid">전체저장 완료 시 검사항목 매핑정보 Refresh</param>
        private void SetBindingMappingInfo(string ProductDefID, string ProductDefVersion, string ProductClassID, int SheetIndex, int RevisionNo, bool isDocumentDownload, bool isRefreshGrid, bool isCopy = false)
        {
            try
            {
                sprTemplate.Document.History.Undo();
                txtCustomerName.Text = CurrentDataRow["CustomerName"].ToSafeString();
                txtProductDefID.Text = CurrentDataRow["ProductDefID"].ToSafeString();

                //int RevisionNo = spnRevisionNo.Value.ToInt32();

                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    {"PRODUCTDEFID", ProductDefID },
                    {"PRODUCTDEFVERSION", ProductDefVersion},
                    {"PRODUCTCLASSID" , ProductClassID},
                    {"REVISIONNO" , RevisionNo},
                    {"SHEETINDEX" , SheetIndex},
                    {"LANGUAGETYPE", UserInfo.Current.LanguageType }
                };

                DataTable dtHeader = SqlExecuter.Query("GetShipmentInspMappingHeader", "10001", param);
                SetMappingHeaderData(dtHeader);

                if (isRefreshGrid)
                {
                    DataTable dtItem = SqlExecuter.Query("GetMappingInspectionItem", "10001", param);
                    _dtShipmentInspectionType = SqlExecuter.Query("GetCodeFromParentCodeClass", "10001", new Dictionary<string, object> { { "PARENTCODECLASSID", "ShipmentInspectionType" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
                    grdMappingList.DataSource = dtItem;

                    //Test(0, true);
                }

                _isEdit = false;

                if (isDocumentDownload)
                {
                    string FileID = txtFileName.Tag.ToSafeString() + System.IO.Path.GetExtension(txtFileName.Text);
                    string LocalFilePath = DownloadDocument(FileID, txtFileName.Text);
                    if (System.IO.File.Exists(LocalFilePath))
                    {
                        sprTemplate.Document.LoadDocument(LocalFilePath);

                        if (SheetIndex < 0)
                        {
                            int ActiveSheetIndex = dtHeader.Rows.Count > 0 ? dtHeader.Rows[0]["MinSheetIndex"].ToSafeInt32() : 0;
                            sprTemplate.Document.Worksheets.ActiveWorksheet = sprTemplate.Document.Worksheets[ActiveSheetIndex];
                            spnSheetIndex.Value = ActiveSheetIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 매핑 헤더정보 바인딩
        /// </summary>
        /// <param name="dtHeaderMappingData"></param>
        private void SetMappingHeaderData(DataTable dtHeaderMappingData)
        {
            try
            {
                DataRow headerData = null;
                if (dtHeaderMappingData.Rows.Count > 0)
                {
                    headerData = dtHeaderMappingData.Rows[0];
                    txtFileName.Text = headerData["FileName"].ToSafeString();
                    txtFileName.Tag = headerData["FileID"].ToSafeString();
                    txtModel.Text = headerData["ModelValue"].ToSafeString();
                    txtPartNo.Text = headerData["PartNoValue"].ToSafeString();
                    txtPartName.Text = headerData["PartNameValue"].ToSafeString();
                    txtProductDefVersion.Text = headerData["ProductDefVersionValue"].ToSafeString();
                    _MaxRevisionNo = headerData["MaxRevisionNo"].ToSafeInt32();
                    lblMaxRevision.Text = string.Format(_MaxRevisionNoLanguageKey, _MaxRevisionNo.ToString());
                }
                SmartSplitTableLayoutPanel[] smartSplitTableLayoutPanels = { LayoutPanel1, LayoutPanel2 };

                foreach (Control control in smartSplitTableLayoutPanels)
                {
                    foreach (Control c in control.Controls)
                    {
                        if (c.Tag != null && c.Tag.ToSafeString() != string.Empty)
                        {
                            foreach (DataColumn col in dtHeaderMappingData.Columns)
                            {
                                if (col.ColumnName.Equals(c.Tag.ToString()))
                                {
                                    if (dtHeaderMappingData.Rows.Count > 0)
                                    {
                                        if (c == spnSheetIndex)
                                            continue;
                                        else
                                            c.Text = headerData[col.ColumnName].ToSafeString();
                                    }
                                    else
                                    {
                                        if (c == spnSheetIndex)
                                            continue;
                                        else
                                            c.Text = "";
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region File Control
        /// <summary>
        /// 양식파일 다운로드
        /// </summary>
        /// <param name="sourceFileName">DB에 저장 된 양식파일ID (FileID)</param>
        /// <param name="targetFileName">다운로드 할 파일명 (FileName)</param>
        /// <returns>다운로드된 파일 Local Full Path</returns>
        private string DownloadDocument(string sourceFileName, string targetFileName)
        {
            try
            {
                if (targetFileName.Equals(string.Empty))
                    return "";

                if (sprTemplate.CreateNewDocument())
                {
                    if (System.IO.Directory.Exists(_tempFilePath))
                    {
                        System.IO.Directory.Delete(_tempFilePath, true);
                    }
                    GC.Collect();
                }
                //Common Framework 참조
                using (System.Net.WebClient web = new System.Net.WebClient())
                {
                    if (!System.IO.Directory.Exists(_tempFilePath))
                        System.IO.Directory.CreateDirectory(_tempFilePath);

                    string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                    string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));
                    string ServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url")) + "ShipmentMappingDocument";
                    ServerPath = ServerPath + ((ServerPath.EndsWith("/")) ? "" : "/") + sourceFileName;
                    web.Credentials = new System.Net.NetworkCredential(ftpServerUserId, ftpServerPassword);

                    web.DownloadFile(ServerPath, string.Join("", _tempFilePath, targetFileName));
                }

                return _tempFilePath + targetFileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 양식파일 업로드
        /// </summary>
        /// <param name="fileID">(품목ID)-(품목REV)-(품목CLASS)-(양식REVISION)</param>
        /// <param name="fileName">양식 파일명</param>
        /// <param name="localFilePath">양식파일 경로</param>
        /// <param name="fileExt">양식파일 확장자</param>
        /// <param name="fileSize">양식파일 크기</param>
        /// <returns>성공시 true : false</returns>
        private bool UploadDocument(string fileID, string fileName, string localFilePath, string fileExt, int fileSize)
        {
            try
            {
                DataTable fileUploadTable = new DataTable("File");

                fileUploadTable.Columns.Add("FILENAME");
                fileUploadTable.Columns.Add("FILEEXT");
                fileUploadTable.Columns.Add("FILESIZE");
                fileUploadTable.Columns.Add("FILEPATH");
                fileUploadTable.Columns.Add("LOCALFILEPATH");
                fileUploadTable.Columns.Add("PROCESSINGSTATUS");

                DataRow fileRow = fileUploadTable.NewRow();

                fileRow["FILENAME"] = fileID;
                fileRow["FILEEXT"] = fileExt.Replace(".", "");
                fileRow["FILESIZE"] = fileSize;
                fileRow["FILEPATH"] = "ShipmentMappingDocument";
                fileRow["PROCESSINGSTATUS"] = "Wait";
                fileRow["LOCALFILEPATH"] = localFilePath;

                fileUploadTable.Rows.Add(fileRow);

                Commons.Controls.FileProgressDialog fileProgressDialog = new Commons.Controls.FileProgressDialog(fileUploadTable, Commons.Controls.UpDownType.Upload, "", fileSize);
                fileProgressDialog.ShowDialog(this);

                if (fileProgressDialog.DialogResult.Equals(DialogResult.Cancel))
                {
                    return false;
                }
                Commons.Controls.ProgressingResult fileResult = fileProgressDialog.Result;

                if (!fileResult.IsSuccess)
                {
                    fileProgressDialog.Close();
                    throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
                }
                return fileResult.IsSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Set Data
        /// <summary>
        /// 헤더 매핑정보 가져오기
        /// </summary>
        /// <param name="SheetIndex">활성화된 현재 SheetIndex (WorkSheet 체인지 이벤트로인해 넘어오는 경우 OldValue / NewValue 구분해서 넘겨야 함)</param>
        /// <returns>DataTable</returns>
        private DataTable GetMappingHeaderData(int SheetIndex)
        {
            DataTable dtHeader = new DataTable("Header");
            /*------------------------------------------------------------------------------------------------------
             * 
             * 헤더정보는 각 TextBox의 Tag에 컬럼값을 지정 하여 자동으로 바인딩 하도록 개발 하였습니다.
             * 
             * 추가 시 TextBox를 추가 하고 각 Tag에 컬럼명을 지정 하여 일치 하면 Get/Set 자동으로 진행 합니다.
             * 
             *-----------------------------------------------------------------------------------------------------*/

            SmartSplitTableLayoutPanel[] smartSplitTableLayoutPanels = { LayoutPanel1, LayoutPanel2 };
            foreach (Control control in smartSplitTableLayoutPanels)
            {
                foreach (Control c in control.Controls)
                {
                    if (!c.Tag.ToSafeString().Equals(string.Empty))
                    {
                        dtHeader.Columns.Add(c.Tag.ToSafeString());
                    }
                }
            }
            //변경 사항이 있는 경우 저장데이터 GET
            if (_isEdit)
            {
                DataRow d = dtHeader.NewRow();
                foreach (Control control in smartSplitTableLayoutPanels)
                {
                    foreach (Control c in control.Controls)
                    {
                        if (!c.Tag.ToSafeString().Equals(string.Empty))
                        {
                            //numeric Editor는 별도로
                            if (c.Equals(spnSheetIndex))
                                d[c.Tag.ToSafeString()] = SheetIndex;
                            else
                                d[c.Tag.ToSafeString()] = c.Text;
                        }
                    }
                }

                dtHeader.Rows.Add(d);
            }
            else
            {
                DataRow d = dtHeader.NewRow();
                d["SHEETINDEX"] = SheetIndex;
                dtHeader.Rows.Add(d);
            }

            return dtHeader;
        }

        /// <summary>
        /// 항목 매핑정보 가져오기
        /// </summary>
        /// <param name="isAllSave"></param>
        /// <returns></returns>
        private DataTable GetMappingInspectionItem(bool isAllSave, bool isRevision)
        {
            DataTable dtItem = ((DataTable)grdMappingList.DataSource).Clone();
            dtItem.TableName = "Item";
            grdMappingList.View.PostEditor();
            if (isAllSave)
            {
                //grdMappingList.View.SetColumnError(grdMappingList.View.Columns["SHIPMENTINSPECTIONTYPE"], "SELECT RESOURCE", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information);
                grdMappingList.View.PostEditor();
                string ErrorMessage = "";
                
                //grdMappingList.View.CheckValidation();
                for (int i = 0; i < grdMappingList.View.RowCount; i++)
                {
                    ErrorMessage = "";
                    grdMappingList.View.GetDataRow(i).ClearErrors();
                    grdMappingList.View.ClearColumnErrors();
                    if (grdMappingList.View.IsDeletedRow(grdMappingList.View.GetDataRow(i)))
                        continue;
                    string InspectionClassID = grdMappingList.View.GetRowCellValue(i, "INSPECTIONCLASSID").ToString();

                    //자원유형 ComboBox에 DataSource가 있으나 선택 안한 경우 Throw
                    if(_dtShipmentInspectionType.AsEnumerable().Where(w=>w.Field<string>("CODECLASSID").Equals(InspectionClassID)).Count() > 0)
                    {
                        if(grdMappingList.View.GetRowCellDisplayText(i, "SHIPMENTINSPECTIONTYPE").ToString().Equals(string.Empty))
                        {
                            ErrorMessage = string.Format(Framework.MessageException.Create("InValidRequiredField").Message, grdMappingList.View.Columns["SHIPMENTINSPECTIONTYPE"].Caption);
                            grdMappingList.View.GetDataRow(i).SetColumnError("SHIPMENTINSPECTIONTYPE", ErrorMessage);
                            throw new Exception(ErrorMessage);
                        }
                    }
                    for(int j = 0; j < grdMappingList.View.Columns.Count; j++)
                    {
                        if (grdMappingList.View.GetRowCellDisplayText(i, grdMappingList.View.Columns[j]).ToString().Equals(string.Empty))
                        {
                            //필수항목
                            if (grdMappingList.View.Columns[j].AppearanceHeader.ForeColor == Color.Red)
                            {
                                ErrorMessage = string.Format(Framework.MessageException.Create("InValidRequiredField").Message, grdMappingList.View.Columns[j].Caption);
                                grdMappingList.View.GetDataRow(i).SetColumnError(grdMappingList.View.Columns[j].FieldName, ErrorMessage);
                                throw new Exception(ErrorMessage);
                            }
                        }
                    }

                    dtItem.ImportRow(grdMappingList.View.GetDataRow(i));
                }
                grdMappingList.View.CloseEditor();
                grdMappingList.View.CheckValidation();
                ////////저장 시 삭제건 제외하고 저장하도록 수정할것.
            }
            return dtItem;
        }
        #endregion
        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {
            
        }

        /// <summary>
        /// 스프레드 시트의 컬럼 인덱스를 문자 주소로 변경
        /// </summary>
        /// <param name="ColumnIndex">Column Index</param>
        /// <returns>컬럼 인덱스 문자값(1 -> A, 2 -> B, 27 -> AA~)</returns>
        private string GetExcelColumnName(string ColumnIndex)
        {
            int CheckIndex = 0;
            if (!Int32.TryParse(ColumnIndex.ToString(), out CheckIndex))
                return ColumnIndex.ToString();

            CheckIndex = Convert.ToInt32(ColumnIndex);

            int ColumnNo = CheckIndex + 1;
            string ColumnName = String.Empty;
            int Modulo;

            while (ColumnNo > 0)
            {
                Modulo = (ColumnNo - 1) % 26;
                ColumnName = Convert.ToChar(65 + Modulo).ToString() + ColumnName;
                ColumnNo = (int)((ColumnNo - Modulo) / 26);
            }

            return ColumnName;
        }
        
        /// <summary>
        /// 매핑데이터 저장
        /// </summary>
        /// <param name="SheetIndex">현재 Active Sheet Index (시트 체인지 이벤트로인해 넘어오는 경우 OldValue / NewValue 구분해서 넘겨야 함)</param>
        /// <param name="isAllSave">헤더정보만 저장하는 경우 false (시트 체인지 이벤트)</param>
        private bool SaveData(int SheetIndex, bool isAllSave)
        {
            try
            {
                int RevisionNo = 1;
                bool isRevision = false;
                if (spnRevisionNo.Value.Equals(0))
                {
                    spnRevisionNo.Value = 1;
                }

                RevisionNo = spnRevisionNo.Value.ToSafeInt32();
                DialogResult dr = DialogResult.Cancel;
                if (_MaxRevisionNo < spnRevisionNo.Value.ToSafeInt32())
                {
                    isRevision = true;
                    _isEdit = true;
                }
                DataTable dtHeader = GetMappingHeaderData(SheetIndex);
                DataTable dtItem = GetMappingInspectionItem(isAllSave, isRevision);
               
                int FileSize = CurrentDataRow["FILESIZE"].ToSafeInt32();
                string FileID = CurrentDataRow["FILEID"].ToString();
                string FileName = System.IO.Path.GetFileNameWithoutExtension(txtFileName.Text);
                string FileExt = System.IO.Path.GetExtension(txtFileName.Text);
                
                //신규 양식 파일인 경우
                if (txtFileName.Text.Contains('\\'))
                {
                    FileID = (CurrentDataRow["ProductDefID"] + "-" + CurrentDataRow["ProductDefVersion"] + "-" + CurrentDataRow["ProductClassID"] + "-" + RevisionNo.ToString() + Guid.NewGuid().ToString()).Substring(0,40);
                    FileSize = new System.IO.FileInfo(txtFileName.Text).Length.ToInt32();
                    if (UploadDocument(FileID, FileName, txtFileName.Text, FileExt, FileSize))
                    {
                        txtFileName.Text = System.IO.Path.GetFileName(txtFileName.Text);
                    }
                    else
                    {
                        if (!isAllSave)
                            sprTemplate.Document.Sheets.ActiveSheet = sprTemplate.Document.Sheets[spnSheetIndex.Value.ToSafeInt32() - 1];
                        
                        return false;
                        //업로드 실패 시
                    }
                }
                else
                {
                    FileID = txtFileName.Tag.ToSafeString();
                }
               
                MessageWorker messageWorker = new MessageWorker("SaveShipmentInspMapping");
                messageWorker.SetBody(new MessageBody()
                {
                    { "ProductDefID", CurrentDataRow["ProductDefID"]  },
                    { "ProductDefVersion", CurrentDataRow["ProductDefVersion"] },
                    { "ProductClassID", CurrentDataRow["ProductClassID"] },
                    { "CustomerID", CurrentDataRow["CustomerID"] },
                    { "FileName", FileName},
                    { "FileSize", FileSize},
                    { "FileID", FileID},
                    { "FileExt", FileExt},
                    { "RevisionNo", RevisionNo},
                    { "Header", dtHeader },
                    { "Item", dtItem}
                });

                var response = messageWorker.Execute<DataTable>();

                if (response.Success)
                {
                    if (isAllSave)
                    {
                        ShowMessage("SuccedSave");

                        SetBindingMappingInfo(CurrentDataRow["PRODUCTDEFID"].ToString(),
                                              CurrentDataRow["PRODUCTDEFVERSION"].ToString(),
                                              CurrentDataRow["PRODUCTCLASSID"].ToString(),
                                              SheetIndex,
                                              RevisionNo,
                                              false,
                                              true);
                        //MessageBox.Show("InfoPopupSave", "InfoPopupSave", MessageBoxButtons.OK);
                        //Refresh Inspection Item Mapping Info
                        //Dictionary<string, object> param = new Dictionary<string, object>
                        //{
                        //    {"PRODUCTDEFID", CurrentDataRow["PRODUCTDEFID"].ToString() },
                        //    {"PRODUCTDEFVERSION", CurrentDataRow["PRODUCTDEFVERSION"].ToString()},
                        //    {"PRODUCTCLASSID" , CurrentDataRow["PRODUCTCLASSID"].ToString()},
                        //    {"REVISIONNO" , RevisionNo},
                        //    {"SHEETINDEX" , SheetIndex},
                        //    {"LANGUAGETYPE" ,  UserInfo.Current.LanguageType}
                        //};
                        //dtItem = SqlExecuter.Query("GetMappingInspectionItem", "10001", param);
                        //grdMappingList.DataSource = dtItem;
                    }
                    _isEdit = false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}