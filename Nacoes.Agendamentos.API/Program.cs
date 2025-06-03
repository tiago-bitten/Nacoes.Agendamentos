using Nacoes.Agendamentos.API.IoC;
using Nacoes.Agendamentos.Infra.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAppConfiguration(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddAppHandlers();
builder.Services.AddValidators();
builder.Services.AddFactories();
builder.Services.AddServices();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

var authSettings = builder.Configuration.GetSection("Authentication").Get<AuthenticationSettings>();
builder.Services.AddJwt(authSettings!.Jwt.Issuer, authSettings.Jwt.Audience, authSettings.Jwt.Secret);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
