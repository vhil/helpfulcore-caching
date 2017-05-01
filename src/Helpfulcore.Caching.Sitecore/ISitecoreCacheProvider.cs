namespace Helpfulcore.Caching.Sitecore
{
    using System;

    public interface ISitecoreCacheProvider : ICacheProvider
    {
        bool Add<T>(string key, T value, TimeSpan expiresIn = new TimeSpan(), bool clearOnPublish = false);
        bool Set<T>(string key, T value, TimeSpan expiresIn = default(TimeSpan), bool clearOnPublish = false);
    }
}
