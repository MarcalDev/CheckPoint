using CheckPointBase.Data.Interface;
using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;

namespace CheckPoint.iOS.Helpers
{
    public class DatabasePath : IDBPath
    {
        public string GetDbPath()
        {
            string filename = "CheckPoint.db3";
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
                Directory.CreateDirectory(libFolder);

            return Path.Combine(libFolder, filename);
        }
    }
}