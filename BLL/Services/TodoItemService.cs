using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class TodoItemService : ITodoItemService
{
    private readonly ITodoItemRepository _repository;

    public TodoItemService(ITodoItemRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<TodoItemModel>> GetAllAsync()
    {
        var todoItems = await _repository.GetAllAsync();
        return todoItems.Select(EntityToModel);
    }
    
    public async Task AddAsync(TodoItemModel model)
    {
        var entity = ModelToEntity(model);
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(TodoItemModel model)
    {
        var entity = ModelToEntity(model);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteByIdAsync(int modelId)
    {
        await _repository.DeleteByIdAsync(modelId);
    }

    private TodoItemModel EntityToModel(TodoItem entity)
    {
        return new TodoItemModel()
        {
            Id = entity.Id,
            IsCompleted = entity.IsCompleted,
            Title = entity.Title,
        };
    }

    private TodoItem ModelToEntity(TodoItemModel model)
    {
        return new TodoItem()
        {
            Id = model.Id,
            Title = model.Title,
            IsCompleted = model.IsCompleted,
        };
    }
}