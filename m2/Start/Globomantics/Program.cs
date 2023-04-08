using Globomantics;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Authentication;
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

//builder.Services.AddHttpContextAccessor();
builder.Services
  .AddAuthentication(
    o =>
    {
        o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        // scheme action when trying to access a resource where authentication is required
        //o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    // Scheme name "cookies" will be used if no scheme name is explicitly given
    .AddCookie(o => o.Cookie.SameSite = SameSiteMode.Strict)
    .AddCookie(ExternalAuthenticationDefaults.AuthenticationScheme)
    //.AddTwitter(...)
    .AddGoogle(o =>
  {
      // bad practice to expose the secrets
      o.ClientId = "686977813024-d9i87jqqovj5tu5luks9rk8gl33ck3rb.apps.googleusercontent.com";
      o.ClientSecret = "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";
  });
/*
  .AddCookie(
    o => o.Events = new CookieAuthenticationEvents
    {
        // an event that is raised during cookie authentication to validate the security stamp of a user.
        // in the code below, cookie always reject the principal and sign out the user
        // A use case would be if the user¡¯s account has been compromised or their password has been reset.
        OnValidatePrincipal = 
        async context =>
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync();
        }
    }
    );
*/

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
