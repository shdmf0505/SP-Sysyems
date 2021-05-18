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
    public partial class RoutingItemCopyPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        #region Local Variables

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region 생성자
        public RoutingItemCopyPopup(DataRow ItemRow)
        {
            InitializeComponent();
            InitializeCondition();
            InitializeEvent();

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            popTransProduct.SetValue(ItemRow["ITEMID"]);
            txtTransProductRev.Text = ItemRow["ITEMVERSION"].ToString();
            txtTransProductName.Text = ItemRow["ITEMNAME"].ToString();

            layoutControlItem1.Text = Language.Get("EXISTINGPRODUCTDEFID");
            layoutControlItem2.Text = Language.Get("TRANSPRODUCTDEFID");
            layoutControlItem5.Text = Language.Get("EXISTINGPRODUCTDEFNAME");
            layoutControlItem6.Text = Language.Get("TRANSPRODUCTDEFNAME");
        }

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {

            //품목(기존)
            InitializePopup_PrevProduct();

            //품목(변경)
            InitializePopup_TransProduct();


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
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            popTransProduct.EditValueChanged += PopTransProduct_EditValueChanged;
            popPrevProduct.EditValueChanged += PopPrevProduct_EditValueChanged;
        }

      

        /// <summary>
        /// 품목(기존) 팝업 초기화
        /// </summary>
        private void InitializePopup_PrevProduct()
        {
            if (popPrevProduct.SelectPopupCondition != null) popPrevProduct.SelectPopupCondition = null;

            ConditionItemSelectPopup options = new ConditionItemSelectPopup();
            options.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);
            options.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false);
            options.Id = "ITEMID";
            options.SearchQuery = new SqlQuery("GetItemMasterList", "10004", $"ENTERPRISEID={UserInfo.Current.Enterprise}");
            options.DisplayFieldName = "ITEMID";
            options.ValueFieldName = "ITEMID";
            options.LanguageKey = "ITEMID";
            options.SetPopupAutoFillColumns("ITEMNAME");
            options.SetPopupResultCount(1);
            options.SetPopupApplySelection((selectRow, gridRow) => {
                DataRow row = selectRow.FirstOrDefault();
                //popPrevProduct.Tag = row["ITEM"];
                txtPrevProductRev.Text = row["ITEMVERSION"].ToString();
                txtPrevProductName.Text = row["ITEMNAME"].ToString();

            });

            options.Conditions.AddTextBox("TXTITEM");

            options.GridColumns.AddTextBoxColumn("ITEMID", 150);
            options.GridColumns.AddTextBoxColumn("ITEMVERSION", 60);
            options.GridColumns.AddTextBoxColumn("ITEMNAME", 200);

            popPrevProduct.SelectPopupCondition = options;
            popPrevProduct.EditValue = string.Empty;
        }

        /// <summary>
		/// 품목(변경) 팝업 초기화
		/// </summary>
		private void InitializePopup_TransProduct()
        {
            if (popTransProduct.SelectPopupCondition != null) popTransProduct.SelectPopupCondition = null;

            ConditionItemSelectPopup options = new ConditionItemSelectPopup();
            options.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);
            options.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false);
            options.Id = "ITEMID";
            options.SearchQuery = new SqlQuery("GetItemMasterList", "10004", $"ENTERPRISEID={UserInfo.Current.Enterprise}");
            options.DisplayFieldName = "ITEMID";
            options.ValueFieldName = "ITEMID";
            options.LanguageKey = "ITEMID";
            options.SetPopupAutoFillColumns("ITEMNAME");
            options.SetPopupResultCount(1);
            options.SetPopupApplySelection((selectRow, gridRow) => {
                DataRow row = selectRow.FirstOrDefault();
                popTransProduct.Tag = row["ITEM"];

                txtTransProductRev.Text = row["ITEMVERSION"].ToString();
                txtTransProductName.Text = row["ITEMNAME"].ToString();

                //Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Join(",", productRevisionList);

            });

            options.Conditions.AddTextBox("TXTITEM");
            options.GridColumns.AddTextBoxColumn("ITEMID", 150);
            options.GridColumns.AddTextBoxColumn("ITEMVERSION", 60);
            options.GridColumns.AddTextBoxColumn("ITEMNAME", 200);

            popTransProduct.SelectPopupCondition = options;
            popTransProduct.EditValue = string.Empty;
        }

        /// <summary>
		/// 품목(변경)팝업 값 변경 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PopTransProduct_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Format.GetString(popTransProduct.EditValue)))
            {
                txtTransProductRev.Text = string.Empty;
                txtTransProductName.Text = string.Empty;
            }
            else
            {
                string product = Format.GetString(popTransProduct.Tag);
                if (string.IsNullOrEmpty(product)) return;



                txtTransProductName.Text = product.Split('|')[0];
                txtTransProductRev.Text = product.Split('|')[1];

            }//else
        }
        /// <summary>
		/// 품목(기존)팝업 값 변경 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void PopPrevProduct_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Format.GetString(popPrevProduct.EditValue)))
            {
                txtPrevProductRev.Text = string.Empty;
                txtPrevProductName.Text = string.Empty;
            }
            else
            {
                string product = Format.GetString(popPrevProduct.Tag);
                if (string.IsNullOrEmpty(product)) return;

                txtPrevProductName.Text = product.Split('|')[0];
                txtPrevProductRev.Text = product.Split('|')[1];


            }//else
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Format.GetString(popTransProduct.GetValue()))
           || string.IsNullOrEmpty(Format.GetString(txtTransProductRev.Text)))
            {
                //변경 품목은 필수 입력입니다.
                ShowMessage("ISREQUIREDRCPRODUCT");
                return;
            }

            if (string.IsNullOrEmpty(Format.GetString(popPrevProduct.GetValue()))
            || string.IsNullOrEmpty(Format.GetString(txtPrevProductRev.Text)))
            {
                //기존 품목은 필수 입니다.
                ShowMessage("ISREQUIREDPREVPRODUCT");
                return;
            }

            //수주사양 승인여부


            Dictionary<string, object> ParamSaleChk = new Dictionary<string, object>();
            ParamSaleChk.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            
            ParamSaleChk.Add("ITEMID", Format.GetString(popTransProduct.GetValue()));
            ParamSaleChk.Add("ITEMVERSION", Format.GetString(txtTransProductRev.Text));
            DataTable dtSoChk = SqlExecuter.Query("GetSaleorderapprovalChk", "10001", ParamSaleChk);

            // 제품일경우만 승인여부 체크

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

          
            DataTable dtRoutingChk = SqlExecuter.Query("GetRouting", "10001", ParamSaleChk);

            if (dtRoutingChk != null)
            {
                if (dtRoutingChk.Rows.Count != 0)
                {
                    DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "RoutingCopy");

                    switch (result)
                    {
                        
                        case DialogResult.No:
                            return;
                    }
                }
            }


           


            DataTable dtRountItemCopy = new DataTable();
            dtRountItemCopy.Columns.Add("PRODUCTDEFID_SOURCE");
            dtRountItemCopy.Columns.Add("PRODUCTDEFVERSION_SOURCE");
            dtRountItemCopy.Columns.Add("PRODUCTDEFID_TARGET");
            dtRountItemCopy.Columns.Add("PRODUCTDEFVERSION_TARGET");
            dtRountItemCopy.Columns.Add("CREATOR");
            
            dtRountItemCopy.Columns.Add("_STATE_");

            DataRow rowRountItemCopy = dtRountItemCopy.NewRow();
            rowRountItemCopy["PRODUCTDEFID_SOURCE"] = popPrevProduct.GetValue();
            rowRountItemCopy["PRODUCTDEFVERSION_SOURCE"] = txtPrevProductRev.Text;
            rowRountItemCopy["PRODUCTDEFID_TARGET"] = popTransProduct.GetValue();
            rowRountItemCopy["PRODUCTDEFVERSION_TARGET"] = txtTransProductRev.Text;
            rowRountItemCopy["CREATOR"] = UserInfo.Current.Id;
            rowRountItemCopy["_STATE_"] = "added";
            dtRountItemCopy.Rows.Add(rowRountItemCopy);

            dtRountItemCopy.TableName = "routingitemcopy";

            DataSet ds = new DataSet();
            ds.Tables.Add(dtRountItemCopy);

            ExecuteRule("ItemMaster", ds);
            
            MSGBox.Show(MessageBoxType.Information, "SuccedSave");
            this.Close();
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
