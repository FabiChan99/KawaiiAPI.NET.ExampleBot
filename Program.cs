using DisCatSharp;
using DisCatSharp.ApplicationCommands;
using DisCatSharp.Enums;
using KawaiiAPI.NET;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ExampleBot;

internal class Bot : ApplicationCommandsModule
{
    private static void Main(string[] args)
    {
        RunBotAsync().GetAwaiter().GetResult();
    }

    private static async Task RunBotAsync()
    {
        string Token = "Discord-Bot-Token"; // Never save your token in your code, use a config file or environment variables instead. This is just an example.

        // Create a new service collection
        ServiceCollection services = new();


        // Register KawaiiClient in the service collection
        services.AddSingleton<KawaiiClient>(sp =>
        {
            var apiKey = "your-token-here"; // Replace with your actual API key // See https://kawaii.red/dashboard
            return new KawaiiClient(apiKey);
        });

        // Build the service provider
        ServiceProvider serviceProvider = services.BuildServiceProvider();

        // Create a new Discord client
        DiscordClient client = new(new DiscordConfiguration
        {
            Token = Token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.All,
            AutoReconnect = true,
            ServiceProvider = serviceProvider,
            MinimumLogLevel = LogLevel.Debug,
        });

        // Register the commands
        var appCommands = client.UseApplicationCommands(new ApplicationCommandsConfiguration
        {
            ServiceProvider = serviceProvider,
        });
        appCommands.RegisterGlobalCommands(Assembly.GetExecutingAssembly());

        // Connect to Discord
        await client.ConnectAsync();

        // Block this task until the program is closed. Prevents the bot from exiting.
        await Task.Delay(-1);
    }
}