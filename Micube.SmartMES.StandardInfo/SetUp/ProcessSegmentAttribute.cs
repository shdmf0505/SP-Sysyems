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
using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 품목유형 등록 및 조회
    /// 업 무 설명 : 품목 유형등록 
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// 
    /// 
    /// </summary> 
	public partial class ProcessSegmentAttribute : SmartConditionManualBaseForm
    {
        #region Local Variables
        #endregion

        #region 생성자
        public ProcessSegmentAttribute()
        {
            InitializeComponent();
            InitializeEvent();
        }
        #endregion


        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        protected override void InitializeCondition()
        {

            base.InitializeCondition();
            InitializeCondition_ProductPopup();
            //InitializeCondition_Popup();
            // 버전
         


        }
        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeControl()
        {

            // 대공정
            //cboPsgTop.DisplayMember = "PROCESSSEGMENTCLASSNAME";
            //cboPsgTop.ValueMember = "PROCESSSEGMENTCLASSID";
            //cboPsgTop.ShowHeader = false;
            //Dictionary<string, object> ParamTop = new Dictionary<string, object>();
            //ParamTop.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //ParamTop.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtValidState = SqlExecuter.Query("GetProcessSegMentTop", "10001", ParamTop);
            //cboPsgTop.DataSource = dtValidState;

          

        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            //공정위치
            grdProcessPath.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdProcessPath.View.AddTextBoxColumn("PROCESSDEFID").SetIsHidden();
            grdProcessPath.View.AddTextBoxColumn("PROCESSDEFVERSION").SetIsHidden();
            grdProcessPath.View.AddTextBoxColumn("USERSEQUENCE").SetIsReadOnly();
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTID",100).SetIsReadOnly();
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTNAME",250).SetIsReadOnly();
            //grdProcessPath.View.AddTextBoxColumn("AREANAME").SetIsReadOnly();
            grdProcessPath.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdProcessPath.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID").SetIsHidden();

            grdProcessPath.View.PopulateColumns();
        
            //외주단가 유형등록
            grdRoutingAttributeValue.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdRoutingAttributeValue.View.AddTextBoxColumn("ITEMID", 100).SetIsHidden();
            grdRoutingAttributeValue.View.AddTextBoxColumn("ITEMVERSION", 100).SetIsHidden();
            grdRoutingAttributeValue.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsHidden();
            grdRoutingAttributeValue.View.AddTextBoxColumn("PLANTID", 100).SetIsHidden();
            grdRoutingAttributeValue.View.AddTextBoxColumn("USERSEQUENCE", 100).SetIsHidden();
            
            grdRoutingAttributeValue.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100).SetIsHidden();
            grdRoutingAttributeValue.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsHidden();

            
            grdRoutingAttributeValue.View.AddSpinEditColumn("SEQUENCE", 80).SetIsHidden();
            grdRoutingAttributeValue.View.AddComboBoxColumn("ATTRIBUTECLASS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AttributeClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();

            //grdRoutingAttributeValue.View.AddTextBoxColumn("OSPPRICECODE", 100).SetIsReadOnly();
            InitializeGrid_OspPriceListPopup();
            grdRoutingAttributeValue.View.AddTextBoxColumn("OSPPRICENAME", 250).SetIsReadOnly();
            grdRoutingAttributeValue.View.AddSpinEditColumn("ATTRIBUTEVALUE1")
             .SetDisplayFormat("#,##0.#########");
            grdRoutingAttributeValue.View.AddTextBoxColumn("ATTRIBUTECODE").SetIsReadOnly();

            //grdRoutingAttributeValue.View.AddComboBoxColumn("ATTRIBUTECODE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AttributeCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //grdRoutingAttributeValue.View.AddComboBoxColumn("SPECUNIT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AttributeCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //grdRoutingAttributeValue.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdRoutingAttributeValue.View.AddComboBoxColumn("CALCULATEUNIT", 100, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFNAME", "UOMDEFID").SetIsReadOnly();

            grdRoutingAttributeValue.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();

            grdRoutingAttributeValue.View.AddTextBoxColumn("CREATOR", 80)
                              .SetIsReadOnly()
                              .SetTextAlignment(TextAlignment.Center);
            grdRoutingAttributeValue.View.AddTextBoxColumn("CREATEDTIME", 130)
                                // Display Format 지정
                                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                                .SetIsReadOnly()
                                .SetTextAlignment(TextAlignment.Center);
            grdRoutingAttributeValue.View.AddTextBoxColumn("MODIFIER", 80)
                                .SetIsReadOnly()
                                .SetTextAlignment(TextAlignment.Center);
            grdRoutingAttributeValue.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                                .SetIsReadOnly()
                                .SetTextAlignment(TextAlignment.Center);


            grdRoutingAttributeValue.View.PopulateColumns();


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //외주단가 유형등록
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //grdRoutingSpecAttributeValue.GridButtonItem = GridButtonItem.Export;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            //grdRoutingSpecAttributeValue.View.AddTextBoxColumn("ITEMID", 100).SetIsHidden();
            //grdRoutingSpecAttributeValue.View.AddTextBoxColumn("ITEMVERSION", 100).SetIsHidden();
            //grdRoutingSpecAttributeValue.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsHidden();
            //grdRoutingSpecAttributeValue.View.AddTextBoxColumn("PLANTID", 100).SetIsHidden();
            //grdRoutingSpecAttributeValue.View.AddTextBoxColumn("USERSEQUENCE", 100).SetIsHidden();

            //grdRoutingSpecAttributeValue.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100).SetIsHidden();
            //grdRoutingSpecAttributeValue.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsHidden();


            //grdRoutingSpecAttributeValue.View.AddSpinEditColumn("SEQUENCE", 80).SetIsHidden();
            //grdRoutingSpecAttributeValue.View.AddComboBoxColumn("ATTRIBUTECLASS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AttributeClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();


            //// InitializeGrid_OspPriceListPopup();
            //// grdRoutingAttributeValue.View.AddTextBoxColumn("OSPPRICENAME", 100).SetIsReadOnly();
            //grdRoutingSpecAttributeValue.View.AddSpinEditColumn("ATTRIBUTEVALUE1", 200)
            //.SetDisplayFormat("#,##0.#########");

            //InitializeGrid_AttributePopup();

            ////grdRoutingAttributeValue.View.AddComboBoxColumn("ATTRIBUTECODE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AttributeCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            ////grdRoutingSpecAttributeValue.View.AddTextBoxColumn("ATTRIBUTECODE", 100).SetIsHidden();

            ////grdRoutingAttributeValue.View.AddComboBoxColumn("SPECUNIT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AttributeCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            ////grdRoutingAttributeValue.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            ////grdRoutingAttributeValue.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFNAME", "UOMDEFID").SetIsReadOnly();

            //grdRoutingSpecAttributeValue.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //grdRoutingSpecAttributeValue.View.AddTextBoxColumn("CREATOR", 80)
            //                  .SetIsReadOnly()
            //                  .SetTextAlignment(TextAlignment.Center);
            //grdRoutingSpecAttributeValue.View.AddTextBoxColumn("CREATEDTIME", 130)
            //                    // Display Format 지정
            //                    .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            //                    .SetIsReadOnly()
            //                    .SetTextAlignment(TextAlignment.Center);
            //grdRoutingSpecAttributeValue.View.AddTextBoxColumn("MODIFIER", 80)
            //                    .SetIsReadOnly()
            //                    .SetTextAlignment(TextAlignment.Center);
            //grdRoutingSpecAttributeValue.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            //                    .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            //                    .SetIsReadOnly()
            //                    .SetTextAlignment(TextAlignment.Center);


            //grdRoutingSpecAttributeValue.View.PopulateColumns();

            ////표준공정
            //grdPsgAttrBute.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            //grdPsgAttrBute.View.AddTextBoxColumn("PROCESSSEGMENTID");
            //grdPsgAttrBute.View.AddTextBoxColumn("PROCESSSEGMENTNAME");
            //grdPsgAttrBute.View.AddTextBoxColumn("AREAID");
            //grdPsgAttrBute.View.AddTextBoxColumn("AREANAME");
            //grdPsgAttrBute.View.PopulateColumns();

            ////항목정의
            //grdPsg.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            //grdPsg.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsHidden();
            //grdPsg.View.AddTextBoxColumn("PLANTID", 100).SetIsHidden();
            //grdPsg.View.AddTextBoxColumn("RESOURCEID", 100).SetIsHidden();

            //grdPsg.View.AddTextBoxColumn("RESOURCETYPE");
            //grdPsg.View.AddComboBoxColumn("ATTRIBUTECLASS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AttributeClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //grdPsg.View.AddComboBoxColumn("ATTRIBUTECODE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AttributeCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //grdPsg.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFNAME", "UOMDEFID");
            //grdPsg.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //grdPsg.View.PopulateColumns();

            InitializeGrdRoutingSpecAttributeValue();
        }

        private void InitializeGrdRoutingSpecAttributeValue()
        {
            grdRoutingSpecAttributeValue.GridButtonItem = GridButtonItem.Export;

            grdRoutingSpecAttributeValue.View.AddTextBoxColumn("ATTRIBUTECODE", 100).SetIsReadOnly();

            //InitializeGrid_ProcessAttributePopup();

            grdRoutingSpecAttributeValue.View.AddTextBoxColumn("ATTRIBUTEVALUE", 200).SetIsReadOnly()
                .SetDisplayFormat("#,##0.#########").SetLabel("ATTRIBUTEVALUE1");


            grdRoutingSpecAttributeValue.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdRoutingSpecAttributeValue.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdRoutingSpecAttributeValue.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdRoutingSpecAttributeValue.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);

            grdRoutingSpecAttributeValue.View.AddTextBoxColumn("OPERATIONID", 80)
                .SetIsReadOnly().SetIsHidden();
            grdRoutingSpecAttributeValue.View.AddTextBoxColumn("SEQUENCE", 80)
                .SetIsReadOnly().SetIsHidden();



            grdRoutingSpecAttributeValue.View.PopulateColumns();
        }

        private void InitializeGrid_AttributePopup()
        {

            var parentPopupColumn = this.grdRoutingSpecAttributeValue.View.AddSelectPopupColumn("ATTRIBUTECODE", 100, new SqlQuery("GetRoutingPSMAttributeValueCode", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("ATTRIBUTECODE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow);
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정

            //.SetPopupValidationCustom(ValidationOspPricePopup);


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
        private void InitializeGrid_OspPriceListPopup()
        {
           
            var parentPopupColumn = this.grdRoutingAttributeValue.View.AddSelectPopupColumn("OSPPRICECODE", new SqlQuery("GetOspPriceCodePupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("OSPPRICECODE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                //.SetPopupAutoFillColumns("CODECLASSNAME")
                // Validation 이 필요한 경우 호출할 Method 지정
              
                .SetPopupValidationCustom(ValidationOspPricePopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentPopupColumn.Conditions.AddTextBox("OSPPRICECODE");
            parentPopupColumn.Conditions.AddTextBox("OSPPRICENAME");
            // 팝업에서 사용할 조회조건 항목 추가
            parentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTID")
                .SetPopupDefaultByGridColumnId("PROCESSSEGMENTID")
                .SetIsHidden();
            parentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTCLASSID")
                 .SetPopupDefaultByGridColumnId("PROCESSSEGMENTCLASSID")
                .SetIsHidden();
            parentPopupColumn.Conditions.AddTextBox("ATTRIBUTECODE")
                 .SetPopupDefaultByGridColumnId("ATTRIBUTECODE")
                 .SetIsHidden();
            // 팝업 그리드 설정
            parentPopupColumn.GridColumns.AddTextBoxColumn("OSPPRICECODE", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("OSPPRICENAME", 150);

        }

        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeControl();
            InitializeGridIdDefinitionManagement();
        }

        #endregion

        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();



            DataTable changedAbvalue = new DataTable();

            changedAbvalue = grdRoutingAttributeValue.GetChangedRows();


            if (changedAbvalue != null)
            {
                if(changedAbvalue.Rows.Count !=0)
                {
                    DataTable dt = (DataTable)grdRoutingAttributeValue.DataSource;

                    DataRow[] rowSeq = dt.Select("1=1", "SEQUENCE DESC");

                    string sSeq = "";
                    if (rowSeq.Length !=0)
                    {
                        sSeq = rowSeq[0]["SEQUENCE"].ToString();
                    }
                    else
                    {
                        sSeq = "0";
                    }

                    decimal iseq = decimal.Parse(sSeq);
                    DataRow row = grdProcessPath.View.GetFocusedDataRow();


                    DataRow[] rowAbvalue = changedAbvalue.Select("_STATE_ = 'added'");

                    if(rowAbvalue.Length !=0)
                    {
                        foreach (DataRow rowNew in changedAbvalue.Select("_STATE_ = 'added'"))
                        {
                            rowNew["SEQUENCE"] = iseq = iseq + 1;
                        }
                    }

                    ExecuteRule("ProcessAttributeValue", changedAbvalue);
                }
                
            }



            DataTable changedSpecAbvalue = new DataTable();

            changedSpecAbvalue = grdRoutingSpecAttributeValue.GetChangedRows();


            if (changedSpecAbvalue != null)
            {
                if (changedSpecAbvalue.Rows.Count != 0)
                {
                    DataTable dt = (DataTable)grdRoutingSpecAttributeValue.DataSource;

                    DataRow[] rowSeq = dt.Select("1=1", "SEQUENCE DESC");

                    string sSeq = "";
                    if (rowSeq.Length != 0)
                    {
                        sSeq = rowSeq[0]["SEQUENCE"].ToString();
                    }
                    else
                    {
                        sSeq = "0";
                    }

                    decimal iseq = decimal.Parse(sSeq);
                    


                    DataRow[] rowAbvalue = changedSpecAbvalue.Select("_STATE_ = 'added'");

                    if (rowAbvalue.Length != 0)
                    {
                        foreach (DataRow rowNew in changedAbvalue.Select("_STATE_ = 'added'"))
                        {
                            rowNew["SEQUENCE"] = iseq = iseq + 1;
                        }
                    }




                    ExecuteRule("ProcessAttributeValue", changedSpecAbvalue);
                }

            }


            //DataTable changedPsg = new DataTable();
            //changedPsg = grdPsg.GetChangedRows();
            //if (changedPsg != null)
            //{
            //    if(changedPsg.Rows.Count !=0)
            //    {

            //        ExecuteRule("ProcessSegmentAttribute", changedPsg);
            //    }

            //}

        }
        #endregion

        #region 검색


        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_Popup()
        {
 
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("ITEMID", new SqlQuery("GetProductItemGroup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMID", "ITEMID")
               .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               .SetPosition(1)
               .SetPopupApplySelection((selectRow, gridRow) => {

                   List<string> productRevisionList = new List<string>();

                   selectRow.AsEnumerable().ForEach(r => {
                       productRevisionList.Add(Format.GetString(r["ITEMVERSION"]));
                   });

                   Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue = string.Join(",", productRevisionList);
               });
            parentPopupColumn.Conditions.AddComboBox("MASTERDATACLASSID", new SqlQuery("GetmasterdataclassList", "10001", $"ITEMOWNER={"Specifications"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MASTERDATACLASSNAME", "MASTERDATACLASSID");
            parentPopupColumn.Conditions.AddTextBox("ITEMID");
            parentPopupColumn.Conditions.AddTextBox("ITEMNAME");

            //팝업 그리드
            parentPopupColumn.GridColumns.AddTextBoxColumn("MASTERDATACLASSNAME", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("SPEC", 250);



        }
        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                //.SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(1.2)
                .SetValidationIsRequired()
                .SetPopupResultCount(1)
                .SetPopupApplySelection((selectRow, gridRow) => {

                    List<string> productDefnameList = new List<string>();
                    List<string> productRevisionList = new List<string>();

                    selectRow.AsEnumerable().ForEach(r => {
                        productDefnameList.Add(Format.GetString(r["PRODUCTDEFNAME"]));
                        productRevisionList.Add(Format.GetString(r["PRODUCTDEFVERSION"]));
                    });

                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Join(",", productDefnameList);
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Join(",", productRevisionList);
                });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetDefault("Product");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 110)
                .SetTextAlignment(TextAlignment.Center);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID")
                .SetTextAlignment(TextAlignment.Center);


        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {

            base.InitializeConditionControls();

            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").ReadOnly = true;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += ProductDefIDChanged;
            //Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").ReadOnly = true;
            //Conditions.GetControl<SmartSelectPopupEdit>("ITEMID").EditValueChanged += ProductDefIDChanged;

            SmartComboBox combo = Conditions.GetControl<SmartComboBox>("P_PLANTID");
            combo.EditValue = UserInfo.Current.Plant;
            combo.ReadOnly = true;



        }


        private void ProductDefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue = string.Empty;
            }
        }


        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 그리드 초기화
            grdProcessPath.DataSource = null;

            DataTable dtRoutingAttributeValue = (DataTable)grdRoutingAttributeValue.DataSource;
            if(dtRoutingAttributeValue != null)
            {
                dtRoutingAttributeValue.Clear();
            }

            DataTable dtRoutingSpecAttributeValue = (DataTable)grdRoutingSpecAttributeValue.DataSource;
            if (dtRoutingSpecAttributeValue != null)
            {
                dtRoutingSpecAttributeValue.Clear();
            }


            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            //TODO : Id를 수정하세요            

            //switch (tabIdManagement.SelectedTabPageIndex)
            //{
            //    case 0://ID Class

            DataTable dProcessPathList = await SqlExecuter.QueryAsync("GetProcessAttributeValuePath", "10001", values);
            grdProcessPath.DataSource = dProcessPathList;
            if (dProcessPathList.Rows.Count < 1) // 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
          

            DataRow row = grdProcessPath.View.GetFocusedDataRow();

            if(row !=null)
            {
                Dictionary<string, object> ParamValue = new Dictionary<string, object>();
                ParamValue.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                ParamValue.Add("PLANTID", UserInfo.Current.Plant);

                //ParamValue.Add("PROCESSSEGMENTCLASSID", row["PROCESSSEGMENTCLASSID"]);
                ParamValue.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);
                ParamValue.Add("USERSEQUENCE", row["USERSEQUENCE"]);

                ParamValue.Add("ITEMID", values["P_PRODUCTDEFID"].ToString());
                ParamValue.Add("ITEMVERSION", values["P_PRODUCTDEFVERSION"].ToString());
                ParamValue.Add("ATTRIBUTECLASS", "OSP");

                DataTable dtValue = SqlExecuter.Query("GetProcessAttributeValueList", "10001", ParamValue);
                grdRoutingAttributeValue.DataSource = dtValue;


                Dictionary<string, object> ParamSpecValue = new Dictionary<string, object>();
                ParamSpecValue.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                ParamSpecValue.Add("PLANTID", UserInfo.Current.Plant);

                //ParamValue.Add("PROCESSSEGMENTCLASSID", row["PROCESSSEGMENTCLASSID"]);
                ParamSpecValue.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);
                ParamSpecValue.Add("USERSEQUENCE", row["USERSEQUENCE"]);

                ParamSpecValue.Add("ITEMID", values["P_PRODUCTDEFID"].ToString());
                ParamSpecValue.Add("ITEMVERSION", values["P_PRODUCTDEFVERSION"].ToString());
                ParamSpecValue.Add("ATTRIBUTECLASS", "Specification");


                DataTable dtSpecValue = SqlExecuter.Query("GetProcessAttributeValueList", "10002", ParamSpecValue);
                grdRoutingSpecAttributeValue.DataSource = dtSpecValue;
            }


          



            //        break;
            //    case 1://공정조회
            //        DataTable dtProcessSegMent = await SqlExecuter.QueryAsync("GetProcessSegMentPupop", "10001", values);
            //        grdPsgAttrBute.DataSource = dtProcessSegMent;
            //        if (dtProcessSegMent.Rows.Count < 1) // 
            //        {
            //            ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            //        }



            //        break;

            //}

        }
        #endregion

        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = new DataTable();
            grdRoutingAttributeValue.View.CheckValidation();
            changed = grdRoutingAttributeValue.GetChangedRows();

            DataTable changed1 = new DataTable();
            grdRoutingSpecAttributeValue.View.CheckValidation();
            changed1 = grdRoutingSpecAttributeValue.GetChangedRows();

            if (changed.Rows.Count == 0 && changed1.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }
        #endregion

        #region 이벤트
        private void InitializeEvent()
        {
           grdProcessPath.View.FocusedRowChanged += grdProcessPath_FocusedRowChanged;
           grdRoutingAttributeValue.View.AddingNewRow += grdRoutingAttributeValue_AddingNewRow;
           grdRoutingSpecAttributeValue.View.AddingNewRow += grdRoutingSpecAttributeValue_AddingNewRow;


        }

        private void grdRoutingSpecAttributeValue_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow focrow = grdProcessPath.View.GetFocusedDataRow();

            if (focrow == null)
            {
                args.IsCancel = true;
            }

            args.NewRow["ENTERPRISEID"] = focrow["ENTERPRISEID"];
            args.NewRow["PLANTID"] = focrow["PLANTID"];

            args.NewRow["PROCESSSEGMENTID"] = focrow["PROCESSSEGMENTID"];
            args.NewRow["PROCESSSEGMENTCLASSID"] = focrow["PROCESSSEGMENTCLASSID"];
            args.NewRow["USERSEQUENCE"] = focrow["USERSEQUENCE"];

            args.NewRow["ITEMID"] = focrow["PROCESSDEFID"];
            args.NewRow["ITEMVERSION"] = focrow["PROCESSDEFVERSION"];
            args.NewRow["ATTRIBUTECLASS"] = "Specification";
            args.NewRow["SEQUENCE"] = 0;
            args.NewRow["VALIDSTATE"] = "Valid";
        }

        private void grdRoutingAttributeValue_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            DataRow focrow = grdProcessPath.View.GetFocusedDataRow();

            if(focrow == null)
            {
                args.IsCancel = true;
            }

            args.NewRow["ENTERPRISEID"] = focrow["ENTERPRISEID"];
            args.NewRow["PLANTID"] = focrow["PLANTID"];

            args.NewRow["PROCESSSEGMENTID"] = focrow["PROCESSSEGMENTID"];
            args.NewRow["PROCESSSEGMENTCLASSID"] = focrow["PROCESSSEGMENTCLASSID"];
            args.NewRow["USERSEQUENCE"] = focrow["USERSEQUENCE"];

            args.NewRow["ITEMID"] = focrow["PROCESSDEFID"];
            args.NewRow["ITEMVERSION"] = focrow["PROCESSDEFVERSION"];
            args.NewRow["ATTRIBUTECLASS"] = "OSP";
            args.NewRow["SEQUENCE"] = 0;
            args.NewRow["VALIDSTATE"] = "Valid";
            

        }

        //private void grdRoutingAttributeValue_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        //{
            

        //    switch(this.grdRoutingAttributeValue.View.FocusedColumn.FieldName)
        //    {
        //        case "OSPPRICECODE":
        //            DataRow row = this.grdRoutingAttributeValue.View.GetFocusedDataRow();
        //            if (row != null)
        //            {
        //                switch (row["ATTRIBUTECLASS"].ToString())
        //                {
        //                    case "OSP":
        //                        grdRoutingAttributeValue.View.SetIsReadOnly(true);
        //                        break;
        //                    default:
        //                        grdRoutingAttributeValue.View.SetIsReadOnly(false);
        //                        break;
        //                }
        //            }
        //            break;
        //        default:
        //            grdRoutingAttributeValue.View.SetIsReadOnly(false);
        //            break;
        //    }
            


            
        //}

       

        private void CboPsgTop_EditValueChanged(object sender, EventArgs e)
        {
            // 중공정
            ////cboPsgMid.DisplayMember = "PROCESSSEGMENTCLASSNAME";
            ////cboPsgMid.ValueMember = "PROCESSSEGMENTCLASSID";
            ////cboPsgMid.ShowHeader = false;
            ////Dictionary<string, object> ParamMid = new Dictionary<string, object>();
            ////ParamMid.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ////ParamMid.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            ////ParamMid.Add("PARENTPROCESSSEGMENTCLASSID", cboPsgTop.EditValue);
            ////DataTable dtConfirmation = SqlExecuter.Query("GetProcessSegMentMiddle", "10001", ParamMid);
            ////cboPsgMid.DataSource = dtConfirmation;
        }

        private void btnPsgScarch_Click(object sender, EventArgs e)
        {

            //Dictionary<string, object> ParamPsg = new Dictionary<string, object>();
            //ParamPsg.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //ParamPsg.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //ParamPsg.Add("PARENTPROCESSSEGMENTCLASSID", cboPsgTop.EditValue);
            //ParamPsg.Add("PROCESSSEGMENTCLASSID", cboPsgMid.EditValue);
            //DataTable dtPsg = SqlExecuter.Query("GetProcessSegMentAttributeTempList", "10001", ParamPsg);

            //grdPsgAttrBute.DataSource = dtPsg;


        }

        private void TxtCalculus_TextChanged(object sender, EventArgs e)
        {
            //Calculus();
        }

        //void Calculus()
        //{
        //    txtCardTary.Text = (decimal.Parse(txtPackQty.Text) * decimal.Parse(txtCaseQty.Text) * decimal.Parse(txtBoxQty.Text)).ToString();
        //        txtCase.Text = (decimal.Parse(txtPackQty.Text) * decimal.Parse(txtCaseQty.Text)).ToString();
        //}




        #region 그리드이벤트


        private void grdRoutingAttributeValue_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //DataRow row = grdPsgAttrBute.View.GetFocusedDataRow();

            //Dictionary<string, object> ParamPsg = new Dictionary<string, object>();
            //ParamPsg.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //ParamPsg.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);
            //DataTable dtPsg = SqlExecuter.Query("GetProcessSegMentAttributeList", "10001", ParamPsg);

            //grdPsg.DataSource = dtPsg;
        }

        private void grdProcessPath_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdProcessPath.View.FocusedRowHandle < 0)
                return;

            var values = this.Conditions.GetValues();

            DataRow row = grdProcessPath.View.GetFocusedDataRow();
            Dictionary<string, object> ParamValue = new Dictionary<string, object>();
            ParamValue.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamValue.Add("PLANTID", UserInfo.Current.Plant);

            //ParamValue.Add("PROCESSSEGMENTCLASSID", row["PROCESSSEGMENTCLASSID"]);
            ParamValue.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);
            ParamValue.Add("USERSEQUENCE", row["USERSEQUENCE"]);

            ParamValue.Add("ITEMID", values["P_PRODUCTDEFID"].ToString());
            ParamValue.Add("ITEMVERSION", values["P_PRODUCTDEFVERSION"].ToString());
            ParamValue.Add("ATTRIBUTECLASS", "OSP");

            DataTable dtValue = SqlExecuter.Query("GetProcessAttributeValueList", "10001", ParamValue);
            grdRoutingAttributeValue.DataSource = dtValue;


            Dictionary<string, object> ParamSpecValue = new Dictionary<string, object>();
            ParamSpecValue.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamSpecValue.Add("PLANTID", UserInfo.Current.Plant);

            //ParamValue.Add("PROCESSSEGMENTCLASSID", row["PROCESSSEGMENTCLASSID"]);
            ParamSpecValue.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);
            ParamSpecValue.Add("USERSEQUENCE", row["USERSEQUENCE"]);

            ParamSpecValue.Add("ITEMID", values["P_PRODUCTDEFID"].ToString());
            ParamSpecValue.Add("ITEMVERSION", values["P_PRODUCTDEFVERSION"].ToString());
            ParamSpecValue.Add("ATTRIBUTECLASS", "Specification");


            DataTable dtSpecValue = SqlExecuter.Query("GetProcessAttributeValueList", "10002", ParamSpecValue);
            grdRoutingSpecAttributeValue.DataSource = dtSpecValue;



        }

        private void grdPsg_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //DataRow row = grdPsgAttrBute.View.GetFocusedDataRow();

            //args.NewRow["RESOURCETYPE"] = "ProcessSegmentID";
            //args.NewRow["VALIDSTATE"] = "Valid";
            //args.NewRow["RESOURCEID"] = row["PROCESSSEGMENTID"];
            //args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            //args.NewRow["PLANTID"] = UserInfo.Current.Plant;
        }



        #endregion

        #region 기타이벤트

        //private void TabIdManagement_Click(object sender, EventArgs e)
        //{
        //    var values = this.Conditions.GetValues();
        //    values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

        //    switch (tabIdManagement.SelectedTabPageIndex)
        //    {
        //        case 0:
        //             // 마스터 클래스 조회
        //            DataTable dtPPLList = SqlExecuter.Query("GetPackageProductList", "10001", values);
        //            if (dtPPLList.Rows.Count < 1) // 
        //            {
        //                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
        //            }

        //            this.grdPackageProduct.DataSource = dtPPLList;
        //            break;
        //        case 1:
        //            // 등록된 마스트 클래스 데이터 조회
        //            DataTable dtPPLHIsList = SqlExecuter.Query("GetPackageProductHisList", "10001", values);
        //            if (dtPPLHIsList.Rows.Count < 1) // 
        //            {
        //                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
        //            }

        //           // this.grdPackageProductHis.DataSource = dtPPLHIsList;

        //            break;
        //        default:
        //            break;

        //    }


          
        //}


        private ValidationResultCommon ValidationOspPricePopup(DataRow currentRow, IEnumerable<DataRow> popupSelections)
        {

            ValidationResultCommon result = new ValidationResultCommon();

            DataTable dtAttributeValue = (DataTable)grdRoutingAttributeValue.DataSource;


            foreach (DataRow row in popupSelections)
            {
                if(dtAttributeValue.Select("OSPPRICECODE = '" + row["OSPPRICECODE"].ToString() + "'").Length !=0)
                {
                    Language.LanguageMessageItem item = Language.GetMessage("SelectOverlap");
                    result.IsSucced = false;
                    result.FailMessage = item.Message;
                    result.Caption = item.Title;
                }
                else
                {
                    currentRow["OSPPRICENAME"] = row["OSPPRICENAME"];
                    currentRow["ATTRIBUTECODE"] = row["SPECUNIT"];
                    currentRow["CALCULATEUNIT"] = row["CALCULATEUNIT"];
                }
                
            }
            return result;
        }

        #endregion

        #endregion



        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion
    }
}
