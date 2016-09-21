using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SSEProject.Account
{
    public partial class HomePage : System.Web.UI.Page
    {
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Z:\SSE\Git\Git\SSEProject\SSEProject\Resources\ToDoList.accdb;Persist Security Info=True;Jet OLEDB:Database Password = 123456");
        String commandText = "";
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
            try
            {
                if (commandText != "TimerRefresh")
                {
                    OleDbCommand oconn = new OleDbCommand("Select * From [Items]", con);
                    con.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter(oconn);
                    DataTable data = new DataTable();
                    da.Fill(data);
                    itemsGrid.DataSource = data;
                    itemsGrid.DataBind();
                }
                foreach (GridViewRow grow in itemsGrid.Rows)
                {
                    Label Time = grow.FindControl("lbl_Time") as Label;
                    try
                    {
                        if (Time.Text != null)
                        {
                            DateTime Time_Date = DateTime.ParseExact(Time.Text.Trim(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                            var diff = (Time_Date.Date) - (DateTime.Now);
                            if (diff.TotalSeconds > 0)
                            {
                                Label lbl_Timer = grow.FindControl("lbl_Timer") as Label;
                                lbl_Timer.Text = string.Format("{0} d {1:D2}:{2:D2}:{3:D2}", diff.Days, diff.Hours, diff.Minutes, diff.Seconds);
                            }
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Text = ex.Source + "Please enter Due Date in { MM / dd / yyyy hh: mm: ss tt}";
                    }
                }
            }
            finally
            {
                con.Close();
            }
        }
        protected void itemsGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Countdown_Timer.Enabled = !Countdown_Timer.Enabled;
            itemsGrid.EditIndex = e.NewEditIndex;
            loadList();
        }
        protected void itemsGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            itemsGrid.EditIndex = -1;
            loadList();
            Countdown_Timer.Enabled = Countdown_Timer.Enabled;
        }
        protected void DeleteRecord(string id)
        {
            OleDbCommand com = new OleDbCommand("delete from [Items] where ID=@ID", con);
            com.Parameters.AddWithValue("@ID", id);
            com.ExecuteNonQuery();
        }
        protected void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                foreach (GridViewRow grow in itemsGrid.Rows)
                {
                    CheckBox selectRow = (CheckBox)grow.FindControl("selectRow");
                    //If CheckBox is checked than delete the record with particular id  
                    if (selectRow.Checked)
                    {
                        Label id = grow.FindControl("lbl_ID") as Label;
                        Countdown_Timer.Enabled = !Countdown_Timer.Enabled;
                        DeleteRecord(id.Text);
                    }
                }
            }
            finally
            {
                con.Close();
                loadList();
                Countdown_Timer.Enabled = Countdown_Timer.Enabled;
            }
        }
        protected void buttonAssign_Click(object sender, EventArgs e)
        {
            Debug.Write("---------------------->>Entered Assign Function");
            Countdown_Timer.Enabled = !Countdown_Timer.Enabled;
            List<Label> ids = new List<Label>();
            foreach (GridViewRow grow in itemsGrid.Rows)
            {
                CheckBox selectRow = (CheckBox)grow.FindControl("selectRow");
                if (selectRow.Checked)
                {
                    Debug.Write("---------------------->>checking if a row is selected-TRUE");
                    if ((grow.FindControl("lbl_Status") as Label).Text.Equals("Completed"))
                    {
                        MessageBox.Text = "Cannot Assign a Completed Item!!";

                    }
                    else
                    {
                        ids.Add(grow.FindControl("lbl_ID") as Label);
                    }
                }
            }
            Session["ids"] = ids;
            if (ids.Count != 0)
            {
                Debug.Write("---------------------->>Before going to Assign.aspx");
                Response.Redirect("~/Account/Assign.aspx");
                //Response.Write("  <script language='javascript'> window.open('Assign.aspx','','width=1020,Height=720,fullscreen=1,location=0,scrollbars=1,menubar=1,toolbar=1'); </script>");
            }
            else
            {
                Debug.Write("---------------------->>ids count =0");
                loadList();
                Countdown_Timer.Enabled = Countdown_Timer.Enabled;
            }
        }
        protected void itemsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Countdown_Timer.Enabled = !Countdown_Timer.Enabled;
            Label ID = itemsGrid.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            TextBox Description = itemsGrid.Rows[e.RowIndex].FindControl("Description") as TextBox;
            TextBox Time_Box = itemsGrid.Rows[e.RowIndex].FindControl("Time") as TextBox;
            TextBox Status = itemsGrid.Rows[e.RowIndex].FindControl("Status") as TextBox;
            try
            {
                DateTime Time_Date = new DateTime();
                try
                {
                    Time_Date = DateTime.ParseExact(Time_Box.Text.Trim(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    MessageBox.Text = ex.Source + "Please enter Due Date in {MM/dd/yyyy hh:mm:ss tt} format! ";
                }
                string sqlQuery = "UPDATE [Items] SET [Description] = @description, [Time] = @datevalue, [Status] = @status WHERE [ID] = @id";
                OleDbCommand cmd = new OleDbCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@description", Description.Text);
                cmd.Parameters.AddWithValue("@datevalue", Time_Date.Date);
                cmd.Parameters.AddWithValue("@status", Status.Text);
                cmd.Parameters.AddWithValue("@id", ID.Text);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                itemsGrid.EditIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Text = "Please enter all the values and try again! ";
            }
            finally
            {
                con.Close();
                loadList();
                Countdown_Timer.Enabled = Countdown_Timer.Enabled;
            }
        }

        protected void newRecord_Click(object sender, EventArgs e)
        {
            Countdown_Timer.Enabled = !Countdown_Timer.Enabled;
            NewRecord.Enabled = !NewRecord.Enabled;
            itemsGrid.ShowFooter = true;
            loadList();
        }
        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            ImageButton bt = (ImageButton)sender;
            GridViewRow grdRow = (GridViewRow)bt.Parent.Parent;

            try
            {
                TextBox ID = (TextBox)(grdRow.Cells[0].FindControl("tbID"));
                TextBox Description = (TextBox)(grdRow.Cells[0].FindControl("tbDescription"));
                TextBox Time = (TextBox)(grdRow.Cells[0].FindControl("tbTime"));
                DateTime Time_Date = DateTime.ParseExact(Time.Text, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                TextBox Status = (TextBox)(grdRow.Cells[0].FindControl("tbStatus"));
                string sqlQuery = "INSERT INTO [Items] VALUES(@ID, @Description, @Time, @Status)";
                OleDbCommand cmd = new OleDbCommand(sqlQuery, con);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@ID", ID.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", Description.Text.Trim());
                cmd.Parameters.AddWithValue("@Time", Time_Date.Date);
                cmd.Parameters.AddWithValue("@Status", Status.Text.Trim());
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Text = "Please enter all the values and try again! ";
            }
            finally
            {
                con.Close();
                itemsGrid.ShowFooter = false;
                NewRecord.Enabled = NewRecord.Enabled;
                loadList();
                if (!Countdown_Timer.Enabled)
                {
                    Countdown_Timer.Enabled = Countdown_Timer.Enabled;
                }
            }
        }
        protected void itemsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["Status"].ToString().Equals("Completed"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#A4F0B7");
                }
                else if (drv["Status"].ToString().Equals("In Progress"))
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
        protected void CustomValidator1_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            DateTime d;
            e.IsValid = DateTime.TryParseExact(e.Value, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
        }
        protected void Countdown_Timer_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(500);
            commandText = "TimerRefresh";
            loadList();
        }
        
    }
}