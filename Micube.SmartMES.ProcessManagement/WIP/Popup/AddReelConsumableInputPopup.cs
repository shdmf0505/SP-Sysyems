#region using
using Micube.Framework;
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
#endregion

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 공정작업 > 입고검사등록
	/// 업  무  설  명  : 사진등록 팝업
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-06-20
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class AddReelConsumableInputPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }

        public DataTable GetData
        {
            get { return this.grdReelConsumable.DataSource as DataTable; }
        }

		#region 생성자
		public AddReelConsumableInputPopup()
		{
			InitializeComponent();

			if (!this.IsDesignMode())
			{
				InitializeEvent();

            }
            InitializeControl();
            InitializeGrid();

        }
        private void InitializeControl()
        {
            this.txtReelConsumable.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            grdReelConsumable.View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            grdReelConsumable.View.Appearance.Row.Font = new Font("Tahoma", 13);
            grdReelConsumable.View.ColumnPanelRowHeight = 30;

        }

            /// <summary>
            /// 그리드 정보를 초기화한다.
            /// </summary>
            private void InitializeGrid()
        {
            grdReelConsumable.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdReelConsumable.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdReelConsumable.View.SetIsReadOnly();

            grdReelConsumable.View.AddTextBoxColumn("CONSUMABLEID", 450).SetLabel("REELCONSUMABLEID").SetIsReadOnly();


            grdReelConsumable.View.PopulateColumns();


            grdReelConsumable.View.Columns[0].AppearanceHeader.Font = new Font("Tahoma", 13);
        }




        #endregion

        #region Event
        private void InitializeEvent()
		{
			this.btnApply.Click += BtnApply_Click;
			this.btnCancel.Click += BtnCancel_Click;
            this.txtReelConsumable.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.grdReelConsumable.View.AddNewRow();

                    int irow = this.grdReelConsumable.View.RowCount - 1;
                    DataRow classRow = this.grdReelConsumable.View.GetDataRow(irow);
                    classRow["CONSUMABLEID"] = this.txtReelConsumable.Text;
                    this.grdReelConsumable.View.RaiseValidateRow(irow);

                    this.txtReelConsumable.Text = string.Empty;

                }

            };
            this.txtReelConsumable.Click += (s, e) =>
            {
                this.txtReelConsumable.SelectAll();
            };

            this.grdReelConsumable.View.KeyDown += (s, e) =>
             {
                 if(e.KeyCode == Keys.Delete)
                 {
                     if(this.grdReelConsumable.View.FocusedRowHandle > -1)
                     {
                         int rowHandle = this.grdReelConsumable.View.FocusedRowHandle;

                         this.grdReelConsumable.View.DeleteRow(rowHandle);

                         if (this.grdReelConsumable.View.RowCount < rowHandle)
                             this.grdReelConsumable.View.FocusedRowHandle = this.grdReelConsumable.View.RowCount - 1;
                         else
                             this.grdReelConsumable.View.FocusedRowHandle = rowHandle;


                     }

                 }
             };
        }

		/// <summary>
		/// Cancel 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Apply 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnApply_Click(object sender, EventArgs e)
		{
            DataTable dtConsumable = this.grdReelConsumable.DataSource as DataTable;

            var duplicateConsumable = from r in dtConsumable.AsEnumerable()
                                      group r by new
                                      {
                                          CONSUMABLEID = r.Field<string>("CONSUMABLEID"),
                                      } into g
                                      where g.Count() > 1
                                      select g;

            if (duplicateConsumable.Count() > 0)
                throw MessageException.Create("DuplicationReelConsumableID", duplicateConsumable.ElementAt(0).Key.CONSUMABLEID);

            this.Close();

		}
		#endregion
	}
}
