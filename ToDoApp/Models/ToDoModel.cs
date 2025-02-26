using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace ToDoApp.Models
{
    /// <summary>
    /// Represents the model for creating todo items
    /// </summary>
    public partial class ToDoModel : ObservableObject
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        [ObservableProperty] private bool isComplete = false;
    }
}
