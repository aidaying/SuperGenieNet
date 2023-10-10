using Genie.Infrastructure.Data;
using GenieAPI.Errors;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace GenieAPI.Controllers;

public class BuggyController : BaseAPIController
{
    private readonly GenieContext _context;

    public BuggyController(GenieContext context)
    {
        _context = context;
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest() 
    {
        var thing = _context.Products.Find(42);
        if (thing == null) 
        {
            return NotFound(new APIResponse(404));
        }
        return Ok();
    }
    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        var thing = _context.Products.Find(42);
        var thingToReturn = thing.ToString();
       
        return Ok();
    }
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new APIResponse(400));
    }
    [HttpGet("badrequest/{id}")]
    public ActionResult GetBadRequest(int id)
    {
        return Ok();
    }
}
