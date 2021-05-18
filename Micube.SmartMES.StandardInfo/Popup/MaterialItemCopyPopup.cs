#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;

#endregion

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > 작업장 관리 > 설비 유형 팝업
    /// 업  무  설  명  : 설비 유형(EquipmentClass)을 선택
    /// 생    성    자  :  정승원
    /// 생    성    일  : 2019-05-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MaterialItemCopyPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables
        string _sCD_ITEM = "";
        string _sITEMVERSION = "";
        string _sNM_ITEM = "";

      

		//Resource Type = Equipment
		
        /// <summary>
        ///  선택한 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        public event chat_room_evecnt_handler write_handler;
        #endregion

        #region 생성자
        public MaterialItemCopyPopup()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();
        }

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {

            //자재구분
            cboConsumableType.DisplayMember = "CODENAME";
            cboConsumableType.ValueMember = "CODEID";
            cboConsumableType.ShowHeader = false;

            cboConsumableType.EmptyItemValue = 0;
            cboConsumableType.EmptyItemCaption = "전체";
            cboConsumableType.UseEmptyItem = true;


            Dictionary<string, object> ParamConsumableType = new Dictionary<string, object>();
            ParamConsumableType.Add("CODECLASSID", "MaterialClass");
            ParamConsumableType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtConsumableType = SqlExecuter.Query("GetCodeList", "00001", ParamConsumableType);
            cboConsumableType.DataSource = dtConsumableType;


            //품목구분
            cboMasterDataClassId.DisplayMember = "MASTERDATACLASSNAME";
            cboMasterDataClassId.ValueMember = "MASTERDATACLASSID";
            cboMasterDataClassId.ShowHeader = false;

            cboMasterDataClassId.EmptyItemValue = 0;
            cboMasterDataClassId.EmptyItemCaption = "전체";
            cboMasterDataClassId.UseEmptyItem = true;

            Dictionary<string, object> ParamMasterDataClassId = new Dictionary<string, object>();
            ParamMasterDataClassId.Add("ENTERPRISEID", UserInfo.Current.Enterprise );
            ParamMasterDataClassId.Add("ITEMOWNER", "Material");
            DataTable dtMasterDataClassId = SqlExecuter.Query("GetmasterdataclassList", "10001", ParamMasterDataClassId);
            cboMasterDataClassId.DataSource = dtMasterDataClassId;
            

        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdItemCopy.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdItemCopy.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
          
            grdItemCopy.View.AddTextBoxColumn("ITEMID_SO", 150).SetIsReadOnly();
            grdItemCopy.View.AddTextBoxColumn("ITEMVERSION_SO", 100).SetIsReadOnly();
            grdItemCopy.View.AddTextBoxColumn("ITEMID_TG", 150).SetValidationIsRequired();
            grdItemCopy.View.AddTextBoxColumn("ITEMVERSION_TG", 150).SetIsReadOnly();


            grdItemCopy.View.AddTextBoxColumn("ITEMNAME", 250).SetIsReadOnly();
            grdItemCopy.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFID", "UOMDEFNAME");
            grdItemCopy.View.AddComboBoxColumn("MATERIALCLASS", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            
            grdItemCopy.View.AddTextBoxColumn("MASTERDATACLASSNAME", 150);
            grdItemCopy.View.AddTextBoxColumn("MASTERDATACLASSID", 150).SetIsHidden();

            //grdItemCopy.View.AddComboBoxColumn("STATUS", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ItemStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdItemCopy.View.PopulateColumns();

            //RepositoryItemCheckEdit repositoryCheckEdit1 = grdProductItem.View.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            //repositoryCheckEdit1.ValueChecked = "True";
            //repositoryCheckEdit1.ValueUnchecked = "False";
            //repositoryCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            //grdProductItem.View.Columns["S"].ColumnEdit = repositoryCheckEdit1;

        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnAPPLY.Click += BtnAPPLY_Click;
            btnCancel.Click += BtnCancel_Click;
             
        }

        private void BtnAPPLY_Click(object sender, EventArgs e)
        {
            DataTable dt = grdItemCopy.View.GetCheckedRows();

            foreach(DataRow row in dt.Rows)
            {

                if (row["ITEMID_TG"].ToString() == "")
                {
                    ShowMessage("ToolRequestProductCodeValidation");
                    return;
                }

                Dictionary<string, object> ParamImChk = new Dictionary<string, object>();
                ParamImChk.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                ParamImChk.Add("ITEMID", row["ITEMID_TG"]);
                ParamImChk.Add("ITEMVERSION", row["ITEMVERSION_TG"]);
                ParamImChk.Add("MASTERDATACLASSID", row["MASTERDATACLASSID"]);
                DataTable dtItemMasterChk = SqlExecuter.Query("GetMaterialItemMaster", "10001", ParamImChk).Copy();

                if(dtItemMasterChk != null)
                {
                    if(dtItemMasterChk.Rows.Count !=0)
                    {
                        ShowMessage("InValidData002");
                        return;
                    }
                }

                Dictionary<string, object> ParamIm = new Dictionary<string, object>();
                ParamIm.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                ParamIm.Add("ITEMID", row["ITEMID_SO"]);
                ParamIm.Add("ITEMVERSION", row["ITEMVERSION_SO"]);
                ParamIm.Add("MASTERDATACLASSID", row["MASTERDATACLASSID"]);
                DataTable dtItemMasterCopy = SqlExecuter.Query("GetMaterialItemMaster", "10001", ParamIm).Copy();

                Dictionary<string, object> ParamMi = new Dictionary<string, object>();
                ParamMi.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                ParamMi.Add("ITEMID", row["ITEMID_SO"]);
                ParamMi.Add("ITEMVERSION", row["ITEMVERSION_SO"]);
            
                DataTable dtItemSpecCopy = SqlExecuter.Query("GetMateritalitemspec", "10001", ParamMi).Copy();

              

                dtItemMasterCopy.Columns.Add("_STATE_");
                dtItemSpecCopy.Columns.Add("_STATE_");

                DataTable dtItemId = SqlExecuter.Query("GetItemId", "10001");

                foreach (DataRow rowImCopy in  dtItemMasterCopy.Rows)
                {
                    rowImCopy["ITEMID"] = row["ITEMID_TG"];
                    rowImCopy["ITEMVERSION"] = row["ITEMVERSION_TG"];

                    rowImCopy["_STATE_"] = "added";
                    //rowImCopy.AcceptChanges();
                    //rowImCopy.SetAdded();
                }
                foreach (DataRow rowIsCopy in dtItemSpecCopy.Rows)
                {
                    rowIsCopy["ITEMID"] = row["ITEMID_TG"];
                    rowIsCopy["ITEMVERSION"] = row["ITEMVERSION_TG"];

                    rowIsCopy["_STATE_"] = "added";
                    //rowIsCopy.AcceptChanges();
                    // rowIsCopy.SetAdded();
                }

                DataSet dsItemChang = new DataSet();

                dtItemMasterCopy.TableName = "itemmaster";
                dtItemSpecCopy.TableName = "materialItemSpec";
                dsItemChang.Tables.Add(dtItemMasterCopy);
                dsItemChang.Tables.Add(dtItemSpecCopy);

                ExecuteRule("MaterialItemSpec", dsItemChang);

                ShowMessage("SuccessSave");
            }

            DataTable dtitem = (DataTable)grdItemCopy.DataSource;
            if(dtitem != null)
            {
                dtitem.Clear();
            }


        }

      

        

        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {

            string sConsumableType = "";
            if ( cboConsumableType.GetDataValue() != null)
            {
                sConsumableType = cboConsumableType.GetDataValue().ToString();
            }

            string sMasterDataClassId = "";
            if (cboMasterDataClassId.GetDataValue() != null)
            {
                sMasterDataClassId = cboMasterDataClassId.GetDataValue().ToString();
            }

            Search( txtItemCode.Text, txtItemVer.Text, txtItemNm.Text, sConsumableType, sMasterDataClassId);
        }

        void Search(string sCD_ITEM,string _sITEMVERSION,string sNM_ITEM, string sConsumableType, string sMasterDataClassId)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("ITEMID", sCD_ITEM);
            Param.Add("ITEMVERSION", _sITEMVERSION);
            Param.Add("ITEMNAME", sNM_ITEM);
          
            Param.Add("MASTERDATACLASSID", sMasterDataClassId);
            Param.Add("CONSUMABLETYPE", sConsumableType);

            DataTable dt = SqlExecuter.Query("GetMaterialItemPopup", "10001", Param);
            grdItemCopy.DataSource = dt;
            
        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
