namespace SuperHeroCatalogue.Application.Models
{
    public class ProtectionAreaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
        public float Radius { get; set; }
    }
}