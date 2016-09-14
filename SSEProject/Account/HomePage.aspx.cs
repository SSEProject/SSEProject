using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSEProject.Account
{
    public partial class HomePage : System.Web.UI.Page
    {

        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\noama\Documents\Projects\SSEProject\Resources\ToDoList.accdb;Persist Security Info=True;Jet OLEDB:Database Password = 123456");
        protected void Page_Load(object sender, EventArgs e)
        {            
         
        }

     
        protected void loadList()
        {
            String name = "Items";
            
            OleDbCommand oconn = new OleDbCommand("Select * From " + name , con);
            con.Open();
        
            OleDbDataAdapter da = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
            da.Fill(data);
            itemsGrid.DataSource = data;
        }
        protected void itemsGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            itemsGrid.EditIndex = e.NewEditIndex;
            

        }
        protected void itemsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = itemsGrid.Rows[e.RowIndex].Cells[0].Text;
            string Description = itemsGrid.Rows[e.RowIndex].Cells[1].Text;
            string Time = itemsGrid.Rows[e.RowIndex].Cells[2].Text;
            string Status = itemsGrid.Rows[e.RowIndex].Cells[3].Text;
            Debug.WriteLine("Here I am in update method with:  "+ ID+ " ..." + Description+"..."+Time+"..."+Status);
            string sqlQuery = "UPDATE Items SET Description = @description, Time = @time, Status = @status WHERE [ID] = @id";
            OleDbCommand cmd = new OleDbCommand(sqlQuery,con);
            cmd.Parameters.AddWithValue("@description", Description);
            cmd.Parameters.AddWithValue("@time", Time);
            cmd.Parameters.AddWithValue("@status", Status);
            cmd.Parameters.AddWithValue("@id", ID);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        protected void itemsGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
           itemsGrid.EditIndex = -1;
            
        }
        protected void itemsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = itemsGrid.DataKeys[e.RowIndex].Values["ID"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand("delete from Items where ID=" + id, con);
            int result = cmd.ExecuteNonQuery();
            con.Close();
           
        }
        protected void itemsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ID"));
                Button lnkbtnresult = (Button)e.Row.FindControl("ButtonDelete");
                if (lnkbtnresult != null)
                {
                    lnkbtnresult.Attributes.Add("onclick", "javascript:return deleteConfirm('" + id + "')");
                }
            }
        }
        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                TextBox newID = (TextBox)itemsGrid.FooterRow.FindControl("newId");
                TextBox newDescription = (TextBox)itemsGrid.FooterRow.FindControl("newDescription");
                TextBox newTime = (TextBox)itemsGrid.FooterRow.FindControl("newTime");
                TextBox newStatus = (TextBox)itemsGrid.FooterRow.FindControl("newStatus");
                con.Open();
                OleDbCommand cmd = new OleDbCommand("insert into Items(ID,Description,Time,Status) values('" + newID.Text + "','" +
                        newDescription.Text + "','" + newTime.Text + "','" + newStatus.Text + "')", con);
                int result = cmd.ExecuteNonQuery();
                con.Close();
                 itemsGrid.DataBind();
            }
        }

      
    }
}