using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MusicApi.Data;
using MusicApi.Models.DB_Models;
using MusicApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MusicContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));
builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

builder.Services.AddScoped<IRepo<Song>, Repo<Song>>();
builder.Services.AddScoped<IRepo<Artist>, Repo<Artist>>();
builder.Services.AddScoped<IRepo<Album>, Repo<Album>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "Music API",
        Description = "An ASP.NET Core Web API for Interacting with My Music DataBase",
        License = new OpenApiLicense {
            Name = "GNU GPL v3 License",
            Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.en.html#license-text")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
