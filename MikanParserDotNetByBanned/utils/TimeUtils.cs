namespace MikanParserDotNetByBanned.utils
{
    internal class TimeUtils
    {
        private static readonly TimeZoneInfo ShanghaiZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");

        public static DateTime ConvertTimeFromShanghaiTimeToLocalTime(DateTime time)
        {
            // 将上海时间转换为 UTC 时间
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(time, ShanghaiZone);

            // 获取当前操作系统的时区信息
            var localZone = TimeZoneInfo.Local;

            // 将 UTC 时间转换为本地时间
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, localZone);

            return localTime;
        }
    }
}