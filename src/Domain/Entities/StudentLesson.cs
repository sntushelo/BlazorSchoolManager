using BlazorSchoolManager.Domain.Contracts;

namespace BlazorSchoolManager.Domain.Entities
{
    public class StudentLesson : AuditableEntity<int>
    {
        public int StudentId { get; set; }
        public int LessonId { get; set; }

        public Student Student { get; set; }
        public Lesson Lesson { get; set; }
    }
}
