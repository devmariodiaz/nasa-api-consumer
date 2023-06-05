using NasaAPIConsumer.Services;
using NasaAPIConsumer.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IHttpService, HttpService>();
builder.Services.AddTransient<NasaAPIConfigurationsLoader>();
builder.Services.AddTransient<IDefaultHttpClientFactory, DefaultHttpClientFactory>();
builder.Services.AddTransient<INasaAPIService, NasaAPIService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ServiceTool.ServiceProvider = app.Services;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
