using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 工作台
{
    public class BaiCheng20190413
    {
        public static void ChuLiQiXiangZhanHeTuRang()
        {
            string prefix = @"C:\Users\Administrator\Desktop\SMS\";
           // prefix = @"C:\Users\Administrator\Desktop\baicheng\";
            string dPqxz =prefix + @"气象土壤\气象站\";
            string dirP = prefix + @"气象土壤\土壤\";
            //首先依然是创建Fmdel
            List<Fmodel> fmodels = new List<Fmodel>();
            int fidBegin = 18013;
            //先处理气象站
            Dictionary<DateTime, decimal> qxzData = new Dictionary<DateTime, decimal>();
            string f1 = prefix + @"气象土壤\雨量2017.csv";
            string f2 = prefix + @"气象土壤\雨量2018.csv";
            Action<string> actQxz = (file) => {
                string[] lines = File.ReadAllLines(file);
                bool s = true;
                foreach (string line in lines)
                {
                    if (s) { s = false; continue; } //跳过第一行
                    string[] spLine = line.Split(',');
                    DateTime dt = DateTime.Parse(spLine[0]);
                    decimal v = 0;
                    if (!string.IsNullOrWhiteSpace(spLine[1]))
                        v = decimal.Parse(spLine[1]);
                    if (qxzData.ContainsKey(dt)) continue; //跳过重复数据
                    qxzData.Add(dt, v);
                }
            };
            actQxz(f1); actQxz(f2);

            string[] dirs = Directory.GetDirectories(dPqxz);
            foreach (var item in dirs)
            {
                Fmodel fm = new Fmodel();
                fm.Feed.Pid = 0;
                fm.Feed.Id = fidBegin++;                
                fm.Feed.Name = item.Replace(dPqxz,"");                
                Feed fd = new Feed() { Id = fidBegin++, Name = "雨量", Pid = fm.Feed.Id, datas = qxzData};
                fm.Children.Add(fd);
                fmodels.Add(fm);                
            }

            //再处理土壤           
            string[] dirts = Directory.GetDirectories(dirP);
            foreach (var item in dirts)
            {
                Fmodel fm = new Fmodel();
                string name = item.Replace(dirP, "");                
                //如果有了,找出来
                if (fmodels.Exists(f => f.Feed.Name == name))
                {
                    fm = fmodels.Find(f => f.Feed.Name == name);
                }
                else
                {
                    fm.Feed = new Feed() { Id = fidBegin++, FileFullName = item, Name = name, Pid = 0 };
                    fmodels.Add(fm);
                }
                string[] files = Directory.GetFiles(item);
                foreach (var f in files)
                {
                    Feed fd = new Feed() { Id = fidBegin++, FileFullName = f, Name = f.Replace(item + "\\", "").Replace(".csv", ""), Pid = fm.Feed.Id };
                    fm.Children.Add(fd);
                }
            }

            Action<Feed, string> setAllData = (feed, fname) =>
            {                                
                string[] lines = File.ReadAllLines(fname);
                bool s = true;
                foreach (string line in lines)
                {
                    if (s) { s = false; continue; } //跳过第一行
                    string[] spLine = line.Split(',');
                    DateTime dt = DateTime.Parse(spLine[0]);
                    decimal v = 0;
                    if (!string.IsNullOrWhiteSpace(spLine[1]))
                        v = decimal.Parse(spLine[1]);
                    if (feed.datas.ContainsKey(dt)) continue; //跳过重复数据
                    feed.datas.Add(dt, v);
                }
            };
            //填土壤数据
            foreach (var item in fmodels)
            {
                foreach (var c in item.Children)
                {
                    if (!string.IsNullOrWhiteSpace(c.FileFullName))
                    {
                        setAllData(c,c.FileFullName);
                    }
                }
            }

            //Insert(fmodels);
       
            Console.WriteLine("OK");

        }

        public static void ChuangJianFeed()
        {
            //增加一个父feed
            /*
    INSERT INTO feed(
    id, parent_id,`name`, key_type, value_type, updated, creator_id,`status`, access, owner_id, host_id
    ) VALUES(
    17867, 0, 'f17865c7', 0, 0, now(), 78, 0, 0, 78, 0);
    */
            //增加一个子feed
            /*
    INSERT INTO feed(
    id, parent_id,`name`, key_type, value_type, updated, creator_id,`status`, access, owner_id, host_id
    ) VALUES(
    17867, 17865, 'f17865c7', 1, 2, now(), 78, 0, 0, 78, 0);
    */
          
            //            VALUES
            //(100, 'Name 1', 'Value 1', 'Other 1'),
            //(101, 'Name 2', 'Value 2', 'Other 2'),
            //C:\Users\Administrator\Desktop\SMS\大合集_67668修改\大合集\2017

            List<Fmodel> fmodels = new List<Fmodel>();
         
            //id,name,pid
            int fidBegin = 17868;            
            string dP17 = @"C:\Users\Administrator\Desktop\SMS\大合集_67668修改\大合集\2017\";            
            string dP18 = @"C:\Users\Administrator\Desktop\SMS\大合集_67668修改\大合集\2018\";
           dP17 = @"C:\Users\Administrator\Desktop\baicheng\新建文件夹\大合集\2017\";
           dP18 = @"C:\Users\Administrator\Desktop\baicheng\新建文件夹\大合集\2018\";
            Action<string> act = (dirP) =>
            {
                string[] dirs = Directory.GetDirectories(dirP);
                foreach (var item in dirs)
                {
                    if (item.Contains("气象") || item.Contains("土壤")) continue; //气象和土壤先跳过
                    Fmodel fm = new Fmodel();                    
                    fm.Feed = new Feed() { Id = fidBegin, FileFullName = item, Name = item.Replace(dirP, ""),Pid = 0 };
                    fm.Children = new List<Feed>();
                    //如果有了,找出来,赋值另一个文件路径FileFullName2
                    if (fmodels.Exists(f => f.Feed.Name == fm.Feed.Name))
                    {
                        Fmodel fm2 = fmodels.Find(f => f.Feed.Name == fm.Feed.Name);
                        string[] file2s = Directory.GetFiles(item);
                        foreach (var f in file2s)
                        {
                            string name = f.Replace(item + "\\", "").Replace(".csv", "");
                            Feed fc = fm2.Children.Find(c => c.Name == name);
                            if (fc != null)
                            {
                                fc.FileFullName2 = f;
                            }
                        }
                        continue;
                    }
                    fmodels.Add(fm);
                    fidBegin++;
                    string[] files = Directory.GetFiles(item);
                    foreach (var f in files)
                    {
                        Feed fd = new Feed() { Id = fidBegin, FileFullName = f, Name = f.Replace(item + "\\", "").Replace(".csv", ""), Pid=fm.Feed.Id };
                        fm.Children.Add(fd);
                        fidBegin++;
                    }
                }
            };
            act(dP17); act(dP18);


            //时间无需做出特殊处理.
            //开始导入数据
            //获取文件记录的所有数据
            Action<Feed, List<string>> setAllData = (feed, dfs) =>
              {
                  foreach (string fname in dfs)
                  {
                      string[] lines = File.ReadAllLines(fname);
                      bool s = true;
                      foreach (string line in lines)
                      {
                          if (s) { s = false;continue; } //跳过第一行
                          string[] spLine = line.Split(',');
                          DateTime dt = DateTime.Parse(spLine[0]);
                          decimal v = 0;
                          if (!string.IsNullOrWhiteSpace(spLine[1]))
                              v = decimal.Parse(spLine[1]);
                          if (feed.datas.ContainsKey(dt)) continue; //跳过重复数据
                          feed.datas.Add(dt, v);
                      }
                      
                  }
              };
 
            foreach (var item in fmodels)
            {   
                foreach (var c in item.Children)
                {
                    if (string.IsNullOrWhiteSpace(c.FileFullName2))
                    {
                        setAllData(c, new List<string> { c.FileFullName });
                    }
                    else
                    {
                        setAllData(c, new List<string> { c.FileFullName,c.FileFullName2 });
                    }
                }                
            }
            Insert(fmodels);

            Console.WriteLine("AllOver");
        }

        public static void Insert(List<Fmodel> fmodels)
        {
            //string cSqlFmt = @"INSERT INTO time_number_entry (`key`,feed_id,`value`,`at`)VALUES({3},{0},{2},'{1}');"; 
            string midFmt = @"({3},{0},{2},'{1}'),
";//id,时间,值,key
            string sqlB = "INSERT INTO time_number_entry (`key`,feed_id,`value`,`at`) VALUES";

            foreach (var fm in fmodels)
            {
                foreach (var feedc in fm.Children)
                {
                    string sql = "";
                    StringBuilder mid = new StringBuilder();
                    foreach (var item in feedc.datas)
                    {
                        string line = string.Format(midFmt, feedc.Id, item.Key, item.Value, (item.Key.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
                        mid.Append(line);
                    }
                    sql = sqlB + mid.ToString();
                    sql = sql.Remove(sql.Length - 3) + ";";

                    int count = Insert(sql);
                    Console.WriteLine("[{0} {1}] 数量 {2}", fm.Feed.Name, feedc.Name, count);
                    Thread.Sleep(1000);
                }
            }
        }

        static int Insert(string sql)
        {
            int retval = 0;
            string conStr =StaticData.conStr;
            IDbConnection cn = new MySql.Data.MySqlClient.MySqlConnection(conStr);
            IDbCommand cmd = cn.CreateCommand();
            cmd.CommandTimeout = 300;
            cmd.CommandText = sql;
            cn.Open();
            retval = cmd.ExecuteNonQuery();
            cn.Close();
            return retval;
        }
    }

    public class Fmodel
    {
        public Feed Feed { get; set; } = new Feed();

        public List<Feed> Children { get; set; } = new List<Feed>();

        public override string ToString()
        {
            return Feed.Name;
        }
    }

    public class Feed
    {
        public int Id { get; set; }

        public int Pid { get; set; }

        public string Name { get; set; }

        public string FileFullName { get; set; }

        public string FileFullName2 { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public Dictionary<DateTime, decimal> datas { get; set; } = new Dictionary<DateTime, decimal>();

        public KeyValuePair<DateTime, decimal> LastValue => datas.Last();
        
    }

    
}




/*
 * 创建feed
    string sqlP = @"INSERT INTO feed(
    id, parent_id,`name`, key_type, value_type, updated, creator_id,`status`, access, owner_id, host_id
    ) VALUES(
    {0}, {2}, '{1}', 0, 0, NOW(), 78, 0, 0, 78, 0);
";
            string sqlC = @"INSERT INTO feed(
    id, parent_id,`name`, key_type, value_type, updated, creator_id,`status`, access, owner_id, host_id
    ) VALUES(
    {0}, {2}, '{1}', 1, 2, '{3}', 78, 0, 0, 78, 0);
";
            string sql = "";
            foreach (var item in fmodels)
            {
                sql += string.Format(sqlP, item.Feed.Id, item.Feed.Name, item.Feed.Pid);
                foreach (var citem in item.Children)
                {
                    sql += string.Format(sqlC, citem.Id, citem.Name, citem.Pid,citem.LastValue.Key.AddHours(-8));
                }
            }
            Console.WriteLine(sql);

 * 获取指定文件的最后一条数据
            //Action<Feed,string> setLast = (feed,fname) => {
            //    string[] lines = File.ReadAllLines(fname);
            //    string lastLine = lines.Last();
            //    string[] spLine = lastLine.Split(',');
            //    DateTime dt = DateTime.Parse(spLine[0]);
            //    decimal v = 0;
            //    if(!string.IsNullOrWhiteSpace(spLine[1]))
            //        decimal.Parse(spLine[1]);
            //    feed.LastValue.Add(dt,v);
            //};

 * 测试将数据放入current表中
            string sql = "";
            string cSqlFmt = @"INSERT INTO current_time_number (`key`,feed_id,`value`,`at`)
VALUES(unix_timestamp('{1}'),{0},{2},'{1}');";

            foreach (var item in fmodels)
            {
                foreach (var f in item.Children)
                {
                    string insertCSql = string.Format(cSqlFmt, f.Id, f.LastValue.Key, f.LastValue.Value);
                    sql += insertCSql;
                }
            }
            Console.WriteLine(sql);

 * 
 * 
 * 
 * 
 */
