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
	public partial class GovernanceMaterialSpecPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		private string _governanceNo = string.Empty;
		public DataRow CurrentDataRow { get; set; }

		#region 생성자
		public GovernanceMaterialSpecPopup(string governanceNo)
		{
			InitializeComponent();

			_governanceNo = governanceNo;

			InitailizeGrid();
			SpecDataBind();
			InitailizeEvent();

		}
		#endregion

		#region 컨텐츠 초기화

		private void InitailizeGrid()
		{
			grdMaterialSpecList.GridButtonItem = GridButtonItem.Export;
			grdMaterialSpecList.View.SetAutoFillColumn("SPECVALUE");
			grdMaterialSpecList.View.AddTextBoxColumn("SPECITEM").SetIsReadOnly();
			grdMaterialSpecList.View.AddTextBoxColumn("SPECVALUE");
			grdMaterialSpecList.View.AddTextBoxColumn("SEQUENCE").SetIsHidden();
			grdMaterialSpecList.View.PopulateColumns();
			
		}

		/// <summary>
		/// 
		/// </summary>
		private void SpecDataBind()
		{
			DataTable dt = SqlExecuter.Query("GetMaterialSpec", "10001", new Dictionary<string, object>(){ { "GOVERNANCENO", _governanceNo} });
			if(dt.Rows.Count <= 0)
			{
				//DataTable dt = new DataTable();
				//dt.Columns.Add("SPECITEM", typeof(string));
				//dt.Columns.Add("SPECVALUE", typeof(string));

				dt.Rows.Add("Base(내층1)", string.Empty, 1);    //Base(내층1)	
				dt.Rows.Add("Base(내층2)", string.Empty, 2);    //Base(내층2)	
				dt.Rows.Add("Base(내층3)", string.Empty, 3);    //Base(내층3)	
				dt.Rows.Add("Base(외층1)", string.Empty, 4);    //Base(외층1)	
				dt.Rows.Add("절연체(내층1)", string.Empty, 5);    //절연체(내층1)	
				dt.Rows.Add("절연체(내층2)", string.Empty, 6);    //절연체(내층2)	
				dt.Rows.Add("절연체(내층3)", string.Empty, 7);    //절연체(내층3)	
				dt.Rows.Add("절연체(외층1)", string.Empty, 8);    //절연체(외층1)    
				dt.Rows.Add("절연체(외층2)", string.Empty, 9);    //절연체(외층2)    
				dt.Rows.Add("층간접착제(1)", string.Empty, 10);    //층간접착제(1)    
				dt.Rows.Add("층간접착제(2)", string.Empty, 11);    //층간접착제(2)    
				dt.Rows.Add("층간접착제(3)", string.Empty, 12);    //층간접착제(3)    
				dt.Rows.Add("층간접착제(4)", string.Empty, 13);    //층간접착제(4)    
				dt.Rows.Add("보강판(1)", string.Empty, 14);    //보강판(1)    
				dt.Rows.Add("보강판(2)", string.Empty, 15);    //보강판(2)    
				dt.Rows.Add("보강판(3)", string.Empty, 16);    //보강판(3)    
				dt.Rows.Add("보강판(4)", string.Empty, 17);    //보강판(4)    
				dt.Rows.Add("보강판(5)", string.Empty, 18);    //보강판(5)    
				dt.Rows.Add("보강판(6)", string.Empty, 19);    //보강판(6)    
				dt.Rows.Add("보강판(7)", string.Empty, 20);    //보강판(7)    
				dt.Rows.Add("보강판(8)", string.Empty, 21);    //보강판(8)    
				dt.Rows.Add("SHIELD(1)", string.Empty, 22);    //SHIELD(1)  
				dt.Rows.Add("SHIELD(2)", string.Empty, 23);    //SHIELD(2)  
			}
			
			grdMaterialSpecList.DataSource = dt;
		}
		#endregion

		#region 이벤트

		/// <summary>
		/// 이벤트
		/// </summary>
		private void InitailizeEvent()
		{
			btnCancel.Click += BtnCancel_Click;
			btnConfirm.Click += BtnConfirm_Click;
		}

		/// <summary>
		/// 취소 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 확인 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnConfirm_Click(object sender, EventArgs e)
		{
			grdMaterialSpecList.View.PostEditor();
			grdMaterialSpecList.View.UpdateCurrentRow();

			MessageWorker worker = new MessageWorker("SaveGovernanceMaterialSpec");
			worker.SetBody(new MessageBody()
			{
				{ "governanceNo", _governanceNo },
				{ "specList", grdMaterialSpecList.DataSource }

			});
			worker.Execute();
			ShowMessage("SuccessSave");

			this.Close();
		}

		#endregion
	}
}
