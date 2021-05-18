namespace Micube.SmartMES.StandardInfo
{
    partial class ucSubMaterialByHotPress
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdLayup = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdNote = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdChange = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartSpliterControl3 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartLayoutControl1 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.grdInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).BeginInit();
            this.smartLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdLayup, 0, 4);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdNote, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdChange, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl2, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl3, 0, 3);
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 90);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 5;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(600, 510);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdLayup
            // 
            this.grdLayup.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLayup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLayup.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLayup.IsUsePaging = false;
            this.grdLayup.LanguageKey = null;
            this.grdLayup.Location = new System.Drawing.Point(0, 216);
            this.grdLayup.Margin = new System.Windows.Forms.Padding(0);
            this.grdLayup.Name = "grdLayup";
            this.grdLayup.ShowBorder = true;
            this.grdLayup.Size = new System.Drawing.Size(600, 294);
            this.grdLayup.TabIndex = 7;
            this.grdLayup.UseAutoBestFitColumns = false;
            // 
            // grdNote
            // 
            this.grdNote.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNote.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdNote.IsUsePaging = false;
            this.grdNote.LanguageKey = null;
            this.grdNote.Location = new System.Drawing.Point(0, 108);
            this.grdNote.Margin = new System.Windows.Forms.Padding(0);
            this.grdNote.Name = "grdNote";
            this.grdNote.ShowBorder = true;
            this.grdNote.Size = new System.Drawing.Size(600, 98);
            this.grdNote.TabIndex = 6;
            this.grdNote.UseAutoBestFitColumns = false;
            // 
            // grdChange
            // 
            this.grdChange.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChange.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdChange.IsUsePaging = false;
            this.grdChange.LanguageKey = null;
            this.grdChange.Location = new System.Drawing.Point(0, 0);
            this.grdChange.Margin = new System.Windows.Forms.Padding(0);
            this.grdChange.Name = "grdChange";
            this.grdChange.ShowBorder = true;
            this.grdChange.Size = new System.Drawing.Size(600, 98);
            this.grdChange.TabIndex = 5;
            this.grdChange.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl2.Location = new System.Drawing.Point(0, 98);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(600, 5);
            this.smartSpliterControl2.TabIndex = 3;
            this.smartSpliterControl2.TabStop = false;
            // 
            // smartSpliterControl3
            // 
            this.smartSpliterControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl3.Location = new System.Drawing.Point(0, 206);
            this.smartSpliterControl3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl3.Name = "smartSpliterControl3";
            this.smartSpliterControl3.Size = new System.Drawing.Size(600, 5);
            this.smartSpliterControl3.TabIndex = 4;
            this.smartSpliterControl3.TabStop = false;
            // 
            // smartLayoutControl1
            // 
            this.smartLayoutControl1.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.smartLayoutControl1.Controls.Add(this.grdInfo);
            this.smartLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.smartLayoutControl1.Name = "smartLayoutControl1";
            this.smartLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(759, 255, 650, 400);
            this.smartLayoutControl1.Root = this.smartLayoutControlGroup1;
            this.smartLayoutControl1.Size = new System.Drawing.Size(600, 600);
            this.smartLayoutControl1.TabIndex = 1;
            this.smartLayoutControl1.Text = "smartLayoutControl1";
            // 
            // grdInfo
            // 
            this.grdInfo.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInfo.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInfo.IsUsePaging = false;
            this.grdInfo.LanguageKey = null;
            this.grdInfo.Location = new System.Drawing.Point(0, 0);
            this.grdInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdInfo.Name = "grdInfo";
            this.grdInfo.ShowBorder = true;
            this.grdInfo.Size = new System.Drawing.Size(600, 85);
            this.grdInfo.TabIndex = 4;
            this.grdInfo.UseAutoBestFitColumns = false;
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(600, 600);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdInfo;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 85);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(200, 85);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(600, 85);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.smartSplitTableLayoutPanel1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 90);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(600, 510);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 85);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 5);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 5);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(600, 5);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ucSubMaterialByHotPress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartLayoutControl1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucSubMaterialByHotPress";
            this.Size = new System.Drawing.Size(600, 600);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).EndInit();
            this.smartLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdLayup;
        private Framework.SmartControls.SmartBandedGrid grdNote;
        private Framework.SmartControls.SmartBandedGrid grdChange;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl3;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl1;
        private Framework.SmartControls.SmartBandedGrid grdInfo;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
