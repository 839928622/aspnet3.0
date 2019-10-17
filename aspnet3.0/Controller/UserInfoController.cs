using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aspnet3.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        [HttpPost]
        [Route("reg")]
        public async Task<IActionResult> Post()
        {
            return NotFound();
        }
    }
}