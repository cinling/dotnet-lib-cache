using Cinling.LibCache.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace TestLibCache.Services {
    
    
    public class FileCacheServiceTest {
        // private readonly TestServer server;
        private readonly ICacheService cacheSrv;
        
        public FileCacheServiceTest() {
            var server = new TestServer(new WebHostBuilder().UseStartup<StartUp>());
            cacheSrv = server.Host.Services.GetService<ICacheService>();
        }

        [Test]
        public void SetAndGet() {
            const string key = "FileCacheServiceTest.Get";
            const string value = "112233";
            cacheSrv.Set(key, value);
            Assert.AreEqual(value, cacheSrv.Get(key));
        }
    }
}