
namespace ToDoApp.MauiMobile.UsingRestApi.DataServices
{
    public interface IRestDataService
    {
        Task<List<ToDo>?> GetAllToDosAsync();
        Task AddToDoAsync(ToDo toDo);
        Task UpdateToDoAsync(int id, ToDo toDo);
        Task DeleteToDoAsync(int id);
    }
}
