using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Collections;
using System.Threading;
using System.Configuration;

namespace diseasePrediction.Member
{
    public partial class _DTResults : System.Web.UI.Page
    {
        double _outcomeCntNB = 0;
        string _timeNB = null;
        int _ActualCnt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _Results();
            }
            catch
            {

            }
        }

        private void _Results()
        {
            Table1.Rows.Clear();

            Table1.BorderStyle = BorderStyle.Double;
            Table1.GridLines = GridLines.Both;
            Table1.BorderColor = System.Drawing.Color.Black;

            //mainrow
            TableRow mainrow = new TableRow();
            mainrow.Height = 30;
            mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;
            mainrow.BackColor = System.Drawing.Color.SteelBlue;

            TableCell cell1 = new TableCell();
            cell1.Width = 350;
            cell1.Text = "<b>Constraint</b>";
            mainrow.Controls.Add(cell1);

            TableCell cell2 = new TableCell();
            cell2.Width = 200;
            cell2.Text = "<b>DECISION TREE Algorithm</b>";
            mainrow.Controls.Add(cell2);

            Table1.Controls.Add(mainrow);

            CompareResults();

            //1st row
            TableRow row1 = new TableRow();

            TableCell cellAcc = new TableCell();
            cellAcc.Text = "<b>Accuracy</b>";
            row1.Controls.Add(cellAcc);

            TableCell cellAccLVQ = new TableCell();
            cellAccLVQ.Font.Bold = true;
            cellAccLVQ.ForeColor = System.Drawing.Color.DarkGreen;
            //cal
            double _percentageNB = (_outcomeCntNB / _ActualCnt) * 100;
            cellAccLVQ.Text = _percentageNB.ToString() + "%";
            Session["DT_Accuracy"] = null;
            Session["DT_Accuracy"] = _percentageNB;
            row1.Controls.Add(cellAccLVQ);

            Table1.Controls.Add(row1);

            //2nd row           
            TableRow row2 = new TableRow();

            TableCell cellTime = new TableCell();
            cellTime.Text = "<b>Efficiency (milli secs)</b>";
            row2.Controls.Add(cellTime);

            TableCell cellTimeLVQ = new TableCell();
            cellTimeLVQ.Font.Bold = true;
            cellTimeLVQ.ForeColor = System.Drawing.Color.DarkBlue;
            cellTimeLVQ.Text = _timeNB;
            Session["DT_Time"] = null;
            Session["DT_Time"] = cellTimeLVQ.Text;
            row2.Controls.Add(cellTimeLVQ);

            Table1.Controls.Add(row2);

            //3rd row           
            //TableRow row3 = new TableRow();

            //TableCell cellCorrect = new TableCell();
            //cellCorrect.Text = "<b>Precision</b>";
            //row3.Controls.Add(cellCorrect);

            //TableCell cellCorrectLVQ = new TableCell();
            //double _s1 = TP / (TP + FN);
            //cellCorrectLVQ.Text = _s1.ToString();
            //Session["NB_Precision"] = null;
            //Session["NB_Precision"] = cellCorrectLVQ.Text;
            //row3.Controls.Add(cellCorrectLVQ);

            //Table1.Controls.Add(row3);

            ////4th row           
            //TableRow row4 = new TableRow();

            //TableCell cellInCorrect = new TableCell();
            //cellInCorrect.Text = "<b>Recall</b>";
            //row4.Controls.Add(cellInCorrect);

            //TableCell cellInCorrectLVQ = new TableCell();
            //double _s2 = TN / (TN + FP);
            //cellInCorrectLVQ.Text = _s2.ToString();
            //Session["NB_Recall"] = null;
            //Session["NB_Recall"] = cellInCorrectLVQ.Text;
            //row4.Controls.Add(cellInCorrectLVQ);

            //Table1.Controls.Add(row4);
        }

        static double TP = 0, TN = 0, FN = 0, FP = 0;

        private void CompareResults()
        {
            TP = 0;
            TN = 0;
            FN = 0;
            FP = 0;

            string FileName = "ActualDataset.xls";

            string Extension = ".xls";

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string _Location = "ActualDataset";

            string FilePath = Server.MapPath(FolderPath + FileName);

            string conStr = "";

            switch (Extension)
            {

                case ".xls": //Excel 97-03

                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]

                             .ConnectionString;

                    break;

                case ".xlsx": //Excel 07

                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]

                              .ConnectionString;

                    break;

            }

            conStr = String.Format(conStr, FilePath, _Location);

            OleDbConnection connExcel = new OleDbConnection(conStr);

            OleDbCommand cmdExcel = new OleDbCommand();

            OleDbDataAdapter oda = new OleDbDataAdapter();

            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet

            connExcel.Open();

            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();

            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT *From [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;

            oda.Fill(dt);

            //BLL obj = new BLL();


            if (dt.Rows.Count > 0)
            {
                _ActualCnt = dt.Rows.Count;

                string[] _PResult = Session["DTOutput"].ToString().Split(',');

                _timeNB = _PResult[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (_PResult[i + 1].Equals("1"))
                    {
                        if (dt.Rows[i]["result"].ToString().Equals(_PResult[i + 1]))
                        {
                            ++_outcomeCntNB;
                            ++TP;
                        }
                        else
                        {
                            ++TN;
                        }
                    }
                    else
                    {
                        if (dt.Rows[i]["result"].ToString().Equals(_PResult[i + 1]))
                        {
                            ++_outcomeCntNB;
                            ++FN;
                        }
                        else
                        {
                            ++FP;
                        }
                    }
                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('No Actual Dataset Found!!!')</script>");
            }

            connExcel.Close();

        }

      
    }
}