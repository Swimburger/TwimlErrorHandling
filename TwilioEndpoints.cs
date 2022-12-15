using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TwimlErrorHandling;

public static class TwilioEndpoints
{
    public static IEndpointRouteBuilder MapTwilioEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.Map("/minimal-message", OnMessage);
        return builder;
    }
    
    [CatchWithMessageTwiml]
    private static IResult OnMessage(HttpContext context)
    {
        var zero = 0;
        var result = 1 / zero;
        return new MessagingResponse()
            .Message($"1/0 is {result}!")
            .ToTwiMLResult();
    }
}