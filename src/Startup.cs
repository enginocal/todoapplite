using System.Collections.Generic;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using TodoAppLite.Common;
using TodoAppLite.Common.Auth;
using TodoAppLite.Repositories;
using TodoAppLite.Services;

namespace TodoAppLite
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
            services.AddJwt(Configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITodoItemRepository,TodoItemRepository>();
            services.AddScoped<IEncrypter, Encrypter>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITodoItemService,TodoItemService>();


            services
                .AddCouchbase(Configuration.GetSection("Couchbase"))
                .AddCouchbaseBucket<ITodoAppBucketProvider>("Todos");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "My ToDo App Case",
                    Version = "v1",
                    Contact = new Contact()
                    {
                        Email = "e.isaocal@gmail.com",
                        Name = "Engin İsa ÖCAL",
                        Url = "https://www.linkedin.com/in/enginocal/"
                    },
                    Description = ""

                });
                
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IApplicationLifetime applicationLifetime)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                app.ApplicationServices.GetRequiredService<ICouchbaseLifetimeService>().Close();
            });
            app.UseMvc();
        }
    }
}
