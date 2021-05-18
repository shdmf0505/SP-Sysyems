namespace Micube.SmartMES.ProcessManagement
{
    partial class PackingTransitCancel
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
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdPacking = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.pnlContent.Controls.Add(this.grdPacking);
            this.pnlContent.Controls.Add(this.spcSpliter);
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Size = new System.Drawing.Size(756, 489);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1061, 518);
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spcSpliter.Location = new System.Drawing.Point(0, 205);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(756, 5);
            this.spcSpliter.TabIndex = 8;
            this.spcSpliter.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdLotList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 210);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 279);
            this.panel1.TabIndex = 7;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "LOTINFO";
            this.grdLotList.Location = new System.Drawing.Point(0, 0);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.Size = new System.Drawing.Size(756, 279);
            this.grdLotList.TabIndex = 2;
            // 
            // grdPacking
            // 
            this.grdPacking.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPacking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPacking.IsUsePaging = false;
            this.grdPacking.LanguageKey = "CANCELTARGET";
            this.grdPacking.Location = new System.Drawing.Point(0, 0);
            this.grdPacking.Margin = new System.Windows.Forms.Padding(0);
            this.grdPacking.Name = "grdPacking";
            this.grdPacking.ShowBorder = true;
            this.grdPacking.Size = new System.Drawing.Size(756, 205);
            this.grdPacking.TabIndex = 9;
            // 
            // PackingTransitCancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "PackingTransitCancel";
            this.Text = "Box Packing Dispatch Cancel";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartBandedGrid grdPacking;
    }
}