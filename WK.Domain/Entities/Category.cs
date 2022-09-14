using WK.Domain.Filter;

namespace WK.Domain.Entities
{
    public class Category : BaseFilter
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
