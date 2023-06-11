using System.ComponentModel.DataAnnotations;

namespace SliceOfItalyAPI.Models.Abstract;

public class BaseDataTable
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsActive { get; set; } = true;
}