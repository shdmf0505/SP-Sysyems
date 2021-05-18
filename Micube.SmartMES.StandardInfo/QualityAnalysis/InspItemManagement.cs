#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
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
using Micube.SmartMES.Commons;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > 검사항목관리
    /// 업  무  설  명  : 품질 기준 정보의 검사 방법, 검사 항목을 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-06-14
    /// 수  정  이  력  : 김용조 : 2019-11-26 : 전체변경
    /// 
    /// 
    /// </summary>
    public partial class InspItemManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private ConditionItemComboBox comboboxINSPITEMCLASSTYPE;

        private string _inspMethodID;
        private string _inspMethodName;
        private string _selectedNodeID = "";
        #endregion

        #region 생성자

        public InspItemManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            // TODO : 컨트롤 초기화 로직 구성            
            InitializeEvent();
            InitializeGridInspectionItem();
            InitializeGridInspectionMethodCode();
            InitializeGridInspectionMethod();
            InitializeGridInspectionMethodItem();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>

        #region grdInspectionItem 초기화
        private void InitializeGridInspectionItem()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInspectionItem.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspectionItem.GridButtonItem = GridButtonItem.Add|GridButtonItem.Export ; //GetCodeListByToolWithLanguage

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly()
                ;
            grdInspectionItem.View.AddTextBoxColumn("INSPITEMKEYNAME", 150)
                .SetIsHidden()
                ;
            grdInspectionItem.View.AddLanguageColumn("INSPITEMNAME", 200);
            grdInspectionItem.View.AddComboBoxColumn("UNIT", 80
             , new SqlQuery("GetUomDefinitionMapListByStd", "10001", "UOMCATEGORY=RawInspection", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID"); //  단위
            grdInspectionItem.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdInspectionItem.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();
            grdInspectionItem.View.AddComboBoxColumn("EQPINSPITEMID", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=EquipmentInspectionItem", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("EQUIPMENTINSPECTIONITEM")
                .SetTextAlignment(TextAlignment.Center)
                .SetEmptyItem("","")
                .SetDefault("");
            grdInspectionItem.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionItem.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()                
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionItem.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionItem.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionItem.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionItem.View.PopulateColumns();
        }

        #endregion

        #region InspectionMethodCode 초기화
        private void InitializeGridInspectionMethodCode()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInspectionMethodCode.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspectionMethodCode.GridButtonItem = GridButtonItem.Add|GridButtonItem.Export;

            grdInspectionMethodCode.View.AddTextBoxColumn("INSPECTIONMETHODID", 150)
                ;
            grdInspectionMethodCode.View.AddLanguageColumn("INSPECTIONMETHODNAME", 200);
            grdInspectionMethodCode.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdInspectionMethodCode.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();
            grdInspectionMethodCode.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionMethodCode.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethodCode.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethodCode.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethodCode.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethodCode.View.SetKeyColumn("INSPECTIONMETHODID");
            grdInspectionMethodCode.View.PopulateColumns();            
        }
        #endregion

        #region InspectionMethod 초기화
        private void InitializeGridInspectionMethod()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInspectionMethod.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspectionMethod.GridButtonItem = GridButtonItem.Add|GridButtonItem.Export; //GetCodeListByToolWithLanguage

            grdInspectionMethod.View.AddTextBoxColumn("INSPECTIONMETHODID", 150)
                .SetIsReadOnly()
                ;
            //grdInspectionMethod.View.AddTextBoxColumn("ITEMNAME", 150)
            //    ;
            InitializeMethodPopup();
            grdInspectionMethod.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdInspectionMethod.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();
            grdInspectionMethod.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethod.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethod.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethod.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethod.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethod.View.SetKeyColumn("INSPECTIONMETHODID", "INSPECTIONMETHODNAME");
            grdInspectionMethod.View.PopulateColumns();
        }
        #endregion

        #region InitializeMethodPopup
        private void InitializeMethodPopup()
        {
            var popupMethodGrid = grdInspectionMethod.View.AddSelectPopupColumn("INSPECTIONMETHODNAME", 200, new SqlQuery("GetCodeListByStandardInfoWithLanguage", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", "CODECLASSID=InspectionMethod"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("INSPECTIONMETHODNAME", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("INSPECTIONMETHODNAME", "INSPECTIONMETHODNAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()
                //.SetRelationIds("EXCEPTTYPE", "ENTERPRISEID", "PLANTID")
                //.SetPopupAutoFillColumns("ITEMNAME")
                //.SetPopupQueryPopup((DataRow currentrow) =>
                //{
                //    if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("EXCEPTTYPE")))
                //    {
                //        this.ShowMessage("NoSelectSite");
                //        return false;
                //    }

                //    return true;
                //})
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int irow = 0;
                    int crow = 0;
                    
                    DataRow classRow = grdInspectionMethod.View.GetFocusedDataRow();
                    crow = grdInspectionMethod.View.FocusedRowHandle;

                    foreach (DataRow row in selectedRows)
                    {
                        if (irow == 0)
                        {
                            if (checkGridProcess(row.GetString("INSPECTIONMETHODID"), grdInspectionMethod, "INSPECTIONMETHODID") == -1)
                            {
                                classRow["INSPECTIONMETHODID"] = row["INSPECTIONMETHODID"];
                                classRow["INSPECTIONMETHODNAME"] = row["INSPECTIONMETHODNAME"];
                                grdInspectionMethod.View.RaiseValidateRow(crow);
                            }
                            else
                            {
                                classRow["INSPECTIONMETHODID"] = "";
                                classRow["INSPECTIONMETHODNAME"] = "";
                                irow = irow - 1;
                            }
                        }
                        else
                        {
                            popAddGrid(row, grdInspectionMethod, "INSPECTIONMETHODID", "INSPECTIONMETHODID", "INSPECTIONMETHODNAME", "");
                        }
                        irow = irow + 1;
                    }

                })

            ;
            // 팝업에서 사용할 조회조건 항목 추가   .SetRelationIds("EXCEPTTYPE", "ENTERPRISEID","PLANTID")
            popupMethodGrid.Conditions.AddTextBox("INSPECTIONMETHODNAME");
            //popupGridExceptid.Conditions.AddTextBox("ENTERPRISEID")
            //    .SetPopupDefaultByGridColumnId("ENTERPRISEID")
            //    .SetIsHidden();
            //popupMethodGrid.Conditions.AddTextBox("PLANTID")
            //    .SetPopupDefaultByGridColumnId("PLANTID")
            //    .SetIsHidden();
            //popupMethodGrid.Conditions.AddTextBox("EXCEPTTYPE")
            //   .SetPopupDefaultByGridColumnId("EXCEPTTYPE")
            //   .SetIsHidden();
            // 팝업 그리드 설정
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPECTIONMETHODID", 150)
                .SetValidationKeyColumn();
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetValidationKeyColumn();
        }
        #endregion

        #region InspectionMethodItem 초기화
        private void InitializeGridInspectionMethodItem()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInspectionMethodItem.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspectionMethodItem.GridButtonItem = GridButtonItem.Add|GridButtonItem.Export;  //GetCodeListByToolWithLanguage

            grdInspectionMethodItem.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly()
                ;
            grdInspectionMethodItem.View.AddTextBoxColumn("INSPITEMVERSION", 80)
                .SetIsHidden()
                ;
            //grdInspectionMethodItem.View.AddTextBoxColumn("INSPITEMNAME", 150)
            // ;
            InitializeMethodItemPopup();
            grdInspectionMethodItem.View.AddComboBoxColumn("INSPITEMTYPEID", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))                                
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethodItem.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdInspectionMethodItem.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();
            grdInspectionMethodItem.View.AddTextBoxColumn("UNIT", 80)
                .SetIsReadOnly(); 
            grdInspectionMethodItem.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethodItem.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethodItem.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethodItem.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethodItem.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionMethodItem.View.SetKeyColumn("INSPITEMID", "INSPITEMNAME");
            grdInspectionMethodItem.View.PopulateColumns();
        }
        #endregion

        #region InitializeMethodItemPopup
        private void InitializeMethodItemPopup()
        {
            var popupMethodGrid = grdInspectionMethodItem.View.AddSelectPopupColumn("INSPITEMNAME", 200, new SqlQuery("GetInspItemListForMethodByStandardInfo", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("INSPITEMNAME", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("INSPITEMNAME", "INSPITEMNAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()
                //.SetRelationIds("EXCEPTTYPE", "ENTERPRISEID", "PLANTID")
                //.SetPopupAutoFillColumns("ITEMNAME")
                //.SetPopupQueryPopup((DataRow currentrow) =>
                //{
                //    if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("EXCEPTTYPE")))
                //    {
                //        this.ShowMessage("NoSelectSite");
                //        return false;
                //    }

                //    return true;
                //})
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int irow = 0;
                    int crow = 0;

                    DataRow classRow = grdInspectionMethodItem.View.GetFocusedDataRow();
                    crow = grdInspectionMethodItem.View.FocusedRowHandle;

                    foreach (DataRow row in selectedRows)
                    {
                        if (irow == 0)
                        {
                            if (checkGridProcess(row.GetString("INSPITEMID"), grdInspectionMethodItem, "INSPITEMID") == -1)
                            {
                                classRow["INSPITEMID"] = row["INSPITEMID"];
                                classRow["INSPITEMNAME"] = row["INSPITEMNAME"];
                                classRow["INSPITEMVERSION"] = row["INSPITEMVERSION"];
                                classRow["UNIT"] = row["UNIT"];
                                grdInspectionMethodItem.View.RaiseValidateRow(crow);
                            }
                            else
                            {
                                classRow["INSPITEMID"] = "";
                                classRow["INSPITEMNAME"] = "";
                                classRow["UNIT"] = "";
                                irow = irow - 1;
                            }
                        }
                        else
                        {
                            popAddGrid(row, grdInspectionMethodItem, "INSPITEMID", "INSPITEMID", "INSPITEMNAME", "INSPITEMVERSION");
                        }
                        irow = irow + 1;
                    }

                })

            ;
            // 팝업에서 사용할 조회조건 항목 추가   .SetRelationIds("EXCEPTTYPE", "ENTERPRISEID","PLANTID")
            //popupGridExceptid.Conditions.AddTextBox("EXCEPTNAME");
            //popupGridExceptid.Conditions.AddTextBox("ENTERPRISEID")
            //    .SetPopupDefaultByGridColumnId("ENTERPRISEID")
            //    .SetIsHidden();
            //popupMethodGrid.Conditions.AddTextBox("PLANTID")
            //    .SetPopupDefaultByGridColumnId("PLANTID")
            //    .SetIsHidden();
            //popupMethodGrid.Conditions.AddTextBox("EXCEPTTYPE")
            //   .SetPopupDefaultByGridColumnId("EXCEPTTYPE")
            //   .SetIsHidden();
            // 팝업 그리드 설정
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 80)
                .SetIsReadOnly();
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200)
                .SetIsReadOnly();
            popupMethodGrid.GridColumns.AddTextBoxColumn("UNIT", 80)
               .SetIsReadOnly();
        }
        #endregion
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //선택된 탭이 바뀔때 이벤트
            trvInspectionMethod.FocusedNodeChanged += TrvInspectionMethod_FocusedNodeChanged;
            grdInspectionMethod.View.FocusedRowChanged += grdInspectionMethod_FocusedRowChanged;
        }

        private void grdInspectionMethod_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if(grdInspectionMethod.View.RowCount > 0)
                if(grdInspectionMethod.View.FocusedRowHandle > -1)
                    SearchInspectionMethodItem(e.FocusedRowHandle);
        }

        private void TrvInspectionMethod_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            _selectedNodeID = e.Node.GetValue("ITEMID").ToString();
            SearchInspectionMethod();
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// 선택된 탭에 따라 다른 룰 실행
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = null;
            // TODO : 저장 Rule 변경
            switch (tabInspItem1.SelectedTabPageIndex)
            {
                case 0:
                    SaveInspectionItemData();
                    break;
                case 1:
                    SaveInspectionMethodCodeData();
                    break;
                case 2:
                    SaveInspectionMethodData();
                    SaveInspectionMethodItemData();
                    //Research();
                    break;
            }
        }

        #endregion

        #region 검색
        #region OnSearchAsync
        /// <summary>
        /// 비동기 override 모델
        /// 선택된 탭에 따라 다른 쿼리 실행
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            DataTable dt = null;
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);

            switch (tabInspItem1.SelectedTabPageIndex)
            {
                case 0:
                    dt = await SqlExecuter.QueryAsync("GetInspItemListByStandardInfo", "10001", values);
                    grdInspectionItem.DataSource = dt;                    
                    break;
                case 1:
                    dt = await SqlExecuter.QueryAsync("GetInspItemMethodCodeListByStandardInfo", "10001", values);
                    grdInspectionMethodCode.DataSource = dt;
                    break;
                case 2:
                    trvInspectionMethod.FocusedNodeChanged -= TrvInspectionMethod_FocusedNodeChanged;
                    trvInspectionMethod.SetResultCount(1);
                    trvInspectionMethod.SetIsReadOnly();
                    trvInspectionMethod.SetEmptyRoot(UserInfo.Current.Enterprise, "PARENT");
                    trvInspectionMethod.SetMember("ITEMNAME", "ITEMID", "PARENTID");

                    dt = await SqlExecuter.QueryAsync("GetInspItemMethodListForTreeByStandardInfo", "10001", values);
                    if (dt != null)
                    {
                        if (dt.Rows.Count != 0)
                        {
                            trvInspectionMethod.DataSource = dt;
                            trvInspectionMethod.PopulateColumns();
                            trvInspectionMethod.ExpandAll();
                            if (_selectedNodeID != "")
                            {
                                trvInspectionMethod.SetFocusedNode(trvInspectionMethod.FindNodeByFieldValue("ITEMID", _selectedNodeID));
                            }
                            else
                            {
                                trvInspectionMethod.FocusedNode = trvInspectionMethod.FindNodeByID(0);
                            }

                            _inspMethodID = "";
                            _inspMethodName = "";
                        }
                    }
                    trvInspectionMethod.FocusedNodeChanged += TrvInspectionMethod_FocusedNodeChanged;
                    break;
            }
        }
        #endregion

        #region Research
        /// <summary>
        /// 비동기 override 모델
        /// 선택된 탭에 따라 다른 쿼리 실행
        /// </summary>
        private void Research()
        {
            // TODO : 조회 SP 변경
            DataTable dt = null;
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);

            switch (tabInspItem1.SelectedTabPageIndex)
            {
                case 0:
                    dt = SqlExecuter.Query("GetInspItemListByStandardInfo", "10001", values);
                    grdInspectionItem.DataSource = dt;
                    break;
                case 1:
                    dt = SqlExecuter.Query("GetInspItemMethodCodeListByStandardInfo", "10001", values);
                    grdInspectionMethodCode.DataSource = dt;
                    break;
                case 2:
                    trvInspectionMethod.SetResultCount(1);
                    trvInspectionMethod.SetIsReadOnly();
                    trvInspectionMethod.SetEmptyRoot(UserInfo.Current.Enterprise, "PARENT");
                    trvInspectionMethod.SetMember("ITEMNAME", "ITEMID", "PARENTID");

                    dt = SqlExecuter.Query("GetInspItemMethodListForTreeByStandardInfo", "10001", values);
                    if (dt != null)
                    {

                        if (dt.Rows.Count != 0)
                        {
                            trvInspectionMethod.DataSource = dt;
                            trvInspectionMethod.PopulateColumns();
                            trvInspectionMethod.ExpandAll();
                        }
                    }
                    break;
            }
        }
        #endregion

        #region InspectionMethod 검색
        protected void SearchInspectionMethod()
        {
            if (trvInspectionMethod.FocusedNode != null)
            {
                string inspectionClassID = "", inspectionMethodID = "";

                if (trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString().IndexOf("_") > -1)
                {
                    inspectionMethodID = trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString().Split('_')[1];
                    inspectionClassID = trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString().Split('_')[0];
                }
                else
                    inspectionClassID = trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString();

                // TODO : 조회 SP 변경
                DataTable dt = null;
                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
                values.Add("INSPECTIONMETHODID", inspectionMethodID);
                values.Add("INSPECTIONCLASSID", inspectionClassID);

                dt = SqlExecuter.Query("GetInspItemMethodListForClassByStandardInfo", "10001", values);
                grdInspectionMethod.DataSource = dt;

                if(dt.Rows.Count > 0)
                {
                    SearchInspectionMethodItem(0);
                }
                else
                {
                    grdInspectionMethodItem.View.ClearDatas();
                }
            }
        }
        #endregion

        #region InspectionMethodItem 검색
        private void SearchInspectionMethodItem(int rowHandle)
        {
            if (trvInspectionMethod.FocusedNode != null)
            {
                if (grdInspectionMethod.View.RowCount > 0)
                {
                    string inspectionClassID = "", inspectionMethodID = "";

                    _inspMethodID = grdInspectionMethod.View.GetRowCellValue(rowHandle, "INSPECTIONMETHODID").ToString();
                    _inspMethodName = grdInspectionMethod.View.GetRowCellValue(rowHandle, "INSPECTIONMETHODNAME").ToString();
                    //ClassID를 가져와야 한다.
                    if (trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString().IndexOf("_") > -1)
                        inspectionClassID = trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString().Split('_')[0];
                    else
                        inspectionClassID = trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString();

                    inspectionMethodID = grdInspectionMethod.View.GetRowCellValue(rowHandle, "INSPECTIONMETHODID").ToString();

                    // TODO : 조회 SP 변경
                    DataTable dt = null;
                    var values = Conditions.GetValues();
                    values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                    values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
                    values.Add("INSPECTIONCLASSID", inspectionClassID);
                    values.Add("INSPECTIONMETHODID", inspectionMethodID);

                    dt = SqlExecuter.Query("GetInspItemMethodItemListForClassByStandardInfo", "10001", values);
                    grdInspectionMethodItem.DataSource = dt;
                }
            }
        }
        #endregion

        #region 검색조건 초기화
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            // 검사정의
            
            Conditions.AddComboBox("VALIDSTATE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem();

            //Conditions.AddTextBox("INSPITEMCLASSID");
            //Conditions.AddTextBox("INSPITEMCLASSNAME");

            //InitializeCondition_Popup();
        }
        /// <summary>
        /// 검색조건 품목팝업 
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("PARENTINSPITEMCLASSID", new SqlQuery("SelectInspItemClass", "10001", $"P_VALIDSTATE={"Valid"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSNAME", "INSPITEMCLASSID")
               .SetPopupLayout("PARENTINSPITEMCLASSID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultMapping("PARENTINSPITEMCLASSID", "INSPITEMCLASSID")

               .SetPopupResultCount(1);  //팝업창 선택가능한 개수


            comboboxINSPITEMCLASSTYPE = parentPopupColumn.Conditions.AddComboBox("INSPECTIONDEFID", new SqlQuery("GetQcInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID");


            parentPopupColumn.Conditions.AddTextBox("INSPITEMCLASSID");
            parentPopupColumn.Conditions.AddTextBox("INSPITEMCLASSNAME");

            //parentProcesssegMnet.Conditions.AddTextBox("AREANAME");

            // 팝업 그리드 설정
            parentPopupColumn.GridColumns.AddComboBoxColumn("INSPITEMCLASSTYPE", 80, new SqlQuery("GetQcInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID");
            parentPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 200);

        }
        /// <summary>
        /// 최초에 첫번째 탭에 조회조건 바인딩
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();


            //SmartComboBox comboboxINSPECTIONDEFID = Conditions.GetControl<SmartComboBox>("INSPECTIONDEFID");
            //comboboxINSPECTIONDEFID.ItemIndex = 1;
            //comboboxINSPITEMCLASSTYPE.SetDefault(comboboxINSPECTIONDEFID.EditValue);

            //comboboxINSPECTIONDEFID.TextChanged += ComboboxINSPECTIONDEFID_TextChanged;

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용     
        }
        private void ComboboxINSPECTIONDEFID_TextChanged(object sender, EventArgs e)
        {
            SmartComboBox combox = (SmartComboBox)sender;
            comboboxINSPITEMCLASSTYPE.SetDefault(combox.EditValue);
        }
        #endregion
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// 선택된 탭에 따라 다른 그리드 유효성
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        private bool ValidateInspectionItem(DataTable allTable, DataTable insertTable, out string messageCode)
        {
            messageCode = "";
            //이름 중복 검사(K)기준
            int sameCount = 0;
            foreach(DataRow ruleRow in insertTable.Rows)
            {
                sameCount = 0;
                foreach(DataRow targetRow in allTable.Rows)
                {
                    if (ruleRow.GetString("INSPITEMNAME$$KO-KR").Equals(targetRow.GetString("INSPITEMNAME$$KO-KR")))
                    {
                        sameCount++;
                    }
                }

                if (sameCount > 1)
                {
                    messageCode = "ValidateInspItemNameEqual"; //TODO : 메세지 지정
                    return false;
                }
            }

            return true;
        }

        private bool ValidateInspectionMethodCode(DataTable allTable, DataTable insertTable, out string messageCode)
        {
            messageCode = "";
            //이름 중복 검사(K)기준
            int sameCount = 0;
            foreach (DataRow ruleRow in insertTable.Rows)
            {
                sameCount = 0;
                foreach (DataRow targetRow in allTable.Rows)
                {
                    if (ruleRow.GetString("INSPECTIONMETHODID").Equals(targetRow.GetString("INSPECTIONMETHODID")))
                    {
                        sameCount++;
                    }
                }

                if (sameCount > 1)
                {
                    messageCode = "ValidateInspItemNameEqual"; //TODO : 메세지 지정
                    return false;
                }
            }

            return true;
        }

        private bool ValidateInspectionMethod(DataTable allTable, DataTable insertTable, out string messageCode)
        {
            messageCode = "";
            //이름 중복 검사(K)기준
            int sameCount = 0;
            foreach (DataRow ruleRow in insertTable.Rows)
            {
                sameCount = 0;
                foreach (DataRow targetRow in allTable.Rows)
                {
                    if (ruleRow.GetString("INSPITEMID").Equals(targetRow.GetString("INSPITEMID")))
                    {
                        sameCount++;
                    }
                }

                if (sameCount > 1)
                {
                    messageCode = "ValidateInspItemNameEqual"; //TODO : 메세지 지정
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Private Function
        #region popAddGrid : 검색팝업창에서 다수의 행 선택시 자동으로 행을 만들어주는 메소드
        private void popAddGrid(DataRow dr, SmartBandedGrid targetGrid, string columnName, string insertIDColumnName, string insertNameColumnName, string insertVersionColumnName)
        {
            DateTime dateNow = DateTime.Now;
            string itemID = dr.GetString(insertIDColumnName);            
            if (checkGridProcess(itemID, targetGrid, columnName) == -1)
            {
                targetGrid.View.AddNewRow();
                int irow = targetGrid.View.RowCount - 1;
                DataRow classRow = targetGrid.View.GetDataRow(irow);
                
                if (!insertVersionColumnName.Equals(""))
                {
                    classRow["INSPITEMID"] = itemID;
                    classRow["INSPITEMNAME"] = dr[insertNameColumnName];
                    classRow["INSPITEMVERSION"] = dr[insertVersionColumnName];
                    classRow["UNIT"] = dr["UNIT"];

                }
                else
                {
                    classRow["INSPECTIONMETHODID"] = itemID;
                    classRow["INSPECTIONMETHODNAME"] = dr[insertNameColumnName];
                }

                targetGrid.View.RaiseValidateRow(irow);
            }

        }

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

        #region GetInspectionItemTable
        private DataTable GetInspectionItemTable()
        {
            DataTable returnTable = new DataTable();
            returnTable.TableName = "inspectionItemTable";

            returnTable.Columns.Add("INSPITEMID");
            returnTable.Columns.Add("INSPITEMKEYNAME");
            returnTable.Columns.Add("INSPITEMNAME$$KO-KR");
            returnTable.Columns.Add("INSPITEMNAME$$EN-US");
            returnTable.Columns.Add("INSPITEMNAME$$ZH-CN");
            returnTable.Columns.Add("INSPITEMNAME$$VI-VN");
            
            returnTable.Columns.Add("DESCRIPTION");

            returnTable.Columns.Add("ENTERPRISEID");
            returnTable.Columns.Add("PLANTID");

            returnTable.Columns.Add("CREATOR");
            returnTable.Columns.Add("CREATEDTIME");
            returnTable.Columns.Add("MODIFIER");
            returnTable.Columns.Add("MODIFIEDTIME");
            returnTable.Columns.Add("LASTTXNHISTKEY");
            returnTable.Columns.Add("LASTTXNID");
            returnTable.Columns.Add("LASTTXNUSER");
            returnTable.Columns.Add("LASTTXNTIME");
            returnTable.Columns.Add("LASTTXNCOMMENT");
            returnTable.Columns.Add("EQPINSPITEMID");
            returnTable.Columns.Add("UNIT");
            returnTable.Columns.Add("VALIDSTATE");
            returnTable.Columns.Add("_STATE_");

            return returnTable;
        }
        #endregion

        #region GetInspectionMethodCodeTable
        private DataTable GetInspectionMethodCodeTable()
        {
            DataTable returnTable = new DataTable();
            returnTable.TableName = "inspectionMethodTable";

            returnTable.Columns.Add("INSPECTIONMETHODID");
            returnTable.Columns.Add("INSPECTIONMETHODNAME");
            returnTable.Columns.Add("INSPECTIONMETHODNAME$$KO-KR");
            returnTable.Columns.Add("INSPECTIONMETHODNAME$$EN-US");
            returnTable.Columns.Add("INSPECTIONMETHODNAME$$ZH-CN");
            returnTable.Columns.Add("INSPECTIONMETHODNAME$$VI-VN");
            returnTable.Columns.Add("DESCRIPTION");

            returnTable.Columns.Add("ENTERPRISEID");
            returnTable.Columns.Add("PLANTID");

            returnTable.Columns.Add("CREATOR");
            returnTable.Columns.Add("CREATEDTIME");
            returnTable.Columns.Add("MODIFIER");
            returnTable.Columns.Add("MODIFIEDTIME");
            returnTable.Columns.Add("LASTTXNHISTKEY");
            returnTable.Columns.Add("LASTTXNID");
            returnTable.Columns.Add("LASTTXNUSER");
            returnTable.Columns.Add("LASTTXNTIME");
            returnTable.Columns.Add("LASTTXNCOMMENT");
            returnTable.Columns.Add("VALIDSTATE");
            returnTable.Columns.Add("_STATE_");

            return returnTable;
        }
        #endregion

        #region GetInspectionMethodTable
        private DataTable GetInspectionMethodTable()
        {
            DataTable returnTable = new DataTable();
            returnTable.TableName = "inspectionMethodTable";

            returnTable.Columns.Add("INSPECTIONCLASSID");
            returnTable.Columns.Add("INSPECTIONMETHODID");
            returnTable.Columns.Add("INSPECTIONMETHODNAME");
            returnTable.Columns.Add("DESCRIPTION");

            returnTable.Columns.Add("ENTERPRISEID");
            returnTable.Columns.Add("PLANTID");

            returnTable.Columns.Add("CREATOR");
            returnTable.Columns.Add("CREATEDTIME");
            returnTable.Columns.Add("MODIFIER");
            returnTable.Columns.Add("MODIFIEDTIME");
            returnTable.Columns.Add("LASTTXNHISTKEY");
            returnTable.Columns.Add("LASTTXNID");
            returnTable.Columns.Add("LASTTXNUSER");
            returnTable.Columns.Add("LASTTXNTIME");
            returnTable.Columns.Add("LASTTXNCOMMENT");
            returnTable.Columns.Add("VALIDSTATE");
            returnTable.Columns.Add("_STATE_");

            return returnTable;
        }
        #endregion

        #region GetInspectionMethodItemTable
        private DataTable GetInspectionMethodItemTable()
        {
            DataTable returnTable = new DataTable();
            returnTable.TableName = "inspectionMethodTable";

            returnTable.Columns.Add("INSPECTIONCLASSID");
            returnTable.Columns.Add("INSPITEMID");
            returnTable.Columns.Add("INSPITEMVERSION");
            returnTable.Columns.Add("INSPECTIONMETHODID");
            returnTable.Columns.Add("INSPECTIONMETHODNAME");
            returnTable.Columns.Add("INSPITEMTYPE");
            returnTable.Columns.Add("DESCRIPTION");

            returnTable.Columns.Add("ENTERPRISEID");
            returnTable.Columns.Add("PLANTID");

            returnTable.Columns.Add("CREATOR");
            returnTable.Columns.Add("CREATEDTIME");
            returnTable.Columns.Add("MODIFIER");
            returnTable.Columns.Add("MODIFIEDTIME");
            returnTable.Columns.Add("LASTTXNHISTKEY");
            returnTable.Columns.Add("LASTTXNID");
            returnTable.Columns.Add("LASTTXNUSER");
            returnTable.Columns.Add("LASTTXNTIME");
            returnTable.Columns.Add("LASTTXNCOMMENT");
            returnTable.Columns.Add("VALIDSTATE");
            returnTable.Columns.Add("_STATE_");

            return returnTable;
        }
        #endregion

        #region SaveInspectionItemData
        private void SaveInspectionItemData()
        {
            string messageCode = "";
            DataTable allTable = (DataTable)grdInspectionItem.DataSource;
            DataTable insertTable = grdInspectionItem.GetChangedRows();

            DataTable serverTrnsTable = GetInspectionItemTable();
            DataSet serverSet = new DataSet();
            if (ValidateInspectionItem(allTable, insertTable, out messageCode))
            {
                foreach(DataRow insertRow in insertTable.Rows)
                {
                    DataRow newRow = serverTrnsTable.NewRow();

                    newRow["INSPITEMID"] = insertRow.GetString("INSPITEMID");
                    newRow["INSPITEMKEYNAME"] = insertRow.GetString("INSPITEMKEYNAME");
                    newRow["INSPITEMNAME$$KO-KR"] = insertRow.GetString("INSPITEMNAME$$KO-KR");
                    newRow["INSPITEMNAME$$EN-US"] = insertRow.GetString("INSPITEMNAME$$EN-US");
                    newRow["INSPITEMNAME$$ZH-CN"] = insertRow.GetString("INSPITEMNAME$$ZH-CN");
                    newRow["INSPITEMNAME$$VI-VN"] = insertRow.GetString("INSPITEMNAME$$VI-VN");

                    newRow["DESCRIPTION"] = insertRow.GetString("INSPITEMNAME$$KO-KR");
                    newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    newRow["EQPINSPITEMID"] = insertRow.GetString("EQPINSPITEMID");
                    newRow["UNIT"] = insertRow.GetString("UNIT");
                    newRow["VALIDSTATE"] = insertRow.GetString("VALIDSTATE");
                    newRow["CREATOR"] = UserInfo.Current.Id;
                    newRow["MODIFIER"] = UserInfo.Current.Id;

                    //공용이름이 비어있다면 추가 존재한다면 수정
                    if (insertRow.GetString("INSPITEMKEYNAME").Equals(""))
                        newRow["_STATE_"] = "added";
                    else
                        newRow["_STATE_"] = "modified";

                    serverTrnsTable.Rows.Add(newRow);
                }

                serverSet.Tables.Add(serverTrnsTable);

                this.ExecuteRule<DataSet>("SaveInspItem", serverSet);

                Research();
            }
            else
            {
                ShowMessage(messageCode);
            }
        }
        #endregion

        #region SaveInspectionMethodCodeData
        private void SaveInspectionMethodCodeData()
        {
            string messageCode = "";
            DataTable allTable = (DataTable)grdInspectionMethodCode.DataSource;
            DataTable insertTable = grdInspectionMethodCode.GetChangedRows();

            DataTable serverTrnsTable = GetInspectionMethodCodeTable();
            DataSet serverSet = new DataSet();
            if (ValidateInspectionMethodCode(allTable, insertTable, out messageCode))
            {
                foreach (DataRow insertRow in insertTable.Rows)
                {
                    DataRow newRow = serverTrnsTable.NewRow();

                    newRow["INSPECTIONMETHODID"] = insertRow.GetString("INSPECTIONMETHODID");
                    newRow["INSPECTIONMETHODNAME"] = insertRow.GetString("INSPECTIONMETHODNAME");
                    newRow["INSPECTIONMETHODNAME$$KO-KR"] = insertRow.GetString("INSPECTIONMETHODNAME$$KO-KR");
                    newRow["INSPECTIONMETHODNAME$$EN-US"] = insertRow.GetString("INSPECTIONMETHODNAME$$EN-US");
                    newRow["INSPECTIONMETHODNAME$$ZH-CN"] = insertRow.GetString("INSPECTIONMETHODNAME$$ZH-CN");
                    newRow["INSPECTIONMETHODNAME$$VI-VN"] = insertRow.GetString("INSPECTIONMETHODNAME$$VI-VN");

                    newRow["DESCRIPTION"] = insertRow.GetString("INSPECTIONMETHODNAME$$VI-KR");
                    newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;

                    newRow["VALIDSTATE"] = insertRow.GetString("VALIDSTATE");
                    newRow["CREATOR"] = UserInfo.Current.Id;
                    newRow["MODIFIER"] = UserInfo.Current.Id;

                    //공용이름이 비어있다면 추가 존재한다면 수정
                    //if (insertRow.GetString("_STATE_").Equals(""))
                    //    newRow["_STATE_"] = "added";
                    //else
                    //    newRow["_STATE_"] = "modified";
                    newRow["_STATE_"] = insertRow.GetString("_STATE_");

                    serverTrnsTable.Rows.Add(newRow);
                }

                serverSet.Tables.Add(serverTrnsTable);

                this.ExecuteRule<DataSet>("SaveInspMethodCode", serverSet);

                Research();
            }
            else
            {
                ShowMessage(messageCode);
            }
        }
        #endregion

        #region SaveInspectionMethodData
        private void SaveInspectionMethodData()
        {
            string messageCode = "";
            DataTable allTable = (DataTable)grdInspectionMethod.DataSource;
            DataTable insertTable = grdInspectionMethod.GetChangedRows();

            DataTable serverTrnsTable = GetInspectionMethodTable();
            DataSet serverSet = new DataSet();
            if (ValidateInspectionMethodCode(allTable, insertTable, out messageCode))
            {
                string inspectionClassID = "";

                //ClassID를 가져와야 한다.
                if (trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString().IndexOf("_") > -1)
                    inspectionClassID = trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString().Split('_')[0];
                else
                    inspectionClassID = trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString();

                foreach (DataRow insertRow in insertTable.Rows)
                {
                    DataRow newRow = serverTrnsTable.NewRow();

                    newRow["INSPECTIONMETHODID"] = insertRow.GetString("INSPECTIONMETHODID");
                    newRow["INSPECTIONMETHODNAME"] = insertRow.GetString("INSPECTIONMETHODNAME");
                    newRow["INSPECTIONCLASSID"] = inspectionClassID;

                    newRow["DESCRIPTION"] = insertRow.GetString("INSPECTIONMETHODNAME");
                    newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;

                    newRow["VALIDSTATE"] = insertRow.GetString("VALIDSTATE");
                    newRow["CREATOR"] = UserInfo.Current.Id;
                    newRow["MODIFIER"] = UserInfo.Current.Id;

                    //공용이름이 비어있다면 추가 존재한다면 수정
                    //if (insertRow.GetString("_STATE_").Equals(""))
                    //    newRow["_STATE_"] = "added";
                    //else
                    //    newRow["_STATE_"] = "modified";
                    newRow["_STATE_"] = insertRow.GetString("_STATE_");

                    serverTrnsTable.Rows.Add(newRow);
                }

                serverSet.Tables.Add(serverTrnsTable);

                this.ExecuteRule<DataSet>("SaveInspMethod", serverSet);
            }
            else
            {
                ShowMessage(messageCode);
            }
        }
        #endregion

        #region SaveInspectionMethodItemData
        private void SaveInspectionMethodItemData()
        {
            if (grdInspectionMethod.View.FocusedRowHandle > -1)
            {
                string messageCode = "";
                DataTable allTable = (DataTable)grdInspectionMethodItem.DataSource;
                DataTable insertTable = grdInspectionMethodItem.GetChangedRows();

                DataTable serverTrnsTable = GetInspectionMethodItemTable();
                DataSet serverSet = new DataSet();

                if (ValidateInspectionMethod(allTable, insertTable, out messageCode))
                {
                    string inspectionClassID = "";

                    //ClassID를 가져와야 한다.
                    if (trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString().IndexOf("_") > -1)
                        inspectionClassID = trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString().Split('_')[0];
                    else
                        inspectionClassID = trvInspectionMethod.FocusedNode.GetValue("ITEMID").ToString();

                    foreach (DataRow insertRow in insertTable.Rows)
                    {
                        DataRow newRow = serverTrnsTable.NewRow();

                        newRow["INSPECTIONMETHODID"] = _inspMethodID;
                        newRow["INSPECTIONMETHODNAME"] = _inspMethodName;
                        newRow["INSPECTIONCLASSID"] = inspectionClassID;
                        newRow["INSPITEMID"] = insertRow.GetString("INSPITEMID"); ;
                        newRow["INSPITEMVERSION"] = insertRow.GetString("INSPITEMVERSION");
                        newRow["INSPITEMTYPE"] = insertRow.GetString("INSPITEMTYPEID");

                        newRow["DESCRIPTION"] = _inspMethodName;
                        newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;

                        newRow["VALIDSTATE"] = insertRow.GetString("VALIDSTATE");
                        newRow["CREATOR"] = UserInfo.Current.Id;
                        newRow["MODIFIER"] = UserInfo.Current.Id;

                        //공용이름이 비어있다면 추가 존재한다면 수정
                        //if (insertRow.GetString("_STATE_").Equals(""))
                        //    newRow["_STATE_"] = "added";
                        //else
                        //    newRow["_STATE_"] = "modified";
                        newRow["_STATE_"] = insertRow.GetString("_STATE_");

                        serverTrnsTable.Rows.Add(newRow);
                    }

                    serverSet.Tables.Add(serverTrnsTable);

                    this.ExecuteRule<DataSet>("SaveInspMethodItem", serverSet);
                }
                else
                {
                    ShowMessage(messageCode);
                }
            }
        }
        #endregion
        #endregion
    }
}
