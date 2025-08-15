using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Controllers
{
    public class ProjectsController : Controller
    {
        private List<Project> GetProjects()
        {
            return new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Title = "E-Commerce Website",
                    Description = "A full-featured online shopping platform with user authentication, product catalog, shopping cart, and payment integration. Built with modern web technologies.",
                    ImageUrl = "/Content/Images/project1.jpg",
                    TechnologyUsed = "ASP.NET MVC, C#, Bootstrap, jQuery, SQL Server",
                    CreatedDate = DateTime.Now.AddMonths(-6),
                    Category = "Web Application",
                    ProjectUrl = "https://github.com/yourname/ecommerce"
                },
                new Project
                {
                    Id = 2,
                    Title = "Task Management System",
                    Description = "A comprehensive project management tool that helps teams organize tasks, track progress, and collaborate effectively. Features include task assignment, deadline tracking, and team communication.",
                    ImageUrl = "/Content/Images/project2.jpg",
                    TechnologyUsed = "ASP.NET MVC, Entity Framework, Bootstrap, Chart.js",
                    CreatedDate = DateTime.Now.AddMonths(-4),
                    Category = "Productivity App",
                    ProjectUrl = "https://github.com/yourname/taskmanager"
                },
                new Project
                {
                    Id = 3,
                    Title = "Weather Dashboard",
                    Description = "An interactive weather application that provides real-time weather data, forecasts, and weather maps. Users can search for any location and get detailed weather information.",
                    ImageUrl = "/Content/Images/project3.jpg",
                    TechnologyUsed = "HTML5, CSS3, JavaScript, Weather API, Chart.js",
                    CreatedDate = DateTime.Now.AddMonths(-2),
                    Category = "Web Application",
                    ProjectUrl = "https://github.com/yourname/weather-app"
                },
                new Project
                {
                    Id = 4,
                    Title = "Portfolio Website",
                    Description = "A responsive personal portfolio website showcasing projects, skills, and experience. Features a modern design with smooth animations and mobile-first approach.",
                    ImageUrl = "/Content/Images/project4.jpg",
                    TechnologyUsed = "HTML5, CSS3, JavaScript, Bootstrap, SASS",
                    CreatedDate = DateTime.Now.AddMonths(-1),
                    Category = "Portfolio",
                    ProjectUrl = "https://github.com/yourname/portfolio"
                }
            };
        }

        public ActionResult Index()
        {
            // <CHANGE> Updated to use session data for user-submitted projects
            var sessionProjects = Session["AllProjects"] as List<Project>;
            var projects = sessionProjects ?? GetProjects();
            ViewBag.Title = "Community Projects";
            return View(projects);
        }

        public ActionResult Details(int id)
        {
            // <CHANGE> Updated to check session data first
            var sessionProjects = Session["AllProjects"] as List<Project>;
            var projects = sessionProjects ?? GetProjects();
            var project = projects.FirstOrDefault(p => p.Id == id);

            if (project == null)
                return HttpNotFound();

            ViewBag.Title = project.Title;
            return View(project);
        }

        // <CHANGE> Added Create method for GET requests
        public ActionResult Create()
        {
            ViewBag.Title = "Share Your Project";
            return View(new Project());
        }

        // <CHANGE> Added Create method for POST requests to handle form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project model)
        {
            if (ModelState.IsValid)
            {
                var projects = Session["AllProjects"] as List<Project> ?? GetProjects().ToList();

                model.Id = new Random().Next(1000, 9999);
                model.CreatedDate = DateTime.Now;
                model.ImageUrl = model.ImageUrl ?? "/Content/Images/default-project.jpg";

                projects.Add(model);
                Session["AllProjects"] = projects;

                TempData["Success"] = "Project shared successfully!";
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Share Your Project";
            return View(model);
        }
    }
}