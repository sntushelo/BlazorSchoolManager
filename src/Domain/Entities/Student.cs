using System.Collections.Generic;

namespace BlazorSchoolManager.Domain.Entities
{
    public class Student : Person
    {
        public Student()
        {
            Attendance = new HashSet<Attendance>();
        }

        public ICollection<Attendance> Attendance { get; set; }
    }
}
