namespace Micube.SmartMES.Test
{
    partial class UserControlTest
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
            this.btnCancelWorkCompletion = new Micube.Framework.SmartControls.SmartButton();
            this.smartPictureEdit1 = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtConsumableLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.numUseQty = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.btnConsumeConsumableLot = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancelConsumeConsumableLot = new Micube.Framework.SmartControls.SmartButton();
            this.btnCreateConsumableLot = new Micube.Framework.SmartControls.SmartButton();
            this.btnSplitConsumableLot = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsumableLotId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUseQty.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 401);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(470, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.btnSplitConsumableLot);
            this.pnlContent.Controls.Add(this.btnCreateConsumableLot);
            this.pnlContent.Controls.Add(this.btnCancelConsumeConsumableLot);
            this.pnlContent.Controls.Add(this.btnConsumeConsumableLot);
            this.pnlContent.Controls.Add(this.numUseQty);
            this.pnlContent.Controls.Add(this.txtConsumableLotId);
            this.pnlContent.Controls.Add(this.txtLotId);
            this.pnlContent.Controls.Add(this.smartPictureEdit1);
            this.pnlContent.Controls.Add(this.btnCancelWorkCompletion);
            this.pnlContent.Size = new System.Drawing.Size(470, 396);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(780, 430);
            // 
            // btnCancelWorkCompletion
            // 
            this.btnCancelWorkCompletion.AllowFocus = false;
            this.btnCancelWorkCompletion.IsBusy = false;
            this.btnCancelWorkCompletion.IsWrite = false;
            this.btnCancelWorkCompletion.Location = new System.Drawing.Point(247, 85);
            this.btnCancelWorkCompletion.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancelWorkCompletion.Name = "btnCancelWorkCompletion";
            this.btnCancelWorkCompletion.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancelWorkCompletion.Size = new System.Drawing.Size(100, 20);
            this.btnCancelWorkCompletion.TabIndex = 0;
            this.btnCancelWorkCompletion.Text = "작업 완료 취소";
            this.btnCancelWorkCompletion.TooltipLanguageKey = "";
            this.btnCancelWorkCompletion.Click += new System.EventHandler(this.btnCancelWorkCompletion_Click);
            // 
            // smartPictureEdit1
            // 
            this.smartPictureEdit1.Location = new System.Drawing.Point(196, 225);
            this.smartPictureEdit1.Name = "smartPictureEdit1";
            this.smartPictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.smartPictureEdit1.Size = new System.Drawing.Size(100, 96);
            this.smartPictureEdit1.TabIndex = 1;
            // 
            // txtLotId
            // 
            this.txtLotId.LabelText = "Lot No.";
            this.txtLotId.LanguageKey = null;
            this.txtLotId.Location = new System.Drawing.Point(21, 85);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Size = new System.Drawing.Size(220, 20);
            this.txtLotId.TabIndex = 2;
            // 
            // txtConsumableLotId
            // 
            this.txtConsumableLotId.LabelText = "자재 Lot Id";
            this.txtConsumableLotId.LanguageKey = null;
            this.txtConsumableLotId.Location = new System.Drawing.Point(21, 143);
            this.txtConsumableLotId.Name = "txtConsumableLotId";
            this.txtConsumableLotId.Size = new System.Drawing.Size(220, 20);
            this.txtConsumableLotId.TabIndex = 3;
            // 
            // numUseQty
            // 
            this.numUseQty.LabelText = "소모수량";
            this.numUseQty.LanguageKey = null;
            this.numUseQty.Location = new System.Drawing.Point(21, 169);
            this.numUseQty.Name = "numUseQty";
            this.numUseQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numUseQty.Size = new System.Drawing.Size(220, 20);
            this.numUseQty.TabIndex = 4;
            // 
            // btnConsumeConsumableLot
            // 
            this.btnConsumeConsumableLot.AllowFocus = false;
            this.btnConsumeConsumableLot.IsBusy = false;
            this.btnConsumeConsumableLot.IsWrite = false;
            this.btnConsumeConsumableLot.Location = new System.Drawing.Point(247, 169);
            this.btnConsumeConsumableLot.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnConsumeConsumableLot.Name = "btnConsumeConsumableLot";
            this.btnConsumeConsumableLot.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnConsumeConsumableLot.Size = new System.Drawing.Size(80, 20);
            this.btnConsumeConsumableLot.TabIndex = 5;
            this.btnConsumeConsumableLot.Text = "자재소모";
            this.btnConsumeConsumableLot.TooltipLanguageKey = "";
            this.btnConsumeConsumableLot.Click += new System.EventHandler(this.btnConsumeConsumableLot_Click);
            // 
            // btnCancelConsumeConsumableLot
            // 
            this.btnCancelConsumeConsumableLot.AllowFocus = false;
            this.btnCancelConsumeConsumableLot.IsBusy = false;
            this.btnCancelConsumeConsumableLot.IsWrite = false;
            this.btnCancelConsumeConsumableLot.Location = new System.Drawing.Point(333, 169);
            this.btnCancelConsumeConsumableLot.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancelConsumeConsumableLot.Name = "btnCancelConsumeConsumableLot";
            this.btnCancelConsumeConsumableLot.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancelConsumeConsumableLot.Size = new System.Drawing.Size(80, 20);
            this.btnCancelConsumeConsumableLot.TabIndex = 6;
            this.btnCancelConsumeConsumableLot.Text = "자재소모취소";
            this.btnCancelConsumeConsumableLot.TooltipLanguageKey = "";
            this.btnCancelConsumeConsumableLot.Click += new System.EventHandler(this.btnCancelConsumeConsumableLot_Click);
            // 
            // btnCreateConsumableLot
            // 
            this.btnCreateConsumableLot.AllowFocus = false;
            this.btnCreateConsumableLot.IsBusy = false;
            this.btnCreateConsumableLot.IsWrite = false;
            this.btnCreateConsumableLot.Location = new System.Drawing.Point(247, 193);
            this.btnCreateConsumableLot.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCreateConsumableLot.Name = "btnCreateConsumableLot";
            this.btnCreateConsumableLot.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCreateConsumableLot.Size = new System.Drawing.Size(80, 20);
            this.btnCreateConsumableLot.TabIndex = 7;
            this.btnCreateConsumableLot.Text = "자재Lot생성";
            this.btnCreateConsumableLot.TooltipLanguageKey = "";
            this.btnCreateConsumableLot.Click += new System.EventHandler(this.btnCreateConsumableLot_Click);
            // 
            // btnSplitConsumableLot
            // 
            this.btnSplitConsumableLot.AllowFocus = false;
            this.btnSplitConsumableLot.IsBusy = false;
            this.btnSplitConsumableLot.IsWrite = false;
            this.btnSplitConsumableLot.Location = new System.Drawing.Point(333, 193);
            this.btnSplitConsumableLot.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSplitConsumableLot.Name = "btnSplitConsumableLot";
            this.btnSplitConsumableLot.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSplitConsumableLot.Size = new System.Drawing.Size(80, 20);
            this.btnSplitConsumableLot.TabIndex = 8;
            this.btnSplitConsumableLot.Text = "자재Lot분할";
            this.btnSplitConsumableLot.TooltipLanguageKey = "";
            this.btnSplitConsumableLot.Click += new System.EventHandler(this.btnSplitConsumableLot_Click);
            // 
            // UserControlTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "UserControlTest";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsumableLotId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUseQty.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartButton btnCancelWorkCompletion;
        private Framework.SmartControls.SmartPictureEdit smartPictureEdit1;
        private Framework.SmartControls.SmartLabelTextBox txtLotId;
        private Framework.SmartControls.SmartButton btnConsumeConsumableLot;
        private Framework.SmartControls.SmartLabelSpinEdit numUseQty;
        private Framework.SmartControls.SmartLabelTextBox txtConsumableLotId;
        private Framework.SmartControls.SmartButton btnCancelConsumeConsumableLot;
        private Framework.SmartControls.SmartButton btnCreateConsumableLot;
        private Framework.SmartControls.SmartButton btnSplitConsumableLot;
    }
}