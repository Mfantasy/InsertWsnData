using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 气象站数据补充
{
    public partial class Form1 : Form
    {
        const string numDatabase = "time_number_entry";
        const string intDatabase = "time_integer_entry";
        public Form1()
        {
            InitializeComponent();
        }

        //使用之前需要先在本地数据库进行测试
        //首先要统计几个气象站的数据缺失问题.

        //风速和风向...这个不太好搞啊...可以用随机种子.写一个固定算法,然后风速根据这个固定算法再进行调整
        void WindData(List<IntegerModel> wsList, List<IntegerModel> wdList)
        {


        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            
            DateTime timeStart = dateTimePicker1.Value;
            DateTime timeEnd = dateTimePicker2.Value;
            int interval = int.Parse(timeInterval.Text);
            //雨量
            int fidRain = int.Parse(fRain.Text);
            double rainValue = double.Parse(textBoxRain.Text);
            List<NumberModel> rainList = GetNumList(fidRain, rainValue, interval, timeStart, timeEnd);
            if (NumSql(rainList, numDatabase))
            {
                label9.Text += "雨量数据插入完毕\t";
            }
            else
            {
                label9.Text = "雨量数据插入失败";
                return;
            }
            //风速
            int fidWs = int.Parse(fWindSpeed.Text);
            int wsValue = int.Parse(textBoxWindSpeed.Text);
            List<IntegerModel> wsList = GetIntegerList(fidWs, wsValue, interval, timeStart, timeEnd);
          
            //风向
            int fidWd = int.Parse(fWindDirection.Text);
            int wdValue = int.Parse(textBoxWindDirection.Text);
            List<IntegerModel> wdList = GetIntegerList(fidWd, wdValue, interval, timeStart, timeEnd);

            if (IntegerSql(wsList, intDatabase))
            {
                label9.Text += "风速数据插入完毕";
            }
            else
            {
                label9.Text = "风速数据插入失败";
                return;
            }
            if (IntegerSql(wdList, intDatabase))
            {
                label9.Text += "风向数据插入完毕";
            }
            else
            {
                label9.Text = "风向数据插入失败";
                return;
            }
            //空气温度
            int fidTe = int.Parse(fAirTemp .Text);
            double teValue = double.Parse(textBoxAirTemp.Text);
            List<NumberModel> teList = GetNumList(fidTe, teValue, interval, timeStart, timeEnd);
            if (NumSql(teList, numDatabase))
            {
                label9.Text += "空气温度数据插入完毕\t";
            }
            else
            {
                label9.Text = "空气温度数据插入失败";
                return;
            }
            //雨量
            int fidHu = int.Parse(fAirHumid.Text);
            double huValue = double.Parse(textBoxAirHumid.Text);
            List<NumberModel> huList = GetNumList(fidHu, huValue, interval, timeStart, timeEnd);
            if (NumSql(huList, numDatabase))
            {
                label9.Text += "空气湿度数据插入完毕\t";
            }
            else
            {
                label9.Text = "空气湿度数据插入失败";
                return;
            }

        }


        private List<NumberModel> GetNumList(int feed_id, double value, int interval, DateTime startTime, DateTime endTime)
        {
            List<NumberModel> list = new List<NumberModel>();
            double totalSec = (endTime - startTime).TotalSeconds;
            int count = (int)totalSec / interval;

            //List<int> newValues = WaveAlgo(values);
            for (int c = 0; c < count; c++)
            {
                NumberModel dt = new NumberModel(feed_id, startTime.AddSeconds(interval * c), value);
                list.Add(dt);
            }
            list.RemoveAt(0);
            return list;
        }

        List<IntegerModel> GetIntegerList(int feed_id, int value, int interval, DateTime startTime, DateTime endTime)
        {
            List<IntegerModel> list = new List<IntegerModel>();
            double totalSec = (endTime - startTime).TotalSeconds;
            int count = (int)totalSec / interval;

            //List<int> newValues = WaveAlgo(values);
            for (int c = 0; c < count; c++)
            {
                IntegerModel dt = new IntegerModel(feed_id, startTime.AddSeconds(interval * c), value);
                list.Add(dt);
            }
            list.RemoveAt(0);
            return list;
        }

        bool IntegerSql(List<IntegerModel> dtList, string database)
        {
            bool result = false;
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["wsnkey"].ConnectionString;
            IDbConnection cn = new MySql.Data.MySqlClient.MySqlConnection(conStr);
            //new System.Data.SqlClient.SqlConnection(conStr);
            try
            {
                cn.Open();
                IDbTransaction tran = cn.BeginTransaction();

                foreach (IntegerModel item in dtList)
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

        bool NumSql(List<NumberModel> dtList, string database)
        {
            bool result = false;
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["wsnkey"].ConnectionString;
            IDbConnection cn = new MySql.Data.MySqlClient.MySqlConnection(conStr);
            //new System.Data.SqlClient.SqlConnection(conStr);
            try
            {
                cn.Open();
                IDbTransaction tran = cn.BeginTransaction();

                foreach (NumberModel item in dtList)
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



    public class IntegerModel
    {
        public IntegerModel(int feed_id, DateTime time, int value)
        {
            this.show = time;
            this.feed_id = feed_id;
            this.value = value;
        }

        private int value;

        public int Value
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

    public class NumberModel
    {
        public NumberModel(int feed_id, DateTime time, double value)
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
