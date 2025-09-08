using GenerateProperties.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateProperties
{
    internal class Setup
    {
        internal static string configureFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\dbConfigure.txt";

        internal static BindingList<DB_Connectionstrings> dbs = new BindingList<DB_Connectionstrings>();

    }
}
