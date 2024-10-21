using Domain.DTO;
using Domain.DTO.Ticket;
using Domain.Interface.Services;
using Domain.Interface.UseCase.Ticket;
using Domain.Request.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace TicketSystemApi.Controllers
{
    [ApiController]
    [Route("")]
    public class TicketController : Controller
    {
        private ITicketCase _ticketCase;
        private ITokenService _tokenService;
        public TicketController(ITicketCase ticketCase, ITokenService tokenService)
        {
            _ticketCase = ticketCase;
            _tokenService = tokenService;

        }

        [HttpPost("new-ticket")]
        [Authorize]

        public async Task<ActionResult<MessagePayload<string>>> CreateTicket([FromBody] CreateTicketRequest ticket)
        {
            try
            {
                var response = await _ticketCase.CreateTicket(ticket);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<string>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        [HttpGet("property-ticket")]

        public async Task<ActionResult<MessagePayload<HttpGetPropertyCreateTicketResponse>>> GetPropertyCreateTicket()
        {
            try
            {
                var response = await _ticketCase.GetPropertyCreateTicket();
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<HttpGetPropertyCreateTicketResponse>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }


        [HttpGet("ticket")]
        [Authorize]
        public async Task<ActionResult<MessagePayload<IEnumerable<HttpGetTicketResponse>>>> GetTickets()
        {
            try
            {
                var userClaims = User.Claims;
                var _RoleId = _tokenService.GetObjectFromToken(userClaims).RoleId;
                var _UserId = _tokenService.GetObjectFromToken(userClaims).UserId;
                if (_RoleId == 2)
                {
                    var resp = await _ticketCase.GetTickets();
                    return resp.Status == 200 ? Ok(resp) : StatusCode(resp.Status, resp);
                }
                var response = await _ticketCase.GetAllTicketByUser(_UserId);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<IEnumerable<HttpGetTicketResponse>>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        [HttpGet("get-property-create-user")]

        public async Task<ActionResult<MessagePayload<HttpGetPropertyCreateUserResponse>>> GetPropertyCreateUser()
        {
            try
            {
                var response = await _ticketCase.GetPropertyCreateUser();
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<HttpGetPropertyCreateUserResponse>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }


        [HttpGet("get-all-ticket-by-user")]
        [Authorize]
        public async Task<ActionResult<MessagePayload<IEnumerable<HttpGetTicketResponse>>>> GetAllTicketByUser()
        {
            try
            {
                var userClaims = User.Claims;
                var _userId = _tokenService.GetObjectFromToken(userClaims).Id;
                var response = await _ticketCase.GetAllTicketByUser(_userId);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<IEnumerable<HttpGetTicketResponse>>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }
      

        [HttpGet("ticket/{id}")]

        public async Task<ActionResult<MessagePayload<HttpGetTicketResponse>>> GetTicket([FromRoute] int id)
        {
            try
            {
                var response = await _ticketCase.GetTicket(id);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<HttpGetTicketResponse>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        [HttpDelete("tickets/{id}/delete")]
        public async Task<ActionResult<MessagePayload<int>>> DeleteTicket([FromRoute] int id)
        {
            try
            {
                var response = await _ticketCase.DeleteTicket(id);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<int>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        [HttpPut("tickets/{id}/update")]
        public async Task<ActionResult<MessagePayload<int>>> UpdateTicket([FromRoute] int id, [FromBody] CreateTicketRequest ticket)
        {
            try
            {
                var response = await _ticketCase.UpdateTicket(id, ticket);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<int>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        [HttpGet("get-state")]
        public async Task<ActionResult<MessagePayload<IEnumerable<HttpGetAllStateResponse>>>> GetAllState()
        {
            try
            {
                var response = await _ticketCase.GetAllState();
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<IEnumerable<HttpGetAllStateResponse>>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }
    }
}
