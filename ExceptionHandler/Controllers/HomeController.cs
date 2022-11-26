using ExceptionHandler.Exceptions;
using ExceptionHandler.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            var result = new Result.Result { Success = true, Message = "True" };
            throw new Exception();
            return Ok(result);
        }
    }
}
