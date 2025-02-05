
using Duende.IdentityServer;
using Duende.IdentityServer.Test;
using IdentityServerHost.Quickstart.UI;
using NetCoreSecurity.TokenService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryClients(Config.GetClients())
    .AddTestUsers(TestUsers.Users);
//.AddInMemoryApiResources(Config.GetIdentityResources());
//.AddClientStore<InMemoryClientStore>()


builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
    options.ClientId = "";//from console.developer.google.com
    options.ClientSecret = ""; //from console.developer.google.com
});
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
//app.UseRouting();

//app.UseAuthorization();
app.UseIdentityServer();
app.UseStaticFiles();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();

app.UseMvcWithDefaultRoute();

app.Run();


/*
 run in power shell in project directory
    iex ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/IdentityServer/IdentityServer4.Quickstart.UI/main/getmain.ps1'))

 */