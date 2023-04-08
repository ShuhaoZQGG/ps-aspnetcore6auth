using Globomantics.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(
   // global authorization for all controllers
   // o => o.Filters.Add(new AuthorizeFilter())
  );
builder.Services.AddRazorPages();

builder.Services.AddScoped<IConferenceRepository, ConferenceRepository>();
builder.Services.AddScoped<IProposalRepository, ProposalRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services
  .AddAuthentication(
    o => o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme
  )
  // Scheme name "cookies" will be used if no scheme name is explicitly given
  .AddCookie();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

// UseAuthentication and UseAuthorization has to be above UseEndpoints. Otherwise endpoints will bypass the Auths
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Conference}/{action=Index}/{id?}");
});

app.Run();
