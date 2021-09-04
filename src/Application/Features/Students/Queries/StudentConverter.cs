using BlazorSchoolManager.Domain.Entities;

namespace BlazorSchoolManager.Application.Features.Students.Queries
{
    public static class StudentConverter
    {
        public static GetStudentsResponsePaged ToModel(Student entity)
        {
            if (entity == null) return null;

            return new GetStudentsResponsePaged()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Description = entity.Description,
                Age = entity.Age,
                Gender = entity.Gender
            };
        }

        public static void ToEntity(GetStudentsResponsePaged model, Student entity)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Description = model.Description;
            entity.Age = model.Age;
            entity.Gender = model.Gender;
        }
    }
}