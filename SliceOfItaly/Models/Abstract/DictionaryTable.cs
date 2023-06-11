using System.ComponentModel.DataAnnotations;

namespace SliceOfItalyAPI.Models.Abstract;

public class DictionaryTable : BaseDataTable
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}