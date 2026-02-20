using Inventario_API_REST.Database;
using Inventario_API_REST.Extensions.Middlewares;
using Inventario_API_REST.Extensions.Registers;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//OPEN API
builder.Services.AddOpenApiDocRegister();

//DB CONTEXT
builder.Services.AddDbContextRegister(builder.Configuration);

//AUTH
builder.Services.JwtAddRegisterConfig(builder.Configuration);
builder.Services.AddAuthorizationAndPolicy();

//CONFIG
builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(Program).Assembly);

//CORS
builder.Services.AddCorsRegister();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();

//AUTO MIGRATIONS
using (var scope = app.Services.CreateScope())
{
    await DbInitializer.SeedDataAsync(scope.ServiceProvider);
}

//MY MIDDLEWARES
app.UseGlobalException();
app.UseLogTime();

//SECURITY MIDDLEWARES
app.UseCors(CorsPolicy.InventaryAPIPolicy);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//RUN SERVER
app.Run();
