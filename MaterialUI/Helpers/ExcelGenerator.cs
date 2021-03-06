using ClosedXML.Excel;
using MaterialUI.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialUI.Helpers
{
    internal class ExcelGenerator
    {
        private  readonly XLWorkbook wb = new XLWorkbook();
        public void ListToExcel(List<DataModel> list)
        {
            try
            {
                wb.AddWorksheet($"Reporte_{DateTime.UtcNow.Month}_{DateTime.UtcNow.Day}");
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            var ws = wb.Worksheet($"Reporte_{DateTime.UtcNow.Month}_{DateTime.UtcNow.Day}");
            ws.Cell("A1").Value = "Data 1";
            ws.Cell("B1").Value = "Data 2";
            ws.Cell("C1").Value = "Data 3";
            ws.Cell("D1").Value = "Data 4";
            ws.Cell("E1").Value = "Data 5";
            ws.Cell("F1").Value = "Data 6";
            ws.Cell("G1").Value = "Fecha de Creacion";
            ws.Column("G").Width = 30;


            int row = 2;
            foreach(var c in list)
            {
                ws.Cell("A" + row.ToString()).Value = c.Data1;
                ws.Cell("B" + row.ToString()).Value = c.Data2;
                ws.Cell("C" + row.ToString()).Value = c.Data3;
                ws.Cell("D" + row.ToString()).Value = c.Data4;
                ws.Cell("E" + row.ToString()).Value = c.Data5;
                ws.Cell("F" + row.ToString()).Value = c.Data6;
                ws.Cell("G" + row.ToString()).Value = c.DateCreate;

                row++;
            }
                        
            wb.SaveAs($"{Environment.CurrentDirectory}/reporte_{Guid.NewGuid()}.xlsx");
        }
    }
}
