﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using git_statistics_web_app.Services;
using git_statistics_web_app.Services.ExternalServicesProviders;
using git_statistics_web_app.Services.GitService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace git_statistics_web_app
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IGitService, GitService>();
            services.AddSingleton<IGitStatisticsProvider, GitStatisticsProvider>();
			services.AddCors(c =>  {  
			c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());  
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
			app.UseCors(options => options.AllowAnyOrigin());
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
