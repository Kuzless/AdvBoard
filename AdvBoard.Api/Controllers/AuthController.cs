using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using AdvBoard.Application.DTO.CommandDTOs;
using AdvBoard.Application.CQRS.User.Commands;

namespace AdvBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            if (user == null)
            {
                return UnprocessableEntity("Invalid data.");
            }

            var command = _mapper.Map<AuthorizeUserGenerateTokenCommand>(user);
            var token = await _mediator.Send(command);
            return Ok(token);
        }
    }
}
