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

        public async Task<bool> UpdateAsync(ToDoList task)
        {
            _dbContext.Update(task);
            return await Save();
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
