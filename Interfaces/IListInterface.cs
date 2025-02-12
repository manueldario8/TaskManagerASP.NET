using ToDoListWebApp.Models;

namespace ToDoListWebApp.Interfaces
{
    public interface IListInterface
    {
        Task<IEnumerable<ToDoList>> GetAll();
        Task<ToDoList> GetByIdAsync(int id);

        Task<List<ToDoList>> GetByStatusAsync(bool status);

        Task<bool> AddAsync(ToDoList item);
        Task<bool> DeleteAsync(ToDoList item);
        Task<bool> UpdateAsync(ToDoList item);
        Task<bool> DeleteMultipleAsync(List<int> ids);
        Task<bool> Save();


    }
}
