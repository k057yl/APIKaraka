using APIKarakatsiya.Data;
using APIKarakatsiya.Models.Entities;
using APIKarakatsiya.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// =================================================================
// 1. КОНФИГУРАЦИЯ СЕРВИСОВ
// =================================================================

builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- ДОБАВЛЕНИЕ СЕРВИСА CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200",
                               "https://k057yl.github.io")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

// Seed admin и default category
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!await roleManager.RoleExistsAsync("admin"))
        await roleManager.CreateAsync(new IdentityRole("admin"));

    var adminEmail = builder.Configuration["Admin:Email"];
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var admin = new AppUser
        {
            UserName = builder.Configuration["Admin:Username"],
            Email = adminEmail,
            EmailConfirmed = true
        };
        var password = builder.Configuration["Admin:Password"];
        if ((await userManager.CreateAsync(admin, password)).Succeeded)
            await userManager.AddToRoleAsync(admin, "admin");
    }
}

// =================================================================
// 2. КОНФИГУРАЦИЯ MIDDLEWARE
// =================================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.Run();