using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace diseasePrediction.Member
{
    public partial class Comments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["MemberId"] == null)
                {
                    Session.Abandon();
                    Response.Redirect("../Guest/Login.aspx");
                }
                else
                {
                    GetAllTopicComments();
                }
            }
            catch
            {

            }
        }

        public void GetAllTopicComments()
        {
            DataTable tab = new DataTable();
            Class1 obj = new Class1();

            tab.Rows.Clear();
            tab = obj.GetTopicComments(int.Parse(Request.QueryString["topicID"].ToString()));

            int serialNo = 1;

            if (tab.Rows.Count > 0)
            {
                Table1.Rows.Clear();

                TableHeaderRow r1 = new TableHeaderRow();
                TableHeaderCell c1 = new TableHeaderCell();
                c1.ColumnSpan = 5;
                c1.ForeColor = System.Drawing.Color.SteelBlue;
                c1.Text = Request.QueryString["topicname"].ToString();
                r1.Controls.Add(c1);

                TableRow r2 = new TableRow();
                TableCell c2 = new TableCell();
                c2.Text = "<br/>";
                r2.Controls.Add(c2);

                Table1.Controls.Add(r1);
                Table1.Controls.Add(r2);

                for (int cnt = 0; cnt < tab.Rows.Count; cnt++)
                {
                    TableRow row1 = new TableRow();

                    TableCell row1_cell1 = new TableCell();
                    row1_cell1.Font.Size = 10;
                    row1_cell1.Text = cnt + serialNo + ".";
                    row1.Controls.Add(row1_cell1);

                    TableCell cell_comment = new TableCell();
                    cell_comment.Width = 750;
                    cell_comment.Text = tab.Rows[cnt]["Comment"].ToString();
                    row1.Controls.Add(cell_comment);

                    TableRow row3 = new TableRow();

                    TableCell row3cell1 = new TableCell();
                    row3cell1.Text = " ";
                    row3.Controls.Add(row3cell1);

                    DataTable tab50 = new DataTable();
                    tab50.Rows.Clear();

                    tab50 = obj.GetMemberById(tab.Rows[cnt]["MemberId"].ToString());
                    TableCell row3cell2 = new TableCell();
                    row3cell2.Text = "Posted By : " + tab50.Rows[0]["Name"].ToString() + " ," + "Posted Date : " + tab.Rows[cnt]["PostedDate"].ToString() + "<br/>";
                    row3.Controls.Add(row3cell2);

                    TableRow row10 = new TableRow();

                    TableCell row10_cell1 = new TableCell();
                    row10_cell1.ColumnSpan = 10;
                    row10_cell1.Width = 900;
                    row10_cell1.Text = "<hr/>";
                    row10.Controls.Add(row10_cell1);

                    Table1.Controls.Add(row1);
                    Table1.Controls.Add(row3);
                    Table1.Controls.Add(row10);

                }

            }
            else
            {
                Table1.Rows.Clear();

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.ColumnSpan = 5;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.Text = "No Comments Found";
                row.Controls.Add(cell);

                Table1.Controls.Add(row);

            }
        }

        protected void btn_comment_Click(object sender, EventArgs e)
        {
            Class1 obj = new Class1();

            try
            {
                obj.InsertComment(int.Parse(Request.QueryString["topicID"].ToString()), Session["MemberId"].ToString(), txt_comment.Text, DateTime.Now.ToShortDateString());
                txt_comment.Text = string.Empty;
                GetAllTopicComments();
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('New Comment added successfully')</script>");

            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('New Comment added successfully')</script>");
            }
        }

    }
}