namespace WebApplication1.Models
{
    public class MU_Relation
    {
        public int Id { get; set; }
        public int MandantID { get; set; }
        public string UserID { get; set; }

        public virtual Mandant Mandant { get; set; }
        public virtual ContractUser User { get; set; }
    }
}