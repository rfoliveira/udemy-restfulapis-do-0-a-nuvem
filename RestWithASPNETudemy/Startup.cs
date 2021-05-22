using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementation;
using RestWithASPNETUdemy.Models.Context;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy
{
    public class Startup
    {
        private readonly ILogger _logger;
        
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
            
            var loggerFactory = LoggerFactory.Create(builder => 
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddConsole();
                builder.AddEventSourceLogger();
            });

            _logger = loggerFactory.CreateLogger<Startup>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Senha de root no mysql local = p@$$w0rd
            var connectionString = Configuration.GetConnectionString("MySQLConnectionString");
            _logger.Log(LogLevel.Information, $"ConnectionString = {connectionString}");

            if (Environment.IsDevelopment())
            {
                #region Configuração do migrations do Evolve
                try
                {
                    /*
                        No VS2019
                        ----------
                        O cara do vídeo usou o pomelo para conectar ao mysql porque no início do asp.net core não tinha
                        um driver para mysql nem da M$ nem da oracle (agora tem mas não suporta asp.net core 2.0).
                        Dessa forma, dá erro nesta linha pois o compilador identifica tanto o MySqlConnection, 
                        quanto o MySqlConnector do pomelo.
                        Com isso, foi acrescentado ao csproj um alias para resolver as referencias usando o MySqlConnector.
                        Obs.: isso vai ferrar com as demais referências do projeto, 
                        fechando e abrindo novamente o VS2019, ou ou mudar para o modo pasta e depois retornar para o modo solution, 
                        recarregando o projeto. :-S
                    */
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
                     throw;
                }
                #endregion
            }

            services.AddDbContext<MySQLContext>(options => 
            {
                options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.MySqlServerVersion.LatestSupportedServerVersion);
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RESTful API With ASP.NET Core 2.2", Version = "v1" });
            });

            RegisterDIContainer(services);
        }

        private void RegisterDIContainer(IServiceCollection services)
        {
            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestWithASPNETUdemy v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
