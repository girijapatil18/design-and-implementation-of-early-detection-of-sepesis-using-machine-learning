using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace diseasePrediction.Admin
{
    public partial class Topics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["AdminId"] == null)
                {
                    Session.Abandon();
                    Response.Redirect("../Guest/Login.aspx");
                }
                else
                {
                    GetAllTopics();
                }
            }
            catch
            {

            }
        }

        public void GetAllTopics()
        {
            DataTable tab = new DataTable();
            Class1 obj = new Class1();

            tab.Rows.Clear();
            tab = obj.GetAllTopics();

            if (tab.Rows.Count > 0)
            {
                Table1.Rows.Clear();

                for (int cnt = 0; cnt < tab.Rows.Count; cnt++)
                {
                    TableRow row1 = new TableRow();

                    TableCell row1cell1 = new TableCell();
                    row1cell1.Text = " ";
                    row1.Controls.Add(row1cell1);

                    TableCell row1cell2 = new TableCell();
                    HyperLink link = new HyperLink();
                    link.Text = tab.Rows[cnt]["TopicName"].ToString();
                    link.ID = "Link~" + tab.Rows[cnt]["TopicId"].ToString();
                    string url = string.Format("../Admin/Comments.aspx?topicname={0}&topicID={1}", tab.Rows[cnt]["TopicName"].ToString(), tab.Rows[cnt]["TopicId"].ToString());
                    link.NavigateUrl = url;
                    row1cell2.Controls.Add(link);
                    row1.Controls.Add(row1cell2);

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

                    TableCell row10_cell0 = new TableCell();
                    row10_cell0.Text = " ";
                    row10.Controls.Add(row10_cell0);

                    TableCell row10_cell1 = new TableCell();
                    row10_cell1.HorizontalAlign = HorizontalAlign.Right;


                    Button btn_delete = new Button();
                    btn_delete.ID = "Delete~" + tab.Rows[cnt]["TopicId"].ToString();
                    btn_delete.Text = "Delete";
                    btn_delete.OnClientClick = "return confirm('Are your sure want to delete ? ')";
                    btn_delete.Click += new EventHandler(btn_delete_Click);
                    row10_cell1.Controls.Add(btn_delete);
                    row10.Controls.Add(row10_cell1);

                    TableRow row4 = new TableRow();

                    TableCell row4cell1 = new TableCell();
                    row4cell1.Text = " ";
                    row4.Controls.Add(row4cell1);

                    TableCell row4cell2 = new TableCell();
                    row4cell2.Width = 900;
                    row4cell2.Text = "<hr/>";
                    row4.Controls.Add(row4cell2);

                    Table1.Controls.Add(row1);
                    Table1.Controls.Add(row3);
                    Table1.Controls.Add(row10);
                    Table1.Controls.Add(row4);

                }

            }
            else
            {
                Table1.Rows.Clear();

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.ColumnSpan = 5;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.Text = "No Topics Found";
                row.Controls.Add(cell);

                Table1.Controls.Add(row);

            }
        }

        void btn_delete_Click(object sender, EventArgs e)
        {
            Class1 obj = new Class1();
            Button btn = (Button)sender;

            string[] s = btn.ID.ToString().Split('~');

            try
            {
                obj.DeleteTopicComments(int.Parse(s[1].ToString()));
                obj.DeleteTopic(int.Parse(s[1].ToString()));
                GetAllTopics();
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Topic and TopicComments Deleted Successfully')</script>");
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('!Server Error')</script>");
            }
        }

    }
}