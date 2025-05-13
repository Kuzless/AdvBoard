using MediatR;

namespace AdvBoard.Application.CQRS.User.Commands
{
    public class AuthorizeUserGenerateTokenCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
