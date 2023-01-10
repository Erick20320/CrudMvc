using Microsoft.EntityFrameworkCore;
using Crud.DAL.DataContext;
using Crud.DAL.Repositories;
using Crud.DAL.Models;
using Crud.BLL.Service;
using Crud.DAL.Repositories.Contracts;
using Crud.BLL.Service.Contracts;
using Microsoft.AspNetCore.Identity;

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

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/UserAuthentication/Login");

builder.Services.AddTransient(typeof(IServicioAutenticacionUsuario), typeof(ServicioAutenticacionUsuario));
builder.Services.AddScoped <IUsuarioService, UsuarioService>();

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductoService, ProductoService>();

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
