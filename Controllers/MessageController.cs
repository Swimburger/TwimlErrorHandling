using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TwimlErrorHandling.Controllers;

//[GenericErrorTwimlMessage]
[CatchWithMessageTwiml]
[Route("message")]
public class MessageController : Controller
{
    [Route("")]
    public IActionResult Message()
    {
        var zero = 0;
        var result = 1 / zero;
        return new MessagingResponse()
            .Message($"1/0 is {result}!")
            .ToTwiMLResult();
    }
}

