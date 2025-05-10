using AdvBoard.Domain.Interfaces;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Commands.DeleteAnnouncementCommand
{
    public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAnnouncementCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var adv = await _unitOfWork.AnnouncementRepository.GetByIdAsync(request.Id);
            if (adv != null && adv.UserId == request.UserId)
            {
                await _unitOfWork.AnnouncementRepository.Delete(adv);
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
