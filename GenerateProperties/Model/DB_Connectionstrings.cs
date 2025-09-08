using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateProperties.Model
{
    public class DB_Connectionstrings
    {
        public Guid DBID { get; set; } = Guid.NewGuid();
        public IsDev IsDev { get; set; } = IsDev.DEV;
        public string DBName { get; set; } = string.Empty;

        public DBType DBType { get; set; } = DBType.SqlServer;
        public string DBConnectionString { get; set; } = string.Empty;

    }

    public enum IsDev
    {
        [Description("DEV")]
        DEV,
        [Description("UAT")]
        UAT,
        [Description("PROD")]
        PROD,
    }


    public enum DBType
    {
        [Description("SqlServer")]
        SqlServer,
        [Description("MySql")]
        MySql,
        [Description("Oracle")]
        Oracle,
        [Description("PostgreSQL")]
        PostgreSQL,
        [Description("SQLite")]
        SQLite,
        [Description("MongoDB")]
        MongoDB,
        [Description("Redis")]
        Redis
    }
}
