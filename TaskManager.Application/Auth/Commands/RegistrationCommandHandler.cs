using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Auth.Models;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Auth.Commands
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegistrationResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public RegistrationCommandHandler(IAppDbContext context, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegistrationResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
                throw new Exception("Passwords do not match.");

            var exists = await _context.Users.AnyAsync(u => u.Username == request.Username, cancellationToken);

            if (exists)
                throw new Exception("Username already exists.");

            var salt = _passwordHasher.GenerateSalt();
            var hash = _passwordHasher.HashPassword(request.Password, salt);

            var user = new User
            {
                Username = request.Username,
                PasswordSalt = salt,
                PasswordHash = hash,
                Role = request.Role,
                ManagerId = request.ManagerId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new RegistrationResponse { Token = token };
        }
    }
}
