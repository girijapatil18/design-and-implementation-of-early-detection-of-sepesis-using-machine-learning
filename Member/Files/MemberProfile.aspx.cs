using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace diseasePrediction.Member
{
    public partial class MemberProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetProfile();
            }
        }

        private void GetProfile()
        {
            Class1 obj = new Class1();
            DataTable tab = new DataTable();
            tab = obj.GetMemberById(Session["MemberId"].ToString());

            if(tab.Rows.Count>0)
            {
                lblMemberId.Font.Bold = true;
                lblMemberId.Text = "Member Id: " + tab.Rows[0]["MemberId"].ToString();

                txtName.Text = tab.Rows[0]["Name"].ToString();
                txtMobile.Text = tab.Rows[0]["Mobile"].ToString();
                txtEmailId.Text = tab.Rows[0]["EmailId"].ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Class1 obj = new Class1();

                obj.UpdateMember(txtName.Text, txtMobile.Text, txtEmailId.Text, Session["MemberId"].ToString());
                GetProfile();

                ClientScript.RegisterStartupScript(GetType(), "key", "<script>alert('Profile Updated Successfully')</script>");
            }
            catch
            {

            }
        }
    }
}