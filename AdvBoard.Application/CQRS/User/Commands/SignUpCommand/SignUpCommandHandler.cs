using AdvBoard.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AdvBoard.Application.CQRS.User.Commands.SignUpCommand
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SignUpCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.UserManager.CreateAsync(_mapper.Map<Domain.Entities.User>(request));
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
