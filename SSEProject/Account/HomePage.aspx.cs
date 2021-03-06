﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
namespace SSEProject.Account
{
    public partial class HomePage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=sseproject1.database.windows.net;Initial Catalog=sseDB;Integrated Security=False;User ID=sseAdmin;Password=sse1234Roach;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        string commandText = "";
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
                    SqlCommand oconn = new SqlCommand("Select * From [Items]", con);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(oconn);
                    DataTable data = new DataTable();
                    da.Fill(data);
                   
                    itemsGrid.DataSource = data;
                    itemsGrid.DataBind();
                }
                foreach (GridViewRow grow in itemsGrid.Rows)
                {
                    Label Time = grow.FindControl("lbl_Time") as Label;
                    Label Status = grow.FindControl("lbl_Status") as Label;
                    try
                    {
                        if (Time.Text != null)
                        {
                            DateTime Time_Date = DateTime.ParseExact(Time.Text.Trim(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                            if (Status.Text != "Completed")
                            {
                                var diff = (Time_Date.Date) - (DateTime.Now);
                                Label lbl_Timer = grow.FindControl("lbl_Timer") as Label;
                                if (diff.TotalSeconds > 0)
                                {
                                    lbl_Timer.Text = string.Format("{0} d {1:D2}:{2:D2}:{3:D2}", diff.Days, diff.Hours, diff.Minutes, diff.Seconds);
                                }
                                else
                                {
                                    lbl_Timer.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAEAE");
                                    lbl_Timer.Text = "Crossed Deadline!!";
                                }
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
                MessageBox.Text = "";
            }
        }
        protected void itemsGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Countdown_Timer.Enabled = false;
            itemsGrid.EditIndex = e.NewEditIndex;
             loadList();
        }
        protected void itemsGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            itemsGrid.EditIndex = -1;
            loadList();
            Countdown_Timer.Enabled = false;
        }
        protected void itemsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Countdown_Timer.Enabled==true)
            {
                Countdown_Timer.Enabled = false;
            }
            Label ID = itemsGrid.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            TextBox Description = itemsGrid.Rows[e.RowIndex].FindControl("Description") as TextBox;
            TextBox Time_Box = itemsGrid.Rows[e.RowIndex].FindControl("Time") as TextBox;
            TextBox Status = itemsGrid.Rows[e.RowIndex].FindControl("Status") as TextBox;
            Label assignedTo = itemsGrid.Rows[e.RowIndex].FindControl("AssignedTo") as Label;
            try
            {
                DateTime Time_Date = new DateTime();
                try
                {
                    Time_Date = DateTime.ParseExact(Time_Box.Text.Trim(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                }
                catch (FormatException ex)
                {
                    MessageBox.Text = ex.Data+ "Please enter Due Date in {MM/dd/yyyy hh:mm:ss tt} format! ";
                   
                }
                string sqlQuery = "UPDATE [Items] SET [Description] = @description, [Time] = @datevalue, [Status] = @status, [AssignedTo]=@assignedTo WHERE [Id] = @id";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@description", Description.Text);
                cmd.Parameters.AddWithValue("@datevalue", Time_Date);
                cmd.Parameters.AddWithValue("@status", Status.Text);
                cmd.Parameters.AddWithValue("@AssignedTo", assignedTo.Text);
                cmd.Parameters.AddWithValue("@id", ID.Text);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                itemsGrid.EditIndex = -1;
                con.Close();
                loadList();
                Countdown_Timer.Enabled =true;
            }
            catch (Exception ex)
            {
                MessageBox.Text = ex.Data + "Please enter all the values and try again! ";
            }
            finally
            {
                MessageBox.Text = "";
            }
        }
        protected void DeleteRecord(string id)
        {
            SqlCommand com = new SqlCommand("delete from [Items] where Id=@ID", con);
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
                    if (selectRow.Checked)
                    {
                        Label id = grow.FindControl("lbl_ID") as Label;
                       if(Countdown_Timer.Enabled==true)
                        {
                            Countdown_Timer.Enabled = false;
                        }
                        DeleteRecord(id.Text);
                    }
                }
            }
            finally
            {
                con.Close();
                loadList();
                Countdown_Timer.Enabled =true;
            }
        }
        protected void buttonAssign_Click(object sender, EventArgs e)
        {
            if (Countdown_Timer.Enabled==true)
            {
                Countdown_Timer.Enabled = false;
            }
            List<Label> ids = new List<Label>();
              foreach (GridViewRow grow in itemsGrid.Rows)
                  {
                    CheckBox selectRow = (CheckBox)grow.FindControl("selectRow");
                    if (selectRow.Checked)
                     {
                   
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
                Response.Redirect("~/Account/Assign.aspx");
            }
            else
            {
                loadList();
                Countdown_Timer.Enabled = true;
            }
            MessageBox.Text = "";
        }
        
        
        protected void newRecord_Click(object sender, EventArgs e)
        {
            if (Countdown_Timer.Enabled==true)
            {
                Countdown_Timer.Enabled =false;
            }
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
                string sqlQuery = "INSERT INTO [Items] VALUES(@ID, @Description, @Time, @Status, @AssignedTo)";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@ID", ID.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", Description.Text.Trim());
                cmd.Parameters.AddWithValue("@Time", Time_Date.Date);
                cmd.Parameters.AddWithValue("@Status", Status.Text.Trim());
                cmd.Parameters.AddWithValue("@AssignedTo", " ");
                con.Open();
                cmd.ExecuteNonQuery();
                itemsGrid.ShowFooter = false;
                NewRecord.Enabled = NewRecord.Enabled;
                con.Close();
                loadList();
                Countdown_Timer.Enabled = true;
            
            }
            catch (Exception ex)
            {
                MessageBox.Text = ex.Data+ "Please enter all the values and try again! ";
               
            }
        }
        protected void itemsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["Status"].ToString().Equals("Completed"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#B0E57C");
                }
                else if (drv["Status"].ToString().Equals("In Progress"))
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

        protected void selectedIndexChanged(object sender, EventArgs e)
        {
            if (Countdown_Timer.Enabled==true)
            {
                Countdown_Timer.Enabled =false;
            }
        }
    }
}