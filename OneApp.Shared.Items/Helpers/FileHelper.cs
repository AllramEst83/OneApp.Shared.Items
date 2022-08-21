using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Shared.Items.Helpers
{
    public static class FileHelper
    {
        public static string GetFilePath(string fileName)
        {
            string systemStorage = FileSystem.AppDataDirectory;
            string filePath = Path.Combine(systemStorage, fileName);

            return filePath;
        }

      
    }

    
}
