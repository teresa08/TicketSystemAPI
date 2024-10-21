using Domain.DTO;
using Domain.DTO.Category;
using Domain.DTO.Department;
using Domain.DTO.Ticket;
using Domain.Request.Category;
using Domain.Request.Department;
using Domain.Request.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.UseCase.Ticket
{
    public interface ITicketCase
    {
        public Task<MessagePayload<string>> CreateTicket(CreateTicketRequest ticket);
        public Task<MessagePayload<HttpGetPropertyCreateTicketResponse>> GetPropertyCreateTicket();
        public Task<MessagePayload<IEnumerable<HttpGetTicketResponse>>> GetTickets();
        public Task<MessagePayload<HttpGetPropertyCreateUserResponse>> GetPropertyCreateUser();
        public Task<MessagePayload<IEnumerable<HttpGetTicketResponse>>> GetAllTicketByUser(int UserId);
        public Task<MessagePayload<HttpGetTicketResponse>> GetTicket(int id);
        public Task<MessagePayload<int>> DeleteTicket(int id);
        public Task<MessagePayload<int>> UpdateTicket(int id, CreateTicketRequest ticket);
        public Task<MessagePayload<IEnumerable<HttpGetAllStateResponse>>> GetAllState();

    }
}
