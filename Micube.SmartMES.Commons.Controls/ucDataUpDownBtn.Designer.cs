namespace Micube.SmartMES.Commons.Controls
{
    partial class ucDataUpDownBtn
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDataUpDownBtn));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDataDown = new System.Windows.Forms.Button();
            this.btnDataUp = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnDataDown, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDataUp, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(857, 42);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnDataDown
            // 
            this.btnDataDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDataDown.Image")));
            this.btnDataDown.Location = new System.Drawing.Point(371, 3);
            this.btnDataDown.Name = "btnDataDown";
            this.btnDataDown.Size = new System.Drawing.Size(54, 36);
            this.btnDataDown.TabIndex = 2;
            this.btnDataDown.UseVisualStyleBackColor = true;
            // 
            // btnDataUp
            // 
            this.btnDataUp.Image = ((System.Drawing.Image)(resources.GetObject("btnDataUp.Image")));
            this.btnDataUp.Location = new System.Drawing.Point(431, 3);
            this.btnDataUp.Name = "btnDataUp";
            this.btnDataUp.Size = new System.Drawing.Size(54, 36);
            this.btnDataUp.TabIndex = 3;
            this.btnDataUp.UseVisualStyleBackColor = true;
            // 
            // ucDataUpDownBtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ucDataUpDownBtn";
            this.Size = new System.Drawing.Size(857, 42);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnDataDown;
        private System.Windows.Forms.Button btnDataUp;
    }
}
