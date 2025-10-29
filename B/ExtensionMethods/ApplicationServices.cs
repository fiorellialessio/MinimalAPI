namespace B.ExtensionMethods;

public static class ApplicationServices
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoItems, MockItems>();
        services.AddScoped<IProduct, ProductsService>();
        services.AddDbContext<NothWindContext>(x => x.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
        services.AddSwaggerGen();
    }

}
