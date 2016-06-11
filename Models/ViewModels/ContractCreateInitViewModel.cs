using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models.ViewModels
{
    public class ContractCreateInitViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public string CoordinatorId { get; set; }

        public virtual ContractUser Coordinator {get; set;}
        public IEnumerable<SelectListItem> Coordinators { get; set; }
    }
}