namespace Micube.SmartMES.ToolManagement
{
    partial class ReceiptMoveTool
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
            this.grdToolMoveList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.lblGroupTitle = new Micube.Framework.SmartControls.SmartLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 725);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1134, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdToolMoveList);
            this.pnlContent.Controls.Add(this.smartPanel1);
            this.pnlContent.Size = new System.Drawing.Size(1134, 729);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1439, 758);
            // 
            // grdToolMoveList
            // 
            this.grdToolMoveList.Caption = "치공구 이동입고목록:";
            this.grdToolMoveList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdToolMoveList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdToolMoveList.IsUsePaging = false;
            this.grdToolMoveList.LanguageKey = "TOOLMOVERECEIPTLIST";
            this.grdToolMoveList.Location = new System.Drawing.Point(0, 35);
            this.grdToolMoveList.Margin = new System.Windows.Forms.Padding(0);
            this.grdToolMoveList.Name = "grdToolMoveList";
            this.grdToolMoveList.ShowBorder = true;
            this.grdToolMoveList.Size = new System.Drawing.Size(1134, 694);
            this.grdToolMoveList.TabIndex = 105;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.btnSave);
            this.smartPanel1.Controls.Add(this.lblGroupTitle);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1134, 35);
            this.smartPanel1.TabIndex = 104;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(1049, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 113;
            this.btnSave.Text = "저장:";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Visible = false;
            // 
            // lblGroupTitle
            // 
            this.lblGroupTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblGroupTitle.Appearance.Options.UseFont = true;
            this.lblGroupTitle.LanguageKey = "TOOLMOVERECEIPT";
            this.lblGroupTitle.Location = new System.Drawing.Point(5, 5);
            this.lblGroupTitle.Name = "lblGroupTitle";
            this.lblGroupTitle.Size = new System.Drawing.Size(109, 19);
            this.lblGroupTitle.TabIndex = 113;
            this.lblGroupTitle.Text = "치공구 이동입고:";
            // 
            // ReceiptMoveTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 778);
            this.Name = "ReceiptMoveTool";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdToolMoveList;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartLabel lblGroupTitle;
    }
}