using MyVet.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyVet.Domain.Services.Interface
{
    public interface IPetServices
    {
        //Task<List<PetDto>> GetAllMyPets(string token);
        Task<ResponseDto> GetAllMyPets(string token);
        List<TypePetDto> GetAllTypePet();
        List<SexDto> GetAllSexs();
        Task<ResponseDto> DeletePetAsync(int idPet);

        Task<bool> InsertPetAsync(PetDto pet);

        Task<bool> UpdatePetAsync(PetDto pet);
    }
}
