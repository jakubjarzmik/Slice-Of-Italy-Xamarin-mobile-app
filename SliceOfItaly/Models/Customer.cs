using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SliceOfItaly.Models.Abstract;

namespace SliceOfItaly.Models
{
    public class Customer : BaseDataTable
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = default!;
        [Required(ErrorMessage = "Last name is required")]
        public string  LastName { get; set; } = default!;
        [Required(ErrorMessage = "E-mail is required")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; } = default!;
        public int AddressId { get; set; }
        [ForeignKey("AddressId")] 
        public virtual Address Address { get; set; } = default!;
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
