using BlazorApp.Components;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

//BD Connection - Inyección de dependencias
builder.Services.AddScoped<MySqlConnection>(sp =>
    new MySqlConnection(builder.Configuration.GetConnectionString("MySqlConnection"))
);

builder.Services.AddScoped<DatabaseService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
