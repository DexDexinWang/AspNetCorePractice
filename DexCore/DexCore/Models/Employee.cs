using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexCore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public bool Fired { get; set; }
        public int GetByDepartmentId { get; internal set; }
    }

    public enum Gender
    {
        Man = 1,
        Woman = 0,
    }
}
