﻿using Cinling.LibCache.Interfaces;
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
        public void Set() {
            cacheSrv.Set("abc", "abc");
        }
    }
}