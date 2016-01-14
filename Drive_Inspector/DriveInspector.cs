using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Drive_Inspector
{
    /// <summary>
    /// Class that encapsulate the fields that would be saved in the excel file.
    /// </summary>
    public class ExcelSavedFields
    {
        /// <summary>
        /// The volume name of the drive in which the action was made.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// The total space of the drive. The Capacity in bytes.
        /// </summary>
        public string EspacioTotal { get; set; }

        /// <summary>
        /// The action made on this drive.
        /// One of [Rename,created, deleted]
        /// </summary>
        public string Accion { get; set; }

        /// <summary>
        /// The path of the created, deleted or edited file
        /// </summary>
        public string Ruta { get; set; }

        /// <summary>
        /// The name of the created, deleted or edited file
        /// </summary>
        public string NombreFichero { get; set; }

        /// <summary>
        /// The date at which was created, deleted or edited file
        /// </summary>
        public string Fecha { get; set; }

        /// <summary>
        /// The hour minutes and seconds at which was created, deleted or edited file
        /// </summary>
        public string Hora { get; set; }

        /// <summary>
        /// The extension of the created, deleted or edited file
        /// </summary>
        public string Extension { get; set; }

        public override string ToString()
        {
            return Nombre + " - " +
                   Accion + " - " +
                   NombreFichero + " - " +
                   Ruta + " - " +
                   Fecha + " - " +
                   Hora + " - " +
                   EspacioTotal;
        }
    }

    /// <summary>
    /// Class that inspect a Drive and save the 
    /// actions made to/from the drive.
    /// </summary>
    public class DriveInspector
    {

        /// <summary>
        /// True if the object is no longer inspection the drive because dispose was invoked
        /// </summary>
        bool Disposed { get; set; }

        /// <summary>
        /// The object to inspect the drive.
        /// </summary>
        FileSystemWatcher DriveWatcher { get; set; }

        /// <summary>
        /// The drive in which is been doing the inspection
        /// </summary>
        public DriveInfo InspectedDrive { get; private set; }

        /// <summary>
        /// List with the actions do it by the user with the drive.
        /// 
        /// </summary>
        public List<ExcelSavedFields> InspectedDriveActions { get; set; }

        /// <summary>
        /// Create a Drive Inspector Object
        /// </summary>
        /// <param name="inspected_path">The path to the drive to inspect</param>
        /// <param name="inspect_create">If inspect the path for created files on it</param>
        /// <param name="inspect_delete">If inspect the path for deleted files on it</param>
        /// <param name="inspect_rename">If inspect the path for renamed files on it</param>
        public DriveInspector(DriveInfo inspected_drive, bool inspect_create = true, bool inspect_delete = true, bool inspect_rename = true)
        {
            if (inspected_drive == null || !inspected_drive.IsReady || 
                !(inspected_drive.DriveType == DriveType.Removable || inspected_drive.DriveType == DriveType.Fixed))
                throw new ArgumentException("Invalid Arguments. The inspected path should be a real and available drive in the file system.");

            DriveWatcher = new FileSystemWatcher(inspected_drive.RootDirectory.FullName);
            DriveWatcher.EnableRaisingEvents = true;
            DriveWatcher.IncludeSubdirectories = true;

            InspectedDriveActions = new List<ExcelSavedFields>(10);

            InspectedDrive = inspected_drive;

            InspectCreate = inspect_create;
            InspectRename = inspect_rename;
            InspectDelete = inspect_delete;


            //connect the events for inspection
            if (InspectCreate)
                DriveWatcher.Created += DriveWatcher_Created;

            if (InspectDelete)
                DriveWatcher.Deleted += DriveWatcher_Deleted;
            
            if (InspectRename)
                DriveWatcher.Renamed += DriveWatcher_Renamed;

        }        

        #region Changes Monitoring

        /// <summary>
        /// The bolean of which action is inspected
        /// </summary>
        bool InspectCreate { get; set; }

        bool InspectDelete { get; set; }

        bool InspectRename { get; set; }
        
        /// <summary>
        /// Add a new action to the list of current managed actions on the drive
        /// </summary>
        /// <param name="type"></param>
        /// <param name="file_path"></param>
        /// <param name="file_name"></param>
        void AddAction(string type, string file_path, string file_name)
        {
            DateTime now = DateTime.Now;
            
            //disk size in Gb
            double drive_size = Math.Round(InspectedDrive.TotalSize / (1024 * 1024 * 1024.0), 2);

            //get extension
            string ext = "Carpeta";

            if(!Directory.Exists(file_path) && file_path.Contains(".")) //if it's a file
                ext = file_path.Substring(file_path.LastIndexOf('.'));

            InspectedDriveActions.Add(new ExcelSavedFields()
            {
                Nombre = InspectedDrive.VolumeLabel,
                Accion = type,
                EspacioTotal = drive_size.ToString() + "Gb",
                Ruta = file_path,
                Fecha = now.ToShortDateString(),
                Hora = now.Hour.ToString() + ":" + now.Minute + ":" + now.Second,
                NombreFichero = file_name,
                Extension = ext                
            });
        }

        /// <summary>
        /// Get the size in Mb of a file. Is giving some errors when the asked file
        /// is still been copied or when was deleted and simultanously is been requested its size.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        double GetFileSize(string path)
        {
            double file_size = 0;
            FileStream fs = null;
            try
            {
                fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                file_size = Math.Round(fs.Length * 1.0 / (1024.0 * 1024.0), 2);  //in Mb bytes
            }
            catch (Exception) { }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return file_size;
        }


        void DriveWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            AddAction("Renombrado", e.OldFullPath, e.Name);
            
        }

        void DriveWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            AddAction("Borrado", e.FullPath, e.Name);
            
        }

        void DriveWatcher_Created(object sender, FileSystemEventArgs e)
        {
            AddAction("Creado", e.FullPath, e.Name);
           
        }

        /// <summary>
        /// Changes the inspection of the particular action Renamed file
        /// </summary>
        /// <param name="inspect_action">If must be inspecteed the action or not</param>
        public void Inspect_Action_Renamed(bool inspect_action)
        {
            if (Disposed)
                return;

            if (inspect_action && !InspectRename)
                DriveWatcher.Renamed += DriveWatcher_Renamed;

            else if (!inspect_action && InspectRename)
                DriveWatcher.Renamed -= DriveWatcher_Renamed;
        }
       
        /// <summary>
        /// Changes the inspection of the particular action Deleted file
        /// </summary>
        /// <param name="inspect_action">If must be inspecteed the action or not</param>
        public void Inspect_Action_Deleted(bool inspect_action)
        {
            if (Disposed)
                return;

            if (inspect_action && !InspectDelete)
                DriveWatcher.Deleted += DriveWatcher_Deleted;

            else if (!inspect_action && InspectDelete)
                DriveWatcher.Deleted -= DriveWatcher_Deleted;
        }

        /// <summary>
        /// Changes the inspection of the particular action Created file
        /// </summary>
        /// <param name="inspect_action">If must be inspecteed the action or not</param>
        public void Inspect_Action_Created(bool inspect_action)
        {
            if (Disposed)
                return; 
            
            if (inspect_action && !InspectCreate)
                DriveWatcher.Created += DriveWatcher_Created;

            else if (!inspect_action && InspectCreate)
                DriveWatcher.Created -= DriveWatcher_Created;
        }

        #endregion

        /// <summary>
        /// Stop the inspection of the drive and dispose the resources of the inspection.
        /// The action changes are keep equals for future save into disk
        /// </summary>
        public void StopInspection()
        {
            Inspect_Action_Created(false);
            Inspect_Action_Renamed(false);
            Inspect_Action_Deleted(false);
            DriveWatcher.Dispose();
            Disposed = true;

        }

        /// <summary>
        /// Removes all actions inspected by the drive inspector
        /// </summary>
        public void ClearActions()
        {
            InspectedDriveActions.Clear();
        }

        /// <summary>
        /// Save the current stored activities of the inspected drive
        /// </summary>
        /// <param name="path">Path in which would be saved the excell file with the actions of the inspected drive</param>
        public void SaveToExcel(string path)
        {
            //***to debugging
            //SaveTxt.saveTxt(path, InspectedDriveActions);
            //return;
            //***to debugging
            
            //try to save by 3 times if fails
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    //save to excell and exit the loop
                    CreateExcelFile.CreateExcelDocument<ExcelSavedFields>(InspectedDriveActions, path);
                    return;
                }
                catch (Exception)
                {/*error saving*/}

                //wait 100 ms
                System.Threading.Thread.Sleep(100);
            }

        }

        public override bool Equals(object obj)
        {
            return !(obj == null) && obj is DriveInspector && 
                   ((DriveInspector)obj).InspectedDrive.RootDirectory.FullName == InspectedDrive.RootDirectory.FullName &&
                   ((DriveInspector)obj).InspectedDrive.VolumeLabel == InspectedDrive.VolumeLabel &&
                   ((DriveInspector)obj).Disposed == Disposed && 
                   ((DriveInspector)obj).InspectedDrive.TotalSize == InspectedDrive.TotalSize;
        }

    }

    /// <summary>
    /// For debugging save to txt
    /// </summary>
    public static class SaveTxt
    {
        public static void saveTxt(string path,List<ExcelSavedFields> list)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                if (File.Exists(path))
                    File.Delete(path);

                fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                sw = new StreamWriter(fs);

                foreach (var action in list)
                {
                    sw.WriteLine(action);
                }
            }
            catch (Exception)
            { }
            finally
            {
                if (sw != null)
                    sw.Close();

                if (fs != null)
                    fs.Close();
            }

        }

        
    }
}
