/*
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioWebsite.Models
{
    public class Project
    {
    }
}  
*/
using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string TechnologyUsed { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ProjectUrl { get; set; }

        public string Category { get; set; }
    }
}