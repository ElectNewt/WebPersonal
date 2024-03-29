using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System.Data.Common;
using ROP.ApiExtensions.Translations;
using WebPersonal.BackEnd.API.Filters;
using WebPersonal.BackEnd.API.Middlewares;
using WebPersonal.BackEnd.API.Settings;
using WebPersonal.Backend.EmailService;
using WebPersonal.BackEnd.Model.Repositories;
using WebPersonal.BackEnd.Service.PerfilPersonal;
using WebPersonal.BackEnd.ServiceDependencies.Services.PerfilPersonal;
using WebPersonal.BackEnd.Translations;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.AddTranslation<TraduccionErrores>(services);
            } );
            services.AddDataProtection();

            //Todo:Move this to their respecives projects 
            //Temporal connection until explained different environments.
            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddScoped<DbConnection>(x => new MySqlConnection(Database.BuildConnectionString(Configuration)))
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
            
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<AcceptedLanguageHeader>();
            });
            services.AddHttpContextAccessor();

            services.AddSingleton(x =>
                {
                    EmailConfiguration emailConfiguration = new EmailConfiguration();
                    Configuration.Bind("emailService", emailConfiguration);
                    return emailConfiguration;
                })
                .AddScoped<IEmailSender, EmailSender>();
            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailService"));
            services.PostConfigure<EmailConfiguration>(emailConfiguration =>
            {
                if ( string.IsNullOrWhiteSpace(emailConfiguration.SmtpServer))
                {
                    throw new ApplicationException("el campo SmtpServer debe contner información");
                }
            });
            services.AddScoped<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseMiddleware<CustomHeaderValidatorMiddleware>(AcceptedLanguageHeader.HeaderName);

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
                endpoints.MapControllers();
            });
        }
    }
}