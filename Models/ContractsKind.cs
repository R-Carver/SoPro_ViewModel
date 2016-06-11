using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ContractsKind
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}