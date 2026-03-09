using VagasEmpregosAdzuna.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Configura AdzunaSettings
builder.Services.Configure<AdzunaSettings>(
    builder.Configuration.GetSection("Adzuna")
);

// Registra AdzunaClient via HttpClient
builder.Services.AddHttpClient<IJobProvider, AdzunaClient>(client =>
{
    var settings = builder.Configuration.GetSection("Adzuna").Get<AdzunaSettings>();
    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();