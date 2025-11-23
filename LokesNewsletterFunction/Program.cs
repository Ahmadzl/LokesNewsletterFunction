using LokesNewsletterFunction.Data;
using LokesNewsletterFunction.Models;
using LokesNewsletterFunction.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Host
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, configBuilder) =>
    {
        // local.settings.json
        configBuilder.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        // DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Service
        services.AddScoped<RecipientService>();

        // EmailSettings
        var emailSettingsSection = configuration.GetSection("EmailSettings");

        if (!emailSettingsSection.Exists())
            throw new InvalidOperationException("EmailSettings section not found in configuration.");

        var emailSettings = emailSettingsSection.Get<EmailSettings>()
                            ?? throw new InvalidOperationException("Failed to bind EmailSettings.");

        // EmailService
        services.AddSingleton(new EmailService(
            emailSettings.SmtpHost!,
            emailSettings.SmtpPort,
            emailSettings.SmtpUser!,
            emailSettings.SmtpPass!,
            emailSettings.SenderName!,
            emailSettings.SenderEmail!
        ));
    })
    .Build();

host.Run();
