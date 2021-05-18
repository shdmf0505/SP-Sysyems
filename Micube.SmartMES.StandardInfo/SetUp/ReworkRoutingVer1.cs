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
    /// 프로그램명 : 기준정보 > Setup > 품목규칙 등록
    /// 업 무 설명 : 품목 코드,명,계산식 등록
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// </summary> 
	public partial class ReworkRoutingVer1 : SmartConditionManualBaseForm
    {
        #region Local Variables

        private ConditionItemTextBox productDefIdBox;
        private int lastFocusedOperationRowIndex = 0;



        #endregion

        #region 생성자
        public ReworkRoutingVer1()
        {
            InitializeComponent();
            InitializeEvent();

        }

        #endregion

        #region 컨텐츠 영역 초기화


        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeReworkRoutingGrid()
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 재작업 라우팅 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            grdProcessdefinition.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;
            grdProcessdefinition.View.AddComboBoxColumn("PROCESSCLASSTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetValidationIsRequired();

            grdProcessdefinition.View.AddComboBoxColumn("PROCESSCLASSID_R", 100, new SqlQuery("GetProcessclassCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSCLASSNAME", "PROCESSCLASSID")
                .SetRelationIds("PROCESSCLASSTYPE").SetValidationIsRequired();

            grdProcessdefinition.View.AddComboBoxColumn("TOPPROCESSSEGMENTID", 100, new SqlQuery("GetProcessSegMentTop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID").
                SetValidationIsRequired();

            grdProcessdefinition.View.AddTextBoxColumn("PROCESSDEFID_R", 150).SetIsReadOnly();

            grdProcessdefinition.View.AddTextBoxColumn("PROCESSDEFVERSION_R", 150).SetIsReadOnly();
            grdProcessdefinition.View.AddTextBoxColumn("PROCESSDEFNAME_R", 150)
                .SetValidationIsRequired();

            grdProcessdefinition.View.AddTextBoxColumn("DESCRIPTION", 250).SetValidationIsRequired();


            grdProcessdefinition.View.AddTextBoxColumn("CREATORNAME", 80)
             .SetIsReadOnly()
             .SetTextAlignment(TextAlignment.Center);

            grdProcessdefinition.View.AddTextBoxColumn("CREATEDTIME", 130)
               // Display Format 지정
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center);

            grdProcessdefinition.View.AddTextBoxColumn("MODIFIERNAME", 80)
           .SetIsReadOnly()
           .SetTextAlignment(TextAlignment.Center);

            grdProcessdefinition.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
       
           ConditionItemComboBox cbReworkItemControl =  grdProcessdefinition.View.AddComboBoxColumn("REWORKITEMCONTROL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("N")
                .SetTextAlignment(TextAlignment.Center);

            grdProcessdefinition.View.AddComboBoxColumn("REWORKSEGMENTCONTROL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Y")
                .SetIsHidden()
                .SetTextAlignment(TextAlignment.Center);

            grdProcessdefinition.View.AddTextBoxColumn("VERSIONSTATE", 150).SetIsReadOnly();
            grdProcessdefinition.View.AddTextBoxColumn("PLANTID", 120).SetIsReadOnly();
            grdProcessdefinition.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdProcessdefinition.View.AddTextBoxColumn("PROCESSDEFTYPE", 250).SetIsHidden();
            grdProcessdefinition.View.AddTextBoxColumn("ENTERPRISEID", 250).SetIsHidden();
            grdProcessdefinition.View.AddTextBoxColumn("TEMPSEQ", 250).SetIsHidden();







            grdProcessdefinition.View.PopulateColumns();



            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 공정 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 공정
            grdOperation.GridButtonItem = GridButtonItem.All;
            grdOperation.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetValidationIsRequired()
                .SetDefault(UserInfo.Current.Plant);

            grdOperation.View.AddTextBoxColumn("USERSEQUENCE", 100).SetValidationIsRequired();

            InitializeGrid_ProcesssegmentPopup();

            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 250).SetIsReadOnly();

            InitializeGrid_OperationResourcePopup();

            grdOperation.View.AddTextBoxColumn("RESOURCENAME", 250).SetIsReadOnly();


            ConditionItemComboBox uomCombo = grdOperation.View.AddComboBoxColumn("PROCESSUOM", 80, new SqlQuery("GetUomDefinitionList", "10001", $"UOMCLASSID={"Process"}"), "UOMDEFID", "UOMDEFNAME")
                 .SetTextAlignment(TextAlignment.Center);

            if (UserInfo.Current.Enterprise == "INTERFLEX")
            {
                uomCombo.SetDefault("PCS")
                    .SetIsHidden();
            }
            else if (UserInfo.Current.Enterprise == "YOUNGPOONG")
            {
                uomCombo.SetDefault("PNL");
            }
            grdOperation.View.AddComboBoxColumn("ALTERNATIVERESOURCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetDefault("N");

            grdOperation.View.AddTextBoxColumn("COMMENT", 250);

            grdOperation.View.AddComboBoxColumn("ASSIGNEQUIPMENT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetDefault("N");


            grdOperation.View.AddTextBoxColumn("CREATOR", 80)
   .SetIsReadOnly()
   .SetTextAlignment(TextAlignment.Center);

            grdOperation.View.AddTextBoxColumn("CREATEDTIME", 130)
               // Display Format 지정
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center);

            grdOperation.View.AddTextBoxColumn("MODIFIER", 80)
           .SetIsReadOnly()
           .SetTextAlignment(TextAlignment.Center);

            grdOperation.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdOperation.View.AddTextBoxColumn("PROCESSPATHID", 250).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("RESOURCEVERSION", 250).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("ENTERPRISEID", 250).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("PROCESSDEFID", 250).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("PROCESSDEFVERSION", 250).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("PATHSEQUENCE", 250).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("PATHTYPE", 250).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("VALIDSTATE", 250).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("ISPRIMARYRESOURCE", 250).SetIsHidden();

            grdOperation.View.PopulateColumns();


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //// 제품 그리드 초기화
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdReworkProduct.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;
            InitializeGrid_ReworkProductPopup();
            grdReworkProduct.View.AddTextBoxColumn("RESOURCENAME", 300)
                .SetIsReadOnly().SetLabel("PRODUCTDEFNAME");
            grdReworkProduct.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsReadOnly().SetLabel("PRODUCTDEFVERSION");
            grdReworkProduct.View.AddTextBoxColumn("DESCRIPTION", 250);


            grdReworkProduct.View.AddTextBoxColumn("CREATOR", 80)
   .SetIsReadOnly()
   .SetTextAlignment(TextAlignment.Center);

            grdReworkProduct.View.AddTextBoxColumn("CREATEDTIME", 130)
               // Display Format 지정
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center);

            grdReworkProduct.View.AddTextBoxColumn("MODIFIER", 80)
           .SetIsReadOnly()
           .SetTextAlignment(TextAlignment.Center);

            grdReworkProduct.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdReworkProduct.View.AddTextBoxColumn("PROCESSDEFID", 250).SetIsHidden();
            grdReworkProduct.View.AddTextBoxColumn("PROCESSDEFVERSION", 250).SetIsHidden();
            grdReworkProduct.View.AddTextBoxColumn("CONTROLTYPE", 250).SetIsHidden();
            grdReworkProduct.View.AddTextBoxColumn("SEQUENCE", 250).SetIsHidden();
            grdReworkProduct.View.AddTextBoxColumn("ENTERPRISEID", 250).SetIsHidden();
            grdReworkProduct.View.AddTextBoxColumn("PLANTID", 250).SetIsHidden();
            grdReworkProduct.View.AddTextBoxColumn("VALIDSTATE", 250).SetIsHidden();



            grdReworkProduct.View.PopulateColumns();


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //// 작업장 그리드 초기화
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdReworkArea.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;
            InitializeGrid_ReworkAreaPopup();
            grdReworkArea.View.AddTextBoxColumn("RESOURCENAME", 300).SetIsReadOnly().SetLabel("AREANAME");
            grdReworkArea.View.AddTextBoxColumn("DESCRIPTION", 250);


            grdReworkArea.View.AddTextBoxColumn("CREATOR", 80)
   .SetIsReadOnly()
   .SetTextAlignment(TextAlignment.Center);

            grdReworkArea.View.AddTextBoxColumn("CREATEDTIME", 130)
               // Display Format 지정
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center);

            grdReworkArea.View.AddTextBoxColumn("MODIFIER", 80)
           .SetIsReadOnly()
           .SetTextAlignment(TextAlignment.Center);

            grdReworkArea.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdReworkArea.View.AddTextBoxColumn("PROCESSDEFID", 250).SetIsHidden();
            grdReworkArea.View.AddTextBoxColumn("PROCESSDEFVERSION", 250).SetIsHidden();
            grdReworkArea.View.AddTextBoxColumn("CONTROLTYPE", 250).SetIsHidden();
            grdReworkArea.View.AddTextBoxColumn("SEQUENCE", 250).SetIsHidden();
            grdReworkArea.View.AddTextBoxColumn("RESOURCEVERSION", 250).SetIsHidden();
            grdReworkArea.View.AddTextBoxColumn("ENTERPRISEID", 250).SetIsHidden();
            grdReworkArea.View.AddTextBoxColumn("PLANTID", 250).SetIsHidden();
            grdReworkArea.View.AddTextBoxColumn("VALIDSTATE", 250).SetIsHidden();


            grdReworkArea.View.PopulateColumns();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 자재 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdConsumable.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;

            InitializeGrid_ConsumablePopup();
            grdConsumable.View.AddTextBoxColumn("MATERIALDEFVERSION", 100)
                .SetIsReadOnly();
            grdConsumable.View.AddTextBoxColumn("MATERIALDEFNAME", 300)
                .SetIsReadOnly();
            grdConsumable.View.AddTextBoxColumn("UNIT", 80).SetIsReadOnly();

            grdConsumable.View.AddSpinEditColumn("QTY", 120).SetDisplayFormat("#,##0.#########").SetLabel("COMPONENTQTY").SetValidationIsRequired();



            grdConsumable.View.AddTextBoxColumn("CREATOR", 80)
   .SetIsReadOnly()
   .SetTextAlignment(TextAlignment.Center);

            grdConsumable.View.AddTextBoxColumn("CREATEDTIME", 130)
               // Display Format 지정
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center);

            grdConsumable.View.AddTextBoxColumn("MODIFIER", 80)
           .SetIsReadOnly()
           .SetTextAlignment(TextAlignment.Center);

            grdConsumable.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdConsumable.View.AddTextBoxColumn("PRODUCTDEFID", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("PRODUCTDEFVERSION", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("PROCESSDEFID", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("PROCESSDEFVERSION", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("PROCESSSEGMENTID", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("MATERIALTYPE", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("SEQUENCE", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("ENTERPRISEID", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("PLANTID", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("ISALTERABLE", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("MAINMATERIALDEFID", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("MAINMATERIALDEFVERSION", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("DESCRIPTION", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("VALIDSTATE", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("WIPSUPPLYTYPE", 250).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("P_PRODUCTDEFID", 250).SetIsHidden();
            

            grdConsumable.View.PopulateColumns();

            //자원관리 팝업
            grdResource.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;
            InitializeGrid_ResourcePopup();
            grdResource.View.AddTextBoxColumn("RESOURCENAME", 300).SetIsReadOnly();



            grdResource.View.AddTextBoxColumn("CREATOR", 80)
   .SetIsReadOnly()
   .SetTextAlignment(TextAlignment.Center);

            grdResource.View.AddTextBoxColumn("CREATEDTIME", 130)
               // Display Format 지정
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center);

            // grdResource.View.AddTextBoxColumn("MODIFIER", 80)
            //.SetIsReadOnly()
            //.SetTextAlignment(TextAlignment.Center);

            // grdResource.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            //     .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            //     .SetIsReadOnly()
            //     .SetTextAlignment(TextAlignment.Center);




            grdResource.View.AddTextBoxColumn("PRODUCTDEFID", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("PRODUCTDEFVERSION", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("PROCESSDEFID", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("PROCESSDEFVERSION", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("PROCESSSEGMENTID", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("EQUIPMENTID", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("RESOURCETYPE", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("RESOURCECLASSID", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("RESOURCEVERSION", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("EQUIPMENTCLASSID", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("AREAID", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("AREANAME", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("SEQUENCE", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("VALIDSTATE", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("ENTERPRISEID", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("PLANTID", 250).SetIsHidden();
            grdResource.View.AddTextBoxColumn("ISPRIMARYRESOURCE", 250).SetIsHidden();


            grdResource.View.PopulateColumns();


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////  설비 그리드 초기화
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdEquipment.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;
            InitializeGrid_EquipmentPopup();
            grdEquipment.View.AddTextBoxColumn("RESOURCENAME", 300).SetLabel("EQUIPMENTNAME").SetIsReadOnly();


            grdEquipment.View.AddTextBoxColumn("CREATOR", 80)
           .SetIsReadOnly()
           .SetTextAlignment(TextAlignment.Center);

            grdEquipment.View.AddTextBoxColumn("CREATEDTIME", 130)
               // Display Format 지정
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center);

            // grdEquipment.View.AddTextBoxColumn("MODIFIER", 80)
            //.SetIsReadOnly()
            //.SetTextAlignment(TextAlignment.Center);

            // grdEquipment.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            //     .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            //     .SetIsReadOnly()
            //     .SetTextAlignment(TextAlignment.Center);


            grdEquipment.View.AddTextBoxColumn("PRODUCTDEFID", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("PRODUCTDEFVERSION", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("PROCESSDEFID", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("PROCESSDEFVERSION", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("PROCESSSEGMENTID", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("RESOURCETYPE", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("RESOURCECLASSID", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("RESOURCEVERSION", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("SEQUENCE", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("VALIDSTATE", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("ENTERPRISEID", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("PLANTID", 250).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("ISPRIMARYRESOURCE", 250).SetIsHidden();


            grdEquipment.View.PopulateColumns();




        }

        // 자원관리 팝업
        private void InitializeGrid_OperationResourcePopup()
        {

            var parentCodeClassPopupColumn = this.grdOperation.View.AddSelectPopupColumn("RESOURCEID", 150, new SqlQuery("GetResourcePopup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성0화 여부, 자동 검색 여부
                .SetPopupLayout("RESOURCE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                .SetValidationIsRequired()
                // 그리드의 남은 영역을 채울 컬럼 설정
                //.SetPopupAutoFillColumns("CODECLASSNAME")
                // Validation 이 필요한 경우 호출할 Method 지정
                //.SetRelationIds("PROCESSSEGMENTID")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["RESOURCEID"] = row["RESOURCEID"].ToString();
                        dataGridRow["RESOURCENAME"] = row["DESCRIPTION"].ToString();
                        dataGridRow["RESOURCEVERSION"] = "*";
                        dataGridRow["ISPRIMARYRESOURCE"] = "Y";
                    }
                });



            // 팝업에서 사용할 조회조건 항목 추가

            parentCodeClassPopupColumn.Conditions.AddTextBox("RESOURCEID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("DESCRIPTION");
            parentCodeClassPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTID")
                 .SetPopupDefaultByGridColumnId("PROCESSSEGMENTID")
                 .SetIsHidden();

            parentCodeClassPopupColumn.Conditions.AddTextBox("PLANTID")
                .SetPopupDefaultByGridColumnId("PLANTID")
                .SetIsHidden();


            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("RESOURCEID", 90);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("DESCRIPTION", 220);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 80);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 300);

        }



        // 자원관리 팝업
        private void InitializeGrid_ResourcePopup()
        {

            var parentCodeClassPopupColumn = this.grdResource.View.AddSelectPopupColumn("RESOURCEID", 150, new SqlQuery("GetResourcePopup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성0화 여부, 자동 검색 여부
                .SetPopupLayout("RESOURCE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                .SetValidationIsRequired()
                // 그리드의 남은 영역을 채울 컬럼 설정
                //.SetPopupAutoFillColumns("CODECLASSNAME")
                // Validation 이 필요한 경우 호출할 Method 지정
                //.SetRelationIds("PROCESSSEGMENTID")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
               
                    if (selectedRows.Count() > 0)
                    {
                      
                        DataTable dt2 = this.grdResource.DataSource as DataTable;
                        int handle = this.grdResource.View.GetFocusedDataSourceRowIndex();

                        DataRow drReworkRouting = this.grdProcessdefinition.View.GetFocusedDataRow();
                        DataRow rowProcessPath = grdOperation.View.GetFocusedDataRow();

                        // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                        // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                        foreach (DataRow row in selectedRows)
                        {

                            DataRow dr = null;

                            if (dt2.Rows.Count < handle + 1)
                            {
                                dr = dt2.NewRow();
                                dt2.Rows.Add(dr);
                            }
                            else
                            {
                                dr = this.grdResource.View.GetDataRow(handle);
                            }

                            dr["RESOURCEID"] = row["RESOURCEID"].ToString();
                            dr["RESOURCENAME"] = row["DESCRIPTION"].ToString();
                            dr["AREAID"] = row["AREAID"].ToString();
                            dr["AREANAME"] = row["AREANAME"].ToString();
                            dr["EQUIPMENTCLASSID"] = row["EQUIPMENTCLASSID"].ToString();
                            dr["EQUIPMENTCLASSNAME"] = row["EQUIPMENTCLASSNAME"].ToString();
                            dr["PRODUCTDEFID"] = "*"; //rowProcessPath["PROCESSDEFID"];
                            dr["PRODUCTDEFVERSION"] = "*"; //rowProcessPath["PROCESSDEFVERSION"];
                            dr["PROCESSDEFID"] = rowProcessPath["PROCESSDEFID"];
                            dr["PROCESSDEFVERSION"] = rowProcessPath["PROCESSDEFVERSION"];
                            dr["PROCESSSEGMENTID"] = rowProcessPath["PROCESSSEGMENTID"];
                            dr["PROCESSSEGMENTVERSION"] = rowProcessPath["PROCESSSEGMENTVERSION"];
                            dr["EQUIPMENTID"] = "*";
                            dr["RESOURCETYPE"] = "Resource";
                            dr["RESOURCECLASSID"] = "*";
                            dr["SEQUENCE"] = 1;
                            dr["VALIDSTATE"] = "Valid";
                            dr["ENTERPRISEID"] = rowProcessPath["ENTERPRISEID"];
                            dr["PLANTID"] = rowProcessPath["PLANTID"];
                            dr["RESOURCEVERSION"] = "*";
                            dr["ISPRIMARYRESOURCE"] = "N";

                            if (drReworkRouting.RowState == DataRowState.Added)
                            {
                                dr["PROCESSDEFVERSION"] = "";
                                dr["PROCESSDEFID"] = drReworkRouting["TEMPSEQ"];

                            }
                            else
                            {
                                dr["PROCESSDEFVERSION"] = drReworkRouting["PROCESSDEFVERSION_R"];
                                dr["PROCESSDEFID"] = drReworkRouting["PROCESSDEFID_R"];

                            }

                            this.grdResource.View.RaiseValidateRow(handle);

                            handle++;
                        }

                    }
                  


                })
                .SetPopupValidationCustom(ValidationResource);



            // 팝업에서 사용할 조회조건 항목 추가

            parentCodeClassPopupColumn.Conditions.AddTextBox("RESOURCEID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("DESCRIPTION");
            parentCodeClassPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTID")
                 .SetPopupDefaultByGridColumnId("PROCESSSEGMENTID")
                // .SetIsReadOnly()
                 .SetIsHidden();

            parentCodeClassPopupColumn.Conditions.AddTextBox("PLANTID")
                .SetPopupDefaultByGridColumnId("PLANTID")
                .SetIsHidden();


            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("RESOURCEID", 90);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("DESCRIPTION", 220);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 80);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 300);

        }




        private void InitializeGrid_ReworkAreaPopup()
        {

            var parentArea = this.grdReworkArea.View.AddSelectPopupColumn("RESOURCEID", new SqlQuery("GetAreaList", "10003", $"AREATYPE={"Area"}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("AREAIDNAME", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetLabel("AREAID")
            .SetPopupResultCount(0)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
            .SetPopupResultMapping("RESOURCEID", "AREAID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정
            .SetValidationIsRequired()
            .SetPopupApplySelection((selectRows, gridRow) =>
            {
           
                if (selectRows.Count() > 0)
                {
                  
                    DataTable dt2 = this.grdReworkArea.DataSource as DataTable;
                    int handle = this.grdReworkArea.View.FocusedRowHandle;

                    DataRow drReworkRouting = this.grdProcessdefinition.View.GetFocusedDataRow();

                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectRows)
                    {
                        DataRow dr = null;

                        if (dt2.Rows.Count < handle + 1)
                        {
                            dr = dt2.NewRow();
                            dt2.Rows.Add(dr);
                        }
                        else
                        {
                            dr = this.grdReworkArea.View.GetDataRow(handle);
                        }


                        dr["RESOURCEID"] = row["AREAID"].ToString();
                        dr["RESOURCENAME"] = row["AREANAME"].ToString();
                        dr["RESOURCEVERSION"] = "";
                        dr["CONTROLTYPE"] = "Area";
                        dr["VALIDSTATE"] = "Valid";
                        dr["ENTERPRISEID"] = drReworkRouting["ENTERPRISEID"];
                        dr["PLANTID"] = drReworkRouting["PLANTID"];
                        dr["SEQUENCE"] = 0;


                        if (drReworkRouting.RowState == DataRowState.Added)
                        {
                            dr["PROCESSDEFVERSION"] = "";
                            dr["PROCESSDEFID"] = drReworkRouting["TEMPSEQ"];

                        }
                        else
                        {
                            dr["PROCESSDEFVERSION"] = drReworkRouting["PROCESSDEFVERSION_R"];
                            dr["PROCESSDEFID"] = drReworkRouting["PROCESSDEFID_R"];

                        }
                        this.grdReworkArea.View.RaiseValidateRow(handle);

                        handle++;

                    }
                }




            })
            .SetPopupValidationCustom(ValidationReworkArea);

            // 팝업에서 사용할 조회조건 항목 추가

            parentArea.Conditions.AddTextBox("AREA").SetLabel("AREAIDNAME");

            // 팝업 그리드 설정
            parentArea.GridColumns.AddTextBoxColumn("AREAID", 150);
            parentArea.GridColumns.AddTextBoxColumn("AREANAME", 300);
        }

        private void InitializeGrid_ReworkProductPopup()
        {

            // SelectPopup 항목 추가
            var conditionProductId = this.grdReworkProduct.View.AddSelectPopupColumn("RESOURCEID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"))
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("PRODUCTDEFID")
                .SetPopupResultMapping("RESOURCEID", "PRODUCTDEFID")
                .SetPosition(1.2)
                .SetValidationIsRequired()
                .SetPopupResultCount(0)
                .SetPopupApplySelection((selectRows, gridRow) =>
                {
                   if (selectRows.Count() > 0)
                    {

                        DataTable dt2 = this.grdReworkProduct.DataSource as DataTable;
                        int handle = this.grdReworkProduct.View.FocusedRowHandle;

                        DataRow drReworkRouting = this.grdProcessdefinition.View.GetFocusedDataRow();

                        // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                        // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                        foreach (DataRow row in selectRows)
                        {
                            DataRow dr = null;

                            if (dt2.Rows.Count < handle + 1)
                            {
                                dr = dt2.NewRow();
                                dt2.Rows.Add(dr);
                            }
                            else
                            {
                                dr = this.grdReworkProduct.View.GetDataRow(handle);
                            }

                            dr["RESOURCEID"] = row["PRODUCTDEFID"].ToString();
                            dr["RESOURCENAME"] = row["PRODUCTDEFNAME"].ToString();
                            dr["RESOURCEVERSION"] = row["PRODUCTDEFVERSION"].ToString();
                            dr["CONTROLTYPE"] = "Product";
                            dr["VALIDSTATE"] = "Valid";
                            dr["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            dr["PLANTID"] = UserInfo.Current.Plant;
                            dr["SEQUENCE"] = 0;

                            if (drReworkRouting.RowState == DataRowState.Added)
                            {
                                dr["PROCESSDEFVERSION"] = "";
                                dr["PROCESSDEFID"] = drReworkRouting["TEMPSEQ"];

                            }
                            else
                            {
                                dr["PROCESSDEFVERSION"] = drReworkRouting["PROCESSDEFVERSION_R"];
                                dr["PROCESSDEFID"] = drReworkRouting["PROCESSDEFID_R"];

                            }
                            this.grdReworkProduct.View.RaiseValidateRow(handle);

                            handle++;
                        }
                    }


                })
                .SetPopupValidationCustom(ValidationReworkProduct);

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");

            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetDefault("Product");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");


        }

        // 설비팝업
        // 설비팝업
        private void InitializeGrid_EquipmentPopup()
        {
            var parentEquipmentClassPopupColumn = this.grdEquipment.View.AddSelectPopupColumn("RESOURCEID", 150, new SqlQuery("GetReworkRoutingEquipmentPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("EQUIPMENTID")
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("RESOURCEID", "EQUIPMENTID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                .SetValidationIsRequired()
                .SetPopupApplySelection((selectRows, gridRow) =>
                {
               
                    if (selectRows.Count() > 0)
                    {
                      
                        DataTable dt2 = this.grdEquipment.DataSource as DataTable;
                        int handle = this.grdEquipment.View.FocusedRowHandle;

                        DataRow drReworkRouting = this.grdProcessdefinition.View.GetFocusedDataRow();
                        DataRow rowProcessPath = grdOperation.View.GetFocusedDataRow();

                        // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                        // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                        foreach (DataRow row in selectRows)
                        {

                            DataRow dr = null;

                            if (dt2.Rows.Count < handle + 1)
                            {
                                dr = dt2.NewRow();
                                dt2.Rows.Add(dr);
                            }
                            else
                            {
                                dr = this.grdEquipment.View.GetDataRow(handle);
                            }



                            dr["RESOURCEID"] = row["EQUIPMENTID"].ToString();
                            dr["RESOURCENAME"] = row["EQUIPMENTNAME"].ToString();
                            dr["PRODUCTDEFID"] = "*"; //rowProcessPath["PROCESSDEFID"];
                            dr["PRODUCTDEFVERSION"] = "*"; //rowProcessPath["PROCESSDEFVERSION"];
                            dr["PROCESSDEFID"] = rowProcessPath["PROCESSDEFID"];
                            dr["PROCESSDEFVERSION"] = rowProcessPath["PROCESSDEFVERSION"];
                            dr["PROCESSSEGMENTID"] = rowProcessPath["PROCESSSEGMENTID"];
                            dr["PROCESSSEGMENTVERSION"] = rowProcessPath["PROCESSSEGMENTVERSION"];
                            dr["RESOURCETYPE"] = "Equipment";
                            dr["RESOURCECLASSID"] = "*";
                            dr["SEQUENCE"] = 1;
                            dr["VALIDSTATE"] = "Valid";
                            dr["ENTERPRISEID"] = drReworkRouting["ENTERPRISEID"];
                            dr["PLANTID"] = drReworkRouting["PLANTID"];
                            dr["RESOURCEVERSION"] = "*";

                            if (drReworkRouting.RowState == DataRowState.Added)
                            {
                                dr["PROCESSDEFVERSION"] = "";
                                dr["PROCESSDEFID"] = drReworkRouting["TEMPSEQ"];

                            }
                            else
                            {
                                dr["PROCESSDEFVERSION"] = drReworkRouting["PROCESSDEFVERSION_R"];
                                dr["PROCESSDEFID"] = drReworkRouting["PROCESSDEFID_R"];

                            }
                            this.grdEquipment.View.RaiseValidateRow(handle);

                            handle++;
                        }
                    }
                })
            .SetPopupValidationCustom(ValidationEquipment);

            parentEquipmentClassPopupColumn.Conditions.AddTextBox("EQUIPMENTID");
            parentEquipmentClassPopupColumn.Conditions.AddTextBox("EQUIPMENTNAME");
            // 팝업에서 사용할 조회조건 항목 추가
            parentEquipmentClassPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTID")
                 .SetPopupDefaultByGridColumnId("PROCESSSEGMENTID")
                 .SetIsHidden();
            // 팝업 그리드 설정
            parentEquipmentClassPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            parentEquipmentClassPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 300);

        }



        private void InitializeGrid_ConsumablePopup()
        {
            //SqlExecuter.Query("GetReworkRoutingBomPopup", "10002", values);

            var parentCodeClassPopupColumn = this.grdConsumable.View.AddSelectPopupColumn("MATERIALDEFID", 150, new SqlQuery("GetReworkRoutingBomPopup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
               // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성0화 여부, 자동 검색 여부
               .SetPopupLayout("SELECTCONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupResultMapping("MATERIALDEFID", "ITEMID")
               // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
               .SetPopupResultCount(0)
               // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
               //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
               // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
               .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
               .SetValidationIsRequired()
               .SetPopupApplySelection((selectRows, dataGridRow) =>
               {
              
                   if (selectRows.Count() > 0)
                   {
                    
                       DataTable dt2 = grdConsumable.DataSource as DataTable;
                       int handle = grdConsumable.View.FocusedRowHandle;
                   
                       DataRow drReworkRouting = this.grdProcessdefinition.View.GetFocusedDataRow();
                       DataRow drOperation = this.grdOperation.View.GetFocusedDataRow();


                       dt2.Rows.RemoveAt(handle);
                       // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                       // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                       foreach (DataRow selectRow in selectRows)
                       {


                           DataRow dr = null;

                           if (dt2.Rows.Count < handle + 1)
                           {
                               dr = dt2.NewRow();
                               dt2.Rows.Add(dr);
                           }
                           else
                           {
                               dr = this.grdConsumable.View.GetDataRow(handle);
                           }

                           dr["PRODUCTDEFID"] = "*";
                           dr["PRODUCTDEFVERSION"] = "*";
                           dr["PROCESSSEGMENTID"] = drOperation["PROCESSSEGMENTID"];
                           dr["PROCESSSEGMENTVERSION"] = drOperation["PROCESSSEGMENTVERSION"];
                           dr["ENTERPRISEID"] = drReworkRouting["ENTERPRISEID"];
                           dr["PLANTID"] = drReworkRouting["PLANTID"];
                           dr["VALIDSTATE"] = "Valid";
                           dr["MATERIALTYPE"] = "Consumable";
                           dr["MATERIALDEFID"] = selectRow["ITEMID"];
                           dr["MATERIALDEFVERSION"] = selectRow["ITEMVERSION"];
                           dr["MATERIALDEFNAME"] = selectRow["ITEMNAME"];
                           dr["UNIT"] = selectRow["ITEMUOM"];

                           if (drReworkRouting.RowState == DataRowState.Added)
                           {
                               dr["PROCESSDEFVERSION"] = "";
                               dr["PROCESSDEFID"] = drReworkRouting["TEMPSEQ"];

                           }
                           else
                           {
                               dr["PROCESSDEFVERSION"] = drReworkRouting["PROCESSDEFVERSION_R"];
                               dr["PROCESSDEFID"] = drReworkRouting["PROCESSDEFID_R"];

                           }

                           DataTable dtProduct = this.grdReworkProduct.DataSource as DataTable;


                           List<string> productIdList = new List<string>();

                           foreach (DataRow productRow in dtProduct.Rows)
                           {
                               productIdList.Add(string.Format("{0}|{1}", productRow["RESOURCEID"].ToString(), productRow["RESOURCEVERSION"].ToString()));

                           }

                           dr["P_PRODUCTDEFID"] = string.Join(",", productIdList);

                           this.grdConsumable.View.RaiseValidateRow(handle);

                           handle++;
                       }

                   }
             
               })
               .SetPopupValidationCustom(ValidationConsumable);

            parentCodeClassPopupColumn.Conditions.AddTextBox("TXTCONSUMABLEIDNAME").SetLabel("CONSUMABLEIDNAME");
            parentCodeClassPopupColumn.Conditions.AddTextBox("P_PRODUCTDEFID")
                 .SetPopupDefaultByGridColumnId("P_PRODUCTDEFID")
                 .SetIsHidden();

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 200).SetIsReadOnly().SetLabel("MATERIALDEFID");
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 80).SetIsReadOnly().SetLabel("MATERIALDEFVERSION");
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 250).SetIsReadOnly().SetLabel("MATERIALDEFNAME");


            //grdConsumable.View.AddSelectPopupColumn("MATERIALDEFID", 150, new ReworkConsumablePopup())
            //    .SetValidationIsRequired()
            //    .SetPopupResultMapping("MATERIALDEFID", "ITEMID")
            //    // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            //    .SetPopupResultCount(0)
            //       .SetPopupCustomParameter
            //          (
            //              (ISmartCustomPopup sender, DataRow currentRow) => //초기화 작업
            //              {
            //                  DataTable dtProduct = this.grdReworkProduct.DataSource as DataTable;

            //                  if (dtProduct != null)
            //                  {
            //                      List<string> productIdList = new List<string>();

            //                      foreach (DataRow row in dtProduct.Rows)
            //                      {
            //                          productIdList.Add(string.Format("{0}|{1}", row["RESOURCEID"].ToString(), row["RESOURCEVERSION"].ToString()));

            //                      }

            //                      ReworkConsumablePopup popup = sender as ReworkConsumablePopup;
            //                      popup.CurrentDataRow = this.grdConsumable.View.GetFocusedDataRow();

            //                      popup.MultiProductItem = string.Join(",", productIdList);
            //                  }
            //              }
            //          )
            //         .SetPopupApplySelection((selectRows, gridRow) =>
            //         {

            //             DataTable dt2 = grdConsumable.DataSource as DataTable;
            //             int handle = grdConsumable.View.FocusedRowHandle;
            //             DataRow dr = grdConsumable.View.GetFocusedDataRow();

            //             DataRow drReworkRouting = this.grdProcessdefinition.View.GetFocusedDataRow();
            //             DataRow drOperation = this.grdOperation.View.GetFocusedDataRow();


            //             dt2.Rows.RemoveAt(handle);
            //             // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
            //             // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
            //             foreach (DataRow row in selectRows)
            //             {

            //                 DataRow newrow = dt2.NewRow();
            //                 newrow["PRODUCTDEFID"] = "*";
            //                 newrow["PRODUCTDEFVERSION"] = "*";
            //                 newrow["PROCESSSEGMENTID"] = drOperation["PROCESSSEGMENTID"];
            //                 newrow["PROCESSSEGMENTVERSION"] = drOperation["PROCESSSEGMENTVERSION"];
            //                 newrow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            //                 newrow["PLANTID"] = UserInfo.Current.Plant;
            //                 newrow["VALIDSTATE"] = "Valid";
            //                 newrow["MATERIALTYPE"] = "Consumable";
            //                 newrow["MATERIALDEFID"] = row["ITEMID"];
            //                 newrow["MATERIALDEFVERSION"] = row["ITEMVERSION"];
            //                 newrow["MATERIALDEFNAME"] = row["ITEMNAME"];
            //                 newrow["UNIT"] = row["ITEMUOM"];

            //                 if (drReworkRouting.RowState == DataRowState.Added)
            //                 {
            //                     newrow["PROCESSDEFVERSION"] = "";
            //                     newrow["PROCESSDEFID"] = drReworkRouting["TEMPSEQ"];

            //                 }
            //                 else
            //                 {
            //                     newrow["PROCESSDEFVERSION"] = drReworkRouting["PROCESSDEFVERSION_R"];
            //                     newrow["PROCESSDEFID"] = drReworkRouting["PROCESSDEFID_R"];

            //                 }


            //                 dt2.Rows.Add(newrow);

            //             }
            //         })
            //         .SetPopupValidationCustom(ValidationConsumable);


        }



        // 공정 팝업
        private void InitializeGrid_ProcesssegmentPopup()
        {

            var parentProcesssegment = this.grdOperation.View.AddSelectPopupColumn("PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentExtPupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"VALIDSTATE={"Valid"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("SELECTPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
            .SetValidationIsRequired()
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(0)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
            //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            .SetPopupApplySelection((selectRows, gridRow) =>
            {
           

                if (selectRows.Count() > 0)
                {
                    DataTable dt2 = this.grdOperation.DataSource as DataTable;
                    int handle = this.grdOperation.View.FocusedRowHandle;

                    DataRow drReworkRouting = this.grdProcessdefinition.View.GetFocusedDataRow();


                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectRows)
                    {
                        DataRow dr = null;

                        if (dt2.Rows.Count < handle + 1)
                        {
                            dr = dt2.NewRow();
                            dt2.Rows.Add(dr);

                            DataTable dtProcessPath = this.grdOperation.DataSource as DataTable;

                            if (dtProcessPath.AsEnumerable().Where(s => !string.IsNullOrEmpty(s["USERSEQUENCE"].ToString())).Count() == 0)
                            {
                                dr["USERSEQUENCE"] = "10";
                            }
                            else
                            {

                                int userSequence = dtProcessPath.AsEnumerable().Where(s => !string.IsNullOrEmpty(s["USERSEQUENCE"].ToString())).Max(s => int.Parse(s["USERSEQUENCE"].ToString()));

                                dr["USERSEQUENCE"] = (((userSequence / 10) + 1) * 10).ToString();
                            }
                        }
                        else
                        {
                            dr = this.grdOperation.View.GetDataRow(handle);
                        }

                        dr["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"];
                        dr["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
                        dr["PROCESSSEGMENTVERSION"] = "*";
                        dr["ENTERPRISEID"] = drReworkRouting["ENTERPRISEID"];
                        dr["PLANTID"] = drReworkRouting["PLANTID"];
                        dr["VALIDSTATE"] = "Valid";
                        dr["ALTERNATIVERESOURCE"] = "N";
                        dr["ASSIGNEQUIPMENT"] = "N";


                        if (UserInfo.Current.Enterprise == "INTERFLEX")
                        {
                            dr["PROCESSUOM"] = "PCS";
                        }
                        else if (UserInfo.Current.Enterprise == "YOUNGPOONG")
                        {
                            dr["PROCESSUOM"] = "PNL";
                        }


                        if (drReworkRouting.RowState == DataRowState.Added)
                        {
                            dr["PROCESSDEFVERSION"] = "";
                            dr["PROCESSDEFID"] = drReworkRouting["TEMPSEQ"];

                        }
                        else
                        {
                            dr["PROCESSDEFVERSION"] = drReworkRouting["PROCESSDEFVERSION_R"];
                            dr["PROCESSDEFID"] = drReworkRouting["PROCESSDEFID_R"];

                        }

                        this.grdOperation.View.RaiseValidateRow(handle);

                        handle++;
                    }
                }

          
            })
            .SetPopupValidationCustom(ValidationProcessSegment);
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정


            // 팝업에서 사용할 조회조건 항목 추가
            parentProcesssegment.Conditions.AddTextBox("PROCESSSEGMENTID");
            parentProcesssegment.Conditions.AddTextBox("PROCESSSEGMENTNAME");

            // 팝업 그리드 설정
            parentProcesssegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            parentProcesssegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 300);

        }



        /// <summary>
        /// 공정 팝업 컬럼 Validation
        /// </summary>
        /// <param name="currentGridRow"></param>
        /// <param name="popupSelections"></param>
        /// <returns></returns>
        private ValidationResultCommon ValidationProcessSegment(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            ValidationResultCommon result = new ValidationResultCommon();

            DataTable dt = this.grdOperation.DataSource as DataTable;


            foreach (DataRow row in dt.Rows)
            {
                if (popupSelections.Any(s => s.ToStringNullToEmpty("PROCESSSEGMENTID") == Format.GetString(row["PROCESSSEGMENTID"])))
                {
                    if (currentGridRow != row)
                    {
                        //이미 우선순위가 등록된 품목입니다.
                        Language.LanguageMessageItem item = Language.GetMessage("AlreadyInputProcessSegment");
                        result.IsSucced = false;
                        result.FailMessage = item.Message;
                        result.Caption = item.Title;
                    }

                }
            }

            return result;
        }


        /// <summary>
        /// 제품 팝업 컬럼 Validation
        /// </summary>
        /// <param name="currentGridRow"></param>
        /// <param name="popupSelections"></param>
        /// <returns></returns>
        private ValidationResultCommon ValidationReworkProduct(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            ValidationResultCommon result = new ValidationResultCommon();

            DataTable dt = this.grdReworkProduct.DataSource as DataTable;


            foreach (DataRow row in dt.Rows)
            {
                if (popupSelections.Any(s => s.ToStringNullToEmpty("PRODUCTDEFID") == Format.GetString(row["RESOURCEID"])))
                {
                    if (currentGridRow != row)
                    {
                        //이미 등록된 제품입니다.
                        Language.LanguageMessageItem item = Language.GetMessage("AlreadyInputProduct");
                        result.IsSucced = false;
                        result.FailMessage = item.Message;
                        result.Caption = item.Title;
                    }

                }
            }

            return result;
        }


        /// <summary>
        /// 공정 팝업 컬럼 Validation
        /// </summary>
        /// <param name="currentGridRow"></param>
        /// <param name="popupSelections"></param>
        /// <returns></returns>
        private ValidationResultCommon ValidationConsumable(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            ValidationResultCommon result = new ValidationResultCommon();

            DataTable dt = this.grdConsumable.DataSource as DataTable;


            foreach (DataRow row in dt.Rows)
            {
                if (popupSelections.Any(s => s.ToStringNullToEmpty("ITEMID") == Format.GetString(row["MATERIALDEFID"])))
                {
                    if (currentGridRow != row)
                    {
                        //이미 우선순위가 등록된 품목입니다.
                        Language.LanguageMessageItem item = Language.GetMessage("AlreadyInputConsumable");
                        result.IsSucced = false;
                        result.FailMessage = item.Message;
                        result.Caption = item.Title;
                    }

                }
            }

            return result;
        }


        /// <summary>
        /// 작업장 팝업 컬럼 Validation
        /// </summary>
        /// <param name="currentGridRow"></param>
        /// <param name="popupSelections"></param>
        /// <returns></returns>
        private ValidationResultCommon ValidationReworkArea(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            ValidationResultCommon result = new ValidationResultCommon();

            DataTable dt = this.grdReworkArea.DataSource as DataTable;


            foreach (DataRow row in dt.Rows)
            {
                if (popupSelections.Any(s => s.ToStringNullToEmpty("AREAID") == Format.GetString(row["RESOURCEID"])))
                {
                    if (currentGridRow != row)
                    {
                        //이미 등록된 작업장입니다.
                        Language.LanguageMessageItem item = Language.GetMessage("AlreadyInputWorkArea");
                        result.IsSucced = false;
                        result.FailMessage = item.Message;
                        result.Caption = item.Title;
                    }

                }
            }

            return result;
        }

        /// <summary>
        /// 자원 팝업 컬럼 Validation
        /// </summary>
        /// <param name="currentGridRow"></param>
        /// <param name="popupSelections"></param>
        /// <returns></returns>
        private ValidationResultCommon ValidationResource(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            ValidationResultCommon result = new ValidationResultCommon();

            DataRow drIsPrimaryResource = this.grdOperation.View.GetFocusedDataRow();

            if(drIsPrimaryResource != null)
            {
                if (popupSelections.Any(s => s.ToStringNullToEmpty("RESOURCEID") == Format.GetString(drIsPrimaryResource["RESOURCEID"])))
                {
                    //이미 등록된 자재입니다.
                    Language.LanguageMessageItem item = Language.GetMessage("RegistPrimaryResource");
                    result.IsSucced = false;
                    result.FailMessage = item.Message;
                    result.Caption = item.Title;
                }
            }


            DataTable dt = this.grdResource.DataSource as DataTable;


            foreach (DataRow row in dt.Rows)
            {
                if (popupSelections.Any(s => s.ToStringNullToEmpty("RESOURCEID") == Format.GetString(row["RESOURCEID"])))
                {
                    if (currentGridRow != row)
                    {
                        //이미 등록된 자재입니다.
                        Language.LanguageMessageItem item = Language.GetMessage("AlreadyInputResource");
                        result.IsSucced = false;
                        result.FailMessage = item.Message;
                        result.Caption = item.Title;
                    }

                }
            }

            return result;
        }

        /// <summary>
        /// 설비 팝업 컬럼 Validation
        /// </summary>
        /// <param name="currentGridRow"></param>
        /// <param name="popupSelections"></param>
        /// <returns></returns>
        private ValidationResultCommon ValidationEquipment(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            ValidationResultCommon result = new ValidationResultCommon();

            DataTable dt = this.grdEquipment.DataSource as DataTable;


            foreach (DataRow row in dt.Rows)
            {
                if (popupSelections.Any(s => s.ToStringNullToEmpty("EQUIPMENTID") == Format.GetString(row["RESOURCEID"])))
                {
                    if (currentGridRow != row)
                    {
                        //이미 등록된 설비입니다.
                        Language.LanguageMessageItem item = Language.GetMessage("AlreadyInputEquipment");
                        result.IsSucced = false;
                        result.FailMessage = item.Message;
                        result.Caption = item.Title;
                    }

                }
            }

            return result;
        }




        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeReworkRoutingGrid();
        }



        #endregion

        #region 이벤트

        private void InitializeEvent()
        {
            this.smartTabControl1.SelectedPageChanging += SmartTabControl1_SelectedPageChanging;
            this.smartTabControl2.SelectedPageChanging += SmartTabControl2_SelectedPageChanging;

            //재작업 그리드
            grdProcessdefinition.View.AddingNewRow += grdProcessdefinition_AddingNewRow;
            grdProcessdefinition.View.FocusedRowChanged += grdProcessdefinition_FocusedRowChanged;
            grdProcessdefinition.View.CellValueChanged += grdProcessdefinitionView_CellValueChanged;
            grdProcessdefinition.View.ShowingEditor += grdProcessdefinitionView_ShowingEditor;
            grdProcessdefinition.ToolbarDeletingRow += grdProcessdefinition_ToolbarDeletingRow; 

            //공정 그리드
            grdOperation.View.FocusedRowChanged += grdOperation_FocusedRowChanged;
            grdOperation.View.AddingNewRow += grdOperation_AddingNewRow;
            grdOperation.View.ShowingEditor += grdOperation_ShowingEditor;
            grdOperation.View.CellValueChanged += grdOperation_CellValueChanged;
            grdOperation.ToolbarDeletingRow += grdOperation_ToolbarDeletingRow;




            grdReworkProduct.View.AddingNewRow += grdReworkProduct_AddingNewRow;
            grdReworkProduct.View.ShowingEditor += grdReworkProductView_ShowingEditor;
            grdReworkProduct.ToolbarDeletingRow += grdReworkProduct_ToolbarDeletingRow;

            grdReworkArea.View.AddingNewRow += grdReworkArea_AddingNewRow;
            grdReworkArea.View.ShowingEditor += grdReworkAreaView_ShowingEditor;


            grdConsumable.View.AddingNewRow += grdBillofMaterial_AddingNewRow;
            grdConsumable.View.ShowingEditor += grdConsumableView_ShowingEditor;

            grdResource.View.AddingNewRow += grdResource_AddingNewRow;
            grdResource.View.ShowingEditor += grdResourceView_ShowingEditor;
            grdResource.View.FocusedRowChanged += grdResource_FocusedRowChanged;


            grdEquipment.View.AddingNewRow += grdEQUIPMENT_AddingNewRow;
            grdEquipment.View.ShowingEditor += grdEquipmentView_ShowingEditor;


        }

        private DataTable GetDataTableBySmartTabControl1(int tabIndex)
        {
            DataTable dtChangeData = new DataTable();
            switch (tabIndex)
            {
                case 0:
                    try
                    {
                        dtChangeData = this.grdOperation.GetChangedRows();
                    }
                    catch { }
                    break;
                case 1:
                    try
                    {
                        dtChangeData = this.grdReworkProduct.GetChangedRows();
                    }
                    catch
                    { }
                    break;
                case 2:
                    try
                    {
                        dtChangeData = this.grdReworkArea.GetChangedRows();
                    }
                    catch
                    { }
                    break;
            }

            return dtChangeData;
        }


        private DataTable GetDataTableBySmartTabControl2(int tabIndex)
        {
            DataTable dtChangeData = new DataTable();
            switch (tabIndex)
            {
                case 0:
                    try
                    {
                        dtChangeData = this.grdConsumable.GetChangedRows();
                    }
                    catch { }
                    break;
                case 1:
                    try
                    {
                        dtChangeData = this.grdResource.GetChangedRows();
                    }
                    catch
                    { }
                    break;
                case 2:
                    try
                    {
                        dtChangeData = this.grdEquipment.GetChangedRows();
                    }
                    catch
                    { }
                    break;
            }

            return dtChangeData;
        }


        private void SmartTabControl2_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {

             DataTable dtChangeData = GetDataTableBySmartTabControl2(smartTabControl2.TabPages.IndexOf(e.PrevPage));

            if (dtChangeData.Rows.Count > 0)
            {
                DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                switch (result)
                {
                    case DialogResult.Yes:
                        SearchGridTabControl2(smartTabControl2.TabPages.IndexOf(e.Page));
                        break;
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                }
            }
            else
                SearchGridTabControl2(smartTabControl2.TabPages.IndexOf(e.Page));

        }

        private void SmartTabControl1_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
       
            DataTable dtChangeData = GetDataTableBySmartTabControl1(smartTabControl1.TabPages.IndexOf(e.PrevPage));

            if (dtChangeData.Rows.Count > 0)
            {
                DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                switch (result)
                {
                    case DialogResult.Yes:
                        SearchGridTabControl1(smartTabControl1.TabPages.IndexOf(e.Page));
                        break;
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                }
            }
            else
                SearchGridTabControl1(smartTabControl1.TabPages.IndexOf(e.Page));


        }


        private void SearchGridTabControl2(int tabIndex)
        {

            if (this.grdOperation.View.FocusedRowHandle < 0)
            {
                grdConsumable.DataSource = null;
                grdResource.DataSource = null;
                grdEquipment.DataSource = null;

                return;
            }

            DataRow row = this.grdOperation.View.GetFocusedDataRow();

            Dictionary<string, object> Param = new Dictionary<string, object>();

            switch (tabIndex)
            {
                case 0:
                    Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    Param.Add("PROCESSDEFID", row["PROCESSDEFID"].ToString());
                    Param.Add("PROCESSDEFVERSION", row["PROCESSDEFVERSION"].ToString());
                    Param.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"].ToString());
                    Param.Add("PROCESSSEGMENTVERSION", row["PROCESSSEGMENTVERSION"].ToString());


                    SearchGrid(grdConsumable, "GetBillofMaterialList", "10001", Param);
                    break;
                case 1:
                    Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    Param.Add("PROCESSDEFID", row["PROCESSDEFID"].ToString());
                    Param.Add("PROCESSDEFVERSION", row["PROCESSDEFVERSION"].ToString());
                    Param.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"].ToString());
                    Param.Add("PROCESSSEGMENTVERSION", row["PROCESSSEGMENTVERSION"].ToString());
                    Param.Add("PLANTID", row["PLANTID"].ToString());
                    Param.Add("ISPRIMARYRESOURCE", "N");
                    Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    SearchGrid(grdResource, "GetReworkRoutingResource", "10001", Param);
                    break;
                case 2:
                    Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    Param.Add("PROCESSDEFID", row["PROCESSDEFID"].ToString());
                    Param.Add("PROCESSDEFVERSION", row["PROCESSDEFVERSION"].ToString());
                    Param.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"].ToString());
                    Param.Add("PROCESSSEGMENTVERSION", row["PROCESSSEGMENTVERSION"].ToString());
                    Param.Add("PLANTID", row["PLANTID"].ToString());

                    Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    SearchGrid(grdEquipment, "GetReworkRoutingEquipment", "10002", Param);
                    break;
            }
        }

        private void SearchGridTabControl1(int tabIndex)
        {

            if (this.grdProcessdefinition.View.FocusedRowHandle < 0)
            {
                ClearDataTable(false);

                return;
            }

            DataRow row = this.grdProcessdefinition.View.GetFocusedDataRow();

            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("PROCESSDEFID", row["PROCESSDEFID_R"].ToString());
            Param.Add("PROCESSDEFVERSION", row["PROCESSDEFVERSION_R"].ToString());
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);


            switch (tabIndex)
            {
                case 0:
                    SearchGrid(grdOperation, "GetProcessPathList", "10004", Param);

                    SearchGridTabControl2(this.smartTabControl2.SelectedTabPageIndex);
                    break;
                case 1:
                    SearchGrid(grdReworkProduct, "GetProductReworkControl", "10001", Param);

                    break;
                case 2:
                    SearchGrid(grdReworkArea, "GetAreaReworkControl", "10001", Param);

                    break;
            }
        }


        private void grdReworkProduct_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "DeleteLinkedItemByProduct");

            switch (result)
            {
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        private void grdOperation_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "DeleteLinkedItemBySegment");

            switch (result)
            {
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        private void grdProcessdefinition_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "DeleteLinkedItemByReworkRouting");

            switch (result)
            {
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        private void grdEquipmentView_ShowingEditor(object sender, CancelEventArgs e)
        {
            string currentColumnName = this.grdEquipment.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("RESOURCEID"))
            {
                if (!this.grdEquipment.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                    e.Cancel = true;
            }
        }

        private void grdResourceView_ShowingEditor(object sender, CancelEventArgs e)
        {
            string currentColumnName = this.grdResource.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("RESOURCEID"))
            {
                if (!this.grdResource.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                    e.Cancel = true;
            }
        }

        private void grdReworkAreaView_ShowingEditor(object sender, CancelEventArgs e)
        {
            string currentColumnName = this.grdReworkArea.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("RESOURCEID"))
            {
                if (!this.grdReworkArea.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                    e.Cancel = true;
            }
        }

        private void grdReworkProductView_ShowingEditor(object sender, CancelEventArgs e)
        {

            string currentColumnName = this.grdReworkProduct.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("RESOURCEID"))
            {
                if (!this.grdReworkProduct.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                    e.Cancel = true;
            }
        }

        private void grdConsumableView_ShowingEditor(object sender, CancelEventArgs e)
        {
            string currentColumnName = this.grdConsumable.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("MATERIALDEFID"))
            {
                if (!this.grdConsumable.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                    e.Cancel = true;
            }

        }


        private void grdOperation_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string currentColumnName = this.grdOperation.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("ALTERNATIVERESOURCE"))
            {
                if (e.Value.Equals("Y"))
                    this.smartTabControl2.SelectedTabPage = this.xtpResource;

            }
            if (currentColumnName.Equals("ASSIGNEQUIPMENT"))
            {
                if (e.Value.Equals("Y"))
                {

                    DataTable dtResource = grdResource.DataSource as DataTable;

                    if (dtResource != null && dtResource.Rows.Count > 0)
                    {

                        DialogResult result = ShowMessage(MessageBoxButtons.OK, "ReworkRegistEquipment");

                        this.grdOperation.View.SetFocusedRowCellValue("ASSIGNEQUIPMENT", "N");

                        return;

                    }
                    else
                    {
                        this.smartTabControl2.SelectedTabPage = this.xtpEquipment;
                    }
                }

            }
        }

        private void grdOperation_ShowingEditor(object sender, CancelEventArgs e)
        {
            string currentColumnName = this.grdOperation.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("PLANTID") || currentColumnName.Equals("PROCESSSEGMENTID"))
            {
                if (!this.grdOperation.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                    e.Cancel = true;
            }
        }

        private void grdProcessdefinitionView_ShowingEditor(object sender, CancelEventArgs e)
        {
            string currentColumnName = this.grdProcessdefinition.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("PROCESSCLASSTYPE") || currentColumnName.Equals("PROCESSCLASSID_R")
                || currentColumnName.Equals("PROCESSDEFID_R") || currentColumnName.Equals("TOPPROCESSSEGMENTID"))
            {
                if (!this.grdProcessdefinition.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                    e.Cancel = true;
            }


        }

        private void grdProcessdefinitionView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grdProcessdefinition.View.CellValueChanged -= grdProcessdefinitionView_CellValueChanged;


            string currentColumnName = this.grdProcessdefinition.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("REWORKITEMCONTROL"))
            {
                if (e.Value.Equals("Y"))
                {
                    this.smartTabControl1.SelectedTabPage = this.xtpProductInfo;
                }
                else if(e.Value.Equals("N"))
                {
                    DataTable dtReworkProduct = this.grdReworkProduct.DataSource as DataTable;
                    DataTable dtConsumable = this.grdConsumable.DataSource as DataTable;


                    if (dtReworkProduct.Rows.Count > 0 || dtConsumable.Rows.Count > 0)
                    {
                        DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "DeleteLinkedItemByReworkItemControl");

                        switch (result)
                        {
                            case DialogResult.Yes:

                                break;
                            case DialogResult.No:
                                this.grdProcessdefinition.View.SetFocusedRowCellValue(currentColumnName, "Y");
                                break;
                        }
                    }
                }
            }
            

            grdProcessdefinition.View.CellValueChanged += grdProcessdefinitionView_CellValueChanged;

        }

        private void grdBillofMaterial_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            DataTable dtProduct = this.grdReworkProduct.DataSource as DataTable;

            if (dtProduct == null || dtProduct.Rows.Count == 0)
            {
                ShowMessage(MessageBoxButtons.OK, "CheckRegistConsumable");

                args.IsCancel = true;
                return;
            }

            DataRow focusedRow = this.grdReworkProduct.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                //if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                //{
                //    ShowMessage("NoSelectDataRAndProceed");

                //    args.IsCancel = true;

                //    return;
                //}
            }
            else
            {
                args.IsCancel = true;

                return;
            }


            if (this.grdOperation.View.FocusedRowHandle < 0)
            {
                ShowMessage(MessageBoxButtons.OK, "CheckOperationByRework");

                args.IsCancel = true;
                return;
            }

            List<string> productIdList = new List<string>();

             foreach (DataRow productRow in dtProduct.Rows)
            {
                productIdList.Add(string.Format("{0}|{1}", productRow["RESOURCEID"].ToString(), productRow["RESOURCEVERSION"].ToString()));

            }

            args.NewRow["P_PRODUCTDEFID"] = string.Join(",", productIdList);

            DataRow row = grdOperation.View.GetFocusedDataRow();

            args.NewRow["PRODUCTDEFID"] = "*";
            args.NewRow["PRODUCTDEFVERSION"] = "*";

            args.NewRow["PROCESSDEFID"] = row["PROCESSDEFID"];
            args.NewRow["PROCESSDEFVERSION"] = row["PROCESSDEFVERSION"];

            args.NewRow["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"];
            args.NewRow["PROCESSSEGMENTVERSION"] = row["PROCESSSEGMENTVERSION"];
            args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            args.NewRow["PLANTID"] = row["PLANTID"];
            args.NewRow["VALIDSTATE"] = "Valid";
            args.NewRow["MATERIALTYPE"] = "Consumable";
        }



        private void grdResource_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {


            //DataRow rowResource = grdResource.View.GetFocusedDataRow();

            //Dictionary<string, object> ParamEquipment = new Dictionary<string, object>();
            //ParamEquipment.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //ParamEquipment.Add("PROCESSDEFID", rowResource["PROCESSDEFID"].ToString());
            //ParamEquipment.Add("PROCESSDEFVERSION", rowResource["PROCESSDEFVERSION"].ToString());
            //ParamEquipment.Add("PROCESSSEGMENTID", rowResource["PROCESSSEGMENTID"].ToString());
            //ParamEquipment.Add("PROCESSSEGMENTVERSION", rowResource["PROCESSSEGMENTVERSION"].ToString());
            //ParamEquipment.Add("PLANTID", rowResource["PLANTID"].ToString());
            //ParamEquipment.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //ParamEquipment.Add("RESOURCEID", rowResource["RESOURCEID"].ToString());

            //DataTable dtEquipment = SqlExecuter.Query("GetReworkRoutingEquipment", "10002", ParamEquipment);

            //grdEquipment.DataSource = dtEquipment;
        }

        private void grdEQUIPMENT_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (this.grdOperation.View.FocusedRowHandle < 0)
            {
                ShowMessage(MessageBoxButtons.OK, "CheckOperationByRework");

                args.IsCancel = true;
                return;
            }


            DataRow focusedRow = this.grdOperation.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                //if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                //{
                //    ShowMessage("NoSelectDataSaveAndProceed");

                //    args.IsCancel = true;

                //    return;
                //}
            }
            else
            {
                args.IsCancel = true;

                return;
            }


            string data = this.grdOperation.View.GetFocusedRowCellValue("ASSIGNEQUIPMENT").ToString();

            if (data == "Y")
            {
                DataRow rowProcessPath = grdOperation.View.GetFocusedDataRow();
                args.NewRow["PRODUCTDEFID"] = "*"; //rowProcessPath["PROCESSDEFID"];
                args.NewRow["PRODUCTDEFVERSION"] = "*"; //rowProcessPath["PROCESSDEFVERSION"];
                args.NewRow["PROCESSDEFID"] = rowProcessPath["PROCESSDEFID"];
                args.NewRow["PROCESSDEFVERSION"] = rowProcessPath["PROCESSDEFVERSION"];
                args.NewRow["PROCESSSEGMENTID"] = rowProcessPath["PROCESSSEGMENTID"];
                args.NewRow["PROCESSSEGMENTVERSION"] = rowProcessPath["PROCESSSEGMENTVERSION"];
                args.NewRow["RESOURCETYPE"] = "Equipment";
                args.NewRow["RESOURCECLASSID"] = "*";
                args.NewRow["SEQUENCE"] = 1;
                args.NewRow["VALIDSTATE"] = "Valid";
                args.NewRow["ENTERPRISEID"] = rowProcessPath["ENTERPRISEID"];
                args.NewRow["PLANTID"] = rowProcessPath["PLANTID"];
                args.NewRow["RESOURCEVERSION"] = "*";
                args.NewRow["RESOURCEID"] = "";
            }
            else
            {
                ShowMessage(MessageBoxButtons.OK, "CheckRegistEquipmentByRework");

                args.IsCancel = true;

                return;
            }
        }


        private void grdResource_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (this.grdOperation.View.FocusedRowHandle < 0)
            {
                ShowMessage(MessageBoxButtons.OK, "CheckOperationByRework");

                args.IsCancel = true;
                return;
            }


            DataRow focusedRow = this.grdOperation.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                //if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                //{
                //    ShowMessage("NoSelectDataSaveAndProceed");


                //    args.IsCancel = true;

                //    return;
                //}
            }
            else
            {
                args.IsCancel = true;

                return;
            }

            string data = this.grdOperation.View.GetFocusedRowCellValue("ALTERNATIVERESOURCE").ToString();

            if (data == "Y")
            {
                DataRow rowProcessPath = grdOperation.View.GetFocusedDataRow();
                args.NewRow["PRODUCTDEFID"] = "*"; //rowProcessPath["PROCESSDEFID"];
                args.NewRow["PRODUCTDEFVERSION"] = "*"; //rowProcessPath["PROCESSDEFVERSION"];
                args.NewRow["PROCESSDEFID"] = rowProcessPath["PROCESSDEFID"];
                args.NewRow["PROCESSDEFVERSION"] = rowProcessPath["PROCESSDEFVERSION"];
                args.NewRow["PROCESSSEGMENTID"] = rowProcessPath["PROCESSSEGMENTID"];
                args.NewRow["PROCESSSEGMENTVERSION"] = rowProcessPath["PROCESSSEGMENTVERSION"];
                args.NewRow["EQUIPMENTID"] = "*";
                args.NewRow["RESOURCETYPE"] = "Resource";
                args.NewRow["RESOURCECLASSID"] = "*";
                args.NewRow["SEQUENCE"] = 1;
                args.NewRow["VALIDSTATE"] = "Valid";
                args.NewRow["ENTERPRISEID"] = rowProcessPath["ENTERPRISEID"];
                args.NewRow["PLANTID"] = rowProcessPath["PLANTID"];
                args.NewRow["RESOURCEVERSION"] = "*";
                args.NewRow["ISPRIMARYRESOURCE"] = "N";
            }
            else
            {
                ShowMessage(MessageBoxButtons.OK, "CheckRegistResourceByRework");

                args.IsCancel = true;

                return;
            }

        }

        private void grdReworkArea_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            if (this.grdProcessdefinition.View.FocusedRowHandle < 0)
            {
                ShowMessage(MessageBoxButtons.OK, "CheckRegistReworkRouting");

                args.IsCancel = true;
                return;
            }

            DataRow focusedRow = this.grdProcessdefinition.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                //if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                //{
                //    ShowMessage("NoSelectDataSaveAndProceed");

                //    args.IsCancel = true;

                //    return;
                //}
            }
            else
            {
                args.IsCancel = true;

                return;
            }

            DataRow row = grdProcessdefinition.View.GetFocusedDataRow();

            if (row.RowState == DataRowState.Added)
            {
                args.NewRow["PROCESSDEFVERSION"] = "";
                args.NewRow["PROCESSDEFID"] = row["TEMPSEQ"];

            }
            else
            {
                args.NewRow["PROCESSDEFID"] = row["PROCESSDEFID_R"];
                args.NewRow["PROCESSDEFVERSION"] = row["PROCESSDEFVERSION_R"];
            }
            args.NewRow["CONTROLTYPE"] = "Area";
            args.NewRow["VALIDSTATE"] = "Valid";
            args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            args.NewRow["PLANTID"] = row["PLANTID"];
            args.NewRow["SEQUENCE"] = 0;


        }

        private void grdReworkProduct_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (this.grdProcessdefinition.View.FocusedRowHandle < 0)
            {
                ShowMessage(MessageBoxButtons.OK, "CheckRegistReworkRouting");

                args.IsCancel = true;
                return;
            }

            DataRow focusedRow = this.grdProcessdefinition.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                //if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                //{
                //    ShowMessage("NoSelectDataSaveAndProceed");

                //    args.IsCancel = true;

                //    return;
                //}
            }
            else
            {
                args.IsCancel = true;

                return;
            }

            string productControl = this.grdProcessdefinition.View.GetFocusedRowCellValue("REWORKITEMCONTROL").ToString();

            if (productControl == "Y")
            {

                DataRow row = grdProcessdefinition.View.GetFocusedDataRow();

                args.NewRow["PROCESSDEFID"] = row["PROCESSDEFID_R"];
                args.NewRow["PROCESSDEFVERSION"] = row["PROCESSDEFVERSION_R"];
                args.NewRow["CONTROLTYPE"] = "Product";
                args.NewRow["VALIDSTATE"] = "Valid";
                args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
                args.NewRow["PLANTID"] = row["PLANTID"];
                args.NewRow["SEQUENCE"] = 0;
            }
            else
            {
                ShowMessage(MessageBoxButtons.OK, "CheckRegistProductByRework");

                args.IsCancel = true;

                return;
            }

        }


        private void grdOperation_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (this.grdProcessdefinition.View.FocusedRowHandle < 0)
            {
                ShowMessage(MessageBoxButtons.OK, "CheckRegistReworkRouting");

                args.IsCancel = true;
                return;
            }

            DataRow focusedRow = this.grdProcessdefinition.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                //if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                //{
                //    ShowMessage("NoSelectDataSaveAndProceed");

                //    args.IsCancel = true;

                //    return;
                //}
            }
            else
            {
                args.IsCancel = true;

                return;
            }


            DataRow row = grdProcessdefinition.View.GetFocusedDataRow();

            if (row.RowState == DataRowState.Added)
            {
                args.NewRow["PROCESSDEFVERSION"] = "";
                args.NewRow["PROCESSDEFID"] = row["TEMPSEQ"];

            }
            else
            {
                args.NewRow["PROCESSDEFVERSION"] = row["PROCESSDEFVERSION_R"];
                args.NewRow["PROCESSDEFID"] = row["PROCESSDEFID_R"];

            }

            args.NewRow["PROCESSUOM"] = "PNL";
            args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            args.NewRow["PLANTID"] = row["PLANTID"];
            args.NewRow["VALIDSTATE"] = "Valid";


            DataTable dtProcessPath = this.grdOperation.DataSource as DataTable;

            if (dtProcessPath.AsEnumerable().Where(s => !string.IsNullOrEmpty(s["USERSEQUENCE"].ToString())).Count() == 0)
            {
                args.NewRow["USERSEQUENCE"] = "10";
            }
            else
            {

                int userSequenceList = dtProcessPath.AsEnumerable().Where(s => !string.IsNullOrEmpty(s["USERSEQUENCE"].ToString())).Max(s => int.Parse(s["USERSEQUENCE"].ToString()));

                args.NewRow["USERSEQUENCE"] = (userSequenceList += 10).ToString();
            }
        }

        private void grdOperation_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (grdOperation.View.FocusedRowHandle < 0)
                return;


            SearchGridTabControl2(this.smartTabControl2.SelectedTabPageIndex);


        }

        private DataTable SearchGrid(SmartBandedGrid grid, string queryId, string version, Dictionary<string, object> param, params string[] addColumns)
        {
            grid.DataSource = null;

            DataTable dt = SqlExecuter.Query(queryId, version, param);


            foreach (string column in addColumns)
            {
                dt.Columns.Add(column);
            }
            if (dt != null)
            {
                grid.DataSource = dt;
            }



            return dt;
        }

        private void grdProcessdefinition_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            grdProcessdefinition.View.FocusedRowChanged -= grdProcessdefinition_FocusedRowChanged;

            DataTable dtOperation = new DataTable();
            DataTable dtReworkProduct = new DataTable();
            DataTable dtReworkArea = new DataTable();
            DataTable dtConsumable = new DataTable();
            DataTable dtResource = new DataTable();
            DataTable dtEquipment = new DataTable();

            try
            {
                dtOperation = this.grdOperation.GetChangedRows();
            }
            catch
            { }

            try
            {
                dtReworkProduct = this.grdReworkProduct.GetChangedRows();
            }
            catch
            { }

            try
            {
                dtReworkArea = this.grdReworkArea.GetChangedRows();
            }
            catch
            { }

            try
            {
                dtConsumable = this.grdConsumable.GetChangedRows();
            }
            catch
            { }

            try
            {
                dtResource = this.grdResource.GetChangedRows();
            }
            catch
            { }

            try
            {
                dtEquipment = this.grdEquipment.GetChangedRows();
            }
            catch
            { }


            if (dtOperation.Rows.Count > 0 || dtReworkProduct.Rows.Count > 0
                || dtReworkArea.Rows.Count > 0 || dtConsumable.Rows.Count > 0 || dtResource.Rows.Count > 0 || dtEquipment.Rows.Count > 0)
            {
                DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                switch (result)
                {
                    case DialogResult.Yes:
                        SearchAllData();
                        break;
                    case DialogResult.No:
                        this.grdProcessdefinition.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                        break;
                }
            }
            else
                SearchAllData();



            this.smartTabControl1.SelectedTabPageIndex = 0;


         
            grdProcessdefinition.View.FocusedRowChanged += grdProcessdefinition_FocusedRowChanged;


        }

        private void ClearDataTable(bool isReworkRouting)
        {
            if(isReworkRouting)
                this.grdProcessdefinition.DataSource = null;

            this.grdOperation.DataSource = null;
            this.grdReworkProduct.DataSource = null;
            this.grdReworkArea.DataSource = null;
            this.grdConsumable.DataSource = null;
            this.grdResource.DataSource = null;
            this.grdEquipment.DataSource = null;
        }

        private void grdProcessdefinition_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
      
            args.NewRow["TEMPSEQ"] = Guid.NewGuid().ToString();
            args.NewRow["PROCESSDEFTYPE"] = "Rework";
            args.NewRow["VERSIONSTATE"] = "Active";
            args.NewRow["VALIDSTATE"] = "Valid";

            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;

            ClearDataTable(false);

        }

        #endregion

        #region 툴바

        private void CheckDuplicationId(SmartBandedGrid grid, string messageId, string checkColumnId)
        {

            DataTable dt = grid.DataSource as DataTable;

            if (dt != null)
            {

                var checkDuplication = from r in dt.AsEnumerable()
                                       group r by new {
                                           Key = r.Field<object>(checkColumnId)
                                       } into g
                                       where g.Count() > 1
                                       select g;

                if (checkDuplication.Count() > 0)
                    throw MessageException.Create(messageId, checkDuplication.ElementAt(0).Key.Key.ToString());

            }


        }


        private void CheckDuplicationIdAndVersion(SmartBandedGrid grid, string messageId, string checkColumnId, string version)
        {

            DataTable dt = grid.DataSource as DataTable;

            if (dt != null)
            {

                var checkDuplication = from r in dt.AsEnumerable()
                                       group r by new
                                       {
                                            ID = r.Field<object>(checkColumnId)
                                           ,Version = r.Field<object>(version)
                                       } into g
                                       where g.Count() > 1
                                       select g;

                if (checkDuplication.Count() > 0)
                    throw MessageException.Create(messageId, checkDuplication.ElementAt(0).Key.ID.ToString());

            }


        }
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

    


            DataTable dtRework = new DataTable();
            DataTable dtProcessPath = new DataTable();
            DataTable dtProduct = new DataTable();
            DataTable dtWorkArea = new DataTable();
            DataTable dtResource = new DataTable();
            DataTable dtEquipment = new DataTable();
            DataTable dtConsumable = new DataTable();

            try
            {
                dtRework = this.grdProcessdefinition.GetChangedRows();
            }
            catch { }

            try
            {
                dtProcessPath = this.grdOperation.GetChangedRows();
            }
            catch { }

            try
            {
                dtProduct = this.grdReworkProduct.GetChangedRows();
            }
            catch { }

            try
            {
                dtWorkArea = this.grdReworkArea.GetChangedRows();
            }
            catch { }
            try
            {
                dtConsumable = this.grdConsumable.GetChangedRows();
            }
            catch { }
            try
            {
                dtResource = this.grdResource.GetChangedRows();
            }
            catch { }

            try
            {
                dtEquipment = this.grdEquipment.GetChangedRows();
            }
            catch { }



            if (dtRework.Rows.Count == 0 && dtProcessPath.Rows.Count == 0 && dtProduct.Rows.Count == 0 && dtWorkArea.Rows.Count == 0 &&
                dtResource.Rows.Count == 0 && dtEquipment.Rows.Count == 0 && dtConsumable.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            if (dtRework.Rows.Count > 0)
            {
                foreach (DataRow dr in dtResource.Rows)
                {
                    if (!dr["_STATE_"].ToString().Equals("deleted"))// != DataRowState.Deleted)
                    {
                        if ((this.grdOperation.DataSource == null || (this.grdOperation.DataSource as DataTable).Rows.Count == 0) && (this.grdReworkArea.DataSource == null || (this.grdReworkArea.DataSource as DataTable).Rows.Count == 0))
                        {
                            throw MessageException.Create("RequiredSegmentAndWorkArea");
                        }
                        if ((this.grdOperation.DataSource == null || (this.grdOperation.DataSource as DataTable).Rows.Count == 0))
                        {
                            throw MessageException.Create("RequiredSegment");
                        }
                        if ((this.grdReworkArea.DataSource == null || (this.grdReworkArea.DataSource as DataTable).Rows.Count == 0))
                        {
                            throw MessageException.Create("RequiredWorkArea");
                        }
                    }
                }
            }

            CheckDuplicationId(grdOperation, "DuplicationUserSequence", "USERSEQUENCE");

            CheckDuplicationId(grdOperation, "DuplicationSegmentID", "PROCESSSEGMENTID");

            CheckDuplicationIdAndVersion(grdReworkProduct, "DuplicationProduct", "RESOURCEID", "RESOURCEVERSION");

            CheckDuplicationId(grdReworkArea, "DuplicationArea", "RESOURCEID");

            CheckDuplicationIdAndVersion(grdConsumable, "DuplicationConsumable", "MATERIALDEFID", "MATERIALDEFVERSION");

            CheckDuplicationIdAndVersion(grdResource, "DuplicationResource", "RESOURCEID", "RESOURCEVERSION");

            CheckDuplicationId(grdEquipment, "DuplicationEquipment", "RESOURCEID");





            //재작업 라우팅
            string productDefinition = "productDefinitionList";

            //공정
            string processPath = "processPathList";

            DataTable dtProcessPathList = new DataTable();
            dtProcessPathList.Columns.Add("ENTERPRISEID");
            dtProcessPathList.Columns.Add("PLANTID");
            dtProcessPathList.Columns.Add("PROCESSPATHID");
            dtProcessPathList.Columns.Add("USERSEQUENCE");
            dtProcessPathList.Columns.Add("PROCESSSEGMENTID");
            dtProcessPathList.Columns.Add("PROCESSSEGMENTVERSION");
            dtProcessPathList.Columns.Add("VALIDSTATE");
            dtProcessPathList.Columns.Add("PROCESSDEFID");
            dtProcessPathList.Columns.Add("PROCESSDEFVERSION");
            dtProcessPathList.Columns.Add("PATHSEQUENCE");
            dtProcessPathList.Columns.Add("PATHTYPE");
            dtProcessPathList.Columns.Add("ALTERNATIVERESOURCE");
            dtProcessPathList.Columns.Add("ASSIGNEQUIPMENT");
            dtProcessPathList.Columns.Add("DESCRIPTION");
            dtProcessPathList.Columns.Add("PROCESSUOM");
            dtProcessPathList.Columns.Add("PRODUCTDEFID");
            dtProcessPathList.Columns.Add("PRODUCTDEFVERSION");
            dtProcessPathList.Columns.Add("RESOURCETYPE");
            dtProcessPathList.Columns.Add("EQUIPMENTID");
            dtProcessPathList.Columns.Add("RESOURCECLASSID");
            dtProcessPathList.Columns.Add("RESOURCEID");
            dtProcessPathList.Columns.Add("RESOURCEVERSION");
            dtProcessPathList.Columns.Add("ISPRIMARYRESOURCE");
            dtProcessPathList.Columns.Add("_STATE_");

            foreach (DataRow row in dtProcessPath.Rows)
            {
                dtProcessPathList.Rows.Add(new object[]
                {
                    UserInfo.Current.Enterprise
                   , row["PLANTID"].ToString()
                   , row["PROCESSPATHID"].ToString()
                   , row["USERSEQUENCE"].ToString()
                   , row["PROCESSSEGMENTID"].ToString()
                   , row["PROCESSSEGMENTVERSION"].ToString()
                   , "Valid"
                   , row["PROCESSDEFID"].ToString()
                   , row["PROCESSDEFVERSION"].ToString()
                   , string.IsNullOrWhiteSpace(row["PATHSEQUENCE"].ToString()) == true ? "0" :row["PATHSEQUENCE"].ToString()
                   , row["PATHTYPE"].ToString()
                   , row["ALTERNATIVERESOURCE"].ToString()
                   , row["ASSIGNEQUIPMENT"].ToString()
                   , row["COMMENT"].ToString()
                   , row["PROCESSUOM"].ToString()
                   , "*"
                   , "*"
                   , "Resource"
                   , ""
                   , ""
                   , row["RESOURCEID"].ToString()
                   , row["RESOURCEVERSION"].ToString()
                   , row["ISPRIMARYRESOURCE"].ToString()
                   , row["_STATE_"].ToString()

                });
            }


            //제품
            string productList = "productList";


            //작업장
            string areaList = "areaList";

            //자재
            string consumableList = "consumableList";

            string resourceList = "resourceList";

            string equipmentList = "equipmentList";



            MessageWorker worker = new MessageWorker("ReworkRoutingMgnt");
            worker.SetBody(new MessageBody()
                        {
                             { productDefinition, dtRework }
                           , { processPath, dtProcessPathList }
                           , { productList, dtProduct }
                           , { areaList, dtWorkArea }
                           , { consumableList, dtConsumable }
                           , { resourceList, dtResource }
                           , { equipmentList, dtEquipment }
                        });

            worker.Execute();

            this.lastFocusedOperationRowIndex = this.grdProcessdefinition.View.FocusedRowHandle;

        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴

            ClearDataTable(true);

            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

       
            DataTable dtProcessdefinition = await SqlExecuter.QueryAsync("GetProcessdefinitionList", "10001", values);

            if (dtProcessdefinition.Rows.Count < 1) // 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            this.grdProcessdefinition.DataSource = dtProcessdefinition;

            if (this.lastFocusedOperationRowIndex > -1)
            {
                if (dtProcessdefinition.Rows.Count < this.lastFocusedOperationRowIndex)
                    this.lastFocusedOperationRowIndex = dtProcessdefinition.Rows.Count - 1;
            }
            else
                this.lastFocusedOperationRowIndex = 0;

            this.grdProcessdefinition.View.FocusedRowHandle = this.lastFocusedOperationRowIndex;


            SearchAllData();
        }

        private void SearchAllData()
        {
            if (grdProcessdefinition.View.FocusedRowHandle >= 0)
            {

                ClearDataTable(false);

                     DataRow row = grdProcessdefinition.View.GetFocusedDataRow();

                Dictionary<string, object> Param = new Dictionary<string, object>();

                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("PROCESSDEFID", row["PROCESSDEFID_R"].ToString());
                Param.Add("PROCESSDEFVERSION", row["PROCESSDEFVERSION_R"].ToString());
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                SearchGrid(grdOperation, "GetProcessPathList", "10004", Param);
                SearchGrid(grdReworkProduct, "GetProductReworkControl", "10001", Param);
                SearchGrid(grdReworkArea, "GetAreaReworkControl", "10001", Param);


                SearchGridTabControl2(this.smartTabControl2.SelectedTabPageIndex);

            }
            else
            {

                ClearDataTable(false);

            }
        }

        #endregion
        /// <summary>
        /// 검색조건 초기화. 
        /// 조회조건 정보, 메뉴 - 조회조건 매핑 화면에 등록된 정보를 기준으로 구성됩니다.
        /// DB에 등록한 정보를 제외한 추가 조회조건 구성이 필요한 경우 사용합니다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //재작업구분
            Conditions.AddComboBox("PROCESSCLASSID_R", new SqlQuery("GetProcessclassCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSCLASSNAME", "PROCESSCLASSID").SetEmptyItem();
            InitializeCondition_Popup();

            // 버전
            Conditions.AddComboBox("PROCESSDEFVERSION_R", new SqlQuery("GetProcessdefVersion", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "PROCESSDEFVERSIONNAME", "PROCESSDEFVERSIONCODE").SetEmptyItem();

            // 대공정
            Conditions.AddComboBox("TOPPROCESSSEGMENTID", new SqlQuery("GetProcessSegMentTop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID").SetEmptyItem();



        }

        /// <summary>
        /// 툴바 버튼 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);

            SmartButton btn = sender as SmartButton;
            if (btn == null)
                return;

            if(btn.Name.ToUpper().Equals("COPY"))
            {
                CopySaveReworkRoutingAsync("COPY");
            }
            else if(btn.Name.ToUpper().Equals("VERSIONUP"))
            {
                CopySaveReworkRoutingAsync("VERSIONUP");
            }
            else if (btn.Name.ToUpper().Equals("ROUTINGSEGMENTSEQUENCE"))
            {
                DataTable dtOperation = this.grdOperation.DataSource as DataTable;

                SetProcesssegmentSequence(dtOperation);
            
            }

        }

        private void SetProcesssegmentSequence(DataTable dtOperation)
        {
            int sequence = 0;

            this.grdOperation.View.PostEditor();
         

            DataTable changeTable = dtOperation.AsEnumerable().OrderBy(s => Convert.ToInt32(s["USERSEQUENCE"])).CopyToDataTable();


            for (int i = 0; i < changeTable.Rows.Count; i++)
            {
                changeTable.Rows[i]["USERSEQUENCE"] = sequence += 10;

                dtOperation.Rows[i].ItemArray = changeTable.Rows[i].ItemArray;
            }

            this.grdOperation.View.UpdateCurrentRow();

            this.grdOperation.View.RaiseValidateRow();


        }


        private void CopySaveReworkRoutingAsync(string state)
        {
            if (grdProcessdefinition.View.FocusedRowHandle < 0)
                return;

            DataRow rowFocusedData = grdProcessdefinition.View.GetFocusedDataRow();


            if (rowFocusedData.RowState != DataRowState.Added)
            {
                DataTable dtRework = new DataTable();

                dtRework.Columns.Add("PROCESSDEFID_R");
                dtRework.Columns.Add("PROCESSDEFVERSION_R");
                dtRework.Columns.Add("TOPPROCESSSEGMENTID");
                dtRework.Columns.Add("ENTERPRISEID");
                dtRework.Columns.Add("PLANTID");
                dtRework.Columns.Add("_STATE_");
                dtRework.Rows.Add(new object[]
                {
                  rowFocusedData["PROCESSDEFID_R"].ToString()
                , rowFocusedData["PROCESSDEFVERSION_R"].ToString()
                , rowFocusedData["TOPPROCESSSEGMENTID"].ToString()
                , UserInfo.Current.Enterprise
                , UserInfo.Current.Plant
                , state

                });

                MessageWorker worker = new MessageWorker("ReworkRoutingCopy");
                worker.SetBody(new MessageBody()
                        {
                             { "productDefinitionList", dtRework }
                        });

                worker.Execute();

                MSGBox.Show(MessageBoxType.Information, "SuccedSave");

                this.lastFocusedOperationRowIndex = this.grdOperation.View.FocusedRowHandle;

                this.OnSearchAsync();
            }
            else
            {
                if (state.Equals("COPY"))
                {
                    throw MessageException.Create("CannotCopyNewReworkRouting");
                }
                else if (state.Equals("VERSIONUP"))
                {
                    throw MessageException.Create("CannotVersionUpNewReworkRouting");
                }


            }
        }
            /// <summary>
            /// 검색조건 팝업 예제
            /// </summary>
            private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("PROCESSDEFID_R", new SqlQuery("GetReworkRoutingProcessdefGroup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
               .SetPopupLayout("PROCESSDEFNAME", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1);  //팝업창 선택가능한 개수


            // 팝업에서 사용할 조회조건 항목 추가
            parentPopupColumn.Conditions.AddTextBox("PROCESSDEFID_R");
            parentPopupColumn.Conditions.AddTextBox("PROCESSDEFNAME_R");


            // 팝업 그리드 설정
            parentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSDEFID_R", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSDEFNAME_R", 200);

        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            SmartSelectPopupEdit Popupedit = Conditions.GetControl<SmartSelectPopupEdit>("PROCESSDEFID_R");
            Popupedit.Validated += Popupedit_Validated;

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
                sItemcode = row["PROCESSDEFID_R"].ToString();

            }

        }

        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            this.grdProcessdefinition.View.CheckValidation();
            this.grdOperation.View.CheckValidation();
            this.grdReworkProduct.View.CheckValidation();
            this.grdReworkArea.View.CheckValidation();
            this.grdConsumable.View.CheckValidation();
            this.grdResource.View.CheckValidation();
            this.grdEquipment.View.CheckValidation();


        }
        #endregion


    }
}

