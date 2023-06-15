using SliceOfItalyAPI.Models.Abstract;

namespace SliceOfItalyAPI.Models;

public class Category : DictionaryTable
{
    public virtual ICollection<Dish>? Dishes { get; set; }
}