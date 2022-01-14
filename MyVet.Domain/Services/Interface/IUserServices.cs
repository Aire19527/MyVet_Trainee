using Infraestructure.Entity.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyVet.Domain.Services.Interface
{
    public interface IUserServices
    {
        List<UserEntity> GetAll();

        UserEntity GetUser(int idUser);

        Task<bool> UpdateUser(UserEntity user);
    }
}
