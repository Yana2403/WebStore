using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebStore.DAL.Context;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;

        public WebStoreDBInitializer(WebStoreDB db, UserManager<User> UserManager, RoleManager<Role> RoleManager)
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
        }

        public void Initialize()
        {
            var db = _db.Database;

            db.Migrate();
            InitializeIdentityAsync().Wait();
            InitializeProducts();
            InitializeEmployees();
            
        }
        private void InitializeEmployees()
        {
            var db = _db.Database;
            if (!_db.Employees.Any())
                using (db.BeginTransaction())
                {
                    var employees = TestData.Employees.ToList();
                    foreach (var employee in employees)
                        employee.Id = 0;

                    _db.Employees.AddRange(employees);

                }
        }
        private async Task InitializeIdentityAsync()
        {
            if (!await _RoleManager.RoleExistsAsync(Role.Administrator)) //если нет  роли администратора то создать
                await _RoleManager.CreateAsync(new Role { Name = Role.Administrator }); 

            if (!await _RoleManager.RoleExistsAsync(Role.User))
                await _RoleManager.CreateAsync(new Role { Name = Role.User });

            if (await _UserManager.FindByNameAsync(User.Administrator) is null) //поиск пользователя - администратора
            {
                var admin = new User { UserName = User.Administrator };

                var create_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (create_result.Succeeded)
                    await _UserManager.AddToRoleAsync(admin, Role.Administrator);
                else
                {
                    var errors = create_result.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Ошибка при создании пользователя-администратора: {string.Join(",", errors)}");
                }
            }
        }
            private void InitializeProducts()
        {
            var db = _db.Database;
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
