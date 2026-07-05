using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace diseasePrediction.Admin
{
    public partial class NewUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminId"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                GetNewUsers();
            }
        }


        //function to load new members
        private void GetNewUsers()
        {
            try
            {
                DataTable tab = new DataTable();
                Class1 obj = new Class1();

                int serialNo = 1;

                //string _Tdate = DateTime.Now.ToShortDateString();

                DateTime currentDate;
                currentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                string _Tdate = currentDate.AddDays(1).ToString();

                tab = obj.GetNewMembers(currentDate, DateTime.Parse(_Tdate));

                if (tab.Rows.Count > 0)
                {
                    tableUsers.Rows.Clear();

                    tableUsers.BorderStyle = BorderStyle.Double;
                    tableUsers.GridLines = GridLines.Both;
                    tableUsers.BorderColor = System.Drawing.Color.DarkGray;

                    TableRow mainrow = new TableRow();
                    mainrow.Height = 30;
                    mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;

                    mainrow.BackColor = System.Drawing.Color.SteelBlue;

                    TableCell cell1 = new TableCell();
                    cell1.Text = "<b>SerialNo</b>";
                    mainrow.Controls.Add(cell1);

                    TableCell cell2 = new TableCell();
                    cell2.Text = "<b>Member Id</b>";
                    mainrow.Controls.Add(cell2);

                    TableCell cell3 = new TableCell();
                    cell3.Text = "<b>Name</b>";
                    mainrow.Controls.Add(cell3);

                    TableCell cell31 = new TableCell();
                    cell31.Text = "<b>Mobile</b>";
                    mainrow.Controls.Add(cell31);

                    TableCell cell4 = new TableCell();
                    cell4.Text = "<b>EmailId</b>";
                    mainrow.Controls.Add(cell4);

                    TableCell cell41 = new TableCell();
                    cell41.Text = "<b>Date</b>";
                    mainrow.Controls.Add(cell41);
                                       
                    TableCell cell6 = new TableCell();
                    cell6.Text = "<b>Delete</b>";
                    mainrow.Controls.Add(cell6);

                    tableUsers.Controls.Add(mainrow);

                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();

                        TableCell cellSerialNo = new TableCell();
                        cellSerialNo.Width = 50;
                        cellSerialNo.Text = serialNo + i + ".";
                        row.Controls.Add(cellSerialNo);

                        TableCell cellUserId = new TableCell();
                        cellUserId.Width = 150;
                        cellUserId.Text = tab.Rows[i]["MemberId"].ToString();
                        row.Controls.Add(cellUserId);

                        TableCell cellName = new TableCell();
                        cellName.Width = 150;
                        cellName.Text = tab.Rows[i]["Name"].ToString();
                        row.Controls.Add(cellName);

                        TableCell cellMobile = new TableCell();
                        cellMobile.Width = 200;
                        cellMobile.Text = tab.Rows[i]["Mobile"].ToString();
                        row.Controls.Add(cellMobile);

                        TableCell cellEmailId = new TableCell();
                        cellEmailId.Width = 200;
                        cellEmailId.Text = tab.Rows[i]["EmailId"].ToString();
                        row.Controls.Add(cellEmailId);

                        TableCell cellDate = new TableCell();
                        cellDate.Width = 200;
                        cellDate.Text = tab.Rows[i]["RegisteredDate"].ToString();
                        row.Controls.Add(cellDate);
                                               
                        TableCell cell_delete = new TableCell();
                        LinkButton lbtn_delete = new LinkButton();
                        lbtn_delete.ForeColor = System.Drawing.Color.Red;
                        lbtn_delete.Text = "Delete";

                        lbtn_delete.ID = tab.Rows[i]["MemberId"].ToString();
                        lbtn_delete.OnClientClick = "return confirm('Are you sure want to delete this User?')";
                        lbtn_delete.Click += new EventHandler(lbtn_delete_Click);
                        cell_delete.Controls.Add(lbtn_delete);
                        row.Controls.Add(cell_delete);

                        tableUsers.Controls.Add(row);
                    }
                }
                else
                {
                    tableUsers.Rows.Clear();

                    TableHeaderRow rno = new TableHeaderRow();
                    TableHeaderCell cellno = new TableHeaderCell();
                    cellno.ForeColor = System.Drawing.Color.Red;
                    cellno.Text = "No New Users Found!!!";

                    rno.Controls.Add(cellno);
                    tableUsers.Controls.Add(rno);
                }
            }
            catch
            {

            }
        }

        //event to delete member
        void lbtn_delete_Click(object sender, EventArgs e)
        {
            Class1 obj = new Class1();
            LinkButton lbtn = (LinkButton)sender;
           
            try
            {
                obj.DeleteMember(lbtn.ID);

                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Member Deleted Successfully!!!')</script>");
                GetNewUsers();

            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!!!')</script>");
            }
        }

    }
}