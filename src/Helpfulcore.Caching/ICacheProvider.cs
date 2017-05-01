namespace Helpfulcore.Caching
{
    using System;
    using System.Collections.Generic;

    public interface ICacheProvider : IDisposable
    {
        bool Add<T>(string key, T value, TimeSpan expiresIn = default(TimeSpan));

        T Get<T>(string key);

        IDictionary<string, T> GetAll<T>(IEnumerable<string> keys);

        bool Remove(string key);

        void RemoveAll(IEnumerable<string> keys);

        bool Set<T>(string key, T value, TimeSpan expiresIn = default(TimeSpan));

        void FlushAll();
    }
}
