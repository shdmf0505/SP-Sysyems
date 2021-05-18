namespace Micube.SmartMES.SystemManagement
{
	partial class IdDefinitionManagement
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
			this.tabIdManagement = new Micube.Framework.SmartControls.SmartTabControl();
			this.tnpIdClass = new DevExpress.XtraTab.XtraTabPage();
			this.grdIdClassList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.tnpIdDefinition = new DevExpress.XtraTab.XtraTabPage();
			this.grdIdDefinitionList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.grdIdSerialList = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabIdManagement)).BeginInit();
			this.tabIdManagement.SuspendLayout();
			this.tnpIdClass.SuspendLayout();
			this.tnpIdDefinition.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 472);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(457, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.tabIdManagement);
			this.pnlContent.Size = new System.Drawing.Size(457, 476);
			// 
			// pnlMain
			// 
			this.pnlMain.Location = new System.Drawing.Point(19, 19);
			this.pnlMain.Size = new System.Drawing.Size(762, 505);
			// 
			// tabIdManagement
			// 
			this.tabIdManagement.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabIdManagement.Location = new System.Drawing.Point(0, 0);
			this.tabIdManagement.Name = "tabIdManagement";
			this.tabIdManagement.SelectedTabPage = this.tnpIdClass;
			this.tabIdManagement.Size = new System.Drawing.Size(457, 476);
			this.tabIdManagement.TabIndex = 0;
			this.tabIdManagement.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tnpIdClass,
            this.tnpIdDefinition});
			// 
			// tnpIdClass
			// 
			this.tnpIdClass.Controls.Add(this.grdIdClassList);
			this.tabIdManagement.SetLanguageKey(this.tnpIdClass, "IDCLASSID");
			this.tnpIdClass.Name = "tnpIdClass";
			this.tnpIdClass.Size = new System.Drawing.Size(451, 447);
			this.tnpIdClass.Text = "ID Class";
			// 
			// grdIdClassList
			// 
			this.grdIdClassList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdIdClassList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdIdClassList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdIdClassList.IsUsePaging = false;
			this.grdIdClassList.LanguageKey = "IDCLASSINFOLIST";
			this.grdIdClassList.Location = new System.Drawing.Point(0, 0);
			this.grdIdClassList.Margin = new System.Windows.Forms.Padding(0);
			this.grdIdClassList.Name = "grdIdClassList";
			this.grdIdClassList.ShowBorder = true;
			this.grdIdClassList.ShowStatusBar = false;
			this.grdIdClassList.Size = new System.Drawing.Size(451, 447);
			this.grdIdClassList.TabIndex = 0;
			// 
			// tnpIdDefinition
			// 
			this.tnpIdDefinition.Controls.Add(this.grdIdDefinitionList);
			this.tnpIdDefinition.Controls.Add(this.smartSpliterControl1);
			this.tnpIdDefinition.Controls.Add(this.grdIdSerialList);
			this.tabIdManagement.SetLanguageKey(this.tnpIdDefinition, "IDDEFID");
			this.tnpIdDefinition.Name = "tnpIdDefinition";
			this.tnpIdDefinition.Size = new System.Drawing.Size(451, 447);
			this.tnpIdDefinition.Text = "ID Definition";
			// 
			// grdIdDefinitionList
			// 
			this.grdIdDefinitionList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdIdDefinitionList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdIdDefinitionList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdIdDefinitionList.IsUsePaging = false;
			this.grdIdDefinitionList.LanguageKey = "IDDEFINITIONINFOLIST";
			this.grdIdDefinitionList.Location = new System.Drawing.Point(0, 0);
			this.grdIdDefinitionList.Margin = new System.Windows.Forms.Padding(0);
			this.grdIdDefinitionList.Name = "grdIdDefinitionList";
			this.grdIdDefinitionList.ShowBorder = true;
			this.grdIdDefinitionList.ShowStatusBar = false;
			this.grdIdDefinitionList.Size = new System.Drawing.Size(451, 92);
			this.grdIdDefinitionList.TabIndex = 0;
			// 
			// smartSpliterControl1
			// 
			this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.smartSpliterControl1.Location = new System.Drawing.Point(0, 92);
			this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl1.Name = "smartSpliterControl1";
			this.smartSpliterControl1.Size = new System.Drawing.Size(451, 5);
			this.smartSpliterControl1.TabIndex = 2;
			this.smartSpliterControl1.TabStop = false;
			// 
			// grdIdSerialList
			// 
			this.grdIdSerialList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdIdSerialList.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.grdIdSerialList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdIdSerialList.IsUsePaging = false;
			this.grdIdSerialList.LanguageKey = "IDSERIALLIST";
			this.grdIdSerialList.Location = new System.Drawing.Point(0, 97);
			this.grdIdSerialList.Margin = new System.Windows.Forms.Padding(0);
			this.grdIdSerialList.Name = "grdIdSerialList";
			this.grdIdSerialList.ShowBorder = true;
			this.grdIdSerialList.Size = new System.Drawing.Size(451, 350);
			this.grdIdSerialList.TabIndex = 1;
			// 
			// IdDefinitionManagement
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 543);
			this.Name = "IdDefinitionManagement";
			this.Padding = new System.Windows.Forms.Padding(19);
			this.Text = "IdDefinitionManagement";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabIdManagement)).EndInit();
			this.tabIdManagement.ResumeLayout(false);
			this.tnpIdClass.ResumeLayout(false);
			this.tnpIdDefinition.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartTabControl tabIdManagement;
		private DevExpress.XtraTab.XtraTabPage tnpIdClass;
		private DevExpress.XtraTab.XtraTabPage tnpIdDefinition;
		private Framework.SmartControls.SmartBandedGrid grdIdClassList;
		private Framework.SmartControls.SmartBandedGrid grdIdDefinitionList;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
		private Framework.SmartControls.SmartBandedGrid grdIdSerialList;
	}
}