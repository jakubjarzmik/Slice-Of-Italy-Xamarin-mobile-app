using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SliceOfItalyAPI.Models.Abstract;

namespace SliceOfItalyAPI.Models;

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
    public virtual ICollection<Address> Addresses { get; set; } = default!;
    public virtual ICollection<Order>? Orders { get; set; }
}