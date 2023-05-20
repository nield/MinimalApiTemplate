using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MinimalApiTemplate.Infrastructure.Persistence.Configurations;

public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    private static readonly IList<TEntity> EmptySeed = new List<TEntity>();

    public abstract string TableName { get; }

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn();

        builder.HasQueryFilter(x => !x.IsDeleted); //Default exclude IsDeleted

        builder.Ignore(x => x.DomainEvents);

        builder.HasData(SeedData());
    }

    protected virtual IList<TEntity> SeedData() => EmptySeed;
}
