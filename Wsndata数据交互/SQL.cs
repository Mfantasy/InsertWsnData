using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wsndata数据交互
{
    class SQL
    {
        //1420041600
        //1451577600
        //1483200000
        //1514736000
        //1546272000

        public List<IntEntry> GetTimeIntValue(string year, int fid)
        {
            string condition = " AND `key` > 1420041600 AND `key` < 1451577600";
            if (year == "2016") condition = " AND `key` > 1451577600 AND `key` < 1483200000";
            if (year == "2017") condition = " AND `key` > 1483200000 AND `key` < 1514736000";
            if (year == "2018") condition = " AND `key` > 1514736000 AND `key` < 1546272000";
            if (year == "2019") condition = " AND `key` > 1546272000";
            List<IntEntry> lie = new List<IntEntry>();
            string sql = "SELECT * FROM time_integer_entry WHERE feed_id = " + fid + condition; //+ " limit 1000";
            string conStr = StaticData.conStr;
            IDbConnection cn = new MySql.Data.MySqlClient.MySqlConnection(conStr);
            try
            {
                cn.Open();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandTimeout = 3000;
                cmd.CommandText = sql;
                IDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    IntEntry fm = new IntEntry();
                    fm.key = r["key"] is DBNull ? null : r["key"].ToString();
                    fm.value = r["value"] is DBNull ? 0 : Convert.ToInt32(r["value"]);
                    fm.id = r["id"] is DBNull ? 0 : Convert.ToInt32(r["id"]);
                    fm.feed_id = r["feed_id"] is DBNull ? -1 : Convert.ToInt32(r["feed_id"]);
                    lie.Add(fm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                if (cn != null)
                    cn.Close();
            }
            return lie;
        }

        public List<NumEntry> GetTimeNumValue(string year, int fid)
        {
            string condition = " AND `key` > 1420041600 AND `key` < 1451577600";
            if (year == "2016") condition = " AND `key` > 1451577600 AND `key` < 1483200000";
            if (year == "2017") condition = " AND `key` > 1483200000 AND `key` < 1514736000";
            if (year == "2018") condition = " AND `key` > 1514736000 AND `key` < 1546272000";
            if (year == "2019") condition = " AND `key` > 1546272000";
            List<NumEntry> lne = new List<NumEntry>();
            string sql = "SELECT * FROM time_number_entry WHERE feed_id = " + fid + condition;// + " limit 1000";
            string conStr = StaticData.conStr;
            IDbConnection cn = new MySql.Data.MySqlClient.MySqlConnection(conStr);
            try
            {
                cn.Open();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandTimeout = 3000;
                cmd.CommandText = sql;
                IDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    NumEntry fm = new NumEntry();
                    fm.key = r["key"] is DBNull ? null : r["key"].ToString();
                    fm.value = r["value"] is DBNull ? 0 : Convert.ToDecimal(r["value"]);
                    fm.id = r["id"] is DBNull ? 0 : Convert.ToInt32(r["id"]);
                    fm.feed_id = r["feed_id"] is DBNull ? -1 : Convert.ToInt32(r["feed_id"]);
                    lne.Add(fm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                if (cn != null)
                    cn.Close();
            }
            return lne;
        }

        public List<FeedModel> GetFeeds()
        {
            List<FeedModel> lfs = new List<FeedModel>();
            string sql = @"SELECT p2.p0t,p2.p1t,p2.title p2t,p.* from feed p INNER JOIN(
SELECT p1.p0t,p1.title p1t, p.*from feed p INNER JOIN(
(SELECT p0.title p0t, p.* FROM feed p INNER JOIN
(SELECT * FROM feed WHERE(creator_id = 50 or owner_id = 50) AND parent_id = 0 AND `status` != -1) p0
ON p.parent_id = p0.id)) p1
 ON p.parent_id = p1.id) p2
   ON p.parent_id = p2.id"; //白城
            //红光大道
            sql = @" SELECT p2.p0t,p2.p1t,p2.title p2t, p.*from feed p INNER JOIN(
SELECT p1.p0t, p1.title p1t, p.* from feed p INNER JOIN(
(SELECT p0.title p0t, p.* FROM feed p INNER JOIN
(SELECT * FROM feed WHERE(id = 17687) AND parent_id = 0 AND `status` != -1) p0
ON p.parent_id = p0.id)) p1
 ON p.parent_id = p1.id) p2
   ON p.parent_id = p2.id";

            string conStr = StaticData.conStr;
            IDbConnection cn = new MySql.Data.MySqlClient.MySqlConnection(conStr);
            try
            {
                cn.Open();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                IDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    FeedModel fm = new FeedModel();
                    fm.P0T = r["p0t"] is DBNull ? null : (string)r["p0t"];
                    fm.P1T = r["p1t"] is DBNull ? null : (string)r["p1t"];
                    fm.P2T = r["p2t"] is DBNull ? null : (string)r["p2t"];
                    fm.ID = r["id"] is DBNull ? -1 : Convert.ToInt32(r["id"]);
                    fm.Title = r["title"] is DBNull ? null : (string)r["title"];
                    fm.Value_Type = r["value_type"] is DBNull ? -1 : (int)r["value_type"];
                    fm.Created = r["created"] is DBNull ? DateTime.Now : DateTime.Parse(r["created"].ToString());
                    lfs.Add(fm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                if (cn != null)
                    cn.Close();
            }
            return lfs;
        }
    }

    class FeedModel
    {
        public string P0T { get; set; }
        public string P1T { get; set; }
        public string P2T { get; set; }

        public int ID { get; set; }

        public string Title { get; set; }

        public int Value_Type { get; set; }

        public DateTime Created { get; set; }
    }

    class BaseEntry
    {
        public string key { get; set; }
        public int id { get; set; }
        public int feed_id { get; set; }
        //public DateTime at { get; set; }
        public DateTime Time { get { return GetTime(key); } }

        //double ConvertDateTimeInt(System.DateTime time)
        //{
        //    //double intResult = 0;
        //    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
        //    double t = (time - startTime).TotalSeconds;
        //    //long t = (time.Ticks - startTime.Ticks) / 10000;            //除10000调整为13位
        //    return t;
        //}
        public DateTime GetTime(string timeStamp)
        {
            if (timeStamp.Contains("."))
            {
                timeStamp = timeStamp.Remove(timeStamp.IndexOf("."));
            }
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
    }

    class IntEntry : BaseEntry
    {
        public int value { get; set; }
    }

    class NumEntry : BaseEntry
    {
        public decimal value { get; set; }
    }

    public class SQLImport
    {
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
        public bool Sql(List<DataModel> dtList, string database)
        {
            bool result = false;
            string conStr = StaticData.conStr;
            IDbConnection cn = new MySql.Data.MySqlClient.MySqlConnection(conStr);
            //new System.Data.SqlClient.SqlConnection(conStr);
            cn.Open();
            IDbTransaction tran = cn.BeginTransaction();
            try
            {



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
                tran.Rollback();
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
}
