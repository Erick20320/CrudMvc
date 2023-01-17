using Microsoft.EntityFrameworkCore;
using Crud.DAL.DataContext;
using Crud.DAL.Repositories;
using Crud.DAL.Models;
using Crud.BLL.Service;
using Crud.DAL.Repositories.Contracts;
using Crud.BLL.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CrudContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion"));
});

// For Identity  
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<CrudContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
    options.Lockout.MaxFailedAccessAttempts = 3;
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(5);
});

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Autenticacion/Login");

builder.Services.AddTransient(typeof(IServicioAutenticacionUsuario), typeof(ServicioAutenticacionUsuario));
builder.Services.AddScoped <IUsuarioService, UsuarioService>();

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddTransient(typeof(IServicioEmail), typeof(ServicioEmail));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddTransient(typeof(IServicioUsuario), typeof(ServicioUsuario));
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.Configure<SMTPConfig>(builder.Configuration.GetSection("SMTPConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Autenticacion}/{action=Login}/{id?}");

app.Run();