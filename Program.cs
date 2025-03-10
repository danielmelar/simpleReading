using simpleReading.Components;
using simpleReading.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // add servicos do razor

// garante uma instancia para toda a aplicação
builder.Services.AddSingleton<BookService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>() // descobre componentes
    .AddInteractiveServerRenderMode(); // configura renderizacao do lado do servidor
    //.AddInteractiveWebAssymblyRenderMode -> renderiza do lado do clientes

app.Run();
