using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.DependencyResolvers.Autofac;
using KUSYS.Initial;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Host
          .UseServiceProviderFactory(new AutofacServiceProviderFactory())
          .ConfigureContainer<ContainerBuilder>(builder =>
          {
              builder.RegisterModule(new AutofacBusinessModule());
          });


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

              .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
         options =>
         {
             options.LoginPath = "/Account";
             options.LogoutPath = "/";
         }
         );
builder.Services.AddHttpContextAccessor();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var authService = app.Services.GetService<IAuthService>();
var roleService = app.Services.GetService<IRoleService>();
var courseService = app.Services.GetService<ICourseService>();

if (authService != null && roleService != null)
{
    IdentityDataInitializer.SeedData(authService, roleService, courseService);
}

app.Run();
