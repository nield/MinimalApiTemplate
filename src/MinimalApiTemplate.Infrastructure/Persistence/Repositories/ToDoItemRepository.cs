namespace MinimalApiTemplate.Infrastructure.Persistence.Repositories;

public class ToDoItemRepository : BaseRepository<TodoItem>, IToDoItemRepository
{
    public ToDoItemRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {

    }
}
