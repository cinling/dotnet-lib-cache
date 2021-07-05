using System;
using Cinling.Lib.Interfaces;

namespace Cinling.LibCache.Options {

    /// <summary>
    /// 
    /// </summary>
    public class FileCacheServiceOptions : ICloneWith {

        /// <summary>
        /// Default save path for caching file
        /// 缓存文件保存路径
        /// </summary>
        public string SavePath { get; set; } = "./runtime/cin-cache";
        
        /// <summary>
        /// Caching filename salt
        /// 缓存保存文件的盐
        /// </summary>
        public string Salt { get; set; } = "Cinling.LibCache";
        
        /// <summary>
        /// SavePath deeps
        /// 保存文件的深度
        /// </summary>
        public byte PathDeeps { get; set; } = 4;
        
        /// <summary>
        /// SavePath single dirs length
        /// 缓存文件文件夹名字长度
        /// </summary>
        public byte PathUnitLen { get; set; } = 2;

        // /// <summary>
        // /// The detection time for releasing the cache 
        // /// 释放缓存的检测时长
        // /// </summary>
        // public TimeSpan FreeSpan { get; set; } = new TimeSpan(1, 0, 0);
        //
        // /// <summary>
        // /// When releasing the cache detection, if the size exceeds this value after release, the fastest expired cache data will be released.
        // /// Note: The actual cached data will be smaller than this data. Because the cached information also needs to record additional information, such as the timeout period. If this value is 50M, it is expected to cache 5M-20M data 
        // /// 释放缓存检测时，如果释放后，大小超过这个值，则会释放掉最快过期的缓存数据。
        // /// 注意：实际的缓存数据会比这个数据小。因为缓存的信息还要记录额外的信息，如超时时间。如果这个值时 50M，预计可缓存5M-20M的数据
        // /// </summary>
        // public long MaxSize { get; set; } = 200 * 1024 * 1024;
    }
}