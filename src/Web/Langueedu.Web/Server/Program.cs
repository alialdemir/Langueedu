var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.AddControllersWithViews();
services.AddRazorPages();
services.AddComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
//app.MapFallbackToFile("index.html");
app.MapFallbackToPage("{*path:nonfile}", "/_Host");
app.MapFallbackToPage("/", "/Landing");

app.Run();

