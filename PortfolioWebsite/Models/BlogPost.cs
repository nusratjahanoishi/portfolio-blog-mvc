/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioWebsite.Models
{
    public class BlogPost
    {
    }
}
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string Summary { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}