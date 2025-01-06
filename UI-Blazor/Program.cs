using LukomskiMajorkowski.KeyboardCatalog.UI_Blazor.Components;
using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using LukomskiMajorkowski.KeyboardCatalog.BL;
using LukomskiMajorkowski.KeyboardCatalog.UI_Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DAOService>();
builder.Services.AddAntiforgery();
builder.Services.AddRazorComponents();
builder.Services.AddServerSideBlazor().AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
