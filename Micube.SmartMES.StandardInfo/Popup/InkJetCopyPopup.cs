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
    public partial class InkJetCopyPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        public DataRow CurrentDataRow { get; set; }
        public DataRow _rowInkJetCopy;

        #region Local Variables
        string _sCD_ITEM = "";
        string _sITEMVERSION = "";
        string _sNM_ITEM = "";

      

		//Resource Type = Equipment
		
        /// <summary>
        ///  선택한 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
      
        
        #endregion

        #region 생성자
        public InkJetCopyPopup(DataRow rowInkJetCopy)
        {
            InitializeComponent();
            InitializeEvent();
            InitializeCondition();
            _rowInkJetCopy = rowInkJetCopy;

       


            sspCustomerId.SetValue(_rowInkJetCopy["CUSTOMERID"].ToString());
            //sspCustomerId.EditValue = _rowInkJetCopy["CUSTOMERID"].ToString();
            //sspCustomerId.Text = _rowInkJetCopy["CUSTOMERNAME"].ToString();
            txtENDUSERSource.Text = _rowInkJetCopy["ENDUSER"].ToString();
            txtINKJETCODE.Text = _rowInkJetCopy["INKJETCODE"].ToString();

        }

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {

            // 거래처팝업
            ConditionItemSelectPopup cisidvendorCode = new ConditionItemSelectPopup();
            cisidvendorCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisidvendorCode.SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel);

            cisidvendorCode.Id = "CUSTOMERID";
            cisidvendorCode.LabelText = "CUSTOMERID";
            cisidvendorCode.SearchQuery = new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisidvendorCode.IsMultiGrid = false;
            cisidvendorCode.DisplayFieldName = "CUSTOMERID";
            cisidvendorCode.ValueFieldName = "CUSTOMERID";
            cisidvendorCode.LanguageKey = "CUSTOMERID";
            cisidvendorCode.Conditions.AddTextBox("TXTCUSTOMERID");
            cisidvendorCode.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            cisidvendorCode.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            cisidvendorCode.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            cisidvendorCode.GridColumns.AddTextBoxColumn("CEONAME", 100);
            cisidvendorCode.GridColumns.AddTextBoxColumn("TELNO", 100);
            cisidvendorCode.GridColumns.AddTextBoxColumn("FAXNO", 100);
            sspCustomerId.SelectPopupCondition = cisidvendorCode;

            ////자재구분
            //cboConsumableType.DisplayMember = "CODENAME";
            //cboConsumableType.ValueMember = "CODEID";
            //cboConsumableType.ShowHeader = false;
            //Dictionary<string, object> ParamConsumableType = new Dictionary<string, object>();
            //ParamConsumableType.Add("CODECLASSID", "ConsumableType");
            //ParamConsumableType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtConsumableType = SqlExecuter.Query("GetCodeList", "00001", ParamConsumableType);
            //cboConsumableType.DataSource = dtConsumableType;


            ////품목구분
            //cboMasterDataClassId.DisplayMember = "CODENAME";
            //cboMasterDataClassId.ValueMember = "CODEID";
            //cboMasterDataClassId.ShowHeader = false;
            //Dictionary<string, object> ParamMasterDataClassId = new Dictionary<string, object>();
            //ParamMasterDataClassId.Add("ENTERPRISEID", UserInfo.Current.Enterprise );
            //ParamMasterDataClassId.Add("ITEMOWNER", "");
            //DataTable dtMasterDataClassId = SqlExecuter.Query("GetmasterdataclassList", "10001", ParamMasterDataClassId);
            //cboMasterDataClassId.DataSource = dtMasterDataClassId;


        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
           

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
          
            btnAPPLY.Click += BtnAPPLY_Click;
            btnCancel.Click += BtnCancel_Click;
             
        }

        private void BtnAPPLY_Click(object sender, EventArgs e)
        {

            this.Close();

            //foreach (DataRow row in _dtInkJetCopy.Rows)
            //{

            //    Dictionary<string, object> ParamImChk = new Dictionary<string, object>();
            //    ParamImChk.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //    ParamImChk.Add("ITEMCODE", row["ITEMCODE"]);
            //    ParamImChk.Add("ITEMVERSION", row["ITEMVERSION_TG"]);
            //    ParamImChk.Add("MASTERDATACLASSID", row["MASTERDATACLASSID"]);
            //    DataTable dtItemMasterChk = SqlExecuter.Query("GetMaterialItemMaster", "10001", ParamImChk).Copy();

            //    if(dtItemMasterChk != null)
            //    {
            //        if(dtItemMasterChk.Rows.Count !=0)
            //        {
            //            ShowMessage("InValidData002");
            //            return;
            //        }
            //    }



            //    Dictionary<string, object> ParamIm = new Dictionary<string, object>();
            //    ParamIm.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //    ParamIm.Add("ITEMID", row["ITEMID_TG"]);
            //    ParamIm.Add("ITEMVERSION", row["ITEMVERSION_TG"]);
            //    ParamIm.Add("MASTERDATACLASSID", row["MASTERDATACLASSID"]);
            //    DataTable dtItemMasterCopy = SqlExecuter.Query("GetMaterialItemMaster", "10001", ParamIm).Copy();

            //    Dictionary<string, object> ParamMi = new Dictionary<string, object>();
            //    ParamMi.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //    ParamMi.Add("ITEMID", row["ITEMID_TG"]);
            //    ParamMi.Add("ITEMVERSION", row["ITEMVERSION_TG"]);

            //    DataTable dtItemSpecCopy = SqlExecuter.Query("GetMateritalitemspec", "10001", ParamMi).Copy();



            //    dtItemMasterCopy.Columns.Add("_STATE_");
            //    dtItemSpecCopy.Columns.Add("_STATE_");

            //    DataTable dtItemId = SqlExecuter.Query("GetItemId", "10001");

            //    foreach (DataRow rowImCopy in  dtItemMasterCopy.Rows)
            //    {


            //        rowImCopy["ITEMID"] = dtItemId.Rows[0]["ITEMID"].ToString();

            //        rowImCopy["ITEMCODE"] = row["ITEMCODE"];

            //        rowImCopy["_STATE_"] = "added";
            //        rowImCopy.AcceptChanges();
            //        rowImCopy.SetAdded();
            //    }
            //    foreach (DataRow rowIsCopy in dtItemSpecCopy.Rows)
            //    {
            //        rowIsCopy["ITEMID"] = dtItemId.Rows[0]["ITEMID"].ToString();

            //        rowIsCopy["ITEMCODE"] = row["ITEMCODE"];
            //        rowIsCopy["_STATE_"] = "added";
            //        rowIsCopy.SetAdded();
            //    }

            //    DataSet dsItemChang = new DataSet();

            //    dtItemMasterCopy.TableName = "itemmaster";
            //    dtItemSpecCopy.TableName = "materialItemSpec";
            //    dsItemChang.Tables.Add(dtItemMasterCopy);
            //    dsItemChang.Tables.Add(dtItemSpecCopy);

            //    ExecuteRule("MaterialItemSpec", dsItemChang);

            //    ShowMessage("SuccessSave");
            //}
            // dt.AcceptChanges();


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
