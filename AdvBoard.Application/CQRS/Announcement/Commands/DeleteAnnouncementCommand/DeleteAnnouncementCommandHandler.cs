using AdvBoard.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdvBoard.Application.CQRS.Announcement.Commands.DeleteAnnouncementCommand
{
    public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteAnnouncementCommandHandler> _logger;
        public DeleteAnnouncementCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteAnnouncementCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var adv = await _unitOfWork.AnnouncementRepository.GetByIdAsync(request.Id);
                if (adv != null && adv.UserId == request.UserId)
                {
                    await _unitOfWork.AnnouncementRepository.Delete(adv);
                    await _unitOfWork.SaveAsync();
                    return true;
                }
                return false;
            } catch (Exception ex) {
                _logger.LogError(ex, "Error occurred while deleting announcement");
                return false;
            }
        }
    }
}
