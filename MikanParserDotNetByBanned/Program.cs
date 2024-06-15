namespace MikanParserDotNetByBanned
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                // 启动数据刷新线程
                Thread threadA = new(new ThreadStart(RefreshRss));
                threadA.Start();
                Thread threadB = new(new ThreadStart(RefreshDataB));
                threadB.Start();
            }
        }

        private static void RefreshRss()
        {
            while (true)
            {
                // 模拟数据刷新
                Console.WriteLine("Refreshing data A...");
                Thread.Sleep(1000); // 每秒刷新一次
            }
        }

        private static void RefreshDataB()
        {
            while (true)
            {
                // 模拟数据刷新
                Console.WriteLine("Refreshing data B...");
                Thread.Sleep(1000); // 每秒刷新一次
            }
        }
    }
}