using ASP_Meeting_8.AutoMapperProfiles;
using ASP_Meeting_8.Data.Entities;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(CatProfile), typeof(BreedProfile));
builder.Services.AddDbContext<CatContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("CatsDb")
    ?? throw new InvalidOperationException("Connection string not set!")));
// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();
using (var scope = app.Services.CreateScope()) {

    var serviceProvider = scope.ServiceProvider;
    await SeedData.Initialize(serviceProvider,
        app.Environment);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



app.MapControllerRoute(
    name: "CatsByBreed",
    pattern: "/{action=CatsByBreed}/{breed?}",
    defaults: new { Controller = "Cats" });
    //constraints: new { breed = new AlphaRouteConstraint() });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
