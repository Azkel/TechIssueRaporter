using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.IssueModels
{
    public class NewIssueFormModel
    {
        public string Room { get; set; }

        [Required(), MinLength(20)]
        public string Description { get; set; }
    }
}
