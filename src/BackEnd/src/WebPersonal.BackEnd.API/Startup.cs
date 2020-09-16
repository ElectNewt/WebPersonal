using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System.Data.Common;
using WebPersonal.BackEnd.Model.Repositories;
using WebPersonal.BackEnd.Service.PerfilPersonal;
using WebPersonal.BackEnd.ServiceDependencies.Services.PerfilPersonal;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Todo:Move this to their respecives projects 
            //Temporal connection until explained different environments.
            services
                .AddScoped<DbConnection>(x => new MySqlConnection("Server=127.0.0.1;Port=3306;Database=webpersonal;Uid=webpersonaluser;password=webpersonalpass;Allow User Variables=True"))
                .AddScoped<TransactionalWrapper>()
                .AddScoped<PersonalProfile>()
                .AddScoped<PutPersonalProfile>()
                .AddScoped<PostPersonalProfile>()
                .AddScoped<IPostPersonalProfileDependencies, PostPersonalProfileDependencies>()
                .AddScoped<IGetPersonalProfileDependencies, GetPersonalProfileDependencies>()
                .AddScoped<IPutPersonalProfileDependencies, PutPersonalProfileDependencies>()
                .AddScoped<PersonalProfileRepository>()
                .AddScoped<SkillRepository>()
                .AddScoped<InterestsRepository>()
                .AddScoped<UserIdRepository>()
                .AddScoped<AcademicProjectRepository>()
                .AddScoped<EducationRespository>()
                .AddScoped<PersonalProjectsRepository>()
                .AddScoped<WorkProjectRepository>()
                .AddScoped<WorkExpereinceRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
            });
        }
    }
}
