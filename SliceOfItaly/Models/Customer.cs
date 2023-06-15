using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
    [JsonIgnore]
    public virtual ICollection<Address>? Addresses { get; set; }
    [JsonIgnore]
    public virtual ICollection<Order>? Orders { get; set; }
}