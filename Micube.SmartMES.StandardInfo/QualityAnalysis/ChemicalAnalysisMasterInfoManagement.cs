#region using

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Validations;
using Micube.SmartMES.Commons;
using Micube.SmartMES.StandardInfo.Popup;
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

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > 약품분석 기준정보
    /// 업  무  설  명  :  약품분석 기준정보 연계정보를 관리 한다
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-06-26
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ChemicalAnalysisMasterInfoManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        string strInspectiondefid = "";
        #endregion

        #region 생성자

        public ChemicalAnalysisMasterInfoManagement()
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

            InitializeGrdChemicalInspection();
            InitializeGrdInspectionitemrelList();
            InitializeGrdFormulaDef();
            InitializeGrdFormula();
        }

        #region 약품 분석 탭의 그리드 초기화
        /// <summary>        
        /// 약품 분석 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdChemicalInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdEQUIPMENTSub.GridButtonItem = GridButtonItem.Export;
            grdEQUIPMENTSub.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdEQUIPMENTSub.View.SetIsReadOnly();
            grdEQUIPMENTSub.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150);
            grdEQUIPMENTSub.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);
            grdEQUIPMENTSub.View.AddTextBoxColumn("PARENTEQUIPMENTID", 150).SetLabel("EQUIPMENTID");
            grdEQUIPMENTSub.View.AddTextBoxColumn("PARENTEQUIPMENTNAME", 200).SetLabel("EQUIPMENTNAME");
            grdEQUIPMENTSub.View.AddTextBoxColumn("EQUIPMENTID", 200).SetLabel("CHILDEQUIPMENTID");
            grdEQUIPMENTSub.View.AddTextBoxColumn("EQUIPMENTNAME", 200).SetLabel("CHILDEQUIPMENTNAME");
            grdEQUIPMENTSub.View.AddTextBoxColumn("AREAID", 150);
            grdEQUIPMENTSub.View.AddTextBoxColumn("AREANAME", 200);
            grdEQUIPMENTSub.View.PopulateColumns();

        }

        /// <summary>        
        /// 약품 분석 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdInspectionitemrelList()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInspectionitemrelList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdInspectionitemrelList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdInspectionitemrelList.View.AddTextBoxColumn("INSPECTIONDEFID", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("INSPECTIONUNIT", 80).SetIsHidden();
            
            grdInspectionitemrelList.View.AddTextBoxColumn("TOPINSPITEMNAME", 200).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("MIDINSPITEMCLASSNAME", 200).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200).SetIsHidden();
            //grdInspectionitemrelList.View.AddTextBoxColumn("RESOURCETYPE", 80).SetIsHidden();
            //grdInspectionitemrelList.View.AddTextBoxColumn("PROCESSSEGMENTTYPE", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("PROCESSSEGID", 250).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("PROCESSSEGNAME", 250).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("PROCESSEGVERSION", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("PLANTID", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("INSPITEMCLASSID", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("VALIDTYPE", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("SEQUENCE", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("SPECCLASSID", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("SPECSEQUENCE", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("QTYUNIT", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("DESCRIPTION", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("ITEMVERSION", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("RESOURCEID", 150).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("RESOURCEVERSION", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("INSPECTORDEGREE", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("ISAQL", 80).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("AQLINSPECTIONLEVEL", 100).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("AQLDEFECTLEVEL", 100).SetIsHidden();

            grdInspectionitemrelList.View.AddTextBoxColumn("AQLDECISIONDEGREE", 100).SetIsHidden();
           
            grdInspectionitemrelList.View.AddTextBoxColumn("AQLCYCLE", 100).SetIsHidden();
            grdInspectionitemrelList.View.AddSpinEditColumn("NCRINSPECTIONQTY", 100).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("NCRCYCLE", 100).SetIsHidden();
            grdInspectionitemrelList.View.AddTextBoxColumn("INSPITEMTYPE", 100).SetIsHidden();
            InitializeGrd_InspItemIdPopup();
            grdInspectionitemrelList.View.AddTextBoxColumn("INSPITEMNAME", 150).SetIsReadOnly();
            grdInspectionitemrelList.View.AddTextBoxColumn("INSPITEMVERSION", 80)
                .SetIsReadOnly()
                .SetIsHidden(); 
           
            grdInspectionitemrelList.View.AddTextBoxColumn("TANKSIZE", 80);
            grdInspectionitemrelList.View.AddTextBoxColumn("ANALYSISCONST", 100);


            grdInspectionitemrelList.View.AddComboBoxColumn("CALCULATIONTYPE", new SqlQuery("GetRuleDefinitionOnlyQuality", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "RULENAME", "RULEID")
            .SetPopupDefaultByGridColumnId("PLANTID");
            
            grdInspectionitemrelList.View.AddComboBoxColumn("FOMULATYPE", new SqlQuery("GetRuleDefinitionOnlyQuality", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "RULENAME", "RULEID")
            .SetPopupDefaultByGridColumnId("PLANTID");

            grdInspectionitemrelList.View.AddTextBoxColumn("QTYCONST", 100);
            grdInspectionitemrelList.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdInspectionitemrelList.View.AddComboBoxColumn("ISNCR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdInspectionitemrelList.View.AddComboBoxColumn("NCRDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdInspectionitemrelList.View.AddTextBoxColumn("NCRDEFECTRATE", 100).SetIsHidden();
            grdInspectionitemrelList.View.AddSpinEditColumn("NCRLOTSIZE", 100);
            grdInspectionitemrelList.View.AddTextBoxColumn("ISSPEC", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            

            grdInspectionitemrelList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);


            grdInspectionitemrelList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionitemrelList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionitemrelList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionitemrelList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionitemrelList.View.PopulateColumns();
        }
        /// <summary>
        /// 설비단을 선택하는 팝업
        /// </summary>
        //테이블 생성 후 수정 필
        private void InitializeGrd_InspItemIdPopup()
        {
            //팝업 컬럼 설정
            var inspItemId = grdInspectionitemrelList.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemIdChemicalAnalysis", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(500,500, FormBorderStyle.FixedToolWindow)
                                            .SetValidationKeyColumn()
                                            .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            {
                                                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                                foreach (DataRow row in selectedRows)
                                                {
                                                    dataGridRow["INSPITEMNAME"] = row["INSPITEMNAME"].ToString();
                                                    dataGridRow["INSPITEMVERSION"] = row["INSPITEMVERSION"].ToString();
                                                }
                                            });

            inspItemId.Conditions.AddTextBox("P_INSPECTIONDEFID")
               .SetPopupDefaultByGridColumnId("INSPECTIONDEFID")
               .SetLabel("")
               .SetIsHidden();
            ///inspItemId.Conditions.AddTextBox("INSPITEMID");
            inspItemId.Conditions.AddTextBox("INSPITEMNAME");
            
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMID", 100);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 80);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
        }
        #endregion

        #region 계산식 탭의 그리드 초기화
        /// <summary>        
        /// 계산식명 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdFormulaDef()
        {
            // TODO : 그리드 초기화 로직 추가
            grdFormulaDef.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;
            grdFormulaDef.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdFormulaDef.View.AddTextBoxColumn("RULEID", 150)
                .SetValidationKeyColumn();

            grdFormulaDef.View.AddTextBoxColumn("RULENAME", 200)
                .SetValidationIsRequired();

            grdFormulaDef.View.AddTextBoxColumn("RULESET", 150)
                .SetDefault("Quality")
                .SetIsHidden();

            //CODECLASS 수정
            grdFormulaDef.View.AddComboBoxColumn("TARGETATTRIBUTE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=TargetAttribute", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired(); 

            grdFormulaDef.View.AddComboBoxColumn("RULETYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RuleType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Calculation")
                .SetIsReadOnly();

            grdFormulaDef.View.AddTextBoxColumn("ENTERPRISEID")
                .SetIsHidden();

            grdFormulaDef.View.AddTextBoxColumn("PLANTID");
                

            grdFormulaDef.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdFormulaDef.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFormulaDef.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFormulaDef.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFormulaDef.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFormulaDef.View.PopulateColumns();

        }

        /// <summary>        
        /// 계산식 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdFormula()
        {
            // TODO : 그리드 초기화 로직 추가
            grdFormula.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;
            grdFormula.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdFormula.View.AddTextBoxColumn("RULEID", 150)
                .SetIsHidden();

            grdFormula.View.AddTextBoxColumn("SEQUENCE", 150)
                .SetIsReadOnly()
                .SetValidationKeyColumn();

            grdFormula.View.AddTextBoxColumn("FRONTBRACKET", 200);

            InitializeGrid_FormulaAttribute();

            grdFormula.View.AddTextBoxColumn("CODENAME", 250)
                .SetIsReadOnly();
            
            grdFormula.View.AddTextBoxColumn("BACKBRACKET", 200);

            grdFormula.View.AddComboBoxColumn("OPERATOR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Operator", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")); ;

            grdFormula.View.AddComboBoxColumn("SEPARATORCODE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SeparatorCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdFormula.View.AddTextBoxColumn("ENTERPRISEID")
                .SetIsHidden();

            grdFormula.View.AddTextBoxColumn("PLANTID")
                .SetIsHidden();

            grdFormula.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdFormula.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFormula.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFormula.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFormula.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFormula.View.PopulateColumns();

            grdFormula.View.PopulateColumns();
        }
        /// <summary>
        /// Formula의 ATTRIBUTE를 검색하는 팝업
        /// </summary>
        private void InitializeGrid_FormulaAttribute()
        {
            var parentCodeClassPopupColumn = this.grdFormula.View.AddSelectPopupColumn("CODEID", new SqlQuery("GetFormulaCode", "10001", "CODECLASSID=ChemicalRuleColumn", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME")
                .SetPopupLayout("SELECTPARENTCODECLASSID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow);

            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("CODEID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("CODENAME");

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODEID", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODENAME", 200);
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //화면 로드 이벤트
          
            //Spec 버튼 클릭시 이벤트
           // btnToSpec.Click += BtnToSpec_Click;
            //선택 된 탭이 바뀔 때 이벤트


            #region 설비단 이벤트
            grdEQUIPMENTSub.View.FocusedRowChanged += grdEQUIPMENTSub_FocusedRowChanged;
            #endregion

            #region 약품분석 탭의 이벤트


            #endregion

            #region 수질분석 탭의 이벤트

            #endregion

            #region 계산식 탭의 이벤트
            //grdFormulaDef 그리드의 Row추가시 이벤트
            grdFormulaDef.View.AddingNewRow += View_GrdFormulaDef_AddingNewRow;
            //grdFormulaDef 그리드의 포커스 row가 변경될 때 이벤트
            grdFormulaDef.View.FocusedRowChanged += View_GrdFormulaDef_FocusedRowChanged;
            //grdFormula 그리드의 Row추가시 이벤트
            grdFormula.View.AddingNewRow += View_GrdFormula_AddingNewRow;
            //grdFormula 그리드의 Row추가시 확인 이벤트
            grdFormula.ToolbarAddingRow += GrdFormula_ToolbarAddingRow;

            grdInspectionitemrelList.View.AddingNewRow += grdInspectionitemrelList_AddingNewRow;

          //  new SetGridDeleteButonVisible(grdFormulaDef);
          //  new SetGridDeleteButonVisible(grdFormula);

            btnSpecRegisterDetail.Click += BtnSpecRegisterDetail_Click;
            #endregion
        }

        private void BtnSpecRegisterDetail_Click(object sender, EventArgs e)
        {
            if (grdInspectionitemrelList.View.DataRowCount == 0) return;
            DataRow chkData = grdInspectionitemrelList.View.GetFocusedDataRow();
            if (chkData == null) return;
            if (chkData.RowState == DataRowState.Added) return;
            if (!(chkData["INSPITEMTYPE"].ToString().Equals("SPC")))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidStdData002", chkData["INSPITEMTYPE"].ToString()); //메세지 
                return;
            }
            int irow = grdInspectionitemrelList.View.FocusedRowHandle;
            SpecDetailPopUp popup = new SpecDetailPopUp();
            if (chkData["SPECCLASSID"].ToString().Equals(""))
            {
                popup._specSequence = "";
                popup._specClassId = "ChemicalSpec";
            }
            else
            {
                popup._specSequence = chkData["SPECSEQUENCE"].ToString();
                popup._specClassId = chkData["SPECCLASSID"].ToString();

            }
            popup._inspitemid = chkData["INSPITEMID"].ToString();
            popup._inspectiondefid = chkData["INSPECTIONDEFID"].ToString();
            popup._equipmentid = grdEQUIPMENTSub.View.GetFocusedRowCellValue("PARENTEQUIPMENTID").ToString();
            popup._childequipmentid = chkData["RESOURCEID"].ToString();
            popup._enterpriseid = chkData["ENTERPRISEID"].ToString();
            popup._plantid = chkData["PLANTID"].ToString();
            popup._processsegmentclassid = chkData["PROCESSSEGID"].ToString();
            popup._resourceid = chkData["RESOURCEID"].ToString();
            popup._resourceversion = chkData["RESOURCEVERSION"].ToString();
            popup._consumabledefid = "*";
            popup._productdefid = "*";
            popup.buttonType = true;
            // popup._inspectionName = _inspectionName;
            popup.Owner = this;
            popup.ShowDialog();

            DataRow row = grdEQUIPMENTSub.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            var values = Conditions.GetValues();
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("INSPECTIONDEFID", values["INSPECTIONDEFID"].ToString());
            Param.Add("RESOURCEID", row["EQUIPMENTID"].ToString());
            Param.Add("RESOURCEVERSION", "*");
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtInspectionitemrelList = SqlExecuter.Query("GetQcEquipmentList", "10001", Param);
            grdInspectionitemrelList.DataSource = dtInspectionitemrelList;
            grdInspectionitemrelList.View.ClearSelection();
            grdInspectionitemrelList.View.FocusedRowHandle = irow;

            grdInspectionitemrelList.View.SelectRow(irow);
        }

        private void grdInspectionitemrelList_AddingNewRow(SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdEQUIPMENTSub.View.DataRowCount == 0)
            {
                grdInspectionitemrelList.View.DeleteRow(grdInspectionitemrelList.View.FocusedRowHandle);

                return;
            }
            var value = Conditions.GetValues();

            args.NewRow["INSPECTIONDEFID"] = strInspectiondefid;
            args.NewRow["INSPECTIONDEFVERSION"] = "*";
            args.NewRow["RESOURCETYPE"] = "Equipment";

            DataRow row = grdEQUIPMENTSub.View.GetFocusedDataRow();

            args.NewRow["RESOURCEID"] = row["EQUIPMENTID"];
            args.NewRow["RESOURCEVERSION"] = "*";
            //args.NewRow["PROCESSSEGMENTTYPE"] = "TopProcessSegment";2019.11.29 삭제 CHOI 에러 문제 발생

            args.NewRow["PROCESSSEGID"] = row["PROCESSSEGMENTCLASSID"];
            args.NewRow["PROCESSEGVERSION"] = "*";
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;

            //args.NewRow["INSPITEMCLASSID"] = value["INSPECTIONDEFID"];;2019.11.29 삭제 CHOI 에러 문제 발생

            //args.NewRow["PROCESSSEGMENTTYPE"] = "*"; 2019.11.29 삭제 CHOI 에러 문제 발생
            //args.NewRow["PROCESSSEGID"] = "*"; // 2020.03.04-유석진-위에서 선택한 PROCESSSEGMENTCLASSID 값을 넣으므로 주석처리
            args.NewRow["ISNCR"] = "NCR";


            //args.NewRow["SPECCLASSID"] = "ChemicalSpec";

            //GetNumber number = new GetNumber();

            //DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
            //string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);

            //args.NewRow["SPECSEQUENCE"] =  number.GetStdNumber("OperationInspection", "ChemicalSpec");


            args.NewRow["VALIDSTATE"] = "Valid";

        }

        private void grdEQUIPMENTSub_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdEQUIPMENTSub.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            var values = Conditions.GetValues();
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("INSPECTIONDEFID", values["INSPECTIONDEFID"].ToString());
            Param.Add("RESOURCEID", row["EQUIPMENTID"].ToString());
            Param.Add("RESOURCEVERSION", "*");
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            
            DataTable dtInspectionitemrelList = SqlExecuter.Query("GetQcEquipmentList", "10001", Param);
            grdInspectionitemrelList.DataSource = dtInspectionitemrelList;
        }

        /// <summary>
        /// 그리드를 더블 클릭했을 때 specDetail을 조회화는 팝업
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            SmartBandedGridView view = sender as SmartBandedGridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            DataRow chkData = null;
            if (tabChemicalWater.SelectedTabPageIndex == 2)
            {
                chkData = grdInspectionitemrelList.View.GetDataRow(info.RowHandle);
            }
          
            if (chkData == null) return;

            DialogManager.ShowWaitArea(pnlContent);

            SpecRegisterDetailPopUp popup = new SpecRegisterDetailPopUp();
            popup.SetControlHidden();
            popup._specSequence = chkData["SPECSEQUENCE"].ToString();
            popup._specClassId = chkData["SPECCLASSID"].ToString();
            popup._cboCheck = chkData["DEFAULTCHARTTYPE"].ToString();//hc.kim  데이터 넘김 
            popup.Owner = this;
            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);    
        }

     


    
    
        /// <summary>
        /// 계산명그리드의 포커스된 row가 변경될 때 계산식을 검색하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdFormulaDef_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchFormula();
        }

        /// <summary>
        /// 계산명의 row가 없거나 ruleId를 입력하지 않았을 경우 row추가 취소이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdFormula_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            DataRow row = grdFormulaDef.View.GetFocusedDataRow();
            if (row == null || string.IsNullOrWhiteSpace(row["RULEID"].ToString()))
            {
                e.Cancel = true;
                this.ShowMessage("SelectFormulaDef");
            }
        }

        /// <summary>
        /// 계산명 그리드의 row추가시 planId, EnterpriseId 자동입력이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_GrdFormulaDef_AddingNewRow(SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            args.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;

            var values = Conditions.GetValues();
            args.NewRow["PLANTID"] = values["P_PLANTID"];
            args.NewRow["RULESET"] = "Quality";



        }

        //grdFormula 그리드의 Row추가시 planId, EnterpriseId,Sequence,ruleId 자동입력이벤트
        private void View_GrdFormula_AddingNewRow(SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow row = grdFormulaDef.View.GetFocusedDataRow();
            if (row == null) return;
            args.NewRow["RULEID"] = row["RULEID"];
            //2020-03-20 강유라 MM_RULEFORMULA 에 ENTERPRISEID,PLANTID 컬럼 없음
            //args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            //args.NewRow["PLANTID"] = row["PLANTID"];
            args.NewRow["SEQUENCE"] = (grdFormula.View.DataRowCount) * 10;
        }



        /// <summary>
        /// 새로 추가 한 Row가 이미 있는 경우 메세지 띄운후 Row추가 여부를 정하는 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdChemicalInspItemClass_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            SmartBandedGrid grid = sender as SmartBandedGrid;
            if (grid == null) return;
            var rowState = (grid.DataSource as DataTable).Rows.Cast<DataRow>()
                .Select(r => r.RowState.ToString()).ToList();

            //추가 한 후 저장하지 않은 데이터가 있을 때
            if (rowState.Contains("Added"))
            {
                DialogResult result = System.Windows.Forms.DialogResult.No;

                result = this.ShowMessage(MessageBoxButtons.YesNo, "DataIsNotSaved");//새로운 항목을 추가 할 경우 입력한 정보는 저장 되지 않습니다. 계속 하시겠습니까?

                if (result == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }


        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //// TODO : 저장 Rule 변경

            //DataTable changed = null;
            //DataTable changed2 = null;

            //switch (tabChemicalWater.SelectedTabPageIndex)
            //{
               
            //    case 0:
            //        //약품분석 탭
            //        changed = grdInspectionitemrelList.GetChangedRows();
            //        ExecuteRule("Inspectionitemrel", changed);
            //        break;
            //    case 1:
            //        //계산식 탭
            //        changed = grdFormulaDef.GetChangedRows();
            //        changed2 = grdFormula.GetChangedRows();

            //        DataSet rullSet = new DataSet();

            //        changed.TableName = "formulaDefList";
            //        changed2.TableName = "formulaList";

            //        if (changed.Rows.Count > 0)
            //        {
            //            rullSet.Tables.Add(changed);
            //        }

            //        if (changed2.Rows.Count > 0)
            //        {
            //            rullSet.Tables.Add(changed2);
            //        }

            //        ExecuteRule("SaveChemicalAnalysisMasterInfoManagementFormula", rullSet);
            //        break;
            //}
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {

                ProcSave(btn.Text);
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
            strInspectiondefid = values["INSPECTIONDEFID"].ToString();
            DataTable dt = null;

            switch (tabChemicalWater.SelectedTabPageIndex)
            {
                case 0:
                    //약품, 수질 분석 탭
                    grdInspectionitemrelList.View.ClearDatas();

                    values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
                    values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

                    dt = await SqlExecuter.QueryAsync("GetQcEquipmentSub", "10001", values);
                    grdEQUIPMENTSub.DataSource = dt;

                    break;


                case 1:
                    //계산식 탭
                    values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                    dt = await SqlExecuter.QueryAsync("SelectRule", "10001", values);
                    CheckHasData(dt);
                    grdFormulaDef.View.ClearDatas();
                    grdFormulaDef.DataSource = dt;
                    break;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            // 검사정의
            Conditions.AddComboBox("INSPECTIONDEFID", new SqlQuery("GetQcInspectionDefinitionCombo2", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"INSPECTIONCLASSID={"ChemicalInspection"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID").SetValidationIsRequired();
            // 유효상태
            Conditions.AddComboBox("VALIDSTATE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem();
            // 설비
            InitializeCondition_Popup();
        }


        /// <summary>
        /// 검색조건 품목팝업 
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("PARENTEQUIPMENTID", new SqlQuery("GetProcessSegMentEqptlist", "10001", $"EQUIPMENTTYPE={"Production"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
               .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1);  //팝업창 선택가능한 개수


            parentPopupColumn.Conditions.AddTextBox("EQUIPMENTID");
            parentPopupColumn.Conditions.AddTextBox("EQUIPMENTNAME");

            //팝업 그리드
            parentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);

        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용

            SmartComboBox combobox = Conditions.GetControl<SmartComboBox>("INSPECTIONDEFID");
            combobox.ItemIndex = 0;

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
            DataTable changed = null;
            DataTable changed2 = null;

            switch (tabChemicalWater.SelectedTabPageIndex)
            {
                case 0:
                    //약품분석 탭
                    grdInspectionitemrelList.View.CheckValidation();
                    changed = grdInspectionitemrelList.GetChangedRows();
                    CheckChanged(changed);
                    break;
                case 1:
                    //계산식 탭
                    grdFormulaDef.View.CheckValidation();
                    grdFormula.View.CheckValidation();
                    changed = grdFormulaDef.GetChangedRows();
                    changed2 = grdFormula.GetChangedRows();
                    CheckChanged(changed, changed2);
                    break;
            }
        }

        #endregion

        #region Private Function


        /// <summary>
        /// 바인딩 할 데이터 테이블에 데이터있는지 확인하는 함수
        /// </summary>
        /// <param name="table"></param>
        private void CheckHasData(DataTable table)
        {
            if (table.Rows.Count < 1) // 검색 조건에 해당하는 데이타 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

        /// <summary>
        /// 저장 전에 변경된 사항이 있는지 확인하는 함수
        /// </summary>
        /// <param name="table"></param>
        private void CheckChanged(DataTable table)
        {
            if (table.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        private void CheckChanged(DataTable table,DataTable table2)
        {
            if (table.Rows.Count == 0 && table2.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }
       

    

        /// <summary>
        /// 계산명 그리드의 포커스된 행에따라 계산식을 검색하는 함수
        /// </summary>
        private void SearchFormula()
        {
            grdFormula.View.ClearDatas();
            DataRow row = grdFormulaDef.View.GetFocusedDataRow();
            if (row == null) return;
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("RULEID", row["RULEID"]);
            grdFormula.DataSource = SqlExecuter.Query("SelectRuleFormula", "10001", values);
        }

        /// <summary>
        /// InspItemClass를 조회하는 함수
        /// </summary>
        private DataTable SearchInspItemClass()
        {
            var values = Conditions.GetValues();
            DataTable dt = null;
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            dt = SqlExecuter.Query("SelectInspItemClassChemicalAndWater", "10001", values);
            CheckHasData(dt);

            return dt;
        }


      


        private ValidationResultCommon ValidationCodeClassIdPopup(DataRow currentRow, IEnumerable<DataRow> popupSelections)
        {

            ValidationResultCommon result = new ValidationResultCommon();
            //string myCodeClassId = currentRow.ToStringNullToEmpty("CODECLASSID");

            object obj = grdFormula.DataSource;
            DataTable dt = (DataTable)obj;

            foreach (DataRow row in popupSelections)
            {
                if (dt.Select("CODEID = '" + row["CODEID"].ToString() + "'").Length != 0)
                {
                    Language.LanguageMessageItem item = Language.GetMessage("SelectOverlap", row["CODENAME"].ToString());
                    result.IsSucced = false;
                    result.FailMessage = item.Message;
                    result.Caption = item.Title;
                }
                currentRow["CODEID"] = row["CODEID"];
                currentRow["CODENAME"] = row["CODENAME"];
            }
            return result;
        }


        private void ProcSave(string strtitle)
        {

            DataTable changed = null;
            DataTable changed2 = null;

            switch (tabChemicalWater.SelectedTabPageIndex)
            {
                case 0:
                    //약품분석 탭
                    grdInspectionitemrelList.View.FocusedRowHandle = grdInspectionitemrelList.View.FocusedRowHandle;
                    grdInspectionitemrelList.View.FocusedColumn = grdInspectionitemrelList.View.Columns["INSPITEMNAME"];
                    grdInspectionitemrelList.View.ShowEditor();
                    grdInspectionitemrelList.View.CheckValidation();
                    changed = grdInspectionitemrelList.GetChangedRows();
                    CheckChanged(changed);
                    if (changed.Rows.Count == 0)
                    {
                        // 저장할 데이터가 존재하지 않습니다.
                        this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                        return;
                    }
                    break;
                case 1:
                    //계산식 탭
                    grdFormulaDef.View.FocusedRowHandle = grdFormulaDef.View.FocusedRowHandle;
                    grdFormulaDef.View.FocusedColumn = grdFormulaDef.View.Columns["RULEID"];
                    grdFormulaDef.View.ShowEditor();
                   

                    grdFormula.View.FocusedRowHandle = grdFormula.View.FocusedRowHandle;
                    grdFormula.View.FocusedColumn = grdFormula.View.Columns["SEQUENCE"];
                    grdFormula.View.ShowEditor();
                    // TODO : 유효성 로직 변경
                   
                    grdFormulaDef.View.CheckValidation();
                    grdFormula.View.CheckValidation();
                    changed = grdFormulaDef.GetChangedRows();
                    changed2 = grdFormula.GetChangedRows();
                    if (changed.Rows.Count == 0 && changed2.Rows.Count == 0)
                    {
                        // 저장할 데이터가 존재하지 않습니다.
                        this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                        return;
                    }
                    break;
            }

           
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    switch (tabChemicalWater.SelectedTabPageIndex)
                    {

                        case 0:
                            //약품분석 탭
                            changed = grdInspectionitemrelList.GetChangedRows();
                            ExecuteRule("Inspectionitemrel", changed);
                            ShowMessage("SuccessOspProcess");
                            break;
                        case 1:
                            //계산식 탭
                            changed = grdFormulaDef.GetChangedRows();
                            changed2 = grdFormula.GetChangedRows();

                            DataSet rullSet = new DataSet();

                            changed.TableName = "formulaDefList";
                            changed2.TableName = "formulaList";

                            if (changed.Rows.Count > 0)
                            {
                                rullSet.Tables.Add(changed);
                            }

                            if (changed2.Rows.Count > 0)
                            {
                                rullSet.Tables.Add(changed2);
                            }

                            ExecuteRule("SaveChemicalAnalysisMasterInfoManagementFormula", rullSet);
                            ShowMessage("SuccessOspProcess");
                            break;
                    }
                   
                    //재조회 
                    OnSaveConfrimSearch();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();


                }
            }
        }
        private void OnSaveConfrimSearch()
        {

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            strInspectiondefid = values["INSPECTIONDEFID"].ToString();
            DataTable dt = null;
            string strtempvalue = "";
            int irow = 0;
            switch (tabChemicalWater.SelectedTabPageIndex)
            {
                case 0:
                    DataRow row = grdEQUIPMENTSub.View.GetFocusedDataRow();
                    if (row == null)
                    {
                        return;
                    }
                    strtempvalue = grdInspectionitemrelList.View.GetFocusedRowCellValue("INSPITEMID").ToString();
                    Dictionary<string, object> Param = new Dictionary<string, object>();
                    Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    Param.Add("INSPECTIONDEFID", values["INSPECTIONDEFID"].ToString());
                    Param.Add("RESOURCEID", row["EQUIPMENTID"].ToString());
                    Param.Add("RESOURCEVERSION", "*");
                    Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable dtInspectionitemrelList = SqlExecuter.Query("GetQcEquipmentList", "10001", Param);
                    grdInspectionitemrelList.DataSource = dtInspectionitemrelList;

                    irow = checkGridProcess(strtempvalue, grdInspectionitemrelList, "INSPITEMID");
                    if (irow > -1)
                    {
                        grdInspectionitemrelList.View.ClearSelection();
                        grdInspectionitemrelList.View.FocusedRowHandle = irow;

                        grdInspectionitemrelList.View.SelectRow(irow);
                        grdInspectionitemrelList.View.FocusedColumn = grdInspectionitemrelList.View.Columns["INSPITENAME"];
                       
                    }
                    break;


                case 1:
                    //계산식 탭
                    strtempvalue = grdFormulaDef.View.GetFocusedRowCellValue("RULEID").ToString();
                    values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                    dt = SqlExecuter.Query("SelectRule", "10001", values);
                    CheckHasData(dt);
                    grdFormulaDef.View.ClearDatas();
                    grdFormulaDef.DataSource = dt;
                     irow = checkGridProcess(strtempvalue, grdFormulaDef, "RULEID");
                    if (irow > -1)
                    {
                        grdFormulaDef.View.ClearSelection();
                        grdFormulaDef.View.FocusedRowHandle = irow;

                        grdFormulaDef.View.SelectRow(irow);
                        grdFormulaDef.View.FocusedColumn = grdFormulaDef.View.Columns["RULEID"];

                    }
                    break;
            }

        }
        /// <summary>
        /// 해당하는 값 그리드에서 찾기
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="targetGrid"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private int checkGridProcess(string itemID, SmartBandedGrid targetGrid, string columnName)
        {

            for (int i = 0; i < targetGrid.View.DataRowCount; i++)
            {
                if (targetGrid.View.GetRowCellValue(i, columnName).ToString().Equals(itemID))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion
    }
}
