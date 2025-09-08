using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateProperties.Util
{
    public class TxtHelper
    {
        public static string GetTxt(string filePath)
        {
            //如果fs.open 文件不存在则创建,存在则读取内容返回

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
          
        }


        public static void WriteTxt(string filePath, string content)
        {
            //写入文件，如果文件存在则覆盖
            using (var sw = new StreamWriter(filePath, false))
            {
                sw.Write(content);
            }
        }

    }
}
