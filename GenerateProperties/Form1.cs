using AntdUI;
using GenerateProperties.Model;
using GenerateProperties.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using NuGet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Walle.Components.Extensions;

namespace GenerateProperties
{
    public partial class Form1 : Form
    {
        public static Logger logger = LogManager.GetLogger("*");
        private static bool isGeneralSingleDB = false;


        public Form1()
        {
            InitializeComponent();
        }

        // 定义委托和事件
        public delegate void UpdateComboBoxDelegate(IEnumerable<string> newItems);
        public event UpdateComboBoxDelegate UpdateComboBoxRequested;

        // 更新ComboBox的方法
        private void UpdateComboBoxItems(IEnumerable<string> newItems)
        {
            // 确保在主线程上更新UI
            if (cboChooseDatabase.InvokeRequired)
            {
                //comboBox.Invoke(new Action<List<string>>(comboBox,UpdateComboBoxItems), newItems);
                cboChooseDatabase.Invoke(new Action<IEnumerable<string>>(UpdateComboBoxItems), newItems);
                return;
            }

            cboChooseDatabase.Items.Clear();
            cboChooseDatabase.Items.AddRange(newItems.ToArray());

            if (cboChooseDatabase.Items.Count > 0)
                cboChooseDatabase.SelectedIndex = 0;
        }


        private List<string> GetDatabases()
        {
            #region 读取有哪些数据库
            var getAllDatabaseSql = "SELECT NAME FROM MASTER..SYSDATABASES";
            var dbList = DbHelper.ExecSqlDataReader<string>(getAllDatabaseSql);

            return dbList;
            #endregion
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            string db = this.comboBox1.Text;
            var tableList = GetTableList(db);
            if (tableList.HasAny())
                SetCombox(tableList.OrderBy(a => a).ToList(), this.comboBox2);
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        var cnt = ParallelLoopStates.Where(a => !a.IsStopped).Count();
            //        logger.Info("fit" + cnt);
            //        while (cnt > 0)
            //        {
            //            logger.Info("second" + cnt);
            //            this.listBox1.Items.Clear();
            //            var notStopedLoopItems = ParallelLoopStates.Where(a => a.IsStopped == false);
            //            foreach (var item in notStopedLoopItems)
            //            {
            //                this.listBox1.Items.Add(item.IsStopped.ToString());
            //            }


            //        }
            //    }

            //});








            #region 设置combobox demo
            //ArrayList mylist = new ArrayList();
            //mylist.Add(new DictionaryEntry("1", "全部"));
            //mylist.Add(new DictionaryEntry("2", "正常"));
            //mylist.Add(new DictionaryEntry("3", "终止"));
            //comboBox1.DataSource = mylist;
            //comboBox1.DisplayMember = "Value";
            //comboBox1.ValueMember = "Key";

            #endregion
            //加载数据库连接字符串
            SetCombox(Setup.dbs.Select(a => a.DBName).ToList(), this.cboChooseDatabase);

            //加载数据库
            //RenderDatabases();

            #region 预加载json的实体
            //预置测试JSON
            txtJson.Text = "{\"data\":{\"permissionsData\":[{\"id\":\"queryForm\",\"operation\":[\"add\",\"edit\"]}],\"roles\":[{\"id\":\"admin\",\"operation\":[\"add\",\"edit\",\"delete\"]}],\"user\":{\"name\":\"LUIS\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/ubnKSIfAJTxIgXOKlciN.png\",\"address\":\"上海市\",\"position\":{\"CN\":\"前端工程师 | 蚂蚁金服-计算服务事业群-REACT平台\",\"HK\":\"前端工程師 | 螞蟻金服-計算服務事業群-REACT平台\",\"US\":\"Front-end engineer | Ant Financial - Computing services business group - REACT platform\"}},\"token\":\"Authorization:0.03126884429870813\",\"expireAt\":\"2022-05-24T08:09:10.223Z\"},\"code\":0,\"message\":\"下午好，欢迎回来\"}";

            JsonToEntity();


            #endregion

            this.table1.EditMode = TEditMode.Click;//单击进入修改模式
            this.table1.Binding(tableFields);//table绑定数据源
        }

        /// <summary>
        /// 加载数据库
        /// </summary>
        void RenderDatabases()
        {
            #region 预加载数据库的实体
            var dbList = GetDatabases();
            if (dbList.HasAny())
                SetCombox(dbList.OrderBy(a => a).ToList(), this.comboBox1);

            #endregion
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string db = this.comboBox1.Text;

            var builder = new SqlConnectionStringBuilder(DbHelper.DefaultConnString);
            builder.InitialCatalog = db;
            DbHelper.DefaultConnString = builder.ConnectionString;

            //获取表列表
            var tableList = GetTableList(db);

            //切换数据库 设置连接字符串
           

            SetCombox(tableList.OrderBy(a => a).ToList(), this.comboBox2);
        }

        private void SetCombox(List<string> source, ComboBox comboBox)
        {
            if (source != null && source.HasAny())
            {
                ArrayList mylist = new ArrayList();
                for (int i = 0; i < source.Count; i++)
                {
                    mylist.Add(new DictionaryEntry(i, source[i]));
                }

                comboBox.DataSource = source;
                //comboBox.DisplayMember = "Value";
                //comboBox.ValueMember = "Key";
            }
        }

        private List<string> GetTableList(string db)
        {
            //第一种方法获取值
            //string combobox1_index = this.comboBox1.SelectedIndex.ToString();
            var tableList = new List<string>();
            if (db != "System.Collections.DictionaryEntry")
            {
                //logger.Info("combobox1_value===" + combobox1_value);
                //logger.Info("combobox1_index===" + combobox1_index);

                #region 读取有哪些表
                var getAllTableSql = "SELECT NAME FROM SYSOBJECTS WHERE TYPE='U'";
                tableList = DbHelper.ExecSqlDataReader<string>(getAllTableSql);

                #endregion
            }
            return tableList;


            #region 获取combobox的选中项
            //第一种方法获取值
            //string combobox1_value = this.comboBox1.Text;
            //string combobox1_index = this.comboBox1.SelectedIndex.ToString();
            //if (combobox1_value != "System.Collections.DictionaryEntry")
            //{
            //    logger.Info("combobox1_value===" + combobox1_value);
            //    logger.Info("combobox1_index===" + combobox1_index);
            //}
            ////第二种方法获取值
            //var Vcombobox1_value = this.comboBox1.SelectedItem as Region;
            //Console.WriteLine("name===" + Vcombobox1_value.name);
            //Console.WriteLine("id===" + Vcombobox1_value.id);
            #endregion
        }


        List<TableField> _GetTableField(string table, string db)
        {
            #region 读取有哪些字段
            var getAllFieldSql = $@"Select 
SCOL.NAME AS FieldName,                    --列名
SCOL.ISNULLABLE AS FieldIsNullable,            --是否为NULL
SCOL.PREC AS Length,                    --长度
STYPE.NAME AS FieldType ,        --字段数据类型
(SELECT SYS.EXTENDED_PROPERTIES.VALUE FROM SYSCOLUMNS   
INNER JOIN SYS.EXTENDED_PROPERTIES ON SYSCOLUMNS.ID = SYS.EXTENDED_PROPERTIES.MAJOR_ID   
AND SYSCOLUMNS.COLID = SYS.EXTENDED_PROPERTIES.MINOR_ID   
INNER JOIN SYSOBJECTS ON SYSCOLUMNS.ID = SYSOBJECTS.ID   
WHERE SYSOBJECTS.NAME = SO.NAME AND SYSCOLUMNS.NAME = SCOL.NAME) AS FieldNotes   --字段说明文字

from SYSCOLUMNS AS SCOL
LEFT JOIN SYSOBJECTS SO ON SO.ID=SCOL.ID 
LEFT JOIN SYSTYPES AS STYPE ON STYPE.xtype=SCOL.xtype
Where
SCOL.ID=OBJECT_ID('{table}')
AND STYPE.NAME<>'SYSNAME' 
ORDER BY SCOL.colid ASC";
            return DbHelper.ExecSqlDataReader<TableField>(getAllFieldSql);
        }

        /// <summary>
        /// 获取数据库表中的字段
        /// </summary>
        private string GetTableField(string db, string table)
        {

            var properties = string.Empty;

            if (table != "System.Collections.DictionaryEntry")
            {

                var fieldList = _GetTableField(table, db);

                properties += $"using System;\r\n";
                properties += $"using System.Collections.Generic;\r\n";
                properties += $"using System.Linq;\r\n";
                properties += $"using System.Text;\r\n";
                properties += $"using System.Threading.Tasks;\r\n\r\n";

                properties += "namespace Model\r\n";
                properties += "{\r\n";

                properties += $"    public class {table}\r\n";
                properties += "    {\r\n\r\n";


                foreach (var item in fieldList)
                {
                    switch (item.FieldType)
                    {
                        case "bit":
                            properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public bool {item.FieldName} {{get;set;}}\r\n\r\n";
                            break;
                        case "char":
                            properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public string {item.FieldName} {{get;set;}}\r\n\r\n";
                            break;
                        case "varchar":
                            properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public string {item.FieldName} {{get;set;}}\r\n\r\n";
                            break;
                        case "nvarchar":
                            properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public string {item.FieldName} {{get;set;}}\r\n\r\n";
                            break;
                        case "text":
                            properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public string {item.FieldName} {{get;set;}}\r\n\r\n";
                            break;
                        case "uniqueidentifier":
                            properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public Guid {item.FieldName} {{get;set;}}\r\n\r\n";
                            break;
                        case "smallint":
                            properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public short {item.FieldName} {{get;set;}}\r\n\r\n";
                            break;
                        case "int":
                            properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public int {item.FieldName} {{get;set;}}\r\n\r\n";
                            break;
                        case "decimal":
                            properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public decimal {item.FieldName} {{get;set;}}\r\n\r\n";
                            break;
                        case "datetime":
                            properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public DateTime {item.FieldName} {{get;set;}}\r\n\r\n";
                            break;
                    }
                    //if (item.FieldType == "varchar" || item.FieldType == "nvarchar" || item.FieldType == "text")
                    //    properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public string {item.FieldName} {{get;set;}}\r\n\r\n";
                    //else if (item.FieldType == "uniqueidentifier")
                    //    properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public Guid {item.FieldName} {{get;set;}}\r\n\r\n";
                    //else if (item.FieldType == "smallint")
                    //    properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public short {item.FieldName} {{get;set;}}\r\n\r\n";
                    //else if (item.FieldType == "int")
                    //    properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public int {item.FieldName} {{get;set;}}\r\n\r\n";
                    //else if (item.FieldType == "decimal")
                    //    properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public decimal {item.FieldName} {{get;set;}}\r\n\r\n";
                    //else if (item.FieldType == "datetime")
                    //    properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public DateTime {item.FieldName} {{get;set;}}\r\n\r\n";
                }
                properties += "    }\r\n";
                properties += "}";





                #region 验证是否获取成功
                //if (fieldList.HasAny())
                //{
                //    Type t = fieldList.FirstOrDefault().GetType();
                //    PropertyInfo[] pArray = t.GetProperties();

                //    List<string> types = new List<string> { };
                //    foreach (var item in fieldList)
                //    {
                //        foreach (var pi in pArray)
                //        {
                //            var val = pi.GetValue(item).ToString();
                //            //if (pi.Name == "FieldType" && !types.Contains(val))
                //            //{
                //            //    types.Add(val);
                //            //    logger.Info($"字段名：{pi.Name}  值：{val}");
                //            //}
                //            logger.Info($"字段名：{pi.Name}  值：{val}");
                //        }
                //    }
                //}
                #endregion



                #endregion
            }
            return properties;
        }

        //创建属性
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            //db
            string db = this.comboBox1.Text;
            //table
            string table = this.comboBox2.Text;
            //string combobox2_index = this.comboBox2.SelectedIndex.ToString();

            var properties = GetTableField(db, table);
            if (properties.IsNotNullOrEmpty())
            {
                this.textBox1.Text = properties;
                //加入到粘贴板
                SetClipboard(properties);

                var dbPath = GetDbPath(db);
                if (Directory.Exists(dbPath) == false)
                    Directory.CreateDirectory(dbPath);

                //创建文件
                GenerateDBFile(db, table, dbPath, properties);
            }

            EditSqlFieldAnnotation();

        }

        /// <summary>
        /// 复制textbox文本框内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            #region 复制/粘贴
            ////复制： 
            //private void button1_Click(object sender, System.EventArgs e)
            //{

            //    if (textBox1.SelectedText != "")
            //        Clipboard.SetDataObject(textBox1.SelectedText);
            //}

            ////粘贴： 
            //private void button2_Click(object sender, System.EventArgs e)
            //{
            //    IDataObject iData = Clipboard.GetDataObject();
            //    if (iData.GetDataPresent(DataFormats.Text))
            //    {
            //        textBox2.Text = (String)iData.GetData(DataFormats.Text);
            //    }
            //}
            #endregion

            if (textBox1.Text.IsNotNullOrEmpty())
                SetClipboard(textBox1.Text);
        }

        /// <summary>
        ///  生成单个数据库所有表的实体模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGenerateAllDB_Click(object sender, EventArgs e)
        {


            //打开生成数据库文件的所在文件夹
            System.Diagnostics.Process.Start("Explorer.exe", GetRootPath());
            Task.Factory.StartNew(() =>
            {
                //isGeneralSingleDB = true;
                //生成库中所有的model .cs文件

                //db
                var dbList = GetDatabases();
                Parallel.ForEach(dbList, (db) =>
                {
                    //获取表列表
                    var tableList = GetTableList(db);
                    //删除此db目录
                    var dbPath = GetDbPath(db);
                    //if (Directory.Exists(dbPath))
                    //    Directory.Delete(dbPath, true);
                    //else
                    //    Directory.CreateDirectory(dbPath);

                    if (!Directory.Exists(dbPath))  //如果不存在目录则创建，已有的实体文件会覆盖到此文件上,查看文件是否更新查看修改时间即可
                        Directory.CreateDirectory(dbPath);

                    Parallel.ForEach(tableList, (table, ParallelLoopState) =>
                    {
                        //  ParallelLoopStates.Add(ParallelLoopState);
                        var properties = GetTableField(db, table);
                        GenerateDBFile(db, table, dbPath, properties);
                    });
                });


            });
        }

        /// <summary>
        /// 获取根目录
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private string GetRootPath()
        {
            //驱动器盘符列表
            var driver = GetLastDriver();

            string rootPath = $@"{driver}\GeneratePropertiesOutput\";

            return rootPath;

        }

        /// <summary>
        /// 获取Database目录存放路径
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private string GetDbPath(string dbName)
        {
            //驱动器盘符列表
            var driver = GetLastDriver();

            string dbPath = $@"{driver}\GeneratePropertiesOutput\{dbName}\";

            return dbPath;

        }


        private void GenerateDBFile(string db, string table, string dbPath, string content)
        {
            if (db.IsNullOrEmpty() || table.IsNullOrEmpty() || textBox1.Text.IsNullOrEmpty())
                return;


            var fileName = $@"{dbPath}{table}.cs";
            string text = content;

            GenerateFile(fileName, text);

        }

        private void GenerateFile(string fileName, string text)
        {
            File.WriteAllText(fileName, text);

            //FileStream fs = File.OpenWrite(fileName);
            ////将字符串转换为字节数组
            //Byte[] info = Encoding.Default.GetBytes(text);
            ////向文件流中写入文件
            //fs.Write(info, 0, info.Length);
            //fs.Close();   //关闭文件流

        }


        private void button1_Click(object sender, EventArgs e)
        {
            #region 判断盘符是否存在


            bool bc = DriverExists(@"C:\");
            MessageBox.Show(bc.ToString());//true


            bool bd = DriverExists(@"D:\");
            MessageBox.Show(bd.ToString());//true

            bool be = DriverExists(@"E:\");
            MessageBox.Show(be.ToString());//true


            bool bt = DriverExists(@"T:\");
            MessageBox.Show(bt.ToString());//false





            #endregion
        }

        /// <summary>
        /// 盘符是否存在
        /// </summary>
        /// <param name="DriverName"></param>
        /// <returns></returns>
        public static bool DriverExists(string DriverName)
        {
            return System.IO.Directory.GetLogicalDrives().Contains(DriverName);
        }


        /// <summary>
        /// 获取最后一个磁盘驱动器盘符
        /// </summary>
        /// <returns></returns>
        private string GetLastDriver()
        {
            var list = GetRemovableDeviceID();
            return list.Last();
        }


        /// <summary>
        /// 获取计算机上的全部磁盘驱动器盘符
        /// </summary>
        /// <returns></returns>
        private List<string> GetRemovableDeviceID()
        {
            List<string> deviceIDs = new List<string>();
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT  *  From  Win32_LogicalDisk ");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {

                switch (int.Parse(mo["DriveType"].ToString()))
                {
                    case (int)DriveType.Removable:   //可以移动磁盘     
                        {
                            //MessageBox.Show("可以移动磁盘");
                            deviceIDs.Add(mo["DeviceID"].ToString());
                            break;
                        }
                    case (int)DriveType.Fixed:   //本地磁盘     
                        {
                            //MessageBox.Show("本地磁盘");
                            deviceIDs.Add(mo["DeviceID"].ToString());
                            break;
                        }
                    case (int)DriveType.CDRom:   //CD   rom   drives     
                        {
                            //MessageBox.Show("CD   rom   drives ");
                            break;
                        }
                    case (int)DriveType.Network:   //网络驱动   
                        {
                            //MessageBox.Show("网络驱动器 ");
                            break;
                        }
                    case (int)DriveType.Ram:
                        {
                            //MessageBox.Show("驱动器是一个 RAM 磁盘 ");
                            break;
                        }
                    case (int)DriveType.NoRootDirectory:
                        {
                            //MessageBox.Show("驱动器没有根目录 ");
                            break;
                        }
                    default:   //defalut   to   folder     
                        {
                            //MessageBox.Show("驱动器类型未知 ");
                            break;
                        }
                }

            }
            return deviceIDs;
        }

        #region 取得ComboBox中的Items属性的List
        /// <summary>
        /// 取得ComboBox中的Items属性的List
        /// </summary>
        /// <param name="cobName">ComboBox控件名称</param>
        /// <returns>List</returns>
        //public List<string> GetCbomboxItems(ComboBox cobName)
        //{
        //    /*两种方法都可以使用,如果数据量大时可能方法2效率更高一点，没有实际测试过，只是个人见解，因为方法1是循环，方法2直接是复制，所以我认为方法2效率更高点，数据量小的话就不要计较了*/
        //    //==============方法1=============================
        //    List<string> tmpItemsList = new List<string>();
        //    for (int i = 0; i <= cobName.Items.Count - 1; i++)
        //    {
        //        //tmpItemsList.Add(cobName.Items[i].ToString
        //        //this.comboBox1.GetItemText(cobName.Items[i]);
        //        tmpItemsList.Add(this.comboBox1.GetItemText(cobName.Items[i]));
        //    }
        //    /*//==============方法2===========================
        //    string[] aa = new string[cobName.Items.Count];
        //    cobName.Items.CopyTo(aa, 0);
        //    List<string> tmpItemsList = new List<string>(aa);
        //    */
        //    return tmpItemsList;
        //}
        #endregion

        // static List<ParallelLoopState> ParallelLoopStates = new List<ParallelLoopState>();
        /// <summary>
        /// 生成单个数据库所有表的实体模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateSingleDB_Click(object sender, EventArgs e)
        {
            // ParallelLoopStates = new List<ParallelLoopState>();

            //table
            string db = this.comboBox1.Text;

            var dbPath = GetDbPath(db);


            //删除此db目录
            //if (Directory.Exists(dbPath))
            //    Directory.Delete(dbPath, true);
            //else
            //    Directory.CreateDirectory(dbPath);

            if (!Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);//如果不存在目录则创建，已有的实体文件会覆盖到此文件上,查看文件是否更新查看修改时间即可


            //打开生成数据库文件的所在文件夹
            System.Diagnostics.Process.Start("Explorer.exe", dbPath);
            //DirectoryInfo root = new DirectoryInfo(path);
            //FileInfo[] files = root.GetFiles();


            Task.Factory.StartNew(() =>
            {
                //isGeneralSingleDB = true;
                //生成库中所有的model .cs文件

                //删除所有文件
                //foreach (var item in files)
                //{
                //    File.Delete(item.FullName);
                //}


                //获取表列表
                var tableList = GetTableList(db);

                Parallel.ForEach(tableList, (table, ParallelLoopState) =>
                {
                    //  ParallelLoopStates.Add(ParallelLoopState);
                    var properties = GetTableField(db, table);
                    GenerateDBFile(db, table, dbPath, properties);
                });



            });


            //if (!isGeneralSingleDB)
            //{
            //    logger.Info("进入btnGenerateSingleDB_Click执行...");

            //}
            //logger.Info("跳出btnGenerateSingleDB_Click...");

            //http://www.manongjc.com/detail/27-vslkmlzhyrzgkxv.html 参考资料


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        //Newtonsoft documents
        //https://www.newtonsoft.com/json/help/html/Introduction.htm

        /// <summary>
        /// Json生成Json实体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJsonToEntity_Click(object sender, EventArgs e)
        {
            JsonToEntity();
        }

        void JsonToEntity()
        {
            var json = txtJson.Text.Trim();
            var properties = JsonToEntity(json);
            this.textBox1.Text = properties;


            //加入到粘贴板
            SetClipboard(properties);

            #region 生成文件

            //savePath
            string savePath = $@"{GetRootPath()}JsonToEntity";
            if (Directory.Exists(savePath) == false)
                Directory.CreateDirectory(savePath);

            var fileName = $"{savePath}\\entity.cs";
            //创建文件
            GenerateFile(fileName, properties);

            #endregion
        }

        public string JsonToEntity(string jsonString)
        {
            var jObject = JObject.Parse(jsonString);//Newtonsoft.Json中的JObject.Parse转换成json对象

            Dictionary<string, string> classDicts = new Dictionary<string, string>();//key为类名，value为类中的所有属性定义的字符串
            classDicts.Add("Root", JsonHelper.GetClassDefinion(jObject));//拼接顶层的类
            foreach (var item in jObject.Properties())
            {
                if (item.Value.HasValues)//有子属性
                {
                    classDicts.Add(item.Name, JsonHelper.GetClassDefinion(item.Value));
                    JsonHelper.GetClasses(item.Value, classDicts);
                }
            }
            //下面是将所有的类定义完整拼接起来
            StringBuilder sb = new StringBuilder();






            sb.Append($"using System;\r\n");
            sb.Append($"using System.Collections.Generic;\r\n");
            sb.Append($"using System.Linq;\r\n");
            sb.Append($"using System.Text;\r\n");
            sb.Append($"using System.Threading.Tasks;\r\n\r\n");
            sb.Append("namespace Model\r\n");
            sb.Append("{\r\n");

            foreach (var item in classDicts)
            {
                sb.Append($"    public class {item.Key}\r\n");
                sb.Append("    {\r\n\r\n");
                sb.Append(item.Value);
                sb.Append("    }\r\n");
            }
            sb.Append("}");
            return sb.ToString();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.linkLabel1.Text);
            //能不能让IE新开一个窗口？只要把代码改成：
            //System.Diagnostics.Process.Start("iexplore.exe", "http://www.google.com");

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtJson_TextChanged(object sender, EventArgs e)
        {
            JsonToEntity();
        }

        /// <summary>
        /// 设置剪贴板内容
        /// </summary>
        /// <param name="content"></param>
        void SetClipboard(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return;
            Clipboard.SetText(content);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //释放资源
            this.Dispose();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var tables = comboBox2.DataSource as List<string>;

            var targetTables = tables.Where(a => a.ToLower().Contains(textBox2.Text.ToLower())).ToList();
            if (targetTables != null && targetTables.Count > 0)
                this.lstFilterTable.DataSource = targetTables;




        }

        private void 管理数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ManageDatabase frm = new frm_ManageDatabase();
            frm.Show();
        }

        private void 测试在全局捕捉主线程和非主线程异常ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_TestCatchException frm = new frm_TestCatchException();
            frm.Show();
        }

        private void cboChooseDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbHelper.DefaultConnString = Setup.dbs[this.cboChooseDatabase.SelectedIndex].DBConnectionString;
            RenderDatabases();

            var builder = new SqlConnectionStringBuilder(DbHelper.DefaultConnString);

            //设置数据库连接字符串中的当前数据库
            var databases = comboBox1.DataSource as List<string>;

            var targetDatabase = databases.Where(a => a.ToLower() == builder.InitialCatalog.ToLower()).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(targetDatabase))
                this.comboBox1.SelectedIndex = databases.FindIndex(a => a == targetDatabase);




        }

        private void btnEditSqlFieldAnnotation_Click(object sender, EventArgs e)
        {
            EditSqlFieldAnnotation();
        }

        void EditSqlFieldAnnotation()
        {
            string db = this.comboBox1.Text;
            //table
            string table = this.comboBox2.Text;
            //窗口状态持久化，传入dbhelper，编辑数据库表字段注释
            var fieldList = _GetTableField(table, db);
            //子窗体处理字段
            //new frm_EditSqlFieldAnnotation(fieldList).Show();
            tableFields = new BindingList<TableField>();
            tableFields.AddRange(fieldList);
            this.table1.Binding(tableFields);//table绑定数据源

        }
        private BindingList<TableField> tableFields = new BindingList<TableField>();

        private void lstFilterTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tables = comboBox2.DataSource as List<string>;
            var targetTable = tables.Where(a => a.ToLower() == this.lstFilterTable.Text.ToLower()).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(targetTable))
                this.comboBox2.SelectedIndex = tables.FindIndex(a => a == targetTable);
        }

        private bool table1_CellEndEdit(object sender, TableEndEditEventArgs e)
        {
            //table
            string table = this.comboBox2.Text;
            var updateSqlFieldSql = $@"EXEC sys.sp_updateextendedproperty
    @name = N'MS_Description',  -- 固定为 MS_Description，表示注释属性
    @value = N'${e.Value}',   -- 此处替换为你需要设置的新注释文本
    @level0type = N'SCHEMA',   -- 一级对象类型为架构
    @level0name = N'dbo',      -- 一级对象名，通常是架构名，如 dbo
    @level1type = N'TABLE',    -- 二级对象类型为表
    @level1name = N'{table}', -- 此处替换为你的表名
    @level2type = N'COLUMN',   -- 三级对象类型为列
    @level2name = N'{tableFields[e.RowIndex - 1].FieldName}'; -- 此处替换为你的字段名";
            DbHelper.ExecSqlNonQuerry(updateSqlFieldSql);
            return true;
        }
    }
}
