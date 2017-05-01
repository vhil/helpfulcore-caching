namespace Helpfulcore.Caching.Sitecore
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    public class SitecoreCacheRepository : global::Sitecore.Caching.CustomCache, ICacheRepository
    {
        private const string CacheKey = "Helpfulcore.Caching.CacheRepository";

        public SitecoreCacheRepository(string cacheName)
            :base(cacheName, global::Sitecore.Configuration.Settings.Caching.DefaultDataCacheSize)
        {
            global::Sitecore.Events.Event.Subscribe("publish:end", this.OnPublishEnd);
            global::Sitecore.Events.Event.Subscribe("publish:end:remote", this.OnPublishEnd);
        }

        public ConcurrentDictionary<string, MemoryCacheEntry> Entries
        {
            get
            {
                var entries = this.GetObject(CacheKey) as ConcurrentDictionary<string, MemoryCacheEntry>;
                if (entries == null)
                {
                    entries = new ConcurrentDictionary<string, MemoryCacheEntry>();
                    this.SetObject(CacheKey, entries);
                }

                return entries;
            }
        }

        private void OnPublishEnd(object sender, EventArgs e)
        {
            var keys = this.Entries
                .Where(x => x.Value is SitecoreMemoryCacheEntry)
                .Select(x => x.Key).ToArray();

            this.Entries.RemoveAndDispose(keys);
        }
    }
}
