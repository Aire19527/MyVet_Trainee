using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model;
using MyVet.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVet.Domain.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<UserEntity> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll().ToList();
        }

        public UserEntity GetUser(int idUser)
        {
            return _unitOfWork.UserRepository.FirstOrDefault(x=>x.IdUser == idUser);
        }  
        
        public async Task<bool> UpdateUser(UserEntity user)
        {
            UserEntity _user= GetUser(user.IdUser);

            _user.Name=user.Name;
            _user.LastName=user.LastName;
            _unitOfWork.UserRepository.Update(_user);

            return await _unitOfWork.Save() > 0;


            //return _unitOfWork.UserRepository.FirstOrDefault(x=>x.IdUser == idUser);
        }
    }
}
