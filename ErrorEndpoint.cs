using Microsoft.AspNetCore.Diagnostics;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TwimlErrorHandling;

public static class ErrorEndpoint
{
    private const string GenericErrorMessage = "An unexpected error occurred. Please try again.";

    public static IEndpointRouteBuilder MapErrorEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.Map("/error", OnError);
        builder.Map("/error/twiml-message", TwimlMessageError);
        builder.Map("/error/twiml-voice", TwimlVoiceError);
        return builder;
    }

    private static IResult OnError(HttpContext context)
    {
        var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (exceptionFeature?.Endpoint is not null)
        {
            if (exceptionFeature.Endpoint.Metadata.GetMetadata<CatchWithMessageTwimlAttribute>() is not null)
                return TwimlMessageError(context.Response);
            if (exceptionFeature.Endpoint.Metadata.GetMetadata<CatchWithVoiceTwimlAttribute>() is not null)
                return TwimlVoiceError(context.Response);
        }

        return StatusCodeError();
    }

    private static IResult StatusCodeError()
        => Results.StatusCode(StatusCodes.Status500InternalServerError);

    private static IResult TwimlMessageError(HttpResponse response)
    {
        response.StatusCode = StatusCodes.Status200OK;
        return new MessagingResponse()
            .Message(GenericErrorMessage)
            .ToTwiMLResult();
    }

    private static IResult TwimlVoiceError(HttpResponse response)
    {
        response.StatusCode = StatusCodes.Status200OK;
        return new VoiceResponse()
            .Say(GenericErrorMessage)
            .ToTwiMLResult();
    }
}

public class CatchWithMessageTwimlAttribute : Attribute
{
}

public class CatchWithVoiceTwimlAttribute : Attribute
{
}