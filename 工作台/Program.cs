using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 工作台
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal tk = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            Console.WriteLine(tk);
            Console.ReadKey();
            BaiCheng20190413.ChuangJianFeed();
            Console.WriteLine("OK");
            while (true)
            {
                Console.ReadKey();
            }
        }
    }
}
