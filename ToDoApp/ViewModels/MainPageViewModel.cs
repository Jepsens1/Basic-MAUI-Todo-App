using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.ViewModels
{
    /// <summary>
    /// Represents the ViewModel for MainPage
    /// </summary>
    public partial class MainPageViewModel : ObservableObject
    {
        private ITodoService m_TodoService;

        [ObservableProperty] private string todoText = string.Empty;
        public ObservableCollection<ToDoModel> TodoItems { get; private set; }
        public MainPageViewModel(ITodoService todoService)
        {
            this.m_TodoService = todoService;
            _ = InitItems(); //Fire and forget
        }
        /// <summary>
        /// Init TodoItems collection with data from sqlite if any
        /// </summary>
        /// <returns></returns>
        private async Task InitItems()
        {
            var result = await m_TodoService.GetItemsAsync();
            TodoItems = new ObservableCollection<ToDoModel>(result);
            OnPropertyChanged(nameof(TodoItems)); //Updates the view
        }
        /// <summary>
        /// Creates a TodoModel based on input field and adds to both sqlite database and ObservableCollection
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task CreateTodo()
        {
            if(string.IsNullOrEmpty(TodoText))
            {
                await Shell.Current.DisplayAlert("Input field empty", "Input field cannot be empty", "Ok");
                return;
            }
            try
            {
                var model = new ToDoModel { Title = TodoText };
                var result = await m_TodoService.CreateTodoAsync(model);
                if(result > 0)
                    TodoItems.Add(model);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
                await Shell.Current.DisplayAlert("Could not create todo item", "Failed to create the given todo item", "Ok");
            }
            finally
            {
                TodoText = string.Empty;
            }
        }

        /// <summary>
        /// This RelayCommand method removes selected todo item from database and ObservableCollection
        /// </summary>
        [RelayCommand]
        private async Task DeleteTodo(ToDoModel selectedTodo)
        {
            if (selectedTodo is null)
            {
#if DEBUG
                Debug.WriteLine($"{nameof(DeleteTodo)} has null {nameof(ToDoModel)} parameter");
#endif
                return;
            }
            try
            {
                var result = await m_TodoService.DeleteTodoAsync(selectedTodo);
                if (result > 0)
                    TodoItems.Remove(selectedTodo);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
                await Shell.Current.DisplayAlert("Could not delete todo item", "Failed to delete the given todo item", "Ok");
            }
        }
        /// <summary>
        /// Marks the selected todo item as complete in the database and updates the UI when property changes
        /// </summary>
        /// <param name="selectedTodo"></param>
        /// <returns></returns>
        [RelayCommand]
        private async Task MarkComplete(ToDoModel selectedTodo)
        {
            if (selectedTodo is null)
            {
#if DEBUG
                Debug.WriteLine($"{nameof(DeleteTodo)} has null {nameof(ToDoModel)} parameter");
#endif
                return;
            }
            try
            {
                selectedTodo.IsComplete = true;
                var result = await m_TodoService.MarkTodoCompleteAsync(selectedTodo);
                if (result <= 0)
                    throw new Exception($"Failed to mark {nameof(ToDoModel)} with ID: {selectedTodo.ID} as complete");
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
                await Shell.Current.DisplayAlert("Could not mark complete", "Failed to mark the given todo complete", "Ok");
            }
        }
    }
}
