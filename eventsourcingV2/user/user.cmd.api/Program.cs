using Serilog;
using user.cmd.api;

var builder = WebApplication.CreateBuilder(args);
// Configure the HTTP request pipeline.
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
// Add services to the container.
builder.Services.AddApplication()
    .AddInfrastracture(builder.Configuration)
    .AddApiService();

var app = builder.Build();
app.UseRequestContextLogging();
app.UseSerilogRequestLogging();
app.UseApiServices();
app.Run();


