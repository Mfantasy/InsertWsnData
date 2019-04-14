using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wsndata数据交互
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string rain7 =new ProcessCsv().ImportWeatherStationData(2017).ToString();
            string rain8 = new ProcessCsv().ImportWeatherStationData(2018).ToString();
            File.WriteAllText("雨量2017.csv", rain7);
            File.WriteAllText("雨量2018.csv", rain8);
            Console.WriteLine("OK");
            return;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
