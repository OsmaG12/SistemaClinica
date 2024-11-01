using Microsoft.EntityFrameworkCore;
using SistemaClinica.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Inyección del contexto para la base de datos
builder.Services.AddDbContext<DBContext>(opt =>
    opt.UseMySql(
        builder.Configuration.GetConnectionString("clinicaConexion"),
        new MySqlServerVersion(new Version(8, 0, 0)) //Version de MySql
    )
);

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
