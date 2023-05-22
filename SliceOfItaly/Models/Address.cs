using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SliceOfItaly.Models.Abstract;

namespace SliceOfItaly.Models
{
    public class Address : BaseDataTable
    {
        [Required(ErrorMessage = "Address line 1 is required")]
        public string AddressLine1 { get; set; } = default!;
        [Required(ErrorMessage = "Postal code is required")]
        public string PostalCode { get; set; } = default!;
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = default!;
    }
}
