namespace Micube.SmartMES.StandardInfo
{
	partial class MaterialItemReplacementPopup
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
            this.tplReplacement = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtReplaceMaterial = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtSpec = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtAssemblyItemName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.pnlButton = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnConfrim = new Micube.Framework.SmartControls.SmartButton();
            this.popMaterialCode = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtHorizontal = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtVertical = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tplReplacement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReplaceMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpec.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssemblyItemName.Properties)).BeginInit();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popMaterialCode.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHorizontal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVertical.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tplReplacement);
            this.pnlMain.Size = new System.Drawing.Size(399, 179);
            // 
            // tplReplacement
            // 
            this.tplReplacement.ColumnCount = 2;
            this.tplReplacement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tplReplacement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplReplacement.Controls.Add(this.txtReplaceMaterial, 0, 4);
            this.tplReplacement.Controls.Add(this.txtSpec, 0, 2);
            this.tplReplacement.Controls.Add(this.txtAssemblyItemName, 0, 1);
            this.tplReplacement.Controls.Add(this.pnlButton, 1, 5);
            this.tplReplacement.Controls.Add(this.popMaterialCode, 0, 0);
            this.tplReplacement.Controls.Add(this.smartSplitTableLayoutPanel1, 0, 3);
            this.tplReplacement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplReplacement.Location = new System.Drawing.Point(0, 0);
            this.tplReplacement.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplReplacement.Name = "tplReplacement";
            this.tplReplacement.RowCount = 6;
            this.tplReplacement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tplReplacement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tplReplacement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tplReplacement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tplReplacement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tplReplacement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tplReplacement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplReplacement.Size = new System.Drawing.Size(399, 179);
            this.tplReplacement.TabIndex = 0;
            // 
            // txtReplaceMaterial
            // 
            this.txtReplaceMaterial.AutoHeight = false;
            this.tplReplacement.SetColumnSpan(this.txtReplaceMaterial, 2);
            this.txtReplaceMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReplaceMaterial.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtReplaceMaterial.LabelText = "가공대체품목코드";
            this.txtReplaceMaterial.LabelWidth = "18%";
            this.txtReplaceMaterial.LanguageKey = "REPLACECODE";
            this.txtReplaceMaterial.Location = new System.Drawing.Point(3, 119);
            this.txtReplaceMaterial.Name = "txtReplaceMaterial";
            this.txtReplaceMaterial.Properties.AutoHeight = false;
            this.txtReplaceMaterial.Properties.ReadOnly = true;
            this.txtReplaceMaterial.Size = new System.Drawing.Size(393, 23);
            this.txtReplaceMaterial.TabIndex = 5;
            // 
            // txtSpec
            // 
            this.txtSpec.AutoHeight = false;
            this.tplReplacement.SetColumnSpan(this.txtSpec, 2);
            this.txtSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSpec.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSpec.LabelText = "규격";
            this.txtSpec.LabelWidth = "18%";
            this.txtSpec.LanguageKey = "SPEC";
            this.txtSpec.Location = new System.Drawing.Point(3, 61);
            this.txtSpec.Name = "txtSpec";
            this.txtSpec.Properties.AutoHeight = false;
            this.txtSpec.Properties.ReadOnly = true;
            this.txtSpec.Size = new System.Drawing.Size(393, 23);
            this.txtSpec.TabIndex = 3;
            // 
            // txtAssemblyItemName
            // 
            this.txtAssemblyItemName.AutoHeight = false;
            this.tplReplacement.SetColumnSpan(this.txtAssemblyItemName, 2);
            this.txtAssemblyItemName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAssemblyItemName.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtAssemblyItemName.LabelText = "품명";
            this.txtAssemblyItemName.LabelWidth = "18%";
            this.txtAssemblyItemName.LanguageKey = "ASSEMBLYITEMNAME";
            this.txtAssemblyItemName.Location = new System.Drawing.Point(3, 32);
            this.txtAssemblyItemName.Name = "txtAssemblyItemName";
            this.txtAssemblyItemName.Properties.AutoHeight = false;
            this.txtAssemblyItemName.Properties.ReadOnly = true;
            this.txtAssemblyItemName.Size = new System.Drawing.Size(393, 23);
            this.txtAssemblyItemName.TabIndex = 2;
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Controls.Add(this.btnConfrim);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButton.Location = new System.Drawing.Point(103, 148);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(293, 28);
            this.pnlButton.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.Location = new System.Drawing.Point(215, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "닫기";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnConfrim
            // 
            this.btnConfrim.AllowFocus = false;
            this.btnConfrim.IsBusy = false;
            this.btnConfrim.IsWrite = false;
            this.btnConfrim.Location = new System.Drawing.Point(134, 0);
            this.btnConfrim.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnConfrim.Name = "btnConfrim";
            this.btnConfrim.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnConfrim.Size = new System.Drawing.Size(75, 25);
            this.btnConfrim.TabIndex = 1;
            this.btnConfrim.Text = "확인";
            this.btnConfrim.TooltipLanguageKey = "";
            // 
            // popMaterialCode
            // 
            this.popMaterialCode.AutoHeight = false;
            this.tplReplacement.SetColumnSpan(this.popMaterialCode, 2);
            this.popMaterialCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popMaterialCode.Enabled = false;
            this.popMaterialCode.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.popMaterialCode.LabelText = "원자재코드";
            this.popMaterialCode.LabelWidth = "18%";
            this.popMaterialCode.LanguageKey = "RAWMATERIALCODE";
            this.popMaterialCode.Location = new System.Drawing.Point(3, 3);
            this.popMaterialCode.Name = "popMaterialCode";
            this.popMaterialCode.Properties.AutoHeight = false;
            this.popMaterialCode.Properties.ReadOnly = true;
            this.popMaterialCode.Size = new System.Drawing.Size(393, 23);
            this.popMaterialCode.TabIndex = 6;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 4;
            this.tplReplacement.SetColumnSpan(this.smartSplitTableLayoutPanel1, 2);
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 520F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtHorizontal, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartLabel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtVertical, 3, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartLabel2, 2, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(399, 29);
            this.smartSplitTableLayoutPanel1.TabIndex = 7;
            this.smartSplitTableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.smartSplitTableLayoutPanel1_Paint);
            // 
            // txtHorizontal
            // 
            this.txtHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHorizontal.LabelText = null;
            this.txtHorizontal.LanguageKey = null;
            this.txtHorizontal.Location = new System.Drawing.Point(101, 3);
            this.txtHorizontal.Name = "txtHorizontal";
            this.txtHorizontal.Properties.AutoHeight = false;
            this.txtHorizontal.Properties.ReadOnly = true;
            this.txtHorizontal.Size = new System.Drawing.Size(135, 23);
            this.txtHorizontal.TabIndex = 2;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.Options.UseTextOptions = true;
            this.smartLabel1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.smartLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel1.LanguageKey = "HORIZONTALVERTICAL";
            this.smartLabel1.Location = new System.Drawing.Point(3, 3);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(92, 23);
            this.smartLabel1.TabIndex = 0;
            this.smartLabel1.Text = "가로 * 세로";
            // 
            // txtVertical
            // 
            this.txtVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVertical.LabelText = null;
            this.txtVertical.LanguageKey = null;
            this.txtVertical.Location = new System.Drawing.Point(263, 3);
            this.txtVertical.Name = "txtVertical";
            this.txtVertical.Properties.AutoHeight = false;
            this.txtVertical.Properties.ReadOnly = true;
            this.txtVertical.Size = new System.Drawing.Size(514, 23);
            this.txtVertical.TabIndex = 1;
            // 
            // smartLabel2
            // 
            this.smartLabel2.Appearance.Options.UseTextOptions = true;
            this.smartLabel2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.smartLabel2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.smartLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel2.Location = new System.Drawing.Point(242, 3);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(15, 23);
            this.smartLabel2.TabIndex = 3;
            this.smartLabel2.Text = "*";
            // 
            // MaterialItemReplacementPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 199);
            this.Name = "MaterialItemReplacementPopup";
            this.Text = "MaterialItemReplacementPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tplReplacement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReplaceMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpec.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssemblyItemName.Properties)).EndInit();
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popMaterialCode.Properties)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHorizontal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVertical.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel tplReplacement;
		private System.Windows.Forms.FlowLayoutPanel pnlButton;
		private Framework.SmartControls.SmartButton btnCancel;
		private Framework.SmartControls.SmartButton btnConfrim;
		private Framework.SmartControls.SmartLabelTextBox txtReplaceMaterial;
		private Framework.SmartControls.SmartLabelTextBox txtSpec;
		private Framework.SmartControls.SmartLabelTextBox txtAssemblyItemName;
		private Framework.SmartControls.SmartLabelSelectPopupEdit popMaterialCode;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartTextBox txtHorizontal;
        private Framework.SmartControls.SmartTextBox txtVertical;
        private Framework.SmartControls.SmartLabel smartLabel2;
    }
}