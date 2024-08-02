using AutoMapper;
using BusinessServiceLayer;
using Microsoft.EntityFrameworkCore;
using PresentationLayer;
using RepositoryLayer;
using System.Configuration;
using System.Reflection;

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
            builder.Services.AddDbContext<SchoolContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<StudentRepository>();
            builder.Services.AddScoped<StudentService>();
            builder.Services.AddScoped<GradeRepository>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My Api", Version = "v1" });
            });
            var mapperConfig = new MapperConfiguration(mc =>
            {
                // Add all generic profiles dynamically
                AddAllGenericMapperConfigs(mc);
            });
            
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

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


            app.MapControllers();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
        }
        private static void AddAllGenericMapperConfigs(IMapperConfigurationExpression mc)
        {
            var profiles = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(MapperConfig<,>));

            foreach (var profile in profiles)
            {
                var genericArguments = profile.BaseType.GetGenericArguments();
                var profileInstance = Activator.CreateInstance(profile.MakeGenericType(genericArguments));
                mc.AddProfile((Profile)profileInstance);
            }
        }
    }
}