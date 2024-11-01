using Microsoft.EntityFrameworkCore;
using SistemaClinica.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Inyecci�n del contexto para la base de datos
builder.Services.AddDbContext<DBContext>(opt =>
    opt.UseMySql(
        builder.Configuration.GetConnectionString("clinicaConexion"),
        new MySqlServerVersion(new Version(8, 0, 0)) //Version de MySql
    )
);

// Servicio de sesi�n
builder.Services.AddSession();

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

// Activa el middleware de sesi�n
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

// Indicamos que haremos uso de estos m�todos con la siguiente funci�n
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Inicio}/{action=Index}/{id?}");

app.Run();
