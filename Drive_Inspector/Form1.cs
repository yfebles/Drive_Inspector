using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Drive_Inspector
{  
    public partial class Form1 : Form
    {     
        /// <summary>
        /// The list of file inspector for each drive
        /// </summary>
        List<DriveInspector> file_inspectors;       

        public Form1()
        {
            InitializeComponent();

            notify_icon = new NotifyIcon();
            notify_icon.Icon = Properties.Resources.notify_icon;
            notify_icon.MouseDoubleClick += notify_icon_MouseDoubleClick;

            //initial space for O(1) insertions
            file_inspectors = new List<DriveInspector>(10);

            //start minimized on tray
            WindowState = FormWindowState.Minimized;
            ShowInTray();
        }
        
        /// <summary>
        /// set to false when the licence is invalid 
        /// for close the form without saving 
        /// or ask for password
        /// </summary>
        bool valid_licence = true;

        private void Form1_Load(object sender, EventArgs e)
        {

            if (GetMachineLicenceCode() != software_licence)
            {
                valid_licence = false;
                var result = MessageBox.Show("Ha ocurrido un error con la licencia del software."+
                "Tal vez está tratando de ejecutar Drive Inspector en una "
                +"máquina distinta de la que fue instalado."+
                "Trate de volver a abrirlo y si persiste el problema proceda a reinstalarlo.","Drive Inspector Licence Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            InitOptions(set_options:false);//get the report path

            //start to check for drives
            timer_check_drives.Start();
        }

        #region Software Licence Code

        /// <summary>
        /// The password of the user to allow close the inspector
        /// </summary>
        string user_password = "A6-4D-7F-A3-1E-78-9E-41-9E-45-86-7A-26-4D-D0-22";

        /// <summary>
        /// The licence to use the software
        /// </summary>
        string software_licence = "5E-5D-AA-BD-63-FB-FB-94-37-EE-55-52-0A-FA-50-4B";
     
        /// <summary>
        /// Get the machine code of this computer.
        /// The machine code must be the same that the sowtware licence if the software is running on
        /// the machine where was installed and different otherwise.
        /// </summary>
        /// <returns></returns>
        string GetMachineLicenceCode()
        {
            string system_directory = Environment.SystemDirectory;
            string os_version = Environment.OSVersion.VersionString;
            string plataform = Environment.OSVersion.Platform.ToString();

            //get the drive in which the system is
            DriveInfo systemdrive = null;
            foreach (var item in DriveInfo.GetDrives())
            {
                if (item.IsReady && item.DriveType == DriveType.Fixed && system_directory.StartsWith(item.RootDirectory.Root.FullName))
                {
                    systemdrive = item;
                    break;
                }
            }

            string code = system_directory + "-" + os_version + "-" + plataform + systemdrive.TotalSize.ToString() +
                          systemdrive.DriveFormat.ToString() + systemdrive.RootDirectory.CreationTime.ToString();

            var encriptor = new MD5CryptoServiceProvider();

            byte[] bytes = new byte[code.Length];

            for (int i = 0; i < code.Length; i++)
                bytes[i] = BitConverter.GetBytes(code[i])[0];

            encriptor.ComputeHash(bytes);

            return BitConverter.ToString(encriptor.Hash);
        }

        private bool PasswordOk()
        {
            var form_pass = new Form_Password();

            return form_pass.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                          form_pass.Password == user_password;
        }

        #endregion 
       
        #region Report Save Path

        /// <summary>
        /// Path in which the reports would be saved
        /// </summary>
        string report_save_path = "";

        /// <summary>
        /// get the software options if previously saved
        /// </summary>
        /// <param name="set_options">set the .ini file if true else read it from the file</param>
        private void InitOptions(bool set_options=false)
        {
            FileStream fs = null;
            StreamReader sr = null;
            StreamWriter sw = null;

            //the file with the saved report path
            string options_file = "drive_inspector.ini";

            try
            {
                if (File.Exists(options_file) && !set_options) //if file exist and must be read the path
                {
                    //read path
                    fs = new FileStream(options_file, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs);
                    string[] lines = sr.ReadLine().Split(';');
                   

                    if(lines.Length == 5)
                    { //read the init options

                        report_save_path = lines[0];

                        bool result = false;
                        if (Boolean.TryParse(lines[1], out result))
                            if (result)
                                rbttn_daily.Checked = true;
                            else
                                rbttn_hourly.Checked = true;
                       
                        if (Boolean.TryParse(lines[2], out result))
                            cbox_created.Checked = result;
                        
                        if (Boolean.TryParse(lines[3], out result))
                            cbox_deleted.Checked = result;

                        if (Boolean.TryParse(lines[4], out result))
                            cbox_Renamed.Checked = result;

                            
                    }
                   
                }
                else
                {
                    //create a default report path and save to the .ini if must be saved
                    report_save_path = set_options ? report_save_path : "Drive_Inspector_Reports";

                    if (File.Exists(options_file))
                        fs = new FileStream(options_file, FileMode.Truncate, FileAccess.Write);
                    else
                        fs = new FileStream(options_file, FileMode.CreateNew, FileAccess.Write);

                    sw = new StreamWriter(fs);

                    string line = report_save_path + ";" + rbttn_daily.Checked.ToString() + ";" + cbox_created.Checked + ";" + cbox_deleted.Checked + ";" + cbox_Renamed.Checked;
                    
                    sw.WriteLine(line);

                }

                //create if not exists the folder for the reports
                if (!Directory.Exists(report_save_path))
                    Directory.CreateDirectory(report_save_path);

                //update the text box label
                report_selected_path_tbox.Text = report_save_path;

            }
            catch (Exception) { report_save_path = ""; }
            finally
            {
                if (sr != null)
                    sr.Close();

                if (sw != null)
                    sw.Close();

                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// 
        ///Select the folder to save the reports
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Save the folder of reports
            DialogResult result = folderDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                report_save_path = folderDialog.SelectedPath;
                report_selected_path_tbox.Text = report_save_path;
            }
        }

        #endregion

        #region Tray Icon Handling

        /// <summary>
        /// The icon to show in the windows tray
        /// </summary>
        NotifyIcon notify_icon;
        
        void notify_icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (PasswordOk())
            {
                //open the window from tray
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                notify_icon.Visible = false;
            }
            else
                MessageBox.Show("Invalid Password");
            
        }
       
        private void Form1_Resize(object sender, EventArgs e)
        {
            //change the state of the window to normal instead of minimized to the tra
            ShowInTray();
        }

        void ShowInTray()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Hide();
                notify_icon.Visible = true;
            }
        }
      
        #endregion

        #region Check Drives
       
        private void timer_Tick(object sender, EventArgs e)
        {
            checkAvailableDrives();
        }

        /// <summary>
        /// Checks if the available drives are been analyzed by the program.
        /// Detects the incoming drives and add it  to the list of monitored drives.
        /// </summary>
        private void checkAvailableDrives()
        {
            try
            {
                checklist_drives.Items.Clear();

                var available_drives = new List<DriveInspector>(10);

                foreach (var item in DriveInfo.GetDrives())
                {
                    if (item.IsReady && item.DriveType == DriveType.Removable)
                    {
                        //update the list of drives
                        checklist_drives.Items.Add(new Drive(item));

                        available_drives.Add(new DriveInspector(item,
                                         cbox_created.Checked, cbox_deleted.Checked, cbox_Renamed.Checked));
                    }
                }

                //add the inspection if not exists the drive inspector for that drive
                foreach (var item in available_drives)
                    if (!file_inspectors.Contains(item))
                        file_inspectors.Add(item);

                //stop the inspection if the drive is no longer in the file system
                foreach (var item in file_inspectors)
                    if (!available_drives.Contains(item))
                        item.StopInspection();
            }
            catch (Exception)
            {
                
            }
            
          
        }
        
        #endregion

        #region Save And Exit

        /// <summary>
        /// Save to excel the reports of inspections
        /// </summary>
        /// <param name="name_prefix">Prefix name for the saved files</param>
        /// <param name="single_file">If the inspection actions must be saved all in a single excel file</param>
        void SaveToExcel(string name_prefix, bool single_file)
        {
            //get the name of the file according to the date or hour selection of reports
            string day_month_year_date = DateTime.Now.ToShortDateString().Replace('\\', '-').Replace('/', '-');
            string day_month_year_hour_date = DateTime.Now.ToString().Replace('\\', '-').Replace('/', '-').Replace(" ", "--").Replace(':','-');

            string file_name = rbttn_hourly.Checked ? day_month_year_hour_date : day_month_year_date;
            try
            {
                if (single_file)
                {
                    var actions_list = new List<ExcelSavedFields>(10);
                    foreach (var item in file_inspectors)
                        actions_list.AddRange(item.InspectedDriveActions);

                    string meditions_file_name = report_save_path + "\\" + name_prefix + "-" + file_name + ".xlsx";
                   
                    //check for duplicate file names
                    int repeat_name_code = 0;
                    while (File.Exists(meditions_file_name))
                    {
                        repeat_name_code++;
                        meditions_file_name = report_save_path + "\\" + name_prefix + "-" + file_name +"("+ repeat_name_code.ToString() +").xlsx";
                    }

                    //save all the actions
                    CreateExcelFile.CreateExcelDocument<ExcelSavedFields>(actions_list, meditions_file_name );

                    //clear the saved actions
                    foreach (var item in file_inspectors)
                        item.ClearActions();

                }
                else
                    foreach (var item in file_inspectors)
                    {
                        //check for duplicate file names
                        string meditions_file_name = report_save_path + "\\" + item.InspectedDrive.VolumeLabel + "-" + file_name + ".xlsx";
                       
                        int repeat_name_code = 0;
                        while (File.Exists(meditions_file_name))
                        {
                            repeat_name_code++;
                            meditions_file_name = report_save_path + "\\" + item.InspectedDrive.VolumeLabel + "-" + file_name + "(" + repeat_name_code.ToString() + ").xlsx";
                        }

                        item.SaveToExcel(meditions_file_name);
                        item.ClearActions();
                    }
            }
            catch (Exception)
            { }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //get the user password to close the app and save data
            if(valid_licence && !PasswordOk())
            {
                MessageBox.Show("Invalid Password");
                e.Cancel = true;
                return;
            }

            //save into the .ini file
            InitOptions(set_options: true);

            int actions = 0;

            foreach (var item in file_inspectors)
                actions += item.InspectedDriveActions.Count;

            if (actions > 0)//if any action stored
            {                
                try
                {
                    SaveToExcel("Reportes_DriveInspector", true);
                    MessageBox.Show("Guardando Reportes...");
                }
                catch (Exception) { }
            }
            notify_icon.Visible = false;
        }
        
        #endregion
               
        #region Inspected Actions

        private void cbox_created_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (var item in file_inspectors)
                item.Inspect_Action_Created(cbox_created.Checked);
        }     
        
        private void cbox_deleted_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (var item in file_inspectors)
                item.Inspect_Action_Deleted(cbox_deleted.Checked);
        }

        private void cbox_Renamed_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (var item in file_inspectors)
                item.Inspect_Action_Renamed(cbox_Renamed.Checked);
        }
        #endregion

        #region Hourly Reports

        private void timer_hourly_reports_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            int minute = now.Minute;
            if (minute == 0)
                SaveToExcel("Reportes_DriveInspector-" , true);
        }

        private void rbttn_hourly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbttn_hourly.Checked)
                timer_hourly_reports.Start();
            else
                timer_hourly_reports.Stop();
        }

        #endregion
        
    }
}
