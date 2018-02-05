using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using Viewer.Models;

namespace Viewer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Issue> users;
            using (var reader = new StreamReader("dupa.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<Issue>),
                    new XmlRootAttribute("user_list"));
                users = (List<Issue>)deserializer.Deserialize(reader);
            }
            return View(users);
        }
    }
}