#region using

using Micube.Framework.Net;
using Micube.Framework;
using Micube.Framework.SmartControls;
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

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준 정보 > 품질기준정보 > 설비불량코드 연계
    /// 업  무  설  명  : 설비에서 넘어오는 Defect Code를 관리 한다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-23
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class QcOperationInspection : SmartConditionManualBaseForm
    {
        #region Local Variables
        
        #endregion

        #region 생성자

        public QcOperationInspection()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            //grdOutBaseInfo.LanguageKey = "GRIDDEFECTCODELIST";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {

            grdOperationInspectionItem.GridButtonItem = GridButtonItem.Export;
            grdOperationInspectionItem.View.SetIsReadOnly();
            grdOperationInspectionItem.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdOperationInspectionItem.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            grdOperationInspectionItem.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            grdOperationInspectionItem.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            grdOperationInspectionItem.View.AddTextBoxColumn("PLANTID", 80).SetIsHidden();
            grdOperationInspectionItem.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdOperationInspectionItem.View.PopulateColumns();



            grdQcOperationInspection.GridButtonItem = GridButtonItem.Export;
            
            grdQcOperationInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdQcOperationInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("INSPITEMID", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("INSPITEMVERSION", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("RESOURCETYPE", 80).SetIsHidden();

            grdQcOperationInspection.View.AddTextBoxColumn("PROCESSSEGMENTTYPE", 80).SetIsHidden();

            grdQcOperationInspection.View.AddTextBoxColumn("PROCESSEGVERSION", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("PLANTID", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("INSPITEMCLASSID", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("VALIDTYPE", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("UNIT", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("SEQUENCE", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("TANKSIZE", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("CALCULATIONTYPE", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("ANALYSISCONST", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("FOMULATYPE", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("QTYCONST", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("SPECCLASSID", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("SPECSEQUENCE", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("QTYUNIT", 80).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("DESCRIPTION", 80).SetIsHidden();

            grdQcOperationInspection.View.AddComboBoxColumn("ISAQL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("AQLINSPECTIONLEVEL", 100).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("AQLDEFECTLEVEL", 100).SetIsHidden();
            grdQcOperationInspection.View.AddComboBoxColumn("AQLDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdQcOperationInspection.View.AddTextBoxColumn("AQLCYCLE", 100).SetIsHidden();
            grdQcOperationInspection.View.AddSpinEditColumn("NCRINSPECTIONQTY", 100).SetIsHidden();

            grdQcOperationInspection.View.AddTextBoxColumn("ITEMID", 100).SetIsReadOnly();
            grdQcOperationInspection.View.AddTextBoxColumn("ITEMVERSION", 80).SetIsReadOnly();
            grdQcOperationInspection.View.AddTextBoxColumn("ITEMNAME", 250).SetIsReadOnly();
            grdQcOperationInspection.View.AddTextBoxColumn("PROCESSSEGID", 100).SetIsReadOnly()
                .SetLabel("PROCESSSEGMENTID");
            grdQcOperationInspection.View.AddTextBoxColumn("PROCESSSEGNAME", 250).SetIsReadOnly()
                .SetLabel("PROCESSSEGMENTNAME"); 
            grdQcOperationInspection.View.AddTextBoxColumn("INSPITEMNAME", 200).SetIsReadOnly();
            grdQcOperationInspection.View.AddTextBoxColumn("ISSPEC", 80).SetIsReadOnly();
            grdQcOperationInspection.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();
            grdQcOperationInspection.View.AddComboBoxColumn("INSPECTORDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionGrade", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();

            grdQcOperationInspection.View.AddComboBoxColumn("NCRCYCLE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspCycle", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("INSPECTIONCYCLE");
            grdQcOperationInspection.View.AddComboBoxColumn("ISNCR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
 
            grdQcOperationInspection.View.AddComboBoxColumn("NCRDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdQcOperationInspection.View.AddSpinEditColumn("NCRLOTSIZE", 100);
            //grdQcOperationInspection.View.AddComboBoxColumn("INSPECTIONUNIT", 80, new SqlQuery("GetUomDefinitionList", "10001", $"UOMCLASSID=MMQC"), "UOMDEFNAME", "UOMDEFID")
            //   .SetDefault("PCS")
            //   .SetTextAlignment(TextAlignment.Center);

            grdQcOperationInspection.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetIsReadOnly()
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdQcOperationInspection.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdQcOperationInspection.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdQcOperationInspection.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdQcOperationInspection.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdQcOperationInspection.View.AddTextBoxColumn("INSPITEMTYPE", 80).SetIsHidden();
            grdQcOperationInspection.View.PopulateColumns();


            // 외주검사 그리드 
            grdOutSelfInspection.GridButtonItem =  GridButtonItem.Export;
            grdOutSelfInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdOutSelfInspection.View.AddTextBoxColumn("PROCESSSEGID", 100).SetIsReadOnly();
            grdOutSelfInspection.View.AddTextBoxColumn("PROCESSSEGNAME", 250).SetIsReadOnly();
            
            grdOutSelfInspection.View.AddComboBoxColumn("ISAQL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdOutSelfInspection.View.AddComboBoxColumn("AQLINSPECTIONLEVEL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AQLSize", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdOutSelfInspection.View.AddComboBoxColumn("AQLDEFECTLEVEL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AQLRate", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdOutSelfInspection.View.AddComboBoxColumn("AQLDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdOutSelfInspection.View.AddComboBoxColumn("ISNCR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdOutSelfInspection.View.AddSpinEditColumn("NCRINSPECTIONQTY", 100);
            grdOutSelfInspection.View.AddComboBoxColumn("NCRCYCLE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspCycle", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("INSPECTIONCYCLE");
            grdOutSelfInspection.View.AddSpinEditColumn("NCRLOTSIZE", 100);
            //grdOutSelfInspection.View.AddComboBoxColumn("INSPECTIONUNIT", 80, new SqlQuery("GetUomDefinitionList", "10001", $"UOMCLASSID=MMQC"), "UOMDEFNAME", "UOMDEFID")
            // .SetDefault("PCS")
            // .SetTextAlignment(TextAlignment.Center);

            grdOutSelfInspection.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                //.SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();

            grdOutSelfInspection.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOutSelfInspection.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOutSelfInspection.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOutSelfInspection.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdOutSelfInspection.View.SetKeyColumn("PROCESSSEGID");
            grdOutSelfInspection.View.PopulateColumns();

        }

        private void InitializeGrid_ProcesssegPopup()
        {

            var parentProduct = this.grdOutSelfInspection.View.AddSelectPopupColumn("PROCESSSEGID", new SqlQuery("GetOutSelfInspectionPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("PROCESSSEGID", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
            .SetPopupResultMapping("PROCESSSEGID", "QCSEGMENTID")
           // // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정
            .SetValidationIsRequired()
            .SetPopupValidationCustom(ValidationProductPopup);

            // 팝업에서 사용할 조회조건 항목 추가

            parentProduct.Conditions.AddTextBox("INSPECTIONDEFID")
            .SetPopupDefaultByGridColumnId("INSPECTIONDEFID")
            .SetIsHidden();

            

            parentProduct.Conditions.AddTextBox("QCSEGMENTID");
            parentProduct.Conditions.AddTextBox("QCSEGMENTNAME");
          
            // 팝업 그리드 설정
            parentProduct.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150);
            parentProduct.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 80);
        }

        #endregion

        #region Event

        /// <summary>        
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {

            smartTabControl1.SelectedPageChanged += SmartTabControl1_SelectedPageChanged;

           // grdOutSelfInspection.View.AddingNewRow += grdOutBaseInfo_AddingNewRow;

            grdOperationInspectionItem.View.FocusedRowChanged += grdOperationInspectionItem_FocusedRowChanged;
  



        }



        private void SmartTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page== xtraTabPage1)
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Standard"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Standard"].Visible = true;
                   

                }
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Standard"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Standard"].Visible = false;
                   

                }
              
            }

        }

        private void grdOperationInspectionItem_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdOperationInspectionItem.View.GetFocusedDataRow();
            if(row == null)
            {
                return;
            }
           
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            Param.Add("PLANTID", row["PLANTID"].ToString());
            Param.Add("PRODUCTDEFID", row["PRODUCTDEFID"].ToString());
            Param.Add("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"].ToString());
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            Param.Add("INSPECTIONCLASSID", "OperationInspection");


            DataTable dt = SqlExecuter.Query("GetQcOperationInspection", "10001", Param);
            grdQcOperationInspection.DataSource = dt;
        }

     

        private void grdOutBaseInfo_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //DataRow rowOutBaseInfo = grdOutBaseInfo.View.GetFocusedDataRow();

            var value = Conditions.GetValues();

            args.NewRow["INSPECTIONDEFID"] = "OSPInspection";
            args.NewRow["INSPECTIONDEFVERSION"] = "*";
            args.NewRow["INSPITEMID"] = "*";
            args.NewRow["INSPITEMVERSION"] = "*";
            args.NewRow["RESOURCETYPE"] = "*";

            args.NewRow["RESOURCEID"] = "*";
            args.NewRow["RESOURCEVERSION"] = "*";
            args.NewRow["INSPECTIONUNIT"] = "PCS";

            //args.NewRow["PROCESSSEGMENTTYPE"] = "QCSegmentID";
            //args.NewRow["PROCESSSEGID"] = "*";
            args.NewRow["PROCESSEGVERSION"] = "*";

            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;

            args.NewRow["VALIDSTATE"] = "Valid";


        }

        #endregion

        #region 툴바
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Standard"))
            {
                if (smartTabControl1.SelectedTabPageIndex == 0)
                {
                    if (grdQcOperationInspection.View.DataRowCount == 0) return;
                    DataRow chkData = grdQcOperationInspection.View.GetFocusedDataRow();
                    
                    int irow = grdQcOperationInspection.View.FocusedRowHandle;
                    if (!(chkData["INSPITEMTYPE"].ToString().Equals("SPC")))
                    {
                        this.ShowMessage(MessageBoxButtons.OK, "InValidStdData002", chkData["INSPITEMTYPE"].ToString()); //메세지 
                        return;
                    }
                    SpecDetailPopUp popup = new SpecDetailPopUp();
                    if (chkData["SPECCLASSID"].ToString().Equals(""))
                    {
                        popup._specSequence = "";
                        popup._specClassId = "OperationSpec";
                    }
                    else
                    {
                        popup._specSequence = chkData["SPECSEQUENCE"].ToString();
                        popup._specClassId = chkData["SPECCLASSID"].ToString();

                    }
                    
                    popup.buttonType = false;

                    // popup._inspectionName = _inspectionName;
                    popup.Owner = this;
                    popup.ShowDialog();
                }
                else
                {
                    return;
                }


            }
            else if (btn.Name.ToString().Equals("Inspectionpoint"))
            {
                if (smartTabControl1.SelectedTabPageIndex == 1)
                {
                    DataRow row = grdOutSelfInspection.View.GetFocusedDataRow();

                    object[] obj = new object[11];
                    obj[0] = "";
                    obj[1] = row["INSPITEMID"];
                    obj[2] = row["INSPITEMVERSION"];
                    obj[3] = row["INSPECTIONDEFID"];
                    obj[4] = row["INSPECTIONDEFVERSION"];
                    obj[5] = row["RESOURCEID"];
                    obj[6] = row["RESOURCETYPE"];
                    obj[7] = row["RESOURCEVERSION"];
                    obj[8] = "Valid";
                    obj[9] = row["ENTERPRISEID"];
                    obj[10] = row["PLANTID"];

                    OutBaseInfoPopup outbase = new OutBaseInfoPopup(obj);
                    outbase.ShowDialog();
                }
                else
                {
                    return;
                }

            }
            else if (btn.Name.ToString().Equals("Save"))
            {

                ProcSave(btn.Text);
            }

        }



        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //DataTable dtQcOperationInspection = grdQcOperationInspection.GetChangedRows();

            //if (dtQcOperationInspection != null)
            //{
            //    if(dtQcOperationInspection.Rows.Count != 0)
            //    {
            //        dtQcOperationInspection.Columns.Add("RESOURCEID");
            //        dtQcOperationInspection.Columns.Add("RESOURCEVERSION");

            //        foreach (DataRow row in dtQcOperationInspection.Rows)
            //        {
            //            row["RESOURCEID"] = row["ITEMID"];
            //            row["RESOURCEVERSION"] = row["ITEMVERSION"];

            //        }

            //        ExecuteRule("Inspectionitemrel", dtQcOperationInspection);
            //    }
            //}

            //DataTable dtOutSelfInspection = grdOutSelfInspection.GetChangedRows();

            //if (dtOutSelfInspection != null)
            //{
            //    if(dtOutSelfInspection.Rows.Count !=0)
            //    {
            //        ExecuteRule("Inspectionitemrel", dtOutSelfInspection);
            //    }
            //}

        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            InitializeConditionProductDefinitionId_Popup();
            // 검사정의
            //Conditions.AddComboBox("INSPECTIONDEFID", new SqlQuery("GetQcInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"INSPECTIONDEFID={"OperationInspection"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID").SetValidationIsRequired();

        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            Conditions.GetControl<SmartTextBox>("P_ITEMNAME").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").ReadOnly = true;
            Conditions.GetControl<SmartSelectPopupEdit>("ITEMID").EditValueChanged += ProductDefIDChanged;

            //SmartComboBox combobox = Conditions.GetControl<SmartComboBox>("INSPECTIONDEFID");
            //combobox.ItemIndex = 0;

        }
        private void ProductDefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue = string.Empty;
                Conditions.GetControl<SmartTextBox>("P_ITEMNAME").EditValue = string.Empty;
            }
        }



        #endregion

        #region 조회조건 영역 초기화
        /// <summary>
        /// 팝업형 조회조건 생성 - 품목 코드
        /// </summary>
        /// 

        private void InitializeConditionProductDefinitionId_Popup()
        {
           
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("ITEMID", new SqlQuery("GetProductdefidlistByStd", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMID", "ITEMID")
               .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               .SetPosition(0)
               .SetPopupApplySelection((selectRow, gridRow) => {


                   List<string> productDefnameList = new List<string>();
                   List<string> productRevisionList = new List<string>();

                   selectRow.AsEnumerable().ForEach(r => {
                       productDefnameList.Add(Format.GetString(r["ITEMNAME"]));
                       productRevisionList.Add(Format.GetString(r["ITEMVERSION"]));
                   });

                   Conditions.GetControl<SmartTextBox>("P_ITEMNAME").EditValue = string.Join(",", productDefnameList);
                   Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue = string.Join(",", productRevisionList);
               });
            parentPopupColumn.Conditions.AddComboBox("MASTERDATACLASSNAME", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("MASTERDATACLASSNAME")
               .SetDefault("Product");
            parentPopupColumn.Conditions.AddTextBox("ITEMID");
            parentPopupColumn.Conditions.AddTextBox("ITEMNAME");
            parentPopupColumn.Conditions.AddTextBox("ITEMVERSION");
            //팝업 그리드
            parentPopupColumn.GridColumns.AddComboBoxColumn("MASTERDATACLASSNAME", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();

            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            ;
        }



        #endregion


        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                await base.OnSearchAsync();
                var value = Conditions.GetValues();
                Dictionary<string, object> Param = new Dictionary<string, object>();
                value.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                value.Add("PLANTID", UserInfo.Current.Plant);
                value.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                switch (smartTabControl1.SelectedTabPageIndex)
                {
                    case 0:

                        DataTable dtqcClass = await SqlExecuter.QueryAsync("GetQcOperationInspectionItem", "10001", value);

                        if (dtqcClass.Rows.Count < 1)
                        {
                            ShowMessage("NoSelectData");
                            grdQcOperationInspection.View.ClearDatas();
                        }

                        grdOperationInspectionItem.DataSource = dtqcClass;
                        break;
                    case 1:
                        grdOutSelfInspection.View.ClearDatas();
                        value.Add("INSPECTIONDEFID", "OSPInspection");
                        DataTable dtOutSelt = await SqlExecuter.QueryAsync("GetQcOSPInspection", "10001", value);
                        grdOutSelfInspection.DataSource = dtOutSelt;
                        break;
                }

            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

       


        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            //base.OnValidateContent();

            //grdQcOperationInspection.View.CheckValidation();
            //grdOutSelfInspection.View.CheckValidation();
            //if (grdQcOperationInspection.GetChangedRows().Rows.Count == 0 && grdOutSelfInspection.GetChangedRows().Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function
        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 품목)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationProductPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
      
                currentGridRow["PROCESSSEGNAME"] = row["QCSEGMENTNAME"];

            }
            return result;
        }
        #endregion

       
        private void ProcSave(string strtitle)
        {
            DataTable changed = null;
            switch (smartTabControl1.SelectedTabPageIndex)
            {
                case 0:

                    grdQcOperationInspection.View.FocusedRowHandle = grdQcOperationInspection.View.FocusedRowHandle;
                    grdQcOperationInspection.View.FocusedColumn = grdQcOperationInspection.View.Columns["ITEMNAME"];
                    grdQcOperationInspection.View.ShowEditor();
                    grdQcOperationInspection.View.CheckValidation();

                    changed = grdQcOperationInspection.GetChangedRows() ;
                    break;
                case 1:
                    grdOutSelfInspection.View.FocusedRowHandle = grdOutSelfInspection.View.FocusedRowHandle;
                    grdOutSelfInspection.View.FocusedColumn = grdOutSelfInspection.View.Columns["PROCESSSEGNAME"];
                    grdOutSelfInspection.View.ShowEditor();
                    grdOutSelfInspection.View.CheckValidation();
                    changed = grdOutSelfInspection.GetChangedRows();
                    break;
            }
        

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }
          
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    
                    switch (smartTabControl1.SelectedTabPageIndex)
                    {
                        case 0:

                            DataTable dtQcOperationInspection = grdQcOperationInspection.GetChangedRows();

                            if (dtQcOperationInspection != null)
                            {
                                if (dtQcOperationInspection.Rows.Count != 0)
                                {
                                    dtQcOperationInspection.Columns.Add("RESOURCEID");
                                    dtQcOperationInspection.Columns.Add("RESOURCEVERSION");

                                    foreach (DataRow row in dtQcOperationInspection.Rows)
                                    {
                                        row["RESOURCEID"] = row["ITEMID"];
                                        row["RESOURCEVERSION"] = row["ITEMVERSION"];

                                    }

                                    ExecuteRule("SaveInspectionitemrel", dtQcOperationInspection);
                                }
                            }
                            break;
                        case 1:
                            DataTable dtOutSelfInspection = grdOutSelfInspection.GetChangedRows();

                            if (dtOutSelfInspection != null)
                            {
                                if (dtOutSelfInspection.Rows.Count != 0)
                                {
                                    
                                    foreach (DataRow row in dtOutSelfInspection.Rows)
                                    {
                                        row["INSPECTIONDEFID"] = "OSPInspection";
                                        row["PLANTID"] = "*";
                                        row["RESOURCEID"] = "*";
                                        row["RESOURCEVERSION"] = "*";
                                        row["INSPITEMID"] = "*";
                                        row["RESOURCETYPE"] = "QCSegmentID";
                                        

                                    }

                                    ExecuteRule("SaveInspectionitemrel", dtOutSelfInspection);
                                }
                            }
                            break;
                    }
                    ShowMessage("SuccessOspProcess");
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

        /// <summary>
        /// 
        /// </summary>
        private void OnSaveConfrimSearch()
        {
            string strtempvalue = "";
            int irow = 0;
            var value = Conditions.GetValues();
           
            switch (smartTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    strtempvalue = grdQcOperationInspection.View.GetFocusedRowCellValue("INSPITEMID").ToString();
                    DataRow row = grdOperationInspectionItem.View.GetFocusedDataRow();
                    if (row == null)
                    {
                        return;
                    }

                    Dictionary<string, object> Param = new Dictionary<string, object>();
                    Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                    Param.Add("PLANTID", row["PLANTID"].ToString());
                    Param.Add("PRODUCTDEFID", row["PRODUCTDEFID"].ToString());
                    Param.Add("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"].ToString());
                    Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    Param.Add("INSPECTIONCLASSID", "OperationInspection");


                    DataTable dt = SqlExecuter.Query("GetQcOperationInspection", "10001", Param);
                    grdQcOperationInspection.DataSource = dt;
                    irow = checkGridProcess(strtempvalue, grdQcOperationInspection, "INSPITEMID");
                    if (irow > -1)
                    {
                        grdQcOperationInspection.View.ClearSelection();
                        grdQcOperationInspection.View.FocusedRowHandle = irow;

                        grdQcOperationInspection.View.SelectRow(irow);
                        grdQcOperationInspection.View.FocusedColumn = grdQcOperationInspection.View.Columns["INSPITEMNAME"];

                    }
                    break;
                case 1:
                    strtempvalue = grdOutSelfInspection.View.GetFocusedRowCellValue("PROCESSSEGID").ToString();
                    value.Add("INSPECTIONDEFID", "OSPInspection");
                    value.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    DataTable dtOutSelt =  SqlExecuter.Query("GetQcOSPInspection", "10001", value);
                    grdOutSelfInspection.DataSource = dtOutSelt;
                   
                    irow = checkGridProcess(strtempvalue, grdOutSelfInspection, "PROCESSSEGID");
                    if (irow > -1)
                    {
                        grdOutSelfInspection.View.ClearSelection();
                        grdOutSelfInspection.View.FocusedRowHandle = irow;

                        grdOutSelfInspection.View.SelectRow(irow);
                        grdOutSelfInspection.View.FocusedColumn = grdOutSelfInspection.View.Columns["PROCESSSEGNAME"];

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
    }
}
