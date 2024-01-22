using Microsoft.EntityFrameworkCore;
using StorageApi.Abstract;
using StorageApi.Data;
using StorageApi.Mapper;
using StorageApi.Mutation;
using StorageApi.Query;
using StorageApi.Services;
using System;

namespace StorageApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("db") ?? throw new InvalidOperationException("Connection string 'db' not found.");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies());

            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddMemoryCache(option => option.TrackStatistics = true);

            builder.Services.AddTransient<IStorageService, StorageService>();
            builder.Services.AddTransient<IProductService, ProductService>();

            builder.Services
                .AddGraphQLServer()
                .AddQueryType<MySimpleQuery>()
                .AddMutationType<MySimpleMutation>();
            //builder.Services.AddAutoMapper(typeof(MappingProfile));
            //builder.Services
            //    .AddGraphQLServer()
            //    .AddQueryType<>();

            var app = builder.Build();

            app.MapGraphQL();

            app.Run();
        }
    }
}
