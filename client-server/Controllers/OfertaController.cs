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
            var now = DateTime.Now;
            oferty.ForEach(o =>
            {
                if (!o.zakonczona && o.dataWygasnieia > now)
                {
                    niezakoczone.Add(o);
                }
            });
            return niezakoczone;
        }
         

        [HttpPost("/zarezerwuj/{id}")]
        public void zarezerwuj(int id, [FromQuery] string opisRezerwacji)
        {
            _logger.LogInformation("rezerwacja");

            var oferta = oferty.Find(oferta => oferta.id == id);
            if (oferta != null)
            {
                oferta.zarezerwowane = true;
                oferta.opisRezerwacji = opisRezerwacji;
            }
        }

        [HttpPost("/sprzedaj/{id}")]
        public string sprzedaj(int id )
        {
            _logger.LogInformation("sprzedawanie");

            var oferta = oferty.Find(oferta => oferta.id == id);
            if (oferta != null && oferta.zakonczona == false)
            {
                oferta.zakonczona = true;
                oferta.dataSprzedarzy = DateTime.Now;
                return "OK";

            }

            return "ERROR";

        }

    }
}