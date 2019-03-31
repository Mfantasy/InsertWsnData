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
}
