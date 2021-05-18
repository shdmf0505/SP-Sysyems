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
    public partial class QcConsumable : SmartConditionManualBaseForm
    {
        #region Local Variables
        string sItemName = "";
        #endregion

        #region 생성자

        public QcConsumable()
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
            InitializeGridQcRawInspectionList();
            //
            InitializeGridRawInspectionMaster();
            InitializeGridRawInspectionList();
            InitializeGridQcSubassemblyInspection();
            InitializeGridSubassemblyMaster();
            InitializeGridSubassemblyInspection();
        }
        private void InitializeGridQcRawInspectionList() //수입(원자재)
        {
            grdQcRawInspectionList.GridButtonItem = GridButtonItem.Export;
            grdQcRawInspectionList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdQcRawInspectionList.View.SetIsReadOnly();
            grdQcRawInspectionList.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
            grdQcRawInspectionList.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            grdQcRawInspectionList.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 250);
            grdQcRawInspectionList.View.AddTextBoxColumn("CONSUMABLETYPE", 80);
            grdQcRawInspectionList.View.AddTextBoxColumn("CONSUMABLETYPENAME", 250);
            grdQcRawInspectionList.View.AddComboBoxColumn("ISREGISTRATION", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //grdQcRawInspectionList.View.AddTextBoxColumn("INSPECTIONCLASSID", 100);
            //grdQcRawInspectionList.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 250);
            grdQcRawInspectionList.View.AddTextBoxColumn("CONSUMABLECLASSID", 250).SetIsHidden();
            grdQcRawInspectionList.View.AddTextBoxColumn("INSPECTIONCLASSID", 250).SetIsHidden();
            grdQcRawInspectionList.View.PopulateColumns();
        }

        /// <summary>
        /// 수입검사 검사방법 그리드
        /// </summary>
        private void InitializeGridRawInspectionMaster()
        {

            grdRawInspectionMaster.GridButtonItem = GridButtonItem.Export;
            grdRawInspectionMaster.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdRawInspectionMaster.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();
            grdRawInspectionMaster.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();
            grdRawInspectionMaster.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsHidden();
            grdRawInspectionMaster.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 150)
               .SetIsHidden();
            grdRawInspectionMaster.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
               .SetIsHidden();
            grdRawInspectionMaster.View.AddTextBoxColumn("RESOURCETYPE", 150)
               .SetIsHidden();  
            grdRawInspectionMaster.View.AddTextBoxColumn("INSPECTIONMETHODID", 150)
               .SetIsHidden();

            grdRawInspectionMaster.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 150)
               .SetIsReadOnly();
            grdRawInspectionMaster.View.AddComboBoxColumn("ISREGISTRATION", 100
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();
                //.SetValidationIsRequired();
            //grdRawInspectionMaster.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //     .SetDefault("Valid")
            //     .SetValidationIsRequired()
            //     .SetTextAlignment(TextAlignment.Center);
            grdSubassemblyMaster.View.AddComboBoxColumn("ISAQL", 80
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                  .SetIsHidden(); 
            grdSubassemblyMaster.View.AddTextBoxColumn("AQLINSPECTIONLEVEL", 100)
                  .SetIsHidden(); //검사수준
            grdSubassemblyMaster.View.AddTextBoxColumn("AQLDEFECTLEVEL", 100)
                  .SetIsHidden();  //불량수준
            grdSubassemblyMaster.View.AddTextBoxColumn("AQLCYCLE", 100)
                  .SetIsHidden(); 
            grdSubassemblyMaster.View.AddComboBoxColumn("AQLDECISIONDEGREE", 80
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
              .SetIsHidden();// AQL판정등급

            grdRawInspectionMaster.View.AddComboBoxColumn("ISNCR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdRawInspectionMaster.View.AddComboBoxColumn("NCRCYCLE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspCycle", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdRawInspectionMaster.View.AddComboBoxColumn("NCRDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdRawInspectionMaster.View.AddSpinEditColumn("NCRINSPECTIONQTY", 100);//검사수량
            grdRawInspectionMaster.View.AddSpinEditColumn("NCRLOTSIZE", 100);//lotsize
            grdRawInspectionMaster.View.SetKeyColumn("INSPECTIONCLASSID", "INSPECTIONMETHODID", "INSPECTIONMETHODNAME");
            grdRawInspectionMaster.View.PopulateColumns();
        }
        private void InitializeGridRawInspectionList()
        {
            grdRawInspectionList.GridButtonItem = GridButtonItem.Export;
            grdRawInspectionList.View.SetIsReadOnly();
            //grdRawInspectionList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdRawInspectionList.View.AddTextBoxColumn("INSPECTIONDEFID", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 80).SetIsHidden();
            grdRawInspectionList.View.AddComboBoxColumn("INSPECTIONUNIT", 80, new SqlQuery("GetUomDefinitionList", "10001", $"UOMCLASSID={"MMQC"}")).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("TOPINSPITEMNAME", 200).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("MIDINSPITEMCLASSNAME", 200).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("RESOURCETYPE", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("PROCESSSEGMENTTYPE", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("PROCESSSEGID", 250).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("PROCESSSEGNAME", 250).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("PROCESSEGVERSION", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("PLANTID", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("INSPITEMCLASSID", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("VALIDTYPE", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("UNIT", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("SEQUENCE", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("TANKSIZE", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("CALCULATIONTYPE", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("ANALYSISCONST", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("FOMULATYPE", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("QTYCONST", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("SPECCLASSID", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("SPECSEQUENCE", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("QTYUNIT", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("DESCRIPTION", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("ITEMVERSION", 80).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("CONSUMABLECLASSID", 80).SetIsHidden();
          
            //InitializeGrd_InspItemIdPopup();
            grdRawInspectionList.View.AddTextBoxColumn("INSPITEMID", 150).SetIsReadOnly();
            grdRawInspectionList.View.AddTextBoxColumn("INSPITEMNAME", 150).SetIsReadOnly();
            grdRawInspectionList.View.AddTextBoxColumn("INSPITEMVERSION", 80)
                .SetIsReadOnly()
                .SetIsHidden();

            grdRawInspectionList.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsReadOnly()
                .SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("RESOURCEVERSION", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            grdRawInspectionList.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdRawInspectionList.View.AddComboBoxColumn("INSPECTORDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionGrade", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();

            grdRawInspectionList.View.AddComboBoxColumn("ISAQL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("AQLINSPECTIONLEVEL", 100).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("AQLDEFECTLEVEL", 100).SetIsHidden();
            grdRawInspectionList.View.AddComboBoxColumn("AQLDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdRawInspectionList.View.AddTextBoxColumn("AQLCYCLE", 100).SetIsHidden();

            grdRawInspectionList.View.AddComboBoxColumn("ISNCR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden();
            
            grdRawInspectionList.View.AddTextBoxColumn("NCRCYCLE", 100)
                .SetIsHidden();
            grdRawInspectionList.View.AddComboBoxColumn("NCRDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden(); 

            grdRawInspectionList.View.AddTextBoxColumn("NCRDEFECTRATE", 100).SetIsHidden();
            grdRawInspectionList.View.AddSpinEditColumn("NCRINSPECTIONQTY", 100).SetIsHidden(); ;
            grdRawInspectionList.View.AddSpinEditColumn("NCRLOTSIZE", 100).SetIsHidden(); ;
            grdRawInspectionList.View.AddTextBoxColumn("INSPITEMTYPE", 150)
                .SetIsReadOnly();
            grdRawInspectionList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdRawInspectionList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdRawInspectionList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdRawInspectionList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdRawInspectionList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdRawInspectionList.View.PopulateColumns();
        }
        private void InitializeGridQcSubassemblyInspection()
        {
            //수입(원자재가공품)
            grdQcSubassemblyInspection.GridButtonItem = GridButtonItem.Export;
            grdQcSubassemblyInspection.View.SetIsReadOnly();
            //grdQcSubassemblyInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdQcSubassemblyInspection.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
            grdQcSubassemblyInspection.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            grdQcSubassemblyInspection.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 250);
            grdQcSubassemblyInspection.View.AddTextBoxColumn("CONSUMABLETYPE", 80);
            grdQcSubassemblyInspection.View.AddTextBoxColumn("CONSUMABLETYPENAME", 250);
            //grdQcSubassemblyInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 100);
            //grdQcSubassemblyInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 250);
            grdQcSubassemblyInspection.View.AddTextBoxColumn("CONSUMABLECLASSID", 250).SetIsHidden();
            grdQcSubassemblyInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 250).SetIsHidden();
            grdQcSubassemblyInspection.View.AddComboBoxColumn("ISREGISTRATION", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdQcSubassemblyInspection.View.PopulateColumns();

        }
        /// <summary>
        /// 수입검사 검사방법 그리드
        /// </summary>
        private void InitializeGridSubassemblyMaster()
        {

            grdSubassemblyMaster.GridButtonItem = GridButtonItem.Export;
            grdSubassemblyMaster.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdSubassemblyMaster.View.AddTextBoxColumn("PLANTID", 200)
               .SetIsHidden();
            grdSubassemblyMaster.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();
            grdSubassemblyMaster.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsHidden();
            grdSubassemblyMaster.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 150)
               .SetIsHidden();
            grdSubassemblyMaster.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
               .SetIsHidden();
            grdSubassemblyMaster.View.AddTextBoxColumn("RESOURCETYPE", 150)
              .SetIsHidden();
            grdSubassemblyMaster.View.AddTextBoxColumn("INSPECTIONMETHODID", 150)
               .SetIsHidden();

            grdSubassemblyMaster.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 150)
                .SetIsReadOnly();
            grdSubassemblyMaster.View.AddComboBoxColumn("ISREGISTRATION", 100
               , new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               //.SetValidationIsRequired();
               .SetIsReadOnly();
            // grdSubassemblyMaster.View.AddComboBoxColumn("ISREGISTRATION", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdSubassemblyMaster.View.AddComboBoxColumn("ISAQL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdSubassemblyMaster.View.AddComboBoxColumn("AQLINSPECTIONLEVEL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AQLSize", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));// 검사수준
            grdSubassemblyMaster.View.AddComboBoxColumn("AQLDEFECTLEVEL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AQLRate", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));//불량수준
            grdSubassemblyMaster.View.AddComboBoxColumn("AQLCYCLE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspCycle", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdSubassemblyMaster.View.AddComboBoxColumn("AQLDECISIONDEGREE", 80
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));// AQL판정등급

            grdSubassemblyMaster.View.AddComboBoxColumn("ISNCR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdSubassemblyMaster.View.AddComboBoxColumn("NCRCYCLE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspCycle", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdSubassemblyMaster.View.AddComboBoxColumn("NCRDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdSubassemblyMaster.View.AddSpinEditColumn("NCRINSPECTIONQTY", 100);//검사수량
            grdSubassemblyMaster.View.AddSpinEditColumn("NCRLOTSIZE", 100);//lotsize
            //grdSubassemblyMaster.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            // .SetDefault("Valid")
            // .SetValidationIsRequired()
            // .SetTextAlignment(TextAlignment.Center);
            grdSubassemblyMaster.View.SetKeyColumn("INSPECTIONCLASSID", "INSPECTIONMETHODID", "INSPECTIONMETHODNAME");
            grdSubassemblyMaster.View.PopulateColumns();
        }
        private void InitializeGridSubassemblyInspection()
        {
           
            grdSubassemblyInspection.GridButtonItem = GridButtonItem.Export;
            grdSubassemblyInspection.View.SetIsReadOnly();
            //grdSubassemblyInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdSubassemblyInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 80).SetIsHidden();

            grdSubassemblyInspection.View.AddComboBoxColumn("INSPECTIONUNIT", 80, new SqlQuery("GetUomDefinitionList", "10001", $"UOMCLASSID=MMQC"), "UOMDEFNAME", "UOMDEFID")
             .SetDefault("PCS")
             .SetTextAlignment(TextAlignment.Center)
             .SetIsHidden();

            grdSubassemblyInspection.View.AddTextBoxColumn("TOPINSPITEMNAME", 200).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("MIDINSPITEMCLASSNAME", 200).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("RESOURCETYPE", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("PROCESSSEGMENTTYPE", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("PROCESSSEGID", 250).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("PROCESSSEGNAME", 250).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("PROCESSEGVERSION", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("PLANTID", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("INSPITEMCLASSID", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("VALIDTYPE", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("UNIT", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("SEQUENCE", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("TANKSIZE", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("CALCULATIONTYPE", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("ANALYSISCONST", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("FOMULATYPE", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("QTYCONST", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("SPECCLASSID", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("SPECSEQUENCE", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("QTYUNIT", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("DESCRIPTION", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("ITEMVERSION", 80).SetIsHidden();
            grdSubassemblyInspection.View.AddTextBoxColumn("CONSUMABLECLASSID", 80).SetIsHidden();
            
            //InitializeGrd_SubassemblyInspItemIdPopup();
            grdSubassemblyInspection.View.AddTextBoxColumn("INSPITEMID", 150).SetIsReadOnly();
            grdSubassemblyInspection.View.AddTextBoxColumn("INSPITEMNAME", 150).SetIsReadOnly();
            grdSubassemblyInspection.View.AddTextBoxColumn("INSPITEMVERSION", 80)
                .SetIsReadOnly()
                .SetIsHidden(); 

            grdSubassemblyInspection.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsReadOnly()
                .SetIsHidden(); 
            grdSubassemblyInspection.View.AddTextBoxColumn("RESOURCEVERSION", 80)
                .SetIsReadOnly()
                .SetIsHidden(); 
            grdSubassemblyInspection.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80,
                new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetIsHidden();  //검사필수여부
            grdSubassemblyInspection.View.AddComboBoxColumn("INSPECTORDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionGrade", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden();
            grdSubassemblyInspection.View.AddComboBoxColumn("ISAQL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden(); 
            grdSubassemblyInspection.View.AddTextBoxColumn("AQLINSPECTIONLEVEL", 100).SetIsHidden(); //검사수준
            grdSubassemblyInspection.View.AddTextBoxColumn("AQLDEFECTLEVEL", 100).SetIsHidden();  //불량수준
            grdSubassemblyInspection.View.AddTextBoxColumn("AQLCYCLE", 100).SetIsHidden(); 
            grdSubassemblyInspection.View.AddComboBoxColumn("AQLDECISIONDEGREE", 80
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden(); // AQL판정등급

            grdSubassemblyInspection.View.AddComboBoxColumn("ISNCR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden(); 

            grdSubassemblyInspection.View.AddTextBoxColumn("NCRCYCLE", 100).SetIsHidden(); ;
            grdSubassemblyInspection.View.AddComboBoxColumn("NCRDECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden(); 
            grdSubassemblyInspection.View.AddTextBoxColumn("NCRDEFECTRATE", 100).SetIsHidden();
            grdSubassemblyInspection.View.AddSpinEditColumn("NCRINSPECTIONQTY", 100).SetIsHidden(); ;//검사수량
            grdSubassemblyInspection.View.AddSpinEditColumn("NCRLOTSIZE", 100).SetIsHidden(); ;//lotsize
            grdSubassemblyInspection.View.AddTextBoxColumn("INSPITEMTYPE", 150).SetIsReadOnly();
            grdSubassemblyInspection.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetTextAlignment(TextAlignment.Center);
            grdSubassemblyInspection.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSubassemblyInspection.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSubassemblyInspection.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSubassemblyInspection.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSubassemblyInspection.View.PopulateColumns();
        }

       
       

        #endregion



        #region Event

        /// <summary>        
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {

            grdQcRawInspectionList.View.FocusedRowChanged += grdQcRawInspectionList_FocusedRowChanged;
            grdRawInspectionMaster.View.FocusedRowChanged += grdRawInspectionMaster_FocusedRowChanged;
            grdRawInspectionMaster.View.CellValueChanged += grdRawInspectionMaster_CellValueChanged;
            grdRawInspectionMaster.View.ShowingEditor += grdRawInspectionMaster_ShowingEditor;

            grdQcSubassemblyInspection.View.FocusedRowChanged += grdQcSubassemblyInspection_FocusedRowChanged;

            grdSubassemblyMaster.View.FocusedRowChanged += grdSubassemblyMaster_FocusedRowChanged;
            grdSubassemblyMaster.View.CellValueChanged += grdSubassemblyMaster_CellValueChanged;
            grdSubassemblyMaster.View.ShowingEditor += grdSubassemblyMaster_ShowingEditor;
            btnRawPoint.Click += BtnRawPoint_Click;
            btnRawSpecRegisterDetail.Click += BtnRawSpecRegisterDetail_Click;

            btnSubPoint.Click += BtnSubPoint_Click;
            btnSubSpecRegisterDetail.Click += BtnSubSpecRegisterDetail_Click;

            smartTabControl1.SelectedPageChanged += SmartTabControl1_SelectedPageChanged;
            btnSave.Click += BtnSave_Click;
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (smartTabControl1.SelectedTabPage.Name.Equals("xtraTabPage1"))
            {
            
                if (CheckClassPage1Save() == false) return;
            }

            else
            {
                if (CheckClassPage2Save() == false) return;
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    if (smartTabControl1.SelectedTabPage.Name.Equals("xtraTabPage1"))
                    {
                        SaveClassPage1Save();

                    }

                    else 
                    {
                        SaveClassPage2Save();

                    }
                    ShowMessage("SuccessOspProcess");

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();

                    btnSave.Enabled = true;

                }
            }

        }
        private void grdRawInspectionMaster_ShowingEditor(object sender, CancelEventArgs e)
        {

            string focusedFieldName = grdRawInspectionMaster.View.FocusedColumn.FieldName;
            string isNcr = Format.GetString(grdRawInspectionMaster.View.GetFocusedRowCellValue("ISNCR"));
            
            if (focusedFieldName.Equals("NCRCYCLE") || focusedFieldName.Equals("NCRDECISIONDEGREE"))
            {
                if (isNcr.Equals("N"))
                {
                    e.Cancel = true;
                }
            }

        }

        private void grdSubassemblyMaster_ShowingEditor(object sender, CancelEventArgs e)
        {
            string focusedFieldName = grdSubassemblyMaster.View.FocusedColumn.FieldName;
            string isAql = Format.GetString(grdSubassemblyMaster.View.GetFocusedRowCellValue("ISAQL"));
            string isNcr = Format.GetString(grdSubassemblyMaster.View.GetFocusedRowCellValue("ISNCR"));
            if (focusedFieldName.Equals("AQLINSPECTIONLEVEL") || focusedFieldName.Equals("AQLDEFECTLEVEL") 
                || focusedFieldName.Equals("AQLCYCLE") || focusedFieldName.Equals("AQLDECISIONDEGREE") )
            {
                if (isAql.Equals("N"))
                {
                    e.Cancel = true;
                }
            }
            if (focusedFieldName.Equals("NCRCYCLE") || focusedFieldName.Equals("NCRDECISIONDEGREE"))
            {
                if (isNcr.Equals("N"))
                {
                    e.Cancel = true;
                }
            }
        }
        private void grdRawInspectionMaster_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           

            if (e.Column.FieldName == "ISNCR")
            {
              if (grdRawInspectionMaster.View.GetFocusedRowCellValue("ISNCR").ToString().Equals("N"))
                {
                    grdRawInspectionMaster.View.SetFocusedRowCellValue("NCRCYCLE", "");
                    grdRawInspectionMaster.View.SetFocusedRowCellValue("NCRDECISIONDEGREE", "");
                }
            }

            
        }
        private void grdSubassemblyMaster_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {


            if (e.Column.FieldName == "ISAQL")
            {
                if (grdSubassemblyMaster.View.GetFocusedRowCellValue("ISAQL").ToString().Equals("N"))
                {
                    grdSubassemblyMaster.View.SetFocusedRowCellValue("AQLINSPECTIONLEVEL", "");
                    grdSubassemblyMaster.View.SetFocusedRowCellValue("AQLDEFECTLEVEL", "");
                    grdSubassemblyMaster.View.SetFocusedRowCellValue("AQLCYCLE", "");
                    grdSubassemblyMaster.View.SetFocusedRowCellValue("AQLDECISIONDEGREE", "");
                }
            }
            if (e.Column.FieldName == "ISNCR")
            {
                if (grdSubassemblyMaster.View.GetFocusedRowCellValue("ISNCR").ToString().Equals("N"))
                {
                    grdSubassemblyMaster.View.SetFocusedRowCellValue("NCRCYCLE", "");
                    grdSubassemblyMaster.View.SetFocusedRowCellValue("NCRDECISIONDEGREE", "");
                }
            }

        }

        private void SmartTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (smartTabControl1.SelectedTabPage.Name.Equals("xtraTabPage1"))
            {

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("P_CODECLASSID", "MaterialClass");
                param.Add("P_VALIDSTATE", "Valid");
                param.Add("P_PARENTINSPECTIONCLASSID", "RawInspection");
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dt = SqlExecuter.Query("GetInspectionDefiTypeSfcodeMaster", "10001", param);

                SmartComboBox combobox = Conditions.GetControl<SmartComboBox>("CONSUMABLETYPE");
                combobox.DisplayMember = "INSPECTIONCLASSNAME";
                combobox.ValueMember = "INSPECTIONCLASSMAT";

                combobox.DataSource = dt;


            }
            else
            {
                Dictionary<string, object> paramtap2 = new Dictionary<string, object>();

                paramtap2.Add("P_CODECLASSID", "ConsumableType");
                paramtap2.Add("P_PARENTINSPECTIONCLASSID", "SubassemblyInspection");
                paramtap2.Add("P_VALIDSTATE", "Valid");

                paramtap2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dttab2 = SqlExecuter.Query("GetInspectionDefiTypeSfcodeMaster", "10001", paramtap2);

                SmartComboBox comboboxtab2 = Conditions.GetControl<SmartComboBox>("CONSUMABLETYPE");
                comboboxtab2.DisplayMember = "INSPECTIONCLASSNAME";
                comboboxtab2.ValueMember = "INSPECTIONCLASSMAT";
                comboboxtab2.DataSource = dttab2;

            }
            
        }

        /// <summary>
        /// 수입원자재 가공품 규격 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSubSpecRegisterDetail_Click(object sender, EventArgs e)
        {
            if (grdSubassemblyInspection.View.DataRowCount == 0) return;
            DataRow chkData = grdSubassemblyInspection.View.GetFocusedDataRow();
            int irow = grdSubassemblyInspection.View.FocusedRowHandle;
            if (chkData["VALIDSTATE"].ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidStdData003", grdSubassemblyInspection.Caption.ToString()); //메세지 
                return;
            }
            if (!(chkData["INSPITEMTYPE"].ToString().Equals("SPC")))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidStdData002", chkData["INSPITEMTYPE"].ToString()); //메세지 
                return;
            }
            SpecDetailPopUp popup = new SpecDetailPopUp();
            if (chkData["SPECCLASSID"].ToString().Equals(""))
            {
                popup._specSequence = "";
                popup._specClassId = "SubassemblySpec";
            }
            else
            {
                popup._specSequence = chkData["SPECSEQUENCE"].ToString();
                popup._specClassId = chkData["SPECCLASSID"].ToString();

            }
          
            popup._inspitemid = chkData["INSPITEMID"].ToString();
            popup._inspectiondefid = chkData["INSPECTIONDEFID"].ToString();
            popup._equipmentid = "*";
            popup._childequipmentid = "*";
            popup._enterpriseid = chkData["ENTERPRISEID"].ToString();
            popup._plantid = chkData["PLANTID"].ToString();
            popup._processsegmentclassid = "*";
            popup._resourceid = chkData["RESOURCEID"].ToString();
            popup._resourceversion = chkData["RESOURCEVERSION"].ToString();
            popup._consumabledefid = "*";
            popup._productdefid = chkData["RESOURCEID"].ToString();

            popup.buttonType = true;

            // popup._inspectionName = _inspectionName;
            popup.Owner = this;
            popup.ShowDialog();
            InspectdClass2MasterfocusedRowChanged();

            grdSubassemblyInspection.View.ClearSelection();
            grdSubassemblyInspection.View.FocusedRowHandle = irow;

            grdSubassemblyInspection.View.SelectRow(irow);
        }

        private void BtnSubPoint_Click(object sender, EventArgs e)
        {
            if (grdSubassemblyInspection.View.DataRowCount == 0) return;
            DataRow row = grdSubassemblyInspection.View.GetFocusedDataRow();

            object[] obj = new object[11];
            //obj[0] = row["INSPITEMCLASSID"];
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
        /// <summary>
        /// 수입원자재 규격  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRawSpecRegisterDetail_Click(object sender, EventArgs e)
        {
            if (grdRawInspectionList.View.DataRowCount == 0) return;
            DataRow chkData = grdRawInspectionList.View.GetFocusedDataRow();
           
            if (chkData["VALIDSTATE"].ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidStdData003", grdRawInspectionList.Caption.ToString()); //메세지 
                return;
            }
            if (!(chkData["INSPITEMTYPE"].ToString().Equals("SPC")))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidStdData002", chkData["INSPITEMTYPE"].ToString()); //메세지 
                return;
            }
            int irow = grdSubassemblyInspection.View.FocusedRowHandle;
            SpecDetailPopUp popup = new SpecDetailPopUp();
            if (chkData["SPECCLASSID"].ToString().Equals(""))
            {
                popup._specSequence = "";
                popup._specClassId = "RawSpec";
            }
            else
            {
                popup._specSequence = chkData["SPECSEQUENCE"].ToString();
                popup._specClassId = chkData["SPECCLASSID"].ToString();

            }
            popup._inspectiondefid = chkData["INSPECTIONDEFID"].ToString();
            popup._equipmentid = "*";
            popup._childequipmentid = "*";
            popup._enterpriseid = chkData["ENTERPRISEID"].ToString();
            popup._plantid   = chkData["PLANTID"].ToString();
            popup._processsegmentclassid = "*";
            popup._inspitemid = chkData["INSPITEMID"].ToString();
            popup._resourceid = chkData["RESOURCEID"].ToString();
            popup._resourceversion = chkData["RESOURCEVERSION"].ToString();
            popup._consumabledefid = chkData["RESOURCEID"].ToString();
            popup._productdefid = "*";
            popup.buttonType = true;  //저장버튼 btnSave.Visible = false;

            // popup._inspectionName = _inspectionName;
            //popup.Owner = this;
            popup.ShowDialog(this);
            InspectdClass1MasterfocusedRowChanged();
            grdRawInspectionList.View.ClearSelection();
            grdRawInspectionList.View.FocusedRowHandle = irow;

            grdRawInspectionList.View.SelectRow(irow);
        }

        private void BtnRawPoint_Click(object sender, EventArgs e)
        {
            if (grdRawInspectionList.View.DataRowCount == 0) return;
            DataRow row = grdRawInspectionList.View.GetFocusedDataRow();

            object[] obj = new object[11];
            //obj[0] = row["INSPITEMCLASSID"];
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

       

        private void grdQcSubassemblyInspection_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdQcSubassemblyInspection.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            QcSubassemblyInspectionfocusedRowChanged();
           
         
        }
        private void grdSubassemblyMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            InspectdClass2MasterfocusedRowChanged();

        }
        private void grdQcRawInspectionList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            QcRawInspectionListfocusedRowChanged();
            
            
        }
        private void grdRawInspectionMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            InspectdClass1MasterfocusedRowChanged();


        }



        #endregion

        #region 툴바
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                
                BtnSave_Click(null, null);
            }
        }
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //switch (smartTabControl1.SelectedTabPageIndex)
            //{
            //    case 0:
            //        DataTable dtRawInspection = grdRawInspectionList.GetChangedRows();

            //        DataTable dtview = dtRawInspection.DefaultView.ToTable(true, new string[] { "RESOURCEID", "RESOURCEVERSION", "CONSUMABLECLASSID", "ENTERPRISEID", "PLANTID" });

            //        foreach (DataRow row in dtview.Rows)
            //        {
            //            Dictionary<string, object> Param = new Dictionary<string, object>();
            //            Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            //            Param.Add("RESOURCEID", row["RESOURCEID"].ToString());
            //            Param.Add("RESOURCEVERSION", row["RESOURCEVERSION"].ToString());
            //            Param.Add("CONSUMABLECLASSID", row["CONSUMABLECLASSID"].ToString());
            //            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //            DataTable dtRawInspectionList = SqlExecuter.Query("GetQcConsumableItemrelList", "10001", Param);

            //            if (dtRawInspectionList == null)
            //            {
            //                DataRow rowNew = dtRawInspection.NewRow();
            //                rowNew["INSPECTIONDEFID"] = "RawInspection";
            //                rowNew["INSPECTIONDEFVERSION"] = "*";
            //                rowNew["INSPITEMID"] = "*";
            //                rowNew["INSPITEMVERSION"] = "*";
            //                rowNew["RESOURCETYPE"] = "Consumable";
            //                rowNew["RESOURCEID"] = row["RESOURCEID"];
            //                rowNew["RESOURCEVERSION"] = "*";
            //                rowNew["PROCESSSEGMENTTYPE"] = "*";
            //                rowNew["PROCESSSEGID"] = "*";
            //                rowNew["PROCESSEGVERSION"] = "*";
            //                rowNew["ENTERPRISEID"] = row["ENTERPRISEID"];
            //                rowNew["PLANTID"] = row["PLANTID"];
            //                rowNew["ISINSPECTIONREQUIRED"] = "Y";
            //                rowNew["ISNCR"] = "Y";
            //                rowNew["NCRDECISIONDEGREE"] = "A";
            //                rowNew["VALIDSTATE"] = "Valid";
            //                rowNew["_STATE_"] = "added";
            //                dtRawInspection.Rows.Add(rowNew);
            //            }
            //            else
            //            {
            //                if (dtRawInspectionList.Rows.Count == 0)
            //                {
            //                    DataRow rowNew = dtRawInspection.NewRow();
            //                    rowNew["INSPECTIONDEFID"] = "RawInspection";
            //                    rowNew["INSPECTIONDEFVERSION"] = "*";
            //                    rowNew["INSPITEMID"] = "*";
            //                    rowNew["INSPITEMVERSION"] = "*";
            //                    rowNew["RESOURCETYPE"] = "Consumable";
            //                    rowNew["RESOURCEID"] = row["RESOURCEID"];
            //                    rowNew["RESOURCEVERSION"] = "*";
            //                    rowNew["PROCESSSEGMENTTYPE"] = "*";
            //                    rowNew["PROCESSSEGID"] = "*";
            //                    rowNew["PROCESSEGVERSION"] = "*";
            //                    rowNew["ENTERPRISEID"] = row["ENTERPRISEID"];
            //                    rowNew["PLANTID"] = row["PLANTID"];
            //                    rowNew["ISINSPECTIONREQUIRED"] = "Y";
            //                    rowNew["ISNCR"] = "Y";
            //                    rowNew["NCRDECISIONDEGREE"] = "A";
            //                    rowNew["VALIDSTATE"] = "Valid";
            //                    rowNew["_STATE_"] = "added";
            //                    dtRawInspection.Rows.Add(rowNew);
            //                }
            //            }
            //        }
            //        ExecuteRule("Inspectionitemrel", dtRawInspection);
            //        break;

            //    case 1:
            //        DataTable dtSubassemblyInspection = grdSubassemblyInspection.GetChangedRows();
            //        DataTable dtSIview = dtSubassemblyInspection.DefaultView.ToTable(true, new string[] { "RESOURCEID", "RESOURCEVERSION", "CONSUMABLECLASSID", "ENTERPRISEID", "PLANTID" });

            //        foreach (DataRow row in dtSIview.Rows)
            //        {
            //            Dictionary<string, object> Param = new Dictionary<string, object>();
            //            Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            //            Param.Add("RESOURCEID", row["RESOURCEID"].ToString());
            //            Param.Add("RESOURCEVERSION", row["RESOURCEVERSION"].ToString());
            //            Param.Add("CONSUMABLECLASSID", row["CONSUMABLECLASSID"].ToString());
            //            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //            DataTable dtSubassemblyInspectionList = SqlExecuter.Query("GetQcConsumableItemrelList", "10001", Param);

            //            if (dtSubassemblyInspectionList == null)
            //            {
            //                DataRow rowNew = dtSubassemblyInspection.NewRow();
            //                rowNew["INSPECTIONDEFID"] = "SubassemblyInspection";
            //                rowNew["INSPECTIONDEFVERSION"] = "*";
            //                rowNew["INSPITEMID"] = "*";
            //                rowNew["INSPITEMVERSION"] = "*";
            //                rowNew["RESOURCETYPE"] = "Consumable";
            //                rowNew["RESOURCEID"] = row["RESOURCEID"];
            //                rowNew["RESOURCEVERSION"] = "*";
            //                rowNew["PROCESSSEGMENTTYPE"] = "*";
            //                rowNew["PROCESSSEGID"] = "*";
            //                rowNew["PROCESSEGVERSION"] = "*";
            //                rowNew["ENTERPRISEID"] = row["ENTERPRISEID"];
            //                rowNew["PLANTID"] = row["PLANTID"];
            //                rowNew["ISINSPECTIONREQUIRED"] = "Y";
            //                rowNew["ISNCR"] = "Y";
            //                rowNew["NCRDECISIONDEGREE"] = "A";
            //                rowNew["VALIDSTATE"] = "Valid";
            //                rowNew["_STATE_"] = "added";
            //                dtSubassemblyInspection.Rows.Add(rowNew);
            //            }
            //            else
            //            {
            //                if (dtSubassemblyInspectionList.Rows.Count == 0)
            //                {
            //                    DataRow rowNew = dtSubassemblyInspection.NewRow();
            //                    rowNew["INSPECTIONDEFID"] = "SubassemblyInspection";
            //                    rowNew["INSPECTIONDEFVERSION"] = "*";
            //                    rowNew["INSPITEMID"] = "*";
            //                    rowNew["INSPITEMVERSION"] = "*";
            //                    rowNew["RESOURCETYPE"] = "Consumable";
            //                    rowNew["RESOURCEID"] = row["RESOURCEID"];
            //                    rowNew["RESOURCEVERSION"] = "*";
            //                    rowNew["PROCESSSEGMENTTYPE"] = "*";
            //                    rowNew["PROCESSSEGID"] = "*";
            //                    rowNew["PROCESSEGVERSION"] = "*";
            //                    rowNew["ENTERPRISEID"] = row["ENTERPRISEID"];
            //                    rowNew["PLANTID"] = row["PLANTID"];
            //                    rowNew["ISINSPECTIONREQUIRED"] = "Y";
            //                    rowNew["ISNCR"] = "Y";
            //                    rowNew["NCRDECISIONDEGREE"] = "A";
            //                    rowNew["VALIDSTATE"] = "Valid";
            //                    rowNew["_STATE_"] = "added";
            //                    dtSubassemblyInspection.Rows.Add(rowNew);
            //                }
            //            }
            //        }
            //        ExecuteRule("Inspectionitemrel", dtSubassemblyInspection);
            //        break;
            //}
           

        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            // 검사정의
            // Conditions.AddComboBox("INSPECTIONDEFID", new SqlQuery("GetQcInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"INSPECTIONTYPE={"Consumable"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID").SetValidationIsRequired();
            // 자재분류
             Conditions.AddComboBox("CONSUMABLETYPE", new SqlQuery("GetInspectionDefiTypeSfcodeMaster", "10001"
                 , $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_CODECLASSID={"MaterialClass"}", $"P_PARENTINSPECTIONCLASSID={"RawInspection"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSMAT").SetEmptyItem("","");

            //자재코드
            Conditions.AddTextBox("CONSUMABLEDEFID");
            //자재명
            Conditions.AddTextBox("CONSUMABLEDEFNAME");

            //대분류코드
            Conditions.AddTextBox("INSPITEMCLASSID");
            //대분류명
            Conditions.AddTextBox("INSPITEMCLASSNAME");
            Conditions.AddComboBox("ISREGISTRATION", new SqlQuery("GetCodeList", "00001"
                ,$"CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem("","");

            
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            //SmartComboBox combobox = Conditions.GetControl<SmartComboBox>("INSPECTIONDEFID");
            //combobox.ItemIndex = 0;
            //combobox.TextChanged += Combobox_TextChanged;



        }

        private void Combobox_TextChanged(object sender, EventArgs e)
        {

           

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
                value.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);


                //if (ComboBox.GetDataValue().ToString() == "RawInspection")
                //{
                //    cisiCOMPONENTITEMID.SearchQuery = new SqlQuery("GetBomCompPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"MASTERDATACLASSID={"RawMaterial"}");
                //}
                //if (ComboBox.GetDataValue().ToString() == "SubassemblyInspection")
                //{
                //    cisiCOMPONENTITEMID.SearchQuery = new SqlQuery("GetBomCompPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"MASTERDATACLASSID={"SubAssembly"}");

                //}


                switch (smartTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        value.Add("CONSUMABLECLASSID", "RawMaterial");
                        value.Add("PARENTINSPECTIONCLASSID", "RawInspection");

                        DataTable dtRawMaterial =  await SqlExecuter.QueryAsync("GetQcConsumableItemList", "10001", value);
                        grdQcRawInspectionList.DataSource = dtRawMaterial;
                        //string sINSPITEMCLASSNAME = "";
                     

                        //DataTable dtRawMaterialCopy = dtRawMaterial.DefaultView.ToTable(true, new string[] { "CONSUMABLEDEFID", "CONSUMABLEDEFVERSION", "CONSUMABLEDEFNAME", "CONSUMABLETYPE", "CONSUMABLETYPENAME", "CONSUMABLECLASSID" }).Copy();

                        //dtRawMaterialCopy.Columns.Add("INSPITEMCLASSNAME");
                        
                        //foreach (DataRow rowRawMaterialCopy in dtRawMaterialCopy.Rows)
                        //{
                        //    foreach (DataRow rowRawMaterial in dtRawMaterial.Select("CONSUMABLEDEFID = '" + rowRawMaterialCopy["CONSUMABLEDEFID"] + "' AND CONSUMABLEDEFVERSION = '" + rowRawMaterialCopy["CONSUMABLEDEFVERSION"] + "' AND CONSUMABLETYPE = '"+ rowRawMaterialCopy["CONSUMABLETYPE"] + "' AND CONSUMABLECLASSID = '"+ rowRawMaterialCopy["CONSUMABLECLASSID"] + "'", "INSPITEMCLASSID"))
                        //    {
                        //        sINSPITEMCLASSNAME = sINSPITEMCLASSNAME + rowRawMaterial["INSPITEMCLASSNAME"].ToString() + ",";
                        //    }
                        //    rowRawMaterialCopy["INSPITEMCLASSNAME"] = sINSPITEMCLASSNAME;
                        //    sINSPITEMCLASSNAME = "";
                        //}

                        ////rdSubassemblyInspection.
                        ////rdQcRawInspectionList.DataSource = dtRawMaterialCopy;
                        //grdQcRawInspectionList.DataSource = dtRawMaterialCopy;
                        break;
                    case 1:
                       

                        value.Add("CONSUMABLECLASSID", "SubAssembly");
                        value.Add("PARENTINSPECTIONCLASSID", "SubassemblyInspection");

                        DataTable dtSubAssembly = await SqlExecuter.QueryAsync("GetQcConsumableItemList", "10001", value);
                        grdQcSubassemblyInspection.DataSource = dtSubAssembly;
                        //DataTable dtSubAssemblyCopy = dtSubAssembly.DefaultView.ToTable(true, new string[] { "CONSUMABLEDEFID", "CONSUMABLEDEFVERSION", "CONSUMABLEDEFNAME", "CONSUMABLETYPE", "CONSUMABLETYPENAME", "CONSUMABLECLASSID" }).Copy();
                        //dtSubAssemblyCopy.Columns.Add("INSPITEMCLASSNAME");

                        //foreach (DataRow rowSubAssemblyCopy in dtSubAssemblyCopy.Rows)
                        //{
                        //    foreach (DataRow rowRawMaterial in dtSubAssembly.Select("CONSUMABLEDEFID = '" + rowSubAssemblyCopy["CONSUMABLEDEFID"] + "' AND CONSUMABLEDEFVERSION = '" + rowSubAssemblyCopy["CONSUMABLEDEFVERSION"] + "' AND CONSUMABLETYPE = '" + rowSubAssemblyCopy["CONSUMABLETYPE"] + "' AND CONSUMABLECLASSID = '" + rowSubAssemblyCopy["CONSUMABLECLASSID"] + "'", "INSPITEMCLASSID"))
                        //    {
                        //        sINSPITEMCLASSNAMESa = sINSPITEMCLASSNAMESa + rowRawMaterial["INSPITEMCLASSNAME"].ToString() + ",";
                        //    }
                        //    rowSubAssemblyCopy["INSPITEMCLASSNAME"] = sINSPITEMCLASSNAMESa;
                        //    sINSPITEMCLASSNAMESa = "";
                        //}


                        //grdQcSubassemblyInspection.DataSource = dtSubAssemblyCopy;
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
            base.OnValidateContent();

            grdRawInspectionList.View.CheckValidation();

            if (grdRawInspectionList.GetChangedRows().Rows.Count == 0)
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
        /// 수입검사 저장시 체크 로직 
        /// </summary>
        /// <returns></returns>
        private bool CheckClassPage1Save()
        {
            bool blcheck = true;
            DataTable changedMaster = grdRawInspectionMaster.GetChangedRows();

            if (changedMaster.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지 
                return false;
            }
            for (int i = 0; i < grdRawInspectionMaster.View.DataRowCount; i++)
            {
                DataRow row = grdRawInspectionMaster.View.GetDataRow(i);
                string strIsregistration = grdRawInspectionMaster.View.GetRowCellValue(i, "ISREGISTRATION").ToString();
                if (strIsregistration.Equals("Y"))
                {
                    string strIsncr = grdRawInspectionMaster.View.GetRowCellValue(i, "ISNCR").ToString();
                    if (strIsncr.Equals(""))
                    {
                        string lbIsncr = grdRawInspectionMaster.View.Columns["ISNCR"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbIsncr); //메세지
                        return false;
                    }
                    else if (strIsncr.Equals("Y"))
                    {
                        string strtemp = grdRawInspectionMaster.View.GetRowCellValue(i, "NCRCYCLE").ToString();
                        if (strtemp.Equals(""))
                        {
                            string lbltemp = grdRawInspectionMaster.View.Columns["NCRCYCLE"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbltemp); //메세지
                            return false;
                        }
                        strtemp = grdRawInspectionMaster.View.GetRowCellValue(i, "NCRDECISIONDEGREE").ToString();
                        if (strtemp.Equals(""))
                        {
                            string lbltemp = grdRawInspectionMaster.View.Columns["NCRDECISIONDEGREE"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbltemp); //메세지
                            return false;
                        }


                    }
                }
            }

            return blcheck;
        }

        /// <summary>
        /// 수입검사 저장시 체크 로직 
        /// </summary>
        /// <returns></returns>
        private bool CheckClassPage2Save()
        {
            bool blcheck = true;
            DataTable changedMaster = grdSubassemblyMaster.GetChangedRows();

            if (changedMaster.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지 
                return false;
            }
            for (int i = 0; i < grdSubassemblyMaster.View.DataRowCount; i++)
            {
                DataRow row = grdSubassemblyMaster.View.GetDataRow(i);
                string strIsregistration = grdSubassemblyMaster.View.GetRowCellValue(i, "ISREGISTRATION").ToString();
                //if (strIsregistration.Equals("Y")) // 등록되지 않았어도 필수 여부 체크 해야 한다고 판단 주석 처리 함-2020.01.14-유석진
                //{
                    string strIsaql = grdSubassemblyMaster.View.GetRowCellValue(i, "ISAQL").ToString();
                    if (strIsaql.Equals(""))
                    {
                        string lbISAQL = grdSubassemblyMaster.View.Columns["lbISAQL"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbISAQL); //메세지
                        return false;
                    }
                    else if (strIsaql.Equals("Y"))
                    {
                        string strtemp = grdSubassemblyMaster.View.GetRowCellValue(i, "AQLINSPECTIONLEVEL").ToString();
                        if (strtemp.Equals(""))
                        {
                            string lbltemp = grdSubassemblyMaster.View.Columns["AQLINSPECTIONLEVEL"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbltemp); //메세지
                            return false;
                        }
                        strtemp = grdSubassemblyMaster.View.GetRowCellValue(i, "AQLDEFECTLEVEL").ToString();
                        if (strtemp.Equals(""))
                        {
                            string lbltemp = grdSubassemblyMaster.View.Columns["AQLDEFECTLEVEL"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbltemp); //메세지
                            return false;
                        }
                        strtemp = grdSubassemblyMaster.View.GetRowCellValue(i, "AQLCYCLE").ToString();
                        if (strtemp.Equals(""))
                        {
                            string lbltemp = grdSubassemblyMaster.View.Columns["AQLCYCLE"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbltemp); //메세지
                            return false;
                        }
                        strtemp = grdSubassemblyMaster.View.GetRowCellValue(i, "AQLDECISIONDEGREE").ToString();
                        if (strtemp.Equals(""))
                        {
                            string lbltemp = grdSubassemblyMaster.View.Columns["AQLDECISIONDEGREE"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbltemp); //메세지
                            return false;
                        }

                    }
                    string strIsncr = grdSubassemblyMaster.View.GetRowCellValue(i, "ISNCR").ToString();
                    if (strIsncr.Equals(""))
                    {
                        string lbIsncr = grdSubassemblyMaster.View.Columns["ISNCR"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbIsncr); //메세지
                        return false;
                    }
                    else if (strIsncr.Equals("Y"))
                    {
                        string strtemp = grdSubassemblyMaster.View.GetRowCellValue(i, "NCRCYCLE").ToString();
                        if (strtemp.Equals(""))
                        {
                            string lbltemp = grdSubassemblyMaster.View.Columns["NCRCYCLE"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbltemp); //메세지
                            return false;
                        }
                        strtemp = grdSubassemblyMaster.View.GetRowCellValue(i, "NCRDECISIONDEGREE").ToString();
                        if (strtemp.Equals(""))
                        {
                            string lbltemp = grdSubassemblyMaster.View.Columns["NCRDECISIONDEGREE"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbltemp); //메세지
                            return false;
                        }
                    }
               //}
            }
           
            return blcheck;
        }
        /// <summary>
        /// 수입검사 저장및 저장후 조회 
        /// </summary>
        /// <returns></returns>
        private void SaveClassPage1Save()
        {
            string Inspectionclassmat = "";
            DataTable changedMaster = grdRawInspectionMaster.GetChangedRows();
            changedMaster.TableName = "list";
           
            ExecuteRule("QcConsumableInspectionitemrel", changedMaster);
            if (changedMaster.Rows.Count != 0)
            {
                for (int i = 0; i < changedMaster.Rows.Count; i++)
                {
                    DataRow row = changedMaster.Rows[i];
                    Inspectionclassmat = row["INSPECTIONMETHODID"].ToString();
                }
                //
                QcRawInspectionListfocusedRowChanged();
                int irow = checkGridProcess(Inspectionclassmat, grdRawInspectionMaster, "INSPECTIONMETHODID");
                if (irow > -1)
                {
                    grdRawInspectionMaster.View.ClearSelection();
                    grdRawInspectionMaster.View.FocusedRowHandle = irow;

                    grdRawInspectionMaster.View.SelectRow(irow);
                    grdRawInspectionMaster.View.FocusedColumn = grdRawInspectionMaster.View.Columns["INSPECTIONMETHODNAME"];
                    InspectdClass1MasterfocusedRowChanged();
                }
            }
           
        }
        /// <summary>
        /// 수입검사 저장및 저장후 조회 
        /// </summary>
        /// <returns></returns>
        private void SaveClassPage2Save()
        {
            string Inspectionclassmat = "";
            
            DataTable changedMaster = grdSubassemblyMaster.GetChangedRows();
            changedMaster.TableName = "list";
            ExecuteRule("QcConsumableInspectionitemrel", changedMaster);
            if (changedMaster.Rows.Count != 0)
            {
                for (int i = 0; i < changedMaster.Rows.Count; i++)
                {
                    DataRow row = changedMaster.Rows[i];
                    Inspectionclassmat = row["INSPECTIONMETHODID"].ToString();
                }
                QcSubassemblyInspectionfocusedRowChanged();
                int irow = checkGridProcess(Inspectionclassmat, grdSubassemblyMaster, "INSPECTIONMETHODID");
                if (irow > -1)
                {
                    grdSubassemblyMaster.View.ClearSelection();
                    grdSubassemblyMaster.View.FocusedRowHandle = irow;

                    grdSubassemblyMaster.View.SelectRow(irow);
                    grdSubassemblyMaster.View.FocusedColumn = grdSubassemblyMaster.View.Columns["INSPECTIONMETHODNAME"];
                    InspectdClass2MasterfocusedRowChanged();
                }
            }
            
        }
        /// <summary>
        /// 수입검사
        /// </summary>
        private void InspectdClass1MasterfocusedRowChanged()
        {
            DataRow row = grdRawInspectionMaster.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            Dictionary<string, object> ParamInputType = new Dictionary<string, object>();
            ParamInputType.Add("P_INSPECTIONDEFID", row["INSPECTIONDEFID"].ToString());
            ParamInputType.Add("P_CONSUMABLEDEFID", row["CONSUMABLEDEFID"].ToString());
            ParamInputType.Add("P_CONSUMABLEDEFVERSION", row["CONSUMABLEDEFVERSION"].ToString());
            ParamInputType.Add("P_INSPECTIONCLASSID", row["INSPECTIONCLASSID"].ToString());
            ParamInputType.Add("P_INSPECTIONMETHODID", row["INSPECTIONMETHODID"].ToString());

            ParamInputType.Add("P_ISAQL", row["ISAQL"].ToString());
            ParamInputType.Add("P_AQLINSPECTIONLEVEL", row["AQLINSPECTIONLEVEL"].ToString());
            ParamInputType.Add("P_AQLDEFECTLEVEL", row["AQLDEFECTLEVEL"].ToString());
            ParamInputType.Add("P_AQLCYCLE", row["AQLCYCLE"].ToString());
            ParamInputType.Add("P_AQLDECISIONDEGREE", row["AQLDECISIONDEGREE"].ToString());
            ParamInputType.Add("P_ISNCR", row["ISNCR"].ToString());
            ParamInputType.Add("P_NCRCYCLE", row["NCRCYCLE"].ToString());
            ParamInputType.Add("P_NCRDECISIONDEGREE", row["NCRDECISIONDEGREE"].ToString());
            ParamInputType.Add("P_NCRINSPECTIONQTY", row["NCRINSPECTIONQTY"].ToString());
            ParamInputType.Add("P_NCRLOTSIZE", row["NCRLOTSIZE"].ToString());


            ParamInputType.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamInputType.Add("P_PLANTID", UserInfo.Current.Plant);
            ParamInputType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtInputType = SqlExecuter.Query("GetQcConsumableItemrelList", "10001", ParamInputType);
            grdRawInspectionList.DataSource = dtInputType;
            if (dtInputType.Rows.Count  >0)
            {
                grdRawInspectionList.View.ClearSelection();
                grdRawInspectionList.View.FocusedRowHandle = 0;

                grdRawInspectionList.View.SelectRow(0);
            }
            else
            {
                grdRawInspectionList.View.ClearDatas();
            }

        }
        /// <summary>
        /// 수입검사 자재분류변경시
        /// </summary>
        private void InspectdClass2MasterfocusedRowChanged()
        {
            DataRow row = grdSubassemblyMaster.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            Dictionary<string, object> ParamInputType = new Dictionary<string, object>();
            ParamInputType.Add("P_INSPECTIONDEFID", row["INSPECTIONDEFID"].ToString());
            ParamInputType.Add("P_CONSUMABLEDEFID", row["CONSUMABLEDEFID"].ToString());
            ParamInputType.Add("P_CONSUMABLEDEFVERSION", row["CONSUMABLEDEFVERSION"].ToString());
            ParamInputType.Add("P_INSPECTIONCLASSID", row["INSPECTIONCLASSID"].ToString());
            ParamInputType.Add("P_INSPECTIONMETHODID", row["INSPECTIONMETHODID"].ToString());
            ParamInputType.Add("P_ISAQL", row["ISAQL"].ToString());
            ParamInputType.Add("P_AQLINSPECTIONLEVEL", row["AQLINSPECTIONLEVEL"].ToString());
            ParamInputType.Add("P_AQLDEFECTLEVEL", row["AQLDEFECTLEVEL"].ToString());
            ParamInputType.Add("P_AQLCYCLE", row["AQLCYCLE"].ToString());
            ParamInputType.Add("P_AQLDECISIONDEGREE", row["AQLDECISIONDEGREE"].ToString());
            ParamInputType.Add("P_ISNCR", row["ISNCR"].ToString());
            ParamInputType.Add("P_NCRCYCLE", row["NCRCYCLE"].ToString());
            ParamInputType.Add("P_NCRDECISIONDEGREE", row["NCRDECISIONDEGREE"].ToString());
            ParamInputType.Add("P_NCRINSPECTIONQTY", row["NCRINSPECTIONQTY"].ToString());
            ParamInputType.Add("P_NCRLOTSIZE", row["NCRLOTSIZE"].ToString());
            ParamInputType.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamInputType.Add("P_PLANTID", UserInfo.Current.Plant);
            ParamInputType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtInputType = SqlExecuter.Query("GetQcConsumableItemrelList", "10001", ParamInputType);
            grdSubassemblyInspection.DataSource = dtInputType;
            if (dtInputType.Rows.Count > 0)
            {
                grdSubassemblyInspection.View.ClearSelection();
                grdSubassemblyInspection.View.FocusedRowHandle = 0;

                grdSubassemblyInspection.View.SelectRow(0);
            }
           
        }

        /// <summary>
        /// 수입검사
        /// </summary>
        private void QcRawInspectionListfocusedRowChanged()
        {
            DataRow row = grdQcRawInspectionList.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            Dictionary<string, object> ParamInputType = new Dictionary<string, object>();

            ParamInputType.Add("P_CONSUMABLEDEFID", row["CONSUMABLEDEFID"].ToString());
            ParamInputType.Add("P_CONSUMABLEDEFVERSION", row["CONSUMABLEDEFVERSION"].ToString());
            ParamInputType.Add("P_INSPECTIONCLASSID", row["CONSUMABLETYPE"].ToString());
            ParamInputType.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamInputType.Add("P_INSPECTIONCLASSIDITEM", "RawInspection");
            ParamInputType.Add("P_PLANTID", UserInfo.Current.Plant);
            ParamInputType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtInputType = SqlExecuter.Query("GetQcConsumableInspitemClass", "10001", ParamInputType);
            grdRawInspectionMaster.DataSource = dtInputType;
            if (dtInputType.Rows.Count > 0)
            {
                grdRawInspectionMaster.View.ClearSelection();
                grdRawInspectionMaster.View.FocusedRowHandle = 0;

                grdRawInspectionMaster.View.SelectRow(0);
                InspectdClass1MasterfocusedRowChanged();

            }
            else
            {
                grdRawInspectionMaster.View.ClearDatas();
                grdRawInspectionList.View.ClearDatas();
            }
        }
        /// <summary>
        /// 수입검사 자재분류변경시
        /// </summary>
        private void QcSubassemblyInspectionfocusedRowChanged()
        {
            DataRow row = grdQcSubassemblyInspection.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            Dictionary<string, object> ParamInputType = new Dictionary<string, object>();

            ParamInputType.Add("P_CONSUMABLEDEFID", row["CONSUMABLEDEFID"].ToString());
            ParamInputType.Add("P_CONSUMABLEDEFVERSION", row["CONSUMABLEDEFVERSION"].ToString());
            ParamInputType.Add("P_INSPECTIONCLASSID", row["CONSUMABLETYPE"].ToString());
            ParamInputType.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamInputType.Add("P_INSPECTIONCLASSIDITEM", "SubassemblyInspection");
            ParamInputType.Add("P_PLANTID", UserInfo.Current.Plant);
            ParamInputType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtInputType = SqlExecuter.Query("GetQcConsumableInspitemClass", "10001", ParamInputType);
            grdSubassemblyMaster.DataSource = dtInputType;
            if (dtInputType.Rows.Count > 0)
            {
                grdSubassemblyMaster.View.ClearSelection();
                grdSubassemblyMaster.View.FocusedRowHandle = 0;

                grdSubassemblyMaster.View.SelectRow(0);
                InspectdClass2MasterfocusedRowChanged();
            }
            else
            {
                
                grdSubassemblyInspection.View.ClearDatas();
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
