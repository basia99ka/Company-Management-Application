using ServerLibrary.Data;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Helpers;
using BaseLibrary.Contracts;
using ServerLibrary.Repositories.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BaseLibrary.Entities;

using Serilog;
using Serilog.Events;


try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.


    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("Logs/logs.txt",
                  rollingInterval: RollingInterval.Day,
                  restrictedToMinimumLevel: LogEventLevel.Information)
    .WriteTo.File("Logs/errors-.txt",
                  rollingInterval: RollingInterval.Day,
                  restrictedToMinimumLevel: LogEventLevel.Error)
    .CreateLogger();

    builder.Host.UseSerilog();
    builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
    var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

    //Add DataBase
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")??
            throw new InvalidOperationException("Sorry, your connection is not found..."));

    });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSection!.Issuer,
            ValidAudience = jwtSection.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!))
        };
    });



    builder.Services.AddScoped<IUserAccount, UserAccountRepository>();

    builder.Services.AddScoped<IGenericRepositoryInterface<Department>, DepartmentRepository>();
    builder.Services.AddScoped<IGenericRepositoryInterface<Branch>, BranchRepository>();
    builder.Services.AddScoped<IGenericRepositoryInterface<Project>, ProjectRepository>();
    builder.Services.AddScoped<IGenericRepositoryInterface<Employee>, EmployeeRepository>();


    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowBlazorWasm",
            builder => builder
        .WithOrigins("http://localhost:5134", "https://localhost:7134")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
    });





    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowBlazorWasm");
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Aplikacja zakoñczy³a siê z powodu  b³êdu.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
