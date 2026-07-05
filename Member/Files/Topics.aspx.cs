using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace diseasePrediction.Member
{
    public partial class Topics : System.Web.UI.Page
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

            int serialNo = 1;

            if (tab.Rows.Count > 0)
            {
                Table1.Rows.Clear();

                for (int cnt = 0; cnt < tab.Rows.Count; cnt++)
                {

                    TableRow row1 = new TableRow();

                    TableCell row1_cell1 = new TableCell();
                    row1_cell1.Font.Size = 10;
                    row1_cell1.Text = cnt + serialNo + ".";
                    row1.Controls.Add(row1_cell1);

                    TableCell cell_topic = new TableCell();
                    cell_topic.Width = 750;
                    HyperLink li = new HyperLink();
                    li.ID = tab.Rows[cnt]["TopicId"].ToString();
                    li.Text = tab.Rows[cnt]["TopicName"].ToString();
                    li.NavigateUrl = string.Format("Comments.aspx?topicname={0}&topicID={1}", tab.Rows[cnt]["TopicName"].ToString(), tab.Rows[cnt]["TopicId"].ToString());
                    cell_topic.Controls.Add(li);
                    row1.Controls.Add(cell_topic);

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
                cell.Text = "No Topics Found";
                row.Controls.Add(cell);

                Table1.Controls.Add(row);

            }
        }

        protected void btn_topic_Click(object sender, EventArgs e)
        {
            Class1 obj = new Class1();

            try
            {
                obj.InsertTopic(Session["MemberId"].ToString(), txt_topic.Text, DateTime.Now.ToShortDateString());
                txt_topic.Text = string.Empty;
                GetAllTopics();
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('New Topic Posted Successfully')</script>");
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Server Error!')</script>");
            }
        }
    }
}