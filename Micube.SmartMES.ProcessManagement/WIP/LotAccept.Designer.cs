namespace Micube.SmartMES.ProcessManagement
{
    partial class LotAccept
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.pnlTopCondition = new Micube.Framework.SmartControls.SmartPanel();
            this.tlpTopCondition = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtArea = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.tlpRCCheck = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblIsRework = new Micube.Framework.SmartControls.SmartLabel();
            this.lblIsRCLot = new Micube.Framework.SmartControls.SmartLabel();
            this.lblInnerRevisionText = new Micube.Framework.SmartControls.SmartLabel();
            this.lblInnerRevision = new Micube.Framework.SmartControls.SmartLabel();
            this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.pfsInfo = new Micube.SmartMES.ProcessManagement.ucProcessFourStepInfo();
            this.pfsDetail = new Micube.SmartMES.ProcessManagement.ucProcessFourStepDetail();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnInit = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopCondition)).BeginInit();
            this.pnlTopCondition.SuspendLayout();
            this.tlpTopCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            this.tlpRCCheck.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(0, 30);
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.OptionsPrint.AppearanceGroupCaption.BackColor = System.Drawing.Color.LightGray;
            this.pnlCondition.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.pnlCondition.OptionsPrint.AppearanceGroupCaption.Options.UseBackColor = true;
            this.pnlCondition.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.pnlCondition.OptionsPrint.AppearanceGroupCaption.Options.UseTextOptions = true;
            this.pnlCondition.OptionsPrint.AppearanceGroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pnlCondition.OptionsPrint.AppearanceGroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.pnlCondition.Size = new System.Drawing.Size(0, 0);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlToolbar.Size = new System.Drawing.Size(780, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(780, 401);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(780, 430);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.pnlTopCondition, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdLotInfo, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.pfsInfo, 0, 4);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.pfsDetail, 0, 6);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 7;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(780, 401);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // pnlTopCondition
            // 
            this.pnlTopCondition.Controls.Add(this.tlpTopCondition);
            this.pnlTopCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopCondition.Location = new System.Drawing.Point(0, 0);
            this.pnlTopCondition.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTopCondition.Name = "pnlTopCondition";
            this.pnlTopCondition.Size = new System.Drawing.Size(780, 34);
            this.pnlTopCondition.TabIndex = 0;
            // 
            // tlpTopCondition
            // 
            this.tlpTopCondition.ColumnCount = 9;
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTopCondition.Controls.Add(this.txtArea, 1, 1);
            this.tlpTopCondition.Controls.Add(this.txtLotId, 3, 1);
            this.tlpTopCondition.Controls.Add(this.tlpRCCheck, 5, 0);
            this.tlpTopCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTopCondition.Location = new System.Drawing.Point(2, 2);
            this.tlpTopCondition.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpTopCondition.Name = "tlpTopCondition";
            this.tlpTopCondition.RowCount = 3;
            this.tlpTopCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tlpTopCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTopCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpTopCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTopCondition.Size = new System.Drawing.Size(776, 30);
            this.tlpTopCondition.TabIndex = 0;
            // 
            // txtArea
            // 
            this.txtArea.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtArea.Appearance.Options.UseForeColor = true;
            this.txtArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArea.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtArea.LabelText = "작업장";
            this.txtArea.LanguageKey = "AREA";
            this.txtArea.Location = new System.Drawing.Point(10, 4);
            this.txtArea.Margin = new System.Windows.Forms.Padding(0);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(173, 20);
            this.txtArea.TabIndex = 0;
            // 
            // txtLotId
            // 
            this.txtLotId.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtLotId.Appearance.Options.UseForeColor = true;
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotId.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtLotId.LabelText = "LOT NO";
            this.txtLotId.LanguageKey = "LOTID";
            this.txtLotId.Location = new System.Drawing.Point(193, 4);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Size = new System.Drawing.Size(173, 20);
            this.txtLotId.TabIndex = 1;
            // 
            // tlpRCCheck
            // 
            this.tlpRCCheck.ColumnCount = 5;
            this.tlpTopCondition.SetColumnSpan(this.tlpRCCheck, 3);
            this.tlpRCCheck.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpRCCheck.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpRCCheck.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpRCCheck.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpRCCheck.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRCCheck.Controls.Add(this.lblIsRework, 3, 0);
            this.tlpRCCheck.Controls.Add(this.lblIsRCLot, 0, 0);
            this.tlpRCCheck.Controls.Add(this.lblInnerRevisionText, 2, 0);
            this.tlpRCCheck.Controls.Add(this.lblInnerRevision, 1, 0);
            this.tlpRCCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRCCheck.Location = new System.Drawing.Point(376, 0);
            this.tlpRCCheck.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpRCCheck.Name = "tlpRCCheck";
            this.tlpRCCheck.RowCount = 1;
            this.tlpTopCondition.SetRowSpan(this.tlpRCCheck, 3);
            this.tlpRCCheck.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRCCheck.Size = new System.Drawing.Size(356, 30);
            this.tlpRCCheck.TabIndex = 6;
            // 
            // lblIsRework
            // 
            this.lblIsRework.Appearance.BackColor = System.Drawing.Color.Blue;
            this.lblIsRework.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblIsRework.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblIsRework.Appearance.Options.UseBackColor = true;
            this.lblIsRework.Appearance.Options.UseFont = true;
            this.lblIsRework.Appearance.Options.UseForeColor = true;
            this.lblIsRework.Appearance.Options.UseTextOptions = true;
            this.lblIsRework.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblIsRework.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblIsRework.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblIsRework.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIsRework.Location = new System.Drawing.Point(330, 0);
            this.lblIsRework.Margin = new System.Windows.Forms.Padding(0);
            this.lblIsRework.Name = "lblIsRework";
            this.lblIsRework.Size = new System.Drawing.Size(90, 30);
            this.lblIsRework.TabIndex = 8;
            this.lblIsRework.Text = "REWORK";
            // 
            // lblIsRCLot
            // 
            this.lblIsRCLot.Appearance.BackColor = System.Drawing.Color.Red;
            this.lblIsRCLot.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblIsRCLot.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblIsRCLot.Appearance.Options.UseBackColor = true;
            this.lblIsRCLot.Appearance.Options.UseFont = true;
            this.lblIsRCLot.Appearance.Options.UseForeColor = true;
            this.lblIsRCLot.Appearance.Options.UseTextOptions = true;
            this.lblIsRCLot.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblIsRCLot.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblIsRCLot.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblIsRCLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIsRCLot.Location = new System.Drawing.Point(0, 0);
            this.lblIsRCLot.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblIsRCLot.Name = "lblIsRCLot";
            this.lblIsRCLot.Size = new System.Drawing.Size(190, 30);
            this.lblIsRCLot.TabIndex = 5;
            this.lblIsRCLot.Text = "RUNNING CHANGE";
            // 
            // lblInnerRevisionText
            // 
            this.lblInnerRevisionText.Appearance.BackColor = System.Drawing.Color.White;
            this.lblInnerRevisionText.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.lblInnerRevisionText.Appearance.Options.UseBackColor = true;
            this.lblInnerRevisionText.Appearance.Options.UseFont = true;
            this.lblInnerRevisionText.Appearance.Options.UseTextOptions = true;
            this.lblInnerRevisionText.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblInnerRevisionText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblInnerRevisionText.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblInnerRevisionText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInnerRevisionText.Location = new System.Drawing.Point(270, 0);
            this.lblInnerRevisionText.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblInnerRevisionText.Name = "lblInnerRevisionText";
            this.lblInnerRevisionText.Size = new System.Drawing.Size(50, 30);
            this.lblInnerRevisionText.TabIndex = 6;
            // 
            // lblInnerRevision
            // 
            this.lblInnerRevision.Appearance.Options.UseTextOptions = true;
            this.lblInnerRevision.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblInnerRevision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInnerRevision.LanguageKey = "INNERREVISION";
            this.lblInnerRevision.Location = new System.Drawing.Point(200, 0);
            this.lblInnerRevision.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblInnerRevision.Name = "lblInnerRevision";
            this.lblInnerRevision.Size = new System.Drawing.Size(60, 30);
            this.lblInnerRevision.TabIndex = 7;
            this.lblInnerRevision.Text = "내부REV";
            // 
            // grdLotInfo
            // 
            this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotInfo.Location = new System.Drawing.Point(0, 44);
            this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotInfo.Name = "grdLotInfo";
            this.grdLotInfo.Size = new System.Drawing.Size(780, 101);
            this.grdLotInfo.TabIndex = 1;
            // 
            // pfsInfo
            // 
            this.pfsInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pfsInfo.Location = new System.Drawing.Point(0, 155);
            this.pfsInfo.Margin = new System.Windows.Forms.Padding(0);
            this.pfsInfo.Name = "pfsInfo";
            this.pfsInfo.ProcessType = Micube.SmartMES.Commons.ProcessType.LotAccept;
            this.pfsInfo.Size = new System.Drawing.Size(780, 100);
            this.pfsInfo.TabIndex = 2;
            this.pfsInfo.TransitAreaVisible = true;
            this.pfsInfo.UnitComboReadOnly = false;
            // 
            // pfsDetail
            // 
            this.pfsDetail.CommentDataSource = null;
            this.pfsDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pfsDetail.DurableDefDataSource = null;
            this.pfsDetail.EquipmentRecipeDataSource = null;
            this.pfsDetail.EquipmentUseStatusDataSource = null;
            this.pfsDetail.Location = new System.Drawing.Point(0, 265);
            this.pfsDetail.Margin = new System.Windows.Forms.Padding(0);
            this.pfsDetail.MessageDataSource = null;
            this.pfsDetail.Name = "pfsDetail";
            this.pfsDetail.PostProcessEquipmentWipDataSource = null;
            this.pfsDetail.ProcessSpecDataSource = null;
            this.pfsDetail.ProcessType = Micube.SmartMES.Commons.ProcessType.LotAccept;
            this.pfsDetail.Size = new System.Drawing.Size(780, 136);
            this.pfsDetail.StandardRequirementCompleteDataSource = null;
            this.pfsDetail.StandardRequirementStartDataSource = null;
            this.pfsDetail.TabIndex = 3;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 2;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.btnInit, 3, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(733, 24);
            this.smartSplitTableLayoutPanel2.TabIndex = 6;
            // 
            // btnInit
            // 
            this.btnInit.AllowFocus = false;
            this.btnInit.IsBusy = false;
            this.btnInit.IsWrite = false;
            this.btnInit.LanguageKey = "INITIALIZE";
            this.btnInit.Location = new System.Drawing.Point(651, 0);
            this.btnInit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnInit.Name = "btnInit";
            this.btnInit.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "초기화";
            this.btnInit.TooltipLanguageKey = "";
            // 
            // LotAccept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ConditionsVisible = false;
            this.Name = "LotAccept";
            this.ShowSaveCompleteMessage = false;
            this.Text = "LotAccept";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopCondition)).EndInit();
            this.pnlTopCondition.ResumeLayout(false);
            this.tlpTopCondition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            this.tlpRCCheck.ResumeLayout(false);
            this.tlpRCCheck.PerformLayout();
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartButton btnInit;
        private Framework.SmartControls.SmartPanel pnlTopCondition;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpTopCondition;
        private Framework.SmartControls.SmartLabelSelectPopupEdit txtArea;
        private Framework.SmartControls.SmartLabelTextBox txtLotId;
        private Commons.Controls.SmartLotInfoGrid grdLotInfo;
        private ucProcessFourStepInfo pfsInfo;
        private ucProcessFourStepDetail pfsDetail;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpRCCheck;
        private Framework.SmartControls.SmartLabel lblIsRCLot;
        private Framework.SmartControls.SmartLabel lblInnerRevisionText;
        private Framework.SmartControls.SmartLabel lblInnerRevision;
        private Framework.SmartControls.SmartLabel lblIsRework;
    }
}