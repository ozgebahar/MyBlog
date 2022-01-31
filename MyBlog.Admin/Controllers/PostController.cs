using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.Admin.Models;
using MyBlog.Data.Entities;
using MyBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Admin.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostRepository _postRepository;

        private readonly ICategoryRepository _categoryRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult List()
        {
            var vm = _postRepository.GetPosts()
                .Select(x => new PostViewModel()
                { 
                Id =x.Id,
                CategoryName = x.Category.CategoryName,
                Title = x.Title,
                IsPublished = x.IsPublished
                }).ToList();

            return View(vm);
        }


        public IActionResult Detail(int id)
        {
            var post = _postRepository.GetPost(id);

            var vm = new PostViewModel()
            {
                CategoryName = post.Category.CategoryName,
                Content = post.Content,
                IsPublished = post.IsPublished,
                PictureStr = Convert.ToBase64String(post.Picture),
                Title = post.Title,
                Tags = string.Join(',', post.PostTags.Select(x => x.Tag.Name))
            };

            return View(vm);
        }


        public IActionResult Add()
        {
            ViewBag.Categories = _categoryRepository.GetCategories()
                .Select(x => new SelectListItem() 
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(PostViewModel model)
        {
            if (model.Id == 0)
            {
                if (ModelState.ContainsKey("Id"))
                    ModelState.Remove("Id");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryRepository.GetCategories()
                 .Select(x => new SelectListItem()
                 {
                     Text = x.CategoryName,
                     Value = x.Id.ToString()
                 }).ToList();

                return View(model);
            }
            ViewBag.Categories = _categoryRepository.GetCategories()
                .Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }).ToList();

            var currentUserId = GetCurrentUserId();

            var post = new Post()
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                Content = model.Content,
                Title = model.Title,
                IsPublished = model.IsPublished,
                CreatedById = currentUserId

            };

            #region Picture

            if (model.Picture.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    model.Picture.CopyTo(ms);
                    post.Picture = ms.ToArray();
                }
            }

            #endregion

            #region Tag

            if (!string.IsNullOrEmpty(model.Tags))
            {
                var tagList = model.Tags.Split(',').ToList();
                post.PostTags = tagList.Select(x => new PostTag()
                {
                    Tag = new Tag() { Name = x.Trim().ToLower() }
                }).ToList();
            }

            #endregion

            bool result = _postRepository.Add(post);

            if (result)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }


        public IActionResult Edit(int id)
        {
            var uptPost = _postRepository.GetPost(id);

            if (uptPost != null)
            {
                ViewBag.Categories = _categoryRepository.GetCategories().Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }).ToList();

                var vm = new PostViewModel()
                {
                    Title = uptPost.Title,
                    Content = uptPost.Content,
                    Tags = string.Join(',', uptPost.PostTags.Select(x => x.Tag.Name)),
                    
                    CategoryId = uptPost.CategoryId,
                    IsPublished = uptPost.IsPublished
                };

                return View(vm);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PostViewModel model)
        {
            if (model.Id == 0)
            {
                if (ModelState.ContainsKey("Id"))
                    ModelState.Remove("Id");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryRepository.GetCategories()
                 .Select(x => new SelectListItem()
                 {
                     Text = x.CategoryName,
                     Value = x.Id.ToString()
                 }).ToList();

                return View(model);
            }
            ViewBag.Categories = _categoryRepository.GetCategories()
                .Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }).ToList();

            var currentUserId = GetCurrentUserId();

            var post = new Post()
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                Content = model.Content,
                Title = model.Title,
                IsPublished = model.IsPublished,
                UpdatedById = currentUserId

            };

            #region Picture

            if (model.Picture.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    model.Picture.CopyTo(ms);
                    post.Picture = ms.ToArray();
                }
            }

            #endregion

            #region Tag

            if (!string.IsNullOrEmpty(model.Tags))
            {
                var tagList = model.Tags.Split(',').ToList();
                post.PostTags = tagList.Select(x => new PostTag()
                {
                    Tag = new Tag() { Name = x.Trim().ToLower() }
                }).ToList();
            }

            #endregion

            bool result = _postRepository.Edit(post);

            if (result)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }


        public IActionResult Delete(int id)
        {
            var result = _postRepository.Delete(id);

            TempData["Message"] = result ? "Silindi" : "İşlem başarısız oldu";

            return RedirectToAction("List");
        }
    }
}
