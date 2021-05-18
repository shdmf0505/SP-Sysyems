namespace Micube.SmartMES.SystemManagement
{
    partial class ConditionInput_Popup
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
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.grdConditionInput = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panButton = new Micube.Framework.SmartControls.SmartPanel();
            this.btnConfirm = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.panMain = new Micube.Framework.SmartControls.SmartPanel();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.grdReservedWord = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.txtUseExample = new Micube.Framework.SmartControls.SmartTextBox();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).BeginInit();
            this.panButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).BeginInit();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUseExample.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.panMain);
            this.pnlMain.Controls.Add(this.panButton);
            this.pnlMain.Size = new System.Drawing.Size(564, 342);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl1.Location = new System.Drawing.Point(354, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(10, 308);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // grdConditionInput
            // 
            this.grdConditionInput.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdConditionInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdConditionInput.IsUsePaging = false;
            this.grdConditionInput.LanguageKey = null;
            this.grdConditionInput.Location = new System.Drawing.Point(0, 0);
            this.grdConditionInput.Margin = new System.Windows.Forms.Padding(0);
            this.grdConditionInput.Name = "grdConditionInput";
            this.grdConditionInput.ShowBorder = true;
            this.grdConditionInput.Size = new System.Drawing.Size(354, 308);
            this.grdConditionInput.TabIndex = 2;
            // 
            // panButton
            // 
            this.panButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panButton.Controls.Add(this.btnConfirm);
            this.panButton.Controls.Add(this.btnCancel);
            this.panButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panButton.Location = new System.Drawing.Point(0, 308);
            this.panButton.Margin = new System.Windows.Forms.Padding(0);
            this.panButton.Name = "panButton";
            this.panButton.Size = new System.Drawing.Size(564, 34);
            this.panButton.TabIndex = 3;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AllowFocus = false;
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.IsBusy = false;
            this.btnConfirm.LanguageKey = "CONFIRM";
            this.btnConfirm.Location = new System.Drawing.Point(404, 10);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "확인";
            this.btnConfirm.TooltipLanguageKey = "";
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.IsBusy = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(489, 10);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취소";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // panMain
            // 
            this.panMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panMain.Controls.Add(this.grdConditionInput);
            this.panMain.Controls.Add(this.splitterControl1);
            this.panMain.Controls.Add(this.smartPanel1);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Margin = new System.Windows.Forms.Padding(0);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(564, 308);
            this.panMain.TabIndex = 4;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.grdReservedWord);
            this.smartPanel1.Controls.Add(this.smartPanel2);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smartPanel1.Location = new System.Drawing.Point(364, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(200, 308);
            this.smartPanel1.TabIndex = 0;
            // 
            // grdReservedWord
            // 
            this.grdReservedWord.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdReservedWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReservedWord.IsUsePaging = false;
            this.grdReservedWord.LanguageKey = "GRIDRESERVEDWORDLIST";
            this.grdReservedWord.Location = new System.Drawing.Point(0, 0);
            this.grdReservedWord.Margin = new System.Windows.Forms.Padding(0);
            this.grdReservedWord.Name = "grdReservedWord";
            this.grdReservedWord.ShowBorder = true;
            this.grdReservedWord.Size = new System.Drawing.Size(200, 282);
            this.grdReservedWord.TabIndex = 2;
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.txtUseExample);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartPanel2.Location = new System.Drawing.Point(0, 282);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(200, 26);
            this.smartPanel2.TabIndex = 1;
            // 
            // txtUseExample
            // 
            this.txtUseExample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUseExample.LabelText = null;
            this.txtUseExample.LanguageKey = null;
            this.txtUseExample.Location = new System.Drawing.Point(0, 0);
            this.txtUseExample.Margin = new System.Windows.Forms.Padding(0);
            this.txtUseExample.Name = "txtUseExample";
            this.txtUseExample.Properties.AutoHeight = false;
            this.txtUseExample.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtUseExample.Properties.ReadOnly = true;
            this.txtUseExample.Size = new System.Drawing.Size(200, 26);
            this.txtUseExample.TabIndex = 0;
            // 
            // ConditionInput_Popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Name = "ConditionInput_Popup";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 9);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConditionInput_Popup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).EndInit();
            this.panButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).EndInit();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUseExample.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel panMain;
        private Framework.SmartControls.SmartBandedGrid grdConditionInput;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private Framework.SmartControls.SmartPanel panButton;
        private Framework.SmartControls.SmartButton btnConfirm;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartTextBox txtUseExample;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private Framework.SmartControls.SmartBandedGrid grdReservedWord;
    }
}