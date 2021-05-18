namespace Micube.SmartMES.StandardInfo
{
    partial class ProductSpecificationEtcInfo_YPE
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
			this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
			this.tlpEtcInfo = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
			this.smartLabel3 = new Micube.Framework.SmartControls.SmartLabel();
			this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
			this.MemoSpecChange1 = new Micube.Framework.SmartControls.SmartMemoEdit();
			this.MemoSpecNote1 = new Micube.Framework.SmartControls.SmartMemoEdit();
			((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
			this.smartGroupBox1.SuspendLayout();
			this.tlpEtcInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MemoSpecChange1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MemoSpecNote1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// smartGroupBox1
			// 
			this.smartGroupBox1.Controls.Add(this.tlpEtcInfo);
			this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
			this.smartGroupBox1.LanguageKey = "ETCPERIOD";
			this.smartGroupBox1.Location = new System.Drawing.Point(0, 0);
			this.smartGroupBox1.Margin = new System.Windows.Forms.Padding(0);
			this.smartGroupBox1.Name = "smartGroupBox1";
			this.smartGroupBox1.ShowBorder = true;
			this.smartGroupBox1.Size = new System.Drawing.Size(1200, 165);
			this.smartGroupBox1.TabIndex = 0;
			this.smartGroupBox1.Text = "ETCPERIOD";
			// 
			// tlpEtcInfo
			// 
			this.tlpEtcInfo.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tlpEtcInfo.ColumnCount = 4;
			this.tlpEtcInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
			this.tlpEtcInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 600F));
			this.tlpEtcInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
			this.tlpEtcInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpEtcInfo.Controls.Add(this.smartLabel2, 0, 0);
			this.tlpEtcInfo.Controls.Add(this.smartLabel3, 2, 0);
			this.tlpEtcInfo.Controls.Add(this.MemoSpecChange1, 1, 0);
			this.tlpEtcInfo.Controls.Add(this.MemoSpecNote1, 3, 0);
			this.tlpEtcInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpEtcInfo.Location = new System.Drawing.Point(2, 31);
			this.tlpEtcInfo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.tlpEtcInfo.Name = "tlpEtcInfo";
			this.tlpEtcInfo.RowCount = 5;
			this.tlpEtcInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tlpEtcInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tlpEtcInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tlpEtcInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tlpEtcInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tlpEtcInfo.Size = new System.Drawing.Size(1196, 132);
			this.tlpEtcInfo.TabIndex = 1;
			// 
			// smartLabel3
			// 
			this.smartLabel3.Appearance.BackColor = System.Drawing.SystemColors.Control;
			this.smartLabel3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
			this.smartLabel3.Appearance.Options.UseBackColor = true;
			this.smartLabel3.Appearance.Options.UseForeColor = true;
			this.smartLabel3.Appearance.Options.UseTextOptions = true;
			this.smartLabel3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.smartLabel3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.smartLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartLabel3.LanguageKey = "SPECIALNOTE";
			this.smartLabel3.Location = new System.Drawing.Point(693, 1);
			this.smartLabel3.Margin = new System.Windows.Forms.Padding(0);
			this.smartLabel3.Name = "smartLabel3";
			this.tlpEtcInfo.SetRowSpan(this.smartLabel3, 5);
			this.smartLabel3.Size = new System.Drawing.Size(90, 130);
			this.smartLabel3.TabIndex = 2;
			this.smartLabel3.Text = "특이사항";
			// 
			// smartLabel2
			// 
			this.smartLabel2.Appearance.BackColor = System.Drawing.SystemColors.Control;
			this.smartLabel2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
			this.smartLabel2.Appearance.Options.UseBackColor = true;
			this.smartLabel2.Appearance.Options.UseForeColor = true;
			this.smartLabel2.Appearance.Options.UseTextOptions = true;
			this.smartLabel2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.smartLabel2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.smartLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartLabel2.LanguageKey = "SPECCHANGE";
			this.smartLabel2.Location = new System.Drawing.Point(1, 1);
			this.smartLabel2.Margin = new System.Windows.Forms.Padding(0);
			this.smartLabel2.Name = "smartLabel2";
			this.tlpEtcInfo.SetRowSpan(this.smartLabel2, 5);
			this.smartLabel2.Size = new System.Drawing.Size(90, 130);
			this.smartLabel2.TabIndex = 1;
			this.smartLabel2.Text = "사양변경";
			// 
			// MemoSpecChange1
			// 
			this.MemoSpecChange1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MemoSpecChange1.Location = new System.Drawing.Point(92, 1);
			this.MemoSpecChange1.Margin = new System.Windows.Forms.Padding(0);
			this.MemoSpecChange1.Name = "MemoSpecChange1";
			this.tlpEtcInfo.SetRowSpan(this.MemoSpecChange1, 5);
			this.MemoSpecChange1.Size = new System.Drawing.Size(600, 130);
			this.MemoSpecChange1.TabIndex = 107;
			this.MemoSpecChange1.Tag = "SPECCHANGE1";
			// 
			// MemoSpecNote1
			// 
			this.MemoSpecNote1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MemoSpecNote1.Location = new System.Drawing.Point(784, 1);
			this.MemoSpecNote1.Margin = new System.Windows.Forms.Padding(0);
			this.MemoSpecNote1.Name = "MemoSpecNote1";
			this.tlpEtcInfo.SetRowSpan(this.MemoSpecNote1, 5);
			this.MemoSpecNote1.Size = new System.Drawing.Size(411, 130);
			this.MemoSpecNote1.TabIndex = 108;
			this.MemoSpecNote1.Tag = "SPECIALNOTE1";
			// 
			// ProductSpecificationEtcInfo_YPE
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.smartGroupBox1);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "ProductSpecificationEtcInfo_YPE";
			this.Size = new System.Drawing.Size(1200, 165);
			((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
			this.smartGroupBox1.ResumeLayout(false);
			this.tlpEtcInfo.ResumeLayout(false);
			this.tlpEtcInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MemoSpecChange1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MemoSpecNote1.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpEtcInfo;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartLabel smartLabel3;
        private Framework.SmartControls.SmartMemoEdit MemoSpecChange1;
        private Framework.SmartControls.SmartMemoEdit MemoSpecNote1;
    }
}
