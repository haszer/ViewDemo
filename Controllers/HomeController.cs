using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly BackgroundService background;
        public static List<string> getmessage=new List<string>();

        public HomeController(ILogger<HomeController> logger, IHostedService hostedService)
        {
            _logger = logger;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MyTask(CancellationToken cancellationToken)
        {
            _logger.LogInformation("耗时任务开始执行");
            // slow async action, e.g. call external api
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(10, cancellationToken);
                
                var m = "No"+i+"_"+Guid.NewGuid()+".done";
                Console.WriteLine(m);
                getmessage.Add("<a href='#'>"+m+"</a><br>");
            }
            return View("MyTask", "任务执行完毕");
        }

        public IActionResult getAjax()
        {
            return Json(getmessage);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
