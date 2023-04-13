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

        [HttpGet("/allOffers")]
        public IEnumerable<Oferta> GetAllOffers()
        {
            _logger.LogInformation("test");

            return oferty;
        }

        [HttpGet("/all-not-closed-offers")]
        public IEnumerable<Oferta> GetAllNotClosedOffers()
        {
            _logger.LogInformation("pobierz wszystkie nie zakoñczone oferty");

            var niezakoczone = new List<Oferta>();
            oferty.ForEach(o =>
            {
                if (!o.zakonczona)
                {
                    niezakoczone.Add(o);
                }
            });
            return niezakoczone;
        }
    }
}