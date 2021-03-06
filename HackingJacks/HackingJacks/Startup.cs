using HackingJacks.Abstract.Repositories;
using HackingJacks.Abstract.Services;
using HackingJacks.Audio.Controllers;
using HackingJacks.Audio.Domain.Repositories.Abstract;
using HackingJacks.Audio.Domain.Repositories.Interfaces;
using HackingJacks.Audio.Services;
using HackingJacks.Audio.Services.Abstract;
using HackingJacks.MedicalEntities.Controllers;
using HackingJacks.MedicalEntities.Services;
using HackingJacks.MedicalText.Controllers;
using HackingJacks.MedicalText.Services;
using HackingJacks.Patient.Controllers;
using HackingJacks.Patients.Domain.Repositories;
using HackingJacks.Patients.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;

namespace HackingJacks
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
            services.AddControllersWithViews().
                AddApplicationPart(Assembly.GetAssembly(typeof(AudioController))).
                AddApplicationPart(Assembly.GetAssembly(typeof(PatientController))).
                AddApplicationPart(Assembly.GetAssembly(typeof(MedicalTextController))).
                AddApplicationPart(Assembly.GetAssembly(typeof(MedicalEntitiesController)));

            services.AddScoped<IAudioService, AudioService>();
            services.AddScoped<IAudioRepository, AudioRepository>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IMedicalTextService, MedicalTextService>();
            services.AddScoped<IMedicalEntityService, MedicalEntityService>();

            services.AddAutoMapper(typeof(Startup));
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
