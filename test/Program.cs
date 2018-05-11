using System;
using System.Collections.Generic;
using QLCommon;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            // PageInfo page = new PageInfo();

            //Console.WriteLine("Hello World!");
            //Console.WriteLine(SecurityHelper.HTMLEncode("<h3>content</h3>"));
            NetTest();
            Console.Read();
        }


        public static void NetTest()
        {

            //下载问价那测试
            //NetHelper.DownLoad("http://www.gongjuji.net", @"H:\开发工具\SqlServer\1.html");
            //Console.WriteLine("下载成功");



            //string content = NetHelper.Post("http://www.gongjuji.net");
            //Console.WriteLine(content);


            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("content", "qianle3798");

            //string content2 = NetHelper.Post("http://md5.gongjuji.net/common/md5encrypt", dic);
            string content2 = NetHelper.Post("http://md5.gongjuji.net/common/md5encrypt", "content=qianle3798");
            Console.WriteLine(content2);

            dic.Add("UpperCase", "1020A627658A7E36134260627081518A");
            string content3 = NetHelper.Post("http://md5.gongjuji.net/common/md5dencrypt",dic);
            Console.WriteLine(content3);
        }
    }
}