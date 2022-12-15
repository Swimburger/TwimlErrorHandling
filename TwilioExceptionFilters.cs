using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TwimlErrorHandling;

internal enum ErrorTwimlType
{
    Message,
    Voice
}

public class GenericErrorTwimlMessage : TypeFilterAttribute
{
    public GenericErrorTwimlMessage() : base(typeof(GenericErrorTwimlExceptionFilter))
    {
        Arguments = new[] {(object) ErrorTwimlType.Message};
    }
}

public class GenericErrorTwimlVoice : TypeFilterAttribute
{
    public GenericErrorTwimlVoice() : base(typeof(GenericErrorTwimlExceptionFilter))
    {
        Arguments = new[] {(object) ErrorTwimlType.Voice};
    }
}

internal class GenericErrorTwimlExceptionFilter : IExceptionFilter
{
    private const string GenericErrorMessage = "An unexpected error occurred. Please try again.";
    private readonly ILogger<GenericErrorTwimlExceptionFilter> logger;
    private readonly ErrorTwimlType twimlType;

    public GenericErrorTwimlExceptionFilter(
        ILogger<GenericErrorTwimlExceptionFilter> logger,
        ErrorTwimlType twimlType
    )
    {
        this.logger = logger;
        this.twimlType = twimlType;
    }

    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "An unhandled exception has occurred while executing the request.");
        switch (twimlType)
        {
            case ErrorTwimlType.Message:
                context.Result = new MessagingResponse()
                    .Message(GenericErrorMessage)
                    .ToTwiMLResult();
                break;
            case ErrorTwimlType.Voice:
                context.Result = new VoiceResponse()
                    .Say(GenericErrorMessage)
                    .ToTwiMLResult();
                break;
        }
    }
}