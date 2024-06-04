using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;

namespace TodoList.Tests.BLLTests;

public class TodoItemServiceTests
{
    private Mock<ITodoItemRepository> _repositoryMock;
    private ITodoItemService _service;
    
    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<ITodoItemRepository>();
        _service = new TodoItemService(_repositoryMock.Object);
    }
    
    [Test]
    public async Task GetAllAsync_ShouldReturnAllTodoItems()
    {
        // Arrange
        var todoItems = new List<TodoItem>
        {
            new() { Id = 1, Title = "Test 1", IsCompleted = false },
            new() { Id = 2, Title = "Test 2", IsCompleted = true }
        };

        _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(todoItems);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.First().Title, Is.EqualTo("Test 1"));
        Assert.That(result.Last().Title, Is.EqualTo("Test 2"));
    }
    
    [Test]
    public async Task AddAsync_ShouldCallRepositoryAddAsync()
    {
        // Arrange
        var todoItemModel = new TodoItemModel { Id = 3, Title = "Test 3", IsCompleted = false };

        // Act
        await _service.AddAsync(todoItemModel);

        // Assert
        _repositoryMock.Verify(repo => repo.AddAsync(It.Is<TodoItem>(t => t.Title == "Test 3")), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_ShouldCallRepositoryUpdateAsync()
    {
        // Arrange
        var todoItemModel = new TodoItemModel { Id = 1, Title = "Updated Test", IsCompleted = true };

        // Act
        await _service.UpdateAsync(todoItemModel);

        // Assert
        _repositoryMock.Verify(repo => repo.UpdateAsync(It.Is<TodoItem>(t => t.Id == 1 && t.Title == "Updated Test")), Times.Once);
    }

    [Test]
    public async Task DeleteByIdAsync_ShouldCallRepositoryDeleteByIdAsync()
    {
        // Arrange
        var todoItemId = 1;

        // Act
        await _service.DeleteByIdAsync(todoItemId);

        // Assert
        _repositoryMock.Verify(repo => repo.DeleteByIdAsync(todoItemId), Times.Once);
    }
}