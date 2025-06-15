using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Auth.Models;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenHandler;
        public LoginCommandHandler(IAppDbContext context, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenHandler)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtTokenHandler = jwtTokenHandler;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new UnauthorizedAccessException("Invalid credentials");

            return new LoginResponse
            {
                Token = _jwtTokenHandler.GenerateToken(user),
            };
        }
    }
}
