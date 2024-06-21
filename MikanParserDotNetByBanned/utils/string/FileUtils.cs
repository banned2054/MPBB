namespace MikanParserDotNetByBanned.utils.@string
{
    internal class FileUtils
    {
        public static (bool, string) DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                return (true, "");
            }
            catch (IOException e)
            {
                return (false, $"删除文件时出现错误: {e.Message}");
            }
            catch (UnauthorizedAccessException e)
            {
                return (false, $"未授权访问: {e.Message}");
            }
            catch (Exception e)
            {
                return (false, $"发生了错误: {e.Message}");
            }
        }

        public static (bool, string) ReadFile(string path)
        {
            try
            {
                using StreamReader reader = new(path);

                // Read the stream as a string.
                var text = reader.ReadToEnd();
                return (true, text);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}