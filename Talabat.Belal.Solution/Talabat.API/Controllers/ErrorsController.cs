using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
    [Route("Errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] // we use this because of we use endpoint without verb
    public class ErrorsController : ControllerBase
    {

        public IActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code , "not valid endpoint"));
        }
    }
}
