using AdvBoard.Application.CQRS.Announcement.Commands.AddAnnouncementCommand;
using AdvBoard.Application.CQRS.Announcement.Commands.DeleteAnnouncementCommand;
using AdvBoard.Application.CQRS.Announcement.Commands.UpdateAnnouncementCommand;
using AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementByIdQuery;
using AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementsQuery;
using AdvBoard.Application.DTO;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdvBoard.Api.Controllers
{
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
        public async Task<IActionResult> AddAnnouncement([FromBody] EditAdvDTO adv)
        {
            if (adv == null)
            {
                return BadRequest("Invalid announcement data.");
            }
            var command = _mapper.Map<AddAnnouncementCommand>(adv);
            command.UserId = "364d360b-6ad3-44f4-8ebc-df445acc4a53"; // TEMP
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Announcement added successfully.");
            }
            return BadRequest("Failed to add announcement.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody] EditAdvDTO adv)
        {
            if (adv == null)
            {
                return BadRequest("Invalid announcement data.");
            }
            var command = _mapper.Map<UpdateAnnouncementCommand>(adv);
            command.Id = id;
            command.UserId = "364d360b-6ad3-44f4-8ebc-df445acc4a53"; // TEMP
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Announcement updated successfully.");
            }
            return BadRequest("Failed to update announcement.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var command = new DeleteAnnouncementCommand { Id = id };
            command.UserId = "364d360b-6ad3-44f4-8ebc-df445acc4a53"; // TEMP
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Announcement deleted successfully.");
            }
            return BadRequest("Failed to delete announcement.");
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementById(int id)
        {
            var result = await _mediator.Send(new GetAnnouncementByIdQuery { Id = id });
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Announcement not found.");
        }
    }
}
