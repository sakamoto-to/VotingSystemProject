using Microsoft.EntityFrameworkCore;
using VotingSystem.Application.Services;
using VotingSystem.Infrastructure.Data;
using VotingSystem.Infrastructure.Services;
using VotingSystem.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<VotingDbContext>(options =>
    options.UseSqlite("Data Source=voting.db"));

builder.Services.AddScoped<IElectionService, ElectionService>();
builder.Services.AddScoped<IVotingService, VotingService>();

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

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
