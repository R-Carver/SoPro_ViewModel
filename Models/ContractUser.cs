using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ContractUser : IdentityUser
    {
        public virtual ICollection<Contract> OwnerContracts { get; set; }
        public virtual ICollection<Contract> DispatcherContracts { get; set; }
        public virtual ICollection<Contract> CoordinatorContracts { get; set; }
        public virtual ICollection<DU_Relation> DU_Realtions { get; set; }//User_Abt
        public virtual ICollection<MU_Relation> MU_Relations { get; set; }//User_Mandant
        public virtual ICollection<Department> DispatcherDepartments { get; set; }
        public virtual ICollection<Department> StvDispatcherDepartments { get; set; }

    }
   
}