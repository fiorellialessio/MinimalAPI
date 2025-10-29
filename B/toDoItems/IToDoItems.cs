namespace B.toDoItems
{
    public record TodoItem(int Id, string Title, bool IsDone, string Category);
    public record CreateTodoItem(string Title, string Category);

    public interface ITodoItems
    {
        public Task<List<TodoItem>> GetAllItems();
        public Task<TodoItem?> GetItem(int Id);
        public Task<TodoItem> CreateItem(CreateTodoItem newItem);
        Task UpdateItem(TodoItem itemModificato);
        Task DeleteItem(int id);
    }
      


}
