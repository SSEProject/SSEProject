using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
namespace SSEProject.Account
{
    public partial class Assign : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=sseproject1.database.windows.net;Initial Catalog=sseDB;Integrated Security=False;User ID=sseAdmin;Password=sse1234Roach;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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
                SqlCommand cmd = new SqlCommand("Select [Username] From [Users] ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
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
                    SqlCommand cmd = new SqlCommand("Select * From [Items] WHERE [Id]=@ID", con);
                    cmd.Parameters.AddWithValue("@ID", id.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data);
                }
                selectedItemsGrid.DataSource = data;
                selectedItemsGrid.DataBind();
                con.Close();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Text = ex.Data + "Please select Items to assign! ";
            }
            finally
            {
                MessageBox.Text = "";
            }
        }
        protected void itemsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["Status"].ToString().Equals("In Progress"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(" #FFEC94");
                }
                else if (drv["Status"].ToString().Equals("Ready"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#56BAEC");
                }
                else
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
                }
            }
        }

        protected void ButtonAssignGo_Click(object sender, ImageClickEventArgs e)
        {

            List<Label> ids = (List<Label>)System.Web.HttpContext.Current.Session["ids"];
            string assigned = usersDropDownList.SelectedItem.Text;
            foreach (Label id in ids)
            {
                string sqlQuery = "UPDATE [Items] SET [AssignedTo] = @assignedTo WHERE [Id] = @id";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@assignedTo", assigned);
                cmd.Parameters.AddWithValue("@id", id.Text);
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Text = ex.Data + "Cannot connect to the Database! ";
                }
                finally
                {
                    MessageBox.Text = "";
                }
            }
            Response.Redirect("~/Account/HomePage.aspx");
        }
    }
}