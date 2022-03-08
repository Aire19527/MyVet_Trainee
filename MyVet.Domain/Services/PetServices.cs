using Common.Utils.RestServices.Interface;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model.Vet;
using Microsoft.Extensions.Configuration;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVet.Domain.Services
{
    public class PetServices : IPetServices
    {
        #region Attribute
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestService _restService;
        private readonly IConfiguration _config;
        #endregion

        #region Builder
        public PetServices(IUnitOfWork unitOfWork, IRestService restService, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _restService = restService;
            _config = config;
        }
        #endregion

        #region Methods

        
        public async Task<ResponseDto> GetAllMyPets(string token)
        {
            string urlBase = _config.GetSection("ApiMyVet").GetSection("UrlBase").Value;
            string controller = _config.GetSection("ApiMyVet").GetSection("ControlerPet").Value;
            string method = _config.GetSection("ApiMyVet").GetSection("MethodGetAllMyPets").Value;


            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Token", token);

            ResponseDto response = await _restService.GetRestServiceAsync<ResponseDto>(urlBase, controller, method, parameters, headers);
            if (response.IsSuccess)
                response.Result = JsonConvert.DeserializeObject<List<PetDto>>(response.Result.ToString());

            return response;
            //if (response.IsSuccess)
            //{
            //    return JsonConvert.DeserializeObject<List<PetDto>>(response.Result.ToString());
            //}


            //return new List<PetDto>();



            //var pets = _unitOfWork.PetRepository.FindAll(x => x.UserPetEntity.IdUser == idUser,
            //                                            p => p.UserPetEntity,
            //                                            p => p.SexEntity,
            //                                            p => p.TypePetEntity).ToList();

            //List<PetDto> list = pets.Select(x => new PetDto
            //{
            //    DateBorns = x.DateBorns,
            //    Id = x.Id,
            //    Name = x.Name,
            //    IdTypePet = x.IdTypePet,
            //    IdSex = x.IdSex,
            //    Sexo = x.SexEntity.Sex,
            //    TypePet = x.TypePetEntity.TypePet,
            //    Edad = CalculateAge(x.DateBorns)

            //}).ToList();


            //return list;
        }
        //public async Task<List<PetDto>> GetAllMyPets(string token)
        //{
        //    string urlBase = _config.GetSection("ApiMyVet").GetSection("UrlBase").Value;
        //    string controller = _config.GetSection("ApiMyVet").GetSection("ControlerPet").Value;
        //    string method = _config.GetSection("ApiMyVet").GetSection("MethodGetAllMyPets").Value;


        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    Dictionary<string, string> headers = new Dictionary<string, string>();
        //    headers.Add("Token", token);

        //    ResponseDto response = await _restService.GetRestServiceAsync<ResponseDto>(urlBase, controller, method, parameters, headers);

        //    if (response.IsSuccess)
        //    {
        //        return JsonConvert.DeserializeObject<List<PetDto>>(response.Result.ToString());
        //    }


        //    return new List<PetDto>();



        //    //var pets = _unitOfWork.PetRepository.FindAll(x => x.UserPetEntity.IdUser == idUser,
        //    //                                            p => p.UserPetEntity,
        //    //                                            p => p.SexEntity,
        //    //                                            p => p.TypePetEntity).ToList();

        //    //List<PetDto> list = pets.Select(x => new PetDto
        //    //{
        //    //    DateBorns = x.DateBorns,
        //    //    Id = x.Id,
        //    //    Name = x.Name,
        //    //    IdTypePet = x.IdTypePet,
        //    //    IdSex = x.IdSex,
        //    //    Sexo = x.SexEntity.Sex,
        //    //    TypePet = x.TypePetEntity.TypePet,
        //    //    Edad = CalculateAge(x.DateBorns)

        //    //}).ToList();


        //    //return list;
        //}

        private string CalculateAge(DateTime dateBorn)
        {
            string result = string.Empty;

            int age = Math.Abs((DateTime.Now.Month - dateBorn.Month) + 12 * (DateTime.Now.Year - dateBorn.Year));

            if (age != 0)
                result = $"{age} meses";
            else
            {
                TimeSpan resultDate = DateTime.Now.Date - dateBorn.Date;
                result = $"{resultDate.Days} días";
            }

            return result;
        }

        public List<TypePetDto> GetAllTypePet()
        {
            List<TypePetEntity> typePets = _unitOfWork.TypePetRepository.GetAll().ToList();

            List<TypePetDto> list = typePets.Select(x => new TypePetDto
            {
                IdTypePet = x.Id,
                TypePet = x.TypePet
            }).ToList();

            return list;
        }

        public List<SexDto> GetAllSexs()
        {
            List<SexEntity> sexs = _unitOfWork.SexRepository.GetAll().ToList();

            List<SexDto> list = sexs.Select(x => new SexDto
            {
                IdSex = x.Id,
                Sex = x.Sex
            }).ToList();

            return list;
        }


        public async Task<ResponseDto> DeletePetAsync(int idPet)
        {
            ResponseDto response = new ResponseDto();

            _unitOfWork.PetRepository.Delete(idPet);
            response.IsSuccess = await _unitOfWork.Save() > 0;
            if (response.IsSuccess)
                response.Message = "Se elminnó correctamente la Mascota";
            else
                response.Message = "Hubo un error al eliminar la Mascota, por favor vuelva a intentalo";

            return response;
        }

        public async Task<bool> InsertPetAsync(PetDto pet)
        {
            //PetEntity petEntity = new PetEntity()
            //{
            //    DateBorns = pet.DateBorns,
            //    IdSex=  pet.IdSex,
            //    IdTypePet= pet.IdTypePet,
            //    Name= pet.Name, 
            //};
            //_unitOfWork.PetRepository.Insert(petEntity);

            //UserPetEntity userPetEntity = new UserPetEntity()
            //{
            //    IdUser=pet.IdUser,
            //    IdPet=petEntity.Id
            //};
            //_unitOfWork.UserPetRepository.Insert(userPetEntity);


            UserPetEntity userPetEntity = new UserPetEntity()
            {
                IdUser = pet.IdUser,
                PetEntity = new PetEntity()
                {
                    DateBorns = pet.DateBorns,
                    IdSex = pet.IdSex,
                    IdTypePet = pet.IdTypePet,
                    Name = pet.Name,
                }
            };

            _unitOfWork.UserPetRepository.Insert(userPetEntity);
            return await _unitOfWork.Save() > 0;
        }


        public async Task<bool> UpdatePetAsync(PetDto pet)
        {
            bool result = false;

            PetEntity petEntity = _unitOfWork.PetRepository.FirstOrDefault(x => x.Id == pet.Id);
            if (petEntity != null)
            {
                petEntity.DateBorns = pet.DateBorns;
                petEntity.IdSex = pet.IdSex;
                petEntity.IdTypePet = pet.IdTypePet;
                petEntity.Name = pet.Name;

                _unitOfWork.PetRepository.Update(petEntity);

                result = await _unitOfWork.Save() > 0;
            }

            return result;
        }
        #endregion
    }
}
