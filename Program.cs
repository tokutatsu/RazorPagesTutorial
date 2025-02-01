using Microsoft.EntityFrameworkCore;
using RazorPagesTutorial.Data;
using RazorPagesTutorial.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<RazorPagesTutorialContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("RazorPagesTutorialContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesTutorialContext' not found.")));
}
else
{
    builder.Services.AddDbContext<RazorPagesTutorialContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionRazorPagesTutorialContext")));
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    SeedData.Initialize(service);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
