using AdvBoard.Application.DTO;
using AdvBoard.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementByIdQuery
{
    public class GetAnnouncementByIdQueryHandler : IRequestHandler<GetAnnouncementByIdQuery, AdvDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAnnouncementByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AdvDTO> Handle(GetAnnouncementByIdQuery request, CancellationToken cancellationToken)
        {
            var announcement = await _unitOfWork.AnnouncementRepository.GetByIdAsync(request.Id);
            return _mapper.Map<AdvDTO>(announcement);
        }
    }
}
