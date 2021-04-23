using CleanWebApi.Core.Interfaces;
using CleanWebApi.Core.Services;
using CleanWebApi.Infrastructure.Data;
using CleanWebApi.Infrastructure.Filters;
using CleanWebApi.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanWebApi.Api
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
            services.AddControllers();

            //comentado por que se esta cambiando de repositorios separados a uno generico
            //services.AddTransient<IPostRepository, PostRepository>(); //para inyeccion de dependencias del repository en todo el proyecto
            //services.AddTransient<IUserRepository, UserRepository>(); //para inyeccion de dependencias del repository en todo el proyecto
            //services.AddTransient<ICommentRepository, CommentRepository>(); //para inyeccion de dependencias del repository en todo el proyecto
            
            services.AddScoped(typeof(IRepository<>),typeof(BaseRepository<>)); //para inyeccion de dependencias del repository generico en todo el proyecto

            services.AddTransient<IPostService, PostService>(); //para inyeccion de dependencias del service (logica negocio) en todo el proyecto

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebApiCleanCS"))); //para la conexion de EF
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //indica que use los profiles de automapper definidos, se usa los asemblies por ser proyectos separados
            
            services
                .AddMvc(options => { options.Filters.Add<ValidationFilter>(); }) //agregado el filter para validar manualmente el model state
                .AddFluentValidation(options => { options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()); }); // registrando los validators para manejar las restricciones de los dtos y/o entidades, se usa asemblies de dominio por ser proyectos separados

            //services.AddControllers().ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter=true); // sirve para decirle que nosotros validaremos el modelstate por otro lado
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
