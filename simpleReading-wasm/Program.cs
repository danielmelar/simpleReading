using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using simpleReading_wasm;
using simpleReading.Wasm.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<BookService>(); 

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// garante uma instancia para toda a aplicação

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
