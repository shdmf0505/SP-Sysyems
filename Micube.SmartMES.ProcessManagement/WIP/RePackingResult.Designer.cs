namespace Micube.SmartMES.ProcessManagement
{
	partial class RePackingResult
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
			Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup1 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RePackingResult));
			Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
			Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
			this.flpNewBoxInfo = new System.Windows.Forms.FlowLayoutPanel();
			this.lblArea = new Micube.Framework.SmartControls.SmartLabel();
			this.popArea = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
			this.lblWorker = new Micube.Framework.SmartControls.SmartLabel();
			this.cboUser = new Micube.Framework.SmartControls.SmartComboBox();
			this.lblNewBoxNo = new Micube.Framework.SmartControls.SmartLabel();
			this.txtNewBoxNo = new Micube.Framework.SmartControls.SmartTextBox();
			this.btnCreateBoxNo = new Micube.Framework.SmartControls.SmartButton();
			this.grdRePackingLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnStartRePacking = new Micube.Framework.SmartControls.SmartButton();
			this.btnPrintLabel = new Micube.Framework.SmartControls.SmartButton();
			this.grdPrevPackingLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.txtPrevBoxNo = new Micube.Framework.SmartControls.SmartTextBox();
			this.lblPrevBoxNo = new Micube.Framework.SmartControls.SmartLabel();
			this.flpPrevBoxInfo = new System.Windows.Forms.FlowLayoutPanel();
			this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
			this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.ucDataUpDownBtn = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
			this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlToolbar.SuspendLayout();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.flpNewBoxInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.popArea.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboUser.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNewBoxNo.Properties)).BeginInit();
			this.flowLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtPrevBoxNo.Properties)).BeginInit();
			this.flpPrevBoxInfo.SuspendLayout();
			this.smartSplitTableLayoutPanel1.SuspendLayout();
			this.smartSplitTableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(0, 30);
			this.pnlCondition.Size = new System.Drawing.Size(0, 0);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Controls.Add(this.flowLayoutPanel3);
			this.pnlToolbar.Size = new System.Drawing.Size(1227, 24);
			this.pnlToolbar.Controls.SetChildIndex(this.flowLayoutPanel3, 0);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel2);
			this.pnlContent.Controls.Add(this.ucDataUpDownBtn);
			this.pnlContent.Controls.Add(this.smartSpliterControl1);
			this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
			this.pnlContent.Size = new System.Drawing.Size(1227, 632);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(1227, 661);
			// 
			// flpNewBoxInfo
			// 
			this.flpNewBoxInfo.Controls.Add(this.lblArea);
			this.flpNewBoxInfo.Controls.Add(this.popArea);
			this.flpNewBoxInfo.Controls.Add(this.lblWorker);
			this.flpNewBoxInfo.Controls.Add(this.cboUser);
			this.flpNewBoxInfo.Controls.Add(this.lblNewBoxNo);
			this.flpNewBoxInfo.Controls.Add(this.txtNewBoxNo);
			this.flpNewBoxInfo.Controls.Add(this.btnCreateBoxNo);
			this.flpNewBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpNewBoxInfo.Location = new System.Drawing.Point(0, 0);
			this.flpNewBoxInfo.Margin = new System.Windows.Forms.Padding(0);
			this.flpNewBoxInfo.Name = "flpNewBoxInfo";
			this.flpNewBoxInfo.Size = new System.Drawing.Size(1227, 26);
			this.flpNewBoxInfo.TabIndex = 4;
			// 
			// lblArea
			// 
			this.lblArea.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblArea.LanguageKey = "AREA";
			this.lblArea.Location = new System.Drawing.Point(5, 7);
			this.lblArea.Margin = new System.Windows.Forms.Padding(5, 2, 5, 0);
			this.lblArea.Name = "lblArea";
			this.lblArea.Size = new System.Drawing.Size(30, 14);
			this.lblArea.TabIndex = 9;
			this.lblArea.Text = "작업장";
			// 
			// popArea
			// 
			this.popArea.LabelText = null;
			this.popArea.LanguageKey = null;
			this.popArea.Location = new System.Drawing.Point(43, 3);
			this.popArea.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
			this.popArea.Name = "popArea";
			conditionItemSelectPopup1.ApplySelection = null;
			conditionItemSelectPopup1.AutoFillColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.AutoFillColumnNames")));
			conditionItemSelectPopup1.CanOkNoSelection = true;
			conditionItemSelectPopup1.ConditionDefaultId = null;
			conditionItemSelectPopup1.ConditionLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
			conditionItemSelectPopup1.ConditionRequireId = "";
			conditionItemSelectPopup1.Conditions = conditionCollection1;
			conditionItemSelectPopup1.CustomPopup = null;
			conditionItemSelectPopup1.CustomValidate = null;
			conditionItemSelectPopup1.DefaultDisplayValue = null;
			conditionItemSelectPopup1.DefaultValue = null;
			conditionItemSelectPopup1.DisplayFieldName = "";
			conditionItemSelectPopup1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			conditionItemSelectPopup1.GreatThenEqual = false;
			conditionItemSelectPopup1.GreatThenId = "";
			conditionItemSelectPopup1.GridColumns = conditionCollection2;
			conditionItemSelectPopup1.Id = null;
			conditionItemSelectPopup1.InitAction = null;
			conditionItemSelectPopup1.IsEnabled = true;
			conditionItemSelectPopup1.IsHidden = false;
			conditionItemSelectPopup1.IsImmediatlyUpdate = true;
			conditionItemSelectPopup1.IsKeyColumn = false;
			conditionItemSelectPopup1.IsMultiGrid = false;
			conditionItemSelectPopup1.IsReadOnly = false;
			conditionItemSelectPopup1.IsRequired = false;
			conditionItemSelectPopup1.IsSearchOnLoading = true;
			conditionItemSelectPopup1.LabelText = null;
			conditionItemSelectPopup1.LanguageKey = null;
			conditionItemSelectPopup1.LessThenEqual = false;
			conditionItemSelectPopup1.LessThenId = "";
			conditionItemSelectPopup1.NoSelectionMessageId = "";
			conditionItemSelectPopup1.PopupButtonStyle = Micube.Framework.SmartControls.PopupButtonStyles.Ok_Cancel;
			conditionItemSelectPopup1.PopupCustomValidation = null;
			conditionItemSelectPopup1.Position = 0D;
			conditionItemSelectPopup1.QueryPopup = null;
			conditionItemSelectPopup1.RelationIds = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.RelationIds")));
			conditionItemSelectPopup1.ResultAction = null;
			conditionItemSelectPopup1.ResultCount = 1;
			conditionItemSelectPopup1.SearchQuery = null;
			conditionItemSelectPopup1.SelectionQuery = null;
			conditionItemSelectPopup1.ShowSearchButton = true;
			conditionItemSelectPopup1.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
			conditionItemSelectPopup1.Title = null;
			conditionItemSelectPopup1.ToolTip = null;
			conditionItemSelectPopup1.ToolTipLanguageKey = null;
			conditionItemSelectPopup1.ValueFieldName = "";
			conditionItemSelectPopup1.WindowSize = new System.Drawing.Size(800, 500);
			this.popArea.SelectPopupCondition = conditionItemSelectPopup1;
			this.popArea.Size = new System.Drawing.Size(190, 20);
			this.popArea.TabIndex = 10;
			// 
			// lblWorker
			// 
			this.lblWorker.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblWorker.LanguageKey = "ACTUALUSER";
			this.lblWorker.Location = new System.Drawing.Point(253, 7);
			this.lblWorker.Margin = new System.Windows.Forms.Padding(5, 2, 5, 0);
			this.lblWorker.Name = "lblWorker";
			this.lblWorker.Size = new System.Drawing.Size(30, 14);
			this.lblWorker.TabIndex = 7;
			this.lblWorker.Text = "작업자";
			// 
			// cboUser
			// 
			this.cboUser.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cboUser.LabelText = null;
			this.cboUser.LanguageKey = null;
			this.cboUser.Location = new System.Drawing.Point(288, 4);
			this.cboUser.Margin = new System.Windows.Forms.Padding(0, 2, 15, 0);
			this.cboUser.Name = "cboUser";
			this.cboUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboUser.Properties.NullText = "";
			this.cboUser.ShowHeader = true;
			this.cboUser.Size = new System.Drawing.Size(100, 20);
			this.cboUser.TabIndex = 8;
			// 
			// lblNewBoxNo
			// 
			this.lblNewBoxNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblNewBoxNo.LanguageKey = "NEWBOXNO";
			this.lblNewBoxNo.Location = new System.Drawing.Point(406, 6);
			this.lblNewBoxNo.Name = "lblNewBoxNo";
			this.lblNewBoxNo.Size = new System.Drawing.Size(63, 14);
			this.lblNewBoxNo.TabIndex = 0;
			this.lblNewBoxNo.Text = "신규 Box No";
			// 
			// txtNewBoxNo
			// 
			this.txtNewBoxNo.LabelText = null;
			this.txtNewBoxNo.LanguageKey = null;
			this.txtNewBoxNo.Location = new System.Drawing.Point(475, 3);
			this.txtNewBoxNo.Name = "txtNewBoxNo";
			this.txtNewBoxNo.Size = new System.Drawing.Size(285, 20);
			this.txtNewBoxNo.TabIndex = 1;
			// 
			// btnCreateBoxNo
			// 
			this.btnCreateBoxNo.AllowFocus = false;
			this.btnCreateBoxNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnCreateBoxNo.IsBusy = false;
			this.btnCreateBoxNo.LanguageKey = "CREATEBOXNO";
			this.btnCreateBoxNo.Location = new System.Drawing.Point(766, 1);
			this.btnCreateBoxNo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnCreateBoxNo.Name = "btnCreateBoxNo";
			this.btnCreateBoxNo.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnCreateBoxNo.Size = new System.Drawing.Size(90, 23);
			this.btnCreateBoxNo.TabIndex = 2;
			this.btnCreateBoxNo.Text = "Box 번호 생성";
			this.btnCreateBoxNo.TooltipLanguageKey = "";
			// 
			// grdRePackingLotList
			// 
			this.grdRePackingLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdRePackingLotList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdRePackingLotList.IsUsePaging = false;
			this.grdRePackingLotList.LanguageKey = "REPACKINGLOTLIST";
			this.grdRePackingLotList.Location = new System.Drawing.Point(0, 36);
			this.grdRePackingLotList.Margin = new System.Windows.Forms.Padding(0);
			this.grdRePackingLotList.Name = "grdRePackingLotList";
			this.grdRePackingLotList.ShowBorder = true;
			this.grdRePackingLotList.Size = new System.Drawing.Size(1227, 299);
			this.grdRePackingLotList.TabIndex = 5;
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.btnStartRePacking);
			this.flowLayoutPanel3.Controls.Add(this.btnPrintLabel);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(47, 0);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(1180, 24);
			this.flowLayoutPanel3.TabIndex = 6;
			// 
			// btnStartRePacking
			// 
			this.btnStartRePacking.AllowFocus = false;
			this.btnStartRePacking.IsBusy = false;
			this.btnStartRePacking.LanguageKey = "PACKINGSTART";
			this.btnStartRePacking.Location = new System.Drawing.Point(1100, 0);
			this.btnStartRePacking.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnStartRePacking.Name = "btnStartRePacking";
			this.btnStartRePacking.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnStartRePacking.Size = new System.Drawing.Size(80, 23);
			this.btnStartRePacking.TabIndex = 0;
			this.btnStartRePacking.Text = "포장작업완료";
			this.btnStartRePacking.TooltipLanguageKey = "";
			// 
			// btnPrintLabel
			// 
			this.btnPrintLabel.AllowFocus = false;
			this.btnPrintLabel.IsBusy = false;
			this.btnPrintLabel.LanguageKey = "PRINTLABEL";
			this.btnPrintLabel.Location = new System.Drawing.Point(1017, 0);
			this.btnPrintLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnPrintLabel.Name = "btnPrintLabel";
			this.btnPrintLabel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnPrintLabel.Size = new System.Drawing.Size(80, 23);
			this.btnPrintLabel.TabIndex = 1;
			this.btnPrintLabel.Text = "라벨출력";
			this.btnPrintLabel.TooltipLanguageKey = "";
			// 
			// grdPrevPackingLotList
			// 
			this.grdPrevPackingLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdPrevPackingLotList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdPrevPackingLotList.IsUsePaging = false;
			this.grdPrevPackingLotList.LanguageKey = "PREVPACKINGLOTLIST";
			this.grdPrevPackingLotList.Location = new System.Drawing.Point(0, 36);
			this.grdPrevPackingLotList.Margin = new System.Windows.Forms.Padding(0);
			this.grdPrevPackingLotList.Name = "grdPrevPackingLotList";
			this.grdPrevPackingLotList.ShowBorder = true;
			this.grdPrevPackingLotList.Size = new System.Drawing.Size(1227, 207);
			this.grdPrevPackingLotList.TabIndex = 1;
			// 
			// txtPrevBoxNo
			// 
			this.txtPrevBoxNo.LabelText = null;
			this.txtPrevBoxNo.LanguageKey = null;
			this.txtPrevBoxNo.Location = new System.Drawing.Point(72, 3);
			this.txtPrevBoxNo.Name = "txtPrevBoxNo";
			this.txtPrevBoxNo.Size = new System.Drawing.Size(285, 20);
			this.txtPrevBoxNo.TabIndex = 1;
			// 
			// lblPrevBoxNo
			// 
			this.lblPrevBoxNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblPrevBoxNo.LanguageKey = "PREVBOXNO";
			this.lblPrevBoxNo.Location = new System.Drawing.Point(3, 6);
			this.lblPrevBoxNo.Name = "lblPrevBoxNo";
			this.lblPrevBoxNo.Size = new System.Drawing.Size(63, 14);
			this.lblPrevBoxNo.TabIndex = 0;
			this.lblPrevBoxNo.Text = "이전 Box No";
			// 
			// flpPrevBoxInfo
			// 
			this.flpPrevBoxInfo.Controls.Add(this.lblPrevBoxNo);
			this.flpPrevBoxInfo.Controls.Add(this.txtPrevBoxNo);
			this.flpPrevBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpPrevBoxInfo.Location = new System.Drawing.Point(0, 0);
			this.flpPrevBoxInfo.Margin = new System.Windows.Forms.Padding(0);
			this.flpPrevBoxInfo.Name = "flpPrevBoxInfo";
			this.flpPrevBoxInfo.Size = new System.Drawing.Size(1227, 26);
			this.flpPrevBoxInfo.TabIndex = 0;
			// 
			// smartSplitTableLayoutPanel1
			// 
			this.smartSplitTableLayoutPanel1.ColumnCount = 1;
			this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.Controls.Add(this.flpPrevBoxInfo, 0, 0);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.grdPrevPackingLotList, 0, 2);
			this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
			this.smartSplitTableLayoutPanel1.RowCount = 3;
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1227, 243);
			this.smartSplitTableLayoutPanel1.TabIndex = 6;
			// 
			// smartSpliterControl1
			// 
			this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smartSpliterControl1.Location = new System.Drawing.Point(0, 243);
			this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl1.Name = "smartSpliterControl1";
			this.smartSpliterControl1.Size = new System.Drawing.Size(1227, 5);
			this.smartSpliterControl1.TabIndex = 7;
			this.smartSpliterControl1.TabStop = false;
			// 
			// ucDataUpDownBtn
			// 
			this.ucDataUpDownBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucDataUpDownBtn.Location = new System.Drawing.Point(0, 248);
			this.ucDataUpDownBtn.Name = "ucDataUpDownBtn";
			this.ucDataUpDownBtn.Size = new System.Drawing.Size(1227, 49);
			this.ucDataUpDownBtn.SourceGrid = null;
			this.ucDataUpDownBtn.TabIndex = 8;
			this.ucDataUpDownBtn.TargetGrid = null;
			// 
			// smartSplitTableLayoutPanel2
			// 
			this.smartSplitTableLayoutPanel2.ColumnCount = 1;
			this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel2.Controls.Add(this.flpNewBoxInfo, 0, 0);
			this.smartSplitTableLayoutPanel2.Controls.Add(this.grdRePackingLotList, 0, 2);
			this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 297);
			this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
			this.smartSplitTableLayoutPanel2.RowCount = 3;
			this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(1227, 335);
			this.smartSplitTableLayoutPanel2.TabIndex = 9;
			// 
			// RePackingResult
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1247, 681);
			this.ConditionsVisible = false;
			this.Name = "RePackingResult";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlToolbar.ResumeLayout(false);
			this.pnlToolbar.PerformLayout();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.flpNewBoxInfo.ResumeLayout(false);
			this.flpNewBoxInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.popArea.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboUser.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNewBoxNo.Properties)).EndInit();
			this.flowLayoutPanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtPrevBoxNo.Properties)).EndInit();
			this.flpPrevBoxInfo.ResumeLayout(false);
			this.flpPrevBoxInfo.PerformLayout();
			this.smartSplitTableLayoutPanel1.ResumeLayout(false);
			this.smartSplitTableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel flpNewBoxInfo;
		private Framework.SmartControls.SmartLabel lblNewBoxNo;
		private Framework.SmartControls.SmartTextBox txtNewBoxNo;
		private Framework.SmartControls.SmartLabel lblArea;
		private Framework.SmartControls.SmartSelectPopupEdit popArea;
		private Framework.SmartControls.SmartLabel lblWorker;
		private Framework.SmartControls.SmartComboBox cboUser;
		private Framework.SmartControls.SmartBandedGrid grdRePackingLotList;
		private Framework.SmartControls.SmartButton btnCreateBoxNo;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private Framework.SmartControls.SmartButton btnStartRePacking;
		private Framework.SmartControls.SmartButton btnPrintLabel;
		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
		private Micube.SmartMES.Commons.Controls.ucDataUpDownBtn ucDataUpDownBtn;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flpPrevBoxInfo;
		private Framework.SmartControls.SmartLabel lblPrevBoxNo;
		private Framework.SmartControls.SmartTextBox txtPrevBoxNo;
		private Framework.SmartControls.SmartBandedGrid grdPrevPackingLotList;
	}
}