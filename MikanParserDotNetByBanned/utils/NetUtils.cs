using RestSharp;

namespace MikanParserDotNetByBanned.utils
{
    internal class NetUtils
    {
        public static async Task<(bool, string)> Fetch(string                       url,
                                                       int                          lastTimes,
                                                       Method                       method  = Method.Get,
                                                       Dictionary<string, string> ? headers = null,
                                                       object ?                     body    = null)
        {
            var result = await FetchAsync(url, method, headers, body);
            while (lastTimes > 1 && !result.Item1)
            {
                lastTimes--;
                result = await FetchAsync(url, method, headers, body);
            }

            return result;
        }

        private static async Task<(bool, string)> FetchAsync(string                       url,
                                                             Method                       method,
                                                             Dictionary<string, string> ? headers = null,
                                                             object ?                     body    = null)
        {
            try
            {
                var client  = new RestClient();
                var request = new RestRequest(url, method);

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }

                if (body != null)
                {
                    request.AddJsonBody(body);
                }

                var response = await client.ExecuteAsync(request);
                var result   = (true, response.Content ?? "");
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static async Task<(bool, string)> DownloadFile(string                       url, string downloadPath,
                                                              int                          lastTimes,
                                                              Dictionary<string, string> ? headers = null)
        {
            var result = await DownloadFileAsync(url, downloadPath, headers);
            while (!result.Item1 && lastTimes > 1)
            {
                lastTimes--;
                result = await DownloadFileAsync(url, downloadPath, headers);
            }

            return result;
        }

        private static async Task<(bool, string)> DownloadFileAsync(string                       url,
                                                                    string                       downloadPath,
                                                                    Dictionary<string, string> ? headers = null)
        {
            try
            {
                var client  = new RestClient();
                var request = new RestRequest(url);

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }

                var response = await client.DownloadDataAsync(request);

                if (response == null || response.Length == 0)
                {
                    throw new Exception("Failed to download file or file is empty.");
                }

                var fileName = Path.GetFileName(url);
                var filePath = Path.Combine(downloadPath, fileName);

                await File.WriteAllBytesAsync(filePath, response);

                return (true, filePath);
            }
            catch (Exception ex)
            {
                // 记录异常信息或进行其他处理
                Console.WriteLine($"Error: {ex.Message}");
                return (false, ex.Message);
            }
        }
    }
}