namespace BlazorSchoolManager.Application.Features.Attendance.Queries
{
    public static class AttendanceConverter
    {
        public static GetAttendanceResponsePaged ToModel(Domain.Entities.Attendance entity)
        {
            if (entity == null) return null;

            return new GetAttendanceResponsePaged()
            {
                Id = entity.Id,
                LessonId = entity.LessonId,
                StudentId = entity.StudentId,
                IsPresent = entity.IsPresent,
                IsLate = entity.IsLate
            };
        }

        public static void ToEntity(GetAttendanceResponsePaged model, Domain.Entities.Attendance entity)
        {
            entity.LessonId = model.LessonId;
            entity.StudentId = model.StudentId;
            entity.IsPresent = model.IsPresent;
            entity.IsLate = model.IsLate;
        }
    }
}