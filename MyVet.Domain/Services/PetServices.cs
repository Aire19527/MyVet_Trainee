using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model.Vet;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVet.Domain.Services
{
    public class PetServices : IPetServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion


        #region Builder
        public PetServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion


        #region Methods
        public List<PetDto> GetAllMyPets(int idUser)
        {
            var pets = _unitOfWork.PetRepository.FindAll(x => x.UserPetEntity.IdUser == idUser,
                                                        p => p.UserPetEntity,
                                                        p => p.SexEntity,
                                                        p => p.TypePetEntity).ToList();

            List<PetDto> list = pets.Select(x => new PetDto
            {
                DateBorns = x.DateBorns,
                Id = x.Id,
                Name = x.Name,
                IdTypePet = x.IdTypePet,
                IdSex = x.IdSex,
                Sexo = x.SexEntity.Sex,
                TypePet = x.TypePetEntity.TypePet,
                Edad = CalculateAge(x.DateBorns)

            }).ToList();


            return list;
        }

        private string CalculateAge(DateTime dateBorn)
        {
            int meses = Math.Abs((DateTime.Now.Month - dateBorn.Month) + 12 * (DateTime.Now.Year - dateBorn.Year));

            return $"{meses} meses";
        }

        public List<TypePetDto> GetAllTypePe()
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


        public async Task<ResponseDto> DeletePet(int idPet)
        {
            ResponseDto response = new ResponseDto();

            _unitOfWork.PetRepository.Delete(idPet);
            response.IsSuccess = await _unitOfWork.Save() > 0;
            if (response.IsSuccess)
                response.Message = "Se elminnó Correctamente la Mascota";
            else
                response.Message = "Hubo un error al eliminar la Mascota, por favor vuelva a intentalo";

            return response;
        }

        #endregion
    }
}
