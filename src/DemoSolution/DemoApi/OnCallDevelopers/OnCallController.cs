using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace DemoApi.OnCallDevelopers;

public class OnCallController : ControllerBase
{

    // GET /who-is-on-call
    [HttpGet("who-is-on-call")] // "Attributes" - adds data to something in .net that other code can read
    public ActionResult GetOnCallDeveloper()
    {
        var response = new WhoIsOnCall("Jeff", "330-000-0000", "Jeff@somewhere.com");
        // 200 OK
        // Ok method belongs to ControllerBase
        return Ok(response);
    }
}
public record WhoIsOnCall(string Name, string Phone, string Email);
