namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucEquipmentLOT
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
            this.pnlMain = new Micube.Framework.SmartControls.SmartPanel();
            this.ucProcessAreaGrid1 = new Micube.SmartMES.QualityAnalysis.ucProcessAreaGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.ucProcessAreaGrid1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(832, 520);
            this.pnlMain.TabIndex = 0;
            // 
            // ucProcessAreaGrid1
            // 
            this.ucProcessAreaGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucProcessAreaGrid1.Location = new System.Drawing.Point(2, 2);
            this.ucProcessAreaGrid1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucProcessAreaGrid1.Name = "ucProcessAreaGrid1";
            this.ucProcessAreaGrid1.Size = new System.Drawing.Size(828, 230);
            this.ucProcessAreaGrid1.TabIndex = 0;
            // 
            // ucAreaEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "ucAreaEquipment";
            this.Size = new System.Drawing.Size(832, 520);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel pnlMain;
        private ucProcessAreaGrid ucProcessAreaGrid1;
    }
}
