using eCommerce.Core.Domain;
using eCommerce.Core.Interfaces.RepositoryContracts;
using eCommerce.Core.Interfaces.ServiceContracts;
using eCommerce.Core.Mappings;
using eCommerce.Core.Services;
using eCommerce.Infrastructure.DbContext;
using eCommerce.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


#region DbContext Setup

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity Setup
builder.Services.AddIdentityCore<Account>()
    .AddRoles<IdentityRole<Guid>>()
    .AddTokenProvider<DataProtectorTokenProvider<Account>>("Sourav Ganguly Provider")
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

#endregion

#region Add Repository

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
// Add AutoMapper with the assembly containing the Profile
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
#endregion

builder.Services.AddHttpContextAccessor();

// Add Controllers
builder.Services.AddControllers();

// Configure Swagger for OpenAPI Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });
#endregion


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
