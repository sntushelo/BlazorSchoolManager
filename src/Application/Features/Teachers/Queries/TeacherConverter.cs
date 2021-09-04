using BlazorSchoolManager.Domain.Entities;

namespace BlazorSchoolManager.Application.Features.Teachers.Queries
{
    public static class TeacherConverter
    {
        public static GetTeachersResponsePaged ToModel(Teacher entity)
        {
            if (entity == null) return null;

            return new GetTeachersResponsePaged()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Description = entity.Description,
                Age = entity.Age,
                Gender = entity.Gender
            };
        }

        public static void ToEntity(GetTeachersResponsePaged model, Teacher entity)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Description = model.Description;
            entity.Age = model.Age;
            entity.Gender = model.Gender;
        }
    }
}