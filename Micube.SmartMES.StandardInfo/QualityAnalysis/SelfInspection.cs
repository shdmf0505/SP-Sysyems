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
    public partial class SelfInspection : SmartConditionManualBaseForm
    {
        #region Local Variables
      
        #endregion

        #region 생성자

        public SelfInspection()
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
            grdSelfInspection.GridButtonItem =GridButtonItem.Export;
            grdSelfInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdSelfInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("INSPITEMID", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("INSPITEMVERSION", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("RESOURCETYPE", 80).SetIsHidden();

            //InitializeGrid_ProcesssegPopup();
            grdSelfInspection.View.AddTextBoxColumn("PROCESSSEGID", 100);
            grdSelfInspection.View.AddTextBoxColumn("PROCESSSEGNAME", 250);

            grdSelfInspection.View.AddTextBoxColumn("PROCESSEGVERSION", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("PLANTID", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("VALIDTYPE", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("UNIT", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("SEQUENCE", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("TANKSIZE", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("CALCULATIONTYPE", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("ANALYSISCONST", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("FOMULATYPE", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("QTYCONST", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("SPECCLASSID", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("SPECSEQUENCE", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("QTYUNIT", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("DESCRIPTION", 80).SetIsHidden();


            grdSelfInspection.View.AddTextBoxColumn("ITEMVERSION", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("ITEMNAME", 250).SetIsHidden();


            grdSelfInspection.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdSelfInspection.View.AddComboBoxColumn("INSPECTORDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionGrade", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdSelfInspection.View.AddComboBoxColumn("ISAQL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("AQLINSPECTIONLEVEL", 100).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("AQLDEFECTLEVEL", 100).SetIsHidden();
            grdSelfInspection.View.AddComboBoxColumn("AQLDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("AQLCYCLE", 100).SetIsHidden();
            grdSelfInspection.View.AddComboBoxColumn("ISNCR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdSelfInspection.View.AddSpinEditColumn("NCRINSPECTIONQTY", 100);
            grdSelfInspection.View.AddComboBoxColumn("NCRCYCLE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspCycle", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("INSPECTIONCYCLE");
            grdSelfInspection.View.AddTextBoxColumn("NCRDECISIONDEGREE", 100).SetIsHidden();
            grdSelfInspection.View.AddSpinEditColumn("NCRLOTSIZE", 100);
            //grdSelfInspection.View.AddComboBoxColumn("INSPECTIONUNIT", 80, new SqlQuery("GetUomDefinitionList", "10001", $"UOMCLASSID=MMQC"), "UOMDEFNAME", "UOMDEFID")
            //    .SetDefault("PCS")
            //    .SetTextAlignment(TextAlignment.Center);

            grdSelfInspection.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();

            grdSelfInspection.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfInspection.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfInspection.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfInspection.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfInspection.View.PopulateColumns();
            

            
        }

        private void InitializeGrid_ProcesssegPopup()
        {

            var parentProduct = this.grdSelfInspection.View.AddSelectPopupColumn("PROCESSSEGID", new SqlQuery("GetSelfInspectionPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
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
            grdSelfInspection.View.AddingNewRow += grdOutBaseInfo_AddingNewRow;

            btnPoint.Click += BtnPoint_Click;
            
        }

     

        private void BtnPoint_Click(object sender, EventArgs e)
        {
            if (grdSelfInspection.View.RowCount > 0)
            {
                DataRow row = grdSelfInspection.View.GetFocusedDataRow();

                object[] obj = new object[11];
                obj[0] = "";
                obj[1] = row["INSPITEMID"];
                obj[2] = row["INSPITEMVERSION"];
                obj[3] = row["INSPECTIONDEFID"];
                obj[4] = row["INSPECTIONDEFVERSION"];
                obj[5] = row["PROCESSSEGID"];
                obj[6] = row["RESOURCETYPE"];
                obj[7] = row["RESOURCEVERSION"];
                obj[8] = "Valid";
                obj[9] = row["ENTERPRISEID"];
                obj[10] = row["PLANTID"];

                OutBaseInfoPopup outbase = new OutBaseInfoPopup(obj);
                outbase.ShowDialog();
            }

        }

        private void grdOutBaseInfo_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //DataRow rowOutBaseInfo = grdOutBaseInfo.View.GetFocusedDataRow();

            var value = Conditions.GetValues();

            args.NewRow["INSPECTIONDEFID"] = value["INSPECTIONDEFID"];
            args.NewRow["INSPECTIONDEFVERSION"] = "*";
            args.NewRow["INSPITEMID"] = "*";
            args.NewRow["INSPITEMVERSION"] = "*";
            args.NewRow["RESOURCETYPE"] = "*";

            args.NewRow["RESOURCEID"] = "*";
            args.NewRow["RESOURCEVERSION"] = "*";
            args.NewRow["INSPECTIONUNIT"] = "PCS";

            //args.NewRow["PROCESSSEGMENTTYPE"] = "*";
            //args.NewRow["PROCESSSEGID"] = "*";
            args.NewRow["PROCESSEGVERSION"] = "*";

            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;

            args.NewRow["VALIDSTATE"] = "Valid";


        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                grdSelfInspection.View.FocusedRowHandle = grdSelfInspection.View.FocusedRowHandle;
                grdSelfInspection.View.FocusedColumn = grdSelfInspection.View.Columns["PROCESSSEGNAME"];
                grdSelfInspection.View.ShowEditor();
               // base.OnToolbarSaveClick();
                DataTable dtchange = grdSelfInspection.GetChangedRows();
                //dtchange.Columns.Add("RESOURCEID");
                //dtchange.Columns.Add("RESOURCEVERSION");

                foreach (DataRow row in dtchange.Rows)
                {
                    //row["RESOURCEID"] = row["ITEMID"];
                    //row["RESOURCEVERSION"] = row["ITEMVERSION"];

                    if (row["ISAQL"].ToString() != "Y")
                    {
                        row["ISAQL"] = "";
                    }

                    if (row["ISNCR"].ToString() != "Y")
                    {
                        row["ISNCR"] = "";
                    }

                }
                ExecuteRule("SaveInspectionitemrel", dtchange);
                //재조회 
                OnSaveConfrimSearch();
            }
            if (btn.Name.ToString().Equals("Inspectionpoint"))
            {
                if (grdSelfInspection.View.RowCount > 0)
                {
                    DataRow row = grdSelfInspection.View.GetFocusedDataRow();

                    object[] obj = new object[11];
                    obj[0] = "";
                    obj[1] = row["INSPITEMID"];
                    obj[2] = row["INSPITEMVERSION"];
                    obj[3] = row["INSPECTIONDEFID"];
                    obj[4] = row["INSPECTIONDEFVERSION"];
                    obj[5] = row["PROCESSSEGID"];
                    obj[6] = row["RESOURCETYPE"];
                    obj[7] = row["RESOURCEVERSION"];
                    obj[8] = "Valid";
                    obj[9] = row["ENTERPRISEID"];
                    obj[10] = row["PLANTID"];

                    OutBaseInfoPopup outbase = new OutBaseInfoPopup(obj);
                    outbase.ShowDialog();
                }
            }
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            
            // 검사정의
            Conditions.AddComboBox("INSPECTIONDEFID", new SqlQuery("GetQcInspectionDefinitionCombo", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"INSPECTIONCLASSID={"SelfInspection"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID").SetValidationIsRequired();


        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartComboBox combobox = Conditions.GetControl<SmartComboBox>("INSPECTIONDEFID");
            combobox.ItemIndex = 1;

        }
     
        #endregion

        #region 조회조건 영역 초기화
        /// <summary>
        /// 팝업형 조회조건 생성 - 품목 코드
        /// </summary>
      




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

                value.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                //value.Add("P_PLANTID", UserInfo.Current.Plant);
                value.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                grdSelfInspection.DataSource = await SqlExecuter.QueryAsync("GetSelfInspection", "10001", value);
              
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
            base.OnValidateContent();

            grdSelfInspection.View.CheckValidation();

            if (grdSelfInspection.GetChangedRows().Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
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

        /// <summary>
        /// 저장 후 재조회용 
        /// </summary>

        private void OnSaveConfrimSearch()
        {

            var value = Conditions.GetValues();

            value.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //value.Add("P_PLANTID", UserInfo.Current.Plant);
            value.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdSelfInspection.DataSource =  SqlExecuter.Query("GetSelfInspection", "10001", value);
            
        }
        #endregion
    }
}
