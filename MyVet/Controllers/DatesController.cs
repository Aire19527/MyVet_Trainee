using Infraestructure.Entity.Model.Vet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using MyVet.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Common.Utils.Constant.Const;

namespace MyVet.Controllers
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class DatesController : Controller
    {
        #region Attributes
        private readonly IDatesServices _datesServices;
        #endregion

        #region Builder
        public DatesController(IDatesServices datesServices)
        {
            _datesServices = datesServices;
        }
        #endregion


        #region Views
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DatesVet()
        {
            return View();
        }

        #endregion


        #region Methods
        [HttpGet]
        public IActionResult GetAllDates()
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser)!.Value;

            List<DatesDto> result = _datesServices.GetAllDates(Convert.ToInt32(idUser));
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllMyDates()
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser)!.Value;

            List<DatesDto> result = _datesServices.GetAllMyDates(Convert.ToInt32(idUser));
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllServices()
        {
            List<ServicesEtntity> result = _datesServices.GetAllServices();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDates(DatesDto dates)
        {
            bool result = await _datesServices.InsertDatesAsync(dates);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDates(DatesDto dates)
        {
            bool result = await _datesServices.UpdateDatesAsync(dates);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDates(int idDates)
        {
            ResponseDto result = await _datesServices.DeleteDatesAsync(idDates);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> CancelDates(int idDates)
        {
            bool result = await _datesServices.CancelDatesAsync(idDates, idUserVet: null);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDatesVet(DatesDto dates)
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;
            dates.IdUserVet = Convert.ToInt32(idUser);

            bool result = await _datesServices.UpdateDatesVetAsync(dates);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> CancelDatesVet(int idDates)
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;

            bool result = await _datesServices.CancelDatesAsync(idDates, Convert.ToInt32(idUser));
            return Ok(result);
        }


        #endregion
    }
}
