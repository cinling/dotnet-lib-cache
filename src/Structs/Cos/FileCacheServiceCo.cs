using System;
using Cinling.Lib.Constants;
using Cinling.Lib.Structs.Vos;

namespace LibCache.Structs.Cos {
    /// <summary>
    /// 
    /// </summary>
    public class FileCacheServiceCo : BaseVo {
        /// <summary>
        /// Default save path for caching file
        /// 缓存文件保存路径
        /// </summary>
        public string savePath { get; set; } = "./runtime/cin-cache";

        /// <summary>
        /// Caching filename salt
        /// 缓存保存文件的额盐
        /// </summary>
        public string salt { get; set; } = "Cinling.LibCache";

        /// <summary>
        /// Path deeps
        /// 缓存文件目录深度
        /// </summary>
        public int pathDeeps { get; } = 4;

        /// <summary>
        /// Path single dirs length
        /// 缓存文件文件夹名字长度
        /// </summary>
        public int pathUnitLen { get; } = 2;

        /// <summary>
        /// The actual cache file size will be larger than this value and will only be checked when it is released. If the total size of cache files exceeds this value, the fastest outdated cache will be deleted first 
        /// 最大缓存的文件大小。实际缓存文件大小会比这个值大，仅释放时检测。如果缓存文件总大小超过这个值，将会优先删除最快过时的缓存
        /// </summary>
        public long cacheMaxSize = 50 * FileSize.Mb;

        /// <summary>
        /// Detection interval of automatic file length 
        /// 文件自动时长的检测时间间隔
        /// </summary>
        public TimeSpan freeSpan = new TimeSpan(1, 0, 0);
    }
}