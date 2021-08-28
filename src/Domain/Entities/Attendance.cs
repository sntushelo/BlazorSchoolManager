using BlazorSchoolManager.Domain.Contracts;
using System;

namespace BlazorSchoolManager.Domain.Entities
{
    public class Attendance : AuditableEntity<int>
    {
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public bool IsPresent { get; set; }
        public bool IsLate { get; set; }
        public DateTime Date { get; set; }

        public Student Student { get; set; }
        public Lesson Lesson { get; set; }
    }
}
