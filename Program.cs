using TwimlErrorHandling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseRouting();

app.MapErrorEndpoint();

app.MapControllers();

app.MapTwilioEndpoints();

app.Run();