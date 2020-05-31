using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Surname = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                Age = 53,
                Birthday=new DateTime(1967,07,24 ),
                StartDateofWork=new DateTime(2019,10,01)
            },
            new Employee
            {
                Id = 2,
                Surname = "Петров",
                FirstName = "Пётр",
                Patronymic = "Петрович",
                Age = 25,
                Birthday=new DateTime(1995,03,22),

                StartDateofWork=new DateTime(2019,12,06)
            },
            new Employee
            {
                Id = 3,
                Surname = "Сидоров",
                FirstName = "Сидор",
                Patronymic = "Сидорович",
                Age = 30,
                Birthday=new DateTime(1990,01,16),

                StartDateofWork=new DateTime(2020,02,22)
            },
        };
    }
}
