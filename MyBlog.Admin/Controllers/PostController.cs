using Microsoft.AspNetCore.Mvc;
using MyBlog.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Admin.Controllers
{
    public class PostController : Controller
    {
        public IActionResult List()
        {
            return View();
        }


        public IActionResult Detail(int id)
        {
            return View();
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(PostViewModel model)
        {
            return View();
        }


        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PostViewModel model)
        {
            return View();
        }


        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
