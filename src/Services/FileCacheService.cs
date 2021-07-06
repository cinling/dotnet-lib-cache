using System;
using System.IO;
using System.Threading;
using Cinling.Lib.Exceptions;
using Cinling.Lib.Helpers;
using Cinling.LibCache.Interfaces;
using Cinling.LibCache.Options;
using Cinling.LibCache.Structs.Vos;
using Microsoft.Extensions.Options;

namespace Cinling.LibCache.Services {

    /// <summary>
    /// 
    /// </summary>
    public class FileCacheService : ICacheService, IDisposable {
        /// <summary>
        /// 
        /// </summary>
        private readonly FileCacheServiceOptions options;
        /// <summary>
        /// Operate write lock. Methods that will be locked: Set(), Del(), Free(), Clear() 
        /// 操作写锁。会上锁的方法：Set()、Del()、Free()、Clear()
        /// </summary>
        private readonly Mutex writeMutex = new();
        // private Timer freeTimer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOptions"></param>
        public FileCacheService(IOptions<FileCacheServiceOptions> iOptions) {
            options = iOptions.Value;
            CheckOptions();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        public string Set(string key, string value, TimeSpan? span) {
            var filename = GetSaveFilename(key);
            long expired = -1;
            if (span != null) {
                expired = (long)(TimeHelper.UnixTimeSeconds() + span.Value.TotalSeconds);
            }
            var cacheVo = new FileCacheVo {
                E = expired,
                C = value
            };
            writeMutex.WaitOne();
            var fileWriter = File.CreateText(filename);
            fileWriter.Write(cacheVo.ToJson());
            fileWriter.Flush();
            fileWriter.Close();
            writeMutex.ReleaseMutex();
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string Get(string key) {
            var filename = GetSaveFilename(key);
            if (!File.Exists(filename)) {
                return "";
            }
            var content = File.ReadAllText(filename);
            var cacheVo = GetFileCacheVoByContent(content);
            if (cacheVo != null && (cacheVo.E >= TimeHelper.UnixTimeSeconds() || cacheVo.E == -1)) {
                return cacheVo.C;
            }
            // Del(key);
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string Del(string key) {
            var filename = GetSaveFilename(key);
            if (!File.Exists(filename)) {
                return "";
            }
            var content = File.ReadAllText(filename);
            var cacheVo = GetFileCacheVoByContent(content);
            
            // 删除文件
            writeMutex.WaitOne();
            File.Delete(filename);
            writeMutex.ReleaseMutex();

            return cacheVo.C;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Free() {
            writeMutex.WaitOne();
            writeMutex.ReleaseMutex();
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Clear() {
            writeMutex.WaitOne();
            writeMutex.ReleaseMutex();
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
            // freeTimer?.Close();
            // Free();
        }
        
        /// <summary>
        /// Get the path where the cache file is saved
        /// 获取缓存文件保存的路径
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetSaveFilename(string key) {
            var sha1 = EncryptHelper.Sha1(options.Salt + key);
            var maxOffset = sha1.Length - options.PathUnitLen;
            var cacheFilename = "";
            for (var i = 0; i < options.PathDeeps; i++) {
                var offset = i * options.PathUnitLen;
                if (offset > maxOffset) {
                    break;
                }
                
                if (cacheFilename != "") {
                    cacheFilename += "/";
                }
        
                cacheFilename += sha1.Substring(offset, options.PathUnitLen);
            }
            var path = options.SavePath + "/" + cacheFilename;
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            return path +  "/" + sha1 + ".cache";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="BaseLibException"></exception>
        protected void CheckOptions() {
            if (options.PathDeeps * options.PathUnitLen > 32) {
                throw new BaseLibException("[options.PathDeeps * options.PathUnitLen] cannot greater then 32");
            }
        }

        /// <summary>
        /// 通过文件数据加载缓存数据
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected FileCacheVo GetFileCacheVoByContent(string content) {
            if (content == "") {
                return null;
            }
            var vo = new FileCacheVo();
            vo.SetByJson(content);
            return vo;
        }
    }
}