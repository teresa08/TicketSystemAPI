using Domain.DTO;
using Domain.DTO.User;
using Domain.Request.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Repositories.User
{
    public interface IUserRepository
    {
        public Task<IEnumerable<HttpGetUserResponse>> GetUsers();

        public Task<IEnumerable<HttpGetUserNamesByDepartmentsResponse>> GetUserNamesByDepartments(int departmentId);
        public Task<HttpAuthenticateUserResponse> AuthenticateUser(httpAuthenticateUserRequest user);

        public Task<int> CreateUser(CreateUserRequest user);

        public Task<bool> UserExist(string email);

        public Task<HttpGetUserResponse> GetUser(int id);

        public Task<int> DeleteUser(int id);

    }
}
