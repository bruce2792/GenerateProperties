using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerateProperties
{
    // 错误显示窗体
    public class ErrorForm : Form
    {
        private TextBox txtExceptionDetails;
        private Button btnClose;
        private Label label1;

        public ErrorForm(Exception ex, string exceptionType)
        {
            InitializeComponent();
            this.Text = "错误报告";

            // 显示异常信息
            string errorMessage = $@"异常类型: {exceptionType}
异常消息: {ex.Message}
异常来源: {ex.Source}
堆栈跟踪: 
{ex.StackTrace}";

            this.txtExceptionDetails.Text = errorMessage;
        }

        private void InitializeComponent()
        {
            this.txtExceptionDetails = new TextBox();
            this.btnClose = new Button();
            this.label1 = new Label();
            this.SuspendLayout();

            // txtExceptionDetails
            this.txtExceptionDetails.Location = new System.Drawing.Point(12, 40);
            this.txtExceptionDetails.Multiline = true;
            this.txtExceptionDetails.Name = "txtExceptionDetails";
            this.txtExceptionDetails.ReadOnly = true;
            this.txtExceptionDetails.ScrollBars = ScrollBars.Vertical;
            this.txtExceptionDetails.Size = new System.Drawing.Size(460, 250);
            this.txtExceptionDetails.TabIndex = 0;

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(200, 300);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "程序发生未处理的异常:";

            // ErrorForm
            this.ClientSize = new System.Drawing.Size(484, 341);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtExceptionDetails);
            this.Name = "ErrorForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
