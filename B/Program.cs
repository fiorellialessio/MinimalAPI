using B.EndPoints;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.GeneralHttpExtension();

builder.Services.RegisterServices();



var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleWare>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
    //app.UseExceptionHandler();
}
app.RegisterToDoItemEndpoints();


app.MapGet("/products", async (NothWindContext context) =>
{
    await Task.Delay(1000);

    return Results.Ok(await context.Products.Select(p => new ProductDTO()
    {
        Id = p.ProductId,
        Nome = p.ProductName,
        Categoria = p.Category.CategoryName
    }).ToListAsync());
});

//codici http per OK (200 ok e restituisco valori, 204 OK non restituisco valori)

app.UseHttpsRedirection();

app.Run();
