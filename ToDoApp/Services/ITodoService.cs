using ToDoApp.Models;

namespace ToDoApp.Services
{
    public interface ITodoService
    {
        Task<int> CreateTodoAsync(ToDoModel model);
        Task<int> DeleteTodoAsync(ToDoModel selectedTodo);
        Task<int> MarkTodoCompleteAsync(ToDoModel selectedTodo);
        Task<List<ToDoModel>> GetItemsAsync();
    }
}
