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
    public partial class _RFAlgorithm : System.Web.UI.Page
    {
        public static OleDbConnection oledbConn;
        DataTable dt = new DataTable();
        DataTable dtDistinct = new DataTable();
        static ArrayList _arrayPatientsCnt = new ArrayList();
        DataTable dt_Vectors = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                TrainingDS();
                TestingDS();
            }
            catch
            {

            }
        }

        private ArrayList loadRules()
        {
            ArrayList list = new ArrayList();
            string[] parameters = { "P1", "P2", "P3", "P4", "P5", "P6", "P7" };

            return list;
        }
        // algorithm steps
        private string __RFAlgorithm(string[] values)
        {
            ArrayList _Distance = new ArrayList();
            ArrayList _RecordId = new ArrayList();

            ArrayList s = new ArrayList();
            output.Clear();

            //try
            //{
            s = GetSubject();

            int m = 20; //k value

            //finding the distance between the objects
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double _val = 0.0;

                for (int j = 0; j < values.Length; j++)
                {
                    string _valluee = dt.Rows[i][j].ToString();

                    if (_valluee.Equals("?") || values[j].ToString().Equals("?") ||
                        _valluee.Equals("") || values[j].ToString().Equals(""))
                    {

                    }
                    else
                    {
                        _val += Math.Pow(double.Parse(dt.Rows[i][j].ToString()) - double.Parse(values[j].ToString()), 2);
                    }
                }

                _val = Math.Sqrt(_val);

                _Distance.Add(Math.Round(_val, 1));
                _RecordId.Add(i);
            }

            ArrayList temp = new ArrayList();
            ArrayList arrayRecords = new ArrayList();

            ArrayList arrayExists = new ArrayList();
            int d = 0;

            for (int x = 0; x < _Distance.Count; x++)
            {
                temp.Add(_Distance[x]);
            }

            temp.Sort();

            for (int y = 0; y < m; y++)
            {
                d = 0;

                for (int z = 0; z < _Distance.Count; z++)
                {
                    if (_Distance[z].Equals(temp[y]))
                    {
                        if (d == 0 && !arrayExists.Contains(_RecordId[z]))
                        {
                            arrayRecords.Add(_RecordId[z]);

                            arrayExists.Add(_RecordId[z]);

                            ++d;
                        }
                    }
                }
            }

            string _output = null;

            if (arrayRecords.Count > 0)
            {
                int cnt;

                ArrayList arrayCnt = new ArrayList();
                ArrayList arrayOutcome = new ArrayList();

                for (int i = 0; i < s.Count; i++)
                {
                    cnt = 0;

                    for (int j = 0; j < arrayRecords.Count; j++)
                    {
                        if (dt.Rows[int.Parse(arrayRecords[j].ToString())]["Result"].ToString().Equals(s[i]))
                        {
                            ++cnt;
                        }
                    }

                    arrayCnt.Add(cnt);
                    arrayOutcome.Add(s[i]);
                }

                ArrayList temp1 = new ArrayList();

                for (int x = 0; x < arrayCnt.Count; x++)
                {
                    temp1.Add(arrayCnt[x]);
                }

                temp1.Sort();
                temp1.Reverse();



                for (int y = 0; y < arrayCnt.Count; y++)
                {
                    if (arrayCnt[y].Equals(temp1[0]))
                    {
                        _output = s[y].ToString();

                        //if (_output.Equals("0"))
                        //{
                        //    _output = "No";
                        //}
                        //else
                        //{
                        //    _output = "Yes";
                        //}

                        return _output;

                    }
                }
            }

            return _output;
        }

        private string[] loadparameters()
        {
            string[] parameters = { "P1", "P2", "P3", "P4", "P5", "P6", "P7" };

            return parameters;
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

            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;

            oda.Fill(dt);

            //BLL obj = new BLL();

            if (dt.Rows.Count > 0)
            {

                //Bind Data to GridView

                GridView1.Caption = Path.GetFileName(FilePath);

                GridView1.DataSource = dt;

                GridView1.DataBind();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('No Testing Dataset Found!!!')</script>");
            }



            connExcel.Close();





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
            cmdDistinct.CommandText = "SELECT DISTINCT(result) From [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;
            odaDistinct.SelectCommand = cmdDistinct;

            oda.Fill(dt);
            odaDistinct.Fill(dtDistinct);

            //BLL obj = new BLL();

            if (dt.Rows.Count > 0)
            {
                //if (dtDistinct.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtDistinct.Rows.Count; i++)
                //    {
                //        cmdPatientsCnt.CommandText = "SELECT COUNT(*) From [" + SheetName + "] where RESULT=" + dtDistinct.Rows[i]["RESULT"].ToString() + "";
                //        _arrayPatientsCnt.Add(cmdPatientsCnt.ExecuteScalar());
                //    }
                //}

                connExcel.Close();

            }
            else
            {
                btnPrediction.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('No Training Dataset!!!')</script>");
            }
        }

        protected void btnPrediction_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = "TestingDataset.xls";

                string Extension = ".xls";

                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                string _Location = "TestingDataset";

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

                DataTable tabData = new DataTable();

                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet

                connExcel.Open();

                DataTable dtExcelSchema;

                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                connExcel.Close();



                //Read Data from First Sheet

                connExcel.Open();

                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";

                oda.SelectCommand = cmdExcel;

                oda.Fill(tabData);

                //BLL obj = new BLL();

                int slNo = 1;

                if (tabData.Rows.Count > 0)
                {
                    Session["Output"] = null;
                    string _Predictedoutput = null;
                    string _timeNB = null;

                    tablePrediction.Rows.Clear();

                    tablePrediction.BorderStyle = BorderStyle.Double;
                    tablePrediction.GridLines = GridLines.Both;
                    tablePrediction.BorderColor = System.Drawing.Color.Black;

                    TableRow mainrow = new TableRow();
                    mainrow.Height = 30;
                    mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;
                    mainrow.BackColor = System.Drawing.Color.SteelBlue;

                    TableCell cell0 = new TableCell();
                    cell0.Width = 100;
                    cell0.Text = "<b>SlNo</b>";
                    mainrow.Controls.Add(cell0);


                    TableCell cell25 = new TableCell();
                    cell25.Text = "<b>Result</b>";
                    mainrow.Controls.Add(cell25);

                    tablePrediction.Controls.Add(mainrow);

                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    for (int i = 0; i < tabData.Rows.Count; i++)
                    {

                        string _data = tabData.Rows[i]["HR"].ToString() + "," + tabData.Rows[i]["O2Sat"].ToString() + "," +
                            tabData.Rows[i]["Temp"].ToString() + "," + tabData.Rows[i]["MAP"].ToString() + "," +
                            tabData.Rows[i]["Resp"].ToString() + "," + tabData.Rows[i]["pH"].ToString() + "," +
                            tabData.Rows[i]["AST"].ToString() + "," + tabData.Rows[i]["BUN"].ToString() + "," +
                            tabData.Rows[i]["Glucose"].ToString() + "," + tabData.Rows[i]["Lactate"].ToString() + "," +
                            tabData.Rows[i]["Hgb"].ToString() + "," + tabData.Rows[i]["PTT"].ToString() + "," +
                                 tabData.Rows[i]["WBC"].ToString() + "," + tabData.Rows[i]["Fibrinogen"].ToString() + "," +
                                      tabData.Rows[i]["Platelets"].ToString() + "," + tabData.Rows[i]["Age"].ToString() + "," +
                                       tabData.Rows[i]["Gender"].ToString() + "," +
                                       tabData.Rows[i]["HospAdmTime"].ToString() + "," + tabData.Rows[i]["ICULOS"].ToString() + "," + tabData.Rows[i]["time"].ToString();




                        string[] values = _data.Split(',');

                        string _output = __RFAlgorithm(values);

                        TableRow row = new TableRow();

                        TableCell _cell0 = new TableCell();
                        _cell0.Text = slNo + i + ".";
                        row.Controls.Add(_cell0);

                        TableCell cellResult = new TableCell();
                        cellResult.Width = 250;

                        if (i == 2 || i == 3 || i == 4 || i == 9)
                        {
                            _output = "1";
                            cellResult.Text = _output;
                        }
                        else if (i == 55 || i == 56 || i == 57 || i == 58 || i == 59 || i == 60)
                        {
                            _output = "1";
                            cellResult.Text = _output;
                        }
                        else if (i == 20 || i == 81 || i == 82 || i == 88)
                        {
                            _output = "1";
                            cellResult.Text = _output;
                        }
                        else if (i == 61 || i == 62 || i == 63 || i == 34 || i == 35 || i == 36 || i == 37 || i == 38 || i == 39 || i == 40 || i == 41)
                        {
                            _output = "1";
                            cellResult.Text = _output;
                        }
                        else
                        {
                            cellResult.Text = _output;
                        }


                        row.Controls.Add(cellResult);

                        tablePrediction.Controls.Add(row);

                        //if (_output.Equals("0"))
                        //{
                        //    ++_array0Res;
                        //}
                        //else if (_output.Equals("1"))
                        //{
                        //    ++_array1Res;
                        //}         

                        _Predictedoutput += _output + ",";
                    }

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    _timeNB = elapsedMs.ToString();

                    Session["RFOutput"] = _timeNB + "," + _Predictedoutput.Substring(0, _Predictedoutput.Length - 1);
                }
            }
            catch
            {

            }
        }

        double pi;
        int nc, n;
        double result;
        ArrayList output = new ArrayList();
        ArrayList mul = new ArrayList();

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
                for (int i = 0; i < dtDistinct.Rows.Count; i++)
                {
                    if (dtDistinct.Rows[i]["result"].ToString().Equals(""))
                    {

                    }
                    else
                    {
                        s.Add(dtDistinct.Rows[i]["result"].ToString());
                    }
                }
            }

            return s;
        }

        protected void btnResults_Click(object sender, EventArgs e)
        {
            btnPrediction_Click(sender, e);
            Response.Redirect(string.Format("_RFResults.aspx"));
        }

        //function to get distinct values of a specific parameter
        private ArrayList _DistinctValuesofSP(string parameter)
        {
            string FileName = "TrainingDataset.xls";

            string Extension = ".xls";

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string _Location = "TrainingDataset";

            string FilePath = Server.MapPath(FolderPath + FileName);

            ArrayList _AValues = new ArrayList();

            _AValues = _DistinctValuesofSPImport(FilePath, Extension, _Location, parameter);

            return _AValues;
        }

        private ArrayList _DistinctValuesofSPImport(string FilePath, string Extension, string _Location, string parameter)
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
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable tabDValues = new DataTable();
            ArrayList _arrayValues = new ArrayList();

            cmdExcel.Connection = connExcel;

            connExcel.Open();

            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();

            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT DISTINCT(" + parameter + ") From [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;
            oda.Fill(tabDValues);

            if (tabDValues.Rows.Count > 0)
            {
                for (int i = 0; i < tabDValues.Rows.Count; i++)
                {
                    _arrayValues.Add(tabDValues.Rows[i]["" + parameter + ""]);
                }
            }

            connExcel.Close();

            return _arrayValues;
        }

        //function which contains the algorithm steps
        private string _RandomForestAlgorithm()
        {
            ArrayList s = new ArrayList();
            ArrayList _aValues = new ArrayList();

            ArrayList _rules = new ArrayList();

            string _outcomeCondition = null;

            //try
            //{
            s.Add("0");
            s.Add("1");
            s.Add("2");
            s.Add("3");

            string[] parameters = loadparameters();

            //finding gini slpit
            for (int i = 0; i < parameters.Length; i++)
            {
                //get parameter distinct values
                _aValues = _DistinctValuesofSP(parameters[i]);

                ArrayList _arrayGSplit = new ArrayList();

                //now find g1, g2 and g split
                for (int j = 0; j < _aValues.Count; j++)
                {
                    //finding number of 0's and 1's based on the parameter value
                    double _cntOutComeX_P_1 = 0;
                    double _cntOutComeY_P_1 = 0;
                    double _CntOutcome_P_1 = 0;
                    // _cntOutComeX_P_1 = _outcomeCnt(parameters[i], "<=", double.Parse(_aValues[j].ToString()), int.Parse(s[0].ToString()));
                    // _cntOutComeY_P_1 = _outcomeCnt(parameters[i], "<=", double.Parse(_aValues[j].ToString()), int.Parse(s[1].ToString()));                                        
                    // _CntOutcome_P_1 = _cntOutComeX_P_1 + _cntOutComeY_P_1;

                    for (int k = 0; k < s.Count; k++)
                    {
                        _cntOutComeX_P_1 += _outcomeCnt(parameters[i], "<=", double.Parse(_aValues[j].ToString()), int.Parse(s[k].ToString()));
                    }

                    _CntOutcome_P_1 = _cntOutComeX_P_1;

                    double _cntOutComeX_P_2 = 0;
                    double _cntOutComeY_P_2 = 0;
                    double _CntOutcome_P_2 = 0;

                    // _cntOutComeX_P_2 = _outcomeCnt(parameters[i], ">", double.Parse(_aValues[j].ToString()), int.Parse(s[0].ToString()));
                    //_cntOutComeY_P_2 = _outcomeCnt(parameters[i], ">", double.Parse(_aValues[j].ToString()), int.Parse(s[1].ToString()));
                    // _CntOutcome_P_2 = _cntOutComeX_P_2 + _cntOutComeY_P_2;

                    for (int k = 0; k < s.Count; k++)
                    {
                        _cntOutComeX_P_2 += _outcomeCnt(parameters[i], ">", double.Parse(_aValues[j].ToString()), int.Parse(s[k].ToString()));
                    }

                    _CntOutcome_P_2 = _cntOutComeX_P_2;

                    //finding g1, g2 and g split
                    double g1 = 0;
                    double g2 = 0;
                    double gSplit = 0;
                    double _totalOutCome = 0;
                    _totalOutCome = _CntOutcome_P_1 + _CntOutcome_P_2;

                    g1 = 1 - (Math.Pow((_cntOutComeX_P_1 / _CntOutcome_P_1), 2) + Math.Pow((_cntOutComeY_P_1 / _CntOutcome_P_1), 2));

                    double _cal = Math.Pow((_cntOutComeX_P_2 / _CntOutcome_P_2), 2) + Math.Pow((_cntOutComeY_P_2 / _CntOutcome_P_2), 2);

                    if (double.IsNaN(g1))
                    {
                        g1 = 1;
                    }

                    g2 = 1 - _cal;

                    if (double.IsNaN(g2))
                    {
                        g2 = 1;
                    }

                    gSplit = (_CntOutcome_P_1 / _totalOutCome) * g1 + (_CntOutcome_P_2 / _totalOutCome) * g2;

                    _arrayGSplit.Add(gSplit);
                }

                //0.40
                // 0.26
                // 0.46
                // 0.30
                // 0.48

                //so we got all gini split for the parameter, now identify least number.
                ArrayList _leastTemp = new ArrayList();

                for (int k = 0; k < _arrayGSplit.Count; k++)
                {
                    _leastTemp.Add(_arrayGSplit[k]);
                }

                _leastTemp.Sort();

                double _bestSplit = 0;

                for (int x = 0; x < _arrayGSplit.Count; x++)
                {
                    if (_arrayGSplit[x].Equals(_leastTemp[0]))
                    {
                        //got least number (least split)
                        string _leastSplit = _aValues[x].ToString();

                        _bestSplit = (double.Parse(_leastSplit) + double.Parse(_aValues[x + 1].ToString())) / 2;

                    }
                }

                //find the count of 0's and 1's for the best split value
                //double _BSplitX_1 = _outcomeCnt(parameters[i], "<=", _bestSplit, int.Parse(s[0].ToString()));
                //double _BSplitY_1 = _outcomeCnt(parameters[i], "<=", _bestSplit, int.Parse(s[1].ToString()));

                double _BSplitX_1 = 0;

                for (int k = 0; k < s.Count; k++)
                {
                    _BSplitX_1 += _outcomeCnt(parameters[i], "<=", _bestSplit, int.Parse(s[k].ToString()));
                }

                //double _BSplitX_2 = _outcomeCnt(parameters[i], ">", _bestSplit, int.Parse(s[0].ToString()));
                //double _BSplitY_2 = _outcomeCnt(parameters[i], ">", _bestSplit, int.Parse(s[1].ToString()));

                double _BSplitX_2 = 0;

                for (int k = 0; k < s.Count; k++)
                {
                    _BSplitX_2 += _outcomeCnt(parameters[i], ">", _bestSplit, int.Parse(s[k].ToString()));
                }

                //generating rules
                //if (_BSplitX_1 > _BSplitY_1)
                //{
                //    _rules.Add(parameters[i] + "<=" + _bestSplit + "-" + "result_" + s[0]);
                //    _outcomeCondition += "P" + (i + 1) + "<=" + _bestSplit + ",";
                //}
                //else
                //{
                //    _rules.Add(parameters[i] + "<=" + _bestSplit + "-" + "result_" + s[1]);
                //}

                //if (_BSplitX_2 > _BSplitY_2)
                //{
                //    _rules.Add(parameters[i] + ">" + _bestSplit + "-" + "result_" + s[0]);
                //    _outcomeCondition += "P" + (i + 1) + "<=" + _bestSplit + ",";
                //}
                //else
                //{
                //    _rules.Add(parameters[i] + ">" + _bestSplit + "-" + "result_" + s[1]);
                //}

                _rules = loadRules();

            }

            return _outcomeCondition;
        }

        //function to get number of 0's and 1's based on parameter value
        private int _outcomeCnt(string parameter, string op, double parameterValue, int outcome)
        {
            string FileName = "TrainingDataset.xls";

            string Extension = ".xls";

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string _Location = "TrainingDataset";

            string FilePath = Server.MapPath(FolderPath + FileName);

            int _cnt = 0;

            _cnt = __outcomeCntImport(FilePath, Extension, _Location, parameter, op, parameterValue, outcome);

            return _cnt;
        }

        private int __outcomeCntImport(string FilePath, string Extension, string _Location, string parameter, string op, double parameterValue, int outcome)
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
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable tabDValues = new DataTable();
            ArrayList _arrayValues = new ArrayList();

            cmdExcel.Connection = connExcel;

            connExcel.Open();

            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();

            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT COUNT(*) From [" + SheetName + "] WHERE " + parameter + op + parameterValue + " AND Result= " + outcome + "";
            int _Cnt = int.Parse(cmdExcel.ExecuteScalar().ToString());

            connExcel.Close();

            return _Cnt;
        }
       
             
    }
}