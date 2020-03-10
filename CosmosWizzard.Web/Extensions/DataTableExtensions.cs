namespace CosmosWizard.Web.Extensions
{
    using System;
    using System.Data;
    using System.Drawing;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;

    public static class DataTableExtensions
    {
        public static byte[] GetExcelBytes(this DataTable dt)
        {
            using (var xlPackage = new ExcelPackage())
            {
                var worksheetName = $"export.{DateTime.Now.ToString("yyyy.MM.dd")}";

                // create worksheet if it does not exist
                var worksheet = xlPackage.Workbook.Worksheets[worksheetName] ?? xlPackage.Workbook.Worksheets.Add(worksheetName);

                worksheet.Cells["A1"].LoadFromDataTable(dt, true);

                // apply header styling
                var topRow = worksheet.Row(1);
                topRow.Style.Font.Bold = true;
                topRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
                topRow.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                topRow.Style.Font.Color.SetColor(Color.DarkGreen);

                // format decimal columns
                var decimalColumns = new[] { 5, 6 };
                foreach (var index in decimalColumns)
                {
                    worksheet.Column(index).Style.Numberformat.Format = @"0\%";
                }

                // apply final styling
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                worksheet.Cells[worksheet.Dimension.Address].AutoFilter = true;

                worksheet.Column(2).Width = 40;
                worksheet.Column(3).Width = 60;
                worksheet.Column(4).Width = 60;

                return xlPackage.GetAsByteArray();
            }
        }
    }
}