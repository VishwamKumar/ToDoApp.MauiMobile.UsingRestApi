
namespace ToDoApp.MauiMobile.UsingRestApi.Pages;

[QueryProperty(nameof(ToDo),"ToDo")]
public partial class MangeToDoPage : ContentPage
{
    private readonly IRestDataService _dataService;
	ToDo _toDo;
	bool _isNew;

	public ToDo ToDo
	{
		get =>_toDo;
		set
		{
			_isNew = IsNew(value);
			_toDo = value;
			OnPropertyChanged();
		}
	}
    public MangeToDoPage(IRestDataService dataService)
	{
		InitializeComponent();
		_dataService = dataService;
		BindingContext= this;
	}

	bool IsNew(ToDo toDo)
	{
		if (toDo.Id== 0)
		{
			return true;
		}
		return false;
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		if (_isNew)
		{
			Console.WriteLine("-->Add New Item");
			await _dataService.AddToDoAsync(ToDo);
		}
		else
		{
            Console.WriteLine("-->Update New Item");
            await _dataService.UpdateToDoAsync(ToDo.Id,ToDo);
        }
        await Shell.Current.GoToAsync("..");
    }
	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		await _dataService.DeleteToDoAsync(ToDo.Id);
        await Shell.Current.GoToAsync("..");
    }

	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}