using WasteCollection.RazorWebApp.HuyNQ.Hubs;
using WasteCollection.Services.HuyNQ;

var builder = WebApplication.CreateBuilder(args);

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

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapHub<WasteCollectionHub>("/wasteCollectionHub");

app.Run();
