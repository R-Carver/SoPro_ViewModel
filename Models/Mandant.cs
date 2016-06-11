using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Mandant
    {
        public int Id { get; set; }
        public string MandantName { get; set; }
        public int BuchungsKreis { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<MU_Relation> MU_Relations { get; set; }
    }
}