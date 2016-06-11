using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AbteilungToUser
    {
        public int Id { get; set; }
        public int AbteilungsId { get; set; }
        public string UserId { get; set; }

        public virtual Abteilung Abteilung { get; set; }
        public virtual ContractUser User { get; set; }
    }
}