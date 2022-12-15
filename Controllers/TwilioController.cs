using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TwimlErrorHandling.Controllers;

[Route("{controller}/{action}")]
public class TwilioController : Controller
{
    public IActionResult Message()
    {
        var zero = 0;
        var result = 1 / zero;
        return new MessagingResponse()
            .Message($"1/0 is {result}!")
            .ToTwiMLResult();
    }

    public IActionResult Voice()
    {
        var zero = 0;
        var result = 1 / zero;
        return new VoiceResponse()
            .Say($"1/0 is {result}!")
            .ToTwiMLResult();
    }
}