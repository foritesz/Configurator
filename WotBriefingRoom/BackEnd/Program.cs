using BackEnd.ApiClients;
using BackEnd.Models.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
using Serilog.Filters;
using DotNetEnv;
using Microsoft.OpenApi.Models;
using BackEnd.Services.Bonuses;
using BackEnd.Services.TankDataImport;
using BackEnd.Services.TankMatch;
using BackEnd.Services.MongoDb;
using BackEnd.Services.Xml;


Env.Load();

var builder = WebApplication.CreateBuilder(args);



Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.FromSource<TankDataImporter>())
        .WriteTo.File("/app/Logs/TankDataImporter-.txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.FromSource<TanksId>())
        .WriteTo.File("/app/Logs/TanksId-.txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.FromSource<Bonuses>())
        .WriteTo.File("/app/Logs/Bonuses-.txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.FromSource<TankMatcher>())
        .WriteTo.File("/app/Logs/TankMatcher-.txt", rollingInterval: RollingInterval.Day))
    .CreateLogger();

builder.Host.UseSerilog();


builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<MongoDbSettings>(opts =>
{

    builder.Configuration.GetSection("MongoDbSettings").Bind(opts);

    var envConn = builder.Configuration["MONGODB_URI"];
    var envDb = builder.Configuration["MONGODB_DB"];
    if (!string.IsNullOrWhiteSpace(envConn)) opts.ConnectionString = envConn;
    if (!string.IsNullOrWhiteSpace(envDb)) opts.DatabaseName = envDb;
});



builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var s = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(s.ConnectionString);
});
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddCors(o => o.AddPolicy("frontend",
    p => p.WithOrigins("http://localhost:3000")
          .AllowAnyHeader()
          .AllowAnyMethod()));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "BackEnd API", Version = "v1" });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}


app.UseCors("frontend");
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var settings = scope.ServiceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = scope.ServiceProvider.GetRequiredService<IMongoClient>();
    var database = client.GetDatabase(settings.DatabaseName);

    for (var i = 1; i <= 30; i++)
    {
        try
        {
            await database.RunCommandAsync((Command<BsonDocument>)"{ ping: 1 }");
            logger.LogInformation("MongoDB ping OK.");
            break;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "MongoDB még nem elérhető, újrapróbálkozás {i}/30...", i);
            await Task.Delay(2000);
        }
    }
}
/*using (var scope = app.Services.CreateScope())
{
    var mongoContext = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    var loggerTanksId = scope.ServiceProvider.GetRequiredService<ILogger<TanksId>>();
    var loggerTankMatcher = scope.ServiceProvider.GetRequiredService<ILogger<TankMatcher>>();
    var loggerImporter = scope.ServiceProvider.GetRequiredService<ILogger<TankDataImporter>>();
    var loggerBonuses = scope.ServiceProvider.GetRequiredService<ILogger<Bonuses>>();

    var apiKey =
        builder.Configuration["API_KEY"]
        ?? Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new Exception("API_KEY missing");

    var api = new WorldOfTanksApi(apiKey);
    var tankService = new TanksId(api, mongoContext, loggerTanksId);

    var tankIds = await tankService.GetTankIdsAsync();
    var tanksData = await tankService.GetTanksDataAsync(tankIds);

    await tankService.SaveTanksToMongoAsync(tanksData);
    await tankService.SaveTankProfilesToMongoAsync(tankIds);

    var xmlParser = new XmlParser();
    var tankMatcher = new TankMatcher(mongoContext, loggerTankMatcher);
    var importer = new TankDataImporter(xmlParser, tankMatcher, mongoContext, loggerImporter);

    await importer.ImportXmlData("/app/Repositories/TestXml");

    var bonuses = new Bonuses(mongoContext, "/app/Repositories/Bonus", loggerBonuses);
    await bonuses.ImportAll();
}*/

//tankMatcher.ParseAndUpdateTankXmlFields();





app.UseSwagger();
app.UseSwaggerUI();


app.UseRouting();
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();