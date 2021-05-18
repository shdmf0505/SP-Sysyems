namespace Micube.SmartMES.StandardInfo
{
    partial class ProductOutsourcingSpec2
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartSplitTableLayoutPanel5 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdOperation = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdSpecList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 472);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlToolbar.Size = new System.Drawing.Size(1196, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel1, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(1196, 476);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1501, 505);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 2;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSplitTableLayoutPanel5, 1, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1149, 24);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // smartSplitTableLayoutPanel5
            // 
            this.smartSplitTableLayoutPanel5.ColumnCount = 2;
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.49724F));
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.50276F));
            this.smartSplitTableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel5.Location = new System.Drawing.Point(574, 0);
            this.smartSplitTableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel5.Name = "smartSplitTableLayoutPanel5";
            this.smartSplitTableLayoutPanel5.RowCount = 1;
            this.smartSplitTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel5.Size = new System.Drawing.Size(575, 24);
            this.smartSplitTableLayoutPanel5.TabIndex = 6;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdOperation);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdSpecList);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1196, 476);
            this.smartSpliterContainer1.SplitterPosition = 528;
            this.smartSpliterContainer1.TabIndex = 6;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdOperation
            // 
            this.grdOperation.Caption = "공정";
            this.grdOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOperation.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOperation.IsUsePaging = false;
            this.grdOperation.LanguageKey = "OPERATION";
            this.grdOperation.Location = new System.Drawing.Point(0, 0);
            this.grdOperation.Margin = new System.Windows.Forms.Padding(0);
            this.grdOperation.Name = "grdOperation";
            this.grdOperation.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.grdOperation.ShowBorder = true;
            this.grdOperation.Size = new System.Drawing.Size(528, 476);
            this.grdOperation.TabIndex = 4;
            this.grdOperation.UseAutoBestFitColumns = false;
            // 
            // grdSpecList
            // 
            this.grdSpecList.Caption = "외주사양정보";
            this.grdSpecList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSpecList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSpecList.IsUsePaging = false;
            this.grdSpecList.LanguageKey = "";
            this.grdSpecList.Location = new System.Drawing.Point(0, 0);
            this.grdSpecList.Margin = new System.Windows.Forms.Padding(0);
            this.grdSpecList.Name = "grdSpecList";
            this.grdSpecList.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.grdSpecList.ShowBorder = true;
            this.grdSpecList.Size = new System.Drawing.Size(663, 476);
            this.grdSpecList.TabIndex = 5;
            this.grdSpecList.UseAutoBestFitColumns = false;
            // 
            // ProductOutsourcingSpec2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1539, 543);
            this.Name = "ProductOutsourcingSpec2";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel5;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdOperation;
        private Framework.SmartControls.SmartBandedGrid grdSpecList;
    }
}

