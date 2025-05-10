using AdvBoard.Application.CQRS.User.Commands.SignUpCommand;
using AdvBoard.Application.DTO;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediator.Send(_mapper.Map<SignUpCommand>(user));
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("User creation failed");
            }
        }
    }
}
