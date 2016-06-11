using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DispatcherId { get; set; }
        public string StvDispatcherId { get; set; }
        

        public virtual ContractUser Dispatcher { get; set; }
        public virtual ContractUser StvDispatcher { get; set; }
        public virtual Mandant Mandant { get; set; }
        public virtual ICollection<DU_Relation> DU_Realtions { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Contract> UContracts { get; set; }
    }
}