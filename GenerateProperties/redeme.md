using System;
using System.IO;
using OfficeOpenXml;

class Program
{
    static void Main()
    {
        // 设置 EPPlus 许可证（非商业用途）
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        string templatePath = @"模板路径\你的模板.xlsx";
        string outputPath = @"输出路径\带注释的模板.xlsx";

        FileInfo templateFile = new FileInfo(templatePath);
        FileInfo outputFile = new FileInfo(outputPath);

        using (ExcelPackage package = new ExcelPackage(templateFile))
        {
            // 获取第一个工作表
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            // 示例：向 A1 单元格添加注释
            var cell = worksheet.Cells["A1"];
            cell.AddComment("这是字段注释内容", "系统");

            // 你可以循环添加多个注释
            // 例如：worksheet.Cells["B1"].AddComment("字段说明：用户ID", "系统");

            // 保存为新文件
            package.SaveAs(outputFile);
        }

        Console.WriteLine("注释已写入完成，文件保存在：" + outputPath);
    }
}