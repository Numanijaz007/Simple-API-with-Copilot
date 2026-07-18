using UserManagementApi.Middleware;
using UserManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom middleware, in order: logging first so every request is captured,
// then API key auth to gate access to the actual endpoints.
app.UseRequestLogging();
app.UseApiKeyAuth();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
