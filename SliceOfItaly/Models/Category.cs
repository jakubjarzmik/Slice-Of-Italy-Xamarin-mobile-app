using SliceOfItalyAPI.Models.Abstract;

namespace SliceOfItalyAPI.Models
{
    public class Category : DictionaryTable
    {
        public List<Dish>? Dishes { get; set; }
    }
}
