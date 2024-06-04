using DAL.Data;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Tests.DALTests;

public class TodoItemRepositoryTests
{
    private ApplicationDbContext _context;
    private TodoItemRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TodoListTest")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new TodoItemRepository(_context);
    }
    
    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
    
    [Test]
    public async Task AddAsync_ShouldAddItem()
    {
        // Arrange
        var todoItem = new TodoItem { Title = "Test Task", IsCompleted = false };

        // Act
        await _repository.AddAsync(todoItem);
        var result = await _context.TodoItems.FirstOrDefaultAsync(t => t.Title == "Test Task");
        
        // Assert
        Assert.That(result.Title, Is.EqualTo("Test Task"));
    }
    
    [Test]
    public async Task GetAllAsync_ShouldReturnAllItems()
    {
        // Arrange
        _context.TodoItems.Add(new TodoItem { Title = "Test Task 1", IsCompleted = false });
        _context.TodoItems.Add(new TodoItem { Title = "Test Task 2", IsCompleted = true });
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }
    
    [Test]
    public async Task UpdateAsync_ShouldUpdateItem()
    {
        // Arrange
        var todoItem = new TodoItem { Title = "Test Task", IsCompleted = false };
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();
    
        // Act
        todoItem.IsCompleted = true;
        await _repository.UpdateAsync(todoItem);
        var result = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == todoItem.Id);
    
        // Assert
        Assert.That(result.IsCompleted, Is.EqualTo(true));
    }
    
    [Test]
    public async Task DeleteByIdAsync_ShouldDeleteItem()
    {
        // Arrange
        var todoItem = new TodoItem { Title = "Test Task", IsCompleted = false };
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteByIdAsync(todoItem.Id);
        var result = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == todoItem.Id);

        // Assert
        Assert.IsNull(result);
    }
}