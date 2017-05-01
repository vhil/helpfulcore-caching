namespace Helpfulcore.Caching.Sitecore
{
    using System;

    public class SitecoreMemoryCacheEntry : MemoryCacheEntry
    {
        public bool ClearOnPublish { get; protected set; }
        public SitecoreMemoryCacheEntry(object value, TimeSpan expiresIn = new TimeSpan(), bool clearOnPublish = true)
            : base(value, expiresIn)
        {
            this.ClearOnPublish = clearOnPublish;
        }
    }
}
