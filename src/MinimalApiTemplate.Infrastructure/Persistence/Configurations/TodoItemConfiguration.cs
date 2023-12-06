using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MinimalApiTemplate.Infrastructure.Persistence.Configurations;

public class TodoItemConfiguration : BaseConfiguration<TodoItem>
{
    public override string TableName => "ToDoItem";

    public override void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Tags)
            .HasMaxLength(1000)
            .IsUnicode(false)
            .IsRequired(false);
    }

    // Optional - Load default data
    protected override IList<TodoItem> SeedData()
    {
        return new List<TodoItem>()
        {
            new() { Id = 1, Title = "Work work work", Priority = PriorityLevel.Medium, Tags = ["work"] }
        };
    }
}