using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Viewer.Models
{
    [XmlRoot("Issue")]
    public class Issue
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Assigned")]
        public bool Assigned { get; set; }

        [XmlElement("AssignedTo")]
        public string AssignedTo { get; set; }

        [XmlElement("Room")]
        public string Room { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Added")]
        public DateTime Added { get; set; }

        [XmlElement("Changed")]
        public DateTime Changed { get; set; }
    }
}