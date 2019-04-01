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
        //想法是遍历所有文件，如果文件第一行包含PH或者TSS做特别处理，如果包含TSS并且文件fullpath包含鹤鸣湖，再特别处理。处理完成后将文本保存至新文件夹下
        //用UTF8读，用default写
        public void Work()
        {
            string path2017 = @"C:\Users\Administrator\Desktop\2017";
            string path2018 = @"C:\Users\Administrator\Desktop\2018";
			path2017 = @"C:\Users\mengft.TIANXIANG\Desktop\OTK\D1718\2017";
			path2018 = @"C:\Users\mengft.TIANXIANG\Desktop\OTK\D1718\2018";

			ProcDirectory(path2017);
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
            string delStr = @"C:\Users\Administrator\Desktop\";
            string sP = fullName.Replace(delStr,"");
            Directory.CreateDirectory(Path.GetDirectoryName(sP));
            string[] lines = File.ReadAllLines(fullName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(lines[0]);
            int index = -1;
            string nv = "";
            if (lines[0].Contains("PH"))
            {
                 nv = (r.NextDouble() * (9 - 6) + 6).ToString("F2");
                 index = GetIndex(lines[0],"PH");                                
            }
            else if (lines[0].Contains("TSS"))
            {
                if (fullName.Contains("鹤鸣湖"))
                {
                    nv = (r.NextDouble() * (9 - 6) + 6).ToString("F2");
                    index = GetIndex(lines[0], "TSS");                    
                }
                else
                {
                    nv = (r.NextDouble() * (200 - 0) + 0).ToString("F2");
                    index = GetIndex(lines[0], "TSS");                    
                }
            }
            for (int i = 1; i < lines.Length; i++)
            {
                if (index == -1)
                    sb.Append(lines[i]);
                else
                    sb.AppendLine(ChangeLine(lines[i], index, nv));
            }
            
            File.WriteAllText(sP, sb.ToString());
        }

		/// <summary>
		/// 根据文本文件中的数据生成全年雨量数据，转为CSV格式。
		/// </summary>
		public static void ImportWeatherStationData()
		{
			/*月份单行字符为数字，用TryParse判断
			 \t分隔数据行，行首为X日X时
			 */
		}


	}
}
