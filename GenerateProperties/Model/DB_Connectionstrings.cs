using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateProperties.Model
{
    public class DB_Connectionstrings
    {
        public Guid DBID { get; set; }
        public string DBName { get; set; }
        public DBType DBType { get; set; }
        public string DBConnectionString { get; set; }

    }

    public enum DBType
    {
        SqlServer,
        MySql,
        Oracle,
        PostgreSQL,
        SQLite,
        MongoDB,
        Redis
    }
}
