using ENINET.TransaprentPortal.Persistence;
using ENINET.TransparentPortal.API.Configuration;
using ENINET.TransparentPortal.API.Extension;
using ENINET.TransparentPortal.API.Middleware;
using ENINET.TransparentPortal.API.Services.AuthService;
using ENINET.TransparentPortal.API.Services.Storage;
using Microsoft.EntityFrameworkCore;

namespace ENINET.TransparentPortal.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        // Cors
        string CorsAllowSites = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public void ConfigureServices(IServiceCollection services)
        {


            // Add services to the container.
            services.Configure<PostgresSettings>(_configuration.GetSection("Postgres"));
            services.Configure<CorsSettings>(_configuration.GetSection("CORS"));
            services.Configure<StorageSettings>(_configuration.GetSection("Storage"));
            // Preleviamo la password per la stringa di connessione dalla variabile di ambiente
            var cnstring = _configuration.GetConnectionString("DefaultConnection")!;
            cnstring = cnstring.Replace("$PASSWORD", System.Environment.GetEnvironmentVariable("POSTGRES_DB_PASSWORD"));
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(cnstring));
            services.ConfigureRepositoryManager();
            services.AddScoped<IStorageManager, LocalStorageManager>();
            services.AddScoped<IAuthService, AuthService>();
            //Aggiungiamo le impostazioni per autorizzare l'autorizzazione AD di Azure
            services.ConfigureAzureADAuthorization(_configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PortaleTrasparenza, Version 1" });


            });

            services.AddAutoMapper(typeof(Program));

            services.AddCors(options =>
            {

                options.AddPolicy(name: CorsAllowSites,
                                  policy =>
                                  {
                                      policy.WithOrigins(_configuration.GetValue<string>("CORS:AllowOrigins")!);
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                      policy.AllowCredentials();
                                  });

            });
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Middleware per la gestione degli errori
            app.UseMiddleware<ExceptionHandlingMiddleware>();


            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseCors(CorsAllowSites);
            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();



            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.ApplyMigrations();




        }
    }
}
