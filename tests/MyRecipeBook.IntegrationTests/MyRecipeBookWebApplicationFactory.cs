using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;

namespace MyRecipeBook.IntegrationTests;

public class MyRecipeBookWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer;
    
    public MyRecipeBookWebApplicationFactory()
    {
        _postgresContainer = new PostgreSqlBuilder("postgres:17-alpine")
            .WithDatabase("myrecipebook_db")
            .Build();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Tests")
            .ConfigureAppConfiguration((_, config) =>
            {
                var parameters = new Dictionary<string,string?>
                {
                    ["ConnectionStrings:DbConnection"] = _postgresContainer.GetConnectionString(),
                };
                
                config.AddInMemoryCollection(parameters);
            });
    }

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
       return _postgresContainer.StopAsync();
    }

}