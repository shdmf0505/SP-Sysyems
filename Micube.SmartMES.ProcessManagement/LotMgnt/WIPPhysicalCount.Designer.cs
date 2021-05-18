namespace Micube.SmartMES.ProcessManagement
{
	partial class WIPPhysicalCount
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
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlButton = new System.Windows.Forms.FlowLayoutPanel();
            this.btnWipPhysicalList = new Micube.Framework.SmartControls.SmartButton();
            this.btnTakeOverStop = new Micube.Framework.SmartControls.SmartButton();
            this.btnTakeOverStart = new Micube.Framework.SmartControls.SmartButton();
            this.lblState = new Micube.Framework.SmartControls.SmartLabel();
            this.pnlState = new Micube.Framework.SmartControls.SmartPanel();
            this.timerState = new System.Windows.Forms.Timer(this.components);
            this.tplWipPhysicalCount = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tabLotList = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgNotCompleteList = new DevExpress.XtraTab.XtraTabPage();
            this.pnlLotId = new Micube.Framework.SmartControls.SmartPanel();
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.tpgCompleteList = new DevExpress.XtraTab.XtraTabPage();
            this.grdCompleteList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlState)).BeginInit();
            this.pnlState.SuspendLayout();
            this.tplWipPhysicalCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabLotList)).BeginInit();
            this.tabLotList.SuspendLayout();
            this.tpgNotCompleteList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLotId)).BeginInit();
            this.pnlLotId.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            this.tpgCompleteList.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 401);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.pnlButton);
            this.pnlToolbar.Size = new System.Drawing.Size(470, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.pnlButton, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabLotList);
            this.pnlContent.Controls.Add(this.tplWipPhysicalCount);
            this.pnlContent.Size = new System.Drawing.Size(470, 396);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(780, 430);
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "WIP";
            this.grdLotList.Location = new System.Drawing.Point(0, 30);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.Size = new System.Drawing.Size(468, 281);
            this.grdLotList.TabIndex = 0;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnWipPhysicalList);
            this.pnlButton.Controls.Add(this.btnTakeOverStop);
            this.pnlButton.Controls.Add(this.btnTakeOverStart);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButton.Location = new System.Drawing.Point(45, 0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(425, 24);
            this.pnlButton.TabIndex = 5;
            // 
            // btnWipPhysicalList
            // 
            this.btnWipPhysicalList.AllowFocus = false;
            this.btnWipPhysicalList.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnWipPhysicalList.Appearance.Options.UseFont = true;
            this.btnWipPhysicalList.IsBusy = false;
            this.btnWipPhysicalList.IsWrite = false;
            this.btnWipPhysicalList.LanguageKey = "WIPCHECKLIST";
            this.btnWipPhysicalList.Location = new System.Drawing.Point(302, 0);
            this.btnWipPhysicalList.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnWipPhysicalList.Name = "btnWipPhysicalList";
            this.btnWipPhysicalList.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnWipPhysicalList.Size = new System.Drawing.Size(120, 23);
            this.btnWipPhysicalList.TabIndex = 0;
            this.btnWipPhysicalList.Text = "실사리스트";
            this.btnWipPhysicalList.TooltipLanguageKey = "";
            // 
            // btnTakeOverStop
            // 
            this.btnTakeOverStop.AllowFocus = false;
            this.btnTakeOverStop.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnTakeOverStop.Appearance.Options.UseFont = true;
            this.btnTakeOverStop.IsBusy = false;
            this.btnTakeOverStop.IsWrite = false;
            this.btnTakeOverStop.LanguageKey = "WIPCHECKFINISH";
            this.btnTakeOverStop.Location = new System.Drawing.Point(176, 0);
            this.btnTakeOverStop.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnTakeOverStop.Name = "btnTakeOverStop";
            this.btnTakeOverStop.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnTakeOverStop.Size = new System.Drawing.Size(120, 23);
            this.btnTakeOverStop.TabIndex = 2;
            this.btnTakeOverStop.Text = "재공실사 완료";
            this.btnTakeOverStop.TooltipLanguageKey = "";
            this.btnTakeOverStop.Visible = false;
            // 
            // btnTakeOverStart
            // 
            this.btnTakeOverStart.AllowFocus = false;
            this.btnTakeOverStart.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnTakeOverStart.Appearance.Options.UseFont = true;
            this.btnTakeOverStart.IsBusy = false;
            this.btnTakeOverStart.IsWrite = false;
            this.btnTakeOverStart.LanguageKey = "WIPCHECKSTART";
            this.btnTakeOverStart.Location = new System.Drawing.Point(50, 0);
            this.btnTakeOverStart.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnTakeOverStart.Name = "btnTakeOverStart";
            this.btnTakeOverStart.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnTakeOverStart.Size = new System.Drawing.Size(120, 23);
            this.btnTakeOverStart.TabIndex = 3;
            this.btnTakeOverStart.Text = "재공실사 시작";
            this.btnTakeOverStart.TooltipLanguageKey = "";
            this.btnTakeOverStart.Visible = false;
            // 
            // lblState
            // 
            this.lblState.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.lblState.Appearance.Options.UseFont = true;
            this.lblState.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblState.Location = new System.Drawing.Point(0, 0);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(464, 39);
            this.lblState.TabIndex = 1;
            this.lblState.Text = "현재 인수 인계 Locking 상태 입니다.";
            // 
            // pnlState
            // 
            this.pnlState.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlState.Controls.Add(this.lblState);
            this.pnlState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlState.Location = new System.Drawing.Point(3, 3);
            this.pnlState.Name = "pnlState";
            this.pnlState.Size = new System.Drawing.Size(464, 39);
            this.pnlState.TabIndex = 2;
            // 
            // tplWipPhysicalCount
            // 
            this.tplWipPhysicalCount.ColumnCount = 1;
            this.tplWipPhysicalCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplWipPhysicalCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplWipPhysicalCount.Controls.Add(this.pnlState, 0, 0);
            this.tplWipPhysicalCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.tplWipPhysicalCount.Location = new System.Drawing.Point(0, 0);
            this.tplWipPhysicalCount.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplWipPhysicalCount.Name = "tplWipPhysicalCount";
            this.tplWipPhysicalCount.RowCount = 2;
            this.tplWipPhysicalCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplWipPhysicalCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tplWipPhysicalCount.Size = new System.Drawing.Size(470, 55);
            this.tplWipPhysicalCount.TabIndex = 3;
            // 
            // tabLotList
            // 
            this.tabLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLotList.Location = new System.Drawing.Point(0, 55);
            this.tabLotList.Name = "tabLotList";
            this.tabLotList.SelectedTabPage = this.tpgNotCompleteList;
            this.tabLotList.Size = new System.Drawing.Size(470, 341);
            this.tabLotList.TabIndex = 4;
            this.tabLotList.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgNotCompleteList,
            this.tpgCompleteList});
            // 
            // tpgNotCompleteList
            // 
            this.tpgNotCompleteList.Controls.Add(this.grdLotList);
            this.tpgNotCompleteList.Controls.Add(this.pnlLotId);
            this.tabLotList.SetLanguageKey(this.tpgNotCompleteList, "WIP");
            this.tpgNotCompleteList.Name = "tpgNotCompleteList";
            this.tpgNotCompleteList.Size = new System.Drawing.Size(468, 311);
            this.tpgNotCompleteList.Text = "재공 현황";
            // 
            // pnlLotId
            // 
            this.pnlLotId.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlLotId.Controls.Add(this.txtLotId);
            this.pnlLotId.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLotId.Location = new System.Drawing.Point(0, 0);
            this.pnlLotId.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLotId.Name = "pnlLotId";
            this.pnlLotId.Size = new System.Drawing.Size(468, 30);
            this.pnlLotId.TabIndex = 1;
            // 
            // txtLotId
            // 
            this.txtLotId.EditorWidth = "70%";
            this.txtLotId.LabelText = "Lot No.";
            this.txtLotId.LabelWidth = "30%";
            this.txtLotId.LanguageKey = "LOTID";
            this.txtLotId.Location = new System.Drawing.Point(10, 5);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Size = new System.Drawing.Size(300, 20);
            this.txtLotId.TabIndex = 0;
            // 
            // tpgCompleteList
            // 
            this.tpgCompleteList.Controls.Add(this.grdCompleteList);
            this.tabLotList.SetLanguageKey(this.tpgCompleteList, "WIPSURVEYCOMPLETELIST");
            this.tpgCompleteList.Name = "tpgCompleteList";
            this.tpgCompleteList.Size = new System.Drawing.Size(749, 399);
            this.tpgCompleteList.Text = "실사 완료 리스트";
            // 
            // grdCompleteList
            // 
            this.grdCompleteList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCompleteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCompleteList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCompleteList.IsUsePaging = false;
            this.grdCompleteList.LanguageKey = "WIPSURVEYCOMPLETELIST";
            this.grdCompleteList.Location = new System.Drawing.Point(0, 0);
            this.grdCompleteList.Margin = new System.Windows.Forms.Padding(0);
            this.grdCompleteList.Name = "grdCompleteList";
            this.grdCompleteList.ShowBorder = true;
            this.grdCompleteList.ShowStatusBar = false;
            this.grdCompleteList.Size = new System.Drawing.Size(749, 399);
            this.grdCompleteList.TabIndex = 0;
            this.grdCompleteList.UseAutoBestFitColumns = false;
            // 
            // WIPPhysicalCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "WIPPhysicalCount";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlState)).EndInit();
            this.pnlState.ResumeLayout(false);
            this.tplWipPhysicalCount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabLotList)).EndInit();
            this.tabLotList.ResumeLayout(false);
            this.tpgNotCompleteList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLotId)).EndInit();
            this.pnlLotId.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            this.tpgCompleteList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartBandedGrid grdLotList;
		private System.Windows.Forms.FlowLayoutPanel pnlButton;
		private Framework.SmartControls.SmartButton btnWipPhysicalList;
		private Framework.SmartControls.SmartButton btnTakeOverStop;
		private Framework.SmartControls.SmartButton btnTakeOverStart;
		private Framework.SmartControls.SmartPanel pnlState;
		private Framework.SmartControls.SmartLabel lblState;
		private System.Windows.Forms.Timer timerState;
		private Framework.SmartControls.SmartSplitTableLayoutPanel tplWipPhysicalCount;
        private Framework.SmartControls.SmartTabControl tabLotList;
        private DevExpress.XtraTab.XtraTabPage tpgNotCompleteList;
        private DevExpress.XtraTab.XtraTabPage tpgCompleteList;
        private Framework.SmartControls.SmartPanel pnlLotId;
        private Framework.SmartControls.SmartLabelTextBox txtLotId;
        private Framework.SmartControls.SmartBandedGrid grdCompleteList;
    }
}