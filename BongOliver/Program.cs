using BongOliver.Models;
using BongOliver.Models.Seed;
using BongOliver.Profiles;
using BongOliver.Repositories;
using BongOliver.Repositories.BookingRepository;
using BongOliver.Repositories.HairRepository;
using BongOliver.Repositories.ItemRepository;
using BongOliver.Repositories.NotificationRepository;
using BongOliver.Repositories.OrderRepository;
using BongOliver.Repositories.PaymentRepository;
using BongOliver.Repositories.ProductRepository;
using BongOliver.Repositories.RateRepository;
using BongOliver.Repositories.ServiceRepository;
using BongOliver.Repositories.UserRepository;
using BongOliver.Services;
using BongOliver.Services.AuthService;
using BongOliver.Services.BookingService;
using BongOliver.Services.EmailService;
using BongOliver.Services.HairService;
using BongOliver.Services.ItemService;
using BongOliver.Services.NotificationService;
using BongOliver.Services.OrderService;
using BongOliver.Services.PaymentService;
using BongOliver.Services.ProductService;
using BongOliver.Services.RateService;
using BongOliver.Services.RoleService;
using BongOliver.Services.ServiceService;
using BongOliver.Services.StatisticalService;
using BongOliver.Services.TokenService;
using BongOliver.Services.UserService;
using BongOliver.Services.VnPayService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("Default");


// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));


services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IRoleRepository, RoleRepository>();
services.AddScoped<IBookingRepository, BookingRepository>();
services.AddScoped<IServiceRepository, ServiceRepository>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<IItemRepository, ItemRepository>();
services.AddScoped<IPaymentRepository, PaymentRepository>();
services.AddScoped<IRateService, RateService>();
services.AddScoped<INotificationService, NotificationService>();
services.AddScoped<IHairRepository, HairRepository>();

services.AddScoped<IHairService, HairService>();
services.AddScoped<INotificationRepository, NotificationRepository>();
services.AddScoped<IRateRepository, RateRepository>();
services.AddScoped<IStatisticalService,  StatisticalService>();
services.AddScoped<IVnPayService,  VnPayService>();
services.AddScoped<IPaymentService, PaymentService>();
services.AddScoped<IItemService, ItemService>();
services.AddScoped<IOrderService, OrderService>();
services.AddScoped<IProductService, ProductService>();
services.AddScoped<IServiceService, ServiceService>();
services.AddScoped<IEmailService, EmailService>();
services.AddScoped<IBookingService, BookingService>();
services.AddScoped<ITokenService, TokenService>();
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IRoleService, RoleService>();
services.AddScoped<IUserService, UserService>();

services.AddAutoMapper(typeof(MapperProfile).Assembly);

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]))
        };
    });

services.AddCors(o =>
    o.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("http://localhost:3000")
            .WithOrigins("http://localhost:3001")
            .AllowAnyHeader()
            .AllowAnyMethod()));

var app = builder.Build();

// Add seed data
using var scope = app.Services.CreateScope();
var servicesProvider = scope.ServiceProvider;
try
{
    var context = servicesProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    Seed.SeedUsers(context);
}
catch (Exception e)
{
    var logger = servicesProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "Migration failed");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
