using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace B.EndPoints;

public static class ToDoItemsEndPoint
{

    private static async Task<IResult> GetAll(ITodoItems service)
    {
        return Results.Ok(await service.GetAllItems());
    }
    public static void RegisterToDoItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/todoitems");

        group.MapGet("/", async (ITodoItems service) => {
            return Results.Ok(await service.GetAllItems());

        });
        group.MapGet("/{id}", async (int id, ITodoItems service) => {
            return await service.GetItem(id) == null ?
                            Results.NotFound() :
                            Results.Ok(await service.GetItem(id));
        });
        group.MapPost("/", async (CreateTodoItem newItem, ITodoItems service) => {
            if (newItem.Category is null || newItem.Title is null) return Results.BadRequest();
            var item = await service.CreateItem(newItem);

            return Results.Created($"/todoitems/{item.Id}", item);
        });

        group.MapPut("{id}", async (int id, TodoItem item, ITodoItems service) =>
        {
            if (id != item.Id) return Results.BadRequest();
            await service.UpdateItem(item);
            return Results.NoContent();
        });

        group.MapPatch("{id}", async (int id, TodoItem item, ITodoItems service) =>
        {
            if (id != item.Id) return Results.BadRequest();
            await service.UpdateItem(item);
            return Results.NoContent();
        });
        group.MapDelete("{id}", async (int id, ITodoItems service) =>
        {
            await service.DeleteItem(id);
            return Results.NoContent();
        });

        app.MapGet("/boom", async () =>
        {
           throw new InvalidProgramException("Scemo, CORREGGI");
        });
    }
}
