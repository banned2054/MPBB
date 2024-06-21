using RestSharp;

namespace MikanParserDotNetByBanned.utils
{
    internal class BangumiNetUtils
    {
        public static (bool, string) FetchSubjectById(int subjectId)
        {
            try
            {
                var result = NetUtils.Fetch($"https://api.bgm.tv/v0/subjects/{subjectId}", 5, Method.Get,
                                            StaticConfig.BangumiApiHeaderWithoutAuthorization).Result;
                return result;
            }
            catch (Exception e)
            {
                {
                    return (false, e.Message);
                }
            }
        }
    }
}