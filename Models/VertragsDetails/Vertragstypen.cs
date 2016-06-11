using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.VertragsDetails
{
    public class Vertragstypen
    {
        public int Id { get; set; }
        public string Bezeichnung { get; set; }


        public virtual ICollection<Contract> Contracts { get; set; }
    }
}