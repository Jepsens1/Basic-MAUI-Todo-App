using SQLite;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class TodoService : ITodoService
    {
        SQLiteAsyncConnection m_DataBase;

        /// <summary>
        /// Init the database connection
        /// </summary>
        /// <returns></returns>
        async Task Init()
        {
            if (m_DataBase != null)
                return;
            m_DataBase = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await m_DataBase.CreateTableAsync<ToDoModel>();
        }
        /// <summary>
        /// Inserts model to table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> CreateTodoAsync(ToDoModel model)
        {
            await Init();
            return await m_DataBase.InsertAsync(model);
        }
        /// <summary>
        /// Deletes given todo model from database
        /// </summary>
        /// <param name="selectedTodo"></param>
        /// <returns></returns>
        public async Task<int> DeleteTodoAsync(ToDoModel selectedTodo)
        {
            await Init();
            return await m_DataBase.DeleteAsync(selectedTodo);
        }
        /// <summary>
        /// Updates selected todo item 
        /// </summary>
        /// <param name="selectedTodo"></param>
        /// <returns></returns>
        public async Task<int> MarkTodoCompleteAsync(ToDoModel selectedTodo)
        {
            await Init();
            return await m_DataBase.UpdateAsync(selectedTodo);
        }
        /// <summary>
        /// Returns all ToDoModel's in database
        /// </summary>
        /// <returns></returns>
        public async Task<List<ToDoModel>> GetItemsAsync()
        {
            await Init();
            return await m_DataBase.Table<ToDoModel>().ToListAsync();
        }
    }
}
