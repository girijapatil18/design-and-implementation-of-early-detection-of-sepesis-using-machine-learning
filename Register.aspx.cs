using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

namespace diseasePrediction
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {                
                txtUserId.Focus();
            }
        }

        static string pwd = null;

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                Class1 obj = new Class1();

                if (obj.CheckMemberId(txtUserId.Text))
                {
                    obj.InsertMember(txtUserId.Text, txtPassword.Text, txtName.Text, txtMobile.Text, txtEmailId.Text, DateTime.Now);
                    ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('User Registration is Successful!!!')</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('UserId Already Exists!!!')</script>");
                }

                txtEmailId.Text = txtMobile.Text = txtName.Text = txtPassword.Text = txtUserId.Text = string.Empty;
            }
            catch
            {

            }
        }

        

        
    }
}