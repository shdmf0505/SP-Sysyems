namespace Micube.SmartMES.SystemManagement
{
    partial class SystemLog
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
            this.grdLog = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartTabControl1 = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgQueryInfo = new DevExpress.XtraTab.XtraTabPage();
            this.tpgResultMessage = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.txtRequestMessageSet = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtQueryID = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtQueryVersion = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtQueryParameter = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtResultMessage = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).BeginInit();
            this.smartTabControl1.SuspendLayout();
            this.tpgQueryInfo.SuspendLayout();
            this.tpgResultMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestMessageSet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryParameter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdLog);
            this.pnlContent.Controls.Add(this.smartSpliterControl2);
            this.pnlContent.Controls.Add(this.smartTabControl1);
            // 
            // grdLog
            // 
            this.grdLog.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLog.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLog.IsUsePaging = false;
            this.grdLog.LanguageKey = "";
            this.grdLog.Location = new System.Drawing.Point(0, 0);
            this.grdLog.Margin = new System.Windows.Forms.Padding(0);
            this.grdLog.Name = "grdLog";
            this.grdLog.ShowBorder = true;
            this.grdLog.Size = new System.Drawing.Size(475, 177);
            this.grdLog.TabIndex = 0;
            // 
            // smartTabControl1
            // 
            this.smartTabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartTabControl1.Location = new System.Drawing.Point(0, 182);
            this.smartTabControl1.Name = "smartTabControl1";
            this.smartTabControl1.SelectedTabPage = this.tpgQueryInfo;
            this.smartTabControl1.Size = new System.Drawing.Size(475, 219);
            this.smartTabControl1.TabIndex = 1;
            this.smartTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgQueryInfo,
            this.tpgResultMessage});
            // 
            // tpgQueryInfo
            // 
            this.tpgQueryInfo.Controls.Add(this.txtQueryParameter);
            this.tpgQueryInfo.Controls.Add(this.txtQueryVersion);
            this.tpgQueryInfo.Controls.Add(this.txtQueryID);
            this.tpgQueryInfo.Controls.Add(this.txtRequestMessageSet);
            this.tpgQueryInfo.Name = "tpgQueryInfo";
            this.tpgQueryInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpgQueryInfo.Size = new System.Drawing.Size(469, 190);
            this.tpgQueryInfo.Text = "Query 정보";
            // 
            // tpgResultMessage
            // 
            this.tpgResultMessage.Controls.Add(this.txtResultMessage);
            this.tpgResultMessage.Name = "tpgResultMessage";
            this.tpgResultMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tpgResultMessage.Size = new System.Drawing.Size(469, 190);
            this.tpgResultMessage.Text = "Result Message";
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartSpliterControl2.Location = new System.Drawing.Point(0, 177);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(475, 5);
            this.smartSpliterControl2.TabIndex = 2;
            this.smartSpliterControl2.TabStop = false;
            // 
            // txtRequestMessageSet
            // 
            this.txtRequestMessageSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRequestMessageSet.EditorWidth = "90%";
            this.txtRequestMessageSet.LabelText = "RequestMessageSet";
            this.txtRequestMessageSet.LabelWidth = "10%";
            this.txtRequestMessageSet.LanguageKey = null;
            this.txtRequestMessageSet.Location = new System.Drawing.Point(5, 6);
            this.txtRequestMessageSet.Name = "txtRequestMessageSet";
            this.txtRequestMessageSet.Size = new System.Drawing.Size(460, 20);
            this.txtRequestMessageSet.TabIndex = 0;
            // 
            // txtQueryID
            // 
            this.txtQueryID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQueryID.EditorWidth = "90%";
            this.txtQueryID.LabelText = "Query ID";
            this.txtQueryID.LabelWidth = "10%";
            this.txtQueryID.LanguageKey = null;
            this.txtQueryID.Location = new System.Drawing.Point(5, 32);
            this.txtQueryID.Name = "txtQueryID";
            this.txtQueryID.Size = new System.Drawing.Size(460, 20);
            this.txtQueryID.TabIndex = 0;
            // 
            // txtQueryVersion
            // 
            this.txtQueryVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQueryVersion.EditorWidth = "90%";
            this.txtQueryVersion.LabelText = "Query Version";
            this.txtQueryVersion.LabelWidth = "10%";
            this.txtQueryVersion.LanguageKey = null;
            this.txtQueryVersion.Location = new System.Drawing.Point(5, 58);
            this.txtQueryVersion.Name = "txtQueryVersion";
            this.txtQueryVersion.Size = new System.Drawing.Size(460, 20);
            this.txtQueryVersion.TabIndex = 0;
            // 
            // txtQueryParameter
            // 
            this.txtQueryParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQueryParameter.EditorWidth = "90%";
            this.txtQueryParameter.LabelText = "Query Parameter";
            this.txtQueryParameter.LabelWidth = "10%";
            this.txtQueryParameter.LanguageKey = null;
            this.txtQueryParameter.Location = new System.Drawing.Point(5, 84);
            this.txtQueryParameter.Name = "txtQueryParameter";
            this.txtQueryParameter.Size = new System.Drawing.Size(460, 20);
            this.txtQueryParameter.TabIndex = 0;
            // 
            // txtResultMessage
            // 
            this.txtResultMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResultMessage.Location = new System.Drawing.Point(3, 3);
            this.txtResultMessage.Name = "txtResultMessage";
            this.txtResultMessage.Size = new System.Drawing.Size(463, 184);
            this.txtResultMessage.TabIndex = 0;
            this.txtResultMessage.Text = "";
            // 
            // SystemLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "SystemLog";
            this.Text = "System Log";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).EndInit();
            this.smartTabControl1.ResumeLayout(false);
            this.tpgQueryInfo.ResumeLayout(false);
            this.tpgResultMessage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestMessageSet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryParameter.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdLog;
        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage tpgQueryInfo;
        private DevExpress.XtraTab.XtraTabPage tpgResultMessage;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private Framework.SmartControls.SmartLabelTextBox txtQueryParameter;
        private Framework.SmartControls.SmartLabelTextBox txtQueryVersion;
        private Framework.SmartControls.SmartLabelTextBox txtQueryID;
        private Framework.SmartControls.SmartLabelTextBox txtRequestMessageSet;
        private System.Windows.Forms.RichTextBox txtResultMessage;
    }
}