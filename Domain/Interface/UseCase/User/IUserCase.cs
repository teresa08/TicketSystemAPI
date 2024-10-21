using Domain.DTO;
using Domain.DTO.User;
using Domain.Request.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.UseCase.User
{
    public interface IUserCase
    {
        public Task<MessagePayload<IEnumerable<HttpGetUserNamesByDepartmentsResponse>>> GetUserNamesByDepartments(int departmentId);
        public Task<MessagePayload<IEnumerable<HttpGetUserResponse>>> GetUsers();
        public Task<MessagePayload<HttpGetUserResponse>> AuthenticateUser(httpAuthenticateUserRequest user);
        public Task<MessagePayload<string>> CreateUser(CreateUserRequest user);

        public int ValidateToken(string token);

        public Task<MessagePayload<HttpGetUserResponse>> GetUser(int id);

        public Task<MessagePayload<int>> DeleteUser(int id);

    }
}
