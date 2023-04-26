using Innoloft_Test.Data;
using Innoloft_Test.Interfaces;
using Innoloft_Test.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();

//swagger
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Innoloft",
        Version = "v1",
        Description = "API for AS TEST"
    });
    c.EnableAnnotations();
});

//db context
builder.Services.AddEntityFrameworkMySql().AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

    //var dbContext = new AppDbContext();
    //if (dbContext.Database.IsMySql())
    //{
    //    dbContext.Database.Migrate();
    //}
});

// injecting services
builder.Services.AddTransient<IEvent, EventRepository>();
builder.Services.AddHttpClient<IUser, UserRepository>(client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

//caching
builder.Services.AddResponseCaching();

// cors
builder.Services.AddCors(c => c.AddPolicy("Policy", options =>
    options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader()
));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// use cors
app.UseCors("Policy");

//use caching
app.UseResponseCaching();
app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue { MaxAge = TimeSpan.FromSeconds(10), Public = true };
    await next();
});
// Configure the HTTP request pipeline.
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
