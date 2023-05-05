using src.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters; 
using System.Text.Json.Serialization;
using src.Services.UserServices;
// using DotNetEnv;

// DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);
// string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")!;
// Console.WriteLine(connectionString);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder => {
    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option => {
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey 
    });
    option.OperationFilter<SecurityRequirementsOperationFilter>();
    option.CustomSchemaIds(type => type.ToString());
});
builder.Services.AddDbContext<DataContext>(options => {
    // options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"));
    options.UseNpgsql(builder.Configuration.GetConnectionString("db"));
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAuthentication().AddJwtBearer(option => {
    option.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("AppSettings:Token")!))
    }; 
});
builder.Services.AddControllers().AddJsonOptions(option => 
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("corsapp");

app.Run();
