using JobScheduler.Service.Extensoins;
using JobScheduler.Server.Extensions;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddSchedulerService();
builder.Services.AddHangfireService(builder.Configuration);
builder.Services.AddHagfireSettings(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashbordConfig();

app.Services.RunJobStarter();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
