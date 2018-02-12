using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;
using Web.Models.IssueModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext context;

        public IssueController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public JsonResult AddNewIssue(NewIssueFormModel model)
        {
            Issue newIssue = new Issue()
            {
                ReportDate = DateTime.Now,
                Description = model.Description,
                Room = model.Room
            };
            context.Issues.Add(newIssue);
            context.SaveChanges();
            return new JsonResult(true);
        } 

        [HttpPost, ValidateAntiForgeryToken]
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
            }
            return RedirectToAction("Index", "Home", null);
        }
    }
}
