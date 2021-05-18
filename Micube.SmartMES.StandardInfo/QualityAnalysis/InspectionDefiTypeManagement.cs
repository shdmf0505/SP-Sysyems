#region using

using Micube.Framework;
using Micube.Framework.Net;
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

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질기준정보 > 검사정의별 유형관리
    /// 업  무  설  명  : 품질정보에서 사용하는 검사정의별 유형을 관리.
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-11-27
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InspectionDefiTypeManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _strInspectionclassid = "";
        private string _strInspectionclassName = "";
        #endregion

        #region 생성자

        public InspectionDefiTypeManagement()
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
            tabInspect.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            tapClassEmpty.PageVisible = true;
            tapClassPage1.PageVisible = false;
            tapClassPage2.PageVisible = false;
            tapClassPage3.PageVisible = false;
            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrid_InspectdClass1Master();
            InitializeGrid_InspectdClass1Detail();
            InitializeGrid_InspectdClass1DetailSub();
            InitializeGrid_InspectdClass2Master();
            InitializeGrid_InspectdClass2Detail();
            InitializeGrid_InspectdClass3Master();
            InitializeGrid_InspectdClass3Detail();
            InitializeGrid_InspectdClass3DetailSub();
        }
       /// <summary>
       /// 수입검사 자재분류 그리드
       /// </summary>
        private void InitializeGrid_InspectdClass1Master()
        {
            grdInspectdClass1Master.GridButtonItem = GridButtonItem.Export;
            grdInspectdClass1Master.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdInspectdClass1Master.View.AddTextBoxColumn("ENTERPRISEID", 200)
               .SetIsHidden();
            grdInspectdClass1Master.View.AddTextBoxColumn("INSPECTIONCLASSID", 200)
                .SetIsHidden();
            grdInspectdClass1Master.View.AddTextBoxColumn("PARENTINSPECTIONCLASSID", 200)
               .SetIsHidden();
            grdInspectdClass1Master.View.AddComboBoxColumn("INSPECTIONCLASSMAT",250
               , new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
             .SetValidationIsRequired()
             .SetIsReadOnly();  // 

            grdInspectdClass1Master.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
             .SetDefault("Valid")
             .SetValidationIsRequired()
             .SetTextAlignment(TextAlignment.Center);
            grdInspectdClass1Master.View.SetKeyColumn("INSPECTIONCLASSMAT");
            grdInspectdClass1Master.View.PopulateColumns();

        }
        /// <summary>
        /// 수입검사 검사방법 그리드
        /// </summary>
        private void InitializeGrid_InspectdClass1Detail()
        {

            grdInspectdClass1Detail.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdInspectdClass1Detail.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspectdClass1Detail.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdInspectdClass1Detail.View.AddTextBoxColumn("PARENTINSPECTIONCLASSID", 200)
                .SetIsHidden();
            grdInspectdClass1Detail.View.AddTextBoxColumn("INSPECTIONCLASSMAT", 150)
                .SetIsHidden();
            grdInspectdClass1Detail.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();
            grdInspectdClass1Detail.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();
            //검사방법 팝업 추가 
            InitializeClass1MethodPopup();
            grdInspectdClass1Detail.View.AddTextBoxColumn("INSPECTIONMETHODID", 150)
                .SetIsHidden();
            grdInspectdClass1Detail.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
             .SetDefault("Valid")
             .SetValidationIsRequired()
             .SetTextAlignment(TextAlignment.Center);
            grdInspectdClass1Detail.View.SetKeyColumn("INSPECTIONCLASSID", "INSPECTIONMETHODID", "INSPECTIONMETHODNAME");

            grdInspectdClass1Detail.View.AddTextBoxColumn("CREATOR", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass1Detail.View.AddTextBoxColumn("CREATEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass1Detail.View.AddTextBoxColumn("MODIFIER", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass1Detail.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass1Detail.View.PopulateColumns();
        }
        #region InitializeMethodPopup
        /// <summary>
        /// 수입검사 검상방법 팝업
        /// </summary>
        private void InitializeClass1MethodPopup()
        {
            var popupMethodGrid = grdInspectdClass1Detail.View.AddSelectPopupColumn("INSPECTIONMETHODNAME", 200, new SqlQuery("GetInspectionmethodCtmethodSfcodebyStd", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", "CODECLASSID=InspectionMethod"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("INSPECTIONMETHODNAME", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                // .SetPopupResultMapping("INSPITEMNAME", "CODENAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()
                .SetRelationIds("PARENTINSPECTIONCLASSID")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int irow = 0;
                    int crow = 0;

                    DataRow classRow = grdInspectdClass1Detail.View.GetFocusedDataRow();
                    crow = grdInspectdClass1Detail.View.FocusedRowHandle;
                    string strtemp = grdInspectdClass1Master.View.GetFocusedRowCellValue("INSPECTIONCLASSMAT").ToString() + "-";
                    foreach (DataRow row in selectedRows)
                    {
                        if (irow == 0)
                        {
                            if (checkGridProcess(row.GetString("INSPECTIONMETHODID"), grdInspectdClass1Detail, "INSPECTIONMETHODID") == -1)
                            {
                                classRow["INSPECTIONMETHODID"] = row["INSPECTIONMETHODID"];
                                classRow["INSPECTIONMETHODNAME"] = row["INSPECTIONMETHODNAME"];
                                classRow["INSPECTIONDEFID"] = strtemp + row["INSPECTIONMETHODID"].ToString();
                                
                                grdInspectdClass1Detail.View.RaiseValidateRow(crow);
                            }
                            else
                            {
                                classRow["INSPECTIONMETHODID"] = "";
                                classRow["INSPECTIONMETHODNAME"] = "";
                                classRow["INSPECTIONDEFID"] = "";
                                irow = irow - 1;
                            }
                        }
                        else
                        {
                            popAddClass1Grid(row, grdInspectdClass1Detail, "INSPECTIONMETHODID", "INSPECTIONMETHODID", "INSPECTIONMETHODNAME", strtemp);
                        }
                        irow = irow + 1;
                    }

                })

            ;
            popupMethodGrid.Conditions.AddTextBox("P_INSPECTIONCLASSMID")
               .SetPopupDefaultByGridColumnId("PARENTINSPECTIONCLASSID")
               .SetLabel("")
               .SetIsHidden();
            popupMethodGrid.Conditions.AddTextBox("INSPECTIONMETHODNAME");
            
            // 팝업 그리드 설정
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPECTIONMETHODID", 150)
                .SetValidationKeyColumn();
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetValidationKeyColumn();
        }

        /// <summary>
        /// 수입검사 검사항목 그리드
        /// </summary>
        private void InitializeGrid_InspectdClass1DetailSub()
        {
            grdInspectdClass1DetailSub.GridButtonItem = GridButtonItem.Export;
            grdInspectdClass1DetailSub.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspectdClass1DetailSub.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();
            grdInspectdClass1DetailSub.View.AddTextBoxColumn("INSPECTIONMETHODID", 150)
                .SetIsHidden();
           
            grdInspectdClass1DetailSub.View.AddTextBoxColumn("INSPITEMID", 150)
                 .SetIsHidden();
            grdInspectdClass1DetailSub.View.AddTextBoxColumn("INSPITEMNAME", 150)
                 .SetIsReadOnly()
                 ;
            //검사항목 유형 
            grdInspectdClass1DetailSub.View.AddComboBoxColumn("INSPITEMTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetTextAlignment(TextAlignment.Center)
                 .SetIsReadOnly();
            grdInspectdClass1DetailSub.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetDefault("Valid")
                 .SetValidationIsRequired()
                 .SetTextAlignment(TextAlignment.Center)
                 .SetIsReadOnly();

            grdInspectdClass1DetailSub.View.PopulateColumns();

        }
        /// <summary>
        /// 약품분석및 기타  검사방법 그리드
        /// </summary>
        private void InitializeGrid_InspectdClass2Master()
        {

            grdInspectdClass2Master.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdInspectdClass2Master.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspectdClass2Master.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdInspectdClass2Master.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                 .SetIsHidden(); 

            grdInspectdClass2Master.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();
            //검사방법 팝업 추가 
            InitializeClass2MethodPopup();
            grdInspectdClass2Master.View.AddTextBoxColumn("INSPECTIONMETHODID", 150)
                .SetIsHidden();

            grdInspectdClass2Master.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
             .SetDefault("Valid")
             .SetValidationIsRequired()
             .SetTextAlignment(TextAlignment.Center);
            grdInspectdClass2Master.View.SetKeyColumn("INSPECTIONCLASSID", "INSPECTIONMETHODID",  "INSPECTIONMETHODNAME");

            grdInspectdClass2Master.View.AddTextBoxColumn("CREATOR", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass2Master.View.AddTextBoxColumn("CREATEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass2Master.View.AddTextBoxColumn("MODIFIER", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass2Master.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);
       
            grdInspectdClass2Master.View.PopulateColumns();
        }
        #region InitializeMethodPopup
        /// <summary>
        /// 약품분석및 기타 검사방법 팝업
        /// </summary>
        private void InitializeClass2MethodPopup()
        {
            var popupMethodGrid = grdInspectdClass2Master.View.AddSelectPopupColumn("INSPECTIONMETHODNAME", 200, new SqlQuery("GetInspectionmethodSfcodebyStd", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", "CODECLASSID=InspectionMethod"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("INSPECTIONMETHODNAME", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("INSPECTIONMETHODNAME", "INSPECTIONMETHODNAME")
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

                    DataRow classRow = grdInspectdClass2Master.View.GetFocusedDataRow();
                    crow = grdInspectdClass2Master.View.FocusedRowHandle;
                    string strtemp = grdInspectdClass2Master.View.GetFocusedRowCellValue("INSPECTIONCLASSID").ToString() + "-";
                    foreach (DataRow row in selectedRows)
                    {
                        if (irow == 0)
                        {
                            if (checkGridProcess(row.GetString("INSPECTIONMETHODID"), grdInspectdClass2Master, "INSPECTIONMETHODID") == -1)
                            {
                                classRow["INSPECTIONMETHODID"] = row["INSPECTIONMETHODID"];
                                classRow["INSPECTIONMETHODNAME"] = row["INSPECTIONMETHODNAME"];
                                classRow["INSPECTIONDEFID"] = strtemp + row["INSPECTIONMETHODID"].ToString();
                                grdInspectdClass2Master.View.RaiseValidateRow(crow);
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
                            popAddClass1Grid(row, grdInspectdClass2Master, "INSPECTIONMETHODID", "INSPECTIONMETHODID", "INSPECTIONMETHODNAME", strtemp);
                        }
                        irow = irow + 1;
                    }

                })

            ;
            popupMethodGrid.Conditions.AddTextBox("INSPECTIONMETHODNAME");
            // 팝업 그리드 설정

            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPECTIONMETHODID", 150)
                .SetValidationKeyColumn();
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetValidationKeyColumn();
        }
        #endregion
        /// <summary>
        /// 약품분석및 기타 검사항목 그리드
        /// </summary>
        private void InitializeGrid_InspectdClass2Detail()
        {

            grdInspectdClass2Detail.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete| GridButtonItem.Export;
            grdInspectdClass2Detail.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspectdClass2Detail.View.AddTextBoxColumn("ENTERPRISEID", 200)
               .SetIsHidden();
            grdInspectdClass2Detail.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
               .SetIsHidden();
            grdInspectdClass2Detail.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
               .SetIsHidden();
            grdInspectdClass2Detail.View.AddTextBoxColumn("INSPECTIONMETHODID", 150)
               .SetIsHidden();
            //검사항목
            InitializeClass2MethodItemPopup();
            grdInspectdClass2Detail.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsHidden();
            //검사항목 유형 
            grdInspectdClass2Detail.View.AddComboBoxColumn("INSPITEMTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetTextAlignment(TextAlignment.Center)
                 .SetValidationIsRequired();
            grdInspectdClass2Detail.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetDefault("Valid")
                 .SetValidationIsRequired()
                 .SetTextAlignment(TextAlignment.Center);
            grdInspectdClass2Detail.View.SetKeyColumn("INSPECTIONCLASSID", "INSPECTIONMETHODID", "INSPITEMID", "INSPITEMNAME");

            grdInspectdClass2Detail.View.AddTextBoxColumn("CREATOR", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass2Detail.View.AddTextBoxColumn("CREATEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass2Detail.View.AddTextBoxColumn("MODIFIER", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass2Detail.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass2Detail.View.PopulateColumns();
        }
        /// <summary>
        /// 약품분석및 기타 검사항목 팝업
        /// </summary>
        private void InitializeClass2MethodItemPopup()
        {
            var popupMethodGrid = grdInspectdClass2Detail.View.AddSelectPopupColumn("INSPITEMNAME", 200, new SqlQuery("GetInspItemListForMethodByStandardInfo", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"))
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

                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int irow = 0;
                    int crow = 0;

                    DataRow classRow = grdInspectdClass2Detail.View.GetFocusedDataRow();
                    crow = grdInspectdClass2Detail.View.FocusedRowHandle;

                    foreach (DataRow row in selectedRows)
                    {
                        if (irow == 0)
                        {
                            if (checkGridProcess(row.GetString("INSPITEMID"), grdInspectdClass2Detail, "INSPITEMID") == -1)
                            {
                                classRow["INSPITEMID"] = row["INSPITEMID"];
                                classRow["INSPITEMNAME"] = row["INSPITEMNAME"];
                                grdInspectdClass2Detail.View.RaiseValidateRow(crow);
                            }
                            else
                            {
                                classRow["INSPITEMID"] = "";
                                classRow["INSPITEMNAME"] = "";
                                irow = irow - 1;
                            }
                        }
                        else
                        {
                            popAdditemGrid(row, grdInspectdClass2Detail, "INSPITEMID", "INSPITEMID", "INSPITEMNAME");
                        }
                        irow = irow + 1;
                    }

                })

            ;
            popupMethodGrid.Conditions.AddTextBox("INSPITEMNAME");
           
            // 팝업 그리드 설정
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPITEMID", 150)
                .SetValidationKeyColumn();
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200)
                .SetValidationKeyColumn();
        }
        /// <summary>
        /// 신뢰성 검사 검사그룹 그리드 
        /// </summary>
        private void InitializeGrid_InspectdClass3Master()
        {
            grdInspectdClass3Master.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdInspectdClass3Master.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspectdClass3Master.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdInspectdClass3Master.View.AddTextBoxColumn("PARENTINSPECTIONCLASSID", 150)
                .SetIsHidden();
            grdInspectdClass3Master.View.AddTextBoxColumn("INSPECTIONCLASSID", 100)
                .SetLabel("INSPECTIONCLASSGROUP")     
               ;
            grdInspectdClass3Master.View.AddLanguageColumn("INSPECTIONCLASSNAME", 150)
                 .SetLabel("INSPECTIONCLASSGROUPNAME");

            grdInspectdClass3Master.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
             .SetDefault("Valid")
             .SetValidationIsRequired()
             .SetTextAlignment(TextAlignment.Center);
            grdInspectdClass3Master.View.SetKeyColumn("PARENTINSPECTIONCLASSID", "INSPECTIONCLASSID");

            grdInspectdClass3Master.View.AddTextBoxColumn("CREATOR", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3Master.View.AddTextBoxColumn("CREATEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3Master.View.AddTextBoxColumn("MODIFIER", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3Master.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3Master.View.PopulateColumns();
        }
        /// <summary>
        /// 신뢰성검사 검사방법그리드
        /// </summary>
        private void InitializeGrid_InspectdClass3Detail()
        {
            grdInspectdClass3Detail.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdInspectdClass3Detail.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspectdClass3Detail.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdInspectdClass3Detail.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
               .SetIsHidden();
            grdInspectdClass3Detail.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
               .SetIsHidden();
            //검사방법 팝업 추가 
            InitializeClass3MethodPopup();
            grdInspectdClass3Detail.View.AddTextBoxColumn("INSPECTIONMETHODID", 150)
                .SetIsHidden();
            grdInspectdClass3Detail.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
             .SetDefault("Valid")
             .SetValidationIsRequired()
             .SetTextAlignment(TextAlignment.Center);
            grdInspectdClass3Detail.View.SetKeyColumn("INSPECTIONCLASSID", "INSPECTIONMETHODID", "INSPECTIONMETHODNAME");

            grdInspectdClass3Detail.View.AddTextBoxColumn("CREATOR", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3Detail.View.AddTextBoxColumn("CREATEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3Detail.View.AddTextBoxColumn("MODIFIER", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3Detail.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3Detail.View.PopulateColumns();

        }
        #region InitializeMethodPopup
        private void InitializeClass3MethodPopup()
        {
            var popupMethodGrid = grdInspectdClass3Detail.View.AddSelectPopupColumn("INSPECTIONMETHODNAME", 200, new SqlQuery("GetInspectionmethodSfcodebyStd", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", "CODECLASSID=InspectionMethod"))
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

                    DataRow classRow = grdInspectdClass3Detail.View.GetFocusedDataRow();
                    crow = grdInspectdClass3Detail.View.FocusedRowHandle;
                    string strtemp = grdInspectdClass3Master.View.GetFocusedRowCellValue("INSPECTIONCLASSID").ToString() + "-";
                    foreach (DataRow row in selectedRows)
                    {
                        if (irow == 0)
                        {
                            if (checkGridProcess(row.GetString("INSPECTIONMETHODID"), grdInspectdClass3Detail, "INSPECTIONMETHODID") == -1)
                            {
                                classRow["INSPECTIONMETHODID"] = row["INSPECTIONMETHODID"];
                                classRow["INSPECTIONMETHODNAME"] = row["INSPECTIONMETHODNAME"];
                                classRow["INSPECTIONDEFID"] = strtemp + row["INSPECTIONMETHODID"].ToString();
                                grdInspectdClass3Detail.View.RaiseValidateRow(crow);
                            }
                            else
                            {
                                classRow["INSPECTIONMETHODID"] = "";
                                classRow["INSPECTIONMETHODNAME"] = "";
                                classRow["INSPECTIONDEFID"] = "";
                                irow = irow - 1;
                            }
                        }
                        else
                        {
                            popAddClass1Grid(row, grdInspectdClass3Detail, "INSPECTIONMETHODID", "INSPECTIONMETHODID", "INSPECTIONMETHODNAME", strtemp);
                        }
                        irow = irow + 1;
                    }

                })

            ;
            popupMethodGrid.Conditions.AddTextBox("INSPECTIONMETHODNAME");
            // 팝업 그리드 설정
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPECTIONMETHODID", 150)
                .SetValidationKeyColumn();
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetValidationKeyColumn();
        }
        #endregion
        /// <summary>
        /// 신뢰성 검사 검사항목그리드
        /// </summary>
        private void InitializeGrid_InspectdClass3DetailSub()
        {
            grdInspectdClass3DetailSub.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdInspectdClass3DetailSub.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspectdClass3DetailSub.View.AddTextBoxColumn("ENTERPRISEID", 200)
               .SetIsHidden();
            grdInspectdClass3DetailSub.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
               .SetIsHidden();
            grdInspectdClass3DetailSub.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
               .SetIsHidden();
            grdInspectdClass3DetailSub.View.AddTextBoxColumn("INSPECTIONMETHODID", 150)
               .SetIsHidden();
            //검사항목
            InitializeClass3MethodItemPopup();
            grdInspectdClass3DetailSub.View.AddTextBoxColumn("INSPITEMID", 150)
               .SetIsHidden();
            grdInspectdClass3DetailSub.View.AddComboBoxColumn("NCRDECISIONDEGREE", 80
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetTextAlignment(TextAlignment.Center); 
            //검사항목 유형 
            grdInspectdClass3DetailSub.View.AddComboBoxColumn("INSPITEMTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationIsRequired();
            grdInspectdClass3DetailSub.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetDefault("Valid")
                 .SetValidationIsRequired()
                 .SetTextAlignment(TextAlignment.Center);
            grdInspectdClass3DetailSub.View.SetKeyColumn("INSPECTIONCLASSID", "INSPECTIONMETHODID", "INSPITEMID", "INSPITEMNAME");

            grdInspectdClass3DetailSub.View.AddTextBoxColumn("CREATOR", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3DetailSub.View.AddTextBoxColumn("CREATEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3DetailSub.View.AddTextBoxColumn("MODIFIER", 80)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3DetailSub.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdInspectdClass3DetailSub.View.PopulateColumns();

        }
        /// <summary>
        /// 신뢰성 검사항목 팝업
        /// </summary>
        private void InitializeClass3MethodItemPopup()
        {
            var popupMethodGrid = grdInspectdClass3DetailSub.View.AddSelectPopupColumn("INSPITEMNAME", 200, new SqlQuery("GetInspItemListForMethodByStandardInfo", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("INSPITEMID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("INSPITEMNAME", "INSPITEMNAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()

                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int irow = 0;
                    int crow = 0;

                    DataRow classRow = grdInspectdClass3DetailSub.View.GetFocusedDataRow();
                    crow = grdInspectdClass3DetailSub.View.FocusedRowHandle;

                    foreach (DataRow row in selectedRows)
                    {
                        if (irow == 0)
                        {
                            if (checkGridProcess(row.GetString("INSPITEMID"), grdInspectdClass3DetailSub, "INSPITEMID") == -1)
                            {
                                classRow["INSPITEMID"] = row["INSPITEMID"];
                                classRow["INSPITEMNAME"] = row["INSPITEMNAME"];
                                grdInspectdClass3DetailSub.View.RaiseValidateRow(crow);
                            }
                            else
                            {
                                classRow["INSPITEMID"] = "";
                                classRow["INSPITEMNAME"] = "";
                                irow = irow - 1;
                            }
                        }
                        else
                        {
                            popAdditemGrid(row, grdInspectdClass3DetailSub, "INSPITEMID", "INSPITEMID", "INSPITEMNAME");
                        }
                        irow = irow + 1;
                    }

                })

            ;
            popupMethodGrid.Conditions.AddTextBox("INSPITEMNAME");

            // 팝업 그리드 설정
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPITEMID", 150)
                .SetValidationKeyColumn();
            popupMethodGrid.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200)
                .SetValidationKeyColumn();
        }
        #endregion
       
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //// TODO : 화면에서 사용할 이벤트 추가
            grdInspectdClass1Master.View.FocusedRowChanged += grdInspectdClass1Master_FocusedRowChanged;
            grdInspectdClass1Detail.View.FocusedRowChanged += grdInspectdClass1Detail_FocusedRowChanged;
            grdInspectdClass2Master.View.FocusedRowChanged += grdInspectdClass2Master_FocusedRowChanged;
           
            grdInspectdClass3Master.View.FocusedRowChanged += grdInspectdClass3Master_FocusedRowChanged;
            grdInspectdClass3Detail.View.FocusedRowChanged += grdInspectdClass3Detail_FocusedRowChanged;

            grdInspectdClass1Detail.View.AddingNewRow += grdInspectdClass1Detail_AddingNewRow;
            grdInspectdClass2Master.View.AddingNewRow += grdInspectdClass2Master_AddingNewRow;
            grdInspectdClass2Detail.View.AddingNewRow += grdInspectdClass2Detail_AddingNewRow;
            grdInspectdClass3Master.View.AddingNewRow += grdInspectdClass3Master_AddingNewRow;
            grdInspectdClass3Detail.View.AddingNewRow += grdInspectdClass3Detail_AddingNewRow;
            grdInspectdClass3DetailSub.View.AddingNewRow += grdInspectdClass3DetailSub_AddingNewRow;
            btnSave.Click += BtnSave_Click;

        }
        private void grdInspectdClass1Master_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            InspectdClass1MasterfocusedRowChanged();
        }
        private void grdInspectdClass1Detail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            InspectdClass1DetailfocusedRowChanged();
        }
        private void grdInspectdClass2Master_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            InspectdClass2MasterfocusedRowChanged();
        }
       
        private void grdInspectdClass3Master_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            InspectdClass3MasterfocusedRowChanged();
        }
        private void grdInspectdClass3Detail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            InspectdClass3DetailfocusedRowChanged();
        }
        /// <summary>
        /// 저장 메인
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (tabInspect.SelectedTabPage.Name.Equals("tapClassPage1"))
            {
                if (CheckClassPage1Save() == false) return;
            }

            else if (tabInspect.SelectedTabPage.Name.Equals("tapClassPage2"))
            {
                if (CheckClassPage2Save() == false) return;
            }
            else if (tabInspect.SelectedTabPage.Name.Equals("tapClassPage3"))
            {
                if (CheckClassPage3Save() == false) return;
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    if (tabInspect.SelectedTabPage.Name.Equals("tapClassPage1"))
                    {
                        SaveClassPage1Save();
                       
                    }

                    else if (tabInspect.SelectedTabPage.Name.Equals("tapClassPage2"))
                    {
                        SaveClassPage2Save();
                        
                    }
                    else if (tabInspect.SelectedTabPage.Name.Equals("tapClassPage3"))
                    {
                        SaveClassPage3Save();
                       
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


        #region 그리드 행 추가 시 
        /// <summary>
        ///  추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void grdInspectdClass1Detail_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            if (grdInspectdClass1Master.View.DataRowCount == 0)
            {
                grdInspectdClass1Detail.View.DeleteRow(grdInspectdClass1Detail.View.FocusedRowHandle);

                return;
            }
            grdInspectdClass1Detail.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            //grdInspectdClass1Detail.View.SetFocusedRowCellValue("PARENTINSPECTIONCLASSID", "11111");
            grdInspectdClass1Detail.View.SetFocusedRowCellValue("INSPECTIONCLASSMAT", grdInspectdClass1Master.View.GetFocusedRowCellValue("INSPECTIONCLASSMAT"));//                                                                                                             
            grdInspectdClass1Detail.View.SetFocusedRowCellValue("INSPECTIONCLASSID", _strInspectionclassid+"."+ grdInspectdClass1Master.View.GetFocusedRowCellValue("INSPECTIONCLASSMAT"));//
            
            grdInspectdClass1Detail.View.SetFocusedRowCellValue("PARENTINSPECTIONCLASSID", _strInspectionclassid);// 회사코드
            grdInspectdClass1Detail.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");// 
        }
        private void grdInspectdClass2Master_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            grdInspectdClass2Master.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdInspectdClass2Master.View.SetFocusedRowCellValue("INSPECTIONCLASSID", _strInspectionclassid);
            grdInspectdClass2Master.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");// 

        }
        private void grdInspectdClass2Detail_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdInspectdClass2Master.View.DataRowCount == 0)
            {
                grdInspectdClass2Detail.View.DeleteRow(grdInspectdClass2Detail.View.FocusedRowHandle);
               
                return;
            }
            string Inspectiondefid = grdInspectdClass2Master.View.GetFocusedRowCellValue("INSPECTIONDEFID").ToString();
            if (Inspectiondefid.Equals(""))
            {
                grdInspectdClass2Detail.View.DeleteRow(grdInspectdClass2Detail.View.FocusedRowHandle);

                return;
            }
            grdInspectdClass2Detail.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdInspectdClass2Detail.View.SetFocusedRowCellValue("INSPECTIONCLASSID", _strInspectionclassid);// 회사코드
            grdInspectdClass2Detail.View.SetFocusedRowCellValue("INSPECTIONDEFID", grdInspectdClass2Master.View.GetFocusedRowCellValue("INSPECTIONDEFID").ToString());// 회사코드
            grdInspectdClass2Detail.View.SetFocusedRowCellValue("INSPECTIONMETHODID", grdInspectdClass2Master.View.GetFocusedRowCellValue("INSPECTIONMETHODID").ToString());// 회사코드
            grdInspectdClass2Detail.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");// 
            //검사방법코드 추가 
        }
        private void grdInspectdClass3Master_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            grdInspectdClass3Master.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdInspectdClass3Master.View.SetFocusedRowCellValue("PARENTINSPECTIONCLASSID", _strInspectionclassid);
            grdInspectdClass3Master.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");// 
            //상위코드 추가
        }
        private void grdInspectdClass3Detail_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdInspectdClass3Master.View.DataRowCount == 0)
            {
                grdInspectdClass3Detail.View.DeleteRow(grdInspectdClass3Detail.View.FocusedRowHandle);

                return;
            }
            string Inspectionclassid = grdInspectdClass3Master.View.GetFocusedRowCellValue("INSPECTIONCLASSID").ToString();
            if (Inspectionclassid.Equals(""))
            {
                grdInspectdClass3Detail.View.DeleteRow(grdInspectdClass3Detail.View.FocusedRowHandle);

                return;
            }
            grdInspectdClass3Detail.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdInspectdClass3Detail.View.SetFocusedRowCellValue("INSPECTIONCLASSID", grdInspectdClass3Master.View.GetFocusedRowCellValue("INSPECTIONCLASSID"));//
            grdInspectdClass3Detail.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");// 
            //검사그룹코드 추가
        }
        private void grdInspectdClass3DetailSub_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdInspectdClass3Detail.View.DataRowCount == 0)
            {
                grdInspectdClass3DetailSub.View.DeleteRow(grdInspectdClass3DetailSub.View.FocusedRowHandle);

                return;
            }
            string Inspectiondefid = grdInspectdClass3Detail.View.GetFocusedRowCellValue("INSPECTIONDEFID").ToString();
            if (Inspectiondefid.Equals(""))
            {
                grdInspectdClass3DetailSub.View.DeleteRow(grdInspectdClass3DetailSub.View.FocusedRowHandle);

                return;
            }
            grdInspectdClass3DetailSub.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdInspectdClass3DetailSub.View.SetFocusedRowCellValue("INSPECTIONCLASSID", _strInspectionclassid);// 회사코드
            grdInspectdClass3DetailSub.View.SetFocusedRowCellValue("INSPECTIONDEFID", grdInspectdClass3Detail.View.GetFocusedRowCellValue("INSPECTIONDEFID").ToString());// 회사코드
            grdInspectdClass3DetailSub.View.SetFocusedRowCellValue("INSPECTIONMETHODID", grdInspectdClass3Detail.View.GetFocusedRowCellValue("INSPECTIONMETHODID").ToString());// 회사코드
            grdInspectdClass3DetailSub.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");// 
            grdInspectdClass3DetailSub.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdInspectdClass3DetailSub.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");// 
            //검사방법코드 추가 
        }
        #endregion

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
           // base.OnToolbarSaveClick();

            //// TODO : 저장 Rule 변경
            //DataTable changed = grdList.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                BtnSave_Click(null, null);
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
            _strInspectionclassid = values["P_INSPECTIONCLASSID"].ToString();
            //
            TabPageVisible();
            if (_strInspectionclassid.Equals(""))
            {
                return;
            }
            else if (_strInspectionclassid.Equals("OperationInspection") || _strInspectionclassid.Equals("WaterInspection") || _strInspectionclassid.Equals("ChemicalInspection"))
            {
                SearchInspectdClass2Master();
            }
            //수입검사
            else if (_strInspectionclassid.Equals("RawInspection") || _strInspectionclassid.Equals("SubassemblyInspection"))
            {
                SearchInspectdClass1Tree();
                SearchInspectdClass1Master();
            }
            //신뢰성
            else if (_strInspectionclassid.Equals("ReliabilityInspection"))
            {
                SearchInspectdClass3Tree();
                SearchInspectdClass3Master();
            }

        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeConditionPopup_Inspectionclassid();
            InitializeConditionPopup_ValidState();
            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }
        /// <summary>
        /// 진행상태
        /// </summary>
        private void InitializeConditionPopup_Inspectionclassid()
        {

            var Ospetcprogressstatuscbobox = Conditions.AddComboBox("p_Inspectionclassid", new SqlQuery("GetInspectionclassidListByStd", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
               .SetLabel("INSPECTIONCLASSNAME")
               // .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(0.5)
               .SetEmptyItem("", "")
               .SetValidationIsRequired()
            ;
            //   .SetIsReadOnly(true);

        }
        /// <summary>
        /// 유효상태값 확인
        /// </summary>
        private void InitializeConditionPopup_ValidState()
        {
            var plantCbobox = Conditions.AddComboBox("p_validstate", new SqlQuery("GetCodeListNotCodebyStd", "10001", new Dictionary<string, object>() { { "CODECLASSID", "ValidState" },{ "NOTCODEID", "Invalid" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
              .SetLabel("VALIDSTATE")
              .SetPosition(0.8)
              .SetEmptyItem("", "")
              .SetDefault("Valid") //기본값 설
               ;
        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

        }


        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            //// TODO : 유효성 로직 변경
            //grdList.View.CheckValidation();

            //DataTable changed = grdList.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }
        /// <summary>
        ///  검색후 화면 통제 tab 페이지 통제 처리 
        /// </summary>
        private void TabPageVisible()
        {
            var values = Conditions.GetValues();
            _strInspectionclassid = values["P_INSPECTIONCLASSID"].ToString();
            SmartComboBox cboInspectionTemp = Conditions.GetControl<SmartComboBox>("p_Inspectionclassid");
            _strInspectionclassName = cboInspectionTemp.GetDisplayText().ToString();
            
            tapClassEmpty.PageVisible = false;
            tapClassPage1.PageVisible = false;
            tapClassPage2.PageVisible = false;
            tapClassPage3.PageVisible = false;
            if (_strInspectionclassid.Equals(""))
            {
                tapClassEmpty.PageVisible = true;
                tabInspect.SelectedTabPage = tapClassEmpty;
            }
            else if (_strInspectionclassid.Equals("OperationInspection") || _strInspectionclassid.Equals("WaterInspection") || _strInspectionclassid.Equals("ChemicalInspection"))
            {
                tapClassPage2.PageVisible = true;
                txtClassPage2.Text = _strInspectionclassName;
                tabInspect.SelectedTabPage = tapClassPage2;

            }
            
            else if (_strInspectionclassid.Equals("RawInspection")|| _strInspectionclassid.Equals("SubassemblyInspection"))
            {
                tapClassPage1.PageVisible = true;
                txtClassPage1.Text = _strInspectionclassName;
                tabInspect.SelectedTabPage = tapClassPage1;
            }
           
            else if (_strInspectionclassid.Equals("ReliabilityInspection"))
            {
                tapClassPage3.PageVisible = true;
                txtClassPage3.Text = _strInspectionclassName;
                tabInspect.SelectedTabPage = tapClassPage3;
            }
        }

        /// <summary>
        /// 검사종류별 자재분류값 가져오기
        /// </summary>
        private void OnInspectionclassidMatCodeSearch(string strinspectionclassidmat)
        {

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("CODECLASSID", strinspectionclassidmat);
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdInspectdClass1Master.View.RefreshComboBoxDataSource("INSPECTIONCLASSMAT", new SqlQuery("GetCodeList", "00001", dicParam));

        }
        /// <summary>
        /// 검사방법 팝업후 그리드에 추가 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="targetGrid"></param>
        /// <param name="columnName"></param>
        /// <param name="insertIDColumnName"></param>
        /// <param name="insertNameColumnName"></param>
        /// <param name="strtemp"></param>
        private void popAddClass1Grid(DataRow dr, SmartBandedGrid targetGrid, string columnName, string insertIDColumnName, string insertNameColumnName,string strtemp)
        {
            DateTime dateNow = DateTime.Now;
            string itemID = dr.GetString(insertIDColumnName);
            if (checkGridProcess(itemID, targetGrid, columnName) == -1)
            {
                targetGrid.View.AddNewRow();
                int irow = targetGrid.View.RowCount - 1;
                DataRow classRow = targetGrid.View.GetDataRow(irow);
                classRow["INSPECTIONMETHODID"] = dr[insertIDColumnName];
                classRow["INSPECTIONMETHODNAME"] = dr[insertNameColumnName];
                classRow["INSPECTIONDEFID"] = strtemp +dr[insertIDColumnName].ToString();
                targetGrid.View.RaiseValidateRow(irow);
            }

        }
        /// <summary>
        /// 팝업후 그리드에 추가시
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="targetGrid"></param>
        /// <param name="columnName"></param>
        /// <param name="insertIDColumnName"></param>
        /// <param name="insertNameColumnName"></param>
        private void popAdditemGrid(DataRow dr, SmartBandedGrid targetGrid, string columnName, string insertIDColumnName, string insertNameColumnName)
        {
            DateTime dateNow = DateTime.Now;
            string itemID = dr.GetString(insertIDColumnName);
            if (checkGridProcess(itemID, targetGrid, columnName) == -1)
            {
                targetGrid.View.AddNewRow();
                int irow = targetGrid.View.RowCount - 1;
                DataRow classRow = targetGrid.View.GetDataRow(irow);
                classRow["INSPITEMID"] = dr[insertIDColumnName];
                classRow["INSPITEMNAME"] = dr[insertNameColumnName];
               
                targetGrid.View.RaiseValidateRow(irow);
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
        /// <summary>
        /// 수입검사 자재분류변경시
        /// </summary>
        private void InspectdClass1MasterfocusedRowChanged()
        {
            //포커스 행 체크 
            if (grdInspectdClass1Master.View.FocusedRowHandle < 0) return;
            var values = Conditions.GetValues();
            
            Dictionary<string, object> Param = new Dictionary<string, object>();
            string strInspectionclassmat = grdInspectdClass1Master.View.GetFocusedRowCellValue("INSPECTIONCLASSMAT").ToString();
            if (_strInspectionclassid.Equals("RawInspection"))
            {

                Param.Add("P_INSPECTIONCLASSID", "RawInspection." + strInspectionclassmat);

            }
            else
            {
                Param.Add("P_INSPECTIONCLASSID", "SubassemblyInspection." + strInspectionclassmat);

            }
            //Param.Add("p_INSPECTIONCLASSID", grdInspectdClass1Master.View.GetFocusedRowCellValue("INSPECTIONCLASSMAT"));
            Param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            Param.Add("P_VALIDSTATE", values["P_VALIDSTATE"].ToString());
            DataTable dt = SqlExecuter.Query("GetInspectionDefiTypeSfcodeDetail", "10001", Param);

            if (dt.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtTemp = (grdInspectdClass1Detail.DataSource as DataTable).Clone();
                grdInspectdClass1Detail.DataSource = dtTemp;
            }
            grdInspectdClass1Detail.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                grdInspectdClass1Detail.View.FocusedRowHandle = 0;
                grdInspectdClass1Detail.View.SelectRow(0);
                InspectdClass1DetailfocusedRowChanged();
            }
            else
            {
                grdInspectdClass1DetailSub.View.ClearDatas(); 
            }
        }
        /// <summary>
        /// 수입검사 검사방법 변경시 
        /// </summary>
        private void InspectdClass1DetailfocusedRowChanged()
        {
            if (grdInspectdClass1Detail.View.FocusedRowHandle < 0) return;
            var values = Conditions.GetValues();

            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_INSPECTIONCLASSID", _strInspectionclassid);
            Param.Add("P_INSPECTIONMETHODID", grdInspectdClass1Detail.View.GetFocusedRowCellValue("INSPECTIONMETHODID"));
            Param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            Param.Add("P_VALIDSTATE", values["P_VALIDSTATE"].ToString());
            DataTable dt = SqlExecuter.Query("GetInspectionDefiTypeSfClassDetailSub", "10001", Param);

            if (dt.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtTemp = (grdInspectdClass1DetailSub.DataSource as DataTable).Clone();
                grdInspectdClass1DetailSub.DataSource = dtTemp;
            }
            grdInspectdClass1DetailSub.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                grdInspectdClass1DetailSub.View.FocusedRowHandle = 0;
                grdInspectdClass1DetailSub.View.SelectRow(0);

            }
            
        }
        /// <summary>
        /// 약품분석검사방법 변경시 
        /// </summary>
        private void InspectdClass2MasterfocusedRowChanged()
        {
            if (grdInspectdClass2Master.View.FocusedRowHandle < 0) return;
            var values = Conditions.GetValues();

            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_INSPECTIONCLASSID", _strInspectionclassid);
            Param.Add("P_INSPECTIONMETHODID", grdInspectdClass2Master.View.GetFocusedRowCellValue("INSPECTIONMETHODID"));
            Param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            Param.Add("P_VALIDSTATE", values["P_VALIDSTATE"].ToString());
            DataTable dt = SqlExecuter.Query("GetInspectionDefiTypeSfClassDetailSub", "10001", Param);

            if (dt.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtTemp = (grdInspectdClass2Detail.DataSource as DataTable).Clone();
                grdInspectdClass2Detail.DataSource = dtTemp;
            }
            grdInspectdClass2Detail.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                grdInspectdClass2Detail.View.FocusedRowHandle = 0;
                grdInspectdClass2Detail.View.SelectRow(0);
               
            }
            
        }
       /// <summary>
       /// 신뢰성 검사 검사그룹 변경시 
       /// </summary>
        private void InspectdClass3MasterfocusedRowChanged()
        {
            if (grdInspectdClass3Master.View.FocusedRowHandle < 0) return;
            var values = Conditions.GetValues();

            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("p_INSPECTIONCLASSID", grdInspectdClass3Master.View.GetFocusedRowCellValue("INSPECTIONCLASSID"));
            Param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            Param.Add("P_VALIDSTATE", values["P_VALIDSTATE"].ToString());
            DataTable dt = SqlExecuter.Query("GetInspectionDefiTypeSfcodeDetail", "10001", Param);

            if (dt.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtTemp = (grdInspectdClass3Detail.DataSource as DataTable).Clone();
                grdInspectdClass3Detail.DataSource = dtTemp;
            }
            grdInspectdClass3Detail.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                grdInspectdClass3Detail.View.FocusedRowHandle = 0;
                grdInspectdClass3Detail.View.SelectRow(0);
                InspectdClass3DetailfocusedRowChanged();
            }
            else
            {
                grdInspectdClass3DetailSub.View.ClearDatas();
            }
            
        }
        /// <summary>
        /// 신뢰성 검사 검사방법 변경시 
        /// </summary>
        private void InspectdClass3DetailfocusedRowChanged()
        {
            if (grdInspectdClass3Detail.View.FocusedRowHandle < 0) return;

            
            var values = Conditions.GetValues();

            Dictionary<string, object> Param = new Dictionary<string, object>();
            
            Param.Add("P_INSPECTIONCLASSID", _strInspectionclassid);
            Param.Add("P_INSPECTIONDEFID", grdInspectdClass3Detail.View.GetFocusedRowCellValue("INSPECTIONDEFID"));
            Param.Add("P_INSPECTIONMETHODID", grdInspectdClass3Detail.View.GetFocusedRowCellValue("INSPECTIONMETHODID"));
            Param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            Param.Add("P_VALIDSTATE", values["P_VALIDSTATE"].ToString());
            DataTable dt = SqlExecuter.Query("GetInspectionDefiTypeSfClassDetailReliabilitySub", "10001", Param);

            if (dt.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtTemp = (grdInspectdClass3DetailSub.DataSource as DataTable).Clone();
                grdInspectdClass3DetailSub.DataSource = dtTemp;
            }
            grdInspectdClass3DetailSub.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                grdInspectdClass3DetailSub.View.FocusedRowHandle = 0;
                grdInspectdClass3DetailSub.View.SelectRow(0);
            }
        }
        /// <summary>
        /// InspectdClass1Master 조회하는 함수
        /// </summary>
        private void SearchInspectdClass1Master()
        {
            var values = Conditions.GetValues();
            Dictionary<string, object> param = new Dictionary<string, object>();
            if (_strInspectionclassid.Equals("RawInspection"))
            {
                param.Add("P_CODECLASSID", "MaterialClass");
                param.Add("P_PARENTINSPECTIONCLASSID", "RawInspection");
                OnInspectionclassidMatCodeSearch("MaterialClass");
            }
            else
            {
                param.Add("P_CODECLASSID", "ConsumableType");
                param.Add("P_PARENTINSPECTIONCLASSID", "SubassemblyInspection");
                OnInspectionclassidMatCodeSearch("ConsumableType");

            }
            param.Add("P_VALIDSTATE", values["P_VALIDSTATE"].ToString());

            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dt = SqlExecuter.Query("GetInspectionDefiTypeSfcodeMaster", "10001", param);
         
            grdInspectdClass1Master.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                grdInspectdClass1Master.View.ClearSelection();
                grdInspectdClass1Master.View.FocusedRowHandle = 0;

                grdInspectdClass1Master.View.SelectRow(0);
                grdInspectdClass1Master.View.FocusedColumn = grdInspectdClass1Master.View.Columns["INSPECTIONCLASSMAT"];
                InspectdClass1MasterfocusedRowChanged();
            }
            else
            {
                
                grdInspectdClass1Detail.View.ClearDatas();
                grdInspectdClass1DetailSub.View.ClearDatas();
                
            }
        }
        /// <summary>
        /// InspectdClass2Master 조회하는 함수
        /// </summary>
        private void SearchInspectdClass2Master()
        {

            var values = Conditions.GetValues();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_INSPECTIONCLASSID", _strInspectionclassid);
            param.Add("P_VALIDSTATE", values["P_VALIDSTATE"].ToString());

            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dt = SqlExecuter.Query("GetInspectionDefiTypeSfcodeDetail", "10001", param);
            grdInspectdClass2Master.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                grdInspectdClass2Master.View.ClearSelection();
                grdInspectdClass2Master.View.FocusedRowHandle = 0;
                grdInspectdClass2Master.View.SelectRow(0);
                grdInspectdClass2Master.View.FocusedColumn = grdInspectdClass2Master.View.Columns["INSPECTIONMETHODNAME"];
                InspectdClass2MasterfocusedRowChanged();
            }
            else
            {
                
                grdInspectdClass2Detail.View.ClearDatas();
            }
        }
        /// <summary>
        /// InspectdClass3Master 조회하는 함수
        /// </summary>
        private void SearchInspectdClass3Master()
        {

            var values = Conditions.GetValues();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_INSPECTIONCLASSID", _strInspectionclassid);
            param.Add("P_VALIDSTATE", values["P_VALIDSTATE"].ToString());

            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetInspectionDefiTypeSfClassMaster", "10001", values);
            grdInspectdClass3Master.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                grdInspectdClass3Master.View.ClearSelection();
                grdInspectdClass3Master.View.FocusedRowHandle = 0;
                grdInspectdClass3Master.View.SelectRow(0);
                grdInspectdClass3Master.View.FocusedColumn = grdInspectdClass3Master.View.Columns["INSPECTIONCLASSNAME"];
                InspectdClass3MasterfocusedRowChanged();
            }
            else
            {
                grdInspectdClass3Detail.View.ClearDatas();
                grdInspectdClass3DetailSub.View.ClearDatas();
               
            }
        }
        /// <summary>
        /// InspectdClass1tree 조회하는 함수
        /// </summary>
        private void SearchInspectdClass1Tree()
        {
            var values = Conditions.GetValues();
            treeInspItemClass1.SetResultCount(1);

            treeInspItemClass1.SetIsReadOnly();
            treeInspItemClass1.SetEmptyRoot(UserInfo.Current.Enterprise, "PARENT");
            treeInspItemClass1.SetMember("INSPECTIONCLASSNAME", "INSPECTIONCLASSID", "PARENTID");
            Dictionary<string, object> param = new Dictionary<string, object>();
            if (_strInspectionclassid.Equals("RawInspection"))
            {
                param.Add("P_CODECLASSID", "MaterialClass");
            }
            else
            {
                param.Add("P_CODECLASSID", "ConsumableType");

            }
            param.Add("P_VALIDSTATE", values["P_VALIDSTATE"].ToString());

            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dt = SqlExecuter.Query("GetInspectionDefiTypeSfcodeTree", "10001", param);
            if (dt != null)
            {

                if (dt.Rows.Count != 0)
                {
                    treeInspItemClass1.DataSource = dt;
                    treeInspItemClass1.PopulateColumns();
                    treeInspItemClass1.ExpandAll();
                }
            }
        }

        /// <summary>
        /// InspectdClass3tree 조회하는 함수
        /// </summary>
        private void SearchInspectdClass3Tree()
        {
            var values = Conditions.GetValues();
            treeInspItemClass3.SetResultCount(1);

            treeInspItemClass3.SetIsReadOnly();
            treeInspItemClass3.SetEmptyRoot(UserInfo.Current.Enterprise, "PARENT");
            treeInspItemClass3.SetMember("INSPECTIONCLASSNAME", "INSPECTIONCLASSID", "PARENTID");
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("P_INSPECTIONCLASSID", _strInspectionclassid);
            param.Add("P_VALIDSTATE", values["P_VALIDSTATE"].ToString());
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dt = SqlExecuter.Query("GetInspectionDefiTypeSfClassTree", "10001", param);
            if (dt != null)
            {

                if (dt.Rows.Count != 0)
                {
                    treeInspItemClass3.DataSource = dt;
                    treeInspItemClass3.PopulateColumns();
                    treeInspItemClass3.ExpandAll();
                }
            }
        }
        /// <summary>
        /// 수입검사 저장시 체크 로직 
        /// </summary>
        /// <returns></returns>
        private bool CheckClassPage1Save()
        {
            bool blcheck = true;
            DataTable changedMaster = grdInspectdClass1Master.GetChangedRows();

            DataTable changedDetail = grdInspectdClass1Detail.GetChangedRows();
            if (changedMaster.Rows.Count == 0 && changedDetail.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지 
                return false ;
            }
            for (int i = 0; i < grdInspectdClass1Master.View.DataRowCount; i++)
            {
                DataRow row = grdInspectdClass1Master.View.GetDataRow(i);
                string strInspectionclassmat = grdInspectdClass1Master.View.GetRowCellValue(i, "INSPECTIONCLASSMAT").ToString();
                if (strInspectionclassmat.Equals(""))
                {
                    string lblInspectionclassmat = grdInspectdClass1Master.View.Columns["INSPECTIONCLASSMAT"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblInspectionclassmat); //메세지
                    return false;
                }
                string strValidState = grdInspectdClass1Master.View.GetRowCellValue(i, "VALIDSTATE").ToString();
                if (strValidState.Equals(""))
                {
                    string lblValidState = grdInspectdClass1Master.View.Columns["VALIDSTATE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblValidState); //메세지
                    return false;
                }
   
            }
            for (int i = 0; i < grdInspectdClass1Detail.View.DataRowCount; i++)
            {
                DataRow row = grdInspectdClass1Detail.View.GetDataRow(i);
                string strInspectionmethodid = grdInspectdClass1Detail.View.GetRowCellValue(i, "INSPECTIONMETHODID").ToString();
                if (strInspectionmethodid.Equals(""))
                {
                    string lblInspectionmethodid = grdInspectdClass1Master.View.Columns["INSPECTIONMETHODNAME"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblInspectionmethodid); //메세지
                    return false;
                }
                string strValidState = grdInspectdClass1Detail.View.GetRowCellValue(i, "VALIDSTATE").ToString();
                if (strValidState.Equals(""))
                {
                    string lblValidState = grdInspectdClass1Detail.View.Columns["VALIDSTATE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblValidState); //메세지
                    return false;
                }

            }
            return blcheck;
        }

        /// <summary>
        /// 약품분석및 기타 저장시 체크 로직 
        /// </summary>
        /// <returns></returns>
        private bool CheckClassPage2Save()
        {
            bool blcheck = true;
            DataTable changedMaster = grdInspectdClass2Master.GetChangedRows();

            DataTable changedDetail = grdInspectdClass2Detail.GetChangedRows();
          
            if (changedMaster.Rows.Count == 0 && changedDetail.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지 
                return false;
            }
            for (int i = 0; i < grdInspectdClass2Master.View.DataRowCount; i++)
            {
                DataRow row = grdInspectdClass2Master.View.GetDataRow(i);
                string strInspectionmethodid = grdInspectdClass2Master.View.GetRowCellValue(i, "INSPECTIONMETHODID").ToString();
                if (strInspectionmethodid.Equals(""))
                {
                    string lblInspectionmethodid = grdInspectdClass2Master.View.Columns["INSPECTIONMETHODNAME"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblInspectionmethodid); //메세지
                    return false;
                }
                string strValidState = grdInspectdClass2Master.View.GetRowCellValue(i, "VALIDSTATE").ToString();
                if (strValidState.Equals(""))
                {
                    string lblValidState = grdInspectdClass2Master.View.Columns["VALIDSTATE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblValidState); //메세지
                    return false;
                }
            }
            for (int i = 0; i < grdInspectdClass2Detail.View.DataRowCount; i++)
            {

                DataRow row = grdInspectdClass2Detail.View.GetDataRow(i);
                string strInspectionmethodid = grdInspectdClass2Detail.View.GetRowCellValue(i, "INSPECTIONMETHODID").ToString();
                if (strInspectionmethodid.Equals(""))
                {
                    string lblInspectionmethodid = grdInspectdClass2Detail.View.Columns["INSPECTIONMETHODNAME"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblInspectionmethodid); //메세지
                    return false;
                }
                string strInspitemid = grdInspectdClass2Detail.View.GetRowCellValue(i, "INSPITEMID").ToString();
                if (strInspitemid.Equals(""))
                {
                    string lblInspitemid = grdInspectdClass2Detail.View.Columns["INSPITEMNAME"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblInspitemid); //메세지
                    return false;
                }

                string strInspItemType = grdInspectdClass2Detail.View.GetRowCellValue(i, "INSPITEMTYPE").ToString();
                if (strInspItemType.Equals(""))
                {
                    string lblInspItemType = grdInspectdClass2Detail.View.Columns["INSPITEMTYPE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblInspItemType); //메세지
                    return false;
                }
                string strValidState = grdInspectdClass2Detail.View.GetRowCellValue(i, "VALIDSTATE").ToString();
                if (strValidState.Equals(""))
                {
                    string lblValidState = grdInspectdClass2Detail.View.Columns["VALIDSTATE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblValidState); //메세지
                    return false;
                }
            }
            return blcheck;
        }
        /// <summary>
        /// 신뢰성 검사 저장시 체크 로직
        /// </summary>
        /// <returns></returns>
        private bool CheckClassPage3Save()
        {
            bool blcheck = true;
            DataTable changedMaster = grdInspectdClass3Master.GetChangedRows();

            DataTable changedDetail = grdInspectdClass3Detail.GetChangedRows();
            DataTable changedDetailSub = grdInspectdClass3DetailSub.GetChangedRows();
            if (changedMaster.Rows.Count == 0 && changedDetail.Rows.Count == 0 && changedDetailSub.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지 
                return false;
            }
            for (int i = 0; i < grdInspectdClass3Master.View.DataRowCount; i++)
            {
                DataRow row = grdInspectdClass3Master.View.GetDataRow(i);
                //
                string strInspectionclassid = grdInspectdClass3Master.View.GetRowCellValue(i, "INSPECTIONCLASSID").ToString();
                if (strInspectionclassid.Equals(""))
                {
                    string lblInspectionclassid = grdInspectdClass3Master.View.Columns["INSPECTIONCLASSID"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblInspectionclassid); //메세지
                    return false;
                }
                string strValidState = grdInspectdClass3Master.View.GetRowCellValue(i, "VALIDSTATE").ToString();
                if (strValidState.Equals(""))
                {
                    string lblValidState = grdInspectdClass3Master.View.Columns["VALIDSTATE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblValidState); //메세지
                    return false;
                }

            }
            for (int i = 0; i < grdInspectdClass3Detail.View.DataRowCount; i++)
            {
                DataRow row = grdInspectdClass3Detail.View.GetDataRow(i);
                string strInspectionmethodid = grdInspectdClass3Detail.View.GetRowCellValue(i, "INSPECTIONMETHODID").ToString();
                if (strInspectionmethodid.Equals(""))
                {
                    string lblInspectionmethodid = grdInspectdClass3Detail.View.Columns["INSPECTIONMETHODNAME"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblInspectionmethodid); //메세지
                    return false;
                }
                string strValidState = grdInspectdClass3Detail.View.GetRowCellValue(i, "VALIDSTATE").ToString();
                if (strValidState.Equals(""))
                {
                    string lblValidState = grdInspectdClass3Detail.View.Columns["VALIDSTATE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblValidState); //메세지
                    return false;
                }
            }
            for (int i = 0; i < grdInspectdClass3DetailSub.View.DataRowCount; i++)
            {

                DataRow row = grdInspectdClass3DetailSub.View.GetDataRow(i);
                string strInspectionmethodid = grdInspectdClass3DetailSub.View.GetRowCellValue(i, "INSPECTIONMETHODID").ToString();
                if (strInspectionmethodid.Equals(""))
                {
                    string lblInspectionmethodid = grdInspectdClass3DetailSub.View.Columns["INSPECTIONMETHODNAME"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblInspectionmethodid); //메세지
                    return false;
                }
                string strInspitemid = grdInspectdClass3DetailSub.View.GetRowCellValue(i, "INSPITEMID").ToString();
                if (strInspitemid.Equals(""))
                {
                    string lblInspitemid = grdInspectdClass3DetailSub.View.Columns["INSPITEMNAME"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblInspitemid); //메세지
                    return false;
                }

                string strInspItemType = grdInspectdClass3DetailSub.View.GetRowCellValue(i, "INSPITEMTYPE").ToString();
                if (strInspItemType.Equals(""))
                {
                    string lblInspItemType = grdInspectdClass3DetailSub.View.Columns["INSPITEMTYPE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblInspItemType); //메세지
                    return false;
                }
                string strValidState = grdInspectdClass3DetailSub.View.GetRowCellValue(i, "VALIDSTATE").ToString();
                if (strValidState.Equals(""))
                {
                    string lblValidState = grdInspectdClass3DetailSub.View.Columns["VALIDSTATE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblValidState); //메세지
                    return false;
                }
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
            string Inspectiondefid = "";
            DataTable changedMaster = grdInspectdClass1Master.GetChangedRows();
            changedMaster.TableName = "listMaster";
            DataTable changedDetail = grdInspectdClass1Detail.GetChangedRows();
            changedDetail.TableName = "listDetail";
            
             DataSet dtSave = new DataSet();
            dtSave.Tables.Add(changedMaster);
            dtSave.Tables.Add(changedDetail);

            ExecuteRule("SaveInspectionDefiTypeImport", dtSave);
            if (changedMaster.Rows.Count != 0)
            {
                for(int i = 0; i < changedMaster.Rows.Count; i++)
                {
                    DataRow row = changedMaster.Rows[i];
                    Inspectionclassmat = row["INSPECTIONCLASSMAT"].ToString();
                }
                //
                SearchInspectdClass1Tree();
                SearchInspectdClass1Master();
                int irow = checkGridProcess(Inspectionclassmat, grdInspectdClass1Master, "INSPECTIONCLASSMAT");
                if (irow >-1)
                {
                    grdInspectdClass1Master.View.ClearSelection();
                    grdInspectdClass1Master.View.FocusedRowHandle = irow;

                    grdInspectdClass1Master.View.SelectRow(irow);
                    grdInspectdClass1Master.View.FocusedColumn = grdInspectdClass1Master.View.Columns["INSPECTIONCLASSMAT"];
                    InspectdClass1MasterfocusedRowChanged();
                }
            }
            if (changedDetail.Rows.Count != 0)
            {
                for (int i = 0; i < changedDetail.Rows.Count; i++)
                {
                    DataRow row = changedDetail.Rows[i];
                    Inspectiondefid = row["INSPECTIONDEFID"].ToString();
                }
                if (Inspectionclassmat.Equals(""))
                {

                    InspectdClass1MasterfocusedRowChanged();
                }
                int irow = checkGridProcess(Inspectiondefid, grdInspectdClass1Detail, "INSPECTIONDEFID");
                if (irow > -1)
                {
                    grdInspectdClass1Detail.View.ClearSelection();
                    grdInspectdClass1Detail.View.FocusedRowHandle = irow;

                    grdInspectdClass1Detail.View.SelectRow(irow);
                    grdInspectdClass1Detail.View.FocusedColumn = grdInspectdClass1Detail.View.Columns["INSPECTIONMETHODNAME"];
                    
                }
            }
        }
        /// <summary>
        /// 약품분석및 기타 저장및 저정후 조회   
        /// </summary>
        /// <returns></returns>
        private void SaveClassPage2Save()
        {
            string Inspectiondefid = "";
            string Inspitemid = "";
            DataTable changedMaster = grdInspectdClass2Master.GetChangedRows();
            changedMaster.TableName = "listMaster";
            DataTable changedDetail = grdInspectdClass2Detail.GetChangedRows();
            changedDetail.TableName = "listDetail";
            DataSet dtSave = new DataSet();
            dtSave.Tables.Add(changedMaster);
            dtSave.Tables.Add(changedDetail);

            ExecuteRule("SaveInspectionDefiTypeChemical", dtSave);

            if (changedMaster.Rows.Count != 0)
            {
                for (int i = 0; i < changedMaster.Rows.Count; i++)
                {
                    DataRow row = changedMaster.Rows[i];
                    Inspectiondefid = row["INSPECTIONDEFID"].ToString();
                }
                //
                
                SearchInspectdClass2Master();
                int irow = checkGridProcess(Inspectiondefid, grdInspectdClass2Master, "INSPECTIONDEFID");
                if (irow > -1)
                {
                    grdInspectdClass2Master.View.ClearSelection();
                    grdInspectdClass2Master.View.FocusedRowHandle = irow;

                    grdInspectdClass2Master.View.SelectRow(irow);
                    grdInspectdClass2Master.View.FocusedColumn = grdInspectdClass2Master.View.Columns["INSPECTIONMETHODNAME"];
                    InspectdClass2MasterfocusedRowChanged();
                }
            }

            if (changedDetail.Rows.Count != 0)
            {
                for (int i = 0; i < changedDetail.Rows.Count; i++)
                {
                    DataRow row = changedDetail.Rows[i];
                    Inspitemid = row["INSPITEMID"].ToString();
                }
                if (Inspectiondefid.Equals(""))
                {

                    InspectdClass2MasterfocusedRowChanged();
                }
                int irow = checkGridProcess(Inspitemid, grdInspectdClass2Detail, "INSPITEMID");
                if (irow > -1)
                {
                    grdInspectdClass2Detail.View.ClearSelection();
                    grdInspectdClass2Detail.View.FocusedRowHandle = irow;

                    grdInspectdClass2Detail.View.SelectRow(irow);
                    grdInspectdClass2Detail.View.FocusedColumn = grdInspectdClass2Detail.View.Columns["INSPITEMNAME"];

                }
            }
        }
        /// <summary>
        /// 신뢰성 저장 ,저장후 조회   
        /// </summary>
        /// <returns></returns>
        private void SaveClassPage3Save()
        {
            string Inspectionclassid= "";
            string Inspectiondefid = "";
            string Inspitemid = "";
            DataTable changedMaster = grdInspectdClass3Master.GetChangedRows();
            changedMaster.TableName = "listMaster";
            DataTable changedDetail = grdInspectdClass3Detail.GetChangedRows();
            changedDetail.TableName = "listDetail";
            DataTable changedDetailSub = grdInspectdClass3DetailSub.GetChangedRows();
            changedDetailSub.TableName = "listDetailSub";
            DataSet dtSave = new DataSet();
            dtSave.Tables.Add(changedMaster);
            dtSave.Tables.Add(changedDetail);
            dtSave.Tables.Add(changedDetailSub);
            ExecuteRule("SaveInspectionDefiTypeReliability", dtSave);
            if (changedMaster.Rows.Count != 0)
            {
                for (int i = 0; i < changedMaster.Rows.Count; i++)
                {
                    DataRow row = changedMaster.Rows[i];
                    Inspectionclassid = row["INSPECTIONCLASSID"].ToString();
                }
                //
                SearchInspectdClass3Tree();
                SearchInspectdClass3Master();
                int irow = checkGridProcess(Inspectionclassid, grdInspectdClass3Master, "INSPECTIONCLASSID");
                if (irow > -1)
                {
                    grdInspectdClass3Master.View.ClearSelection();
                    grdInspectdClass3Master.View.FocusedRowHandle = irow;

                    grdInspectdClass3Master.View.SelectRow(irow);
                    grdInspectdClass3Master.View.FocusedColumn = grdInspectdClass3Master.View.Columns["INSPECTIONCLASSID"];
                    InspectdClass3MasterfocusedRowChanged();
                }
            }
            if (changedDetail.Rows.Count != 0)
            {
                for (int i = 0; i < changedDetail.Rows.Count; i++)
                {
                    DataRow row = changedDetail.Rows[i];
                    Inspectiondefid = row["INSPECTIONDEFID"].ToString();
                }
                if (Inspectionclassid.Equals(""))
                {

                    InspectdClass3MasterfocusedRowChanged();
                }
                int irow = checkGridProcess(Inspectiondefid, grdInspectdClass3Detail, "INSPECTIONDEFID");
                if (irow > -1)
                {
                    grdInspectdClass3Detail.View.ClearSelection();
                    grdInspectdClass3Detail.View.FocusedRowHandle = irow;

                    grdInspectdClass3Detail.View.SelectRow(irow);
                    grdInspectdClass3Detail.View.FocusedColumn = grdInspectdClass3Detail.View.Columns["INSPECTIONMETHODNAME"];
                    InspectdClass3DetailfocusedRowChanged();

                }
            }
            if (changedDetailSub.Rows.Count != 0)
            {
                for (int i = 0; i < changedDetailSub.Rows.Count; i++)
                {
                    DataRow row = changedDetailSub.Rows[i];
                    Inspitemid = row["INSPITEMID"].ToString();
                }
                if (Inspectionclassid.Equals(""))
                {

                    InspectdClass3DetailfocusedRowChanged();
                }
                int irow = checkGridProcess(Inspitemid, grdInspectdClass3DetailSub, "INSPITEMID");
                if (irow > -1)
                {
                    grdInspectdClass3DetailSub.View.ClearSelection();
                    grdInspectdClass3DetailSub.View.FocusedRowHandle = irow;

                    grdInspectdClass3DetailSub.View.SelectRow(irow);
                    grdInspectdClass3DetailSub.View.FocusedColumn = grdInspectdClass3DetailSub.View.Columns["INSPITEMNAME"];

                }
            }

        }
        #endregion
    }
}
