using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using PushSharp.Google;
using Web.Data;
using Web.Models;
using Web.Models.IssueModels;
using Web.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext context;
        private IHubContext<IndexHub> hub;
        private GcmServiceBroker broker;

        public IssueController(ApplicationDbContext context, IHubContext<IndexHub> hub)
        {
            this.hub = hub;
            this.context = context;
            var config = new GcmConfiguration("915208635165-f4kbjf9f2hrc8t5p5rncbim11465j2hg", "AIzaSyDP4et_tj70DN-lihb3xGwKlOuSRIImtFI", null);

            // Create a new broker
            var gcmBroker = new GcmServiceBroker(config);
            gcmBroker.Start();
        }



        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles ="Admin,TechnicalUser")]
        public IActionResult Save(IssueFormModel issue)
        {
            if(ModelState.IsValid)
            {
                var item = context.Issues.Find(issue.Id);
                item.LastUpdate = DateTime.Now;
                item.State = issue.State;
                item.AssignedTo = User.Identity.Name;
                context.Issues.Update(item);
                context.SaveChanges();
                hub.Clients.All.InvokeAsync("refresh");
            }
            return RedirectToAction("Index", "Home", null);
        }

        [HttpGet, Authorize(Roles = "Room")]
        public ActionResult CreateIssue()
        {
            return View();
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost, Authorize(Roles = "Room")]
        public IActionResult CreateIssue(NewIssueFormModel model)
        {
            if(ModelState.IsValid)
            {
                context.Issues.Add(new Issue
                {
                    Description = model.Description,
                    Room = User.Identity.Name,
                    LastUpdate = DateTime.Now,
                    ReportDate = DateTime.Now,
                    State = "New"
                });
                context.SaveChanges();
                hub.Clients.All.InvokeAsync("refresh");
                return RedirectToAction("AddedSuccesfully");
            }
            return View(model);
        }

        public ActionResult AddedSuccesfully()
        {
            return View();
        }
    }
}
