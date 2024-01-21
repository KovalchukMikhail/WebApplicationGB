
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using WebApplicationGB.Model;
using WebApplicationGB.Repo;

namespace WebApplicationGB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("db") ?? throw new InvalidOperationException("Connection string 'db' not found.");
            builder.Services.AddDbContext<ProductContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies());

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            //{
            //    containerBuilder.RegisterType<ProductRepository>().As<IProductRepository>();
            //});
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddMemoryCache(option => option.TrackStatistics = true);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            string staticFilesPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
            Directory.CreateDirectory(staticFilesPath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesPath), RequestPath = "/static"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
