using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Zap
{
    public class ShutdownService : IDisposable, IService
    {
        private IWebHost webHost;
        private Task webHostTask;

        public void Start()
        {
            webHost = CreateWebHostBuilder(null).Build();
            webHostTask = webHost.RunAsync();
        }

        public async Task Stop()
        {
            await webHost.StopAsync();
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public void Dispose()
        {
            webHost?.Dispose();
            while (!webHostTask.IsCompleted)
            {
                Thread.Sleep(100);
            }
            webHostTask?.Dispose();
        }
    }

    public interface IService
    {
        void Start();
        Task Stop();
    }
}
