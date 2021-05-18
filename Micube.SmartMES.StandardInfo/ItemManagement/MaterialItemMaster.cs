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
using DevExpress.XtraEditors.Mask;
using Micube.Framework.SmartControls.Validations;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;
using Micube.SmartMES.StandardInfo.Popup;
using Micube.SmartMES.Commons;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목등록(자재)
    /// 업  무  설  명  : 자재품목등록
    /// 생    성    자  :  윤성원
    /// 생    성    일  : 2019-06-28
    /// 수  정  이  력  : 
    /// </summary>
    /// 
    public partial class MaterialItemMaster : SmartConditionManualBaseForm
    {

        #region Local Variables
        ConditionItemSelectPopup sCustomer = new ConditionItemSelectPopup();
        #endregion

        #region 생성자
        public MaterialItemMaster()
        {
            InitializeComponent();
            InitializeEvent();

        }
        #endregion

        #region 컨텐츠 영역 초기화


        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //품목마스터 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdIMList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;//.CheckBoxSelect;
            grdIMList.View.OptionsSelection.MultiSelect = false;

            //ENTERPRISE
            grdIMList.View.AddTextBoxColumn("ENTERPRISEID", 50).SetIsHidden().SetDefault(UserInfo.Current.Enterprise);

            //SITE
            grdIMList.View.AddComboBoxColumn("PLANTID", 50, new SqlQuery("GetPlantList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                          .SetDefault(UserInfo.Current.Plant)
                          .SetIsHidden();
            //자재 코드
            grdIMList.View.AddTextBoxColumn("ITEMID", 100)
                          .SetLabel("CONSUMABLEDEFID")
                          .SetValidationIsRequired();

            //자재 명
            grdIMList.View.AddTextBoxColumn("ITEMNAME", 250)
                          .SetLabel("CONSUMABLEDEFNAME");
            //품목 단위
            grdIMList.View.AddComboBoxColumn("ITEMUOM", 60, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UNIT")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            //자재 유형
            grdIMList.View.AddComboBoxColumn("MATERIALTYPE", 60, new SqlQuery("GetmasterdataclassList", "10001", "ITEMOWNER=Material", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MASTERDATACLASSNAME", "MASTERDATACLASSID")
                .SetTextAlignment(TextAlignment.Center);

            //대분류 - 품목유형
            grdIMList.View.AddComboBoxColumn("TOPCLASSID", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialLargeClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("LARGECLASS");
            //.IsRequired = true;
            if (UserInfo.Current.Enterprise == "YOUNGPOONG")
            {
                //중분류 - 품목유형
                grdIMList.View.AddComboBoxColumn("MIDDLECLASSID", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialMiddleClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                    .SetLabel("MIDDLECLASS");
                //소분류 - 품목유형
                grdIMList.View.AddComboBoxColumn("MATERIALCLASS", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialSmallClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                    .SetLabel("SMALLCLASS");
            }
            if (UserInfo.Current.Enterprise == "INTERFLEX")
            {
                //중분류 - 품목유형
                grdIMList.View.AddComboBoxColumn("MIDDLECLASSID", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialMiddleClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                    .SetLabel("MIDDLECLASS")
                    .SetValidationIsRequired();
                //소분류 - 품목유형
                grdIMList.View.AddComboBoxColumn("MATERIALCLASS", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialSmallClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                    .SetLabel("SMALLCLASS")
                    .SetValidationIsRequired();


            }

            //입고 창고
            grdIMList.View.AddComboBoxColumn("WAERHOUSE", 100, new SqlQuery("GetWarehouseList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "WAREHOUSENAME", "WAREHOUSEID");
                //.SetMultiColumns(ComboBoxColumnShowType.All);

            //공급사
            grdIMList.View.AddComboBoxColumn("CONVENDORNAME", 60, new SqlQuery("GetVendorList", "10001", $"PLANTID={UserInfo.Current.Plant}"), "VENDORNAME", "VENDORID");
            //MAKER
            grdIMList.View.AddComboBoxColumn("MAKER", 60, new SqlQuery("GetVendorList", "10001", $"PLANTID={UserInfo.Current.Plant}"), "VENDORNAME", "VENDORID");


            //채번 팝업
            //InitializeGrd_IdClassListPopup();
            //채번 사이트
            //grdIMList.View.AddComboBoxColumn("IDSERIALSITE", 80, new SqlQuery("GetPlantList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
            //              .SetDefault(UserInfo.Current.Plant);
            //채번 그룹
            //grdIMList.View.AddTextBoxColumn("IDSERIALSITE", 80)
            //              //.SetIsReadOnly(true)
            //              .SetTextAlignment(TextAlignment.Right);
            //자재품목구분
            //grdIMList.View.AddComboBoxColumn("MATERIALCLASS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //세부구분
            //grdIMList.View.AddComboBoxColumn("SUBCLASS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SubClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //구매 단위
            //grdIMList.View.AddComboBoxColumn("UNITOFPURCHASING", 60, new SqlQuery("GetUOMList", "10001", "UOMTYPE=Currency", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFNAME", "UOMDEFID");

            //제조유무
            grdIMList.View.AddComboBoxColumn("MAKEBUYTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MakeBuyType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //재고실사
            grdIMList.View.AddComboBoxColumn("CYCLECOUNT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);//.SetEmptyItem("", "", true);
            //단종구분
            grdIMList.View.AddComboBoxColumn("ENDTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);//.SetEmptyItem("", "", true);
            //LOTCONTROL
            grdIMList.View.AddComboBoxColumn("LOTCONTROL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);//.SetEmptyItem("", "", true);

            //DESCRIPTION
            grdIMList.View.AddTextBoxColumn("DESCRIPTION", 120)
                .SetTextAlignment(TextAlignment.Left);

            //유효상태
            grdIMList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationKeyColumn();

            grdIMList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdIMList.View.AddDateEditColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdIMList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdIMList.View.AddDateEditColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdIMList.View.PopulateColumns();
        }

        private void InitializeGrd_IdClassListPopup()
        {
            //팝업 컬럼 설정
            var IdClassLis = grdIMList.View.AddSelectPopupColumn("IDCLASSNAME", 100, new SqlQuery("SelectIdDefList", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_PLANTID={UserInfo.Current.Plant}"))
                                                      .SetPopupLayout("IDCLASSINFOLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                                      .SetPopupResultCount(1)
                                                      .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                                      //.SetValidationKeyColumn()
                                                      .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                                      {
                                                          // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                          // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                                          foreach (DataRow row in selectedRows)
                                                          {
                                                              dataGridRow["IDSERIALSITE"] = row["PLANTID"].ToString();
                                                              dataGridRow["IDCLASSID"] = row["IDCLASSID"].ToString();
                                                              dataGridRow["IDCLASSNAME"] = row["IDCLASSNAME"].ToString();
                                                          }
                                                      });

            IdClassLis.Conditions.AddComboBox("P_PLANTID", new SqlQuery("GetPlantList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetLabel("IDSERIALSITE")
                .SetDefault(UserInfo.Current.Plant);

            //IdClassLis.GridColumns.AddTextBoxColumn("ENTERPRISEID", 80);
            IdClassLis.GridColumns.AddTextBoxColumn("PLANTID", 80);
            IdClassLis.GridColumns.AddTextBoxColumn("IDCLASSID", 100);
            IdClassLis.GridColumns.AddTextBoxColumn("IDCLASSNAME", 120);
            IdClassLis.GridColumns.AddTextBoxColumn("IDDEFID", 100);
            IdClassLis.GridColumns.AddTextBoxColumn("IDDEFNAME", 120);
        }

        /// <summary>
        /// 로드 하면서 조회
        /// </summary>
        void initloadSearch()
        {
            var values = Conditions.GetValues();

            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            DataTable dtIMList = SqlExecuter.Query("GetMaterialItemMaster", "10002", values);

            this.grdIMList.DataSource = dtIMList;
        }

        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeGridIdDefinitionManagement();
            initloadSearch();

        }

        #endregion

        #region 이벤트
        private void InitializeEvent()
        {
            grdIMList.View.CellValueChanging += grdIMList_CellValueChanging;

            new SetGridDeleteButonVisible(grdIMList);
        }

        #region 기타이벤트

        #endregion


        #region 그리드이벤트

        /// <summary>
        /// 자재 분류시 항목 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdIMList_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("LARGECLASS"))
            {
                // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
                var values = this.Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("PARENTCODEID", e.Value); // 품목 자재

                DataTable dtMDCList = SqlExecuter.Query("GetMatarialMiddleClassList", "10001", values);

                //if (dtMDCList.Rows.Count < 1) // 
                //{
                //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                //}

                RepositoryItemLookUpEdit edit = (RepositoryItemLookUpEdit)grdIMList.View.Columns["MIDDLECLASS"].ColumnEdit;
                edit.DataSource = dtMDCList;

                grdIMList.View.Columns["MIDDLECLASS"].ColumnEdit = edit;
            }

            if (e.Column.FieldName.Equals("MIDDLECLASS"))
            {
                DataRowView view = (DataRowView)grdIMList.View.GetRow(e.RowHandle);

                // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
                var values = this.Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("CODECLASSID", e.Value); // 품목 자재

                DataTable dtMDCList = SqlExecuter.Query("GetCodeList", "00001", values);

                //if (dtMDCList.Rows.Count < 1) // 
                //{
                //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                //}

                RepositoryItemLookUpEdit edit = (RepositoryItemLookUpEdit)grdIMList.View.Columns["MATERIALCLASS"].ColumnEdit;
                edit.DataSource = dtMDCList;

                grdIMList.View.Columns["MATERIALCLASS"].ColumnEdit = edit;
            }
        }

        private void View_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            object obj = grdIMList.DataSource;
            DataTable dt = (DataTable)obj;

            switch (grdIMList.View.FocusedColumn.FieldName)
            {
                case "S":
                    bool chk = bool.Parse(grdIMList.View.GetFocusedRowCellValue("S").ToString());

                    if (dt.Select("S = True").Length != 0 && chk == false)
                    {
                        grdIMList.View.CancelFixColumn();
                    }
                    break;
            }
        }

        private void grdIMList_DoubleClick(object sender, EventArgs e)
        {
            // 등록 팝업
            DataRow row = this.grdIMList.View.GetFocusedDataRow();
            MaterialItemSpecPopup pis = new MaterialItemSpecPopup(row["ITEMID"].ToString(), row["ITEMVERSION"].ToString(), row["MASTERDATACLASSID"].ToString(), row["ENTERPRISEID"].ToString());
            pis.ShowDialog();
        }

        private void grdMDCList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged();
        }

        private void FocusedRowChanged()
        {
            //if (grdMDCList.View.FocusedRowHandle < 0)
            //    return;

            //var values = Conditions.GetValues();

            //values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //DataRow row = this.grdMDCList.View.GetFocusedDataRow();
            //values.Add("MASTERDATACLASSID", row["MASTERDATACLASSID"].ToString());
            //DataTable dtIMList = SqlExecuter.Query("GetMaterialItemMaster", "10001", values);

            //this.grdIMList.DataSource = dtIMList;
        }
        #endregion


        #endregion

        #region 조회조건 영역
        /// <summary>
        /// 검색조건 초기화. 
        /// 조회조건 정보, 메뉴 - 조회조건 매핑 화면에 등록된 정보를 기준으로 구성됩니다.
        /// DB에 등록한 정보를 제외한 추가 조회조건 구성이 필요한 경우 사용합니다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //품목 상태
            //Conditions.AddComboBox("ITEMSTATUS", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetPosition(1); 

        }

        /// <summary>
        /// 검색조건 팝업
        /// </summary>
        private void InitializeCondition_Popup()
        {
        }

        #endregion

        #region 툴바

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Replacement"))
            {

                DataRow drNew = grdIMList.View.GetFocusedDataRow();

                //if (dtNew != null)
                //{
                //    if (dtNew.Rows.Count != 0)
                //    {
                //        MaterialItemReplicePopup itemReplice = new MaterialItemReplicePopup(dtNew);
                //        itemReplice.ShowDialog();
                //    }
                //    else
                //    {
                //        ShowMessage("PopupNoCheck");
                //    }
                //}
                //else
                //{
                //    ShowMessage("PopupNoCheck");
                //}

                if (drNew != null)
                {
                    MaterialItemReplicePopup itemReplice = new MaterialItemReplicePopup(drNew);
                    itemReplice.StartPosition = FormStartPosition.CenterParent;
                    itemReplice.ShowDialog();
                }
                else
                {
                    ShowMessage("PopupNoCheck");
                }

                //MaterialItemCopyPopup itemReplice = new MaterialItemCopyPopup();
                //itemReplice.ShowDialog();

            }
            else if (btn.Name.ToString().Equals("ProductSpec"))
            {
                // 등록 팝업
                DataRow row = this.grdIMList.View.GetFocusedDataRow();
                MaterialSpecPopup pis = new MaterialSpecPopup(row["ITEMID"].ToString(), row["ITEMNAME"].ToString(), row["ITEMVERSION"].ToString(), row["MATERIALTYPE"].ToString(), row["ENTERPRISEID"].ToString());
                pis.StartPosition = FormStartPosition.CenterParent;
                pis.ShowDialog();

            }
			else if (btn.Name.ToString().Equals("AttachFile"))
			{
				DataRow row = this.grdIMList.View.GetFocusedDataRow();
				ItemMasterfilePopup pis = new ItemMasterfilePopup(row["ITEMID"].ToString(), row["ITEMVERSION"].ToString(), "ItemMaster", "ItemMasterMgnt/MaterialItemMaster");
				pis.ShowDialog();

			}
			#region 사용 안함
			//else if (btn.Name.ToString().Equals("AttachFile"))
			//{
			//    DataRow row = this.grdIMList.View.GetFocusedDataRow();
			//    ItemMasterfilePopup pis = new ItemMasterfilePopup(row["ITEMID"].ToString(), row["ITEMVERSION"].ToString(), "ItemMaster", "ItemMasterMgnt / ItemMaster");
			//    pis.ShowDialog();

			//}
			//else if (btn.Name.ToString().Equals("Copy"))
			//{
			//    MaterialItemCopyPopup itemReplice = new MaterialItemCopyPopup();
			//    itemReplice.ShowDialog();

			//}
			#endregion
		}

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            //DataTable dtChanged = grdIMList.GetChangedRows();

            //if(dtChanged != null)
            //    ExecuteRule("SaveMaterialItemMaster", dtChanged);

            DataTable changed = new DataTable();
            DataTable dtItemserialI = new DataTable();

            // 자재 그룹 정보
            DataTable dtConsumableclass = new DataTable();
            dtConsumableclass.Columns.Add("CONSUMABLECLASSID");
            dtConsumableclass.Columns.Add("CONSUMABLECLASSNAME");
            dtConsumableclass.Columns.Add("DESCRIPTION");
            dtConsumableclass.Columns.Add("ENTERPRISEID");
            dtConsumableclass.Columns.Add("CONSUMABLECLASSTYPE");
            dtConsumableclass.Columns.Add("VALIDSTATE");
            dtConsumableclass.Columns.Add("_STATE_");

            // 사양 스펙
            DataTable dtmaterialitemspec = new DataTable();
            dtmaterialitemspec.Columns.Add("MATERIALTYPE");
            dtmaterialitemspec.Columns.Add("ENTERPRISEID");
            dtmaterialitemspec.Columns.Add("ITEMID");           //품목코드
            dtmaterialitemspec.Columns.Add("ITEMVERSION");      //품목버전

            dtmaterialitemspec.Columns.Add("WAERHOUSE");        //입고 창고
            //dtmaterialitemspec.Columns.Add("IDSERIALSITE");       //채번
            dtmaterialitemspec.Columns.Add("CONVENDORNAME");    //공급사
            dtmaterialitemspec.Columns.Add("MAKER");            //제조사

            dtmaterialitemspec.Columns.Add("TOPCLASSID");
            dtmaterialitemspec.Columns.Add("MIDDLECLASSID");
            dtmaterialitemspec.Columns.Add("MATERIALCLASS");

            dtmaterialitemspec.Columns.Add("VALIDSTATE");
            dtmaterialitemspec.Columns.Add("_STATE_");

            // 자재 정의 정보
            DataTable dtconsumabledefinition = new DataTable();
            dtconsumabledefinition.Columns.Add("CONSUMABLEDEFID");
            dtconsumabledefinition.Columns.Add("CONSUMABLEDEFVERSION");
            dtconsumabledefinition.Columns.Add("CONSUMABLECLASSID");
            dtconsumabledefinition.Columns.Add("CONSUMABLEDEFNAME");
            dtconsumabledefinition.Columns.Add("ENTERPRISEID");
            dtconsumabledefinition.Columns.Add("CONSUMABLETYPE");
            dtconsumabledefinition.Columns.Add("UNIT");
            dtconsumabledefinition.Columns.Add("VALIDSTATE");
            dtconsumabledefinition.Columns.Add("DESCRIPTION");
            dtconsumabledefinition.Columns.Add("_STATE_");
            dtconsumabledefinition.Columns.Add("ISLOTMNG");

            changed = grdIMList.GetChangedRows();

            foreach (DataRow row in changed.Rows)
            {
                string rowState = row["_STATE_"].ToString();

                if (rowState == "added")
                {
                    //자재 그룹  정보 조회
                    Dictionary<string, object> paramConsum = new Dictionary<string, object>();
                    paramConsum.Add("CONSUMABLECLASSID", row["MATERIALTYPE"].ToString());//MASTERDATACLASSID
                    DataTable dtConsum = SqlExecuter.Query("GetConsumableclassList", "10001", paramConsum);

                    if (dtConsum != null)
                    {
                        if (dtConsum.Rows.Count == 0)
                        {
                            // 자재 그룹 정보  등록
                            DataRow rowCclNew = dtConsumableclass.NewRow();
                            rowCclNew["CONSUMABLECLASSID"] = row["MATERIALTYPE"].ToString();//row["MASTERDATACLASSID"].ToString();
                            rowCclNew["CONSUMABLECLASSNAME"] = "";// rowMDC["MASTERDATACLASSNAME"].ToString();
                            rowCclNew["DESCRIPTION"] = row["DESCRIPTION"].ToString();
                            rowCclNew["ENTERPRISEID"] = row["ENTERPRISEID"].ToString();
                            rowCclNew["CONSUMABLECLASSTYPE"] = row["MATERIALTYPE"];//row["MASTERDATACLASSID"].ToString();
                            rowCclNew["VALIDSTATE"] = "Valid";
                            rowCclNew["_STATE_"] = "added";
                            dtConsumableclass.Rows.Add(rowCclNew);
                        }
                        else
                        {
                            // 자재 그룹 정보  등록
                            DataRow rowCclNew = dtConsumableclass.NewRow();
                            rowCclNew["CONSUMABLECLASSID"] = row["MATERIALTYPE"].ToString();//row["MASTERDATACLASSID"].ToString();
                            rowCclNew["CONSUMABLECLASSNAME"] = "";// rowMDC["MASTERDATACLASSNAME"].ToString();
                            rowCclNew["DESCRIPTION"] = row["DESCRIPTION"].ToString();
                            rowCclNew["ENTERPRISEID"] = row["ENTERPRISEID"].ToString();
                            rowCclNew["CONSUMABLECLASSTYPE"] = row["MATERIALTYPE"].ToString();//row["MASTERDATACLASSID"].ToString();
                            rowCclNew["VALIDSTATE"] = "Valid";
                            rowCclNew["_STATE_"] = "modified";
                            dtConsumableclass.Rows.Add(rowCclNew);
                        }
                    }

                    // 품목 스펙 등록
                    DataRow rowmaterialitemspec = dtmaterialitemspec.NewRow();
                    rowmaterialitemspec["MATERIALTYPE"] = row["MATERIALTYPE"];//row["MASTERDATACLASSID"];
                    rowmaterialitemspec["ENTERPRISEID"] = row["ENTERPRISEID"];
                    rowmaterialitemspec["ITEMID"] = row["ITEMID"];
                    rowmaterialitemspec["ITEMVERSION"] = row["ITEMVERSION"];

                    rowmaterialitemspec["TOPCLASSID"] = row["TOPCLASSID"];
                    rowmaterialitemspec["MIDDLECLASSID"] = row["MIDDLECLASSID"];
                    rowmaterialitemspec["MATERIALCLASS"] = row["MATERIALCLASS"];//row["MASTERDATACLASSID"];
                    rowmaterialitemspec["WAERHOUSE"] = row["WAERHOUSE"];                //입고 창고
                    //rowmaterialitemspec["IDSERIALSITE"] = row["IDSERIALSITE"];          //채번
                    rowmaterialitemspec["CONVENDORNAME"] = row["CONVENDORNAME"];        //공급사
                    rowmaterialitemspec["MAKER"] = row["MAKER"];                        //제조사

                    rowmaterialitemspec["VALIDSTATE"] = "Valid";
                    rowmaterialitemspec["_STATE_"] = "added";
                    dtmaterialitemspec.Rows.Add(rowmaterialitemspec);

                    //자재 정의 정보
                    DataRow rowConsumabledefinition = dtconsumabledefinition.NewRow();
                    rowConsumabledefinition["CONSUMABLEDEFID"] = row["ITEMID"];
                    rowConsumabledefinition["CONSUMABLEDEFVERSION"] = row["ITEMVERSION"];
                    rowConsumabledefinition["CONSUMABLECLASSID"] = row["MATERIALTYPE"];//row["MASTERDATACLASSID"];
                    rowConsumabledefinition["CONSUMABLEDEFNAME"] = row["ITEMNAME"];
                    rowConsumabledefinition["ENTERPRISEID"] = row["ENTERPRISEID"];
                    rowConsumabledefinition["CONSUMABLETYPE"] = row["MATERIALCLASS"];//row["MATERIALCLASS"];
                    rowConsumabledefinition["UNIT"] = row["ITEMUOM"];
                    rowConsumabledefinition["VALIDSTATE"] = "Valid";
                    rowConsumabledefinition["DESCRIPTION"] = row["DESCRIPTION"];
                    rowConsumabledefinition["_STATE_"] = "added";
                    rowConsumabledefinition["ISLOTMNG"] = row["LOTCONTROL"];

                    dtconsumabledefinition.Rows.Add(rowConsumabledefinition);

                }

                if (rowState == "modified")
                {
                    // 품목 스펙 등록
                    DataRow rowmaterialitemspec = dtmaterialitemspec.NewRow();
                    rowmaterialitemspec["MATERIALTYPE"] = row["MATERIALTYPE"];//row["MASTERDATACLASSID"];
                    rowmaterialitemspec["ENTERPRISEID"] = row["ENTERPRISEID"];
                    rowmaterialitemspec["ITEMID"] = row["ITEMID"];
                    rowmaterialitemspec["ITEMVERSION"] = row["ITEMVERSION"];

                    rowmaterialitemspec["MIDDLECLASSID"] = row["MIDDLECLASSID"];
                    rowmaterialitemspec["TOPCLASSID"] = row["TOPCLASSID"];
                    rowmaterialitemspec["MATERIALCLASS"] = row["MATERIALCLASS"];//row["MATERIALCLASS"];

                    rowmaterialitemspec["WAERHOUSE"] = row["WAERHOUSE"];                //입고 창고
                    //rowmaterialitemspec["IDSERIALSITE"] = row["IDSERIALSITE"];          //채번
                    rowmaterialitemspec["CONVENDORNAME"] = row["CONVENDORNAME"];        //공급사
                    rowmaterialitemspec["MAKER"] = row["MAKER"];                        //제조사

                    rowmaterialitemspec["_STATE_"] = "modified";
                    dtmaterialitemspec.Rows.Add(rowmaterialitemspec);

                    //자재 정의 정보
                    DataRow rowConsumabledefinition = dtconsumabledefinition.NewRow();
                    rowConsumabledefinition["CONSUMABLEDEFID"] = row["ITEMID"];
                    rowConsumabledefinition["CONSUMABLEDEFVERSION"] = row["ITEMVERSION"];
                    rowConsumabledefinition["CONSUMABLECLASSID"] = row["MATERIALTYPE"];//row["MASTERDATACLASSID"];
                    rowConsumabledefinition["CONSUMABLEDEFNAME"] = row["ITEMNAME"];
                    rowConsumabledefinition["ENTERPRISEID"] = row["ENTERPRISEID"];
                    rowConsumabledefinition["CONSUMABLETYPE"] = row["MATERIALCLASS"];
                    rowConsumabledefinition["UNIT"] = row["ITEMUOM"];
                    rowConsumabledefinition["VALIDSTATE"] = "Valid";
                    rowConsumabledefinition["DESCRIPTION"] = row["DESCRIPTION"];
                    rowConsumabledefinition["_STATE_"] = "modified";
                    rowConsumabledefinition["ISLOTMNG"] = row["LOTCONTROL"];
                    dtconsumabledefinition.Rows.Add(rowConsumabledefinition);
                }
            }

            DataSet dSchanged = new DataSet();

            changed.TableName = "itemmaster";
            dSchanged.Tables.Add(changed);

            dtItemserialI.TableName = "itemserial";
            dSchanged.Tables.Add(dtItemserialI);

            dtConsumableclass.TableName = "consumableclass";
            dSchanged.Tables.Add(dtConsumableclass);

            dtmaterialitemspec.TableName = "materialitemspec";
            dSchanged.Tables.Add(dtmaterialitemspec);

            // 자재 그룹 정보
            dtconsumabledefinition.TableName = "consumabledefinition";
            dSchanged.Tables.Add(dtconsumabledefinition);

            ExecuteRule("SaveMaterialItemMaster", dSchanged);


        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            //자재 마스터
            //grdIMList.DataSource = null;
            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("ITEMOWNER", "Material"); // 품목 자재
            //values.Add("MASTERDATACLASSID", "");

            DataTable dtMDCList = await SqlExecuter.QueryAsync("GetMaterialItemMaster", "10002", values);

            if (dtMDCList.Rows.Count < 1) // 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            this.grdIMList.DataSource = dtMDCList;

            // 자재품목조회
            //FocusedRowChanged();

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

            grdIMList.View.CheckValidation();
            changed = grdIMList.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }
        #endregion

        #region private Fuction
        #endregion

    }
}

