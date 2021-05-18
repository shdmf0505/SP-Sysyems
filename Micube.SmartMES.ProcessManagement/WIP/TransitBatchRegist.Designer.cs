namespace Micube.SmartMES.ProcessManagement
{
    partial class TransitBatchRegist
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
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            this.smartLayout1 = new Micube.Framework.SmartControls.SmartLayout();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.cboTargetArea = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.grdTransArea = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdWIP = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.ucDataUpDownBtn = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
            this.txtLotno = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayout1)).BeginInit();
            this.smartLayout1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTargetArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.OptionsView.UseDefaultDragAndDropRendering = false;
            this.pnlCondition.Size = new System.Drawing.Size(296, 753);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(806, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartLayout1);
            this.pnlContent.Size = new System.Drawing.Size(806, 757);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1111, 786);
            // 
            // smartLayout1
            // 
            this.smartLayout1.Conditions = conditionCollection1;
            this.smartLayout1.Controls.Add(this.smartGroupBox1);
            this.smartLayout1.Controls.Add(this.grdTransArea);
            this.smartLayout1.Controls.Add(this.grdWIP);
            this.smartLayout1.Controls.Add(this.ucDataUpDownBtn);
            this.smartLayout1.Controls.Add(this.txtLotno);
            this.smartLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayout1.IsBusy = false;
            this.smartLayout1.LayoutType = DevExpress.XtraLayout.Utils.LayoutType.Vertical;
            this.smartLayout1.Location = new System.Drawing.Point(0, 0);
            this.smartLayout1.Name = "smartLayout1";
            this.smartLayout1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1217, 169, 650, 899);
            this.smartLayout1.Root = this.layoutControlGroup1;
            this.smartLayout1.Size = new System.Drawing.Size(806, 757);
            this.smartLayout1.TabIndex = 0;
            this.smartLayout1.Text = "smartLayout1";
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.cboTargetArea);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "";
            this.smartGroupBox1.Location = new System.Drawing.Point(662, 420);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(132, 325);
            this.smartGroupBox1.TabIndex = 10;
            this.smartGroupBox1.Text = "인계 작업장";
            // 
            // cboTargetArea
            // 
            this.cboTargetArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboTargetArea.LabelText = "대상자원";
            this.cboTargetArea.LabelWidth = "20%";
            this.cboTargetArea.LanguageKey = "TARGETRESOURCE";
            this.cboTargetArea.Location = new System.Drawing.Point(2, 31);
            this.cboTargetArea.Name = "cboTargetArea";
            this.cboTargetArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTargetArea.Properties.NullText = "";
            this.cboTargetArea.Size = new System.Drawing.Size(128, 20);
            this.cboTargetArea.TabIndex = 1;
            // 
            // grdTransArea
            // 
            this.grdTransArea.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdTransArea.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTransArea.IsUsePaging = false;
            this.grdTransArea.LanguageKey = null;
            this.grdTransArea.Location = new System.Drawing.Point(12, 402);
            this.grdTransArea.Margin = new System.Windows.Forms.Padding(0);
            this.grdTransArea.Name = "grdTransArea";
            this.grdTransArea.ShowBorder = true;
            this.grdTransArea.Size = new System.Drawing.Size(646, 343);
            this.grdTransArea.TabIndex = 9;
            this.grdTransArea.UseAutoBestFitColumns = true;
            // 
            // grdWIP
            // 
            this.grdWIP.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWIP.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdWIP.IsUsePaging = false;
            this.grdWIP.LanguageKey = null;
            this.grdWIP.Location = new System.Drawing.Point(12, 36);
            this.grdWIP.Margin = new System.Windows.Forms.Padding(0);
            this.grdWIP.Name = "grdWIP";
            this.grdWIP.ShowBorder = true;
            this.grdWIP.Size = new System.Drawing.Size(782, 326);
            this.grdWIP.TabIndex = 8;
            this.grdWIP.UseAutoBestFitColumns = true;
            // 
            // ucDataUpDownBtn
            // 
            this.ucDataUpDownBtn.Location = new System.Drawing.Point(12, 366);
            this.ucDataUpDownBtn.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.ucDataUpDownBtn.Name = "ucDataUpDownBtn";
            this.ucDataUpDownBtn.Size = new System.Drawing.Size(782, 32);
            this.ucDataUpDownBtn.SourceGrid = null;
            this.ucDataUpDownBtn.TabIndex = 7;
            this.ucDataUpDownBtn.TargetGrid = null;
            // 
            // txtLotno
            // 
            this.txtLotno.Location = new System.Drawing.Point(57, 12);
            this.txtLotno.Name = "txtLotno";
            this.txtLotno.Properties.Name = "txtLotno";
            this.txtLotno.Size = new System.Drawing.Size(50, 20);
            this.txtLotno.StyleController = this.smartLayout1;
            this.txtLotno.TabIndex = 11;
            this.txtLotno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLotno_KeyDown);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.simpleLabelItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(806, 757);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucDataUpDownBtn;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 354);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.OptionsTableLayoutItem.ColumnSpan = 2;
            this.layoutControlItem2.OptionsTableLayoutItem.RowIndex = 1;
            this.layoutControlItem2.Size = new System.Drawing.Size(786, 36);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdWIP;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.OptionsTableLayoutItem.ColumnSpan = 2;
            this.layoutControlItem1.Size = new System.Drawing.Size(786, 330);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.grdTransArea;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 390);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.OptionsTableLayoutItem.RowIndex = 2;
            this.layoutControlItem3.Size = new System.Drawing.Size(650, 347);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.smartGroupBox1;
            this.layoutControlItem4.Location = new System.Drawing.Point(650, 408);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.OptionsTableLayoutItem.ColumnIndex = 1;
            this.layoutControlItem4.OptionsTableLayoutItem.RowIndex = 2;
            this.layoutControlItem4.Size = new System.Drawing.Size(136, 329);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtLotno;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(99, 24);
            this.layoutControlItem5.Text = "LotNo";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(42, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(394, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(392, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(197, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(197, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(99, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(98, 24);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.Location = new System.Drawing.Point(650, 390);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(136, 18);
            this.simpleLabelItem1.Text = "후공정 : ";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(42, 14);
            // 
            // TransitBatchRegist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 806);
            this.Name = "TransitBatchRegist";
            this.ShowSaveCompleteMessage = false;
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayout1)).EndInit();
            this.smartLayout1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboTargetArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartLayout smartLayout1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Commons.Controls.ucDataUpDownBtn ucDataUpDownBtn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartLabelComboBox cboTargetArea;
        private Framework.SmartControls.SmartBandedGrid grdTransArea;
        private Framework.SmartControls.SmartBandedGrid grdWIP;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit txtLotno;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
    }
}