using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace SSEProject.Account
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void itemsGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GetList_Click(object sender, EventArgs e)
        {
            String name = "Items";
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\Resources\Sample.xlsx;
                                        Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";


            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
            con.Open();
        
            OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
            sda.Fill(data);
            itemsGrid.DataSource = data;
        }
    }
}