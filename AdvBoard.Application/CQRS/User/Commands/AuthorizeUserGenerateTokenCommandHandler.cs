using AdvBoard.Application.Interfaces;
using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;
using MediatR;

namespace AdvBoard.Application.CQRS.User.Commands
{
    public class AuthorizeUserGenerateTokenCommandHandler : IRequestHandler<AuthorizeUserGenerateTokenCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTService _jWTService;
        public AuthorizeUserGenerateTokenCommandHandler(IUnitOfWork unitOfWork, IJWTService jWTService)
        {
            _unitOfWork = unitOfWork;
            _jWTService = jWTService;
        }
        public async Task<string> Handle(AuthorizeUserGenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.UserManager.FindByIdAsync(request.Id);
            var token = _jWTService.GenerateToken(request);

            if (existingUser == null)
            {
                await _unitOfWork.UserManager.CreateAsync(new Domain.Entities.User
                {
                    Id = request.Id,
                    UserName = request.Email,
                    Email = request.Email,
                    Name = request.Name,
                    AccessToken = token
                });
            }
            else
            {
                await _unitOfWork.UserManager.UpdateAsync(existingUser);
            }
            await _unitOfWork.SaveAsync();

            return token;
        }
    }
}
