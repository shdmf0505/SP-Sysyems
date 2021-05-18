using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.QualityAnalysis.Helper;
using SmartDeploy.Common;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 > 원자재/원자재가공품 수입검사 결과등록
    /// 업  무  설  명  : 원자재 수입검사 결과등록하는 팝업
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-06-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RawMaterialImportInspectionResultPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }
        

        #endregion

        #region Local Variables
        private DataTable _consumableInspectionTable;// 검색 한 inspectionResult 결과를 담을 테이블 
        public DataTable _defectTable;
        public DataTable _measureTable;
        private DataTable _fileDt;
        private bool _hasImage = true;
        private DataRow _toCopyRow = null;
        int _toCopyHandle = 0;
        private bool _autoChange = false;
        private string _type;
        public bool isEnable = true;
        //2020-02-19 강유라 이메일 첨부용
        DataTable _FileToSendEmail;
        #endregion

        #region 생성자
        public RawMaterialImportInspectionResultPopup(string type)
        {
            _type = type;
            InitializeComponent();
            InitializeGrid();
            InitializeEvent();

        }
        #endregion

        #region 컨텐츠 영역 초기화

        #region 외관검사 탭 초기화
        private void InitializeGrid()
        {
            grdInspectionItem.GridButtonItem = GridButtonItem.None;
            grdInspectionItem.View.OptionsView.AllowCellMerge = true;

            grdInspectionItem.View.SetSortOrder("SORT");

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMCLASSID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMMIDDLECLASSID", 200)
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

            grdInspectionItem.View.AddSpinEditColumn("MEASUREVALUE", 150)
                .SetIsHidden();

            grdInspectionItem.View.AddComboBoxColumn("INSPECTIONRESULT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdInspectionItem.View.AddTextBoxColumn("INSPECTIONSTANDARD", 200)
                .SetIsReadOnly()
                .SetIsHidden();

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

            //=====================================================================================================================================
            //YJKIM : 파일의 전체경로를 저장할 필드설정
            grdInspectionItem.View.AddTextBoxColumn("FILEFULLPATH1", 10)
                .SetIsHidden()
                ;
            grdInspectionItem.View.AddTextBoxColumn("FILEFULLPATH2", 10)
                .SetIsHidden()
                ;
            grdInspectionItem.View.AddTextBoxColumn("FILEID1", 10)
               .SetIsHidden()
               ;
            grdInspectionItem.View.AddTextBoxColumn("FILEID2", 10)
                .SetIsHidden()
                ;
            //--------------------------------------------------------------------------------------------------------------------------------------

            grdInspectionItem.View.PopulateColumns();
        }
        #endregion

        #region 측정검사 탭 초기화
        private void InitializeGridMeasure()
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


        }
        #endregion

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
            conditionItem.ValueFieldName = "USERID";
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
            this.Load += RawMaterialImportInspectionResultPopup_Load;

            //특정 컬럼만 Merge하는 이벤트 
            grdInspectionItem.View.CellMerge += View_CellMerge;
            grdInspectionItemSpec.View.CellMerge += View_CellMerge;

            //측정값을 입력하면 스펙체크를 하여 판정결과를 자동판정하는 이벤트
            grdInspectionItemSpec.View.CellValueChanged += View_CellValueChangedSpecCheck;

            //소분류의 판정결과에 따라 대분류의 판정결과를 자동판정하는 이벤트
            //grdInspectionItem.View.CellValueChanged += View_CellValueChangedItemResult;
            //grdInspectionItemSpec.View.CellValueChanged += View_CellValueChangedItemResult;

            //대분류의 판정결과에 따라 전체 판정결과를 자동판정하는 이벤트
            //grdInspectionItem.View.CellValueChanged += View_CellValueChangedClassItemResult;
            //grdInspectionItemSpec.View.CellValueChanged += View_CellValueChangedClassItemResult;

            //검사타입에 따라 컬럼(측정값,판정결과)을 readonly 처리하는 이벤트
            grdInspectionItem.View.ShowingEditor += View_ShowingEditor;
            grdInspectionItemSpec.View.ShowingEditor += View_ShowingEditor;

            //팝업저장버튼을 클릭시 이벤트
            btnSave.Click += BtnSave_Click;
            //팝업닫기버튼을 클릭시 이벤트
            btnClose.Click += BtnClose_Click;

            //grdInspectionItem에 입력된 값은 수정하지 못하게 하는 이벤트
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
        /// 각각의 판정결과에 의해 전체 검사결과를 판정하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChangedAllResult(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;
            DataRow row = view.GetDataRow(e.RowHandle);

            if (e.Column.FieldName == "INSPECTIONRESULT" && row != null)
            {
                string sequence = "";

                if (row["NCRDECISIONDEGREE"] == null)
                {
                    ShowMessage("NoActionStandardData");//판정등급이 없습니다.
                    return;
                }

                string qcgrade = InspectionHelper.GetQcGradeAndSequenceNCRAndSpecType("RawInspection", row["NCRDECISIONDEGREE"].ToString(), out sequence);

                row["QCGRADE"] = qcgrade;
                row["PRIORITY"] = sequence;

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
                }

            }

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

            InitializeGridMeasure();
            grdInspectionItemSpec.DataSource = dt;
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
            InitializeGridMeasure();
            grdInspectionItemSpec.DataSource = dt;
            grdInspectionItemSpec.View.FocusedRowHandle = _toCopyHandle;
        }

        /// <summary>
        /// 로드 이벤트
        /// 팝업내의 팝업초기화 및 데이터로드
        /// 컨트롤 설정(그리드 확대 / 축소 버튼 안보이게)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RawMaterialImportInspectionResultPopup_Load(object sender, EventArgs e)
        {
            InitializeGridMeasure();

            btnSave.Enabled = isEnable;

            fpcImage.LanguageKey = "IMAGE";
            fpcReport.LanguageKey = "SUPPLIERREPORTATTACHMENT";
            InitializeImageFileControl();
            InitializeReportFileControl();
            grpMaterialInfo.GridButtonItem = GridButtonItem.None;
            grpInspectionInfo.GridButtonItem = GridButtonItem.None;

            picDefect.Properties.ShowMenu = false;
            picDefect2.Properties.ShowMenu = false;

            if (_type.Equals("insertData"))
            {//이미등록된 결과가 없을 때
                grdInspectionItem.View.FocusedRowChanged += FocusedRowChangedBeforeSave;
            }
            else
            {
                grdInspectionItem.View.FocusedRowChanged += View_FocusedRowChanged;
            }

            OnSearch();
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

                if (row["FILEFULLPATH1"].ToString() == string.Empty && row["FILENAME1"].ToString() == string.Empty
                        && row["FILEFULLPATH2"].ToString() == string.Empty && row["FILENAME2"].ToString() == string.Empty)
                {
                    return;
                }

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
                //====================================================================================================================================
                //YJKIM : 파일다운로드부분을 작성
                Dictionary<string, object> values = new Dictionary<string, object>()
                {
                    {"RESOURCETYPE", "RawInspection" },
                    {"RESOURCEID", CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString() + row["INSPITEMID"]},
                    {"RESOURCEVERSION", "*"}
                };

                _fileDt = SqlExecuter.Query("GetFileHttpPathFromObjectFileByStandardInfo", "10001", values);
                foreach (DataRow fileRow in _fileDt.Rows)
                {
                    string filenameAndExt = fileRow.GetString("FILENAME") + "." + fileRow.GetString("FILEEXT");
                    if (picDefect.Image == null)
                    {
                        //2020-01-28 파일 경로변경
                        picDefect.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                    }
                    else
                    {
                        picDefect2.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                    }

                }
                //____________________________________________________________________________________________________________________________________
                //Dictionary<string, object> values = new Dictionary<string, object>()
                //{
                //    {"FILEINSPECTIONTYPE","RawInspection" },
                //    {"FILERESOURCEID",CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString() + row["INSPITEMID"]},
                //    {"PROCESSRELNO",CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString()}

                //};

                //_fileDt = SqlExecuter.Query("SelectInspectionResultImage", "10001", values);

                //foreach (DataRow fileRow in _fileDt.Rows)
                //{
                //    ImageConverter converter = new ImageConverter();

                //    if (picDefect.Image == null)
                //    {
                //        picDefect.Image = (Image)converter.ConvertFrom(fileRow["FILEDATA"]);
                //    }
                //    else
                //    {
                //        picDefect2.Image = (Image)converter.ConvertFrom(fileRow["FILEDATA"]);
                //    }

                //}
                //-------------------------------------------------------------------------------------------------------------------------------------

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

                    row["FILEINSPECTIONTYPE"] = "RawInspection";
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
                            row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            row["FILEFULLPATH1"] = dialog.FileName; //파일의 전체경로 저장
                            row["FILEID1"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            //row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            //-------------------------------------------------------------------------------------------------------------------------
                            row["FILECOMMENTS1"] = "InspectionResult/RawInspection";
                            row["FILESIZE1"] = Math.Round(Convert.ToDouble(fileInfo.Length) / 1024);
                            row["FILEEXT1"] = fileInfo.Extension.Substring(1);                       

                            picDefect.Image = Image.FromStream(ms);
                        }
                        else
                        {
                            row["FILENAME2"] = dialog.SafeFileName;
                            row["FILEID2"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            //=========================================================================================================================
                            //YJKIM : binary파일을 저장하지 않고 File을 Upload하는 형태로 변경
                            //2020-02-19 강유라 이메일 첨부용
                            row["FILEDATA2"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            row["FILEFULLPATH2"] = dialog.FileName; //파일의 전체경로 저장
                            //-------------------------------------------------------------------------------------------------------------------------

                            row["FILECOMMENTS2"] = "InspectionResult/RawInspection";
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
        /// Spec 값에 따라 판정결과를 입력 해 주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChangedSpecCheck(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //측정값이 바뀌었을때
            if (e.Column.FieldName == "MEASUREVALUE")
            {
                DataRow row = grdInspectionItemSpec.View.GetFocusedDataRow();
                //스펙을 찾기위한 파라미터가 없는경우 return
                if (string.IsNullOrWhiteSpace(row["SPECCLASSID"].ToString()) || string.IsNullOrWhiteSpace(row["SPECSEQUENCE"].ToString()))
                    return;

                //스펙체크 결과에 의해 판정결과 값 입력
                grdInspectionItemSpec.View.SetFocusedRowCellValue("INSPECTIONRESULT", CheckValidationSpecOut(row));
            }
        }

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

        #region 삭제 대상
        ///// <summary>
        ///// 소분류의 판정 결과에 따라 대분류를 결과를 자동판정 해주는 이벤트
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void View_CellValueChangedItemResult(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    //판정결과값이 바뀌었을때
        //    if (e.Column.FieldName == "INSPECTIONRESULT")
        //    {
        //        DataRow row = grdInspectionItem.View.GetFocusedDataRow();
        //        //ROW의 SORT값 ex)10,10_1..
        //        string order = row["SORT"].ToString();

        //        //소분류가 아니면 return
        //        if (!order.Contains("_")) return;

        //        //대분류의 SORT값 ex)10
        //        string itemClassOrder = order.Substring(0, order.IndexOf("_"));

        //        //대분류에 해당하는 소분류를 찾기위한  SORT값 ex)10-
        //        string itemOrder = order.Substring(0, order.IndexOf("_") + 1);

        //        //대분류의 rowhandle
        //        int handle = grdInspectionItem.View.LocateByValue("SORT", itemClassOrder);
        //        if (handle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
        //            grdInspectionItem.View.FocusedRowHandle = handle;

        //        //소분류의 판정결과가 NG 일때
        //        if (e.Value.ToString() == "NG" || string.IsNullOrWhiteSpace(e.Value.ToString()))
        //        {
        //            grdInspectionItem.View.SetRowCellValue(handle, "INSPECTIONRESULT", "NG");
        //        }
        //        //소분류의 판정결과가 OK 일때
        //        else if (e.Value.ToString() == "OK")
        //        {
        //            DataTable tempTable = grdInspectionItem.DataSource as DataTable;

        //            var query = tempTable.AsEnumerable().Where(x => System.Text.RegularExpressions.Regex.IsMatch(x.Field<string>("SORT"), itemOrder))
        //                                                .Select(x => x["INSPECTIONRESULT"]).ToList();

        //            if (!query.Contains("NG"))
        //            {
        //                grdInspectionItem.View.SetRowCellValue(handle, "INSPECTIONRESULT", "OK");
        //            }

        //        }
        //    }
        //}

        ///// <summary>
        ///// 대분류의 판정 결과에 따라 전체 결과를 자동판정 해주는 이벤트
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void View_CellValueChangedClassItemResult(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    DataRow row = grdInspectionItem.View.GetFocusedDataRow();

        //    //ROW의 SORT값 ex)10,10_1..
        //    string order = row["SORT"].ToString();

        //    //대분류가 아니면 return
        //    if (order.Contains("_")) return;

        //    if (e.Column.FieldName == "INSPECTIONRESULT")
        //    {
        //        //대분류의 판정결과가 NG 일때
        //        if (e.Value.ToString() == "NG" || string.IsNullOrWhiteSpace(e.Value.ToString()))
        //        {
        //            txtInspectionResult.EditValue = "NG";
        //        }
        //        //대분류의 판정결과가 OK 일때
        //        else if (e.Value.ToString() == "OK")
        //        {
        //            DataTable tempTable = grdInspectionItem.DataSource as DataTable;

        //            var query = tempTable.AsEnumerable().Where(x => !System.Text.RegularExpressions.Regex.IsMatch(x.Field<string>("SORT"), "_"))
        //                                                .Select(x => x["INSPECTIONRESULT"]).ToList();

        //            if (!query.Contains("NG"))
        //            {
        //                txtInspectionResult.EditValue = "OK";
        //            }

        //        }
        //    }
        //}

        ///// <summary>
        ///// inspectionType에 따라 (QTY,OK_NG,SPC)
        ///// inspectionResult와 measurevalue
        ///// readonly 처리하는 이벤트
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void View_ShowingEditor(object sender, CancelEventArgs e)
        //{
        //    var inspType = this.grdInspectionItem.View.GetRowCellValue(grdInspectionItem.View.FocusedRowHandle, "INSPITEMTYPE");
        //    var sort = this.grdInspectionItem.View.GetRowCellValue(grdInspectionItem.View.FocusedRowHandle, "SORT");
        //    var inspItemClassId = this.grdInspectionItem.View.GetRowCellValue(grdInspectionItem.View.FocusedRowHandle, "INSPITEMCLASSID");
        //    if (string.IsNullOrWhiteSpace(CurrentDataRow["TXNHISTKEY"].ToString()))
        //    {//이미등록된 결과가 없을 때

        //        if (grdInspectionItem.View.FocusedColumn.FieldName == "INSPECTIONRESULT")
        //        {   //inpitemtype 이 Spec인 경우 inspectionresult readonly => CheckValidationSpecOut 결과에 의해 자동판정
        //            if (inspType != null && inspType.ToString() == "SPC")
        //            {
        //                this.ShowMessage("InspItemTypeSpecCantInputResult");//측정값을 입력하면 판정결과가 자동입력 됩니다.
        //                e.Cancel = true;
        //            }
        //            //inpitemtype 이 OK_NG이고 대분류이면서 소분류를 가지고 있는 경우 readonly => 소분류 값에 의해 자동판정
        //            else if (inspType != null && inspType.ToString() == "OK_NG" && string.IsNullOrWhiteSpace(inspItemClassId.ToString()))
        //            {
        //                DataTable tempTable = grdInspectionItem.DataSource as DataTable;

        //                var query = tempTable.AsEnumerable().Where(x => System.Text.RegularExpressions.Regex.IsMatch(x.Field<string>("SORT"), sort.ToString()))
        //                                                    .Select(x => x["SORT"]).ToList();
        //                if (query.Contains(sort.ToString() + "_1"))
        //                {
        //                    this.ShowMessage("InspItemTypeYesNoLargeCantInputResult");//소분류의 검사결과를 입력하면 대분류의 판정결과가 자동으로 입력됩니다.
        //                    e.Cancel = true;
        //                }

        //            }

        //        }
        //        else if (grdInspectionItem.View.FocusedColumn.FieldName == "MEASUREVALUE")
        //        {
        //            if (inspType != null && inspType.ToString() == "OK_NG")
        //            {
        //                this.ShowMessage("InspItemTypeYesNoLargeCantInputMeasureValue");//측정값은 입력할 수 없습니다. 판정결과를 입력하세요.
        //                e.Cancel = true;
        //            }

        //        }
        //    }

        //}
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
                }
                /*
                if (gridView.FocusedColumn.FieldName == "INSPECTIONRESULT" || gridView.FocusedColumn.FieldName == "MEASUREVALUE")
                {   //inpitemtype 이 NOTYPE인 경우 readonly
                    if (inspType != null && inspType.ToString() == "NOTYPE")
                    {
                        this.ShowMessage("InspItemTypeYesNoLargeCantInputResult");//소분류의 검사결과를 입력하면 대분류의 판정결과가 자동으로 입력됩니다.
                        e.Cancel = true;
                    }

                }*/
            }
        }
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

                if (string.IsNullOrEmpty(popRequesterUser.Text)) // 2020.02.27-유석진-의뢰자 {0} 항목은 필수 입력입니다
                {
                    string[] param = new string[1];
                    param[0] = Language.Get("REQUESTER");
                    this.ShowMessage("InValidRequiredField", param);

                    return;
                }

                if (CheckAllResultAndImageInput(grdInspectionItem) == false || CheckAllResultInput(grdInspectionItemSpec) == false)
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

                    //inspectionItemResult Table에 저장 될 Data
                    DataTable changed = (grdInspectionItem.DataSource as DataTable).Copy();
                    changed.TableName = "list2";

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

                    //2020-02-19 강유라 이메일 첨부용
                    _FileToSendEmail = QcmImageHelper.GetImageFileTableToSendEmail();

                    int totalFileSize = 0;
                    foreach (DataRow originRow in changed.Rows)
                    {
                        if (!originRow.IsNull("FILENAME1"))
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

                        if (!originRow.IsNull("FILENAME2"))
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

                    //Imagefile DataTable
                    DataTable ImageFileTable = fpcImage.GetChangedRows();
                    ImageFileTable.TableName = "list3";

                    //Reportfile DataTable
                    DataTable ReportFileTable = fpcReport.GetChangedRows();
                    ReportFileTable.TableName = "list4";

                    //inspectionItemResult Table에 저장 될 Data - Measure
                    //DataTable measureChanged = (grdInspectionItemSpec.DataSource as DataTable).AsEnumerable()
                    //   .Where(r => !string.IsNullOrWhiteSpace(r["INSPECTIONRESULT"].ToString()))
                    //   .CopyToDataTable(); 

                    DataTable measureChanged = (grdInspectionItemSpec.DataSource as DataTable).Copy();

                    measureChanged.TableName = "list5";
                    CheckChanged(inspectionResult.Rows[0]["_STATE_"].ToString(), changed, ImageFileTable, ReportFileTable);

                    if (fpcImage.GetChangedRows().Rows.Count > 0)
                    {
                        fpcImage.SaveChangedFiles();
                    }


                    if (fpcReport.GetChangedRows().Rows.Count > 0)
                    {
                        fpcReport.SaveChangedFiles();
                    }
  
                    MessageWorker worker = new MessageWorker("SaveRawMaterialInspection");
                    worker.SetBody(new MessageBody()
                    {

                        { "list", inspectionResult},
                        { "list2", changed},
                        { "list3", ImageFileTable},
                        { "list4", ReportFileTable},
                        { "list5", measureChanged},
                        { "ENTERPRISEID", UserInfo.Current.Enterprise},
                        { "PLANTID", UserInfo.Current.Plant},
                        { "inspectionclassId", "RawInspection"},

                    });

                    var isSendEmailDt = worker.Execute<DataTable>();
                    var isSendEmailRS = isSendEmailDt.GetResultSet();

                    if (isSendEmailRS.Rows.Count > 0)
                    {

                        DataRow responseRow = isSendEmailRS.Rows[0];

                        if (responseRow["ISSENDEMAIL"].ToString().Equals("True"))
                        {                           
                            string exteriorNG = responseRow["EXTERIORNG"].ToString();
                            string measureNG = responseRow["MEASURENG"].ToString();

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
                            newRow["MEASUREVALUE"] =  measureNG;
                            newRow["INSPECTIONRESULT"] = "NG";
                            newRow["REMARK"] = "";
                            newRow["USERID"] = UserInfo.Current.Id;
                            newRow["TITLE"] = Language.Get("RAWABNORMALTITLE");
                            newRow["INSPECTION"] = "RawInspection";
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

        /// <summary>
        /// 닫기버튼을 클릭했을때 팝업을 닫는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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

        #region 검색
        /// <summary>
        /// 조회 함수
        /// </summary>
        public void OnSearch()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_CONSUMABLEDEFID", CurrentDataRow["CONSUMABLEDEFID"]);
            values.Add("P_CONSUMABLEDEFVERSION", CurrentDataRow["CONSUMABLEDEFVERSION"]);
            values.Add("P_ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            values.Add("P_PLANTID", CurrentDataRow["PLANTID"]);
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_ORDERNUMBER", CurrentDataRow["ORDERNUMBER"]);
            values.Add("P_RESOURCEID", CurrentDataRow["MATERIALLOTID"]);
            values.Add("P_PROCESSRELNO", CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString());
            values.Add("RESOURCETYPE", "RawInspection");//**원자재
            values.Add("P_RESULTTXNHISTKEY", CurrentDataRow["TXNHISTKEY"].ToString());
            values.Add("P_RESULTTXNGROUPHISTKEY", CurrentDataRow["TXNGROUPHISTKEY"].ToString());
            values.Add("P_INSPITEMTYPE", "OK_NG");
            values.Add("P_INSPECTIONDEFID", "RawInspection");
            values.Add("P_INSPECTIONCLASSID", "RawInspection");

            Dictionary<string, object> imageValues = new Dictionary<string, object>();
            //이미지 파일Search parameter
            imageValues.Add("P_FILERESOURCETYPE", "RawMaterialImage");
            imageValues.Add("P_FILERESOURCEID", CurrentDataRow["ORDERNUMBER"].ToString() + CurrentDataRow["STORENO"].ToString());//InitializeFileControl 설정 정보에 따라 수정***
            imageValues.Add("P_FILERESOURCEVERSION", "0");

            Dictionary<string, object> reportValues = new Dictionary<string, object>();
            //성적서 파일Search parameter
            reportValues.Add("P_FILERESOURCETYPE", "RawMaterialReport");
            reportValues.Add("P_FILERESOURCEID", CurrentDataRow["ORDERNUMBER"].ToString() + CurrentDataRow["STORENO"].ToString());//InitializeFileControl 설정 정보에 따라 수정***
            reportValues.Add("P_FILERESOURCEVERSION", "0");

            
            //InspectionInfo
            DataTable inspectionTable = SqlExecuter.Query("SelectRawMaterialResult", "10001", values);

            //외관검사
            //ItemClassTable
            //DataTable defectItemClassTable = SqlExecuter.Query("SelectRawMaterialItemClass", "10002", values);
            //ItemTable
            DataTable defectItemTable = SqlExecuter.Query("SelectRawInspectionExterior", "10001", values);

            values.Remove("P_INSPITEMTYPE");
            values.Add("P_INSPITEMTYPE", "SPC");

            //측정검사
            //ItemClassTable
            //DataTable measureItemClassTable = SqlExecuter.Query("SelectRawMaterialItemClass", "10001", values);
            //ItemTable
            DataTable measureItemTable = SqlExecuter.Query("SelectRawInspectionMeasure", "10001", values);

            //ImageFileTable
            DataTable imageFileTable = SqlExecuter.Query("SelectFileInspResult", "10001", imageValues);
            //ReportFileTable
            DataTable reportFileTable = SqlExecuter.Query("SelectFileInspResult", "10001", reportValues);

            //InspectionInfo
            _consumableInspectionTable = inspectionTable;
            SetControlSearchData(_consumableInspectionTable);//inspectionResult 정보 컨트롤 바인딩
            SetEnableControl(_consumableInspectionTable);//MaterialId, 접수일 입력 된 내용 있을 때 Enable


            //grdInspectionItem - 합불판정
            //grdInspectionItem.DataSource = SetDataTableOrder(defectItemClassTable, defectItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
            string processRelNo = CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString();

            grdInspectionItem.DataSource = InspectionHelper.SetProcessRelNo(processRelNo, defectItemTable);

            //grdInspectionItemSpec - 측정검사
            //grdInspectionItemSpec.DataSource = SetDataTableOrder(measureItemClassTable, measureItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
            grdInspectionItemSpec.DataSource = InspectionHelper.SetProcessRelNo(processRelNo, measureItemTable);

            //ImageFileTable
            fpcImage.DataSource = imageFileTable;//이미지 파일데이터 그리드 바인딩
            //ReportFileTable
            fpcReport.DataSource = reportFileTable;//이미지 파일데이터 그리드 바인딩
        }
        #endregion

        #region Private Function
        //private void ClearInput()
        //{
        //    //컨트롤추가 필요
        //    txtVendor.EditValue = null;

        //}
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
            //inspectionTable.Columns.Add(new DataColumn("AREAID", typeof(string)));
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
            inspectionTable.Columns.Add(new DataColumn("_STATE_", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("AREAID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONCLASSID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("QCGRADE", typeof(string)));
     

            row = inspectionTable.NewRow();
            row["TXNHISTKEY"] = searchedRow["TXNHISTKEY"];
            row["ORDERNUMBER"] = CurrentDataRow["ORDERNUMBER"];
            row["CONSUMABLEDEFID"] = CurrentDataRow["CONSUMABLEDEFID"];
            row["STORENO"] = CurrentDataRow["STORENO"];
            row["PROCESSRELNO"] = CurrentDataRow["ORDERNUMBER"] + CurrentDataRow["STORENO"].ToString();
            row["CONSUMABLEDEFVERSION"] = CurrentDataRow["CONSUMABLEDEFVERSION"];
            //row["AREAID"] = CurrentDataRow["AREAID"];
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
              fpcImage.GetChangedRows().Rows.Count == 0 &&
              fpcReport.GetChangedRows().Rows.Count == 0)
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
            if (!string.IsNullOrWhiteSpace(row["MEASUREVALUE"].ToString()))
            {
                //spec값을 찾기 위한 currentRow의 파라미터
                Dictionary<string, object> specParam = new Dictionary<string, object>();
                specParam.Add("P_SPECCLASSID", row["SPECCLASSID"]);
                specParam.Add("P_SPECSEQUENCE", row["SPECSEQUENCE"]);
                specParam.Add("P_CONTROLTYPE", "XBARR");

                //spec값을 가져오는 쿼리 실행

                SqlQuery specRange = new SqlQuery("GetSpecLimitValue", "10002", specParam);
                DataTable specTable = specRange.Execute();

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
            else
            {
                return "";
            }

        }
        */
        #endregion

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
        /// 파일추가시 데이터 **** map정보 Key 고려
        /// </summary>
        private void InitializeImageFileControl()
        {
            fpcImage.UploadPath = "RawMaterial/Image";
            fpcImage.Resource = new ResourceInfo()
            {
                Type = "RawMaterialImage",
                Id = CurrentDataRow["ORDERNUMBER"].ToString() + CurrentDataRow["STORENO"].ToString(),
                Version = "0"
            };

            fpcImage.UseCommentsColumn = true;
        }

        /// <summary>
        /// 파일추가시 데이터 **** map정보 Key 고려
        /// </summary>
        private void InitializeReportFileControl()
        {
            fpcReport.UploadPath = "RawMaterial/Report";
            fpcReport.Resource = new ResourceInfo()
            {
                Type = "RawMaterialReport",
                Id = CurrentDataRow["ORDERNUMBER"].ToString() + CurrentDataRow["STORENO"].ToString(),
                Version = "0"
            };

            fpcReport.UseCommentsColumn = true;
        }
        /// <summary>
        /// 모든 검사 결과를 입력 했는지, 사진을 첨부했는지 확인하는 함수
        /// </summary>
        /// <returns></returns>
        private Boolean CheckAllResultAndImageInput(SmartBandedGrid grid)
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

                if (row["INSPECTIONRESULT"].ToString().Equals("NG"))
                {
                    if (string.IsNullOrWhiteSpace(row["FILEFULLPATH1"].ToString()) && string.IsNullOrWhiteSpace(row["FILEFULLPATH2"].ToString()))
                    {
                        if(UserInfo.Current.Enterprise.Equals("INTERFLEX"))
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
        private void CheckChanged(string state, DataTable table, DataTable table2, DataTable table3)
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