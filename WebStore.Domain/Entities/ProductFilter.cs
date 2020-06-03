namespace WebStore.Domain.Entities
{
    public class ProductFilter //для возможности фильтрации товаров
    {
        public int? SectionId { get; set; }

        public int? BrandId { get; set; }
    }
}
