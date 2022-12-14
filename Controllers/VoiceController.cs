using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TwimlErrorHandling.Controllers;

//[GenericErrorTwimlVoice]
[CatchWithVoiceTwiml]
[Route("voice")]
public class VoiceController : TwilioController
{
    [Route("")]
    public IActionResult Voice()
    {
        var zero = 0;
        var result = 1 / zero;
        return new VoiceResponse()
            .Say($"1/0 is {result}!")
            .ToTwiMLResult();
    }
}