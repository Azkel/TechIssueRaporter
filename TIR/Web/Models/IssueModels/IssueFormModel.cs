using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.IssueModels
{
    public class IssueFormModel
    {
        [HiddenInput]
        [Key]
        public int Id { get; set; }

        public string AssignedTo { get; set; }

        public DateTime LastUpdate { get; set; }

        public string State { get; set; }

        [NotMapped]
        public static List<SelectListItem> States { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Assigned", Value = "Assigned" },
            new SelectListItem { Text = "Done", Value = "Done"},
            new SelectListItem { Text = "Cancelled", Value = "Cancelled"},
            new SelectListItem { Text = "Something came along, please take it", Value = "Something came along, please take it"},
        };
    }
}
