using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MandantToUser
    {
        public int Id { get; set; }
        public int MandantId { get; set; }
        public string UserId { get; set; }


        public virtual Mandant Mandant { get; set; }
        public virtual ContractUser Koordinator { get; set; }
    }
}