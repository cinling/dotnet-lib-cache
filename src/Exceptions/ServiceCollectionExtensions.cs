using System;
using Cinling.Lib.FileLogger;
using Cinling.Lib.Interfaces;
using Cinling.Lib.Options;
using Cinling.Lib.Services;
using Cinling.Lib.Structs.Cos;
using Cinling.LibCache.Interfaces;
using Cinling.LibCache.Options;
using Cinling.LibCache.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection {
    
    /// <summary>
    /// IServiceCollection Extensions
    /// 服务扩展方法
    /// </summary>
    public static class ServiceCollectionExtensions {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileCacheServiceScoped(this IServiceCollection services) => AddFileCacheServiceScoped(services, new FileCacheServiceOptions());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileCacheServiceScoped(this IServiceCollection services, FileCacheServiceOptions options) => AddFileCacheServiceScoped(services, originOptions => {
            originOptions.CloneWith(options);
        });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileCacheServiceScoped(this IServiceCollection services, Action<FileCacheServiceOptions> optionsAction) {
            services.AddOptions();
            services.AddOptions<FileCacheServiceOptions>().Configure(optionsAction);
            services.TryAddScoped<ICacheService, FileCacheService>();
            return services;
        }
    }
}