namespace Micube.SmartMES.EquipmentManagement
{
    partial class RegistPMWorkOrder
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
            DevExpress.XtraScheduler.TimeRuler timeRuler1 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler2 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler3 = new DevExpress.XtraScheduler.TimeRuler();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel6 = new Micube.Framework.SmartControls.SmartLabel();
            this.lblCreate = new Micube.Framework.SmartControls.SmartLabel();
            this.lblFinish = new Micube.Framework.SmartControls.SmartLabel();
            this.lblSkip = new Micube.Framework.SmartControls.SmartLabel();
            this.lblDelay = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.schPlan = new Micube.Framework.SmartControls.SmartSchedulerControl();
            this.schedulerDataStorage1 = new DevExpress.XtraScheduler.SchedulerDataStorage(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerDataStorage1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 664);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1149, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.schPlan);
            this.pnlContent.Controls.Add(this.smartPanel1);
            this.pnlContent.Size = new System.Drawing.Size(1149, 668);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1454, 697);
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.smartLabel2);
            this.smartPanel1.Controls.Add(this.smartLabel6);
            this.smartPanel1.Controls.Add(this.lblCreate);
            this.smartPanel1.Controls.Add(this.lblFinish);
            this.smartPanel1.Controls.Add(this.lblSkip);
            this.smartPanel1.Controls.Add(this.lblDelay);
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1149, 36);
            this.smartPanel1.TabIndex = 103;
            // 
            // smartLabel2
            // 
            this.smartLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.smartLabel2.Appearance.BackColor = System.Drawing.Color.PowderBlue;
            this.smartLabel2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.smartLabel2.Appearance.Options.UseBackColor = true;
            this.smartLabel2.Appearance.Options.UseFont = true;
            this.smartLabel2.Appearance.Options.UseTextOptions = true;
            this.smartLabel2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.smartLabel2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.smartLabel2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.smartLabel2.LanguageKey = "";
            this.smartLabel2.Location = new System.Drawing.Point(804, 5);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(80, 26);
            this.smartLabel2.TabIndex = 119;
            this.smartLabel2.Text = "Start";
            // 
            // smartLabel6
            // 
            this.smartLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.smartLabel6.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.smartLabel6.Appearance.Options.UseFont = true;
            this.smartLabel6.LanguageKey = "LEGEND";
            this.smartLabel6.Location = new System.Drawing.Point(670, 9);
            this.smartLabel6.Name = "smartLabel6";
            this.smartLabel6.Size = new System.Drawing.Size(28, 19);
            this.smartLabel6.TabIndex = 118;
            this.smartLabel6.Text = "범례";
            // 
            // lblCreate
            // 
            this.lblCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCreate.Appearance.BackColor = System.Drawing.Color.Gold;
            this.lblCreate.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblCreate.Appearance.Options.UseBackColor = true;
            this.lblCreate.Appearance.Options.UseFont = true;
            this.lblCreate.Appearance.Options.UseTextOptions = true;
            this.lblCreate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblCreate.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblCreate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCreate.LanguageKey = "";
            this.lblCreate.Location = new System.Drawing.Point(718, 5);
            this.lblCreate.Name = "lblCreate";
            this.lblCreate.Size = new System.Drawing.Size(80, 26);
            this.lblCreate.TabIndex = 117;
            this.lblCreate.Text = "Create";
            // 
            // lblFinish
            // 
            this.lblFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinish.Appearance.BackColor = System.Drawing.Color.LimeGreen;
            this.lblFinish.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblFinish.Appearance.Options.UseBackColor = true;
            this.lblFinish.Appearance.Options.UseFont = true;
            this.lblFinish.Appearance.Options.UseTextOptions = true;
            this.lblFinish.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblFinish.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblFinish.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFinish.LanguageKey = "";
            this.lblFinish.Location = new System.Drawing.Point(890, 5);
            this.lblFinish.Name = "lblFinish";
            this.lblFinish.Size = new System.Drawing.Size(80, 26);
            this.lblFinish.TabIndex = 116;
            this.lblFinish.Text = "Finish";
            // 
            // lblSkip
            // 
            this.lblSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSkip.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.lblSkip.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblSkip.Appearance.Options.UseBackColor = true;
            this.lblSkip.Appearance.Options.UseFont = true;
            this.lblSkip.Appearance.Options.UseTextOptions = true;
            this.lblSkip.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblSkip.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblSkip.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSkip.LanguageKey = "";
            this.lblSkip.Location = new System.Drawing.Point(976, 5);
            this.lblSkip.Name = "lblSkip";
            this.lblSkip.Size = new System.Drawing.Size(80, 26);
            this.lblSkip.TabIndex = 115;
            this.lblSkip.Text = "Skip";
            // 
            // lblDelay
            // 
            this.lblDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDelay.Appearance.BackColor = System.Drawing.Color.Salmon;
            this.lblDelay.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblDelay.Appearance.Options.UseBackColor = true;
            this.lblDelay.Appearance.Options.UseFont = true;
            this.lblDelay.Appearance.Options.UseTextOptions = true;
            this.lblDelay.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDelay.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblDelay.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDelay.LanguageKey = "";
            this.lblDelay.Location = new System.Drawing.Point(1062, 5);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(80, 26);
            this.lblDelay.TabIndex = 114;
            this.lblDelay.Text = "Delay";
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.smartLabel1.Appearance.Options.UseFont = true;
            this.smartLabel1.LanguageKey = "EQUIPMENTPMRESULTLIST";
            this.smartLabel1.Location = new System.Drawing.Point(5, 5);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(98, 19);
            this.smartLabel1.TabIndex = 113;
            this.smartLabel1.Text = "PM 계획등록 : ";
            // 
            // schPlan
            // 
            this.schPlan.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;
            this.schPlan.DataStorage = this.schedulerDataStorage1;
            this.schPlan.DateNavigationBar.Visible = false;
            this.schPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schPlan.Location = new System.Drawing.Point(0, 36);
            this.schPlan.Name = "schPlan";
            this.schPlan.ResourceNavigator.Visibility = DevExpress.XtraScheduler.ResourceNavigatorVisibility.Never;
            this.schPlan.Size = new System.Drawing.Size(1149, 632);
            this.schPlan.Start = new System.DateTime(2019, 9, 8, 0, 0, 0, 0);
            this.schPlan.TabIndex = 104;
            this.schPlan.Text = "smartSchedulerControl1";
            this.schPlan.Views.DayView.TimeRulers.Add(timeRuler1);
            this.schPlan.Views.FullWeekView.Enabled = true;
            this.schPlan.Views.FullWeekView.TimeRulers.Add(timeRuler2);
            this.schPlan.Views.WeekView.Enabled = false;
            this.schPlan.Views.WorkWeekView.TimeRulers.Add(timeRuler3);
            // 
            // schedulerDataStorage1
            // 
            this.schedulerDataStorage1.AppointmentDependencies.AutoReload = false;
            this.schedulerDataStorage1.Labels.ColorSaving = DevExpress.XtraScheduler.DXColorSavingType.ArgbColor;
            // 
            // RegistPMWorkOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 717);
            this.Name = "RegistPMWorkOrder";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerDataStorage1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartSchedulerControl schPlan;
        private DevExpress.XtraScheduler.SchedulerDataStorage schedulerDataStorage1;
        private Framework.SmartControls.SmartLabel smartLabel6;
        private Framework.SmartControls.SmartLabel lblCreate;
        private Framework.SmartControls.SmartLabel lblFinish;
        private Framework.SmartControls.SmartLabel lblSkip;
        private Framework.SmartControls.SmartLabel lblDelay;
        private Framework.SmartControls.SmartLabel smartLabel2;
    }
}