using BlazorSchoolManager.Application.Specifications.Base;
using BlazorSchoolManager.Domain.Entities;

namespace BlazorSchoolManager.Application.Features.Lessons.Queries
{
    public class LessonFilterSpecification : HeroSpecification<Lesson>
    {
        public LessonFilterSpecification(string searchString)
        {
            Includes.Add(l => l.Teacher);
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Teacher != null && (p.Name.Contains(searchString) || p.Description.Contains(searchString));
            }
        }
    }
}