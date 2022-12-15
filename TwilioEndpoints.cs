using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TwimlErrorHandling;

public static class TwilioEndpoints
{
    public static IEndpointRouteBuilder MapTwilioEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.Map("/minimal-message", OnMessage);
        builder.Map("/minimal-voice", OnVoice);
        return builder;
    }
    
    [CatchWithMessageTwiml]
    private static IResult OnMessage()
    {
        var zero = 0;
        var result = 1 / zero;
        return new MessagingResponse()
            .Message($"1/0 is {result}!")
            .ToTwiMLResult();
    }
    
    [CatchWithVoiceTwiml]
    private static IResult OnVoice()
    {
        var zero = 0;
        var result = 1 / zero;
        return new VoiceResponse()
            .Say($"1/0 is {result}!")
            .ToTwiMLResult();
    }
}