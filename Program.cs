using Twilio.AspNet.Core;
using Twilio.TwiML;
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

app.MapGet("/minimal-message", [CatchWithMessageTwiml]() =>
{
    var zero = 0;
    var result = 1 / zero;
    return new MessagingResponse()
        .Message($"1/0 is {result}!")
        .ToTwiMLResult();
});

app.Run();