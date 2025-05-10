using AdvBoard.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Commands.AddAnnouncementCommand
{
    public class AddAnnouncementCommandHandler : IRequestHandler<AddAnnouncementCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddAnnouncementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var adv = _mapper.Map<Domain.Entities.Announcement>(request);
            adv.CreatedAt = DateTime.UtcNow;
            adv.StatusId = 1;
            await _unitOfWork.AnnouncementRepository.AddAsync(adv);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
