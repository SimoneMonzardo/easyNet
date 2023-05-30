using easyNetAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using easyNetAPI.Utility.Services;
using easyNetAPI.Utility;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using easyNetAPI.Models;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Data.Repository;
using easyNetAPI.Controllers;
using Microsoft.Extensions.FileProviders;

var allowedOrigins = "_allowedOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowedOrigins,
        builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("*"));
});

// Add services to the container.

builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
    });
});

#region Connection Strings

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = ServerVersion.AutoDetect(connectionString);
builder.Services.AddDbContext<AppDbContext>(
 dbContextOptions => dbContextOptions
 .UseMySql(connectionString, serverVersion)
 .LogTo(Console.WriteLine, LogLevel.Information)
 .EnableSensitiveDataLogging()
 .EnableDetailedErrors()
);

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDBDatabase"));

#endregion

builder.Services.Configure<EmailSenderOptions>(builder.Configuration.GetSection("EmailSender"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7260",
            ValidAudience = "https://localhost:7260",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("!SomethingSecret!")
            ),
        };
        options.SaveToken = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Moderator", policy => policy.RequireRole(SD.ROLE_MODERATOR));
    options.AddPolicy("CompanyAdmin", policy => policy.RequireRole(SD.ROLE_COMPANY_ADMIN));
    options.AddPolicy("Employee", policy => policy.RequireRole(SD.ROLE_EMPLOYEE));
    options.AddPolicy("User", policy => policy.RequireRole(SD.ROLE_USER));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(allowedOrigins);

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images")),
    RequestPath = new PathString("/wwwroot/images")
});


app.Run();

