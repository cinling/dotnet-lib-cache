using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestLibCache {
    public class StartUp {
        private readonly IConfiguration conf;

        public StartUp(IConfiguration configuration) {
            conf = configuration;
        }

        public void Configure(IApplicationBuilder builder, IWebHostEnvironment  environment) {
            
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddLogServiceScoped();
            services.AddFileCacheServiceScoped();
        }
    }
}