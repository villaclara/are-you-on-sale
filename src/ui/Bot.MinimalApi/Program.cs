using Bot.MinimalApi.Interfaces;
using Bot.MinimalApi.Jobs;
using Bot.MinimalApi.Services;
using Bot.MinimalApi.UserCommands;
using Coravel;
using Core.Interfaces;
using Core.Repository.Interfaces;
using Core.Repository.Services;
using Core.Services;
using DB.DB;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
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



//builder.Services.AddSingleton<IBotUpdateHandler, BotUpdateHandler>();
//builder.Services.AddSingleton<IUserCommandFactory, UserCommandFactory>();


builder.Services.AddScoped<IBotUpdateHandler, BotUpdateHandler>();
builder.Services.AddScoped<IUserCommandFactory, UserCommandFactory>();

var token = builder.Configuration["BotToken"]!;             // set your bot token in appsettings.json
var webhookUrl = builder.Configuration["BotWebhookUrl"]!;   // set your bot webhook public url in appsettings.json

builder.Services.ConfigureTelegramBot<Microsoft.AspNetCore.Http.Json.JsonOptions>(opt => opt.SerializerOptions);
builder.Services.AddHttpClient("tgwebhook").RemoveAllLoggers().AddTypedClient(httpClient => new TelegramBotClient(token, httpClient));

builder.Services.AddTransient<RecheckAllProductsJob>();
//builder.Services.AddScoped<RecheckOneProductJob>();

builder.Services.AddScheduler();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();



app.MapGet("/getall", (IServiceProvider sp) =>
{
	app.Logger.LogInformation("getall");
	var products = sp.GetRequiredService<IProductService>().GetAllProducts();
	return products;
});

app.MapGet("/bot/setWebhook", async (TelegramBotClient bot) =>
{
	await bot.SetWebhookAsync(webhookUrl, allowedUpdates: [], dropPendingUpdates: true);
	return $"Webhook set to {webhookUrl}";
});
app.MapPost("/bot", OnUpdate);


//app.Services.UseScheduler(scheduler =>
//{
//	scheduler.Schedule<RecheckAllProductsJob>()
//	.EverySeconds(30)
//	.PreventOverlapping(nameof(RecheckAllProductsJob));

//}).LogScheduledTaskProgress();

await app.RunAsync();




async Task OnUpdate(Update update, TelegramBotClient bot, IBotUpdateHandler handler)
{
	await handler.HandleUpdateAsync(bot, update, new CancellationToken());
	//await bot.SendTextMessageAsync(update.Message.Chat.Id, "text");
}


