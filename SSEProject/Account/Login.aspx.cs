using System;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using SSEProject.Models;

namespace SSEProject.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            if(   ( (Email.Text == "gabrielle@etsu.edu") || (Email.Text == "zmjm40@etsu.edu") || (Email.Text == "samreen@etsu.edu")  )  && Password.Text == "1234" )
            {
                Type cstype = this.GetType();
                Response.BufferOutput = true;

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                {
                    String cstext = "alert('Nice Login!');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }

                //FormsAuthentication.SetAuthCookie(Email.Text, true, "HomePage.aspx");
                //FormsAuthentication.RedirectFromLoginPage(Email.Text, true);
                Response.Redirect("HomePage.aspx",true);

                return;
            }



            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            Trace.Write("Got it");
            
        }
    }
}