using SWCE.Aplicatition.Interfaces.Repositories.API_Interface;
using SWCE.Persistence.ApiClients;
using SWCE.Web1.Interfaces;
using SWCE.Web1.Services;

var builder = WebApplication.CreateBuilder(args);

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];


builder.Services.AddHttpClient("Client", client =>
{
    // Establece la dirección base para todas las peticiones futuras
    client.BaseAddress = new Uri(apiBaseUrl);

});

builder.Services.AddScoped<IAPIUserRepository, APIUserRepository>();
builder.Services.AddScoped<IAPIUserServices, APIUserServices>();

builder.Services.AddScoped<IAPIAddressRepository, APIAddressRepository>();
builder.Services.AddScoped<IAPIAddressServices, APIAddressServices>();

builder.Services.AddScoped<IAPIWishListItemRepository, APIWishListItemRepository>();
builder.Services.AddScoped<IAPIWishListItemServices, APIWishListItemServices>();

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
