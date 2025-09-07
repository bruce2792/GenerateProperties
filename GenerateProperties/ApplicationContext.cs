using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateProperties
{
    internal class ApplicationContext
    {
        internal static string SqlConnectionString { get; set; }

        public static string GetCurrentDirectory()
        {
            // 获取当前应用程序的目录
            return AppContext.BaseDirectory;
        }

        public static string db_connectionstrings()
        {
            // 获取当前应用程序的目录
            return AppContext.BaseDirectory;
        }
    }
}
