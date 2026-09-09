using MinimalApiTemplate.Infrastructure.Persistence.Repositories;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence.Repositories;

public class ToDoItemRepositoryTests : BasePersistenceTestFixture<ToDoItemRepository>
{
    private readonly ToDoItemRepository _repository;

    public ToDoItemRepositoryTests()
    {
        _repository = new(_dbContext);
    }

    [Fact]
    public async Task Given_ToDoItemDoesExist_Then_ReturnTrue()
    {
        var sut = await _repository.GetByIdAsync(1, TestContext.Current.CancellationToken);

        sut.Should().NotBeNull();
    }


    [Fact]
    public async Task Given_ToDoItemDoesExist_When_Deleting_Then_ToDoItemShouldBeRemoved()
    {
        var entity = await _repository.GetByIdAsync(1, TestContext.Current.CancellationToken);

        entity.Should().NotBeNull();

        await _repository.DeleteAsync(entity, TestContext.Current.CancellationToken);

        var deletedTemplate = await _repository.GetByIdAsync(1, TestContext.Current.CancellationToken);

        deletedTemplate.Should().BeNull();
    }

    [Fact]
    public async Task Given_ToDoItemDoesExist_When_Updating_Then_ToDoItemShouldBeUpdate()
    {
        var entity = await _repository.GetByIdAsync(1, TestContext.Current.CancellationToken);

        entity.Should().NotBeNull();

        var newNote = "updated note";

        entity.Note = newNote;

        await _repository.UpdateAsync(entity, TestContext.Current.CancellationToken);

        var updatedTemplate = await _repository.GetByIdAsync(1, TestContext.Current.CancellationToken);

        updatedTemplate.Should().NotBeNull();
        updatedTemplate.Note.Should().Be(newNote);
    }
}
