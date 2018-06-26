using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBased
{
    public partial class Home : System.Web.UI.Page
    {
        SqlConnection con;

        string sqlconn;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void connection()
        {
            var sqlconn = ConfigurationManager.ConnectionStrings["SqlCom"].ConnectionString;
            var con = new SqlConnection(sqlconn);

        }
        private void InsertCSVRecords(DataTable csvdt)
        {

            connection();
            //creating object of SqlBulkCopy    
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            //assigning Destination table name    
            objbulk.DestinationTableName = "Employee";
            //Mapping Table column    
            objbulk.ColumnMappings.Add("Name", "Name");
            objbulk.ColumnMappings.Add("City", "City");
            objbulk.ColumnMappings.Add("Address", "Address");
            objbulk.ColumnMappings.Add("Designation", "Designation");
            //inserting Datatable Records to DataBase    
            con.Open();
            objbulk.WriteToServer(csvdt);
            con.Close();


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //Creating object of datatable  
            DataTable tblcsv = new DataTable();
            //creating columns  
            tblcsv.Columns.Add("Name");
            tblcsv.Columns.Add("City");
            tblcsv.Columns.Add("Address");
            tblcsv.Columns.Add("Designation");
            //getting full file path of Uploaded file  
            string CSVFilePath = Path.GetFullPath(FileUpload1.PostedFile.FileName);
            //Reading All text  
            string ReadCSV = File.ReadAllText(CSVFilePath);
            //spliting row after new line  
            foreach (string csvRow in ReadCSV.Split('\n'))
            {
                if (!string.IsNullOrEmpty(csvRow))
                {
                    //Adding each row into datatable  
                    tblcsv.Rows.Add();
                    int count = 0;
                    foreach (string FileRec in csvRow.Split(','))
                    {
                        tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
                        count++;
                    }
                }


            }
            //Calling insert Functions  
            InsertCSVRecords(tblcsv);
        }
    }
}