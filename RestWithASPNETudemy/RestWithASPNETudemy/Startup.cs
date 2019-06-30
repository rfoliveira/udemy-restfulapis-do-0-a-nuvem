using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using RestWithASPNETudemy.Business;
using RestWithASPNETudemy.Business.Implementation;
using RestWithASPNETudemy.Models.Context;
using RestWithASPNETudemy.Repository.Generic;

namespace RestWithASPNETudemy
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IHostingEnvironment _environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = env;
            _logger = logger;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString("MySQLConnectionString");

            if (_environment.IsDevelopment())
            {
                try
                {
                    // O cara do vídeo usou o pomelo para conectar ao mysql porque no início do asp.net core não tinha
                    // um driver para mysql nem da M$ nem da oracle (agora tem mas não suporta asp.net core 2.0).
                    // Dessa forma, dá erro nesta linha pois o compilador identifica tanto o MySqlConnection, quanto o MySqlConnector do pomelo.
                    // Com isso, foi acrescentado ao csproj um alias para resolver as referencias usando o MySqlConnector.
                    // Obs.: isso vai ferrar com as demais referências do projeto...tem que fechar e abrir novamente 
                    // ou mudar para o modo pasta e depois retornar para o modo solution, recarregando o projeto. :-S
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve(evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new[] { "db/migrations" },
                        IsEraseDisabled = true
                    };

                    evolve.Migrate();
                }
                catch (System.Exception ex)
                {
                    _logger.LogCritical("Database migration failed", ex);
                    throw ex;
                }
            }

            services.AddDbContext<MySQLContext>(options =>
            {
                options.UseMySql(connectionString);
            });

            // Configuração mínima, (para adicionar autenticação, precisa colocar depois...)
            //services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // Configuração normal
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                
            services.AddApiVersioning();

            RegisterDIContainer(services);
        }

        private static void RegisterDIContainer(IServiceCollection services)
        {
            // Nâo funcionou o HATEOAS configurado como o cara do curso falou...
            /*
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);
            */

            //services.AddScoped<IPersonService, PersonMockService>();
            //services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonBusiness, PersonBusiness>();
            //services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
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

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}"
                );
            });
        }
    }
}
