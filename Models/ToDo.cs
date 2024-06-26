
namespace ToDoApp.MauiMobile.UsingRestApi.Models
{
    public class ToDo : INotifyPropertyChanged
    {
        int _id;
        public int Id
        {
            get => _id; 
            set
            {
                if (_id==value) return;
                _id=value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("id"));
            } 
        }

        string _todoname = null!;
        public string ToDoName
        {
            get=>_todoname;
            set
            {
                if (_todoname==value) 
                    return; 

                _todoname=value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToDoName)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
