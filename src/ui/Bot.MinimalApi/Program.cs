using Bot.MinimalApi.Services;
using Core.Interfaces;
using Core.Repository.Interfaces;
using Core.Repository.Services;
using Core.Services;
using DB.DB;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conString = builder.Configuration.GetConnectionString("Npgsql_ConnecionString");
builder.Services.AddDbContext<ApplicationDbContext>(
	options => options.UseNpgsql(conString));

builder.Services.AddScoped<IProductBaseService, ProductBaseService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITrackProductService, TrackProductService>();


builder.Services.AddScoped<IUpdateHandler, UpdateHandler>();

var token = builder.Configuration["BotToken"]!;             // set your bot token in appsettings.json
var webhookUrl = builder.Configuration["BotWebhookUrl"]!;   // set your bot webhook public url in appsettings.json

//builder.Services.ConfigureTelegramBot<Microsoft.AspNetCore.Http.Json.JsonOptions>(opt => opt.SerializerOptions);
builder.Services.AddHttpClient("tgwebhook").RemoveAllLoggers().AddTypedClient(httpClient => new TelegramBotClient(token, httpClient));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

string[] summaries = [
	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
];

app.MapGet("/weatherforecast", () =>
{
	var forecast = Enumerable.Range(1, 5).Select(index =>
		new WeatherForecast
		(
			DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			Random.Shared.Next(-20, 55),
			summaries[Random.Shared.Next(summaries.Length)]
		))
		.ToArray();
	return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/getall", (IServiceProvider sp) =>
{
	var products = sp.GetRequiredService<IProductService>().GetAllProducts();
	return products;
});

app.MapGet("/bot/setWebhook", async (TelegramBotClient bot) => { await bot.SetWebhookAsync(webhookUrl); Console.WriteLine($"{webhookUrl} url"); return $"Webhook set to {webhookUrl}"; });
app.MapPost("/bot", (Update update, TelegramBotClient bot, IUpdateHandler handler, CancellationToken cts) => OnUpdate);


app.Run();




async void OnUpdate(Update update, TelegramBotClient bot, IUpdateHandler handler, CancellationToken cts)
{
	await handler.HandleUpdateAsync(bot, update, cts);
}


internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


