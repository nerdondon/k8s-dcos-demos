using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_test_app.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var podNodeName = Environment.GetEnvironmentVariable("POD_NODE_NAME");
            var podName = Environment.GetEnvironmentVariable("POD_NAME");
            ViewData["POD_NODE_NAME"] = podNodeName ?? "node name not found";
            ViewData["POD_NAME"] = podName ?? "pod name not found";
            var x = 0.0001;
            //500000
            long iterations = 5000000;
            for(long i = 0; i <= iterations; i++) {
                x += Math.Sqrt(x);
            }
            ViewData["CALC"] = x;
            ViewData["ITERATIONS"] = iterations;
            return View();
        }
        
        public IActionResult Error()
        {
            return View();
        }
    }
}
