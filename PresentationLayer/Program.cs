using AutoMapper;
using BusinessServiceLayer;
using BusinessServiceLayer.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PresentationLayer;
using PresentationLayer.ExceptionHandler;
using RepositoryLayer;
using RepositoryLayer.EntityRepo;
using System.Configuration;
using System.Reflection;
using System.Text.Json.Serialization;

namespace WebAPISample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SchoolContext>(options =>
             {
                 options.UseSqlServer(connectionString, b => b.MigrationsAssembly("PresentationLayer"));
                 options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
             });
            builder.Services.AddExceptionHandler<AppExceptionHandler>();
            builder.Services.AddScoped<StudentRepository>();
            builder.Services.AddScoped<StudentService>();
            builder.Services.AddScoped<StudentAddressService>();
            builder.Services.AddScoped<CourseRepository>();
            builder.Services.AddScoped<CourseService>();
            builder.Services.AddScoped<GradeService>();
            builder.Services.AddScoped<GradeRepository>();
            builder.Services.AddScoped<StudentAddressRepository>();
            builder.Services.AddScoped<StudentInCourseRepository>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My Api", Version = "v1" });
            });

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");
                    c.RoutePrefix = "myschool/swagger";
                });
            }
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseExceptionHandler(_ => { });

            app.MapControllers();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
        }

    }
}