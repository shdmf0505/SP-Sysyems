namespace Micube.SmartMES.EquipmentManagement
{
    partial class RegistPMPlan
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
            this.grdPMPlan = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel4 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnAutoCreate = new Micube.Framework.SmartControls.SmartButton();
            this.btnPublishMaintOrder = new Micube.Framework.SmartControls.SmartButton();
            this.btnCreateSchedule = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancelMaintOrder = new Micube.Framework.SmartControls.SmartButton();
            this.lblInputTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).BeginInit();
            this.smartPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 722);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1193, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdPMPlan);
            this.pnlContent.Controls.Add(this.smartPanel4);
            this.pnlContent.Size = new System.Drawing.Size(1193, 726);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1498, 755);
            // 
            // grdPMPlan
            // 
            this.grdPMPlan.Caption = "PM 계획목록";
            this.grdPMPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPMPlan.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPMPlan.IsUsePaging = false;
            this.grdPMPlan.LanguageKey = "PMPLANLIST";
            this.grdPMPlan.Location = new System.Drawing.Point(0, 36);
            this.grdPMPlan.Margin = new System.Windows.Forms.Padding(0);
            this.grdPMPlan.Name = "grdPMPlan";
            this.grdPMPlan.ShowBorder = true;
            this.grdPMPlan.Size = new System.Drawing.Size(1193, 690);
            this.grdPMPlan.TabIndex = 104;
            // 
            // smartPanel4
            // 
            this.smartPanel4.Controls.Add(this.btnAutoCreate);
            this.smartPanel4.Controls.Add(this.btnPublishMaintOrder);
            this.smartPanel4.Controls.Add(this.btnCreateSchedule);
            this.smartPanel4.Controls.Add(this.btnCancelMaintOrder);
            this.smartPanel4.Controls.Add(this.lblInputTitle);
            this.smartPanel4.Controls.Add(this.btnSave);
            this.smartPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartPanel4.Name = "smartPanel4";
            this.smartPanel4.Size = new System.Drawing.Size(1193, 36);
            this.smartPanel4.TabIndex = 103;
            // 
            // btnAutoCreate
            // 
            this.btnAutoCreate.AllowFocus = false;
            this.btnAutoCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoCreate.IsBusy = false;
            this.btnAutoCreate.IsWrite = false;
            this.btnAutoCreate.LanguageKey = "PMAUTOCREATE";
            this.btnAutoCreate.Location = new System.Drawing.Point(633, 5);
            this.btnAutoCreate.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnAutoCreate.Name = "btnAutoCreate";
            this.btnAutoCreate.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAutoCreate.Size = new System.Drawing.Size(114, 25);
            this.btnAutoCreate.TabIndex = 115;
            this.btnAutoCreate.Text = "자동생성:";
            this.btnAutoCreate.TooltipLanguageKey = "";
            this.btnAutoCreate.Visible = false;
            // 
            // btnPublishMaintOrder
            // 
            this.btnPublishMaintOrder.AllowFocus = false;
            this.btnPublishMaintOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPublishMaintOrder.IsBusy = false;
            this.btnPublishMaintOrder.IsWrite = false;
            this.btnPublishMaintOrder.LanguageKey = "PUBLISHWORKORDER";
            this.btnPublishMaintOrder.Location = new System.Drawing.Point(959, 5);
            this.btnPublishMaintOrder.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPublishMaintOrder.Name = "btnPublishMaintOrder";
            this.btnPublishMaintOrder.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPublishMaintOrder.Size = new System.Drawing.Size(108, 25);
            this.btnPublishMaintOrder.TabIndex = 114;
            this.btnPublishMaintOrder.Text = "보전Order발행:";
            this.btnPublishMaintOrder.TooltipLanguageKey = "";
            this.btnPublishMaintOrder.Visible = false;
            // 
            // btnCreateSchedule
            // 
            this.btnCreateSchedule.AllowFocus = false;
            this.btnCreateSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateSchedule.IsBusy = false;
            this.btnCreateSchedule.IsWrite = false;
            this.btnCreateSchedule.LanguageKey = "CREATESCHEDULE";
            this.btnCreateSchedule.Location = new System.Drawing.Point(753, 5);
            this.btnCreateSchedule.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCreateSchedule.Name = "btnCreateSchedule";
            this.btnCreateSchedule.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCreateSchedule.Size = new System.Drawing.Size(114, 25);
            this.btnCreateSchedule.TabIndex = 111;
            this.btnCreateSchedule.Text = "스케쥴 생성:";
            this.btnCreateSchedule.TooltipLanguageKey = "";
            this.btnCreateSchedule.Visible = false;
            // 
            // btnCancelMaintOrder
            // 
            this.btnCancelMaintOrder.AllowFocus = false;
            this.btnCancelMaintOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelMaintOrder.IsBusy = false;
            this.btnCancelMaintOrder.IsWrite = false;
            this.btnCancelMaintOrder.LanguageKey = "CANCELWORKORDER";
            this.btnCancelMaintOrder.Location = new System.Drawing.Point(1073, 5);
            this.btnCancelMaintOrder.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancelMaintOrder.Name = "btnCancelMaintOrder";
            this.btnCancelMaintOrder.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancelMaintOrder.Size = new System.Drawing.Size(114, 25);
            this.btnCancelMaintOrder.TabIndex = 111;
            this.btnCancelMaintOrder.Text = "보전Order취소";
            this.btnCancelMaintOrder.TooltipLanguageKey = "";
            this.btnCancelMaintOrder.Visible = false;
            // 
            // lblInputTitle
            // 
            this.lblInputTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblInputTitle.Appearance.Options.UseFont = true;
            this.lblInputTitle.LanguageKey = "PMPLANLIST";
            this.lblInputTitle.Location = new System.Drawing.Point(5, 5);
            this.lblInputTitle.Name = "lblInputTitle";
            this.lblInputTitle.Size = new System.Drawing.Size(82, 19);
            this.lblInputTitle.TabIndex = 113;
            this.lblInputTitle.Text = "PM 계획목록";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(873, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 112;
            this.btnSave.Text = "저장:";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Visible = false;
            // 
            // RegistPMPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1518, 775);
            this.Name = "RegistPMPlan";
            this.Text = "                                                                                 " +
    " ";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).EndInit();
            this.smartPanel4.ResumeLayout(false);
            this.smartPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdPMPlan;
        private Framework.SmartControls.SmartPanel smartPanel4;
        private Framework.SmartControls.SmartButton btnPublishMaintOrder;
        private Framework.SmartControls.SmartButton btnCreateSchedule;
        private Framework.SmartControls.SmartButton btnCancelMaintOrder;
        private Framework.SmartControls.SmartLabel lblInputTitle;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnAutoCreate;
    }
}