namespace Micube.SmartMES.SystemManagement
{
    partial class Message
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
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdMessageClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdMessage = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdMessageClass);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdMessage);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(470, 396);
            this.smartSpliterContainer1.SplitterPosition = 300;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdMessageClass
            // 
            this.grdMessageClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMessageClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMessageClass.IsUsePaging = false;
            this.grdMessageClass.LanguageKey = "GRIDMESSAGECLASSLIST";
            this.grdMessageClass.Location = new System.Drawing.Point(0, 0);
            this.grdMessageClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdMessageClass.Name = "grdMessageClass";
            this.grdMessageClass.ShowBorder = true;
            this.grdMessageClass.Size = new System.Drawing.Size(300, 396);
            this.grdMessageClass.TabIndex = 3;
            // 
            // grdMessage
            // 
            this.grdMessage.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMessage.IsUsePaging = false;
            this.grdMessage.LanguageKey = "GRIDMESSAGELIST";
            this.grdMessage.Location = new System.Drawing.Point(0, 0);
            this.grdMessage.Margin = new System.Windows.Forms.Padding(0);
            this.grdMessage.Name = "grdMessage";
            this.grdMessage.ShowBorder = true;
            this.grdMessage.Size = new System.Drawing.Size(160, 396);
            this.grdMessage.TabIndex = 4;
            // 
            // Message
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Message";
            this.Text = "MessageManagement";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdMessage;
        private Framework.SmartControls.SmartBandedGrid grdMessageClass;
    }
}