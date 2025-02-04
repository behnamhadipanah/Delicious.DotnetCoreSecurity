using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreSecurity.Models.DataServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StudentDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NetCoreSecurityContext"));
});


builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NetCoreSecurityContext"), optionbuilder =>
    {
        optionbuilder.MigrationsAssembly("NetCoreSecurity");
    });
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();



builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("FacultyOnly",
        policy => policy.RequireClaim("FacultyNumber"));

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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
