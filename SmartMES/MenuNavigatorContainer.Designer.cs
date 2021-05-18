namespace SmartMES
{
    partial class MenuNavigatorContainer
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
            this.smartBackstageViewControl1 = new Micube.Framework.SmartControls.SmartBackstageViewControl();
            ((System.ComponentModel.ISupportInitialize)(this.smartBackstageViewControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // smartBackstageViewControl1
            // 
            this.smartBackstageViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartBackstageViewControl1.Location = new System.Drawing.Point(0, 0);
            this.smartBackstageViewControl1.Name = "smartBackstageViewControl1";
            this.smartBackstageViewControl1.Size = new System.Drawing.Size(831, 468);
            this.smartBackstageViewControl1.TabIndex = 0;
            this.smartBackstageViewControl1.Text = "smartBackstageViewControl1";
            // 
            // MenuNavigatorContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartBackstageViewControl1);
            this.Name = "MenuNavigatorContainer";
            this.Size = new System.Drawing.Size(831, 468);
            ((System.ComponentModel.ISupportInitialize)(this.smartBackstageViewControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Micube.Framework.SmartControls.SmartBackstageViewControl smartBackstageViewControl1;
    }
}
