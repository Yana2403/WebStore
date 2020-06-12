using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Employees;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlEmployeesData : IEmployeesData
    {
        private readonly WebStoreDB _db;

        public SqlEmployeesData(WebStoreDB db) => _db = db;

        public IEnumerable<Employee> Get() => _db.Employees;

        public Employee GetById(int id) => _db.Employees.Find(id);

        public int Add(Employee Employee)
        {
            if (Employee is null) throw new ArgumentNullException(nameof(Employee));
            if (Employee.Id != 0) throw new InvalidOperationException("Для добавляемого сотрудника вручную задан первичный ключ");

            _db.Employees.Add(Employee);
            return Employee.Id;
        }

        public void Edit(Employee Employee)
        {
            if (Employee is null) throw new ArgumentNullException(nameof(Employee));

            _db.Update(Employee);
        }

        public bool Delete(int id)
        {
            var employee = _db.Employees.FirstOrDefault(e => e.Id == id); //извлечение сущности
            if (employee is null) return false;

            _db.Remove(employee);

            return true;
        }

        public void SaveChanges() => _db.SaveChanges();
    }
}
