using eCommerce.Core.Domain;
using eCommerce.Core.Interfaces.RepositoryContracts;
using eCommerce.Core.Interfaces.ServiceContracts;
using eCommerce.Core.Mappings;
using eCommerce.Core.Services;
using eCommerce.Infrastructure.DbContext;
using eCommerce.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


#region DbContext Setup

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity Setup
builder.Services.AddIdentity<Customer, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<Customer, IdentityRole<Guid>, ApplicationDbContext, Guid>>()
    .AddRoleStore<RoleStore<IdentityRole<Guid>, ApplicationDbContext, Guid>>();
#endregion

#region Add Repository

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add AutoMapper with the assembly containing the Profile
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
#endregion


// Add Controllers
builder.Services.AddControllers();

// Configure Swagger for OpenAPI Documentation
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

// Authentication and Authorization Middleware
app.UseAuthentication(); // Ensure authentication happens before authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
