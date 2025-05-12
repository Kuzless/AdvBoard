using AdvBoard.Application.DTO;
using AdvBoard.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementByIdQuery
{
    public class GetAnnouncementEditByIdQueryHandler : IRequestHandler<GetAnnouncementEditByIdQuery, EditAdvInfoDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAnnouncementEditByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EditAdvInfoDTO> Handle(GetAnnouncementEditByIdQuery request, CancellationToken cancellationToken)
        {
            var announcement = await _unitOfWork.AnnouncementRepository.GetByIdAsync(request.Id);
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var subCategories = await _unitOfWork.SubCategoryRepository.GetAllAsync();
            var statuses = await _unitOfWork.StatusRepository.GetAllAsync();
            var result = _mapper.Map<EditAdvInfoDTO>(announcement);
            result.Categories = _mapper.Map<List<CategoryDTO>>(categories);
            result.SubCategories = _mapper.Map<List<SubCategoryDTO>>(subCategories);
            result.Statuses = _mapper.Map<List<StatusDTO>>(statuses);
            return result;
        }
    }
}
