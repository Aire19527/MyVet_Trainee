using MyVet.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyVet.Domain.Services.Interface
{
    public interface IPetServices
    {
        List<PetDto> GetAllMyPets(int idUser);
        List<TypePetDto> GetAllTypePet();
        List<SexDto> GetAllSexs();
        Task<ResponseDto> DeletePet(int idPet);
    }
}
