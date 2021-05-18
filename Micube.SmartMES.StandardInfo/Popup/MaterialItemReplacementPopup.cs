using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micube.SmartMES.StandardInfo
{
	public partial class MaterialItemReplacementPopup : SmartPopupBaseForm
	{
        public DataRow CurrentDataRow;



        public delegate void AddSemiProductEventHandlerDelegate(object sender, AddSemiProductEventArgs e);

        public event AddSemiProductEventHandlerDelegate AddSemiProductEventHandler;

        public MaterialItemReplacementPopup(DataRow row)
		{

            CurrentDataRow = row;
            InitializeComponent();
            

            InitializeCondition();
			InitializeEvent();


        }

		/// <summary>
		/// 컨트롤 초기화
		/// </summary>
		private void InitializeCondition()
		{

            popMaterialCode.EditValue = Format.GetString(CurrentDataRow["ITEMID"]);


        }

		/// <summary>
		/// 이벤트 초기화
		/// </summary>
		private void InitializeEvent()
		{
			btnCancel.Click += BtnCancel_Click;
            btnConfrim.Click += BtnConfrim_Click;
            txtHorizontal.EditValueChanged += TxtHorizontal_EditValueChanged;

        }

        private void TxtHorizontal_EditValueChanged(object sender, EventArgs e)
        {

            txtReplaceMaterial.EditValue = popMaterialCode.EditValue + "-" + txtHorizontal.EditValue;




        }

        private void RaiseOnAddSemiProduct(object sender, AddSemiProductEventArgs e)
        {
            AddSemiProductEventHandler?.Invoke(sender, e);
        }

        private void BtnConfrim_Click(object sender, EventArgs e)
        {
            /*
            DataTable dt = new DataTable();

            CurrentDataRow["HORIZONTALVERTICAL"] = Format.GetString(txtHorizontal.EditValue) + Format.GetString(txtVertical.EditValue);


             
            dr["ITEMID"] = txtReplaceMaterial.EditValue;
            dr["MASTERDATACLASSID"] = CurrentDataRow["MASTERDATACLASSID"];
            dr["DEFAULTCODE"] = CurrentDataRow["DEFAULTCODE"];
            dr["ITEMNAME"] = txtAssemblyItemName.EditValue;
            dr["SPEC"] = txtSpec.EditValue;
            dr["MATERIALQUALITY"] = CurrentDataRow["MATERIALQUALITY"];

            dr["WIDTH"] = txtHorizontal.EditValue;
            dr["LENGTH"] = txtVertical.EditValue;
            dr["LME"] = CurrentDataRow["LME"];
            dr["ORIGIN"] = CurrentDataRow["ORIGIN"];
            dr["MAKER"] = CurrentDataRow["MAKER"];
            dr["CONVERSIONFACTOR"] = CurrentDataRow["CONVERSIONFACTOR"];
            dr["PROCUREMENT"] = CurrentDataRow["PROCUREMENT"];
            dr["ACCOUNTCODE"] = CurrentDataRow["ACCOUNTCODE"];
            dr["MATERIALCLASS"] = CurrentDataRow["MATERIALCLASS"];
            dr["UNITOFSTOCK"] = CurrentDataRow["UNITOFSTOCK"];

            dr["UNITOFPURCHASING"] = CurrentDataRow["UNITOFPURCHASING"];
            dr["COLOR"] = CurrentDataRow["COLOR"];

            dr["ENTERPRISEID"] = CurrentDataRow["ENTERPRISEID"];
            dr["PLANTID"] = CurrentDataRow["PLANTID"];
            dr["VALIDSTATE"] = "Valid";
            dr["CUSTOMERITEMID"] = CurrentDataRow["CUSTOMERITEMID"];
            dr["DESCRIPTION"] = CurrentDataRow["DESCRIPTION"];
            dr["MASTERDATACLASSID"] = CurrentDataRow["ENDTYPE"];
            dr["ISINCOMMINGINSPECTION"] = CurrentDataRow["ISINCOMMINGINSPECTION"];

            dr["LOTCONTROL"] = CurrentDataRow["LOTCONTROL"];
            dr["LIMITATION"] = CurrentDataRow["LIMITATION"];
            dr["COPPERTYPE"] = CurrentDataRow["COPPERTYPE"];
            dr["ADH"] = CurrentDataRow["ADH"];
            dr["THICK"] = CurrentDataRow["THICK"];
            dr["HORIZONTALVERTICAL"] = CurrentDataRow["HORIZONTALVERTICAL"];
            RaiseOnAddSemiProduct(sender,
                new AddSemiProductEventArgs()
                {
                    ProductDataRow = CurrentDataRow
                });
                */
            this.Close();
        }




        /// <summary>
        /// 취소 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void smartSplitTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
