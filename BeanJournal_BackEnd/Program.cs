using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Identity 
builder.Services.AddIdentity<User, Role>(options =>
{
  options.Password.RequiredLength = 5;
  options.Password.RequireNonAlphanumeric = true;
  options.Password.RequireUppercase = true;
  options.Password.RequireLowercase = true;
  options.Password.RequireDigit = true;
  options.Password.RequiredUniqueChars = 1;
})
  .AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders()
  .AddUserStore<UserStore<User, Role, ApplicationDbContext>>()
  .AddRoleStore<RoleStore<Role, ApplicationDbContext>>();

// Add Scoped to Inversion of Control (IoC container) for Services
builder.Services.AddScoped<IDiaryEntryService, DiaryEntryService>();
builder.Services.AddScoped<IEntryTagService, EntryTagService>();
builder.Services.AddScoped<IMediaAttachmentService, MediaAttachmentService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUserService, UserService>();

// Add Scoped to Inversion of Control (IoC container) for Repositories
builder.Services.AddScoped<IDiaryEntryRepository, DiaryEntryRepository>();
builder.Services.AddScoped<IEntryTagRepository, EntryTagRepository>();
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

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
