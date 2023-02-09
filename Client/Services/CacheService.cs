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
        private SortedSet<(DateTime, string)> escapeCache = new(ItemComparer.Instance);

        public IEnumerable<string> GetCachedInputs()
        {
            return inputCache.Select(i => i.Item2);
        }

        public void StoreInputInCache(string input)
        {
            StoreInCache(input, inputCache);
        }

        public IEnumerable<string> GetCachedPatterns()
        {
            return patternCache.Select(i => i.Item2);
        }

        public void StorePatternInCache(string pattern)
        {
            StoreInCache(pattern, patternCache);
        }

        public IEnumerable<string> GetCachedEscapes()
        {
            return escapeCache.Select(i => i.Item2);
        }

        public void StoreEscapeInCache(string encode)
        {
            StoreInCache(encode, escapeCache);
        }

        private void StoreInCache(string data, SortedSet<(DateTime, string)> cache)
        {
            if (string.IsNullOrEmpty(data)) return;
            var item = cache.FirstOrDefault(i => i.Item2 == data);
            if (item != default) cache.Remove(item);
            cache.Add(new(DateTime.Now, data));
        }
    }
}