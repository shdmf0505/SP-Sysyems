namespace Micube.SmartMES.ProcessManagement
{
    partial class LotReturnList
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
            this.grdReturnLot = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 485);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(756, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdReturnLot);
            this.pnlContent.Controls.Add(this.spcSpliter);
            this.pnlContent.Controls.Add(this.grdLotList);
            this.pnlContent.Size = new System.Drawing.Size(756, 489);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1061, 518);
            // 
            // grdReturnLot
            // 
            this.grdReturnLot.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdReturnLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReturnLot.IsUsePaging = false;
            this.grdReturnLot.LanguageKey = "RETURNLOTS";
            this.grdReturnLot.Location = new System.Drawing.Point(0, 0);
            this.grdReturnLot.Margin = new System.Windows.Forms.Padding(0);
            this.grdReturnLot.Name = "grdReturnLot";
            this.grdReturnLot.ShowBorder = true;
            this.grdReturnLot.Size = new System.Drawing.Size(756, 201);
            this.grdReturnLot.TabIndex = 1;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "WIPLIST";
            this.grdLotList.Location = new System.Drawing.Point(0, 206);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.Size = new System.Drawing.Size(756, 283);
            this.grdLotList.TabIndex = 2;
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spcSpliter.Location = new System.Drawing.Point(0, 201);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(756, 5);
            this.spcSpliter.TabIndex = 5;
            this.spcSpliter.TabStop = false;
            // 
            // LotReturnList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "LotReturnList";
            this.Text = "Return Lot List";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdReturnLot;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
    }
}