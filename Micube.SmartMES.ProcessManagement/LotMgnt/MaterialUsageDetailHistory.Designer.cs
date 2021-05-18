namespace Micube.SmartMES.ProcessManagement
{
    partial class MaterialUsageDetailHistory
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
            this.tabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.tabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.tabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.tabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.tabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.grdPage1 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdPage2 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdPage3 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdPage4 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdPage5 = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
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
            this.tabMain.SelectedTabPage = this.tabPage1;
            this.tabMain.Size = new System.Drawing.Size(756, 489);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage1,
            this.tabPage2,
            this.tabPage3,
            this.tabPage4,
            this.tabPage5});
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grdPage1);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(750, 460);
            this.tabPage1.Text = "1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grdPage2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(750, 460);
            this.tabPage2.Text = "2";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grdPage3);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(750, 460);
            this.tabPage3.Text = "3";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.grdPage4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(750, 460);
            this.tabPage4.Text = "4";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.grdPage5);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(750, 460);
            this.tabPage5.Text = "5";
            // 
            // grdPage1
            // 
            this.grdPage1.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPage1.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPage1.IsUsePaging = false;
            this.grdPage1.LanguageKey = null;
            this.grdPage1.Location = new System.Drawing.Point(3, 3);
            this.grdPage1.Name = "grdPage1";
            this.grdPage1.ShowBorder = true;
            this.grdPage1.ShowStatusBar = false;
            this.grdPage1.Size = new System.Drawing.Size(744, 454);
            this.grdPage1.TabIndex = 0;
            this.grdPage1.UseAutoBestFitColumns = false;
            // 
            // grdPage2
            // 
            this.grdPage2.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPage2.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPage2.IsUsePaging = false;
            this.grdPage2.LanguageKey = null;
            this.grdPage2.Location = new System.Drawing.Point(3, 3);
            this.grdPage2.Margin = new System.Windows.Forms.Padding(0);
            this.grdPage2.Name = "grdPage2";
            this.grdPage2.ShowBorder = true;
            this.grdPage2.ShowStatusBar = false;
            this.grdPage2.Size = new System.Drawing.Size(744, 454);
            this.grdPage2.TabIndex = 0;
            this.grdPage2.UseAutoBestFitColumns = false;
            // 
            // grdPage3
            // 
            this.grdPage3.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPage3.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPage3.IsUsePaging = false;
            this.grdPage3.LanguageKey = null;
            this.grdPage3.Location = new System.Drawing.Point(3, 3);
            this.grdPage3.Margin = new System.Windows.Forms.Padding(0);
            this.grdPage3.Name = "grdPage3";
            this.grdPage3.ShowBorder = true;
            this.grdPage3.ShowStatusBar = false;
            this.grdPage3.Size = new System.Drawing.Size(744, 454);
            this.grdPage3.TabIndex = 0;
            this.grdPage3.UseAutoBestFitColumns = false;
            // 
            // grdPage4
            // 
            this.grdPage4.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPage4.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPage4.IsUsePaging = false;
            this.grdPage4.LanguageKey = null;
            this.grdPage4.Location = new System.Drawing.Point(3, 3);
            this.grdPage4.Margin = new System.Windows.Forms.Padding(0);
            this.grdPage4.Name = "grdPage4";
            this.grdPage4.ShowBorder = true;
            this.grdPage4.ShowStatusBar = false;
            this.grdPage4.Size = new System.Drawing.Size(744, 454);
            this.grdPage4.TabIndex = 0;
            this.grdPage4.UseAutoBestFitColumns = false;
            // 
            // grdPage5
            // 
            this.grdPage5.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPage5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPage5.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPage5.IsUsePaging = false;
            this.grdPage5.LanguageKey = null;
            this.grdPage5.Location = new System.Drawing.Point(3, 3);
            this.grdPage5.Margin = new System.Windows.Forms.Padding(0);
            this.grdPage5.Name = "grdPage5";
            this.grdPage5.ShowBorder = true;
            this.grdPage5.Size = new System.Drawing.Size(744, 454);
            this.grdPage5.TabIndex = 0;
            this.grdPage5.UseAutoBestFitColumns = false;
            // 
            // MaterialUsageDetailHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "MaterialUsageDetailHistory";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabPage1;
        private DevExpress.XtraTab.XtraTabPage tabPage2;
        private DevExpress.XtraTab.XtraTabPage tabPage3;
        private DevExpress.XtraTab.XtraTabPage tabPage4;
        private DevExpress.XtraTab.XtraTabPage tabPage5;
        private Framework.SmartControls.SmartBandedGrid grdPage1;
        private Framework.SmartControls.SmartBandedGrid grdPage2;
        private Framework.SmartControls.SmartBandedGrid grdPage3;
        private Framework.SmartControls.SmartBandedGrid grdPage4;
        private Framework.SmartControls.SmartBandedGrid grdPage5;
    }
}
