using Microsoft.EntityFrameworkCore;
using TektonApp.Application;
using TektonApp.Infrastructure;
using TektonApp.Infrastructure.Persistence;
using TektonApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("TektonOrigin",
          builder => builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod());
});

builder.Services.AddDbContext<TektonDbContext>(
    x => x.UseSqlServer(configuration.GetConnectionString("Default"),
    b => b.MigrationsAssembly(typeof(TektonDbContext).Assembly.GetName().ToString()))
);

builder.Services.AddApplication(configuration);
builder.Services.AddInfrastructure(configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwagger();
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("TektonOrigin");
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
