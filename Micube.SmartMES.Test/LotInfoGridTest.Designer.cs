namespace Micube.SmartMES.Test
{
    partial class LotInfoGridTest
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
            this.smartLotInforGrid1 = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartLotInforGrid1);
            // 
            // smartLotInforGrid1
            // 
            this.smartLotInforGrid1.ColumnCount = 4;
            this.smartLotInforGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLotInforGrid1.Location = new System.Drawing.Point(0, 0);
            this.smartLotInforGrid1.Margin = new System.Windows.Forms.Padding(0);
            this.smartLotInforGrid1.Name = "smartLotInforGrid1";
            this.smartLotInforGrid1.Size = new System.Drawing.Size(470, 396);
            this.smartLotInforGrid1.TabIndex = 0;
            // 
            // LotInfoGridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "LotInfoGridTest";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Commons.Controls.SmartLotInfoGrid smartLotInforGrid1;
    }
}