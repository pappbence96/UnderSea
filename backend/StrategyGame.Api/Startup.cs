using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StrategyGame.Api.Extensions;
using StrategyGame.Bll.Services.Building;
using StrategyGame.Bll.Services.GameEngineService;
using StrategyGame.Bll.Services.Identity;
using StrategyGame.Bll.Services.Research;
using StrategyGame.Bll.Services.Scoreboard;
using StrategyGame.Bll.Services.Units;
using StrategyGame.Dal;
using StrategyGame.Model.Entities;

namespace StrategyGame.Api
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
            services.AddDbContext<UnderseaDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UnderseaConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
                });
            });

            services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;
            }).AddEntityFrameworkStores<UnderseaDbContext>();


            services.AddTransient<IBuildingService, BuildingService>();
            services.AddTransient<IGameEngineService, GameEngineService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IResearchService, ResearchService>();
            services.AddTransient<IScoreboardService, ScoreboardService>();
            services.AddTransient<IUnitsService, UnitsService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("EnableCORS");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCustomExceptionHandlingMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
