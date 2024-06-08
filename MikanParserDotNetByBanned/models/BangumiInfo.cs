namespace MikanParserDotNetByBanned.models
{
    internal class BangumiInfo
    {
        public int      Id           { get; set; }
        public string   OriginalName { get; set; } = string.Empty;
        public string   CnName       { get; set; } = string.Empty;
        public DateTime Date         { get; set; }

        public List<string> UnofficialNameList { get; set; } = new List<string>();
    }
}