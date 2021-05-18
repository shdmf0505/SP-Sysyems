namespace Micube.SmartMES.ToolManagement
{
    partial class NewRequestMakeFilmVerYp
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
            this.grdProduct = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdFilm = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtRequestDate = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtRequestUser = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel3 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtRequestDepartment = new Micube.Framework.SmartControls.SmartTextBox();
            this.grdUserInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.grdActionFilm = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestDepartment.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 670);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.txtRequestDepartment);
            this.pnlToolbar.Controls.Add(this.smartLabel3);
            this.pnlToolbar.Controls.Add(this.txtRequestUser);
            this.pnlToolbar.Controls.Add(this.smartLabel2);
            this.pnlToolbar.Controls.Add(this.txtRequestDate);
            this.pnlToolbar.Controls.Add(this.smartLabel1);
            this.pnlToolbar.Size = new System.Drawing.Size(1087, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartLabel1, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.txtRequestDate, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.smartLabel2, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.txtRequestUser, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.smartLabel3, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.txtRequestDepartment, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(1087, 674);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1392, 703);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdProduct);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartSpliterContainer2);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1087, 674);
            this.smartSpliterContainer1.SplitterPosition = 54;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdProduct
            // 
            this.grdProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProduct.Location = new System.Drawing.Point(0, 0);
            this.grdProduct.Margin = new System.Windows.Forms.Padding(0);
            this.grdProduct.Name = "grdProduct";
            this.grdProduct.Size = new System.Drawing.Size(1087, 54);
            this.grdProduct.TabIndex = 0;
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdFilm);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdActionFilm);
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdUserInfo);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(1087, 615);
            this.smartSpliterContainer2.SplitterPosition = 275;
            this.smartSpliterContainer2.TabIndex = 0;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdFilm
            // 
            this.grdFilm.Caption = "필름목록:";
            this.grdFilm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFilm.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy)
            | Micube.Framework.SmartControls.GridButtonItem.Delete)
            | Micube.Framework.SmartControls.GridButtonItem.Preview)
            | Micube.Framework.SmartControls.GridButtonItem.Import)
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFilm.IsUsePaging = false;
            this.grdFilm.LanguageKey = "BROWSEREQUSTFILM";
            this.grdFilm.Location = new System.Drawing.Point(0, 0);
            this.grdFilm.Margin = new System.Windows.Forms.Padding(0);
            this.grdFilm.Name = "grdFilm";
            this.grdFilm.ShowBorder = true;
            this.grdFilm.Size = new System.Drawing.Size(1087, 275);
            this.grdFilm.TabIndex = 1291;
            this.grdFilm.UseAutoBestFitColumns = false;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.Options.UseTextOptions = true;
            this.smartLabel1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.smartLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.smartLabel1.Location = new System.Drawing.Point(78, 4);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(106, 20);
            this.smartLabel1.TabIndex = 0;
            this.smartLabel1.Text = "요청일";
            this.smartLabel1.Visible = false;
            // 
            // txtRequestDate
            // 
            this.txtRequestDate.LabelText = null;
            this.txtRequestDate.LanguageKey = null;
            this.txtRequestDate.Location = new System.Drawing.Point(218, 2);
            this.txtRequestDate.Name = "txtRequestDate";
            this.txtRequestDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txtRequestDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtRequestDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.txtRequestDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtRequestDate.Properties.ReadOnly = true;
            this.txtRequestDate.Size = new System.Drawing.Size(97, 20);
            this.txtRequestDate.TabIndex = 1;
            this.txtRequestDate.Visible = false;
            // 
            // smartLabel2
            // 
            this.smartLabel2.Appearance.Options.UseTextOptions = true;
            this.smartLabel2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.smartLabel2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.smartLabel2.Location = new System.Drawing.Point(333, 1);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(106, 20);
            this.smartLabel2.TabIndex = 2;
            this.smartLabel2.Text = "요청자:";
            this.smartLabel2.Visible = false;
            // 
            // txtRequestUser
            // 
            this.txtRequestUser.LabelText = null;
            this.txtRequestUser.LanguageKey = null;
            this.txtRequestUser.Location = new System.Drawing.Point(472, 1);
            this.txtRequestUser.Name = "txtRequestUser";
            this.txtRequestUser.Properties.ReadOnly = true;
            this.txtRequestUser.Size = new System.Drawing.Size(147, 20);
            this.txtRequestUser.TabIndex = 3;
            this.txtRequestUser.Visible = false;
            // 
            // smartLabel3
            // 
            this.smartLabel3.Appearance.Options.UseTextOptions = true;
            this.smartLabel3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.smartLabel3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.smartLabel3.Location = new System.Drawing.Point(625, 0);
            this.smartLabel3.Name = "smartLabel3";
            this.smartLabel3.Size = new System.Drawing.Size(106, 20);
            this.smartLabel3.TabIndex = 4;
            this.smartLabel3.Text = "요청부서:";
            this.smartLabel3.Visible = false;
            // 
            // txtRequestDepartment
            // 
            this.txtRequestDepartment.LabelText = null;
            this.txtRequestDepartment.LanguageKey = null;
            this.txtRequestDepartment.Location = new System.Drawing.Point(770, 4);
            this.txtRequestDepartment.Name = "txtRequestDepartment";
            this.txtRequestDepartment.Properties.ReadOnly = true;
            this.txtRequestDepartment.Size = new System.Drawing.Size(162, 20);
            this.txtRequestDepartment.TabIndex = 5;
            this.txtRequestDepartment.Visible = false;
            // 
            // grdUserInfo
            // 
            this.grdUserInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdUserInfo.Location = new System.Drawing.Point(0, 0);
            this.grdUserInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdUserInfo.Name = "grdUserInfo";
            this.grdUserInfo.Size = new System.Drawing.Size(1087, 29);
            this.grdUserInfo.TabIndex = 1;
            // 
            // grdActionFilm
            // 
            this.grdActionFilm.Caption = "작업대상필름목록:";
            this.grdActionFilm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdActionFilm.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy)
            | Micube.Framework.SmartControls.GridButtonItem.Delete)
            | Micube.Framework.SmartControls.GridButtonItem.Preview)
            | Micube.Framework.SmartControls.GridButtonItem.Import)
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdActionFilm.IsUsePaging = false;
            this.grdActionFilm.LanguageKey = "REGISTERREQUESTMAKINGFILM";
            this.grdActionFilm.Location = new System.Drawing.Point(0, 29);
            this.grdActionFilm.Margin = new System.Windows.Forms.Padding(0);
            this.grdActionFilm.Name = "grdActionFilm";
            this.grdActionFilm.ShowBorder = true;
            this.grdActionFilm.Size = new System.Drawing.Size(1087, 306);
            this.grdActionFilm.TabIndex = 1293;
            this.grdActionFilm.UseAutoBestFitColumns = false;
            // 
            // NewRequestMakeFilmVerINTER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 723);
            this.Name = "NewRequestMakeFilmVerINTER";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestDepartment.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdFilm;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartTextBox txtRequestDate;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartTextBox txtRequestUser;
        private Framework.SmartControls.SmartLabel smartLabel3;
        private Framework.SmartControls.SmartTextBox txtRequestDepartment;
        private Commons.Controls.SmartLotInfoGrid grdProduct;
        private Framework.SmartControls.SmartBandedGrid grdActionFilm;
        private Commons.Controls.SmartLotInfoGrid grdUserInfo;
    }
}