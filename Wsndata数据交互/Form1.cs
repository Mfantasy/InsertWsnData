using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Wsndata数据交互.SQLImport;

namespace Wsndata数据交互
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			//ProcessCsv.ImportWeatherStationData(2018);
			return;
            new ProcessCsv().Work();
            Console.WriteLine(null + "123");
            DateTime dt = new DateTime(2015, 1, 1);
            string k15 = ConvertDateTimeInt(dt).ToString();
            string k16 = ConvertDateTimeInt(dt.AddYears(1)).ToString();
            string k17 = ConvertDateTimeInt(dt.AddYears(2)).ToString();
            string k18 = ConvertDateTimeInt(dt.AddYears(3)).ToString();
            string k19 = ConvertDateTimeInt(dt.AddYears(4)).ToString();
            Console.WriteLine(k15);
            Console.WriteLine(k16);
            Console.WriteLine(k17);
            Console.WriteLine(k18);
            Console.WriteLine(k19);
            DataColumn col = new DataColumn("123");
            col.Caption = "AV";
            DataTable dtt = new DataTable();
            dtt.Columns.Add(col);
            DataRow r = dtt.NewRow(); r["123"] = "as";
            dtt.Rows.Add(r);
            dataGridView1.DataSource = dtt;
            
        }
        double ConvertDateTimeInt(System.DateTime time)
        {
            //double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            double t = (time - startTime).TotalSeconds;
            //long t = (time.Ticks - startTime.Ticks) / 10000;            //除10000调整为13位
            return t;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ExportData(textBox1.Text);

            MessageBox.Show("~搞定~");
            //ImportData();
        }

        void ImportData()
        {
            FileParse();
        }

        void FileParse()
        {
            string file1 = @"中国铁塔门前0701至0731表.csv";
            string file2 = @"中国铁塔门前0801至0831表.csv";
            string file3 = @"中国铁塔门前 0901至0930表.csv";
            List<DataModel> dms = new List<DataModel>();
            List<DataModel> ports = new List<DataModel>();
            dms.AddRange(ReadFile(file1, ports));
            dms.AddRange(ReadFile(file2, ports));
            //dms.AddRange(ReadFile(file3, ports));

            SQLImport si = new SQLImport();
            si.Sql(dms, "time_number_entry");
            si.Sql(ports, "time_integer_entry");
            MessageBox.Show("OK");
        }

        List<DataModel> ReadFile(string file, List<DataModel> ports)
        {
            int j = 10;
            List<DataModel> dms = new List<DataModel>();
            string[] lines = File.ReadAllLines(file, Encoding.Default);
            for (int i = 1; i < lines.Length; i++)
            {
                //中国铁塔门前,0.520 m/s,13.000 ℃,0.200 m/s,0.330 m,2018/7/1 0:00:51
                string[] strs = lines[i].Split(',');
                //DateTime dt = DateTime.Parse(strs[5]+":"+j++);
                //if (j > 55) j = 10;
                DateTime dt = DateTime.Parse(strs[5]);
                double ls = double.Parse(strs[1].Replace("m/s", ""));
                double wd = double.Parse(strs[2].Replace("℃", ""));
                double ll = double.Parse(strs[3].Replace("m/s", ""));
                double yw = double.Parse(strs[4].Replace("m", ""));
                DataModel dls = new DataModel(11465, dt, ls);
                DataModel dwd = new DataModel(11466, dt, wd);
                DataModel dll = new DataModel(11467, dt, ll);
                DataModel dyw = new DataModel(11468, dt, yw);
                DataModel dport = new DataModel(11464, dt, 1);
                ports.Add(dport);
                dms.Add(dls); dms.Add(dwd); dms.Add(dll); dms.Add(dyw);
            }

            return dms;
        }
        //中国铁塔门前		天健井下雨水流量	17126	17125	port	采集通道		1	1	2018-06-17 04:18:00	2018-06-22 02:50:57	50	0															0					0	0
        //中国铁塔门前		天健井下雨水流量	17127	17125	speed	流速(m/s)		1	2	2018-06-17 04:18:00	2018-06-22 02:50:57	50	0															0					0	0
        //中国铁塔门前		天健井下雨水流量	17128	17125	temp	温度(C)		1	2	2018-06-17 04:18:00	2018-06-22 02:50:57	50	0															0					0	0
        //中国铁塔门前		天健井下雨水流量	17129	17125	flow	流量(m3/s)		1	2	2018-06-17 04:18:00	2018-06-22 02:50:57	50	0															0					0	0
        //中国铁塔门前		天健井下雨水流量	17130	17125	level	液位(m)       1	2	2018-06-17 04:18:00	2018-06-22 02:50:57	50	0															0					0	0

        //胜利西路中国铁塔股份有限公司 天健井下雨水流量	11464	11463	port 采集通道		1	1	2017-09-12 01:09:01	2018-06-16 22:23:21	2	0															0					50	0
        //胜利西路中国铁塔股份有限公司 天健井下雨水流量	11465	11463	speed 流速(m/s)     1	2	2017-09-12 01:09:01	2018-06-16 22:23:21	2	0															0					50	0
        //胜利西路中国铁塔股份有限公司 天健井下雨水流量	11466	11463	temp 温度(C)       1	2	2017-09-12 01:09:01	2018-06-16 22:23:21	2	0															0					50	0
        //胜利西路中国铁塔股份有限公司 天健井下雨水流量	11467	11463	flow 流量(m3/s)        1	2	2017-09-12 01:09:01	2018-06-16 22:23:21	2	0															0					50	0
        //胜利西路中国铁塔股份有限公司 天健井下雨水流量	11468	11463	level 液位(m)       1	2	2017-09-12 01:09:01	2018-06-16 22:23:21	2	0															0					50	0

        void BianShuJu()
        {
            //创造数据表~ 首先需要一个时间序列~ 以00:00:00 为起始
            // 2018年8月到2019年2月底 
            //列名: 时间, 液位(m) 12 左右

        }

        void ExportData(string year)
        {

            string s1p2t = "天健井下雨水流量";
            string s2p2t = "井下液位T型";
            string s3p2t = "井下水质PH值";
            string s4p2t = "井下水质TSS值";
            string s5p2t = "井下雨水流量T型";

            SQL s = new SQL();
            var feeds = s.GetFeeds();
            //首先按p0t分组. 导出时p0t即为文件夹名称            

            //组里按p1t + p2t分组

            //按组创建数据列  判断value_type 1 int 2 num, 取出各个id的数据集.通过key关联起来.形成数据行. title1,title2,time

            var p0g = feeds.GroupBy(o => o.P0T);
            foreach (var p0gi in p0g)
            {
                var groups = p0gi.GroupBy(o => o.P1T + o.P2T); //所有设备

                foreach (var g in groups)
                {
                    //g.key 设备名
                    //if (!(g.Key.Contains(s1p2t) || g.Key.Contains(s2p2t) || g.Key.Contains(s3p2t) || g.Key.Contains(s4p2t) || g.Key.Contains(s5p2t)))
                    //{
                    //    continue; //筛选设备
                    //}
                    //if(g)
                    DataTable dt = new DataTable(g.Key);
                    DataColumn coltime = new DataColumn("时间",typeof(DateTime));
                    dt.Columns.Add(coltime);
                    DateTime mtime = DateTime.MaxValue;
                    if (g.Count() > 0) mtime = g.ElementAt(0).Created;
                    if (mtime.Year > int.Parse(year)) continue;
                    
                    foreach (var f in g)//f>设备的指标
                    {
                        if (dt.Columns.Contains(f.Title)) continue;
                        if (f.Title.Contains("采集通道"))
                        {
                            DataColumn column = new DataColumn(f.Title);
                            
                            dt.Columns.Add(column);
                        }
                        else
                        {
                            DataColumn column = new DataColumn(f.ID.ToString());
                            column.Caption = f.Title;
                            dt.Columns.Add(column);
                        }
                        if (f.Value_Type == 1)
                        {
                            var ints = s.GetTimeIntValue(year, f.ID);
                            for (int i = 0; i < ints.Count; i++)
                            {
                                if (dt.Rows.Count <= i)
                                {
                                    DataRow r = dt.NewRow();
                                    dt.Rows.Add(r);
                                    r["时间"] = ints[i].Time;
                                }
                                if (f.Title.Contains("采集通道"))
                                {
                                    dt.Rows[i][f.Title] = ints[i].value.ToString();
                                }
                                else
                                {
                                    dt.Rows[i][f.ID.ToString()] = ints[i].value.ToString();
                                }
                            }
                        }
                        else
                        {
                            var nums = s.GetTimeNumValue(year, f.ID);
                            for (int i = 0; i < nums.Count; i++)
                            {
                                if (dt.Rows.Count <= i)
                                {
                                    DataRow r = dt.NewRow();
                                    dt.Rows.Add(r);
                                    r["时间"] = nums[i].Time;
                                }
                                dt.Rows[i][f.ID.ToString()] = nums[i].value.ToString("F2");
                            }
                        }
                    }
                    //补数据
                    dt = BuShuJu(mtime, dt,year);
                    

                    //做表格
                    TableToFile(year + "/" + p0gi.Key, dt);

                }
        

            }
        }

        DataTable BuShuJu(DateTime mtime, DataTable dt,string year)
        {
            DateTime etime = new DateTime(int.Parse(year)+1, 1, 1);
            if (year == "2018")
                etime = new DateTime(2018, 12, 31);
            else
                etime = new DateTime(2019,2,27);
            List<DateTime> times = new List<DateTime>();
            foreach (DataRow item in dt.Rows)
            {
                try
                {
                    times.Add((DateTime)item["时间"]);
                    //times.Add(DateTime.Parse(item["时间"].ToString()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(item["时间"].ToString());
                }                
            }
      
            while (mtime < etime)
            {
                mtime = mtime.AddDays(1);
                if (mtime.ToString("yyyyMMdd") == "20180101") continue;
                if (!times.Exists(t => t.ToString("yyyyMMdd") == mtime.ToString("yyyyMMdd")))
                {                   
                    BuYiTian(dt, mtime.ToString("yyyyMMdd"));
                    
                }
            }

            //排序
            dt.DefaultView.Sort = "时间 ASC";
            return dt.DefaultView.ToTable();
        }

        //补了286条数据
        void BuYiTian(DataTable dt, string date)
        {
            //date.ToString("yyyyMMdd)
            Random r = new Random();
            DateTime d = DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            for (int i = 0; i < 286; i++)
            {
                d = d.AddMinutes(5);
                BuYiTiao(dt, d,r);
            }
        }

        void BuYiTiao(DataTable dt, DateTime sj,Random r)
        {
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);
            row["时间"] = sj;
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.Contains("采集通道"))
                {
                    row[col] = "1";
                }
                #region 补沣西红光大道
                //else if (col.ColumnName.Contains("流速(m/s)"))
                //{
                //    row[col] = (r.NextDouble() * (0.38 - 0.18) + 0.18).ToString("F2");
                //}
                else
                {
                    switch (col.ColumnName)
                    {
                        case "17708":
                        case "17713":
                        case "17714":
                        case "17719":
                        case "17720":
                        case "17725":
                        case "17726":
                        case "17727":
                            row[col] = "0.00";
                            break;
                        case "17691": //526.00-680.00
                            row[col] = (r.NextDouble() * (680 - 526) + 526).ToString("F2");
                            break;
                        case "17695": //12-15
                        case "17707":
                            row[col] = r.Next(12, 16).ToString("F2");
                            break;
                        case "17699": //730-809
                            row[col] = r.Next(730,810).ToString("F2");
                            break;
                        case "17703":
                            row[col] = (r.NextDouble() * (680 - 526) + 526).ToString("F2");
                            break;
                        case "17712":
                            row[col] = r.Next(33, 40).ToString("F2");
                            break;
                        case "17715":
                            row[col] = (r.NextDouble() * (12- 9) + 9).ToString("F2");
                            break;
                        case "17724":
                            row[col] = r.Next(2, 5).ToString("F2");
                            break;
                        case "17743":
                        case "17749":
                        case "17755":
                        case "17761":
                            row[col] = (r.NextDouble() * (10.6 - 9) + 9).ToString("F2");
                            break;
                        case "17744":
                        case "17750":
                        case "17756":
                        case "17762":
                            row[col] = (r.NextDouble() * (4.3 - 2) + 2).ToString("F2");
                            break;
                        //default:
                        //    row[col] = "0.00";
                        //    break;
                    }
                }
                #endregion
                #region 补白城PH,TSS,T型液位,天健流量
                //else if (col.ColumnName.Contains("流速(m/s)"))
                //{
                //    row[col] = (r.NextDouble() * (0.38 - 0.18) + 0.18).ToString("F2");
                //}
                //else if (col.ColumnName.Contains("温度(C)"))
                //{
                //    row[col] = (r.NextDouble() * (26 - 13) + 13).ToString("F2");//(new Random().Next(13, 26) + new Random().NextDouble()).ToString("F2")
                //}
                //else if (col.ColumnName.Contains("流量(m3/s)"))
                //{
                //    row[col] = (r.NextDouble() * (0.025 - 0) + 0).ToString("F2");
                //}
                //else if (col.ColumnName.Contains("液位(m)"))
                //{
                //    row[col] = (r.NextDouble() * (11 - 1) + 1).ToString("F2");//(new Random().Next(1,11)+new Random().NextDouble()).ToString("F2");
                //}
                //else if (col.ColumnName.Contains("PH值"))
                //{
                //    row[col] = (r.NextDouble() * (14 - 5) + 5).ToString("F2");
                //}
                //else if (col.ColumnName.Contains("TSS值"))
                //{
                //    row[col] = (r.NextDouble() * (200 - 20) + 20).ToString("F2");
                //}
                //else if (col.ColumnName.Contains("液位(mm)"))
                //{
                //    row[col] = (r.NextDouble() * (1500 - 300) + 300).ToString("F2");
                //}
                #endregion
            }
        }

        void TableToFile(string directory, DataTable dt)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            StringBuilder sb = new StringBuilder();
            string l0 = "";
            foreach (DataColumn item in dt.Columns)
            {
                if (string.IsNullOrWhiteSpace(l0))
                {
                    l0 += string.IsNullOrWhiteSpace(item.Caption)?item.ColumnName:item.Caption;
                }
                else
                {
                    l0 += "," + (string.IsNullOrWhiteSpace(item.Caption) ? item.ColumnName : item.Caption);
                }
            }
            sb.AppendLine(l0);
            foreach (DataRow item in dt.Rows)
            {
                string line = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        line += item[i];
                    }
                    else
                    {
                        line += "," + item[i];
                    }
                }
                sb.AppendLine(line);
            }
            File.AppendAllText(Path.Combine(directory, dt.TableName + ".csv"), sb.ToString());
        }

    }
}
