using System.Collections.Generic;
using SuperHeroCatalogue.Domain.Entities;

namespace SuperHeroCatalogue.Application.Models
{
    public class SuperHeroModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int IdProtectionArea { get; set; }
        public ProtectionArea ProtectionArea { get; set; }
        public IList<SuperPower> SuperPowers { get; set; }
    }
}