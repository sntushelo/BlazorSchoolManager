using BlazorSchoolManager.Domain.Entities;

namespace BlazorSchoolManager.Application.Features.Lessons.Queries
{
    public static class LessonConverter
    {
        public static GetLessonsResponsePaged ToModel(Lesson entity)
        {
            /**Expression<Func<Lesson, GetLessonsResponsePaged>> expression = e => new GetLessonsResponsePaged
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                VenueId = e.VenueId,
                TeacherId = e.TeacherId
            };**/

            if (entity == null) return null;

            return new GetLessonsResponsePaged()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                TeacherId = entity.TeacherId,
                //Teacher = TeacherConverter.ToModel(entity.Teacher),
                VenueId = entity.VenueId,
                //Venue = VenueConverter.ToModel(entity.Venue),
                Monday = entity.Monday,
                Tuesday = entity.Tuesday,
                Wednesday = entity.Wednesday,
                Thursday = entity.Thursday,
                Friday = entity.Friday,
                Saturday = entity.Saturday,
                Sunday = entity.Sunday
            };
        }

        public static void ToEntity(GetLessonsResponsePaged model, Lesson entity)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.StartTime = model.StartTime;
            entity.EndTime = model.EndTime;
            entity.TeacherId = model.TeacherId;
            entity.VenueId = model.VenueId;
            entity.Monday = model.Monday;
            entity.Tuesday = model.Tuesday;
            entity.Wednesday = model.Wednesday;
            entity.Thursday = model.Thursday;
            entity.Friday = model.Friday;
            entity.Saturday = model.Saturday;
            entity.Sunday = model.Sunday;
        }
    }
}
