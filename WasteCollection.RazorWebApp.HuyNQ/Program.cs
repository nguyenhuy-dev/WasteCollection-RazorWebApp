using Microsoft.AspNetCore.Authentication.Cookies;
using WasteCollection.RazorWebApp.HuyNQ.Hubs;
using WasteCollection.Services.HuyNQ;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();

// Add Dependency Injection
builder.Services.AddScoped<ICollectorAssignmentsHuyNqService, CollectorAssignmentsHuyNqService>();
builder.Services.AddScoped<ReportsHuyNqService>();
builder.Services.AddScoped<SystemUserAccountService>();

var assemblyService = typeof(IAssemblyReference).Assembly;
// Add AutoMapper profiles
builder.Services.AddAutoMapper(cfg => { }, assemblyService);

builder.Services.AddSignalR();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        long expireTime = long.Parse(configuration["Authentication:CookieAuthTTL"] ?? throw new InvalidDataException("Invalid Authentication:CookieAuthTTL variable environment."));
        options.ExpireTimeSpan = TimeSpan.FromMilliseconds(expireTime);
        options.SlidingExpiration = true;
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapHub<WasteCollectionHub>("/wasteCollectionHub");

app.Run();
