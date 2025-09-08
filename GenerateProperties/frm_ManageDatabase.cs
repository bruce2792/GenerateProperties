using AntdUI;
using GenerateProperties.Model;
using GenerateProperties.Util;
using Newtonsoft.Json;
using NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GenerateProperties
{
    public partial class frm_ManageDatabase : Form
    {
        // 定义事件，使用在主窗体中定义的委托类型
        public event Form1.UpdateComboBoxDelegate UpdateComboBox;
        public frm_ManageDatabase()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Setup.dbs.Add(new DB_Connectionstrings()
            {
            
            });
        }

        private void frm_ManageDatabase_Load(object sender, EventArgs e)
        {
            //this.table1.AutoSizeColumnsMode = ColumnsMode.Auto;//自动调整列宽
            this.table1.EditMode = TEditMode.Click;//单机进入修改模式
            this.table1.Binding(Setup.dbs);//table绑定数据源
        }

        private bool table1_CellEndEdit(object sender, TableEndEditEventArgs e)
        {
            // value 修改后值, object? record 原始行, int rowIndex 行序号, int columnIndex 列序号
            Debug.WriteLine(e.ColumnIndex);
            Debug.WriteLine(e.Value);

            //控件提供的方法
            //this.dbs[e.RowIndex].SetPropertyValue<DB_Connectionstrings, string>(a => a.DBName, e.Value);

            Setup.dbs[e.RowIndex - 1].SetPropertyValue1(e.ColumnIndex, e.Value);
            Util.TxtHelper.WriteTxt(Setup.configureFilePath, JsonConvert.SerializeObject(this.table1.DataSource));
            UpdateComboBox?.Invoke(Setup.dbs.Select(a=>a.DBName));
            return true;
        }

        private bool table1_CellEndValueEdit(object sender, TableEndValueEditEventArgs e)
        {
            //Trace.WriteLine(e.ColumnIndex);
            //this.dbs[e.ColumnIndex].SetPropertyValue(a=>a.DBID==this.dbs[e.ColumnIndex].DBID, e.Value);
            //Util.TxtHelper.WriteTxt(Setup.configureFilePath, JsonConvert.SerializeObject(this.table1.DataSource));
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 200; i++)
            {
                Setup.dbs.Add(new DB_Connectionstrings());
            }
            Setup.dbs.Add(new DB_Connectionstrings() { DBName = "202" });

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.table1.ScrollLine(201);
        }

        private bool table1_CellBeginEdit(object sender, TableEventArgs e)
        {
            //if (e.Value == null)
            //    return false;

            //Clipboard.SetText(e.Value.ToString());
            return true;
        }
    }
}
