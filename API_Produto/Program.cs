using API_Produto.Context;
using API_Produto.Entities;
using API_Produto.Entities.EntitiesMongoDB;
using API_Produto.ServicesMongoDB;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using static API_Produto.ServicesMongoDB.ProdutoService;

namespace API_Produto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.Configure<ProdutoDatabaseSetting>
            //    (builder.Configuration.GetSection("DevNetStoreDatabase"));

            builder.Services.AddDbContext<ProdutoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoBancoProduto")));

            //builder.Services.AddSingleton<ProdutoService>();

            var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("ConexaoMongoDB"));
            var mongoDatabase = mongoClient.GetDatabase("Banco_Principal_Produtos");
            builder.Services.AddSingleton<IMongoDatabase>(mongoDatabase);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}