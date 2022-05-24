
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtJson = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnJsonToEntity = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据库：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(88, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(167, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(100, 190);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(798, 648);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "实体属性：";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(387, 31);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(181, 20);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "表：";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(261, 23);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(71, 35);
            this.btnReload.TabIndex = 0;
            this.btnReload.Text = "刷新";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btGenerateSingleDB
            // 
            this.btGenerateSingleDB.Location = new System.Drawing.Point(577, 23);
            this.btGenerateSingleDB.Name = "btGenerateSingleDB";
            this.btGenerateSingleDB.Size = new System.Drawing.Size(124, 35);
            this.btGenerateSingleDB.TabIndex = 7;
            this.btGenerateSingleDB.Text = "生成选中库的全部表";
            this.btGenerateSingleDB.UseVisualStyleBackColor = true;
            this.btGenerateSingleDB.Click += new System.EventHandler(this.btnGenerateSingleDB_Click);
            // 
            // btGenerateAllDB
            // 
            this.btGenerateAllDB.Location = new System.Drawing.Point(707, 23);
            this.btGenerateAllDB.Name = "btGenerateAllDB";
            this.btGenerateAllDB.Size = new System.Drawing.Size(121, 35);
            this.btGenerateAllDB.TabIndex = 7;
            this.btGenerateAllDB.Text = "生成全部库的全部表";
            this.btGenerateAllDB.UseVisualStyleBackColor = true;
            this.btGenerateAllDB.Click += new System.EventHandler(this.btGenerateAllDB_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtJson);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnJsonToEntity);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 67);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "根据JSON内容生成实体属性";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
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
            // txtJson
            // 
            this.txtJson.Location = new System.Drawing.Point(88, 26);
            this.txtJson.Name = "txtJson";
            this.txtJson.Size = new System.Drawing.Size(480, 21);
            this.txtJson.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnReload);
            this.groupBox2.Controls.Add(this.btGenerateAllDB);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btGenerateSingleDB);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Location = new System.Drawing.Point(12, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(886, 79);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "根据数据库表生成实体";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // btnJsonToEntity
            // 
            this.btnJsonToEntity.Location = new System.Drawing.Point(577, 18);
            this.btnJsonToEntity.Name = "btnJsonToEntity";
            this.btnJsonToEntity.Size = new System.Drawing.Size(124, 35);
            this.btnJsonToEntity.TabIndex = 7;
            this.btnJsonToEntity.Text = "生成实体";
            this.btnJsonToEntity.UseVisualStyleBackColor = true;
            this.btnJsonToEntity.Click += new System.EventHandler(this.btnJsonToEntity_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 847);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成属性";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}

