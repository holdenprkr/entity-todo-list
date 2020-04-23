using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity_Todo.Data;
using Identity_Todo.Models;
using Identity_Todo.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Identity_Todo.Controllers
{
    [Authorize]
    public class TodoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoItemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TodoItems
        public async Task<ActionResult> Index(string filter)
        {
            var user = await GetCurrentUserAsync();
            var items = await _context.TodoItem
                .Where(ti => ti.ApplicationUserId == user.Id)
                .Include(ti => ti.TodoStatus)
                .ToListAsync();

            switch (filter)
            {
                case "To Do":
                    items = await _context.TodoItem
                        .Where(ti => ti.ApplicationUserId == user.Id)
                        .Where(ti => ti.TodoStatusId == 1)
                        .Include(ti => ti.TodoStatus)
                        .ToListAsync();
                    break;
                case "Progress":
                    items = await _context.TodoItem
                        .Where(ti => ti.ApplicationUserId == user.Id)
                        .Where(ti => ti.TodoStatusId == 2)
                        .Include(ti => ti.TodoStatus)
                        .ToListAsync();
                    break;
                case "Done":
                    items = await _context.TodoItem
                        .Where(ti => ti.ApplicationUserId == user.Id)
                        .Where(ti => ti.TodoStatusId == 3)
                        .Include(ti => ti.TodoStatus)
                        .ToListAsync();
                    break;
                case "All":
                    items = await _context.TodoItem
                        .Where(ti => ti.ApplicationUserId == user.Id)
                        .Include(ti => ti.TodoStatus)
                        .ToListAsync();
                    break;
            }

            return View(items);
        }

        // GET: TodoItems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TodoItems/Create
        public async Task<ActionResult> Create()
        {
            var allStatuses = await _context.TodoStatus
                .Select(td => new SelectListItem() { Text = td.Title, Value = td.Id.ToString() })
                .ToListAsync();

            var viewModel = new TodoItemViewModel();

            viewModel.TodoStatusOptions = allStatuses;

            return View(viewModel);
        }

        // POST: TodoItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoItemViewModel todoItemViewModel)
        {
            try
            {
                var todoItem = new TodoItem
                {
                    Title = todoItemViewModel.Title,
                    TodoStatusId = todoItemViewModel.TodoStatusId
                };

                var user = await GetCurrentUserAsync();
                todoItem.ApplicationUserId = user.Id;

                _context.TodoItem.Add(todoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItems/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var allStatuses = await _context.TodoStatus
                .Select(td => new SelectListItem() { Text = td.Title, Value = td.Id.ToString() })
                .ToListAsync();

            var todoItem = _context.TodoItem.FirstOrDefault(ti => ti.Id == id);

            var viewModel = new TodoItemViewModel()
            {
                Title = todoItem.Title,
                TodoStatusId = todoItem.TodoStatusId,
                ApplicationUserId = todoItem.ApplicationUserId,
                TodoStatusOptions = allStatuses
            };

            return View(viewModel);
        }

        // POST: TodoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TodoItemViewModel todoItemViewModel)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var todoItem = new TodoItem()
                {
                    Id = id,
                    Title = todoItemViewModel.Title,
                    TodoStatusId = todoItemViewModel.TodoStatusId,
                    ApplicationUserId = user.Id
                };

                _context.TodoItem.Update(todoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItems/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var todoItem = await _context.TodoItem.Include(ti => ti.TodoStatus).FirstOrDefaultAsync(ti => ti.Id == id);

            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, TodoItem todoItem)
        {
            try
            {
                _context.TodoItem.Remove(todoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}