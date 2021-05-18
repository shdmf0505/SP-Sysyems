#region using
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.QualityAnalysis.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 > 원자재/원자재가공품 수입검사 결과등록
    /// 업  무  설  명  : 원자재가공품 결과등록하는 팝업
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-07-31
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary
    public partial class RawAssyImportInspectionResultPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }
        
        #endregion

        #region Local Variables

        private DataTable _consumableInspectionTable; // 검색 한 inspectionResult 결과를 담을 테이블 
        private bool _hasImage = true;
        private DataTable _fileDt;
        private DataRow _toCopyRow = null;
        int _toCopyHandle = 0;
        private bool _autoChange = false;
        private string _type = "";
        private double _lotQty = 0.0;
        public bool isEnable = true;
        private double _inspectionQty;

        //2020-02-19 강유라 이메일 첨부용
        DataTable _FileToSendEmail;
        #endregion

        #region 생성자
        public RawAssyImportInspectionResultPopup(string type)
        {
            _type = type;
            InitializeComponent();
            InitializeGridDefect();
            InitializeGridMeasure(type);
            InitializationSummaryRow();
            InitializeEvent();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        #region 불량검사 탭 초기화
        private void InitializeGridDefect()
        {
            grdInspectionItem.GridButtonItem = GridButtonItem.None;
            grdInspectionItem.View.OptionsView.AllowCellMerge = true;

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

            grdInspectionItem.View.AddSpinEditColumn("INSPECTIONQTY", 150);

            grdInspectionItem.View.AddSpinEditColumn("SPECOUTQTY", 150);

            grdInspectionItem.View.AddTextBoxColumn("DEFECTRATE", 150)
                .SetIsReadOnly()
               // .SetDisplayFormat("{###.#:P0}", MaskTypes.Numeric);
               .SetDisplayFormat("###.#", MaskTypes.Numeric);


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
        }
        #endregion

        #region 측정검사 탭 초기화
        private void InitializeGridMeasure(string _type)
        {
            grdInspectionItemSpec.View.ClearColumns();

            if (_type.Equals("insertData"))
            {//이미등록된 결과가 없을 때
                grdInspectionItemSpec.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            }
            else
            {
                grdInspectionItemSpec.GridButtonItem = GridButtonItem.None;
            }

            grdInspectionItemSpec.View.OptionsView.AllowCellMerge = true;

            grdInspectionItemSpec.View.SetSortOrder("SORT");

            //grdInspectionItemSpec.View.AddTextBoxColumn("TYPE", 150)
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

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMNAME", 200)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("UOMDEFID", 80)
                .SetLabel("UNIT")
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMVERSION", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMTYPE", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddSpinEditColumn("MEASUREVALUE", 100)
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
        }
        #endregion

        /// <summary>
        /// 합계 Row 초기화
        /// </summary>
        private void InitializationSummaryRow()
        {
            grdInspectionItem.View.Columns["INSPITEMNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInspectionItem.View.Columns["INSPITEMNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdInspectionItem.View.Columns["INSPECTIONQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
            grdInspectionItem.View.Columns["INSPECTIONQTY"].SummaryItem.DisplayFormat = "{0}";
            grdInspectionItem.View.Columns["SPECOUTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInspectionItem.View.Columns["SPECOUTQTY"].SummaryItem.DisplayFormat = "{0}";
            grdInspectionItem.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;//***불량율 합계는 100넘을 수있음 다시계산?***
            grdInspectionItem.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:f2} %";

            grdInspectionItem.View.OptionsView.ShowFooter = true;
            grdInspectionItem.ShowStatusBar = false;
        }
        /// <summary>
        /// 의뢰자, 검사자를 선택하는 팝업
        /// </summary>
        public void UserPopup()
        {
            popRequesterUser.SelectPopupCondition = CreateUserPopup();
            popInspectionUser.SelectPopupCondition = CreateUserPopup();
        }

        private ConditionItemSelectPopup CreateUserPopup()
        {
            ConditionItemSelectPopup conditionItem = new ConditionItemSelectPopup();
            conditionItem.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            conditionItem.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true);
            conditionItem.Id = "INSPECTIONUSER";
            conditionItem.LabelText = "INSPECTIONUSER";
            conditionItem.SearchQuery = new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            conditionItem.IsMultiGrid = false;
            conditionItem.DisplayFieldName = "USERNAME";
            conditionItem.ValueFieldName = "USERNAME";
            conditionItem.LanguageKey = "INSPECTIONUSER";

            conditionItem.Conditions.AddTextBox("USERIDNAME");

            conditionItem.GridColumns.AddTextBoxColumn("USERID", 150);
            conditionItem.GridColumns.AddTextBoxColumn("USERNAME", 200);

            return conditionItem;
        }

        #endregion

        #region Event
        private void InitializeEvent()
        {
            //팝업 로드 이벤트
            this.Load += RawAssyImportInspectionResultPopup_Load;
            //특정 컬럼만 Merge하는 이벤트 
            grdInspectionItem.View.CellMerge += View_CellMerge;
            grdInspectionItemSpec.View.CellMerge += View_CellMerge;

            //불량율 합계 이벤트
            grdInspectionItem.View.CustomSummaryCalculate += View_CustomSummaryCalculate;
            //검사수량 및 불량수량을 입력하면 불량율을계산 해주는 이벤트
            grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
            //불량율이 바뀔 때  판정결과를 자동판정하는 이벤트
            grdInspectionItem.View.CellValueChanged += GrdDefectRate_CellValueChanged;
            //측정값을 입력하면 스펙체크를 하여 판정결과를 자동판정하는 이벤트
            grdInspectionItemSpec.View.CellValueChanged += View_CellValueChangedSpecCheck;

            //소분류의 판정결과에 따라 대분류의 판정결과를 자동판정하는 이벤트
            //grdInspectionItem.View.CellValueChanged += View_CellValueChangedItemResult;
            //grdInspectionItemSpec.View.CellValueChanged += View_CellValueChangedItemResult;

            //대분류의 판정결과에 따라 전체 판정결과를 자동판정하는 이벤트
            //grdInspectionItem.View.CellValueChanged += View_CellValueChangedClassItemResult;
            //grdInspectionItemSpec.View.CellValueChanged += View_CellValueChangedClassItemResult;

            //검사타입에 따라 컬럼(측정값,판정결과)을 readonly 처리하는 이벤트 -> 그리드 컬럼 READONLY
            grdInspectionItem.View.ShowingEditor += View_ShowingEditor;
            grdInspectionItemSpec.View.ShowingEditor += View_ShowingEditor;
            //grdDefectInspection.View.ShowingEditor += InspItemClass_ReadOnly;
            //grdMeasureInspection.View.ShowingEditor += InspItemClass_ReadOnly;

            //팝업저장버튼을 클릭시 이벤트
            btnSave.Click += BtnSave_Click;
            //팝업닫기버튼을 클릭시 이벤트
            btnClose.Click += (s, e) => 
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            //grdDefectInspection 입력된 값은 수정하지 못하게 하는 이벤트
            grdInspectionItem.View.ShowingEditor += View_CantChangeResult;
            grdInspectionItemSpec.View.ShowingEditor += View_CantChangeResult;

            //이미지 추가 버튼 클릭 이벤트
            btnAddImage.Click += BtnAddImage_Click;
            //이미지 삭제 이벤트
            picDefect.KeyDown += PicDefect_KeyDown;
            picDefect2.KeyDown += PicDefect_KeyDown;
            
            //측정검사 Row + 이벤트
            grdInspectionItemSpec.ToolbarAddingRow += GrdInspectionItemSpec_ToolbarAddingRow;
            grdInspectionItemSpec.View.FocusedRowChanged += (s, e) =>
            {

                int presentHandle = e.FocusedRowHandle;
                if (e.PrevFocusedRowHandle >= 0)
                {
                    _toCopyRow = null;
                    _toCopyRow = grdInspectionItemSpec.View.GetDataRow(e.PrevFocusedRowHandle);
                    _toCopyHandle = e.PrevFocusedRowHandle;
                }
            };

            //측정검사 +하여 항목 추가하는 이벤트
            grdInspectionItemSpec.View.AddingNewRow += View_AddingNewRow;
            grdInspectionItemSpec.ToolbarDeleteRow += GrdInspectionItemSpec_ToolbarDeleteRow;
            grdInspectionItemSpec.View.RowStyle += View_RowStyle;

            //판정결과가 바뀔때 전체 판정결과를 판정하는 이벤트
            grdInspectionItem.View.CellValueChanged += View_CellValueChangedAllResult;
            grdInspectionItemSpec.View.CellValueChanged += View_CellValueChangedAllResult;

            //NG 빨간색 처리 이벤트
            grdInspectionItemSpec.View.RowCellStyle += View_RowCellStyle;
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
        /// 로드 이벤트
        /// 팝업내의 팝업초기화 및 데이터로드
        /// 컨트롤 설정(그리드 확대 / 축소 버튼 안보이게)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RawAssyImportInspectionResultPopup_Load(object sender, EventArgs e)
        {

            btnSave.Enabled = isEnable;

            fpcImage.LanguageKey = "IMAGE";
            InitializeImageFileControl();
            grpMaterialInfo.GridButtonItem = GridButtonItem.None;
            grpInspectionInfo.GridButtonItem = GridButtonItem.None;

            picDefect.Properties.ShowMenu = false;
            picDefect2.Properties.ShowMenu = false;

            if (_type.Equals("insertData"))
            {//이미등록된 결과가 없을 때
                grdInspectionItem.View.FocusedRowChanged += FocusedRowChangedBeforeSave;
                txtInspectionResult.EditValue = "OK";
            }
            else
            {
                grdInspectionItem.View.FocusedRowChanged += View_FocusedRowChanged;
            }

            //2019-12-13 lotQty 할당 
            _lotQty = CurrentDataRow["QTY"].ToSafeDoubleZero();

            OnSearch();
        }

        /// <summary>
        /// 각각의 판정결과에 의해 전체 검사결과를 판정하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChangedAllResult(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;
            DataRow row = view.GetDataRow(e.RowHandle);

            if (e.Column.FieldName == "INSPECTIONRESULT")
            {
                string sequence = "";

                if (row["NCRDECISIONDEGREE"] == null)
                {
                    ShowMessage("NoActionStandardData");//판정등급이 없습니다.
                    return;
                }

                if(row["INSPITEMTYPE"].ToString().Equals("SPC"))
                { 
                    string qcgrade = InspectionHelper.GetQcGradeAndSequenceNCRAndSpecType("RawInspection", row["NCRDECISIONDEGREE"].ToString(), out sequence);

                    row["QCGRADE"] = qcgrade;
                    row["PRIORITY"] = sequence;
                }
                DataTable exGrd = grdInspectionItem.DataSource as DataTable;
                DataTable specGrd = grdInspectionItemSpec.DataSource as DataTable;

                var exGrdResult = exGrd.AsEnumerable().Select(r => r["INSPECTIONRESULT"]).ToList();
                var specGrdResult = specGrd.AsEnumerable().Select(r => r["INSPECTIONRESULT"]).ToList();

                if (exGrdResult.Contains("NG") || specGrdResult.Contains("NG"))
                {
                    txtInspectionResult.EditValue = "NG";
                }
                else
                {
                    txtInspectionResult.EditValue = "OK";
                    txtInspectionResult.EditValue = "OK";
                }

            }

        }

        /// <summary>
        /// 불량률 계산
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                GridSummaryItem item = e.Item as GridSummaryItem;
                if (item.FieldName == "DEFECTRATE")
                {
                    double inspectionQty = 0;
                    double defectQty = 0;
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        inspectionQty = (sender as GridView).Columns["INSPECTIONQTY"].SummaryItem.SummaryValue.ToSafeDoubleZero();
                        defectQty = (sender as GridView).Columns["SPECOUTQTY"].SummaryItem.SummaryValue.ToSafeDoubleZero();
                        if (inspectionQty != 0)
                        {
                            decimal defectRate = Math.Round((defectQty / inspectionQty * 100).ToSafeDecimal(), 1);
                            e.TotalValue = defectRate;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 등록된 검사 결과가 있을 때 결과 수정 못함 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CantChangeResult(object sender, CancelEventArgs e)
        {
            if (_type.Equals("updateData"))
            {//이미등록된 결과가 있을 때
                e.Cancel = true;
            }
            else
            {
                if (grdInspectionItem.View.FocusedColumn.FieldName == "SPECOUTQTY")
                {
                    var inspectionQty = grdInspectionItem.View.GetRowCellValue(grdInspectionItem.View.FocusedRowHandle, "INSPECTIONQTY");
                    if (inspectionQty == null || string.IsNullOrWhiteSpace(inspectionQty.ToString()) || inspectionQty.ToSafeInt32() == 0)
                    {
                        e.Cancel = true;
                    }

                }
            }
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

        /// <summary>
        /// 포커스된 row에 등록된 이미지를 보여주는 이벤트(저장전)
        /// inspItem dt에 파일정보 저장하여 이미지 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FocusedRowChangedBeforeSave(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdInspectionItem.View.GetDataRow(e.FocusedRowHandle);
            if (row == null) return;

            //검사결과가 N이고, 대분류가 아닐 때
            if (row["INSPECTIONRESULT"].ToString().Equals("NG"))
            {
                picDefect.Image = null;
                picDefect2.Image = null;
                //======================================================================================================================================
                //YJKIM : 파일저장전 로컬경로의 이미지를 보여줄 때 사용
                if (row["FILEFULLPATH1"].ToString() == string.Empty && row["FILENAME1"].ToString() == string.Empty
                        && row["FILEFULLPATH2"].ToString() == string.Empty && row["FILENAME2"].ToString() == string.Empty)
                {
                    return;
                }

                ImageConverter converter = new ImageConverter();

                if (!string.IsNullOrWhiteSpace(row["FILEFULLPATH1"].ToString()))
                {
                    picDefect.Image = QcmImageHelper.GetImageFromFile(row["FILEFULLPATH1"].ToString());//(Image)converter.ConvertFrom(row["FILEFULLPATH1"]);
                }

                if (!string.IsNullOrWhiteSpace(row["FILEFULLPATH2"].ToString()))
                {
                    picDefect2.Image = QcmImageHelper.GetImageFromFile(row["FILEFULLPATH2"].ToString());//(Image)converter.ConvertFrom(row["FILEFULLPATH2"]);
                }
                //------------------------------------------------------------------------------------------------------------------------------------------
            }
            else
            {
                picDefect.Image = null;
                picDefect2.Image = null;
            }

        }

        /// <summary>
        /// 포커스된 row에 등록된 이미지를 보여주는 이벤트(저장후)
        /// lotid+inspItem 으로 파일정보 셀렉트하여 이미지 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdInspectionItem.View.GetDataRow(e.FocusedRowHandle);
            if (row == null) return;

            picDefect.Image = null;
            picDefect2.Image = null;

            //검사결과가 NG이고, 대분류가 아닐 때
            if (row["INSPECTIONRESULT"].ToString().Equals("NG"))
            {
                //5. 파일다운로드 부분 작성
                //====================================================================================================================================
                //YJKIM : 파일다운로드부분을 작성
                //Dictionary<string, object> values = new Dictionary<string, object>()
                //{
                //    {"FILEINSPECTIONTYPE","ArrivalRawMaterialInspection" },
                //    {"FILERESOURCEID",CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString() + row["INSPITEMID"]},
                //    {"PROCESSRELNO",CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString()}

                //};

                //_fileDt = SqlExecuter.Query("SelectInspectionResultImage", "10001", values);

                Dictionary<string, object> values = new Dictionary<string, object>()
                {
                    {"RESOURCETYPE", "ArrivalRawMaterialInspection" },
                    {"RESOURCEID", CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString() + row["INSPITEMID"]},
                    {"RESOURCEVERSION", "*"}
                };

                _fileDt = SqlExecuter.Query("GetFileHttpPathFromObjectFileByStandardInfo", "10001", values);
                foreach (DataRow fileRow in _fileDt.Rows)
                {
                    string filenameAndExt = fileRow.GetString("FILENAME") + "." + fileRow.GetString("FILEEXT");
                    if (picDefect.Image == null)
                    {   //2020-01-28 파일 경로변경           
                        picDefect.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                    }
                    else
                    {
                        picDefect2.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                    }

                }

            }
        }

        /// <summary>
        /// PictureBox에 delete버튼 클릭시 사진지우는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicDefect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && _type.Equals("insertData"))
            {
                DataRow row = grdInspectionItem.View.GetFocusedDataRow();
                if (row == null) return;
                SmartPictureEdit picBox = sender as SmartPictureEdit;
                picBox.Image = null;

                if (picBox.Name.Equals(picDefect.Name))
                {
                    row["FILEDATA1"] = null;
                    row["FILECOMMENTS1"] = null;
                    row["FILESIZE1"] = DBNull.Value;
                    row["FILEEXT1"] = null;
                    row["FILENAME1"] = null;
                    //===========================================================================
                    //YJKIM : File의 FullPath를 저장할 필드를 설정
                    row["FILEFULLPATH1"] = null;
                    row["FILEID1"] = null;
                    //---------------------------------------------------------------------------

                }
                else if (picBox.Name.Equals(picDefect2.Name))
                {
                    row["FILEDATA2"] = null;
                    row["FILECOMMENTS2"] = null;
                    row["FILESIZE2"] = DBNull.Value;
                    row["FILEEXT2"] = null;
                    row["FILENAME2"] = null;
                    //===========================================================================
                    //YJKIM : File의 FullPath를 저장할 필드를 설정
                    row["FILEFULLPATH2"] = null;
                    row["FILEID2"] = null;
                    //----------------------------------------------------------------------------
                }
            }
        }

        /// <summary>
        /// 이미지 추가버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddImage_Click(object sender, EventArgs e)
        {
            try
            {

                DataRow row = grdInspectionItem.View.GetFocusedDataRow();
                if (row == null) return;

                if (_type.Equals("updateData"))
                {
                    ShowMessage("CanAddImageBeforeResultSave");//검사 결과가 저장된 후에는 이미지를 추가 할 수 없습니다.
                    return;
                }

                if (!row["INSPECTIONRESULT"].ToString().Equals("NG"))
                {
                    ShowMessage("CanAddImageOnlyNG");//판정결과가 NG인 경우만 사진을 등록할수 있습니다.
                    return;
                }

                DialogManager.ShowWaitArea(this);

                string imageFile = string.Empty;

                OpenFileDialog dialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)|*.BMP;*.JPG;*.JPEG;*.PNG",
                    FilterIndex = 0
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    row["FILEINSPECTIONTYPE"] = "ArrivalRawMaterialInspection";
                    row["FILERESOURCEID"] = CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString() + row["INSPITEMID"];
                    row["PROCESSRELNO"] = CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString();

                    imageFile = dialog.FileName;
                    FileInfo fileInfo = new FileInfo(dialog.FileName);
                    using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        byte[] data = new byte[fileInfo.Length];
                        fs.Read(data, 0, (int)fileInfo.Length);

                        MemoryStream ms = new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(data).ToString()));
                        if (picDefect.Image == null)
                        {
                            row["FILENAME1"] = dialog.SafeFileName;
                            //2. 파일을 읽어들일때 File Binary를 읽어오던 부분을 경로를 저장하는 것으로 변경
                            //=========================================================================================================================
                            //YJKIM : binary파일을 저장하지 않고 File을 Upload하는 형태로 변경
                            //2020-02-19 강유라 이메일 첨부용
                            //row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            row["FILEFULLPATH1"] = dialog.FileName; //파일의 전체경로 저장
                            row["FILEID1"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            //-------------------------------------------------------------------------------------------------------------------------
                            row["FILECOMMENTS1"] = "InspectionResult/ArrivalRawMaterialInspection";
                            row["FILESIZE1"] = Math.Round(Convert.ToDouble(fileInfo.Length) / 1024);
                            row["FILEEXT1"] = fileInfo.Extension.Substring(1);           

                            picDefect.Image = Image.FromStream(ms);
                        }
                        else
                        {
                            row["FILENAME2"] = dialog.SafeFileName;
                            //2. 파일을 읽어들일때 File Binary를 읽어오던 부분을 경로를 저장하는 것으로 변경
                            //=========================================================================================================================
                            //YJKIM : binary파일을 저장하지 않고 File을 Upload하는 형태로 변경
                            //File Data를 저장할 필요가 없음
                            //row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            row["FILEFULLPATH2"] = dialog.FileName; //파일의 전체경로 저장
                            row["FILEID2"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            //2020-02-19 강유라 이메일 첨부용
                            row["FILEDATA2"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            //-------------------------------------------------------------------------------------------------------------------------
                            row["FILECOMMENTS2"] = "InspectionResult/ArrivalRawMaterialInspection";
                            row["FILESIZE2"] = Math.Round(Convert.ToDouble(fileInfo.Length) / 1024);
                            row["FILEEXT2"] = fileInfo.Extension.Substring(1);
                            picDefect2.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this);
            }
        }
        /// <summary>
        /// 검사수량, 불량수량을 입력 했을때 불량율을 계산해준다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefectQTY_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if ((e.Column.FieldName == "INSPECTIONQTY" && e.Value != DBNull.Value) || e.Column.FieldName == "SPECOUTQTY")
            {
                grdInspectionItem.View.CellValueChanged -= GrdDefectQTY_CellValueChanged;

                DataRow row = grdInspectionItem.View.GetDataRow(e.RowHandle);

                DataTable dt = grdInspectionItem.DataSource as DataTable;

                if (e.Column.FieldName == "INSPECTIONQTY")
                {
                    //2019-12-13** 전체수량과 입력한 검사수량 비교
                    if (_type.Equals("insertData"))
                    {
                        if (_lotQty < e.Value.ToSafeDoubleZero() || e.Value.ToSafeDoubleZero() == 0 )
                        {
                            if(_lotQty < e.Value.ToSafeDoubleZero())
                            ShowMessage("InvalidSampleQtyOverQty");//샘플 수량은 전체 수량보다 클 수 없습니다

                            else if (e.Value.ToSafeDoubleZero() == 0)
                                ShowMessage("InspectionQtyCount");//검사수량은 0이 될 수 없습니다.
                            /*
                            foreach (DataRow dtRow in dt.Rows)
                            {
                                if (!dtRow["INSPITEMTYPE"].ToString().Equals("NOTYPE"))
                                {
                                    dtRow["INSPECTIONQTY"] = DBNull.Value;
                                    //grdInspectionItem.View.SetFocusedRowCellValue("INSPECTIONQTYPNL", inspectionQtyPNL);
                                }
                            }*/

                            grdInspectionItem.View.SetFocusedRowCellValue(e.Column.FieldName, DBNull.Value);
                            grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
                            return;
                        }
                        else
                        {

                            foreach (DataRow dtRow in dt.Rows)
                            {
                                dtRow["INSPECTIONQTY"] = e.Value;
                                //grdInspectionItem.View.SetFocusedRowCellValue("INSPECTIONQTYPNL", inspectionQtyPNL);                                
                            }

                            _inspectionQty = Format.GetDouble(e.Value, 0);
                        }
                    }
                }

                //음수를 입력했을 때 0으로 바꿔줌
                if (e.Value.ToSafeInt32() < 0)
                    grdInspectionItem.View.SetFocusedRowCellValue(e.Column.FieldName, 0);
               
                if (row["INSPECTIONQTY"].ToSafeInt32() < row["SPECOUTQTY"].ToSafeInt32())
                {//검사 수량보다 불량수가 많을 때
                    ShowMessage("PcsQtyRangeOver");//검사수량보다 불량수량이 초과하였습니다.
                    grdInspectionItem.View.SetFocusedRowCellValue("SPECOUTQTY", 0);
                    grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
                    return; 
                }

                if (e.Column.FieldName == "SPECOUTQTY")
                {
                    var specOutQtySum = dt.AsEnumerable()
                        .Select(r => r["SPECOUTQTY"].ToSafeInt32())
                        .Sum().ToSafeInt32();

                    if (row["INSPECTIONQTY"].ToSafeInt32() < specOutQtySum)
                    {
                        ShowMessage("PcsQtyRangeOver");//검사수량보다 불량수량이 초과하였습니다.
                        grdInspectionItem.View.SetFocusedRowCellValue("SPECOUTQTY", 0);
                        grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
                        return;
                    }
                }

                if (string.IsNullOrWhiteSpace(row["SPECOUTQTY"].ToString()))
                {
                    grdInspectionItem.View.SetFocusedRowCellValue("DEFECTRATE", null);
                }
                else
                {
                    decimal defectRate = Math.Round((row["SPECOUTQTY"].ToSafeDecimal() / row["INSPECTIONQTY"].ToSafeDecimal() * 100).ToSafeDecimal(), 1);
                    string per = defectRate.ToString() + "%";
                    grdInspectionItem.View.SetFocusedRowCellValue("DEFECTRATE", per);
                }

                grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
            }
            
        }

        /// <summary>
        /// 불량율 값이 바뀔 경우 판정결과를 판정하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefectRate_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //불량율이 바뀌었을때
            if (e.Column.FieldName == "DEFECTRATE")
            {
                DataRow row = grdInspectionItem.View.GetFocusedDataRow();


                //불량 기준에 의해 판정결과 값 입력
                if (row["NCRDECISIONDEGREE"]== null)
                {
                    ShowMessage("NoStandardData");
                    return;
                }

                string messageId = null;

                string result  = InspectionHelper.SetQcGradeAndResultNCRQtyRateType(row, "RawInspection", row["NCRDECISIONDEGREE"].ToString(), false, out messageId);

                grdInspectionItem.View.SetFocusedRowCellValue("INSPECTIONRESULT", result);

                if (messageId != null)
                {
                    ShowMessage(messageId);
                }

                
            }
        }
        /// <summary>
        /// Spec 값에 따라 판정결과를 입력 해 주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChangedSpecCheck(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //측정값이 바뀌었을때
            if (e.Column.FieldName == "MEASUREVALUE")
            {
                DataRow row = grdInspectionItemSpec.View.GetDataRow(e.RowHandle);
                //스펙을 찾기위한 파라미터가 없는경우 return
                if (string.IsNullOrWhiteSpace(row["SPECCLASSID"].ToString()) || string.IsNullOrWhiteSpace(row["SPECSEQUENCE"].ToString()))
                {
                    ShowMessage("NoStandardData");//판정기준이 없습니다.
                    return;
                }

                if (row["NCRDECISIONDEGREE"] == null)
                {
                    ShowMessage("NoActionStandardData");//판정등급이 없습니다.
                    return;
                }

                string sequence = "";

                string qcgrade = InspectionHelper.GetQcGradeAndSequenceNCRAndSpecType("RawInspection", row["NCRDECISIONDEGREE"].ToString(), out sequence);

                row["QCGRADE"] = qcgrade;
                row["PRIORITY"] = sequence;

                //스펙체크 결과에 의해 판정결과 값 입력
                grdInspectionItemSpec.View.SetFocusedRowCellValue("INSPECTIONRESULT", CheckValidationSpecOut(row));
            }
        }

        #region 삭제 대상
        /*
        /// <summary>
        /// 소분류의 판정 결과에 따라 대분류를 결과를 자동판정 해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChangedItemResult(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_autoChange == true)
            {
                _autoChange = false;
                return;
            }

            SmartBandedGridView gridView = sender as SmartBandedGridView;

            //판정결과값이 바뀌었을때
            if (e.Column.FieldName == "INSPECTIONRESULT")
            {
                DataRow row = gridView.GetFocusedDataRow();
                //ROW의 SORT값 ex)10,10_1..
                string order = row["SORT"].ToString();

                //소분류가 아니면 return
                if (!order.Contains("_")) return;

                //대분류의 SORT값 ex)10
                string itemClassOrder = order.Substring(0, order.IndexOf("_"));

                //대분류에 해당하는 소분류를 찾기위한  SORT값 ex)10-
                string itemOrder = order.Substring(0, order.IndexOf("_") + 1);

                //대분류의 rowhandle
                int handle = gridView.LocateByValue("SORT", itemClassOrder);
                if (handle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                    gridView.FocusedRowHandle = handle;

                //소분류의 판정결과가 NG 일때
                if (e.Value.ToString() == "NG" || string.IsNullOrWhiteSpace(e.Value.ToString()))
                {
                    _autoChange = true;
                    gridView.SetRowCellValue(handle, "INSPECTIONRESULT", "NG");
                }
                //소분류의 판정결과가 OK 일때
                else if (e.Value.ToString() == "OK")
                {
                    DataTable tempTable = null;

                    if (tabInspection.SelectedTabPageIndex == 0)
                    {
                        tempTable = grdInspectionItem.DataSource as DataTable;
                    }
                    else
                    {
                        tempTable = grdInspectionItemSpec.DataSource as DataTable;
                    }
           
                    var query = tempTable.AsEnumerable().Where(x => System.Text.RegularExpressions.Regex.IsMatch(x.Field<string>("SORT"), itemOrder))
                                                        .Select(x => x["INSPECTIONRESULT"]).ToList();

                    if (!query.Contains("NG"))
                    {
                        _autoChange = true;
                        gridView.SetRowCellValue(handle, "INSPECTIONRESULT", "OK");
                    }

                }
            }
        }

        /// <summary>
        /// 대분류의 판정 결과에 따라 전체 결과를 자동판정 해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChangedClassItemResult(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            SmartBandedGridView gridView = sender as SmartBandedGridView;

            DataRow row = gridView.GetFocusedDataRow();

            //ROW의 SORT값 ex)10,10_1..
            string order = row["SORT"].ToString();

            //대분류가 아니면 return
            if (order.Contains("_")) return;

            if (e.Column.FieldName == "INSPECTIONRESULT")
            { 
                //대분류의 판정결과가 NG 일때
                if (e.Value.ToString() == "NG" || string.IsNullOrWhiteSpace(e.Value.ToString()))
                {
                    txtInspectionResult.EditValue = "NG";
                }
                //대분류의 판정결과가 OK 일때
                else if (e.Value.ToString() == "OK")
                {

                    DataTable selectedGrdTable;//선택된 그리드의 데이터 테이블
                    DataTable unSelectedGrdTable;//선택되지 않은 그리드의 데이터 테이블

                    if (tabInspection.SelectedTabPageIndex == 0)
                    {
                        selectedGrdTable = grdInspectionItem.DataSource as DataTable;
                        unSelectedGrdTable = grdInspectionItemSpec.DataSource as DataTable;
                    }
                    else
                    {
                        selectedGrdTable = grdInspectionItemSpec.DataSource as DataTable;
                        unSelectedGrdTable = grdInspectionItem.DataSource as DataTable;
                    }

                    //selectedGrdTable 중 결과가 NG인게 있는지 확인을 위한 결과 리스트화(대분류만 - 전체 찾아도 무관..)
                    var selectedGrdquery = selectedGrdTable.AsEnumerable().Where(x => !System.Text.RegularExpressions.Regex.IsMatch(x.Field<string>("SORT"), "_"))
                                                        .Select(x => x["INSPECTIONRESULT"]).ToList();

                    //unSelectedGrdTable 중 결과가 NG인게 있는지 확인을 위한 결과 리스트화(전체)
                    var unselectedGrdquery = unSelectedGrdTable.AsEnumerable()
                        .Select(x => x["INSPECTIONRESULT"]).ToList();

                    if (selectedGrdquery.Contains("NG"))
                    {//selectedGrdTable 중 결과 NG있을 때 -> 전체 결과 NG 
                        txtInspectionResult.EditValue = "NG";
                    }
                    else
                    {//selectedGrdTable 중 결과 NG없을 때 
                        if (unselectedGrdquery.Contains("NG"))
                        {//unSelectedGrdTable 결과 중 NG있을 때 -> 전체 결과 NG 
                            txtInspectionResult.EditValue = "NG";
                        }
                        else
                        {//unSelectedGrdTable 결과 중 NG어뵤을 때 -> 전체 결과 OK 
                            txtInspectionResult.EditValue = "OK";
                        }
                    }
                    
                }
            }
        }
        */
        #endregion
        /// <summary>
        /// inspectionType에 따라 (QTY,OK_NG,SPC)
        /// inspectionResult와 measurevalue
        /// readonly 처리하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            SmartBandedGridView gridView = sender as SmartBandedGridView;
            var inspType = gridView.GetRowCellValue(gridView.FocusedRowHandle, "INSPITEMTYPE");
            if (_type.Equals("insertData"))
            {//이미등록된 결과가 없을 때

                if (gridView.FocusedColumn.FieldName == "INSPECTIONRESULT")
                {   //inpitemtype 이 Spec인 경우 inspectionresult readonly => CheckValidationSpecOut 결과에 의해 자동판정
                    if (inspType != null && inspType.ToString() == "SPC")
                    {
                        this.ShowMessage("InspItemTypeSpecCantInputResult");//측정값을 입력하면 판정결과가 자동입력 됩니다.
                        e.Cancel = true;
                    }

                    //inpitemtype 이 QTY 경우 inspectionresult readonly => CheckValidationDefectRate 결과에 의해 자동판정
                    if (inspType != null && inspType.ToString() == "OK_NG")
                    {
                        this.ShowMessage("InspItemTypeQTYCantInputResult");//검사 수량 및 불량수량을 입력하면 판정결과가 자동입력 됩니다.
                        e.Cancel = true;
                    }

                }

                if (gridView.FocusedColumn.FieldName == "SPECOUTQTY")
                {
                    var inspectionQty = gridView.GetRowCellValue(gridView.FocusedRowHandle, "INSPECTIONQTY");
                    if (inspectionQty == null || string.IsNullOrWhiteSpace(inspectionQty.ToString()) || inspectionQty.ToSafeInt32() == 0)
                    {                       
                        e.Cancel = true;
                    }

                }
                /*
                if (gridView.FocusedColumn.FieldName == "INSPECTIONQTY" || gridView.FocusedColumn.FieldName == "SPECOUTQTY"
               || gridView.FocusedColumn.FieldName == "DEFECTRATE" || gridView.FocusedColumn.FieldName == "INSPECTIONRESULT"
               || gridView.FocusedColumn.FieldName == "MEASUREVALUE")
                {   //inpitemtype 이 NOTYPE인 경우 readonly
                    if (inspType != null && inspType.ToString() == "NOTYPE")
                    {
                        this.ShowMessage("InspItemTypeYesNoLargeCantInputResult");//소분류의 검사결과를 입력하면 대분류의 판정결과가 자동으로 입력됩니다.
                        e.Cancel = true;
                    }

                }*/
            }
        }

        #region 삭제 대상
        /*
        /// <summary>
        /// 대분류일 경우(INSPITEMTYPE = 'NOTYPE') (검사수량, 불량수량, 불량율, 판정, 측정값 모두 입력불가)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InspItemClass_ReadOnly(object sender, CancelEventArgs e)
        {
            SmartBandedGridView gridView = sender as SmartBandedGridView;
            var inspType = gridView.GetRowCellValue(gridView.FocusedRowHandle, "INSPITEMTYPE");

            if (gridView.FocusedColumn.FieldName == "INSPECTIONQTY" || gridView.FocusedColumn.FieldName == "SPECOUTQTY"
                || gridView.FocusedColumn.FieldName == "DEFECTRATE" || gridView.FocusedColumn.FieldName == "INSPECTIONRESULT"
                || gridView.FocusedColumn.FieldName == "MEASUREVALUE")
            {   //inpitemtype 이 NOTYPE인 경우 readonly
                if (inspType != null && inspType.ToString() == "NOTYPE")
                {
                    this.ShowMessage("InspItemTypeYesNoLargeCantInputResult");//소분류의 검사결과를 입력하면 대분류의 판정결과가 자동으로 입력됩니다.
                    e.Cancel = true;
                }

            }
        }*/
        #endregion

        /// <summary>
        /// 저장버튼을 클릭했을때 검사 결과를 저장하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;

            if (_type.Equals("insertData"))
            { //검사 결과 최초 등록일때 결과 저장여부 평가
                if (txtMaterialLotId.EditValue == null || string.IsNullOrWhiteSpace(Format.GetString(txtMaterialLotId.EditValue)))
                {
                    this.ShowMessage("NeedToInputMaterialLotId");//자재 LotId를 입력해야합니다.
                    return;
                }
                
                if (CheckAllResultAndImageInput(grdInspectionItem, true) == false || CheckAllResultInput(grdInspectionItemSpec) == false)
                {
                    //2020-03-03 강유라 영풍만 이미지 저장 필수 풀어달라함
                    if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
                    {
                        if (_hasImage == false)
                        {
                            this.ShowMessage("NeedToAddImage");//판정 결과가 NG 인 항목은 적어도 하나의 이미지를 추가해야합니다.
                        }
                        else
                        {
                            this.ShowMessage("NeedToAllInspectionResult");//모든 검사 결과를 입력해야 합니다.
                        }
                    }
                    else if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                    {
                        this.ShowMessage("NeedToAllInspectionResult");//모든 검사 결과를 입력해야 합니다.
                    }

                    return;
                }
                

                txtInspectionDate.EditValue = DateTime.Now;
            }

            result = this.ShowMessage(MessageBoxButtons.YesNo, "ResultRegisterConfirmNote");//검사 결과를 저장하시겠습니까?

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnClose.Enabled = false;

                    //inspectionResult Table에 저장 될 Data 
                    DataTable inspectionResult;
                    if (_type.Equals("insertData"))
                    {//sf_inspectionResult에 Insert
                        inspectionResult = GetInspectionResultTableToInsert();
                    }
                    else
                    {//sf_inspectionResult에 Update
                        inspectionResult = GetInspectionResultTableToUpdate();
                    }

                    inspectionResult.TableName = "list";

                    //inspectionItemResult Table에 저장 될 Data - Defect
                    /*DataTable defectChanged = (grdInspectionItem.DataSource as DataTable).AsEnumerable()
                       .Where(r => !string.IsNullOrWhiteSpace(r["INSPECTIONRESULT"].ToString()))
                       .CopyToDataTable();*/

                    DataTable defectChanged = (grdInspectionItem.DataSource as DataTable).Clone();

                    var tempDefect = (grdInspectionItem.DataSource as DataTable).AsEnumerable()
                       .Where(r => !string.IsNullOrWhiteSpace(r["INSPECTIONQTY"].ToString()))
                       .ToList();

                    if (tempDefect.Count > 0)
                    {
                        defectChanged = tempDefect.CopyToDataTable();
                    }

                     defectChanged.TableName = "list2";

                    //3. 파일을 저장하는 로직을 작성
                    //=====================================================================================================
                    //YJKIM : Server에 파일을 Upload하는 로직을 작성
                    //______________________________________________________________________________________________________
                    //    ColumnName      TableName -> |     SF_INSPECTIONFILE             |    SF_OBJECTFILEMAP
                    //______________________________________________________________________________________________________
                    //         리소스타입               |     FILEINSPECTIONTYPE            |       RESOURCETYPE
                    //         리소스아이디             |     FILERESOURCEID                |       RESOURCEID
                    //         파일아이디               |     Generate.createID("FILE-")    |       FILEID
                    //         리소스버전               |     *                             |       RESOURCEVERSION
                    //_______________________________________________________________________________________________________
                    DataTable fileUploadTable = QcmImageHelper.GetImageFileTable();
                    int totalFileSize = 0;

                    //2020-02-19 강유라 이메일 첨부용
                    _FileToSendEmail = QcmImageHelper.GetImageFileTableToSendEmail();

                    foreach (DataRow originRow in defectChanged.Rows)
                    {
                        if (!originRow.IsNull("FILENAME1"))
                        {
                            if (!originRow.GetString("FILEFULLPATH1").Equals(""))
                            {
                                DataRow newRow = fileUploadTable.NewRow();
                                newRow["FILEID"] = originRow["FILEID1"];         //Server에서 FileID를 생성하여서 가져와야 한다.
                                newRow["FILENAME"] = originRow["FILEID1"];
                                newRow["FILEEXT"] = originRow.GetString("FILEEXT1").Replace(".", "");      //파일업로드시에는 확장자에서 . 을 빼야 한다.
                                newRow["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];// originRow["FILEFULLPATH1"];
                                newRow["SAFEFILENAME"] = originRow["FILENAME1"];
                                newRow["FILESIZE"] = originRow["FILESIZE1"];
                                newRow["SEQUENCE"] = 1;
                                newRow["LOCALFILEPATH"] = originRow["FILEFULLPATH1"];
                                newRow["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                                newRow["RESOURCEID"] = originRow["FILERESOURCEID"];
                                newRow["RESOURCEVERSION"] = "*";
                                newRow["PROCESSINGSTATUS"] = "Wait";


                                //YJKIM : 서버에서 데이타베이스를 입력하기 위해 파일아이디를 전달해야 한다.
                                

                                totalFileSize += originRow.GetInteger("FILESIZE1");

                                fileUploadTable.Rows.Add(newRow);

                                //2020-02-19 강유라 이메일 첨부용
                                DataRow emailNewRow = _FileToSendEmail.NewRow();
                                emailNewRow["FILEDATA"] = originRow["FILEDATA1"];
                                emailNewRow["FILENAME"] = originRow["FILENAME1"];

                                _FileToSendEmail.Rows.Add(emailNewRow);
                            }
                        }

                        if (!originRow.IsNull("FILENAME2"))
                        {
                            if (!originRow.GetString("FILEFULLPATH2").Equals(""))
                            {
                                DataRow newRow2 = fileUploadTable.NewRow();
                                newRow2["FILEID"] = originRow["FILEID2"];          //Server에서 FileID를 생성하여서 가져와야 한다.
                                newRow2["FILENAME"] = originRow["FILEID2"];
                                newRow2["FILEEXT"] = originRow.GetString("FILEEXT2").Replace(".", "");
                                newRow2["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];//originRow["FILEFULLPATH2"];
                                newRow2["SAFEFILENAME"] = originRow["FILENAME2"];
                                newRow2["FILESIZE"] = originRow["FILESIZE2"];
                                newRow2["SEQUENCE"] = 2;
                                newRow2["LOCALFILEPATH"] = originRow["FILEFULLPATH2"];
                                newRow2["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                                newRow2["RESOURCEID"] = originRow["FILERESOURCEID"];
                                newRow2["RESOURCEVERSION"] = "*";
                                newRow2["PROCESSINGSTATUS"] = "Wait";

                                totalFileSize += originRow.GetInteger("FILESIZE2");

                                fileUploadTable.Rows.Add(newRow2);

                                //2020-02-19 강유라 이메일 첨부용
                                DataRow emailNewRow = _FileToSendEmail.NewRow();
                                emailNewRow["FILEDATA"] = originRow["FILEDATA2"];
                                emailNewRow["FILENAME"] = originRow["FILENAME2"];

                                _FileToSendEmail.Rows.Add(emailNewRow);
                            }
                        }
                    }

                    if (fileUploadTable.Rows.Count > 0)
                    {
                        FileProgressDialog fileProgressDialog = new FileProgressDialog(fileUploadTable, UpDownType.Upload, "", totalFileSize);
                        fileProgressDialog.ShowDialog(this);

                        if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                            throw MessageException.Create("CancelMessageToUploadFile");  //파일업로드를 취소하였습니다.

                        ProgressingResult fileResult = fileProgressDialog.Result;

                        if (!fileResult.IsSuccess)
                            throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
                    }
                    //------------------------------------------------------------------------------------------------------


                    //inspectionItemResult Table에 저장 될 Data - Measure
                    /*DataTable measureChanged = (grdInspectionItemSpec.DataSource as DataTable).AsEnumerable()
                       .Where(r => !string.IsNullOrWhiteSpace(r["INSPECTIONRESULT"].ToString()))
                       .CopyToDataTable(); */
                    DataTable measureChanged = (grdInspectionItemSpec.DataSource as DataTable).Copy();

                    measureChanged.TableName = "list3";

                    //Imagefile DataTable
                    DataTable ImageFileTable = fpcImage.GetChangedRows();
                    ImageFileTable.TableName = "list4";

                    CheckChanged(inspectionResult.Rows[0]["_STATE_"].ToString(), defectChanged, measureChanged, ImageFileTable);

                    var isModified = fpcImage.GetChangedRows().AsEnumerable()
                        .Select(r => r["_STATE_"]).ToList();

                    if (fpcImage.GetChangedRows().Rows.Count > 0 && !isModified.Contains("modified"))
                    {
                        fpcImage.SaveChangedFiles();
                    }

                    /*
                    DataSet rullSet = new DataSet();

                    rullSet.Tables.Add(inspectionResult);
                    rullSet.Tables.Add(defectChanged);
                    rullSet.Tables.Add(measureChanged);
                    rullSet.Tables.Add(ImageFileTable);

                    ExecuteRule("SaveRawAssyInspection", rullSet);
                    */

                    MessageWorker worker = new MessageWorker("SaveRawAssyInspection");
                    worker.SetBody(new MessageBody()
                    {

                        { "list", inspectionResult},
                        { "list2", defectChanged},
                        { "list3", measureChanged},
                        { "list4", ImageFileTable},
                        { "ENTERPRISEID", UserInfo.Current.Enterprise},
                        { "PLANTID", CurrentDataRow["PLANTID"].ToString()},
                        { "inspectionclassId", "RawInspection"},

                    });

                    var isSendEmailDt = worker.Execute<DataTable>();
                    var isSendEmailRS = isSendEmailDt.GetResultSet();

                    if (isSendEmailRS.Rows.Count > 0)
                    {

                        DataRow responseRow = isSendEmailRS.Rows[0];

                        if (responseRow["ISSENDEMAIL"].ToString().Equals("True"))
                        {
                            //* 2019-12-26 xml로 수정중
                           
                            string exteriorNG = responseRow["EXTERIORNG"].ToString();
                            //if(exteriorNG.Length > 0)
                             //   exteriorNG = exteriorNG.Substring(0, (exteriorNG.Length) - 1);

                            string measureNG = responseRow["MEASURENG"].ToString();
                            //if (measureNG.Length > 0)
                            //measureNG = measureNG.Substring(0, (measureNG.Length) - 1);

                            DataTable toSendDt = CommonFunction.CreateRawAbnormalEmailDt();

                            DataRow newRow = toSendDt.NewRow();
                            newRow["VENDORNAME"] = CurrentDataRow["VENDORNAME"];
                            newRow["CONSUMABLEDEFNAME"] = CurrentDataRow["CONSUMABLEDEFNAME"];
                            newRow["CONSUMABLEDEFID"] = CurrentDataRow["CONSUMABLEDEFID"];
                            newRow["CONSUMABLEDEFVERSION"] = CurrentDataRow["CONSUMABLEDEFVERSION"];
                            newRow["MATERIALLOT"] = txtMaterialLotId.EditValue;
                            newRow["QTY"] = CurrentDataRow["QTY"];
                            newRow["ENTRYEXITDATE"] = CurrentDataRow["ENTRYEXITDATE"];
                            newRow["DEFECTNAME"] = exteriorNG;
                            newRow["MEASUREVALUE"] = measureNG;
                            newRow["INSPECTIONRESULT"] = "NG";
                            newRow["REMARK"] = "";
                            newRow["USERID"] = UserInfo.Current.Id; 
                            newRow["TITLE"] = Language.Get("RAWABNORMALTITLE");
                            newRow["INSPECTION"] = "ArrivalRawMaterialInspection";
                            newRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType;

                            toSendDt.Rows.Add(newRow);

                            //2020-02-19 강유라 이메일 첨부용
                            //CommonFunction.ShowSendEmailPopupDataTable(toSendDt, _FileToSendEmail);
                            CommonFunction.ShowSendEmailPopupDataTable(toSendDt);
     
                        }
                    }
                    ShowMessage("SuccessSave");

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.DialogResult = DialogResult.OK;
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnClose.Enabled = true;
                    this.Close();
                }
            }
        }

        private void GrdInspectionItemSpec_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            DataRow toAddingRow = grdInspectionItemSpec.View.GetFocusedDataRow();
            if (toAddingRow == null) e.Cancel = true;
        }

        /// <summary>
        ///측정검사에 +버튼을 누르면 해당 항목 row추가 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            if (_toCopyRow == null) return;

            args.NewRow["INSPECTIONCLASSID"] = _toCopyRow["INSPECTIONCLASSID"];
            args.NewRow["MIDDLEINSPECTIONCLASSID"] = _toCopyRow["MIDDLEINSPECTIONCLASSID"];
            args.NewRow["INSPECTIONMETHODNAME"] = _toCopyRow["INSPECTIONMETHODNAME"];
            args.NewRow["INSPITEMNAME"] = _toCopyRow["INSPITEMNAME"];
            args.NewRow["INSPITEMID"] = _toCopyRow["INSPITEMID"];
            args.NewRow["INSPITEMVERSION"] = _toCopyRow["INSPITEMVERSION"].ToString();
            args.NewRow["INSPECTIONDEFID"] = _toCopyRow["INSPECTIONDEFID"];
            args.NewRow["INSPECTIONDEFVERSION"] = _toCopyRow["INSPECTIONDEFVERSION"];
            args.NewRow["TXNHISTKEY"] = _toCopyRow["TXNHISTKEY"];
            args.NewRow["RESOURCEID"] = _toCopyRow["RESOURCEID"];
            args.NewRow["RESOURCETYPE"] = _toCopyRow["RESOURCETYPE"];
            args.NewRow["PROCESSRELNO"] = _toCopyRow["PROCESSRELNO"];
            args.NewRow["INSPECTIONSTANDARD"] = _toCopyRow["INSPECTIONSTANDARD"];
            args.NewRow["PROCESSRELNO"] = _toCopyRow["PROCESSRELNO"];

            args.NewRow["PLANTID"] = _toCopyRow["PLANTID"];
            args.NewRow["ENTERPRISEID"] = _toCopyRow["ENTERPRISEID"];
            args.NewRow["NCRDECISIONDEGREE"] = _toCopyRow["NCRDECISIONDEGREE"];
            args.NewRow["INSPITEMTYPE"] = _toCopyRow["INSPITEMTYPE"];

            args.NewRow["SPECCLASSID"] = _toCopyRow["SPECCLASSID"];
            args.NewRow["SPECSEQUENCE"] = _toCopyRow["SPECSEQUENCE"];

            args.NewRow["TARGETVALUE"] = _toCopyRow["TARGETVALUE"];
            args.NewRow["UPPERSPECLIMIT"] = _toCopyRow["UPPERSPECLIMIT"];
            args.NewRow["LOWERSPECLIMIT"] = _toCopyRow["LOWERSPECLIMIT"];
            args.NewRow["CL"] = _toCopyRow["CL"];
            args.NewRow["UPPERCONTROLLIMIT"] = _toCopyRow["UPPERCONTROLLIMIT"];
            args.NewRow["LOWERCONTROLLIMIT"] = _toCopyRow["LOWERCONTROLLIMIT"];
            args.NewRow["UPPERSCREENLIMIT"] = _toCopyRow["UPPERSCREENLIMIT"];
            args.NewRow["LOWERSCREENLIMIT"] = _toCopyRow["LOWERSCREENLIMIT"];

            args.NewRow["CANDELETE"] = "Y";

            args.NewRow["SORT"] = _toCopyRow["SORT"].ToString() + "_1";


            DataTable dt = (grdInspectionItemSpec.DataSource as DataTable).Copy();
            InitializeGridMeasure(_type);
            grdInspectionItemSpec.DataSource = dt;
            grdInspectionItemSpec.View.FocusedRowHandle = _toCopyHandle;
        }

        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (grdInspectionItemSpec.View.GetRowCellValue(e.RowHandle, "CANDELETE") == null) return;
            if (grdInspectionItemSpec.View.GetRowCellValue(e.RowHandle, "CANDELETE").Equals("Y"))
            {
                e.Appearance.BackColor = Color.FromArgb(207, 218, 250);
            }
        }

        private void GrdInspectionItemSpec_ToolbarDeleteRow(object sender, EventArgs e)
        {
            DataRow focusedRow = grdInspectionItemSpec.View.GetFocusedDataRow();
            DataTable dt = grdInspectionItemSpec.DataSource as DataTable;
            if (focusedRow["CANDELETE"].ToString().Equals("Y"))
            {
                dt.Rows.Remove(focusedRow);
            }

            InitializeGridMeasure(_type);
            grdInspectionItemSpec.DataSource = dt;
        }
        #endregion

        #region 검색
        /// <summary>
        /// 조회 함수
        /// </summary>
        public void OnSearch()
        {       
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_CONSUMABLEDEFID", CurrentDataRow["CONSUMABLEDEFID"]);
            values.Add("P_CONSUMABLEDEFVERSION", CurrentDataRow["CONSUMABLEDEFVERSION"]);
            values.Add("P_RELRESOURCEID", CurrentDataRow["CONSUMABLEDEFID"]);
            values.Add("P_RELRESOURCEVERSION", CurrentDataRow["CONSUMABLEDEFVERSION"]);
            values.Add("P_RELRESOURCETYPE", "Consumable");
            values.Add("P_ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            values.Add("P_PLANTID", CurrentDataRow["PLANTID"]);
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_ORDERNUMBER", CurrentDataRow["ORDERNUMBER"]);
            values.Add("P_RESOURCEID", CurrentDataRow["MATERIALLOTID"]);
            values.Add("P_PROCESSRELNO", CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString());
            values.Add("RESOURCETYPE", "ArrivalRawMaterialInspection");
            values.Add("P_RESULTTXNHISTKEY", CurrentDataRow["TXNHISTKEY"].ToString());
            values.Add("P_RESULTTXNGROUPHISTKEY", CurrentDataRow["TXNGROUPHISTKEY"].ToString());
            values.Add("P_INSPECTIONDEFID", "RawInspection");
            values.Add("P_INSPECTIONCLASSID", "RawInspection");
            values.Add("P_INSPITEMTYPE", "OK_NG");

            Dictionary<string, object> imageValues = new Dictionary<string, object>();
            //이미지 파일Search parameter
            imageValues.Add("P_FILERESOURCETYPE", "RawAssyImage");
            imageValues.Add("P_FILERESOURCEID", CurrentDataRow["ORDERNUMBER"].ToString() + CurrentDataRow["STORENO"].ToString());
            imageValues.Add("P_FILERESOURCEVERSION", "0");

            //InspectionInfo
            DataTable inspectionTable = SqlExecuter.Query("SelectRawMaterialResult", "10001", values);

            //ItemClassTable - Defect
            //DataTable defectItemClassTable = SqlExecuter.Query("SelectItemClassArrivalRawMaterialEx", "10001", values);
            //ItemTable - Defect
            DataTable defectItemTable = SqlExecuter.Query("SelectRawInspectionExterior", "10001", values);

            values.Remove("P_INSPITEMTYPE");
            values.Add("P_INSPITEMTYPE", "SPC");

            //ItemClassTable - Measure
            //DataTable measureItemClassTable = SqlExecuter.Query("SelectItemClassArrivalRawMaterial", "10001", values);
            //ItemTable - Measure
            DataTable measureItemTable = SqlExecuter.Query("SelectRawInspectionMeasure", "10001", values);

            //ImageFileTable
            DataTable imageFileTable = SqlExecuter.Query("SelectFileInspResult", "10001", imageValues);

            //InspectionInfo
            _consumableInspectionTable = inspectionTable;
            SetControlSearchData(_consumableInspectionTable);//inspectionResult 정보 컨트롤 바인딩
            SetEnableControl(_consumableInspectionTable);//MaterialId, 접수일 입력 된 내용 있을 때 Enable

            //grdDefectInspection
            //grdInspectionItem.DataSource = SetDataTableOrder(defectItemClassTable, defectItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
            string processRelNo = CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString();

            grdInspectionItem.DataSource = InspectionHelper.SetProcessRelNo(processRelNo, defectItemTable);

            //grdMeasureInspection
            //grdInspectionItemSpec.DataSource = SetDataTableOrder(measureItemClassTable, measureItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
            grdInspectionItemSpec.DataSource = InspectionHelper.SetProcessRelNo(processRelNo, measureItemTable);

            //ImageFileTable
            fpcImage.DataSource = imageFileTable;//이미지 파일데이터 그리드 바인딩

        }
        #endregion

        #region Private Function
        /*
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
                row1["PROCESSRELNO"] = CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString();

                foreach (DataRow row2 in table2.Rows)
                {
                    if (row1["INSPITEMID"].Equals(row2["INSPITEMCLASSID"]))
                    {
                        row2["SORT"] = inspItemclassNo + "_" + inspItemNo;
                        row2["PROCESSRELNO"] = CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString();

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
        */

        /// <summary>
        /// sf_inspectionresult에 insert할 테이블 생성 함수
        /// </summary>
        private DataTable GetInspectionResultTableToInsert()
        {
            if (_consumableInspectionTable.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            DataRow searchedRow = _consumableInspectionTable.Rows[0];

            string priotiryQCGrade = InspectionHelper.GetPriorityQCGrade(grdInspectionItem, grdInspectionItemSpec, "NCR");

            //inspectionResult Table에 저장될 Data
            DataTable inspectionTable = new DataTable();
            inspectionTable.TableName = "list";
            DataRow row = null;


            inspectionTable.Columns.Add(new DataColumn("TXNHISTKEY", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("ORDERNUMBER", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("CONSUMABLEDEFID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("STORENO", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PROCESSRELNO", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("CONSUMABLEDEFVERSION", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("AREAID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("RESOURCEID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("RESOURCETYPE", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("ACCEPTDATE", typeof(DateTime)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONDATE", typeof(DateTime)));
            inspectionTable.Columns.Add(new DataColumn("REQUESTUSER", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONUSER", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONRESULT", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("DESCRIPTION", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PLANTID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONCLASSID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("_STATE_", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("QCGRADE", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONQTY", typeof(Int16)));


            row = inspectionTable.NewRow();
            row["TXNHISTKEY"] = searchedRow["TXNHISTKEY"];
            row["ORDERNUMBER"] = CurrentDataRow["ORDERNUMBER"];
            row["CONSUMABLEDEFID"] = CurrentDataRow["CONSUMABLEDEFID"];
            row["STORENO"] = CurrentDataRow["STORENO"];
            row["PROCESSRELNO"] = CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString();
            row["CONSUMABLEDEFVERSION"] = CurrentDataRow["CONSUMABLEDEFVERSION"];
            row["RESOURCEID"] = txtMaterialLotId.EditValue;
            row["RESOURCETYPE"] = "";
            row["ACCEPTDATE"] = dateAccepteDate.EditValue;
            row["INSPECTIONDATE"] = txtInspectionDate.EditValue;
            row["REQUESTUSER"] = popRequesterUser.Text;
            row["INSPECTIONUSER"] = popInspectionUser.Text;
            //row["REQUESTUSER"] = popRequesterUser.GetValue();
            //row["INSPECTIONUSER"] = popInspectionUser.GetValue();
            row["INSPECTIONRESULT"] = txtInspectionResult.EditValue;
            row["DESCRIPTION"] = txtRemark.EditValue;
            row["PLANTID"] = CurrentDataRow["PLANTID"];
            row["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            row["_STATE_"] = "added";
            row["AREAID"] = CurrentDataRow["AREAID"];
            row["INSPECTIONCLASSID"] = "RawInspection";
            row["QCGRADE"] = priotiryQCGrade;
            row["INSPECTIONQTY"] = _inspectionQty;

            inspectionTable.Rows.Add(row);

            return inspectionTable;


        }

        /// <summary>
        /// sf_inspectionresult에 update할 테이블 생성 함수
        /// </summary>
        private DataTable GetInspectionResultTableToUpdate()
        {
            if (_consumableInspectionTable.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            DataRow searchedRow = _consumableInspectionTable.Rows[0];
            //inspectionResult Table에 저장될 Data
            DataTable inspectionTable = new DataTable();          
            DataRow row = null;

            //key
            inspectionTable.Columns.Add(new DataColumn("TXNHISTKEY", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PROCESSRELNO", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("RESOURCEID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("RESOURCETYPE", typeof(string)));

            //내용
            inspectionTable.Columns.Add(new DataColumn("REQUESTUSER", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONUSER", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("DESCRIPTION", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("ACCEPTDATE", typeof(DateTime)));
            inspectionTable.Columns.Add(new DataColumn("_STATE_", typeof(string)));

            row = inspectionTable.NewRow();
            row["TXNHISTKEY"] = searchedRow["TXNHISTKEY"];
            row["PROCESSRELNO"] = CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString();
            row["RESOURCEID"] = txtMaterialLotId.EditValue;
            row["RESOURCETYPE"] = "";
            row["REQUESTUSER"] = popRequesterUser.Text;
            row["INSPECTIONUSER"] = popInspectionUser.Text;
            //row["REQUESTUSER"] = popRequesterUser.GetValue();
            //row["INSPECTIONUSER"] = popInspectionUser.GetValue();
            row["DESCRIPTION"] = txtRemark.EditValue;
            row["ACCEPTDATE"] = dateAccepteDate.EditValue;

            if (searchedRow["REQUESTUSER"].ToString().Equals(popRequesterUser.GetValue().ToString()) &&
              searchedRow["INSPECTIONUSER"].ToString().Equals(popInspectionUser.GetValue().ToString()) &&
              searchedRow["DESCRIPTION"].ToString().Equals(txtRemark.EditValue.ToString()) &&
              searchedRow["ACCEPTDATE"].ToString().Equals(dateAccepteDate.EditValue.ToString()) &&
              fpcImage.GetChangedRows().Rows.Count  == 0)
            {
                row["_STATE_"] = "unchanged";
            }
            else
            {
                row["_STATE_"] = "modified";
            }
            inspectionTable.Rows.Add(row);

            return inspectionTable;
        }

        /// <summary>
        /// popup을 로드할때 조회된 데이터를 각각의 컨트롤에 
        /// 할당해주는 함수
        /// </summary>
        private void SetControlSearchData(DataTable table)
        {
            if (table.Rows.Count < 1) return;
            DataRow row = table.Rows[0];
            txtVendor.EditValue = CurrentDataRow["VENDORNAME"];
            txtOrderNumber.EditValue = CurrentDataRow["ORDERNUMBER"];
            txtMaterialDefName.EditValue = CurrentDataRow["CONSUMABLEDEFNAME"];
            txtMaterialLotId.EditValue = CurrentDataRow["MATERIALLOTID"];
            txtEntryExitDate.EditValue = CurrentDataRow["ENTRYEXITDATE"];
            txtMaterialType.EditValue = CurrentDataRow["CONSUMABLETYPE"];
            txtMaterialDefID.EditValue = CurrentDataRow["CONSUMABLEDEFID"];
            txtQty.EditValue = CurrentDataRow["QTY"];
            txtUnit.EditValue = CurrentDataRow["UNIT"];

            dateAccepteDate.EditValue = row["ACCEPTDATE"];
            txtInspectionDate.EditValue = row["INSPECTIONDATE"].ToString();

            if (_type.Equals("updateData"))
                txtInspectionResult.EditValue = row["INSPECTIONRESULT"];

            txtRemark.EditValue = row["DESCRIPTION"];
            popRequesterUser.SetValue(row["REQUESTUSER"]);
            popRequesterUser.Text = row["REQUESTUSERNAME"].ToString();
            popInspectionUser.SetValue(row["INSPECTIONUSER"]);
            popInspectionUser.Text = row["INSPECTIONUSER"].ToString();

        }

        /// <summary>
        /// 입력된 값이 spec값을 벗어나지 않았는지 체크하여 결과를 return 하는 함수
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String CheckValidationSpecOut(DataRow row)
        {
            string result = "OK";
            if (!string.IsNullOrWhiteSpace(row["MEASUREVALUE"].ToString()))
            {
                DataRow itemRow = grdInspectionItemSpec.View.GetFocusedDataRow();

                //LSL, USL 
                double lsl = row["LOWERSPECLIMIT"].ToString().ToSafeDoubleNaN();
                double usl = row["UPPERSPECLIMIT"].ToString().ToSafeDoubleNaN();

                //입력된 값
                double measureValue = row["MEASUREVALUE"].ToSafeDoubleNaN();



                //입력된 값이 Spec범위를 벗어났을 경우
                if (lsl > measureValue || usl < measureValue)
                {
                    result = "NG";
                }

                return result;
            }
            else
            {
                return "";
            }

        }

        #region OLD VERSION
        /*
        /// <summary>
        /// 입력된 값이 spec값을 벗어나지 않았는지 체크하여 결과를 return 하는 함수
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String CheckValidationSpecOut(DataRow row)
        {
            string result = "OK";

            //spec값을 찾기 위한 currentRow의 파라미터
            Dictionary<string, object> specParam = new Dictionary<string, object>();
            specParam.Add("P_SPECCLASSID", row["SPECCLASSID"]);
            specParam.Add("P_SPECSEQUENCE", row["SPECSEQUENCE"]);
            specParam.Add("P_CONTROLTYPE", "XBARR");

            //spec값을 가져오는 쿼리 실행
            SqlQuery subEquipmentId = new SqlQuery("GetSpecLimitValue", "10002", specParam);
            DataTable specTable = subEquipmentId.Execute();

            if (specTable.Rows.Count == 0)
            {
                this.ShowMessage("NoSpecDetail");//유효성 검사를 할 Spec정보가 등록되어있지 않습니다.
                result = "";
            }
            else
            {
                //spec값
                DataRow specLimitRow = specTable.Rows[0];


                //LSL, USL 
                double lsl = specLimitRow["LSL"].ToString().ToSafeDoubleNaN();
                double usl = specLimitRow["USL"].ToString().ToSafeDoubleNaN();

                //입력된 값
                double measureValue = row["MEASUREVALUE"].ToSafeDoubleNaN();

                //입력된 값이 Spec범위를 벗어났을 경우
                if (lsl > measureValue || usl < measureValue)
                {
                    result = "NG";
                }

            }

            return result;
        }
        */
        #endregion
/*
        // <summary>
        /// 입력된 검사수량, 불량수량을 통한 불량율 계산값이 기준에 벗어나지 않았는지 체크하여 결과를 return 하는 함수********불량율 기준 정해지면 수정******
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String CheckValidationDefectRate(DataRow row)
        {
            string result = "";
            // spec값을 찾기 위한 currentRow의 파라미터
            Dictionary<string, object> specParam = new Dictionary<string, object>();
            specParam.Add("P_INSPECTIONCLASSID", "RawInspection");
            specParam.Add("P_NCRDECISIONDEGREE", row["NCRDECISIONDEGREE"]);

            //spec값을 가져오는 쿼리 실행
            SqlQuery NCRQuery = new SqlQuery("SelectNCRCheckBasis", "10001", specParam);
            DataTable NCRDt = NCRQuery.Execute();

            //spec값
            DataRow NCRRow = NCRDt.Rows[0];

            //입력된 값
            string defectRateString = NCRRow["NGRATE"].ToString();
            string defectQuantityString = NCRRow["NGQUANTITY"].ToString();

            if (!string.IsNullOrWhiteSpace(defectRateString))
            {
                decimal defectRate = defectRateString.ToSafeDecimal();

                //불량율이 기준을 벗어났을 경우
                decimal inputedDefectRate = row["DEFECTRATE"].ToString().Replace("%", "").ToSafeDecimal();
                if (inputedDefectRate > defectRate)
                {
                    result = "NG";
                }
                else
                {
                    result = "OK";
                }
            }
            else if (!string.IsNullOrWhiteSpace(defectQuantityString))
            {
                decimal defectQuantity = defectQuantityString.ToSafeDecimal();

                //불량율이 기준을 벗어났을 경우
                if (row["SPECOUTQTY"].ToSafeDecimal() >= defectQuantity)
                {
                    result = "NG";
                }
                else
                {
                    result = "OK";
                }
            }
            //판정기준나오면 주석풀기
            //spec값을 찾기 위한 currentRow의 파라미터
            //Dictionary<string, object> specParam = new Dictionary<string, object>();
            //specParam.Add("P_INSPECTIONDEFID", "RawInspection");
            //specParam.Add("P_NCRDECISIONDEGREE", row["NCRDECISIONDEGREE"]);

            ////spec값을 가져오는 쿼리 실행
            //SqlQuery NCRQuery = new SqlQuery("SelectNCRCheckBasis", "10001", specParam);
            //DataTable NCRDt = NCRQuery.Execute();

            ////spec값
            //DataRow NCRRow = NCRDt.Rows[0];

            ////기준 값
            //string defectRateString = NCRRow["NGRATE"].ToString();
            //string defectQuantityString = NCRRow["NGQUANTITY"].ToString();

            //if (!string.IsNullOrWhiteSpace(defectRateString))
            //{
            //    decimal defectRate = defectRateString.ToSafeDecimal();

            //    //불량율이 기준을 벗어났을 경우
            //    if (row["DEFECTRATE"].ToSafeDecimal() >= defectRate)
            //    {
            //        result = "NG";
            //    }
            //    else
            //    {
            //        result = "OK";
            //    }
            //}
            //else if (!string.IsNullOrWhiteSpace(defectQuantityString))
            //{
            //    decimal defectQuantity = defectQuantityString.ToSafeDecimal();

            //    //불량율이 기준을 벗어났을 경우
            //    if (row["SPECOUTQTY"].ToSafeDecimal() >= defectQuantity)
            //    {
            //        result = "NG";
            //    }
            //    else
            //    {
            //        result = "OK";
            //    }
            //}

            // 불량율 기준을 찾기 위한 currentRow의 파라미터
            //Dictionary<string, object> defectRateParam = new Dictionary<string, object>();
            //defectRateParam.Add("PLANTID", CurrentDataRow["PLANTID"]);
            //defectRateParam.Add("CONSUMABLEDEFID", CurrentDataRow["CONSUMABLEDEFID"]);
            //defectRateParam.Add("VENDORID", CurrentDataRow["VENDORID"]);
            //defectRateParam.Add("INSPITEMID", row["INSPITEMID"]);
            //defectRateParam.Add("INSPITEMMIDDLECLASSID", row["INSPITEMCLASSID"]);

            // 불량율 기준값을 가져오는 쿼리 실행
            //SqlQuery defectRateStandard = new SqlQuery("GetSpecLimitValue", "10001", defectRateParam);
            //DataTable defectRateTable = defectRateStandard.Execute();

            //if (defectRateTable.Rows.Count == 0)
            //{
            //    this.ShowMessage("NoSpecDetail");//유효성 검사를 할 Spec정보가 등록되어있지 않습니다.
            //    result = "";
            //}
            //else
            //{
            //불량율 기준값
            //DataRow defectRateRow = defectRateTable.Rows[0];


            //불량율 기준값
            //decimal lsl = decimal.Parse(defectRateRow[""].ToString());
            //decimal usl = decimal.Parse(defectRateRow[""].ToString());
            //}
            
            return result;
        }
    
    */
        /// <summary>
        /// MaterialLotId, 접수일에 입력된 값이 있을때 변경 불가 함수
        /// </summary>
        /// <param name="table"></param>
        private void SetEnableControl(DataTable table)
        {
            DataRow row = table.Rows[0];

            if (!string.IsNullOrWhiteSpace(row["RESOURCEID"].ToString()))
            {
                txtMaterialLotId.Enabled = false;
            }

            if (!string.IsNullOrWhiteSpace(row["ACCEPTDATE"].ToString()))
            {
                dateAccepteDate.Enabled = false;
            }
        }

        /// <summary>
        /// 파일추가시 데이터
        /// </summary>
        private void InitializeImageFileControl()
        {
            fpcImage.UploadPath = "RawAssy/Image";
            fpcImage.Resource = new ResourceInfo()
            {
                Type = "RawAssyImage",
                Id = CurrentDataRow["ORDERNUMBER"].ToString() + CurrentDataRow["STORENO"].ToString(),
                Version = "0"
            };

            fpcImage.UseCommentsColumn = true;
        }


        /// <summary>
        /// 모든 검사 결과를 입력 했는지, 사진을 첨부했는지 확인하는 함수
        /// </summary>
        /// <returns></returns>
        private Boolean CheckAllResultAndImageInput(SmartBandedGrid grid, bool isCheckQTY = false)
        {
            DataTable changed = grid.DataSource as DataTable;

            bool inputAllResult = true;
            _hasImage = true;

            foreach (DataRow row in changed.Rows)
            {
                if (string.IsNullOrWhiteSpace(row["INSPECTIONRESULT"].ToString()) || (isCheckQTY == true && string.IsNullOrWhiteSpace(row["INSPECTIONQTY"].ToString())))
                {
                    inputAllResult = false;
                    break;
                }

                if (row["INSPECTIONRESULT"].ToString().Equals("NG"))
                {
                    if (string.IsNullOrWhiteSpace(row["FILEFULLPATH1"].ToString()) && string.IsNullOrWhiteSpace(row["FILEFULLPATH2"].ToString()))
                    {
                        if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
                        inputAllResult = false;

                        _hasImage = false;
                        break;
                    }
                }
            }

            return inputAllResult;
        }

        /// <summary>
        /// 모든 검사 결과를 입력 했는지 확인하는 함수
        /// </summary>
        /// <returns></returns>
        private Boolean CheckAllResultInput(SmartBandedGrid grid)
        {
            DataTable changed = grid.DataSource as DataTable;

            bool inputAllResult = true;
            _hasImage = true;

            foreach (DataRow row in changed.Rows)
            {
                if (string.IsNullOrWhiteSpace(row["INSPECTIONRESULT"].ToString()))
                {
                    inputAllResult = false;
                    break;
                }
            }

            return inputAllResult;
        }
        /// <summary>
        /// 저장 할 데이터가 있는지 체크하는 함수
        /// </summary>
        /// <param name="state"></param>
        /// <param name="table"></param>
        /// <param name="table2"></param>
        /// <param name="table3"></param>
        private void CheckChanged(string state,DataTable table, DataTable table2, DataTable table3)
        {
            if (state.Equals("unchanged") && table.Rows.Count == 0 && table2.Rows.Count == 0 && table3.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }
        //---------------------------------------------------------------------------------------------------------------------
        #endregion
    }
}
