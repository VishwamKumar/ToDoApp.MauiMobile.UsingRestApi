namespace ToDoApp.MauiMobile.UsingRestApi;

public partial class MainPage : ContentPage
{
    private readonly IRestDataService _dataService;

    public MainPage(IRestDataService dataService)
    {
        InitializeComponent();
        _dataService = dataService;

    }

    async protected override void OnAppearing()
    {
        base.OnAppearing();
        collectionView.ItemsSource = await _dataService.GetAllToDosAsync();
    }

    async void OnAddToDoClicked(object sender, EventArgs e)
    {
        Console.WriteLine("-->Add Button Clicked");

        var navigationParameter = new Dictionary<string, object>
    {
        { nameof(ToDo), new ToDo() }
    };

        await Shell.Current.GoToAsync(nameof(MangeToDoPage), navigationParameter);
    }

    

    async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Console.WriteLine("-->Update Button Clicked");

        var navigationParameter = new Dictionary<string, object>
    {
        { nameof(ToDo), e.CurrentSelection.FirstOrDefault() as ToDo }
    };

        await Shell.Current.GoToAsync(nameof(MangeToDoPage), navigationParameter);
    }


}
