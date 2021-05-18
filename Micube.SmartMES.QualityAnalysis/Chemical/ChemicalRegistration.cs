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
using DevExpress.XtraEditors.Repository;
using Micube.SmartMES.QualityAnalysis;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.SPCLibrary;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 약품관리 > 약품분석값 등록
    /// 업  무  설  명  : 약품스펙값을 분석하여 적정량, 분석치, 보충량등을 등록한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ChemicalRegistration : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 보충량저장, 확정의상태값
        /// </summary>
        private string _saveConfirmState;

        /// <summary>
        /// 약품별로 스펙을 담고있는 데이터테이블
        /// </summary>
        private DataTable _chemicalSpecDt = new DataTable();

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ChemicalRegistration()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdChemicalRegistration.GridButtonItem = GridButtonItem.Export;

            grdChemicalRegistration.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 100)
                .SetLabel("LARGEPROCESSSEGMENTNAME")
                .SetIsReadOnly();  // 대공정명
            grdChemicalRegistration.View.AddTextBoxColumn("FACTORYNAME", 80)
                .SetIsReadOnly(); // 공장명
            //grdChemicalRegistration.View.AddTextBoxColumn("EQUIPMENTNAME", 150)
            //    .SetIsReadOnly(); // 설비명
            grdChemicalRegistration.View.AddTextBoxColumn("CONFIRMSTATE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("STATE")
                .SetIsReadOnly(); // 상태
            grdChemicalRegistration.View.AddTextBoxColumn("DEGREE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 차수
            grdChemicalRegistration.View.AddTextBoxColumn("EQUIPMENTNAME", 150)
                .SetIsReadOnly(); // 설비명
            grdChemicalRegistration.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 120)
                .SetIsReadOnly(); // 설비단
            grdChemicalRegistration.View.AddTextBoxColumn("CHEMICALNAME", 120)
                .SetIsReadOnly(); // 약품명
            grdChemicalRegistration.View.AddTextBoxColumn("CHEMICALLEVEL", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 약품등급
            grdChemicalRegistration.View.AddTextBoxColumn("MANAGEMENTSCOPE", 140)
                .SetIsReadOnly(); // 관리범위
            grdChemicalRegistration.View.AddTextBoxColumn("SPECSCOPE", 140)
                .SetIsReadOnly(); // 규격범위

            grdChemicalRegistration.View.AddSpinEditColumn("TITRATIONQTY", 100)              
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 적정량 
            grdChemicalRegistration.View.AddSpinEditColumn("ANALYSISVALUE", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetIsReadOnly(); // 분석량
            grdChemicalRegistration.View.AddSpinEditColumn("SUPPLEMENTQTY", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetIsReadOnly(); // 보충량
            grdChemicalRegistration.View.AddTextBoxColumn("COLLECTIONTIME")
                .SetTextAlignment(TextAlignment.Center); // 채취시간대
            grdChemicalRegistration.View.AddComboBoxColumn("ISSUPPLEMENT", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("N"); // 액보충필요
            grdChemicalRegistration.View.AddComboBoxColumn("ISRESUPPLEMENT", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("N"); // 재분석필요
            grdChemicalRegistration.View.AddTextBoxColumn("MESSAGE", 150); // 전달사항 

            grdChemicalRegistration.View.AddTextBoxColumn("CREATOR", 100)             
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 생성자
            grdChemicalRegistration.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 생성시간
            grdChemicalRegistration.View.AddTextBoxColumn("MODIFIER", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 수정자
            grdChemicalRegistration.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 수정시간           

            grdChemicalRegistration.View.AddTextBoxColumn("ENTERPRISEID")
                .SetIsHidden(); // Enterprise ID
            grdChemicalRegistration.View.AddTextBoxColumn("PLANTID")
                .SetIsHidden(); // Site ID
            grdChemicalRegistration.View.AddTextBoxColumn("SEQUENCE")
                .SetIsHidden(); // 일련번호
            grdChemicalRegistration.View.AddTextBoxColumn("ANALYSISDATE")
                .SetIsHidden(); // 분석일자
            grdChemicalRegistration.View.AddTextBoxColumn("PMTYPE")
                .SetIsHidden(); // 구분
            grdChemicalRegistration.View.AddTextBoxColumn("PMTYPENAME")
                .SetIsHidden(); // 구분명
            grdChemicalRegistration.View.AddTextBoxColumn("ANALYSISTYPE")
                .SetIsHidden(); // 약품수질구분
            grdChemicalRegistration.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID")
                .SetIsHidden(); // 대공정ID
            grdChemicalRegistration.View.AddTextBoxColumn("EQUIPMENTID")
                .SetIsHidden(); // 설비ID
            grdChemicalRegistration.View.AddTextBoxColumn("CHILDEQUIPMENTID")
                .SetIsHidden(); // 설비단ID
            grdChemicalRegistration.View.AddTextBoxColumn("INSPITEMID")
                .SetIsHidden(); // 약품ID
            grdChemicalRegistration.View.AddTextBoxColumn("REANALYSISTYPE")
                .SetIsHidden(); // 재분석, 분석 구분자
            grdChemicalRegistration.View.AddTextBoxColumn("PARENTSEQUENCE")
                .SetIsHidden(); // 상위일련번호
            grdChemicalRegistration.View.AddTextBoxColumn("STATECODEID")
                .SetIsHidden(); // 상태의 코드ID
            grdChemicalRegistration.View.AddTextBoxColumn("ISCLOSE")
                .SetIsHidden(); // 마감여부
            grdChemicalRegistration.View.AddTextBoxColumn("ISSPECOUT")
                .SetDefault("N")
                .SetIsHidden(); // SpecOut 여부
            grdChemicalRegistration.View.AddTextBoxColumn("TANKSIZE")
                .SetIsHidden(); // 탱크사이즈
            grdChemicalRegistration.View.AddTextBoxColumn("ANALYSISCONST")
                .SetIsHidden(); // 분석상수
            grdChemicalRegistration.View.AddTextBoxColumn("QTYCONST")
                .SetIsHidden(); // 보충량상수
            grdChemicalRegistration.View.AddTextBoxColumn("SL")
                .SetIsHidden(); // Default 차트타입 스펙값의 SL값
            grdChemicalRegistration.View.AddTextBoxColumn("COLVALUE")
                .SetIsHidden(); // 탱크사이즈, 분석상수, 보충량상수중에 하나라도 값이 적용되어있지 않으면 Y반환 (보충량, 분석치를 계산하지 않음)
            grdChemicalRegistration.View.AddTextBoxColumn("REASONCODEID")
                .SetIsHidden(); // 이상발생테이블에 넣을 사유코드
            grdChemicalRegistration.View.AddTextBoxColumn("ISCONTROLLIMITOUT")
                .SetIsHidden(); // 관리범위 체크
            grdChemicalRegistration.View.AddTextBoxColumn("CALCULATIONTYPE")
                .SetIsHidden(); // 분석값 계산식 유형
            grdChemicalRegistration.View.AddTextBoxColumn("FOMULATYPE")
                .SetIsHidden(); // 보충량 계산식 유형
            grdChemicalRegistration.View.AddTextBoxColumn("ISMODIFY")
                .SetIsHidden(); // 작업장 통제 권한에 따른 수정가능여부
            grdChemicalRegistration.View.AddTextBoxColumn("AREAID")
                .SetIsHidden(); // 작업장 ID
            grdChemicalRegistration.View.AddTextBoxColumn("AREANAME")
                .SetIsHidden(); // 작업장명
            grdChemicalRegistration.View.AddTextBoxColumn("ISDELETE")
                .SetIsHidden(); // 삭제할 행인지 판단하는 컬럼(데이터 삭제 개선사항 나올경우 활용)
            grdChemicalRegistration.View.AddTextBoxColumn("SPECSEQUENCE")
                .SetIsHidden(); 

            grdChemicalRegistration.View.PopulateColumns();
            grdChemicalRegistration.View.Columns["TITRATIONQTY"].AppearanceCell.BackColor = Color.Moccasin; // 적정량 Column 색깔변경
            grdChemicalRegistration.View.OptionsView.AllowCellMerge = true; // CellMerge
            //grdChemicalRegistration.View.FixColumn(new string[] { "PROCESSSEGMENTCLASSNAME", "EQUIPMENTNAME", "CONFIRMSTATE", "DEGREE", "CHILDEQUIPMENTNAME", "CHEMICALNAME", "CHEMICALLEVEL", "MANAGEMENTSCOPE", "SPECSCOPE" });

            RepositoryItemTimeEdit edit = new RepositoryItemTimeEdit();

            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            edit.Mask.EditMask = "([0-1]?[0-9]|2[0-3]):[0-5][0-9]";
            edit.Mask.UseMaskAsDisplayFormat = true;

            grdChemicalRegistration.View.Columns["COLLECTIONTIME"].ColumnEdit = edit;

            grdChemicalRegistration.View.Columns["TITRATIONQTY"].AppearanceCell.BackColor = Color.Moccasin; // 적정량 입력란 색깔변경
            grdChemicalRegistration.View.Columns["TITRATIONQTY"].AppearanceHeader.ForeColor = Color.Red; // 적정량 Column 색깔변경

            grdChemicalRegistration.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdChemicalRegistration.View.CellMerge += View_CellMerge;
            grdChemicalRegistration.View.CellValueChanged += View_CellValueChanged;
            grdChemicalRegistration.View.RowCellStyle += View_RowCellStyle;
            grdChemicalRegistration.View.ShowingEditor += View_ShowingEditor;
            grdChemicalRegistration.View.KeyDown += View_KeyDown;
            grdChemicalRegistration.View.RowStyle += View_RowStyle;
            //btnSupplementRegistartion.Click += BtnSupplementRegistartion_Click;
            //btnSupplementConfirmation.Click += BtnSupplementConfirmation_Click;
        }

        /// <summary>
        /// 포커스 받은 Row 색깔표시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.HighPriority = true;
                e.Appearance.BackColor = Color.Yellow;
            }
        }

        /// <summary>
        /// 적정량 입력후 엔터누를때 다음 Row의 적정량 입력할 수 있는 Cell로 Focus이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdChemicalRegistration.View.FocusedColumn.FieldName.Equals("TITRATIONQTY"))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int rowHandle = grdChemicalRegistration.View.FocusedRowHandle;
                    grdChemicalRegistration.View.FocusedRowHandle = rowHandle + 1;
                }
            }
        }

        /// <summary>
        /// 보충량 확정된 설비 ReadOnly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grdChemicalRegistration.View.GetRowCellValue(grdChemicalRegistration.View.FocusedRowHandle, "STATECODEID").Equals("SupplementConfirmed"))
                e.Cancel = true;
        }

        /// <summary>
        /// 보충량 저장버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupplementRegistartion_Click(object sender, EventArgs e)
        {
            if (this.Conditions.GetValue("p_division").Equals("Period")
                && this.Conditions.GetValue("p_round").Equals("*"))
            {
                // 회차는 전체조회로 저장할 수 없습니다.
                this.ShowMessage("AllSearchNotSave");
                return;
            }

            // 상태(분석대기)
            _saveConfirmState = "AnaysisStandby";
            DataTable changed = grdChemicalRegistration.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                this.ShowMessage("NoSaveData");
            }
            else
            {
                // 작업장 권한 체크
                CheckAuthorityArea(changed);

                foreach (DataRow row in changed.Rows)
                {
                    // 적정량 필수입력
                    if (string.IsNullOrEmpty(row["TITRATIONQTY"].ToString()))
                    {
                        if (row["ISDELETE"].Equals("N"))
                        {
                            throw MessageException.Create("VALIDATEREQUIREDVALUES");
                        }
                    }
                }

                changed.Columns.Add("SAVECONFIRM");

                foreach (DataRow row in changed.Rows)
                {
                    row["SAVECONFIRM"] = _saveConfirmState;
                    row["ANALYSISDATE"] = string.Format("{0:yyyy-MM-dd}", this.Conditions.GetValue("p_analysisDate")); // 분석일자
                    row["PMTYPE"] = Convert.ToString(this.Conditions.GetValue("p_division")); // 구분
                    row["DEGREE"] = Convert.ToString(this.Conditions.GetValue("p_round")); // 차수
                }
                if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(changed.Copy());
                    _chemicalSpecDt.TableName = "specData";
                    _chemicalSpecDt = _chemicalSpecDt.DefaultView.ToTable(true);

                    ds.Tables.Add(_chemicalSpecDt.Copy());

                    this.ExecuteRule("SaveChemicalRegistration", ds);
                    this.ShowMessage("SuccessSave");
                    this.OnSearchAsync();
                }
                else
                {
                    this.ShowMessage("CancelSave");
                }
            }
        }

        /// <summary>
        /// 보충량 확정버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupplementConfirmation_Click(object sender, EventArgs e)
        {
            int cnt = 0; // 분석대기 Row 갯수 Check

            for (int i = 0; i < grdChemicalRegistration.View.RowCount; i++)
            {
                if ((grdChemicalRegistration.DataSource as DataTable).Rows[i].RowState == DataRowState.Modified)
                {
                    // 변경된 사항을 저장후 확정해주세요.
                    this.ShowMessage("AfterSaveChanges");
                    return;
                }
                else if ((grdChemicalRegistration.DataSource as DataTable).Rows[i]["STATECODEID"].Equals("AnaysisStandby"))
                {
                    cnt++;
                }
            }

            // 분석대기인 Row가 한개도 없다면
            if (cnt == 0)
            {
                this.ShowMessage("보충량을 확정할 항목이 없습니다.");
                return;
            }

            // 상태(보충량확정)
            _saveConfirmState = "SupplementConfirmed";

            // 분석대기인 Row를 모두 가져온다.
            DataTable dt = (grdChemicalRegistration.DataSource as DataTable).AsEnumerable()
                                                                .Where(r => r["STATECODEID"].Equals("AnaysisStandby"))                                                        
                                                                .CopyToDataTable();

            dt.TableName = "list";
            dt.Columns.Add("SAVECONFIRM");

            // 작업장 권한 체크
            CheckAuthorityArea(dt);

            // 대공정 - 설비 - 설비단별로 재분석여부를 체크한 데이터테이블을 병합할 테이블
            DataTable mergeDt = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                row["SAVECONFIRM"] = _saveConfirmState;

                DataTable rdt = dt.AsEnumerable().Where(r => r["PROCESSSEGMENTCLASSID"].Equals(row["PROCESSSEGMENTCLASSID"])
                                                        && r["EQUIPMENTID"].Equals(row["EQUIPMENTID"])
                                                        && r["CHILDEQUIPMENTID"].Equals(row["CHILDEQUIPMENTID"])).CopyToDataTable();

                foreach (DataRow rRow in rdt.Rows)
                {
                    rRow["SAVECONFIRM"] = _saveConfirmState;

                    if (rRow["ISRESUPPLEMENT"].Equals("Y"))
                    {
                        foreach (DataRow rRow2 in rdt.Rows)
                        {
                            rRow2["ISRESUPPLEMENT"] = "Y";
                            rRow2["ISSUPPLEMENT"] = "Y";
                        }
                        break;
                    }
                }

                mergeDt.Merge(rdt, true, MissingSchemaAction.Ignore);
                mergeDt = mergeDt.DefaultView.ToTable(true);
            }

            if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mergeDt = mergeDt.AsEnumerable().Where(r => !string.IsNullOrWhiteSpace(r["SAVECONFIRM"].ToString()))
                                                .CopyToDataTable();
                mergeDt.TableName = "list";

                MessageWorker worker = new MessageWorker("SaveChemicalRegistration");
                worker.SetBody(new MessageBody()
                {
                    { "list", mergeDt }, // 보충량 저장정보 Merge 데이터 테이블
                });

                this.ShowMessage("Confirmation"); // '확정 되었습니다' 로 메시지 변경-2020.02.13-유석진
                this.OnSearchAsync();

                DataTable result = worker.Execute<DataTable>().GetResultSet();
                DataTable tgResult = result.Clone();

                if (result.Rows.Count != 0)
                {
                    DataTable toSendDt = CommonFunction.CreateChemicalAbnormalEmailDt();

                    string inspitemContents = ""; // 검사항목 내용
                    string gradeContents = ""; // 관리등급 내용
                    string analysisValueContents = ""; // 분석값 내용
                    string manageScopeContents = ""; // 관리범위 내용
                    string spectScopeContents = ""; // 규격범위 내용

                    foreach (DataRow standardRow in result.Rows)
                    {
                        if (tgResult.AsEnumerable().Where(r => r["PROCESSSEGMENTCLASSID"].Equals(standardRow["PROCESSSEGMENTCLASSID"])
                                                          && r["EQUIPMENTID"].Equals(standardRow["EQUIPMENTID"])
                                                          && r["CHILDEQUIPMENTID"].Equals(standardRow["CHILDEQUIPMENTID"])).Count() == 0)
                        {
                            tgResult.ImportRow(standardRow);

                            var standardList = result.AsEnumerable().Where(r => r["PROCESSSEGMENTCLASSID"].Equals(standardRow["PROCESSSEGMENTCLASSID"])
                                                                           && r["EQUIPMENTID"].Equals(standardRow["EQUIPMENTID"])
                                                                           && r["CHILDEQUIPMENTID"].Equals(standardRow["CHILDEQUIPMENTID"])).ToList();

                            if (standardList.Count != 0)
                            {
                                int count = 1;

                                foreach (DataRow itemRow in standardList)
                                {
                                    if (count != standardList.Count)
                                    {
                                        count++;
                                        inspitemContents += itemRow["CHEMICALNAME"] + "/";
                                        gradeContents += itemRow["CHEMICALLEVEL"] + "/";
                                        analysisValueContents += itemRow["ANALYSISVALUE"] + "/";
                                        manageScopeContents += itemRow["MANAGEMENTSCOPE"] + "/";
                                        spectScopeContents += itemRow["SPECSCOPE"] + "/";
                                    }
                                    else
                                    {
                                        inspitemContents += itemRow["CHEMICALNAME"];
                                        gradeContents += itemRow["CHEMICALLEVEL"];
                                        analysisValueContents += itemRow["ANALYSISVALUE"];
                                        manageScopeContents += itemRow["MANAGEMENTSCOPE"];
                                        spectScopeContents += itemRow["SPECSCOPE"];
                                    }
                                }
                            }

                            DataRow newRow = toSendDt.NewRow();

                            newRow["ANALYSISTYPE"] = Format.GetString(standardRow["PMTYPE"]); // 분석종류
                            newRow["LARGEPROCESSSEGMENTNAME"] = Format.GetString(standardRow["PROCESSSEGMENTCLASSNAME"]); // 대공정명
                            //2020-01-13 강유라 AREAID, AREANAME 추가
                            newRow["AREAID"] = Format.GetString(standardRow["AREAID"]); // 작업장ID
                            newRow["AREANAME"] = Format.GetString(standardRow["AREANAME"]); // 작업장명
                            newRow["EQUIPMENTNAME"] = Format.GetString(standardRow["EQUIPMENTNAME"]); // 설비명
                            newRow["CHILDEQUIPMENTNAME"] = Format.GetString(standardRow["CHILDEQUIPMENTNAME"]); // 설비단명
                            newRow["CHEMICALNAME"] = inspitemContents; // 약품명
                            newRow["CHEMICALLEVEL"] = gradeContents; // 약품등급
                            newRow["DEGREE"] = Format.GetString(standardRow["DEGREE"]); // 차수
                            newRow["ANALYSISVALUE"] = analysisValueContents; // 분석량
                            newRow["MANAGEMENTSCOPE"] = manageScopeContents; // 관리범위
                            newRow["SPECSCOPE"] = spectScopeContents; // 규격범위
                            newRow["BREAKTYPE"] = Format.GetString(standardRow["BREAKTYPE"]); // 이탈유형
                            newRow["REMARK"] = ""; // 비고
                            newRow["USERID"] = UserInfo.Current.Id; // 보내는사람
                            newRow["TITLE"] = Language.Get("CHEMICALABNORMALTITLE"); // 메일타이틀
                            newRow["INSPECTION"] = "ChemicalInspection"; // 검사종류
                            newRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType; // 언어타입

                            toSendDt.Rows.Add(newRow);

                            inspitemContents = string.Empty;
                            gradeContents = string.Empty;
                            analysisValueContents = string.Empty;
                            manageScopeContents = string.Empty;
                            spectScopeContents = string.Empty;
                        }
                    }

                    CommonFunction.ShowSendEmailPopupDataTable(toSendDt);
                }
            }
            else
            {
                this.ShowMessage("CancelSave");
            }


            // 대공정 - 설비 - 설비단별로 한세트로 만들기(이메일 내용 구성)
            //    if (result.Rows.Count != 0)
            //    {
            //        string allContents = ""; // 이메일 내용
            //        string inspitemContents = ""; // 검사항목 내용
            //        string gradeContents = ""; // 관리등급 내용
            //        string analysisValueContents = ""; // 분석값 내용
            //        string manageScopeContents = ""; // 관리범위 내용
            //        string spectScopeContents = ""; // 규격범위 내용

            //        foreach (DataRow standardRow in result.Rows)
            //        {
            //            if (tgResult.AsEnumerable().Where(r => r["PROCESSSEGMENTCLASSID"].Equals(standardRow["PROCESSSEGMENTCLASSID"])
            //                                              && r["EQUIPMENTID"].Equals(standardRow["EQUIPMENTID"])
            //                                              && r["CHILDEQUIPMENTID"].Equals(standardRow["CHILDEQUIPMENTID"])).Count() == 0)
            //            {
            //                tgResult.ImportRow(standardRow);

            //                var standardList = result.AsEnumerable().Where(r => r["PROCESSSEGMENTCLASSID"].Equals(standardRow["PROCESSSEGMENTCLASSID"])
            //                                                               && r["EQUIPMENTID"].Equals(standardRow["EQUIPMENTID"])
            //                                                               && r["CHILDEQUIPMENTID"].Equals(standardRow["CHILDEQUIPMENTID"])).ToList();

            //                if (standardList.Count != 0)
            //                {
            //                    int count = 1;

            //                    foreach (DataRow itemRow in standardList)
            //                    {
            //                        if (count != standardList.Count)
            //                        {
            //                            count++;
            //                            inspitemContents += itemRow["CHEMICALNAME"] + "/";
            //                            gradeContents += itemRow["CHEMICALLEVEL"] + "/";
            //                            analysisValueContents += itemRow["ANALYSISVALUE"] + "/";
            //                            manageScopeContents += itemRow["MANAGEMENTSCOPE"] + "/";
            //                            spectScopeContents += itemRow["SPECSCOPE"] + "/";
            //                        }
            //                        else
            //                        {
            //                            inspitemContents += itemRow["CHEMICALNAME"];
            //                            gradeContents += itemRow["CHEMICALLEVEL"];
            //                            analysisValueContents += itemRow["ANALYSISVALUE"];
            //                            manageScopeContents += itemRow["MANAGEMENTSCOPE"];
            //                            spectScopeContents += itemRow["SPECSCOPE"];
            //                        }
            //                    }
            //                }

            //                allContents += "\r\n" + "○ " + Language.Get("ANALYSISTYPE") + " : " + Format.GetString(standardRow["PMTYPE"]) + "\r\n" // 분석종류
            //                             + "○ " + Language.Get("LARGEPROCESSSEGMENTNAME") + " : " + Format.GetString(standardRow["PROCESSSEGMENTCLASSNAME"]) + "\r\n" // 대공정명
            //                             + "○ " + Language.Get("EQUIPMENTNAME") + " : " + Format.GetString(standardRow["EQUIPMENTNAME"]) + "\r\n" // 설비명
            //                             + "○ " + Language.Get("CHILDEQUIPMENTNAME") + " : " + Format.GetString(standardRow["CHILDEQUIPMENTNAME"]) + "\r\n" // 설비단명
            //                             + "○ " + Language.Get("CHEMICALNAME") + " : " + inspitemContents + "\r\n" // 약품명
            //                             + "○ " + Language.Get("CHEMICALLEVEL") + " : " + gradeContents + "\r\n" // 약품등급
            //                             + "○ " + Language.Get("DEGREE") + " : " + Format.GetString(standardRow["DEGREE"]) + "\r\n" // 차수
            //                             + "○ " + Language.Get("ANALYSISVALUE") + " : " + analysisValueContents + "\r\n" // 분석량
            //                             + "○ " + Language.Get("MANAGEMENTSCOPE") + " : " + manageScopeContents + "\r\n" // 관리범위 
            //                             + "○ " + Language.Get("SPECSCOPE") + " : " + spectScopeContents + "\r\n" // 규격범위
            //                             + "○ " + Language.Get("BREAKTYPE") + " : " + Format.GetString(standardRow["BREAKTYPE"]) + "\r\n"; // 이탈유형
            //            }

            //            inspitemContents = string.Empty;
            //            gradeContents = string.Empty;
            //            analysisValueContents = string.Empty;
            //            manageScopeContents = string.Empty;
            //            spectScopeContents = string.Empty;
            //        }

            //        CommonFunction.ShowSendEmailPopup(Language.Get("CHEMICALABNORMALTITLE"), allContents);
            //    }
            //}
            //else
            //{
            //    this.ShowMessage("CancelSave");
            //}
        }

        /// <summary>
        /// Spec Out Data 색깔 변경
        /// </summary>
        /// <param name="sender"></param>★
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "PROCESSSEGMENTCLASSNAME" || e.Column.FieldName == "EQUIPMENTNAME"
                || e.Column.FieldName == "CONFIRMSTATE" //|| e.Column.FieldName == "CHILDEQUIPMENTNAME"
                || e.Column.FieldName == "DEGREE" || e.Column.FieldName == "FACTORYNAME")
            {
                e.Appearance.BackColor = Color.White;
            }

            // 규격범위 벗어났을때 색깔
            if (grdChemicalRegistration.View.GetRowCellValue(e.RowHandle, "ISSPECOUT").Equals("Y")
                && !string.IsNullOrWhiteSpace(grdChemicalRegistration.View.GetRowCellValue(e.RowHandle, "TITRATIONQTY").ToString()))
            {
                if (e.Column.FieldName == "TITRATIONQTY" || e.Column.FieldName == "ANALYSISVALUE"
                    || e.Column.FieldName == "SUPPLEMENTQTY" //|| e.Column.FieldName == "COLLECTIONTIME"
                    || e.Column.FieldName == "CHEMICALNAME" || e.Column.FieldName == "CHEMICALLEVEL"
                    || e.Column.FieldName == "MANAGEMENTSCOPE" || e.Column.FieldName == "SPECSCOPE"
                    || e.Column.FieldName == "CHILDEQUIPMENTNAME")
                {
                    e.Appearance.BackColor = Color.PaleVioletRed;
                }
            }
            // 관리범위 벗어났을때 색깔
            else if (grdChemicalRegistration.View.GetRowCellValue(e.RowHandle, "ISCONTROLLIMITOUT").Equals("Y")
                     && !string.IsNullOrWhiteSpace(grdChemicalRegistration.View.GetRowCellValue(e.RowHandle, "TITRATIONQTY").ToString()))
            {
                if (e.Column.FieldName == "TITRATIONQTY" || e.Column.FieldName == "ANALYSISVALUE"
                    || e.Column.FieldName == "SUPPLEMENTQTY" //|| e.Column.FieldName == "COLLECTIONTIME"
                    || e.Column.FieldName == "CHEMICALNAME" || e.Column.FieldName == "CHEMICALLEVEL"
                    || e.Column.FieldName == "MANAGEMENTSCOPE" || e.Column.FieldName == "SPECSCOPE"
                    || e.Column.FieldName == "CHILDEQUIPMENTNAME")
                {
                    e.Appearance.BackColor = Color.DarkOrange;
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
                // 적정량을 사용자가 지웠다면 해당 Row 초기화 ★
                if (string.IsNullOrWhiteSpace(Format.GetString(e.Value)))
                {
                    grdChemicalRegistration.View.SetFocusedRowCellValue("ISSUPPLEMENT", "N");
                    grdChemicalRegistration.View.SetFocusedRowCellValue("ISRESUPPLEMENT", "N");
                    grdChemicalRegistration.View.SetFocusedRowCellValue("ANALYSISVALUE", null);
                    grdChemicalRegistration.View.SetFocusedRowCellValue("SUPPLEMENTQTY", null);
                    grdChemicalRegistration.View.SetFocusedRowCellValue("MESSAGE", null);

                    grdChemicalRegistration.View.GetDataRow(e.RowHandle).AcceptChanges();

                    DeleteSpecTempTable(grdChemicalRegistration.View.GetDataRow(e.RowHandle));

                    return;

                    //// 신규행이라면 편집모드만 종료
                    //if (string.IsNullOrWhiteSpace(Format.GetString(grdChemicalRegistration.View.GetFocusedRowCellValue("SEQUENCE"))))
                    //{
                    //    grdChemicalRegistration.View.SetFocusedRowCellValue("ISSUPPLEMENT", "N");
                    //    grdChemicalRegistration.View.SetFocusedRowCellValue("ISRESUPPLEMENT", "N");
                    //    grdChemicalRegistration.View.SetFocusedRowCellValue("ANALYSISVALUE", null);
                    //    grdChemicalRegistration.View.SetFocusedRowCellValue("SUPPLEMENTQTY", null);
                    //    grdChemicalRegistration.View.SetFocusedRowCellValue("MESSAGE", null);

                    //    grdChemicalRegistration.View.GetDataRow(e.RowHandle).AcceptChanges();
                    //    return;
                    //}
                    //// 기존행이라면 해당 데이터 삭제
                    //else
                    //{
                    //    grdChemicalRegistration.View.SetFocusedRowCellValue("ANALYSISVALUE", null);
                    //    grdChemicalRegistration.View.SetFocusedRowCellValue("SUPPLEMENTQTY", null);
                    //    grdChemicalRegistration.View.SetFocusedRowCellValue("ISDELETE", "Y");
                    //    return;
                    //}
                }

                Dictionary<string, object> param = new Dictionary<string, object>();
                DataRow keyRow = grdChemicalRegistration.View.GetFocusedDataRow();
                param.Add("SPECSEQUENCE", keyRow["SPECSEQUENCE"]);
                param.Add("SPECCLASSID", "ChemicalSpec");
                //param.Add("PROCESSSEGMENTCLASSID", keyRow["PROCESSSEGMENTCLASSID"]);
                //param.Add("EQUIPMENTID", keyRow["EQUIPMENTID"]);
                //param.Add("CHILDEQUIPMENTID", keyRow["CHILDEQUIPMENTID"]);
                //param.Add("INSPITEMID", keyRow["INSPITEMID"]);

                if (_chemicalSpecDt.Rows.Count == 0)
                {
                    _chemicalSpecDt = SqlExecuter.Query("GetInspitemSpecData", "10001", param);

                    // 2020.03.21-유석진-분석일자 설정하도록 수정
                    foreach (DataRow rw in _chemicalSpecDt.Rows)
                    {
                        rw["REANALYSISTYPE"] = "Analysis";
                        rw["ANALYSISTYPE"] = Convert.ToString(this.Conditions.GetValue("p_chemicalWaterType"));
                        rw["ANALYSISDATE"] = string.Format("{0:yyyy-MM-dd}", this.Conditions.GetValue("p_analysisDate"));
                        rw["DEGREE"] = Convert.ToString(this.Conditions.GetValue("p_round"));
                        rw["PMTYPE"] = Convert.ToString(this.Conditions.GetValue("p_division"));
                    }

                    _chemicalSpecDt = _chemicalSpecDt.DefaultView.ToTable(true);
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
                        rw["ANALYSISDATE"] = string.Format("{0:yyyy-MM-dd}", this.Conditions.GetValue("p_analysisDate"));
                        rw["DEGREE"] = Convert.ToString(this.Conditions.GetValue("p_round"));
                        rw["PMTYPE"] = Convert.ToString(this.Conditions.GetValue("p_division"));
                    }

                    _chemicalSpecDt = _chemicalSpecDt.DefaultView.ToTable(true);
                }

                // 분석치가 계산된 후 다시 CellValueChange를 타기위한 이벤트(계산된 값을 가지고 SpecCheck)
                View_CellValueChanged2(grdChemicalRegistration.View, new DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs
                                    (e.RowHandle
                                    , grdChemicalRegistration.View.Columns["ANALYSISVALUE"]
                                    , grdChemicalRegistration.View.GetRowCellValue(e.RowHandle, "ANALYSISVALUE")));
            }
        }

        /// <summary>
        /// 적정량에 의해 계산된 분석치를 가지고 SpeckCheck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged2(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow paramrow = this.grdChemicalRegistration.View.GetFocusedDataRow();

            string _sRULEID = "'" + paramrow["CALCULATIONTYPE"] + "','" + paramrow["FOMULATYPE"] + "'";
            string[] _areaRULEID = _sRULEID.Split(new char[] { ',' });

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("RULEID", _sRULEID);

            DataTable dtResult = SqlExecuter.Query("GetCalculationRule", "10001", dicParam);

            if (paramrow["COLVALUE"].ToString() != "Y")
            {
                // 수정된 행이 존재 할경우 로직 실행
                DataRow[] rowChk = dtResult.Select("CODEID = '" + grdChemicalRegistration.View.FocusedColumn.FieldName + "'");

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
        /// 사용자 지정 Cell Merge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            if (e.Column.FieldName == "PROCESSSEGMENTCLASSNAME" || e.Column.FieldName == "EQUIPMENTNAME"
                || e.Column.FieldName == "CONFIRMSTATE" //|| e.Column.FieldName == "CHILDEQUIPMENTNAME"
                || e.Column.FieldName == "DEGREE" || e.Column.FieldName == "FACTORYNAME")
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

            ExecuteRule("SaveChemicalRegistration", grdChemicalRegistration.GetChangedRows());
        }

        /// <summary>
        /// 툴바버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                BtnSupplementRegistartion_Click(null, null);
            }
            else if (btn.Name.ToString().Equals("Confirmation"))
            {
                BtnSupplementConfirmation_Click(null, null);
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

            DateTime analysisDate = Convert.ToDateTime(values["P_ANALYSISDATE"]);
            values["P_ANALYSISDATE"] = string.Format("{0:yyyy-MM-dd}", analysisDate);
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable chemicalAnalysisDt = await SqlExecuter.QueryAsync("GetChemicalAnalysis", "10001", values);

            if (chemicalAnalysisDt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }
            grdChemicalRegistration.DataSource = chemicalAnalysisDt;
            _chemicalSpecDt.Clear();

            // 분석값 계산 Rule ID
            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("ENTERPRISEID", chemicalAnalysisDt.Rows[0]["ENTERPRISEID"]);
            //param.Add("PLANTID", chemicalAnalysisDt.Rows[0]["PLANTID"]);
            //param.Add("TARGETATTRIBUTE", "ANALYSISVALUE");

            //DataTable ruleDt1 = SqlExecuter.Query("GetRuleIdByPlant", "10001", param);

            //// 보충량 계산 Rule ID
            //param.Remove("TARGETATTRIBUTE");
            //param.Add("TARGETATTRIBUTE", "SUPPLEMENTQTY");
            //DataTable ruleDt2 = SqlExecuter.Query("GetRuleIdByPlant", "10001", param);

            //// 자동계산(분석치, 보충량)
            //SetCalculationRule calc = new SetCalculationRule();
            //calc.SetCalculationRulegrdChemicalRegistration(new string[] { Format.GetString(ruleDt1.Rows[0]["RULEID"]), Format.GetString(ruleDt2.Rows[0]["RULEID"]) }, grdChemicalRegistration, "COLVALUE");
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
        /// 정기이면서 전체조회라면 보충량 저장버튼 비활성화
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // 조회조건에 구성된 Control에 대한 처리가 필요한 경우
            SmartComboBox divisionCombo = Conditions.GetControl<SmartComboBox>("p_division");
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

            divisionCombo.EditValueChanged += (sender, args) =>
            {
                ButtonEnableProcess(Format.GetString(divisionCombo.EditValue), Format.GetString(roundCombo.EditValue));
            };

            roundCombo.EditValueChanged += (sender, args) =>
            {
                if (roundCombo.GetDataValue() == null)
                {
                    return;
                }

                ButtonEnableProcess(Format.GetString(divisionCombo.EditValue), Format.GetString(roundCombo.EditValue));
            };
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

            grdChemicalRegistration.View.CheckValidation();

            if (grdChemicalRegistration.GetChangedRows().Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 적정량 Cell을 지울때 Spec을 저장하는 임시테이블 삭제
        /// </summary>
        private void DeleteSpecTempTable(DataRow row)
        {
            DataTable dt = _chemicalSpecDt;

            string reAnalysisType = "Analysis";
            string analysisType = Convert.ToString(this.Conditions.GetValue("p_chemicalWaterType"));
            string analysisDate = string.Format("{0:yyyy-MM-dd}", this.Conditions.GetValue("p_analysisDate"));
            string degree = Convert.ToString(this.Conditions.GetValue("p_round"));
            string pmType = Convert.ToString(this.Conditions.GetValue("p_division"));

            foreach (DataRow delRow in _chemicalSpecDt.AsEnumerable().Where(r => r["INSPITEMID"].Equals(Format.GetString(row["INSPITEMID"])) && r["CHILDEQUIPMENTID"].Equals(Format.GetString(row["CHILDEQUIPMENTID"]))
                                       && r["REANALYSISTYPE"].Equals(reAnalysisType) && r["EQUIPMENTID"].Equals(Format.GetString(row["EQUIPMENTID"])) && r["PROCESSSEGMENTCLASSID"].Equals(Format.GetString(row["PROCESSSEGMENTCLASSID"]))
                                       && r["ANALYSISTYPE"].Equals(analysisType) && r["ANALYSISDATE"].Equals(analysisDate) && r["DEGREE"].Equals(degree) && r["PMTYPE"].Equals(pmType)))
            {
                delRow.Delete();
            }

            _chemicalSpecDt.AcceptChanges();
        }

        /// <summary>
        /// Spec Check 모듈
        /// </summary>
        private void SpecCheck(DataTable specDt)
        {
            DataRow focusRow = grdChemicalRegistration.View.GetFocusedDataRow();

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

            if (string.IsNullOrEmpty(grdChemicalRegistration.View.GetRowCellValue(grdChemicalRegistration.View.FocusedRowHandle, "ANALYSISVALUE").ToString()))
            {
                return;
            }

            spcRules.nValue = Convert.ToDouble(grdChemicalRegistration.View.GetRowCellValue(grdChemicalRegistration.View.FocusedRowHandle, "ANALYSISVALUE"));

            spcRules.defaultChartType = SpcChartType.xbarr; // 기본 차트타입은 xbarr기준으로 함

            //Spec Check
            SpcRulesOver returnRulesOver = new SpcRulesOver();
            returnRulesOver = spcHelper.SpcSpecRuleCheck(spcRules);

            // 규격범위(SL)가 벗어났을땐 스펙아웃 및 메일팝업호출
            if (returnRulesOver.isSpec == true)
            {
                grdChemicalRegistration.View.SetRowCellValue(grdChemicalRegistration.View.FocusedRowHandle, "ISSPECOUT", "Y"); // SpecOut여부 Y
                grdChemicalRegistration.View.SetRowCellValue(grdChemicalRegistration.View.FocusedRowHandle, "ISSUPPLEMENT", "Y"); // 액보충필요 Y
                grdChemicalRegistration.View.SetRowCellValue(grdChemicalRegistration.View.FocusedRowHandle, "ISRESUPPLEMENT", "Y"); // 재분석여부 Y

                // Spec Out입니다.
                this.ShowMessage(returnRulesOver.message.value);

                //if (this.ShowMessageBox(returnRulesOver.message.value, "Caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
                //{
                //    SendMailPopup popup = new SendMailPopup();
                //    popup.Owner = this;
                //    popup.CurrentDataRow = grdChemicalRegistration.View.GetFocusedDataRow();
                //    popup.ShowDialog();
                //}
            }
            // 규격범위(SL)가 벗어나지 않았을땐 정상스펙
            else
            {
                // 관리범위(CL)가 벗어났을땐 해당 Row 색깔만 변경
                if (returnRulesOver.isControlLimit == true)
                {
                    grdChemicalRegistration.View.SetRowCellValue(grdChemicalRegistration.View.FocusedRowHandle, "ISCONTROLLIMITOUT", "Y");
                }
                else
                {
                    grdChemicalRegistration.View.SetRowCellValue(grdChemicalRegistration.View.FocusedRowHandle, "ISCONTROLLIMITOUT", "N"); // 관리범위여부 N
                }

                grdChemicalRegistration.View.SetRowCellValue(grdChemicalRegistration.View.FocusedRowHandle, "ISSPECOUT", "N"); // SpecOut여부 N
                grdChemicalRegistration.View.SetRowCellValue(grdChemicalRegistration.View.FocusedRowHandle, "ISSUPPLEMENT", "N"); // 액보충필요 N
                grdChemicalRegistration.View.SetRowCellValue(grdChemicalRegistration.View.FocusedRowHandle, "ISRESUPPLEMENT", "N"); // 재분석여부 N

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
        /// 툴바버튼 활성화, 비활성화 체크
        /// </summary>
        /// <param name="pmtype"></param>
        /// <param name="degree"></param>
        private void ButtonEnableProcess(string pmtype, string degree)
        {
            // 정기건이면서 전체조회라면 보충량 저장버튼 비활성화
            if (pmtype.Equals("Period"))
            {
                if (degree.Equals("*"))
                {
                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        {
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                        }
                    }
                }
                else
                {
                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        {
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                        }
                    }
                }
            }
            // 그렇지 않은 경우 보충량 저장버튼 활성화
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                    }
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

        #endregion
    }
}
