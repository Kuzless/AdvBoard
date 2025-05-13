using System.Security.Claims;
using AdvBoard.Application.CQRS.Announcement.Commands.AddAnnouncementCommand;
using AdvBoard.Application.CQRS.Announcement.Commands.DeleteAnnouncementCommand;
using AdvBoard.Application.CQRS.Announcement.Commands.UpdateAnnouncementCommand;
using AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementByIdQuery;
using AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementByUserIdQuery;
using AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementsQuery;
using AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementStructureQuery;
using AdvBoard.Application.DTO.CommandDTOs;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvBoard.Api.Controllers
{
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AnnouncementController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncement([FromBody] UpdAdvDTO adv)
        {
            if (adv == null)
            {
                return UnprocessableEntity("Invalid announcement data.");
            }
            
            var command = _mapper.Map<AddAnnouncementCommand>(adv);
            command.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Announcement added successfully.");
            }
            return UnprocessableEntity("Failed to add announcement.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] UpdAdvDTO adv)
        {
            if (adv == null)
            {
                return UnprocessableEntity("Invalid announcement data.");
            }
            var command = _mapper.Map<UpdateAnnouncementCommand>(adv);
            command.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Announcement updated successfully.");
            }
            return UnprocessableEntity("Failed to update announcement.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var command = new DeleteAnnouncementCommand { Id = id };
            command.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Announcement deleted successfully.");
            }
            return UnprocessableEntity("Failed to delete announcement.");
        }
        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            var result = await _mediator.Send(new GetAnnouncementsQuery());
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Announcement not found.");
        }
        [HttpGet("user/")]
        public async Task<IActionResult> GetAnnouncementByUserId()
        {
            var query = new GetAnnouncementByUserIdQuery();
            query.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await _mediator.Send(query);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Announcement not found.");
        }
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetAnnouncementEditStructureById(int id)
        {
            var result = await _mediator.Send(new GetAnnouncementEditByIdQuery { Id = id });
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Announcement not found.");
        }
        [HttpGet("user/add")]
        public async Task<IActionResult> GetAnnouncementStructure()
        {
            var result = await _mediator.Send(new GetAnnouncementStructureQuery());
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Announcement not found.");
        }
    }
}
