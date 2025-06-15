using System;

namespace TaskManager.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Role { get; }
    }
}
