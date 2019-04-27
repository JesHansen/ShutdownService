using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.ServiceConfigurators;

namespace Zap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(ConfigureHost);
            var result = (int) Convert.ChangeType(exitCode, exitCode.GetTypeCode(), CultureInfo.InvariantCulture);
            Environment.ExitCode = result;
        }

        private static void ConfigureHost(HostConfigurator x)
        {
            x.Service<ShutdownService>(ConfigureService);
            x.SetDescription("Allows remote shutdown of the computer");
            x.SetDisplayName("Shutdown service");
            x.SetServiceName("ShutdownService");
        }

        private static void ConfigureService(ServiceConfigurator<ShutdownService> s)
        {
            s.ConstructUsing(t => new ShutdownService());
            s.WhenStarted(t => t.Start());
            s.WhenStopped(async t => await t.Stop());
        }
    }
}
