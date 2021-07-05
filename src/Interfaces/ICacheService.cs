using System;

namespace Cinling.LibCache.Interfaces {
    /// <summary>
    /// CacheService's Interface
    /// </summary>
    public interface ICacheService {
        /// <summary>
        /// Set Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        public string Set(string key, string value, TimeSpan? span);

        /// <summary>
        /// Get Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key);

        /// <summary>
        /// Delete Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Del(string key);

        /// <summary>
        /// Release outdated cache 
        /// </summary>
        public void Free();

        /// <summary>
        /// Clear all cache
        /// </summary>
        public void Clear();
    }

    public static class CacheServiceExtensions {
        public static string Set(this ICacheService service, string key, string value) => service.Set(key, value, null);
    }
}