using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisioForge.Shared.MediaFoundation.OPM;

namespace WebStore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime StartDateofWork { get; set; }
    }
}
