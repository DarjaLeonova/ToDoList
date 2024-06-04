using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

public class TodoController : Controller
{
    private readonly ITodoItemService _service;

    public TodoController(ITodoItemService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _service.GetAllAsync());
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(TodoItemModel todoItem)
    {
        if (ModelState.IsValid)
        {
            await _service.AddAsync(todoItem);
            return RedirectToAction(nameof(Index));
        }

        return View(todoItem);
    }

    [HttpPost]
    public async Task<IActionResult> Update(TodoItemModel todoItem)
    {
        await _service.UpdateAsync(todoItem);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}