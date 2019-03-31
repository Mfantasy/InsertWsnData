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
            string path2017 = @"C:\Users\Administrator\Desktop\2017";
            string path2018 = @"C:\Users\Administrator\Desktop\2018";


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
            
            string delStr = @"C:\Users\Administrator\Desktop\";
            string sP = fullName.Replace(delStr,"");
            Directory.CreateDirectory(Path.GetDirectoryName(sP));
            string[] lines = File.ReadAllLines(fullName);
            StringBuilder sb = new StringBuilder();
            
            string nv = "";

            Action<double, double> act = (h, l) => {
                DateTime temp = ds;
                while (temp < de)
                {
                    temp = temp.AddMinutes(5);
                    nv = (r.NextDouble() * (h - l) + l).ToString("F2");
                    sb.AppendLine(temp+","+ nv);
                }
            };

            if (lines[0].Contains("PH"))
            {
                sb.AppendLine("时间,PH值");
                act(9, 6);                                  
            }
            else if (lines[0].Contains("TSS"))
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
            else
            {            
                for (int i = 0; i < lines.Length; i++)
                {
                    sb.AppendLine(lines[i]);
                }
            }
                        
            File.WriteAllText(sP, sb.ToString(), Encoding.Default);
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
