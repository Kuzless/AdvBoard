using AdvBoard.Application.DTO;
using AdvBoard.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementByUserIdQuery
{
    public class GetAnnouncementByUserIdQueryHandler : IRequestHandler<GetAnnouncementByUserIdQuery, List<AdvDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAnnouncementByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<AdvDTO>> Handle(GetAnnouncementByUserIdQuery request, CancellationToken cancellationToken)
        {
            var announcement = await _unitOfWork.AnnouncementRepository.GetAnnouncementsByUserIdAsync(request.UserId);
            return _mapper.Map<List<AdvDTO>>(announcement);
        }
    }
}
