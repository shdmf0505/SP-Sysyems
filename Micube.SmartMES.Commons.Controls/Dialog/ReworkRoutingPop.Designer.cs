namespace Micube.SmartMES.Commons.Controls
{
    partial class ReworkRoutingPop
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
            this.pnlTop = new Micube.Framework.SmartControls.SmartPanel();
            this.lblProductDefName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.lblProductDefID = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.pnlBottom = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.ucReworkRouting = new Micube.SmartMES.Commons.Controls.ucReworkRouting();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblProductDefName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProductDefID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.ucReworkRouting);
            this.pnlMain.Controls.Add(this.pnlBottom);
            this.pnlMain.Controls.Add(this.pnlTop);
            this.pnlMain.Size = new System.Drawing.Size(1164, 654);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblProductDefName);
            this.pnlTop.Controls.Add(this.lblProductDefID);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1164, 34);
            this.pnlTop.TabIndex = 0;
            // 
            // lblProductDefName
            // 
            this.lblProductDefName.Enabled = false;
            this.lblProductDefName.LabelText = "품목명";
            this.lblProductDefName.LabelWidth = "20%";
            this.lblProductDefName.LanguageKey = "PRODUCTDEFNAME";
            this.lblProductDefName.Location = new System.Drawing.Point(318, 7);
            this.lblProductDefName.Name = "lblProductDefName";
            this.lblProductDefName.Size = new System.Drawing.Size(392, 20);
            this.lblProductDefName.TabIndex = 1;
            // 
            // lblProductDefID
            // 
            this.lblProductDefID.EditorWidth = "70%";
            this.lblProductDefID.Enabled = false;
            this.lblProductDefID.LabelText = "품목 코드";
            this.lblProductDefID.LabelWidth = "28%";
            this.lblProductDefID.LanguageKey = "PRODUCTDEFID";
            this.lblProductDefID.Location = new System.Drawing.Point(10, 7);
            this.lblProductDefID.Name = "lblProductDefID";
            this.lblProductDefID.Size = new System.Drawing.Size(289, 20);
            this.lblProductDefID.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 612);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1164, 42);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "PopupSelect";
            this.btnSave.Location = new System.Drawing.Point(993, 8);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "선 택";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(1079, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취 소";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // ucReworkRouting
            // 
            this.ucReworkRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucReworkRouting.Location = new System.Drawing.Point(0, 34);
            this.ucReworkRouting.Name = "ucReworkRouting";
            this.ucReworkRouting.ReworkTypeVisible = true;
            this.ucReworkRouting.Size = new System.Drawing.Size(1164, 578);
            this.ucReworkRouting.SplitterPosition = 609;
            this.ucReworkRouting.TabIndex = 2;
            // 
            // ReworkRoutingPop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 674);
            this.LanguageKey = "SELECTREWORKROUTING";
            this.Name = "ReworkRoutingPop";
            this.Text = "ReworkRoutingPop";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblProductDefName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProductDefID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartPanel pnlBottom;
        private Framework.SmartControls.SmartPanel pnlTop;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartLabelTextBox lblProductDefName;
        private Framework.SmartControls.SmartLabelTextBox lblProductDefID;
        private Commons.Controls.ucReworkRouting ucReworkRouting;
    }
}