using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace GenerateProperties.Util
{
    /// <summary>
    /// 数据库通用操作类
    /// </summary>
    public abstract class DbHelper
    {
        #region " 连接字符串 "
        //连接字符串
        //public static string ConnString = ConfigurationManager.ConnectionStrings["CommonSqlConnectionString"].ConnectionString;
        public static string DefaultConnString = ConfigurationManager.AppSettings["SqlConnectionString"].ToString();
        public const int defaultCommandTimeout = 180;
        public const int defaultdbconfig = 1;
        public static Logger logger = LogManager.GetLogger("*");

        #endregion

        #region " GetSqlCommand "

        /// <summary>
        /// 获取初始化好的Command对象
        /// </summary>
        /// <param name="conn">Connection对象</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>初始化好的Command对象</returns>
        private static SqlCommand GetSqlCommand(SqlConnection conn, string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = defaultCommandTimeout;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            return cmd;
        }

        #endregion

        #region " ExecSqlDataSet "

        public static DataTable ExecSqlDataSet(string strSQL)
        {
            return ExecSqlDataSet(strSQL, null);
        }

        public static DataTable ExecSqlDataSet(string strSQL, SqlParameter[] parameters)
        {
            return ExecSqlDataSet(strSQL, parameters, DefaultConnString);
        }

        public static DataTable ExecSqlDataSet(string strSQL, SqlParameter[] parameters, string connStr)
        {
            if (string.IsNullOrWhiteSpace(connStr))
            {
                return new DataTable();
            }
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = GetSqlCommand(conn, strSQL, CommandType.Text, parameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Dispose();
                da.Dispose();
                return ds.Tables[0];
            }
        }



        #endregion

        #region " ExecSqlNonQuerry "

        public static bool InsertOne<T>(T model) where T : class
        {
            if (model == null)
                return false;
            StringBuilder sql1 = new StringBuilder();
            StringBuilder sql2 = new StringBuilder();

            if (!typeof(T).IsClass || typeof(T) == typeof(string))
                return false;


            //引用类型 class
            Type type = typeof(T);
            var tableName = type.Name;

            sql1.Append($"insert into {tableName}(");

            PropertyInfo[] properties = type.GetProperties();
            var noMappingField = new List<string> { "ID", "ObjectID", "Tags", "CreationTime" };

            foreach (var pi in properties)//遍历对象的属性
            {
                if (!noMappingField.Contains(pi.Name))
                {
                    sql1.Append($"{pi.Name},");
                    if (!pi.PropertyType.Equals(typeof(string)) && !pi.PropertyType.Equals(typeof(DateTime)))
                        sql2.Append($"{pi.GetValue(model)},");
                    else
                        sql2.Append($"'{pi.GetValue(model)}',");
                }
            }

            var sql1Str = sql1.ToString();
            var sql2Str = sql2.ToString();
            var strSQL = $"{sql1Str.Substring(0, sql1Str.Length - 1)}) values({sql2Str.Substring(0, sql2Str.Length - 1)})";

            return ExecSqlNonQuerry(strSQL) > 0 ? true : false;

        }

        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        /// <param name="strSQL">待执行SQL语句</param>
        /// <returns>受影响的行数</returns>
        public static int ExecSqlNonQuerry(string strSQL)
        {
            return ExecSqlNonQuerry(strSQL, null);
        }

        /// <summary>
        /// 执行非查询的带参数的SQL语句
        /// </summary>
        /// <param name="strSQL">待执行SQL语句</param>
        /// <returns>受影响的行数</returns>
        public static int ExecSqlNonQuerry(string strSQL, SqlParameter[] parameters)
        {
            return ExecSqlNonQuerry(strSQL, parameters, DefaultConnString);
        }



        public static int ExecSqlNonQuerry(string strSQL, SqlParameter[] parameters, string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = GetSqlCommand(conn, strSQL, CommandType.Text, parameters);
                cmd.CommandTimeout = 0;
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
        }

        #endregion

        #region " ExecSqlScalar "

        /// <summary>
        /// 执行统计查询
        /// </summary>
        /// <param name="strSQL">待执行SQL语句</param>
        /// <returns>执行结果的第1行第1列的值</returns>
        public static T ExecSqlScalar<T>(string strSQL)
        {
            return ExecSqlScalar<T>(strSQL, null);
        }

        /// <summary>
        /// 执行带参数的统计查询
        /// </summary>
        /// <param name="strSQL">待执行SQL语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>执行结果的第1行第1列的值</returns>
        public static T ExecSqlScalar<T>(string strSQL, SqlParameter[] parameters)
        {
            return ExecSqlScalar<T>(strSQL, parameters, DefaultConnString);
        }

        /// <summary>
        /// 执行带参数的统计查询
        /// </summary>
        /// <param name="strSQL">待执行SQL语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>执行结果的第1行第1列的值</returns>
        public static T ExecSqlScalar<T>(string strSQL, SqlParameter[] parameters, string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = GetSqlCommand(conn, strSQL, CommandType.Text, parameters);
                object result = cmd.ExecuteScalar();
                cmd.Dispose();
                return (T)result;
            }
        }

        #endregion

        #region " ExecProcDataSet "

        /// <summary>
        /// 执行存储过程，返回执行结果
        /// </summary>
        /// <param name="procName">待执行存储过程</param>
        /// <returns>查询结果</returns>
        public static DataSet ExecProcDataSet(string procName)
        {
            return ExecProcDataSet(procName, null);
        }
        /// <summary>
        /// 执行带参数的存储过程，返回执行结果
        /// </summary>
        /// <param name="procName">待执行存储过程</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>查询结果</returns>
        public static DataSet ExecProcDataSet(string procName, SqlParameter[] parameters)
        {
            return ExecProcDataSet(procName, parameters, DefaultConnString);
        }

        /// <summary>
        /// 执行带参数的存储过程，返回执行结果
        /// </summary>
        /// <param name="procName">待执行存储过程</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>查询结果</returns>
        public static DataSet ExecProcDataSet(string procName, SqlParameter[] parameters, string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = GetSqlCommand(conn, procName, CommandType.StoredProcedure, parameters);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Dispose();
                return ds;
            }
        }


        #endregion

        #region " ExecProcDataTable "

        /// <summary>
        /// 执行存储过程，返回执行结果
        /// </summary>
        /// <param name="procName">待执行存储过程</param>
        /// <returns>查询结果</returns>
        public static DataTable ExecProcDataTable(string procName)
        {
            return ExecProcDataSet(procName).Tables[0];
        }
        /// <summary>
        /// 执行带参数的存储过程，返回执行结果
        /// </summary>
        /// <param name="procName">待执行存储过程</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>查询结果</returns>
        public static DataTable ExecProcDataTable(string procName, SqlParameter[] parameters)
        {
            return ExecProcDataSet(procName, parameters).Tables[0];
        }

        public static DataTable ExecProcDataTable(string procName, SqlParameter[] parameters, string connStr)
        {
            return ExecProcDataSet(procName, parameters, connStr).Tables[0];
        }


        #endregion

        #region " ExecProcNonQuerry "

        /// <summary>
        /// 执行非查询存储过程
        /// </summary>
        /// <param name="procName">待执行存储过程</param>
        /// <returns>受影响的行数</returns>
        public static int ExecProcNonQuerry(string procName)
        {
            return ExecProcNonQuerry(procName);
        }

        /// <summary>
        /// 执行非查询的带参数的存储过程
        /// </summary>
        /// <param name="procName">待执行存储过程</param>
        /// <returns>受影响的行数</returns>
        public static int ExecProcNonQuerry(string procName, SqlParameter[] parameters)
        {
            return ExecProcNonQuerry(procName, parameters, DefaultConnString);
        }


        /// <summary>
        /// 执行非查询的带参数的存储过程
        /// </summary>
        /// <param name="procName">待执行存储过程</param>
        /// <returns>受影响的行数</returns>
        public static int ExecProcNonQuerry(string procName, SqlParameter[] parameters, string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = GetSqlCommand(conn, procName, CommandType.StoredProcedure, parameters);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
        }



        #endregion

        #region " ExecSqlDataReader "

        /// <summary>
        /// 执行SQL语句，返回执行结果
        /// </summary>
        /// <param name="strSQL">待执行SQL语句</param>
        /// <returns>查询结果</returns>
        public static List<T> ExecSqlDataReader<T>(string strSQL, string db = "master")
        {
            return ExecSqlDataReader<T>(strSQL, db, null);
        }

        ///// <summary>
        ///// 执行带参数的SQL语句，返回执行结果
        ///// </summary>
        ///// <param name="strSQL">待执行SQL语句</param>
        ///// <param name="parameters">参数数组</param>
        ///// <returns>查询结果</returns>
        //public static SqlDataReader ExecSqlDataReader(string strSQL, SqlParameter[] parameters, ref SqlConnection conn)
        //{
        //    return ExecSqlDataReader(strSQL, parameters, ref conn);
        //}

        /// <summary>
        /// 执行带参数的SQL语句，返回执行结果
        /// </summary>
        /// <param name="strSQL">待执行SQL语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>查询结果</returns>
        public static List<T> ExecSqlDataReader<T>(string strSQL, string db, SqlParameter[] parameters)
        {

            var SqlConnectionStr = string.Empty;
            if (db == "master")
                SqlConnectionStr = DefaultConnString;
            else
                SqlConnectionStr = DefaultConnString.Replace("Initial Catalog=master", $"Initial Catalog = {db}");

            using (var conn = new SqlConnection(SqlConnectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = GetSqlCommand(conn, strSQL, CommandType.Text, parameters))
                {
                    List<T> list = new List<T>();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (typeof(T).IsClass && typeof(T) != typeof(string))
                        {
                            //引用类型 class

                            if (reader.HasRows)
                            {
                                Type type = typeof(T);
                                //PropertyInfo[] pArray = t.GetProperties();
                                var noMappingField = new List<string> { "ID", "ObjectID", "Tags", "CreationTime" };
                                while (reader.Read())
                                {
                                    //Assembly ass = Assembly.GetAssembly(t);//获取泛型的程序集
                                    //Object assemblyObj = ass.CreateInstance(t.FullName);//泛型实例化'
                                    T assemblyObj = (T)Activator.CreateInstance(type);//创建该类型的对象

                                    foreach (var item in type.GetProperties())//遍历对象的属性
                                    {
                                        //if (!typeof(BaseModel).IsAssignableFrom(item.PropertyType))//如果是其他实体类就不加载

                                        if (!noMappingField.Contains(item.Name))
                                        {
                                            // item.SetValue(assemblyObj, reader[item.Name].ToString());//对对象的属性赋值
                                            //if (item.Name == "ID")
                                            //{
                                            //    //
                                            //}
                                            if (item.PropertyType.Equals(typeof(string)))//判断属性的类型是不是String
                                            {
                                                item.SetValue(assemblyObj, reader[item.Name].ToString(), null);//给泛型的属性赋值
                                            }
                                            else if (item.PropertyType.Equals(typeof(int)))
                                            {
                                                item.SetValue(assemblyObj, reader[item.Name].ToString().ToInt(), null);
                                            }
                                            else if (item.PropertyType.Equals(typeof(short)))
                                            {
                                                item.SetValue(assemblyObj, reader[item.Name].ToString().ToSmallInt(), null);
                                            }
                                            else if (item.PropertyType.Equals(typeof(DateTime)))
                                            {
                                                item.SetValue(assemblyObj, reader[item.Name].ToString().ToDateTime(), null);
                                            }

                                        }
                                    }


                                    //Array.ForEach<PropertyInfo>(pArray, (p) =>
                                    //{
                                    //    if (!noMappingField.Contains(p.Name))
                                    //    {
                                    //        if (p.PropertyType.Equals(typeof(string)))//判断属性的类型是不是String
                                    //        {
                                    //            p.SetValue(assemblyObj, reader[p.Name].ToString(), null);//给泛型的属性赋值
                                    //        }
                                    //        else if (p.PropertyType.Equals(typeof(int)))
                                    //        {
                                    //            p.SetValue(assemblyObj, reader[p.Name].ToString().ToInt(), null);
                                    //        }
                                    //        else if (p.PropertyType.Equals(typeof(short)))
                                    //        {
                                    //            p.SetValue(assemblyObj, reader[p.Name].ToString().ToSmallInt(), null);
                                    //        }
                                    //        else if (p.PropertyType.Equals(typeof(DateTime)))
                                    //        {
                                    //            p.SetValue(assemblyObj, reader[p.Name].ToString().ToDateTime(), null);
                                    //        }
                                    //    }
                                    //});
                                    list.Add(assemblyObj);
                                }
                                // Console.WriteLine();
                                reader.Close();

                            }
                        }
                        else
                        {
                            //值类型 struct
                            using (reader)
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        list.Add((T)reader[0]);
                                    }
                                    reader.Close();
                                }
                            }
                        }
                        return list;
                    }
                }
            }
        }


        #endregion

        #region " ExecProcDataReader "

        /// <summary>
        /// 执行存储过程，返回执行结果
        /// </summary>
        /// <param name="procName">待执行存储过程</param>
        /// <returns>查询结果</returns>
        public static SqlDataReader ExecProcDataReader(string procName)
        {
            return ExecProcDataReader(procName, null);
        }

        /// <summary>
        /// 执行带参数的存储过程，返回执行结果
        /// </summary>
        /// <param name="procName">待执行存储过程</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>查询结果</returns>
        public static SqlDataReader ExecProcDataReader(string procName, SqlParameter[] parameters)
        {
            return ExecProcDataReader(procName, parameters, DefaultConnString);
        }

        /// <summary>
        /// 执行带参数的存储过程，返回执行结果
        /// </summary>
        /// <param name="procName">待执行存储过程</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>查询结果</returns>
        public static SqlDataReader ExecProcDataReader(string procName, SqlParameter[] parameters, string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = GetSqlCommand(conn, procName, CommandType.StoredProcedure, parameters);
                SqlDataReader result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Dispose();
                return result;
            }
        }



        #endregion

        #region " DtToSqlServer "

        /// <summary>
        /// 将DataTable批量导入SqlServer
        /// </summary>
        /// <param name="dtExcel">数据表</param>
        /// <param name="tableName">目标数据表名</param>
        /// <param name="dtColName">对应列的数据集</param>
        public static void DtToSqlServer(DataTable dtExcel, string tableName, DataTable dtColName)
        {
            DtToSqlServer(dtExcel, tableName, dtColName, DefaultConnString);
        }

        /// <summary>
        /// 将DataTable批量导入SqlServer
        /// </summary>
        /// <param name="dtExcel">数据表</param>
        /// <param name="tableName">目标数据表名</param>
        /// <param name="dtColName">对应列的数据集</param>
        public static void DtToSqlServer(DataTable dtExcel, string tableName, DataTable dtColName, string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                {
                    try
                    {
                        bulkCopy.DestinationTableName = tableName;//要插入的表的表名
                        for (int i = 0; i < dtColName.Rows.Count; i++)
                        {
                            bulkCopy.ColumnMappings.Add(dtColName.Rows[i][0].ToString().Trim(), dtColName.Rows[i][1].ToString().Trim());

                        }

                        bulkCopy.WriteToServer(dtExcel);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        #endregion

        #region
        /// <summary>
        ///
        /// </summary>
        /// <param name="dbconfig">目标连接字符</param>
        /// <param name="tablename">目标表</param>
        /// <param name="dt">源数据</param>
        public static string SqlBulkCopyByDatatable(string tablename, DataTable table, string connStr, SqlConnection m_clsSqlConn)
        {
            string dataBaseStr = "";
            if (tablename.Contains("."))
            {
                dataBaseStr = tablename.Substring(0, tablename.LastIndexOf(".") + 1);
                tablename = tablename.Substring(tablename.LastIndexOf(".") + 1);
            }

            try
            {
                string result = "";
                SqlBulkCopy sqlBulkCopy = null;
                if (m_clsSqlConn != null)
                {
                    sqlBulkCopy = new SqlBulkCopy(m_clsSqlConn);
                    if (m_clsSqlConn.State == ConnectionState.Closed)
                    {
                        m_clsSqlConn.Open();
                    }
                }
                else
                {
                    sqlBulkCopy = new SqlBulkCopy(connStr);
                }



                sqlBulkCopy.DestinationTableName = dataBaseStr + ((tablename.IndexOf("[") > -1 && tablename.IndexOf("]") > -1) ? tablename : "[" + tablename + "]");
                sqlBulkCopy.BulkCopyTimeout = 500;
                //sqlBulkCopy.BatchSize = 800;

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    sqlBulkCopy.ColumnMappings.Add(table.Columns[i].ColumnName, table.Columns[i].ColumnName);
                }

                if (table.Rows.Count > 0)
                {
                    sqlBulkCopy.WriteToServer(table);
                }
                else
                {
                    result = "表为空";
                }

                sqlBulkCopy.Close();
                return result;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                try
                {
                    if (m_clsSqlConn != null)
                    {

                        try
                        {
                            if (m_clsSqlConn.State == ConnectionState.Open)
                            {
                                m_clsSqlConn.Close();
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        public static string SqlBulkCopyByDatatable(string tablename, DataTable table, SqlConnection m_clsSqlConn)
        {
            return SqlBulkCopyByDatatable(tablename, table, string.Empty, m_clsSqlConn);

        }
        public static string SqlBulkCopyByDatatable(string tablename, DataTable table, string connStr)
        {
            return SqlBulkCopyByDatatable(tablename, table, connStr, null);
        }

        public static string SqlBulkCopyByDatatable(string tablename, DataTable table)
        {
            return SqlBulkCopyByDatatable(tablename, table, DefaultConnString, null);
        }

        public static string CreateTempTable(string tablename, DataTable table, string connStr)
        {
            return CreateTempTable(tablename, table, new SqlConnection(connStr));
        }
        public static string CreateTempTable(string tablename, DataTable table, SqlConnection connStr)
        {
            try
            {

                string sqlstr = "CREATE TABLE [" + tablename + "](";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    switch (table.Columns[i].DataType.FullName)
                    {
                        case "System.String":
                            {
                                sqlstr += "[" + table.Columns[i].ColumnName + "] [nvarchar](4000) NULL,";
                            }
                            break;
                        case "System.Int32":
                            {
                                sqlstr += "[" + table.Columns[i].ColumnName + "] [int] NULL,";
                            }
                            break;
                        case "System.Double":
                            {
                                sqlstr += "[" + table.Columns[i].ColumnName + "] [numeric](24,2) NULL,";
                            }
                            break;
                        case "System.DateTime":
                            {
                                sqlstr += "[" + table.Columns[i].ColumnName + "] [datetime] NULL,";
                            }
                            break;
                        default:
                            {
                                sqlstr += "[" + table.Columns[i].ColumnName + "] [nvarchar](4000) NULL,";
                            }
                            break;
                    }
                }
                sqlstr = sqlstr.Substring(0, sqlstr.Length - 1) + ")";

                if (connStr.State != ConnectionState.Open)
                {
                    connStr.Open();
                }

                SqlCommand cmd = GetSqlCommand(connStr, sqlstr, CommandType.Text, null);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return "";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        #endregion


        #region SqlBuckCopy
        public static bool SqlBuckCopy(string TableName, DataTable table)
        {
            return SqlBuckCopy(TableName, table, new SqlConnection(DefaultConnString));
        }
        public static bool SqlBuckCopy(string TableName, DataTable dt, SqlConnection sqlConn)
        {
            try
            {


                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();
                }


                SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn);
                bulkCopy.DestinationTableName = TableName;



                if (dt != null && dt.Rows.Count != 0)
                {
                    bulkCopy.BatchSize = dt.Rows.Count;
                    bulkCopy.BulkCopyTimeout = 0; //设置超时时间0 表示会一直等待， 不然sql连接会超时

                    bulkCopy.WriteToServer(dt);
                }


                sqlConn.Close();
                sqlConn.Dispose();

            }
            catch (Exception ex)
            {
                logger.Error(ex, $"SqlBuckCopy异常信息,Code:{dt.Rows[0]["Code"]} 数量 {dt.Rows.Count} ");
                return false;
            }
            finally
            {

            }
            return true;
        }

        #endregion


    }
}
