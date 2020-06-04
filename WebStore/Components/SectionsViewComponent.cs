using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModel;

namespace WebStore.Components
{
    public class SectionsViewComponent:ViewComponent
    {
        private readonly IProductData _ProductData;

        public SectionsViewComponent(IProductData ProductData)
        {
            _ProductData = ProductData;
        }

        public IViewComponentResult Invoke() => View(GetSections());

        private IEnumerable<SectionViewModel> GetSections() //метод,возвращающий перечисления секций
        {
            var sections = _ProductData.GetSections();

            var parent_sections = sections.Where(s => s.ParentId is null);

            var parent_Sections_views = parent_sections
               .Select(s => new SectionViewModel
               {
                   Id = s.Id,
                   Name = s.Name,
                   Order = s.Order,
               })
               .ToList();

            foreach (var parent_section in parent_Sections_views) //перебор родительских секций
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id); //определение дочерних секций

                foreach (var child_section in childs)
                    //генерация моделей-представления
                    parent_section.ChildSections.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        ParentSection = parent_section
                    });

                parent_section.ChildSections.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order)); //сортировка дочерних секций
            }

            parent_Sections_views.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order)); //сортировка родительских секций

            return parent_Sections_views;
        }
    }
}
