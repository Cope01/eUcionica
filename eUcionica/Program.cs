using DataBaseContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(); // Add this line

string databasePath = Path.Combine("..", "Baza.db");
builder.Services.AddDbContext<DB_Context_Class>(options => options.UseSqlite($"Data Source={databasePath}"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); // Add this line

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();