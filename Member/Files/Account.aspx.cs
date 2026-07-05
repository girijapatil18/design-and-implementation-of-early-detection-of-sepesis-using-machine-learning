using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace diseasePrediction.Member
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MemberId"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (!this.IsPostBack)

                    txtOldPassword.Focus();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tab = new DataTable();
                Class1 obj = new Class1();
                tab.Rows.Clear();

                tab = obj.GetMemberById(Session["MemberId"].ToString());
                string oldPassword = tab.Rows[0]["Password"].ToString();

                if (txtOldPassword.Text.Equals(oldPassword))
                {
                    obj.MemberUpdatePassword(txtNewPassword.Text, Session["MemberId"].ToString());
                    ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Member Password changed Successfully!!!')</script>");

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Member Old password Incorrect!!!')</script>");
                }
            }
            catch
            {

            }
        }
    }
}