namespace Micube.SmartMES.ProcessManagement
{
	partial class RecieveConsumable
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
			this.grdMaterialListByRequest = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.pnlCheckAllReceive = new Micube.Framework.SmartControls.SmartPanel();
			this.chkIsAllReceive = new Micube.Framework.SmartControls.SmartCheckBox();
			this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.grdConsumableLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
			this.smartGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlCheckAllReceive)).BeginInit();
			this.pnlCheckAllReceive.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chkIsAllReceive.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-1557, 398, 650, 400);
			this.pnlCondition.Size = new System.Drawing.Size(296, 604);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(554, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.grdConsumableLotList);
			this.pnlContent.Controls.Add(this.smartSpliterControl1);
			this.pnlContent.Controls.Add(this.smartGroupBox1);
			this.pnlContent.Size = new System.Drawing.Size(554, 608);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(859, 637);
			// 
			// smartGroupBox1
			// 
			this.smartGroupBox1.Controls.Add(this.grdMaterialListByRequest);
			this.smartGroupBox1.Controls.Add(this.pnlCheckAllReceive);
			this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
			this.smartGroupBox1.LanguageKey = "MATERIALREQUESTSTATE";
			this.smartGroupBox1.Location = new System.Drawing.Point(0, 0);
			this.smartGroupBox1.Margin = new System.Windows.Forms.Padding(0);
			this.smartGroupBox1.Name = "smartGroupBox1";
			this.smartGroupBox1.ShowBorder = true;
			this.smartGroupBox1.Size = new System.Drawing.Size(554, 400);
			this.smartGroupBox1.TabIndex = 0;
			this.smartGroupBox1.Text = "smartGroupBox1";
			// 
			// grdMaterialListByRequest
			// 
			this.grdMaterialListByRequest.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdMaterialListByRequest.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdMaterialListByRequest.IsUsePaging = false;
			this.grdMaterialListByRequest.LanguageKey = "MATERIALREQUESTSTATE";
			this.grdMaterialListByRequest.Location = new System.Drawing.Point(2, 61);
			this.grdMaterialListByRequest.Margin = new System.Windows.Forms.Padding(0);
			this.grdMaterialListByRequest.Name = "grdMaterialListByRequest";
			this.grdMaterialListByRequest.ShowBorder = true;
			this.grdMaterialListByRequest.Size = new System.Drawing.Size(550, 337);
			this.grdMaterialListByRequest.TabIndex = 3;
			// 
			// pnlCheckAllReceive
			// 
			this.pnlCheckAllReceive.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pnlCheckAllReceive.Controls.Add(this.chkIsAllReceive);
			this.pnlCheckAllReceive.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlCheckAllReceive.Location = new System.Drawing.Point(2, 31);
			this.pnlCheckAllReceive.Margin = new System.Windows.Forms.Padding(0);
			this.pnlCheckAllReceive.Name = "pnlCheckAllReceive";
			this.pnlCheckAllReceive.Size = new System.Drawing.Size(550, 30);
			this.pnlCheckAllReceive.TabIndex = 2;
			// 
			// chkIsAllReceive
			// 
			this.chkIsAllReceive.LanguageKey = "ALLRECEIVE";
			this.chkIsAllReceive.Location = new System.Drawing.Point(10, 5);
			this.chkIsAllReceive.Margin = new System.Windows.Forms.Padding(0);
			this.chkIsAllReceive.Name = "chkIsAllReceive";
			this.chkIsAllReceive.Properties.Caption = "일괄인수";
			this.chkIsAllReceive.Size = new System.Drawing.Size(100, 19);
			this.chkIsAllReceive.TabIndex = 0;
			// 
			// smartSpliterControl1
			// 
			this.smartSpliterControl1.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smartSpliterControl1.Location = new System.Drawing.Point(0, 400);
			this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl1.Name = "smartSpliterControl1";
			this.smartSpliterControl1.Size = new System.Drawing.Size(554, 5);
			this.smartSpliterControl1.TabIndex = 1;
			this.smartSpliterControl1.TabStop = false;
			// 
			// grdConsumableLotList
			// 
			this.grdConsumableLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdConsumableLotList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdConsumableLotList.IsUsePaging = false;
			this.grdConsumableLotList.LanguageKey = "CONSUMABLELOTINFO";
			this.grdConsumableLotList.Location = new System.Drawing.Point(0, 405);
			this.grdConsumableLotList.Margin = new System.Windows.Forms.Padding(0);
			this.grdConsumableLotList.Name = "grdConsumableLotList";
			this.grdConsumableLotList.ShowBorder = true;
			this.grdConsumableLotList.Size = new System.Drawing.Size(554, 203);
			this.grdConsumableLotList.TabIndex = 2;
			// 
			// RecieveConsumable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(879, 657);
			this.Name = "RecieveConsumable";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
			this.smartGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlCheckAllReceive)).EndInit();
			this.pnlCheckAllReceive.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.chkIsAllReceive.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartGroupBox smartGroupBox1;
		private Framework.SmartControls.SmartBandedGrid grdConsumableLotList;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
		private Framework.SmartControls.SmartBandedGrid grdMaterialListByRequest;
		private Framework.SmartControls.SmartPanel pnlCheckAllReceive;
		private Framework.SmartControls.SmartCheckBox chkIsAllReceive;
	}
}