using Carter;
using GameShop.API.Queries.DatabaseSeed;
using GameShop.Queries.API;

namespace GameShop.API.Queries;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddQueriesApi(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //------------------------------------------------------
        app.MapCarter(); // Carter will take care of mapping all API routes that are specified in Services

        if (app.Environment.EnvironmentName != "Test")
        {
            DatabaseCreation.CreateDatabase(builder.Configuration);
        }
        //------------------------------------------------------

        app.Run();
    }
}
