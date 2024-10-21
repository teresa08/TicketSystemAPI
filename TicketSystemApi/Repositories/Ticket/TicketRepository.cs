using Domain.DTO.Category;
using Domain.DTO.Department;
using Domain.DTO.Ticket;
using Domain.Interface.Repositories.Ticket;
using Domain.Request.Ticket;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using TicketSystemApi.DB;

namespace TicketSystemApi.Repositories.Ticket
{
    public class TicketRepository(TicketSystemDbContext ticketSystemDbContext) : ITicketRepository
    {
        private readonly TicketSystemDbContext _ticketSystemDbContext = ticketSystemDbContext;

  
        public async Task<int> CreateTicket(CreateTicketRequest ticket)
        {
            try
            {
                var newTicket = await _ticketSystemDbContext.Tickets.AddAsync(new DB.Ticket
                {
                    Title = ticket.Title,
                    Description = ticket.Description,
                    CategoryId = ticket.CategoryId,
                    PriorityId = ticket.PriorityId,
                    DepartmentId = ticket.DepartmentId,
                    UserId = ticket.UserId,
                    StateId = ticket.StateId

                });
                _ticketSystemDbContext.SaveChanges();
               return newTicket.Entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<int> DeleteTicket(int id)
        {
            try
            {
                var ticketsToDelete = await _ticketSystemDbContext.Tickets
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (ticketsToDelete != null)
                {
                    _ticketSystemDbContext.Tickets.Remove(ticketsToDelete);
                    await _ticketSystemDbContext.SaveChangesAsync();

                    return ticketsToDelete.Id;
                }
                else
                {
                    throw new Exception("department not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting department: {ex.Message}");
            }
        }


        public async Task<HttpGetPropertyCreateTicketResponse> GetAllPriority()
        {
            try
            {
                var priority = await _ticketSystemDbContext.Priorities.ToListAsync();
                var priorityResponses = priority.Select(c => new HttpGetAllPriorityNameResponse
                {
                    Id = c.Id,
                    PriorityName = c.PriorityName,
                }).ToList();

                var categories = await _ticketSystemDbContext.Categories.ToListAsync();
                var categoriesResponses = categories.Select(c => new HttpGetAllCategoryNameResponse
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                }).ToList();

                var department = await _ticketSystemDbContext.Departments.ToListAsync();
                var departmentResponses = department.Select(c => new HttpGetAllDepartmentNameResponse
                {
                    Id = c.Id,
                    DepartmentName = c.DepartmentName,
                }).ToList();

                return new HttpGetPropertyCreateTicketResponse
                {
                    PriorityNames = priorityResponses,
                    CategoryNames = categoriesResponses,
                    DepartmentNames = departmentResponses
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<HttpGetAllStateResponse>> GetAllState()
        {
            try
            {
                var states = await(from _states in _ticketSystemDbContext.States
                                         select new HttpGetAllStateResponse
                                         {
                                             Id =_states.Id,
                                             StateName = _states.StateName,
                                         }).ToArrayAsync();

                return states;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<HttpGetTicketResponse>> GetAllTicketByUser(int UserId)
        {
            try
            {
                var tickets = await (from _ticket in _ticketSystemDbContext.Tickets
                                     where _ticket.UserId == UserId
                                     join category in _ticketSystemDbContext.Categories
                                                 on _ticket.CategoryId equals category.Id
                                     join state in _ticketSystemDbContext.States
                                         on _ticket.StateId equals state.Id
                                     join user in _ticketSystemDbContext.Users
                                          on _ticket.UserId equals user.Id into userJoin
                                     from user in userJoin.DefaultIfEmpty()
                                     join priority in _ticketSystemDbContext.Priorities
                                     on _ticket.PriorityId equals priority.Id
                                     join department in _ticketSystemDbContext.Departments
                                     on _ticket.DepartmentId equals department.Id
                                     select new HttpGetTicketResponse
                                     {
                                         Id = _ticket.Id,
                                         Title = _ticket.Title,
                                         Description = _ticket.Description,
                                         Priority = priority.PriorityName,
                                         Category = category.CategoryName,
                                         State = state.StateName,
                                         Date = _ticket.CreationDate,
                                         User = user.Name,
                                         Department = department.DepartmentName
                                     }).ToListAsync();
                return tickets;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<HttpGetPropertyCreateUserResponse> GetPropertyCreateUser()
        {
            try
            {
                var DepartmentNames = await _ticketSystemDbContext.Departments.ToListAsync();
                var RolesNames = await _ticketSystemDbContext.Roles.ToListAsync();

                return new HttpGetPropertyCreateUserResponse
                {

                    DepartmentNames = DepartmentNames.Select(d => new HttpGetAllDepartmentNameResponse
                    {
                        DepartmentName = d.DepartmentName,
                        Id = d.Id,
                    }).ToList(),
                    RoleNames = RolesNames.Select(d => new HttpGetAllRoleNameResponse
                    {
                        Id = d.Id,
                        RoleName = d.RoleName,
                    }).ToList(),
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<HttpGetTicketResponse> GetTicket(int id)
        {
            try
            {
                var ticket = await (from _ticket in _ticketSystemDbContext.Tickets
                                    where _ticket.Id == id
                                    join category in _ticketSystemDbContext.Categories
                                                          on _ticket.CategoryId equals category.Id
                                    join state in _ticketSystemDbContext.States
                                        on _ticket.StateId equals state.Id
                                    join user in _ticketSystemDbContext.Users
                                         on _ticket.UserId equals user.Id into userJoin
                                    from user in userJoin.DefaultIfEmpty()
                                    join priority in _ticketSystemDbContext.Priorities
                                    on _ticket.PriorityId equals priority.Id
                                    join department in _ticketSystemDbContext.Departments
                                    on _ticket.DepartmentId equals department.Id

                                    select new HttpGetTicketResponse
                                    {
                                        Id = _ticket.Id,
                                        Title = _ticket.Title,
                                        Description = _ticket.Description,
                                        Priority = priority.PriorityName,
                                        Category = category.CategoryName,
                                        State = state.StateName,
                                        Date = _ticket.CreationDate,
                                        User = user.Name,
                                        Department = department.DepartmentName,
                                        CategoryId = _ticket.CategoryId,
                                        DepartmentId = _ticket.CategoryId,
                                        PriorityId = _ticket.PriorityId,
                                        StateId = _ticket.StateId,
                                        UserId = _ticket.User.Id,
                                    }).FirstOrDefaultAsync();

                return ticket;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<HttpGetTicketResponse>> GetTickets()
        {
            try
            {
                var result = await (from ticket in _ticketSystemDbContext.Tickets
                                    join category in _ticketSystemDbContext.Categories
                                        on ticket.CategoryId equals category.Id
                                    join state in _ticketSystemDbContext.States
                                        on ticket.StateId equals state.Id
                                    join user in _ticketSystemDbContext.Users
                                         on ticket.UserId equals user.Id into userJoin
                                    from user in userJoin.DefaultIfEmpty()
                                    join priority in _ticketSystemDbContext.Priorities
                                    on ticket.PriorityId equals priority.Id
                                    join department in _ticketSystemDbContext.Departments
                                    on ticket.DepartmentId equals department.Id
                                    select new HttpGetTicketResponse
                                    {
                                        Id = ticket.Id,
                                        Title = ticket.Title,
                                        Description = ticket.Description,
                                        Priority = priority.PriorityName,
                                        Category = category.CategoryName,
                                        State = state.StateName,
                                        Date = ticket.CreationDate,
                                        User = user.Name,
                                        Department = department.DepartmentName
                                    }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<int> UpdateTicket(int id, CreateTicketRequest ticket)
        {
            try
            {
                var updateticket = await(from _ticket in _ticketSystemDbContext.Tickets
                                             where _ticket.Id == id
                                             select _ticket).FirstOrDefaultAsync();

                if (updateticket != null)
                {
                    updateticket.Title = ticket.Title;
                    updateticket.StateId = ticket.StateId;
                    updateticket.UserId = ticket.UserId;
                    updateticket.DepartmentId = ticket.DepartmentId;
                    updateticket.CategoryId = ticket.CategoryId;
                    updateticket.Description = ticket.Description;
                    updateticket.PriorityId = ticket.PriorityId;
              
                    await _ticketSystemDbContext.SaveChangesAsync();
                }
                return updateticket.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
