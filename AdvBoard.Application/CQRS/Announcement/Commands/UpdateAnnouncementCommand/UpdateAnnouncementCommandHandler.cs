using AdvBoard.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Commands.UpdateAnnouncementCommand
{
    public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateAnnouncementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
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
    }
}
