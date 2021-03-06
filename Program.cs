using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WarungKuApp.Datas;
using WarungKuApp.Interfaces;
using WarungKuApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<warungkuContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);
#region Bussiness Service Injection
builder.Services.AddScoped<IKategoriService, KategoriProdukService>();
builder.Services.AddScoped<IProdukService, ProdukService>();
builder.Services.AddScoped<IProdKategoriService, ProdKategoriService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IKeranjangService, KeranjangService>();
builder.Services.AddScoped<IAlamatService, AlamatService>();
builder.Services.AddScoped<ITransaksiService, TransaksiService>();
builder.Services.AddScoped<IDetailOrderService, DetailOrderService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IPembayaranService, PembayaranService>();
builder.Services.AddScoped<IPengirimanService, PengirimanService>();
builder.Services.AddScoped<IUlasanService, UlasanService>();

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        options =>
            {
               options.ExpireTimeSpan = TimeSpan.FromDays(365);
               options.SlidingExpiration = true;
               options.AccessDeniedPath = "/Home/Denied";
               options.LoginPath = "/AccountCustomer/Login";
            }
    );

builder.Services.AddDistributedMemoryCache();

#endregion

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

app.Run();
