using GenerateProperties.Model;
using GenerateProperties.Util;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region 读取有哪些数据库
            var getAllDatabaseSql = "SELECT NAME FROM MASTER..SYSDATABASES";
            var dbList = DbHelper.ExecSqlDataReader<string>(getAllDatabaseSql);
            ArrayList mylist = new ArrayList();
            for (int i = 0; i < dbList.Count; i++)
            {
                mylist.Add(new DictionaryEntry(i, dbList[i]));
            }

            comboBox1.DataSource = mylist;
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            #endregion










            #region 设置combobox demo
            //ArrayList mylist = new ArrayList();
            //mylist.Add(new DictionaryEntry("1", "全部"));
            //mylist.Add(new DictionaryEntry("2", "正常"));
            //mylist.Add(new DictionaryEntry("3", "终止"));
            //comboBox1.DataSource = mylist;
            //comboBox1.DisplayMember = "Value";
            //comboBox1.ValueMember = "Key";

            #endregion
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取表列表
            GetTableList();
        }

        private void GetTableList()
        {
            //第一种方法获取值
            string db = this.comboBox1.Text;
            string combobox1_index = this.comboBox1.SelectedIndex.ToString();

            if (db != "System.Collections.DictionaryEntry")
            {
                //logger.Info("combobox1_value===" + combobox1_value);
                //logger.Info("combobox1_index===" + combobox1_index);

                #region 读取有哪些表
                var getAllTableSql = "SELECT NAME FROM SYSOBJECTS WHERE TYPE='U'  --读取所有表";
                var tableList = DbHelper.ExecSqlDataReader<string>(getAllTableSql, db);
                ArrayList mylist = new ArrayList();
                for (int i = 0; i < tableList.Count; i++)
                {
                    mylist.Add(new DictionaryEntry(i, tableList[i]));
                }

                comboBox2.DataSource = mylist;
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";
                #endregion
            }



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


        /// <summary>
        /// 获取数据库表中的字段
        /// </summary>
        private void GetTableField(string db, string table)
        {



            if (table != "System.Collections.DictionaryEntry")
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
AND STYPE.NAME<>'SYSNAME'";
                var fieldList = DbHelper.ExecSqlDataReader<TableField>(getAllFieldSql, db);
                var properties = string.Empty;

                properties += $"using System\r\n";
                properties += $"using System.Collections.Generic;\r\n";
                properties += $"using System.Linq;\r\n";
                properties += $"using System.Text;\r\n";
                properties += $"using System.Threading.Tasks;\r\n\r\n";

                properties += "namespace Model\r\n";
                properties += "{\r\n";

                properties += $"    public class {table}\r\n";
                properties += "    {\r\n";


                foreach (var item in fieldList)
                {
                    if (item.FieldType == "varchar" || item.FieldType == "nvarchar" || item.FieldType == "text")
                        properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public string {item.FieldName} {{get;set;}}\r\n\r\n";
                    else if (item.FieldType == "uniqueidentifier")
                        properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public Guid {item.FieldName} {{get;set;}}\r\n\r\n";
                    else if (item.FieldType == "smallint")
                        properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public short {item.FieldName} {{get;set;}}\r\n\r\n";
                    else if (item.FieldType == "int ")
                        properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public int {item.FieldName} {{get;set;}}\r\n\r\n";
                    else if (item.FieldType == "decimal ")
                        properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public decimal {item.FieldName} {{get;set;}}\r\n\r\n";
                    if (item.FieldType == "datetime")
                        properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public DateTime {item.FieldName} {{get;set;}}\r\n\r\n";
                }
                properties += "    }\r\n";
                properties += "}";


                if (properties.IsNotNullOrEmpty())
                {
                    this.textBox1.Text = properties;
                    //加入到粘贴板
                    Clipboard.SetDataObject(properties);
                    GenerateFile();
                }
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

            GetTableField(db, table);
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
                Clipboard.SetDataObject(textBox1.Text);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            GetTableList();
        }
        /// <summary>
        /// 生成单个数据库所有表的实体模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateSingleDB_Click(object sender, EventArgs e)
        {
            //生成库中所有的model .cs文件


            GenerateFile();

        }

        /// <summary>
        ///  生成单个数据库所有表的实体模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGenerateAllDB_Click(object sender, EventArgs e)
        {

        }




        private void GenerateFile()
        {
            //table
            string db = this.comboBox1.Text;
            string table = this.comboBox2.Text;
            if (db.IsNullOrEmpty() || table.IsNullOrEmpty() || textBox1.Text.IsNullOrEmpty())
                return;

            //驱动器盘符列表
            var driver = GetLastDriver();

            string path = $@"{driver}\GeneratePropertiesOutput\{db}\";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }


            var fileName = $@"{path}{table}.cs";
            string text = this.textBox1.Text;

            FileStream fs = File.OpenWrite(fileName);
            //将字符串转换为字节数组
            Byte[] info = Encoding.Default.GetBytes(text);
            //向文件流中写入文件
            fs.Write(info, 0, info.Length);
            fs.Close();   //关闭文件流

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
        /// 获取最后一个盘符
        /// </summary>
        /// <returns></returns>
        private string GetLastDriver()
        {
            var list = GetRemovableDeviceID();
            return list.Last();
        }

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            //var list = GetRemovableDeviceID();

            //foreach (var item in list)
            //{
            //    logger.Warn(item);
            //}
            //logger.Warn("---");
            //logger.Warn(list);
            //MessageBox.Show(GetLastDriver());


        }


    }
}
