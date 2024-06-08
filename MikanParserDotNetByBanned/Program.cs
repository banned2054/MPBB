using MikanParserDotNetByBanned.utils;
using MikanParserDotNetByBanned.utils.parsers;

namespace MikanParserDotNetByBanned
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var a = NetUtils.Fetch("https://mikanani.me/Home/Episode/23e1b6b1f12493407ba0174436915110bfa28725", 5)
                            .Result;
            if (!a.Item1)
            {
                Console.WriteLine(a.Item2);
                return;
            }

            var b      = a.Item2;
            var result = RssMikanParser.GetHomeUrlFromEpisodePageHtml(b);
            Console.WriteLine(result.Item2);

            if (!result.Item1)
            {
                return;
            }

            a = NetUtils.Fetch(result.Item2, 5).Result;
            if (!a.Item1)
            {
                Console.WriteLine(a.Item2);
                return;
            }

            b      = a.Item2;
            result = RssMikanParser.GetBangumiUrlFromHomePageHtml(b);
            Console.WriteLine(result.Item2);
            /*
                {
                    // 启动数据刷新线程
                    Thread threadA = new(new ThreadStart(RefreshDataA));
                    threadA.Start();
                    Thread threadB = new(new ThreadStart(RefreshDataB));
                    threadB.Start();
                }
                */
        }

        private static void RefreshDataA()
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