using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using Udemy.DotNetWebAPI.Data;
using Udemy.DotNetWebAPI.Mappings;
using Udemy.DotNetWebAPI.Repositories;
using Udemy.DotNetWebAPI.Repositories.Implements;
using Udemy.DotNetWebAPI.Repositories.Interfaces;
using Udemy.DotNetWebAPI.Services.Implements;
using Udemy.DotNetWebAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

// serilog
var logger = new LoggerConfiguration().WriteTo.Console().MinimumLevel.Information().CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Udemy.DotNetWebAPI", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "OAuth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
            },
            new string[] { }
        }
    }); ;
});
// repositories
builder.Services.AddDbContext<DotNetDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// services
builder.Services.AddScoped<IUserBizLogic, UserBizLogic>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// role --- identity
builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityAuthDbContext>().AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("DotNetWebAPI").AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 1;
}
);



builder.Services.AddDbContext<IdentityAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure CORS
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(builder => builder.WithOrigins(
    "https://localhost:5000",
    "http://localhost:5000",
    "https://localhost:7000",
    "https://localhost:44407",
    "https://localhost:3000",
    "http://localhost:3000",
    "http://localhost:3002"

    )
    .AllowAnyHeader().AllowAnyMethod().AllowCredentials());
}
else
{
    app.UseCors(builder => builder.WithOrigins(
        "https://localhost:5000",
        "http://localhost:3000",
         "http://localhost:3002",
        "https://localhost:3000")
    .AllowAnyHeader().AllowAnyMethod().AllowCredentials());
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
