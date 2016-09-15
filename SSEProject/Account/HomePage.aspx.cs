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
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Samreen\SSEProject\Resources\ToDoList.accdb;Persist Security Info=True;Jet OLEDB:Database Password = 123456");
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
            OleDbCommand com = new OleDbCommand("delete from [Items] where ID=@ID", con);
            Debug.Write("********************Retrieved ID is : " + id);
            com.Parameters.AddWithValue("@ID", id);
            com.ExecuteNonQuery();
        }

        protected void buttonDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            foreach (GridViewRow grow in itemsGrid.Rows)
            {
                CheckBox selectRow = (CheckBox)grow.FindControl("selectRow");
                //If CheckBox is checked than delete the record with particular id  
                if (selectRow.Checked)
                {
                    Label id = grow.FindControl("lbl_ID") as Label;
                    DeleteRecord(id.Text);
                }  
            }
            con.Close();
            loadList();
        }
        protected void itemsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label ID =itemsGrid.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            TextBox Description = itemsGrid.Rows[e.RowIndex].FindControl("Description") as TextBox;
            TextBox Time_Box = itemsGrid.Rows[e.RowIndex].FindControl("Time") as TextBox;
            TextBox Status = itemsGrid.Rows[e.RowIndex].FindControl("Status") as TextBox;
            string sqlQuery = "UPDATE [Items] SET [Description] = @description, [Time] = @datevalue, [Status] = @status WHERE [ID] = @id";
            OleDbCommand cmd = new OleDbCommand(sqlQuery, con);
            cmd.Parameters.AddWithValue("@description", Description.Text);
            cmd.Parameters.AddWithValue("@datevalue",Time_Box.Text);
            cmd.Parameters.AddWithValue("@status", Status.Text);
            cmd.Parameters.AddWithValue("@id", ID.Text);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            itemsGrid.EditIndex = -1;
            loadList();

        }
        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            ImageButton bt = (ImageButton)sender;
            GridViewRow grdRow = (GridViewRow)bt.Parent.Parent;

            TextBox ID = (TextBox)(grdRow.Cells[0].FindControl("tbID"));
            TextBox Description = (TextBox)(grdRow.Cells[0].FindControl("tbDescription"));
            TextBox Time = (TextBox)(grdRow.Cells[0].FindControl("tbTime"));
            TextBox Status = (TextBox)(grdRow.Cells[0].FindControl("tbStatus"));
            string sqlQuery = "INSERT INTO [Items] VALUES(@ID, @Description, @Time, @Status)";
            OleDbCommand cmd = new OleDbCommand(sqlQuery, con);
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ID", ID.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", Description.Text.Trim());
            cmd.Parameters.AddWithValue("@Time", Time.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", Status.Text.Trim());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            loadList();
        }

        protected void itemsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["Status"].ToString().Equals("Completed"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#AAF200");
                }
                else if (drv["Status"].ToString().Equals("In Progress"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCF75");
                }
                else if (drv["Status"].ToString().Equals("Ready"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF3333");
                }
                else
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
                }
            }
        }


    }
}