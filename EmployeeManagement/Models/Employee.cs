using System.Diagnostics.CodeAnalysis;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [MaybeNull]
        public string Description { get; set; }

    }
}
