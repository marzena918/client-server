namespace client_server
{
    public class Oferta
    {
        public double cena { get; set; }
        public string opis { get; set; }
        public byte[] zdjecie { get; set; }
        public DateTime dataUtworzenia { get; set; }
        public DateTime dataWygasnieia { get; set; }
        public bool zarezerwowane { get; set; } 
    }
}