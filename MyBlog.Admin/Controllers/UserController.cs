using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "admin")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult List()
        {
            var vm = _userRepository.GetUsers().Select(x => new UserViewModel()
            {
                Firstname = x.Name,
                Lastname = x.Surname,
                Email = x.Email,
                Password = x.Password,
                Role = x.Role,
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
        public IActionResult Add(UserViewModel model)
        {
            if (model.Id == 0)
            {
                if (ModelState.ContainsKey("id"))
                    ModelState.Remove("id");

            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            var entity = new User()
            {
                Name = model.Firstname,
                Surname = model.Lastname,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                CreatedById = currentUserId
            };

            bool result = _userRepository.Add(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var uptEntity = _userRepository.GetUser(id);

            if (uptEntity != null)
            {
                var vm = new UserViewModel()
                {
                    Firstname = uptEntity.Name,
                    Lastname = uptEntity.Surname,
                    Email = uptEntity.Email,
                    Password = uptEntity.Password,
                    Role = uptEntity.Password,
                    Id = uptEntity.Id
                };

                return View(vm);

            }

            TempData["Message"] = "Kullanıcı bulunamadı.";

            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var currentUserId = GetCurrentUserId();

            var entity = new User()
            {
                Name = model.Firstname,
                Surname = model.Lastname,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                CreatedById = currentUserId
            };

            bool result = _userRepository.Edit(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var result = _userRepository.Delete(id);

            TempData["Message"] = result ? "İşlem başarılı" : "Silme yapılamadı";

            return RedirectToAction("List");
        }
    }

}

