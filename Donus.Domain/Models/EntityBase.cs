using System;

namespace Donus.Domain.Models
{
    // ENTIDADE BASE
    public abstract class EntityBase
    {
        public string Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
