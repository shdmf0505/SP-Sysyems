#region using

using Micube.Framework.Net;
using Micube.Framework;
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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout.Customization;
using DevExpress.XtraEditors.Repository;
using Micube.SmartMES.Commons.SPCLibrary;
using Micube.SmartMES.Commons;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 약품관리 > 보충량 등록 재분석 현황
    /// 업  무  설  명  : 약품등록값 이력을 분석하여 적정량, 분석치, 보충량등을 다시 등록한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ChemicalReanalysisRequest : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 재분석이력 마감여부
        /// </summary>
        private string _closeFlag = "N";

        /// <summary>
        /// 재분석이력 마지막차수
        /// </summary>
        private int _lastDegree = 0;

        /// <summary>
        /// 약품별로 스펙을 담고있는 데이터테이블
        /// </summary>
        private DataTable _chemicalSpecDt = new DataTable();

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ChemicalReanalysisRequest()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGenerationGrid();
            InitializeReanalysisGrid();

            InitializeEvent();
        }

        /// <summary>        
        /// 발생이력 그리드 초기화
        /// </summary>
        private void InitializeGenerationGrid()
        {
            grdGenerationHistory.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdGenerationHistory.View.SetSortOrder("DEGREE", DevExpress.Data.ColumnSortOrder.Ascending);

            grdGenerationHistory.View.AddTextBoxColumn("ANALYSISDATE", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 분석일
            grdGenerationHistory.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 100)
                .SetLabel("LARGEPROCESSSEGMENTNAME")
                .SetIsReadOnly(); // 대공정명
            grdGenerationHistory.View.AddTextBoxColumn("FACTORYNAME", 80)
                .SetIsReadOnly(); // 공장명
            //grdGenerationHistory.View.AddTextBoxColumn("EQUIPMENTNAME", 150)
            //    .SetIsReadOnly(); // 설비명
            grdGenerationHistory.View.AddTextBoxColumn("STATENAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("STATE")
                .SetIsReadOnly(); // 상태
            grdGenerationHistory.View.AddTextBoxColumn("DEGREE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 차수
            grdGenerationHistory.View.AddTextBoxColumn("EQUIPMENTNAME", 150)
                .SetIsReadOnly(); // 설비명
            grdGenerationHistory.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 150)
                .SetIsReadOnly(); // 설비단명
            grdGenerationHistory.View.AddTextBoxColumn("CHEMICALNAME", 120)
                .SetIsReadOnly(); // 약품명
            grdGenerationHistory.View.AddTextBoxColumn("CHEMICALLEVEL", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();// 약품등급
            grdGenerationHistory.View.AddTextBoxColumn("MANAGEMENTSCOPE", 140)
                .SetIsReadOnly(); // 관리범위
            grdGenerationHistory.View.AddTextBoxColumn("SPECSCOPE", 140)
                .SetIsReadOnly(); // 규격범위

            grdGenerationHistory.View.AddSpinEditColumn("TITRATIONQTY", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetIsReadOnly(); // 적정량
            grdGenerationHistory.View.AddSpinEditColumn("ANALYSISVALUE", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetIsReadOnly(); // 분석량
            grdGenerationHistory.View.AddSpinEditColumn("SUPPLEMENTQTY", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetIsReadOnly(); // 보충량
            grdGenerationHistory.View.AddTextBoxColumn("COLLECTIONTIME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 채취시간대
            grdGenerationHistory.View.AddComboBoxColumn("ISSUPPLEMENT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 액보충필요
            grdGenerationHistory.View.AddComboBoxColumn("ISRESUPPLEMENT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 재분석필요
            grdGenerationHistory.View.AddTextBoxColumn("MESSAGE", 150); // 전달사항
            grdGenerationHistory.View.AddSpinEditColumn("ACTUALCOMPLEMENTQTY", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 실보충량
            grdGenerationHistory.View.AddComboBoxColumn("ISCLOSE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center); // 마감여부
            grdGenerationHistory.View.AddTextBoxColumn("CREATOR", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 생성자
            grdGenerationHistory.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 생성시간
            grdGenerationHistory.View.AddTextBoxColumn("MODIFIER", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 수정자
            grdGenerationHistory.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 수정시간   

            grdGenerationHistory.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID")
                .SetIsHidden(); // 대공정ID
            grdGenerationHistory.View.AddTextBoxColumn("EQUIPMENTID")
                .SetIsHidden(); // 설비ID
            grdGenerationHistory.View.AddTextBoxColumn("CHILDEQUIPMENTID")
                .SetIsHidden(); // 설비단ID
            grdGenerationHistory.View.AddTextBoxColumn("INSPITEMID")
                .SetIsHidden(); // 약품ID
            grdGenerationHistory.View.AddTextBoxColumn("SEQUENCE")
                .SetIsHidden(); // 일련번호
            grdGenerationHistory.View.AddTextBoxColumn("ANALYSISTYPE")
                .SetIsHidden(); // 약품수질구분
            grdGenerationHistory.View.AddTextBoxColumn("REANALYSISTYPE")
                .SetIsHidden(); // 재분석구분
            grdGenerationHistory.View.AddTextBoxColumn("PMTYPE")
                .SetIsHidden(); // 구분
            grdGenerationHistory.View.AddTextBoxColumn("PMTYPENAME")
                .SetIsHidden(); // 구분명
            grdGenerationHistory.View.AddTextBoxColumn("ISSPECOUT")
                .SetDefault("N")
                .SetIsHidden(); // SpecOut 여부
            grdGenerationHistory.View.AddTextBoxColumn("TANKSIZE")
                .SetIsHidden(); // 탱크사이즈
            grdGenerationHistory.View.AddTextBoxColumn("ANALYSISCONST")
                .SetIsHidden(); // 분석상수
            grdGenerationHistory.View.AddTextBoxColumn("QTYCONST")
                .SetIsHidden(); // 보충량상수
            grdGenerationHistory.View.AddTextBoxColumn("SL")
                .SetIsHidden(); // Default 차트타입 스펙값의 SL값
            grdGenerationHistory.View.AddTextBoxColumn("INSPECTIONDEFID")
                .SetIsHidden();
            grdGenerationHistory.View.AddTextBoxColumn("CALCULATIONTYPE")
                .SetIsHidden(); // 분석값 계산식 유형
            grdGenerationHistory.View.AddTextBoxColumn("FOMULATYPE")
                .SetIsHidden(); // 보충량 계산식 유형
            grdGenerationHistory.View.AddTextBoxColumn("ISMODIFY")
                .SetIsHidden(); // 작업장 통제 권한에 따른 수정가능여부
            grdGenerationHistory.View.AddTextBoxColumn("AREAID")
                .SetIsHidden(); // 작업장 ID
            grdGenerationHistory.View.AddTextBoxColumn("AREANAME")
                .SetIsHidden(); // 작업장명

            grdGenerationHistory.View.PopulateColumns();
            grdGenerationHistory.View.Columns["TITRATIONQTY"].AppearanceCell.BackColor = Color.Moccasin; // 적정량 Column 색깔변경
            grdGenerationHistory.View.OptionsView.AllowCellMerge = true; // CellMerge
            grdGenerationHistory.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>
        /// 재분석이력 그리드 초기화
        /// </summary>
        private void InitializeReanalysisGrid()
        {
            grdReanalysisHistory.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;

            grdReanalysisHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdReanalysisHistory.View.AddTextBoxColumn("DEGREEPOINT", 70)
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationKeyColumn()
                .SetIsReadOnly()
                .SetLabel("순번"); // 순번
            grdReanalysisHistory.View.AddTextBoxColumn("ANALYSISDATETIME", 170)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 분석일자
            grdReanalysisHistory.View.AddSpinEditColumn("TITRATIONQTY", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetValidationIsRequired(); // 적정량
            grdReanalysisHistory.View.AddSpinEditColumn("ANALYSISVALUE", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetIsReadOnly(); // 분석치
            grdReanalysisHistory.View.AddSpinEditColumn("SUPPLEMENTQTY", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetIsReadOnly(); // 보충량
            grdReanalysisHistory.View.AddComboBoxColumn("ISCLOSE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("N")
                .SetTextAlignment(TextAlignment.Center); // 마감여부
            grdReanalysisHistory.View.AddTextBoxColumn("CREATOR", 100)                
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 생성자 
            grdReanalysisHistory.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 생성시간
            grdReanalysisHistory.View.AddTextBoxColumn("MODIFIER", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 수정자
            grdReanalysisHistory.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 수정시간

            grdReanalysisHistory.View.AddTextBoxColumn("ENTERPRISEID")
                .SetIsHidden(); // Enterprise ID
            grdReanalysisHistory.View.AddTextBoxColumn("PLANTID")
                .SetIsHidden(); // Plant ID
            grdReanalysisHistory.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID")
                .SetValidationKeyColumn()
                .SetIsHidden(); // 대공정ID
            grdReanalysisHistory.View.AddTextBoxColumn("EQUIPMENTID")
                .SetValidationKeyColumn()
                .SetIsHidden(); // 설비ID
            grdReanalysisHistory.View.AddTextBoxColumn("CHILDEQUIPMENTID")
                .SetValidationKeyColumn()
                .SetIsHidden(); // 설비단ID
            grdReanalysisHistory.View.AddTextBoxColumn("INSPITEMID")
                .SetValidationKeyColumn()
                .SetIsHidden(); // 약품ID
            grdReanalysisHistory.View.AddTextBoxColumn("PMTYPE")
                .SetValidationKeyColumn()
                .SetIsHidden(); // PMType
            grdReanalysisHistory.View.AddTextBoxColumn("PARENTSEQUENCE")
                .SetIsHidden(); // 상위일련번호
            grdReanalysisHistory.View.AddTextBoxColumn("SEQUENCE")
                .SetValidationKeyColumn()
                .SetIsHidden(); // 일련번호
            grdReanalysisHistory.View.AddTextBoxColumn("DEGREE")
                .SetValidationKeyColumn()
                .SetIsHidden(); // 차수
            grdReanalysisHistory.View.AddTextBoxColumn("ANALYSISTYPE")
                .SetValidationKeyColumn()
                .SetIsHidden(); // 약품수질구분
            grdReanalysisHistory.View.AddTextBoxColumn("REANALYSISTYPE")
                .SetValidationKeyColumn()
                .SetIsHidden(); // 재분석구분
            grdReanalysisHistory.View.AddTextBoxColumn("ANALYSISDATE")
                .SetIsHidden(); // 재분석일
            grdReanalysisHistory.View.AddTextBoxColumn("COLLECTIONTIME")
                .SetIsHidden(); // 재채취시간대
            grdReanalysisHistory.View.AddTextBoxColumn("ANALYSISDATEHISTORY")
                .SetIsHidden(); // 발생이력의 분석일
            grdReanalysisHistory.View.AddTextBoxColumn("DEGREEHISTORY")
                .SetIsHidden(); // 발생이력의 차수
            grdReanalysisHistory.View.AddTextBoxColumn("ISCLOSEHISTORY")
                .SetIsHidden(); // 발생이력의 마감여부
            grdReanalysisHistory.View.AddTextBoxColumn("ISSPECOUT")
                .SetDefault("N")
                .SetIsHidden(); // SpecOut 여부
            grdReanalysisHistory.View.AddTextBoxColumn("TANKSIZE")
                .SetIsHidden(); // 탱크사이즈
            grdReanalysisHistory.View.AddTextBoxColumn("ANALYSISCONST")
                .SetIsHidden(); // 분석상수
            grdReanalysisHistory.View.AddTextBoxColumn("QTYCONST")
                .SetIsHidden(); // 보충량상수
            grdReanalysisHistory.View.AddTextBoxColumn("SL")
                .SetIsHidden(); // Default 차트타입 스펙값의 SL값
            grdReanalysisHistory.View.AddTextBoxColumn("COLVALUE")
                .SetIsHidden(); // 탱크사이즈, 분석상수, 보충량상수중에 하나라도 값이 적용되어있지 않으면 Y반환 (보충량, 분석치를 계산하지 않음)
            grdReanalysisHistory.View.AddTextBoxColumn("REASONCODEID")
                .SetIsHidden(); // 이상발생테이블에 넣을 사유코드
            grdReanalysisHistory.View.AddTextBoxColumn("ISCONTROLLIMITOUT")
                .SetIsHidden(); // 관리범위 체크
            grdReanalysisHistory.View.AddTextBoxColumn("CALCULATIONTYPE")
                .SetIsHidden(); // 분석값 계산식 유형
            grdReanalysisHistory.View.AddTextBoxColumn("FOMULATYPE")
                .SetIsHidden(); // 보충량 계산식 유형
            grdReanalysisHistory.View.AddTextBoxColumn("SPECSEQUENCE")
                .SetIsHidden();

            grdReanalysisHistory.View.PopulateColumns();
            grdReanalysisHistory.View.Columns[2].AppearanceCell.BackColor = Color.Moccasin; // 적정량 Column 색깔변경

            RepositoryItemTimeEdit edit = new RepositoryItemTimeEdit();

            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            edit.Mask.EditMask = "([0-1]?[0-9]|2[0-3]):[0-5][0-9]";
            edit.Mask.UseMaskAsDisplayFormat = true;

            grdReanalysisHistory.View.Columns["COLLECTIONTIME"].ColumnEdit = edit;

            RepositoryItemDateEdit dateEdit = new RepositoryItemDateEdit();

            dateEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            dateEdit.Mask.EditMask = "(19|20){2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])";
            dateEdit.Mask.UseMaskAsDisplayFormat = true;

            grdReanalysisHistory.View.Columns["ANALYSISDATE"].ColumnEdit = dateEdit;
            grdReanalysisHistory.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdGenerationHistory.View.CellMerge += View_CellMerge;
            grdGenerationHistory.View.RowCellStyle += GenerationView_RowCellStyle;
            grdGenerationHistory.View.RowClick += View_RowClick;
            grdGenerationHistory.View.FocusedRowChanged += View_FocusedRowChanged;
            grdGenerationHistory.View.ShowingEditor += View_ShowingEditor;
            grdGenerationHistory.View.CellValueChanged += View_CellValueChanged1;

            grdReanalysisHistory.ToolbarDeletingRow += GrdReanalysisHistory_ToolbarDeletingRow;
            grdReanalysisHistory.View.AddingNewRow += View_AddingNewRow;
            grdReanalysisHistory.View.CellValueChanged += View_CellValueChanged;
            grdReanalysisHistory.View.RowCellStyle += ReanalysisHistoryView_RowCellStyle;
            grdReanalysisHistory.View.ShowingEditor += View_ShowingEditor1;
            grdReanalysisHistory.View.KeyDown += View_KeyDown;

            //btnDeadline.Click += BtnDeadline_Click;
        }

        /// <summary>
        /// 적정량 입력후 엔터누를때 다음 Row의 적정량 입력할 수 있는 Cell로 Focus이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdReanalysisHistory.View.FocusedColumn.FieldName.Equals("TITRATIONQTY"))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int rowHandle = grdReanalysisHistory.View.FocusedRowHandle;
                    grdReanalysisHistory.View.FocusedRowHandle = rowHandle + 1;
                }
            }
        }

        /// <summary>
        /// 마감이 된 재분석이력이라면 전체 ReadOnly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor1(object sender, CancelEventArgs e)
        {
            // 부모의 마감여부가 Y라면 자식 그리드 전체 ReadOnly
            if (grdGenerationHistory.View.GetFocusedRowCellValue("ISCLOSE").Equals("Y"))
            {
                e.Cancel = true;
            }
            
            // 분석항목별로 Spec Out이라면 해당 Row ReadOnly
            if (grdReanalysisHistory.View.GetFocusedRowCellValue("ISSPECOUT").Equals("Y")
                && !string.IsNullOrWhiteSpace(Format.GetString(grdReanalysisHistory.View.GetFocusedRowCellValue("CREATOR")))
                && grdReanalysisHistory.View.FocusedColumn.FieldName.Equals("TITRATIONQTY"))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 발생이력 그리드에서 재분석필요가 Y인 검사항목들 설비단 기준으로 보충량 Check 일괄적용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ISCHECKQTY")
            {
                DataTable dt = grdGenerationHistory.DataSource as DataTable;

                string processsegmentClassId = grdGenerationHistory.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID").ToString(); // 대공정 ID
                string equipmentId = grdGenerationHistory.View.GetFocusedRowCellValue("EQUIPMENTID").ToString(); // 설비 ID
                string childEquipmentId = grdGenerationHistory.View.GetFocusedRowCellValue("CHILDEQUIPMENTID").ToString(); // 설비단 ID

                foreach (DataRow row in dt.AsEnumerable().Where(r => r["PROCESSSEGMENTCLASSID"].Equals(processsegmentClassId)
                                                                  && r["EQUIPMENTID"].Equals(equipmentId)
                                                                  && r["CHILDEQUIPMENTID"].Equals(childEquipmentId)
                                                                  && r["ISRESUPPLEMENT"].Equals("Y")))
                {
                    row["ISCHECKQTY"] = e.Value;
                }
            }
        }

        /// <summary>
        /// 마감버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeadline_Click(object sender, EventArgs e)
        {
            DataTable generation = grdGenerationHistory.GetChangedRows(); // 발생이력
            generation.TableName = "generation";
            DataTable reanalysis = grdReanalysisHistory.GetChangedRows(); // 재분석이력
            reanalysis.TableName = "reanalysis";
            DataTable parent = generation.Clone(); // 재분석이력 부모 Row
            parent.TableName = "parent";
            parent.ImportRow(grdGenerationHistory.View.GetFocusedDataRow());

            if (generation.Rows.Count == 0 && reanalysis.Rows.Count == 0)
            {
                this.ShowMessage("NoSaveData");
            }
            else
            {
                // 작업장 권한 체크 (Row 단위)
                if (generation.Rows.Count == 0) CheckAuthorityArea(grdGenerationHistory.View.GetFocusedDataRow());
                // 작업장 권한 체크 (Table 단위)
                else CheckAuthorityArea(generation);

                if (reanalysis.Rows.Count != 0)
                {
                    foreach (DataRow row in reanalysis.Rows)
                    {
                        if (string.IsNullOrEmpty(row["TITRATIONQTY"].ToString()))
                        {
                            throw MessageException.Create("VALIDATEREQUIREDVALUES");
                        }
                    }
                }

                if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    _chemicalSpecDt.TableName = "specData";
                    _chemicalSpecDt = _chemicalSpecDt.DefaultView.ToTable(true);

                    MessageWorker worker = new MessageWorker("SaveChemicalReanalysisRequest");
                    worker.SetBody(new MessageBody()
                    {
                        { "generation", generation },
                        { "reanalysis", reanalysis },
                        { "parent", parent },
                        { "specData", _chemicalSpecDt },
                    });

                    DataTable result = worker.Execute<DataTable>().GetResultSet();

                    this.ShowMessage("SuccessSave");
                    this.OnSearchAsync();

                    grdReanalysisHistory.BeginInvoke(new MethodInvoker(() =>
                    {
                        this.SearchReanalysisHistory();
                    }));

                    if (result.Rows.Count != 0)
                    {
                        DataTable toSendDt = CommonFunction.CreateChemicalReanlaysisAbnormalEmailDt();

                        foreach (DataRow standardRow in result.Rows)
                        {
                            DataRow newRow = toSendDt.NewRow();

                            newRow["ANALYSISTYPE"] = Format.GetString(standardRow["PMTYPE"]); // 분석종류
                            newRow["LARGEPROCESSSEGMENTNAME"] = Format.GetString(standardRow["PROCESSSEGMENTCLASSNAME"]); // 대공정명
                            //2020-01-13 강유라 AREAID, AREANAME 추가
                            newRow["AREAID"] = Format.GetString(standardRow["AREAID"]); // 작업장ID
                            newRow["AREANAME"] = Format.GetString(standardRow["AREANAME"]); // 작업장명
                            newRow["EQUIPMENTNAME"] = Format.GetString(standardRow["EQUIPMENTNAME"]); // 설비명
                            newRow["CHILDEQUIPMENTNAME"] = Format.GetString(standardRow["CHILDEQUIPMENTNAME"]); // 설비단명
                            newRow["CHEMICALNAME"] = Format.GetString(standardRow["CHEMICALNAME"]); // 약품명
                            newRow["CHEMICALLEVEL"] = Format.GetString(standardRow["CHEMICALLEVEL"]); // 약품등급
                            newRow["DEGREE"] = Format.GetString(standardRow["DEGREE"]); // 차수
                            newRow["ANALYSISVALUE"] = Format.GetString(reanalysis.Rows[0]["ANALYSISVALUE"]); // 분석량
                            newRow["MANAGEMENTSCOPE"] = Format.GetString(standardRow["MANAGEMENTSCOPE"]); // 관리범위
                            newRow["SPECSCOPE"] = Format.GetString(standardRow["SPECSCOPE"]); // 규격범위
                            newRow["ISRESUPPLEMENT"] = Format.GetString(standardRow["ISRESUPPLEMENT"]); // 재분석여부
                            newRow["MESSAGE"] = Format.GetString(standardRow["MESSAGE"]); // 전달사항
                            newRow["REMARK"] = ""; // 비고
                            newRow["USERID"] = UserInfo.Current.Id; // 보내는사람
                            newRow["TITLE"] = Language.Get("CHEMICALREABNORMALTITLE"); // 메일타이틀
                            newRow["INSPECTION"] = "ChemicalInspectionReanalysis"; // 검사종류
                            newRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType; // 언어타입

                            toSendDt.Rows.Add(newRow);
                        }

                        CommonFunction.ShowSendEmailPopupDataTable(toSendDt);
                    }

                    // 이메일 내용 구성
                    //if (result.Rows.Count != 0)
                    //{
                    //    string allContents = ""; // 이메일 내용

                    //    foreach (DataRow standardRow in result.Rows)
                    //    {
                    //        allContents += "○ " + Language.Get("ANALYSISTYPE") + " : " + Format.GetString(standardRow["PMTYPE"]) + "\r\n" // 분석종류
                    //                     + "○ " + Language.Get("LARGEPROCESSSEGMENTNAME") + " : " + Format.GetString(standardRow["PROCESSSEGMENTCLASSNAME"]) + "\r\n" // 대공정명
                    //                     + "○ " + Language.Get("EQUIPMENTNAME") + " : " + Format.GetString(standardRow["EQUIPMENTNAME"]) + "\r\n" // 설비명
                    //                     + "○ " + Language.Get("CHILDEQUIPMENTNAME") + " : " + Format.GetString(standardRow["CHILDEQUIPMENTNAME"]) + "\r\n" // 설비단명
                    //                     + "○ " + Language.Get("CHEMICALNAME") + " : " + standardRow["CHEMICALNAME"] + "\r\n" // 약품명
                    //                     + "○ " + Language.Get("CHEMICALLEVEL") + " : " + standardRow["CHEMICALLEVEL"] + "\r\n" // 약품등급
                    //                     + "○ " + Language.Get("DEGREE") + " : " + Format.GetString(standardRow["DEGREE"]) + "\r\n" // 차수
                    //                     + "○ " + Language.Get("ANALYSISVALUE") + " : " + standardRow["ANALYSISVALUE"] + "\r\n" // 분석량
                    //                     + "○ " + Language.Get("MANAGEMENTSCOPE") + " : " + standardRow["MANAGEMENTSCOPE"] + "\r\n" // 관리범위 
                    //                     + "○ " + Language.Get("SPECSCOPE") + " : " + standardRow["SPECSCOPE"] + "\r\n" // 규격범위
                    //                     + "○ " + Language.Get("ISRESUPPLEMENT") + " : " + Format.GetString(standardRow["ISRESUPPLEMENT"]) + "\r\n" // 재분석여부
                    //                     + "○ " + Language.Get("MESSAGE") + " : " + Format.GetString(standardRow["MESSAGE"]) + "\r\n"; // 전달사항
                    //    }

                    //    CommonFunction.ShowSendEmailPopup(Language.Get("CHEMICALABNORMALTITLE"), allContents);
                    //}
                }
                else
                {
                    this.ShowMessage("CancelSave");
                }
            }
        }

        /// <summary>
        /// 재분석필요가 Y라면 실보충량과 마감여부 ReadOnly, 재분석필요가 N이고 마감여부가 Y인데 RowState가 Modifiy가 아니라면 ReadOnly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            // 재분석필요가 Y라면 실보충량과 마감여부 ReadOnly
            if (grdGenerationHistory.View.FocusedColumn.FieldName == "ACTUALCOMPLEMENTQTY"
                || grdGenerationHistory.View.FocusedColumn.FieldName == "ISCLOSE")
            {
                if (grdGenerationHistory.View.GetFocusedRowCellValue("ISRESUPPLEMENT").Equals("Y"))
                {
                    e.Cancel = true;
                }
            }

            // 재분석필요가 N이고 마감여부가 Y인데 RowState가 Modifiy가 아니라면 ReadOnly
            if (grdGenerationHistory.View.GetFocusedRowCellValue("ISRESUPPLEMENT").Equals("N")
                && grdGenerationHistory.View.GetFocusedRowCellValue("ISCLOSE").Equals("Y"))
            {
                if (grdGenerationHistory.View.GetFocusedDataRow().RowState != DataRowState.Modified)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 마감된 이력이라면 행삭제 불가능
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdReanalysisHistory_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            if (grdReanalysisHistory.View.GetCheckedRows().Rows.Count == 0)
            {
                // 체크된 행이 없습니다.
                this.ShowMessage("GridNoChecked");
            }
            else if (grdGenerationHistory.View.GetFocusedRowCellValue("ISCLOSE").Equals("Y"))
            {
                // 마감된 이력에 대해서 삭제할 수 없습니다.
                e.Cancel = true;
                this.ShowMessage("DeadlineHistoryIsNotdelete");
            }
        }

        /// <summary>
        /// 새 행추가시 기본값 자동입력, 부모그리드의 재분석필요가 N이라면 행추가 취소
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            // 재분석필요가 N이라면 재분석이력 추가불가능
            if (grdGenerationHistory.View.GetFocusedRowCellValue("ISRESUPPLEMENT").Equals("N"))
            {
                args.IsCancel = true;
                this.ShowMessage("IsReanalysisN"); // 재분석여부가 N입니다.
            }
            // 마감된 이력이라면 재분석이력 추가불가능
            else if (_closeFlag.Equals("Y"))
            {
                args.IsCancel = true;
                this.ShowMessage("DeadlineHistory");  // 마감된 이력입니다.
            }
            else
            {
                args.NewRow["DEGREE"] = grdGenerationHistory.View.GetFocusedDataRow()["DEGREE"];


                // 순번 자동증가
                args.NewRow["DEGREEPOINT"] = grdReanalysisHistory.View.RowCount;

                if (_lastDegree + 2 == Convert.ToInt32(args.NewRow["DEGREEPOINT"]))
                {
                    args.IsCancel = true;
                    this.ShowMessage("ReanalysisOnebyone"); // 재분석은 한차수씩 등록 가능합니다.
                }
                else
                {
                    DataRow row = grdGenerationHistory.View.GetDataRow(grdGenerationHistory.View.FocusedRowHandle);

                    string date = DateTime.Now.ToString("yyyy-MM-dd"); // 분석일자
                    string hour = DateTime.Now.ToString("HH:mm"); // 채취시간대

                    args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
                    args.NewRow["PLANTID"] = row["PLANTID"];
                    args.NewRow["ANALYSISDATETIME"] = date + " " + hour;
                    args.NewRow["ANALYSISDATE"] = date;
                    args.NewRow["COLLECTIONTIME"] = hour;
                    args.NewRow["PROCESSSEGMENTCLASSID"] = row["PROCESSSEGMENTCLASSID"];
                    args.NewRow["EQUIPMENTID"] = row["EQUIPMENTID"];
                    args.NewRow["CHILDEQUIPMENTID"] = row["CHILDEQUIPMENTID"];
                    args.NewRow["INSPITEMID"] = row["INSPITEMID"];
                    args.NewRow["PARENTSEQUENCE"] = row["SEQUENCE"];
                    args.NewRow["ANALYSISTYPE"] = row["ANALYSISTYPE"];
                    args.NewRow["REANALYSISTYPE"] = "ReAnalysis";
                    args.NewRow["ANALYSISDATEHISTORY"] = row["ANALYSISDATE"];
                    args.NewRow["DEGREEHISTORY"] = row["DEGREE"];
                    args.NewRow["PMTYPE"] = row["PMTYPE"];
                    args.NewRow["TANKSIZE"] = row["TANKSIZE"];
                    args.NewRow["ANALYSISCONST"] = row["ANALYSISCONST"];
                    args.NewRow["QTYCONST"] = row["QTYCONST"];
                    args.NewRow["SL"] = row["SL"];
                    args.NewRow["CALCULATIONTYPE"] = row["CALCULATIONTYPE"];
                    args.NewRow["FOMULATYPE"] = row["FOMULATYPE"];
                    args.NewRow["SPECSEQUENCE"] = row["SPECSEQUENCE"];
                }
            }     
        }

        /// <summary>
        /// 적정량 Spec Check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "TITRATIONQTY")
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                DataRow keyRow = grdReanalysisHistory.View.GetFocusedDataRow();
                param.Add("SPECSEQUENCE", keyRow["SPECSEQUENCE"]);
                param.Add("SPECCLASSID", "ChemicalSpec");
                //param.Add("PROCESSSEGMENTCLASSID", keyRow["PROCESSSEGMENTCLASSID"]);
                //param.Add("EQUIPMENTID", keyRow["EQUIPMENTID"]);
                //param.Add("CHILDEQUIPMENTID", keyRow["CHILDEQUIPMENTID"]);
                //param.Add("INSPITEMID", keyRow["INSPITEMID"]);

                if (_chemicalSpecDt.Rows.Count == 0)
                {
                    _chemicalSpecDt = SqlExecuter.Query("GetInspitemSpecData", "10001", param);
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = SqlExecuter.Query("GetInspitemSpecData", "10001", param);

                    _chemicalSpecDt.Merge(dt, true, MissingSchemaAction.Ignore);

                    foreach (DataRow rw in _chemicalSpecDt.Rows)
                    {
                        rw["REANALYSISTYPE"] = "Analysis";
                        rw["ANALYSISTYPE"] = Convert.ToString(this.Conditions.GetValue("p_chemicalWaterType"));
                        rw["ANALYSISDATE"] = string.Format("{0:yyyy-MM-dd}", grdReanalysisHistory.View.GetFocusedRowCellValue("ANALYSISDATE"));
                        rw["DEGREE"] = Convert.ToString(this.Conditions.GetValue("p_round"));
                        rw["PMTYPE"] = Convert.ToString(this.Conditions.GetValue("p_division"));
                    }

                    _chemicalSpecDt = _chemicalSpecDt.DefaultView.ToTable(true);
                }

                // 분석치가 계산된 후 다시 CellValueChange를 타기위한 이벤트
                View_CellValueChanged2(grdReanalysisHistory.View, new DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs
                                    (e.RowHandle
                                    , grdReanalysisHistory.View.Columns["ANALYSISVALUE"]
                                    , grdReanalysisHistory.View.GetRowCellValue(e.RowHandle, "ANALYSISVALUE")));
            }
        }

        /// <summary>
        /// 적정량에 의해 계산된 분석치를 가지고 SpeckCheck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged2(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow paramrow = this.grdReanalysisHistory.View.GetFocusedDataRow();

            string _sRULEID = "'" + paramrow["CALCULATIONTYPE"] + "','" + paramrow["FOMULATYPE"] + "'";
            string[] _areaRULEID = _sRULEID.Split(new char[] { ',' });

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("RULEID", _sRULEID);

            DataTable dtResult = SqlExecuter.Query("GetCalculationRule", "10001", dicParam);

            if (paramrow["COLVALUE"].ToString() != "Y")
            {
                // 수정된 행이 존재 할경우 로직 실행
                DataRow[] rowChk = dtResult.Select("CODEID = '" + grdReanalysisHistory.View.FocusedColumn.FieldName + "'");

                if (rowChk.Length != 0)
                {
                    int num = 0;

                    foreach (string sRULEID in _areaRULEID)
                    {
                        foreach (DataRow row in dtResult.DefaultView.ToTable(true, new string[] { "RULEID", "TARGETATTRIBUTE" }).Select("RULEID = '" + sRULEID.Replace("'", "") + "'"))
                        {
                            String sAdd = "";

                            foreach (DataRow rowAdd in dtResult.Select("TARGETATTRIBUTE = '" + row["TARGETATTRIBUTE"].ToString() + "'", "SEQUENCE"))
                            {
                                String scodeid = "";
                                if (rowAdd["CODEID"].ToString() != "")
                                {
                                    if (paramrow.Table.Columns.IndexOf(rowAdd["CODEID"].ToString()) != -1)
                                    {
                                        if (string.IsNullOrEmpty(paramrow[rowAdd["CODEID"].ToString()].ToString()))
                                        {
                                            scodeid = "0";
                                        }
                                        else
                                        {
                                            scodeid = paramrow[rowAdd["CODEID"].ToString()].ToString();
                                        }
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(rowAdd["CODEID"].ToString()))
                                        {
                                            scodeid = "0";
                                        }
                                        else
                                        {
                                            scodeid = rowAdd["CODEID"].ToString();
                                        }
                                    }
                                }

                                sAdd = sAdd + rowAdd["FRONTBRACKET"].ToString() + scodeid + rowAdd["OPERATOR"].ToString() + rowAdd["BACKBRACKET"].ToString();
                                sAdd = sAdd.Replace(",", "");
                            }

                            foreach (DataColumn col in paramrow.Table.Columns)
                            {
                                sAdd = sAdd.Replace(col.ColumnName, paramrow[col.ColumnName].ToString());

                                if (col.ColumnName == row["TARGETATTRIBUTE"].ToString())
                                {
                                    if (col.ColumnName == "SUPPLEMENTQTY")
                                    {
                                        if (sAdd.Contains("/0"))
                                        {
                                            return;
                                        }
                                    }

                                    DataTable dtCalulation = new DataTable();

                                    // 보충량이 음수라면 0으로 바꿔준다.
                                    string value = dtCalulation.Compute(sAdd, "").ToString();
                                    if (col.ColumnName.Equals("SUPPLEMENTQTY"))
                                    {
                                        if (value.Contains("-"))
                                        {
                                            paramrow[col.ColumnName] = 0;
                                        }
                                        else
                                        {
                                            paramrow[col.ColumnName] = value;
                                        }
                                    }
                                    else
                                    {
                                        paramrow[col.ColumnName] = value;
                                    }
                                }
                            }
                        }

                        if (num == 0)
                        {
                            SpecCheck(_chemicalSpecDt);
                            num++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 발생이력 그리드의 Row Click시 재분석 이력 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick(object sender, RowClickEventArgs e)
        {
            pnlContent.ShowWaitArea(); // 대기
            SearchReanalysisHistory();
            pnlContent.CloseWaitArea(); // 닫기
        }

        /// <summary>
        /// 발생이력 그리드의 Row Focus Changed시 재분석 이력 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            pnlContent.ShowWaitArea(); // 대기
            SearchReanalysisHistory();       
            pnlContent.CloseWaitArea(); // 닫기
        }

        /// <summary>
        /// Spec Out Data 색깔 변경(분석이력), Focus받는 행의 색깔 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerationView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.ForeColor = Color.Black;
            }

            if (e.Column.FieldName == "PROCESSSEGMENTCLASSNAME" || e.Column.FieldName == "EQUIPMENTNAME"
                || e.Column.FieldName == "STATENAME" //|| e.Column.FieldName == "CHILDEQUIPMENTNAME"
                || e.Column.FieldName == "DEGREE" || e.Column.FieldName == "ANALYSISDATE" 
                || e.Column.FieldName == "FACTORYNAME") 
            {
                e.Appearance.BackColor = Color.White;
            }
            // 규격범위 벗어났을때 색깔
            if (grdGenerationHistory.View.GetRowCellValue(e.RowHandle, "ISSPECOUT").Equals("Y"))
            {
                if (e.Column.FieldName == "TITRATIONQTY" || e.Column.FieldName == "ANALYSISVALUE"
                    || e.Column.FieldName == "SUPPLEMENTQTY" || e.Column.FieldName == "CHEMICALNAME"
                    || e.Column.FieldName == "CHEMICALLEVEL" || e.Column.FieldName == "MANAGEMENTSCOPE"
                    || e.Column.FieldName == "SPECSCOPE" || e.Column.FieldName == "CHILDEQUIPMENTNAME")
                {
                    e.Appearance.BackColor = Color.PaleVioletRed;
                }
            }
        }

        /// <summary>
        /// Spec Out Data 색깔 변경(재분석이력), Focus받는 행의 색깔 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReanalysisHistoryView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.ForeColor = Color.Black;
            }

            if (e.Column.FieldName == "PROCESSSEGMENTCLASSNAME" || e.Column.FieldName == "EQUIPMENTNAME"
                || e.Column.FieldName == "STATENAME" || e.Column.FieldName == "CHILDEQUIPMENTNAME"
                || e.Column.FieldName == "DEGREE" || e.Column.FieldName == "ANALYSISDATE")
            {
                e.Appearance.BackColor = Color.White;
            }

            // 규격범위 벗어났을때 색깔
            if (grdReanalysisHistory.View.GetRowCellValue(e.RowHandle, "ISSPECOUT").Equals("Y")
                && !string.IsNullOrWhiteSpace(grdReanalysisHistory.View.GetRowCellValue(e.RowHandle, "TITRATIONQTY").ToString()))
            {
                if (e.Column.FieldName == "TITRATIONQTY" || e.Column.FieldName == "ANALYSISVALUE"
                    || e.Column.FieldName == "SUPPLEMENTQTY")
                {
                    e.Appearance.BackColor = Color.PaleVioletRed;
                }
            }
            // 관리범위 벗어났을때 색깔
            else if (grdReanalysisHistory.View.GetRowCellValue(e.RowHandle, "ISCONTROLLIMITOUT").Equals("Y")
                     && !string.IsNullOrWhiteSpace(grdReanalysisHistory.View.GetRowCellValue(e.RowHandle, "TITRATIONQTY").ToString()))
            {
                if (e.Column.FieldName == "TITRATIONQTY" || e.Column.FieldName == "ANALYSISVALUE"
                    || e.Column.FieldName == "SUPPLEMENTQTY")
                {
                    e.Appearance.BackColor = Color.DarkOrange;
                }
            }
        }

        /// <summary>
        /// 사용자 지정 Cell Merge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null) return;

            if (e.Column.FieldName == "PROCESSSEGMENTCLASSNAME" || e.Column.FieldName == "EQUIPMENTNAME"
                || e.Column.FieldName == "STATENAME" //|| e.Column.FieldName == "CHILDEQUIPMENTNAME"
                || e.Column.FieldName == "ANALYSISDATE" || e.Column.FieldName == "DEGREE"
                || e.Column.FieldName == "FACTORYNAME")
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

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = grdReanalysisHistory.GetChangedRows();

            ExecuteRule("RuleName", changed);
        }

        /// <summary>
        /// 툴바버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Deadline"))
            {
                BtnDeadline_Click(null, null);
            }
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            DateTime analysisDateFr = Convert.ToDateTime(values["P_PERIOD_PERIODFR"]);
            values["P_PERIOD_PERIODFR"] = string.Format("{0:yyyy-MM-dd}", analysisDateFr);
            DateTime analysisDateTo = Convert.ToDateTime(values["P_PERIOD_PERIODTO"]);
            values["P_PERIOD_PERIODTO"] = string.Format("{0:yyyy-MM-dd}", analysisDateTo);
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);             

            DataTable dt = await SqlExecuter.QueryAsync("GetChemicalGenerationHistory", "10001", values);

            if (dt.Rows.Count < 1) 
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
                grdReanalysisHistory.View.SetEmptyDataTable();
            }

            grdGenerationHistory.DataSource = dt;
            grdReanalysisHistory.BeginInvoke(new MethodInvoker(() =>
            {
                this.SearchReanalysisHistory();
            }));
        }

        /// <summary>
        /// 검색조건 초기화. 
        /// 조회조건 정보, 메뉴 - 조회조건 매핑 화면에 등록된 정보를 기준으로 구성됩니다.
        /// DB에 등록한 정보를 제외한 추가 조회조건 구성이 필요한 경우 사용합니다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //this.Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
            //    .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
            //    .SetDefault(UserInfo.Current.Plant)
            //    .SetValidationIsRequired()
            //    .SetLabel("PLANT")
            //    .SetPosition(1.1); // Site (기본값 Login 유저의 Site)

            this.Conditions.AddComboBox("p_chemicalWaterType", new SqlQuery("GetChemicalWaterType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault("ChemicalInspection")
                .SetRelationIds("p_plantId")
                .SetValidationIsRequired()
                .SetLabel("CHEMICALWATERTYPE")
                .SetPosition(1.2); // 약품수질구분 (기본값 Login 유저의 Site에 해당하는 InspectionClassId)

            this.Conditions.AddComboBox("p_round", new SqlQuery("GetRoundListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CYCLESEQUENCETIME", "CYCLESEQUENCE")
                .SetRelationIds("p_plantId", "p_chemicalWaterType", "p_division")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetValidationIsRequired()
                .SetEmptyItem()
                .SetLabel("ROUND")
                .SetPosition(2.1); // 회차

            this.Conditions.AddComboBox("p_processsegmentclassId", new SqlQuery("GetLargeProcesssegmentListByQcm", "10002", "CODECLASSID=ChemicalAnalyRound", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                //.SetRelationIds("p_plantId")
                .SetRelationIds("p_chemicalWaterType")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetEmptyItem()
                .SetLabel("LARGEPROCESSSEGMENT")
                .SetPosition(2.2); // 대공정

            InitializeConditionPopup_Equipment();
            InitializeConditionPopup_EquipmentStage();
            InitializeConditionPopup_Chemical();
        }

        /// <summary>
        /// 회차에 등록된 시간이 현재시간보다 크다면 전회차의 시간으로 조회조건 기본값 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // 조회조건에 구성된 Control에 대한 처리가 필요한 경우
            SmartComboBox roundCombo = Conditions.GetControl<SmartComboBox>("p_round");

            DataTable dt = roundCombo.DataSource as DataTable;
            string[] splitStr;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["CYCLESEQUENCE"].Equals("*")) continue;

                splitStr = dt.Rows[i]["CYCLESEQUENCETIME"].ToString().Split('('); // 앞의 '('를 먼저 자른다.
                DateTime time = Convert.ToDateTime(splitStr[1].ToString().Replace(") ~", "")); // 뒤의 ')'를 자른뒤 DateTime으로 변환한다.

                // 회차에 등록된 시간이 현재시간보다 크다면 전회차의 시간으로 조회조건 기본값 설정
                if (time > DateTime.Now)
                {
                    if (i == 1)
                    {
                        roundCombo.EditValue = dt.Rows[i]["CYCLESEQUENCE"];
                        break;
                    }

                    roundCombo.EditValue = dt.Rows[i - 1]["CYCLESEQUENCE"];
                    break;
                }
            }
        }

        /// <summary>
        /// 설비 조회조건
        /// </summary>
        private void InitializeConditionPopup_Equipment()
        {
            // 팝업 컬럼설정
            var equipmentPopupColumn = Conditions.AddSelectPopup("p_equipmentId", new SqlQuery("GetEquipmentListByChemicalAnalysis", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
               .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("EQUIPMENT")
               .SetPopupResultCount(1)
               .SetPosition(3.1)
               .SetRelationIds("p_plantId", "p_processsegmentclassId");

            // 팝업 조회조건
            equipmentPopupColumn.Conditions.AddTextBox("EQUIPMENTIDNAME")
                .SetLabel("EQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);
        }

        /// <summary>
        /// 설비단 조회조건
        /// </summary>
        private void InitializeConditionPopup_EquipmentStage()
        {
            // 팝업 컬럼설정
            var equipmentStagePopupColumn = Conditions.AddSelectPopup("p_childEquipmentId", new SqlQuery("GetChildEquipmentListByChemicalAnalysis", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CHILDEQUIPMENTNAME", "CHILDEQUIPMENTID")
               .SetPopupLayout("CHILDEQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("CHILDEQUIPMENT")
               .SetPopupResultCount(1)
               .SetPosition(3.2)
               .SetRelationIds("p_plantId", "p_equipmentId");

            // 팝업 조회조건
            equipmentStagePopupColumn.Conditions.AddTextBox("CHILDEQUIPMENTIDNAME")
                .SetLabel("CHILDEQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentStagePopupColumn.GridColumns.AddTextBoxColumn("CHILDEQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentStagePopupColumn.GridColumns.AddTextBoxColumn("CHILDEQUIPMENTNAME", 200);
        }

        /// <summary>
        /// 약품 조회조건
        /// </summary>
        private void InitializeConditionPopup_Chemical()
        {
            // 팝업 컬럼설정
            var chemicalPopupColumn = Conditions.AddSelectPopup("p_chemicalId", new SqlQuery("GetChemicalListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMNAME", "INSPITEMID")
               .SetPopupLayout("CHEMICAL", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("CHEMICAL")
               .SetPopupResultCount(1)
               .SetPosition(3.3)
               .SetRelationIds("p_processsegmentclassId", "p_equipmentId", "p_childEquipmentId");

            // 팝업 조회조건
            chemicalPopupColumn.Conditions.AddTextBox("INSPITEMIDNAME")
                .SetLabel("INSPITEMIDNAME");

            // 팝업 그리드
            chemicalPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMID", 150)
                .SetValidationKeyColumn();
            chemicalPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdReanalysisHistory.View.CheckValidation();

            DataTable changed = grdReanalysisHistory.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 발생이력의 각각의 항목에 대한 재분석이력 검색함수
        /// </summary>
        private void SearchReanalysisHistory()
        {
            var row = grdGenerationHistory.View.GetDataRow(grdGenerationHistory.View.FocusedRowHandle);

            if (row == null)
            {
                return;
            }
            if (row["SEQUENCE"] == System.DBNull.Value)
            {
                grdReanalysisHistory.View.SetEmptyDataSource();
                return;
            }
            
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_languageType", UserInfo.Current.LanguageType);
            param.Add("p_processsegmentclassId", row["PROCESSSEGMENTCLASSID"].ToString());
            param.Add("p_equipmentId", row["EQUIPMENTID"].ToString());
            param.Add("p_childequipmentId", row["CHILDEQUIPMENTID"].ToString());
            param.Add("p_inspitemId", row["INSPITEMID"].ToString());
            param.Add("p_degree", row["DEGREE"].ToString());
            param.Add("p_analysisDate", row["ANALYSISDATE"].ToString());
            param.Add("p_analysisType", row["ANALYSISTYPE"].ToString());
            param.Add("p_sequence", Convert.ToInt32(row["SEQUENCE"]));
            param.Add("p_inspectionDefId", row["INSPECTIONDEFID"]);

            DataTable historyDt = SqlExecuter.Query("GetChemicalReanalysisHistory", "10001", param);
            grdReanalysisHistory.DataSource = historyDt;

            if (historyDt.Rows.Count != 0)
            {       
                //    // 분석값 계산 Rule ID
                //    Dictionary<string, object> ruleParam = new Dictionary<string, object>();
                //    ruleParam.Add("ENTERPRISEID", historyDt.Rows[0]["ENTERPRISEID"]);
                //    ruleParam.Add("PLANTID", historyDt.Rows[0]["PLANTID"]);
                //    ruleParam.Add("TARGETATTRIBUTE", "ANALYSISVALUE");

                //    DataTable ruleDt1 = SqlExecuter.Query("GetRuleIdByPlant", "10001", ruleParam);

                //    // 보충량 계산 Rule ID
                //    ruleParam.Remove("TARGETATTRIBUTE");
                //    ruleParam.Add("TARGETATTRIBUTE", "SUPPLEMENTQTY");
                //    DataTable ruleDt2 = SqlExecuter.Query("GetRuleIdByPlant", "10001", ruleParam);

                //    // 자동계산(분석치, 보충량)
                //    SetCalculationRule calc = new SetCalculationRule();
                //    calc.SetCalculationRule_grid(new string[] { Format.GetString(ruleDt1.Rows[0]["RULEID"]), Format.GetString(ruleDt2.Rows[0]["RULEID"]) }, grdReanalysisHistory, "COLVALUE");
            }

            if (grdReanalysisHistory.View.RowCount < 1)
            {
                _closeFlag = "N";
                _lastDegree = 0;
            }
            else
            {
                // 마감여부(재분석이력의 마지막 행의 마감여부)
                _closeFlag = grdReanalysisHistory.View.GetRowCellValue(grdReanalysisHistory.View.RowCount - 1, "ISCLOSE").ToString();
                // 마지막차수(재분석이력은 1차수씩 등록가능)
                _lastDegree = Convert.ToInt32(grdReanalysisHistory.View.GetRowCellValue(grdReanalysisHistory.View.RowCount - 1, "DEGREEPOINT"));
            }
        }

        /// <summary>
        /// Spec Check 모듈
        /// </summary>
        private void SpecCheck(DataTable specDt)
        {
            DataRow focusRow = grdReanalysisHistory.View.GetFocusedDataRow();

            // 기본 차트 타입이 없다면 Spec Check 안함
            if (specDt.Rows.Cast<DataRow>()
                           .Where(r => r["EQUIPMENTID"].Equals(focusRow["EQUIPMENTID"])
                                  && r["CHILDEQUIPMENTID"].Equals(focusRow["CHILDEQUIPMENTID"])
                                  && r["INSPITEMID"].Equals(focusRow["INSPITEMID"].ToString())
                                  && r["CONTROLTYPE"].Equals(r["DEFAULTCHARTTYPE"]))
                           .Count() == 0)
            {
                return;
            }
            // 적정량이 지워졌다면 Spec Check 안함
            else if (string.IsNullOrWhiteSpace(focusRow["TITRATIONQTY"].ToString()))
            {
                focusRow["ANALYSISVALUE"] = DBNull.Value;
                focusRow["SUPPLEMENTQTY"] = DBNull.Value;
                return;
            }

            DataRow row = specDt.Rows.Cast<DataRow>()
                                     .Where(r => r["EQUIPMENTID"].Equals(focusRow["EQUIPMENTID"])
                                            && r["CHILDEQUIPMENTID"].Equals(focusRow["CHILDEQUIPMENTID"])
                                            && r["INSPITEMID"].Equals(focusRow["INSPITEMID"])
                                            && r["CONTROLTYPE"].Equals(r["DEFAULTCHARTTYPE"]))
                                     .First();

            //입력 Parameter 입력
            SpcLibraryHelper spcHelper = new SpcLibraryHelper();
            SpcRules spcRules = new SpcRules();
            spcRules.xbarr.uol = string.IsNullOrEmpty(row["UOL"].ToString()) ? 99999999999 : Convert.ToDouble(row["UOL"]);
            spcRules.xbarr.usl = string.IsNullOrEmpty(row["USL"].ToString()) ? 99999999999 : Convert.ToDouble(row["USL"]);
            spcRules.xbarr.ucl = string.IsNullOrEmpty(row["UCL"].ToString()) ? 99999999999 : Convert.ToDouble(row["UCL"]);
            spcRules.xbarr.lcl = string.IsNullOrEmpty(row["LCL"].ToString()) ? -99999999999 : Convert.ToDouble(row["LCL"]);
            spcRules.xbarr.lsl = string.IsNullOrEmpty(row["LSL"].ToString()) ? -99999999999 : Convert.ToDouble(row["LSL"]);
            spcRules.xbarr.lol = string.IsNullOrEmpty(row["LOL"].ToString()) ? -99999999999 : Convert.ToDouble(row["LOL"]);

            if (string.IsNullOrEmpty(grdReanalysisHistory.View.GetRowCellValue(grdReanalysisHistory.View.FocusedRowHandle, "ANALYSISVALUE").ToString()))
            {
                return;
            }

            spcRules.nValue = Convert.ToDouble(grdReanalysisHistory.View.GetRowCellValue(grdReanalysisHistory.View.FocusedRowHandle, "ANALYSISVALUE"));

            spcRules.defaultChartType = SpcChartType.xbarr; // 기본 차트타입은 xbarr기준으로 함

            //Spec Check
            SpcRulesOver returnRulesOver = new SpcRulesOver();
            returnRulesOver = spcHelper.SpcSpecRuleCheck(spcRules);

            // 규격범위(SL)가 벗어났을땐 스펙아웃 및 메일팝업호출
            if (returnRulesOver.isSpec == true)
            {
                grdReanalysisHistory.View.SetRowCellValue(grdReanalysisHistory.View.FocusedRowHandle, "ISSPECOUT", "Y"); // SpecOut여부 Y
                grdReanalysisHistory.View.SetRowCellValue(grdReanalysisHistory.View.FocusedRowHandle, "ISSUPPLEMENT", "Y"); // 액보충필요 Y
                grdReanalysisHistory.View.SetRowCellValue(grdReanalysisHistory.View.FocusedRowHandle, "ISRESUPPLEMENT", "Y"); // 재분석여부 Y

                // Spec Out입니다.
                this.ShowMessage(returnRulesOver.message.value);
            }
            // 규격범위(SL)가 벗어나지 않았을땐 정상스펙
            else
            {
                // 관리범위(CL)가 벗어났을땐 해당 Row 색깔만 변경
                if (returnRulesOver.isControlLimit == true)
                {
                    grdReanalysisHistory.View.SetRowCellValue(grdReanalysisHistory.View.FocusedRowHandle, "ISCONTROLLIMITOUT", "Y");
                }
                else
                {
                    grdReanalysisHistory.View.SetRowCellValue(grdReanalysisHistory.View.FocusedRowHandle, "ISCONTROLLIMITOUT", "N"); // 관리범위여부 N
                }

                grdReanalysisHistory.View.SetRowCellValue(grdReanalysisHistory.View.FocusedRowHandle, "ISSPECOUT", "N"); // SpecOut여부 N
                grdReanalysisHistory.View.SetRowCellValue(grdReanalysisHistory.View.FocusedRowHandle, "ISSUPPLEMENT", "N"); // 액보충필요 N
                grdReanalysisHistory.View.SetRowCellValue(grdReanalysisHistory.View.FocusedRowHandle, "ISRESUPPLEMENT", "N"); // 재분석여부 N

                if (spcRules.nValue.IsDouble())
                {
                    //this.ShowMessageBox("정상 - SPEC Check", "OO");
                }
                else
                {
                    this.ShowMessageBox("측정값이 없습니다.", "OO");
                }
            }
        }

        /// <summary>
        /// 로그인한 사용자가 해당 작업장에 대한 수정권한이 있는지 체크 (Table 단위)
        /// </summary>
        /// <param name="dt"></param>
        private void CheckAuthorityArea(DataTable dt)
        {
            List<object> noAuthorityArea = dt.AsEnumerable().Where(r => r["ISMODIFY"].Equals("N")).Select(r => r["AREANAME"]).Distinct().ToList();

            if (noAuthorityArea.Count > 0)
            {
                string areaList = "";

                for (int i = 0; i < noAuthorityArea.Count; i++)
                {
                    if (i == noAuthorityArea.Count - 1) areaList += noAuthorityArea[i];
                    else areaList += noAuthorityArea[i] + ", ";
                }

                throw MessageException.Create("NoMatchingAreaUser", areaList);
            }
        }

        /// <summary>
        /// 로그인한 사용자가 해당 작업장에 대한 수정권한이 있는지 체크 (Row 단위)
        /// </summary>
        /// <param name="dt"></param>
        private void CheckAuthorityArea(DataRow row)
        {
            if (row["ISMODIFY"].Equals("N"))
            {
                string area = Format.GetString(row["AREANAME"]);
                throw MessageException.Create("NoMatchingAreaUser", area);
            }
        }

        #endregion
    }
}
