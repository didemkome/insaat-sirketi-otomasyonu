using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
namespace BESEN_İNŞAAT_LTD.STİ
{
    class Excelveri

    {
        public string Exceldenveriaktar(string tablename)
        {
            try
            {
                String FolderPath = @"C:\";
                string DatabaseName = "Besen.İnsaat.Ltd.Sti.";
                string SQLServerName = "DESKTOP-M5E1QKG";

                SqlConnection SQLConnection = new SqlConnection();
                SQLConnection.ConnectionString = "Data Source = "
                    + SQLServerName + "; Initial Catalog ="
                    + DatabaseName + "; "
                    + "Integrated Security=true;";

                SQLConnection.Open();

                var directory = new DirectoryInfo(FolderPath);
                FileInfo[] files = directory.GetFiles();



                var FD = new System.Windows.Forms.OpenFileDialog();

                string Filepath = "", filename;
                FD.Filter = ".xlsx|*.xlsx| .xls |*.xls";
                FD.FilterIndex = 2;
                FD.Title = "Excel Dosyası Seçiniz..";
                if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Filepath = FD.FileName;
                    filename = FD.SafeFileName;

                }
                String excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", Filepath);
                using (OleDbConnection excelConnection = new OleDbConnection(excelConnString))
                {
                    //Create OleDbCommand to fetch data from Excel 
                    using (OleDbCommand cmd = new OleDbCommand("Select * from [Sheet1$]", excelConnection))
                    {
                        excelConnection.Open();
                        using (OleDbDataReader dReader = cmd.ExecuteReader())
                        {
                            using (SqlBulkCopy sqlBulk = new SqlBulkCopy(SQLConnection))
                            {
                                //Give your Destination table name 
                                sqlBulk.DestinationTableName = tablename;
                                sqlBulk.WriteToServer(dReader);
                                
                            }
                        }
                    }
                } return "oldu";
            }

            catch (Exception A)
            {
                return A.ToString();
            }
           


            //                            sheetname = sheetname.Replace("$", "");
            //                            filename = filename.Replace(".xls", "");


            //                            string tableDDL = "";
            //                            for (int i = 0; i < dt.Columns.Count; i++)
            //                            {
            //                                if (i != dt.Columns.Count - 1)
            //                                    tableDDL += "[" + dt.Columns[i].ColumnName + "] " + "NVarchar(max)" + ",";
            //                                else
            //                                    tableDDL += "[" + dt.Columns[i].ColumnName + "] " + "NVarchar(max)";
            //                            }
            //                            tableDDL += ")";


            //                            SqlCommand SQLCmd = new SqlCommand(tableDDL, SQLConnection);
            //                            SQLCmd.ExecuteNonQuery();

            //                            SqlBulkCopy blk = new SqlBulkCopy(SQLConnection);
            //                            blk.DestinationTableName = "[" + filename + "_" + sheetname + "]";
            //                            blk.WriteToServer(dt);
            //                            SQLConnection.Close();
            //                            cnn.Close();
            //}
            //}


        }

        public void ExportToExcel(DataGridView gridviewID, string excelFilename)
        {
            int excelno = 1;
            Microsoft.Office.Interop.Excel.Application objexcelapp = new Microsoft.Office.Interop.Excel.Application();
            objexcelapp.Application.Workbooks.Add(Type.Missing);
            objexcelapp.Columns.ColumnWidth = 15;
            for (int i = 1; i < gridviewID.Columns.Count + 1; i++)
            {
                objexcelapp.Cells[1, i] = gridviewID.Columns[i - 1].HeaderText;
            }
            /*For storing Each row and column value to excel sheet*/
            for (int i = 0; i < gridviewID.Rows.Count; i++)
            {
                for (int j = 0; j < gridviewID.Columns.Count; j++)
                {
                    if (gridviewID.Rows[i].Cells[j].Value != null)
                    {
                        objexcelapp.Cells[i + 2, j + 1] = gridviewID.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }
            MessageBox.Show("Your excel file exported successfully at D:\\" + excelFilename + ".xlsx");
            objexcelapp.ActiveWorkbook.SaveCopyAs("D:\\" + excelFilename + "_"+ excelno +".xlsx");
            objexcelapp.ActiveWorkbook.Saved = true;

        }


    }
} 
