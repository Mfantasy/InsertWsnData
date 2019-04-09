using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsndata数据交互
{
    class ProcessCsv
    {
        //PH是6-9 鹤鸣湖的TSS（7-15) 其他小于200
        //Delete想法是遍历所有文件，如果文件第一行包含PH或者TSS做特别处理，如果包含TSS并且文件fullpath包含鹤鸣湖，再特别处理。处理完成后将文本保存至新文件夹下
        //New 重做所有PH与TSS。 除时间外其它字段填0
        public void Work()
        {
            string path2017 = @"C:\Users\mengft.TIANXIANG\Desktop\OTK\20190328\2017";
            string path2018 = @"C:\Users\mengft.TIANXIANG\Desktop\OTK\20190328\2018";


            ProcDirectory(path2018);
            Console.WriteLine("OK");
        }

        void ProcDirectory(string path)
        {
            DirectoryInfo dInfo = Directory.CreateDirectory(path);
            DirectoryInfo[] directoryInfos = dInfo.GetDirectories();
            foreach (DirectoryInfo item in directoryInfos)
            {
                FileInfo[] files = item.GetFiles();
                foreach (var f in files)
                {
                    ProcFile(f.FullName);
                }
            }
        }

        void ProcFile(string fullName)
        {
            Random r = new Random();
            DateTime ds = DateTime.MinValue;
            DateTime de = DateTime.MaxValue;
            bool is2017 = fullName.Contains("2017");
            if (fullName.Contains("2017"))
            {
                ds = new DateTime(2017, 1, 1);
                de = new DateTime(2018, 1, 1);
            }
            else
            {
                ds = new DateTime(2018,1,1);
                de = new DateTime(2019,1,1);
            }
            
            string delStr = @"C:\Users\mengft.TIANXIANG\Desktop\OTK\20190328\";
            string sP = fullName.Replace(delStr,"");
            Directory.CreateDirectory(Path.GetDirectoryName(sP));
            string[] lines = File.ReadAllLines(fullName);
            StringBuilder sb = new StringBuilder();
            
            string nv = "0";

            Action<double, double> act = (h, l) => {
                DateTime temp = ds;
			
                while (temp < de)
                {
					temp = temp.AddMinutes(5);
					if (is2017)
					{
						if (fullName.Contains("纵八路"))
						{
							/*2017  8月份8月3日7点到14点流量数据保留，其他流量数据为0
                                    9月份9月19日流量数据保留，其他流量数据为0  */
							DateTime d17080307 = new DateTime(2017, 8, 3, 7, 0, 0);
							DateTime d17080314 = new DateTime(2017, 8, 3, 14, 0, 0);
							DateTime d17091907 = new DateTime(2017, 8, 9, 19, 0, 0);
							DateTime d17091907e = d17091907.AddDays(1);
							if ((temp > d17080307 && temp < d17080314) || (temp > d17091907 && temp < d17091907e))
							{
								nv = (r.NextDouble() * (h - l) + l).ToString("F2");
							}
							else
							{
								nv = "0";
							}
						}
						else if (fullName.Contains("丽江路北侧新区中学") || fullName.Contains("市民中心") || fullName.Contains("科文中心"))
						{
							nv = "0";
						}
						else if (fullName.Contains("胜利路与金辉街") || fullName.Contains("南干渠华严寺正门"))
						{
							/*
							8月份（8月3日7点到4日5点）（8月8日2点到9点）（8月11日23点到12日2点）流量数据保留，其他流量数据为0.01或0.02；
							 9月份（9月5日8点到17点）（9月19日2点到16点）流量数据保留，其他流量数据为0.01、0.02*/
							DateTime d17080307 = new DateTime(2017, 8, 3, 7, 0, 0);
							DateTime d17080405 = new DateTime(2017, 8, 4, 5, 0, 0);
							DateTime d17080802 = new DateTime(2017, 8, 8, 2, 0, 0);
							DateTime d17080809 = new DateTime(2017, 8, 8, 9, 0, 0);
							DateTime d17081123 = new DateTime(2017, 8, 11, 23, 0, 0);
							DateTime d17081202 = new DateTime(2017, 8, 12, 2, 0, 0);
							DateTime d17090508 = new DateTime(2017, 9, 5, 8, 0, 0);
							DateTime d17090517 = new DateTime(2017, 9, 5, 17, 0, 0);
							DateTime d17091902 = new DateTime(2017, 9, 19, 2, 0, 0);
							DateTime d17091916 = new DateTime(2017, 9, 19, 16, 0, 0);
							if ((temp > d17080307 && temp < d17080405) || (temp > d17080802) && (temp < d17080809) || (temp > d17081123) && (temp < d17081202)
							|| (temp > d17090508 && temp < d17090517) || (temp > d17091902 && temp < d17091916))
							{
								nv = (r.NextDouble() * (h - l) + l).ToString("F2");
							}
							else
							{
								nv = "0.01";
							}
						}
						else if (GetIsRain(temp,is2017))
						{
							nv = (r.NextDouble() * (h - l) + l).ToString("F2");
						}
						else
						{
							nv = "0";
						}
					}
					else
					{
						if (fullName.Contains("丽江路北侧新区中学") || fullName.Contains("市政府"))
						{
							//后期7.25、7.24、7.17、7.8、5.28、5.27流量数据保留，其他为0
							string[] dates = {"0725","0724","0717","0708","0528","0527"};
							if (dates.Contains(temp.ToString("MMdd")))
							{
								nv = (r.NextDouble() * (h - l) + l).ToString("F2");
							}
							else
							{
								nv = "0";
							}
						}
						else if (fullName.Contains("市民中心") || fullName.Contains("科文中心") || fullName.Contains("纵八路"))
						{
							nv = "0";
						}
						else if (GetIsRain(temp, is2017))
						{
							nv = (r.NextDouble() * (h - l) + l).ToString("F2");
						}
						else
						{
							nv = "0";
						}
					}

					
					sb.AppendLine(temp + "," + nv);

				}
            };

			if (fullName.Contains("PH"))
			{
				sb.AppendLine("时间,PH值");
				act(9, 6);
			}
			else if (fullName.Contains("TSS"))
			{
				sb.AppendLine("时间,TSS值");
				if (fullName.Contains("鹤鸣湖"))
				{
					act(15, 7);
				}
				else
				{
					act(200, 0);
				}
			}
			else if (fullName.Contains("流量"))
			{
				sb.AppendLine("时间,流量");
				act(0.025, 0);
			}
			else if (fullName.Contains("液位"))
			{
				sb.AppendLine("时间,液位");
				act(1500, 300);
			}
			
			else
			{
				for (int i = 0; i < lines.Length; i++)
				{
					sb.AppendLine(lines[i]);
				}
			}
                        
            File.WriteAllText(sP, sb.ToString(), Encoding.Default);
        }


		/// <summary>
		/// 获取指定时间是否有降雨
		/// </summary>
		private bool GetIsRain(DateTime temp, bool is2017)
		{
			List<DateTime> times;
			if (is2017)
			{
				times = rains2017.Keys.ToList();
			}
			else
			{
				times = rains2018.Keys.ToList();
			}

			return times.Exists(t => t.AddMinutes(-30) < temp && t.AddMinutes(30) > temp);
		}

		Dictionary<DateTime, string> rains2017 = new Dictionary<DateTime, string>();
		Dictionary<DateTime, string> rains2018 = new Dictionary<DateTime, string>();

		/// <summary>
		/// 根据文本文件中的数据生成全年雨量数据，转为CSV格式。
		/// </summary>
		public StringBuilder ImportWeatherStationData(int year)
		{
			Dictionary<DateTime, string> rains;if (year == 2017) rains = rains2017; else rains = rains2018;
			string fname = "气象站.txt" + year;
			string[] allLines = File.ReadAllLines(fname);
			//时间雨量，字典
			if (year == 2017)
			{
				/*月份单行字符为数字，用TryParse判断
				 \t分隔数据行，行首为X日X时
				 */
				int month = 0;
				foreach (string l in allLines)
				{
					int mtemp = 0;
					if (int.TryParse(l, out mtemp))
					{
						//切换一个新的月份
						month = mtemp;
					}
					else
					{
						string[] strs = l.Split('\t');
						int date = int.Parse(strs[0].Remove(strs[0].IndexOf("日")));
						int hour = int.Parse(strs[0].Remove(strs[0].IndexOf("时")).Substring(strs[0].IndexOf("日") + 1));
						for (int i = 1; i < 61; i++)
						{
							if (decimal.Parse(strs[i]) > 0)
							{
								DateTime dk = new DateTime(year, month, date, hour, i - 1, 0);
								rains.Add(dk, strs[i]);
							}
						}
					}
				}
			}
			else if (year == 2018)
			{
				/* 年\t 月 日  时 分 量
				2018    1   8   21  3   0.1
				*/
				foreach (string l in allLines)
				{
					string[] strs = l.Split('\t');
					int month = int.Parse(strs[1]);
					int date = int.Parse(strs[2]);
					int hour = int.Parse(strs[3]);
					int minute = int.Parse(strs[4]);
					string rainV = strs[5];
					rains.Add(new DateTime(year, month, date, hour, minute, 0), rainV);
				}
			}

			int syear = year;
			int eyear = syear + 1;

			DateTime ds = new DateTime(syear, 1, 1);
			DateTime de = new DateTime(eyear - 1, 4, 1);
			DateTime dtemp = ds;

			StringBuilder txt = new StringBuilder();
			txt.AppendLine("时间,雨量");
			while (dtemp < de)
			{
				if (rains.ContainsKey(dtemp))
				{
					txt.AppendLine(dtemp + "," + rains[dtemp]);
				}
				else
				{
					txt.AppendLine(dtemp + ",0");
				}
				dtemp = dtemp.AddMinutes(1);
			}

			return txt;
		}


	}

    class Info
    {
        /*
         1.做气象站数据 见表

         2.做其它数据         

        除去特殊处理的节点，剩下的按照气象站降雨序列对流量，PH，TSS清0

        //2017：
        8月份8月3日7点到14点流量数据保留，其他流量数据为0
        9月份9月19日流量数据保留，其他流量数据为0
        //纵八路

        全部清0
        //丽江路北侧新区中学
        //市民中心
        //科文中心

        8月份（8月3日7点到4日5点）（8月8日2点到9点）（8月11日23点到12日2点）流量数据保留，其他流量数据为0.01或0.02；
        9月份（9月5日8点到17点）（9月19日2点到16点）流量数据保留，其他流量数据为0.01、0.02
        //胜利路与金辉街

        8月份（8月3日7点到4日5点）（8月8日2点到9点）（8月11日23点到12日2点）流量数据保留，其他流量数据为0.01或0.02；相应液位数据按此修改
        9月份（9月5日8点到17点）（9月19日2点到16点）流量数据保留，其他流量数据为0.01或0.02；相应液位数据按此修改
        //南干渠华严寺正门

		2018:
        后期7.25、7.24、7.17、7.8、5.28、5.27流量数据保留，其他为0
        //丽江路北侧新区中学
        //市政府
        全部清0
        //市民中心
        //科文中心
        //纵八路


    */
    }
}


