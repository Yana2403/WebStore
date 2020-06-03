using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModel
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }


        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя является обязательным")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 40 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Ошибка формата имени")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия является обязательной")]
        [MinLength(3, ErrorMessage = "Длина фамилии должна быть больше 3 символов")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Required]
        [Range(20, 80, ErrorMessage = "Возраст должен быть в пределах от 20 до 80 лет")]
        public int Age { get; set; }

        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Дата приема на работу")]
        public DateTime StartDateofWork { get; set; }
    }
}
