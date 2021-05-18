namespace Micube.SmartMES.Commons.Controls
{
    partial class ucDataLeftRightBtn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDataLeftRightBtn));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDataRight = new Micube.Framework.SmartControls.SmartButton();
            this.btnDataLeft = new Micube.Framework.SmartControls.SmartButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnDataRight, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDataLeft, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(62, 235);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnDataRight
            // 
            this.btnDataRight.AllowFocus = false;
            this.btnDataRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDataRight.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDataDown.ImageOptions.Image")));
            this.btnDataRight.IsBusy = false;
            this.btnDataRight.Location = new System.Drawing.Point(3, 68);
            this.btnDataRight.Name = "btnDataRight";
            this.btnDataRight.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDataRight.Size = new System.Drawing.Size(56, 44);
            this.btnDataRight.TabIndex = 0;
            this.btnDataRight.TooltipLanguageKey = "";
            // 
            // btnDataLeft
            // 
            this.btnDataLeft.AllowFocus = false;
            this.btnDataLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDataLeft.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDataUp.ImageOptions.Image")));
            this.btnDataLeft.IsBusy = false;
            this.btnDataLeft.Location = new System.Drawing.Point(3, 118);
            this.btnDataLeft.Name = "btnDataLeft";
            this.btnDataLeft.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDataLeft.Size = new System.Drawing.Size(56, 44);
            this.btnDataLeft.TabIndex = 1;
            this.btnDataLeft.TooltipLanguageKey = "";
            // 
            // ucDataLeftRightBtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ucDataLeftRightBtn";
            this.Size = new System.Drawing.Size(62, 235);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnDataRight;
        private Framework.SmartControls.SmartButton btnDataLeft;
    }
}
