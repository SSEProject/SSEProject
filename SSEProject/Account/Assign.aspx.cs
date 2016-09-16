using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SSEProject.Account
{
    public partial class Assign : System.Web.UI.Page
    {
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\noama\Documents\Projects\SSEProject\Resources\ToDoList.accdb;Persist Security Info=True;Jet OLEDB:Database Password = 123456");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadSelectedItemsList();
                loadUsersToAssign();
                ButtonAssignGo.Attributes.Add("onclick", "javascript:return AssignConfirm()");
            }
        }
        private void loadUsersToAssign()
        {
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select [Username] From [Users] ", con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable data = new DataTable();
                da.Fill(data);
                usersDropDownList.DataSource = data;
                usersDropDownList.DataBind();
            }

            finally
            {
                con.Close();
            }
        }
        private void loadSelectedItemsList()
        {
            try
            {
                List<Label> ids = (List<Label>)System.Web.HttpContext.Current.Session["ids"];
                con.Open();
                DataTable data = new DataTable();
                foreach (Label id in ids)
                {
                    OleDbCommand cmd = new OleDbCommand("Select * From [Items] WHERE [ID]=@ID", con);
                    cmd.Parameters.AddWithValue("@ID", id.Text);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(data);
                }
                selectedItemsGrid.DataSource = data;
                selectedItemsGrid.DataBind();
               
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Text = ex.Data + "Please select Items to assign! ";
            }
            finally
            {
                con.Close();
            }
        }
        protected void itemsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["Status"].ToString().Equals("In Progress"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF284");
                }
                else if (drv["Status"].ToString().Equals("Ready"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9797");
                }
                else
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
                }
            }
        }
    }
}