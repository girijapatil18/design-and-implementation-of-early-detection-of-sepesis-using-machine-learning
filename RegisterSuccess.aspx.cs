using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace diseasePrediction
{
    public partial class RegisterSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)

                txtOTP.Focus();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                Class1 obj = new Class1();

                if (obj.CheckMemberId(Request.QueryString["MemberId"]))
                {
                    if (Request.QueryString["OTP"].Equals(txtOTP.Text))
                    {
                        obj.InsertMember(Request.QueryString["MemberId"], Request.QueryString["Password"], Request.QueryString["Name"], Request.QueryString["Mobile"], Request.QueryString["EmailId"], DateTime.Now);

                        txtOTP.Text = string.Empty;

                        ClientScript.RegisterStartupScript(GetType(), "key", "<script>alert('Registered Successfully!!!')</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "key", "<script>alert('OTP Incorrect!!!')</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "key", "<script>alert('MemberId Exists!!!')</script>");
                }
            }
            catch
            {

            }
        }
    }
}