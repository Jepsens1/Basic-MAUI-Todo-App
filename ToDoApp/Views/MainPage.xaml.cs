using ToDoApp.ViewModels;

namespace ToDoApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            //Binds to viewmodel
            BindingContext = vm;
        }
    }

}
