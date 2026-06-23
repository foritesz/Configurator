// Client/Program.cs (WASM)
using FrontEnd;
using FrontEnd.Services;
using FrontEnd.Services.CrewServices;
using FrontEnd.Services.TankCalculators;
using FrontEnd.Services.TankCalculators.TierElevenTanks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// A WASM saját originjére állítjuk a BaseAddress-t.
// Így a TanksApi relatív "api/..." URL-t fog használni, és nem kell CORS.
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });



builder.Services.AddScoped<TanksApi>();

builder.Services.AddScoped<TankMechanicRegistry>();

builder.Services.AddScoped<ITankMechanic, StandardTankMechanic>();
builder.Services.AddScoped<ITankMechanic, BlatteMechanic>();

builder.Services.AddScoped<CrewSkillService>();

await builder.Build().RunAsync();
