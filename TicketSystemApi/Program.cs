using Applicaction.Category;
using Applicaction.Department;
using Applicaction.Ticket;
using Applicaction.User;
using Domain.Interface.Repositories.Category;
using Domain.Interface.Repositories.Department;
using Domain.Interface.Repositories.Ticket;
using Domain.Interface.Repositories.User;
using Domain.Interface.Services;
using Domain.Interface.UseCase.Category;
using Domain.Interface.UseCase.Department;
using Domain.Interface.UseCase.Ticket;
using Domain.Interface.UseCase.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TicketSystemApi.DB;
using TicketSystemApi.Repositories.Category;
using TicketSystemApi.Repositories.Department;
using TicketSystemApi.Repositories.Ticket;
using TicketSystemApi.Repositories.User;
using TicketSystemApi.Services;
using TicketSystemApi.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirOrigenesEspecificos", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();  
    });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.AddScoped<IUserCase, UserCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITicketCase, TicketCase>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddScoped<IDepartmentCase, DepartmentCase>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<ICategoryCase, CategoryCase>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        // Los valores del Emisor (Issuer) y la Audiencia (Audience)
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,

        // La clave secreta para firmar el token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        RoleClaimType = ClaimTypes.Role
    };
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TicketSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TicketSystemConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("PermitirOrigenesEspecificos");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
