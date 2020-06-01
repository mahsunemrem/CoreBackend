using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // tek katmanda veritaban� ba�lant�s�
        //    services.AddDbContext<SchoolContext>(options =>
        //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            // services.AddControllersWithViews(); haz�r gelmi�ti
            services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


            services.AddSession();
            services.AddDistributedMemoryCache();

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule(),
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt => {
                    opt.LoginPath = "/Auth/Login";
                });


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


            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/products")
            //    {
            //        var value = context.Request.Query["category"].ToString();
            //        if (int.TryParse(value, out int intValue))
            //        {
            //            await context.Response.WriteAsync($"category say�sal bir ifade : {intValue}");
            //        }

            //        else
            //        {
            //            context.Items["value"] = value; // ald���m�z valueyi yine contexte yani kuyru�a ekleyip yola devam et dedik 

            //            await next();  // B�R SONRAK� M�DDLEWARE GE� DEMEK

            //        }

            //    }


            //    else
            //    {
            //        await next();
            //    }
            //});

            //M�DDLEWARE YAZMADAN UFAK VE KOLAY B�R �EK�LDE K���K M�DDLEWAREW DE DEN�LEB�L�R.
            // YA�AM D�NG�S�NE DOKUNUP GELEN VER�LERLE OYNAYIP B�R SONRAK� M�DDLEWARE G�NDERD�K

            //app.Use(async (context, next) =>
            //{
            //    if (context.Items["value"] != null)
            //    {
            //        var value = context.Items["value"].ToString();

            //        context.Items["values"] = value.ToLower();

            //    }

            //    await next();
            //});

            //app.Use(async (context, next) =>
            //{
            //    if (context.Items["value"] != null)
            //    {

            //        await context.Response.WriteAsync($"category : {context.Items["value"].ToString()}");
            //    }


            //});


            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // seed data middleware eklemen laz�m ( db inithalizer yani )
        }
    }
}
