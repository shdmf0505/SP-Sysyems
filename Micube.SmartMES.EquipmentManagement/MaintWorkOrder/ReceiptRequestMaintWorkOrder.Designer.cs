namespace Micube.SmartMES.EquipmentManagement
{
    partial class ReceiptRequestMaintWorkOrder
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
            this.grdMaintWorkOrder = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel4 = new Micube.Framework.SmartControls.SmartPanel();
            this.chkRefresh = new Micube.Framework.SmartControls.SmartCheckBox();
            this.lblInputTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.btnAccept = new Micube.Framework.SmartControls.SmartButton();
            this.tmMaintWorkOrder = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).BeginInit();
            this.smartPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkRefresh.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 652);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1053, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdMaintWorkOrder);
            this.pnlContent.Controls.Add(this.smartPanel4);
            this.pnlContent.Size = new System.Drawing.Size(1053, 656);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1358, 685);
            // 
            // grdMaintWorkOrder
            // 
            this.grdMaintWorkOrder.Caption = "설비수리요청목록:";
            this.grdMaintWorkOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaintWorkOrder.IsUsePaging = false;
            this.grdMaintWorkOrder.LanguageKey = "REQUESTREPAIRMAINTWORKORDERLIST";
            this.grdMaintWorkOrder.Location = new System.Drawing.Point(0, 34);
            this.grdMaintWorkOrder.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaintWorkOrder.Name = "grdMaintWorkOrder";
            this.grdMaintWorkOrder.ShowBorder = true;
            this.grdMaintWorkOrder.Size = new System.Drawing.Size(1053, 622);
            this.grdMaintWorkOrder.TabIndex = 1293;
            // 
            // smartPanel4
            // 
            this.smartPanel4.Controls.Add(this.chkRefresh);
            this.smartPanel4.Controls.Add(this.lblInputTitle);
            this.smartPanel4.Controls.Add(this.btnAccept);
            this.smartPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartPanel4.Name = "smartPanel4";
            this.smartPanel4.Size = new System.Drawing.Size(1053, 34);
            this.smartPanel4.TabIndex = 1292;
            // 
            // chkRefresh
            // 
            this.chkRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRefresh.LanguageKey = "AUTOREFRESH";
            this.chkRefresh.Location = new System.Drawing.Point(833, 7);
            this.chkRefresh.Name = "chkRefresh";
            this.chkRefresh.Properties.Caption = "자동새로고침:";
            this.chkRefresh.Size = new System.Drawing.Size(130, 19);
            this.chkRefresh.TabIndex = 1292;
            // 
            // lblInputTitle
            // 
            this.lblInputTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblInputTitle.Appearance.Options.UseFont = true;
            this.lblInputTitle.LanguageKey = "REQUESTREPAIRMAINTWORKSTATUS";
            this.lblInputTitle.Location = new System.Drawing.Point(5, 5);
            this.lblInputTitle.Name = "lblInputTitle";
            this.lblInputTitle.Size = new System.Drawing.Size(118, 19);
            this.lblInputTitle.TabIndex = 1130;
            this.lblInputTitle.Text = "설비수리요청현황:";
            // 
            // btnAccept
            // 
            this.btnAccept.AllowFocus = false;
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.IsBusy = false;
            this.btnAccept.LanguageKey = "ACCEPTBUTTON";
            this.btnAccept.Location = new System.Drawing.Point(969, 4);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAccept.Size = new System.Drawing.Size(80, 25);
            this.btnAccept.TabIndex = 126;
            this.btnAccept.Text = "접수:";
            this.btnAccept.TooltipLanguageKey = "";
            // 
            // tmMaintWorkOrder
            // 
            this.tmMaintWorkOrder.Interval = 60000;
            // 
            // ReceiptRequestMaintWorkOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 705);
            this.Name = "ReceiptRequestMaintWorkOrder";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).EndInit();
            this.smartPanel4.ResumeLayout(false);
            this.smartPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkRefresh.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdMaintWorkOrder;
        private Framework.SmartControls.SmartPanel smartPanel4;
        private Framework.SmartControls.SmartCheckBox chkRefresh;
        private Framework.SmartControls.SmartLabel lblInputTitle;
        private Framework.SmartControls.SmartButton btnAccept;
        private System.Windows.Forms.Timer tmMaintWorkOrder;
    }
}