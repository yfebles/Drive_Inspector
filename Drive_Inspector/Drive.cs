using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Drive_Inspector
{

    /// <summary>
    /// Wraper class to use in the list of 
    /// drives that redefine the tostring method of a drive
    /// </summary>
    class Drive
    {
        /// <summary>
        /// Internal Drive info
        /// </summary>
        DriveInfo info;

        public Drive(DriveInfo drive)
        {
            if (drive == null) throw new ArgumentNullException();

            info = drive;
        }

        /// <summary>
        /// Override tostring for best visualization
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return info.Name + " \'" + info.VolumeLabel + "\'";
        }
    }
}
