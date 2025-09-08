using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerateProperties
{
    public partial class frm_TestCatchException : Form
    {
        private Button btnUIException;
        private Button btnNonUIException;
        private Label label1;

        public frm_TestCatchException()
        {
            this.InitializeComponent();
            this.Text = "全局异常处理演示";
        }

        private void InitializeComponent()
        {
            //this.components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            //this.Text = "frm_TestCatchException";

            this.btnUIException = new Button();
            this.btnNonUIException = new Button();
            this.label1 = new Label();
            this.SuspendLayout();

            // btnUIException
            this.btnUIException.Location = new System.Drawing.Point(50, 80);
            this.btnUIException.Name = "btnUIException";
            this.btnUIException.Size = new System.Drawing.Size(200, 40);
            this.btnUIException.TabIndex = 0;
            this.btnUIException.Text = "触发UI线程异常";
            this.btnUIException.UseVisualStyleBackColor = true;
            this.btnUIException.Click += new System.EventHandler(this.btnUIException_Click);

            // btnNonUIException
            this.btnNonUIException.Location = new System.Drawing.Point(50, 140);
            this.btnNonUIException.Name = "btnNonUIException";
            this.btnNonUIException.Size = new System.Drawing.Size(200, 40);
            this.btnNonUIException.TabIndex = 1;
            this.btnNonUIException.Text = "触发非UI线程异常";
            this.btnNonUIException.UseVisualStyleBackColor = true;
            this.btnNonUIException.Click += new System.EventHandler(this.btnNonUIException_Click);

            // label1
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "点击按钮测试不同类型的异常处理";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // MainForm
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNonUIException);
            this.Controls.Add(this.btnUIException);
            this.Name = "MainForm";
            this.ResumeLayout(false);
        }

        private void btnUIException_Click(object sender, EventArgs e)
        {
            // 在UI线程上抛出异常
            throw new InvalidOperationException("这是一个在UI线程上抛出的示例异常");
        }

        private void btnNonUIException_Click(object sender, EventArgs e)
        {
            // 在非UI线程上抛出异常
            Thread thread = new Thread(() =>
            {
                throw new AccessViolationException("这是一个在非UI线程上抛出的示例异常");
            });
            thread.Start();
        }
    }
}
