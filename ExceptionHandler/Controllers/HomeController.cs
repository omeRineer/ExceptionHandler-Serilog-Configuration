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
        [HttpGet("validationexception")]
        public IActionResult ValidationException()
        {
            throw new ValidationException();
        }

        [HttpGet("argumentnullexception")]
        public IActionResult ArgumentNullException()
        {
            throw new ArgumentNullException();
        }

        [HttpGet("nullreferanceexception")]
        public IActionResult NullReferanceException()
        {
            throw new NullReferenceException();
        }

        [HttpGet("aggregateexception")]
        public IActionResult AggregateException()
        {
            throw new AggregateException();
        }

        [HttpGet("applicationexception")]
        public IActionResult ApplicationException()
        {
            throw new ApplicationException();
        }

        [HttpGet("argumentexception")]
        public IActionResult ArgumentException()
        {
            throw new ArgumentException();
        }
    }
}
