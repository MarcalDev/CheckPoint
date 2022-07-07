using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CheckPoint.Droid.Helpers;
using CheckPointBase.Data.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(DatabasePath))]
namespace CheckPoint.Droid.Helpers
{
    
    public class DatabasePath : IDBPath
    {
        public DatabasePath()
        {

        }

        public string GetDbPath()
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "CheckPoint.db3");
        }
    }
}