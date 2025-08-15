/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioWebsite.Models
{
    public class Comment
    {
    }
}
*/
using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        [StringLength(500)]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public int BlogPostId { get; set; }
    }
}