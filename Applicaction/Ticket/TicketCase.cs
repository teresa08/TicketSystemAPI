using Domain.DTO;
using Domain.DTO.Category;
using Domain.DTO.Department;
using Domain.DTO.Ticket;
using Domain.Interface.Repositories.Ticket;
using Domain.Interface.UseCase.Ticket;
using Domain.Request.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Applicaction.Ticket
{
    public class TicketCase: ITicketCase
    {
        private ITicketRepository _ticketRepository;
        public TicketCase(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<MessagePayload<string>> CreateTicket(CreateTicketRequest ticket)
        {
            try
            {
               await _ticketRepository.CreateTicket(ticket);
                return new MessagePayload<string>
                {
                    Status = 200,
                    Payload = "Ticket Creado",
                    Response = EResponse.Success,
                };

            }catch (Exception ex)
            {
                return new MessagePayload<string>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<int>> DeleteTicket(int id)
        {
            try
            {
                return new MessagePayload<int>
                {
                    Status = 200,
                    Payload = await _ticketRepository.DeleteTicket(id),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<int>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<IEnumerable<HttpGetAllStateResponse>>> GetAllState()
        {
            try
            {
                return new MessagePayload<IEnumerable<HttpGetAllStateResponse>>
                {
                    Status = 200,
                    Payload = await _ticketRepository.GetAllState(),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<IEnumerable<HttpGetAllStateResponse>>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<IEnumerable<HttpGetTicketResponse>>> GetAllTicketByUser(int UserId)
        {
            try
            {
                return new MessagePayload<IEnumerable<HttpGetTicketResponse>>
                {
                    Status = 200,
                    Payload = await _ticketRepository.GetAllTicketByUser(UserId),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<IEnumerable<HttpGetTicketResponse>>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<HttpGetPropertyCreateTicketResponse>> GetPropertyCreateTicket()
        {
            try
            {
                return new MessagePayload<HttpGetPropertyCreateTicketResponse>
                {
                    Status = 200,
                    Payload = await _ticketRepository.GetAllPriority(),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<HttpGetPropertyCreateTicketResponse>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<HttpGetPropertyCreateUserResponse>> GetPropertyCreateUser()
        {
            try
            {
                return new MessagePayload<HttpGetPropertyCreateUserResponse>
                {
                    Status = 200,
                    Payload = await _ticketRepository.GetPropertyCreateUser(),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<HttpGetPropertyCreateUserResponse>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<HttpGetTicketResponse>> GetTicket(int id)
        {
            try
            {
                return new MessagePayload<HttpGetTicketResponse>
                {
                    Status = 200,
                    Payload = await _ticketRepository.GetTicket(id),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<HttpGetTicketResponse>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<IEnumerable<HttpGetTicketResponse>>> GetTickets()
        {
            try
            {
                return new MessagePayload<IEnumerable<HttpGetTicketResponse>>
                {
                    Status = 200,
                    Payload = await _ticketRepository.GetTickets(),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<IEnumerable<HttpGetTicketResponse>>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<int>> UpdateTicket(int id, CreateTicketRequest ticket)
        {
            try
            {
                return new MessagePayload<int>
                {
                    Status = 200,
                    Payload = await _ticketRepository.UpdateTicket(id,ticket),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<int>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }
    }
}
