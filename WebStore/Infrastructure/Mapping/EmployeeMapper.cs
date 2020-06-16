using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Employees;
using WebStore.ViewModel;

namespace WebStore.Infrastructure.Mapping
{
    public static class EmployeeMapper
    {
        public static EmployeeViewModel ToView(this Employee e) => new EmployeeViewModel
        {
            Id = e.Id,
            Name = e.FirstName,
            Surname = e.Surname,
            Patronymic = e.Patronymic,
            Age = e.Age,
            Birthday = e.Birthday,
            StartDateofWork = e.StartDateofWork

        };

        public static Employee FromView(this EmployeeViewModel e) => new Employee
        {
            Id = e.Id,
            FirstName = e.Name,
            Surname = e.Surname,
            Patronymic = e.Patronymic,
            Age = e.Age,
            Birthday = e.Birthday,
            StartDateofWork = e.StartDateofWork
        };
    }
}
