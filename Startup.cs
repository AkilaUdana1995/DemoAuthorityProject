using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DemoAuthorityProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllersWithViews();
        //    // services.AddAuthentication().AddCookie();
        //    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        //    {

        //        options.LoginPath = "/login";
        //        options.AccessDeniedPath = "/denied";

        //        options.Events = new CookieAuthenticationEvents()
        //        {
        //            OnSigningIn = async context =>
        //              {
        //                  var principal = context.Principal;
        //                  if(principal.HasClaim(c=>c.Type==ClaimTypes.NameIdentifier))
        //                  {
        //                      if(principal.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier).Value=="AAA")
        //                      {
        //                          var claimsIdentity = principal.Identity as ClaimsIdentity;
        //                          claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
        //                      }
        //                  }
        //                  await Task.CompletedTask;
        //              },

        //            OnSignedIn = async context =>
        //              {
        //                  await Task.CompletedTask;
        //              },

                  
        //             OnValidatePrincipal= async context=>
        //             {
        //                 await Task.CompletedTask;
        //             }

        //        };

        //    }).AddGoogle(options=>
        //        {
        //            options.ClientId = "78010272082-7ru4pa608258v7lfskpfq273ca1l3es7.apps.googleusercontent.com";
        //            options.ClientSecret = "Pg94giXkz_mLAFhi0F1GBBbP";
        //            options.CallbackPath = "/auth";
        //        });

           
        //}



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // services.AddAuthentication().AddCookie();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "GoogleOpenID"; //GoogleDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
            {

                options.LoginPath = "/login";
                options.AccessDeniedPath = "/denied";

            })
                .AddOpenIdConnect("GoogleOpenID", options =>
                {
                options.Authority = "https://accounts.google.com";
                options.ClientId = "78010272082-7ru4pa608258v7lfskpfq273ca1l3es7.apps.googleusercontent.com";
                options.ClientSecret = "Pg94giXkz_mLAFhi0F1GBBbP";
                options.CallbackPath = "/auth";
                options.SaveTokens = true;

                options.Events = new OpenIdConnectEvents()
                {

                    OnTokenValidated = async context =>
                    {
                    if (context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value == "101895140628866277197")
                            {
                                 var claim= new Claim(ClaimTypes.Role, "Admin");
                                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                            claimsIdentity.AddClaim(claim);
                        }
                          //  var claims = context.Principal.Claims;
                        }

                    };
                    //google OpenIdConnect is an extended version of oAuth 2.0
            });
                
            //    .AddGoogle(options =>
            //{
            //    options.ClientId = "78010272082-7ru4pa608258v7lfskpfq273ca1l3es7.apps.googleusercontent.com";
            //    options.ClientSecret = "Pg94giXkz_mLAFhi0F1GBBbP";
            //    options.CallbackPath = "/auth";
            //    options.AuthorizationEndpoint += "?promt=consent";  //this for select user account after logout from the current context page
            //});     //This is google open handler that has made with oauth 2.0


        }
























        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
