using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Abteilung
    {
        public int Id { get; set; }
        public string Bezeichnung { get; set; }

        public string DispatcherId { get; set; }
        public string Stv_DispatcherId { get; set; }

        public virtual Mandant Mandant { get; set; }
        public virtual ContractUser Dispatcher { get; set; }
        public virtual ContractUser Stv_Dispatcher { get; set; }
        public virtual ICollection<AbteilungToUser> AbteilungToUsers { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Contract> UeContracts { get; set; }
    }
}