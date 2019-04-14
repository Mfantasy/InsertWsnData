using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsndata数据交互
{
    public class BaiCheng20190413
    {
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
                              decimal.Parse(spLine[1]);
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
                break;
            }
            string sql = "";
            string cSqlFmt = @"INSERT INTO time_number_entry (`key`,feed_id,`value`,`at`)
VALUES(unix_timestamp('{1}'),{0},{2},'{1}');"; //id,时间,值

            string midFmt = "(unix_timestamp('{1}'),{0},{2},'{1}'),";
            string sqlB = "INSERT INTO time_number_entry (`key`,feed_id,`value`,`at`) VALUES";
            StringBuilder mid = new StringBuilder();
            string sqlE = ";";
            var feedc = fmodels[0].Children[0];
            foreach (var item in feedc.datas)
            {
                string line = string.Format(midFmt,17867,item.Key,item.Value);
                mid.Append(line);
            }
            sql = sqlB + mid.ToString().Remove(mid.Length-1) + sqlE;
            Console.WriteLine(sql);
        }
    }

    public class Fmodel
    {
        public Feed Feed { get; set; }
        
        public List<Feed> Children { get; set; }

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
