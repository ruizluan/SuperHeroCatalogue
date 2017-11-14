using System.Collections.Generic;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace SuperHeroCatalogue.Domain.Entities
{
    public class SuperHero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int IdProtectionArea { get; set; }
        [DapperAttribute.Computed]
        public ProtectionArea ProtectionArea { get; set; }
        [DapperAttribute.Computed]
        public IList<SuperPower> SuperPowers { get; set; }
    }
}
