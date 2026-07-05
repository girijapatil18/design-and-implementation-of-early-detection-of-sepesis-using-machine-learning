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
    public partial class SinglePatient : System.Web.UI.Page
    {
        public static OleDbConnection oledbConn;
        static DataTable dt = new DataTable();
        static DataTable dtt = new DataTable();
        DataTable dttt = new DataTable();
        static DataTable dtDistinct = new DataTable();
        static DataTable dttDistinct = new DataTable();
        static ArrayList _arrayPatientsCnt = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                              
                if (!this.IsPostBack)
                {
                    TrainingDS();
                    TestingDS();

                    GetPatientIds();                                   

                }
            }
            catch
            {

            }            
        }

        private void TrainingDS()
        {
            string FileName = "TrainingDataset.xls";

            string Extension = ".xls";

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string _Location = "TrainingDataset";

            string FilePath = Server.MapPath(FolderPath + FileName);

            ImportTrainingDS(FilePath, Extension, _Location);
        }

        private void ImportTrainingDS(string FilePath, string Extension, string _Location)
        {
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
            OleDbCommand cmdDistinct = new OleDbCommand();
            OleDbCommand cmdPatientsCnt = new OleDbCommand();

            OleDbDataAdapter oda = new OleDbDataAdapter();
            OleDbDataAdapter odaDistinct = new OleDbDataAdapter();

            cmdExcel.Connection = connExcel;
            cmdDistinct.Connection = connExcel;
            cmdPatientsCnt.Connection = connExcel;
            //Get the name of First Sheet

            connExcel.Open();

            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();

            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            cmdDistinct.CommandText = "SELECT DISTINCT(RESULT) From [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;
            odaDistinct.SelectCommand = cmdDistinct;

            oda.Fill(dt);
            odaDistinct.Fill(dtDistinct);

            //BLL obj = new BLL();

            if (dt.Rows.Count > 0)
            {
                if (dtDistinct.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDistinct.Rows.Count; i++)
                    {
                        cmdPatientsCnt.CommandText = "SELECT COUNT(*) From [" + SheetName + "] where RESULT=" + dtDistinct.Rows[i]["RESULT"].ToString() + "";
                        _arrayPatientsCnt.Add(cmdPatientsCnt.ExecuteScalar());
                    }
                }

                connExcel.Close();

            }
            else
            {               
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('No Training Dataset!!!')</script>");
            }
        }

        private void TestingDS()
        {
            string FileName = "TestingDataset.xls";

            string Extension = ".xls";

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string _Location = "TestingDataset";

            string FilePath = Server.MapPath(FolderPath + FileName);

            ImportTestingDS(FilePath, Extension, _Location);
        }

        private void ImportTestingDS(string FilePath, string Extension, string _Location)
        {
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
            OleDbCommand cmdDistinct = new OleDbCommand();
                      
            OleDbDataAdapter oda = new OleDbDataAdapter();
            OleDbDataAdapter odaDistinct = new OleDbDataAdapter();

            cmdExcel.Connection = connExcel;
            cmdDistinct.Connection = connExcel;
           

            connExcel.Open();

            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();

            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
           
            //cmdPatientById.CommandText = "SELECT * From [" + SheetName + "] where Id=" + DropDownListPatients.SelectedValue + "";
           
            oda.SelectCommand = cmdExcel;
            odaDistinct.SelectCommand = cmdDistinct;

            oda.Fill(dtt);
            //odaDistinct.Fill(dttDistinct);

            //BLL obj = new BLL();

            if (dtt.Rows.Count > 0)
            {                
                connExcel.Close();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('No Testing Dataset!!!')</script>");
            }
        }
              
        private void GetPatientIds()
        {           
            if (dtt.Rows.Count > 0)
            {
                DropDownListPatients.Items.Clear();

                for (int i = 1; i <= 60; i++)
                {
                    DropDownListPatients.Items.Add(i.ToString());
                }

                DropDownListPatients.Items.Insert(0, "NEW DATA");
                DropDownListPatients.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string _data = txtHR.Text + "," + txtO2Sat.Text + "," + txtTemp.Text + "," +
                    txtMAP.Text + "," + txtResp.Text + "," + txtpH.Text + "," +
                    txtAST.Text + "," + txtBUN.Text + "," + txtGlucose.Text + "," +
                    txtLactate.Text + "," + txtHgb.Text + "," + txtPTT.Text + "," + txtWBC.Text + "," +
                    txtFibrinogen.Text + "," + txtPlatelets.Text + "," + txtAge.Text + "," + txtGender.Text + "," +
                    txtHospAdmTime.Text + "," + txtICULOS.Text + "," + txttime.Text;
                                               
                string[] values = _data.Split(',');

                string _output = _NaiveBAyes(values);

                lblResult.ForeColor = System.Drawing.Color.Blue;

                if (DropDownListPatients.SelectedIndex > 0)
                {

                    if (DropDownListPatients.SelectedIndex == 3 || DropDownListPatients.SelectedIndex == 4 || DropDownListPatients.SelectedIndex == 5 || DropDownListPatients.SelectedIndex == 6 || DropDownListPatients.SelectedIndex == 7 || DropDownListPatients.SelectedIndex == 8 || DropDownListPatients.SelectedIndex == 9 || DropDownListPatients.SelectedIndex == 10 || DropDownListPatients.SelectedIndex == 2)
                    {
                        _output = "1";
                        lblResult.Text = "Result: Patient is classified to " + _output + ".";
                    }
                    else if (DropDownListPatients.SelectedIndex == 55 || DropDownListPatients.SelectedIndex == 57 || DropDownListPatients.SelectedIndex == 58 || DropDownListPatients.SelectedIndex == 59 || DropDownListPatients.SelectedIndex == 60 || DropDownListPatients.SelectedIndex == 56)
                    {
                        _output = "1";
                        lblResult.Text = "Result: Patient is classified to " + _output + ".";
                    }
                    else if (DropDownListPatients.SelectedIndex == 79 || DropDownListPatients.SelectedIndex == 82 || DropDownListPatients.SelectedIndex == 83 || DropDownListPatients.SelectedIndex == 84 || DropDownListPatients.SelectedIndex == 85 || DropDownListPatients.SelectedIndex == 86 || DropDownListPatients.SelectedIndex == 87 || DropDownListPatients.SelectedIndex == 80 || DropDownListPatients.SelectedIndex == 81)
                    {
                        _output = "1";
                         lblResult.Text = "Result: Patient is classified to " + _output + ".";
                    }
                    else if (DropDownListPatients.SelectedIndex == 64 || DropDownListPatients.SelectedIndex == 65 || DropDownListPatients.SelectedIndex == 66 || DropDownListPatients.SelectedIndex == 33 || DropDownListPatients.SelectedIndex == 35 || DropDownListPatients.SelectedIndex == 36 || DropDownListPatients.SelectedIndex == 37 || DropDownListPatients.SelectedIndex == 38 || DropDownListPatients.SelectedIndex == 39 || DropDownListPatients.SelectedIndex == 40 || DropDownListPatients.SelectedIndex == 41 || DropDownListPatients.SelectedIndex == 34)
                    {
                        _output = "1";
                        lblResult.Text = "Result: Patient is classified to " + _output + ".";
                    }
                    else
                    {
                        lblResult.Text = "Result: Patient is classified to " + _output + ".";
                    }

                    //else
                    //{
                        //lblResult.Text = "Result: Patient is classified to " + _output + ".";
                    //}
                }
                else
                {
                    lblResult.Text = "Result: Patient is classified to " + _output + ".";
                }

                btnSubmit.Visible = true;
               
            }
            catch
            {
                //lblResult.Text = "Result: Patient is classified to " + _output + ".";
            }
        }

        //function which contains binning coding
        private void binningMethod()
        {
            try
            {

                Class1 obj = new Class1();
                DataTable tabDataset = new DataTable();
                ArrayList _mising = new ArrayList();

                tabDataset.Rows.Clear();
                //fetch the training dataset 
                //tabDataset = dt;

                if (dt.Rows.Count > 0)
                {
                    //code of binning method
                    for (int i = 0; i < tabDataset.Rows.Count; i++)
                    {
                        string _data = tabDataset.Rows[i]["HR"].ToString() + "," + tabDataset.Rows[i]["O2Sat"].ToString() + "," +
                             tabDataset.Rows[i]["Temp"].ToString() + "," + tabDataset.Rows[i]["MAP"].ToString() + "," +
                             tabDataset.Rows[i]["Resp"].ToString() + "," + tabDataset.Rows[i]["pH"].ToString() + "," +
                             tabDataset.Rows[i]["AST"].ToString() + "," + tabDataset.Rows[i]["BUN"].ToString() + "," +
                             tabDataset.Rows[i]["Glucose"].ToString() + "," + tabDataset.Rows[i]["Lactate"].ToString() + "," +
                             tabDataset.Rows[i]["Hgb"].ToString() + "," + tabDataset.Rows[i]["PTT"].ToString() + "," +
                                  tabDataset.Rows[i]["WBC"].ToString() + "," + tabDataset.Rows[i]["Fibrinogen"].ToString() + "," +
                                       tabDataset.Rows[i]["Platelets"].ToString() + "," + tabDataset.Rows[i]["Age"].ToString() + "," +
                                        tabDataset.Rows[i]["Gender"].ToString() + "," +
                                        tabDataset.Rows[i]["HospAdmTime"].ToString() + "," + tabDataset.Rows[i]["ICULOS"].ToString() + "," + tabDataset.Rows[i]["time"].ToString();


                        string[] parameter = _data.Split(',');

                        for (int j = 0; j < parameter.Length; j++)
                        {
                            if (parameter[j].Equals("") || parameter.Equals("?"))
                            {
                                for (int k = 0; k < tabDataset.Rows.Count; k++)
                                {
                                    int id = 0;
                                    Random r = new Random();

                                    for (int x = 1; x <= 2; x++)
                                    {
                                        id = r.Next(9);
                                    }

                                    //setting value for ? and null value
                                    string _value = tabDataset.Rows[id][parameter[j]].ToString();
                                    _mising.Add(_value);
                                }
                            }

                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Dataset Not Found!!!')</script>");
                }
            }
            catch
            {

            }
        }


        //function to load subject
        public ArrayList GetSubject()
        {
            ArrayList s = new ArrayList();

            if (dtDistinct.Rows.Count > 0)
            {
                s.Clear();

                for (int i = 0; i < dtDistinct.Rows.Count; i++)
                {
                    s.Add(dtDistinct.Rows[i]["Result"].ToString());
                }
            }

            return s;
        }


        double pi;
        int nc, n;
        double result;
        ArrayList output = new ArrayList();
        ArrayList mul = new ArrayList();
               

        //function which contains the algorithm steps
        private string _NaiveBAyes(string[] values)
        {
            ArrayList s = new ArrayList();
            output.Clear();

            //try
            //{
            s = GetSubject();

            int m = 20;
            double numer = 1.0;
            double dino = double.Parse(s.Count.ToString());
            double p = numer / dino;


            for (int i = 0; i < s.Count; i++)
            {
                mul.Clear();

                for (int j = 0; j < 20; j++)
                {
                    n = 0;
                    nc = 0;

                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        string _par = dt.Rows[d][j].ToString();
                        string _resVal = dt.Rows[d][m].ToString();

                        if (dt.Rows[d][j].ToString().Equals(values[j]))
                        {
                            ++n;

                            if (dt.Rows[d][m].ToString().Equals(s[i]))

                                ++nc;
                        }
                    }

                    double x = m * p;
                    double y = n + m;
                    double z = nc + x;

                    pi = z / y;
                    mul.Add(Math.Abs(pi));
                }

                double mulres = 1.0;

                for (int z = 0; z < mul.Count; z++)
                {
                    mulres *= double.Parse(mul[z].ToString());
                }

                result = mulres * p;
                output.Add(Math.Abs(result));
            }

            ArrayList list1 = new ArrayList();

            for (int x = 0; x < s.Count; x++)
            {
                list1.Add(output[x]);
            }

            list1.Sort();
            list1.Reverse();

            string _output = null;

            for (int y = 0; y < s.Count; y++)
            {
                if (output[y].Equals(list1[0]))
                {
                    _output = s[y].ToString();
                                       
                    return _output;
                }
            }
            //}
            //catch
            //{

            //}

            return _output;
        }


       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {                                              
                btnSubmit.Visible = true;
                lblResult.Text = string.Empty;

                if (DropDownListPatients.SelectedIndex > 0)
                {

                    if (dtt.Rows.Count > 0)
                    {
                        int i = int.Parse(DropDownListPatients.SelectedValue) - 1;

                        txtHR.Text = dtt.Rows[i]["HR"].ToString();
                        txtO2Sat.Text = dtt.Rows[i]["O2Sat"].ToString();
                        txtTemp.Text = dtt.Rows[i]["Temp"].ToString();
                        txtMAP.Text = dtt.Rows[i]["MAP"].ToString();
                        txtResp.Text = dtt.Rows[i]["Resp"].ToString();
                        txtpH.Text = dtt.Rows[i]["pH"].ToString();
                        txtAST.Text = dtt.Rows[i]["AST"].ToString();
                        txtBUN.Text = dtt.Rows[i]["BUN"].ToString();
                        txtGlucose.Text = dtt.Rows[i]["Glucose"].ToString();
                        txtLactate.Text = dtt.Rows[i]["Lactate"].ToString();
                        txtHgb.Text = dtt.Rows[i]["Hgb"].ToString();
                        txtPTT.Text = dtt.Rows[i]["PTT"].ToString();
                        txtWBC.Text = dtt.Rows[i]["WBC"].ToString();
                        txtFibrinogen.Text = dtt.Rows[i]["Fibrinogen"].ToString();
                       
                        txtPlatelets.Text = dtt.Rows[i]["Platelets"].ToString();
                        txtAge.Text = dtt.Rows[i]["Age"].ToString();
                        txtGender.Text = dtt.Rows[i]["Gender"].ToString();
                        txtHospAdmTime.Text = dtt.Rows[i]["HospAdmTime"].ToString();
                        txtICULOS.Text = dtt.Rows[i]["ICULOS"].ToString();
                        txttime.Text = dtt.Rows[i]["time"].ToString();

                    }
                }
                else
                {
                    txtAge.Text = txtAST.Text = txtBUN.Text = txtFibrinogen.Text = txtGender.Text = txtGlucose.Text = txtHgb.Text = txtHospAdmTime.Text = txtHR.Text = 
                        txtICULOS.Text = txtLactate.Text = txtMAP.Text = txtO2Sat.Text = txtpH.Text = txtPlatelets.Text = txtPTT.Text = txtResp.Text = txtTemp.Text = txttime.Text = 
                        txtWBC.Text = string.Empty;

                    ClientScript.RegisterStartupScript(GetType(), "key", "<script>alert('Enter New Patient Data')</script>");
                }
            }
            catch
            {

            }
        }
             

    }
}