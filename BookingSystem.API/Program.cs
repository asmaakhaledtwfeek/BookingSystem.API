using BookingSystem.API.Extensions;
using BookingSystem.API.Middleware;
using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
});

builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddMappings();

await builder.Services.AddRolesAsync();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();



var app = builder.Build();

app.UseMiddleware<ExciptionMiddleware>();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.DocumentTitle = "BookingSystem.API API Documentation V1";
    options.DocExpansion(DocExpansion.None);
    options.DisplayRequestDuration();
});

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(options =>
{
    options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
