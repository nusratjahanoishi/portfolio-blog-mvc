using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Controllers
{
    public class BlogController : Controller
    {
        private List<BlogPost> GetBlogPosts()
        {
            return new List<BlogPost>
            {
                new BlogPost
                {
                    Id = 1,
                    Title = "Getting Started with ASP.NET MVC",
                    Summary = "Learn the fundamentals of building web applications with ASP.NET MVC framework. This comprehensive guide covers everything from setup to deployment.",
                    Content = @"ASP.NET MVC is a powerful framework for building web applications using the Model-View-Controller architectural pattern. 

In this post, we'll explore the key concepts that make MVC such a popular choice for web development:

**What is MVC?**
MVC stands for Model-View-Controller, which separates your application into three main components:
- Model: Represents the data and business logic
- View: Handles the user interface and presentation
- Controller: Manages user input and coordinates between Model and View

**Benefits of ASP.NET MVC:**
1. Separation of Concerns: Each component has a specific responsibility
2. Testability: Easy to unit test individual components
3. Flexibility: Full control over HTML markup
4. SEO Friendly: Clean URLs and better search engine optimization
5. Scalability: Suitable for large applications

**Getting Started:**
To create your first MVC application, you'll need Visual Studio and the .NET Framework. The framework provides scaffolding tools that help you quickly generate controllers, views, and models.

**Best Practices:**
- Keep controllers thin and focused
- Use strongly-typed views
- Implement proper error handling
- Follow naming conventions
- Use dependency injection for better testability

This is just the beginning of your MVC journey. In upcoming posts, we'll dive deeper into advanced topics like routing, authentication, and deployment strategies.",
                    Author = "John Developer",
                    PublishedDate = DateTime.Now.AddDays(-15),
                    Category = "Web Development"
                },
                new BlogPost
                {
                    Id = 2,
                    Title = "Modern Web Development Best Practices",
                    Summary = "Discover essential practices for creating maintainable, scalable, and user-friendly web applications in today's development landscape.",
                    Content = @"The web development landscape is constantly evolving, and staying up-to-date with best practices is crucial for building successful applications.

**Performance Optimization:**
Performance is key to user experience and SEO rankings. Here are essential optimization techniques:
- Minimize HTTP requests by combining files
- Optimize images and use appropriate formats
- Implement caching strategies
- Use Content Delivery Networks (CDNs)
- Minify CSS and JavaScript files

**Responsive Design:**
With mobile traffic exceeding desktop, responsive design is no longer optional:
- Mobile-first approach
- Flexible grid systems
- Scalable images and media
- Touch-friendly interfaces
- Progressive enhancement

**Security Considerations:**
Security should be built into your application from the ground up:
- Input validation and sanitization
- Protection against SQL injection
- Cross-Site Scripting (XSS) prevention
- Secure authentication and authorization
- HTTPS implementation
- Regular security updates

**Code Quality:**
Maintainable code is essential for long-term project success:
- Follow coding standards and conventions
- Write meaningful comments and documentation
- Implement proper error handling
- Use version control effectively
- Regular code reviews
- Automated testing

**User Experience (UX):**
Great UX leads to higher user satisfaction and conversion rates:
- Intuitive navigation
- Fast loading times
- Clear call-to-actions
- Accessibility compliance
- User feedback and testing

**Tools and Technologies:**
Stay current with modern development tools:
- Task runners and build tools
- CSS preprocessors
- JavaScript frameworks and libraries
- Development environments and IDEs
- Debugging and profiling tools

By following these best practices, you'll create web applications that are not only functional but also maintainable, secure, and user-friendly.",
                    Author = "Sarah Tech",
                    PublishedDate = DateTime.Now.AddDays(-8),
                    Category = "Best Practices"
                },
                new BlogPost
                {
                    Id = 3,
                    Title = "The Future of Web Development",
                    Summary = "Explore emerging trends and technologies that are shaping the future of web development, from AI integration to progressive web apps.",
                    Content = @"Web development continues to evolve at a rapid pace, with new technologies and methodologies emerging regularly. Let's explore what the future holds.

**Artificial Intelligence Integration:**
AI is becoming increasingly integrated into web development:
- Automated code generation and optimization
- Intelligent chatbots and virtual assistants
- Personalized user experiences
- Predictive analytics and recommendations
- Voice and image recognition capabilities

**Progressive Web Apps (PWAs):**
PWAs bridge the gap between web and mobile applications:
- Offline functionality
- Push notifications
- App-like user experience
- Improved performance
- Cross-platform compatibility

**Serverless Architecture:**
Serverless computing is changing how we build and deploy applications:
- Reduced infrastructure management
- Automatic scaling
- Cost-effective for variable workloads
- Faster development cycles
- Focus on business logic rather than server management

**WebAssembly (WASM):**
WebAssembly enables high-performance applications in the browser:
- Near-native performance
- Support for multiple programming languages
- Complex applications in the browser
- Gaming and multimedia applications
- Scientific computing and data visualization

**Micro-Frontends:**
Breaking down monolithic frontend applications:
- Independent development and deployment
- Technology diversity
- Team autonomy
- Scalable architecture
- Easier maintenance and updates

**Enhanced Developer Experience:**
Tools and workflows continue to improve:
- Hot reloading and instant feedback
- Better debugging tools
- Improved IDE support
- Automated testing and deployment
- Low-code and no-code platforms

**Sustainability in Web Development:**
Environmental consciousness is becoming important:
- Green hosting solutions
- Optimized code for energy efficiency
- Sustainable design practices
- Carbon footprint awareness
- Eco-friendly development workflows

The future of web development is exciting, with technologies that will enable us to create more powerful, efficient, and user-friendly applications than ever before.",
                    Author = "Mike Future",
                    PublishedDate = DateTime.Now.AddDays(-3),
                    Category = "Technology Trends"
                }
            };
        }

        public ActionResult Index()
        {
            // <CHANGE> Updated to use session data for user-submitted blog posts
            var sessionPosts = Session["AllBlogPosts"] as List<BlogPost>;
            var posts = (sessionPosts ?? GetBlogPosts()).OrderByDescending(p => p.PublishedDate).ToList();
            ViewBag.Title = "Community Blog";
            return View(posts);
        }

        public ActionResult Details(int id)
        {
            // <CHANGE> Updated to check session data first
            var sessionPosts = Session["AllBlogPosts"] as List<BlogPost>;
            var posts = sessionPosts ?? GetBlogPosts();
            var post = posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return HttpNotFound();

            // Get comments from session
            var sessionKey = $"Comments_{id}";
            var comments = Session[sessionKey] as List<Comment> ?? new List<Comment>();
            post.Comments = comments.OrderByDescending(c => c.CreatedDate).ToList();

            ViewBag.Title = post.Title;
            return View(post);
        }

        // <CHANGE> Added Create method for GET requests
        public ActionResult Create()
        {
            ViewBag.Title = "Write a Blog Post";
            return View(new BlogPost());
        }

        // <CHANGE> Added Create method for POST requests to handle form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogPost model, string authorName)
        {
            if (ModelState.IsValid)
            {
                var posts = Session["AllBlogPosts"] as List<BlogPost> ?? GetBlogPosts().ToList();

                model.Id = new Random().Next(1000, 9999);
                model.PublishedDate = DateTime.Now;
                model.Author = !string.IsNullOrEmpty(authorName) ? authorName : "Anonymous";
                model.Comments = new List<Comment>();

                posts.Insert(0, model); // Add to beginning
                Session["AllBlogPosts"] = posts;

                TempData["Success"] = "Blog post published successfully!";
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Write a Blog Post";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(int blogPostId, Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Id = new Random().Next(1000, 9999);
                comment.CreatedDate = DateTime.Now;
                comment.BlogPostId = blogPostId;

                var sessionKey = $"Comments_{blogPostId}";
                var comments = Session[sessionKey] as List<Comment> ?? new List<Comment>();
                comments.Add(comment);
                Session[sessionKey] = comments;

                TempData["Success"] = "Comment added successfully!";
            }
            else
            {
                TempData["Error"] = "Please fill in all required fields correctly.";
            }

            return RedirectToAction("Details", new { id = blogPostId });
        }
    }
}