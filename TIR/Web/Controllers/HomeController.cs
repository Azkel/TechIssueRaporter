using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IHubContext<IndexHub> hub;

        public HomeController(IHubContext<IndexHub> hub) => this.hub = hub;

        public IActionResult Index()
        {
            return View();
        }
        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Test()
        {
            hub.Clients.All.InvokeAsync("refresh");
            return View();
        }
    }
}
