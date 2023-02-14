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

        public const string MATCH_INPUT_CACHE = "MATCH_INPUT_CACHE";
        public const string MATCH_PATTERN_CACHE = "MATCH_PATTERN_CACHE";
        public const string ESCAPE_CACHE = "REPLACEESCAPE_CACHE";
        public const string REPLACE_INPUT_CACHE = "REPLACE_INPUT_CACHE";
        public const string REPLACE_PATTERN_CACHE = "REPLACE_PATTERN_CACHE";
        public const string REPLACE_REPLACEMENT_CACHE = "REPLACE_REPLACEMENT_CACHE";

        private Dictionary<string, SortedSet<(DateTime, string)>> cacheHolder = new();

        public IEnumerable<string> GetCachedData(string cacheName)
        {
            return GetOrCreateCache(cacheName).Select(i => i.Item2);
        }

        public void StoreDataInCache(string cacheName, string data)
        {
            if (data == "") return;
            var cache = GetOrCreateCache(cacheName);
            var item = cache.FirstOrDefault(i => i.Item2 == data);
            if (item != default) cache.Remove(item);
            cache.Add(new(DateTime.Now, data));
        }

        private SortedSet<(DateTime, string)> GetOrCreateCache(string name)
        {
            if (cacheHolder.ContainsKey(name)) return cacheHolder[name];
            var cache = new SortedSet<(DateTime, string)>(ItemComparer.Instance);
            cacheHolder[name] = cache;
            return cache;
        }
    }
}