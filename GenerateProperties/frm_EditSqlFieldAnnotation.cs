using AntdUI;
using GenerateProperties.Model;
using NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerateProperties
{
    public partial class frm_EditSqlFieldAnnotation : Form
    {
        private string db;
        private string table;
        private BindingList<TableField> tableFields = new BindingList<TableField>();
        public frm_EditSqlFieldAnnotation(List<TableField> _tableFields)
        {
            InitializeComponent();
            tableFields.AddRange(_tableFields);
        }

        private void frm_EditSqlFieldAnnotation_Load(object sender, EventArgs e)
        {
            //this.table1.AutoSizeColumnsMode = ColumnsMode.Auto;//自动调整列宽
            this.table1.EditMode = TEditMode.Click;//单机进入修改模式
            this.table1.Binding(tableFields);//table绑定数据源
        }
    }
}
