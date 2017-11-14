namespace SuperHeroCatalogue.Domain.Entities
{
    public class ProtectionArea
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
        public float Radius { get; set; }
    }
}
