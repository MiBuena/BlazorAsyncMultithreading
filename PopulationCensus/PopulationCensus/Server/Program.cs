using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using PopulationCensus.Data.DB;
using PopulationCensus.Data.Interfaces;
using PopulationCensus.Data.Repositories;
using PopulationCensus.Data.UnitOfWork;
using PopulationCensus.Server.Interfaces;
using PopulationCensus.Server.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PopulationContext>(options =>
    options.UseSqlServer(
        connectionString: connectionString));

builder.Services.AddTransient(typeof(IAsyncRepository<>), typeof(EfRepository<>));
builder.Services.AddTransient<IFileService, LocalFileService>();
builder.Services.AddTransient<IImportService, ImportService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


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
app.MapFallbackToFile("index.html");

app.Run();
