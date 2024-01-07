using FunctionApp.IsolatedDemo.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using FunctionApp.IsolatedDemo.Api.Services;
using Microsoft.Extensions.DependencyInjection;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        var configuration = new ConfigurationBuilder()
                 .SetBasePath(Environment.CurrentDirectory)
                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables()
                 .Build();

        services.AddApplication(configuration);
        services.AddInfrastructure(configuration);
        services.AddSingleton<ICauseProblemsWhenLive, RealProblemCauser>();
    })
    .Build();

await host.RunAsync();

/// <summary>
/// For the integration Tests Project
/// </summary>
public partial class Program { }

public class RealProblemCauser : ICauseProblemsWhenLive
{
    public void CauseProblems()
    {
        Console.WriteLine("I am causing problems!");
        throw new Exception("I am causing problems!");
    }
} 