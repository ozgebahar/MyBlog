using Microsoft.AspNetCore.Mvc;
using MyBlog.Admin.Models;
using MyBlog.Data.Entities;
using MyBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Admin.Controllers
{
    public class TagController : BaseController
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IActionResult List()
        {
            var vm = _tagRepository.GetTags().
                Select(x => new TagViewModel()
                { 
                 Name = x.Name,
                 Id = x.Id
                }).ToList();

            return View(vm);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(TagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            var entity = new Tag() 
            {
            Id= model.Id,
            Name = model.Name,
            CreatedById = currentUserId
           
            };

            bool result = _tagRepository.Add(entity);
            if (result)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var uptEntity = _tagRepository.GetTag(id);

            if (uptEntity!= null)
            {
                var vm = new TagViewModel()
                { 
                Name = uptEntity.Name,
                Id= uptEntity.Id
                };

                return View(vm);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            var entity = new Tag()
            { 
            Name = model.Name,
            Id = model.Id,
            UpdatedById = currentUserId
            };

            bool result = _tagRepository.Edit(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var result = _tagRepository.Delete(id);

            TempData["Message"] = result ? "Silme işlemi başarılı." : "Silme işlemi gerçekleşemedi.";

            return RedirectToAction("List");
        }
    }
}
