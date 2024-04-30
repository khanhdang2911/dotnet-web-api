using dotnet_web_api.Data;
using dotnet_web_api.Interfaces;
using dotnet_web_api.Respository;
using dotnet_web_api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDBContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// jwt
builder.Services.AddAuthentication(options=>{
    options.DefaultAuthenticateScheme=
    options.DefaultChallengeScheme=
    options.DefaultForbidScheme=
    options.DefaultSignInScheme=
    options.DefaultSignOutScheme=JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(
    options =>{
        options.TokenValidationParameters=new TokenValidationParameters()
        {
            ValidateIssuer=true,
            ValidIssuer=builder.Configuration["JWT:Issuer"],
            ValidateAudience=true,
            ValidAudience=builder.Configuration["JWT:Audience"],
            ValidateIssuerSigningKey=true,
            IssuerSigningKey=new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
            )
        };
    }
);
// 
builder.Services.AddScoped<IStockRespository,StockRespository>();
builder.Services.AddScoped<IcommentRespository,CommentRespository>();
builder.Services.AddScoped<ITokenService,TokenService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
