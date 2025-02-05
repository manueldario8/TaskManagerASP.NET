using Microsoft.AspNetCore.Mvc;

using ToDoListWebApp.Interfaces;
using ToDoListWebApp.Models;

namespace ToDoListWebApp.Controllers
{
    public class TasksController : Controller
    {

        private readonly IListInterface _listtask;

        public TasksController(IListInterface listtask)
        {
            _listtask = listtask;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ToDoList> task = await _listtask.GetAll();

            return View(task);
        }

        [HttpGet("Tasks/Index")]
        public async Task<IActionResult> Index(bool? success)
        {
            var tasks = success.HasValue ? await _listtask.GetByStatusAsync(success.Value) : await _listtask.GetAll();
            
            if (Request.Headers["x-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_TaskListPartial", tasks);
            }
            
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> Create(ToDoList newtask)
        {
            if (ModelState.IsValid) {
                var _tasknew = new ToDoList
                {
                    Title = newtask.Title,
                    Description = newtask.Description,
                    Success = newtask.Success

                };

                bool added = await _listtask.AddAsync(_tasknew);
                if (added) {
                    await _listtask.Save();
                    return RedirectToAction("Index");
                }
                    
            }
            else
            {
                ModelState.AddModelError("", "Cannot upload the task");
  
            }

            return View(newtask); 

        }


        [HttpPost]
        public async Task<IActionResult> ToggleTaskStatus(int id, bool success)
        {
            var task = await _listtask.GetByIdAsync(id);
            if (task == null)
            {
                return Json(new { success = false });  // Si no se encuentra la tarea, devolvemos 'false'
            }

            task.Success = !task.Success;  // Cambiar el estado de 'Success'

            // Llamamos a UpdateAsync y comprobamos si fue exitoso
            var updateSuccess = await _listtask.UpdateAsync(task);
            if (updateSuccess)
            {
                return Json(new { success = true, newStatus = task.Success });
            }
            else
            {
                return Json(new { success = false });
            }
        }


    }
}
