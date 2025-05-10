using MediatR;

namespace AdvBoard.Application.CQRS.User.Commands.SignUpCommand
{
    public class SignUpCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
