namespace WebApplication1.Models
{
    public class DU_Relation
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int DepartmentID { get; set; }

        public virtual ContractUser User { get; set; }
        public virtual Department Department { get; set; }
    }
}