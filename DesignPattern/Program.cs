using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new PorscheCar();
            car.SetAttack(new GunShot());
            car.Attatck();
            car.SetAttack(new KnifeSplash());
            car.Attatck();


            //Console.Write("輸入關鍵字 >> ");
            //string keyword = Console.ReadLine();
            //Console.WriteLine("------------------------------- {0}", DateTime.Now.ToString("HH:mm:ss"));
            //int pages = 5;
            //for(int page = 1; page <= pages; page++)
            //{
            //    for (int item = 0; item <= 50; item++)
            //    {
            //        Console.Write($"\r第 {page}/{pages} 頁, 第 {item:00}/50 項");
            //        Thread.Sleep(25);
            //    }
            //    Console.WriteLine("", ConsoleColor.Red);
            //}
            //Console.WriteLine("------------------------------- {0}", DateTime.Now.ToString("HH:mm:ss"));


            //Firebase().Wait();

            //PipelineAsync.PipelineDemo.JobExecute();
            //PipelineBreak.PipelineDemo.Execute();
            //PipelineBreakAsync.PipelineDemo.JobExecute();

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("主线程测试开始..");
            AsyncMethod();
            Thread.Sleep(1000);
            Console.WriteLine("主线程测试结束..");
            Console.ReadLine();
        }

        static async void AsyncMethod()
        {
            Console.WriteLine("开始异步代码");
            var result = await MyMethod();
            Console.WriteLine("异步代码执行完毕" + result.ToString());
        }
        
        static async Task<int> MyMethod()
        {
            for (int i = 0; i< 5; i++)
            {
                Console.WriteLine("异步执行" + i.ToString() + "..");
                await Task.Delay(1000); //模拟耗时操作
            }
            return 100;
        }

        static async Task Firebase()
        {
            FirebaseClient client = new FirebaseClient("https://xxxxxxxxxxxxxxx.firebaseio.com");

            ChildQuery child = client.Child("xxxxxx");

            Shopee data = await child.OnceSingleAsync<Shopee>();

            Console.WriteLine(JsonConvert.SerializeObject(data));
        }

        public class Crawler
        {
            public Alibaba Alibaba { get; set; }
            public Shopee Shopee { get; set; }
        }

        public class Alibaba
        {
            public string KeywordCategory { get; set; }
            public string SearchData { get; set; }
        }

        public class Shopee
        {
            public string KeywordPrice { get; set; }
        }

        public class Crawler_P4P
        {
            public string Version { get; set; }
        }

    }
}
