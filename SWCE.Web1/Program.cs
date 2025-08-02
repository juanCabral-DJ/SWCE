using SWCE.Web.Repositories.Interfaces;
using SWCE.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var EnvioApiBaseUrl = builder.Configuration["ApiSettings:EnvioApiBaseUrl"] ?? throw new InvalidOperationException("EnvioApiBaseUrl is not configured in ApiSettings.");
builder.Services.AddHttpClient<IEnvioHttpService, EnvioHttpService>(client =>
{
    client.BaseAddress = new Uri(EnvioApiBaseUrl);
});



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
