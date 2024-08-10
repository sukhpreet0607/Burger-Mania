using Microsoft.EntityFrameworkCore;
using BurgerManiaServer.Models;
using Microsoft.Extensions.DependencyInjection;
using BurgerManiaServer.Data;

namespace BurgerManiaServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BurgerManiaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BurgerCartContext") ?? throw new InvalidOperationException("Connection string 'BurgerCartContext' not found.")));

            // Add services to the container.
            //builder.Services.AddDbContext<BurgerCartContext>
            //    (opt => );

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
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

            // Use CORS policy
            app.UseCors("AllowAllOrigins");

            app.MapControllers();

            app.Run();
        }
    }
}
