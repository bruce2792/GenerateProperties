using System;
using System.IO;
using OfficeOpenXml;

class Program
{
    static void Main()
    {
        // ���� EPPlus ���֤������ҵ��;��
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        string templatePath = @"ģ��·��\���ģ��.xlsx";
        string outputPath = @"���·��\��ע�͵�ģ��.xlsx";

        FileInfo templateFile = new FileInfo(templatePath);
        FileInfo outputFile = new FileInfo(outputPath);

        using (ExcelPackage package = new ExcelPackage(templateFile))
        {
            // ��ȡ��һ��������
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            // ʾ������ A1 ��Ԫ�����ע��
            var cell = worksheet.Cells["A1"];
            cell.AddComment("�����ֶ�ע������", "ϵͳ");

            // �����ѭ����Ӷ��ע��
            // ���磺worksheet.Cells["B1"].AddComment("�ֶ�˵�����û�ID", "ϵͳ");

            // ����Ϊ���ļ�
            package.SaveAs(outputFile);
        }

        Console.WriteLine("ע����д����ɣ��ļ������ڣ�" + outputPath);
    }
}