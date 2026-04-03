using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PayrollEngine.Ui;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// API Base URL (geliştirme ortamında)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5260/") });

// Türkçe kültür ayarları (sayılar: 33.000,12 formatında gösterilecek)
var culture = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await builder.Build().RunAsync();
