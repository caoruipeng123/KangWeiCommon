using System;
using System.Text;
using KangWeiCommon;
namespace KangWeiCommonRef
{
    class Program
    {
        static void Main(string[] args)
        {
            //IdWorker worker = new IdWorker(1, 10);
            //for(int i = 0; i < 1000; i++)
            //{
            //    Console.WriteLine(worker.NextId());
            //}
            //Console.WriteLine("Hello World!");
            string[] array = ",1,".Split(new char[] { ',', '，', '|' });
            StringBuilder builder = new StringBuilder();
            builder.Append("test");
            builder.Append(Environment.NewLine);
            builder.Append("test2");
            Console.WriteLine(builder.ToString());
            Console.ReadLine();
        }
    }
}
