using AdvBoard.Application.DTO.QueryDTOs;
using AdvBoard.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementStructureQuery
{
    public class GetAnnouncementStructureQueryHandler : IRequestHandler<GetAnnouncementStructureQuery, AdvStructureDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAnnouncementStructureQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AdvStructureDTO> Handle(GetAnnouncementStructureQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var subCategories = await _unitOfWork.SubCategoryRepository.GetAllAsync();
            var structure = new AdvStructureDTO();
            structure.Categories = _mapper.Map<List<CategoryDTO>>(categories);
            structure.SubCategories = _mapper.Map<List<SubCategoryDTO>>(subCategories);
            return structure;
        }
    }
}
