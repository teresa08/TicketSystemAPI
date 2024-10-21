using Domain.DTO.Ticket;
using Domain.Request.Ticket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystemApi.Repositories.Ticket;

namespace Domain.Interface.Repositories.Ticket
{
    public interface ITicketRepository
    {
        public Task<int> CreateTicket(CreateTicketRequest ticket);
        public Task<IEnumerable<HttpGetTicketResponse>> GetTickets();
        public Task<int> UpdateTicket(int id, CreateTicketRequest ticket);
        public Task<int> DeleteTicket(int id);
        public Task<HttpGetPropertyCreateTicketResponse> GetAllPriority();
        public Task<HttpGetPropertyCreateUserResponse> GetPropertyCreateUser();

        public Task<IEnumerable<HttpGetTicketResponse>> GetAllTicketByUser(int UserId);
        public Task<HttpGetTicketResponse> GetTicket(int id);

        public Task<IEnumerable<HttpGetAllStateResponse>> GetAllState();
    }
}
