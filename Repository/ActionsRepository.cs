using Microsoft.EntityFrameworkCore;
using ToDoListWebApp.Data;
using ToDoListWebApp.Interfaces;
using ToDoListWebApp.Models;

namespace ToDoListWebApp.Repository
{
    public class ActionsRepository : IListInterface
    {
        private readonly ToDoListDbContext _dbContext;
        public ActionsRepository(ToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(ToDoList task)
        {
            await _dbContext.AddAsync(task);
            return await Save();
        }
        public async Task<bool> DeleteAsync(ToDoList task)
        {
            _dbContext.Remove(task);
            return await Save();
        }

        public async Task<bool> UpdateAsync(ToDoList item)
        {
            try
            {
                var task = await _dbContext.ListToDo.FindAsync(item.Id);
                if (task == null)
                {
                    return false;  // No se encontró la tarea
                }

                task.Success = item.Success;  // Actualizar el estado de 'Success'
                task.Title = item.Title;      // Si también quieres permitir editar otros campos

                _dbContext.ListToDo.Update(task);
                await _dbContext.SaveChangesAsync();

                return true;  // Si todo fue exitoso
            }
            catch
            {
                return false;  // En caso de error, retornamos 'false'
            }
        }

        public async Task<IEnumerable<ToDoList>> GetAll()
        {
            return await _dbContext.ListToDo.ToListAsync();
        }

        public async Task<ToDoList> GetByIdAsync(int id)
        {
            return await _dbContext.ListToDo.Where(p =>  p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<ToDoList>> GetByStatusAsync(bool success)
        {
            return await _dbContext.ListToDo.Where(p => p.Success == success).ToListAsync();
        }


        public async Task<bool> Save()
        {
            var saved = await _dbContext.SaveChangesAsync();
            return saved > 0;    
        }


    }
}
