using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Logging
builder.Services.AddLogging(loggingBuilder =>
{
  loggingBuilder.AddConsole();
  loggingBuilder.AddDebug();
});

builder.Services.AddControllers(options =>
{
  // Add Authorization Policy
  var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
  options.Filters.Add(new AuthorizeFilter(policy));
});

// Add Json options to have loop reference and handle infinite looping
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Authorization to SwaggerGen
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo()
  {
    Title = "BeanJournal API",
    Version = "v1"
  });
  options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "Bearer"
  });
  options.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        }
      },
      new string[]{}
    }
  });
});

// Add Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Identity 
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
  options.Password.RequiredLength = 5;
  options.Password.RequireNonAlphanumeric = false;
  options.Password.RequireUppercase = false;
  options.Password.RequireLowercase = false;
  options.Password.RequireDigit = false;
  options.Password.RequiredUniqueChars = 0;
})
  .AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders()
  .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext>>()
  .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext>>();

// Add Authentication
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
{
  options.Events = new JwtBearerEvents
  {
    OnTokenValidated = context =>
    {
      // Log the claims in the token
      var claims = context.Principal!.Claims;
      foreach (var claim in claims)
      {
        Console.WriteLine($"{claim.Type}: {claim.Value}");
      }

      return Task.CompletedTask;
    },
    OnAuthenticationFailed = context =>
    {
      Console.WriteLine($"Authentication failed: {context.Exception.Message}");
      return Task.CompletedTask;
    }
  };
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidateAudience = true,
    ValidAudience = builder.Configuration["Jwt:Audience"],
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey
    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
  };
});

// Add Authorization
builder.Services.AddAuthorization(options =>
{

});

// Add Transient to Token Service
builder.Services.AddTransient<ITokenService, TokenService>();

// Add Scoped to Inversion of Control (IoC container) for Services
builder.Services.AddScoped<IDiaryEntryService, DiaryEntryService>();
builder.Services.AddScoped<IMediaAttachmentService, MediaAttachmentService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUserService, UserService>();

// Add Scoped to Inversion of Control (IoC container) for Repositories
builder.Services.AddScoped<IDiaryEntryRepository, DiaryEntryRepository>();
builder.Services.AddScoped<IMediaAttachmentRepository, MediaAttachmentRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//  var request = context.Request;
//  var response = context.Response;

//  Console.Out.WriteLine($"Request: {request.Method} {request.Path}");

//  foreach (var header in request.Headers)
//  {
//    Console.WriteLine($"Header: {header.Key}: {header.Value}");
//  }

//  var authHeader = context.Request.Headers["Authorization"].ToString();
//  Console.WriteLine($"Authorization Header: {authHeader}");

//  await next();

//  Console.WriteLine($"Response: {response.StatusCode}");
//});

app.MapControllers();

app.Run();
