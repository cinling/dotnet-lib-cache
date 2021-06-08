using System;

namespace LibCache.Interfaces {
    /// <summary>
    /// CacheService's Interface
    /// </summary>
    public interface ICacheService {

        public string set(string key, string value);
        public string set(string key, string value, TimeSpan span);
        public T set<T>(string key, T value);
        public T set<T>(string key, T value, TimeSpan span);
        public string get(string key);
        public T get<T>(string key);
        public void del(string key);
        public void free();
        public void clear();

    }
}