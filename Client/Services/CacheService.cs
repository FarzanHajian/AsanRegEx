namespace AsanRegEx.Client.Services
{
    public class CacheService
    {
        private class ItemComparer : IComparer<(DateTime, string)>
        {
            private static ItemComparer instance = new();

            public static ItemComparer Instance => instance;

            public int Compare((DateTime, string) x, (DateTime, string) y)
            {
                return -1 * x.Item1.CompareTo(y.Item1);
            }
        }

        private SortedSet<(DateTime, string)> inputCache = new(ItemComparer.Instance);
        private SortedSet<(DateTime, string)> patternCache = new(ItemComparer.Instance);

        public IEnumerable<string> GetCachedInputs()
        {
            return inputCache.Select(i => i.Item2);
        }

        public void StoreInputInCache(string input)
        {
            if (string.IsNullOrEmpty(input)) return;
            var item = inputCache.FirstOrDefault(i => i.Item2 == input);
            if (item != default) inputCache.Remove(item);
            inputCache.Add(new(DateTime.Now, input));
        }

        public IEnumerable<string> GetCachedPatterns()
        {
            return patternCache.Select(i => i.Item2);
        }

        public void StorePatternInCache(string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return;
            var item = patternCache.FirstOrDefault(i => i.Item2 == pattern);
            if (item != default) patternCache.Remove(item);
            patternCache.Add(new(DateTime.Now, pattern));
        }
    }
}