#region using
using DevExpress.XtraTreeList.Nodes;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.StandardInfo.Popup;
using Micube.SmartMES.Commons;
using Newtonsoft.Json;
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
using DevExpress.XtraEditors.Repository;
using Micube.Framework.SmartControls.Grid;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > Routing 자원등록
    /// 업 무 설명 : Routing 등록및 조회
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// 
    /// 
    /// </summary> 
    public partial class RoutingResource : SmartConditionManualBaseForm
	{
        #region Local Variables
        int focus = 0;
        private List<int> focusedNodeIndexList { get; set; }
        private int savedFocuseOperationRowHandle = 0;
        
        #endregion

        #region 생성자
        public RoutingResource()
		{
			InitializeComponent();
            smartTabControl1.SetLanguageKey(this.xtraTabPage1, "RESOURCE");
            smartTabControl1.SetLanguageKey(this.xtraTabPage2, "DURABLE");
            smartTabControl1.SetLanguageKey(this.xtraTabPage3, "PROCESSATTRIBUTEVALUE");

        }

        /// <summary>
        /// 외부에서 호출시 자동 조회
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                _parameters = parameters;
                Conditions.SetValue("P_PRODUCTDEFID", 0, parameters["ITEMID"]);
                Conditions.SetValue("P_PRODUCTDEFVERSION", 0, parameters["ITEMVERSION"]);
                Conditions.SetValue("p_ProductName", 0, parameters["ITEMNAME"]);
                OnSearchAsync();


            }
        }
        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeControl()
        {
            this.scRTRSheet.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            this.scRTRSheet.ValueMember = "CODEID";
            this.scRTRSheet.DisplayMember = "CODENAME";

            this.scRTRSheet.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "RTRSHT" } });
            this.scRTRSheet.UseEmptyItem = true;

            this.scRTRSheet.ShowHeader = false;
        }
		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGridList()
		{

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //공정 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdOperation.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;
            //grdOperation.View.AddSpinEditColumn("OPERATIONSEQUENCE")
            //    .SetIsReadOnly();
            grdOperation.View.AddTextBoxColumn("PLANTID", 70)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("USERSEQUENCE", 75)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTID",70)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 160)
                .SetIsReadOnly();
            grdOperation.View.AddTextBoxColumn("RESOURCEYN", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("ISRESOURCEREGIST");
            grdOperation.View.AddTextBoxColumn("DURABLEYN", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("ISDURABLEREGIST");
            grdOperation.View.AddTextBoxColumn("DESCRIPTION", 300)
                .SetIsReadOnly();

            //자재필수여부
            grdOperation.View.AddTextBoxColumn("ISREQUIREDMATERIAL", 90)
                .SetLabel("ISREQUIREDMATERIAL2")
                .SetTextAlignment(TextAlignment.Center);
            //공정SPEC필수여부
            grdOperation.View.AddTextBoxColumn("ISREQUIREDOPERATIONSPEC", 90)
                .SetTextAlignment(TextAlignment.Center);
            //TOOL필수여부
            grdOperation.View.AddTextBoxColumn("ISREQUIREDTOOL", 90)
                .SetTextAlignment(TextAlignment.Center);

            grdOperation.View.AddTextBoxColumn("CREATOR", 80)
             .SetIsReadOnly()
             .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);


            grdOperation.View.PopulateColumns();


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //TAB 0 : 자원 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //자원관리 팝업
            InitializeGrid_ResourcePopup();
            grdResource.View.AddTextBoxColumn("RESOURCENAME", 220).SetIsReadOnly();

            grdResource.View.AddComboBoxColumn("ISPRIMARY", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);

            grdResource.View.AddTextBoxColumn("EQUIPMENTCLASSID", 100).SetIsReadOnly();
            grdResource.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 100).SetIsReadOnly();

            //InitializeGrid_AreaPopup();
            grdResource.View.AddTextBoxColumn("AREAID", 100).SetIsReadOnly();
            grdResource.View.AddTextBoxColumn("AREANAME", 170).SetIsReadOnly();


            this.grdResource.View.AddTextBoxColumn("CREATORNAME", 80)
             // ReadOnly 컬럼 지정
             .SetIsReadOnly()
             .SetTextAlignment(TextAlignment.Center);
            this.grdResource.View.AddTextBoxColumn("CREATEDTIME", 130)
                // Display Format 지정
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            this.grdResource.View.AddTextBoxColumn("MODIFIERNAME", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            this.grdResource.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdResource.View.PopulateColumns();

            this.grdDurable.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;



            if (UserInfo.Current.Enterprise == "INTERFLEX")
            {
                //자원관리 팝업
                InitializeGrid_DurabledefinitPopup();
                this.grdDurable.View.AddTextBoxColumn("TOOLNAME", 250).SetIsReadOnly();
                this.grdDurable.View.AddTextBoxColumn("TOOLVERSION", 120).SetIsReadOnly().SetLabel("DURABLEDEFVERSION");
                this.grdDurable.View.AddTextBoxColumn("FILMUSELAYER1", 120).SetIsReadOnly();
                this.grdDurable.View.AddTextBoxColumn("FILMUSELAYER2", 120).SetIsReadOnly();

                this.grdDurable.View.AddTextBoxColumn("DESCRIPTION", 200);
                this.grdDurable.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
                this.grdDurable.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
                this.grdDurable.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
                this.grdDurable.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);

                grdDurable.View.PopulateColumns();
            }
            else if (UserInfo.Current.Enterprise == "YOUNGPOONG")
            {
                //OPERATION ID
                grdDurable.View.AddTextBoxColumn("OPERATIONID").SetIsHidden();
      //          grdDurable.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID").SetIsHidden();
                grdDurable.View.AddTextBoxColumn("DURABLETYPE").SetIsHidden();
                //품목코드
                grdDurable.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly().SetIsHidden();
                //품목버전
                grdDurable.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetIsReadOnly().SetIsHidden();
                //품목명
                grdDurable.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetIsReadOnly().SetIsHidden();
                //TOOL구분
                grdDurable.View.AddComboBoxColumn("DURABLECLASSID", 80, new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "CODECLASSID=DurableClass"), "CODENAME", "CODEID")
                    .SetTextAlignment(TextAlignment.Center).SetLabel("DURABLECATEGORYCODE").SetIsReadOnly();
                //치공구 코드
                InitializeGridSpec_ToolPopup();
                //치공구명
                grdDurable.View.AddTextBoxColumn("TOOLNAME", 250);
                //치공구버전
                grdDurable.View.AddTextBoxColumn("TOOLVERSION", 50).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetLabel("DURABLEDEFVERSION");
                //TOOL유형1
                grdDurable.View.AddComboBoxColumn("TOOLTYPE", 60, new SqlQuery("GetToolTypeByClassId", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                    .SetTextAlignment(TextAlignment.Center).SetLabel("TOOLCATEGORYDETAIL").SetIsReadOnly();
                //TOOL유형2
                grdDurable.View.AddComboBoxColumn("TOOLDETAILTYPE", 60, new SqlQuery("GetDurableClassType2", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                    .SetTextAlignment(TextAlignment.Center).SetLabel("TOOLDETAIL").SetIsReadOnly();
                //TOOL형식
                grdDurable.View.AddTextBoxColumn("FORM", 80)
                    .SetLabel("TOOLFORMCODE").SetIsReadOnly();
                //합수
                grdDurable.View.AddSpinEditColumn("SUMMARY", 70).SetLabel("ARRAY").SetIsReadOnly();
                //사용층
                //grdDurable.View.AddComboBoxColumn("FILMUSELAYER1", 70, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=FilmUseLayer1"), "CODENAME", "CODEID")
                //    .SetLabel("USERLAYER").SetIsReadOnly();
                grdDurable.View.AddTextBoxColumn("FILMUSELAYER1", 70).SetIsHidden();
                grdDurable.View.AddTextBoxColumn("FILMUSELAYER1NAME", 70).SetLabel("USERLAYER").SetIsReadOnly();

                //타수
                grdDurable.View.AddTextBoxColumn("HITCOUNT", 70).SetIsReadOnly();
                //아대/일반
                grdDurable.View.AddComboBoxColumn("WRAPTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecPanelGuide", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID").SetLabel("PANELGUIDENORMAL");

                this.grdDurable.View.AddTextBoxColumn("DESCRIPTION", 200);
                this.grdDurable.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
                this.grdDurable.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
                this.grdDurable.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
                this.grdDurable.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);


                grdDurable.View.PopulateColumns();

                (grdDurable.View.Columns["TOOLCODE"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += RoutingVer3_ButtonClick;
            }




            // 외주 단가 그리드 초기화
            InitializeGridProcessAttributeValue();

        }

        /// <summary>
        /// 팝업형 그리드 컬럼 초기화 - 치공구ID
        /// </summary>
        private void InitializeGridSpec_ToolPopup()
        {
            var toolPopupColumn = this.grdDurable.View.AddSelectPopupColumn("TOOLCODE", 150, new SqlQuery("GetDurabledefiniTionPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTDURABLE", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(1000, 800, FormBorderStyle.SizableToolWindow)
                .SetPopupApplySelection((selectRows, dataGridRow) =>
                {
                    if (selectRows.Count() > 0)
                    {
                        DataTable dt2 = this.grdDurable.DataSource as DataTable;
                        int handle = this.grdDurable.View.FocusedRowHandle;
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
                                dr = this.grdDurable.View.GetDataRow(handle);
                            }

                            dr["PRODUCTDEFID"] = this.txtProductDEFId.Text;
                            dr["PRODUCTDEFVERSION"] = this.txtProductDEFVersion.Text;
                            dr["PRODUCTDEFNAME"] = this.txtProductDEFName.Text;
                            dr["DURABLETYPE"] = row["DURABLECLASSTYPE"];
                            dr["TOOLCODE"] = row["TOOLCODE"];
                            dr["TOOLNAME"] = row["DURABLEDEFNAME"];
                            dr["DURABLECLASSID"] = Format.GetString(row["DURABLECLASSID"]);//TOOL 구분 = DURABLECLASSID
                            dr["TOOLTYPE"] = Format.GetString(row["TOOLTYPE"]);//유형1 = TOOLTYPE
                            dr["TOOLDETAILTYPE"] = Format.GetString(row["TOOLDETAILTYPE"]);//유형2 = TOOLDETAILTYPE
                            dr["FORM"] = Format.GetString(row["FORM"]);//TOOL 형식 = FORM
                            dr["TOOLVERSION"] = Format.GetString(row["DURABLEDEFVERSION"]);//치공구 Rev
                            dr["SUMMARY"] = row["SUMMARY"];//합수
                            dr["FILMUSELAYER1"] = Format.GetString(row["FILMUSELAYER1"]);//사용층
                            dr["FILMUSELAYER1NAME"] = Format.GetString(row["FILMUSELAYER1NAME"]);//사용층
                            dr["HITCOUNT"] = row["HITCOUNT"];//타수


                            this.grdDurable.View.RaiseValidateRow(handle);
                            handle++;
                        }
                    }
                })
                .SetPopupAutoFillColumns("DURABLEDEFNAME")
                .SetLabel("DURABLEDEFID");

            toolPopupColumn.Conditions.AddTextBox("ITEMID").SetLabel("TXTPRODUCTDEFNAME").SetPopupDefaultByGridColumnId("PRODUCTDEFID");



            // 팝업에서 사용할 조회조건 항목 추가
            toolPopupColumn.Conditions.AddTextBox("DURABLEDEFID");
            toolPopupColumn.Conditions.AddTextBox("DURABLEDEFNAME");
            //	toolPopupColumn.Conditions.AddTextBox("ITEMVERSION").SetPopupDefaultByGridColumnId("PRODUCTDEFVERSION").SetIsHidden();
            toolPopupColumn.Conditions.AddTextBox("OPERATIONID").SetPopupDefaultByGridColumnId("OPERATIONID").SetIsHidden();


            toolPopupColumn.GridColumns.AddTextBoxColumn("DURABLECLASSTYPE", 60);
            toolPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 100);
            toolPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetTextAlignment(TextAlignment.Center);
            toolPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            //TOOL 구분(DURABLECLASSID)
            toolPopupColumn.GridColumns.AddComboBoxColumn("DURABLECLASSID", 80, new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "CODECLASSID=DurableClass"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("DURABLECATEGORYCODE");
            //TOOL ID
            toolPopupColumn.GridColumns.AddTextBoxColumn("TOOLCODE", 100).SetLabel("DURABLEDEFID");
            //TOOL VERSION
            toolPopupColumn.GridColumns.AddTextBoxColumn("DURABLEDEFVERSION", 50).SetTextAlignment(TextAlignment.Center);
            //TOOL 명
            toolPopupColumn.GridColumns.AddTextBoxColumn("DURABLEDEFNAME", 100);
            //TOOL TYPE
            toolPopupColumn.GridColumns.AddComboBoxColumn("TOOLTYPE", 60, new SqlQuery("GetToolTypeByClassId", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center).SetLabel("TOOLCATEGORYDETAIL");
            //TOOL DETAIL TYPE
            toolPopupColumn.GridColumns.AddComboBoxColumn("TOOLDETAILTYPE", 60, new SqlQuery("GetDurableClassType2", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center).SetLabel("TOOLDETAIL");
            //TOOL 형식(FORM)
            toolPopupColumn.GridColumns.AddTextBoxColumn("FORM", 80)
                .SetLabel("TOOLFORMCODE");
            //합수
            toolPopupColumn.GridColumns.AddSpinEditColumn("SUMMARY", 70).SetLabel("ARRAY");

            //사용층
            toolPopupColumn.GridColumns.AddTextBoxColumn("FILMUSELAYER1", 70).SetIsHidden();
            toolPopupColumn.GridColumns.AddTextBoxColumn("FILMUSELAYER1NAME", 70).SetLabel("USERLAYER");

            //타수
            toolPopupColumn.GridColumns.AddSpinEditColumn("HITCOUNT", 70);
        }



        /// <summary>
        /// 팝업 컬럼 x버튼 누를 때 이벤트 - 치공구
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoutingVer3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
            {
                grdDurable.View.SetFocusedRowCellValue("DURABLETYPE", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("DURABLEDEFVERSION", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("DURABLECLASSID", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("TOOLTYPE", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("TOOLDETAILTYPE", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("FORM", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("TOOLVERSION", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("SUMMARY", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("FILMUSELAYER1", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("HITCOUNT", string.Empty);
            }
        }

        private void InitializeGridProcessAttributeValue()
        {
            grdProcessAttributeValue.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;

            grdProcessAttributeValue.View.AddComboBoxColumn("ATTRIBUTECODE", 100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001",
                    "UOMCATEGORY=OSPSPEC", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetEmptyItem("", "")
                //.SetLabel("ATTRIBUTECODE")
                .SetValidationKeyColumn()
                .SetValidationIsRequired();

            //InitializeGrid_ProcessAttributePopup();

            grdProcessAttributeValue.View.AddSpinEditColumn("ATTRIBUTEVALUE1", 200)
            .SetDisplayFormat("#,##0.#########").SetValidationIsRequired();


            grdProcessAttributeValue.View.AddTextBoxColumn("CREATOR", 80)
               .SetIsReadOnly()
               .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdProcessAttributeValue.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdProcessAttributeValue.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdProcessAttributeValue.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);

            grdProcessAttributeValue.View.AddTextBoxColumn("OPERATIONID", 80)
                .SetIsReadOnly().SetIsHidden();



            grdProcessAttributeValue.View.PopulateColumns();
        }

        //private void InitializeGrid_ProcessAttributePopup()
        //{

        //    var parentPopupColumn = this.grdProcessAttributeValue.View.AddSelectPopupColumn("ATTRIBUTECODE", 100, new SqlQuery("GetRoutingPSMAttributeValueCode", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
        //        // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
        //        .SetPopupLayout("ATTRIBUTECODE", PopupButtonStyles.Ok_Cancel, true, false)
        //        // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
        //        .SetPopupResultCount(0)
        //        .SetValidationIsRequired()
        //        // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
        //        //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
        //        // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
        //        .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow);



        //    parentPopupColumn.Conditions.AddTextBox("ATTRIBUTECODE");
        //    // 팝업에서 사용할 조회조건 항목 추가
        //    parentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTID")
        //        .SetPopupDefaultByGridColumnId("PROCESSSEGMENTID")
        //        .SetIsHidden();
        //    parentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTCLASSID")
        //         .SetPopupDefaultByGridColumnId("PROCESSSEGMENTCLASSID")
        //        .SetIsHidden();
        //    // 팝업 그리드 설정
        //    parentPopupColumn.GridColumns.AddTextBoxColumn("ATTRIBUTECODE", 150);
        //    //parentPopupColumn.GridColumns.AddTextBoxColumn("OSPPRICENAME", 150);

        //}

        private void grdProcessAttributeValueView_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (this.grdOperation.View.FocusedRowHandle < 0)
            {
                args.IsCancel = true;

                return;
            }

            DataRow focusedRow = this.grdOperation.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                {
                    ShowMessage("NoSelectDataSaveAndProceed");

                    args.IsCancel = true;

                    return;
                }
            }
            else
            {
                args.IsCancel = true;

                return;
            }
        }



        // 설비그룹팝업
        private void InitializeGrid_EquipmentClassPopup()
        {
            var parentCodeClassPopupColumn = this.grdResource.View.AddSelectPopupColumn("EQUIPMENTCLASSID", new SqlQuery("SelectEquipMentClass", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("SELECTEQUIPMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                //.SetPopupAutoFillColumns("CODECLASSNAME")
                // Validation 이 필요한 경우 호출할 Method 지정

                .SetPopupValidationCustom(ValidationEquipmentClassIdPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("EQUIPMENTCLASSID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("EQUIPMENTCLASSNAME");

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 100);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("TOPEQUIPMENTCLASS", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEEQUIPMENTCLASS", 150);

        }






        // 자원관리 팝업
        private void InitializeGrid_ResourcePopup()
        {

            var parentCodeClassPopupColumn = this.grdResource.View.AddSelectPopupColumn("RESOURCEID", 120, new SqlQuery("GetResourcePopup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성0화 여부, 자동 검색 여부
                .SetPopupLayout("RESOURCE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                //.SetPopupAutoFillColumns("CODECLASSNAME")
                // Validation 이 필요한 경우 호출할 Method 지정
                //.SetRelationIds("PROCESSSEGMENTID")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                 
                    if (selectedRows.Count() > 0)
                    {
                        DataTable dt2 = this.grdResource.DataSource as DataTable;
                        int handle = this.grdResource.View.FocusedRowHandle;

                        DataRow operationRow = this.grdOperation.View.GetFocusedDataRow();

                        // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                        // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                        foreach (DataRow row in selectedRows)
                        {
                            DataRow dr = null;


                            DataTable dtResource = this.grdResource.DataSource as DataTable;
                            DataRow[] rowR = dtResource.Select("1=1", "RESOURCESEQUENCE DESC");


                            if (dt2.Rows.Count < handle + 1)
                            {
                                dr = dt2.NewRow();
                                dt2.Rows.Add(dr);

                                if(dtResource != null)
                                { 
                                    if (dtResource.Rows.Count != 0)
                                        dr["RESOURCESEQUENCE"] = int.Parse(rowR[0]["RESOURCESEQUENCE"].ToString()) + 10;
                                    else
                                        dr["RESOURCESEQUENCE"] = 10;
                                }
                                else
                                    dr["RESOURCESEQUENCE"] = 10;
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


                            dr["ENTERPRISEID"] = operationRow["ENTERPRISEID"];
                            dr["PLANTID"] = operationRow["PLANTID"];
                            dr["OPERATIONID"] = operationRow["OPERATIONID"];
                            dr["DURABLETYPE"] = "Resource";
                            dr["RESOURCESEQUENCE"] = 0;

                            dr["MAINPRODUCTID"] = operationRow["MAINPRODUCTID"];
                            dr["MAINPRODUCTVERSION"] = operationRow["MAINPRODUCTVERSION"];
                            dr["PROCESSSEGMENTID"] = operationRow["PROCESSSEGMENTID"];
                            dr["VALIDSTATE"] = "Valid";

                            if (dtResource != null)
                            {
                                if (dtResource.Rows.Count != 0)
                                    dr["ISPRIMARY"] = "N";
                                else
                                    dr["ISPRIMARY"] = "Y";
                            }
                            else
                                dr["ISPRIMARY"] = "Y";

                            this.grdResource.View.RaiseValidateRow(handle);

                            handle++;
                        }

                    }

                })
                ;


            // 팝업에서 사용할 조회조건 항목 추가

            parentCodeClassPopupColumn.Conditions.AddTextBox("RESOURCEID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("DESCRIPTION");
            parentCodeClassPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTID")
                 .SetPopupDefaultByGridColumnId("PROCESSSEGMENTID")
                 .SetIsReadOnly()
                 .SetIsHidden();

            parentCodeClassPopupColumn.Conditions.AddTextBox("PLANTID")
                .SetPopupDefaultByGridColumnId("PLANTID")
                .SetIsReadOnly()
                 .SetIsHidden();


            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("RESOURCEID", 90);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("DESCRIPTION", 220);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 80);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);

        }


        // 치공구관리 팝업
        private void InitializeGrid_DurabledefinitPopup()
        {
            var parentDurabledefinitPopupColumn = this.grdDurable.View.AddSelectPopupColumn("TOOLCODE", 120, new SqlQuery("GetDurabledefiniTionPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("SELECTDURABLE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("TOOLCODE", "DURABLEDEFID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                .SetLabel("DURABLEDEFID")
                  // 그리드의 남은 영역을 채울 컬럼 설정
                  //.SetPopupAutoFillColumns("CODECLASSNAME")
                  // Validation 이 필요한 경우 호출할 Method 지정
                  .SetPopupApplySelection((selectRows, gridRow) =>
                  {
                      if (selectRows.Count() > 0)
                      {
                          DataTable dt2 = this.grdDurable.DataSource as DataTable;
                          int handle = this.grdDurable.View.FocusedRowHandle;

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
                                  dr = this.grdDurable.View.GetDataRow(handle);
                              }

                              dr["DURABLETYPE"] = row["DURABLECLASSTYPE"].ToString();
                              dr["TOOLCODE"] = row["DURABLEDEFID"].ToString();
                              dr["TOOLNAME"] = row["DURABLEDEFNAME"].ToString();
                              dr["TOOLVERSION"] = row["DURABLEDEFVERSION"].ToString();
                              dr["FILMUSELAYER1"] = row["FILMUSELAYER1"].ToString();
                              dr["FILMUSELAYER2"] = row["FILMUSELAYER2"].ToString();

                              this.grdDurable.View.RaiseValidateRow(handle);

                              handle++;
                          }
                      }
                  });
            // .SetPopupValidationCustom(ValidationDurable);

            // 팝업에서 사용할 조회조건 항목 추가
            parentDurabledefinitPopupColumn.Conditions.AddTextBox("DURABLEDEFID");
            parentDurabledefinitPopupColumn.Conditions.AddTextBox("DURABLEDEFNAME");

            parentDurabledefinitPopupColumn.Conditions.AddTextBox("ITEMID");
            parentDurabledefinitPopupColumn.Conditions.AddTextBox("ITEMNAME");
            parentDurabledefinitPopupColumn.Conditions.AddTextBox("ITEMVERSION");

            parentDurabledefinitPopupColumn.Conditions.AddComboBox("DURABLECLASSTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=DurableClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("*");

            // 팝업 그리드 설정
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("DURABLETYPENAME", 100).SetLabel("DURABLECLASSTYPE");
            parentDurabledefinitPopupColumn.GridColumns.AddComboBoxColumn("DURABLECLASSTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DurableClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("DURABLECLASS").SetIsHidden();
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("DURABLEDEFID", 120);
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("DURABLEDEFNAME", 250);
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("DURABLEDEFVERSION", 100);

            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("FILMUSELAYER1", 120);
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("FILMUSELAYER2", 120);

            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            parentDurabledefinitPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);


        }


        /// <summary>
        /// 설정 초기화
        /// </summary>
        protected override void InitializeContent()
		{
			base.InitializeContent();
			InitializeGridList();
			InitializeEvent();
            InitializeControl();

        }
		#endregion

		#region 이벤트
		/// <summary>
		/// 이벤트 초기화
		/// </summary>
		public void InitializeEvent()
		{
            smartTabControl1.SelectedPageChanging += SmartTabControl1_SelectedPageChanging;
            grdOperation.View.FocusedRowChanged += grdOperation_FocusedRowChanged;
           
            // 공정자원 그리드
            grdResource.View.AddingNewRow += grdResource_AddingNewRow;
            grdResource.ToolbarDeletingRow += grdResource_ToolbarDeletingRow;
            grdResource.View.CellValueChanged += grdResource_CellValueChanged;
            grdResource.View.ShowingEditor += GrdResourceView_ShowingEditor;

            grdDurable.View.AddingNewRow += grdDurableView_AddingNewRow;
            grdDurable.View.ShowingEditor += grdDurableView_ShowingEditor;

            this.treeRouting.FocusedNodeChanged += TreeRouting_FocusedNodeChanged;

        }

        private void grdResource_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grdResource.View.CellValueChanged -= grdResource_CellValueChanged;

            if(e.Column.FieldName.Equals("ISPRIMARY"))
            {
                DataTable dt = (DataTable)grdResource.DataSource;

               int count = dt.AsEnumerable().Count(s => s["ISPRIMARY"].Equals("Y"));

                if (e.Value.ToString().Equals("Y") && count > 1)
                {
                    ShowMessage("DuplicatePrimaryResource");

                    this.grdResource.View.SetFocusedRowCellValue("ISPRIMARY", "N");
                }
            }
            grdResource.View.CellValueChanged += grdResource_CellValueChanged;
        }
     
        private void SmartTabControl1_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            DataTable dtChange = new DataTable();

            if (e.PrevPage.Tag.ToString().ToUpper() == "RESOURCE")
            {
                try
                {
                    dtChange = this.grdResource.GetChangedRows();

                }
                catch
                { }
                if (dtChange.Rows.Count > 0)
                {
                    DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                    switch (result)
                    {
                        case DialogResult.Yes:

                            if (e.Page.Tag.ToString().ToUpper() == "DURABLE")
                                grdDurable_View();
                            else if (e.Page.Tag.ToString().ToUpper() == "PROCESSATTRIBUTEVALUE")
                            {
                                grdProcessAttributeValue_View();
                            }
                            break;
                        case DialogResult.No:
                            e.Cancel = true;
                            break;
                    }
                }
                else
                {
                    if (e.Page.Tag.ToString().ToUpper() == "DURABLE")
                        grdDurable_View();
                    else if (e.Page.Tag.ToString().ToUpper() == "PROCESSATTRIBUTEVALUE")
                    {
                        grdProcessAttributeValue_View();
                    }
                }
            }
            else if (e.PrevPage.Tag.ToString().ToUpper() == "DURABLE")
            {
                try
                {
                    dtChange = this.grdDurable.GetChangedRows();
                }
                catch
                { }

                if (dtChange.Rows.Count > 0)
                {
                    DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                    switch (result)
                    {
                        case DialogResult.Yes:
                            if (e.Page.Tag.ToString().ToUpper() == "RESOURCE")
                                grdResource_View();
                            else if (e.Page.Tag.ToString().ToUpper() == "PROCESSATTRIBUTEVALUE")
                            {
                                grdProcessAttributeValue_View();
                            }
                            break;
                        case DialogResult.No:
                            e.Cancel = true;
                            break;
                    }
                }
                else
                {
                    if (e.Page.Tag.ToString().ToUpper() == "RESOURCE")
                        grdResource_View();
                    else if (e.Page.Tag.ToString().ToUpper() == "PROCESSATTRIBUTEVALUE")
                    {
                        grdProcessAttributeValue_View();
                    }

                }
            }
            else if (e.PrevPage.Tag.ToString().ToUpper() == "PROCESSATTRIBUTEVALUE")
            {
                try
                {
                    dtChange = this.grdProcessAttributeValue.GetChangedRows();
                }
                catch
                { }

                if (dtChange.Rows.Count > 0)
                {
                    DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                    switch (result)
                    {
                        case DialogResult.Yes:
                            if (e.Page.Tag.ToString().ToUpper() == "ProcessAttributeValue".ToUpper())
                                grdResource_View();
                            else if (e.Page.Tag.ToString().ToUpper() == "PROCESSATTRIBUTEVALUE")
                            {
                                grdProcessAttributeValue_View();
                            }
                            break;
                        case DialogResult.No:
                            e.Cancel = true;
                            break;
                    }
                }
                else
                {
                    if (e.Page.Tag.ToString().ToUpper() == "ProcessAttributeValue".ToUpper())
                        grdResource_View();
                    else if (e.Page.Tag.ToString().ToUpper() == "PROCESSATTRIBUTEVALUE")
                    {
                        grdProcessAttributeValue_View();
                    }

                }
            }

        }

        private void TreeRouting_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

            if (e.Node["MASTERDATACLASSID"].ToString() != "Product" && e.Node["MASTERDATACLASSID"].ToString() != "SubAssembly")
            {
                this.treeRouting.SetFocusedNode(e.OldNode);
                return;
            }

            this.treeRouting.FocusedNodeChanged -= TreeRouting_FocusedNodeChanged;

            string assemblyItemId = e.Node["ASSEMBLYITEMID"].ToString();
            string assemblyItemVersion = e.Node["ASSEMBLYITEMVERSION"].ToString();
            string plantId = e.Node["PLANTID"].ToString();

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", plantId);
            values.Add("P_PRODUCTDEFID", assemblyItemId);
            values.Add("P_PRODUCTDEFVERSION", assemblyItemVersion);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtChange = new DataTable();

            if (smartTabControl1.SelectedTabPageIndex == 0)
            {
                try
                {
                    dtChange = this.grdResource.GetChangedRows();

                }
                catch
                { }

                if (dtChange.Rows.Count > 0)
                {
                    DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                    switch (result)
                    {
                        case DialogResult.Yes:
                            SearchProductInfo(values);
                            break;
                        case DialogResult.No:
                            this.treeRouting.SetFocusedNode(e.OldNode);
                            break;
                    }
                }
                else
                {
                    SearchProductInfo(values);
                }

            }
            else if (smartTabControl1.SelectedTabPageIndex == 1)
            {
                try
                {
                    dtChange = this.grdDurable.GetChangedRows();
                }
                catch
                { }

                if (dtChange.Rows.Count > 0)
                {
                    DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                    switch (result)
                    {
                        case DialogResult.Yes:
                            SearchProductInfo(values);
                            break;
                        case DialogResult.No:
                            this.treeRouting.SetFocusedNode(e.OldNode);
                            break;
                    }
                }
                else
                    SearchProductInfo(values);

            }
            else if (smartTabControl1.SelectedTabPageIndex == 2)
            {
            }


      

            this.treeRouting.FocusedNodeChanged += TreeRouting_FocusedNodeChanged;
        }


        private void GrdResourceView_ShowingEditor(object sender, CancelEventArgs e)
        {
            string currentColumnName = this.grdResource.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("RESOURCEID"))
            {
                if (!this.grdResource.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                    e.Cancel = true;
            }


        }

        private void grdDurableView_ShowingEditor(object sender, CancelEventArgs e)
        {

            string currentColumnName = this.grdDurable.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("TOOLCODE"))
            {
                if (!this.grdDurable.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                    e.Cancel = true;
            }


        }

        private void grdDurableView_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            if (this.grdOperation.View.FocusedRowHandle < 0)
            {
                args.IsCancel = true;

                return;
            }

            DataRow focusedRow = this.grdOperation.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                {
                    ShowMessage("NoSelectDataSaveAndProceed");


                    args.IsCancel = true;

                    return;
                }
            }
            else
            {
                args.IsCancel = true;

                return;
            }

            args.NewRow["OPERATIONID"] = focusedRow["OPERATIONID"];
            args.NewRow["EQUIPMENTID"] = "*";
            args.NewRow["RESOURCECLASSID"] = "*";
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
         //       args.NewRow["PROCESSSEGMENTCLASSID"] = focusedRow["PROCESSSEGMENTCLASSID"];
                args.NewRow["PRODUCTDEFID"] = txtProductDEFId.Text;
                args.NewRow["PRODUCTDEFVERSION"] = txtProductDEFVersion.Text;
                args.NewRow["PRODUCTDEFNAME"] = txtProductDEFName.Text;
            }
        }

        private void grdResource_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DataRow row = grdResource.View.GetFocusedDataRow();
            DataTable dt = (DataTable)grdResource.DataSource;

            // 주자원을 삭제할 방법이 없음!! 2020-03-03 이상진
            //if (row["ISPRIMARY"].ToString() == "Y" && dt.Select("ISPRIMARY <> 'Y'").Length !=0)
            //{
            //    e.Cancel = true;
            //}
        }


       

        private void grdResource_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (this.grdOperation.View.FocusedRowHandle < 0)
            {
                args.IsCancel = true;
                return;
            }
            DataRow row = this.grdOperation.View.GetFocusedDataRow();

            if (row != null)
            {
                args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
                args.NewRow["PLANTID"] = row["PLANTID"];
                args.NewRow["OPERATIONID"] = row["OPERATIONID"];
                args.NewRow["DURABLETYPE"] = "Resource";
                args.NewRow["RESOURCESEQUENCE"] = 0;

                args.NewRow["MAINPRODUCTID"] = row["MAINPRODUCTID"];
                args.NewRow["MAINPRODUCTVERSION"] = row["MAINPRODUCTVERSION"];

                DataRow rowOperation = grdOperation.View.GetFocusedDataRow();
                var values = Conditions.GetValues();
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("OPERATIONID", rowOperation["OPERATIONID"].ToString());
                Param.Add("ENTERPRISEID", rowOperation["ENTERPRISEID"].ToString());
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable dtISPRIMARYRESOURCE = this.grdOperation.DataSource as DataTable;
                if (dtISPRIMARYRESOURCE != null)
                {
                    if (dtISPRIMARYRESOURCE.Rows.Count != 0)
                    {
                        args.NewRow["ISPRIMARY"] = "N";
                    }
                    else
                    {
                        args.NewRow["ISPRIMARY"] = "Y";
                    }
                }
                else
                {
                    args.NewRow["ISPRIMARY"] = "Y";
                }


                // 순번 맥스 + 1
                object obj = this.grdResource.DataSource;
                DataTable dt = ((DataTable)obj).Copy();
                DataRow[] rowR = dt.Select("1=1", "RESOURCESEQUENCE DESC");
                args.NewRow["RESOURCESEQUENCE"] = int.Parse(rowR[0]["RESOURCESEQUENCE"].ToString()) + 10;
                args.NewRow["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"];


                args.NewRow["VALIDSTATE"] = "Valid";
            }
         
        }

        // 공정 로우 체인지
        private void grdOperation_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            grdOperation.View.FocusedRowChanged -= grdOperation_FocusedRowChanged;

            DataTable dtChange = new DataTable();


            if (smartTabControl1.SelectedTabPageIndex == 0)
            {
                try
                {
                    dtChange = this.grdResource.GetChangedRows();

                }
                catch
                { }

                if (dtChange.Rows.Count > 0)
                {
                    DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                    switch (result)
                    {
                        case DialogResult.Yes:
                            grdResource_View();
                            break;
                        case DialogResult.No:
                            this.grdOperation.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                            break;
                    }
                }
                else
                    grdResource_View();

            }
            else if (smartTabControl1.SelectedTabPageIndex == 1)
            {
                try
                {
                    dtChange = this.grdDurable.GetChangedRows();

                }
                catch
                { }

                if (dtChange.Rows.Count > 0)
                {
                    DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                    switch (result)
                    {
                        case DialogResult.Yes:
                            grdDurable_View();
                            break;
                        case DialogResult.No:
                            this.grdOperation.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                            break;
                    }
                }
                else
                    grdDurable_View();


            }
            else if (smartTabControl1.SelectedTabPageIndex == 2)
            {

                try
                {
                    dtChange = this.grdProcessAttributeValue.GetChangedRows();

                }
                catch
                { }

                if (dtChange.Rows.Count > 0)
                {
                    DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                    switch (result)
                    {
                        case DialogResult.Yes:
                            grdProcessAttributeValue_View();
                            break;
                        case DialogResult.No:
                            this.grdOperation.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                            break;
                    }
                }
                else
                    grdProcessAttributeValue_View();

            }

            grdOperation.View.FocusedRowChanged += grdOperation_FocusedRowChanged;

        }

        #endregion



        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
            try
            {
                var values = Conditions.GetValues();
                savedFocuseOperationRowHandle = grdOperation.View.FocusedRowHandle;
                DataRow rowOperation = grdOperation.View.GetFocusedDataRow();
                focus = grdOperation.View.FocusedRowHandle;

                DataTable dtResource = new DataTable();
                DataTable dtTool = new DataTable();

            
           
           
                //// 자원 등록 /////////////////////////////////////////////////////////////////////////
                if (smartTabControl1.SelectedTabPageIndex == 0)
                {
                    try
                    {
                        dtResource = grdResource.GetChangedRows();
                    }
                    catch { }



                    DataTable dtBillOfResource = new DataTable();
                    dtBillOfResource.Columns.Add("PRODUCTDEFID");
                    dtBillOfResource.Columns.Add("PRODUCTDEFVERSION");
                    dtBillOfResource.Columns.Add("PROCESSDEFID");
                    dtBillOfResource.Columns.Add("PROCESSDEFVERSION");
                    dtBillOfResource.Columns.Add("PROCESSSEGMENTID");
                    dtBillOfResource.Columns.Add("PROCESSSEGMENTVERSION");
                    dtBillOfResource.Columns.Add("EQUIPMENTID");
                    dtBillOfResource.Columns.Add("DURABLETYPE");
                    dtBillOfResource.Columns.Add("RESOURCECLASSID");
                    dtBillOfResource.Columns.Add("RESOURCEID");
                    dtBillOfResource.Columns.Add("RESOURCEVERSION");

                    dtBillOfResource.Columns.Add("SEQUENCE");
                    dtBillOfResource.Columns.Add("VALIDSTATE");
                    dtBillOfResource.Columns.Add("ENTERPRISEID");
                    dtBillOfResource.Columns.Add("PLANTID");
                    dtBillOfResource.Columns.Add("ISPRIMARYRESOURCE");
                    dtBillOfResource.Columns.Add("_STATE_");

                    // sf_BillOfResource 트렌젝션
                    foreach (DataRow rowResource in dtResource.Rows)
                    {

                        if (rowResource["_STATE_"].ToString() == "added")
                        {
                            Dictionary<string, object> paramdt = new Dictionary<string, object>();
                            DataTable dtDate = SqlExecuter.Query("GetItemId", "10001", paramdt);
                            string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);
                            GetNumber number = new GetNumber();
                            rowResource["OPERATIONRESOURCEID"] = number.GetStdNumber("OperationResource", "ORS" + sdate);

                        }

                        DataRow rowBr = dtBillOfResource.NewRow();
                        rowBr["PRODUCTDEFID"] = txtProductDEFId.EditValue;
                        rowBr["PRODUCTDEFVERSION"] = txtProductDEFVersion.EditValue;
                        rowBr["PROCESSDEFID"] = txtProductDEFId.EditValue;
                        rowBr["PROCESSDEFVERSION"] = txtProductDEFVersion.EditValue;
                        rowBr["PROCESSSEGMENTID"] = rowOperation["PROCESSSEGMENTID"];
                        rowBr["PROCESSSEGMENTVERSION"] = "*";
                        rowBr["EQUIPMENTID"] = "*";
                        rowBr["DURABLETYPE"] = "Resource";
                        rowBr["RESOURCECLASSID"] = "*";
                        rowBr["RESOURCEID"] = rowResource["RESOURCEID"];
                        rowBr["RESOURCEVERSION"] = "*";

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("ENTERPRISEID", rowOperation["ENTERPRISEID"]);
                        param.Add("PRODUCTDEFID", txtProductDEFId.EditValue);
                        param.Add("PRODUCTDEFVERSION", txtProductDEFVersion.Text);
                        param.Add("PROCESSDEFID", txtProductDEFId.Text);
                        param.Add("PROCESSDEFVERSION", txtProductDEFVersion.EditValue);
                        param.Add("PROCESSSEGMENTID", rowOperation["PROCESSSEGMENTID"]);
                        param.Add("RESOURCEID", rowResource["RESOURCEID"]);
                        param.Add("RESOURCETYPE", "Resource");
                        DataTable dtBillofResourceChk = SqlExecuter.Query("GetBillofResourceChk", "10001", param);

                        if (dtBillofResourceChk != null)
                        {
                            if (dtBillofResourceChk.Rows.Count != 0)
                            {
                                rowBr["SEQUENCE"] = dtBillofResourceChk.Rows[0]["SEQUENCE"];
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
                        rowBr["ISPRIMARYRESOURCE"] = rowResource["ISPRIMARY"];
                        rowBr["_STATE_"] = rowResource["_STATE_"];
                        rowBr["ENTERPRISEID"] = rowOperation["ENTERPRISEID"];
                        rowBr["PLANTID"] = rowOperation["PLANTID"];
                        dtBillOfResource.Rows.Add(rowBr);
                    }


                    DataSet dSchanged = new DataSet();
                    dtResource.TableName = "operationresource";
                    dSchanged.Tables.Add(dtResource);
                    dtBillOfResource.TableName = "billOfResource";
                    dSchanged.Tables.Add(dtBillOfResource);
                    ExecuteRule("OperationResource", dSchanged);



                }
                else if (smartTabControl1.SelectedTabPageIndex == 1)
                {
                    try
                    {
                        dtTool = grdDurable.GetChangedRows();
                    }
                    catch { }

                    DataTable dtChangeData = null;
                    DataTable dtDurable = this.grdDurable.DataSource as DataTable;
                    var duplicateDurable = from r in dtDurable.AsEnumerable()
                                           group r by new
                                           {
                                               TOOLCODE = r.Field<string>("TOOLCODE"),
                                               TOOLNAME = r.Field<string>("TOOLNAME")
                                           } into g
                                           where g.Count() > 1
                                           select g;

                    if (duplicateDurable.Count() > 0)
                        throw MessageException.Create("DuplicationDurableID", duplicateDurable.ElementAt(0).Key.TOOLNAME);


                    dtChangeData = this.grdDurable.GetChangedRows();

                    DataTable dtSendData = new DataTable();
                    string key = string.Empty;

                    key = "billOfDurableList";

                    dtSendData.Columns.Add("OPERATIONID");
                    dtSendData.Columns.Add("PRODUCTDEFID");
                    dtSendData.Columns.Add("PRODUCTDEFVERSION");
                    dtSendData.Columns.Add("PROCESSDEFID");
                    dtSendData.Columns.Add("PROCESSDEFVERSION");
                    dtSendData.Columns.Add("PROCESSSEGMENTID");
                    dtSendData.Columns.Add("PROCESSSEGMENTVERSION");
                    dtSendData.Columns.Add("RESOURCETYPE");
                    dtSendData.Columns.Add("DURABLETYPE");
                    dtSendData.Columns.Add("SEQUENCE");
                    dtSendData.Columns.Add("RESOURCEID");
                    dtSendData.Columns.Add("RESOURCEVERSION");
                    dtSendData.Columns.Add("EQUIPMENTID");
                    dtSendData.Columns.Add("RESOURCECLASSID");
                    dtSendData.Columns.Add("ISPRIMARYRESOURCE");
                    dtSendData.Columns.Add("ENTERPRISEID");
                    dtSendData.Columns.Add("PLANTID");
                    dtSendData.Columns.Add("DESCRIPTION");
                    dtSendData.Columns.Add("VALIDSTATE");
                    dtSendData.Columns.Add("FILMUSELAYER1");
                    dtSendData.Columns.Add("_STATE_");
                    dtSendData.Columns.Add("WRAPTYPE");

                    if (dtChangeData.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtChangeData.Rows)
                        {

                            dtSendData.Rows.Add(new object[]
                            {
                                this.grdOperation.View.GetFocusedDataRow()["OPERATIONID"].ToString()
                                , txtProductDEFId.Text
                                , txtProductDEFVersion.Text
                                , txtProductDEFId.Text
                                , txtProductDEFVersion.Text
                                , this.grdOperation.View.GetFocusedDataRow()["PROCESSSEGMENTID"].ToString()
                                , "*"
                                , "Durable"
                                , dr["DURABLETYPE"].ToString()
                                ,  string.IsNullOrWhiteSpace(dr["SEQUENCE"].ToString()) == true ? "0" : dr["SEQUENCE"].ToString()//SEQUENCE UI에서 가지고있는 인덱스를 찾아서 넣어야 하는지 확인 필요.
                                , dr["TOOLCODE"].ToString()
                                , dr["TOOLVERSION"].ToString()
                                , "*"
                                , dr["RESOURCECLASSID"].ToString()
                                , "Y"
                                , UserInfo.Current.Enterprise
                                , this.grdOperation.View.GetFocusedDataRow()["PLANTID"].ToString()
                                , dr["DESCRIPTION"].ToString()
                                , "Valid"
                                , dr["FILMUSELAYER1"].ToString()
                                ,dr["_STATE_"].ToString()
                                ,dr["WRAPTYPE"]
                            });

                        }



                    MessageWorker worker = new MessageWorker("RoutingMgnt");
                        worker.SetBody(new MessageBody()
                        {
                            {  "billOfDurableList", dtSendData}
                        });

                        worker.Execute();
                    }
                }
                else//외주단가등록
                {
                    var dtChangeData = this.grdProcessAttributeValue.GetChangedRows();

                    DataTable dtSendData = new DataTable();
                    string key = string.Empty;

                    key = "billOfDurableList";

                    dtSendData.Columns.Add("OPERATIONID");
                    dtSendData.Columns.Add("ATTRIBUTECODE");
                    dtSendData.Columns.Add("ATTRIBUTEVALUE");
                    dtSendData.Columns.Add("DESCRIPTION");
                    dtSendData.Columns.Add("_STATE_");

                    if (dtChangeData.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtChangeData.Rows)
                        {
                            //var seq = grdProcessAttributeValue.View.GetRowHandleByValue("ATTRIBUTECODE",
                            //    dr["ATTRIBUTECODE"]);

                            dtSendData.Rows.Add(new object[]
                            {
                                this.grdOperation.View.GetFocusedDataRow()["OPERATIONID"].ToString()
                                , dr["ATTRIBUTECODE"].ToString()
                                , dr["ATTRIBUTEVALUE1"].ToString()
                                , string.Empty
                                , dr["_STATE_"].ToString()
                            });

                        }

                        MessageWorker worker = new MessageWorker("RoutingMgnt");
                        worker.SetBody(new MessageBody()
                        {
                            {  "OperationOspAttribute", dtSendData}
                        });

                        worker.Execute();
                    }
                }
                this.focusedNodeIndexList = new List<int>();

                if (this.treeRouting.FocusedNode != null)
                    RecursiveFindNodeIndex(this.treeRouting.FocusedNode);
                else
                    this.focusedNodeIndexList = new List<int>();

            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
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

            ClearData();


            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //treeRouting.DataSource = null;

            SearchProductInfo(values);


            InitializeTree();
            this.focusedNodeIndexList = new List<int>();

            grdOperation.View.FocusedRowHandle = savedFocuseOperationRowHandle;

            switch (smartTabControl1.SelectedTabPage.Tag)
            {
                case "Resource":
                    grdResource_View();
                    break;
                case "Durable":
                    grdDurable_View();
                    break;
                case "ProcessAttributeValue":
                    grdProcessAttributeValue_View();
                    break;
            }

        }

        #region 조회조건 영역

        /// <summary>
        /// 조회조건 영역 초기화 시작
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목
            InitializeCondition_ProductPopup();
        }

        #endregion

        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
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

        }

        private void ProductDefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Empty;
                Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
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

            DataTable dtResource = new DataTable();
            DataTable dtDurable = new DataTable();

            DataTable dtAttribute = new DataTable();
        
            try
            {
                dtResource = grdResource.GetChangedRows();
            }
            catch { }
            try
            {
                dtDurable = grdDurable.GetChangedRows();
            }
            catch { }

            try
            {
                dtAttribute = grdProcessAttributeValue.GetChangedRows();
            }
            catch { }

            if (dtResource.Rows.Count == 0 && dtDurable.Rows.Count == 0 && dtAttribute.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
            else
            {

                switch (this.smartTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        this.grdResource.View.CheckValidation();

                        dtResource = this.grdResource.DataSource as DataTable;
                        if (dtResource.Rows.Count > 0)
                        {
                            var duplicateResource = from r in dtResource.AsEnumerable()
                                                    group r by new
                                                    {
                                                        RESOURCEID = r.Field<string>("RESOURCEID"),
                                                        RESOURCENAME = r.Field<string>("RESOURCENAME")
                                                    } into g
                                                    where g.Count() > 1
                                                    select g;

                            if (duplicateResource.Count() > 0)
                                throw MessageException.Create("DuplicationResourceID", duplicateResource.ElementAt(0).Key.RESOURCENAME);
                        }

                        DataTable dtISPRIMARYRESOURCE = (DataTable)grdResource.DataSource;


                        if (dtISPRIMARYRESOURCE.Rows.Count > 0)
                        {
                            if (dtISPRIMARYRESOURCE.Select("ISPRIMARY = 'Y'").Length != 1)
                            {
                                throw MessageException.Create("IsprimaryResource");
                            }
                        }

                        break;
                    case 1:
                        //공정스펙
                        this.grdDurable.View.CheckValidation();


                        dtDurable = this.grdDurable.DataSource as DataTable;
                        if (dtDurable.Rows.Count > 0)
                        {
                            var duplicateDurable = from r in dtDurable.AsEnumerable()
                                                   group r by new
                                                   {
                                                       TOOLCODE = r.Field<string>("TOOLCODE"),
                                                       TOOLNAME = r.Field<string>("TOOLNAME")
                                                   } into g
                                                   where g.Count() > 1
                                                   select g;

                            if (duplicateDurable.Count() > 0)
                                throw MessageException.Create("DuplicationDurableID", duplicateDurable.ElementAt(0).Key.TOOLNAME);

                        }

                        break;
                    case 2:
                        //치공구
                        this.grdProcessAttributeValue.View.CheckValidation();
                        dtAttribute = this.grdProcessAttributeValue.DataSource as DataTable;

                        if (dtAttribute.Rows.Count > 0)
                        {
                            var duplicateAttribute = from r in dtAttribute.AsEnumerable()
                                                     group r by new
                                                     {
                                                         RESOURCEID = r.Field<string>("ATTRIBUTECODE")
                                                     } into g
                                                     where g.Count() > 1
                                                     select g;

                            if (duplicateAttribute.Count() > 0)
                                throw MessageException.Create("DuplicationItem", duplicateAttribute.ElementAt(0).Key.RESOURCEID);
                        }
                        break;
              
                }
            
            }
   

        }
        #endregion

        #region private Fuction


        /// <summary>
        /// Area Tree 초기화
        /// </summary>
        private void InitializeTree()
        {
            //dt.Clear(); 

            this.treeRouting.DataSource = null;

            this.treeRouting.SetResultCount(1);
            this.treeRouting.SetIsReadOnly();
            //this.treeRouting.SetMember("DISPLAYNAME", "BOMID", "PARENTBOMID");
            this.treeRouting.SetMember("DISPLAYNAME", "BOMSEQUENCE", "PARENTBOMSEQUENCE");

            this.treeRouting.SetSortColumn("USERSEQUENCE");

            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            DataTable dt = SqlExecuter.Query("GetBOMTree", "10001", values);

            if (dt != null)
            {

                this.treeRouting.DataSource = dt;
                this.treeRouting.PopulateColumns();

                this.treeRouting.ExpandToLevel(0);


                if (this.focusedNodeIndexList != null && this.treeRouting.Nodes.Count > 0)
                {

                    TreeListNode focusedNode = this.treeRouting.Nodes[0];

                    for (int i = 1; i < this.focusedNodeIndexList.Count; i++)
                    {
                        if (focusedNode.Nodes.Count >= this.focusedNodeIndexList[i])
                        {
                            focusedNode = focusedNode.Nodes[focusedNodeIndexList[i]];
                        }
                    }

                    this.treeRouting.FocusedNode = focusedNode;

                }
            }
        }

        private void SearchProductInfo(Dictionary<string, object> values)
        {
        
            ClearData();
            
            DataTable dtProductDEF = SqlExecuter.Query("GetProductDEFInfo", "10001", values);
            if (dtProductDEF.Rows.Count < 1) //
            {
                // 조회할 데이터가 없습니다.
                //throw MessageException.Create("NoSelectData");
                return;
            }

            this.txtpnlx.Text = dtProductDEF.Rows[0]["PNLSIZEXAXIS"].ToString();
            this.txtpnly.Text = dtProductDEF.Rows[0]["PNLSIZEYAXIS"].ToString();

            this.txtCutomerName.Text = dtProductDEF.Rows[0]["CUSTOMERNAME"].ToString();
            this.txtProductDEFId.Text = dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString();
            this.txtProductDEFVersion.Text = dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString();
            //this.spCompletionWareHouse.SetValue(this.dtProductDEF.Rows[0]["COMPLETIONWAREHOUSEID"].ToString());
            //this.spCompletionWareHouse.Text = this.dtProductDEF.Rows[0]["WAREHOUSENAME"].ToString();
            this.txtProductDEFName.Text = dtProductDEF.Rows[0]["PRODUCTDEFNAME"].ToString();
            this.txtWorkType.Text = dtProductDEF.Rows[0]["JOBTYPENAME"].ToString();
            this.txtProductionType.Text = dtProductDEF.Rows[0]["PRODUCTIONTYPENAME"].ToString();
            this.scRTRSheet.EditValue = string.IsNullOrWhiteSpace(dtProductDEF.Rows[0]["RTRSHT"].ToString()) ? "SHT" : dtProductDEF.Rows[0]["RTRSHT"].ToString();

           
            //this.txtCutomerName.Text = string.IsNullOrEmpty(dtProductDEF.Rows[0]["CUSTOMERNAME"].ToString())
            //    ? string.Empty
            //    : dtProductDEF.Rows[0]["CUSTOMERNAME"].ToString();
            //this.txtProductDEFId.Text = string.IsNullOrEmpty(dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString())
            //    ? string.Empty
            //    : dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString();
            //this.txtProductDEFVersion.Text = string.IsNullOrEmpty(dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString())
            //    ? string.Empty
            //    : dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString();
            //this.txtCompletionWareHouse.Text =
            //    string.IsNullOrEmpty(dtProductDEF.Rows[0]["COMPLETIONWAREHOUSEID"].ToString())
            //        ? string.Empty
            //        : dtProductDEF.Rows[0]["COMPLETIONWAREHOUSEID"].ToString(); 
            //this.txtProductDEFName.Text = string.IsNullOrEmpty(dtProductDEF.Rows[0]["PRODUCTDEFNAME"].ToString())
            //    ? string.Empty
            //    : dtProductDEF.Rows[0]["PRODUCTDEFNAME"].ToString();
            //this.txtWorkType.Text = string.IsNullOrEmpty(dtProductDEF.Rows[0]["JOBTYPENAME"].ToString())
            //    ? string.Empty
            //    : dtProductDEF.Rows[0]["JOBTYPENAME"].ToString();
            //this.txtProductionType.Text = string.IsNullOrEmpty(dtProductDEF.Rows[0]["PRODUCTIONTYPENAME"].ToString())
            //    ? string.Empty
            //    : dtProductDEF.Rows[0]["PRODUCTIONTYPENAME"].ToString();
            //this.scRTRSheet.EditValue = string.IsNullOrWhiteSpace(dtProductDEF.Rows[0]["RTRSHT"].ToString()) ? "SHT" : dtProductDEF.Rows[0]["RTRSHT"].ToString();





            DataTable dtOperAtion = SqlExecuter.Query("GetOpeRationResource", "10002", values);
                       
            grdOperation.DataSource = dtOperAtion;

            if (smartTabControl1.SelectedTabPageIndex == 0)
            {
                grdResource_View();
            }
            else if (smartTabControl1.SelectedTabPageIndex == 1)
            {
                grdDurable_View();
            }
            else if (smartTabControl1.SelectedTabPageIndex == 2)
            {
            }
        }

        /// <summary>
        /// 품목 내용 클리어
        /// </summary>
        /// <returns></returns>
        private void ClearData()
        {

            this.txtpnlx.Text = string.Empty;
            this.txtpnly.Text = string.Empty;

            this.txtCutomerName.Text = string.Empty;
            this.txtProductDEFId.Text = string.Empty;
            this.txtProductDEFVersion.Text = string.Empty;
            this.txtProductDEFName.Text = string.Empty;
            this.txtWorkType.Text = string.Empty;
            this.txtProductionType.Text = string.Empty;

            grdOperation.DataSource = null;
            grdResource.DataSource = null;
            grdDurable.DataSource = null;
            grdProcessAttributeValue.DataSource = null;

        }
    

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
                foreach (DataColumn col in currentGridRow.Table.Columns)
                {
                    switch(col.ColumnName)
                    {
                        case "RESOURCENAME":
                            currentGridRow["RESOURCENAME"] = row["DURABLEDEFNAME"];
                            break;
                    }
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

        private void grdResource_View()
        {
            if (grdOperation.View.FocusedRowHandle < 0)
                return;
            //grdAlterResource.DataSource = null;
            //grdDff.DataSource = null;

            DataRow row = grdOperation.View.GetFocusedDataRow();
            var values = Conditions.GetValues();

            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("OPERATIONID", row["OPERATIONID"].ToString());
            Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdResource.DataSource = SqlExecuter.Query("GetOpeRationResource", "10001", Param);

        }

        private void grdDurable_View()
        {
            if (grdOperation.View.FocusedRowHandle < 0)
                return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            
            //치공구
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("OPERATIONID", this.grdOperation.View.GetFocusedDataRow()["OPERATIONID"].ToString());
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("PRODUCTDEFID", txtProductDEFId.Text);
            param.Add("PRODUCTDEFVERSION", txtProductDEFVersion.Text);
            SearchGrid(this.grdDurable, "GetRoutingDurableList", "10001", param);

        }

        private void grdProcessAttributeValue_View()
        {
            if (grdOperation.View.FocusedRowHandle < 0)
                return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            DataRow row = grdOperation.View.GetFocusedDataRow();

            //외주단가
            param.Add("OPERATIONID", row["OPERATIONID"].ToString());
            param.Add("OPERATIONSEQUENCE", row["OPERATIONSEQUENCE"].ToString());
            param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //param.Add("PLANTID", this.grdOperation.View.GetFocusedDataRow()["PLANTID"].ToString());
            //param.Add("ITEMID", txtProductDEFId.Text);
            //param.Add("ITEMVERSION", txtProductDEFVersion.Text);
            //param.Add("USERSEQUENCE", this.grdOperation.View.GetFocusedDataRow()["USERSEQUENCE"].ToString());
            //param.Add("PROCESSSEGMENTID", this.grdOperation.View.GetFocusedDataRow()["PROCESSSEGMENTID"].ToString());
            //param.Add("ATTRIBUTECLASS", "");

            SearchGrid(this.grdProcessAttributeValue, "GetOperationOspAttribute", "10001", param);

        }

        private void SearchGrid(SmartBandedGrid grid, string query, string version, Dictionary<string, object> param, params string[] addColumns)
        {

            grid.DataSource = null;

            DataTable dt = SqlExecuter.Query(query, version, param);

            foreach (string column in addColumns)
            {
                dt.Columns.Add(column);
            }
            grid.DataSource = dt;


        }
        private void RecursiveFindNodeIndex(TreeListNode tNode)
        {
            if (tNode.ParentNode != null)
                RecursiveFindNodeIndex(tNode.ParentNode);

            this.focusedNodeIndexList.Add(this.treeRouting.GetNodeIndex(tNode));
        }
        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationEquipmentClassIdPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["EQUIPMENTCLASSNAME"] = row["EQUIPMENTCLASSNAME"];

            }
            return result;
        }

     
        #endregion
    }
}
