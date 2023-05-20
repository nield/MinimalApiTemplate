using Microsoft.EntityFrameworkCore;

namespace MinimalApiTemplate.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoItem> TodoItems { get; }
}
