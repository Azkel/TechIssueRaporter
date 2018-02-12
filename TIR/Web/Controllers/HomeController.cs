using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Web.Data;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IHubContext<IndexHub> hub;
        private readonly ApplicationDbContext context;

        public HomeController(IHubContext<IndexHub> hub, ApplicationDbContext context){
            this.hub = hub;
            this.context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            if(User.IsInRole("Room"))
            {
                return RedirectToAction("CreateIssue", "Issue", null);
            }
            return View(context.Issues.ToList().OrderByDescending(x => x.ReportDate));
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult Test()
        //{
        //    hub.Clients.All.InvokeAsync("refresh");
        //    return View();
        //}
    }
}
