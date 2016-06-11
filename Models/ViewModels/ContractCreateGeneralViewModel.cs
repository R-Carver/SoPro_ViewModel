using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models.ViewModels
{
    public class ContractCreateGeneralViewModel
    {
        public int Id { get; set; }
        public int ContractId { get; set; }

        public int ContractsKindId { get; set; }
        public int ContractsTypeId { get; set; }

        public IEnumerable<SelectListItem> ContractKinds { get; set; }
        public IEnumerable<SelectListItem> ContractTypes { get; set; }

    }
}