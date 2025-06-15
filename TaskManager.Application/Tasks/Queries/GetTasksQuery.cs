using MediatR;
using TaskManager.Application.Tasks.Models;
using System.Collections.Generic;

namespace TaskManager.Application.Tasks.Queries
{
    public class GetTasksQuery : IRequest<List<TaskDto>> { }
}
