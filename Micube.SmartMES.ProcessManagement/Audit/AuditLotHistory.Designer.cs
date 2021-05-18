namespace Micube.SmartMES.ProcessManagement
{
    partial class AuditLotHistory
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
            this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabLotHist = new Micube.Framework.SmartControls.SmartTabControl();
            this.tbpMeasure = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionMeasure = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tbpConsume = new DevExpress.XtraTab.XtraTabPage();
            this.grdConsumable = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tbpDurable = new DevExpress.XtraTab.XtraTabPage();
            this.grdDurable = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tbpEquipment = new DevExpress.XtraTab.XtraTabPage();
            this.grdEquipment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tbpWeek = new DevExpress.XtraTab.XtraTabPage();
            this.grdPacking = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl6 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.grdQR = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl5 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdInkjet = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tbpFilm = new DevExpress.XtraTab.XtraTabPage();
            this.grdFilm = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tbpWTime = new DevExpress.XtraTab.XtraTabPage();
            this.grdWTIME = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tbpShipping = new DevExpress.XtraTab.XtraTabPage();
            this.grdShipping = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tbpMessage = new DevExpress.XtraTab.XtraTabPage();
            this.grdMessage = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl3 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtComment = new Micube.Framework.SmartControls.SmartRichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtTitle = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblMessageTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.grdLotRouting = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.pnlLotHistory = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabLotHist)).BeginInit();
            this.tabLotHist.SuspendLayout();
            this.tbpMeasure.SuspendLayout();
            this.tbpConsume.SuspendLayout();
            this.tbpDurable.SuspendLayout();
            this.tbpEquipment.SuspendLayout();
            this.tbpWeek.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tbpFilm.SuspendLayout();
            this.tbpWTime.SuspendLayout();
            this.tbpShipping.SuspendLayout();
            this.tbpMessage.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            this.pnlLotHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 653);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(968, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.pnlLotHistory);
            this.pnlContent.Controls.Add(this.spcSpliter);
            this.pnlContent.Controls.Add(this.tabLotHist);
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Size = new System.Drawing.Size(968, 657);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1273, 686);
            // 
            // grdLotInfo
            // 
            this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotInfo.Location = new System.Drawing.Point(0, 0);
            this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotInfo.Name = "grdLotInfo";
            this.grdLotInfo.Size = new System.Drawing.Size(968, 125);
            this.grdLotInfo.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdLotInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel1.Size = new System.Drawing.Size(968, 128);
            this.panel1.TabIndex = 1;
            // 
            // tabLotHist
            // 
            this.tabLotHist.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabLotHist.Location = new System.Drawing.Point(0, 279);
            this.tabLotHist.Name = "tabLotHist";
            this.tabLotHist.Padding = new System.Windows.Forms.Padding(3);
            this.tabLotHist.SelectedTabPage = this.tbpMeasure;
            this.tabLotHist.Size = new System.Drawing.Size(968, 378);
            this.tabLotHist.TabIndex = 2;
            this.tabLotHist.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tbpMeasure,
            this.tbpConsume,
            this.tbpDurable,
            this.tbpEquipment,
            this.tbpWeek,
            this.tbpFilm,
            this.tbpWTime,
            this.tbpShipping,
            this.tbpMessage});
            // 
            // tbpMeasure
            // 
            this.tbpMeasure.Controls.Add(this.grdInspectionMeasure);
            this.tabLotHist.SetLanguageKey(this.tbpMeasure, "MEASUREMENTVALUEINFORMATION");
            this.tbpMeasure.Name = "tbpMeasure";
            this.tbpMeasure.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMeasure.Size = new System.Drawing.Size(962, 349);
            this.tbpMeasure.Text = "계측값";
            // 
            // grdInspectionMeasure
            // 
            this.grdInspectionMeasure.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionMeasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionMeasure.IsUsePaging = false;
            this.grdInspectionMeasure.LanguageKey = "MEASUREMENTVALUEINFORMATION";
            this.grdInspectionMeasure.Location = new System.Drawing.Point(3, 3);
            this.grdInspectionMeasure.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionMeasure.Name = "grdInspectionMeasure";
            this.grdInspectionMeasure.ShowBorder = true;
            this.grdInspectionMeasure.ShowStatusBar = false;
            this.grdInspectionMeasure.Size = new System.Drawing.Size(956, 343);
            this.grdInspectionMeasure.TabIndex = 11;
            // 
            // tbpConsume
            // 
            this.tbpConsume.Controls.Add(this.grdConsumable);
            this.tabLotHist.SetLanguageKey(this.tbpConsume, "MATERIALINFO");
            this.tbpConsume.Name = "tbpConsume";
            this.tbpConsume.Padding = new System.Windows.Forms.Padding(3);
            this.tbpConsume.Size = new System.Drawing.Size(469, 349);
            this.tbpConsume.Text = "원부자재";
            // 
            // grdConsumable
            // 
            this.grdConsumable.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdConsumable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdConsumable.IsUsePaging = false;
            this.grdConsumable.LanguageKey = "MATERIALINFO";
            this.grdConsumable.Location = new System.Drawing.Point(3, 3);
            this.grdConsumable.Margin = new System.Windows.Forms.Padding(0);
            this.grdConsumable.Name = "grdConsumable";
            this.grdConsumable.ShowBorder = true;
            this.grdConsumable.ShowStatusBar = false;
            this.grdConsumable.Size = new System.Drawing.Size(463, 343);
            this.grdConsumable.TabIndex = 11;
            // 
            // tbpDurable
            // 
            this.tbpDurable.Controls.Add(this.grdDurable);
            this.tabLotHist.SetLanguageKey(this.tbpDurable, "DURABLE");
            this.tbpDurable.Name = "tbpDurable";
            this.tbpDurable.Padding = new System.Windows.Forms.Padding(3);
            this.tbpDurable.Size = new System.Drawing.Size(469, 349);
            this.tbpDurable.Text = "치공구";
            // 
            // grdDurable
            // 
            this.grdDurable.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDurable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDurable.IsUsePaging = false;
            this.grdDurable.LanguageKey = "DURABLEINFO";
            this.grdDurable.Location = new System.Drawing.Point(3, 3);
            this.grdDurable.Margin = new System.Windows.Forms.Padding(0);
            this.grdDurable.Name = "grdDurable";
            this.grdDurable.ShowBorder = true;
            this.grdDurable.ShowStatusBar = false;
            this.grdDurable.Size = new System.Drawing.Size(463, 343);
            this.grdDurable.TabIndex = 11;
            // 
            // tbpEquipment
            // 
            this.tbpEquipment.Controls.Add(this.grdEquipment);
            this.tabLotHist.SetLanguageKey(this.tbpEquipment, "EQUIPMENT");
            this.tbpEquipment.Name = "tbpEquipment";
            this.tbpEquipment.Padding = new System.Windows.Forms.Padding(3);
            this.tbpEquipment.Size = new System.Drawing.Size(469, 349);
            this.tbpEquipment.Text = "설비";
            // 
            // grdEquipment
            // 
            this.grdEquipment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipment.IsUsePaging = false;
            this.grdEquipment.LanguageKey = "EQUIPMENT";
            this.grdEquipment.Location = new System.Drawing.Point(3, 3);
            this.grdEquipment.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipment.Name = "grdEquipment";
            this.grdEquipment.ShowBorder = true;
            this.grdEquipment.ShowStatusBar = false;
            this.grdEquipment.Size = new System.Drawing.Size(463, 343);
            this.grdEquipment.TabIndex = 12;
            // 
            // tbpWeek
            // 
            this.tbpWeek.Controls.Add(this.grdPacking);
            this.tbpWeek.Controls.Add(this.smartSpliterControl6);
            this.tbpWeek.Controls.Add(this.panel4);
            this.tabLotHist.SetLanguageKey(this.tbpWeek, "WEEK");
            this.tbpWeek.Name = "tbpWeek";
            this.tbpWeek.Padding = new System.Windows.Forms.Padding(3);
            this.tbpWeek.Size = new System.Drawing.Size(469, 349);
            this.tbpWeek.Text = "주차정보";
            // 
            // grdPacking
            // 
            this.grdPacking.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPacking.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdPacking.IsUsePaging = false;
            this.grdPacking.LanguageKey = "WEEK";
            this.grdPacking.Location = new System.Drawing.Point(3, 181);
            this.grdPacking.Margin = new System.Windows.Forms.Padding(0);
            this.grdPacking.Name = "grdPacking";
            this.grdPacking.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.grdPacking.ShowBorder = true;
            this.grdPacking.ShowStatusBar = false;
            this.grdPacking.Size = new System.Drawing.Size(419, 165);
            this.grdPacking.TabIndex = 13;
            // 
            // smartSpliterControl6
            // 
            this.smartSpliterControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl6.Location = new System.Drawing.Point(3, 176);
            this.smartSpliterControl6.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl6.Name = "smartSpliterControl6";
            this.smartSpliterControl6.Size = new System.Drawing.Size(463, 5);
            this.smartSpliterControl6.TabIndex = 15;
            this.smartSpliterControl6.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.grdQR);
            this.panel4.Controls.Add(this.smartSpliterControl5);
            this.panel4.Controls.Add(this.grdInkjet);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(463, 173);
            this.panel4.TabIndex = 14;
            // 
            // grdQR
            // 
            this.grdQR.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdQR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQR.IsUsePaging = false;
            this.grdQR.LanguageKey = "WEEK";
            this.grdQR.Location = new System.Drawing.Point(419, 0);
            this.grdQR.Margin = new System.Windows.Forms.Padding(0);
            this.grdQR.Name = "grdQR";
            this.grdQR.ShowBorder = true;
            this.grdQR.ShowStatusBar = false;
            this.grdQR.Size = new System.Drawing.Size(44, 173);
            this.grdQR.TabIndex = 12;
            // 
            // smartSpliterControl5
            // 
            this.smartSpliterControl5.Location = new System.Drawing.Point(414, 0);
            this.smartSpliterControl5.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl5.Name = "smartSpliterControl5";
            this.smartSpliterControl5.Size = new System.Drawing.Size(5, 173);
            this.smartSpliterControl5.TabIndex = 13;
            this.smartSpliterControl5.TabStop = false;
            // 
            // grdInkjet
            // 
            this.grdInkjet.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInkjet.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdInkjet.IsUsePaging = false;
            this.grdInkjet.LanguageKey = "WEEK";
            this.grdInkjet.Location = new System.Drawing.Point(0, 0);
            this.grdInkjet.Margin = new System.Windows.Forms.Padding(0);
            this.grdInkjet.Name = "grdInkjet";
            this.grdInkjet.ShowBorder = true;
            this.grdInkjet.ShowStatusBar = false;
            this.grdInkjet.Size = new System.Drawing.Size(414, 173);
            this.grdInkjet.TabIndex = 11;
            // 
            // tbpFilm
            // 
            this.tbpFilm.Controls.Add(this.grdFilm);
            this.tbpFilm.Name = "tbpFilm";
            this.tbpFilm.Padding = new System.Windows.Forms.Padding(3);
            this.tbpFilm.Size = new System.Drawing.Size(469, 349);
            this.tbpFilm.Text = "Film";
            // 
            // grdFilm
            // 
            this.grdFilm.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdFilm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFilm.IsUsePaging = false;
            this.grdFilm.LanguageKey = "FILMINFO";
            this.grdFilm.Location = new System.Drawing.Point(3, 3);
            this.grdFilm.Margin = new System.Windows.Forms.Padding(0);
            this.grdFilm.Name = "grdFilm";
            this.grdFilm.ShowBorder = true;
            this.grdFilm.ShowStatusBar = false;
            this.grdFilm.Size = new System.Drawing.Size(463, 343);
            this.grdFilm.TabIndex = 11;
            // 
            // tbpWTime
            // 
            this.tbpWTime.Controls.Add(this.grdWTIME);
            this.tbpWTime.Name = "tbpWTime";
            this.tbpWTime.Padding = new System.Windows.Forms.Padding(3);
            this.tbpWTime.Size = new System.Drawing.Size(469, 349);
            this.tbpWTime.Text = "W-Time";
            // 
            // grdWTIME
            // 
            this.grdWTIME.Caption = "W-TIME";
            this.grdWTIME.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWTIME.IsUsePaging = false;
            this.grdWTIME.LanguageKey = "";
            this.grdWTIME.Location = new System.Drawing.Point(3, 3);
            this.grdWTIME.Margin = new System.Windows.Forms.Padding(0);
            this.grdWTIME.Name = "grdWTIME";
            this.grdWTIME.ShowBorder = true;
            this.grdWTIME.ShowStatusBar = false;
            this.grdWTIME.Size = new System.Drawing.Size(463, 343);
            this.grdWTIME.TabIndex = 11;
            // 
            // tbpShipping
            // 
            this.tbpShipping.Controls.Add(this.grdShipping);
            this.tabLotHist.SetLanguageKey(this.tbpShipping, "SHIPPINGINFO");
            this.tbpShipping.Name = "tbpShipping";
            this.tbpShipping.Padding = new System.Windows.Forms.Padding(3);
            this.tbpShipping.Size = new System.Drawing.Size(962, 349);
            this.tbpShipping.Text = "출하정보";
            // 
            // grdShipping
            // 
            this.grdShipping.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdShipping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdShipping.IsUsePaging = false;
            this.grdShipping.LanguageKey = "SHIPPINGINFO";
            this.grdShipping.Location = new System.Drawing.Point(3, 3);
            this.grdShipping.Margin = new System.Windows.Forms.Padding(0);
            this.grdShipping.Name = "grdShipping";
            this.grdShipping.ShowBorder = true;
            this.grdShipping.ShowStatusBar = false;
            this.grdShipping.Size = new System.Drawing.Size(956, 343);
            this.grdShipping.TabIndex = 11;
            // 
            // tbpMessage
            // 
            this.tbpMessage.Controls.Add(this.grdMessage);
            this.tbpMessage.Controls.Add(this.smartSpliterControl3);
            this.tbpMessage.Controls.Add(this.panel2);
            this.tabLotHist.SetLanguageKey(this.tbpMessage, "MESSAGEINFO");
            this.tbpMessage.Name = "tbpMessage";
            this.tbpMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMessage.Size = new System.Drawing.Size(469, 349);
            this.tbpMessage.Text = "메시지 정보";
            // 
            // grdMessage
            // 
            this.grdMessage.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMessage.IsUsePaging = false;
            this.grdMessage.LanguageKey = "MESSAGEINFO";
            this.grdMessage.Location = new System.Drawing.Point(3, 3);
            this.grdMessage.Margin = new System.Windows.Forms.Padding(0);
            this.grdMessage.Name = "grdMessage";
            this.grdMessage.ShowBorder = true;
            this.grdMessage.ShowStatusBar = false;
            this.grdMessage.Size = new System.Drawing.Size(43, 343);
            this.grdMessage.TabIndex = 11;
            // 
            // smartSpliterControl3
            // 
            this.smartSpliterControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.smartSpliterControl3.Location = new System.Drawing.Point(46, 3);
            this.smartSpliterControl3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl3.Name = "smartSpliterControl3";
            this.smartSpliterControl3.Size = new System.Drawing.Size(5, 343);
            this.smartSpliterControl3.TabIndex = 14;
            this.smartSpliterControl3.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtComment);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(51, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(415, 343);
            this.panel2.TabIndex = 15;
            // 
            // txtComment
            // 
            this.txtComment.AccessibleRole = System.Windows.Forms.AccessibleRole.SplitButton;
            this.txtComment.AlignCenterVisible = false;
            this.txtComment.AlignLeftVisible = false;
            this.txtComment.AlignRightVisible = false;
            this.txtComment.BoldVisible = false;
            this.txtComment.BulletsVisible = false;
            this.txtComment.ChooseFontVisible = false;
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtComment.FontColorVisible = false;
            this.txtComment.FontFamilyVisible = false;
            this.txtComment.FontSizeVisible = false;
            this.txtComment.INDENT = 10;
            this.txtComment.ItalicVisible = false;
            this.txtComment.Location = new System.Drawing.Point(0, 34);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtComment.Name = "txtComment";
            this.txtComment.Rtf = "{\\rtf1\\ansi\\ansicpg949\\deff0\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset" +
    "204 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18\\par\r\n}\r\n";
            this.txtComment.SeparatorAlignVisible = false;
            this.txtComment.SeparatorBoldUnderlineItalicVisible = false;
            this.txtComment.SeparatorFontColorVisible = false;
            this.txtComment.SeparatorFontVisible = false;
            this.txtComment.Size = new System.Drawing.Size(415, 309);
            this.txtComment.TabIndex = 3;
            this.txtComment.ToolStripVisible = false;
            this.txtComment.UnderlineVisible = false;
            this.txtComment.WordWrapVisible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtTitle);
            this.panel3.Controls.Add(this.lblMessageTitle);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(415, 34);
            this.panel3.TabIndex = 0;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.LabelText = null;
            this.txtTitle.LanguageKey = null;
            this.txtTitle.Location = new System.Drawing.Point(58, 7);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(353, 20);
            this.txtTitle.TabIndex = 3;
            // 
            // lblMessageTitle
            // 
            this.lblMessageTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessageTitle.LanguageKey = "TITLE";
            this.lblMessageTitle.Location = new System.Drawing.Point(6, 10);
            this.lblMessageTitle.Name = "lblMessageTitle";
            this.lblMessageTitle.Size = new System.Drawing.Size(32, 14);
            this.lblMessageTitle.TabIndex = 2;
            this.lblMessageTitle.Text = "제목 : ";
            // 
            // grdLotRouting
            // 
            this.grdLotRouting.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotRouting.IsUsePaging = false;
            this.grdLotRouting.LanguageKey = "ROUTING";
            this.grdLotRouting.Location = new System.Drawing.Point(0, 0);
            this.grdLotRouting.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotRouting.Name = "grdLotRouting";
            this.grdLotRouting.ShowBorder = true;
            this.grdLotRouting.ShowStatusBar = false;
            this.grdLotRouting.Size = new System.Drawing.Size(968, 146);
            this.grdLotRouting.TabIndex = 10;
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spcSpliter.Location = new System.Drawing.Point(0, 274);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(968, 5);
            this.spcSpliter.TabIndex = 8;
            this.spcSpliter.TabStop = false;
            // 
            // pnlLotHistory
            // 
            this.pnlLotHistory.Controls.Add(this.grdLotRouting);
            this.pnlLotHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLotHistory.Location = new System.Drawing.Point(0, 128);
            this.pnlLotHistory.Name = "pnlLotHistory";
            this.pnlLotHistory.Size = new System.Drawing.Size(968, 146);
            this.pnlLotHistory.TabIndex = 9;
            // 
            // AuditLotHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 706);
            this.LanguageKey = "LOTHISTORY";
            this.Name = "AuditLotHistory";
            this.Text = "Lot History";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabLotHist)).EndInit();
            this.tabLotHist.ResumeLayout(false);
            this.tbpMeasure.ResumeLayout(false);
            this.tbpConsume.ResumeLayout(false);
            this.tbpDurable.ResumeLayout(false);
            this.tbpEquipment.ResumeLayout(false);
            this.tbpWeek.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tbpFilm.ResumeLayout(false);
            this.tbpWTime.ResumeLayout(false);
            this.tbpShipping.ResumeLayout(false);
            this.tbpMessage.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            this.pnlLotHistory.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Commons.Controls.SmartLotInfoGrid grdLotInfo;
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartTabControl tabLotHist;
        private DevExpress.XtraTab.XtraTabPage tbpMeasure;
        private DevExpress.XtraTab.XtraTabPage tbpConsume;
        private DevExpress.XtraTab.XtraTabPage tbpDurable;
        private DevExpress.XtraTab.XtraTabPage tbpWeek;
        private DevExpress.XtraTab.XtraTabPage tbpFilm;
        private DevExpress.XtraTab.XtraTabPage tbpWTime;
        private DevExpress.XtraTab.XtraTabPage tbpShipping;
        private DevExpress.XtraTab.XtraTabPage tbpMessage;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private Framework.SmartControls.SmartBandedGrid grdLotRouting;
        private Framework.SmartControls.SmartBandedGrid grdInspectionMeasure;
        private Framework.SmartControls.SmartBandedGrid grdConsumable;
        private Framework.SmartControls.SmartBandedGrid grdDurable;
        private Framework.SmartControls.SmartBandedGrid grdInkjet;
        private Framework.SmartControls.SmartBandedGrid grdFilm;
        private Framework.SmartControls.SmartBandedGrid grdWTIME;
        private Framework.SmartControls.SmartBandedGrid grdShipping;
        private Framework.SmartControls.SmartBandedGrid grdMessage;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private Framework.SmartControls.SmartTextBox txtTitle;
        private Framework.SmartControls.SmartLabel lblMessageTitle;
        private Framework.SmartControls.SmartRichTextBox txtComment;
        private Framework.SmartControls.SmartBandedGrid grdQR;
        private Framework.SmartControls.SmartBandedGrid grdPacking;
        private DevExpress.XtraTab.XtraTabPage tbpEquipment;
        private Framework.SmartControls.SmartBandedGrid grdEquipment;
        private System.Windows.Forms.Panel panel4;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl5;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl6;
        private System.Windows.Forms.Panel pnlLotHistory;
    }
}