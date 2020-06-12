using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using WebStore.DAL.Context;

namespace WebStore.Data
{ 
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;
        public WebStoreDBInitializer(WebStoreDB db) => _db = db;

        public void Initialize()
        {
            var db = _db.Database;

            db.Migrate();
            if (!_db.Employees.Any())
                using (db.BeginTransaction())
                {
                    var employees = TestData.Employees.ToList();
                    foreach (var employee in employees)
                        employee.Id = 0;

                    _db.Employees.AddRange(employees);

                }

            if (_db.Products.Any()) return; //если в бд есть хоть 1 товар

            using (db.BeginTransaction()) 
            {
                _db.Sections.AddRange(TestData.Sections);//добавление секций в контекст

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductSection] ON");//переключение таблицы в ручной режим
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductSection] OFF");

                db.CommitTransaction();
            }

            using (var transaction = db.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductBrand] ON"); 
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductBrand] OFF");

                transaction.Commit();
            }

            using (var transaction = db.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);


                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                transaction.Commit();
            }
        }
    }

    
}
