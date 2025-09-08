using GenerateProperties.Model;
using Newtonsoft.Json;
using NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerateProperties
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 设置全局异常处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //获取数据库配置
            var configureContent = Util.TxtHelper.GetTxt(Setup.configureFilePath);
            if (!string.IsNullOrEmpty(configureContent))
                Setup.dbs.AddRange(JsonConvert.DeserializeObject<IList<DB_Connectionstrings>>(configureContent));




            Application.Run(new Form1());
        }

        // 处理UI线程异常
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowErrorDialog(e.Exception, "UI线程异常");
        }

        // 处理非UI线程异常
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                ShowErrorDialog(ex, "非UI线程异常");
            }
            else
            {
                MessageBox.Show($"发生了未知类型的异常: {e.ExceptionObject}",
                    "全局异常处理", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 显示错误对话框
        private static void ShowErrorDialog(Exception ex, string exceptionType)
        {
            using (var errorForm = new ErrorForm(ex, exceptionType))
            {
                errorForm.ShowDialog();
            }
        }
    }
}
