using System.ComponentModel.DataAnnotations;
using SliceOfItalyAPI.Models.Abstract;

namespace SliceOfItalyAPI.Models;

public class Customer : BaseDataTable
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = default!;
    [Required(ErrorMessage = "E-mail is required")]
    public string Email { get; set; } = default!;
    [Required(ErrorMessage = "Phone is required")]
    public string Phone { get; set; } = default!;
    public virtual ICollection<Address>? Addresses { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
}