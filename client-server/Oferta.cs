namespace client_server
{
    public class Oferta
    {
        public int id { get; set; }
        public double cena { get; set; }
        public string opis { get; set; }
        public byte[] zdjecie { get; set; }
        public DateTime dataUtworzenia { get; set; }
        public DateTime dataWygasnieia { get; set; }
        public DateTime dataSprzedarzy { get; set; }
        public bool zarezerwowane { get; set; } 
        public String opisRezerwacji { get; set; }
        public bool zakonczona { get; set; }
    }
}