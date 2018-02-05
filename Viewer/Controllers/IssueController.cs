using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;
using Viewer.Models;
using Viewer.SignalR;

namespace Viewer.Controllers
{
    public class IssueController : ApiController
    {
        private IIssueHub issueHub;

        public IssueController(IIssueHub issuehub)
        {
            this.issueHub = issuehub;
        }

        // GET: api/Issue
        public IEnumerable<string> Get()
        {
            Save("dupa.xml", new Issue()
            {
                Added = DateTime.Now,
                Assigned = false,
                AssignedTo = "Azkel",
                Changed = DateTime.Now,
                Description = "Plz help",
                Id = 1,
                Room = "404"
            });
            var context = GlobalHost.ConnectionManager.GetHubContext<IssueHub>();
            context.Clients.All.refreshWindow();
            return new string[] { "value1", "value2" };
        }
       
        public void Save(string FileName, Issue issue)
        {
            using (var writer = new FileStream(FileName, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<Issue>),
                    new XmlRootAttribute("user_list"));
                List<Issue> list = new List<Issue>();
                list.Add(issue);
                ser.Serialize(writer, list);
            }
        }
    }
}
