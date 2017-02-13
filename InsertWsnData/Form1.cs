using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsertWsnData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //database 应为datatable
        private void button1_Click(object sender, EventArgs e)
        {
            //int min = Int32.Parse(MIN.Text);
            //int max = Int32.Parse(MAX.Text);
            DateTime timeStart = dateTimePicker1.Value;//DateTime.Parse(MIND.Text);
            DateTime timeEnd = dateTimePicker2.Value;//DateTime.Parse(MAXD.Text);
            int interval = Int32.Parse(IV.Text)*60;
            int feed_id = Int32.Parse(FI.Text);
            double value = double.Parse(textBox1.Text);
            List<DataModel> list = GetDataList1(feed_id, value, interval, timeStart, timeEnd);
            string database = "time_number_entry";
            bool OK = Sql(list, database);

            if (OK)
            {
                MessageBox.Show("OK OH YEAH!");
            }
            else
            {
                MessageBox.Show("oh no, sorry...");
            }

        }

        private List<DataModel> GetDataList1(int feed_id, double value, int interval, DateTime startTime, DateTime endTime)
        {
            List<DataModel> list = new List<DataModel>();
            double totalSec = (endTime - startTime).TotalSeconds;
            int count = (int)totalSec / interval;
                
            //List<int> newValues = WaveAlgo(values);
            for (int c = 0; c < count; c++)
            {
                DataModel dt = new DataModel(feed_id, startTime.AddSeconds(interval * c), value);
                list.Add(dt);
            }
            list.RemoveAt(0);
            return list;
        }

        private List<DataModel> GetDataList(int feed_id, double min, double max, int interval, DateTime startTime, DateTime endTime)
        {
            List<DataModel> list = new List<DataModel>();
            double totalSec = (endTime - startTime).TotalSeconds;
            int count = (int)totalSec / interval;
            double midValue = (max - min) / count;
            List<int> values = new List<int>();

            for (int i = 0; i < count; i++)
            {
                //DataModel dt = new DataModel(feed_id, startTime.AddSeconds(interval * i), min);
                values.Add((int)(midValue * i + min));
            }
            //List<int> newValues = WaveAlgo(values);
            for (int c = 0; c < count; c++)
            {
                DataModel dt = new DataModel(feed_id, startTime.AddSeconds(interval * c), values[c]);
                list.Add(dt);
            }
            list.RemoveAt(0);
            return list;
        }

        private List<int> WaveAlgo(List<int> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if (i % 5 == 0 || i % 7 == 0)
                {
                    values[i] = values[i] + 1;
                }
                else if (i % 3 == 0)
                {
                    values[i] = values[i] - 1;
                }
            }
            return values;
        }

        bool Sql(List<DataModel> dtList, string database)
        {
            bool result = false;
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["wsnkey"].ConnectionString;
            IDbConnection cn = new MySql.Data.MySqlClient.MySqlConnection(conStr);
                //new System.Data.SqlClient.SqlConnection(conStr);
            try
            {
                cn.Open();
                IDbTransaction tran = cn.BeginTransaction();

                foreach (DataModel item in dtList)
                {
                    IDbCommand cmd = cn.CreateCommand();
                    string cmdtxt = "INSERT INTO " + database + "(feed_id,value,`key`,at) VALUES(@feed_id,@value,@key,@at)";
                    //string cmdtxt = "INSERT INTO "+database+"() VALUES(@at)";
                    cmd.CommandText = cmdtxt;
                    cmd.Transaction = tran;
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("@feed_id", item.Feed_id));
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("@value", item.Value));
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("@at", item.at));
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("@key", item.key));
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();

                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);

                result = false;
            }
            finally
            {
                if (cn != null)
                    cn.Close();
            }
            return result;
        }

    }

    public class DataModel
    {
        public DataModel(int feed_id, DateTime time, double value)
        {
            this.show = time;
            this.feed_id = feed_id;
            this.value = value;
        }

        private double value;

        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private int feed_id;

        public int Feed_id
        {
            get { return feed_id; }
            set { feed_id = value; }
        }

        private DateTime show;

        public DateTime at
        {
            get { return show.AddHours(-8); }
        }

        public double key
        {
            get { return ConvertDateTimeInt(show); }
        }

        double ConvertDateTimeInt(System.DateTime time)
        {
            //double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            double t = (time - startTime).TotalSeconds;
            //long t = (time.Ticks - startTime.Ticks) / 10000;            //除10000调整为13位
            return t;
        }

    }
}