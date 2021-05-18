namespace Micube.SmartMES.StandardInfo
{
	partial class SpecManagement
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
            this.grdChemicalSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabSpec = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgSpecClass = new DevExpress.XtraTab.XtraTabPage();
            this.grdSpecClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgChemical = new DevExpress.XtraTab.XtraTabPage();
            this.tpgWater = new DevExpress.XtraTab.XtraTabPage();
            this.grdWaterSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgOSP = new DevExpress.XtraTab.XtraTabPage();
            this.grdOSPSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgRaw = new DevExpress.XtraTab.XtraTabPage();
            this.grdRawSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgSubassembly = new DevExpress.XtraTab.XtraTabPage();
            this.tpgSubsidiary = new DevExpress.XtraTab.XtraTabPage();
            this.grdSubsidiarySpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgOperation = new DevExpress.XtraTab.XtraTabPage();
            this.grdOperationSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgEtchingrRate = new DevExpress.XtraTab.XtraTabPage();
            this.grdEtchingSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgShipment = new DevExpress.XtraTab.XtraTabPage();
            this.grdShipmentSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgMeasurement = new DevExpress.XtraTab.XtraTabPage();
            this.grdMeasurementSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdSubassemblySpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabSpec)).BeginInit();
            this.tabSpec.SuspendLayout();
            this.tpgSpecClass.SuspendLayout();
            this.tpgChemical.SuspendLayout();
            this.tpgWater.SuspendLayout();
            this.tpgOSP.SuspendLayout();
            this.tpgRaw.SuspendLayout();
            this.tpgSubassembly.SuspendLayout();
            this.tpgSubsidiary.SuspendLayout();
            this.tpgOperation.SuspendLayout();
            this.tpgEtchingrRate.SuspendLayout();
            this.tpgShipment.SuspendLayout();
            this.tpgMeasurement.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 447);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(709, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabSpec);
            this.pnlContent.Size = new System.Drawing.Size(709, 451);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1014, 480);
            // 
            // grdChemicalSpec
            // 
            this.grdChemicalSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdChemicalSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChemicalSpec.IsUsePaging = false;
            this.grdChemicalSpec.LanguageKey = "SPECMANAGEMENT";
            this.grdChemicalSpec.Location = new System.Drawing.Point(0, 0);
            this.grdChemicalSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdChemicalSpec.Name = "grdChemicalSpec";
            this.grdChemicalSpec.ShowBorder = false;
            this.grdChemicalSpec.ShowStatusBar = false;
            this.grdChemicalSpec.Size = new System.Drawing.Size(469, 372);
            this.grdChemicalSpec.TabIndex = 2;
            // 
            // tabSpec
            // 
            this.tabSpec.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tabSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSpec.Location = new System.Drawing.Point(0, 0);
            this.tabSpec.Name = "tabSpec";
            this.tabSpec.SelectedTabPage = this.tpgSpecClass;
            this.tabSpec.Size = new System.Drawing.Size(709, 451);
            this.tabSpec.TabIndex = 3;
            this.tabSpec.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgSpecClass,
            this.tpgChemical,
            this.tpgWater,
            this.tpgOSP,
            this.tpgRaw,
            this.tpgSubassembly,
            this.tpgSubsidiary,
            this.tpgOperation,
            this.tpgEtchingrRate,
            this.tpgShipment,
            this.tpgMeasurement});
            // 
            // tpgSpecClass
            // 
            this.tpgSpecClass.Controls.Add(this.grdSpecClass);
            this.tabSpec.SetLanguageKey(this.tpgSpecClass, "SPECCLASS");
            this.tpgSpecClass.Name = "tpgSpecClass";
            this.tpgSpecClass.Size = new System.Drawing.Size(703, 422);
            this.tpgSpecClass.Text = "SPECCLASS";
            // 
            // grdSpecClass
            // 
            this.grdSpecClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSpecClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSpecClass.IsUsePaging = false;
            this.grdSpecClass.LanguageKey = "SPECCLASSLIST";
            this.grdSpecClass.Location = new System.Drawing.Point(0, 0);
            this.grdSpecClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdSpecClass.Name = "grdSpecClass";
            this.grdSpecClass.ShowBorder = false;
            this.grdSpecClass.ShowStatusBar = false;
            this.grdSpecClass.Size = new System.Drawing.Size(703, 422);
            this.grdSpecClass.TabIndex = 0;
            // 
            // tpgChemical
            // 
            this.tpgChemical.Controls.Add(this.grdChemicalSpec);
            this.tabSpec.SetLanguageKey(this.tpgChemical, "CHEMICALSPEC");
            this.tpgChemical.Name = "tpgChemical";
            this.tpgChemical.Size = new System.Drawing.Size(469, 372);
            this.tpgChemical.Text = "CHEMICAL";
            // 
            // tpgWater
            // 
            this.tpgWater.Controls.Add(this.grdWaterSpec);
            this.tabSpec.SetLanguageKey(this.tpgWater, "WATERSPEC");
            this.tpgWater.Name = "tpgWater";
            this.tpgWater.Size = new System.Drawing.Size(469, 372);
            this.tpgWater.Text = "WATER";
            // 
            // grdWaterSpec
            // 
            this.grdWaterSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWaterSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWaterSpec.IsUsePaging = false;
            this.grdWaterSpec.LanguageKey = "SPECMANAGEMENT";
            this.grdWaterSpec.Location = new System.Drawing.Point(0, 0);
            this.grdWaterSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdWaterSpec.Name = "grdWaterSpec";
            this.grdWaterSpec.ShowBorder = false;
            this.grdWaterSpec.ShowStatusBar = false;
            this.grdWaterSpec.Size = new System.Drawing.Size(469, 372);
            this.grdWaterSpec.TabIndex = 0;
            // 
            // tpgOSP
            // 
            this.tpgOSP.Controls.Add(this.grdOSPSpec);
            this.tabSpec.SetLanguageKey(this.tpgOSP, "OSPSPEC");
            this.tpgOSP.Name = "tpgOSP";
            this.tpgOSP.Size = new System.Drawing.Size(469, 372);
            this.tpgOSP.Text = "OSP";
            // 
            // grdOSPSpec
            // 
            this.grdOSPSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdOSPSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOSPSpec.IsUsePaging = false;
            this.grdOSPSpec.LanguageKey = "SPECMANAGEMENT";
            this.grdOSPSpec.Location = new System.Drawing.Point(0, 0);
            this.grdOSPSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdOSPSpec.Name = "grdOSPSpec";
            this.grdOSPSpec.ShowBorder = false;
            this.grdOSPSpec.ShowStatusBar = false;
            this.grdOSPSpec.Size = new System.Drawing.Size(469, 372);
            this.grdOSPSpec.TabIndex = 0;
            // 
            // tpgRaw
            // 
            this.tpgRaw.Controls.Add(this.grdRawSpec);
            this.tabSpec.SetLanguageKey(this.tpgRaw, "RAWSPEC");
            this.tpgRaw.Name = "tpgRaw";
            this.tpgRaw.Size = new System.Drawing.Size(703, 422);
            this.tpgRaw.Text = "RAW";
            // 
            // grdRawSpec
            // 
            this.grdRawSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdRawSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRawSpec.IsUsePaging = false;
            this.grdRawSpec.LanguageKey = "SPECMANAGEMENT";
            this.grdRawSpec.Location = new System.Drawing.Point(0, 0);
            this.grdRawSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdRawSpec.Name = "grdRawSpec";
            this.grdRawSpec.ShowBorder = false;
            this.grdRawSpec.ShowStatusBar = false;
            this.grdRawSpec.Size = new System.Drawing.Size(703, 422);
            this.grdRawSpec.TabIndex = 0;
            // 
            // tpgSubassembly
            // 
            this.tpgSubassembly.Controls.Add(this.grdSubassemblySpec);
            this.tabSpec.SetLanguageKey(this.tpgSubassembly, "SUBASSEMBLYSPEC");
            this.tpgSubassembly.Name = "tpgSubassembly";
            this.tpgSubassembly.Size = new System.Drawing.Size(703, 422);
            this.tpgSubassembly.Text = "SUBASSEMBLY";
            // 
            // tpgSubsidiary
            // 
            this.tpgSubsidiary.Controls.Add(this.grdSubsidiarySpec);
            this.tabSpec.SetLanguageKey(this.tpgSubsidiary, "SUBSIDIARYSPEC");
            this.tpgSubsidiary.Name = "tpgSubsidiary";
            this.tpgSubsidiary.Size = new System.Drawing.Size(703, 422);
            this.tpgSubsidiary.Text = "SUBSIDIARY";
            // 
            // grdSubsidiarySpec
            // 
            this.grdSubsidiarySpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSubsidiarySpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSubsidiarySpec.IsUsePaging = false;
            this.grdSubsidiarySpec.LanguageKey = "SPECMANAGEMENT";
            this.grdSubsidiarySpec.Location = new System.Drawing.Point(0, 0);
            this.grdSubsidiarySpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdSubsidiarySpec.Name = "grdSubsidiarySpec";
            this.grdSubsidiarySpec.ShowBorder = false;
            this.grdSubsidiarySpec.ShowStatusBar = false;
            this.grdSubsidiarySpec.Size = new System.Drawing.Size(703, 422);
            this.grdSubsidiarySpec.TabIndex = 1;
            // 
            // tpgOperation
            // 
            this.tpgOperation.Controls.Add(this.grdOperationSpec);
            this.tabSpec.SetLanguageKey(this.tpgOperation, "OPERATIONSPEC");
            this.tpgOperation.Name = "tpgOperation";
            this.tpgOperation.Size = new System.Drawing.Size(469, 372);
            this.tpgOperation.Text = "OPERATION";
            // 
            // grdOperationSpec
            // 
            this.grdOperationSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdOperationSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOperationSpec.IsUsePaging = false;
            this.grdOperationSpec.LanguageKey = "SPECMANAGEMENT";
            this.grdOperationSpec.Location = new System.Drawing.Point(0, 0);
            this.grdOperationSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdOperationSpec.Name = "grdOperationSpec";
            this.grdOperationSpec.ShowBorder = false;
            this.grdOperationSpec.ShowStatusBar = false;
            this.grdOperationSpec.Size = new System.Drawing.Size(469, 372);
            this.grdOperationSpec.TabIndex = 1;
            // 
            // tpgEtchingrRate
            // 
            this.tpgEtchingrRate.Controls.Add(this.grdEtchingSpec);
            this.tabSpec.SetLanguageKey(this.tpgEtchingrRate, "ETCHINGRATESPEC");
            this.tpgEtchingrRate.Name = "tpgEtchingrRate";
            this.tpgEtchingrRate.PageVisible = false;
            this.tpgEtchingrRate.Size = new System.Drawing.Size(469, 372);
            this.tpgEtchingrRate.Text = "ETCHINGRATE";
            // 
            // grdEtchingSpec
            // 
            this.grdEtchingSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEtchingSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEtchingSpec.IsUsePaging = false;
            this.grdEtchingSpec.LanguageKey = "SPECMANAGEMENT";
            this.grdEtchingSpec.Location = new System.Drawing.Point(0, 0);
            this.grdEtchingSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdEtchingSpec.Name = "grdEtchingSpec";
            this.grdEtchingSpec.ShowBorder = false;
            this.grdEtchingSpec.ShowStatusBar = false;
            this.grdEtchingSpec.Size = new System.Drawing.Size(469, 372);
            this.grdEtchingSpec.TabIndex = 2;
            // 
            // tpgShipment
            // 
            this.tpgShipment.Controls.Add(this.grdShipmentSpec);
            this.tabSpec.SetLanguageKey(this.tpgShipment, "SHIPMENTSPEC");
            this.tpgShipment.Name = "tpgShipment";
            this.tpgShipment.Size = new System.Drawing.Size(469, 372);
            this.tpgShipment.Text = "SHIPMENT";
            // 
            // grdShipmentSpec
            // 
            this.grdShipmentSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdShipmentSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdShipmentSpec.IsUsePaging = false;
            this.grdShipmentSpec.LanguageKey = "SPECMANAGEMENT";
            this.grdShipmentSpec.Location = new System.Drawing.Point(0, 0);
            this.grdShipmentSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdShipmentSpec.Name = "grdShipmentSpec";
            this.grdShipmentSpec.ShowBorder = false;
            this.grdShipmentSpec.ShowStatusBar = false;
            this.grdShipmentSpec.Size = new System.Drawing.Size(469, 372);
            this.grdShipmentSpec.TabIndex = 1;
            // 
            // tpgMeasurement
            // 
            this.tpgMeasurement.Controls.Add(this.grdMeasurementSpec);
            this.tabSpec.SetLanguageKey(this.tpgMeasurement, "MEASUREMENTSPEC");
            this.tpgMeasurement.Name = "tpgMeasurement";
            this.tpgMeasurement.Size = new System.Drawing.Size(469, 372);
            this.tpgMeasurement.Text = "MAESUREMENT";
            // 
            // grdMeasurementSpec
            // 
            this.grdMeasurementSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMeasurementSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMeasurementSpec.IsUsePaging = false;
            this.grdMeasurementSpec.LanguageKey = "SPECMANAGEMENT";
            this.grdMeasurementSpec.Location = new System.Drawing.Point(0, 0);
            this.grdMeasurementSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdMeasurementSpec.Name = "grdMeasurementSpec";
            this.grdMeasurementSpec.ShowBorder = false;
            this.grdMeasurementSpec.ShowStatusBar = false;
            this.grdMeasurementSpec.Size = new System.Drawing.Size(469, 372);
            this.grdMeasurementSpec.TabIndex = 1;
            // 
            // grdSubassemblySpec
            // 
            this.grdSubassemblySpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSubassemblySpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSubassemblySpec.IsUsePaging = false;
            this.grdSubassemblySpec.LanguageKey = "SPECMANAGEMENT";
            this.grdSubassemblySpec.Location = new System.Drawing.Point(0, 0);
            this.grdSubassemblySpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdSubassemblySpec.Name = "grdSubassemblySpec";
            this.grdSubassemblySpec.ShowBorder = false;
            this.grdSubassemblySpec.ShowStatusBar = false;
            this.grdSubassemblySpec.Size = new System.Drawing.Size(703, 422);
            this.grdSubassemblySpec.TabIndex = 1;
            // 
            // SpecManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 500);
            this.Name = "SpecManagement";
            this.Text = "SpecManagement";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabSpec)).EndInit();
            this.tabSpec.ResumeLayout(false);
            this.tpgSpecClass.ResumeLayout(false);
            this.tpgChemical.ResumeLayout(false);
            this.tpgWater.ResumeLayout(false);
            this.tpgOSP.ResumeLayout(false);
            this.tpgRaw.ResumeLayout(false);
            this.tpgSubassembly.ResumeLayout(false);
            this.tpgSubsidiary.ResumeLayout(false);
            this.tpgOperation.ResumeLayout(false);
            this.tpgEtchingrRate.ResumeLayout(false);
            this.tpgShipment.ResumeLayout(false);
            this.tpgMeasurement.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdChemicalSpec;
        private Framework.SmartControls.SmartTabControl tabSpec;
        private DevExpress.XtraTab.XtraTabPage tpgSpecClass;
        private Framework.SmartControls.SmartBandedGrid grdSpecClass;
        private DevExpress.XtraTab.XtraTabPage tpgChemical;
        private DevExpress.XtraTab.XtraTabPage tpgOSP;
        private Framework.SmartControls.SmartBandedGrid grdOSPSpec;
        private DevExpress.XtraTab.XtraTabPage tpgRaw;
        private Framework.SmartControls.SmartBandedGrid grdRawSpec;
        private DevExpress.XtraTab.XtraTabPage tpgOperation;
        private DevExpress.XtraTab.XtraTabPage tpgShipment;
        private DevExpress.XtraTab.XtraTabPage tpgMeasurement;
        private Framework.SmartControls.SmartBandedGrid grdOperationSpec;
        private Framework.SmartControls.SmartBandedGrid grdShipmentSpec;
        private Framework.SmartControls.SmartBandedGrid grdMeasurementSpec;
        private DevExpress.XtraTab.XtraTabPage tpgEtchingrRate;
        private Framework.SmartControls.SmartBandedGrid grdEtchingSpec;
        private DevExpress.XtraTab.XtraTabPage tpgWater;
        private Framework.SmartControls.SmartBandedGrid grdWaterSpec;
        private DevExpress.XtraTab.XtraTabPage tpgSubassembly;
        private DevExpress.XtraTab.XtraTabPage tpgSubsidiary;
        private Framework.SmartControls.SmartBandedGrid grdSubsidiarySpec;
        private Framework.SmartControls.SmartBandedGrid grdSubassemblySpec;
    }
}