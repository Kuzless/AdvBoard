using AdvBoard.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdvBoard.Application.CQRS.Announcement.Commands.UpdateAnnouncementCommand
{
    public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAnnouncementCommandHandler> _logger;
        public UpdateAnnouncementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateAnnouncementCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var announcement = await _unitOfWork.AnnouncementRepository.GetByIdAsync(request.Id);
                if (announcement.UserId == request.UserId)
                {
                    _mapper.Map(request, announcement);
                    await _unitOfWork.SaveAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating announcement");
                return false;
            }
        }
    }
}
