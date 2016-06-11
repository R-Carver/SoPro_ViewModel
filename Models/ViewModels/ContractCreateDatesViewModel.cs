using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class ContractCreateDatesViewModel
    {
        public int Id { get; set; }
        public int ContractId { get; set; }

        public DateTime ContractBegin { get; set; }
        public DateTime ContractEnd { get; set; }

    }
}