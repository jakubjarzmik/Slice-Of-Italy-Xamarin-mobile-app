using SliceOfItaly.Models.Abstract;

namespace SliceOfItaly.Models
{
    public class Category : DictionaryTable
    {
        public List<Dish>? Dishes { get; set; }
    }
}
