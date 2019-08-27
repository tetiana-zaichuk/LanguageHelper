using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageHelper.BusinessLayer.Interfaces;
using LanguageHelper.BusinessLayer.Services;
using LanguageHelper.DataAccessLayer;
using LanguageHelper.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using LanguageHelper.DataAccessLayer.Entities;
using LanguageHelper.Shared.Dtos;

namespace LanguageHelper
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.AddTransient<ISpreadSheetsService, SpreadSheetsService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserLanguageService, UserLanguageService>();
            services.AddTransient<ISheetService, SheetService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<LanguageHelperDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMapper>(m => GetAutoMapperConfig().CreateMapper());
        }

        private MapperConfiguration GetAutoMapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<RoleDto, Role>().ForMember(r => r.Id, o => o.Ignore());
                cfg.CreateMap<Sheet, SheetDto>();
                cfg.CreateMap<SheetDto, Sheet>();
                cfg.CreateMap<UserLanguage, UserLanguageDto>();
                cfg.CreateMap<UserLanguageDto, UserLanguage>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
            });

            return config;
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
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
