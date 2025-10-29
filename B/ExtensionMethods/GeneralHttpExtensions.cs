public static class  GeneralHttpExtensions 
{
    public static void GeneralHttpExtension(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        //Gestione custom del JSON 
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });
    }
}
