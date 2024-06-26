namespace ToDoApp.MauiMobile.UsingRestApi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MangeToDoPage), typeof(MangeToDoPage));
        }
    }
}
