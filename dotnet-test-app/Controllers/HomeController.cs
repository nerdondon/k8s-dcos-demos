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
            ViewData["POD_NODE_NAME"] = podNodeName;
            ViewData["POD_NAME"] = podName;
            return View();
        }
        
        public IActionResult Error()
        {
            return View();
        }
    }
}
