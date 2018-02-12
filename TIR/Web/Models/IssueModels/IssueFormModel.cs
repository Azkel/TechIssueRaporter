using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.IssueModels
{
    public class IssueFormModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public string AssignedTo { get; set; }

        public DateTime LastUpdate { get; set; }

        public string State { get; set; }

        public List<SelectListItem> States { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Biorę się za to", Value = "Biorę się za to" },
            new SelectListItem { Text = "Odjebane perfekcyjnie", Value = "Odjebane perfekcyjnie"},
            new SelectListItem { Text = "Anulowane", Value = "Anulowane"},
            new SelectListItem { Text = "Coś mi wypadło, niech ktoś inny weźmie", Value = "Coś mi wypadło, niech ktoś inny weźmie"},
        };
    }
}
