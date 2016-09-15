using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSEProject.Account
{
    public partial class HomePage : System.Web.UI.Page
    {
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\noama\Documents\Projects\SSEProject\Resources\ToDoList.accdb;Persist Security Info=True;Jet OLEDB:Database Password = 123456");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadList();
                ButtonDelete.Attributes.Add("onclick", "javascript:return DeleteConfirm()");
            }
        }

        protected void loadList()
        {
            String name = "Items";
            OleDbCommand oconn = new OleDbCommand("Select * From " + name, con);
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
            da.Fill(data);
            itemsGrid.DataSource = data;
            itemsGrid.DataBind();
        }
        protected void itemsGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            itemsGrid.EditIndex = e.NewEditIndex;
            loadList();
        }
        protected void itemsGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            itemsGrid.EditIndex = -1;
            loadList();
        }
        protected void DeleteRecord(string id)
        {
            OleDbCommand com = new OleDbCommand("delete from Items where ID=?", con);
            Debug.Write("Retrieved ID is : " + id);
            com.Parameters.AddWithValue("@ID", id);
            com.ExecuteNonQuery();
        }

        protected void buttonDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow in itemsGrid.Rows)
            {
                con.Open();
                CheckBox selectRow = (CheckBox)grow.FindControl("selectRow");
                //If CheckBox is checked than delete the record with particular empid  
                if (selectRow.Checked)
                {
                    string id = grow.Cells[1].Text;
                    DeleteRecord(id);
                }
                con.Close();
                loadList();
            }
        }
        protected void itemsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = itemsGrid.Rows[e.RowIndex].Cells[1].Text;
            string Description = itemsGrid.Rows[e.RowIndex].Cells[2].Text;
            string Time = itemsGrid.Rows[e.RowIndex].Cells[3].Text;
            string Status = itemsGrid.Rows[e.RowIndex].Cells[4].Text;
            Debug.WriteLine("Here I am in update method with:  " + ID + " ..." + Description + "..." + Time + "..." + Status);
            string sqlQuery = "UPDATE [Items] SET [Description] = @description, [Time] = @time, [Status] = @status WHERE [ID] = @id";
            OleDbCommand cmd = new OleDbCommand(sqlQuery, con);
            cmd.Parameters.AddWithValue("@description", Description);
            // fix date and time format exception 
            cmd.Parameters.AddWithValue("@time", DateTime.ParseExact(Time, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None));
            cmd.Parameters.AddWithValue("@status", Status);
            cmd.Parameters.AddWithValue("@id", ID);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            loadList();

        }

    }
}