namespace Micube.SmartMES.StandardInfo
{
    partial class SubMaterialRoutingBomHistory
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabDF = new DevExpress.XtraTab.XtraTabPage();
            this.grdTab1 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabHP = new DevExpress.XtraTab.XtraTabPage();
            this.grdTab2 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabChemical = new DevExpress.XtraTab.XtraTabPage();
            this.grdTab3 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabInk = new DevExpress.XtraTab.XtraTabPage();
            this.grdTab4 = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabDF.SuspendLayout();
            this.tabHP.SuspendLayout();
            this.tabChemical.SuspendLayout();
            this.tabInk.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabDF;
            this.tabMain.Size = new System.Drawing.Size(756, 489);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabDF,
            this.tabHP,
            this.tabChemical,
            this.tabInk});
            // 
            // tabDF
            // 
            this.tabDF.Controls.Add(this.grdTab1);
            this.tabDF.Name = "tabDF";
            this.tabDF.Padding = new System.Windows.Forms.Padding(3);
            this.tabDF.Size = new System.Drawing.Size(750, 460);
            this.tabDF.Text = "xtraTabPage1";
            // 
            // grdTab1
            // 
            this.grdTab1.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTab1.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTab1.IsUsePaging = false;
            this.grdTab1.LanguageKey = "";
            this.grdTab1.Location = new System.Drawing.Point(3, 3);
            this.grdTab1.Margin = new System.Windows.Forms.Padding(0);
            this.grdTab1.Name = "grdTab1";
            this.grdTab1.ShowBorder = true;
            this.grdTab1.ShowStatusBar = false;
            this.grdTab1.Size = new System.Drawing.Size(744, 454);
            this.grdTab1.TabIndex = 0;
            this.grdTab1.UseAutoBestFitColumns = false;
            // 
            // tabHP
            // 
            this.tabHP.Controls.Add(this.grdTab2);
            this.tabHP.Name = "tabHP";
            this.tabHP.Padding = new System.Windows.Forms.Padding(3);
            this.tabHP.Size = new System.Drawing.Size(750, 460);
            this.tabHP.Text = "xtraTabPage2";
            // 
            // grdTab2
            // 
            this.grdTab2.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdTab2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTab2.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTab2.IsUsePaging = false;
            this.grdTab2.LanguageKey = "";
            this.grdTab2.Location = new System.Drawing.Point(3, 3);
            this.grdTab2.Margin = new System.Windows.Forms.Padding(0);
            this.grdTab2.Name = "grdTab2";
            this.grdTab2.ShowBorder = true;
            this.grdTab2.ShowStatusBar = false;
            this.grdTab2.Size = new System.Drawing.Size(744, 454);
            this.grdTab2.TabIndex = 1;
            this.grdTab2.UseAutoBestFitColumns = false;
            // 
            // tabChemical
            // 
            this.tabChemical.Controls.Add(this.grdTab3);
            this.tabChemical.Name = "tabChemical";
            this.tabChemical.Padding = new System.Windows.Forms.Padding(3);
            this.tabChemical.Size = new System.Drawing.Size(750, 460);
            this.tabChemical.Text = "xtraTabPage3";
            // 
            // grdTab3
            // 
            this.grdTab3.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdTab3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTab3.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTab3.IsUsePaging = false;
            this.grdTab3.LanguageKey = "";
            this.grdTab3.Location = new System.Drawing.Point(3, 3);
            this.grdTab3.Margin = new System.Windows.Forms.Padding(0);
            this.grdTab3.Name = "grdTab3";
            this.grdTab3.ShowBorder = true;
            this.grdTab3.ShowStatusBar = false;
            this.grdTab3.Size = new System.Drawing.Size(744, 454);
            this.grdTab3.TabIndex = 1;
            this.grdTab3.UseAutoBestFitColumns = false;
            // 
            // tabInk
            // 
            this.tabInk.Controls.Add(this.grdTab4);
            this.tabInk.Name = "tabInk";
            this.tabInk.Padding = new System.Windows.Forms.Padding(3);
            this.tabInk.Size = new System.Drawing.Size(750, 460);
            this.tabInk.Text = "xtraTabPage4";
            // 
            // grdTab4
            // 
            this.grdTab4.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdTab4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTab4.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTab4.IsUsePaging = false;
            this.grdTab4.LanguageKey = "";
            this.grdTab4.Location = new System.Drawing.Point(3, 3);
            this.grdTab4.Margin = new System.Windows.Forms.Padding(0);
            this.grdTab4.Name = "grdTab4";
            this.grdTab4.ShowBorder = true;
            this.grdTab4.ShowStatusBar = false;
            this.grdTab4.Size = new System.Drawing.Size(744, 454);
            this.grdTab4.TabIndex = 1;
            this.grdTab4.UseAutoBestFitColumns = false;
            // 
            // SubMaterialRoutingBomHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "SubMaterialRoutingBomHistory";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabDF.ResumeLayout(false);
            this.tabHP.ResumeLayout(false);
            this.tabChemical.ResumeLayout(false);
            this.tabInk.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabDF;
        private DevExpress.XtraTab.XtraTabPage tabHP;
        private DevExpress.XtraTab.XtraTabPage tabChemical;
        private DevExpress.XtraTab.XtraTabPage tabInk;
        private Framework.SmartControls.SmartBandedGrid grdTab1;
        private Framework.SmartControls.SmartBandedGrid grdTab2;
        private Framework.SmartControls.SmartBandedGrid grdTab3;
        private Framework.SmartControls.SmartBandedGrid grdTab4;
    }
}
