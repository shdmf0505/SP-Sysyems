#region using
using DevExpress.XtraTreeList.Nodes;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.StandardInfo.Popup;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Columns;

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;


#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 표준공정등록
    /// 업 무 설명 : 표준공정 등록및 조회
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// </summary> 
    public partial class RoutingVer2 : SmartConditionBaseForm
	{
        #region Local Variables
        DataTable dttreeSet = new DataTable();
        //string sASSEMBLYROUTINGID = "";
        #endregion

        #region 생성자
        public RoutingVer2()
		{
			InitializeComponent();
		}
		#endregion

		#region 컨텐츠 영역 초기화
		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGridList()
		{
         

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //TAB 0 : 포준공정 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
           
            // 라운팅
            grdRouting.View.AddTextBoxColumn("ASSEMBLYROUTINGID");
            grdRouting.View.AddTextBoxColumn("ENTERPRISEID");
            grdRouting.View.AddTextBoxColumn("PLANTID");
            grdRouting.View.AddTextBoxColumn("NEWREQUESTNO");
            grdRouting.View.AddTextBoxColumn("ASSEMBLYITEMID");
            grdRouting.View.AddTextBoxColumn("ASSEMBLYITEMNAME");

            grdRouting.View.AddTextBoxColumn("LEADTIME");
            
            grdRouting.View.AddTextBoxColumn("ASSEMBLYITEMVERSION");
            grdRouting.View.AddTextBoxColumn("ASSEMBLYITEMCLASS");
            grdRouting.View.AddTextBoxColumn("ASSEMBLYITEMUOM");

            grdRouting.View.AddTextBoxColumn("ENGINEERINGCHANGE");
            grdRouting.View.AddTextBoxColumn("IMPLEMENTATIONDATE");

            grdRouting.View.AddTextBoxColumn("COMMONROUTINGID");
            grdRouting.View.AddTextBoxColumn("COMPLETIONWAREHOUSEID");
            grdRouting.View.AddTextBoxColumn("COMPLETIONLOCATIONID");
            grdRouting.View.AddTextBoxColumn("TOTALLEADTIME");

            grdRouting.View.AddTextBoxColumn("ASSEMBLYBOMID");
            grdRouting.View.AddTextBoxColumn("OPERATIONID");
            grdRouting.View.AddTextBoxColumn("OPERATIONID_PAR");
            
            grdRouting.View.AddTextBoxColumn("OPERATIONSEQUENCE");
            grdRouting.View.AddTextBoxColumn("ITEMUOM");
            
            grdRouting.View.PopulateColumns();
            
            

            // 공정
            grdOperation.GridButtonItem = GridButtonItem.All;
            grdOperation.View.AddComboBoxColumn("PLANTID",80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID").SetValidationIsRequired();
            grdOperation.View.AddTextBoxColumn("USERSEQUENCE",100).SetValidationIsRequired();
            //grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            InitializeGrid_ProcesssegMnetPopup();

            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 300).SetIsReadOnly();
            //grdOperation.View.AddTextBoxColumn("AREAID", 100);
            //InitializeGrid_AreaPopup();

            grdOperation.View.AddTextBoxColumn("AREANAME", 100).SetIsHidden();

            grdOperation.View.AddComboBoxColumn("OPERATIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OperationType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdOperation.View.AddSpinEditColumn("OPERATIONLEADTIMEPERCENT", 100).SetIsHidden();
            grdOperation.View.AddComboBoxColumn("DEPARTMENT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Department", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdOperation.View.AddSpinEditColumn("MINIMUMTRANSFERQTY", 100).SetIsHidden();

            grdOperation.View.AddComboBoxColumn("COUNTPOINTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CountPointType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();

            grdOperation.View.AddTextBoxColumn("DESCRIPTION", 250);
           
            
            grdOperation.View.AddSpinEditColumn("YIELD", 100).SetIsHidden();
            grdOperation.View.AddSpinEditColumn("WORKTIME", 100).SetIsHidden();

            grdOperation.View.AddSpinEditColumn("EQUIPMENTTIME", 100).SetIsHidden();

            grdOperation.View.AddSpinEditColumn("TACTTIME", 100).SetIsHidden();
            grdOperation.View.AddSpinEditColumn("LEADTIME", 100).SetIsHidden();

            grdOperation.View.AddComboBoxColumn("STEPTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=StepType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdOperation.View.AddComboBoxColumn("STEPCLASS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=StepCodeClassID", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();

            grdOperation.View.AddSpinEditColumn("RECEIVETIME", 100).SetIsHidden();
            grdOperation.View.AddSpinEditColumn("STARTTIME", 100).SetIsHidden();
            grdOperation.View.AddSpinEditColumn("ENDTIME", 100).SetIsHidden();
            grdOperation.View.AddSpinEditColumn("SENDTIME", 100).SetIsHidden();
            grdOperation.View.AddSpinEditColumn("MOVETIME", 100).SetIsHidden();

            InitializeGrid_WareHousePopup();
            grdOperation.View.AddTextBoxColumn("COMPLETEWAREHOUSENAME", 100).SetIsHidden();
            InitializeGrid_LocationPopup("");
            grdOperation.View.AddTextBoxColumn("COMPLETELOCATIONNAME", 100).SetIsHidden();

            grdOperation.View.AddSpinEditColumn("NETPLANNINGPERCENT", 100).SetIsHidden();

            grdOperation.View.AddComboBoxColumn("INCLUDEINROLLUP", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=IncludeInRollup", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();

            grdOperation.View.AddTextBoxColumn("ENGINEERINGCHANGE", 250).SetIsHidden();


            grdOperation.View.AddComboBoxColumn("SHUTDOWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ShutdownType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();

            // 영풍
            if (UserInfo.Current.Enterprise == "YOUNGPOONG")
            {
                grdOperation.View.AddComboBoxColumn("PROCESSUOM", 80, new SqlQuery("GetUomDefinitionList", "10001", $"UOMCLASSID={"Process"}"), "UOMDEFID", "UOMDEFNAME").SetEmptyItem("", "", true);
            }
            grdOperation.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 250).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("MAINPRODUCTID", 10).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("MAINPRODUCTVERSION", 10).SetIsHidden();
                  

            grdOperation.View.PopulateColumns();


            grdBomComp.GridButtonItem = GridButtonItem.All;
            grdBomComp.View.AddSpinEditColumn("ASSEMBLYBOMID", 100).SetIsHidden();
            grdBomComp.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsHidden();
            grdBomComp.View.AddTextBoxColumn("OPERATIONID", 100).SetIsHidden();
            grdBomComp.View.AddSpinEditColumn("OPERATIONSEQUENCE", 100).SetIsHidden();
            grdBomComp.View.AddSpinEditColumn("COMPONENTBOMID", 100).SetIsHidden();
            grdBomComp.View.AddSpinEditColumn("COMPONENTSEQUENCE", 100).SetIsHidden();
            grdBomComp.View.AddTextBoxColumn("PLANTID", 100).SetIsHidden();
            InitializeGrid_ItemMasterPopup();
            grdBomComp.View.AddTextBoxColumn("COMPONENTITEMNAME", 300);
            grdBomComp.View.AddTextBoxColumn("COMPONENTITEMVERSION", 100).SetIsReadOnly();
            grdBomComp.View.AddTextBoxColumn("COMPONENTITEMCLASS", 100).SetIsHidden();

            //grdBomComp.View.AddComboBoxColumn("COMPONENTUOM", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UOMDefID", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdBomComp.View.AddComboBoxColumn("COMPONENTUOM", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFID", "UOMDEFNAME");
            //grdBomComp.View.AddComboBoxColumn("MATERIALTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden(); 
            grdBomComp.View.AddSpinEditColumn("COMPONENTQTY", 100)
                .SetDisplayFormat("#,##0.#########").SetValidationIsRequired();
            grdBomComp.View.AddSpinEditColumn("REGISTEREDHANDBOOKQTY", 100)
                .SetDisplayFormat("#,##0.#########").SetIsHidden();
            grdBomComp.View.AddTextBoxColumn("PLANNINGFACTOR", 100).SetIsHidden();
            grdBomComp.View.AddTextBoxColumn("COMPONENTYIELDFACTOR", 100).SetIsHidden();

            grdBomComp.View.AddComboBoxColumn("WORKSURFACE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkSurface", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem("","",true);


            

            grdBomComp.View.AddComboBoxColumn("WIPSUPPLYTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WipSupplyType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired();
            grdBomComp.View.AddComboBoxColumn("USERLAYER", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            
            grdBomComp.View.AddTextBoxColumn("SUPPLYWAREHOUSEID", 100).SetIsHidden(); 
            grdBomComp.View.AddTextBoxColumn("SUPPLYLOCATIONID", 100).SetIsHidden(); 
            grdBomComp.View.AddComboBoxColumn("ISREQUIREDMATERIAL",100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));


            //grdBomComp.View.AddTextBoxColumn("ISAUTOREQUESTMATERIAL", 100).SetIsHidden();
            InitializeGrid_ProcesssegMnetFromPopup();

            grdBomComp.View.AddTextBoxColumn("INCLUDEINCOSTROLLUP", 100).SetIsHidden(); 
            grdBomComp.View.AddTextBoxColumn("FROMOPERATIONID", 100).SetIsHidden(); 
            grdBomComp.View.AddTextBoxColumn("TOOPERATIONID", 100).SetIsHidden(); 
            grdBomComp.View.AddTextBoxColumn("OPTIONAL", 100).SetIsHidden(); 
            grdBomComp.View.AddTextBoxColumn("CHECKATP", 100).SetIsHidden(); ;
            grdBomComp.View.AddTextBoxColumn("ENGINEERINGCHANGE", 100).SetIsHidden(); 
            grdBomComp.View.AddTextBoxColumn("IMPLEMENTATIONDATE", 100).SetIsHidden();

        
            
            grdBomComp.View.AddTextBoxColumn("DESCRIPTION", 100).SetIsHidden(); 
            grdBomComp.View.AddTextBoxColumn("VALIDSTATE", 100).SetIsHidden(); 

            grdBomComp.View.AddTextBoxColumn("GUBUN", 100).SetIsHidden();
            grdBomComp.View.AddTextBoxColumn("USERSEQUENCE", 100).SetIsHidden();
            
            grdBomComp.View.PopulateColumns();

            grdSpecattribute.GridButtonItem = GridButtonItem.None;
            grdSpecattribute.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdSpecattribute.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdSpecattribute.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdSpecattribute.View.AddTextBoxColumn("SPECSEQUENCE").SetIsHidden();
            grdSpecattribute.View.AddComboBoxColumn("INSPECTIONDEFID", 100, new SqlQuery("GetInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID").SetIsHidden();
            //grdSpecattribute.View.AddComboBoxColumn("INSPITEMCLASSID", 100, new SqlQuery("GetInspitemClassCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSNAME", "INSPITEMCLASSID").SetIsHidden();
            grdSpecattribute.View.AddTextBoxColumn("INSPITEMID", 100).SetIsHidden();
            grdSpecattribute.View.AddTextBoxColumn("INSPITEMNAME",250).SetIsReadOnly();
            grdSpecattribute.View.AddTextBoxColumn("YN_AOI", 100).SetIsReadOnly();
            //grdSpecattribute.View.AddComboBoxColumn("AOIQCLAYER", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdSpecattribute.View.AddTextBoxColumn("DESCRIPTION",250).SetIsReadOnly();
            grdSpecattribute.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdSpecattribute.View.PopulateColumns();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //TAB 1 : TOOL 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdTool.GridButtonItem = GridButtonItem.All;
            grdTool.View.AddTextBoxColumn("OPERATIONRESOURCEID", 100).SetIsHidden();
            grdTool.View.AddSpinEditColumn("RESOURCESEQUENCE", 100).SetIsHidden();
            grdTool.View.AddTextBoxColumn("OPERATIONID", 100).SetIsHidden();
            grdTool.View.AddSpinEditColumn("OPERATIONSEQUENCE", 100).SetIsHidden();
            grdTool.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsHidden();
            grdTool.View.AddTextBoxColumn("PLANTID", 100).SetIsHidden();
            //자원관리 팝업
            InitializeGrid_DurabledefinitPopup();
            grdTool.View.AddTextBoxColumn("TOOLNAME", 100).SetIsReadOnly();
            grdTool.View.AddTextBoxColumn("TOOLVERSION", 100).SetIsReadOnly();
            //grdTool.View.AddTextBoxColumn("FILMUSELAYER", 100).SetIsReadOnly();
            grdTool.View.AddComboBoxColumn("FILMUSELAYER", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();


            grdTool.View.AddComboBoxColumn("RESOURCETYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=DurableClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            grdTool.View.AddSpinEditColumn("SCHEDULESEQUENCE", 100).SetIsHidden();

            grdTool.View.AddTextBoxColumn("ISSTANDARDRATE").SetIsHidden();
            grdTool.View.AddSpinEditColumn("ASSIGNEDUNITS").SetIsHidden();
            grdTool.View.AddSpinEditColumn("MANHOUR").SetIsHidden();
            grdTool.View.AddTextBoxColumn("ISAUTOCHARGE").SetIsHidden();
            grdTool.View.AddTextBoxColumn("BASISTYPE").SetIsHidden();
            grdTool.View.AddTextBoxColumn("ISSCHEDULE").SetIsHidden();
            grdTool.View.AddSpinEditColumn("USEDLIMIT").SetIsHidden();
            grdTool.View.AddSpinEditColumn("CLEANLIMIT").SetIsHidden();
            grdTool.View.AddSpinEditColumn("THICKNESSLIMIT").SetIsHidden();
            grdTool.View.AddSpinEditColumn("USEDFACTOR").SetIsHidden();

            grdTool.View.AddSpinEditColumn("ASSEMBLYITEMID").SetIsHidden();
            grdTool.View.AddSpinEditColumn("ASSEMBLYITEMVERSION").SetIsHidden();
            grdTool.View.AddSpinEditColumn("PROCESSSEGMENTID").SetIsHidden();
            
            


            this.grdTool.View.AddTextBoxColumn("CREATOR", 80)
              // ReadOnly 컬럼 지정
              .SetIsReadOnly()
              .SetTextAlignment(TextAlignment.Center);
            this.grdTool.View.AddTextBoxColumn("CREATEDTIME", 130)
                // Display Format 지정
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            this.grdTool.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            this.grdTool.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdTool.View.PopulateColumns();


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //AOI 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            grdOperationspecvalue.View.AddTextBoxColumn("OPERATIONID", 150).SetIsHidden();
            grdOperationspecvalue.View.AddTextBoxColumn("OPERATIONSEQUENCE", 150).SetIsHidden();
            grdOperationspecvalue.View.AddTextBoxColumn("ENTERPRISEID", 150).SetIsHidden();
            grdOperationspecvalue.View.AddTextBoxColumn("PLANTID", 150).SetIsHidden();
            grdOperationspecvalue.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();

          
            grdOperationspecvalue.View.AddComboBoxColumn("AOIQCLAYER", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdOperationspecvalue.View.AddComboBoxColumn("AOIQCLAYER2", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
              
            grdOperationspecvalue.View.AddTextBoxColumn("DESCRIPTION", 150);
            grdOperationspecvalue.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //grdDff.View.AddTextBoxColumn("ATTRIBUTEDESCRIPTION", 150).SetIsHidden();

            grdOperationspecvalue.View.PopulateColumns();


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //외주단가 유형등록
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdRoutingAttributeValue.GridButtonItem = GridButtonItem.All;// 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdRoutingAttributeValue.View.AddTextBoxColumn("ITEMID", 100).SetIsHidden();
            grdRoutingAttributeValue.View.AddTextBoxColumn("ITEMVERSION", 100).SetIsHidden();
            grdRoutingAttributeValue.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsHidden();
            grdRoutingAttributeValue.View.AddTextBoxColumn("PLANTID", 100).SetIsHidden();
            grdRoutingAttributeValue.View.AddTextBoxColumn("USERSEQUENCE", 100).SetIsHidden();

            grdRoutingAttributeValue.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100).SetIsHidden();
            grdRoutingAttributeValue.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsHidden();


            grdRoutingAttributeValue.View.AddSpinEditColumn("SEQUENCE", 80).SetIsHidden();
            grdRoutingAttributeValue.View.AddTextBoxColumn("ATTRIBUTECLASS", 100).SetIsHidden();
           // InitializeGrid_OspPriceListPopup();
           // grdRoutingAttributeValue.View.AddTextBoxColumn("OSPPRICENAME", 100).SetIsReadOnly();
            grdRoutingAttributeValue.View.AddSpinEditColumn("ATTRIBUTEVALUE1",200)
            .SetDisplayFormat("#,##0.#########").SetValidationIsRequired();

            //grdRoutingAttributeValue.View.AddComboBoxColumn("ATTRIBUTECODE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AttributeCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));


            InitializeGrid_AttributePopup();
            //grdRoutingAttributeValue.View.AddComboBoxColumn("ATTRIBUTECODE", 100, new SqlQuery("GetRoutingPSMAttributeValueCode", "10001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "ATTRIBUTENAME", "ATTRIBUTECODE")
            //.SetPopupDefaultByGridColumnId("PROCESSSEGMENTCLASSID")
            //.SetPopupDefaultByGridColumnId("PROCESSSEGMENTID");

            

            //grdRoutingAttributeValue.View.AddComboBoxColumn("SPECUNIT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AttributeCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //grdRoutingAttributeValue.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //grdRoutingAttributeValue.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFNAME", "UOMDEFID").SetIsReadOnly();

            grdRoutingAttributeValue.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));


            grdRoutingAttributeValue.View.PopulateColumns();



        }
        private void InitializeGrid_AttributePopup()
        {

            var parentPopupColumn = this.grdRoutingAttributeValue.View.AddSelectPopupColumn("ATTRIBUTECODE",100, new SqlQuery("GetRoutingPSMAttributeValueCode", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("ATTRIBUTECODE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow);
                // 그리드의 남은 영역을 채울 컬럼 설정
                //.SetPopupAutoFillColumns("CODECLASSNAME")
                // Validation 이 필요한 경우 호출할 Method 지정

            //.SetPopupValidationCustom(ValidationOspPricePopup);

            // 팝업에서 사용할 조회조건 항목 추가




            parentPopupColumn.Conditions.AddTextBox("ATTRIBUTECODE");
            // 팝업에서 사용할 조회조건 항목 추가
            parentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTID")
                .SetPopupDefaultByGridColumnId("PROCESSSEGMENTID")
                .SetIsHidden();
            parentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTCLASSID")
                 .SetPopupDefaultByGridColumnId("PROCESSSEGMENTCLASSID")
                .SetIsHidden();
            // 팝업 그리드 설정
            parentPopupColumn.GridColumns.AddTextBoxColumn("ATTRIBUTECODE", 150);
            //parentPopupColumn.GridColumns.AddTextBoxColumn("OSPPRICENAME", 150);

        }

        // tool 팝업
        private void InitializeGrid_DurabledefinitPopup()
        {
            var parentDurabledefinitPopupColumn = this.grdTool.View.AddSelectPopupColumn("TOOLCODE", new SqlQuery("GetDurabledefiniTionPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("TOOL", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("TOOLCODE", "DURABLEDEFID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                //.SetPopupAutoFillColumns("CODECLASSNAME")
                // Validation 이 필요한 경우 호출할 Method 지정

                .SetPopupValidationCustom(ValidationToolPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentDurabledefinitPopupColumn.Conditions.AddTextBox("DURABLEDEFID");
            parentDurabledefinitPopupColumn.Conditions.AddTextBox("DURABLEDEFNAME");

            parentDurabledefinitPopupColumn.Conditions.AddTextBox("ITEMID");
            parentDurabledefinitPopupColumn.Conditions.AddTextBox("ITEMNAME");
            parentDurabledefinitPopupColumn.Conditions.AddTextBox("ITEMVERSION");
            //.SetPopupDefaultByGridColumnId("ASSEMBLYITEMVERSION")
            //.SetIsHidden();
            
            //.SetPopupDefaultByGridColumnId("ASSEMBLYITEMID")
            //.SetIsHidden();

            parentDurabledefinitPopupColumn.Conditions.AddTextBox("OPERATIONID")
            .SetPopupDefaultByGridColumnId("OPERATIONID")
            .SetIsHidden();
            parentDurabledefinitPopupColumn.Conditions.AddTextBox("OPERATIONSEQUENCE")
            .SetPopupDefaultByGridColumnId("OPERATIONSEQUENCE")
            .SetIsHidden();
            // 팝업 그리드 설정
            parentDurabledefinitPopupColumn.GridColumns.AddComboBoxColumn("DURABLECLASSTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DurableClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("DURABLEDEFID", 100);
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("DURABLEDEFNAME", 250);
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            parentDurabledefinitPopupColumn.GridColumns.AddComboBoxColumn("FILMUSELAYER", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
        }

        private void InitializeGrid_ProcesssegMnetFromPopup()
        {

            var parentProcesssegMnet = this.grdBomComp.View.AddSelectPopupColumn("ISAUTOREQUESTMATERIAL", new SqlQuery("GetRoutingAutoRequestMaterialPopup", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("ISAUTOREQUESTMATERIAL", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
             .SetPopupResultMapping("ISAUTOREQUESTMATERIAL", "USERSEQUENCE")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow);
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정

            //.SetPopupValidationCustom(ValidationProcesssegMnetToPopup);



            // 팝업에서 사용할 조회조건 항목 추가
            parentProcesssegMnet.Conditions.AddTextBox("PROCESSSEGMENTID");
            parentProcesssegMnet.Conditions.AddTextBox("PROCESSSEGMENTNAME");
            parentProcesssegMnet.Conditions.AddTextBox("USERSEQUENCE")
               .SetPopupDefaultByGridColumnId("USERSEQUENCE")
               .SetIsHidden();
            parentProcesssegMnet.Conditions.AddTextBox("ASSEMBLYITEMVERSION")
                .SetPopupDefaultByGridColumnId("ASSEMBLYITEMVERSION")
                .SetIsHidden();
            parentProcesssegMnet.Conditions.AddTextBox("ASSEMBLYITEMID")
                .SetPopupDefaultByGridColumnId("ASSEMBLYITEMID")
                .SetIsHidden();

            // parentItem.Conditions.AddTextBox("ITEMNAME");

            // 팝업 그리드 설정
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 300);
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("USERSEQUENCE", 250);
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("PLANTID", 250);

        }

        private void InitializeGrid_ItemMasterPopup()
        {

            var parentItem = this.grdBomComp.View.AddSelectPopupColumn("COMPONENTITEMID", new SqlQuery("GetBomCompPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("COMPONENTITEM", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
             // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)

             .SetPopupResultMapping("COMPONENTITEMID", "ITEMID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정
            .SetValidationIsRequired()
            .SetPopupValidationCustom(ValidationItemMasterPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentItem.Conditions.AddTextBox("ITEMID");
            parentItem.Conditions.AddTextBox("ITEMVERSION");
            parentItem.Conditions.AddTextBox("ITEMNAME");

            // 팝업 그리드 설정
            parentItem.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentItem.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            parentItem.GridColumns.AddTextBoxColumn("ITEMNAME", 250);
            parentItem.GridColumns.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFNAME", "UOMDEFID");

            //parentItem.GridColumns.AddTextBoxColumn("UOMDEFNAME", 250);

        }

        private void InitializeGrid_LocationPopup(string sCOMPLETEWAREHOUSEID)
        {

            var parentProcesssegMnet = this.grdOperation.View.AddSelectPopupColumn("COMPLETELOCATIONID", new SqlQuery("GetlocationList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"WAREHOUSEID={sCOMPLETEWAREHOUSEID}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("LOCATIONID", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
            //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정
            .SetIsHidden()
            .SetPopupValidationCustom(ValidationLocationPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentProcesssegMnet.Conditions.AddTextBox("TXTLOCATION");
            

            // 팝업 그리드 설정
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("LOCATIONID", 150);
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("LOCATIONNAME", 200);

         


        }

        private void InitializeGrid_LocationPopupShow(string sCOMPLETEWAREHOUSEID)
        {

            var parentProcesssegMnet = this.grdOperation.View.AddSelectPopupColumn("COMPLETELOCATIONID", new SqlQuery("GetlocationList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"WAREHOUSEID={sCOMPLETEWAREHOUSEID}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("LOCATIONID", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
            //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정
            
            .SetPopupValidationCustom(ValidationLocationPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentProcesssegMnet.Conditions.AddTextBox("TXTLOCATION");


            // 팝업 그리드 설정
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("LOCATIONID", 150);
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("LOCATIONNAME", 200);

            

        }


        private void InitializeGrid_WareHousePopup()
        {

            var parentProcesssegMnet = this.grdOperation.View.AddSelectPopupColumn("COMPLETEWAREHOUSEID", new SqlQuery("GetWarehouseList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("WAREHOUSEID", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
            //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정
            .SetIsHidden()
            .SetPopupValidationCustom(ValidationWareHousePopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentProcesssegMnet.Conditions.AddTextBox("TXTWAREHOUSE");

            // 팝업 그리드 설정
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("COMPLETEWAREHOUSEID", 150);
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("COMPLETEWAREHOUSENAME", 200);

        }

        private void InitializeGrid_ProcesssegMnetPopup()
        {

            var parentProcesssegMnet = this.grdOperation.View.AddSelectPopupColumn("PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentExtPupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"VALIDSTATE={"Valid"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
            //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정

            .SetPopupValidationCustom(ValidationProcesssegMnetPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentProcesssegMnet.Conditions.AddTextBox("PROCESSSEGMENTID");
            parentProcesssegMnet.Conditions.AddTextBox("PROCESSSEGMENTNAME");

            // 팝업 그리드 설정
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }

        private void InitializeGrid_AreaPopup()
        {

            var parentProcesssegMnet = this.grdOperation.View.AddSelectPopupColumn("AREAID", new SqlQuery("GetAreaList", "10003", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
            //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정

            .SetPopupValidationCustom(ValidationAreaPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentProcesssegMnet.Conditions.AddTextBox("AREA");
            //parentProcesssegMnet.Conditions.AddTextBox("AREANAME");

            // 팝업 그리드 설정
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("AREAID", 150);
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }


        /// <summary>
        /// Area Tree 초기화
        /// </summary>
        private void InitializeTreeArea()
		{
		
		}

		/// <summary>
		/// 설정 초기화
		/// </summary>
		protected override void InitializeContent()
		{
            InitializeGridList();
            InitializeEvent();

            // 상태
            //cboStatus.DisplayMember = "CODENAME";
            //cboStatus.ValueMember = "CODEID";
            //cboStatus.ShowHeader = false;
            //Dictionary<string, object> ParamStatus = new Dictionary<string, object>();
            //ParamStatus.Add("CODECLASSID", "Status");
            //ParamStatus.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtStatus = SqlExecuter.Query("GetCodeList", "00001", ParamStatus);
            //cboStatus.DataSource = dtStatus;


            // 유효상태
            cboValidState.DisplayMember = "CODENAME";
            cboValidState.ValueMember = "CODEID";
            cboValidState.ShowHeader = false;
            Dictionary<string, object> ParamValidState = new Dictionary<string, object>();
            ParamValidState.Add("CODECLASSID", "ValidState");
            ParamValidState.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtValidState = SqlExecuter.Query("GetCodeList", "00001", ParamValidState);
            cboValidState.DataSource = dtValidState;


            //ProductionType
            cboPRODUCTIONTYPE.DisplayMember = "CODENAME";
            cboPRODUCTIONTYPE.ValueMember = "CODEID";
            cboPRODUCTIONTYPE.ShowHeader = false;
            Dictionary<string, object> ParamcboPRODUCTIONTYPE = new Dictionary<string, object>();
            ParamcboPRODUCTIONTYPE.Add("CODECLASSID", "ProductionType");
            ParamcboPRODUCTIONTYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtPRODUCTIONTYPE = SqlExecuter.Query("GetCodeList", "00001", ParamcboPRODUCTIONTYPE);
            cboPRODUCTIONTYPE.DataSource = dtPRODUCTIONTYPE;


            //Uom
            cboUom.DisplayMember = "UOMDEFNAME";
            cboUom.ValueMember = "UOMDEFID";
            cboUom.ShowHeader = false;
            Dictionary<string, object> ParamUom = new Dictionary<string, object>();
            ParamUom.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
           //ParamUom.Add("PLANTID", UserInfo.Current.Plant);
            DataTable dtUom = SqlExecuter.Query("GetUOMList", "10001", ParamUom);
            cboUom.DataSource = dtUom;

            //Site
            cboSite1.DisplayMember = "PLANTNAME";
            cboSite1.ValueMember = "PLANTID";
            cboSite1.ShowHeader = false;
            Dictionary<string, object> ParamSite = new Dictionary<string, object>();
            ParamSite.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamSite.Add("PLANTID", "");
            ParamSite.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtSite = SqlExecuter.Query("GetPlantList", "10002", ParamSite);
            cboSite1.DataSource = dtSite;

            //ucItemPopup.CODE.Tag = "ASSEMBLYITEMID";
            //ucItemPopup.VERSION.Tag = "ASSEMBLYITEMVERSION";



            ConditionItemSelectPopup WarehouseCode = new ConditionItemSelectPopup();
            WarehouseCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            WarehouseCode.SetPopupLayout("WAREHOUSEID", PopupButtonStyles.Ok_Cancel);
            WarehouseCode.Id = "WAREHOUSEID";
            WarehouseCode.LabelText = "WAREHOUSEID";
            WarehouseCode.SearchQuery = new SqlQuery("GetWarehouseList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            WarehouseCode.IsMultiGrid = false;
            WarehouseCode.DisplayFieldName = "COMPLETEWAREHOUSENAME";
            WarehouseCode.ValueFieldName = "COMPLETEWAREHOUSEID";
            WarehouseCode.LanguageKey = "WAREHOUSEID";
            WarehouseCode.Conditions.AddTextBox("TXTWAREHOUSE");
            WarehouseCode.GridColumns.AddTextBoxColumn("COMPLETEWAREHOUSEID", 150);
            WarehouseCode.GridColumns.AddTextBoxColumn("COMPLETEWAREHOUSENAME", 200);
            spWarehouse.SelectPopupCondition = WarehouseCode;
            spWarehouse.Validated += SpWarehouse_Validated;

            ConditionItemSelectPopup LocationCode = new ConditionItemSelectPopup();
            LocationCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            LocationCode.SetPopupLayout("LOCATIONID", PopupButtonStyles.Ok_Cancel);
            LocationCode.Id = "LOCATIONID";
            LocationCode.LabelText = "LOCATIONID";
            LocationCode.SearchQuery = new SqlQuery("GetlocationList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"WAREHOUSEID={spWarehouse.GetValue()}");
            LocationCode.IsMultiGrid = false;
            LocationCode.DisplayFieldName = "LOCATIONNAME";
            LocationCode.ValueFieldName = "LOCATIONID";
            LocationCode.LanguageKey = "LOCATIONID";
            LocationCode.Conditions.AddTextBox("TXTLOCATION");
            LocationCode.GridColumns.AddTextBoxColumn("LOCATIONID", 150);
            LocationCode.GridColumns.AddTextBoxColumn("LOCATIONNAME", 200);
            spLocation.SelectPopupCondition = LocationCode;


           

        }

        private void SpWarehouse_Validated(object sender, EventArgs e)
        {
            ConditionItemSelectPopup LocationCode = new ConditionItemSelectPopup();
            LocationCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            LocationCode.SetPopupLayout("LOCATIONID", PopupButtonStyles.Ok_Cancel);
            LocationCode.Id = "LOCATIONID";
            LocationCode.LabelText = "LOCATIONID";
            LocationCode.SearchQuery = new SqlQuery("GetlocationList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"WAREHOUSEID={spWarehouse.GetValue()}");
            LocationCode.IsMultiGrid = false;
            LocationCode.DisplayFieldName = "LOCATIONNAME";
            LocationCode.ValueFieldName = "LOCATIONID";
            LocationCode.LanguageKey = "LOCATIONID";
            LocationCode.Conditions.AddTextBox("TXTLOCATION");
            LocationCode.GridColumns.AddTextBoxColumn("LOCATIONID", 150);
            LocationCode.GridColumns.AddTextBoxColumn("LOCATIONNAME", 200);
            spLocation.SelectPopupCondition = LocationCode;
        }
        #endregion

        #region 이벤트
        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
		{
            // 탭
            // 그리드 숨김
            //grdRouting.Hide();

            

            //treeRouting.FocusedNodeChanged += TreeRouting_FocusedNodeChanged;

            grdOperation.View.AddingNewRow += grdOperation_AddingNewRow;
            grdBomComp.View.AddingNewRow += grdBomComp_AddingNewRow;
            grdBomComp.View.CellValueChanging += grdBomComp_CellValueChanging;

            grdOperation.View.FocusedRowChanged += grdOperation_FocusedRowChanged;
            //grdOperation.View.Click += grdOperation_Click;
            // 툴 그리드
            grdTool.View.AddingNewRow += grdTool_AddingNewRow;
            // 특기사항 그리드
            grdOperationspecvalue.View.AddingNewRow += grdOperationspecvalue_AddingNewRow;

            grdRoutingAttributeValue.View.AddingNewRow += grdRoutingAttributeValue_AddingNewRow;

            btnSpecRegisterDetail.Click += BtnSpecRegisterDetail_Click;

            //grdOperation.View.FocusedColumnChanged += grdOperation_FocusedColumnChanged;
          
            new SetGridHeadDeleteDetail(grdOperation, new object[] { grdBomComp, grdTool, grdOperationspecvalue }, new string[] { "OPERATIONID", "OPERATIONSEQUENCE" }, new string[] { "OPERATIONID", "OPERATIONSEQUENCE" });
            //new SetGridHeadDeleteDetail(grdOperation, new object[] { grdBomComp, grdRoutingAttributeValue }, new string[] { "PROCESSSEGMENTID" });

            grdSpecattribute.View.FocusedRowChanged += grdSpecattribute_FocusedRowChanged;

        }

        private void grdSpecattribute_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            DataRow rowSpecattribute = grdSpecattribute.View.GetFocusedDataRow();
            if(rowSpecattribute != null)
            {
               if (rowSpecattribute["VALIDSTATE"].ToString() == "")
                {
                    grdSpecattribute.View.SetIsReadOnly(true);
                }
                else
                {
                    grdSpecattribute.View.SetIsReadOnly(false);
                }
            }
            
        }

        //private void grdOperation_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        //{
        //    switch (grdOperation.View.FocusedColumn.FieldName)
        //    {


        //        case "PROCESSSEGMENTID":
        //            DataTable dtBomComp = (DataTable)grdBomComp.DataSource;
        //            DataTable dtSpecattribute = (DataTable)grdSpecattribute.DataSource;
        //            DataTable dtTool = (DataTable)grdTool.DataSource;
        //            DataTable dtDff = (DataTable)grdOperationspecvalue.DataSource;
        //            DataTable dtRoutingAttributeValue = (DataTable)grdRoutingAttributeValue.DataSource;

        //            if(dtSpecattribute != null)
        //            {
        //                if (dtSpecattribute.Select("YN_AOI = 'Y'").Length != 0)
        //                {
        //                    grdOperation.View.SetIsReadOnly(true);

        //                }
        //            }
                    

        //            if (dtBomComp.Rows.Count != 0)
        //            {
        //                grdOperation.View.SetIsReadOnly(true);
        //            }

        //            if (dtTool.Rows.Count != 0)
        //            {
        //                grdOperation.View.SetIsReadOnly(true);
        //            }

        //            if (dtDff.Rows.Count != 0)
        //            {
        //                grdOperation.View.SetIsReadOnly(true);
        //            }

        //            if (dtRoutingAttributeValue.Rows.Count != 0)
        //            {
        //                grdOperation.View.SetIsReadOnly(true);
        //            }

        //            break;
        //        default:
        //            grdOperation.View.SetIsReadOnly(false);
        //            break;
        //    }
        //}



        private void BtnSpecRegisterDetail_Click(object sender, EventArgs e)
        {
            DataRow rowSpecattribute = grdSpecattribute.View.GetFocusedDataRow();
            DataRow focusRow = treeRouting.GetFocusedDataRow();
            DataRow dataRow = grdOperation.View.GetFocusedDataRow();


            DataTable dtSpecattribute = (DataTable)grdSpecattribute.DataSource;

            if(dtSpecattribute == null)
            {
                ShowMessage("RoutingInspection");
                return;
            }
            else
            {
                if(dtSpecattribute.Rows.Count ==0)
                {
                    ShowMessage("RoutingInspection");
                    return;
                }
            }


            DataTable dt = new DataTable();
            dt = ((DataTable)grdSpecattribute.DataSource).Clone();
            //dt.Columns.Add("RESOURCEID");
            //dt.Columns.Add("RESOURCEVERSION");
            dt.Columns.Add("ITEMUOM");
            dt.Columns.Add("OPERATIONID");
            dt.Columns.Add("OPERATIONSEQUENCE");

            DataRow row = dt.NewRow();
            foreach (DataColumn col in dt.Columns)
            {
                switch(col.ColumnName)
                {
                    case "RESOURCEID":
                        if (focusRow["MASTERDATACLASSID"].ToString() != "SubAssembly")
                        {
                            row["RESOURCEID"] = focusRow["PARENTS_ASSEMBLYITEMID"];
                        }
                        else
                        {
                            row["RESOURCEID"] = focusRow["ASSEMBLYITEMID"];
                        }
                        break;
                    case "RESOURCEVERSION":
                        if (focusRow["MASTERDATACLASSID"].ToString() != "SubAssembly")
                        {
                            row["RESOURCEVERSION"] = focusRow["PARENTS_ASSEMBLYITEMVERSION"];
                        }
                        else
                        {
                            row["RESOURCEVERSION"] = focusRow["ASSEMBLYITEMVERSION"];
                        }
                        break;
                    case "ITEMUOM":
                        row["ITEMUOM"] = focusRow["UOMDEFID"];
                        break;
                    case "OPERATIONID":
                        row["OPERATIONID"] = dataRow["OPERATIONID"];
                        break;
                    case "OPERATIONSEQUENCE":
                        row["OPERATIONSEQUENCE"] = dataRow["OPERATIONSEQUENCE"];
                        break;
                    case "PLANTID":
                        row["PLANTID"] = dataRow["PLANTID"];
                        break;
                    default:
                        row[col.ColumnName] = rowSpecattribute[col.ColumnName];
                        break;

                }

                
            }
            dt.Rows.Add(row);
            RoutingSpecRegisterDetailPopUp popup = new RoutingSpecRegisterDetailPopUp(dt.Rows[0]);
            popup.ShowDialog();

            fnSearch();
        }

        private void grdRoutingAttributeValue_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            DataRow focrow = grdOperation.View.GetFocusedDataRow();

            if (focrow == null)
            {
                args.IsCancel = true;
            }

            args.NewRow["ENTERPRISEID"] = focrow["ENTERPRISEID"];
            args.NewRow["PLANTID"] = focrow["PLANTID"];

            args.NewRow["PROCESSSEGMENTID"] = focrow["PROCESSSEGMENTID"];
            args.NewRow["PROCESSSEGMENTCLASSID"] = focrow["PROCESSSEGMENTCLASSID"];
            args.NewRow["USERSEQUENCE"] = focrow["USERSEQUENCE"];

            args.NewRow["ITEMID"] = focrow["ASSEMBLYITEMID"];
            args.NewRow["ITEMVERSION"] = focrow["ASSEMBLYITEMVERSION"];
            args.NewRow["ATTRIBUTECLASS"] = "Specification";
            args.NewRow["SEQUENCE"] = 0;
            args.NewRow["VALIDSTATE"] = "Valid";

            


        }

        private void grdOperationspecvalue_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow row = grdOperation.View.GetFocusedDataRow();

            args.NewRow["OPERATIONID"] = row["OPERATIONID"];
            args.NewRow["OPERATIONSEQUENCE"] = row["OPERATIONSEQUENCE"];
            args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            args.NewRow["PLANTID"] = row["PLANTID"];
            args.NewRow["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"];

            GetNumber number = new GetNumber();
            DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
            string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);

            string sSPECSEQUENCE = "";
            sSPECSEQUENCE = number.GetStdNumber("Operationspecvalue", "OSV" + sdate);
            args.NewRow["SEQUENCE"] = sSPECSEQUENCE;
            args.NewRow["VALIDSTATE"] = "Valid";
            

        }

        private void grdTool_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow row = this.grdOperation.View.GetFocusedDataRow();
            args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            args.NewRow["PLANTID"] = row["PLANTID"];
            args.NewRow["OPERATIONID"] = row["OPERATIONID"];
            args.NewRow["OPERATIONSEQUENCE"] = row["OPERATIONSEQUENCE"];
            //args.NewRow["RESOURCETYPE"] = "Equipment";

            args.NewRow["RESOURCESEQUENCE"] = 0;
            args.NewRow["SCHEDULESEQUENCE"] = 0;

            args.NewRow["ASSEMBLYITEMID"] = row["ASSEMBLYITEMID"];
            args.NewRow["ASSEMBLYITEMVERSION"] = row["ASSEMBLYITEMVERSION"];

            DataRow dataRowtree = treeRouting.GetFocusedDataRow();

            if (dataRowtree["MASTERDATACLASSID"].ToString() == "OperationItem")
            {
                args.NewRow["ASSEMBLYITEMID"] = dataRowtree["PARENTS_ASSEMBLYITEMID"];
                args.NewRow["ASSEMBLYITEMVERSION"] = dataRowtree["PARENTS_ASSEMBLYITEMVERSION"];
            }
            else
            {
                args.NewRow["ASSEMBLYITEMID"] = dataRowtree["ASSEMBLYITEMID"];
                args.NewRow["ASSEMBLYITEMVERSION"] = dataRowtree["ASSEMBLYITEMVERSION"];
            }



            args.NewRow["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"];
            

            // 순번 맥스 + 1
            object obj = this.grdTool.DataSource;
            DataTable dt = (DataTable)obj;
            DataRow[] row1 = dt.Select("1=1", "RESOURCESEQUENCE DESC");
            args.NewRow["RESOURCESEQUENCE"] = int.Parse(row1[0]["RESOURCESEQUENCE"].ToString()) + 10;
            args.NewRow["VALIDSTATE"] = "Valid";

            // 계획 순번 + 1
            object objs = this.grdTool.DataSource;
            DataTable dts = (DataTable)objs;
            DataRow[] rows = dt.Select("1=1", "SCHEDULESEQUENCE DESC");
            args.NewRow["SCHEDULESEQUENCE"] = int.Parse(rows[0]["SCHEDULESEQUENCE"].ToString()) + 1;

           
        }

        private void grdBomComp_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //////DataRow row = this.grdBomComp.View.GetFocusedDataRow();

            //////if(row.RowState == DataRowState.Added)
            //////{
            //////    grdBomComp.View.SetIsReadOnly(false);
            //////}
            //////else
            //////{
            //////    switch (grdBomComp.View.FocusedColumn.FieldName)
            //////    {
            //////        case "COMPONENTITEMCODE":
            //////        case "COMPONENTQTY":
            //////            grdBomComp.View.SetIsReadOnly(false);
            //////            break;
            //////        default:
                        
            //////            grdBomComp.View.SetIsReadOnly(true);
            //////            break;
            //////    }
            //////}
            
        }

        //private void grdOperation_Click(object sender, EventArgs e)
        //{
        //    if (grdOperation.View.FocusedRowHandle < 0)
        //        return;

        //    //grdBomComp.DataSource = null;
        //    //grdSpecattribute.DataSource = null;

        //    DataTable dtBomComp = (DataTable)grdBomComp.DataSource;
        //    if(dtBomComp != null)
        //    {
        //        dtBomComp.Clear();
        //    }

        //    DataTable dtSpecattribute = (DataTable)grdSpecattribute.DataSource;
        //    if (dtSpecattribute != null)
        //    {
        //        dtSpecattribute.Clear();
        //    }



        //    DataRow dataRow = grdOperation.View.GetFocusedDataRow();

        //    if (dataRow["OPERATIONID"].ToString() != "")
        //    {
        //        Dictionary<string, object> ParamComp = new Dictionary<string, object>();
        //        ParamComp.Add("ENTERPRISEID", dataRow["ENTERPRISEID"].ToString());
        //        ParamComp.Add("OPERATIONID", dataRow["OPERATIONID"].ToString());
        //        ParamComp.Add("OPERATIONSEQUENCE", dataRow["OPERATIONSEQUENCE"].ToString());

        //        ParamComp.Add("ASSEMBLYITEMID", dataRow["ASSEMBLYITEMID"].ToString());
        //        ParamComp.Add("ASSEMBLYITEMVERSION", dataRow["ASSEMBLYITEMVERSION"].ToString());


        //        DataTable dtComp = SqlExecuter.Query("GetBomcompmentlist", "10001", ParamComp);
        //        grdBomComp.DataSource = dtComp;
        //    }
          
        //    Dictionary<string, object> paramPS = new Dictionary<string, object>();
        //    paramPS.Add("ENTERPRISEID", dataRow["ENTERPRISEID"].ToString());
        //    paramPS.Add("PROCESSSEGMENTID", dataRow["PROCESSSEGMENTID"].ToString());
        //    paramPS.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
        //    paramPS.Add("VALIDSTATE", "Valid");

        //    DataRow focusRow = treeRouting.GetFocusedDataRow();

        //    switch (focusRow["MASTERDATACLASSID"].ToString())
        //    {
        //        case "SubAssembly":
        //        case "":
        //            paramPS.Add("RESOURCEID", focusRow["ASSEMBLYITEMID"]);
        //            paramPS.Add("RESOURCEVERSION", focusRow["ASSEMBLYITEMVERSION"]);
        //            break;
        //        default:
        //            paramPS.Add("RESOURCEID", focusRow["PARENTS_ASSEMBLYITEMID"]);
        //            paramPS.Add("RESOURCEVERSION", focusRow["PARENTS_ASSEMBLYITEMVERSION"]);
        //            break;
        //    }

        //    paramPS.Add("OPERATIONID", dataRow["OPERATIONID"].ToString());
        //    paramPS.Add("OPERATIONSEQUENCE", dataRow["OPERATIONSEQUENCE"].ToString());

        //    // 스펙 
        //    DataTable dtgrdSpecattribute = SqlExecuter.Query("GetRoutingSpecAttributelist", "10001", paramPS);

        //    //if (dtgrdSpecattribute.Rows.Count < 1) // 
        //    //{
        //    //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
        //    //}

        //    grdSpecattribute.DataSource = dtgrdSpecattribute;
        //}

        private void grdOperation_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (grdOperation.View.FocusedRowHandle < 0)
                return;

            //grdBomComp.DataSource = null;
            //grdSpecattribute.DataSource = null;

            DataTable dtBomComp = (DataTable)grdBomComp.DataSource;
            if (dtBomComp != null)
            {
                dtBomComp.Clear();
            }

            DataTable dtSpecattribute = (DataTable)grdSpecattribute.DataSource;
            if (dtSpecattribute != null)
            {
                dtSpecattribute.Clear();
            }

            DataTable dtTool = (DataTable)grdTool.DataSource;
            if (dtTool != null)
            {
                dtTool.Clear();
            }

            DataTable dtDff = (DataTable)grdOperationspecvalue.DataSource;
            if (dtDff != null)
            {
                dtDff.Clear();
            }


            DataTable dtRoutingAttributeValue = (DataTable)grdRoutingAttributeValue.DataSource;
            if (dtRoutingAttributeValue != null)
            {
                dtRoutingAttributeValue.Clear();
            }
            

            DataRow dataRow = grdOperation.View.GetFocusedDataRow();

            if (dataRow == null)
            {
                return;
            }

            if (dataRow["OPERATIONID"].ToString() != "")
            {
                DataRow focusRow = grdRouting.View.GetFocusedDataRow();


                Dictionary<string, object> ParamComp = new Dictionary<string, object>();
                ParamComp.Add("ENTERPRISEID", dataRow["ENTERPRISEID"].ToString());
                ParamComp.Add("OPERATIONID", dataRow["OPERATIONID"].ToString());
                ParamComp.Add("OPERATIONSEQUENCE", dataRow["OPERATIONSEQUENCE"].ToString());

            
                ParamComp.Add("ASSEMBLYITEMID", focusRow["ASSEMBLYITEMID"]);
                ParamComp.Add("ASSEMBLYITEMVERSION", focusRow["ASSEMBLYITEMVERSION"]);
            
               // ParamComp.Add("ASSEMBLYITEMID", dataRow["ASSEMBLYITEMID"].ToString());
               // ParamComp.Add("ASSEMBLYITEMVERSION", dataRow["ASSEMBLYITEMVERSION"].ToString());

                DataTable dtComp = SqlExecuter.Query("GetBomcompmentlist", "10001", ParamComp);
                grdBomComp.DataSource = dtComp;

                Dictionary<string, object> paramPS = new Dictionary<string, object>();
                paramPS.Add("ENTERPRISEID", dataRow["ENTERPRISEID"].ToString());
                paramPS.Add("PROCESSSEGMENTID", dataRow["PROCESSSEGMENTID"].ToString());
                paramPS.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                paramPS.Add("VALIDSTATE", "Valid");

                paramPS.Add("RESOURCEID", focusRow["ASSEMBLYITEMID"]);
                paramPS.Add("RESOURCEVERSION", focusRow["ASSEMBLYITEMVERSION"]);

                paramPS.Add("OPERATIONID", dataRow["OPERATIONID"].ToString());
                paramPS.Add("OPERATIONSEQUENCE", dataRow["OPERATIONSEQUENCE"].ToString());


                // 스펙 
                DataTable dtgrdSpecattribute = SqlExecuter.Query("GetRoutingSpecAttributelist", "10001", paramPS);
                grdSpecattribute.DataSource = dtgrdSpecattribute;
                


                //grdDff.DataSource = null;
                var values = Conditions.GetValues();

                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("OPERATIONID", dataRow["OPERATIONID"].ToString());
                Param.Add("OPERATIONSEQUENCE", dataRow["OPERATIONSEQUENCE"].ToString());
                Param.Add("ENTERPRISEID", dataRow["ENTERPRISEID"].ToString());
                Param.Add("PLANTID", dataRow["PLANTID"].ToString());
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                //Param.Add("ITEMID", values["ITEMID"]);
                //Param.Add("ITEMVERSION", values["ITEMVERSION"]);

                DataTable dtRationDurabledefinition = SqlExecuter.Query("GetOpeRationDurabledefinition", "10001", Param);
                grdTool.DataSource = dtRationDurabledefinition;


                DataTable dtOperationDff = SqlExecuter.Query("GetOperationspecvalue", "10001", Param);
                grdOperationspecvalue.DataSource = dtOperationDff;


                Dictionary<string, object> ParamValue = new Dictionary<string, object>();
                ParamValue.Add("ENTERPRISEID", dataRow["ENTERPRISEID"]);
                ParamValue.Add("PLANTID", dataRow["PLANTID"]);
                ParamValue.Add("PROCESSSEGMENTID", dataRow["PROCESSSEGMENTID"]);
                ParamValue.Add("PROCESSSEGMENTCLASSID", dataRow["PROCESSSEGMENTCLASSID"].ToString());

                ParamValue.Add("USERSEQUENCE", dataRow["USERSEQUENCE"].ToString());


                ParamValue.Add("ITEMID", focusRow["ASSEMBLYITEMID"].ToString());
                ParamValue.Add("ITEMVERSION", focusRow["ASSEMBLYITEMVERSION"].ToString());
                ParamValue.Add("ATTRIBUTECLASS", "Specification");

                DataTable dtValue = SqlExecuter.Query("GetProcessAttributeValueList", "10001", ParamValue);
                grdRoutingAttributeValue.DataSource = dtValue;


            }

            
        }

        private void grdBomComp_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow dataRow = grdOperation.View.GetFocusedDataRow();

            if(dataRow["PROCESSSEGMENTID"].ToString() == "")
            {
                ShowMessage("RequiredProcessSegId");
                args.IsCancel = true;
            }

            args.NewRow["ASSEMBLYBOMID"] = dataRow["ASSEMBLYBOMID"];
            args.NewRow["ENTERPRISEID"] = dataRow["ENTERPRISEID"];
            args.NewRow["OPERATIONID"] = dataRow["OPERATIONID"];
            args.NewRow["OPERATIONSEQUENCE"] = dataRow["OPERATIONSEQUENCE"];

            args.NewRow["PLANTID"] = dataRow["PLANTID"];

            args.NewRow["VALIDSTATE"] = "Valid";
            args.NewRow["ISREQUIREDMATERIAL"] = "Y";
            args.NewRow["ISAUTOREQUESTMATERIAL"] = "10";

            GetNumber number = new GetNumber();
            args.NewRow["COMPONENTBOMID"] = number.GetStdNumber("BomComponent", "");
            args.NewRow["COMPONENTSEQUENCE"] = dataRow["OPERATIONSEQUENCE"];



        }

        private void grdOperation_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

         
            DataRow dataRow = grdRouting.View.GetFocusedDataRow();

            if(dataRow == null)
            {
                ShowMessage("SiteSubItem");
                args.IsCancel = true;
            }

            args.NewRow["ENTERPRISEID"] = dataRow["ENTERPRISEID"];
            args.NewRow["PLANTID"] = dataRow["PLANTID"];

            string sASSEMBLYITEM = "";

            sASSEMBLYITEM = dataRow["ASSEMBLYITEMID"].ToString() + dataRow["ASSEMBLYITEMVERSION"].ToString();

            GetNumber number = new GetNumber();
            args.NewRow["OPERATIONID"] = number.GetStdNumber("Operation", sASSEMBLYITEM);
            args.NewRow["ASSEMBLYROUTINGID"] = dataRow["ASSEMBLYROUTINGID"];
            args.NewRow["VALIDSTATE"] = "Valid";
            args.NewRow["OPERATIONSEQUENCE"] = 0;

            var values = Conditions.GetValues();
            DataTable dtUom = SqlExecuter.Query("GetProductItemMaster", "10001", values);

            if (UserInfo.Current.Enterprise == "YOUNGPOONG")
            {
                args.NewRow["PROCESSUOM"] = "PNL";
            }

            args.NewRow["MAINPRODUCTID"] = dataRow["ASSEMBLYITEMID"];
            args.NewRow["MAINPRODUCTVERSION"] = dataRow["ASSEMBLYITEMVERSION"];


            
        }
        #endregion

        #region 조회조건 영역

        /// <summary>
        /// 조회조건 영역 초기화 시작
        /// </summary>
        protected override void InitializeCondition()
		{
			base.InitializeCondition();

            InitializeCondition_Popup();
            // 버전

            Conditions.AddComboBox("ITEMVERSION", new SqlQuery("GetItemVersion", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"),"ITEMVERSIONNAME", "ITEMVERSIONCODE").SetEmptyItem("","",true).SetValidationIsRequired();

            // 유효상태
            Conditions.AddComboBox("ROUTINGVALIDSTATE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem();

            // 트리초기화
            TreeListColumn tcKey = treeRouting.Columns.Add();
            tcKey.FieldName = "NAME1";
            tcKey.Caption = "SORT";
            tcKey.Visible = false;
            tcKey.VisibleIndex = 1;

        }


        /// <summary>
        /// Area Tree 초기화
        /// </summary>
        private void InitializeTree()
        {
            //dt.Clear(); 

            DataRow rowFocu = grdRouting.View.GetFocusedDataRow();

            treeRouting.SetResultCount(1);
            treeRouting.SetIsReadOnly();
           // treeRouting.SetEmptyRoot(rowFocu["ASSEMBLYITEMID"].ToString() + " " + rowFocu["ASSEMBLYITEMVERSION"].ToString(), "A");
            treeRouting.SetMember("NAME1", "CHILD", "PARENTS");

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", rowFocu["ENTERPRISEID"].ToString());
            param.Add("ASSEMBLYITEMID", rowFocu["ASSEMBLYITEMID"].ToString());
            param.Add("ASSEMBLYITEMVERSION", rowFocu["ASSEMBLYITEMVERSION"].ToString());
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetRountingTreeVer2", "10001", param);
            
            dttreeSet = dt;

            if (dt != null)
            {
                
                if (dt.Rows.Count !=0)
                {
                    treeRouting.DataSource = dt;
                    //treeRouting.PopulateColumns();

                    //treeRouting.ExpandAll();
                    treeRouting.ExpandToLevel(1);

                }
            }

        }

        /// <summary>
        /// 검색조건 품목팝업 
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("ITEMID", new SqlQuery("GetProductItemGroup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMID", "ITEMID")
               .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               .SetValidationIsRequired();

          

            parentPopupColumn.Conditions.AddComboBox("MASTERDATACLASSID", new SqlQuery("GetmasterdataclassList", "10001", $"ITEMOWNER={"Specifications"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MASTERDATACLASSNAME", "MASTERDATACLASSID");
           

            parentPopupColumn.Conditions.AddTextBox("ITEMID");
            parentPopupColumn.Conditions.AddTextBox("ITEMNAME");

            //팝업 그리드
            parentPopupColumn.GridColumns.AddTextBoxColumn("MASTERDATACLASSNAME", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);

        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartSelectPopupEdit Popupedit = Conditions.GetControl<SmartSelectPopupEdit>("ITEMID");
            Popupedit.Validated += Popupedit_Validated;

            SmartComboBox combobox = Conditions.GetControl<SmartComboBox>("ITEMVERSION");
            combobox.TextChanged += Combobox_TextChanged;
            
        }
       
        private void Popupedit_Validated(object sender, EventArgs e)
        {
            SmartSelectPopupEdit Popupedit = (SmartSelectPopupEdit)sender;


            string sItemcode = "";


            if (Popupedit.SelectedData.Count<DataRow>() == 0)
            {
                sItemcode = "-1";
            }

            foreach (DataRow row in Popupedit.SelectedData)
            {
                sItemcode = row["ITEMID"].ToString();

            }

            SmartComboBox combobox = Conditions.GetControl<SmartComboBox>("ITEMVERSION");
            combobox.DisplayMember = "ITEMVERSIONNAME";
            combobox.ValueMember = "ITEMVERSIONCODE";
            combobox.ShowHeader = false;
            Dictionary<string, object> ParamIv = new Dictionary<string, object>();
            ParamIv.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamIv.Add("PLANTID", UserInfo.Current.Plant);
            ParamIv.Add("ITEMID", sItemcode);
            DataTable dtIv = SqlExecuter.Query("GetItemVersion", "10001", ParamIv);

            combobox.DataSource = dtIv;

            
        }

        private void Combobox_TextChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit Popupedit = Conditions.GetControl<SmartSelectPopupEdit>("ITEMID");

            SmartComboBox cboitemVer = (SmartComboBox)sender;
            SmartComboBox cboOpertion = Conditions.GetControl<SmartComboBox>("OPERATION");



            string sItemcode = "";

            if (Popupedit.SelectedData.Count<DataRow>() == 0)
            {
                sItemcode = "-1";
            }

            foreach (DataRow row in Popupedit.SelectedData)
            {
                sItemcode = row["ITEMID"].ToString();

            }
            // 20190731 공정제외
            // cboOpertion.DisplayMember = "OPERATIONID";
            // cboOpertion.ValueMember = "OPERATIONID";
            // cboOpertion.ShowHeader = false;
            // Dictionary<string, object> ParamOpertion = new Dictionary<string, object>();
            // ParamOpertion.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //// ParamOpertion.Add("PLANTID", UserInfo.Current.Plant);
            // ParamOpertion.Add("ASSEMBLYITEMID", sItemcode);
            // ParamOpertion.Add("ASSEMBLYITEMVERSION", cboitemVer.EditValue);
            // DataTable dtValidState = SqlExecuter.Query("GetOpertionCombo", "10001", ParamOpertion);
            // cboOpertion.DataSource = dtValidState;

            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ITEMID", sItemcode);
            Param.Add("ITEMVERSION", cboitemVer.EditValue);
            Param.Add("ITEMNAME", "");
            Param.Add("SEARCH", "");

            DataTable dt = SqlExecuter.Query("GetProductItemMaster", "10001", Param);

            if(dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    txtItemCode.Text = dt.Rows[0]["ITEMID"].ToString();
                    txtItemName.Text = dt.Rows[0]["ITEMNAME"].ToString();
                    txtItemVer.Text = dt.Rows[0]["ITEMVERSION"].ToString();
                    cboSite1.EditValue = dt.Rows[0]["PLANTID"].ToString();
                    cboUom.EditValue = dt.Rows[0]["ITEMUOM"].ToString();
                    cboPRODUCTIONTYPE.EditValue = dt.Rows[0]["PRODUCTIONTYPE"].ToString();
                    
                }
            }
            
            

        }
        #endregion

        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            var values = Conditions.GetValues();

            GetControlsFrom confrom = new GetControlsFrom();

            GetNumber number = new GetNumber();
            // 채번 시리얼 존재 유무 체크
            DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
            string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);

            confrom.GetControlsFromGrid(smartSplitTableLayoutPanel4, grdRouting);

            //변경된 로우 데이터
            DataTable dtRouting = new DataTable();
            dtRouting = grdRouting.GetChangedRows();

            //현라우팅 로우 데이터
            DataRow dataRowRouting = grdRouting.View.GetFocusedDataRow();

            if (dtRouting.Rows.Count != 0)
            {

                //라우팅 정의
                DataTable dtPROCESSDEFINITION = new DataTable();
                dtPROCESSDEFINITION.Columns.Add("PROCESSDEFID");
                dtPROCESSDEFINITION.Columns.Add("PROCESSDEFVERSION");
                dtPROCESSDEFINITION.Columns.Add("PROCESSCLASSID");
                dtPROCESSDEFINITION.Columns.Add("PROCESSDEFNAME");
                dtPROCESSDEFINITION.Columns.Add("ENTERPRISEID");
                dtPROCESSDEFINITION.Columns.Add("PLANTID");
                dtPROCESSDEFINITION.Columns.Add("PROCESSDEFTYPE");
                dtPROCESSDEFINITION.Columns.Add("LEADTIME");
                dtPROCESSDEFINITION.Columns.Add("VERSIONSTATE");
                dtPROCESSDEFINITION.Columns.Add("REWORKITEMCONTROL");
                dtPROCESSDEFINITION.Columns.Add("REWORKSEGMENTCONTROL");
                dtPROCESSDEFINITION.Columns.Add("DESCRIPTION");
                dtPROCESSDEFINITION.Columns.Add("VALIDSTATE");
                dtPROCESSDEFINITION.Columns.Add("_STATE_");

                // 공정 테이블 생성
                DataTable dtOperation = new DataTable();
                dtOperation.Columns.Add("OPERATIONID");
                dtOperation.Columns.Add("OPERATIONSEQUENCE");
                dtOperation.Columns.Add("USERSEQUENCE");
                dtOperation.Columns.Add("ENTERPRISEID");
                dtOperation.Columns.Add("PLANTID");
                dtOperation.Columns.Add("ASSEMBLYROUTINGID");
                dtOperation.Columns.Add("VALIDSTATE");
                dtOperation.Columns.Add("_STATE_");

                dtOperation.Columns.Add("MAINPRODUCTID");
                dtOperation.Columns.Add("MAINPRODUCTVERSION");



                // 공정 데이터 생성
                //DataRow rowOperation = dtOperation.NewRow();
                //string sASSEMBLYITEM = "";

                //// 제품&반제품
                //sASSEMBLYITEM = dtRouting.Rows[0]["ASSEMBLYITEMID"].ToString() + dtRouting.Rows[0]["ASSEMBLYITEMVERSION"].ToString();

                //rowOperation["MAINPRODUCTID"] = dtRouting.Rows[0]["ASSEMBLYITEMID"].ToString();
                //rowOperation["MAINPRODUCTVERSION"] = dtRouting.Rows[0]["ASSEMBLYITEMVERSION"].ToString();

                //rowOperation["OPERATIONID"] = number.GetStdNumber("Operation", sASSEMBLYITEM);

                //rowOperation["OPERATIONSEQUENCE"] = "1";
                //rowOperation["USERSEQUENCE"] = "1";
                //rowOperation["ENTERPRISEID"] = dtRouting.Rows[0]["ENTERPRISEID"].ToString();
                //rowOperation["PLANTID"] = dtRouting.Rows[0]["PLANTID"].ToString();
                //rowOperation["ASSEMBLYROUTINGID"] = dtRouting.Rows[0]["ASSEMBLYROUTINGID"].ToString();

                //rowOperation["VALIDSTATE"] = "Valid";

                //rowOperation["_STATE_"] = dtRouting.Rows[0]["_STATE_"].ToString();

                //dtOperation.Rows.Add(rowOperation);

                //BOM 테이블 생성
                DataTable dtBom = new DataTable();
                dtBom.Columns.Add("ASSEMBLYBOMID", typeof(Int32));
                dtBom.Columns.Add("ENTERPRISEID");
                dtBom.Columns.Add("PLANTID");

                dtBom.Columns.Add("ASSEMBLYITEMID");
                dtBom.Columns.Add("ASSEMBLYITEMVERSION");
                dtBom.Columns.Add("ASSEMBLYITEMUOM");
                dtBom.Columns.Add("ASSEMBLYITEMCLASS");

                dtBom.Columns.Add("ASSEMBLYTYPE");
                dtBom.Columns.Add("VALIDSTATE");
                dtBom.Columns.Add("_STATE_");

                //BOM 데이터 생성
                DataRow rowBom = dtBom.NewRow();
                rowBom["ASSEMBLYBOMID"] = number.GetStdNumber("AssemblyBomId", "");
                rowBom["ENTERPRISEID"] = dtRouting.Rows[0]["ENTERPRISEID"].ToString();
                rowBom["PLANTID"] = dtRouting.Rows[0]["PLANTID"].ToString();
                rowBom["ASSEMBLYITEMID"] = dtRouting.Rows[0]["ASSEMBLYITEMID"].ToString();
                rowBom["ASSEMBLYITEMVERSION"] = dtRouting.Rows[0]["ASSEMBLYITEMVERSION"].ToString();
                rowBom["ASSEMBLYITEMUOM"] = cboUom.GetDataValue();
                rowBom["ASSEMBLYITEMCLASS"] = dtRouting.Rows[0]["ASSEMBLYITEMCLASS"].ToString();
                rowBom["ASSEMBLYTYPE"] = dtRouting.Rows[0]["ASSEMBLYITEMCLASS"].ToString();
                rowBom["_STATE_"] = dtRouting.Rows[0]["_STATE_"].ToString();
                rowBom["VALIDSTATE"] = "Valid";
                dtBom.Rows.Add(rowBom);


                Dictionary<string, object> parampf = new Dictionary<string, object>();
                parampf.Add("ENTERPRISEID", dtRouting.Rows[0]["ENTERPRISEID"].ToString());
                parampf.Add("PROCESSDEFID", dtRouting.Rows[0]["ASSEMBLYITEMID"].ToString());
                parampf.Add("PROCESSDEFVERSION", dtRouting.Rows[0]["ASSEMBLYITEMVERSION"].ToString());
                DataTable dtBomChk = SqlExecuter.Query("GetProcessdefinitionMainList", "10001", parampf);

                if (dtBomChk != null)
                {
                    if (dtBomChk.Rows.Count == 0)
                    {
                        // 반제품
                        DataRow rowPROCESSDEFINITION = dtPROCESSDEFINITION.NewRow();

                        rowPROCESSDEFINITION["PROCESSDEFID"] = dtRouting.Rows[0]["ASSEMBLYITEMID"];
                        rowPROCESSDEFINITION["PROCESSDEFVERSION"] = dtRouting.Rows[0]["ASSEMBLYITEMVERSION"];
                        rowPROCESSDEFINITION["PROCESSCLASSID"] = dtRouting.Rows[0]["ASSEMBLYITEMCLASS"]; // 제품 Product/ 반제품SubAssembly
                        rowPROCESSDEFINITION["PROCESSDEFNAME"] = dtRouting.Rows[0]["ASSEMBLYITEMNAME"]; //
                        rowPROCESSDEFINITION["ENTERPRISEID"] = dtRouting.Rows[0]["ENTERPRISEID"];
                        rowPROCESSDEFINITION["PLANTID"] = dtRouting.Rows[0]["PLANTID"];
                        rowPROCESSDEFINITION["PROCESSDEFTYPE"] = "Main";
                        rowPROCESSDEFINITION["LEADTIME"] = dtRouting.Rows[0]["LEADTIME"]; //
                        rowPROCESSDEFINITION["VERSIONSTATE"] = "Active";
                        rowPROCESSDEFINITION["VALIDSTATE"] = "Valid";
                        rowPROCESSDEFINITION["_STATE_"] = "added";

                        dtPROCESSDEFINITION.Rows.Add(rowPROCESSDEFINITION);
                    }
                }
                else
                {
                    // 반제품
                    DataRow rowPROCESSDEFINITION = dtPROCESSDEFINITION.NewRow();

                    rowPROCESSDEFINITION["PROCESSDEFID"] = dtRouting.Rows[0]["ASSEMBLYITEMID"];
                    rowPROCESSDEFINITION["PROCESSDEFVERSION"] = dtRouting.Rows[0]["ASSEMBLYITEMVERSION"];
                    rowPROCESSDEFINITION["PROCESSCLASSID"] = dtRouting.Rows[0]["ASSEMBLYITEMCLASS"]; // 제품 Product/ 반제품SubAssembly
                    rowPROCESSDEFINITION["PROCESSDEFNAME"] = dtRouting.Rows[0]["ASSEMBLYITEMNAME"]; //
                    rowPROCESSDEFINITION["ENTERPRISEID"] = dtRouting.Rows[0]["ENTERPRISEID"];
                    rowPROCESSDEFINITION["PLANTID"] = dtRouting.Rows[0]["PLANTID"];
                    rowPROCESSDEFINITION["PROCESSDEFTYPE"] = "Main";
                    rowPROCESSDEFINITION["LEADTIME"] = dtRouting.Rows[0]["LEADTIME"]; //
                    rowPROCESSDEFINITION["VERSIONSTATE"] = "Active";
                    rowPROCESSDEFINITION["VALIDSTATE"] = "Valid";
                    rowPROCESSDEFINITION["_STATE_"] = "added";

                    dtPROCESSDEFINITION.Rows.Add(rowPROCESSDEFINITION);
                }



                //dtItemserialI.TableName = "idclassserial";
                dtRouting.TableName = "routing";
                dtOperation.TableName = "operation";
                dtBom.TableName = "assemblybillofmaterial";
                dtPROCESSDEFINITION.TableName = "processdefinition";
                //dtItemserialI.Merge(dtBomChkI);

                DataSet dsChang = new DataSet();
                //dsChang.Tables.Add(dtItemserialI);
                dsChang.Tables.Add(dtRouting);
                dsChang.Tables.Add(dtOperation);
                dsChang.Tables.Add(dtBom);
                dsChang.Tables.Add(dtPROCESSDEFINITION);

                ExecuteRule("Routing", dsChang);

            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            ////오퍼
            //////////////////////////////////////////////////////////////////////////////////////////////////
            DataTable dtOperationup = grdOperation.GetChangedRows();
            if (dtOperationup.Rows.Count != 0)
            {
                //Routing 테이블 생성
                DataTable dtRoutingComp = new DataTable();
                //BOM 테이블 생성
                DataTable dtBom = new DataTable();

                // 공정 순번에서 추가시에 만 생성

                //Routing 테이블 컬럼생성
                dtRoutingComp.Columns.Add("ASSEMBLYROUTINGID");
                dtRoutingComp.Columns.Add("ENTERPRISEID");
                dtRoutingComp.Columns.Add("PLANTID");
                dtRoutingComp.Columns.Add("NEWREQUESTNO");
                dtRoutingComp.Columns.Add("ASSEMBLYITEMID");
                dtRoutingComp.Columns.Add("ASSEMBLYITEMVERSION");
                dtRoutingComp.Columns.Add("ASSEMBLYITEMCLASS");
                dtRoutingComp.Columns.Add("ENGINEERINGCHANGE");
                dtRoutingComp.Columns.Add("IMPLEMENTATIONDATE");
                dtRoutingComp.Columns.Add("COMMONROUTINGID");
                dtRoutingComp.Columns.Add("COMPLETIONWAREHOUSEID");
                dtRoutingComp.Columns.Add("COMPLETIONLOCATIONID");
                dtRoutingComp.Columns.Add("TOTALLEADTIME");
                dtRoutingComp.Columns.Add("_STATE_");
                dtRoutingComp.Columns.Add("VALIDSTATE");
               

                //Bom 테이블 컬럼생성
                dtBom.Columns.Add("ASSEMBLYBOMID", typeof(Int32));
                dtBom.Columns.Add("ENTERPRISEID");
                dtBom.Columns.Add("PLANTID");

                dtBom.Columns.Add("ASSEMBLYITEMID");
                dtBom.Columns.Add("ASSEMBLYITEMVERSION");
                dtBom.Columns.Add("ASSEMBLYITEMUOM");
                dtBom.Columns.Add("ASSEMBLYITEMCLASS");

                dtBom.Columns.Add("ASSEMBLYTYPE");
                dtBom.Columns.Add("_STATE_");
                dtBom.Columns.Add("VALIDSTATE");
              

                //라우팅 정의
                DataTable dtPROCESSDEFINITION = new DataTable();
                dtPROCESSDEFINITION.Columns.Add("PROCESSDEFID");
                dtPROCESSDEFINITION.Columns.Add("PROCESSDEFVERSION");
                dtPROCESSDEFINITION.Columns.Add("PROCESSCLASSID");
                dtPROCESSDEFINITION.Columns.Add("PROCESSDEFNAME");
                dtPROCESSDEFINITION.Columns.Add("ENTERPRISEID");
                dtPROCESSDEFINITION.Columns.Add("PLANTID");
                dtPROCESSDEFINITION.Columns.Add("PROCESSDEFTYPE");
                dtPROCESSDEFINITION.Columns.Add("LEADTIME");
                dtPROCESSDEFINITION.Columns.Add("VERSIONSTATE");
                dtPROCESSDEFINITION.Columns.Add("REWORKITEMCONTROL");
                dtPROCESSDEFINITION.Columns.Add("REWORKSEGMENTCONTROL");
                dtPROCESSDEFINITION.Columns.Add("DESCRIPTION");
                dtPROCESSDEFINITION.Columns.Add("VALIDSTATE");
                dtPROCESSDEFINITION.Columns.Add("_STATE_");

                //if(dtRouting != null)
                //{
                //    if(dtRouting.Rows.Count !=0)
                //    {
                //        if (dtRouting.Rows[0]["_STATE_"].ToString() == "added")
                //        {

                //            DataRow rowRoutingComp = dtRoutingComp.NewRow();
                //            string sRouting = number.GetStdNumber("Routing", "ROT" + sdate);

                //            rowRoutingComp["ASSEMBLYROUTINGID"] = sRouting;
                //            rowRoutingComp["ENTERPRISEID"] = dtRouting.Rows[0]["ENTERPRISEID"];
                //            rowRoutingComp["PLANTID"] = dtRouting.Rows[0]["PLANTID"];
                //            rowRoutingComp["NEWREQUESTNO"] = "";
                //            rowRoutingComp["ASSEMBLYITEMID"] = dtRouting.Rows[0]["ASSEMBLYITEMID"];
                //            rowRoutingComp["ASSEMBLYITEMVERSION"] = dtRouting.Rows[0]["ASSEMBLYITEMVERSION"];
                //            rowRoutingComp["ASSEMBLYITEMCLASS"] = dtRouting.Rows[0]["ASSEMBLYITEMCLASS"];
                //            rowRoutingComp["ENGINEERINGCHANGE"] = "";
                //            rowRoutingComp["IMPLEMENTATIONDATE"] = "";
                //            rowRoutingComp["COMMONROUTINGID"] = "";
                //            rowRoutingComp["COMPLETIONWAREHOUSEID"] = "";
                //            rowRoutingComp["COMPLETIONLOCATIONID"] = "";
                //            rowRoutingComp["TOTALLEADTIME"] = "";
                //            rowRoutingComp["_STATE_"] = "added";
                //            rowRoutingComp["VALIDSTATE"] = "Valid";

                //            dtRoutingComp.Rows.Add(rowRoutingComp);


                //            //BOM 데이터 생성
                //            DataRow rowBom = dtBom.NewRow();
                //            string sASSEMBLYBOMID = number.GetStdNumber("AssemblyBomId", "");
                //            rowBom["ASSEMBLYBOMID"] = sASSEMBLYBOMID;
                //            rowBom["ENTERPRISEID"] = dtRouting.Rows[0]["ENTERPRISEID"].ToString();
                //            rowBom["PLANTID"] = dtRouting.Rows[0]["PLANTID"].ToString();
                //            rowBom["ASSEMBLYITEMID"] = dtRouting.Rows[0]["ASSEMBLYITEMID"].ToString();
                //            rowBom["ASSEMBLYITEMVERSION"] = dtRouting.Rows[0]["ASSEMBLYITEMVERSION"].ToString();
                //            rowBom["ASSEMBLYITEMUOM"] = dtRouting.Rows[0]["ASSEMBLYITEMUOM"];
                //            rowBom["ASSEMBLYITEMCLASS"] = dtRouting.Rows[0]["ASSEMBLYITEMCLASS"].ToString();
                //            rowBom["ASSEMBLYTYPE"] = dtRouting.Rows[0]["ASSEMBLYITEMCLASS"].ToString();
                //            rowBom["_STATE_"] = "added";
                //            rowBom["VALIDSTATE"] = "Valid";
                //            dtBom.Rows.Add(rowBom);

                //            //if(rowBomComp["ASSEMBLYBOMID"].ToString() == "0")
                //            //{
                //            //rowBomComp["ASSEMBLYBOMID"] = dtBomChkI.Rows[0]["LASTSERIALNO"].ToString();
                //            //}


                //            foreach (DataRow row in dtOperationup.Rows)
                //            {
                //                row["ASSEMBLYBOMID"] = sASSEMBLYBOMID;
                //                row["ASSEMBLYROUTINGID"] = sRouting;
                //            }


                //            Dictionary<string, object> parampf = new Dictionary<string, object>();
                //            parampf.Add("ENTERPRISEID", dtRouting.Rows[0]["ENTERPRISEID"].ToString());
                //            parampf.Add("PROCESSDEFID", dtRouting.Rows[0]["ASSEMBLYITEMID"].ToString());
                //            parampf.Add("PROCESSDEFVERSION", dtRouting.Rows[0]["ASSEMBLYITEMVERSION"].ToString());
                //            DataTable dtBomChk = SqlExecuter.Query("GetProcessdefinitionMainList", "10001", parampf);

                //            if (dtBomChk != null)
                //            {
                //                if (dtBomChk.Rows.Count == 0)
                //                {
                //                    // 반제품
                //                    DataRow rowPROCESSDEFINITION = dtPROCESSDEFINITION.NewRow();

                //                    rowPROCESSDEFINITION["PROCESSDEFID"] = dtRouting.Rows[0]["ASSEMBLYITEMID"];
                //                    rowPROCESSDEFINITION["PROCESSDEFVERSION"] = dtRouting.Rows[0]["ASSEMBLYITEMVERSION"];
                //                    rowPROCESSDEFINITION["PROCESSCLASSID"] = dtRouting.Rows[0]["ASSEMBLYITEMCLASS"]; // 제품 Product/ 반제품SubAssembly
                //                    rowPROCESSDEFINITION["PROCESSDEFNAME"] = dtRouting.Rows[0]["ASSEMBLYITEMNAME"]; //
                //                    rowPROCESSDEFINITION["ENTERPRISEID"] = dtRouting.Rows[0]["ENTERPRISEID"];
                //                    rowPROCESSDEFINITION["PLANTID"] = dtRouting.Rows[0]["PLANTID"];
                //                    rowPROCESSDEFINITION["PROCESSDEFTYPE"] = "Main";
                //                    rowPROCESSDEFINITION["LEADTIME"] = dtRouting.Rows[0]["LEADTIME"]; //
                //                    rowPROCESSDEFINITION["VERSIONSTATE"] = "Active";
                //                    rowPROCESSDEFINITION["VALIDSTATE"] = "Valid";
                //                    rowPROCESSDEFINITION["_STATE_"] = "added";

                //                    dtPROCESSDEFINITION.Rows.Add(rowPROCESSDEFINITION);
                //                }
                //            }
                //            else
                //            {
                //                // 반제품
                //                DataRow rowPROCESSDEFINITION = dtPROCESSDEFINITION.NewRow();

                //                rowPROCESSDEFINITION["PROCESSDEFID"] = dtRouting.Rows[0]["ASSEMBLYITEMID"];
                //                rowPROCESSDEFINITION["PROCESSDEFVERSION"] = dtRouting.Rows[0]["ASSEMBLYITEMVERSION"];
                //                rowPROCESSDEFINITION["PROCESSCLASSID"] = dtRouting.Rows[0]["ASSEMBLYITEMCLASS"]; // 제품 Product/ 반제품SubAssembly
                //                rowPROCESSDEFINITION["PROCESSDEFNAME"] = dtRouting.Rows[0]["ASSEMBLYITEMNAME"]; //
                //                rowPROCESSDEFINITION["ENTERPRISEID"] = dtRouting.Rows[0]["ENTERPRISEID"];
                //                rowPROCESSDEFINITION["PLANTID"] = dtRouting.Rows[0]["PLANTID"];
                //                rowPROCESSDEFINITION["PROCESSDEFTYPE"] = "Main";
                //                rowPROCESSDEFINITION["LEADTIME"] = dtRouting.Rows[0]["LEADTIME"]; //
                //                rowPROCESSDEFINITION["VERSIONSTATE"] = "Active";
                //                rowPROCESSDEFINITION["VALIDSTATE"] = "Valid";
                //                rowPROCESSDEFINITION["_STATE_"] = "added";

                //                dtPROCESSDEFINITION.Rows.Add(rowPROCESSDEFINITION);
                //            }

                //        }
                //    }

                //}



                //DataTable dtOPERATION_tar = new DataTable();
                //dtOPERATION_tar.Columns.Add("OPERATIONID");
                //dtOPERATION_tar.Columns.Add("OPERATIONSEQUENCE");
                //dtOPERATION_tar.Columns.Add("USERSEQUENCE");
                //dtOPERATION_tar.Columns.Add("_STATE_");

                //DataRow rowFocu = grdRouting.View.GetFocusedDataRow();

                ////// 공장별 작업지시순서 최소 최대 가져오기

                ////Dictionary<string, object> param = new Dictionary<string, object>();
                ////param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                ////param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                ////param.Add("MASTERDATACLASSID", "OperationItem");

                ////DataTable dtminmax = SqlExecuter.Query("GetOperationMinmax", "10001", param);

                ////foreach (DataRow row in dtminmax.Rows)
                ////{

                ////    DataRow[] rowUserSeq = dttreeSet.Select("USERSEQUENCE <> '1' AND  PLANTID = '" + row["PLANTID"].ToString() + "' AND MASTERDATACLASSID = 'OperationItem' ", "USERSEQUENCE ASC");
                ////    if(rowUserSeq.Length !=0)
                ////    {
                ////        row["OPERATIONIDMIN"] = dttreeSet.Select("USERSEQUENCE <> '1' AND  PLANTID = '" + row["PLANTID"].ToString() + "' AND MASTERDATACLASSID = 'OperationItem' ", "USERSEQUENCE ASC")[0]["USERSEQUENCE"].ToString();
                ////        row["OPERATIONIDMAX"] = dttreeSet.Select("USERSEQUENCE <> '1' AND  PLANTID = '" + row["PLANTID"].ToString() + "' AND MASTERDATACLASSID = 'OperationItem'", "USERSEQUENCE DESC")[0]["USERSEQUENCE"].ToString();
                ////    }
                    
                ////}




                ////// 공장 맥스 가져오기
                ////DataRow[] rowmax = dtminmax.Select("PLANTID = '" + dtOperationup.Rows[0]["PLANTID"].ToString() + "'");

                ////// 현공장의  최대 값보다 큰 다른공장의 최소 단위  
                ////if (rowmax.Length != 0)
                ////{




                ////    if (rowmax[0]["OPERATIONIDMAX"].ToString() != "")
                ////    {



                ////        DataRow[] rowmin = dtminmax.Select("OPERATIONIDMIN > " + rowmax[0]["OPERATIONIDMAX"].ToString() + "", "OPERATIONIDMIN");

                ////        if (rowmin.Length != 0)
                ////        {
                ////            // 맥스 작업지시 번호
                ////            DataTable dtUserSeq = (DataTable)grdOperation.DataSource;

                ////            string sDelete = "'',";
                ////            // 삭제 리스트
                ////            foreach (DataRow rowDelete in dtOperationup.Select("_STATE_ = 'deleted'"))
                ////            {
                ////                sDelete = sDelete + "'" + rowDelete["USERSEQUENCE"].ToString() + "',";
                ////            }

                ////            sDelete = sDelete.Substring(0, sDelete.Length - 1);
                ////            // 삭제 제외
                ////            DataRow[] rowUserSeq = dtUserSeq.Select("USERSEQUENCE NOT IN (" + sDelete + ")  ", "USERSEQUENCE DESC");


                ////            // 예제 ((29 - (29 % 10 )) + 10) 
                ////            // 10 배수로 변경하고  + 10 하여 다음공장 의 최소 예상 순번 구하기
                ////            Int32 i32USERSEQUENCE_tar = ((Int32.Parse(rowUserSeq[0]["USERSEQUENCE"].ToString()) - (Int32.Parse(rowUserSeq[0]["USERSEQUENCE"].ToString()) % 10)) + 10);

                ////            // 다음공장의 현재 최소 순번 10 배수로 변경
                ////            Int32 i32USERSEQUENCE_soc = ((Int32.Parse(rowmin[0]["OPERATIONIDMIN"].ToString()) - (Int32.Parse(rowmin[0]["OPERATIONIDMIN"].ToString()) % 10)));

                ////            //30 - 40 예상 수번과 현재 순번의 차를 구함
                ////            Int32 cha = i32USERSEQUENCE_tar - i32USERSEQUENCE_soc;

                ////            if (cha != 0)
                ////            {
                ////                //Dictionary<string, object> paramUserSeq = new Dictionary<string, object>();
                ////                //paramUserSeq.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                ////                //paramUserSeq.Add("ASSEMBLYROUTINGID", rowFocu["ASSEMBLYROUTINGID"].ToString());
                ////                //paramUserSeq.Add("USERSEQUENCE", rowmax[0]["OPERATIONIDMAX"].ToString());
                ////                DataRow dataRowUserYn = treeRouting.GetFocusedDataRow();


                ////                if (dataRowUserYn["ASSEMBLYROUTINGID"].ToString() != "")
                ////                {
                ////                    DataRow[] rowOpUs = dttreeSet.Select("USERSEQUENCE > '" + rowmax[0]["OPERATIONIDMAX"].ToString() + "' AND MASTERDATACLASSID = 'OperationItem' ");
                ////                    foreach (DataRow rowOpus in rowOpUs)
                ////                    {
                ////                        DataRow rowOpusI = dtOPERATION_tar.NewRow();
                ////                        rowOpusI["OPERATIONID"] = rowOpus["OPERATIONID"];
                ////                        rowOpusI["OPERATIONSEQUENCE"] = rowOpus["OPERATIONSEQUENCE"];
                ////                        rowOpusI["USERSEQUENCE"] = rowOpus["USERSEQUENCE"];
                ////                        rowOpusI["_STATE_"] = "modified";
                ////                        rowOpusI.AcceptChanges();
                ////                        rowOpusI.SetModified();
                ////                    }
                ////                }

                ////            }
                ////        }
                ////    }
                ////}

                DataTable dtPROCESSPATH = new DataTable();
                dtPROCESSPATH.Columns.Add("PROCESSPATHID");
                dtPROCESSPATH.Columns.Add("ENTERPRISEID");
                dtPROCESSPATH.Columns.Add("PLANTID");
                dtPROCESSPATH.Columns.Add("PROCESSDEFID");
                dtPROCESSPATH.Columns.Add("PROCESSDEFVERSION");
                dtPROCESSPATH.Columns.Add("PROCESSSEGMENTID");
                dtPROCESSPATH.Columns.Add("PROCESSSEGMENTVERSION");
                dtPROCESSPATH.Columns.Add("PATHSEQUENCE");
                dtPROCESSPATH.Columns.Add("USERSEQUENCE");
                dtPROCESSPATH.Columns.Add("PATHTYPE");
                dtPROCESSPATH.Columns.Add("DESCRIPTION");
                dtPROCESSPATH.Columns.Add("VALIDSTATE");
                dtPROCESSPATH.Columns.Add("PROCESSUOM");
                dtPROCESSPATH.Columns.Add("_STATE_");
            

                foreach (DataRow dataRow in  dtOperationup.Rows)
                {
        
                    DataRow rowPROCESSPATH = dtPROCESSPATH.NewRow();
                    rowPROCESSPATH["PROCESSPATHID"] = dataRow["OPERATIONID"];

                    rowPROCESSPATH["ENTERPRISEID"] = dataRow["ENTERPRISEID"];
                    rowPROCESSPATH["PLANTID"] = dataRow["PLANTID"];
                    rowPROCESSPATH["PROCESSDEFID"] = dataRowRouting["ASSEMBLYITEMID"];
                    rowPROCESSPATH["PROCESSDEFVERSION"] = dataRowRouting["ASSEMBLYITEMVERSION"];

                    rowPROCESSPATH["PROCESSSEGMENTID"] = dataRow["PROCESSSEGMENTID"];
                    rowPROCESSPATH["PROCESSSEGMENTVERSION"] = "*";
                    rowPROCESSPATH["PATHSEQUENCE"] = dataRow["OPERATIONSEQUENCE"];
                    rowPROCESSPATH["USERSEQUENCE"] = dataRow["USERSEQUENCE"];
                    //rowPROCESSPATH["PATHTYPE"] = dataRow["OPERATIONID"];
                    rowPROCESSPATH["VALIDSTATE"] = dataRow["VALIDSTATE"];
                    rowPROCESSPATH["_STATE_"] = dataRow["_STATE_"];
                    dtPROCESSPATH.Rows.Add(rowPROCESSPATH);
                
                }

                dtPROCESSPATH.TableName = "processPath";

                //dtOPERATION_tar.TableName = "operUserSeq";
                dtOperationup.TableName = "operation";
                dtRoutingComp.TableName = "routing";
                dtBom.TableName = "assemblybillofmaterial";
                dtPROCESSDEFINITION.TableName = "processdefinition";

                DataSet ds = new DataSet();

                ds.Tables.Add(dtPROCESSPATH);
                ds.Tables.Add(dtOperationup);
                //ds.Tables.Add(dtOPERATION_tar);
                ds.Tables.Add(dtRoutingComp);
                ds.Tables.Add(dtBom);
                ds.Tables.Add(dtPROCESSDEFINITION);

                ExecuteRule("Operation", ds);

              
                // Startend 변경
                Dictionary<string, object> ParamStartend = new Dictionary<string, object>();
                ParamStartend.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                ParamStartend.Add("PROCESSDEFID", dataRowRouting["ASSEMBLYITEMID"]);
                ParamStartend.Add("PROCESSDEFVERSION", dataRowRouting["ASSEMBLYITEMVERSION"]);

                // 순번 변경
                DataTable dtminmaxProcesspath = SqlExecuter.Query("GetRoutingProcesspathList", "10001", ParamStartend);

                DataTable dtminmaxI = dtminmaxProcesspath.Copy();


                dtminmaxI.Columns.Add("_STATE_");
                //dtminmaxI.Columns["USERSEQUENCE"].DataType = typeof(Int32);


                DataRow[] drmin = dtminmaxI.Select("1=1", "USERSEQUENCE ASC");
                DataRow[] drmax = dtminmaxI.Select("1=1", "USERSEQUENCE DESC");

                // Normal 로변경
                foreach (DataRow rowminmax in dtminmaxI.Rows)
                {
                    rowminmax["PATHTYPE"] = "Normal";
                    rowminmax["_STATE_"] = "modified";
                }

                //  min 을 Start

                foreach (DataRow rowminmax in dtminmaxI.Select("PROCESSPATHID = '" + drmin[0]["PROCESSPATHID"].ToString() + "'"))
                {
                    rowminmax["PATHTYPE"] = "Start";
                    rowminmax["_STATE_"] = "modified";
                }
                foreach (DataRow rowminmax in dtminmaxI.Select("PROCESSPATHID = '" + drmax[0]["PROCESSPATHID"].ToString() + "'"))
                {
                    //  min max 패스번호가 같을 경우  StartEnd
                    if (drmin[0]["PROCESSPATHID"].ToString() == rowminmax["PROCESSPATHID"].ToString())
                    {
                        rowminmax["PATHTYPE"] = "StartEnd";
                        rowminmax["_STATE_"] = "modified";
                    }
                    else
                    {
                        //  max 인경우  End
                        rowminmax["PATHTYPE"] = "End";
                        rowminmax["_STATE_"] = "modified";
                    }

                }
                DataSet dsChangemaimax = new DataSet();
                dtminmaxI.TableName = "minmax";


                DataTable dttemp = new DataTable();
                dttemp.TableName = "temp";

                dsChangemaimax.Tables.Add(dtminmaxI);
                dsChangemaimax.Tables.Add(dttemp);

                ExecuteRule("ProcessPath", dsChangemaimax);
           }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Bomcomp 자재정보
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            DataTable dtBomComp = grdBomComp.GetChangedRows();

            if (dtBomComp.Rows.Count != 0)
            {
                DataTable dtBILLOFMATERIAL = new DataTable();

                dtBILLOFMATERIAL.Columns.Add("PRODUCTDEFID");
                dtBILLOFMATERIAL.Columns.Add("PRODUCTDEFVERSION");
                dtBILLOFMATERIAL.Columns.Add("PROCESSDEFID");
                dtBILLOFMATERIAL.Columns.Add("PROCESSDEFVERSION");
                dtBILLOFMATERIAL.Columns.Add("PROCESSSEGMENTID");
                dtBILLOFMATERIAL.Columns.Add("PROCESSSEGMENTVERSION");
                dtBILLOFMATERIAL.Columns.Add("MATERIALTYPE");
                dtBILLOFMATERIAL.Columns.Add("MATERIALDEFID");
                dtBILLOFMATERIAL.Columns.Add("MATERIALDEFVERSION");
                dtBILLOFMATERIAL.Columns.Add("ENTERPRISEID");
                dtBILLOFMATERIAL.Columns.Add("PLANTID");
                dtBILLOFMATERIAL.Columns.Add("UNIT");
                dtBILLOFMATERIAL.Columns.Add("QTY", typeof(decimal));
                dtBILLOFMATERIAL.Columns.Add("ISREQUIRED");
                dtBILLOFMATERIAL.Columns.Add("_STATE_");

                dtBILLOFMATERIAL.Columns.Add("VALIDSTATE");
                dtBILLOFMATERIAL.Columns.Add("WIPSUPPLYTYPE");




                dtBILLOFMATERIAL.Columns.Add("SEQUENCE");


                //DataRow dataRowtree = treeRouting.GetFocusedDataRow();
                DataRow dataRowtOperation = grdOperation.View.GetFocusedDataRow();

                foreach (DataRow rowBomComp in dtBomComp.Rows)
                {
                    DataRow rowBILLOFMATERIAL = dtBILLOFMATERIAL.NewRow();

                    rowBILLOFMATERIAL["PROCESSSEGMENTID"] = dataRowtOperation["PROCESSSEGMENTID"];
                    rowBILLOFMATERIAL["PROCESSSEGMENTVERSION"] = "*";
                    rowBILLOFMATERIAL["VALIDSTATE"] = "Valid";


                    rowBILLOFMATERIAL["WIPSUPPLYTYPE"] = rowBomComp["WIPSUPPLYTYPE"];



            
                    if (rowBomComp["COMPONENTITEMCLASS"].ToString() == "SubAssembly")
                    {
                        rowBILLOFMATERIAL["MATERIALTYPE"] = "Product";
                    }
                    else
                    {
                        rowBILLOFMATERIAL["MATERIALTYPE"] = "Consumable";
                    }
                    rowBILLOFMATERIAL["PRODUCTDEFID"] = dataRowRouting["ASSEMBLYITEMID"];
                    rowBILLOFMATERIAL["PRODUCTDEFVERSION"] = dataRowRouting["ASSEMBLYITEMVERSION"];
                    rowBILLOFMATERIAL["PROCESSDEFID"] = dataRowRouting["ASSEMBLYITEMID"];
                    rowBILLOFMATERIAL["PROCESSDEFVERSION"] = dataRowRouting["ASSEMBLYITEMVERSION"];
                    

                    rowBILLOFMATERIAL["MATERIALDEFID"] = rowBomComp["COMPONENTITEMID"];
                    rowBILLOFMATERIAL["MATERIALDEFVERSION"] = rowBomComp["COMPONENTITEMVERSION"];
                    rowBILLOFMATERIAL["ENTERPRISEID"] = rowBomComp["ENTERPRISEID"];
                    rowBILLOFMATERIAL["PLANTID"] = rowBomComp["PLANTID"];
                    rowBILLOFMATERIAL["UNIT"] = rowBomComp["COMPONENTUOM"];
                    rowBILLOFMATERIAL["QTY"] = rowBomComp["COMPONENTQTY"];
                    rowBILLOFMATERIAL["ISREQUIRED"] = rowBomComp["ISREQUIREDMATERIAL"];
                    rowBILLOFMATERIAL["_STATE_"] = rowBomComp["_STATE_"];

                    if (rowBomComp["BOMSEQUENCE"].ToString() == "")
                    {
                        rowBILLOFMATERIAL["SEQUENCE"] = dataRowtOperation["OPERATIONSEQUENCE"];
                    }
                    else
                    {
                        rowBILLOFMATERIAL["SEQUENCE"] = rowBomComp["BOMSEQUENCE"];
                    }

                    dtBILLOFMATERIAL.Rows.Add(rowBILLOFMATERIAL);
                }

                dtBILLOFMATERIAL.TableName = "billofmaterial";
                dtBomComp.TableName = "bomComponent";

                DataSet dataSetChg = new DataSet();
                dataSetChg.Tables.Add(dtBILLOFMATERIAL);
                dataSetChg.Tables.Add(dtBomComp);


                ExecuteRule("BomComponent", dataSetChg);
            }

            //tool 등록
            DataTable dtTool = grdTool.GetChangedRows();


            //-- 툴 등록-- //
            if (dtTool.Rows.Count != 0)
            {
                dtTool.Columns.Add("RESOURCEID");
                dtTool.Columns.Add("RESOURCEVERSION");
                dtTool.Columns.Add("RESOURCEIDVERSION");
                

                DataTable dtBillOfResource = new DataTable();
                dtBillOfResource.Columns.Add("PRODUCTDEFID");
                dtBillOfResource.Columns.Add("PRODUCTDEFVERSION");
                dtBillOfResource.Columns.Add("PROCESSDEFID");
                dtBillOfResource.Columns.Add("PROCESSDEFVERSION");
                dtBillOfResource.Columns.Add("PROCESSSEGMENTID");
                dtBillOfResource.Columns.Add("PROCESSSEGMENTVERSION");
                dtBillOfResource.Columns.Add("EQUIPMENTID");
                dtBillOfResource.Columns.Add("RESOURCETYPE");
                dtBillOfResource.Columns.Add("RESOURCECLASSID");
                dtBillOfResource.Columns.Add("RESOURCEID");
                dtBillOfResource.Columns.Add("RESOURCEVERSION");
                dtBillOfResource.Columns.Add("SEQUENCE");
                dtBillOfResource.Columns.Add("VALIDSTATE");
                dtBillOfResource.Columns.Add("ENTERPRISEID");
                dtBillOfResource.Columns.Add("PLANTID");
                dtBillOfResource.Columns.Add("ISPRIMARYRESOURCE");
                dtBillOfResource.Columns.Add("_STATE_");

                foreach (DataRow rowTool in dtTool.Rows)
                {
                    rowTool["RESOURCEID"] = rowTool["TOOLCODE"];
                    rowTool["RESOURCEVERSION"] = rowTool["TOOLVERSION"];
                    rowTool["RESOURCEIDVERSION"] = rowTool["TOOLVERSION"];


                    if (rowTool["_STATE_"].ToString() == "added")
                    {
                        rowTool["OPERATIONRESOURCEID"] = number.GetStdNumber("Operation", "ORS" + sdate); ;
                    }


                    if(rowTool["_STATE_"].ToString() == "modified")
                    {
                        if(rowTool["RESOURCEID"] != rowTool["RESOURCEIDOLD"])
                        {
                            DataRow rowBr = dtBillOfResource.NewRow();
                 
                            rowBr["PRODUCTDEFID"] = dataRowRouting["ASSEMBLYITEMID"];
                            rowBr["PRODUCTDEFVERSION"] = dataRowRouting["ASSEMBLYITEMVERSION"];
                            rowBr["PROCESSDEFID"] = dataRowRouting["ASSEMBLYITEMID"];
                            rowBr["PROCESSDEFVERSION"] = dataRowRouting["ASSEMBLYITEMVERSION"];
                       
                            rowBr["PROCESSSEGMENTID"] = rowTool["PROCESSSEGMENTID"];
                            rowBr["PROCESSSEGMENTVERSION"] = "*";
                            rowBr["EQUIPMENTID"] = "";
                            rowBr["RESOURCETYPE"] = "Durable";
                            rowBr["RESOURCECLASSID"] = "";
                            rowBr["RESOURCEID"] = rowTool["RESOURCEID"];                   //툴자원
                            rowBr["RESOURCEVERSION"] = rowTool["RESOURCEVERSION"];     //툴자원번호

                            rowBr["ENTERPRISEID"] = rowTool["ENTERPRISEID"];


                            Dictionary<string, object> param = new Dictionary<string, object>();
                            param.Add("ENTERPRISEID", rowBr["ENTERPRISEID"]);
                            param.Add("PRODUCTDEFID", rowBr["PROCESSDEFID"]);
                            param.Add("PROCESSDEFVERSION", rowBr["PROCESSDEFVERSION"]);
                            param.Add("PROCESSSEGMENTID", rowBr["PROCESSSEGMENTID"]);
                            param.Add("RESOURCETYPE", "Durable");


                            DataTable dtBillofResourceChk = SqlExecuter.Query("GetBillofResourceChk", "10001", param);


                            if (dtBillofResourceChk != null)
                            {
                                DataRow[] rowSeq = dtBillofResourceChk.Select
                                (
                                "PROCESSDEFID = '" + rowBr["PRODUCTDEFID"].ToString() + "' " +
                                "AND PROCESSDEFVERSION = '" + rowBr["PROCESSDEFVERSION"].ToString() + "' " +
                                "AND PROCESSSEGMENTID = '" + rowBr["PROCESSSEGMENTID"].ToString() + "' " +
                                "AND PRODUCTDEFID = '" + rowBr["PRODUCTDEFID"].ToString() + "' " +
                                "AND PRODUCTDEFVERSION = '" + rowBr["PRODUCTDEFVERSION"].ToString() + "' " +
                                "AND RESOURCEID = '" + rowBr["RESOURCEID"].ToString() + "' "
                                );

                                if (rowSeq.Length != 0)
                                {
                                    rowBr["SEQUENCE"] = rowSeq[0]["SEQUENCE"].ToString();
                                }
                                else
                                {
                                    rowBr["SEQUENCE"] = 1;
                                }
                            }
                            else
                            {
                                rowBr["SEQUENCE"] = 1;
                            }


                            rowBr["VALIDSTATE"] = "Valid";
                            rowBr["ISPRIMARYRESOURCE"] = "Y";
                            rowBr["_STATE_"] = "added";

                            rowBr["PLANTID"] = rowTool["PLANTID"];
                            dtBillOfResource.Rows.Add(rowBr);


                            DataRow rowBrD = dtBillOfResource.NewRow();
                    
                            rowBrD["PRODUCTDEFID"] = dataRowRouting["ASSEMBLYITEMID"];
                            rowBrD["PRODUCTDEFVERSION"] = dataRowRouting["ASSEMBLYITEMVERSION"];
                            rowBrD["PROCESSDEFID"] = dataRowRouting["ASSEMBLYITEMID"];
                            rowBrD["PROCESSDEFVERSION"] = dataRowRouting["ASSEMBLYITEMVERSION"];
                          
                            rowBrD["PROCESSSEGMENTID"] = rowTool["PROCESSSEGMENTID"];
                            rowBrD["PROCESSSEGMENTVERSION"] = "*";
                            rowBrD["EQUIPMENTID"] = "";
                            rowBrD["RESOURCETYPE"] = "Durable";
                            rowBrD["RESOURCECLASSID"] = "";
                            rowBrD["RESOURCEID"] = rowTool["RESOURCEIDOLD"];
                            rowBrD["RESOURCEVERSION"] = rowTool["RESOURCEVERSION"];

                            rowBrD["ENTERPRISEID"] = rowTool["ENTERPRISEID"];


                            Dictionary<string, object> paramDel = new Dictionary<string, object>();
                            paramDel.Add("ENTERPRISEID", rowBrD["ENTERPRISEID"]);
                            paramDel.Add("PRODUCTDEFID", rowBrD["PROCESSDEFID"]);
                            paramDel.Add("PROCESSDEFVERSION", rowBrD["PROCESSDEFVERSION"]);
                            paramDel.Add("PROCESSSEGMENTID", rowBrD["PROCESSSEGMENTID"]);
                            paramDel.Add("RESOURCETYPE", "Durable");


                            DataTable dtBillofResourceChkD = SqlExecuter.Query("GetBillofResourceChk", "10001", paramDel);


                            if (dtBillofResourceChkD != null)
                            {
                                DataRow[] rowSeq = dtBillofResourceChk.Select
                                (
                                "PROCESSDEFID = '" + rowBr["PRODUCTDEFID"].ToString() + "' " +
                                "AND PROCESSDEFVERSION = '" + rowBr["PROCESSDEFVERSION"].ToString() + "' " +
                                "AND PROCESSSEGMENTID = '" + rowBr["PROCESSSEGMENTID"].ToString() + "' " +
                                "AND PRODUCTDEFID = '" + rowBr["PRODUCTDEFID"].ToString() + "' " +
                                "AND PRODUCTDEFVERSION = '" + rowBr["PRODUCTDEFVERSION"].ToString() + "' " +
                                "AND RESOURCEID = '" + rowBr["RESOURCEID"].ToString() + "' " +
                                "AND RESOURCEVERSION = '" + rowBr["RESOURCEVERSION"].ToString() + "' "

                                );

                                if (rowSeq.Length != 0)
                                {
                                    rowBrD["SEQUENCE"] = rowSeq[0]["SEQUENCE"].ToString();
                                }
                                else
                                {
                                    rowBrD["SEQUENCE"] = 1;
                                }
                            }
                            else
                            {
                                rowBrD["SEQUENCE"] = 1;
                            }


                            rowBrD["VALIDSTATE"] = "Valid";
                            rowBrD["ISPRIMARYRESOURCE"] = "Y";
                            rowBrD["_STATE_"] = "deleted";

                            rowBrD["PLANTID"] = rowTool["PLANTID"];
                            dtBillOfResource.Rows.Add(rowBrD);

                        }
                        else
                        {
                            DataRow rowBr = dtBillOfResource.NewRow();
                    
                            rowBr["PRODUCTDEFID"] = dataRowRouting["ASSEMBLYITEMID"];
                            rowBr["PRODUCTDEFVERSION"] = dataRowRouting["ASSEMBLYITEMVERSION"];
                            rowBr["PROCESSDEFID"] = dataRowRouting["ASSEMBLYITEMID"];
                            rowBr["PROCESSDEFVERSION"] = dataRowRouting["ASSEMBLYITEMVERSION"];
                         
                            rowBr["PROCESSSEGMENTID"] = rowTool["PROCESSSEGMENTID"];
                            rowBr["PROCESSSEGMENTVERSION"] = "*";
                            rowBr["EQUIPMENTID"] = "";
                            rowBr["RESOURCETYPE"] = "Durable";
                            rowBr["RESOURCECLASSID"] = "";
                            rowBr["RESOURCEID"] = rowTool["RESOURCEID"];
                            rowBr["RESOURCEVERSION"] = rowTool["RESOURCEVERSION"];

                            rowBr["ENTERPRISEID"] = rowTool["ENTERPRISEID"];


                            Dictionary<string, object> param = new Dictionary<string, object>();
                            param.Add("ENTERPRISEID", rowBr["ENTERPRISEID"]);
                            param.Add("PRODUCTDEFID", rowBr["PROCESSDEFID"]);
                            param.Add("PROCESSDEFVERSION", rowBr["PROCESSDEFVERSION"]);
                            param.Add("PROCESSSEGMENTID", rowBr["PROCESSSEGMENTID"]);
                            param.Add("RESOURCETYPE", "Durable");


                            DataTable dtBillofResourceChk = SqlExecuter.Query("GetBillofResourceChk", "10001", param);


                            if (dtBillofResourceChk != null)
                            {
                                DataRow[] rowSeq = dtBillofResourceChk.Select
                                (
                                "PROCESSDEFID = '" + rowBr["PRODUCTDEFID"].ToString() + "' " +
                                "AND PROCESSDEFVERSION = '" + rowBr["PROCESSDEFVERSION"].ToString() + "' " +
                                "AND PROCESSSEGMENTID = '" + rowBr["PROCESSSEGMENTID"].ToString() + "' " +
                                "AND PRODUCTDEFID = '" + rowBr["PRODUCTDEFID"].ToString() + "' " +
                                "AND PRODUCTDEFVERSION = '" + rowBr["PRODUCTDEFVERSION"].ToString() + "' " +
                                "AND RESOURCEID = '" + rowBr["RESOURCEID"].ToString() + "' "
                                );

                                if (rowSeq.Length != 0)
                                {
                                    rowBr["SEQUENCE"] = rowSeq[0]["SEQUENCE"].ToString();
                                }
                                else
                                {
                                    rowBr["SEQUENCE"] = 1;
                                }
                            }
                            else
                            {
                                rowBr["SEQUENCE"] = 1;
                            }


                            rowBr["VALIDSTATE"] = "Valid";
                            rowBr["ISPRIMARYRESOURCE"] = "Y";
                            rowBr["_STATE_"] = rowTool["_STATE_"];

                            rowBr["PLANTID"] = rowTool["PLANTID"];
                            dtBillOfResource.Rows.Add(rowBr);
                        }

                    }
                    else
                    {
                        DataRow rowBr = dtBillOfResource.NewRow();
              
                        rowBr["PRODUCTDEFID"] = dataRowRouting["ASSEMBLYITEMID"];
                        rowBr["PRODUCTDEFVERSION"] = dataRowRouting["ASSEMBLYITEMVERSION"];
                        rowBr["PROCESSDEFID"] = dataRowRouting["ASSEMBLYITEMID"];
                        rowBr["PROCESSDEFVERSION"] = dataRowRouting["ASSEMBLYITEMVERSION"];
                   
                        rowBr["PROCESSSEGMENTID"] = rowTool["PROCESSSEGMENTID"];
                        rowBr["PROCESSSEGMENTVERSION"] = "*";
                        rowBr["EQUIPMENTID"] = "";
                        rowBr["RESOURCETYPE"] = "Durable";
                        rowBr["RESOURCECLASSID"] = "";
                        rowBr["RESOURCEID"] = rowTool["RESOURCEID"];
                        rowBr["RESOURCEVERSION"] = rowTool["RESOURCEVERSION"];
                        rowBr["ENTERPRISEID"] = rowTool["ENTERPRISEID"];


                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("ENTERPRISEID", rowBr["ENTERPRISEID"]);
                        param.Add("PRODUCTDEFID", rowBr["PROCESSDEFID"]);
                        param.Add("PROCESSDEFVERSION", rowBr["PROCESSDEFVERSION"]);
                        param.Add("PROCESSSEGMENTID", rowBr["PROCESSSEGMENTID"]);
                        param.Add("RESOURCETYPE", "Durable");


                        DataTable dtBillofResourceChk = SqlExecuter.Query("GetBillofResourceChk", "10001", param);


                        if (dtBillofResourceChk != null)
                        {
                            DataRow[] rowSeq = dtBillofResourceChk.Select
                            (
                            "PROCESSDEFID = '" + rowBr["PRODUCTDEFID"].ToString() + "' " +
                            "AND PROCESSDEFVERSION = '" + rowBr["PROCESSDEFVERSION"].ToString() + "' " +
                            "AND PROCESSSEGMENTID = '" + rowBr["PROCESSSEGMENTID"].ToString() + "' " +
                            "AND PRODUCTDEFID = '" + rowBr["PRODUCTDEFID"].ToString() + "' " +
                            "AND PRODUCTDEFVERSION = '" + rowBr["PRODUCTDEFVERSION"].ToString() + "' " +
                            "AND RESOURCEID = '" + rowBr["RESOURCEID"].ToString() + "' "
                            );

                            if (rowSeq.Length != 0)
                            {
                                rowBr["SEQUENCE"] = rowSeq[0]["SEQUENCE"].ToString();
                            }
                            else
                            {
                                rowBr["SEQUENCE"] = 1;
                            }
                        }
                        else
                        {
                            rowBr["SEQUENCE"] = 1;
                        }


                        rowBr["VALIDSTATE"] = "Valid";
                        rowBr["ISPRIMARYRESOURCE"] = "Y";
                        rowBr["_STATE_"] = rowTool["_STATE_"];

                        rowBr["PLANTID"] = rowTool["PLANTID"];
                        dtBillOfResource.Rows.Add(rowBr);
                    }

                  

                }


                DataSet dSchanged = new DataSet();

                dtTool.TableName = "operationresource";
                dSchanged.Tables.Add(dtTool);

                dtBillOfResource.TableName = "billOfResource";
                dSchanged.Tables.Add(dtBillOfResource);

                ExecuteRule("OperationResource", dSchanged);



            }


            // AOI 등록
            DataTable dtOperationspecvalue = grdOperationspecvalue.GetChangedRows();
            if (dtOperationspecvalue != null)
            {
                if (dtOperationspecvalue.Rows.Count != 0)
                {
                    ExecuteRule("Operationspecvalue", dtOperationspecvalue);
                }
            }

            // 외주단가 등록
            DataTable dtRoutingAttributeValue = grdRoutingAttributeValue.GetChangedRows();
            if(dtRoutingAttributeValue != null)
            {
                if (dtRoutingAttributeValue.Rows.Count != 0)
                {


                    DataRow row = grdOperation.View.GetFocusedDataRow();

                    foreach (DataRow rowNew in dtRoutingAttributeValue.Rows)
                    {
                        rowNew["ENTERPRISEID"] = row["ENTERPRISEID"];
                        rowNew["PLANTID"] = row["PLANTID"];
                        rowNew["ATTRIBUTECLASS"] = "Specification";
                        rowNew["VALIDSTATE"] = "Valid";
                        rowNew["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"];
                        rowNew["ITEMID"] = row["ASSEMBLYITEMID"];
                        rowNew["ITEMVERSION"] = row["ASSEMBLYITEMVERSION"];
                        rowNew["USERSEQUENCE"] = row["USERSEQUENCE"].ToString();
                        rowNew["SEQUENCE"] = 0;

                        

                        Dictionary<string, object> paramValue = new Dictionary<string, object>();
                        paramValue.Add("ITEMID", row["ASSEMBLYITEMID"]);
                        paramValue.Add("ITEMVERSION", row["ASSEMBLYITEMVERSION"]);
                        paramValue.Add("ENTERPRISEID", row["ENTERPRISEID"]);
                        paramValue.Add("PLANTID", row["PLANTID"]);
                        paramValue.Add("USERSEQUENCE", row["USERSEQUENCE"]);
                        paramValue.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);
                        DataTable dtValue = SqlExecuter.Query("GetProcessAttributeValueList", "10001", paramValue);

                        string sSeq = "";

                        if (dtValue != null)
                        {
                            if (dtValue.Rows.Count != 0)
                            {
                                DataRow[] rowSeq = dtValue.Select("1=1", "SEQUENCE DESC");
                                sSeq = rowSeq[0]["SEQUENCE"].ToString();
                                //rowNew["_STATE_"] = "modified";
                            }
                            else
                            {
                               // rowNew["_STATE_"] = "added";
                                sSeq = "0";
                            }
                        }
                        else
                        {
                           // rowNew["_STATE_"] = "added";
                            sSeq = "0";
                        }

                        decimal iseq = decimal.Parse(sSeq);
                        rowNew["SEQUENCE"] = iseq = iseq + 1;
                    }

                    ExecuteRule("ProcessAttributeValue", dtRoutingAttributeValue);
                }
            }

            //////// 스펙 등록 /////
            DataTable dtSpecattribute =  grdSpecattribute.GetChangedRows();
            if (dtSpecattribute != null)
            {
                if (dtSpecattribute.Rows.Count != 0)
                {
                   

                    DataTable dtINSPECTIONITEMREL = dtSpecattribute.Copy();
                    DataTable dtSPECDEFINITION = dtSpecattribute.Copy();

                    DataSet dschange = new DataSet();
                    dtINSPECTIONITEMREL.TableName = "sINSPECTIONITEMREL";
                    dtSPECDEFINITION.TableName = "sSPECDEFINITION";

                    dschange.Tables.Add(dtINSPECTIONITEMREL);
                    dschange.Tables.Add(dtSPECDEFINITION);

                    ExecuteRule("RoutingSaveSpecRegister", dschange);

                }
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
            fnSearch();
        }
		#endregion

        void fnSearch()
        {
            
            //그리드 초기화
            DataTable dtBomComp = (DataTable)grdBomComp.DataSource;
            if (dtBomComp != null)
            {
                dtBomComp.Clear();
            }

          

            DataTable dtOperation = (DataTable)grdOperation.DataSource;
            if (dtOperation != null)
            {
                dtOperation.Clear();
            }

            DataTable dtRouting = (DataTable)grdRouting.DataSource;
            if (dtRouting != null)
            {
                dtRouting.Clear();
            }

            DataTable dtTool = (DataTable)grdTool.DataSource;
            if (dtTool != null)
            {
                dtTool.Clear();
            }

            DataTable dtDff = (DataTable)grdOperationspecvalue.DataSource;
            if (dtDff != null)
            {
                dtDff.Clear();
            }

            DataTable dtRoutingAttributeValue = (DataTable)grdRoutingAttributeValue.DataSource;
            if (dtRoutingAttributeValue != null)
            {
                dtRoutingAttributeValue.Clear();
            }


            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            treeRouting.DataSource = null;
            

            DataTable dRouting = SqlExecuter.Query("GetRoutingVer2", "10001", values);

            grdRouting.DataSource = dRouting;

            if (dRouting.Rows.Count == 0)
            {
                DataTable dt = SqlExecuter.Query("GetProductItemMaster", "10001", values);

                //공장
                cboSite1.EditValue = dt.Rows[0]["PLANTID"].ToString();
                //단위
                cboUom.EditValue = dt.Rows[0]["ITEMUOM"].ToString();
                //고객명
                txtCUSTOMERNAME.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                //상태
                //cboStatus.EditValue = dt.Rows[0]["ITEMSTATUS"].ToString();
                //품목유형
                cboPRODUCTIONTYPE.EditValue = dt.Rows[0]["PRODUCTIONTYPE"].ToString();

                //ucItemPopup.CODE.Text = dt.Rows[0]["ITEMCODE"].ToString();
                //ucItemPopup.NAME.Text = dt.Rows[0]["ITEMCODE"].ToString();
                //ucItemPopup.VERSION.Text = dt.Rows[0]["ITEMVERSION"].ToString();


                grdRouting.View.AddNewRow();

                object objNew = this.grdRouting.DataSource;
                DataTable dtNew = (DataTable)objNew;

                dtNew.Rows[0]["ASSEMBLYITEMID"] = dt.Rows[0]["ITEMID"].ToString();
                dtNew.Rows[0]["ASSEMBLYITEMNAME"] = dt.Rows[0]["ITEMNAME"].ToString();
                dtNew.Rows[0]["ASSEMBLYITEMVERSION"] = dt.Rows[0]["ITEMVERSION"].ToString();
                dtNew.Rows[0]["ASSEMBLYITEMCLASS"] = dt.Rows[0]["MASTERDATACLASSID"].ToString();
                dtNew.Rows[0]["ENTERPRISEID"] = dt.Rows[0]["ENTERPRISEID"].ToString();
                dtNew.Rows[0]["PLANTID"] = dt.Rows[0]["PLANTID"].ToString();
                dtNew.Rows[0]["ITEMUOM"] = dt.Rows[0]["ITEMUOM"].ToString();
                dtNew.Rows[0]["VALIDSTATE"] = "Valid";

                // 채번 시리얼 존재 유무 체크
                DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
                string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);

                // 채번 시리얼 
                GetNumber number = new GetNumber();
                dtNew.Rows[0]["ASSEMBLYROUTINGID"] = number.GetStdNumber("Routing", "ROT" + sdate);
                dtNew.Rows[0]["ASSEMBLYBOMID"] = number.GetStdNumber("AssemblyBomId", "");
                dtNew.Rows[0]["ASSEMBLYITEMUOM"] = cboUom.GetDataValue();
                dtNew.Rows[0]["ASSEMBLYTYPE"] = dt.Rows[0]["MASTERDATACLASSID"].ToString();


            }


            txtSALEORDERAPPROVAL.Text = "";

            DataRow focusRow = grdRouting.View.GetFocusedDataRow();
            Dictionary<string, object> ParamSaleChk = new Dictionary<string, object>();
            ParamSaleChk.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamSaleChk.Add("PLANTID", values["P_PLANTID"].ToString());
            ParamSaleChk.Add("ITEMID", values["ITEMID"].ToString());
            ParamSaleChk.Add("ITEMVERSION", values["ITEMVERSION"].ToString());
            DataTable dtSoChk = SqlExecuter.Query("GetSaleorderapprovalChk", "10001", ParamSaleChk);
         
            if(dtSoChk != null)
            {
                if(dtSoChk.Rows.Count !=0)
                {
                    if(dtSoChk.Select("ISAPPROVAL = 'Y'").Length !=0)
                    {
                        txtSALEORDERAPPROVAL.Text = "Y";
                    }
                }
            }



            DataTable dtSpecattribute = (DataTable)grdSpecattribute.DataSource;
            if (dtSpecattribute != null)
            {
                dtSpecattribute.Clear();
            }
         

            if (focusRow != null)
            {

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("ASSEMBLYITEMID", focusRow["ASSEMBLYITEMID"].ToString());
                param.Add("ASSEMBLYITEMVERSION", focusRow["ASSEMBLYITEMVERSION"].ToString());
               
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("MASTERDATACLASSID", focusRow["MASTERDATACLASSID"].ToString());
                param.Add("VALIDSTATE", values["ROUTINGVALIDSTATE"]);

                grdOperation.DataSource = SqlExecuter.Query("GetOperationVer2list", "10001", param);
                grdOperation_FocusedRowChanged(null, null);

            }

            //라우팅 트리 초기화
            InitializeTree();

        }

        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
		{
			base.OnValidateContent();

            DataTable dtProcessSegmentClass = new DataTable();
            DataTable dtgrdProcessSegment = new DataTable();
            DataTable dtgrdProSegMEC = new DataTable();
            DataTable dtgrdSpecattribute = new DataTable();


            //수주사양 승인여부
            DataRow focusRow = grdRouting.View.GetFocusedDataRow();

            var values = Conditions.GetValues();
            DataTable dtPm = SqlExecuter.Query("GetProductItemMaster", "10001", values);

            Dictionary<string, object> ParamSaleChk = new Dictionary<string, object>();
            ParamSaleChk.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamSaleChk.Add("PLANTID", focusRow["PLANTID"].ToString());
            ParamSaleChk.Add("ITEMID", focusRow["ASSEMBLYITEMID"].ToString());
            ParamSaleChk.Add("ITEMVERSION", focusRow["ASSEMBLYITEMVERSION"].ToString());
            DataTable dtSoChk = SqlExecuter.Query("GetSaleorderapprovalChk", "10001", ParamSaleChk);

            // 제품일경우만 승인여부 체크
            if(dtPm.Rows[0]["MASTERDATACLASSID"].ToString() == "Product")
            {
                if (dtSoChk != null)
                {
                    if (dtSoChk.Rows.Count != 0)
                    {
                        if (dtSoChk.Select("ISAPPROVAL = 'Y'").Length != 0)
                        {
                            throw MessageException.Create("ApprovedSave");
                        }
                    }
                }
            }


            switch (smartTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    DataTable dbBomComp = new DataTable();
                    dbBomComp = grdBomComp.GetChangedRows();

                    if (dbBomComp.Rows.Count != 0)
                    {
                        grdBomComp.View.CheckValidation();
                    }
                    break;
                case 1:
                    //DataTable dtSpecattribute = grdSpecattribute.GetChangedRows();

                    //if (dtSpecattribute.Rows.Count != 0)
                    //{
                    //    if (dtSpecattribute.Select("YN_AOI = 'N'").Length != 0)
                    //    {
                    //        // 저장할 데이터가 존재하지 않습니다.
                    //        throw MessageException.Create("YN_AOI");
                    //    }
                    //}
                    break;

            }
            

           

            DataTable dbOperation = new DataTable();
            grdOperation.View.CheckValidation();
            dbOperation = grdOperation.GetChangedRows();
            if (dbOperation.Rows.Count != 0)
            {
                if(dbOperation.Select("USERSEQUENCE = ''").Length !=0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }

            }

        }
        #endregion

        #region private Fuction

        /// <summary>
		/// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑
		/// </summary>
		/// <param name="currentGridRow">화면 grid row</param>
		/// <param name="popupSelections">팝업에서 선택한 row</param>
		/// <returns></returns>
		private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationToolPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {

                DataRow focusRow = grdOperation.View.GetFocusedDataRow();

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", focusRow["ENTERPRISEID"]);
                param.Add("RESOURCEID", row["DURABLEDEFID"]);
                if (focusRow["MASTERDATACLASSID"].ToString() != "SubAssembly")
                {
                    param.Add("ASSEMBLYITEMID", focusRow["ASSEMBLYITEMID"]);
                    param.Add("ASSEMBLYITEMVERSION", focusRow["ASSEMBLYITEMVERSION"]);
                }
                else
                {
                    param.Add("ASSEMBLYITEMID", focusRow["ITEMID"]);
                    param.Add("ASSEMBLYITEMVERSION", focusRow["ITEMVERSION"]);
                }

                DataTable dtRoutingToolChk = SqlExecuter.Query("GetRoutingToolChk", "10001", param);

                if (dtRoutingToolChk.Rows.Count != 0)
                {
                    Language.LanguageMessageItem item = Language.GetMessage("SelectOverlap");
                    result.IsSucced = false;
                    result.FailMessage = item.Message;
                    result.Caption = item.Title;
                }
                else
                {
                   
                    currentGridRow["TOOLNAME"] = row["DURABLEDEFNAME"];
                    currentGridRow["TOOLVERSION"] = row["DURABLEDEFVERSION"];
                    currentGridRow["RESOURCETYPE"] = row["DURABLECLASSTYPE"];
                    currentGridRow["FILMUSELAYER"] = row["FILMUSELAYER"];
                }
            }
            return result;
        }

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 (표준공정)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationProcesssegMnetPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
		{
			Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();
          
            DataRow focusRow = grdRouting.View.GetFocusedDataRow();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            //param.Add("PARENTS_ASSEMBLYITEMID", focusRow["ASSEMBLYITEMID"]);
            //param.Add("PARENTS_ASSEMBLYITEMVERSION", focusRow["ASSEMBLYITEMVERSION"]);
         

            param.Add("ASSEMBLYITEMID", focusRow["ASSEMBLYITEMID"]);
            param.Add("ASSEMBLYITEMVERSION", focusRow["ASSEMBLYITEMVERSION"]);


            param.Add("PLANTID", "");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("MASTERDATACLASSID", focusRow["MASTERDATACLASSID"]);

            DataTable dtOperationlist = SqlExecuter.Query("GetOperationVer2list", "10001", param);

            foreach (DataRow row in popupSelections)
            {


                Dictionary<string, object> paramPS = new Dictionary<string, object>();
                paramPS.Add("ENTERPRISEID", currentGridRow["ENTERPRISEID"].ToString());
                paramPS.Add("PROCESSSEGMENTID", currentGridRow["PROCESSSEGMENTID"].ToString());
                paramPS.Add("OPERATIONID", currentGridRow["OPERATIONID"].ToString());
                paramPS.Add("OPERATIONSEQUENCE", currentGridRow["OPERATIONSEQUENCE"].ToString());

                DataTable dtSpecattribute = SqlExecuter.Query("GetRoutingSpecAttributelist", "10001", paramPS);

                if (dtSpecattribute != null)
                {
                    if (dtSpecattribute.Select("YN_AOI = 'Y'").Length != 0)
                    {
                        Language.LanguageMessageItem item = Language.GetMessage("GridDetailChk", row["PROCESSSEGMENTID"].ToString());
                        result.IsSucced = false;
                        result.FailMessage = item.Message;
                        result.Caption = item.Title;
                    }
                }


                DataTable dtComp = SqlExecuter.Query("GetBomcompmentlist", "10001", paramPS);

                if (dtComp != null)
                {
                    if (dtComp.Rows.Count != 0)
                    {
                        Language.LanguageMessageItem item = Language.GetMessage("GridDetailChk", row["PROCESSSEGMENTID"].ToString());
                        result.IsSucced = false;
                        result.FailMessage = item.Message;
                        result.Caption = item.Title;
                    }
                }


                DataTable dtRationDurabledefinition = SqlExecuter.Query("GetOpeRationDurabledefinition", "10001", paramPS);
                if (dtRationDurabledefinition != null)
                {
                    if (dtRationDurabledefinition.Rows.Count != 0)
                    {
                        Language.LanguageMessageItem item = Language.GetMessage("GridDetailChk", row["PROCESSSEGMENTID"].ToString());
                        result.IsSucced = false;
                        result.FailMessage = item.Message;
                        result.Caption = item.Title;
                    }
                }


                var values = Conditions.GetValues();


                Dictionary<string, object> ParamValue = new Dictionary<string, object>();
                ParamValue.Add("ENTERPRISEID", currentGridRow["ENTERPRISEID"]);
                ParamValue.Add("PLANTID", currentGridRow["PLANTID"]);
                ParamValue.Add("PROCESSSEGMENTID", currentGridRow["PROCESSSEGMENTID"]);
                //ParamValue.Add("PROCESSSEGMENTCLASSID", dataRow["PROCESSSEGMENTCLASSID"].ToString());

                ParamValue.Add("USERSEQUENCE", currentGridRow["USERSEQUENCE"].ToString());
                ParamValue.Add("ITEMID", values["ITEMID"].ToString());
                ParamValue.Add("ITEMVERSION", values["ITEMVERSION"].ToString());
                ParamValue.Add("ATTRIBUTECLASS", "Specification");

                DataTable dtAbValue = SqlExecuter.Query("GetProcessAttributeValueList", "10001", ParamValue);

                if (dtAbValue != null)
                {
                    if (dtAbValue.Rows.Count != 0)
                    {
                        Language.LanguageMessageItem item = Language.GetMessage("GridDetailChk", row["PROCESSSEGMENTID"].ToString());
                        result.IsSucced = false;
                        result.FailMessage = item.Message;
                        result.Caption = item.Title;
                    }
                }


                if (dtOperationlist.Select("PROCESSSEGMENTID = '" + row["PROCESSSEGMENTID"].ToString() + "'").Length != 0)
                {
                    Language.LanguageMessageItem item = Language.GetMessage("SelectOverlap", row["PROCESSSEGMENTID"].ToString());
                    result.IsSucced = false;
                    result.FailMessage = item.Message;
                    result.Caption = item.Title;
                }
                else
                {
                    currentGridRow["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
                }

               
            }
            return result;
		}

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationAreaPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["AREANAME"] = row["AREANAME"];

            }
            return result;
        }

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationWareHousePopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["COMPLETEWAREHOUSENAME"] = row["COMPLETEWAREHOUSENAME"];

                string sCOMPLETEWAREHOUSEID = row["COMPLETEWAREHOUSEID"].ToString();

                InitializeGrid_LocationPopup(sCOMPLETEWAREHOUSEID);

            }
            return result;
        }
        



        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationLocationPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["COMPLETELOCATIONNAME"] = row["LOCATIONNAME"];

            }
            return result;
        }

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationItemMasterPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["COMPONENTITEMNAME"] = row["ITEMNAME"];
                currentGridRow["COMPONENTITEMVERSION"] = row["ITEMVERSION"];
                currentGridRow["COMPONENTITEMCLASS"] = row["MASTERDATACLASSID"];
                currentGridRow["COMPONENTUOM"] = row["UOMDEFID"];
                
            }
            return result;
        }

        #endregion





    }
}

