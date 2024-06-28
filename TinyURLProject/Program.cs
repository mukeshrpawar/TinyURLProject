using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TinyURLProject.Context;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<ApplicationContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("constr")));
//builder.Services.AddScoped<UrlshortenerService>();
builder.Services.AddControllers();
builder.Services.AddCors(x => {
    x.AddPolicy("AllowAll", builder =>
{
    builder.WithOrigins()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
