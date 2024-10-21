using Domain.DTO.User;
using Domain.Interface.Repositories.User;
using Domain.Request.User;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TicketSystemApi.DB;

namespace TicketSystemApi.Repositories.User
{
    public class UserRepository(TicketSystemDbContext ticketSystemDbContext) : IUserRepository
    {
        private readonly TicketSystemDbContext _ticketSystemDbContext = ticketSystemDbContext;
        public async Task<int> CreateUser(CreateUserRequest user)
        {
            try
            {
                var newUser = new DB.User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    RoleId = user.RoleId,
                    DepartmentId = user.DepartmentId,
                };
                await _ticketSystemDbContext.Users.AddAsync(newUser);
                _ticketSystemDbContext.SaveChanges();

                return newUser.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<HttpAuthenticateUserResponse> AuthenticateUser(httpAuthenticateUserRequest user)
        {
            try
            {
                var _user = await (from User in _ticketSystemDbContext.Users.Where(u =>
                             u.Email == user.Email &&
                             u.Password == user.Password)
                                   join role in _ticketSystemDbContext.Roles
                                   on User.RoleId equals role.Id
                                   join department in _ticketSystemDbContext.Departments
                                   on User.DepartmentId equals department.Id
                                   select new HttpAuthenticateUserResponse
                                   {
                                       UserId = User.Id,
                                       Name = User.Name,
                                       Email = User.Email,
                                       Role = role.RoleName,
                                       Department = department.DepartmentName,
                                       RoleId = role.Id,
                                       DepartmentId = department.Id,
                                   }).FirstOrDefaultAsync();

                return _user ?? null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<HttpGetUserResponse>> GetUsers()
        {
            try
            {
                var users = await ( from user in _ticketSystemDbContext.Users
                                    join department in _ticketSystemDbContext.Departments
                                    on user.DepartmentId equals department.Id
                                    join role in _ticketSystemDbContext.Roles
                                    on user.RoleId equals role.Id
                                    select new HttpGetUserResponse
                                    {
                                        Name = user.Name,
                                        Email = user.Email,
                                        Department = department.DepartmentName,
                                        Role = role.RoleName,
                                        Id = user.Id

                                    }).ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> UserExist(string email)
        {
            var user = await _ticketSystemDbContext.Users.FirstOrDefaultAsync(user => user.Email == email);

            return user != null;
        }

        public async Task<IEnumerable<HttpGetUserNamesByDepartmentsResponse>> GetUserNamesByDepartments(int departmentId)
        {
            try
            {
                var _user = await (from User in _ticketSystemDbContext.Users.Where(u =>
                             u.DepartmentId == departmentId)                     
                                  select new HttpGetUserNamesByDepartmentsResponse
                                  {
                                    Id = User.Id,
                                    Name = User.Name,
                                  }).ToListAsync();

                return _user;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<HttpGetUserResponse> GetUser(int id)
        {
            try
            {
                var _user = await (from user in _ticketSystemDbContext.Users
                                   join department in _ticketSystemDbContext.Departments
                                   on user.DepartmentId equals department.Id
                                   join role in _ticketSystemDbContext.Roles
                                   on user.RoleId equals role.Id
                                   select new HttpGetUserResponse
                                   {
                                       Name = user.Name,
                                       Email = user.Email,
                                       Department = department.DepartmentName,
                                       Role = role.RoleName,

                                   }).FirstOrDefaultAsync();
                return _user;

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<int> DeleteUser(int id)
        {
            try
            {
                var userToDelete = await _ticketSystemDbContext.Users
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (userToDelete != null)
                {
                    _ticketSystemDbContext.Users.Remove(userToDelete);
                    await _ticketSystemDbContext.SaveChangesAsync();

                    return userToDelete.Id;
                }
                else
                {
                    throw new Exception("user not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting user: {ex.Message}");
            }
        }
    }
}
