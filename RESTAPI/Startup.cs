using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RESTAPI.Data;
using RESTAPI.Repositories;

namespace RESTAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<RESTAPIContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Local")));

            services.AddScoped<IUserRepository, UserRepository>();

            var key = Encoding.ASCII.GetBytes(Configuration["JWTSecret"]);
            services.AddAuthentication(ao =>
                {
                    ao.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    ao.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jbo =>
                {
                    jbo.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var userRepository =
                                context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                            var userId = int.Parse(context.Principal.Identity.Name);
                            var user = userRepository.GetById(userId);
                            if (user == null)
                            {
                                context.Fail("Unauthorised");
                            }

                            return Task.CompletedTask;
                        }
                    };
                    jbo.RequireHttpsMetadata = false;
                    jbo.SaveToken = true;
                    jbo.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(cpb => cpb.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}