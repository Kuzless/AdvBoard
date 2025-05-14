using AdvBoard.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdvBoard.Application.CQRS.Announcement.Commands.AddAnnouncementCommand
{
    public class AddAnnouncementCommandHandler : IRequestHandler<AddAnnouncementCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddAnnouncementCommandHandler> _logger;
        public AddAnnouncementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddAnnouncementCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<bool> Handle(AddAnnouncementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var adv = _mapper.Map<Domain.Entities.Announcement>(request);
                adv.CreatedAt = DateTime.Now.ToLocalTime();
                adv.StatusId = 1;
                await _unitOfWork.AnnouncementRepository.AddAsync(adv);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding announcement");
                return false;
            }
        }
    }
}
