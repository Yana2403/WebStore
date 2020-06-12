﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain.Entities.Employees;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<Employee> _Employees = TestData.Employees;

        public int Add(Employee Employee)
        {
            if (Employee is null) //пустой объект
                throw new ArgumentNullException(nameof(Employee));

            if (_Employees.Contains(Employee)) //если уже есть в списке
                return Employee.Id;

            Employee.Id = _Employees.Count == 0 ? 1 : _Employees.Max(e => e.Id) + 1; //назначение индентификатора
            _Employees.Add(Employee);
            return Employee.Id;
        }

        public bool Delete(int id)
        {
            var db_item = GetById(id);
            if (db_item is null)
                return false;

            return _Employees.Remove(db_item);
        }

        public void Edit(Employee Employee)
        {
            if (Employee is null)
                throw new ArgumentNullException(nameof(Employee));

            if (_Employees.Contains(Employee))
                return;

            var db_item = GetById(Employee.Id); //извлечь объект

            db_item.FirstName = Employee.FirstName;
            db_item.Surname = Employee.Surname;
            db_item.Patronymic = Employee.Patronymic;
            db_item.Age = Employee.Age;
        }

        public IEnumerable<Employee> Get() => _Employees;

        public Employee GetById(int id) => _Employees.FirstOrDefault(e => e.Id == id);

        public void SaveChanges()
        {

        }
    }
}
