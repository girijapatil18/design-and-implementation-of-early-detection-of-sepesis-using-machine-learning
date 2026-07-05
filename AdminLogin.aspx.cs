using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace diseasePrediction
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserId.Focus();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Class1 obj = new Class1();

            if (obj.CheckAdminLogin(txtUserId.Text, txtPassword.Text))
            {
                Session["AdminId"] = txtUserId.Text;
                Response.Redirect("~/Admin/AdminHome.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "key", "<script>alert('Login Failed!!!')</script>");
            }
        }
    }
}