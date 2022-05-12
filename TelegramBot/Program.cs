using TelegramBot;
using TelegramBot.ApplicationFiles.Extensions;
using TelegramBot.ConfigurationModels;
using TelegramBot.DataContext;
using TelegramBot.DataContext.Extensions;
using TelegramBot.Repositories.Extensions;
using TelegramBot.Services.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("ConnectionStringTelegramBotDataBase");
        if (string.IsNullOrEmpty(connectionString))
            throw new ApplicationException("�� ������� ������ �����������");

        // Configurations
        services.RegisterDB(connectionString);
        services.Configure<AppSettings>(context.Configuration.GetSection("ApplicationConfigurations"));
        services.Configure<BotSettings>(context.Configuration.GetSection("BotConfigurations"));

        // System
        services.RegisterRepositories();
        services.RegisterServices();
        services.RegisterApp();

        // Workers
        services.AddHostedService<Worker>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var serviceDB = scope.ServiceProvider.GetService<ApplicationDB>();
    if (serviceDB == null)
        throw new ApplicationException("�� ������� ������� ������ ���� ������");
    serviceDB.Initialize();
}

await host.RunAsync();

