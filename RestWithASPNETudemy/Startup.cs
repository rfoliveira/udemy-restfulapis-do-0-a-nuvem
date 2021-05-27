using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementation;
using RestWithASPNETUdemy.Configurations;
using RestWithASPNETUdemy.Hypermedia.Enricher;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Models.Context;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Generic;
using RestWithASPNETUdemy.Repository.Implementation;
using RestWithASPNETUdemy.Security;
using RestWithASPNETUdemy.Security.Implementation;

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
            RegisterTokenConfiguration(services);

            var connectionString = Configuration.GetConnectionString("MySQLConnectionString");

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

            // Para permitir a saída também como xml
            // Para testar, deve-se atribuir no header da requisição como 
            // Accept = "application/xml" (ou Accept = "application/json")
            // --------------------------------------------------------------------------------------------
            // Este método funciona também e diz em sua documentação 
            // que possui o mínimo para executar relacionado ao AspNetMvc e por conter o mínimo,
            // eu fiz a troca de "services.AddMvc(...)" para "services.AddMvcCore(...)".
            // Documentação:
            // -------------
            // Adds the minimum essential MVC services to the specified IServiceCollection. 
            // Additional services including MVC's support for authorization, formatters, 
            // and validation must be added separately using the IMvcCoreBuilder returned from this method.
            services.AddMvcCore(options => 
            {
                // Para que aceite o parâmetro "Accept" no header
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters();

            RegisterHATEOAS(services);

            services.AddApiVersioning();
            
            // Habilitando o CORS
            services.AddCors(options => 
            {
                options.AddDefaultPolicy(builder => 
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RESTful API With ASP.NET Core 5.0", Version = "v1" });
            });

            RegisterDIContainer(services);
        }

        private void RegisterTokenConfiguration(IServiceCollection services)
        {
            var tokenConfiguration = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfiguration")
            )
            .Configure(tokenConfiguration);

            services.AddSingleton(tokenConfiguration);

            services.AddAuthentication(authOptions => 
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions => 
            {
                bearerOptions.TokenValidationParameters = new TokenValidationParameters 
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfiguration.Issuer,
                    ValidAudience = tokenConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret))
                };
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos desse projeto
            services.AddAuthorization(auth => 
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build()
                );
            });
        }

        private void RegisterHATEOAS(IServiceCollection services)
        {
            var filterOptions = new HypermediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());

            services.AddSingleton(filterOptions);
        }

        private void RegisterDIContainer(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ILoginBusiness, LoginBusiness>();
            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IBookBusiness, BookBusiness>();
            services.AddScoped<IFileBusiness, FileBusiness>();

            services.AddTransient<IToken, Token>();
            
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();           
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
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }
    }
}
