using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudent() {
            string[] studentname = new string[] { "A", "B", "C" };
            return Ok(studentname);
        }
    }
}
