using Microsoft.AspNetCore.Mvc;

namespace client_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfertaController : ControllerBase
    {
        private readonly ILogger<OfertaController> _logger;
        private static readonly List<Oferta> _oferty = new List<Oferta>();
        private static int id = 1;

        public OfertaController(ILogger<OfertaController> logger ) {
            _logger = logger;
            
        }

        [HttpGet("/allOffers")]
        public IEnumerable<Oferta> GetAllOffers()
        {
            _logger.LogInformation("test");
            return _oferty;
        }

        [HttpDelete("/usun/{id}")]

        public void usun(int id)
        {
            for(int i = 0; i<_oferty.Count; i++)
            {
                if (_oferty[i].id == id)
                {
                    _oferty.RemoveAt(i);
                    break;
                }
            }
        }

        [HttpPost("/add")]
       public void dodaj([FromBody] Oferta oferta)
        {
            oferta.id = id++;
            oferta.dataUtworzenia = DateTime.Now;
            oferta.zarezerwowane = false;
            oferta.dataWygasnieia = DateTime.Now.AddMonths(2);
            _oferty.Add(oferta);
            _logger.LogInformation(_oferty.ToString());

        }
 

       [HttpGet("/sprzedawca/all-not-closed-offers")]
        public IEnumerable<Oferta> GetAllNotClosedOffers()
        {
            _logger.LogInformation("pobierz wszystkie nie zakoñczone _oferty");

            var niezakoczone = new List<Oferta>();
            var now = DateTime.Now;
            _oferty.ForEach(o =>
            {
                if (o.dataSprzedarzy == null && o.dataWygasnieia > now)
                {
                    niezakoczone.Add(o);
                }
            });
            return niezakoczone;
        }
         

        [HttpPost("/sprzedawca/zarezerwuj/{id}")]
        public string zarezerwuj(int id, [FromQuery] string opisRezerwacji)
        {
            _logger.LogInformation("rezerwacja");

            var oferta = _oferty.Find(oferta => oferta.id == id);
            if (oferta != null)
            {
                oferta.zarezerwowane = true;
                oferta.opisRezerwacji = opisRezerwacji;
                return "OK";
            }
            return "ERROR";
        }

        [HttpPost("/sprzedawca/odrezerwuj/{id}")]
        public string odrezerwuj(int id)
        {
            _logger.LogInformation("odrezerowowywanie");

            var oferta = _oferty.Find(oferta => oferta.id == id);
            if (oferta != null)
            {
                oferta.zarezerwowane = false;
                oferta.opisRezerwacji = "";
                return "OK";
            }
            return "ERROR";
        }

        [HttpPost("/sprzedawca/sprzedaj/{id}")]
        public string sprzedaj(int id )
        {
            _logger.LogInformation("sprzedawanie");

            var oferta = _oferty.Find(oferta => oferta.id == id);
            if (oferta != null && oferta.dataSprzedarzy == null)
            {
                oferta.dataSprzedarzy = DateTime.Now;
                return "OK";
            }
            return "ERROR";

        }

    }
}