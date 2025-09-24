
namespace GenerateProperties
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.btGenerateSingleDB = new System.Windows.Forms.Button();
            this.btGenerateAllDB = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtJson = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnJsonToEntity = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstFilterTable = new System.Windows.Forms.ListBox();
            this.btnEditSqlFieldAnnotation = new System.Windows.Forms.Button();
            this.cboChooseDatabase = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.管理数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择要连接的数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试在全局捕捉主线程和非主线程异常ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tooltipComponent1 = new AntdUI.TooltipComponent();
            this.panel1 = new System.Windows.Forms.Panel();
            this.table1 = new AntdUI.Table();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据库：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(73, 59);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(167, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(905, 118);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(136, 195);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(903, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "实体属性：";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(279, 61);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(181, 20);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "表：";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(359, 23);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(71, 20);
            this.btnReload.TabIndex = 0;
            this.btnReload.Text = "刷新";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btGenerateSingleDB
            // 
            this.btGenerateSingleDB.Location = new System.Drawing.Point(622, 16);
            this.btGenerateSingleDB.Name = "btGenerateSingleDB";
            this.btGenerateSingleDB.Size = new System.Drawing.Size(124, 35);
            this.btGenerateSingleDB.TabIndex = 7;
            this.btGenerateSingleDB.Text = "生成选中库的全部表";
            this.btGenerateSingleDB.UseVisualStyleBackColor = true;
            this.btGenerateSingleDB.Click += new System.EventHandler(this.btnGenerateSingleDB_Click);
            // 
            // btGenerateAllDB
            // 
            this.btGenerateAllDB.Location = new System.Drawing.Point(759, 16);
            this.btGenerateAllDB.Name = "btGenerateAllDB";
            this.btGenerateAllDB.Size = new System.Drawing.Size(121, 35);
            this.btGenerateAllDB.TabIndex = 7;
            this.btGenerateAllDB.Text = "生成全部库的全部表";
            this.btGenerateAllDB.UseVisualStyleBackColor = true;
            this.btGenerateAllDB.Click += new System.EventHandler(this.btGenerateAllDB_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.txtJson);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnJsonToEntity);
            this.groupBox1.Location = new System.Drawing.Point(3, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 67);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "根据JSON内容生成实体属性";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(492, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "Json转C#实体网站工具";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(623, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(263, 12);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://www.bejson.com/convert/json2csharp/";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // txtJson
            // 
            this.txtJson.Location = new System.Drawing.Point(88, 26);
            this.txtJson.Multiline = true;
            this.txtJson.Name = "txtJson";
            this.txtJson.Size = new System.Drawing.Size(480, 35);
            this.txtJson.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "JSON内容：";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnJsonToEntity
            // 
            this.btnJsonToEntity.Location = new System.Drawing.Point(577, 26);
            this.btnJsonToEntity.Name = "btnJsonToEntity";
            this.btnJsonToEntity.Size = new System.Drawing.Size(124, 35);
            this.btnJsonToEntity.TabIndex = 7;
            this.btnJsonToEntity.Text = "生成实体";
            this.btnJsonToEntity.UseVisualStyleBackColor = true;
            this.btnJsonToEntity.Click += new System.EventHandler(this.btnJsonToEntity_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstFilterTable);
            this.groupBox2.Controls.Add(this.btnEditSqlFieldAnnotation);
            this.groupBox2.Controls.Add(this.cboChooseDatabase);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.btnReload);
            this.groupBox2.Controls.Add(this.btGenerateAllDB);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btGenerateSingleDB);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Location = new System.Drawing.Point(3, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(886, 233);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "根据数据库表生成实体";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // lstFilterTable
            // 
            this.lstFilterTable.FormattingEnabled = true;
            this.lstFilterTable.ItemHeight = 12;
            this.lstFilterTable.Location = new System.Drawing.Point(463, 83);
            this.lstFilterTable.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstFilterTable.Name = "lstFilterTable";
            this.lstFilterTable.Size = new System.Drawing.Size(171, 148);
            this.lstFilterTable.TabIndex = 15;
            this.lstFilterTable.SelectedIndexChanged += new System.EventHandler(this.lstFilterTable_SelectedIndexChanged);
            // 
            // btnEditSqlFieldAnnotation
            // 
            this.btnEditSqlFieldAnnotation.Location = new System.Drawing.Point(544, 16);
            this.btnEditSqlFieldAnnotation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEditSqlFieldAnnotation.Name = "btnEditSqlFieldAnnotation";
            this.btnEditSqlFieldAnnotation.Size = new System.Drawing.Size(73, 35);
            this.btnEditSqlFieldAnnotation.TabIndex = 14;
            this.btnEditSqlFieldAnnotation.Text = "编辑注释";
            this.btnEditSqlFieldAnnotation.UseVisualStyleBackColor = true;
            this.btnEditSqlFieldAnnotation.Click += new System.EventHandler(this.btnEditSqlFieldAnnotation_Click);
            // 
            // cboChooseDatabase
            // 
            this.cboChooseDatabase.FormattingEnabled = true;
            this.cboChooseDatabase.Location = new System.Drawing.Point(157, 25);
            this.cboChooseDatabase.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboChooseDatabase.Name = "cboChooseDatabase";
            this.cboChooseDatabase.Size = new System.Drawing.Size(188, 20);
            this.cboChooseDatabase.TabIndex = 13;
            this.cboChooseDatabase.SelectedIndexChanged += new System.EventHandler(this.cboChooseDatabase_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 27);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "选择要使用的数据库连接：";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(463, 59);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(171, 21);
            this.textBox2.TabIndex = 8;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1226, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管理数据库ToolStripMenuItem,
            this.选择要连接的数据库ToolStripMenuItem,
            this.测试在全局捕捉主线程和非主线程异常ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 22);
            this.toolStripMenuItem1.Text = "设置";
            // 
            // 管理数据库ToolStripMenuItem
            // 
            this.管理数据库ToolStripMenuItem.Name = "管理数据库ToolStripMenuItem";
            this.管理数据库ToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.管理数据库ToolStripMenuItem.Text = "管理数据库";
            this.管理数据库ToolStripMenuItem.Click += new System.EventHandler(this.管理数据库ToolStripMenuItem_Click);
            // 
            // 选择要连接的数据库ToolStripMenuItem
            // 
            this.选择要连接的数据库ToolStripMenuItem.Name = "选择要连接的数据库ToolStripMenuItem";
            this.选择要连接的数据库ToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.选择要连接的数据库ToolStripMenuItem.Text = "选择要连接的数据库";
            // 
            // 测试在全局捕捉主线程和非主线程异常ToolStripMenuItem
            // 
            this.测试在全局捕捉主线程和非主线程异常ToolStripMenuItem.Name = "测试在全局捕捉主线程和非主线程异常ToolStripMenuItem";
            this.测试在全局捕捉主线程和非主线程异常ToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.测试在全局捕捉主线程和非主线程异常ToolStripMenuItem.Text = "测试在全局捕捉主线程和非主线程异常";
            this.测试在全局捕捉主线程和非主线程异常ToolStripMenuItem.Click += new System.EventHandler(this.测试在全局捕捉主线程和非主线程异常ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.table1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1226, 898);
            this.panel1.TabIndex = 12;
            // 
            // table1
            // 
            this.table1.Gap = 12;
            this.table1.Location = new System.Drawing.Point(3, 315);
            this.table1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.table1.Name = "table1";
            this.table1.Size = new System.Drawing.Size(1037, 569);
            this.table1.TabIndex = 10;
            this.table1.Text = "table1";
            this.table1.CellEndEdit += new AntdUI.Table.EndEditEventHandler(this.table1_CellEndEdit);
            this.table1.CellEndValueEdit += new AntdUI.Table.EndValueEditEventHandler(this.table1_CellEndValueEdit);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 922);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成属性";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btGenerateSingleDB;
        private System.Windows.Forms.Button btGenerateAllDB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtJson;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnJsonToEntity;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 管理数据库ToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboChooseDatabase;
        private System.Windows.Forms.ToolStripMenuItem 选择要连接的数据库ToolStripMenuItem;
        private AntdUI.TooltipComponent tooltipComponent1;
        private System.Windows.Forms.ToolStripMenuItem 测试在全局捕捉主线程和非主线程异常ToolStripMenuItem;
        private System.Windows.Forms.Button btnEditSqlFieldAnnotation;
        private System.Windows.Forms.ListBox lstFilterTable;
        private System.Windows.Forms.Panel panel1;
        private AntdUI.Table table1;
    }
}

