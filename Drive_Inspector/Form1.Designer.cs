namespace Drive_Inspector
{
    partial class Form1
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
                notify_icon.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label4 = new System.Windows.Forms.Label();
            this.gbox_reporte = new System.Windows.Forms.GroupBox();
            this.rbttn_hourly = new System.Windows.Forms.RadioButton();
            this.rbttn_daily = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.report_selected_path_tbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbox_InspectOptions = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbox_deleted = new System.Windows.Forms.CheckBox();
            this.cbox_Renamed = new System.Windows.Forms.CheckBox();
            this.cbox_created = new System.Windows.Forms.CheckBox();
            this.checklist_drives = new System.Windows.Forms.ListBox();
            this.timer_check_drives = new System.Windows.Forms.Timer(this.components);
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.timer_hourly_reports = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.gbox_reporte.SuspendLayout();
            this.gbox_InspectOptions.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // gbox_reporte
            // 
            this.gbox_reporte.Controls.Add(this.rbttn_hourly);
            this.gbox_reporte.Controls.Add(this.rbttn_daily);
            this.gbox_reporte.Controls.Add(this.label3);
            this.gbox_reporte.Controls.Add(this.button1);
            this.gbox_reporte.Controls.Add(this.report_selected_path_tbox);
            this.gbox_reporte.Controls.Add(this.label2);
            resources.ApplyResources(this.gbox_reporte, "gbox_reporte");
            this.gbox_reporte.Name = "gbox_reporte";
            this.gbox_reporte.TabStop = false;
            // 
            // rbttn_hourly
            // 
            resources.ApplyResources(this.rbttn_hourly, "rbttn_hourly");
            this.rbttn_hourly.Name = "rbttn_hourly";
            this.rbttn_hourly.UseVisualStyleBackColor = true;
            this.rbttn_hourly.CheckedChanged += new System.EventHandler(this.rbttn_hourly_CheckedChanged);
            // 
            // rbttn_daily
            // 
            resources.ApplyResources(this.rbttn_daily, "rbttn_daily");
            this.rbttn_daily.Checked = true;
            this.rbttn_daily.Name = "rbttn_daily";
            this.rbttn_daily.TabStop = true;
            this.rbttn_daily.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // report_selected_path_tbox
            // 
            resources.ApplyResources(this.report_selected_path_tbox, "report_selected_path_tbox");
            this.report_selected_path_tbox.Name = "report_selected_path_tbox";
            this.report_selected_path_tbox.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // gbox_InspectOptions
            // 
            this.gbox_InspectOptions.Controls.Add(this.label1);
            this.gbox_InspectOptions.Controls.Add(this.cbox_deleted);
            this.gbox_InspectOptions.Controls.Add(this.cbox_Renamed);
            this.gbox_InspectOptions.Controls.Add(this.cbox_created);
            resources.ApplyResources(this.gbox_InspectOptions, "gbox_InspectOptions");
            this.gbox_InspectOptions.Name = "gbox_InspectOptions";
            this.gbox_InspectOptions.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cbox_deleted
            // 
            resources.ApplyResources(this.cbox_deleted, "cbox_deleted");
            this.cbox_deleted.Name = "cbox_deleted";
            this.cbox_deleted.UseVisualStyleBackColor = true;
            this.cbox_deleted.CheckStateChanged += new System.EventHandler(this.cbox_deleted_CheckStateChanged);
            // 
            // cbox_Renamed
            // 
            resources.ApplyResources(this.cbox_Renamed, "cbox_Renamed");
            this.cbox_Renamed.Name = "cbox_Renamed";
            this.cbox_Renamed.UseVisualStyleBackColor = true;
            this.cbox_Renamed.CheckStateChanged += new System.EventHandler(this.cbox_Renamed_CheckStateChanged);
            // 
            // cbox_created
            // 
            resources.ApplyResources(this.cbox_created, "cbox_created");
            this.cbox_created.Checked = true;
            this.cbox_created.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbox_created.Name = "cbox_created";
            this.cbox_created.UseVisualStyleBackColor = true;
            this.cbox_created.CheckStateChanged += new System.EventHandler(this.cbox_created_CheckStateChanged);
            // 
            // checklist_drives
            // 
            this.checklist_drives.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            resources.ApplyResources(this.checklist_drives, "checklist_drives");
            this.checklist_drives.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(0)))));
            this.checklist_drives.FormattingEnabled = true;
            this.checklist_drives.Name = "checklist_drives";
            this.checklist_drives.Sorted = true;
            // 
            // timer_check_drives
            // 
            this.timer_check_drives.Interval = 1000;
            this.timer_check_drives.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timer_hourly_reports
            // 
            this.timer_hourly_reports.Interval = 60000;
            this.timer_hourly_reports.Tick += new System.EventHandler(this.timer_hourly_reports_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.gbox_InspectOptions);
            this.panel1.Controls.Add(this.gbox_reporte);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.checklist_drives);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label5.Name = "label5";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Opacity = 0.95D;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.gbox_reporte.ResumeLayout(false);
            this.gbox_reporte.PerformLayout();
            this.gbox_InspectOptions.ResumeLayout(false);
            this.gbox_InspectOptions.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbox_InspectOptions;
        private System.Windows.Forms.CheckBox cbox_deleted;
        private System.Windows.Forms.CheckBox cbox_Renamed;
        private System.Windows.Forms.CheckBox cbox_created;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbox_reporte;
        private System.Windows.Forms.RadioButton rbttn_hourly;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbttn_daily;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox report_selected_path_tbox;
        private System.Windows.Forms.Timer timer_check_drives;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.Timer timer_hourly_reports;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox checklist_drives;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
    }
}

