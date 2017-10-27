using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI;
using System.IO;
using NPOI.XSSF;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace GeckoDownloadTest
{
    class ExcelEntity
    {
        public List<String> Header;
        public List<List<String>> Data;
        public ExcelEntity()
        {
            Header = new List<string>();
            Data = new List<List<string>>();
        }
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            using (FileStream file = new FileStream("c:\\work\\poiTest.xls", FileMode.Open, FileAccess.Read))
            {
                //var wb1 = new XSSFWorkbook(file);
                var wb1 = new HSSFWorkbook(file);
                ISheet sheet = wb1.GetSheetAt(0);
                ExcelEntity entity = new ExcelEntity();
                for (int rowIndex = 0; true; rowIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    if(row == null)
                    {
                        break;
                    }
                    List<String> buffer = null;
                    for(int cellIndex =0; true; cellIndex++)
                    {
                        if(row.GetCell(cellIndex) == null)
                        {
                            break;
                        }
                        if(rowIndex == 0)
                        {
                            entity.Header.Add(row.GetCell(cellIndex).StringCellValue);
                            continue;
                        }
                        if(cellIndex == 0)
                        {
                            entity.Data.Add(buffer = new List<string>());
                        }
                        if (row.GetCell(cellIndex).CellType == CellType.String)
                        {
                            buffer.Add(row.GetCell(cellIndex).StringCellValue);
                        }
                        else
                        {
                            buffer.Add(row.GetCell(cellIndex).NumericCellValue.ToString());
                        }
                    }
                }
                foreach(var v in entity.Header)
                {
                    Console.Write(v);
                    Console.Write("\t");
                }
                Console.WriteLine();
                foreach(var d in entity.Data)
                {
                    foreach(var d2 in d)
                    {
                        Console.Write(d2);
                        Console.Write("\t");
                    }
                    Console.WriteLine();
                }
            }
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
