using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using HotelHub.Application;
using HotelHub.Infrastructure;
using HotelHub.WebAPI.Extrensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (ApiVersionDescription description in app.DescribeApiVersions())
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
    
    app.ApplyMigrations();
    
    // REMARK: Uncomment if you want to seed initial data.
    // app.SeedData();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();

public partial class Program;