using BlazorSchoolManager.Domain.Contracts;

namespace BlazorSchoolManager.Domain.Entities
{
    public class Person : AuditableEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
    }
}
