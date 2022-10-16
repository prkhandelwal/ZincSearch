using static ZincSearch.Constants;

namespace ZincSearch
{
    public record WebPage(int Index, List<string> Keywords)
    {
        public Dictionary<String, int> KeywordsWithWeights { get; init; } = ValidateSizeAndGetKeywords(Keywords);
        public string Name => "P" + Index;

        private static Dictionary<string, int> ValidateSizeAndGetKeywords(List<String> Keywords)
        {
            var result = new Dictionary<string, int>();
            var keywords = Keywords.Count > MAX_WEIGHT ? Keywords.GetRange(0, MAX_WEIGHT) : Keywords;
            var length = keywords.Count;
            for (int i = 0; i < length; i++)
            {
                result.Add(keywords[i], MAX_WEIGHT - i);
            }

            return result;
        }
    }
}
