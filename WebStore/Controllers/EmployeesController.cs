using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Domain.Entities.Employees;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModel;

namespace WebStore.Controllers
{
    //[Route("NewRoute/[controller]/123")]
    //[Route("Staff")]
    public class EmployeesController : Controller
    { private readonly IEmployeesData _EmployeesData;
        public EmployeesController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }
        //[Route("List")]
        public IActionResult Index() => View(_EmployeesData.Get());
        //[Route("{id}")] 
        public IActionResult EmployeeDetails(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();//404 ошибка

            return View(employee);
        }
        #region Редактирование

        public IActionResult Edit(int? Id)
        {
            if (Id is null) return View(new EmployeeViewModel());

            if (Id < 0)
                return BadRequest();//ошибка 400

            var employee = _EmployeesData.GetById((int)Id);
            if (employee is null)
                return NotFound(); //404 ошибка

            return View(employee.ToView());
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel Model)
        {
            if (Model is null)
                throw new ArgumentNullException(nameof(Model));
            if (Model.Age < 18 || Model.Age > 75)
                ModelState.AddModelError("Age", "Сотрудник не подходит по возрасту");

            if (Model.Name == "123" && Model.Surname == "QWE")
                ModelState.AddModelError(string.Empty, "Странное сочетание имени и фамилии");

            if (!ModelState.IsValid) //анализ результата валидации
                return View(Model);

            var employee = Model.FromView();

            if (Model.Id == 0)
                _EmployeesData.Add(employee);
            else
                _EmployeesData.Edit(employee);

            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion
        #region Удаление

        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(employee.ToView());
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id) //доступен только через пост запрос
        {
            _EmployeesData.Delete(id);
            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion
    }
}
