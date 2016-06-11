using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication1.Models
{
    
    public class Contract
    {
        public int Id { get; set; }
        //David: Type of IntContractNum changed to String
        [DisplayName("Int. VertragsNr")]
        public string IntContractNum { get; set; }
        [DisplayName("Ext. VertragNr")]
        public int? ExtContractNum { get; set; }
        [DisplayName("Vertragsbeginn")]
        [DataType(DataType.Date)]
        public DateTime? ContractBegin { get; set; }
        [DisplayName("Vertragsende")]
        [DataType(DataType.Date)]
        public DateTime? ContractEnd { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [DisplayName("Vertragsname")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Vertragsinhaber")]
        public string OwnerId { get; set; }
        [DisplayName("Dispatcher")]
        public string DispatcherId { get; set; }
        [DisplayName("Koordinator")]
        public string CoordinatorId { get; set; }

        [DisplayName("Zugeordnete Abt.")]
        public int? DepartmentId { get; set; }
        [DisplayName("Überwachende Abt.")]
        public int? UDepartmentId { get; set; }
        [DisplayName("Vertragsart")]
        public int? ContractKindId { get; set; }
        [DisplayName("Vertragstyp")]
        public int? ContractTypeId { get; set; }
        [DisplayName("Vertragsstatus")]
        public int? ContractStatusId { get; set; }



        [DisplayName("Vertraginhaber")]
        public virtual ContractUser ContractOwner { get; set; }
        [DisplayName("Dispatcher")]
        public virtual ContractUser Dispatcher { get; set; }
        [DisplayName("Koordinator")]
        public virtual ContractUser Coordinator { get; set; }

        public virtual ContractsKind ContractKind { get; set; }
        public virtual ContractsType ContractType { get; set; }
        public virtual ContractsStatus ContractStatus { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<ContractFile> ContractFiles { get; set; }

        public virtual Department Department { get; set; }
        public virtual Department UDepartment { get; set; }


    }
}