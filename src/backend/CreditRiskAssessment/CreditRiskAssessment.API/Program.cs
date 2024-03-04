using CreditRiskAssessment.AppDbContext;
using CreditRiskAssessment.Infrastructure;
using CreditRiskAssessment.Interfaces;
using CreditRiskAssessment.ML.Interfaces;
using CreditRiskAssessment.ML.Services;
using CreditRiskAssessment.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace CreditRiskAssessment.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add CRASDbContext to the container using Azure Sql server
            //builder.Services.AddDbContext<CRASDbContext>(
            //    options => options.UseSqlServer(builder.Configuration.GetConnectionString("crasServer"))
            //    );

            //CONFIGURES IN-MEMORY DB FPR TESTING PURPOSES
            builder.Services.AddDbContext<CRASDbContext>(
                options => options.UseInMemoryDatabase("CRASDB"));

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //REGISTRATION OF SERVICES AND THEIR IMPLEMENTATIONS
            builder.Services.AddScoped<ICRAS_Service, CRASPredictService>();
            builder.Services.AddScoped<ICheckCreditWorthinessService, CheckCreditWorthinessService>();

            //MAKES SERILOG THE HOST LOGGING TOOL
            builder.Host.UseSerilog();

            //SERILOG LOGGING CONFIGURATION
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.File(builder.Configuration.GetValue<string>("Serilog:FilePath"), rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            //REGISTERS SERILOG AS A SINGLETON SERVICE
            builder.Services.AddSingleton(Log.Logger);

            builder.Services.AddSwaggerGen(x =>
            {
                x.EnableAnnotations();
                x.SchemaFilter<SwaggerSchemaExampleFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}