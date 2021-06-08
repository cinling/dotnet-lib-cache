using System;
using System.Timers;
using Cinling.Lib.Utils;
using LibCache.Interfaces;
using LibCache.Structs.Cos;

namespace LibCache.Services {
    
    /// <summary>
    /// 
    /// </summary>
    public class FileCacheService : ICacheService {
        /// <summary>
        /// 
        /// </summary>
        private readonly FileCacheServiceCo co = new();
        /// <summary>
        /// 
        /// </summary>
        private Timer freeTimer;

        public FileCacheService() {
            reload();
        }

        public void reload() {
            if (freeTimer != null) {
                freeTimer.Enabled = false;
                freeTimer.Stop();
                freeTimer = null;
            }

            freeTimer = new Timer {Enabled = true, Interval = co.freeSpan.Milliseconds};
            freeTimer.Start();
            freeTimer.Elapsed += timer_free;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FileCacheServiceCo getCo() {
            return co;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string set(string key, string value) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string set(string key, string value, TimeSpan span) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T set<T>(string key, T value) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="span"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T set<T>(string key, T value, TimeSpan span) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string get(string key) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T get<T>(string key) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void del(string key) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete files that have timed out
        /// 删除超时的缓存文件
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void free() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete all cache
        /// 删除所有缓存
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void clear() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_free(object sender, ElapsedEventArgs e) {
            free();
        }

        /// <summary>
        /// Get the path where the cache file is saved
        /// 获取缓存文件保存的路径
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string getSaveFilename(string key) {
            var sha1 = EncryptUtil.sha1(key);
            var maxOffset = sha1.Length - co.pathUnitLen;


            var cacheFilename = "";
            for (int i = 0; i < co.pathDeeps; i++) {
                var offset = i * co.pathUnitLen;
                if (offset > maxOffset) {
                    break;
                }
                
                if (cacheFilename != "") {
                    cacheFilename += "/";
                }

                cacheFilename += sha1.Substring(offset, co.pathUnitLen);
            }
            return co.savePath + "/" + cacheFilename + ".cache";
        }
    }
}