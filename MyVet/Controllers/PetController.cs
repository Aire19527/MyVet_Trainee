using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Common.Utils.Constant.Const;

namespace MyVet.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
        #region Attribute
        private readonly IPetServices _petServices;
        #endregion

        #region Buider
        public PetController(IPetServices petServices)
        {
            _petServices = petServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public IActionResult Index()
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;

            List<PetDto> list = _petServices.GetAllMyPets(Convert.ToInt32(idUser));
            return View(list);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePet(int idPet)
        {
            ResponseDto response = await _petServices.DeletePet(idPet);
            return Ok(response);
        }
        #endregion
    }
}
