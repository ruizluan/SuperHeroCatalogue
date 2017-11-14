namespace SuperHeroCatalogue.Domain.Entities
{
    public class SuperPower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdSuperHero { get; set; }
    }
}
