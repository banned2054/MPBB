using System.Globalization;

namespace MikanParserDotNetByBanned.utils.count
{
    internal class CountUtils
    {
        public static DateTime ConvertStringToDatetime(string dateTimeString)
        {
            // 查找小数点的位置
            var dotIndex = dateTimeString.IndexOf('.');

            // 如果找到了小数点
            if (dotIndex != -1)
            {
                // 提取毫秒部分
                var millisecondsPart = dateTimeString[(dotIndex + 1)..];

                millisecondsPart = millisecondsPart.Length switch
                {
                    // 补全毫秒部分至3位
                    1 => millisecondsPart + "00",
                    2 => millisecondsPart + "0",
                    _ => millisecondsPart
                };

                // 构建完整的日期时间字符串
                dateTimeString = dateTimeString[..(dotIndex + 1)] + millisecondsPart;
            }
            else
            {
                // 如果没有小数点，添加".000"表示毫秒部分为0
                dateTimeString = dateTimeString + ".000";
            }

            // 解析日期时间字符串
            var dateTime =
                DateTime.ParseExact(dateTimeString, "yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture);
            return dateTime;
        }

        public static string ConvertByteLongToByte(long bytes)
        {
            var result = string.Empty;
            switch (bytes)
            {
                case >= 1024L * 1024L * 1024L:
                {
                    var sizeInGigabytes = BytesToGigabytes(bytes);
                    result = $"{sizeInGigabytes:F2}GB";
                    break;
                }
                case >= 1024L * 1024L:
                {
                    var sizeInMegabyte = BytesToMegabyte(bytes);
                    result = $"{sizeInMegabyte:F2}MB";
                    break;
                }
                case >= 1024L:
                {
                    var sizeInKilobyte = BytesToKilobyte(bytes);
                    result = $"{sizeInKilobyte:F2}KB";
                    break;
                }
            }

            return result;
        }

        private static float BytesToKilobyte(long bytes)
        {
            return bytes / 1024f;
        }

        private static float BytesToMegabyte(long bytes)
        {
            return bytes / (1024f * 1024f);
        }

        private static float BytesToGigabytes(long bytes)
        {
            return bytes / (1024f * 1024f * 1024f);
        }
    }
}