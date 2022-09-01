using Microsoft.AspNetCore.Mvc;
namespace APICODERHOUSE.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MiscController : ControllerBase
    {
        [HttpGet]
        public string GetName()
        {
            return "Welcome to BuyerHouse:tm:";
        }
    }

}
