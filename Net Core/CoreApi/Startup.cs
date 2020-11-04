using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //se crea la variable Configuration de tipo IConfiguration para la conexion
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Se agrega la version de compatibilidad para que al momento de actualizar la app no exista problemas de compatibilidad
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //2 Add Context 
            //obtine el nombre de Conexion por medio de GetConnectionString
            services.AddDbContext<ApiBaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MiConexion")));


            //3 Añade una politica "" //sirve para que en desarrollo se pueda hacer de cualquir forma la llamada a la api
            services.AddCors(options => {
                options.AddPolicy("CualquierCosa",
                builder => builder.WithOrigins("*")
                                    .WithHeaders("*")
                                    .WithMethods("*")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin());
            });

            //4 Capa de autenticacion JWT 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"], //url configurada en el appsettings
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

        }


        //Metodo que se utiliza para la configuracion de las peticiones http
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

            app.UseCors("CualquierCosa");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();           

            /*app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });*/
        }
    }
}
