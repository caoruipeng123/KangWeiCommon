using System;
using KangWeiCommon;
namespace KangWeiCommonRef
{
    class Program
    {
        static void Main(string[] args)
        {
            IdWorker worker = new IdWorker(1, 10);
            for(int i = 0; i < 1000; i++)
            {
                Console.WriteLine(worker.NextId());
            }
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
