using Microsoft.AspNetCore.Authentication.Cookies;
using Alojat.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Alojat.interfa;
using Alojat.service;
using Alojat.service.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AlquilerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AlquilerDBConexion")));
builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AlquilerDbContext>();


builder.Services.AddControllersWithViews();

//Autenticación por Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/Login/Login";
                    //option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    option.AccessDeniedPath = "/Login/Login";
                });

builder.Services.AddControllers();
builder.Services.AddScoped<ISha, SSha>();
builder.Services.AddScoped<IUsuario, SUsuario>();
builder.Services.AddScoped<ICategoria, SCategoria>();
builder.Services.AddScoped<IRol, SRol>();
builder.Services.AddScoped<IReferencia, SReferencia>();
builder.Services.AddScoped<IUserClaim, SUserClaim>();
builder.Services.AddScoped<IInmueble, SInmueble>();
builder.Services.AddScoped<IVinmueble, SVinmueble>();
builder.Services.AddScoped<IServicio, SServicio>();
builder.Services.AddScoped<IVservicio, SVservicio>();
builder.Services.AddScoped<IBusquedad, SBusquedad>();
builder.Services.AddScoped<IDBusquedad, SDBusquedad>();
builder.Services.AddScoped<IValidarCampos, SValidarCampos>();
builder.Services.AddScoped<IVcategoria, SVcategoria>();
builder.Services.AddScoped<IVpunto, SVpunto>();
builder.Services.AddScoped<IVusuario, SVusuario>();
builder.Services.AddScoped<IVlogin, SVlogin>();
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
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
