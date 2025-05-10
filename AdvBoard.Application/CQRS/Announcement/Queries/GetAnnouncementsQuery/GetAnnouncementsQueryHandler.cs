using AdvBoard.Application.DTO;
using AdvBoard.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementsQuery
{
    public class GetAnnouncementsQueryHandler : IRequestHandler<GetAnnouncementsQuery, List<AdvDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAnnouncementsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<AdvDTO>> Handle(GetAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            var advs = await _unitOfWork.AnnouncementRepository.GetAnnouncementsPageAsync();
            return _mapper.Map<List<AdvDTO>>(advs);
        }
    }
}
