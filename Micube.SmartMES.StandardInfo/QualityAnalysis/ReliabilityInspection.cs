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
    public partial class ReliabilityInspection : SmartConditionManualBaseForm
    {
        #region Local Variables
        string sItemName = "";
        #endregion

        #region 생성자

        public ReliabilityInspection()
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
            grdSelfInspection.GridButtonItem = GridButtonItem.CRUD;
            grdSelfInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdSelfInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 80).SetIsHidden();
            
            grdSelfInspection.View.AddTextBoxColumn("INSPITEMVERSION", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("RESOURCETYPE", 80).SetIsHidden();

            //grdSelfInspection.View.AddTextBoxColumn("PROCESSSEGMENTTYPE", 80).SetIsHidden();
           
            grdSelfInspection.View.AddTextBoxColumn("PROCESSSEGID", 250).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("PROCESSSEGNAME", 250).SetIsHidden();

            grdSelfInspection.View.AddTextBoxColumn("PROCESSEGVERSION", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("PLANTID", 80).SetIsHidden();
            //grdSelfInspection.View.AddTextBoxColumn("INSPITEMCLASSID", 80).SetIsHidden();
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

            
            grdSelfInspection.View.AddTextBoxColumn("INSPITEMID", 100).SetIsHidden();
            //grdSelfInspection.View.AddTextBoxColumn("PLANTID", 80);
            grdSelfInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAMEPATH", 200);
            //grdSelfInspection.View.AddTextBoxColumn("MIDINSPCLASSNAME", 200);
            grdSelfInspection.View.AddTextBoxColumn("INSPITEMNAME", 200);
            grdSelfInspection.View.AddComboBoxColumn("ISAQL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("AQLINSPECTIONLEVEL", 100)
                .SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("AQLDEFECTLEVEL", 100)
                .SetIsHidden();
            grdSelfInspection.View.AddComboBoxColumn("AQLDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("AQLCYCLE", 100).SetIsHidden();
            grdSelfInspection.View.AddComboBoxColumn("ISNCR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden();
            grdSelfInspection.View.AddSpinEditColumn("NCRINSPECTIONQTY", 100)
                .SetIsHidden();
            grdSelfInspection.View.AddTextBoxColumn("NCRCYCLE", 100)
                .SetIsHidden();
            grdSelfInspection.View.AddSpinEditColumn("NCRLOTSIZE", 100)
                .SetIsHidden();
            grdSelfInspection.View.AddComboBoxColumn("INSPECTIONUNIT", 80, new SqlQuery("GetUomDefinitionList", "10001", $"UOMCLASSID=MMQC"), "UOMDEFNAME", "UOMDEFID")
               .SetDefault("PCS")
               .SetTextAlignment(TextAlignment.Center);
         //  grdSelfInspection.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));2019.11.29 삭제 처리 박승건이사
            grdSelfInspection.View.AddComboBoxColumn("NCRDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdSelfInspection.View.AddComboBoxColumn("INSPECTORDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionGrade", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden();

            grdSelfInspection.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            

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

       

        #endregion
               
        #region Event

        /// <summary>        
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //grdSelfInspection.View.AddingNewRow += grdOutBaseInfo_AddingNewRow;
        }

        

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable dtchange = grdSelfInspection.GetChangedRows();
            ExecuteRule("Inspectionitemrel", dtchange);
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 검사정의
            //Conditions.AddComboBox("INSPECTIONDEFID", new SqlQuery("GetQcInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"INSPECTIONCLASSID={"SelfInspection"}"));


        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

           

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
                value.Add("P_PLANTID", UserInfo.Current.Plant);
                value.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                grdSelfInspection.DataSource = await SqlExecuter.QueryAsync("GetReliabilityInspection", "10001", value);
              
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
        #endregion
    }
}
