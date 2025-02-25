using SchedulingService;
using SchedulingService.Services.Implementations;
using SchedulingService.Services.Interfaces;
using SchedulingService.Workers;

var builder = Host.CreateApplicationBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddHostedService<MainWorker>();
builder.Services.AddTransient<IEmailService, EmailService>();

var host = builder.Build();
host.Run();