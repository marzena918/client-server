using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace client_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfertaController : ControllerBase
    {
        private readonly ILogger<OfertaController> _logger;
        private static readonly List<Oferta> _oferty = ReadFromJsonFile();
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

            WriteToJsonFile();
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
            WriteToJsonFile();
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
                WriteToJsonFile();
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
                WriteToJsonFile();
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
                WriteToJsonFile();
                return "OK";
            }
            return "ERROR";

        }
        public static void WriteToJsonFile()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(_oferty);
                writer = new StreamWriter("baza.json", false);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        } 
       
        public static List<Oferta> ReadFromJsonFile() 
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader("baza.json");
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Oferta>>(fileContents);
            }
            catch (Exception e)
            {
                return new List<Oferta>();
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

    }
}