using Hangfire;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Nacoes.Agendamentos.API;
using Nacoes.Agendamentos.API.Middlewares;
using Nacoes.Agendamentos.Application;
using Nacoes.Agendamentos.Infra;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
       .AddApplication()
       .AddPresentation()
       .AddInfra(builder.Configuration);


var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nacoes.Agendamentos API v1");
        c.RoutePrefix = string.Empty;

        c.DisplayRequestDuration();
    });
}

app.UseHangfireDashboard();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
