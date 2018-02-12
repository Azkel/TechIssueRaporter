using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Issue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AssignedTo { get; set; }

        public string Room { get; set; }

        public string Description { get; set; }

        public DateTime ReportDate { get; set; }

        public DateTime LastUpdate { get; set; }

        public string State { get; set; }
    }
}
