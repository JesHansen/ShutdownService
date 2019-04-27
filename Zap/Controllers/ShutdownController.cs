using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Zap.Controllers
{
    [Route("shutdown")]
    [ApiController]
    public class ShutdownController : ControllerBase
    {
        private static Task shutdownTask;

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            shutdownTask = Task.Run(() => Shutdown());
            return "Shutting down in 5 sec.";
        }

        private static void Shutdown()
        {
            Thread.Sleep(5000);
            var psi = new ProcessStartInfo("shutdown", "/s /t 0") {CreateNoWindow = true, UseShellExecute = false};
            Process.Start(psi);
        }
    }
}
