using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PL.Controllers;

namespace TodoList.Tests.PLTests;

public class TodoControllerTests
{
    private Mock<ITodoItemService> _mockService;
    private TodoController _controller;
    
    [SetUp]
    public void SetUp()
    {
        _mockService = new Mock<ITodoItemService>();
        _controller = new TodoController(_mockService.Object);
    }
    
    [Test]
    public async Task Index_ReturnsViewResult_WithListOfTodoItems()
    {
        // Arrange
        var todoItems = new List<TodoItemModel> { new TodoItemModel { Id = 1, Title = "Test", IsCompleted = false} };
        _mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(todoItems);

        // Act
        var result = await _controller.Index();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsNotNull(viewResult);

        Assert.IsInstanceOf<IEnumerable<TodoItemModel>>(viewResult.Model);
        var model = viewResult.Model as IEnumerable<TodoItemModel>;
        Assert.IsNotNull(model);
        Assert.That(model.Count(), Is.EqualTo(1));
    }
    
    [Test]
    public async Task Add_ValidModel_RedirectsToIndex()
    {
        // Arrange
        var newItem = new TodoItemModel { Id = 1, Title = "New Item", IsCompleted = true};
        _mockService.Setup(service => service.AddAsync(newItem)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Add(newItem);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.IsNotNull(redirectToActionResult);
        Assert.That(redirectToActionResult.ActionName, Is.EqualTo(nameof(TodoController.Index)));
    }
    
    [Test]
    public async Task Add_InvalidModel_ReturnsViewWithModel()
    {
        // Arrange
        var newItem = new TodoItemModel { Id = 1, Title = "New Item", IsCompleted = false};
        _controller.ModelState.AddModelError("Title", "Required");

        // Act
        var result = await _controller.Add(newItem);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsNotNull(viewResult);

        Assert.IsInstanceOf<TodoItemModel>(viewResult.Model);
        var model = viewResult.Model as TodoItemModel;
        Assert.IsNotNull(model);
        Assert.That(model, Is.EqualTo(newItem));
    }
    
    [Test]
    public async Task Update_ValidModel_RedirectsToIndex()
    {
        // Arrange
        var updatedItem = new TodoItemModel { Id = 1, Title = "Updated Item", IsCompleted = true};
        _mockService.Setup(service => service.UpdateAsync(updatedItem)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Update(updatedItem);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.IsNotNull(redirectToActionResult);
        Assert.That(redirectToActionResult.ActionName, Is.EqualTo(nameof(TodoController.Index)));
    }
    
    [Test]
    public async Task Delete_ValidId_RedirectsToIndex()
    {
        // Arrange
        int itemId = 1;
        _mockService.Setup(service => service.DeleteByIdAsync(itemId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(itemId);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.IsNotNull(redirectToActionResult);
        Assert.That(redirectToActionResult.ActionName, Is.EqualTo(nameof(TodoController.Index)));
    }
}