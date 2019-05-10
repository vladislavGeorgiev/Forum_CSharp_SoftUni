using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ForumDbContext context;

        public HomeController(ForumDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {

            List<Category> categories = context.Categories
                .Include(c => c.Author)
                .Include(c => c.Topics)
                .ThenInclude(t => t.Author).ToList();


            //get all topics and pass them to the view 
            List<Topic> topics = context.Topics
                .Include(t => t.Author)
                .Include(t=>t.Comments)
                .ThenInclude(c=> c.Author)
                .OrderByDescending(t => t.CreatedDate)
                .ThenByDescending(t => t.LastUpdatedDate)
                .ToList();

            ViewData["Categories"] = categories;

            return View(topics);
        }

      

     

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
