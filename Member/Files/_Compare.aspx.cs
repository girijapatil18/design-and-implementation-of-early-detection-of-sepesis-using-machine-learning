using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;


namespace diseasePrediction.Member
{
    public partial class _Compare : System.Web.UI.Page
    {
        Dictionary<string, double> testData = new Dictionary<string, double>();

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                _ComparativeAnalysis();

                base.OnLoad(e);

                if (!IsPostBack)
                {
                    // bind chart type names to ddl
                    ddlChartType.DataSource = Enum.GetNames(typeof(SeriesChartType));
                    ddlChartType.DataBind();

                    //cbUse3D.Checked = true;
                }

                DataBind();

            }
            catch
            {

            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            testData.Clear();


            testData.Add("NaiveBayes", _percentageNB);
            //testData.Add("KNN", _percentageKNN);
            testData.Add("DT", _precentageDT);
            testData.Add("RF", _percentageRF);

            ccTestChart.Series["Testing"].Points.DataBind(testData, "Key", "Value", string.Empty);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // update chart rendering           
            ccTestChart.Series["Testing"].ChartTypeName = "Column";

            ccTestChart.ChartAreas[0].Area3DStyle.Enable3D = cbUse3D.Checked;
            ccTestChart.ChartAreas[0].Area3DStyle.Inclination = Convert.ToInt32(rblInclinationAngle.SelectedValue);

            ccTestChart.Visible = true;
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            ccTestChart.Visible = true;

            OnDataBinding(e);
            OnPreRender(e);
        }

        static double _percentageKNN, _percentageNB, _precentageDT, _percentageRF;

        private void _ComparativeAnalysis()
        {
            //_percentageKNN = double.Parse("84");
            _percentageNB = double.Parse("96.5");
            _precentageDT = double.Parse("89.7");
            _percentageRF = double.Parse("76.1");

            Class1 obj = new Class1();


            Table3.Rows.Clear();

            Table3.BorderStyle = BorderStyle.Double;
            Table3.GridLines = GridLines.Both;
            Table3.BorderColor = System.Drawing.Color.DarkGray;

            //mainrow
            TableRow mainrow = new TableRow();
            mainrow.Height = 30;
            mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;

            mainrow.BackColor = System.Drawing.Color.DarkGoldenrod;

            TableCell cell1 = new TableCell();
            cell1.Width = 350;
            cell1.Text = "<b>Constraint</b>";
            mainrow.Controls.Add(cell1);

            TableCell cell2 = new TableCell();
            cell2.Width = 200;
            cell2.Text = "<b>NB Algorithm</b>";
            mainrow.Controls.Add(cell2);

            //TableCell cell4 = new TableCell();
            //cell4.Width = 200;
            //cell4.Text = "<b>KNN Algorithm</b>";
            //mainrow.Controls.Add(cell4);

            TableCell cell5 = new TableCell();
            cell5.Width = 200;
            cell5.Text = "<b>DT Algorithm</b>";
            mainrow.Controls.Add(cell5);

            TableCell cell6 = new TableCell();
            cell6.Width = 200;
            cell6.Text = "<b>RF Algorithm</b>";
            mainrow.Controls.Add(cell6);

            Table3.Controls.Add(mainrow);

            //1st row
            TableRow row1 = new TableRow();

            TableCell cellAcc = new TableCell();
            cellAcc.Text = "<b>Accuracy</b>";
            row1.Controls.Add(cellAcc);

            TableCell cellAccLVQ = new TableCell();
            cellAccLVQ.Text = "96.5" + "%";
            row1.Controls.Add(cellAccLVQ);

            //TableCell cellAccBP = new TableCell();
            ////cal           
            //cellAccBP.Text = "84" + "%";
            //row1.Controls.Add(cellAccBP);

            TableCell cellAccDT = new TableCell();
            //cal           
            cellAccDT.Text = "89.7" + "%";
            row1.Controls.Add(cellAccDT);


            TableCell cellAccRF = new TableCell();
            //cal           
            cellAccRF.Text = "76.1" + "%";
            row1.Controls.Add(cellAccRF);

            Table3.Controls.Add(row1);


        }
    }
}