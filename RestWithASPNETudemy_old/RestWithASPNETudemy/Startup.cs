using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using RestWithASPNETudemy.Business;
using RestWithASPNETudemy.Business.Implementation;
using RestWithASPNETudemy.Models.Context;
using RestWithASPNETudemy.Repository.Generic;
using RestWithASPNETudemy.Security.Configuration;
using System;

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
                #region Configuração do migrations do Evolve
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
                #endregion
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
                
            services.AddApiVersioning(options => options.ReportApiVersions = true);

            // Register the Swagger generator, defining 1 or more Swagger documents
            // Ref.: https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "RESTful API With ASP.NET Core 2.2",
                    Version = "v1"
                });
            });

            #region Configuração de autenticação com JWT
            //Ref. ok: https://medium.com/@renato.groffe/asp-net-core-2-0-autentica%C3%A7%C3%A3o-em-apis-utilizando-jwt-json-web-tokens-4b1871efd
            var signingConfiguration = new SigningConfiguration();
            services.AddSingleton(signingConfiguration);

            var tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                _configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfiguration);
            services.AddSingleton(tokenConfiguration);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(bearerOptions =>
                {
                    var paramsValidation = bearerOptions.TokenValidationParameters;
                    paramsValidation.IssuerSigningKey = signingConfiguration.Key;
                    paramsValidation.ValidateAudience = tokenConfiguration.Audience;
                    paramsValidation.ValidIssuer = tokenConfiguration.Issuer;

                    // Valida a assinatura de um token recebido
                    paramsValidation.ValidateIssuerSigningKey = true;

                    // Verificação se um token recebido ainda é válido
                    paramsValidation.ValidateLifetime = true;

                    // Tempo de tolerância para expiração de um token 
                    // (utilizado caso haja problemas de sincronismo de horário
                    // entre diferentes computadores envolvidos no processo de comunicação)
                    paramsValidation.ClockSkew = TimeSpan.Zero;
                });

            // Ativa o uso do token como forma de autorizar o acesso 
            // a recursos desse projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });
            #endregion

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

            // Enable middleware to serve generated Swaager as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, CSS, JS, etc)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;   // To serve the Swagger UI at the app's root (http://localhost:<port>/), set the RoutePrefix property to an empty string
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

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
