using RunRun.Api.Repositories.v1;
using RunRun.Api.Services.v1;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel();
builder.WebHost.UseUrls("http://localhost:5443");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(c => c.AddPolicy("apiOrigin",
    b => b.WithOrigins("https://mqpgzmdssy.eu-west-1.awsapprunner.com", "https://www.runruntarget.com", "http://runruntarget.com", "http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()));
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<ISmtpClient, SmtpClient>();
builder.Services.AddTransient<ISecretManager, SecretManager>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("apiOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
