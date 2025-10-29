

namespace B.toDoItems;

public class MockItems : ITodoItems
{

    async Task<List<TodoItem>> ITodoItems.GetAllItems()
    {
        await Task.Delay(1000);

        return Items;

    }
    //inizializzo gli items
    private static List<TodoItem> Items { get; set; } = new List<TodoItem>() {
        new TodoItem(1, "pilota elicotteri", false,"sport"),
        new TodoItem(2, "la vita prima della vita", true, "paleontologia")
    };
    async Task<TodoItem?> ITodoItems.GetItem(int Id)
    {
        await Task.Delay(1000);
        var item = Items.FirstOrDefault(x => x.Id == Id);
        return item;
    }

    public async Task<TodoItem> CreateItem(CreateTodoItem newItem)
    {
        int maxId = 1;
        await Task.Delay(1000);
        if (Items.Count != 0)
            maxId = Items.Max(x => x.Id) + 1;
        TodoItem item = new TodoItem(maxId, newItem.Title, false, newItem.Category);

        Items.Add(item);
        return item;
    }

    public async Task UpdateItem(TodoItem itemModificato)
    {
        await Task.Delay(1000);
        var item = Items.FirstOrDefault(x => x.Id == itemModificato.Id);

        if (item is not null)
        {
            var newItem = item with
            {
                Title = itemModificato.Title,
                Category = itemModificato.Category,
                IsDone = itemModificato.IsDone,
            };
            Items.Remove(item);
            Items.Add(newItem);
        }
    }

    public async Task DeleteItem(int id)
    {
        await Task.Delay(1000);
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item is not null)
        {
            Items.Remove(item);
        }
    }
}