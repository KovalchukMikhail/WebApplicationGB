using System.ComponentModel;

namespace Seminar.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int CategoryID { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Storage> Storages { get; set; } = new List<Storage>();
    }
}
