using Microsoft.AspNetCore.Mvc;

namespace client_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfertaController : ControllerBase
    {
        private readonly ILogger<OfertaController> _logger;
        private List<Oferta> oferty = new List<Oferta>();

        public OfertaController(ILogger<OfertaController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/allOferts")]
        public IEnumerable<Oferta> GetAllOfferts()
        {
            _logger.LogInformation("test");
            //test2
            return oferty;
        }
         
    }
}