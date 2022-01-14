using Infraestructure.Entity.Model;
using Microsoft.AspNetCore.Mvc;
using MyVet.Domain.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyVet.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<UserEntity> users = _userServices.GetAll();
            return View(users);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            IActionResult response;

            if (id == null)
                response = NotFound();

            UserEntity user = _userServices.GetUser((int)id);
            if (user == null)
            {
                response = NotFound();
            }
            else
            {
                response = View(user);
            }

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEntity user)
        {
            IActionResult response;

            bool result=await _userServices.UpdateUser(user);
            if (result)
            {
                response = RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "No se pudo Actualizar el Usuario");
                response = RedirectToAction(nameof(Index));
            }
            return response;
        }

    }
}
